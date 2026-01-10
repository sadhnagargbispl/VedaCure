using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

public partial class Totalteam : System.Web.UI.Page
{
    string Query;
    SqlDataAdapter Adp;
    DataTable Dt;
    string constr = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Status"] != null && Session["Status"].ToString() == "OK")
        {
            if (!Page.IsPostBack)
            {
                FillDetail();
            }
        }
        else
        {
            Response.Redirect("Logout.aspx");
        }
    }

    private void FillDetail()
    {
        try
        {
            DAL obj = new DAL(Application["Connect"].ToString());

            DateTime startDate;
            DateTime endDate;

            // START DATE
            if (string.IsNullOrEmpty(txtStartDate.Text))
            {
                string sql = "select * from M_companyMaster";
                DataTable dtCo = obj.GetData(sql);

                if (dtCo.Rows.Count > 0)
                {
                    startDate = Convert.ToDateTime(dtCo.Rows[0]["RecTimeStamp"]);
                }
                else
                {
                    startDate = DateTime.Now;
                }
            }
            else
            {
                startDate = Convert.ToDateTime(txtStartDate.Text);
            }

            // END DATE
            if (string.IsNullOrEmpty(txtEndDate.Text))
            {
                endDate = DateTime.Now;
            }
            else
            {
                endDate = Convert.ToDateTime(txtEndDate.Text);
            }

            // STORE PROCEDURE PARAMETERS
            SqlParameter[] prms = new SqlParameter[3];
            prms[0] = new SqlParameter("@FormNo", Session["FormNo"]);
            prms[1] = new SqlParameter("@Frmdate", startDate);
            prms[2] = new SqlParameter("@ToDate", endDate);

            DataSet ds = SqlHelper.ExecuteDataset(constr, "sp_TotalTeam", prms);

            if (ds.Tables[0].Rows.Count > 0)
            {
                DataRow row = ds.Tables[0].Rows[0];

                LeftReg.InnerText = row["LeftRegister"].ToString();
                RightReg.InnerText = row["RightRegister"].ToString();
                TotalReg.InnerText =
                    Convert.ToInt32(row["LeftRegister"]) + Convert.ToInt32(row["RightRegister"]) + "";

                LeftActivation.InnerText = row["LeftActive"].ToString();
                RightActivation.InnerText = row["RightActive"].ToString();
                TotalActv.InnerText =
                    Convert.ToInt32(row["LeftActive"]) + Convert.ToInt32(row["RightActive"]) + "";

                TdLeftSmartPack.InnerText = row["LeftSmartpack"].ToString();
                TdRightSmartPack.InnerText = row["RightSmartpack"].ToString();
                TdTotalsmartPack.InnerText =
                    Convert.ToInt32(row["LeftSmartpack"]) + Convert.ToInt32(row["RightSmartpack"]) + "";

                TdLeftPremium.InnerText = row["LeftPremiumPack"].ToString();
                TdRightPremium.InnerText = row["RightPremiumPack"].ToString();
                decimal left = row["LeftPremiumPack"] == DBNull.Value ? 0 : Convert.ToDecimal(row["LeftPremiumPack"]);
                decimal right = row["RightPremiumPack"] == DBNull.Value ? 0 : Convert.ToDecimal(row["RightPremiumPack"]);

                TdTotalPremium.InnerText = (left + right).ToString();

                //TdTotalPremium.InnerText = Convert.ToInt32(row["LeftPremiumPack"]) + Convert.ToInt32(row["RightPremiumPack"]) + "";

                TdLeftBv.InnerText = row["LeftBV"].ToString();
                TdRightBv.InnerText = row["RightBV"].ToString();
                TotalTeamBv.InnerText =
                    Convert.ToInt32(row["LeftBV"]) + Convert.ToInt32(row["RightBV"]) + "";
            }
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message);
        }
    }

    protected void Btnsubmit_Click(object sender, EventArgs e)
    {
        FillDetail();
    }
}
