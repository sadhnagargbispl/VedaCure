using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using System.Numerics;


public partial class NewTree : System.Web.UI.Page
{
    private string strQuery;
    private int minDeptLevel;
    private SqlConnection conn = new SqlConnection();
    private SqlCommand Comm = new SqlCommand();
    private SqlDataAdapter Adp1;
    private DataSet dsGetQry = new DataSet();
    private string strDrawKit;
    private DAL obj;

    string scrname = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Convert.ToString(Session["Status"]) == "OK")
        {
            conn = new SqlConnection(Convert.ToString(Application["Connect"]));
            conn.Open();

            if (!Page.IsPostBack)
            {
                ValidateTree();
            }
        }
        else
        {
            Response.Redirect("logout.aspx");
        }
    }
    private bool CheckDownLineMemberTree(string formno)
    {
        bool result = false;

        string str = " Select FormnoDwn FROM M_MemTreeRelation " +
                     "WHERE FormNoDwn=" + formno +
                     " AND FormNo=" + Session["FORMNO"];

        SqlCommand Comm = new SqlCommand(str, conn);
        SqlDataAdapter Adp1 = new SqlDataAdapter(Comm);
        DataSet ds1 = new DataSet();

        Adp1.Fill(ds1);

        if (ds1.Tables[0].Rows.Count > 0)
            result = true;

        ds1.Dispose();
        return result;
    }
    private string get_FormNo(string IDNo)
    {
        string FormNo = "";

        SqlDataReader dr;
        Comm = new SqlCommand(
            "Select FormNo,LegNo From M_MemberMaster Where IDNo='" + IDNo + "'", conn);

        dr = Comm.ExecuteReader();

        if (dr.Read())
        {
            FormNo = dr["FormNo"].ToString();
           // lblLevl.Text = dr["LegNo"].ToString();
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
    protected void Button1_Click(object sender, EventArgs e)
    {
        string scrname = "";
        string DownFormNo = get_FormNo(DownLineFormNo.Value);

        if (DownFormNo != "")
        {
            Response.Redirect("NewTree?DownLineFormNo=" + DownFormNo);
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
    protected void BtnStepabove_Click(object sender, EventArgs e)
    {
        if (Session["Upliner"] != null && Session["Upliner"].ToString() != "0" && Session["RefUpliner"] != null && Session["RefUpliner"].ToString() != Session["Upliner"].ToString())
        {
            string uplnformno = Session["Upliner"].ToString();
            //BtnStepabove.Enabled = true;
            Response.Redirect("NewTree?DownLineFormNo=" + uplnformno);
        }
        else if (Session["RefUpliner"] != null && Session["Upliner"] != null && Session["RefUpliner"].ToString() == Session["Upliner"].ToString())
        {
           // BtnStepabove.Enabled = false;
            Response.Write("Sorry!! You can't see your upliner tree.");
            Response.End();
        }
    }
    private void ValidateTree()
    {
        try
        {
            string strSelectedFormNo = "";

            minDeptLevel = 5;
            // ---- Validation Logic ----
            if ((Session["FormNO"] == null || Session["FormNO"].ToString() == "") ||
                (Request["DownLineFormNo"] == null || Request["DownLineFormNo"] == "") ||
                (Session["Formno"] != null && Session["Formno"].ToString() == Request["DownLineFormNo"]))
            {
                strSelectedFormNo = Session["FORMNO"].ToString();
                BtnStepAbove.Enabled = false;
            }
            else if (Session["MemUpliner"] != null && Session["Upliner"] != null &&
                     Session["MemUpliner"].ToString() != Session["Upliner"].ToString())
            {
                if (CheckDownLineMemberTree() == false)
                {
                    Response.Write("Please Check DownLine Member ID");
                    Response.End();
                    return;
                }

                strSelectedFormNo = Request["DownLineFormNo"];
                BtnStepAbove.Enabled = true;
            }
            else if ((Session["Formno"] != null && Session["Upliner"] != null &&
                      Session["Formno"].ToString() == Session["Upliner"].ToString()) ||
                     (Session["MemUpliner"] != null && Session["Upliner"] != null &&
                      Session["MemUpliner"].ToString() == Session["Upliner"].ToString()))
            {
                BtnStepAbove.Enabled = false;
                Response.Write("Sorry!! You can't see your upliner tree.");
                Response.End();
                return;
            }
            else
            {
                if (CheckDownLineMemberTree() == false)
                {
                    Response.Write("Please Check DownLine Member ID");
                    Response.End();
                    return;
                }

                strSelectedFormNo = Request["DownLineFormNo"];
            }

            strQuery = getQuery(strSelectedFormNo, minDeptLevel);
            GenerateTree(strQuery);
        }
        catch (Exception ex)
        {
            string path = HttpContext.Current.Request.Url.AbsoluteUri;
            string text = path + ": " +
                          DateTime.Now.ToString("dd-MMM-yyyy HH:mm:ss:fff") +
                          Environment.NewLine;

            obj.WriteToFile(text + ex.Message);
            Response.Write("Try later.");
        }
    }
    
    private void GenerateTree(string strQuery)
    {
        Comm = new SqlCommand(strQuery, conn);
        Comm.CommandTimeout = 100000000;

        Adp1 = new SqlDataAdapter(Comm);
        Adp1.Fill(dsGetQry);

        double ParentId;
        double FormNo;
        string MemberName;
        string LegNo;
        string Doj = "";
        string Category = "";
        double LeftBV = 0, RightBV = 0, LeftEquity = 0, RightEquity = 0, LeftRepurchase = 0, RightRepurchase = 0;
        double LeftJoining = 0, RightJoining = 0;
        string UpLiner, Sponsor;
        int level;
        string NodeName;
        string myRunTimeString = "";
        string ExpandYesNo;
        string strImageFile;
        string strUrlPath = "";
        string UpDt;
        string tooltipstrig;
        string Target_ = "_self";
        string IdNo;

        myRunTimeString += "<Script Language=Javascript>\n";
        tooltipstrig = ToolTipTable();

        // Parent Setting
        ParentId = -1;

        if (!string.IsNullOrEmpty(Request["DownLineFormNo"]))
            FormNo = Convert.ToDouble(Request["DownLineFormNo"]);
        else
            FormNo = Convert.ToDouble(Session["FormNo"]);

        strImageFile = "img/base.jpg";
        int i = 0;
        int LoopValue;
        string FolderFile = "img/Deactivate.jpg";

        foreach (DataRow dr in dsGetQry.Tables[0].Rows)
        {
            strImageFile = "img/" + dr["JoinColor"].ToString();

            if (i == 0)
            {
                if (!string.IsNullOrEmpty(Request["DownLineFormNo"]))
                    Session["Upliner"] = dr["UplineFormno"].ToString();
                else
                    Session["Upliner"] = null;

                myRunTimeString += "mytree = new dTree('mytree','" + strImageFile + "');\n";
                i++;
            }

            ParentId = Convert.ToDouble(dr["UPLNFORMNO"]);
            FormNo = Convert.ToDouble(dr["FormNoDwn"]);
            LegNo = dr["legno"].ToString();
            UpLiner = dr["UpLiner"].ToString();
            Sponsor = dr["Sponsor"].ToString();
            Doj = dr["doj"].ToString();
            Category = dr["Category"].ToString();

            LeftBV = Convert.ToDouble(dr["LeftBV"]);
            RightBV = Convert.ToDouble(dr["rightBV"]);
            LeftJoining = Convert.ToDouble(dr["Leftjoining"]);
            RightJoining = Convert.ToDouble(dr["rightjoining"]);
            level = Convert.ToInt32(dr["level"]);

            strUrlPath = "NewJoining.aspx?DownLineFormNo=" + FormNo;
            UpDt = dr["UpDt"].ToString();
            IdNo = dr["Formno"].ToString();
            MemberName = dr["Formno"] + "<br />(" + dr["memName"] + ")";
            NodeName = dr["memName"].ToString();
            LoopValue = Convert.ToInt32(dr["mlevel"]);

            ExpandYesNo = (LoopValue < 4 && LoopValue > 0) ? "true" : "false";
            if (ParentId == -1) ExpandYesNo = "true";

            if (UpDt == "01 Jan 00") UpDt = "";

            if (FormNo <= 0)
            {
                Target_ = "_blank";
                strUrlPath = "newjoining.aspx?UpLnFormNo=" + ParentId + "&legno=" + LegNo;
                MemberName = (LegNo == "1") ? "Left" : "Right";
            }
            else
            {
                if (dr["ActiveStatus"].ToString() == "N")
                    strImageFile = "img/deact.jpg";
                else if (Convert.ToInt32(dr["Kitid"]) == 1)
                    strImageFile = "img/Red.jpg";
                else if (Convert.ToInt32(dr["Kitid"]) == 2)
                    strImageFile = "img/Blue.jpg";
                else if (Convert.ToInt32(dr["Kitid"]) == 3)
                    strImageFile = "img/Green.jpg";
                else if (Convert.ToInt32(dr["Kitid"]) == 4)
                    strImageFile = "img/Yellow.jpg";
                else if (Convert.ToInt32(dr["Kitid"]) == 5)
                    strImageFile = "img/Orange.jpg";
                else if (Convert.ToInt32(dr["Kitid"]) == 6)
                    strImageFile = "img/purpel.jpg";
                else
                    strImageFile = "img/empty.jpg";

                Target_ = "";
                strUrlPath = "newtree?DownLineFormNo=" + FormNo + "&upln=" + FormNo;
            }

            strImageFile = "img/" + dr["JoinColor"].ToString();

            myRunTimeString += " mytree.add(" + FormNo + "," + ParentId + ",'" +
                               Category + "','" + Doj + "','" + MemberName + "','" +
                               NodeName + "','" + UpLiner + "','" + Sponsor + "'," +
                               LeftBV + "," + RightBV + ",'" + strUrlPath + "','" +
                               MemberName + "','" + Target_ + "','" + strImageFile +
                               "','" + strImageFile + "'," + ExpandYesNo + ",'" +
                               LeftJoining + "','" + RightJoining + "','" + level +
                               "','" + UpDt + "','" + IdNo + "'," + LeftEquity + "," +
                               RightEquity + "," + LeftRepurchase + "," + RightRepurchase + ");\n";
        }

        myRunTimeString += "\n\n document.write(mytree);\n</script><br /><br /><br /><br />";

        RegisterClientScriptBlock("clientScript", myRunTimeString);
    }

    private bool CheckDownLineMemberTree()
    {
        bool result = false;

        strQuery = "SELECT FormnoDwn FROM M_MemTreeRelation " +
                   "WHERE FormNoDwn = " + Request["DownLineFormNo"] +
                   " AND FormNo = " + Session["FORMNO"];

        SqlCommand Comm = new SqlCommand(strQuery, conn);
        SqlDataAdapter Adp1 = new SqlDataAdapter(Comm);
        DataSet ds1 = new DataSet();

        Adp1.Fill(ds1);

        if (ds1.Tables[0].Rows.Count > 0)
        {
            result = true;
        }

        ds1.Dispose();
        return result;
    }
    private string ToolTipTable()
    {
        string strToolTip = string.Empty;

        // Original tooltip HTML was commented in VB.NET
        // Keep it same if you want to re-use it later

        return strToolTip;
    }
    private string getQuery(string strSelectedFormNo, int minDeptLevel)
    {
        // check if user pass downline member then make according to downline member
        return "exec sp_ShowTree " + strSelectedFormNo + "," + minDeptLevel;
    }

}
