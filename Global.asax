<%@ Application Language="C#" %>
<%@ Import Namespace="System.Data.SqlClient" %>
<%@ Import Namespace="System.Data" %>

<script RunAt="server">
    protected void Application_Start(object sender, EventArgs e)
    {
        Application["InvDB"] = "VedaCure";
        Application["InvDB1"] = "OrganozInv";
        Application["Connect"] = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
        Application["InvConnect"] = ConfigurationManager.ConnectionStrings["dbconstr"].ConnectionString;
        ScriptManager.ScriptResourceMapping.AddDefinition(
"jquery",
new ScriptResourceDefinition
{
    Path = "~/Scripts/jquery-3.7.1.min.js", // अपनी jQuery file path डालें
    DebugPath = "~/Scripts/jquery-3.7.1.js"
}
);
        // Application["CompID"] = "1003";
    }

    protected void Application_End(object sender, EventArgs e)
    {
        // Code that runs on application shutdown
    }

    protected void Application_Error(object sender, EventArgs e)
    {
        // Code that runs when an unhandled error occurs
    }

    protected void Session_Start(object sender, EventArgs e)
    {
        // Code that runs when a new session is started

        getData();
    }

    protected void Session_End(object sender, EventArgs e)
    {
        // Code that runs when a session ends. 
        // Note: The Session_End event is raised only when the sessionstate mode
        // is set to InProc in the Web.config file. If session mode is set to StateServer 
        // or SQLServer, the event is not raised.
    }
    public void getData()
    {
        try
        {
            cls_DataAccess dbConnect = new cls_DataAccess(Application["Connect"].ToString());
            dbConnect.OpenConnection();

            SqlDataReader dRead;
            SqlCommand cmd;

            // -------------------------- Company Master --------------------------
            cmd = new SqlCommand("SELECT * FROM M_CompanyMaster", dbConnect.cnnObject);
            dRead = cmd.ExecuteReader();

            if (dRead.Read())
            {
                Session["CompName"] = dRead["CompName"];
                Session["CompAdd"] = dRead["CompAdd"];
                Session["CompWeb"] = string.IsNullOrEmpty(dRead["WebSite"].ToString())
                                        ? "index.asp"
                                        : dRead["WebSite"].ToString();
                Session["Title"] = dRead["CompTitle"];
                Session["CompMail"] = dRead["CompMail"];
                Session["CompMobile"] = dRead["MobileNo"];
                Session["ClientId"] = dRead["smsSenderId"];
                Session["SmsId"] = dRead["smsUserNm"];
                Session["SmsPass"] = dRead["smPass"];
                Session["MailPass"] = dRead["mailPass"];
                Session["MailHost"] = dRead["mailHost"];
                Session["AdminWeb"] = dRead["AdminWeb"];
            }
            else
            {
                Session["CompName"] = "";
                Session["CompAdd"] = "";
                Session["CompWeb"] = "";
                Session["Title"] = "Welcome";
            }

            dRead.Close();


            // -------------------------- Config Master ---------------------------
            cmd = new SqlCommand("SELECT * FROM M_ConfigMaster", dbConnect.cnnObject);
            dRead = cmd.ExecuteReader();

            if (dRead.Read())
            {
                Session["IsGetExtreme"] = dRead["IsGetExtreme"];
                Session["IsTopUp"] = dRead["IsTopUp"];
                Session["IsSendSMS"] = dRead["IsSendSMS"];
                Session["IdNoPrefix"] = dRead["IdNoPrefix"];
                Session["IsFreeJoin"] = dRead["IsFreeJoin"];
                Session["IsStartJoin"] = dRead["IsStartJoin"];
                Session["JoinStartFrm"] = dRead["JoinStartFrm"];
                Session["IsSubPlan"] = dRead["IsSubPlan"];
                Session["Logout"] = dRead["LogoutPg"];
            }
            else
            {
                Session["IsGetExtreme"] = "N";
                Session["IsTopUp"] = "N";
                Session["IsSendSMS"] = "N";
                Session["IdNoPrefix"] = "";
                Session["IsFreeJoin"] = "N";
                Session["IsStartJoin"] = "N";
                Session["JoinStartFrm"] = "01-Sep-2011";
                Session["IsSubPlan"] = "N";
                Session["Logout"] = "";
            }

            dRead.Close();


            // -------------------------- Max Session (Monthly) ---------------------
            cmd = new SqlCommand("SELECT MAX(SEssid) AS SessID FROM D_Monthlypaydetail", dbConnect.cnnObject);
            dRead = cmd.ExecuteReader();

            if (dRead.Read())
                Session["MaxSessn"] = dRead["SessID"];
            else
                Session["MaxSessn"] = "";

            dRead.Close();


            // -------------------------- Current Session ---------------------------
            cmd = new SqlCommand("SELECT MAX(SEssid) AS SessID FROM m_SessnMaster", dbConnect.cnnObject);
            dRead = cmd.ExecuteReader();

            if (dRead.Read())
                Session["CurrentSessn"] = dRead["SessID"];
            else
                Session["CurrentSessn"] = "";

            dRead.Close();
        }
        catch
        {
            Session["CompName"] = "";
            Session["CompAdd"] = "";
            Session["CompWeb"] = "";
        }
    }
    protected void Application_BeginRequest(object sender, EventArgs e)
    {
        string URL_path = HttpContext.Current.Request.Path;
        string Original_Path = HttpContext.Current.Request.Path;
        int QryStringIdx = 0;

        if (URL_path.Contains("/"))
        {
            // If you need the domain replace logic, uncomment these:
            // URL_path = URL_path.Replace("www.chinarrbusiness.com", "");
            // URL_path = URL_path.Replace("chinarrbusiness.com", "");

            // Check: substring starting at position 5 must be valid
            if (URL_path.Length > 5 && !URL_path.Substring(5).Contains("."))
            {
                int SlashIdx = URL_path.LastIndexOf("/");

                if (URL_path.Contains("?"))
                    QryStringIdx = URL_path.LastIndexOf("?");

                if (QryStringIdx > 0)
                {
                    URL_path = URL_path.Substring(SlashIdx, QryStringIdx - SlashIdx);
                }
                else
                {
                    URL_path = URL_path.Substring(SlashIdx);
                }

                // Rewrite to add .aspx extension
                Context.RewritePath(Original_Path.Replace(URL_path, URL_path + ".aspx"));
            }
        }
    }

</script>
