using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Xml;

public partial class MyPurchase : System.Web.UI.Page
{
    // Dim ChinarAPI As New ChinarWebRef.Service
    SqlConnection Conn;
    SqlCommand Comm;
    SqlDataAdapter Ad;
    DataTable dt;
    DAL obj;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Session["Status"] != null && Session["Status"].ToString() == "OK")
            {
                obj = new DAL(Application["Connect"].ToString());
                if (!Page.IsPostBack)
                {
                    string s = " Sp_GetMyPurchaseReport " + Session["FormNo"] + " ";

                    //s = "select * from ( " +
                    //    "select UserBillNo, Replace(Convert(varchar,Billdate,106),' ','-') as BillDate, " +
                    //    "Case When BillType='L' then 0 else BVvalue end as BvValue, " +
                    //    "PVValue, " +
                    //    "Case When BillType='L' then NetPayable else Amount end as Amount, " +
                    //    "Case When BillType='L' then 0 else (TaxAmount+StaxAmount+CGSTAmt) end as TaxAmount, " +
                    //    "NetPayable " +
                    //    "from " + Application["InvDB"] + "..TrnBillMain " +
                    //    "where ActiveStatus='Y' AND FormNo=" + Session["FormNo"] +
                    //    " and Cast(BillDate as Date)>='01-Jan-2020' " +

                    //    "Union all " +

                    //    "select UserBillNo, Replace(Convert(varchar,Billdate,106),' ','-') as BillDate, " +
                    //    "Case When BillType='L' then 0 else BVvalue end as BvValue, " +
                    //    "PVValue, " +
                    //    "Case When BillType='L' then NetPayable else Amount end as Amount, " +
                    //    "Case When BillType='L' then 0 else (TaxAmount+StaxAmount+CGSTAmt) end as TaxAmount, " +
                    //    "NetPayable " +
                    //    "from " + Application["InvDB1"] + "..TrnBillMain " +
                    //    "where ActiveStatus='Y' AND FormNo=" + Session["FormNo"] +
                    //    " and Cast(BillDate as Date)>='01-Jan-2020' ) as Temp " +
                    //    "Order by Year(BillDate) Desc, Month(BillDate) Desc, Day(BillDate) Desc";

                    dt = new DataTable();
                    dt = obj.GetData(s);

                    GrdDirects.DataSource = dt;
                    GrdDirects.DataBind();

                    Session["PurData1"] = dt;
                }
            }
            else
            {
                Response.Redirect("logout.aspx");
            }
        }
        catch (Exception ex)
        {
            // Optional: log exception
        }
    }

    protected void GrdDirects_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
    {
        GrdDirects.CurrentPageIndex = e.NewPageIndex;
        GrdDirects.DataSource = Session["PurData1"];
        GrdDirects.DataBind();
    }

    /*
    private void GetBillData()
    {
        XmlElement Str = ChinarAPI.GetIDWiseSaleXML(Session["IdNo"].ToString());
        DataSet xmlDS = new DataSet();

        using (StringReader stream = new StringReader(Str.InnerXml))
        {
            using (XmlTextReader reader = new XmlTextReader(stream))
            {
                xmlDS.ReadXml(reader);
            }
        }

        GrdDirects.DataSource = xmlDS.Tables[0];
        GrdDirects.DataBind();
    }
    */
}
