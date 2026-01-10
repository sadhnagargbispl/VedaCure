using System;
using System.Data;
using System.Data.SqlClient;

public partial class ConsistencyReport : System.Web.UI.Page
{
    SqlConnection Conn;
    SqlCommand Comm;
    DataTable Dt;
    SqlDataAdapter Ad;
    DAL Obj;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Status"] == "OK")
        {
            if (!Page.IsPostBack)
            {
                LevelDetail();
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
        GrdDirects.DataSource = Session["DirectData1"];
        GrdDirects.DataBind();
    }

    protected void LevelDetail()
    {
        Conn = new SqlConnection(Application["Connect"].ToString());
        Conn.Open();

        Comm = new SqlCommand("exec Sp_ConsistencyReport " + Convert.ToInt32(Session["Formno"]), Conn);

        Ad = new SqlDataAdapter(Comm);
        Dt = new DataTable();
        Ad.Fill(Dt);

        Session["DirectData1"] = Dt;

        GrdDirects.CurrentPageIndex = 0;
        GrdDirects.DataSource = Dt;
        GrdDirects.DataBind();

        Conn.Close();
    }
}
