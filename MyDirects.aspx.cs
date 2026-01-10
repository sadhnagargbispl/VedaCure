using System;
using System.Data;
using System.Data.SqlClient;

public partial class MyDirects : System.Web.UI.Page
{
    SqlConnection Conn;
    SqlCommand Comm;
    DataTable Dt;
    SqlDataAdapter Ad;
    DAL Obj;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Status"] != null && Session["Status"].ToString() == "OK")
        {
            if (!Page.IsPostBack)
            {
                FillLevel();
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

    protected void DDLLevel_SelectedIndexChanged(object sender, EventArgs e)
    {
        // Reserved for future use
    }

    protected void FillLevel()
    {
        Conn = new SqlConnection(Application["Connect"].ToString());
        Conn.Open();

        string str = "SELECT DISTINCT MLevel FROM R_MemTreeRelation WHERE FormNo='" + Session["FormNo"] + "' ORDER BY MLevel";

        Comm = new SqlCommand(str, Conn);
        Ad = new SqlDataAdapter(Comm);
        Dt = new DataTable();
        Ad.Fill(Dt);

        DDLLevel.DataSource = Dt;
        DDLLevel.DataTextField = "MLevel";
        DDLLevel.DataValueField = "MLevel";
        DDLLevel.DataBind();

        Conn.Close();
    }

    protected void LevelDetail()
    {
        Conn = new SqlConnection(Application["Connect"].ToString());
        Conn.Open();

        string sql = "exec sp_MyDirect " + Convert.ToInt32(Session["FormNo"]) + "," + Convert.ToInt32(DDLLevel.SelectedValue);

        Comm = new SqlCommand(sql, Conn);
        Ad = new SqlDataAdapter(Comm);
        Dt = new DataTable();
        Ad.Fill(Dt);

        Session["DirectData1"] = Dt;

        GrdDirects.CurrentPageIndex = 0;
        GrdDirects.DataSource = Dt;
        GrdDirects.DataBind();

        Conn.Close();
    }

    protected void BtnSearch_Click(object sender, EventArgs e)
    {
        LevelDetail();
    }
}
