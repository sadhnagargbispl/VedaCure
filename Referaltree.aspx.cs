using System;
using System.Data;
using System.Data.SqlClient;

public partial class Referaltree : System.Web.UI.Page
{
    string strQuery;
    int minDeptLevel;
    SqlConnection conn = new SqlConnection();
    SqlCommand Comm = new SqlCommand();
    SqlDataAdapter Adp1;
    DataSet dsGetQry = new DataSet();
    string strDrawKit;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            conn = new SqlConnection(Application["Connect"].ToString());
            conn.Open();

            if (!IsPostBack)
            {
                if (Session["Status"] != null && Session["Status"].ToString() == "OK")
                {
                    ValidateTree();
                }
                else
                {
                    Response.Redirect("default.aspx");
                }
            }
        }
        catch { }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        if (Session["Status"] == "OK")
        {
            string DownFormNo = get_FormNo(DownLineFormNo.Value);
            Response.Redirect("Referaltree?DownLineFormNo=" + DownFormNo);
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
            "Select FormNo From M_MemberMaster Where IDNo='" + IDNo + "'", conn);

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

            Response.Redirect("Referaltree?DownLineFormNo=" + uplnformno);
        }
    }
    private void ValidateTree()
    {
        string strSelectedFormNo = "";

        // ---- If User Not Set MinDept level then set 5 dept level
        if (Request["DeptLevel"] == null || Request["DeptLevel"] == "")
        {
            minDeptLevel = 5;
        }
        else
        {
            minDeptLevel = Convert.ToInt32(Request["DeptLevel"]);
        }

        if (
            Session["FormNO"] == null || Session["FormNO"].ToString() == "" ||
            Request["DownLineFormNo"] == null || Request["DownLineFormNo"] == "" ||
            (Session["FormNO"] != null && Session["Upliner"] != null &&
             Session["FormNO"].ToString() == Session["Upliner"].ToString())
           )
        {
            strSelectedFormNo = Session["FORMNO"].ToString();
            BtnStepAbove.Enabled = false;
        }
        else if (
            Session["MemUpliner"] != null && Session["Upliner"] != null &&
            Session["MemUpliner"].ToString() == Session["Upliner"].ToString()
        )
        {
            strSelectedFormNo = Session["Upliner"].ToString();
            BtnStepAbove.Enabled = true;
        }
        else
        {
            if (!CheckDownLineMemberTree())
            {
                BtnStepAbove.Enabled = false;
                Response.Write("Please Check DownLine Member ID");
                Response.End();
                return;
            }
            BtnStepAbove.Enabled = true;
            strSelectedFormNo = Request["DownLineFormNo"];
        }

        strQuery = getQuery(strSelectedFormNo, minDeptLevel);
        GenerateTree(strQuery);
        getKits();
    }


    private void getKits()
    {
        DataSet dsKit = new DataSet();

        // conn = new SqlConnection(Application["Connect"].ToString());
        // conn.Open();

        Comm = new SqlCommand(
            "Select * from m_KitMaster where ActiveStatus='Y' order by kitid",
            conn
        );

        Adp1 = new SqlDataAdapter(Comm);
        Adp1.Fill(dsKit);

        //if (dsKit.Tables[0].Rows.Count > 0)
        //{
        //    img11.Visible = true;
        //    img11.ImageUrl = "~/img/" + dsKit.Tables[0].Rows[0]["JoinColor"].ToString();
        //    td21.InnerText = dsKit.Tables[0].Rows[0]["KitName"].ToString();

        //    if (dsKit.Tables[0].Rows.Count > 1)
        //    {
        //        img12.Visible = true;
        //        img12.ImageUrl = "~/img/" + dsKit.Tables[0].Rows[1]["JoinColor"].ToString();
        //        td22.InnerText = dsKit.Tables[0].Rows[1]["KitName"].ToString();

        //        if (dsKit.Tables[0].Rows.Count > 2)
        //        {
        //            img13.Visible = true;
        //            img13.ImageUrl = "~/img/" + dsKit.Tables[0].Rows[2]["JoinColor"].ToString();
        //            td23.InnerText = dsKit.Tables[0].Rows[2]["KitName"].ToString();

        //            if (dsKit.Tables[0].Rows.Count > 3)
        //            {
        //                img14.Visible = true;
        //                img14.ImageUrl = "~/img/" + dsKit.Tables[0].Rows[3]["JoinColor"].ToString();
        //                td24.InnerText = dsKit.Tables[0].Rows[3]["KitName"].ToString();

        //                if (dsKit.Tables[0].Rows.Count > 4)
        //                {
        //                    img15.Visible = true;
        //                    img15.ImageUrl = "~/img/" + dsKit.Tables[0].Rows[4]["JoinColor"].ToString();
        //                    td25.InnerText = dsKit.Tables[0].Rows[4]["KitName"].ToString();

        //                    if (dsKit.Tables[0].Rows.Count > 5)
        //                    {
        //                        img16.Visible = true;
        //                        img16.ImageUrl = "~/img/" + dsKit.Tables[0].Rows[5]["JoinColor"].ToString();
        //                        td26.InnerText = dsKit.Tables[0].Rows[5]["KitName"].ToString();

        //                        if (dsKit.Tables[0].Rows.Count > 6)
        //                        {
        //                            img17.Visible = true;
        //                            img17.ImageUrl = "~/img/" + dsKit.Tables[0].Rows[6]["JoinColor"].ToString();
        //                            td27.InnerText = dsKit.Tables[0].Rows[6]["KitName"].ToString();
        //                        }
        //                    }
        //                }
        //            }
        //        }
        //    }
        //}
    }
    private bool CheckDownLineMemberTree()
    {
        bool result = false;

        strQuery = "Select FormnoDwn FROM R_MemTreeRelation " +
                   "WHERE FormNoDwn = " + Request["DownLineFormNo"] +
                   " AND FormNo = " + Session["FORMNO"];

        SqlCommand Comm;
        SqlDataAdapter Adp1;
        DataSet ds1 = new DataSet();

        // conn = new SqlConnection(Application["Connect"].ToString());
        // conn.Open();

        Comm = new SqlCommand(strQuery, conn);
        Adp1 = new SqlDataAdapter(Comm);
        Adp1.Fill(ds1);

        if (ds1.Tables[0].Rows.Count > 0)
        {
            result = true;
        }

        ds1.Dispose();
        return result;
    }


    private void GenerateTree(string strQuery)
    {
        SqlCommand Comm;
        SqlDataAdapter Adp1;
        DataSet ds1 = new DataSet();

        Comm = new SqlCommand(strQuery, conn);
        Adp1 = new SqlDataAdapter(Comm);
        Adp1.Fill(ds1);

        int I = 0;
        int ParentId = 0;
        double FormNo = 0;
        string MemberName = "";
        string LegNo = "";
        string Doj = "";
        string Category = "";
        double LeftBV = 0;
        double RightBV = 0;
        string UpLiner = "";
        string Sponsor = "";
        string NodeName = "";
        string myRunTimeString = "";
        string ExpandYesNo = "";
        string strImageFile = "";
        string strUrlPath = "";
        string tooltipstrig = "";
        string Target_ = "_self";
        string Rank = "";
        string IdNo = "";
        string Upliner1 = "";
        string UpgradeDate = "";

        myRunTimeString += "<Script Language=Javascript>\n";

        tooltipstrig = ToolTipTable();

        // Parent
        ParentId = -1;

        DataSet tmpDS = new DataSet();

        if (!string.IsNullOrEmpty(Request["DownLineFormNo"]))
            FormNo = Convert.ToDouble(Request["DownLineFormNo"]);
        else
            FormNo = Convert.ToDouble(Session["FormNo"]);

        strQuery =
            "SELECT A.*,CASE WHEN a.ActiveStatus='Y' THEN REPLACE(CONVERT(Varchar,UpgradeDate,106),' ','-') ELSE '' END AS UpgradeDate1," +
            " b.KitName,d.JoinColor,C.Direct,C.Indirect,B.KitName AS Category,rnk.Rank " +
            "FROM m_MemberMaster A LEFT JOIN V#RankAchiever rnk ON a.Formno=rnk.Formno, " +
            "M_KitMaster B,V#DI C,V#JoinColor d " +
            "WHERE a.Formno=d.Formno AND A.KitID=B.KitID AND A.FormNo=C.FormNo AND A.FORMNO=" + FormNo;

        Comm = new SqlCommand(strQuery, conn);
        Adp1 = new SqlDataAdapter(Comm);
        Adp1.Fill(tmpDS);

        if (tmpDS.Tables[0].Rows.Count > 0)
        {
            MemberName = tmpDS.Tables[0].Rows[0]["IdNo"].ToString();
            NodeName = tmpDS.Tables[0].Rows[0]["MemFirstName"].ToString();
            strImageFile = "img/" + tmpDS.Tables[0].Rows[0]["JoinColor"];
            Doj = tmpDS.Tables[0].Rows[0]["doj"].ToString();
            Category = tmpDS.Tables[0].Rows[0]["Category"].ToString();
            LeftBV = Convert.ToDouble(tmpDS.Tables[0].Rows[0]["Direct"]);
            RightBV = Convert.ToDouble(tmpDS.Tables[0].Rows[0]["Indirect"]);
            Rank = tmpDS.Tables[0].Rows[0]["Rank"].ToString();
            IdNo = tmpDS.Tables[0].Rows[0]["IdNo"].ToString();
            Upliner1 = tmpDS.Tables[0].Rows[0]["RefFormno"].ToString();
            UpgradeDate = tmpDS.Tables[0].Rows[0]["UpgradeDate1"].ToString();

            Session["Upliner"] = Upliner1;
        }

        myRunTimeString += "mytree = new dTree('mytree','" + strImageFile + "');\n";

        myRunTimeString +=
            "mytree.add(" + FormNo + "," + ParentId + ",'" + Category + "','" + Doj + "','" +
            MemberName + "','" + NodeName + "','" + UpLiner + "','" + Sponsor + "'," +
            LeftBV + "," + RightBV + ",'','" + MemberName + "','','" +
            strImageFile + "','" + strImageFile + "','true','" +
            Rank + "','" + UpgradeDate + "');\n";

        int LoopValue = 0;

        foreach (DataRow dr in ds1.Tables[0].Rows)
        {
            ParentId = Convert.ToInt32(dr["RefFormno"]);
            FormNo = Convert.ToDouble(dr["FormNoDwn"]);
            LegNo = dr["Reflegno"].ToString();
            UpLiner = "0";
            Sponsor = "0";
            Doj = dr["doj"].ToString();
            Category = dr["Category"].ToString();
            LeftBV = Convert.ToDouble(dr["Direct"]);
            RightBV = Convert.ToDouble(dr["Indirect"]);
            Rank = dr["Rank"].ToString();
            IdNo = dr["IdNo"].ToString();
            UpgradeDate = dr["UpgradeDate"].ToString();

            MemberName = dr["IdNo"] + "<br />(" + dr["MemFirstName"] + ")";
            //dr["IdNo"].ToString();
            NodeName = dr["MemFirstName"].ToString();

            LoopValue++;
            ExpandYesNo = (LoopValue <= 5) ? "true" : "false";

            if (FormNo <= 0)
            {
                strImageFile = "img/empty.jpg";
                MemberName = "Direct";
                Target_ = "_blank";
                strUrlPath = "newjoining.aspx?RefFormNo=" + ParentId;
            }
            else
            {
                strImageFile = "img/" + dr["JoinColor"];
                Target_ = "";
                strUrlPath = "ReferalTree?DownLineFormNo=" + FormNo;
            }

            myRunTimeString +=
                "mytree.add(" + FormNo + "," + ParentId + ",'" + Category + "','" + Doj + "','" +
                MemberName + "','" + NodeName + "','" + UpLiner + "','" + Sponsor + "'," +
                LeftBV + "," + RightBV + ",'" + strUrlPath + "','" +
                MemberName + "','" + Target_ + "','" + strImageFile + "','" +
                strImageFile + "'," + ExpandYesNo + ",'" +
                Rank + "','" + UpgradeDate + "');\n";
        }

        myRunTimeString += "\n\n\n\ndocument.write(mytree);\n</script><br><br><br><br>";

        ClientScript.RegisterClientScriptBlock(this.GetType(), "clientScript", myRunTimeString);
    }

    private string ToolTipTable()
    {
        return "";
    }

    private string getQuery(string strSelectedFormNo, int minDeptLevel)
    {
        return "exec sp_ShowRefTree " + strSelectedFormNo + "," + minDeptLevel;
    }

    protected void Page_LoadComplete(object sender, EventArgs e)
    {
        if (conn.State == ConnectionState.Open)
            conn.Close();
    }

    protected void Page_Unload(object sender, EventArgs e)
    {
        if (conn.State == ConnectionState.Open)
            conn.Close();
    }
}
