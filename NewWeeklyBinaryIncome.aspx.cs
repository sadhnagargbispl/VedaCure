using System;
using System.Data;
using System.Data.SqlClient;

public partial class NewWeeklyBinaryIncome : System.Web.UI.Page
{
    SqlConnection Conn;
    SqlCommand Comm;
    SqlDataAdapter Ad;
    DataTable dt;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Status"] != null && Session["Status"].ToString() == "OK")
        {
            //UserStatus.InnerText = "Welcome " & Session("MemName") & "(" & Session("Formno") & ")" & Session("Company") & ""
        }
        else
        {
            Response.Redirect("logout.aspx");
        }

        if (!Page.IsPostBack)
        {
            Conn = new SqlConnection(Application["Connect"].ToString());
            Conn.Open();

            string str = " select Sessid,PayoutDate," +
                         " PairIncome,PairIncentive,RePairIncome,GrowthIncome,AmbassadorFund,RepurchaseFund, " +
                         " BTFund ,BikeFund,LSTFund,DirectIncome,GenerationInc,GenerationBonus,globalpool," +
                         " NetIncome ,TdsAmount,AdminCharge," +
                         " Deduction,Prevbal,chqAmt,ClsBal " +
                         " from V#NewWeeklyPayoutDetail   where 1=1 And Sessid>=150 and " +
                         " (NetIncome>0)  And Idno = '" + Session["IDNo"] + "' and onwebsite='Y'  Order by PayoutNo Desc";

            Comm = new SqlCommand(str, Conn);
            Ad = new SqlDataAdapter(Comm);
            dt = new DataTable();
            Ad.Fill(dt);

            GrdPayout.DataSource = dt;
            GrdPayout.DataBind();

            Session["DailyPayout"] = dt;

            Comm.Cancel();
            Conn.Close();
        }
    }

    protected void GrdPayout_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
    {
        if (e.CommandName == "Select")
        {
            Response.Redirect("Statement.aspx?PayoutNo=" + e.Item.Cells[0].Text);
        }
    }

    protected void GrdPayout_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
    {
        GrdPayout.CurrentPageIndex = e.NewPageIndex;
        GrdPayout.DataSource = Session["DailyPayout"];
        GrdPayout.DataBind();
    }
}
