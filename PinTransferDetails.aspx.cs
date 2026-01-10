using System;
using System.Data;
using System.Data.SqlClient;

public partial class PinTransferDetails : System.Web.UI.Page
{
    SqlConnection conn = new SqlConnection();
    SqlCommand Comm = new SqlCommand();
    SqlDataAdapter Adp;
    DataSet ds = new DataSet();
    SqlDataAdapter Adp1;
    DataSet ds1;
    string strquery;
    DataTable dt;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Status"] == "OK")
        {
            if (Session["Formno"].ToString() != "1")
            {
                if (!Page.IsPostBack)
                {
                    Fillkit();
                    PaymentDetails();
                }
            }
            else
            {
                Response.Redirect("index.aspx");
            }
        }
        else
        {
            Response.Redirect("Logout.aspx");
            Response.End();
        }

        if (!Page.IsPostBack)
        {
            // Empty but preserved from VB
        }
    }

    private void PaymentDetails()
    {
        try
        {
            string Condition = "";

            if (CmbKit.SelectedValue != "0")
            {
                Condition += " And d.ProdID=" + CmbKit.SelectedValue;
            }

            conn = new SqlConnection(Application["Connect"].ToString());
            conn.Open();

            strquery =
                "select Row_Number() Over(Order by a.FromIdno) As SNo,a.*,convert(varchar,a.TDate,106) as PinDate," +
                "b.MemFirstName+''+b.MemLastName as FromMemName," +
                "c.MemFirstName+''+c.MemLastName as ToMemName," +
                "case when d.Isissued = 'N' then 'UnUsed' else 'Used' end as PinStatus,e.kitname " +
                "from TrnTransferPinDetail as a,M_Membermaster as b,m_MemberMAster as c," +
                "M_Formgeneration as d,M_KitMaster as e " +
                "where a.FromIdno = b.IDNO And a.ToIdno = c.IDNo And a.PinNo = d.Formno and d.prodid=e.kitid " +
                "and FromIdno = '" + Session["Idno"] + "' " + Condition +
                " order by Tdate Desc";

            Comm = new SqlCommand(strquery, conn);
            Adp = new SqlDataAdapter(Comm);
            ds = new DataSet();
            Adp.Fill(ds, "Directs1");

            Session["DirectData1"] = ds.Tables["Directs1"];

            DgPayment.DataSource = ds.Tables["Directs1"];
            DgPayment.DataBind();

            Comm.Cancel();
            ds.Dispose();
            conn.Close();
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message + "SideB");
        }
    }

    protected void DgPayment_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
    {
        DgPayment.CurrentPageIndex = e.NewPageIndex;
        DgPayment.DataSource = Session["DirectData1"];
        DgPayment.DataBind();
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        PaymentDetails();
    }

    private void Fillkit()
    {
        conn = new SqlConnection(Application["Connect"].ToString());
        conn.Open();

        Comm = new SqlCommand(
            "Select KitID,KitName From (" +
            "Select 0 As KitID,'-- ALL --' As KitName " +
            "Union Select KitID,KitName+' ('+cast(KitAmount As Varchar)+')'  as KitName " +
            "From M_KitMaster Where ActiveStatus='Y') as temp Order By Kitid ", conn);

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
}
