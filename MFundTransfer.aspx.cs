using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Configuration;
using System.Web.UI;

public partial class MFundTransfer : System.Web.UI.Page
{
    SqlConnection Conn;
    SqlCommand Comm;
    DataTable Dt;
    SqlDataAdapter Ad;
    SqlDataReader Dr;
    string query;
    private string DisableTheButton(Control pge, Control btn)
    {
        try
        {
            var sb = new System.Text.StringBuilder();
            sb.Append("if (typeof(Page_ClientValidate) == 'function') {");
            sb.Append("if (Page_ClientValidate() == false) { return false; }} ");
            sb.Append("if (confirm('Are you sure to proceed?') == false) { return false; } ");
            sb.Append("this.value = 'Please Wait...';");
            sb.Append("this.disabled = true;");
            sb.Append(pge.Page.GetPostBackEventReference(btn));
            sb.Append(";");
            return sb.ToString();
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
    private string formvalidate()
    {
        double Credit = Convert.ToDouble(TxtCredit.Text);
        double Debit;

        if (!double.TryParse(TxtTransferAmt.Text, out Debit))
        {
            Label1.Text = "Please Enter Valid Amount";
            return "";
        }
        else if (Credit <= 100)
        {
            Label1.Text = "You Do Not Have Enough Amount For FundTrasfer!!!";
            return "";
        }
        else if (Credit <= 0)
        {
            Label1.Text = "Sorry..You do Not have enough amount for transfer!!";
            return "";
        }
        else if (Debit < 100)
        {
            Label1.Text = "Please Enter Valid Amount";
            return "";
        }
        else if (Debit > Credit)
        {
            Label1.Text = "Enter Valid Amount..Amount Must Be Less Then Credit And More Then 100 Rs.!!";
            return "";
        }
        else
        {
            TxtTransferAmt.ReadOnly = true;
        }

        return "OK";
    }

    private double Amount()
    {
        try
        {
            double RtrVal = 0;

            Conn = new SqlConnection(Application["Connect"].ToString());
            Conn.Open();

            Comm = new SqlCommand("Select balance From dbo.ufnGetBalance('" + Session["FormNo"] + "','E')", Conn);
            Dr = Comm.ExecuteReader();

            if (Dr.Read())
            {
                RtrVal = Convert.ToDouble(Dr["Balance"]);
            }

            Dr.Close();
            Comm.Cancel();
            Conn.Close();

            return RtrVal;
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message);
            return 0;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        //BtnSubmit.Attributes.Add("OnClick", "return valid();");
        BtnSubmit.Attributes.Add("onclick", DisableTheButton(Page, BtnSubmit));
        if (Session["Status"]?.ToString() != "OK")
        {
            Response.Redirect("Logout.aspx");
        }
        else
        {
            TxtCredit.Text = Amount().ToString();
        }
    }

    protected void BtnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            Label1.Text = "";

            if (formvalidate() != "OK")
            {
                BtnSubmit.Enabled = true;
                return;
            }

            if (Amount() >= Convert.ToDouble(TxtTransferAmt.Text))
            {
                Conn = new SqlConnection(Application["Connect"].ToString());
                Conn.Open();

                query = "Exec Sp_FundTransfer " + Session["FormNo"] + "," +
                        Session["FormNo"] + "," +
                        TxtTransferAmt.Text + ",'E'";

                Comm = new SqlCommand(query, Conn);
                Dr = Comm.ExecuteReader();

                if (Dr.Read())
                {
                    if (Dr["Msg"].ToString() == "Success")
                    {
                        Label1.Text = "Fund Transfer Successfully!!";
                    }
                    else
                    {
                        Label1.Text = "Try After Some Time!!";
                    }
                }

                Dr.Close();
                Comm.Cancel();
                Conn.Close();

                BtnSubmit.Visible = false;
                TxtTransferAmt.Text = "0";
                TxtTransferAmt.ReadOnly = false;

                TxtCredit.Text = Amount().ToString();
            }
            else
            {
                Label1.Text = "Check available fund!!";
            }
        }
        catch (Exception ex)
        {
            Label1.Text = ex.Message;
        }
    }
}
