using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Index : System.Web.UI.Page
{
    DAL Obj;
    DataSet Ds;
    clsGeneral objGen = new clsGeneral();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Session["Status"] != null && Session["Status"].ToString() == "OK")
            {
                Session["TransID"] = DateTime.Now.ToString("yyyyMMddhhmmssfff");

                if (!Page.IsPostBack)
                {
                    LoadTeam();
                }
            }
            else
            {
                Response.Redirect("Default.aspx");
            }
        }
        catch (Exception ex)
        {
            string path = HttpContext.Current.Request.Url.AbsoluteUri;
            string text = path + ":  " + DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss:fff") + Environment.NewLine;
            Obj.WriteToFile(text + ex.Message);
            Response.Write("Try later.");
        }
    }
    private void LoadTeam()
    {
        try
        {
            DataSet Ds = new DataSet();
            SqlParameter[] prms = new SqlParameter[1];
            prms[0] = new SqlParameter("@FormNo", Session["FormNo"]);

            Ds = SqlHelper.ExecuteDataset(Application["Connect"].ToString(), "Sp_LoadMenuData", prms);
            if (Ds.Tables[0].Rows.Count > 0)
            {
                LblMobileNob.Text = Ds.Tables[0].Rows[0]["MobileNo"].ToString();
                Lblemail.Text = Ds.Tables[0].Rows[0]["CompanyMail"].ToString();
                LblWebsite.Text = Ds.Tables[0].Rows[0]["WebSite"].ToString();
            }
            if (Ds.Tables[1].Rows.Count > 0)
            {
                //Image2.ImageUrl = Ds.Tables[1].Rows[0]["ProfilePic"].ToString();
                //LblMemId.Text = Ds.Tables[1].Rows[0]["idno"].ToString();
                //LblMemName.Text = Ds.Tables[1].Rows[0]["Name"].ToString();
               // LblJoiningDate.Text = Ds.Tables[1].Rows[0]["DOJ"].ToString();
               // LblMobileno.Text = Ds.Tables[1].Rows[0]["MobileNo"].ToString();
            }
            if (Ds.Tables[2].Rows.Count > 0)
            {
                LblTotalIncentive.Text = Ds.Tables[2].Rows[0]["TotalIncentive"].ToString();
            }
            if (Ds.Tables[3].Rows.Count > 0)
            {
                LblEWalletBalnace.Text = Ds.Tables[3].Rows[0]["EWallet"].ToString();
                LblRWalletBalnace.Text = Ds.Tables[3].Rows[0]["RWallet"].ToString();
                LblGWalletBalnace.Text = Ds.Tables[3].Rows[0]["GWallet"].ToString();
            }
            if (Ds.Tables[4].Rows.Count > 0)
            {
                RptActivation.DataSource = Ds.Tables[4];
                RptActivation.DataBind();
            }
            if (Ds.Tables[5].Rows.Count > 0)
            {
                RptRepuchase.DataSource = Ds.Tables[5];
                RptRepuchase.DataBind();
            }
            if (Ds.Tables[6].Rows.Count > 0)
            {
                RptSelfBV.DataSource = Ds.Tables[6];
                RptSelfBV.DataBind();
            }
        }
        catch (Exception ex)
        {
            string path = HttpContext.Current.Request.Url.AbsoluteUri;
            string text = path + ":  " + DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss:fff") + Environment.NewLine;
            Obj.WriteToFile(text + ex.Message);
            Response.Write("Try later.");
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {

    }
}