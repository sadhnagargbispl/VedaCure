using System;
using System.Data;
using System.Data.SqlClient;

public partial class Rptwithdrawls : System.Web.UI.Page
{
    SqlConnection Conn;
    SqlCommand Comm;
    DataTable Dt = new DataTable();
    SqlDataAdapter Ad;
    string strquery;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Status"] == "OK")
        {
            if (!Page.IsPostBack)
            {
                Conn = new SqlConnection(Application["Connect"].ToString());
                Conn.Open();

                Comm = new SqlCommand("Exec Sp_GetWithdrawalReport " + Convert.ToInt32(Session["FormNo"]),Conn);

                Ad = new SqlDataAdapter(Comm);
                Dt = new DataTable();
                Ad.Fill(Dt);

                Session["WithdrawData"] = Dt;

                GrdDirects.CurrentPageIndex = 0;
                GrdDirects.DataSource = Dt;
                GrdDirects.DataBind();
            }
        }
        else
        {
            Response.Redirect("logout.aspx");
        }
    }

    protected void GrdDirects_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
    {
        GrdDirects.CurrentPageIndex = e.NewPageIndex;
        GrdDirects.DataSource = Session["WithdrawData"];
        GrdDirects.DataBind();
    }
}
