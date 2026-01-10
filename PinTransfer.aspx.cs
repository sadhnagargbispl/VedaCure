using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class PinTransfer : System.Web.UI.Page
{
    SqlDataReader ds;
    SqlDataReader ds1;
    SqlConnection Conn;
    SqlCommand Comm;
    int TransferId;
    DataTable dt1;
    DataTable dt2;
    string MobileNo1 = "";
    SqlDataAdapter Ad;
    string scrname;
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
    protected void Page_Load(object sender, EventArgs e)
    {
        cmdSave1.Attributes.Add("onclick", DisableTheButton(Page, cmdSave1));
        if (!Page.IsPostBack)
        {
            if (Session["Status"] == "OK")
            {
                if (Session["Formno"].ToString() != "1")
                {
                    Fillkit();
                }
                else
                {
                    Response.Redirect("cpindex.aspx");
                }
            }
            else
            {
                Response.Redirect("Logout.aspx");
                Response.End();
            }
        }
    }
    protected void TxtSerialno_TextChanged(object sender, EventArgs e)
    {
        GetName();
    }
    private string GetName()
    {
        SqlConnection conn = new SqlConnection(Application["Connect"].ToString());
        SqlCommand Comm = new SqlCommand();
        SqlDataReader ds;

        conn.Open();

        Comm = new SqlCommand(
            "Select Formno, MemFirstName + MemLastName as MemName " +
            "from M_Membermaster where IdNo='" + TxtSerialno.Text + "'", conn);

        ds = Comm.ExecuteReader();

        if (!ds.Read())
        {
            scrname = "<SCRIPT language='javascript'>alert('Invalid ID Does Not Exists');</SCRIPT>";
            this.RegisterStartupScript("MyAlert", scrname);

            ds.Close();
            conn.Close();

            return "";
        }

        TxtSpName.Text = ds["MemName"].ToString();

        ds.Close();
        Comm.Cancel();
        conn.Close();

        return "OK";
    }
    private void Fillkit()
    {
        Conn = new SqlConnection(Application["Connect"].ToString());
        Conn.Open();

        Comm = new SqlCommand(
            "Select KitId,(Cast(KitName as varchar)) as KitName From M_KitMaster " +
            "Where ActiveStatus='Y' Order By Kitid ", Conn);

        Ad = new SqlDataAdapter(Comm);
        dt1 = new DataTable();
        Ad.Fill(dt1);

        cmbFillItem.DataSource = dt1;
        cmbFillItem.DataValueField = "KitId";
        cmbFillItem.DataTextField = "KitName";
        cmbFillItem.DataBind();

        Comm.Cancel();
        Conn.Close();
    }
    protected void cmdSave1_Click(object sender, EventArgs e)
    {
        TxtSerialno.Text = TxtSerialno.Text.Replace("'", "").Replace(";", "").Replace("=", "");

        if (TxtSerialno.Text == "" || !IsNumeric(TxtSerialno.Text))
        {
            scrname = "<SCRIPT language='javascript'>alert('Invalid Pin');</SCRIPT>";
            this.RegisterStartupScript("MyAlert", scrname);
            return;
        }

        if (txtNormalPin.Text == "0")
        {
            scrname = "<SCRIPT language='javascript'>alert('Please Enter Qty.');</SCRIPT>";
            this.RegisterStartupScript("MyAlert", scrname);
            return;
        }

        SqlDataReader dr;
        Conn = new SqlConnection(Application["Connect"].ToString());
        Conn.Open();

        try
        {
            if (Session["idno"].ToString() == TxtSerialno.Text)
            {
                scrname = "<SCRIPT language='javascript'>alert('You Can\\'t Transfer Pin on Your Own Account');</SCRIPT>";
                this.RegisterStartupScript("MyAlert", scrname);
                return;
            }

            // Validate Member
            Comm = new SqlCommand(
                "select Formno, Mobl from M_Membermaster " +
                "where Formno In (Select Formno from M_Membermaster Where IdNo='" + TxtSerialno.Text + "')",
                Conn);

            dr = Comm.ExecuteReader();

            if (!dr.Read())
            {
                scrname = "<SCRIPT language='javascript'>alert('Invalid IDNo');</SCRIPT>";
                this.RegisterStartupScript("MyAlert", scrname);
                dr.Close();
                return;
            }

            TransferId = Convert.ToInt32(dr["Formno"]);
            MobileNo1 = dr["Mobl"].ToString();
            dr.Close();

            // Check stock of pins
            if (Convert.ToInt32(txtNormalPin.Text) > 0)
            {
                Session["Qty"] = txtNormalPin.Text;

                Comm = new SqlCommand(
                    "select count(formno) as TotalPin from M_Formgeneration " +
                    "where formno not in (select cardno from M_Membermaster) " +
                    "and FCode='" + Session["IDNO"] + "' and Prodid=" + cmbFillItem.SelectedValue, Conn);

                dr = Comm.ExecuteReader();

                if (dr.Read())
                {
                    if (Convert.ToInt32(dr["TotalPin"]) < Convert.ToInt32(txtNormalPin.Text))
                    {
                        scrname = "<SCRIPT language='javascript'>alert('Your stock of Pin is less than required');</SCRIPT>";
                        this.RegisterStartupScript("MyAlert", scrname);
                        dr.Close();
                        return;
                    }
                }
                dr.Close();
            }

            // Execute Pin Transfer
            string query =
                "Exec PinTransfer '" + Session["IDNO"] + "','" + TxtSerialno.Text + "'," +
                txtNormalPin.Text + ",'" + cmbFillItem.SelectedValue + "'";

            Comm = new SqlCommand(query, Conn);
            Comm.ExecuteNonQuery();

            scrname = "<SCRIPT language='javascript'>alert('Successfully Transferred Pin To IDNo');</SCRIPT>";
            this.RegisterStartupScript("MyAlert", scrname);

            txtNormalPin.Text = "0";
            TxtSerialno.Text = "";
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message);
            Response.End();
        }
    }
    private bool IsNumeric(string value)
    {
        return double.TryParse(value, out _);
    }
}