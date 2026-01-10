using DocumentFormat.OpenXml.Bibliography;
using DocumentFormat.OpenXml.Wordprocessing;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Net;

public partial class welcome : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["constr"].ConnectionString);

    //protected void Page_Load(object sender, EventArgs e)
    //{
    //    if (!IsPostBack)
    //    {
    //        if (Session["Formno"] != null)
    //        {
    //            LoadMember(Session["Formno"].ToString());
    //        }
    //    }
    //}
    protected void Page_Load(object sender, EventArgs e)
    {
        string strcondition = "";
        string str = "";
        string k = "";
        DataTable dt = new DataTable();
        if (!Page.IsPostBack)
        {
            if (Request["id"] != null)
            {
                k = Request["id"].Replace(" ", "+");
                string s = Crypto.Decrypt(k);
                string[] sbstr = s.Split('/');

                strcondition = " and mMst.FormNo=''" + sbstr[0] + "''";
            }
            else
            {
                if (Session["JOIN"] != null && Session["JOIN"].ToString() == "YES")
                {
                    strcondition = " and mMst.IDNo=''" + Session["LASTID"] + "''";
                    Session["JOIN"] = "FINISH";
                }
                else if (Session["Status"] != null && Session["Status"].ToString() == "OK")
                {
                    strcondition = " and mMst.FormNo=''" + Convert.ToInt32(Session["Formno"]) + "''";
                }
                else
                {
                    Response.Redirect("Default.aspx");
                    Response.End();
                }
            }
            SqlConnection conn = new SqlConnection(Application["Connect"].ToString());
            conn.Open();

            SqlCommand Comm = new SqlCommand("exec sp_MemDtl '" + strcondition + "'", conn);
            SqlDataReader ds = Comm.ExecuteReader();

            if (ds.Read())
            {
                LblIdno.Text = ds["Idno"].ToString();
                LblName.Text = ds["MEMNAME"].ToString();
                LblAddress.Text = ds["Address1"].ToString();
                Lblpincode.Text = ds["PinCode"].ToString();
                LblCity.Text = ds["CityName"].ToString();

                lblDoj.Text = Convert.ToDateTime(ds["Doj"]).ToString("dd-MMM-yyyy");

                LblPlacementid.Text = ds["RefIDNo"].ToString();
                LblPlacementName.Text = ds["RefName"].ToString();
                // UplnrId.InnerText = ds["UpLnId"].ToString();
                // UplnrName.InnerText = ds["UpLnName"].ToString();

                Lblkitbv.Text = ds["KitBV"].ToString();
                Lbljoiningtype.Text = ds["Category"].ToString();
            }

            ds.Close();
            conn.Close();
        }

        //string strcondition = "";

        //if (Session["JOIN"] != null && Session["JOIN"].ToString() == "YES")
        //{
        //    strcondition = " and mMst.IDNo=''" + Session["LASTID"] + "''";
        //    Session["JOIN"] = "FINISH";
        //}
        //else if (Session["STATUS"] != null && Session["STATUS"].ToString() == "OK")
        //{
        //    strcondition = " and mMst.FormNo=''" + Convert.ToInt32(Session["Formno"]) + "''";
        //}
        //else
        //{
        //    Response.End();
        //    return;
        //}



        //  Upliner.Visible = false;
    }

    //private void LoadMember(string formno)
    //{
    //    SqlCommand cmd = new SqlCommand(@"
    //        SELECT 
    //           memfirstname as  Name, IDNo, address1 as Address, City, District, statecode as State,pincode,
    //           mobl as  Mobile, Email, PanNo, DOJ,
    //            0 as SponsorID, '' as SponsorName,
    //          bv as   KitAmount,passw as  Password,epassw as  TransPassword
    //        FROM M_MemberMaster WHERE FormNo=@FormNo
    //    ", con);

    //    cmd.Parameters.AddWithValue("@FormNo", formno);

    //    SqlDataAdapter da = new SqlDataAdapter(cmd);
    //    DataTable dt = new DataTable();
    //    da.Fill(dt);

    //    if (dt.Rows.Count == 0) return;

    //    DataRow dr = dt.Rows[0];

    //    LblName.Text = dr["Name"].ToString();
    //    LblIdno.Text = dr["IDNo"].ToString();
    //    LblAddress.Text = dr["Address"].ToString();
    //    LblCity.Text = dr["City"].ToString();
    //    Lblpincode.Text = dr["pincode"].ToString();
    //    LblState.Text = dr["State"].ToString();

    //    lblDoj.Text = Convert.ToDateTime(dr["DOJ"]).ToString("dd-MMM-yyyy");
    //    LblMobl.Text = dr["Mobile"].ToString();
    //    //LblEmail.Text = dr["Email"].ToString();
    //    //LblPanno.Text = dr["PanNo"].ToString();

    //    LblPlacementid.Text = dr["SponsorID"].ToString();
    //    LblPlacementName.Text = dr["SponsorName"].ToString();

    //    //LblKitAmount.Text = dr["KitAmount"].ToString();

    //    //LblPassw.Text = dr["Password"].ToString();
    //    //LblEPassw.Text = dr["TransPassword"].ToString();
    //}
}
