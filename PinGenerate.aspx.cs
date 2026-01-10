using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;

public partial class PinGenerate : System.Web.UI.Page
{
    SqlConnection Conn;
    SqlCommand Comm;
    DAL Obj;
    DataTable Dt;
    SqlDataAdapter Ad;
    string query;
    double KitAmount = 0;
    double TempKitAmount = 0;
    double Available = 0;
    double TempAvailable = 0;
    double TotalAmount = 0;
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
        try
        {
            BtnGenerate.Attributes.Add("onclick", DisableTheButton(Page, BtnGenerate));
            Conn = new SqlConnection(Application["Connect"].ToString());
            Conn.Open();

            if (Session["Status"]?.ToString() == "OK")
            {
                if (!Page.IsPostBack)
                {
                    FillKit();
                    FillBalance(Convert.ToDouble(Session["FormNo"]));
                }
            }
            else
            {
                Response.Redirect("Logout.aspx");
            }
        }
        catch (Exception)
        {
            if (Conn.State == ConnectionState.Open)
                Conn.Close();
        }
    }

    public void FillKit()
    {
        try
        {
            string condition = "";

            query =
                "Select kitId,KitName,KitAmount From M_KitMaster where AllowTopUp='Y' " +
                "and kitamount <= (select Balance from dbo.ufnGetBalance(" + Session["FormNo"] + ",'E')) " +
                condition +
                " and activeStatus='Y' and RowStatus='Y' and Kitamount>0 and MacAdrs<>'O' Order By kitId";

            Comm = new SqlCommand(query, Conn);
            Ad = new SqlDataAdapter(Comm);
            Dt = new DataTable();
            Ad.Fill(Dt);

            CmbKit.DataSource = Dt;
            CmbKit.DataTextField = "KitName";
            CmbKit.DataValueField = "KitId";
            CmbKit.DataBind();

            Session["GenerateKit"] = Dt;

            if (Dt.Rows.Count > 0)
                Txtpackage.Text = Dt.Rows[0]["Kitamount"].ToString();
        }
        catch (Exception)
        {
        }
    }

    private void FillBalance(double FormNo)
    {
        try
        {
            query = "Select Balance From dbo.ufnGetBalance(" + FormNo + ",'E')";
            Comm = new SqlCommand(query, Conn);
            Ad = new SqlDataAdapter(Comm);
            Dt = new DataTable();
            Ad.Fill(Dt);

            if (Dt.Rows.Count > 0)
            {
                lblAvailable.Text = Dt.Rows[0]["Balance"].ToString();
                Session["Balance"] = Dt.Rows[0]["Balance"];
            }

            Comm.Cancel();
        }
        catch (Exception)
        {
        }
    }

    protected void BtnGenerate_Click(object sender, EventArgs e)
    {
        try
        {
            string scrname = "";

            if (TxtQty.Text == "")
            {
                scrname = "<SCRIPT>alert('Enter Pin Quantity!!');</SCRIPT>";
                RegisterStartupScript("MyAlert", scrname);
                BtnGenerate.Enabled = true;
                return;
            }

            if (Convert.ToDouble(TxtQty.Text) <= 0)
            {
                scrname = "<SCRIPT>alert('Pin Quantity Invalid!!');</SCRIPT>";
                RegisterStartupScript("MyAlert", scrname);
                BtnGenerate.Enabled = true;
                return;
            }

            if (CmbKit.SelectedValue == "")
            {
                scrname = "<SCRIPT>alert('Not valid, please check Balance');</SCRIPT>";
                RegisterStartupScript("MyAlert", scrname);
                BtnGenerate.Enabled = true;
                return;
            }

            // Fetch selected kit details
            string sql = "select KitAmount,KitId,KitName From M_KitMaster where KitId='" + CmbKit.SelectedValue + "'";
            Obj = new DAL(Application["Connect"].ToString());
            Dt = Obj.GetData(sql);

            if (Dt.Rows.Count > 0)
            {
                KitAmount = Convert.ToDouble(Dt.Rows[0]["KitAmount"]);
            }

            TotalAmount = Convert.ToDouble(TxtQty.Text) * KitAmount;

            FillBalance(Convert.ToDouble(Session["FormNo"]));

            if (Convert.ToDouble(Session["Balance"]) < TotalAmount)
            {
                LblError.Text = "";
                scrname = "<SCRIPT>alert('Total Amount Less Then Available Balance!!');</SCRIPT>";
                RegisterStartupScript("MyAlert", scrname);
                BtnGenerate.Enabled = true;
                return;
            }
            else
            {
                PinTransfer();
            }
        }
        catch (Exception)
        {
        }
    }

    protected void PinTransfer()
    {
        try
        {
            BtnGenerate.Enabled = false;

            string scrname = "";
            string formNo = Session["FormNo"].ToString();

            if (TxtQty.Text == "")
            {
                scrname = "<SCRIPT>alert('Enter Pin Quantity!!');</SCRIPT>";
                RegisterStartupScript("MyAlert", scrname);
                BtnGenerate.Enabled = true;
                return;
            }

            if (Convert.ToDouble(TxtQty.Text) <= 0)
            {
                scrname = "<SCRIPT>alert('Pin Quantity Invalid!!');</SCRIPT>";
                RegisterStartupScript("MyAlert", scrname);
                BtnGenerate.Enabled = true;
                return;
            }

            if (CmbKit.SelectedValue == "")
            {
                scrname = "<SCRIPT>alert('Not valid, please check Balance');</SCRIPT>";
                RegisterStartupScript("MyAlert", scrname);
                BtnGenerate.Enabled = true;
                return;
            }

            // Fetch kit details
            string sql = "select KitAmount,KitId,KitName From M_KitMaster where KitId='" + CmbKit.SelectedValue + "'";
            Obj = new DAL(Application["Connect"].ToString());
            Dt = Obj.GetData(sql);

            if (Dt.Rows.Count > 0)
                KitAmount = Convert.ToDouble(Dt.Rows[0]["KitAmount"]);

            TotalAmount = Convert.ToDouble(TxtQty.Text) * KitAmount;

            FillBalance(Convert.ToDouble(Session["FormNo"]));

            if (Convert.ToDouble(Session["Balance"]) >= TotalAmount)
            {
                query =
                    "Exec Generate_EPins_Web '" + Session["IDNo"] + "'," +
                    CmbKit.SelectedValue + "," +
                    Convert.ToInt32(TxtQty.Text) + ";";

                Comm = new SqlCommand(query, Conn);

                int I = Comm.ExecuteNonQuery();

                if (I > 0)
                {
                    scrname = "<SCRIPT>alert('Pin Successfully Generated');</SCRIPT>";
                    RegisterStartupScript("MyAlert", scrname);
                }
                else
                {
                    scrname = "<SCRIPT>alert('Pin Not Generated');</SCRIPT>";
                    RegisterStartupScript("MyAlert", scrname);
                }

                TxtQty.Text = "0";
                TxtTotalAmount.Text = "0";

                FillKit();
                FillBalance(Convert.ToDouble(Session["FormNo"]));
                BtnGenerate.Enabled = true
;
            }
            else
            {
                LblError.Text = "";
                scrname = "<SCRIPT>alert('Total Amount Less Then Available Balance!!');</SCRIPT>";
                RegisterStartupScript("MyAlert", scrname);
                BtnGenerate.Enabled = true;
                return;
            }

            TxtQty.Text = "0";
        }
        catch (Exception)
        {
        }
    }

    protected void Page_LoadComplete(object sender, EventArgs e)
    {
        try
        {
            if (Conn != null && Conn.State == ConnectionState.Open)
                Conn.Close();
        }
        catch (Exception)
        {
        }
    }

    protected void Page_Unload(object sender, EventArgs e)
    {
        try
        {
            if (Conn != null && Conn.State == ConnectionState.Open)
                Conn.Close();
        }
        catch (Exception)
        {
        }
    }

    protected void cmbkit_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataTable Dt = (DataTable)Session["GenerateKit"];
        DataRow[] Dr = Dt.Select("KitID='" + CmbKit.SelectedValue + "'");

        if (Dr.Length > 0)
        {
            Txtpackage.Text = Dr[0]["KitAmount"].ToString();
        }
    }
}
