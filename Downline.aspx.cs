using System;
using System.Activities.Expressions;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI;

public partial class Downline : System.Web.UI.Page
{
    SqlConnection conn = new SqlConnection();
    SqlCommand Comm = new SqlCommand();
    SqlDataAdapter Adp;
    DataSet ds = new DataSet();

    private clsGeneral dbGeneral = new clsGeneral();
    private cls_DataAccess dbConnect;

    string strquery;
    string FrmCondition = "";
    int ACnt = 0;
    int BCnt = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        dbConnect = new cls_DataAccess(Application["Connect"].ToString());
        dbConnect.OpenConnection();

        if (Session["Status"] != null && Session["Status"].ToString() == "OK")
        {
            if (!Page.IsPostBack)
            {
                // Initial Load
            }
        }
        else
        {
            Response.Redirect("logout.aspx");
        }
    }

    private void MakeConn()
    {
        conn = new SqlConnection(Application["Connect"].ToString());
        conn.Open();
    }

    private void FillDownlineA()
    {
        try
        {
            dbConnect.OpenConnection();

            string StartDate = txtStartDate.Text == "" ? "01-Jan-2016" : txtStartDate.Text;
            string EndDate = txtEndDate.Text == "" ? DateTime.Now.ToString("dd-MMM-yyyy") : txtEndDate.Text;

            strquery = $"exec sp_ShowDownline {Session["FormNo"]},1,'{StartDate}','{EndDate}','{DDlSearch.SelectedValue}'";

            Comm = new SqlCommand(strquery, dbConnect.cnnObject);
            Adp = new SqlDataAdapter(Comm);
            ds = new DataSet();

            Adp.Fill(ds, "Directs1");

            Session["DirectData1"] = ds.Tables["Directs1"];
            GrdDirects1.DataSource = ds.Tables["Directs1"];
            GrdDirects1.DataBind();

            DivSideA.Style["display"] = "block";
        }
        catch { }
    }

    private void FillDownlineB()
    {
        try
        {
            dbConnect.OpenConnection();

            string StartDate = txtStartDate.Text == "" ? "01-Jan-2016" : txtStartDate.Text;
            string EndDate = txtEndDate.Text == "" ? DateTime.Now.ToString("dd-MMM-yyyy") : txtEndDate.Text;

            strquery = $"exec sp_ShowDownline {Session["FormNo"]},2,'{StartDate}','{EndDate}','{DDlSearch.SelectedValue}'";

            Comm = new SqlCommand(strquery, dbConnect.cnnObject);
            Adp = new SqlDataAdapter(Comm);
            ds = new DataSet();

            Adp.Fill(ds, "Directs2");

            Session["DirectData2"] = ds.Tables["Directs2"];
            GrdDirects2.DataSource = ds.Tables["Directs2"];
            GrdDirects2.DataBind();

            DivSideB.Style["display"] = "block";
        }
        catch { }
    }

    private void FillDownline()
    {
        try
        {
            dbConnect.OpenConnection();

            string Legno = rbleg.SelectedValue != "0" ? rbleg.SelectedValue : "";

            if (Legno == "")
            {
                FillDownlineA();
                FillDownlineB();
            }
            else if (Legno == "1")
            {
                FillDownlineA();
            }
            else if (Legno == "2")
            {
                FillDownlineB();
            }

            Comm.Cancel();
            ds.Dispose();
            RadioButton();
        }
        catch { }
    }

    private void ExportDownlineA()
    {
        try
        {
            DataTable dtTemp = new DataTable();
            dbConnect.OpenConnection();

            string StartDate = txtStartDate.Text == "" ? "01-Jan-2016" : txtStartDate.Text;
            string EndDate = txtEndDate.Text == "" ? DateTime.Now.ToString("dd-MMM-yyyy") : txtEndDate.Text;

            strquery = $"exec sp_ShowDownline {Session["FormNo"]},1,'{StartDate}','{EndDate}','{DDlSearch.SelectedValue}'";

            Comm = new SqlCommand(strquery, dbConnect.cnnObject);
            Adp = new SqlDataAdapter(Comm);
            ds = new DataSet();

            Adp.Fill(ds, "ExportToExcel");

            FillExportTable(dtTemp, ds.Tables["ExportToExcel"]);

            DataGrid dg = new DataGrid();
            dg.DataSource = dtTemp;
            dg.DataBind();

            ExportToExcel("SideADownline.xls", dg);

            Comm.Cancel();
            ds.Dispose();
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message + "Error In Exporting SideA");
        }
    }

    private void ExportDownlineB()
    {
        try
        {
            DataTable dtTemp = new DataTable();
            dbConnect.OpenConnection();

            string StartDate = txtStartDate.Text == "" ? "01-Jan-2016" : txtStartDate.Text;
            string EndDate = txtEndDate.Text == "" ? DateTime.Now.ToString("dd-MMM-yyyy") : txtEndDate.Text;

            strquery = $"exec sp_ShowDownline {Session["FormNo"]},2,'{StartDate}','{EndDate}','{DDlSearch.SelectedValue}'";

            Comm = new SqlCommand(strquery, dbConnect.cnnObject);
            Adp = new SqlDataAdapter(Comm);
            ds = new DataSet();

            Adp.Fill(ds, "ExportToExcel");

            FillExportTable(dtTemp, ds.Tables["ExportToExcel"]);

            DataGrid dg = new DataGrid();
            dg.DataSource = dtTemp;
            dg.DataBind();

            ExportToExcel("SideBDownline.xls", dg);

            Comm.Cancel();
            ds.Dispose();
        }
        catch { }
    }

    private void FillExportTable(DataTable dtTemp, DataTable source)
    {
        dtTemp.Columns.Add("<b>Id No</b>");
        dtTemp.Columns.Add("<b>Member Name</b>");
        dtTemp.Columns.Add("<b>Placement ID</b>");
        dtTemp.Columns.Add("<b>Referal Id</b>");
        dtTemp.Columns.Add("<b>Referal Name</b>");
        dtTemp.Columns.Add("<b>Reward Point</b>");
        dtTemp.Columns.Add("<b>Date Of Joining</b>");
        dtTemp.Columns.Add("<b>Package</b>");
        dtTemp.Columns.Add("<b>Topup Date</b>");
        dtTemp.Columns.Add("<b>Level</b>");

        foreach (DataRow row in source.Rows)
        {
            DataRow dr = dtTemp.NewRow();
            for (int i = 0; i <= 9; i++)
            {
                dr[i] = row[i].ToString();
            }
            dtTemp.Rows.Add(dr);
        }
    }

    private void ExportToExcel(string strFileName, DataGrid dg)
    {
        System.IO.StringWriter sw = new System.IO.StringWriter();
        HtmlTextWriter htw = new HtmlTextWriter(sw);

        Response.Clear();
        Response.Buffer = true;
        Response.ContentType = "application/vnd.xls";
        Response.AddHeader("content-disposition", "attachment;filename=" + strFileName);
        Response.Charset = "";

        dg.EnableViewState = false;
        dg.RenderControl(htw);

        Response.Write(sw.ToString());
        Response.End();
    }

    protected void rbleg_SelectedIndexChanged(object sender, EventArgs e)
    {
        RadioButton();
    }

    private void RadioButton()
    {
        if (rbleg.SelectedIndex == 1)
        {
            DivSideA.Style["display"] = "block";
            DivSideB.Style["display"] = "none";
        }
        else if (rbleg.SelectedIndex == 2)
        {
            DivSideA.Style["display"] = "none";
            DivSideB.Style["display"] = "block";
        }
        else
        {
            DivSideA.Style["display"] = "block";
            DivSideB.Style["display"] = "block";
        }
    }

    protected void BtnExportA_Click(object sender, EventArgs e)
    {
        ExportDownlineA();
    }

    protected void BtnExportB_Click(object sender, EventArgs e)
    {
        ExportDownlineB();
    }

    protected void GrdDirects1_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
    {
        GrdDirects1.CurrentPageIndex = e.NewPageIndex;
        GrdDirects1.DataSource = Session["DirectData1"];
        GrdDirects1.DataBind();
    }

    protected void GrdDirects2_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
    {
        GrdDirects2.CurrentPageIndex = e.NewPageIndex;
        GrdDirects2.DataSource = Session["DirectData2"];
        GrdDirects2.DataBind();
    }

    protected void BtnSearch_Click(object sender, EventArgs e)
    {
        FillDownline();
    }
}
