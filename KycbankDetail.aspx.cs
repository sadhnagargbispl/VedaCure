using ClosedXML.Excel;
using System;
using System.CodeDom;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Net.Mail;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Activities.Expressions;
using System.Collections.Generic;
using System.IdentityModel.Protocols.WSTrust;
using System.Security.Principal;
using System.Drawing.Imaging;
using System.Linq;
public partial class KycbankDetail : System.Web.UI.Page
{
    double dblBank;
    DataTable tmpTable = new DataTable();
    DAL Obj;
    clsGeneral objGen = new clsGeneral();
    string constr = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;

    DAL ObjDal;
    protected void Page_Load(object sender, EventArgs e)
    {
        ObjDal = new DAL(Application["Connect"].ToString());
        try
        {
            BtnIdentity.Attributes.Add("onclick", DisableTheButton(Page, BtnIdentity));
            if (Session["Status"] != null && Session["Status"].ToString() == "OK")
            {
                if (!Page.IsPostBack)
                {
                    FillBankMaster();
                    LoadImages();
                }
            }
            else
            {
                Response.Redirect("logout.aspx");
            }
        }
        catch (Exception ex)
        {
            string path = HttpContext.Current.Request.Url.AbsoluteUri;
            string text = path + ":  " + DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss:fff ") + Environment.NewLine;
            Response.Write("Try later.");
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
    private void FillBankMaster()
    {
        try
        {
            Obj = new DAL(Application["Connect"].ToString());
            string Strquery = "";
            Strquery = "SELECT BankCode as Bid, BANKNAME as Bank FROM M_BankMaster WHERE ACTIVESTATUS='Y' ORDER BY BANKCode";
            tmpTable = SqlHelper.ExecuteDataset(Application["Connect"].ToString(), CommandType.Text, Strquery).Tables[0];
            cmbbank.DataSource = tmpTable;
            cmbbank.DataValueField = "Bid";
            cmbbank.DataTextField = "Bank";
            cmbbank.DataBind();
            cmbbank.SelectedIndex = 0;
        }
        catch (Exception ex)
        {
            string path = HttpContext.Current.Request.Url.AbsoluteUri;
            string text = path + ":  " + DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss:fff ") + Environment.NewLine;
            Obj.WriteToFile(text + ex.Message);
            Response.Write("Try later.");
        }
    }
    protected void CmbBank_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (cmbbank.SelectedItem.Text.ToUpper() == "OTHERS")
        {
            divBank.Visible = true;
            Txtbank.Focus();
            Txtbank.Text = "";
        }
        else
        {
            divBank.Visible = false;
            Txtbank.Text = "";
            Txtbranch.Focus();
        }
    }
    private void LoadImages()
    {
        try
        {
            int c = 0;
            string status = "";
            string str = "";
            DataTable dt = new DataTable();
            str = "SELECT a.IDNo, a.MemFirstName AS MemName, a.Panno, a.Acno, a.BAnkid, a.IFscode, a.Fax, a.Branchname, b.BankProof, " +
            "CASE WHEN b.ISbankverified <> 'N' THEN REPLACE(CONVERT(VARCHAR, b.BankVerifyDate, 106), ' ', '-') ELSE '' END AS BankProofDate, " +
            "b.IsBankVerified, " +
            "CASE WHEN b.IsBankVerified = 'Y' THEN 'Verified' " +
            "WHEN b.IsBankVerified = 'P' THEN 'Pending' " +
            "WHEN b.IsBankVerified = 'R' THEN 'Rejected' " +
            "ELSE 'Verification Due' END AS BankVerf, " +
            "CASE WHEN b.IsBankVerified = 'R' THEN b.BankProofRemark ELSE '' END AS RejectRemark, " +
            "ISNULL(f.Reason, ' ') AS RejectReason " +
            "FROM M_MemberMaster AS a " +
            "INNER JOIN KycVerify AS b ON a.FormNo = b.FormNo " +
            "LEFT JOIN M_KycReject AS f ON b.BankRejectId = f.Kid " +
            "WHERE a.FormNo = '" + Session["FormNo"] + "'";
            dt = SqlHelper.ExecuteDataset(Application["Connect"].ToString(), CommandType.Text, str).Tables[0];
            if (dt.Rows.Count > 0)
            {
                lblid.Text = dt.Rows[0]["idno"].ToString();
                hdnSessn.Value = Crypto.Encrypt(dt.Rows[0]["idno"].ToString());
                LblRemark.Text = dt.Rows[0]["RejectRemark"].ToString();
                LbLrejectRemark.Text = dt.Rows[0]["RejectReason"].ToString();
                status = dt.Rows[0]["BankVerf"].ToString();
                Txtacno.Text = dt.Rows[0]["Acno"].ToString();
                lblverstatus.Text = dt.Rows[0]["BankVerf"].ToString();
                Txtcode.Text = dt.Rows[0]["IFscode"].ToString();
                Txtbranch.Text = dt.Rows[0]["Branchname"].ToString();
                cmbbank.SelectedValue = dt.Rows[0]["BAnkid"].ToString();
                Lblverdate.Text = dt.Rows[0]["BankProofDate"] == DBNull.Value ? "" : dt.Rows[0]["BankProofDate"].ToString();

                DDLAccountType.Text = dt.Rows[0]["Fax"].ToString();
                // FillCityPinDetail();


                if (!string.IsNullOrEmpty(DDLAccountType.Text) && DDLAccountType.SelectedValue != "0")
                {
                    DDLAccountType.Enabled = false;
                }
                else
                {
                    DDLAccountType.Enabled = true;
                }



                if (string.IsNullOrEmpty(cmbbank.SelectedItem.Text) || cmbbank.SelectedItem.Text.ToString() == "--Choose Bank Name--")
                {
                    cmbbank.Enabled = true; // Enable if empty
                }
                else
                {
                    cmbbank.Enabled = false; // Disable if not empty
                }
                if (string.IsNullOrEmpty(Txtbranch.Text))
                {
                    Txtbranch.Enabled = true; // Enable if empty
                }
                else
                {
                    Txtbranch.Enabled = false; // Disable if not empty
                }
                if (string.IsNullOrEmpty(Txtcode.Text))
                {
                    Txtcode.Enabled = true; // Enable if empty
                }
                else
                {
                    Txtcode.Enabled = false; // Disable if not empty
                }



                if (dt.Rows[0]["BankProof"].ToString() == "")
                {
                    bANKiMAGE.ImageUrl = "~/images/no_photo.jpg";
                    BankProof.HRef = "~/images/no_photo.jpg";
                }
                else
                {
                    bANKiMAGE.ImageUrl = dt.Rows[0]["BankProof"].ToString();
                    LblBankImage.Text = dt.Rows[0]["BankProof"].ToString();
                    BankProof.HRef = dt.Rows[0]["BankProof"].ToString();
                    BankKYCFileUpload3.Attributes.Add("class", "input-xxlarge");
                }




                if (!string.IsNullOrEmpty(LblBankImage.Text) && dt.Rows[0]["IsBankVerified"].ToString() != "R")
                {
                    BankKYCFileUpload3.Enabled = false;
                    BankKYCFileUpload3.Attributes.Add("CssClass", "input-xxlarge");
                }
                else
                {
                    BankKYCFileUpload3.Enabled = true;
                    c++;
                }






                if (dt.Rows[0]["IsBankVerified"].ToString() != "R" && !string.IsNullOrEmpty(dt.Rows[0]["BankProof"].ToString()))
                {
                    Txtacno.Enabled = false;
                }
                else
                {
                    Txtacno.Enabled = true;
                    c++;
                }

                if (dt.Rows[0]["IsBankVerified"].ToString() != "R" && Txtacno.Text.Length > 10)
                {
                    Txtacno.Enabled = false;
                }
                else
                {
                    Txtacno.Enabled = true;
                    c++;
                }
            }
            if (dt.Rows[0]["IsBankVerified"].ToString() != "R" && c == 0)
            {
                BtnIdentity.Visible = false;
                //Fuidentity.Enabled = false;
                //txtaddrs.Enabled = false;
                //FileUpload1.Enabled = false;
                //DDLAddressProof.Enabled = false;

                //TxtIdProofNo.Enabled = false;
                LbLrejectRemark.Text = "";
            }
            else
            {
                //ddlvillage.Enabled = true;
                BtnIdentity.Visible = true;
                //Fuidentity.Visible = true;
                //txtaddrs.Enabled = true;

                //Txtpincode.Enabled = true;

                //FileUpload1.Enabled = true;
                //DDLAddressProof.Enabled = true;
                //TxtIdProofNo.Enabled = true;
            }

            if (status == "Verification Due")
            {
                VerifyDate.Visible = false;
                Lblverdate.Visible = false;
                LblVerfReason.Visible = false;
                LblVerfRemark.Visible = false;
                LbLrejectRemark.Text = "";
                VerifyDate.Text = "";
                DivVerify.Attributes.Add("style", "color:black");
            }
            else if (status == "Rejected")
            {
                VerifyDate.Visible = true;
                Lblverdate.Visible = true;
                LblVerfReason.Visible = true;
                LblVerfRemark.Visible = true;
                VerifyDate.Text = "Reject Date:";
                DivVerify.Attributes.Add("style", "color:red");

                cmbbank.Enabled = true; // Enable if empty
                Txtbranch.Enabled = true; // Enable if empty
                Txtcode.Enabled = true; // Enable if empty
                DDLAccountType.Enabled = true;

            }
            else
            {
                VerifyDate.Visible = true;
                Lblverdate.Visible = true;
                LblVerfReason.Visible = false;
                LblVerfRemark.Visible = false;
                LbLrejectRemark.Text = "";
                VerifyDate.Text = "Verify Date:";
                DivVerify.Attributes.Add("style", "color:Green");
            }

            LblVerification.Visible = true;

        }



        catch (Exception ex)
        {
            // Handle exception
            Console.WriteLine(ex.Message); // Or use a logging framework
        }
        finally
        {
            // DbConnect.CloseConnection();
        }
    }



    protected void BtnIdentity_Click(object sender, EventArgs e)
    {
        try
        {
            string uploadRoot = Server.MapPath("~/images/UploadImage/");
            if (!Directory.Exists(uploadRoot))
            {
                Directory.CreateDirectory(uploadRoot);
            }
            DAL obj = new DAL(Application["Connect"].ToString());
            string s1 = "";
            DataTable dt1 = new DataTable();
            string condition = "";
            string adrsProof = "";
            string backAdrsProof;
            string bankProof;
            string panProof;
            string strextension = "";
            string str = "";
            string remark = "";
            string flAddrs = "";
            if (DDLAccountType.SelectedValue == "0")
            {

                BtnIdentity.Enabled = true;
                string script = "alert('Select Account Type.');";
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "alert", script, true);
                return;
            }
            if (BankKYCFileUpload3.Enabled)
            {
                if (!BankKYCFileUpload3.HasFile)
                {
                    ScriptManager.RegisterClientScriptBlock(Page, GetType(), "Close", "<SCRIPT language='javascript'>alert('Please upload a jpg/jpeg/png image of up to 5 MB size only!! ');</SCRIPT>", false);
                    return;
                }
            }
            if (BankKYCFileUpload3.HasFile)
            {
                strextension = System.IO.Path.GetExtension(BankKYCFileUpload3.FileName);
                if (strextension.ToUpper() == ".JPG" || strextension.ToUpper() == ".JPEG" || strextension.ToUpper() == ".PNG")
                {
                    System.Drawing.Image img = System.Drawing.Image.FromStream(BankKYCFileUpload3.PostedFile.InputStream);
                    int height = img.Height;
                    int width = img.Width;
                    decimal size = Math.Round((decimal)(BankKYCFileUpload3.PostedFile.ContentLength) / 1024, 1);

                    if (size > 5120)
                    {
                        string scrname = "<SCRIPT language='javascript'>alert('Please upload jpg/jpeg/png image of up to 5 MB size only!! ');</SCRIPT>";
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "Close", scrname, false);
                        return;
                    }
                    else
                    {
                        string FlBank = "Bank" + DateTime.Now.ToString("yyMMddhhmmssfff") + Session["formno"].ToString() + System.IO.Path.GetExtension(BankKYCFileUpload3.PostedFile.FileName);
                        //BankKYCFileUpload3.PostedFile.SaveAs(Server.MapPath("images/UploadImage/") + FlBank);
                        string savePath4 = Server.MapPath("images/UploadImage/") + FlBank;
                        BankKYCFileUpload3.PostedFile.SaveAs(savePath4);
                        CompressAndSaveImage(BankKYCFileUpload3.PostedFile.InputStream, savePath4, strextension, 50L); // Quality 50%
                        bankProof = "https://" + HttpContext.Current.Request.Url.Host + "/images/UploadImage/" + FlBank;
                    }
                }
                else
                {
                    string scrname = "<SCRIPT language='javascript'>alert('You can upload only .jpg, .jpeg, and .png extension files!! ');</SCRIPT>";
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "Close", scrname, false);
                    return;
                }
            }
            else
            {
                bankProof = LblBankImage.Text;
            }


            DataTable dt;
            if (cmbbank.SelectedItem.Text.ToUpper() == "OTHERS")
            {
                if (!string.IsNullOrWhiteSpace(Txtbank.Text))
                {
                    string q1 = "SELECT * FROM M_BankMaster WHERE BankName = '" + Txtbank.Text.Trim() + "' AND ActiveStatus = 'Y' AND RowStatus = 'Y'";
                    dt = new DataTable();
                    dt = SqlHelper.ExecuteDataset(Application["Connect"].ToString(), CommandType.Text, q1).Tables[0];
                    if (dt.Rows.Count == 0)
                    {
                        q1 = "INSERT INTO M_BankMaster (BankCode, BankName, AcNo, IFSCode, Remarks, ActiveStatus, LastModified, UserCode, UserId, IPAdrs, RowStatus) " +
                             "SELECT ISNULL(MAX(BankCode), '1') + 1 AS BankCode, @BankName, '0', '0','', 'Y', 'Add by " + Session["IdNo"].ToString() + " at " + DateTime.Now.ToString() + "', " +
                             "'" + Session["MemName"].ToString() + "', '" + Convert.ToInt32(Session["FormNo"]).ToString() + "', '', 'Y' FROM M_BankMaster";
                        int i = obj.SaveData(q1);
                        if (i > 0)
                        {
                            q1 = "SELECT MAX(BankCode) AS BankCode FROM M_BankMaster WHERE ActiveStatus = 'Y' AND RowStatus = 'Y'";
                            DataTable dt_ = new DataTable();
                            dt_ = SqlHelper.ExecuteDataset(Application["Connect"].ToString(), CommandType.Text, q1).Tables[0];
                            if (dt_.Rows.Count > 0)
                            {
                                dblBank = Convert.ToInt32(dt_.Rows[0]["BankCode"]);
                            }
                        }
                    }
                    else
                    {
                        dblBank = Convert.ToInt32(dt.Rows[0]["BankCode"]);
                    }
                }
            }
            else
            {
                dblBank = Convert.ToInt32(cmbbank.SelectedValue);
            }

            if (!string.IsNullOrWhiteSpace(Txtbank.Text) || !string.IsNullOrWhiteSpace(Txtcode.Text))
            {
                if (Convert.ToInt32(cmbbank.SelectedValue) == 0)
                {
                    string scrname = "<SCRIPT language='javascript'>alert('Choose Bank Name');</SCRIPT>";
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "MyAlert", scrname);
                    return;
                }

                if (string.IsNullOrWhiteSpace(Txtbranch.Text))
                {
                    string scrname = "<SCRIPT language='javascript'>alert('Enter Branch Name.');</SCRIPT>";
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "MyAlert", scrname);
                    return;
                }

                if (string.IsNullOrWhiteSpace(Txtcode.Text))
                {
                    string scrname = "<SCRIPT language='javascript'>alert('Enter IFSC Code.');</SCRIPT>";
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "MyAlert", scrname);
                    return;
                }
            }


            string strSq = "Exec sp_FillKyc '" + Session["Formno"] + "'";
            dt1 = SqlHelper.ExecuteDataset(Application["Connect"].ToString(), CommandType.Text, strSq).Tables[0];
            string Remark = "";
            if (dt1.Rows.Count > 0)
            {


                if (Convert.ToInt32(dt1.Rows[0]["BankId"]) != Convert.ToInt32(cmbbank.SelectedValue))
                {
                    Remark += "Bank Changed From " + Convert.ToInt32(dt1.Rows[0]["BankId"]) + " to " + Convert.ToInt32(cmbbank.SelectedValue) + ",";
                }

                if (ClearInject(dt1.Rows[0]["BranchName"].ToString()) != ClearInject(Txtbranch.Text))
                {
                    Remark += "BranchName Changed From " + ClearInject(dt1.Rows[0]["BranchName"].ToString()) + " to " + ClearInject(Txtbranch.Text) + ",";
                }
                if (ClearInject(dt1.Rows[0]["AcNo"].ToString()) != ClearInject(Txtacno.Text))
                {
                    Remark += "AccountNo Changed From " + ClearInject(dt1.Rows[0]["AcNo"].ToString()) + " to " + ClearInject(Txtacno.Text) + ",";
                }
                if (ClearInject(dt1.Rows[0]["IFSCode"].ToString()) != ClearInject(Txtcode.Text))
                {
                    Remark += "IFSCCode Changed From " + ClearInject(dt1.Rows[0]["IFSCode"].ToString()) + " to " + ClearInject(Txtcode.Text) + ",";
                }
                if (ClearInject(dt1.Rows[0]["BankProof"].ToString()) != ClearInject(bankProof))
                {
                    Remark += "BankProof Changed From " + ClearInject(dt1.Rows[0]["BankProof"].ToString()) + " to " + ClearInject(bankProof) + ",";
                }

            }

            int AreaCode = 0;
            string q = "";
            string Qry = "Insert Into TempMemberMaster Select *, 'Update BankProof - " + Context.Request.UserHostAddress.ToString() +
             "', GetDate(), 'U' From M_MemberMaster Where FormNo='" + Convert.ToInt32(Session["FormNo"]) + "';";

            Qry += "Insert Into TempKycVerify Select *, GetDate(), '" + Session["FormNo"] +
                   "' From KycVerify Where FormNo='" + Convert.ToInt32(Session["FormNo"]) + "';";

            Qry += "Insert Into UserHistory(UserId, UserName, PageName, Activity, ModifiedFlds, RecTimeStamp, MemberId) Values " +
                   "(0, '" + Session["MemName"] + "', 'BankProof', 'Bank Detail Update', '" + Remark +
                   "', GetDate(), '" + Session["FormNo"] + "');";

            string sql = Qry + "Update M_MemberMaster SET " +
                             "Acno='" + Txtacno.Text + "', " +
                             "Bankid='" + dblBank + "', " +
                             "IFscode='" + Txtcode.Text.ToUpper() + "', " +
                             "Branchname='" + Txtbranch.Text.ToUpper() + "', " +
                             "Fax='" + DDLAccountType.SelectedItem.Text + "' " +
                             "WHERE FormNo='" + Session["FormNo"] + "';";

            sql += "Update KycVerify SET " +
                   "BankProof='" + bankProof + "', " +
                   "BankProofDate=GetDate(), " +
                   "IsBankVerified='P' " +
                   "WHERE FormNo='" + Session["FormNo"] + "';";

            //string qry = " Exec Sp_SaveKYCDetails '" + Session["FormNo"] + "','" + Session["MemName"] + "','" + Remark + "','" + txtaddrs.Text.ToUpper() + "','" + Txtcity.Text.ToUpper() + "','" + Txtcity.Text.ToUpper() + "',";
            //qry += "'" + Txtdistrict.Text.ToUpper() + "','" + StateCode.Value + "','" + Txtpincode.Text + "','" + AreaCode + "','" + DDLAccountType.SelectedItem.Text + "','" + HCityCode.Value + "','" + HDistrictCode.Value + "',";
            //qry += "'" + txtpan.Text.ToUpper() + "','" + Txtacno.Text + "','" + dblBank + "','" + Txtcode.Text.ToUpper() + "','" + Txtbranch.Text.ToUpper() + "','" + DDLAddressProof.SelectedValue + "','" + TxtIdProofNo.Text.Trim().ToUpper() + "',";
            //qry += "'" + adrsProof + "','" + backAdrsProof + "','" + panProof + "','" + bankProof + "'";
            string strTrnFun_Query = "BEGIN TRY BEGIN TRANSACTION " + sql + " COMMIT TRANSACTION END TRY BEGIN CATCH ROLLBACK TRANSACTION END CATCH";
            int result = SqlHelper.ExecuteNonQuery(Application["Connect"].ToString(), CommandType.Text, strTrnFun_Query);
            if (result > 0)
            {
                string script = "<script language='javascript'>alert('KYC Upload successfully.');</script>";
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "Image", script, false);

                FillBankMaster();
                LoadImages();
                divBank.Visible = false;
                Txtbank.Text = "";
            }
            else
            {
                string script = "<script language='javascript'>alert('KYC Upload unsuccessfully.');</script>";
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "Image", script, false);

                LoadImages();
            }

        }
        catch (Exception ex)
        {
            string error = "Error: " + ex.Message;
            ScriptManager.RegisterStartupScript(this, GetType(), "Exception", "alert('" + error + "');", true);
        }
    }

    private void CompressAndSaveImage(Stream inputStream, string savePath, string extension, long quality = 50L)
    {
        using (System.Drawing.Image img = System.Drawing.Image.FromStream(inputStream))
        {
            EncoderParameters encoderParams = new EncoderParameters(1);
            ImageCodecInfo codec = null;

            switch (extension.ToLower())
            {
                case ".jpg":
                case ".jpeg":
                    codec = ImageCodecInfo.GetImageEncoders().FirstOrDefault(c => c.MimeType == "image/jpeg");
                    encoderParams.Param[0] = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, quality);
                    break;

                case ".png":
                    codec = ImageCodecInfo.GetImageEncoders().FirstOrDefault(c => c.MimeType == "image/png");
                    encoderParams = null; // PNG doesn't support quality settings
                    break;

                case ".gif":
                    codec = ImageCodecInfo.GetImageEncoders().FirstOrDefault(c => c.MimeType == "image/gif");
                    encoderParams = null;
                    break;

                default:
                    throw new Exception("Unsupported file type.");
            }

            if (codec != null)
            {
                if (encoderParams != null)
                {
                    img.Save(savePath, codec, encoderParams);
                }
                else
                {
                    img.Save(savePath, codec, null);
                }
            }
        }
    }
    private string ClearInject(string strObj)
    {
        if (strObj == null)
        {
            return string.Empty;
        }

        strObj = strObj.Replace(";", string.Empty)
                       .Replace("'", string.Empty)
                       .Replace("=", string.Empty);

        return strObj;
    }
}
