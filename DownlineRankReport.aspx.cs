using System;
using System.Data;
using System.Data.SqlClient;
using System.Web;

public partial class DownlineRankReport : System.Web.UI.Page
{
    DataSet Ds;
    DataTable dt;
    SqlConnection conn = new SqlConnection();
    SqlCommand Comm = new SqlCommand();
    SqlDataAdapter Adp;
    SqlConnection Conn;
    SqlDataAdapter Ad;


    clsGeneral objGen = new clsGeneral();
    DAL Obj;


    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            Obj = new DAL(Application["Connect"].ToString());
            if (Session["Status"] != null && Session["Status"].ToString() == "OK")
            {
                if (!IsPostBack)
                {
                    FillCityPinDetail();
                }
            }
            else
            {
                Response.Redirect("logout.aspx");
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

    private void FillCityPinDetail()
    {
        try
        {
 

            string sql = "Select RewardID, Rank from M_RewardMaster Where ActiveStatus = 'Y'";

            dt = Obj.GetData(sql);

            ddlstate.DataSource = dt;
            ddlstate.DataTextField = "Rank";
            ddlstate.DataValueField = "RewardID";
            ddlstate.DataBind();

            // Default item (no company condition)
            ddlstate.Items.Insert(0, "--Select Rank--");
        }
        catch (Exception ex)
        {
            string path = HttpContext.Current.Request.Url.AbsoluteUri;
            string text = path + ":  " + DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss:fff") + Environment.NewLine;
            objGen.WriteToFile(text + ex.Message);
        }
    }

    private void FillReport()
    {
        try
        {

            string sql = "";

            sql = " Select d.Idno as [ID No] ,d.MemFirstName as [Member Name],";
            sql += " (Case When R.legno= 1 Then 'Left' else 'Right' End) As Postion, ";
            sql += " b.Rank,Replace(Convert(Varchar,Isnull(c.ToDate,Getdate()),106),' ','-') as Date";
            sql += " from MstRewardAchievers as A left join M_RewardMaster As b on a.RewardID = b.RewardID";
            sql += " left join D_SessnMaster as c on a.SessID = c.SessID";
            sql += " Left join M_memberMaster as d on a.formno = d.formno";
            sql += " Left join M_Memtreerelation as R on R.FormnoDwn = d.formno";
            sql += " Where R.formno = " + Session["FormNo"];

            if (ddlstate.SelectedIndex > 0)
            {
                sql += " And b.RewardID = " + ddlstate.SelectedValue;
            }

            if (!string.IsNullOrEmpty(txtMemberID.Text))
            {
                sql += " And d.Idno = '" + txtMemberID.Text + "'";
            }

            if (rbtnsearch.SelectedValue != "L")
            {
                sql += " And R.legno = " + rbtnsearch.SelectedValue;
            }

            var dal = new DAL(Application["Connect"].ToString());

            dt = Obj.GetData(sql);

            RptDirects.DataSource = dt;
            RptDirects.DataBind();

            // Total count
            lbltotal.Text = dt.Rows.Count.ToString();
        }
        catch (Exception ex)
        {
           
        }
    }

      protected void BtnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            FillReport();
        }
        catch (Exception ex)
        {
            string path = HttpContext.Current.Request.Url.AbsoluteUri;
            string text = path + ":  " +
                          DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss:fff") +
                          Environment.NewLine;

            objGen.WriteToFile(text + ex.Message);
        }
    }
}