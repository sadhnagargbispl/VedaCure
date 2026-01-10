using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Data.SqlClient;
using System.Configuration;
public partial class WUCMenu : System.Web.UI.UserControl
{
    DataTable dtMenu = new DataTable();
    DataTable dtMenuMain = new DataTable();
    // DAL objDAL = new DAL(HttpContext.Current.Session["MlmDatabase" + Session["CompID"]]);
    DAL objDAL;
    clsGeneral objGen = new clsGeneral();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            DataTable Dt = new DataTable();
            string str = "";

            if (Session["Status"] != null && Session["Status"].ToString() == "OK")
            {
                //LoadTeam();
                string Strrank = "select  idno,UPPER(MemFirstName + MemLastName) as memname,replace(convert(varchar,doj,106),' ','-') as DOA,ActiveStatus,isblock from m_membermaster where formno = '" + Session["Formno"].ToString() + "'";
                Dt = SqlHelper.ExecuteDataset(Application["Connect"].ToString(), CommandType.Text, Strrank).Tables[0];
                if (Dt.Rows.Count > 0)
                {
                    //LblUSerID.Text = Dt.Rows[0]["idno"].ToString();
                    //LblName.Text = Dt.Rows[0]["memname"].ToString();
                    //datej.Text = Dt.Rows[0]["DOA"].ToString();
                    //if (Dt.Rows[0]["isblock"].ToString() == "Y")
                    //{
                    //    Session.Abandon();
                    //    Response.Redirect("default.aspx", false);
                    //}
                }
                //  string lnt = Crypto.Encrypt("uid=" + Session["IDNo"].ToString() + "&pwd=" + Session["MemPassw"].ToString() + "&mobile=" + Session["MobileNo"].ToString());
            }
        }
    }

}