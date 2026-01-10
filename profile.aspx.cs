using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class profile : System.Web.UI.Page
{
    double _dblAvailLeg = 0;
    clsGeneral dbGeneral = new clsGeneral();
    cls_DataAccess dbConnect; // not instantiated (matches VB: declared but not New)
    DAL obj;
    SqlCommand cmd = new SqlCommand();
    SqlDataReader dRead;

    string strQuery;
    string strCaptcha;
    DataTable tmpTable = new DataTable();
    // private AccClass.MyAccClass.NewClass QryCls = new AccClass.MyAccClass.NewClass();
    int minSpnsrNoLen;
    int minScrtchLen;
    double Upln;
    double dblSpons;
    double dblTehsil;
    double dblDistrict;
    double dblIdNo;
    DateTime CurrDt;
    string[] montharray = new[] { "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" };
    int LastInsertID = 0;
    string scrname;
    protected void Page_Load(object sender, EventArgs e)
    {
        dbConnect = new cls_DataAccess((string)Application["Connect"]);
        dbConnect.OpenConnection();

        if (Session["Status"] != null && Session["Status"].ToString() == "OK")
        {
            CmdSave.Attributes.Add("onclick", DisableTheButton(Page, CmdSave));
            if (!Page.IsPostBack)
            {
                // FillDate();
                FillStateMaster();
                FillBankMaster();
                FindSession();
                FillDetail();
            }

        }
        else
        {
            Response.Redirect("Logout.aspx");
        }


    }
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
    private void FillDate()
    {
        //for (int i = 1; i <= 31; i++)
        //    ddlDOBdt.Items.Add(i.ToString());
        //
        //for (int i = 1; i <= 12; i++)
        //    ddlDOBmnth.Items.Add(i.ToString());
        //
        //for (int i = 1950; i <= 2031; i++)
        //    ddlDOBYr.Items.Add(i.ToString());
    }
    private string ConvertDateToString(string Month)
    {
        switch (Month)
        {
            case "1": return "JAN";
            case "2": return "FEB";
            case "3": return "Mar";
            case "4": return "Apr";
            case "5": return "May";
            case "6": return "Jun";
            case "7": return "Jul";
            case "8": return "Aug";
            case "9": return "Sep";
            case "10": return "Oct";
            case "11": return "Nov";
            case "12": return "Dec";
        }
        return "";
    }
    private void FillStateMaster()
    {
        strQuery = "SELECT STATECODE, STATENAME AS State FROM M_StateDivMaster WHERE ACTIVESTATUS='Y' ORDER BY STATENAME";
        obj = new DAL((string)Application["Connect"]);
        tmpTable = obj.GetData(strQuery);
        CmbState.DataSource = tmpTable;
        CmbState.DataValueField = "STATECODE";
        CmbState.DataTextField = "State";
        CmbState.DataBind();
        CmbState.SelectedIndex = 1;
    }
    private void FillBankMaster()
    {
        strQuery = "SELECT BankCode AS BID, BANKNAME AS Bank FROM M_BankMaster WHERE ACTIVESTATUS='Y' ORDER BY BANKNAME";
        obj = new DAL((string)Application["Connect"]);
        tmpTable = obj.GetData(strQuery);
        CmbBank.DataSource = tmpTable;
        CmbBank.DataValueField = "BID";
        CmbBank.DataTextField = "Bank";
        CmbBank.DataBind();
        CmbBank.SelectedIndex = 1;
    }
    private void FindSession()
    {
        dbConnect = new cls_DataAccess((string)Application["Connect"]);
        dbConnect.OpenConnection();
        cmd = new SqlCommand(
            "SELECT TOP 1 SessID, ToDate, FrmDate FROM M_SessnMaster ORDER BY SessID DESC",
            dbConnect.cnnObject
        );

        dRead = cmd.ExecuteReader();

        if (dRead.Read())
        {
            Session["SessID"] = dRead["SessID"];

            // Example if you want date formatting later:
            // Session["ToDate"] = $"{((DateTime)dRead["ToDate"]).Day}-{((DateTime)dRead["ToDate"]).ToString("MMM")}-{((DateTime)dRead["ToDate"]).Year}";
            // Session["FrmDate"] = $"{((DateTime)dRead["FrmDate"]).Day}-{((DateTime)dRead["FrmDate"]).ToString("MMM")}-{((DateTime)dRead["FrmDate"]).Year}";
            // Session["CurrDate"] = DateTime.Now.ToString("dd-MMM-yyyy");
        }
        else
        {
            return;
        }

        dRead.Close();
        cmd.Cancel();
    }
    protected void CmdSave_Click(object sender, EventArgs e)
    {
        UpdateDb();
    }
    private bool CheckPANNo(string PAN)
    {
        strQuery = "SELECT PANNo FROM M_MemberMaster WHERE PANNo='" + PAN +
                   "' AND Formno<>'" + Session["Formno"] + "'";

        dbConnect = new cls_DataAccess((string)Application["Connect"]);
        dbConnect.OpenConnection();
        dbConnect.Fill_Data_Tables(strQuery, tmpTable);

        if (tmpTable.Rows.Count > 0)
            return false;

        return true;
    }
    protected void CmdCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("index.aspx");
        // Response.Redirect("Epincpindex.aspx");
    }
    private string ClearInject(string StrObj)
    {
        StrObj = StrObj.Replace(";", "")
                       .Replace("'", "")
                       .Replace("=", "");
        return StrObj;
    }
    private void FillDetail()
    {
        dbConnect = new cls_DataAccess((string)Application["Connect"]);
        dbConnect.OpenConnection();

        // First query (same as VB but not used because overwritten — keeping structure)
        cmd = new SqlCommand(
            "SELECT a.*, " +
            "ISNULL(upMem.MemFirstName + ' ' + upMem.MemLastName, a.MemFirstName + ' ' + a.MemLastName) AS UpName, " +
            "ISNULL(upMem.FormNo, a.FormNo) AS UpId, " +
            "ISNULL(refMem.MemFirstName + ' ' + refMem.MemLastName, a.MemFirstName + ' ' + a.MemLastName) AS RefName, " +
            "ISNULL(refMem.FormNo, a.FormNo) AS RefId " +
            "FROM M_MemberMaster a " +
            "LEFT JOIN M_MemberMaster refMem ON a.refformNo = refMem.FormNo " +
            "LEFT JOIN M_MemberMaster upMem ON a.UplnformNo = upMem.FormNo " +
            "WHERE a.FormNo = '" + Session["Formno"] + "'",
            dbConnect.cnnObject
        );

        // Actual query used in VB code
        cmd = new SqlCommand(
            "EXEC sp_MemDtl ' and mMst.Formno=''" + Session["Formno"] + "'''",
            dbConnect.cnnObject
        );

        dRead = cmd.ExecuteReader();

        if (dRead.Read())
        {
            txtUplinerId.Text = dRead["UpLnId"].ToString();
            lblUplnrNm.Text = dRead["UpLnName"].ToString();
            txtFrstNm.Text = dRead["MemName"].ToString();
            ddlPreFix.SelectedValue = dRead["Prefix"].ToString();
            CmbType.SelectedValue = dRead["MemRelation"].ToString();
            lblPosition.Text = (Convert.ToInt32(dRead["LegNo"]) == 1) ? "Left" : "Right";

            txtFNm.Text = dRead["MemFname"].ToString();
            txtAddLn1.Text = dRead["Address1"].ToString();

            TxtDob.Text = Convert.ToDateTime(dRead["MemDob"]).ToString("dd-MMM-yyyy");

            ddlDistrict.Text = dRead["DistrictName"].ToString();
            ddlTehsil.Text = dRead["CityName"].ToString();
            CmbState.SelectedValue = dRead["StateCode"].ToString();

            txtPinCode.Text = dRead["Pincode"].ToString();
            txtPhNo.Text = dRead["PhN1"].ToString();
            txtMobileNo.Text = dRead["Mobl"].ToString();
            txtEMailId.Text = dRead["EMail"].ToString();
            txtPanNo.Text = dRead["Panno"].ToString();

            txtPanNo.Enabled = string.IsNullOrEmpty(txtPanNo.Text);

            CmbBank.SelectedValue = dRead["BankId"].ToString();

            TxtBranchName.Text = dRead["BranchName"].ToString();
            TxtAccountNo.Text = dRead["Acno"].ToString();
            txtIfsCode.Text = dRead["IFSCode"].ToString();

            txtNominee.Text = dRead["NomineeName"].ToString();
            txtRelation.Text = dRead["Relation"].ToString();
        }

        dRead.Close();
        cmd.Cancel();


        // Enable/Disable controls — same logic as VB
        TxtAccountNo.Enabled = string.IsNullOrEmpty(TxtAccountNo.Text);
        TxtBranchName.Enabled = string.IsNullOrEmpty(TxtBranchName.Text);
        txtIfsCode.Enabled = string.IsNullOrEmpty(txtIfsCode.Text);
    }
    private void UpdateDb()
    {
        string strQry;
        string strDOB;

        try
        {
            // DOB
            // strDOB = ddlDOBdt.Text + "-" + ConvertDateToString(Convert.ToInt32(ddlDOBmnth.Text)) + "-" + ddlDOBYr.Text;
            strDOB = TxtDob.Text.Trim();

            txtPhNo.Text = (txtPhNo.Text == "") ? "0" : txtPhNo.Text;
            txtPinCode.Text = (txtPinCode.Text == "") ? "0" : txtPinCode.Text;

            string s = "";

            // PAN validation
            if (!string.IsNullOrEmpty(txtPanNo.Text))
            {
                s = "SELECT COUNT(Panno) AS PanNo FROM M_MemberMaster WHERE Panno='" + txtPanNo.Text.Trim() + "'";
                DataTable Dt = new DataTable();
                obj = new DAL((string)Application["Connect"]);
                Dt = obj.GetData(s);

                if (Convert.ToInt32(Dt.Rows[0]["PanNo"]) >= 3)
                {
                    CmdSave.Enabled = true;

                    scrname = "<script language='javascript'>alert('Your Pan card Number already registered on another Ids');</script>";
                    this.RegisterStartupScript("MyAlert", scrname);
                    return;
                }
            }

            // Update query
            strQry =
                "UPDATE M_MemberMaster SET " +
                "MemFName='" + ClearInject(txtFNm.Text) + "', " +
                "MemDOB='" + strDOB + "',Prefix='" + ddlPreFix.SelectedValue + "',MemRelation='" + CmbType.SelectedValue + "'," +
                "Address1='" + ClearInject(txtAddLn1.Text) + "', " +
                "City='" + ClearInject(ddlTehsil.Text) + "', " +
                "District='" + ClearInject(ddlDistrict.Text) + "', " +
                "StateCode=" + CmbState.SelectedValue + ", " +
                "PinCode='" + ClearInject(txtPinCode.Text) + "', " +
                "PhN1='" + ClearInject(txtPhNo.Text) + "', " +
                "Mobl='" + ClearInject(txtMobileNo.Text) + "', " +
                "EMail='" + ClearInject(txtEMailId.Text) + "', " +
                "PanNo='" + ClearInject(txtPanNo.Text) + "', " +
                "BankId=" + CmbBank.SelectedValue + ", " +
                "BranchName='" + ClearInject(TxtBranchName.Text) + "', " +
                "AcNo='" + ClearInject(TxtAccountNo.Text) + "', " +
                "IFSCode='" + ClearInject(txtIfsCode.Text) + "', " +
                "NomineeName='" + ClearInject(txtNominee.Text) + "', " +
                "Relation='" + ClearInject(txtRelation.Text) + "' " +
                "WHERE FormNo=" + Session["FormNo"];

            dbConnect = new cls_DataAccess((string)Application["Connect"]);
            dbConnect.OpenConnection();

            // Insert temp record
            string Qry =
                "INSERT INTO TempMemberMaster " +
                "SELECT *, 'Update Profile - " + Context.Request.UserHostAddress.ToString() + "', GETDATE(), 'U' " +
                "FROM M_MemberMaster WHERE FormNo='" + Convert.ToInt32(Session["FormNo"]) + "'";

            dbConnect.Fire_Query(Qry);

            int i = dbConnect.Fire_Query(strQry);

            if (i != 0)
            {
                scrname = "<script language='javascript'>alert('Profile Successfully Updated');</script>";
            }
            else
            {
                scrname = "<script language='javascript'>alert('Try Again Later.');</script>";
            }

            this.RegisterStartupScript("MyAlert", scrname);

            FillDetail();
            return;
        }
        catch (Exception e)
        {
            scrname = "<script language='javascript'>alert('" + e.Message + "');</script>";
            this.RegisterStartupScript("MyAlert", scrname);

            dbGeneral.myMsgBx(e.Message);
            return;
        }
    }

}