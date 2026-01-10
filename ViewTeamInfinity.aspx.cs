using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ViewTeamInfinity : System.Web.UI.Page
{
    DataTable dtData = new DataTable();
    DAL objDAL;
    ModuleFunction objModuleFun;
    string ReqNo;


    protected void Page_Init(object sender, EventArgs e)
    {
        //if (Session["Status"].ToString() != "OK")
        //{
        //    Response.Redirect("Logout.aspx");
        //}
    }
    protected void RptDirects_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            GvData.PageIndex = e.NewPageIndex;
            BindData();
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + ex.Message + "');", true);
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            objDAL = new DAL(Application["Connect"].ToString());
            string scrname;
            objModuleFun = new ModuleFunction(Application["Connect"].ToString());

            if (!Page.IsPostBack)
            {
                if (Session["Status"] != null)
                {
                    if (!string.IsNullOrEmpty(Request["Sessid"]))
                    {
                        BindData();
                    }
                }
                else
                {
                    scrname = "<SCRIPT language='javascript'> window.top.location.reload();" + "</SCRIPT>";
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "Close", scrname, false);
                }
            }
        }
        catch (Exception ex)
        {

        }
    }
    public void BindData(string SrchCond = "")
    {
        try
        {
            DataTable dt = new DataTable();
            DataSet Ds = new DataSet();
            string cond = "";
            string formno = "";
            string sql1 = " Select A.SessID,Replace(Convert(Varchar,C.FrmDate,106),' ','-') As FrmDate,B.IDNo,B.MemFirstName," +
              " a.Mlevel,CAST(ROUND(A.Comm,10) AS DECIMAL(10, 4)) Comm From GenerationIncome As A Inner join M_MemberMaster As B On A.FormNoDwn=B.FormNo" +
              " Inner Join M_SessnMaster As C On A.SessID=C.SessID Where " +
              " A.FormNo='" + Session["formno"] + "' " +
              " And A.SessID='" + Request["Sessid"] + "' and Comm>0 Order by a.Sessid";
            Ds = SqlHelper.ExecuteDataset(Application["Connect"].ToString(), CommandType.Text, sql1);
            dt = Ds.Tables[0];
            Session["GDatalevel"] = dt;
            if (dt.Rows.Count > 0)
            {
                GvData.DataSource = dt;
                GvData.DataBind();
            }
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
    protected void GvData_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            GvData.PageIndex = e.NewPageIndex;
            GvData.DataSource = Session["GDatalevel"];
            GvData.DataBind();
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + ex.Message + "');", true);
        }
    }
}
