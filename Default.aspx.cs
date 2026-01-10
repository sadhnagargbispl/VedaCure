using System.Data.SqlClient;
using System.IO;
using System.Net;
using System;
using System.Data;
using System.Configuration;
using System.Collections;
using DocumentFormat.OpenXml.Spreadsheet;
using System.Web;
using System.Web.UI;
using System.Runtime.Remoting;
using System.Web.UI.HtmlControls;

public partial class Default : System.Web.UI.Page
{
    string uid;
    string Pwd;
    string type;
    string scrname;
    SqlConnection conn = new SqlConnection();
    SqlCommand Cmm = new SqlCommand();
    int i;
    SqlDataReader dr;
    protected void Page_Load(object sender, EventArgs e)
    {
        string Str = string.Empty;
        conn = new SqlConnection(Application["Connect"].ToString());
        conn.Open();
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        if (!Page.IsPostBack)
        {
            if (Request["lgnT"] != null)
            {
                // keep same behavior: replace spaces with + before decrypting
                Str = Crypto.Decrypt(Request["lgnT"].Replace(" ", "+"));


                string idFromRequest = Request["ID"];
                string nowPattern1 = DateTime.Now.Day.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Year.ToString() + (DateTime.Now.Month - 1).ToString();
                string nowPattern2 = DateTime.Now.Day.ToString() + (DateTime.Now.Hour - 1).ToString() + DateTime.Now.Year.ToString() + (DateTime.Now.Month - 1).ToString();


                if (idFromRequest == nowPattern1 || idFromRequest == nowPattern2)
                {
                    if (Str != null && Str.Contains("uid="))
                    {
                        int UIdIndx = Str.IndexOf("&pwd");
                        if (UIdIndx > 4)
                        {
                            uid = Str.Substring(4, UIdIndx - 4);
                            Pwd = Str.Substring(UIdIndx + 5, Str.Length - UIdIndx - 5);
                        }
                    }
                }
                else
                {
                    Response.Redirect("Default.aspx?Error=Y", false);
                }
            }
            else if (Request["uid"] != null)
            {
                uid = Request["uid"];
                Pwd = Request["pwd"];
                type = Request["ref"];


                if (!string.IsNullOrEmpty(uid))
                {
                    uid = uid.Trim().Replace("'", "").Replace("=", "").Replace(";", "");
                }


                if (!string.IsNullOrEmpty(Pwd))
                {
                    Pwd = Pwd.Trim().Replace("'", "").Replace("=", "").Replace(";", "");
                }
            }


            if (!string.IsNullOrEmpty(uid) && !string.IsNullOrEmpty(Pwd))
            {
                if (type == "F")
                {
                    enterFranchisePg();
                }
                else
                {
                    enterHomePg();
                }
            }
        }
    }
    private void enterHomePg()
    {
        if (!string.IsNullOrEmpty(uid) && !string.IsNullOrEmpty(Pwd))
        {
            try
            {
                string query = "Select a.*, b.KitName, b.JoinColor " +
                               "from m_MemberMaster a " +
                               "inner join m_KitMaster b on a.kitid = b.kitid " +
                               "where idno = @uid and passw = @pwd";

                Cmm = new SqlCommand(query, conn);
                Cmm.Parameters.AddWithValue("@uid", uid);
                Cmm.Parameters.AddWithValue("@pwd", Pwd);

                dr = Cmm.ExecuteReader();

                if (!dr.Read())
                {
                    dr.Close();
                    scrname = "<script language='javascript'>alert('Please Enter valid UserName or Password.');</script>";
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "Login Error", scrname, false);
                    return;
                }

                Session["Status"] = "OK";
                Session["IDNo"] = dr["IDNo"];
                Session["FormNo"] = dr["Formno"];
                Session["MemName"] = dr["MemFirstName"] + " " + dr["MemLastName"];
                Session["MobileNo"] = dr["Mobl"];
                Session["MemKit"] = dr["KitID"];
                Session["Package"] = dr["KitName"];
                Session["Position"] = dr["fld3"];
                Session["Doj"] = Convert.ToDateTime(dr["Doj"]).ToString("dd-MMM-yyyy");
                Session["MemUpliner"] = dr["RefFormno"];

                if (Convert.ToInt32(dr["UplnFormno"]) == 0)
                    Session["RefUpliner"] = "-2";
                else
                    Session["RefUpliner"] = dr["UplnFormno"];

                Session["JoinColor"] = dr["JoinColor"];

                dr.Close();
                Response.Redirect("index.aspx", false);
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
        }
    }
    private void enterFranchisePg()
    {
        if (!string.IsNullOrEmpty(uid) && !string.IsNullOrEmpty(Pwd))
        {
            try
            {
                string query = "Select * from M_FranchiseMaster " +
                               "where userid = @uid and passw = @pwd";

                Cmm = new SqlCommand(query, conn);
                Cmm.Parameters.AddWithValue("@uid", uid);
                Cmm.Parameters.AddWithValue("@pwd", Pwd);

                dr = Cmm.ExecuteReader();

                if (!dr.Read())
                {
                    dr.Close();
                    Response.Redirect("Default.aspx?Error=Y", false);
                    return;
                }

                Session["Franchise"] = "OK";
                Session["IDNo"] = dr["UserID"];
                Session["UserID"] = dr["FormNo"];
                Session["MemName"] = dr["FranchiseName"];
                Session["Doj"] = Convert.ToDateTime(dr["Doj"]).ToString("dd-MMM-yyyy");

                dr.Close();
                Response.Redirect("Franchise/findex.aspx", false);
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
        }
    }
    protected void BtnSubmit_ServerClick(object sender, EventArgs e)
    {
        uid = Txtuid.Value;
        Pwd = Txtpwd.Value;
        type = Request["ref"];

        // clean input
        uid = uid.Trim().Replace("'", "").Replace("=", "").Replace(";", "");
        Pwd = Pwd.Trim().Replace("'", "").Replace("=", "").Replace(";", "");

        // validate and redirect
        if (!string.IsNullOrEmpty(uid) && !string.IsNullOrEmpty(Pwd))
        {
            if (type == "F")
                enterFranchisePg();
            else
                enterHomePg();
        }
        else
        {
            Response.Redirect("Default.aspx?Error=Y", false);
        }
    }
}
