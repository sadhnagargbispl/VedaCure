using System;
using System.Data;
using System.Data.SqlClient;

public partial class BinaryTree : System.Web.UI.Page
{
    SqlCommand Comm;
    SqlConnection Conn;
    DAL obj;

    protected void Button1_Click(object sender, EventArgs e)
    {
        string scrname = "";
        string DownFormNo = get_FormNo(DownLineFormNo.Value);

        if (DownFormNo != "")
        {
            TreeFrame.Attributes["src"] = "NewTree?DownLineFormNo=" + DownFormNo;
        }
        else
        {
            scrname = "<SCRIPT language='javascript'>alert('Invalid distributor id');</SCRIPT>";
            Page.RegisterStartupScript("MyAlert", scrname);
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
            "Select FormNo,LegNo From M_MemberMaster Where IDNo='" + IDNo + "'", Conn);

        dr = Comm.ExecuteReader();

        if (dr.Read())
        {
            FormNo = dr["FormNo"].ToString();
            lblLevl.Text = dr["LegNo"].ToString();
        }

        dr.Close();
        Comm.Cancel();

        if (FormNo != "")
        {
            if (CheckDownLineMemberTree(FormNo) == false)
            {
                FormNo = "";
            }
        }

        return FormNo;
    }

    private bool CheckDownLineMemberTree(string formno)
    {
        bool result = false;

        string str = " Select FormnoDwn FROM M_MemTreeRelation " +
                     "WHERE FormNoDwn=" + formno +
                     " AND FormNo=" + Session["FORMNO"];

        SqlCommand Comm = new SqlCommand(str, Conn);
        SqlDataAdapter Adp1 = new SqlDataAdapter(Comm);
        DataSet ds1 = new DataSet();

        Adp1.Fill(ds1);

        if (ds1.Tables[0].Rows.Count > 0)
            result = true;

        ds1.Dispose();
        return result;
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Convert.ToString(Session["Status"]) == "OK")
            {
                //if (Conn.State == ConnectionState.Open)
                //{
                //    Conn.Open();
                //}
            }
            else
            {
                Response.Redirect("default.aspx");
            }
        }
        catch (Exception)
        {
        }
    }

    protected void BtnStepabove_Click(object sender, EventArgs e)
    {
        if (Session["Upliner"] != null && Session["Upliner"].ToString() != "0" && Session["RefUpliner"] != null && Session["RefUpliner"].ToString() != Session["Upliner"].ToString())
        {
            string uplnformno = Session["Upliner"].ToString();
            BtnStepabove.Enabled = true;
            TreeFrame.Attributes["src"] = "NewTree?DownLineFormNo=" + uplnformno;
        }
        else if ((Session["FormNO"] == null || Session["FormNO"].ToString() == "") ||
    (Request["DownLineFormNo"] == null || Request["DownLineFormNo"].ToString() == "") ||
    (Session["FormNO"] != null && Session["Upliner"] != null &&
     Session["FormNO"].ToString() == Session["Upliner"].ToString()))
        {
            TreeFrame.Attributes["src"] = "NewTree?DownLineFormNo=" + Session["FORMNO"];
        }
        else if (Session["RefUpliner"] != null && Session["Upliner"] != null && Session["RefUpliner"].ToString() == Session["Upliner"].ToString())
        {
            BtnStepabove.Enabled = false;
            Response.Write("Sorry!! You can't see your upliner tree.");
            Response.End();
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
