using DocumentFormat.OpenXml.Drawing;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.WebPages;

public partial class DownlinePurchase : System.Web.UI.Page
{
    SqlConnection Conn;
    SqlCommand Comm;
    SqlDataAdapter Ad;
    DataTable dt;
    DAL obj;
    clsGeneral objGen = new clsGeneral();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            obj = new DAL(Application["Connect"].ToString());

            if (Session["Status"] != null && Session["Status"].ToString() == "OK")
            {
                Conn = new SqlConnection(Application["Connect"].ToString());
                Conn.Open();

                if (!Page.IsPostBack)
                {
                    RbtLegNo.Items[1].Text = "Group A";
                    RbtLegNo.Items[2].Text = "Group B";
                    FillLevel();
                    DdlLevel.SelectedValue = "0";
                    // LevelDetail();
                    FillLevel_Ra();
                }

                // FillTotalAmount();
            }
            else
            {
                Response.Redirect("logout.aspx");
            }
        }
        catch (Exception ex)
        {
            string path = HttpContext.Current.Request.Url.AbsoluteUri;
            string text = path + ":  " + DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss:fff") + Environment.NewLine;

            objGen.WriteToFile(text + ex.Message);
        }
    }
    protected void GrdDirects_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
    {
        try
        {
            GrdDirects.CurrentPageIndex = 0;
            GrdDirects.CurrentPageIndex = e.NewPageIndex;

            GrdDirects.DataSource = Session["DirectData1"];
            GrdDirects.PageSize = Convert.ToInt32(ddlPazeSize.SelectedValue);
            GrdDirects.DataBind();
        }
        catch (Exception ex)
        {
            string path = HttpContext.Current.Request.Url.AbsoluteUri;
            string text = path + ":  " +
                          DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss:fff") +
                          Environment.NewLine;

            objGen.WriteToFile(text + ex.Message);
        }
    }
    protected void DdlLevel_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            LevelDetail();
        }
        catch (Exception ex)
        {
            string path = HttpContext.Current.Request.Url.AbsoluteUri;
            string text = path + ":  " +
                          DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss:fff") +
                          Environment.NewLine;

            objGen.WriteToFile(text + ex.Message);
        }
    }
    protected void BtnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            LevelDetail();
        }
        catch (Exception ex)
        {
            string path = HttpContext.Current.Request.Url.AbsoluteUri;
            string text = path + ":  " +
                          DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss:fff") +
                          Environment.NewLine;

            objGen.WriteToFile(text + ex.Message);
        }
    }
    protected void RbtProduct_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            LevelDetail();
        }
        catch (Exception ex)
        {
            string path = HttpContext.Current.Request.Url.AbsoluteUri;
            string text = path + ":  " +
                          DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss:fff") +
                          Environment.NewLine;

            objGen.WriteToFile(text + ex.Message);
        }
    }
    protected void ddlPazeSize_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            GrdDirects.DataSource = Session["DirectData1"];
            GrdDirects.PageSize = Convert.ToInt32(ddlPazeSize.SelectedValue);
            GrdDirects.DataBind();
        }
        catch (Exception)
        {
            // You may log if needed
        }
    }
    protected void LevelDetail()
    {
        try
        {
            string condition = "";
            string FrmDate = TxtFromDate.Text.Trim();
            string ToDate = TxtToDate.Text.Trim();

            // === Build Condition based on Product Type (R = Repurchase, T = Topup/Registration) ===
            if (DDlSelectType.SelectedValue == "G")
            {
                if (DdlLevel.SelectedValue.AsInt() > 0)
                {
                    condition += $" AND d.MLevel = '{DdlLevel.SelectedValue}'";
                }
            }
            else
            {
                if (RbtLegNo.SelectedValue != "0")
                {
                    condition += $" AND d.LegNo = '{RbtLegNo.SelectedValue}'";
                }
            }
            if (!string.IsNullOrEmpty(FrmDate))
            {
                if (!DateTime.TryParse(FrmDate, out _))
                {
                    RegisterStartupScript("MyAlert", "<script>alert('Check Start Date.. ');</script>");
                    return;
                }
            }

            if (!string.IsNullOrEmpty(ToDate))
            {
                if (!DateTime.TryParse(ToDate, out _))
                {
                    RegisterStartupScript("MyAlert", "<script>alert('Check End Date.. ');</script>");
                    return;
                }
            }

            if (!string.IsNullOrEmpty(FrmDate) && !string.IsNullOrEmpty(ToDate))
            {
                condition += $" AND CAST(CONVERT(VARCHAR, b.BillDate, 106) AS DATE) >= '{FrmDate}'" +
                             $" AND CAST(CONVERT(VARCHAR, b.BillDate, 106) AS DATE) <= '{ToDate}'";
            }

            string str = "";
            string compId = Session["CompID"]?.ToString() ?? "";

            // === REPURCHASE SECTION (RbtProduct = "R") ===
            if (DDlSelectType.SelectedValue == "G")
            {
                if (RbtProduct.SelectedValue == "R")
                {
                    str = " select MLevel as [Level],Replace(Convert(Varchar, b.BillDate, 106), ' ', '-') as [Bill Date], "
 + " a.Idno as [Member ID], (a.MemFirstName + ' ' + a.MemLastName) As [Member Name], "
 + " b.RepurchIncome as BV "
 + " from M_MemberMaster as a with(nolock) "
 + " inner join R_MemTreeRelation as d with(nolock) on a.Formno = d.FormnoDwn "
 + " inner join RepurchIncome as b with(nolock) on a.Formno = b.Formno "
 + " left join TrnOrder as c with(nolock) on b.Formno = c.Formno "
 + "     AND 'Order ' + CAST(OrderNo as nvarchar(100)) = b.BillNo "
 + " left join vedacure..TrnBillMain as s with(nolock) "
 + "     on b.Formno = s.Formno and s.BillNo = b.BillNo "
 + " left join vedacure..DeletedBillMain as Ds "
 + "     on b.Formno = Ds.Formno and Ds.BillNo = b.BillNo "
 + " where d.Formno = '" + Session["Formno"] + "' "
 + condition
 + " and a.Formno = b.Formno and b.BillType in ('R','G') "
 + " Order by b.rectimestamp ";
                }
                else
                {
                    str = " select MLevel as [Level],a.Idno as [Member ID], (a.MemFirstName + ' ' + a.MemLastName) As [Member Name], "
    + " Replace(Convert(Varchar, b.BillDate, 106), ' ', '-') as [Bill Date], "
    + " B.Repurchincome as BV "
    + " from M_MemberMaster as a with(nolock) "
    + " Inner Join R_MemTreeRelation as d with(nolock) on a.Formno = d.FormnoDwn "
    + " Inner Join M_KitMaster as k with(nolock) on k.RowStatus = 'Y', "
    + " RepurchIncome as b with(nolock) "
    + " Left Join TrnOrder as c with(nolock) On c.Formno = b.Formno "
    + "     AND 'Order ' + CAST(OrderNo as nvarchar(100)) = b.BillNo "
    + " Left Join vedacure..TrnBillMain as s with(nolock) "
    + "     On b.Formno = s.Formno and s.BillNo = b.BillNo "
    + " where b.Kitid = k.KitId "
    + "     and d.Formno = '" + Session["Formno"] + "' "
    + condition + " and "
    + " a.Formno = b.Formno and b.BillType not in ('R','G') "
    + " Order by b.rectimestamp ";
                }

            }
            else
            {
                if (RbtProduct.SelectedValue == "A")
                {
                    str = " select a.Idno as [Member ID], (a.MemFirstName + ' ' + a.MemLastName) As [Member Name], "
      + " Case when d.LegNo = 1 then 'Group A' else 'Group B' end as [Group Name], "
      + " Replace(Convert(Varchar, b.BillDate, 106), ' ', '-') as [Bill Date], "
      + " B.Repurchincome as BV "
      + " from M_MemberMaster as a with(nolock) "
      + " Inner Join M_MemTreeRelation as d with(nolock) on a.Formno = d.FormnoDwn "
      + " Inner Join M_KitMaster as k with(nolock) on k.RowStatus = 'Y', "
      + " RepurchIncome as b with(nolock) "
      + " Left Join TrnOrder as c with(nolock) On c.Formno = b.Formno "
      + "     AND 'Order ' + CAST(OrderNo as nvarchar(100)) = b.BillNo "
      + " Left Join vedacure..TrnBillMain as s with(nolock) "
      + "     On b.Formno = s.Formno and s.BillNo = b.BillNo "
      + " where b.Kitid = k.KitId "
      + "     and d.Formno = '" + Session["Formno"] + "' "
      + condition + " and "
      + " a.Formno = b.Formno and b.BillType not in ('R','G') "
      + " Order by b.rectimestamp ";
                }
                else
                {
                    str = " select a.Idno as [Member ID], (a.MemFirstName + ' ' + a.MemLastName) As [Member Name], "
      + " Case when d.LegNo = 1 then 'Group A' else 'Group B' end as [Group Name], "
      + " Replace(Convert(Varchar, b.BillDate, 106), ' ', '-') as [Bill Date], "
      + " B.Repurchincome as BV "
      + " from M_MemberMaster as a with(nolock) "
      + " Inner Join M_MemTreeRelation as d with(nolock) on a.Formno = d.FormnoDwn "
      + " Inner Join M_KitMaster as k with(nolock) on k.RowStatus = 'Y', "
      + " RepurchIncome as b with(nolock) "
      + " Left Join TrnOrder as c with(nolock) On c.Formno = b.Formno "
      + "     AND 'Order ' + CAST(OrderNo as nvarchar(100)) = b.BillNo "
      + " Left Join vedacure..TrnBillMain as s with(nolock) "
      + "     On b.Formno = s.Formno and s.BillNo = b.BillNo "
      + " where b.Kitid = k.KitId "
      + "     and d.Formno = '" + Session["Formno"] + "' "
      + condition + " and "
      + " a.Formno = b.Formno and b.BillType not in ('R','G') "
      + " Order by b.rectimestamp ";
                }


            }
            DataTable dt = new DataTable();
            var dal = new DAL(Application["Connect"].ToString());
            dt = dal.GetData(str);

            Session["DirectData1"] = dt;
            GrdDirects.DataSource = dt;
            LblttlRcd.Text = dt.Rows.Count.ToString();
            LblttlRcd1.Text = dt.Rows.Count.ToString();
            GrdDirects.PageSize = Convert.ToInt32(ddlPazeSize.SelectedValue);
            GrdDirects.DataBind();
            if (DDlSelectType.SelectedValue == "M")
            {
                divR.Visible = false;
                divT.Visible = true;
                lblleftbv.Visible = true;
                lblbv.Visible = true;
                ShowBV(dt);
            }
            else // Repurchase
            {
                divR.Visible = true;
                divT.Visible = false;
                ShowTotalBV(dt);
                // CalculateLeftRightBV(dt, "BV");
            }
            if (dt.Columns.Contains("BV"))
                dt.Columns["BV"].ColumnName = Session["ColName1"]?.ToString() ?? "BV";
            GrdDirects.DataSource = dt;
            GrdDirects.DataBind();

            // Visibility Controls
            bool showTotalDiv = !(new[] { "1007" }.Contains(compId) && DDlSelectType.SelectedValue == "M");
            divtotal.Visible = showTotalDiv;
        }
        catch (Exception ex)
        {
            divRoyalty.Visible = (Session["Compid"]?.ToString() == "1010" && RbtProduct.SelectedValue != "R");

            string path = Request.Url.AbsoluteUri;
            string logMessage = $"{path}: {DateTime.Now:dd-MMM-yyyy hh:mm:ss:fff}{Environment.NewLine}{ex.Message}";
            // Assuming objGen is a logger utility
            // objGen.WriteToFile(logMessage);
            Response.Write($"<script>alert('Error: {ex.Message.Replace("'", "\\'")}');</script>");
        }
    }
    private void ShowTotalBV(DataTable dt)
    {
        decimal totalBV = 0;

        if (dt != null && dt.Rows.Count > 0)
        {
            // NULL-safe SUM
            //totalBV = Convert.ToDecimal(dt.Compute("SUM(BV)", ""));
            object totalObj = dt.Compute("SUM(BV)", "");
            totalBV = (totalObj == DBNull.Value || totalObj == null) ? 0m : Convert.ToDecimal(totalObj);
        }

        lblTotalBV.Text = totalBV.ToString("0.00");
    }
    private void ShowBV(DataTable dt)
    {
        decimal leftBV = 0;
        decimal rightBV = 0;

        if (dt != null && dt.Rows.Count > 0)
        {
            // Calculate from table
            object leftObj = dt.Compute("SUM(BV)", "[Group Name] = 'Group A'");
            object rightObj = dt.Compute("SUM(BV)", "[Group Name] = 'Group B'");
            leftBV = (leftObj == DBNull.Value || leftObj == null) ? 0m : Convert.ToDecimal(leftObj);
            rightBV = (rightObj == DBNull.Value || rightObj == null) ? 0m : Convert.ToDecimal(rightObj);
            //leftBV = Convert.ToDecimal(dt.Compute("SUM(BV)", "[Group Name] = 'Group A'"));
            //rightBV = Convert.ToDecimal(dt.Compute("SUM(BV)", "[Group Name] = 'Group B'"));
        }

        // ===============================
        // RADIO BUTTON LOGIC
        // ===============================

        if (RbtLegNo.SelectedValue == "1")        // LEFT SELECTED
        {
            Lblleft.Text = leftBV.ToString("0.00");
            Lblright.Text = "0.00";               // right hide
        }
        else if (RbtLegNo.SelectedValue == "2")   // RIGHT SELECTED
        {
            Lblleft.Text = "0.00";
            Lblright.Text = rightBV.ToString("0.00");
        }
        else                                      // BOTH SELECTED (0)
        {
            Lblleft.Text = leftBV.ToString("0.00");
            Lblright.Text = rightBV.ToString("0.00");
        }

        // TOTAL BV
        lblTotalBV.Text = (leftBV + rightBV).ToString("0.00");
    }
    private string SafeCompute(DataTable table, string expression, string filter = "")
    {
        try
        {
            if (!string.IsNullOrEmpty(filter))
            {
                var dv = new DataView(table) { RowFilter = filter };
                table = dv.ToTable();
            }
            var result = table.Compute(expression, "");
            return result == DBNull.Value ? "0" : result.ToString();
        }
        catch
        {
            return "0";
        }
    }
    protected void FillLevel()
    {
        try
        {
            string str;
            dt = new DataTable();

            str = "Select distinct * from (" +
                  "Select 0 As MLevel,'-- ALL --' As LevelName Union ALL " +
                  "select MLevel,'Level :' + convert(varchar, MLevel) as LevelName " +
                  "from R_MemTreeRelation with(nolock) where FormNo='" + Session["FormNo"] + "'" +
                  ") as Temp order by MLevel";

            Comm = new SqlCommand(str, Conn);
            Ad = new SqlDataAdapter(Comm);
            dt = new DataTable();
            Ad.Fill(dt);

            DdlLevel.DataSource = dt;
            DdlLevel.DataTextField = "LevelName";
            DdlLevel.DataValueField = "MLevel";
            DdlLevel.DataBind();
        }
        catch (Exception ex)
        {
            string path = HttpContext.Current.Request.Url.AbsoluteUri;
            string text = path + ":  " + DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss:fff") + Environment.NewLine;
            objGen.WriteToFile(text + ex.Message);
        }
    }
    protected void FillLevel_Ra()
    {
        try
        {
            if (RbtProduct.SelectedValue == "R")
            {
                LblLevel.Text = "Level Wise";
                DdlLevel.Visible = true;
                RbtLegNo.Visible = false;
            }
            else
            {
                LblLevel.Text = "Group Wise";
                DdlLevel.Visible = false;
                RbtLegNo.Visible = true;
            }
        }
        catch (Exception ex)
        {
            string path = HttpContext.Current.Request.Url.AbsoluteUri;
            string text = path + ":  " + DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss:fff") + Environment.NewLine;
            objGen.WriteToFile(text + ex.Message);
        }
    }
    protected void DDlSelectType_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (DDlSelectType.SelectedValue == "G")
            {
                LblLevel.Text = "Level Wise";
                DdlLevel.Visible = true;
                RbtLegNo.Visible = false;
                LevelDetail();
            }
            else
            {
                LblLevel.Text = "Group Wise";
                DdlLevel.Visible = false;
                RbtLegNo.Visible = true;
                LevelDetail();
            }
        }
        catch (Exception ex)
        {
            string path = HttpContext.Current.Request.Url.AbsoluteUri;
            string text = path + ":  " +
                          DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss:fff") +
                          Environment.NewLine;

            objGen.WriteToFile(text + ex.Message);
        }
    }
}
public static class Extensions
{
    public static int AsInt(this object obj) => int.TryParse(obj?.ToString(), out int i) ? i : 0;
    public static string Val(this string s) => int.TryParse(s, out int i) ? i.ToString() : "0";
}