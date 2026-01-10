using System;
using System.Data;
using System.Data.SqlClient;

public partial class RefTree : System.Web.UI.Page
{
    SqlCommand Comm;
    SqlConnection Conn;

    protected void Button1_Click(object sender, EventArgs e)
    {
        if (Session["Status"] == "OK")
        {
            string DownFormNo = get_FormNo(DownLineFormNo.Value);
            TreeFrame.Attributes["src"] = "Referaltree?DownLineFormNo=" + DownFormNo;
        }
        else
        {
            Response.Redirect("logout.aspx");
        }
    }

    protected void cmdBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("index.aspx");
    }

    private string get_FormNo(string IDNo)
    {
        string FormNo = "";
        SqlDataReader dr;

        Comm = new SqlCommand(
            "Select FormNo From M_MemberMaster Where IDNo='" + IDNo + "'", Conn);

        dr = Comm.ExecuteReader();

        if (dr.Read())
        {
            FormNo = dr["FormNo"].ToString();
        }

        dr.Close();
        Comm.Cancel();

        return FormNo;
    }

    protected void BtnStepAbove_Click(object sender, EventArgs e)
    {
        if (Session["Upliner"] != null && Convert.ToInt32(Session["Upliner"]) != 0)
        {
            string uplnformno = Session["Upliner"].ToString();

            TreeFrame.Attributes["src"] = "Referaltree?DownLineFormNo=" + uplnformno;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            Conn = new SqlConnection(Application["Connect"].ToString());
            Conn.Open();
        }
        catch (Exception)
        {
        }
    }

    protected void Page_LoadComplete(object sender, EventArgs e)
    {
        try
        {
            if (Conn.State == ConnectionState.Open)
            {
                Conn.Open();
            }
        }
        catch (Exception)
        {
        }
    }

    protected void Page_Unload(object sender, EventArgs e)
    {
        try
        {
            if (Conn.State == ConnectionState.Open)
            {
                Conn.Open();
            }
        }
        catch (Exception)
        {
        }
    }
}
