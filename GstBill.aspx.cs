using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class GstBill : System.Web.UI.Page
{
    SqlConnection Conn;
    SqlCommand Comm;
    SqlDataAdapter Ad;
    DataTable dt;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                if (Session["Status"] != null && Session["Status"].ToString() == "OK")
                {
                    Get_BillDetails();
                }
                else
                {
                    Response.Redirect("logout.aspx");
                }

            }
            catch (Exception ex)
            {
                // handle/log error if required
            }
        }
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        int i;
        // For each GridView row
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblRate = e.Row.FindControl("LblRate") as Label;

            if (lblRate != null && lblRate.Visible)
            {
                e.Row.Style["font-size"] = "small";
            }
            else
            {
                e.Row.Style["font-size"] = "smaller";
            }
        }
    }
    public string AmountInWords(string MyNumber)
    {
        // This Function Converts Number to Words (Indian Format)

        string Temp = "";
        string Temp_2 = "";
        string Temp_Num = "";
        string Rupees = "";
        string Ps = "";

        int DecimalPlace;
        int Count;

        string[] Place = new string[10];
        Place[2] = " Thousand ";
        Place[3] = " Lac ";
        Place[4] = " Crore ";
        Place[5] = " Billion ";
        Place[6] = " Trillion ";

        // Convert number to string and trim
        MyNumber = Convert.ToString(Convert.ToDouble(MyNumber)).Trim();

        // Find decimal place
        DecimalPlace = MyNumber.IndexOf('.');

        // If decimal exists
        if (DecimalPlace > -1)
        {
            // Convert Paise
            Temp = (MyNumber.Substring(DecimalPlace + 1) + "00").Substring(0, 2);
            Ps = ConvertTens(Temp);

            // Remove decimal part
            MyNumber = MyNumber.Substring(0, DecimalPlace).Trim();
        }

        Count = 1;

        // Main loop for Rupees
        while (!string.IsNullOrEmpty(MyNumber))
        {
            if ((Count == 1 && MyNumber.Length == 2) || Count == 1)
            {
                Temp = ConvertHundreds(
                    MyNumber.Length >= 3
                    ? MyNumber.Substring(MyNumber.Length - 3)
                    : MyNumber
                );
            }
            else if (MyNumber.Length >= 1 && Count >= 2)
            {
                if (MyNumber.Length == 1)
                    Temp_Num = MyNumber.Substring(0, MyNumber.Length - 1);
                else
                    Temp_Num = MyNumber.Substring(0, MyNumber.Length - 2);

                //MyNumber = MyNumber.Substring(MyNumber.Length - 2);
                if (MyNumber.Length >= 2)
                {
                    MyNumber = MyNumber.Substring(MyNumber.Length - 2);
                }
                else
                {
                    MyNumber = MyNumber.PadLeft(2, '0');
                }
                if (MyNumber.Length == 1)
                    Temp = ConvertDigit(MyNumber.Substring(0, 1));
                else
                    Temp = ConvertTens(MyNumber.Substring(0, 2));

                MyNumber = Temp_Num;
            }
            else
            {
                Temp = "";
                MyNumber = "";
            }

            if (!string.IsNullOrEmpty(Temp))
            {
                Rupees = Temp + Place[Count] + Rupees;
            }

            if (MyNumber.Length <= 2 && Count == 1)
            {
                MyNumber = "";
            }
            else if (MyNumber.Length >= 3 && Count == 1)
            {
                MyNumber = MyNumber.Substring(0, MyNumber.Length - 3);
            }

            Count++;
        }

        // Final Rupees formatting
        if (Rupees == "")
            Rupees = "No Rupees";
        else if (Rupees == "One")
            Rupees = "Rupee One";
        else
            Rupees = Rupees + " Rupees ";

        // Final Paise formatting
        if (Ps == "")
            Ps = "";
        else if (Ps == "One")
            Ps = " And One Paise";
        else
            Ps = " And " + Ps + " Paise";

        return Rupees + Ps + " Only";
    }
    public string ConvertTens(string MyTens)
    {
        string Result = "";

        // Is value between 10 and 19?
        if (Convert.ToInt32(MyTens.Substring(0, 1)) == 1)
        {
            switch (Convert.ToInt32(MyTens))
            {
                case 10: Result = "Ten"; break;
                case 11: Result = "Eleven"; break;
                case 12: Result = "Twelve"; break;
                case 13: Result = "Thirteen"; break;
                case 14: Result = "Fourteen"; break;
                case 15: Result = "Fifteen"; break;
                case 16: Result = "Sixteen"; break;
                case 17: Result = "Seventeen"; break;
                case 18: Result = "Eighteen"; break;
                case 19: Result = "Nineteen"; break;
            }
        }
        else
        {
            // Otherwise it's between 20 and 99
            switch (Convert.ToInt32(MyTens.Substring(0, 1)))
            {
                case 2: Result = "Twenty "; break;
                case 3: Result = "Thirty "; break;
                case 4: Result = "Forty "; break;
                case 5: Result = "Fifty "; break;
                case 6: Result = "Sixty "; break;
                case 7: Result = "Seventy "; break;
                case 8: Result = "Eighty "; break;
                case 9: Result = "Ninety "; break;
            }

            // Convert ones place digit
            Result += ConvertDigit(MyTens.Substring(MyTens.Length - 1));
        }

        return Result;
    }
    public string ConvertHundreds(string MyNumber)
    {
        string Result = "";

        // Exit if there is nothing to convert
        if (Convert.ToInt32(MyNumber) == 0)
            return "";

        // Append leading zeros (ensure 3 digits)
        MyNumber = ("000" + MyNumber).Substring(("000" + MyNumber).Length - 3);

        // Hundreds place
        if (MyNumber.Substring(0, 1) != "0")
        {
            Result = ConvertDigit(MyNumber.Substring(0, 1)) + " Hundred ";
        }

        // Tens place
        if (MyNumber.Substring(1, 1) != "0")
        {
            Result += ConvertTens(MyNumber.Substring(1));
        }
        else
        {
            // Ones place
            Result += ConvertDigit(MyNumber.Substring(2, 1));
        }

        return Result.Trim();
    }
    public string ConvertDigit(string MyDigit)
    {
        switch (Convert.ToInt32(MyDigit))
        {
            case 1: return "One";
            case 2: return "Two";
            case 3: return "Three";
            case 4: return "Four";
            case 5: return "Five";
            case 6: return "Six";
            case 7: return "Seven";
            case 8: return "Eight";
            case 9: return "Nine";
            default: return "";
        }
    }
    private void Get_BillDetails()
    {
        SqlConnection Conn = new SqlConnection(Application["Connect"].ToString());
        Conn.Open();

        string condition = "";
        string id = "";

        if (Request["IdNo"] != null)
            id = Request["IdNo"];
        else
            id = Session["IDNo"].ToString();

        string InvType = "";

        if (Request["Billno"] != null)
        {
            condition = " AND b.UserBillNo='" + Request["Billno"] + "'";
            if (Request["Billno"].StartsWith("S"))
                InvType = Application["InvDB1"].ToString();
            else
                InvType = Application["InvDB"].ToString();
        }

        string sql = "select c.partyName as Party,c.Address1 as PartyAddress,c.TinNo, " +
                     "Case when a.Rate=0 then 'False' else 'True' end as IsVisible, " +
                     "a.FCode,b.PartyName,b.UserBillNo,b.BillDate, " +
                     "a.ProductId,a.ProductName,a.Rate,Cast(a.Qty as int) as Qty, " +
                     "Cast(a.Qty as int)*a.Rate as Amount, a.Cgst,a.CgstAmt,a.SGSTAmt, " +
                     "a.SGST,Cast(a.Qty as int)*a.bv as BV,a.DiscountPer,a.Discount, " +
                     "a.NetAmount,a.Tax, " +
                     "Case when a.TaxType='S' then b.STaxAmount else a.TaxAmount end as TaxAmount, " +
                     "Case when a.taxType='S' then 'True' else 'False' end as GSTVisible, " +
                     "Case when a.TaxType<>'S' then 'True' else 'False' end as TaxVisible, " +
                     "(a.NetAmount+a.CGSTAmt+a.TaxAmount+a.SGSTAmt) as TotalAmount, " +
                     "Case when Convert(Varchar,b.BillDate,112)<20170701 then 'Tax(%)' " +
                     "when Convert(Varchar,b.BillDate,112)>=20170701 and a.TaxType='S' then '' " +
                     "else 'IGST(%)' end as TaxCaption, " +
                     "b.NetPayable,b.RndOff,a.DP,b.CourierName,b.LR,b.LRDate, " +
                     "b.DocketNo,b.DocketDate,b.BillNo,b.BuyerTin,b.Paymode,a.TaxType,c.Cityname " +
                     "from " + InvType + "..TrnBillMain b " +
                     "JOIN " + InvType + "..TrnBillDetails a ON a.BillNo=b.BillNo, " +
                     InvType + "..M_LedgerMaster c " +
                     "where b.SoldBy=c.PartyCode and b.FCode='" + id + "' " +
                     condition + " Order by Rate Desc";

        SqlCommand Comm = new SqlCommand(sql, Conn);
        SqlDataAdapter Ad = new SqlDataAdapter(Comm);
        DataTable dt = new DataTable();
        Ad.Fill(dt);

        if (dt.Rows.Count > 0)
        {
            DataRow r = dt.Rows[0];

            lblOfficeName.Text = r["Party"].ToString();
            lblAddressTop.Text = r["PartyAddress"].ToString();
            lblRegdOffice.Text = "Regd Office : " + Session["CompName"] + ", " + Session["CompAdd"];
            lblInvoiceNoTxt.Text = r["UserBillNo"].ToString();
            lblDistIdtxt.Text = r["FCode"].ToString();
            lblDistNametxt.Text = r["PartyName"].ToString();
            lblDistAddresstxt.Text = "";
            //lblDistAddresstxt.Text = Session["Address"].ToString();
            lblInvoiceDateText.Text = Convert.ToDateTime(r["BillDate"]).ToString("dd-MMM-yyyy");
            lblPCity.Text = r["CityName"].ToString();
            LblBuyer.Text = r["BuyerTin"].ToString() != ""
                ? "Bill BY:" + r["BuyerTin"]
                : "";

            if (r["TaxCaption"].ToString() == "Tax(%)")
            {
                lblVatTax.Text = "Tax Summary";
                LblGstIN.Text = "";
            }
            else if (r["TaxCaption"].ToString() == "IGST(%)")
            {
                lblVatTax.Text = "IGST Summary";
                LblGstIN.Text = "GST IN:" + r["TinNo"];
            }
            else
            {
                lblVatTax.Text = "GST Tax Summary";
                LblGstIN.Text = "GSTIN:" + r["TinNo"];
            }

            Session["TaxType"] = r["TaxType"].ToString().ToUpper().Trim();
            LblPaymentMode.Text = r["PayMode"].ToString();
            lblCourierNameTxt.Text = r["CourierName"].ToString();
            lblCnNoTxt.Text = r["LR"].ToString();
            LblBill.Text = r["BillNo"].ToString();

            lblCNDatetxt.Text = r["LRDate"] == DBNull.Value
                ? ""
                : Convert.ToDateTime(r["LRDate"]).ToString("dd-MMM-yyyy");

            lblRoundOfftxt.Text = r["RndOff"].ToString();
            lblNetPayabletxt.Text = r["NetPayable"].ToString();
            lblinword.Text = AmountInWords(r["NetPayable"].ToString());

            // ---- Total Row ----
            DataRow totalRow = dt.NewRow();
            totalRow["Qty"] = dt.Compute("SUM(Qty)", "");
            totalRow["BV"] = dt.Compute("SUM(BV)", "");
            totalRow["Amount"] = dt.Compute("SUM(Amount)", "");
            totalRow["TaxAmount"] = dt.Compute("SUM(TaxAmount)", "");
            totalRow["TotalAmount"] = dt.Compute("SUM(TotalAmount)", "");
            totalRow["IsVisible"] = true;

            // ---- Column visibility ----
            foreach (DataControlField col in GridView1.Columns)
            {
                if (Session["TaxType"].ToString() == "S")
                {
                    if (col.HeaderText == "Tax(%)" || col.HeaderText == "IGST(%)") col.Visible = false;
                    if (col.HeaderText == "TaxAmount") col.Visible = false;
                    if (col.HeaderText.Contains("CGST") || col.HeaderText.Contains("SGST")) col.Visible = true;
                }
                else
                {
                    if (col.HeaderText == "Tax(%)" || col.HeaderText == "IGST(%)") col.Visible = true;
                    if (col.HeaderText == "TaxAmount") col.Visible = true;
                    if (col.HeaderText.Contains("CGST") || col.HeaderText.Contains("SGST")) col.Visible = false;
                }
            }

            dt.Rows.Add(totalRow);
            GridView1.DataSource = dt;
            GridView1.DataBind();

            // ---- Tax Summary ----
            if (Session["TaxType"].ToString() != "S")
            {
                Comm = new SqlCommand(
                    "Select Tax,Sum(NetAmount) Amount,Sum(TaxAmount) TaxAmount, " +
                    "Round(Sum(NetAmount+TaxAmount),2) NetAmount " +
                    "From " + InvType + "..TrnBillDetails " +
                    "Where NetAmount>0 AND BillNo='" + LblBill.Text + "' Group By Tax", Conn);

                dt = new DataTable();
                new SqlDataAdapter(Comm).Fill(dt);

                RptTax1.DataSource = dt;
                RptTax1.DataBind();
                TrTax.Visible = true;
                TrCGST.Visible = false;
            }
            else
            {
                Comm = new SqlCommand(
                    "Select CGST,Sum(NetAmount) Amount,Sum(CGSTAmt) CGSTAmount, " +
                    "Sum(SGSTAmt) SGSTAmount, " +
                    "Round(Sum(NetAmount+CGSTAmt+SGSTAmt),2) NetAmount " +
                    "From " + InvType + "..TrnBillDetails " +
                    "Where TaxType='S' AND BillNo='" + LblBill.Text + "' " +
                    "and Prodtype='P' Group By CGST", Conn);

                dt = new DataTable();
                new SqlDataAdapter(Comm).Fill(dt);

                RptTax.DataSource = dt;
                RptTax.DataBind();
                TrTax.Visible = false;
                TrCGST.Visible = true;
            }
        }

        Session["DirectData1"] = dt;
        Conn.Close();
    }

}
