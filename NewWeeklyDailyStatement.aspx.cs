using System;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI;

public partial class NewWeeklyDailyStatement : System.Web.UI.Page
{
    private DAL obj;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Convert.ToString(Session["Status"]) == "OK")
        {
        }
        else
        {
            Response.Redirect("logout.aspx");
        }

        string connectionString = Convert.ToString(HttpContext.Current.Application["Connect"]);
        SqlConnection conn = new SqlConnection(connectionString);
        DataSet ds4 = new DataSet();
        string IsGlobal = "N";
        try
        {
            conn.Open();

            string payoutNoStr = Convert.ToString(Request["PayoutNo"]);
            int payoutNo = 0;
            int.TryParse(payoutNoStr, out payoutNo);

            string idNo = Convert.ToString(Session["IDNo"]);
            string strQuery = "select * from V#NewWeeklyPayoutDetail where  onwebsite = 'Y' And  Idno='" + idNo + "' and SessId=" + payoutNo;

            using (SqlCommand comm = new SqlCommand(strQuery, conn))
            {
                using (SqlDataAdapter adp = new SqlDataAdapter(comm))
                {
                    adp.Fill(ds4);
                }
            }

            if (ds4.Tables.Count > 0 && ds4.Tables[0].Rows.Count > 0)
            {
                DataRow row = ds4.Tables[0].Rows[0];

                FromDate.InnerText = Convert.ToString(row["FromDate"]);
                ToDate.InnerText = Convert.ToString(row["ToDate"]);
                // SessID.InnerText = Convert.ToString(row["SessID"]);
                MemName.InnerText = Convert.ToString(row["MemName"]);
                Add.InnerText = Convert.ToString(row["Address1"]);
                IDNO.InnerText = Convert.ToString(row["Idno"]);
                City.InnerText = Convert.ToString(row["CityName"]);
                District.InnerText = Convert.ToString(row["DistrictName"]);
                Mobile.InnerText = Convert.ToString(row["Mobl"]);
                PinCode.InnerText = Convert.ToString(row["PinCode"]);
                State.InnerText = Convert.ToString(row["StateName"]);
                generationinc.InnerText = Convert.ToString(row["GenerationInc"]);
                if (Convert.ToString(row["GenerationInc"]) != "0")
                {
                    DivGenerationIncome.Visible = true;
                }
                else
                {
                    DivGenerationIncome.Visible = false;
                }
                globalpool.InnerText = Convert.ToString(row["globalpool"]);
                generationbonus.InnerText = Convert.ToString(row["GenerationBonus"]);
                PairIncome.InnerText = Convert.ToString(row["PairIncome"]);
                // DiamondFund.InnerText = Convert.ToString(row["DiamondFund"]);
                RepurchaseFund.InnerText = Convert.ToString(row["RepurchaseFund"]);
                PairIncentive.InnerText = Convert.ToString(row["PairIncentive"]);
                RePairIncome.InnerText = Convert.ToString(row["RePairIncome"]);
                GrowthIncome.InnerText = Convert.ToString(row["GrowthIncome"]);
                // MagicIncome.InnerText = Convert.ToString(row["MagicIncome"]);
                AmbassadorFund.InnerText = Convert.ToString(row["AmbassadorFund"]);
                BTFund.InnerText = Convert.ToString(row["BTFund"]);
                BikeFund.InnerText = Convert.ToString(row["BikeFund"]);
                LSTFund.InnerText = Convert.ToString(row["LSTFund"]);
                // rankroyalty.InnerText = Convert.ToString(row["RoyaltyIncome"]);

                // lblRank.Text = "[" + Convert.ToString(row["Rank"]) + "]";

                // calculate totals with DBNull safety
                double generationInc1 = row.IsNull("GenerationInc") ? 0.0 : Convert.ToDouble(row["GenerationInc"]);
                double globalPool1 = row.IsNull("globalpool") ? 0.0 : Convert.ToDouble(row["globalpool"]);
                double repairIncome1 = row.IsNull("RePairIncome") ? 0.0 : Convert.ToDouble(row["RePairIncome"]);
                double growthIncome1 = row.IsNull("GrowthIncome") ? 0.0 : Convert.ToDouble(row["GrowthIncome"]);
                double ambassadorFund1 = row.IsNull("AmbassadorFund") ? 0.0 : Convert.ToDouble(row["AmbassadorFund"]);
                double generationbonus1 = row.IsNull("GenerationBonus") ? 0.0 : Convert.ToDouble(row["GenerationBonus"]);

                double totalIncome = generationInc1 + globalPool1 + repairIncome1 + growthIncome1 + ambassadorFund1 + generationbonus1;

                NetIncome.InnerText = totalIncome.ToString("F2");
                Payable.InnerText = totalIncome.ToString("F2");

                Deduction.InnerText = Convert.ToString(row["Deduction"]);
                tdsAmount.InnerText = Convert.ToString(row["TdsAmount"]);
                AdminCharges.InnerText = Convert.ToString(row["AdminCharge"]);
                TotPayable.InnerText = Convert.ToString(row["ChqAmt"]);
                ChqAmount.InnerText = Convert.ToString(row["ChqAmt"]);
                PrevBal.InnerText = Convert.ToString(row["PrevBal"]);
                ClsBal.InnerText = Convert.ToString(row["ClsBal"]);
                // BinaryDeduction.InnerText = Convert.ToString(row["CouponsAmt"]);
            }

            conn.Close();
        }
        catch (Exception)
        {
            // original VB had empty Catch blocks; consider logging here
        }
        try
        {
            string str = "select b.Idno,(b.MemFirstName+''+b.MemLastName) as MemberName,a.BV ," +
                         "slab,Comm as Commission from MstRefIncomeAlter as a,M_MemberMaster as b where a.FormnoDwn=b.Formno " +
                         " and a.Formno='" + Convert.ToString(Session["Formno"]) + "' and a.Sessid=" + Convert.ToInt32(Convert.ToString(Request["PayoutNo"]));

            obj = new DAL(Application["Connect"].ToString());
            DataTable dt = obj.GetData(str);

            if (dt != null && dt.Rows.Count > 0)
            {
                GvData.DataSource = dt;
                GvData.DataBind();
                // TblActive.Visible = True;
            }
            else
            {
                // TblActive.Visible = False;
            }
        }
        catch (Exception)
        {
            // silent catch as original
        }
        try
        {
            int payoutNo2 = 0;
            int.TryParse(Convert.ToString(Request["PayoutNo"]), out payoutNo2);

            string tableName = payoutNo2 < 225 ? "MstLevelIncome" : "MstLevelIncomeAlter";

            string str2 = " select 'L'+Cast(a.Mlevel as Varchar) +'('+ Cast(Slab as Varchar) +'%)' as Mlevel," +
                          " b.Idno as sponsorid,(b.MemFirstName+''+b.MemLastName) as SponsorName,a.Pairincome as sponsorincome," +
                          " Comm as Commission from " + tableName + " as a,M_MemberMaster as b where a.FormnoDwn=b.Formno  " +
                          " and a.Formno='" + Convert.ToString(Session["Formno"]) + "' and a.Sessid=" + Convert.ToInt32(Convert.ToString(Request["PayoutNo"])) + " Order by Mlevel";

            obj = new DAL(Application["Connect"].ToString());
            DataTable dt2 = obj.GetData(str2);

            if (dt2 != null && dt2.Rows.Count > 0)
            {
                GrdLevel.DataSource = dt2;
                GrdLevel.DataBind();
                // Table1.Visible = True;
            }
            else
            {
                // Table1.Visible = False;
            }
        }
        catch (Exception)
        {
            // silent
        }
        try
        {
            string str1 = "Select * From V#ClubFund Where Sessid=" + Convert.ToInt32(Convert.ToString(Request["PayoutNo"])) + " and IDNo='" + Convert.ToString(Session["Formno"]) + "'";
            obj = new DAL(Application["Connect"].ToString());
            DataTable dt3 = obj.GetData(str1);

            if (dt3 != null && dt3.Rows.Count > 0)
            {
                GrdIncentive.DataSource = dt3;
                GrdIncentive.DataBind();
                // Table1.Visible = True;
            }
            else
            {
                // Table1.Visible = False;
            }
        }
        catch (Exception)
        {
            // silent
        }
        try
        {
            string str3 = "Select Cast(MSM.pairCnt as Numeric(18,0)) as [Qualify Point],MM.Rate as [Point Value Of The Month],pairCnt*Rate as income from " +
                          "M_WeeklyPayDetail as PD, M_SessWiseBv as MSM,M_Sessnmaster as MM " +
                          "where MSM.Sessid=MM.Sessid AND MM.Sessid=PD.Sessid and PD.Formno=MSM.Formno" +
                          " And PD.Sessid=" + Convert.ToInt32(Convert.ToString(Request["PayoutNo"])) + " and PD.formno='" + Convert.ToString(Session["Formno"]) + "'";

            obj = new DAL(Application["Connect"].ToString());
            DataTable dt4 = obj.GetData(str3);

            if (dt4 != null && dt4.Rows.Count > 0)
            {
                GrdMachigbv.DataSource = dt4;
                GrdMachigbv.DataBind();
                // Table1.Visible = True;
            }
            else
            {
                // Table1.Visible = False;
            }
        }
        catch (Exception)
        {
            // silent
        }
        try
        {
            string str = "Select B.Rank From (Select FormNo,SessID,Max(RankID) As RankID From MstStarAchievers Group by FormNo,SessID) As A,M_RewardMaster As B Where A.RankID=B.rewardid AND formno = '" + Convert.ToString(Session["Formno"]) + "'";
            obj = new DAL(Application["Connect"].ToString());
            DataTable dt = obj.GetData(str);
            if (dt != null && dt.Rows.Count > 0)
            {
                rank.InnerText = Convert.ToString(dt.Rows[0]["Rank"]);
            }

        }
        catch (Exception)
        {
            // silent catch as original
        }
        try
        {
            string str = "select a.Rank,GSlab as Slab,GrowthBv as Business,GrowthIncome as Income from M_RewardMaster as a,M_SessWiseBv as b ,V#NewWeeklyPayoutDetail as c " +
                         " where b.formno = c.formno AND b.sessid = c.sessid AND b.formno = '" + Convert.ToString(Session["Formno"]) + "'  AND GRankID <> 0 AND GRankID = rewardid AND b.sessid = " + Convert.ToInt32(Convert.ToString(Request["PayoutNo"]));
            obj = new DAL(Application["Connect"].ToString());
            DataTable dt = obj.GetData(str);
            if (dt != null && dt.Rows.Count > 0)
            {
                RptDirects.DataSource = dt;
                RptDirects.DataBind();
                DivStarGrowthIncome.Visible = true;
            }
            else
            {
                DivStarGrowthIncome.Visible = false;
            }
        }
        catch (Exception)
        {
            // silent catch as original
        }
        try
        {
            string str = "select IsGlobal,* from M_SessWiseBv where Formno = '" + Convert.ToString(Session["Formno"]) + "' AND sessid = '" + Convert.ToString(Request["PayoutNo"]) + "' ";
            obj = new DAL(Application["Connect"].ToString());
            DataTable dt = obj.GetData(str);
            if (dt != null && dt.Rows.Count > 0)
            {
                IsGlobal = Convert.ToString(dt.Rows[0]["IsGlobal"]);
            }
            if (IsGlobal.ToString() == "Y")
            {
                string str1 = "Exec Sp_GetCompanyTurnover '" + Convert.ToString(Request["PayoutNo"]) + "','" + Convert.ToString(Session["Formno"]) + "'";
                obj = new DAL(Application["Connect"].ToString());
                DataTable dt1 = obj.GetData(str1);
                if (dt1 != null && dt1.Rows.Count > 0)
                {
                    DivMonthlySelfLeftBV.InnerText = Convert.ToString(dt1.Rows[0]["MonthlySelfLeftBV"]);
                    Divnoofachievers.InnerText = Convert.ToString(dt1.Rows[0]["noofachievers"]);
                    Divincome.InnerText = Convert.ToString(dt1.Rows[0]["income"]);
                    DivGlobalPoolIncome.Visible = true;
                }
                else
                {
                    DivGlobalPoolIncome.Visible = false;
                }
            }
            else
            {
                DivGlobalPoolIncome.Visible = false;
            }
        }
        catch (Exception)
        {
            // silent catch as original
        }
        try
        {
            string str = "Exec Sp_GetGenerationIncome '" + Convert.ToString(Session["Formno"]) + "'," + Convert.ToInt32(Convert.ToString(Request["PayoutNo"]));

            obj = new DAL(Application["Connect"].ToString());
            DataTable dt = obj.GetData(str);

            if (dt != null && dt.Rows.Count > 0)
            {
                RptGenerationIncome.DataSource = dt;
                RptGenerationIncome.DataBind();
                // TblActive.Visible = True;
            }
            else
            {
                // TblActive.Visible = False;
            }
        }
        catch (Exception)
        {
            // silent catch as original
        }
    }
}
