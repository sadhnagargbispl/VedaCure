using System;
using System.Data;
using System.Data.SqlClient;

public partial class PinreceivedDetails : System.Web.UI.Page
{
    SqlConnection conn = new SqlConnection();
    SqlCommand Comm = new SqlCommand();
    SqlDataAdapter Adp;
    string strquery;
    DataTable dt;
    DataSet Ds;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Status"] == "OK")
        {
            if (Session["Formno"].ToString() != "1")
            {
                if (!Page.IsPostBack)
                {
                    FillKit();
                    PaymentDetails();
                }
            }
            else
            {
                Response.Redirect("cpindex.aspx");
            }
        }
        else
        {
            Response.Redirect("Logout.aspx");
            Response.End();
        }
    }

    private void PaymentDetails()
    {
        try
        {
            DgReceivedPin.DataSource = null;
            DgReceivedPin.DataBind();

            string Condition = "";

            if (CmbKit.SelectedValue != "0")
            {
                Condition += " And d.ProdID=" + CmbKit.SelectedValue;
            }

            conn = new SqlConnection(Application["Connect"].ToString());
            conn.Open();

            strquery =
                "select Row_Number() Over(Order by a.FromIdno) As SNo,a.toidno,a.pinno,a.ScratchNo,a.fromidno," +
                "b.MemFirstName+''+b.MemLastName as FromMemName," +
                "c.MemFirstName+''+c.MemLastName as ToMemName," +
                "convert(varchar,a.TDate,106) as ToDate," +
                "case when d.Isissued = 'N' then 'UnUsed' else 'Used' end as PinStatus," +
                "e.kitName " +
                "from TrnTransferPinDetail as a,M_Membermaster as b,m_MemberMAster as c," +
                "M_Formgeneration as d,M_kitMaster as e " +
                "where a.FromIdno = b.Idno And a.ToIdno = c.Idno And a.PinNo = d.Formno and d.prodid=e.kitid " +
                "and ToIdno = '" + Session["IDNO"] + "' " + Condition +
                " order by TDate Desc";

            Comm = new SqlCommand(strquery, conn);
            Adp = new SqlDataAdapter(Comm);
            Ds = new DataSet();
            Adp.Fill(Ds, "ReceivedPin");

            DgReceivedPin.CurrentPageIndex = 0;

            if (Ds.Tables["ReceivedPin"].Rows.Count > 0)
            {
                Session["ReceivedPin"] = Ds.Tables["ReceivedPin"];
                DgReceivedPin.DataSource = Ds.Tables["ReceivedPin"];
                DgReceivedPin.DataBind();
               // NoData.Visible = false;
            }
            //else
            //{
            //    NoData.Visible = true;
            //}

            Comm.Cancel();
            Ds.Dispose();
            conn.Close();
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message + "SideB");
        }
    }

    protected void DgPayment_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
    {
        DgReceivedPin.CurrentPageIndex = e.NewPageIndex;
        DgReceivedPin.DataSource = Session["ReceivedPin"];
        DgReceivedPin.DataBind();
    }

    private void FillKit()
    {
        conn = new SqlConnection(Application["Connect"].ToString());
        conn.Open();

        Comm = new SqlCommand(
            "Select KitID,KitName From (Select 0 As KitID,'-- ALL --' As KitName " +
            "Union Select KitID,KitName+' ('+cast(KitAmount As Varchar)+')' as KitName " +
            "From M_KitMaster Where ActiveStatus='Y') as temp Order By Kitid ",
            conn);

        Adp = new SqlDataAdapter(Comm);
        dt = new DataTable();
        Adp.Fill(dt);

        CmbKit.DataSource = dt;
        CmbKit.DataValueField = "KitID";
        CmbKit.DataTextField = "KitName";
        CmbKit.DataBind();

        Comm.Cancel();
        conn.Close();
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        PaymentDetails();
    }
}
