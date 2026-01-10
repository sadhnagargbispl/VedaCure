//using DocumentFormat.OpenXml.Drawing.Charts;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Remoting;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class NewJoining : System.Web.UI.Page
{
    double _dblAvailLeg = 0;

    private clsGeneral dbGeneral = new clsGeneral();
    private cls_DataAccess dbConnect;
    private SqlCommand cmd = new SqlCommand();
    private SqlDataReader dRead;
    public string DsnName, UserName, Passw;
    private string strQuery, strCaptcha;
    DataTable tmpTable = new DataTable();
    //AccClass.MyAccClass.NewClass QryCls = new AccClass.MyAccClass.NewClass();
    int minSpnsrNoLen, minScrtchLen;
    double Upln, dblSpons, dblIdNo;
    string dblDistrict, dblTehsil, IfSC, dblState, dblBank;
    string dblPlan;
    DateTime CurrDt;
    string scrname;
    string LastInsertID = "";
    string InVoiceNo;
    int SupplierId;
    string BillNo;
    string TaxType;
    string BillDate;
    int SBillNo;
    string SoldBy = "WR";
    string FType;
    DAL obj;
    private string ClearInject(string StrObj)
    {
        StrObj = StrObj.Replace(";", "").Replace("'", "").Replace("=", "");
        return StrObj;
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
    public string GenerateRandomStringJoining(int length)
    {
        Random rdm = new Random();
        char[] allowChrs = "123456789".ToCharArray();
        string sResult = "";

        for (int i = 0; i < length; i++)
        {
            sResult += allowChrs[rdm.Next(allowChrs.Length)];
        }

        return sResult;
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        // Sanitize Upliner ID
        txtUplinerId.Text = txtUplinerId.Text.Trim().Replace("'", "").Replace("=", "").Replace(";", "");
        dbConnect = new cls_DataAccess(Application["Connect"].ToString());
        try
        {
            CmdSave.Attributes.Add("onclick", DisableTheButton(Page, CmdSave));
            if (!Page.IsPostBack)
            {
                ClrCtrl();
                HdnCheckTrnns.Value = GenerateRandomStringJoining(6);
                RbtnLegNo.Items.Add("Left");
                RbtnLegNo.Items.Add("Right");
                RbtnLegNo.Items[0].Selected = true;

                // --- Check Upliner QueryString ---
                if (!string.IsNullOrEmpty(Request.QueryString["UpLnFormNo"]))
                {
                    txtUplinerId.Text = Request.QueryString["UpLnFormNo"];
                    txtUplinerId.ReadOnly = true;

                    if (Request.QueryString["LegNo"] == "1")
                        RbtnLegNo.Items[0].Selected = true;
                    else
                        RbtnLegNo.Items[1].Selected = true;

                    RbtnLegNo.Enabled = false;
                    Session["iLeg"] = Request.QueryString["LegNo"].ToString();
                }

                // --- Referral QueryString ---
                if (!string.IsNullOrEmpty(Request.QueryString["RefFormNo"]))
                {
                    txtRefralId.Text = Get_IDNo(Request.QueryString["RefFormNo"]);
                    if (txtRefralId.Text.Trim() != "")
                    {
                        FillReferral();
                    }
                    txtRefralId.ReadOnly = true;
                }

                // --- PIN QueryString ---
                if (!string.IsNullOrEmpty(Request.QueryString["Pin"]))
                {
                    txtPIN.Text = Request.QueryString["Pin"];
                    txtPIN.ReadOnly = true;
                }

                // --- Scratch QueryString ---
                if (!string.IsNullOrEmpty(Request.QueryString["scratch"]))
                {
                    txtScratch.Text = Request.QueryString["scratch"];
                    txtPIN.ReadOnly = true;
                }

                //dbGeneral.Fill_Date_box(
                //    ddlDOBdt, ddlDOBmnth, ddlDOBYr,
                //    1940, DateTime.Now.AddYears(-18).Year
                //);

                FillBankMaster();
                FillStateMaster();
                FindSession();
                vsblCtrl(false, true);
            }

            // --- Get Session ID ---
            try
            {
                cls_DataAccess dbConnect2 = new cls_DataAccess(Application["Connect"].ToString());
                dbConnect2.OpenConnection();

                SqlCommand cmd = new SqlCommand(
                    "Select top 1 SessId as SessId from M_MonthSessnMaster order by SessID desc",
                    dbConnect2.cnnObject);

                SqlDataReader dRead = cmd.ExecuteReader();

                if (dRead.Read())
                    Session["Dsessid"] = dRead["SessID"];
                else
                    Session["Dsessid"] = 0;

                dRead.Close();
                cmd.Cancel();
            }
            catch
            {
                // silent catch
            }

            // --- Load Config Master ---
            try
            {
                cls_DataAccess dbConnect3 = new cls_DataAccess(Application["Connect"].ToString());
                dbConnect3.OpenConnection();

                SqlCommand cmd = new SqlCommand(
                    "select * from M_ConfigMaster",
                    dbConnect3.cnnObject);

                SqlDataReader dRead = cmd.ExecuteReader();

                if (dRead.Read())
                {
                    Session["IsGetExtreme"] = dRead["IsGetExtreme"];
                    Session["IsTopUp"] = dRead["IsTopUp"];
                    Session["IsSendSMS"] = dRead["IsSendSMS"];
                    Session["IdNoPrefix"] = dRead["IdNoPrefix"];
                    Session["IsFreeJoin"] = dRead["IsFreeJoin"];
                    Session["IsStartJoin"] = dRead["IsStartJoin"];
                    Session["JoinStartFrm"] = dRead["JoinStartFrm"];
                    Session["IsSubPlan"] = dRead["IsSubPlan"];
                }
                else
                {
                    Session["IsGetExtreme"] = "N";
                    Session["IsTopUp"] = "N";
                    Session["IsSendSMS"] = "N";
                    Session["IdNoPrefix"] = "";
                    Session["IsFreeJoin"] = "N";
                    Session["IsStartJoin"] = "N";
                    Session["JoinStartFrm"] = "01-Sep-2011";
                    Session["IsSubPlan"] = "N";
                }

                dRead.Close();
            }
            catch
            {
                Session["CompName"] = "";
                Session["CompAdd"] = "";
                Session["CompWeb"] = "";
            }

            // Show / Hide Sponsor Row
            if (Session["IsGetExtreme"] != null && Session["IsGetExtreme"].ToString() == "N")
                rwSpnsr.Visible = true;
            else
                rwSpnsr.Visible = false;
        }
        catch (Exception)
        {
            // silent catch as per your VB code
        }
    }
    private string Get_IDNo(string MyFormNo)
    {
        try
        {
            string IdNo = "";

            cls_DataAccess dbConnect = new cls_DataAccess(Application["Connect"].ToString());
            dbConnect.OpenConnection();

            SqlCommand cmd = new SqlCommand(
                "select IdNo from M_MemberMaster WHERE FormNo='" + MyFormNo + "'",
                dbConnect.cnnObject
            );

            SqlDataReader dRead = cmd.ExecuteReader();

            if (dRead.Read())
            {
                IdNo = dRead["IdNo"].ToString();
            }

            dRead.Close();
            return IdNo;
        }
        catch (Exception)
        {
            return "";
        }
    }
    protected void vsblCtrl(bool IsVsbl, bool IsOnlyDv)
    {
        //try
        //{
        //    if (!IsOnlyDv)
        //    {
        //        txtUplinerId.Enabled = !IsVsbl;
        //        txtRefralId.Enabled = !IsVsbl;
        //        txtPIN.Enabled = !IsVsbl;
        //        txtScratch.Enabled = !IsVsbl;
        //    }

        //    dv_Main.Visible = IsVsbl;
        //}
        //catch (Exception)
        //{
        //    // Same silent catch as VB
        //}
    }
    private void ClrCtrl()
    {
        txtAddLn1.Text = "";
        txtEMailId.Text = "";
        txtFNm.Text = "";
        txtFrstNm.Text = "";
        txtMobileNo.Text = "";
        //txtNominee.Text = "";
        //txtPanNo.Text = "";
        //txtPhNo.Text = "";
        txtPinCode.Text = "";
        // txtRelation.Text = "";
        txtUplinerId.Text = "";
        lblUplnrNm.Text = "";
        ddlDistrict.Text = "";
        ddlTehsil.Text = "";
        //TxtBranchName.Text = "";
        //TxtAccountNo.Text = "";
        //txtIfsCode.Text = "";
        txtPIN.Text = "";
        txtScratch.Text = "";
        txtRefralId.Text = "";
        lblRefralNm.Text = "";
        dv_Main.Visible = false;
        txtUplinerId.Enabled = true;
        txtRefralId.Enabled = true;
        txtPIN.Enabled = true;
        txtScratch.Enabled = true;
        //cmdNext.Visible = true;
        RbtnLegNo.Enabled = true;
    }
    private void FillStateMaster()
    {
        try
        {
            strQuery = "SELECT STATECODE, STATENAME as State FROM M_StateDivMaster WHERE ACTIVESTATUS='Y' ORDER BY STATENAME";
            obj = new DAL((string)Application["Connect"]);
            tmpTable = obj.GetData(strQuery);
            CmbState.DataSource = tmpTable;
            CmbState.DataValueField = "STATECODE";
            CmbState.DataTextField = "State";
            CmbState.DataBind();
            CmbState.SelectedIndex = 1;
        }
        catch (Exception)
        {
            // same silent handling as VB
        }
    }
    private void FillBankMaster()
    {
        //try
        //{
        //    strQuery = "SELECT BankCode as Bid, BANKNAME as Bank FROM M_BankMaster WHERE ACTIVESTATUS='Y' ORDER BY BankCode";

        // obj = new DAL((string)Application["Connect"]);
        //tmpTable = obj.GetData(strQuery);
        //    CmbBank.DataSource = tmpTable;
        //    CmbBank.DataValueField = "Bid";
        //    CmbBank.DataTextField = "Bank";
        //    CmbBank.DataBind();
        //    CmbBank.SelectedIndex = 1;
        //}
        //catch (Exception)
        //{
        //}
    }
    public string Validt_SpnsrDtl(string chkby)
    {
        try
        {
            string scrname = "";
            string Validt_SpnsrDtl = "";

            if (Session["IsGetExtreme"].ToString() == "N")
            {
                if (txtUplinerId.Text == "")
                {
                    scrname = "<script>alert('Check Sponsor');</script>";
                    RegisterStartupScript("MyAlert", scrname);
                    txtUplinerId.Focus();
                    CmdSave.Enabled = true;
                    return "";
                }
            }

            txtRefralId.Text = txtRefralId.Text.Trim().Replace("'", "").Replace("=", "").Replace(";", "");
            txtUplinerId.Text = txtUplinerId.Text.Trim().Replace("'", "").Replace("=", "").Replace(";", "");

            dbConnect = new cls_DataAccess(Application["Connect"].ToString());

            // -------- REFERRAL CHECK ----------
            if (txtRefralId.Text.Trim() != "")
            {
                dbConnect.OpenConnection();
                cmd = new SqlCommand(
                    "Select FormNo, MemFirstName + ' ' + MemLastName as MemName from M_MemberMaster " +
                    "where Idno='" + txtRefralId.Text + "'",
                    dbConnect.cnnObject
                );

                dRead = cmd.ExecuteReader();

                if (!dRead.Read())
                {
                    scrname = "<script>alert('Referral ID Not Exist.');</script>";
                    RegisterStartupScript("MyAlert", scrname);
                    vsblCtrl(false, true);
                    dRead.Close();
                    return "";
                }

                Session["Refral"] = dRead["FormNo"];
                lblRefralNm.Text = dRead["MemName"].ToString();
                dv_Main.Visible = true;

                dRead.Close();
                cmd.Cancel();
            }
            else
            {
                scrname = "<script>alert('Check Referral ID.');</script>";
                RegisterStartupScript("MyAlert", scrname);
                txtRefralId.Focus();
                return "";
            }

            // -------- UPLINER CHECK (IF REQUIRED) ----------
            if (Session["IsGetExtreme"].ToString() == "N")
            {
                if (txtUplinerId.Text.Trim() != "")
                {
                    dbConnect.OpenConnection();
                    cmd = new SqlCommand(
                        "Select FormNo, MemFirstName + ' ' + MemLastName as MemName from M_MemberMaster " +
                        "where Idno='" + txtUplinerId.Text + "'",
                        dbConnect.cnnObject);

                    dRead = cmd.ExecuteReader();

                    if (!dRead.Read())
                    {
                        scrname = "<script>alert('Referral ID Not Exist');</script>";
                        RegisterStartupScript("MyAlert", scrname);
                        CmdSave.Enabled = true;
                        vsblCtrl(false, true);
                        dRead.Close();
                        return "";
                    }

                    Session["Uplnr"] = dRead["FormNo"];
                    lblUplnrNm.Text = dRead["MemName"].ToString();
                    Validt_SpnsrDtl = "OK";

                    dRead.Close();
                    cmd.Cancel();
                }
                else
                {
                    scrname = "<script>alert('Referral Sponsor ID');</script>";
                    RegisterStartupScript("MyAlert", scrname);
                    CmdSave.Enabled = true;
                    return "";
                }
            }
            else
            {
                txtUplinerId.Text = "0";
                lblUplnrNm.Text = "";
                Session["Uplnr"] = 0;
            }

            // ---------- CHECK PLACEMENT IN REFERRAL DOWNLINE ----------
            if (Session["IsGetExtreme"].ToString() == "N")
            {
                if (Convert.ToInt32(Session["Refral"]) != Convert.ToInt32(Session["Uplnr"]))
                {
                    dbConnect.OpenConnection();
                    cmd = new SqlCommand(
                        "Select * from M_MemTreeRelation " +
                        "where FormNo=" + Session["Refral"] +
                        " And FormNoDwn=" + Session["Uplnr"],
                        dbConnect.cnnObject);

                    dRead = cmd.ExecuteReader();

                    if (!dRead.Read())
                    {
                        scrname = "<script>alert('Placement Does Not Exist In Sponsor Downline!!');</script>";
                        RegisterStartupScript("MyAlert", scrname);
                        dRead.Close();
                        vsblCtrl(false, true);
                        return "";
                    }

                    dRead.Close();
                    cmd.Cancel();
                }
            }

            // ---------- CHECK LEFT/RIGHT AVAILABILITY ----------
            if (Session["IsGetExtreme"].ToString() == "N")
            {
                if (!checkAvailLeg())
                {
                    vsblCtrl(false, true);
                    return "";
                }
            }

            // ---------- PIN / SCRATCH VALIDATION ----------
            txtPIN.Text = txtPIN.Text.Trim().Replace("'", "").Replace("=", "").Replace(";", "");
            txtScratch.Text = txtScratch.Text.Trim().Replace("'", "").Replace("=", "").Replace(";", "");

            if (Session["IsFreeJoin"].ToString() == "N")
            {
                // PIN used?
                if (txtPIN.Text.Trim() != "")
                {
                    dbConnect.OpenConnection();
                    cmd = new SqlCommand(
                        "Select * from M_FormGeneration where IsIssued='Y' And FormNo=" + txtPIN.Text,
                        dbConnect.cnnObject);

                    dRead = cmd.ExecuteReader();
                    if (dRead.Read())
                    {
                        scrname = "<script>alert('Pin No. Already Used. Enter Another.');</script>";
                        RegisterStartupScript("MyAlert", scrname);
                        dRead.Close();
                        vsblCtrl(false, true);
                        return "";
                    }
                    dRead.Close();
                }
                else
                {
                    scrname = "<script>alert('Check Pin No');</script>";
                    RegisterStartupScript("MyAlert", scrname);
                    txtPIN.Focus();
                    return "";
                }

                // Scratch validation
                if (txtScratch.Text.Trim() != "")
                {
                    dbConnect.OpenConnection();
                    cmd = new SqlCommand(
                        "Select KitM.kitid,KitM.PV,KitM.RP,KitM.KitName,JoinStatus " +
                        "From m_formgeneration as Scrtch " +
                        "Inner Join M_KitMaster as KitM On Scrtch.ProdId=KitM.KitId " +
                        "where Scrtch.IsIssued='N' and Scrtch.ActiveStatus='Y' and KitM.Kitamount=0 " +
                        "and Scrtch.FormNo='" + txtPIN.Text + "' " +
                        "and Scrtch.ScratchNo='" + txtScratch.Text + "'",
                        dbConnect.cnnObject);

                    dRead = cmd.ExecuteReader();

                    if (!dRead.Read())
                    {
                        scrname = "<script>alert('Invalid PIN No. or Scratch No.');</script>";
                        RegisterStartupScript("MyAlert", scrname);
                        dRead.Close();
                        vsblCtrl(false, true);
                        txtPIN.Focus();
                        return "";
                    }

                    Session["Bv"] = dRead["PV"];
                    Session["RP"] = dRead["RP"];
                    Session["Category"] = dRead["KitName"];
                    Session["Kitid"] = dRead["Kitid"];
                    Session["JoinStatus"] = dRead["JoinStatus"];

                    Validt_SpnsrDtl = "OK";

                    dRead.Close();
                    cmd.Cancel();
                }
                else
                {
                    scrname = "<script>alert('Check Scratch No');</script>";
                    RegisterStartupScript("MyAlert", scrname);
                    txtScratch.Focus();
                    return "";
                }
            }

            // Disable controls after validation
            RbtnLegNo.Enabled = false;
            txtUplinerId.Enabled = false;
            txtRefralId.Enabled = false;
            txtPIN.Enabled = false;
            txtScratch.Enabled = false;
            //cmdNext.Visible = false;

            return Validt_SpnsrDtl;
        }
        catch (Exception)
        {
            return "";
        }
    }
    protected void CmdSave_Click(object sender, EventArgs e)
    {
        try
        {
            if (!chkterms.Checked)
            {
                scrname = "<SCRIPT language='javascript'>alert('Please select Terms and Conditions');</SCRIPT>";
                ScriptManager.RegisterClientScriptBlock(Page, GetType(), "Login Error", scrname, false);
                return;
            }
            else if (txtPIN.Text == "")
            {
                scrname = "<SCRIPT language='javascript'>alert('Please Enter Epin No.');</SCRIPT>";
                ScriptManager.RegisterClientScriptBlock(Page, GetType(), "Login Error", scrname, false);
                return;
            }
            else if (txtScratch.Text == "")
            {
                scrname = "<SCRIPT language='javascript'>alert('Please Enter Scratch No.');</SCRIPT>";
                ScriptManager.RegisterClientScriptBlock(Page, GetType(), "Login Error", scrname, false);
                return;
            }
            else if (txtRefralId.Text == "")
            {
                scrname = "<SCRIPT language='javascript'>alert('Please Enter Sponsor ID.');</SCRIPT>";
                ScriptManager.RegisterClientScriptBlock(Page, GetType(), "Login Error", scrname, false);
                return;
            }
            else if (txtUplinerId.Text == "")
            {
                scrname = "<SCRIPT language='javascript'>alert('Please Enter Placement ID.');</SCRIPT>";
                ScriptManager.RegisterClientScriptBlock(Page, GetType(), "Login Error", scrname, false);
                return;
            }
            else if (RbtnLegNo.SelectedValue == "")
            {
                scrname = "<SCRIPT language='javascript'>alert('Please Select Leg.');</SCRIPT>";
                ScriptManager.RegisterClientScriptBlock(Page, GetType(), "Login Error", scrname, false);
                return;
            }
            else if (txtFrstNm.Text == "")
            {
                scrname = "<SCRIPT language='javascript'>alert('Please Enter Name.');</SCRIPT>";
                ScriptManager.RegisterClientScriptBlock(Page, GetType(), "Login Error", scrname, false);
                return;
            }
            else if (txtAddLn1.Text == "")
            {
                scrname = "<SCRIPT language='javascript'>alert('Please Enter Address.');</SCRIPT>";
                ScriptManager.RegisterClientScriptBlock(Page, GetType(), "Login Error", scrname, false);
                return;
            }
            else if (txtMobileNo.Text == "")
            {
                scrname = "<SCRIPT language='javascript'>alert('Please Enter Mobile No.');</SCRIPT>";
                ScriptManager.RegisterClientScriptBlock(Page, GetType(), "Login Error", scrname, false);
                return;
            }
            else if (TxtPasswd.Text == "")
            {
                scrname = "<SCRIPT language='javascript'>alert('Please Enter Password.');</SCRIPT>";
                ScriptManager.RegisterClientScriptBlock(Page, GetType(), "Login Error", scrname, false);
                return;
            }
            else
            {
                SaveIntoDB();
            }
        }
        catch (Exception)
        {
            // silent catch like VB
        }
    }
    private void FindSession()
    {
        try
        {
            dbConnect.OpenConnection();

            cmd = new SqlCommand(
                "Select top 1 SessId as SessId from M_SessnMaster order by SessID desc",
                dbConnect.cnnObject);

            dRead = cmd.ExecuteReader();

            if (dRead.Read())
            {
                Session["SessID"] = dRead["SessID"];
            }
            else
            {
                errMsg.Text = "Session Not Exist. Please Enter New Session.";
                dRead.Close();
                return;
            }

            dRead.Close();
            cmd.Cancel();
        }
        catch (Exception)
        {
            // silent
        }
    }
    private bool checkAvailLeg()
    {
        try
        {
            int iLegNo, iformNo;

            // Determine LEFT / RIGHT leg
            if (RbtnLegNo.SelectedIndex == 0)
                iLegNo = 1;
            else if (RbtnLegNo.SelectedIndex == 1)
                iLegNo = 2;
            else
            {
                scrname = "<SCRIPT language='javascript'>alert('Choose Position.');</SCRIPT>";
                ScriptManager.RegisterClientScriptBlock(Page, GetType(), "Login Error", scrname, false);
                return false;
            }

            // --- GET Upliner Info ---
            cmd = new SqlCommand(
                "SELECT * FROM M_MemberMaster WHERE IdNo='" + txtUplinerId.Text + "'",
                dbConnect.cnnObject
            );

            dRead = cmd.ExecuteReader();

            if (dRead.Read())
            {
                iformNo = Convert.ToInt32(dRead["FormNo"]);
            }
            else
            {
                errMsg.Text = "Check Placeunder Id.";
                scrname = "<SCRIPT language='javascript'>alert('" + errMsg.Text + "');</SCRIPT>";
                ScriptManager.RegisterClientScriptBlock(Page, GetType(), "Login Error", scrname, false);

                dRead.Close();
                return false;
            }

            dRead.Close();

            // --- CHECK IF LEG AVAILABLE ---
            cmd = new SqlCommand(
                "SELECT COUNT(*) AS CNT FROM M_MemberMaster " +
                "WHERE UpLnFormNo=" + iformNo + " AND LegNo=" + iLegNo,
                dbConnect.cnnObject
            );

            dRead = cmd.ExecuteReader();

            if (dRead.Read())
            {
                if (Convert.ToInt32(dRead["CNT"]) > 0)
                {
                    errMsg.Text = (iLegNo == 1 ? "LEFT" : "RIGHT") +
                                  " Position already used, please select correct Position!";

                    scrname = "<SCRIPT language='javascript'>alert('" + errMsg.Text + "');</SCRIPT>";
                    ScriptManager.RegisterClientScriptBlock(Page, GetType(), "Login Error", scrname, false);

                    dRead.Close();
                    CmdSave.Enabled = false;
                    return false;
                }
                else
                {
                    _dblAvailLeg = iformNo;
                    dRead.Close();
                    CmdSave.Enabled = true;
                    return true;
                }
            }
            else
            {
                errMsg.Text = "Error In Position Selection.";
                scrname = "<SCRIPT language='javascript'>alert('" + errMsg.Text + "');</SCRIPT>";
                ScriptManager.RegisterClientScriptBlock(Page, GetType(), "Login Error", scrname, false);

                dRead.Close();
                return false;
            }
        }
        catch (Exception ex)
        {
            string path = HttpContext.Current.Request.Url.AbsoluteUri;
            string text = path + ":  " +
                          DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss:fff") + Environment.NewLine;

            obj.WriteToFile(text + ex.Message);
            Response.Write("Try later.");

            return false;
        }
    }
    private void MaxInvoiceNo()
    {
        try
        {
            strQuery = "SELECT Max(BillNo) + 1 as BillNo FROM M_MemberMAster";
            obj = new DAL((string)Application["Connect"]);
            tmpTable = obj.GetData(strQuery);
            if (tmpTable.Rows.Count > 0)
            {
                DataRow DR = tmpTable.Rows[0];
                InVoiceNo = DR["BillNo"].ToString();
            }

            tmpTable.Clear();
        }
        catch (Exception ex)
        {
            // VB used MsgBox → in ASP.NET we just ignore or log:
            // MsgBox not used in C#
            return;
        }
    }
    private void FindPV()
    {
        try
        {
            dbConnect.OpenConnection();

            cmd = new SqlCommand(
                "Select top 1 PV from M_KitMaster where Kitid = '" + Session["KitId"] + "'",
                dbConnect.cnnObject);

            dRead = cmd.ExecuteReader();

            if (dRead.Read())
            {
                Session["BV"] = dRead["PV"];
            }
            else
            {
                dRead.Close();
                return;
            }

            dRead.Close();
            cmd.Cancel();
        }
        catch (Exception)
        {
        }
    }
    protected void txtUplinerId_TextChanged(object sender, EventArgs e)
    {
        FillSponsor();
    }
    private void FillSponsor()
    {
        try
        {
            errMsg.Text = "";
            lblErrEpin.Text = "";

            int i = 0;

            txtUplinerId.Text = txtUplinerId.Text.Trim()
                                                 .Replace(";", "")
                                                 .Replace("'", "")
                                                 .Replace("=", "");

            strQuery =
                "Select FormNo, MemFirstName + ' ' + MemLastName as MemName " +
                "from M_MemberMaster where IDNo='" + txtUplinerId.Text + "'";

            dbConnect.OpenConnection();
            cmd = new SqlCommand(strQuery, dbConnect.cnnObject);

            dRead = cmd.ExecuteReader();

            if (dRead.Read())
            {
                lblUplnrNm.Text = dRead["MemName"].ToString();
                Session["Uplnr"] = dRead["FormNo"];
                i++;
            }
            else
            {
                errMsg.Text = "Invalid Placement ID!!";
                lblErrEpin.Text = "Invalid Placement ID!!";
            }

            dRead.Close();
            cmd.Cancel();

            if (i == 1)
            {
                checkAvailLeg();
            }
        }
        catch (Exception)
        {
        }
    }
    private void FillReferral()
    {
        try
        {
            lblErrEpin.Text = "";
            errMsg.Text = "";

            txtRefralId.Text = txtRefralId.Text.Trim()
                                               .Replace(";", "")
                                               .Replace("'", "")
                                               .Replace("=", "");

            strQuery =
                "Select FormNo, MemFirstName + ' ' + MemLastName as MemName " +
                "from M_MemberMaster where IDNo='" + txtRefralId.Text + "'";

            dbConnect.OpenConnection();
            cmd = new SqlCommand(strQuery, dbConnect.cnnObject);

            dRead = cmd.ExecuteReader();

            if (dRead.Read())
            {
                lblRefralNm.Text = dRead["MemName"].ToString();
            }
            else
            {
                errMsg.Text = "Invalid Sponsor ID!!";
                lblErrEpin.Text = "Invalid Sponsor ID!!";
            }

            dRead.Close();
            cmd.Cancel();
        }
        catch (Exception)
        {
        }
    }
    protected void CmdCancel_Click(object sender, EventArgs e)
    {
        ClrCtrl();
    }
    protected void txtRefralId_TextChanged(object sender, EventArgs e)
    {
        FillReferral();
    }
    protected void txtPIN_TextChanged(object sender, EventArgs e)
    {
        CheckEpin();
    }
    protected void txtScratch_TextChanged(object sender, EventArgs e)
    {
        CheckEpin();
    }
    private void CheckEpin()
    {
        try
        {
            lblErrEpin.Text = "";
            errMsg.Text = "";

            txtPIN.Text = txtPIN.Text.Trim()
                                     .Replace(";", "")
                                     .Replace("'", "")
                                     .Replace("=", "");

            txtScratch.Text = txtScratch.Text.Trim()
                                             .Replace(";", "")
                                             .Replace("'", "")
                                             .Replace("=", "");

            // ------------------------------
            // Case 1: Both PIN & Scratch given
            // ------------------------------
            if (!string.IsNullOrWhiteSpace(txtPIN.Text) &&
                !string.IsNullOrWhiteSpace(txtScratch.Text))
            {
                dbConnect.OpenConnection();

                int i = 0;

                cmd = new SqlCommand(
                    "Select * from M_FormGeneration as a, M_KitMaster as b " +
                    "where b.RowStatus='Y' and a.Prodid=b.Kitid and b.Kitamount=0 " +
                    "and FormNo=" + txtPIN.Text + " And ScratchNo='" + txtScratch.Text + "'",
                    dbConnect.cnnObject);

                dRead = cmd.ExecuteReader();

                if (!dRead.Read())
                {
                    lblErrEpin.Text = "Invalid Epin & Scratch No.!!";
                    errMsg.Text = "Invalid Epin & Scratch No.!!";
                    i++;
                }

                dRead.Close();
                cmd.Cancel();

                // Epin not valid → exit
                if (i != 0) return;

                // Check if EPIN already used
                cmd = new SqlCommand(
                    "Select * from M_FormGeneration where IsIssued='Y' " +
                    "And FormNo=" + txtPIN.Text + " And ScratchNo='" + txtScratch.Text + "'",
                    dbConnect.cnnObject);

                dRead = cmd.ExecuteReader();

                if (dRead.Read())
                {
                    lblErrEpin.Text = "Already Used, Kindly Check!!";
                    errMsg.Text = "Already Used, Kindly Check!!";
                }

                dRead.Close();
                cmd.Cancel();
            }

            // ------------------------------
            // Case 2: Only PIN entered
            // ------------------------------
            else if (!string.IsNullOrWhiteSpace(txtPIN.Text))
            {
                dbConnect.OpenConnection();

                cmd = new SqlCommand(
                    "Select * from M_FormGeneration as a, M_KitMaster as b " +
                    "where b.RowStatus='Y' and a.Prodid=b.Kitid and b.Kitamount=0 " +
                    "and FormNo=" + txtPIN.Text,
                    dbConnect.cnnObject);

                dRead = cmd.ExecuteReader();

                if (!dRead.Read())
                {
                    lblErrEpin.Text = "Invalid Epin No.!!";
                    errMsg.Text = "Invalid Epin No.!!";
                }

                dRead.Close();
                cmd.Cancel();
            }

            // ------------------------------
            // Case 3: Only Scratch entered
            // ------------------------------
            else if (!string.IsNullOrWhiteSpace(txtScratch.Text))
            {
                dbConnect.OpenConnection();

                cmd = new SqlCommand(
                    "Select * from M_FormGeneration as a, M_KitMaster as b " +
                    "where b.RowStatus='Y' and a.Prodid=b.Kitid and b.Kitamount=0 " +
                    "and ScratchNo='" + txtScratch.Text + "'",
                    dbConnect.cnnObject);

                dRead = cmd.ExecuteReader();

                if (!dRead.Read())
                {
                    lblErrEpin.Text = "Invalid Scratch No.!!";
                    errMsg.Text = "Invalid Scratch No.!!";
                }

                dRead.Close();
                cmd.Cancel();
            }
        }
        catch (Exception)
        {
            // VB ignored errors, so we keep same behavior
        }
    }
    protected void cmdNext_Click(object sender, EventArgs e)
    {
        Validt_SpnsrDtl("");
    }
    private void SaveIntoDB()
    {
        try
        {
            string strQry = "";
            string strDOB, strDOM, strDOJ;
            int iLeg;
            char cGender = 'M';
            char cMarried = 'N';

            string HostIp = Context.Request.UserHostAddress.ToString();
            CmdSave.Enabled = false;

            try
            {
                // --- PAN CARD VALIDATION ---
                //if (txtPanNo.Text.Trim() != "")
                //{
                //    string s = "select Count(Panno) as PanNo from M_Membermaster where Panno='" + txtPanNo.Text.Trim() + "'";
                //    obj = new DAL();
                //    DataTable Dt = obj.GetData(s);

                //    if (Convert.ToInt32(Dt.Rows[0]["PanNo"]) >= 3)
                //    {
                //        CmdSave.Enabled = true;
                //        chkterms.Checked = false;

                //        scrname = "<script>alert('Your Pan card Number already registered on another Ids');</script>";
                //        this.RegisterStartupScript("MyAlert", scrname);
                //        return;
                //    }
                //}

                // Validate Sponsor / Referral Details
                if (Validt_SpnsrDtl("") == "OK")
                {
                    if (RbtnLegNo.SelectedIndex == 0)
                        iLeg = 1;
                    else if (RbtnLegNo.SelectedIndex == 1)
                        iLeg = 2;
                    else
                    {
                        CmdSave.Enabled = true;
                        scrname = "<script>alert('Choose Position.');</script>";
                        this.RegisterStartupScript("MyAlert", scrname);
                        return;
                    }
                    strDOB = TxtDob.Text;
                    strDOM = dbConnect.Get_ServerDate().ToString("dd-MMM-yyyy");
                    strDOJ = strDOM;
                    dblDistrict = ClearInject(ddlDistrict.Text);
                    dblTehsil = ClearInject(ddlTehsil.Text);
                    dblState = CmbState.SelectedValue.ToString();
                    dblBank = "0";
                    IfSC = "";
                    dblPlan = "0";

                    // Ensure Session Exists
                    if (Session["SessID"] == null || Session["SessID"].ToString() == "0")
                    {
                        FindSession();
                    }
                    string Strqueryquer = "";

                    Strqueryquer = "Insert into Trnfundwithdrawcpanel (Transid) values(" + HdnCheckTrnns.Value + ")";
                    int isOk1 = 0;
                    try
                    {
                        isOk1 = Convert.ToInt32(SqlHelper.ExecuteNonQuery((string)Application["Connect"], CommandType.Text, Strqueryquer));
                    }
                    catch (Exception e)
                    {
                        isOk1 = 0;
                    }
                    if (isOk1 > 0)
                    {
                        strQry = "insert into m_memberMaster (" +
                "SessId,IdNo,CardNo,FormNo,KitId," +
                "UpLnFormNo,RefId,LegNo,RefLegNo,RefFormNo," +
                "MemFirstName,MemLastName,MemRelation,MemFName,MemDOB,MemGender," +
                "NomineeName,Address1,Address2,Post," +
                "Tehsil,City,District,StateCode,CountryId," +
                "PinCode,PhN1,Mobl,MarrgDate," +
                "Passw,Doj,Relation,PanNo," +
                "BankID,BranchName,AcNo,IFSCode,EMail,BV," +
                "UpGrdSessId,E_MainPassw,EPassw,ActiveStatus,billNo,RP,HostIp) " +

                "Values(" + Convert.ToInt32(Session["SessID"]) + ",0," + txtPIN.Text +
                ",0," + Convert.ToInt32(Session["Kitid"]) + "," + Convert.ToInt32(Session["Uplnr"]) +
                ",0," + iLeg + ",0," +
                Convert.ToInt32(Session["Refral"]) + ",'" + ClearInject(txtFrstNm.Text) + "',''" +
                ",'" + CmbType.SelectedValue + "','" + ClearInject(txtFNm.Text) + "','" + strDOB + "','" + cGender + "','','" + ClearInject(txtAddLn1.Text) + "','','" +
                "" + "','" + dblTehsil + "','" + dblTehsil + "','" +
                dblDistrict + "'," + dblState + ",1,'" +
                txtPinCode.Text + "','" + txtMobileNo.Text + "','" + txtMobileNo.Text + "','" +
                strDOM + "','" + ClearInject(TxtPasswd.Text) + "','" + strDOJ + "','','','" + dblBank + "','','','" + IfSC + "','" + ClearInject(txtEMailId.Text) + "'," +
                Convert.ToInt32(Session["Bv"]) + ",0,'" + ClearInject(TxtPasswd.Text) + "','" + ClearInject(TxtPasswd.Text) +
                "','" + Session["JoinStatus"] + "','" + InVoiceNo + "','" + Session["RP"] + "','" + HostIp + "')";

                        dbConnect = new cls_DataAccess(Application["Connect"].ToString());
                        dbConnect.OpenConnection();

                        cmd = new SqlCommand(strQry, dbConnect.cnnObject);

                        int isOk = cmd.ExecuteNonQuery();
                        LastInsertID = "0";

                        if (isOk != 0)
                        {
                            // GET LAST INSERTED ID
                            dbConnect.OpenConnection();
                            cmd = new SqlCommand(
                                "SELECT TOP 1 a.IDNO,a.formno,b.IsBill,b.NoOfIDs " +
                                "FROM m_MemberMaster as a,m_KitMaster as b " +
                                "where a.kitid=b.kitid ORDER BY mid DESC",
                                dbConnect.cnnObject);

                            dRead = cmd.ExecuteReader();

                            int isTrio = 0;

                            if (dRead.Read())
                            {
                                LastInsertID = dRead["IDNO"].ToString();
                                Session["Kit"] = dRead["IsBill"];
                                isTrio = Convert.ToInt32(dRead["NoOfIDs"]);
                            }
                            else
                            {
                                LastInsertID = "10001";
                            }

                            dRead.Close();

                            // Add Multiple IDs if applicable
                            if (isTrio == 3)
                            {
                                strQry = "Exec Ins_MultipleIDs '" + LastInsertID + "'";
                                dbConnect.OpenConnection();
                                cmd = new SqlCommand(strQry, dbConnect.cnnObject);
                                cmd.ExecuteNonQuery();
                            }

                            // Send SMS if enabled
                            if (Session["IsSendSMS"].ToString() == "Y")
                            {
                                //sendSMS();
                            }

                            Session["LASTID"] = LastInsertID;
                            Session["Join"] = "YES";

                            Response.Redirect("Welcome.Aspx?IDNo=" + LastInsertID, false);
                        }
                        else
                        {
                            CmdSave.Enabled = true;
                            scrname = "<script>alert('Try Again Later.');</script>";
                            this.RegisterStartupScript("MyAlert", scrname);
                        }
                    }
                    else
                    {
                        Response.Redirect("NewJoining.Aspx");
                    }

                }
            }
            catch (Exception e)
            {
                CmdSave.Enabled = true;
                scrname = "<script>alert('" + e.Message + "');</script>";
                this.RegisterStartupScript("MyAlert", scrname);
                return;
            }
        }
        catch (Exception)
        {
            // Silent catch as per VB code
        }
    }

    protected void RbtnLegNo_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillSponsor();
    }
}