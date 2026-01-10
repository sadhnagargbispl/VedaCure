using System;
using System.Data;
using System.Data.SqlClient;

public partial class Ranks : System.Web.UI.Page
{
    SqlConnection Conn;
    SqlCommand Comm;
    DataTable Dt;
    SqlDataAdapter Ad;
    string str = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Session["Status"] != null && Session["Status"].ToString() == "OK")
            {
                if (!Page.IsPostBack)
                {
                    RankStatus();
                    //LdbStatus();
                }
            }
            else
            {
                Response.Redirect("logout.aspx");
            }
        }
        catch (Exception)
        {
        }
    }

    private void RankStatus()
    {
        try
        {
            Conn = new SqlConnection(Application["Connect"].ToString());
            Conn.Open();

            string str = "";
            str = " Exec Sp_getrankrewardUpdate '" + Session["Formno"] + "'";

            // Old commented VB SQL left intact
            //str = " Select A.Rankid,A.Rank,A.Criteria,..."

            Comm = new SqlCommand(str, Conn);
            Ad = new SqlDataAdapter(Comm);
            Dt = new DataTable();
            Ad.Fill(Dt);

            GrdRanks.DataSource = Dt;
            GrdRanks.DataBind();

            Conn.Close();
        }
        catch (Exception)
        {
        }
    }

    /* DIRECT VB COMMENTED BLOCK KEPT AS C# COMMENT
    private void LdbStatus()
    {
        Conn = new SqlConnection(Application("Connect"));
        Conn.Open();
        Comm = New SqlCommand("Select A.Rank,A.Criteria,Case When B.RankID is Not NULL then 'Achieved' else 'Pending' end As Status," +
                            " Case When B.RankID is  NULL then '' else Min(dbo.FormatDate (C.ToDate,'dd-MMM-yyyy')) end As AchieveDate " +
                            " From LDBRanksNew As A Left Join (select Min(Sessid)as Sessid,Rankid from MstLDBAchieversTotal where " +
                            " formno='" & Session("Formno") & "' Group By RankId ) As B On A.RankID=B.RankID " +
                            " Left join M_SessnMaster As C On B.SessID=C.SessID  Group By  A.Rank,A.Criteria,b.Rankid,a.Rankid Order by A.RankID")
        Comm.Connection = Conn
        Ad = New SqlDataAdapter(Comm)
        Dt = New DataTable
        Ad.Fill(Dt)
        DataGrid1.DataSource = Dt
        DataGrid1.DataBind()
        Conn.Close()
    }
    */
}
