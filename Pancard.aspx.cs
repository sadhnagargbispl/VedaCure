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
public partial class Pancard : System.Web.UI.Page
{
    double dblBank;
    DataTable tmpTable = new DataTable();
    DAL Obj;
    clsGeneral objGen = new clsGeneral();
    string constr = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
    DAL ObjDal;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            ObjDal = new DAL(Application["Connect"].ToString());
            BtnIdentity.Attributes.Add("onclick", DisableTheButton(Page, BtnIdentity));
            if (Session["Status"] != null && Session["Status"].ToString() == "OK")
            {
                if (!Page.IsPostBack)
                {
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
    private void LoadImages()
    {
        try
        {
            int c = 0;
            string status = "";
            string str = "";
            DataTable dt = new DataTable();
            str = "SELECT a.IDNo, a.MemFirstName AS MemName, a.Panno, b.PanImg, " +
            "REPLACE(CONVERT(VARCHAR, b.PANImgDate, 106), ' ', '-') AS PanProofDate, " +
            "b.IsPanVerified, " +
            "CASE WHEN b.IsPanVerified <> 'N' THEN REPLACE(CONVERT(VARCHAR, b.PanVerifyDate, 106), ' ', '-') ELSE '' END AS PanVerifyDate, " +
            "CASE WHEN b.IsPanVerified = 'Y' THEN 'Verified' " +
            "WHEN b.IsPanVerified = 'P' THEN 'Pending' " +
            "WHEN b.IsPanVerified = 'R' THEN 'Rejected' " +
            "ELSE 'Verification Due' END AS PanVerf, " +
            "CASE WHEN b.IsPanVerified = 'R' THEN b.PanRemarks ELSE '' END AS RejectRemark, " +
            "ISNULL(f.Reason, '') AS RejectReason " +
            "FROM M_MemberMaster AS a " +
            "INNER JOIN KycVerify AS b ON a.FormNo = b.FormNo " +
            "LEFT JOIN M_KycReject AS f ON b.PanRejectId = f.Kid " +
            "WHERE a.FormNo = '" + Session["FormNo"] + "'";

            //str = ObjDal.Isostart + "Exec sp_FillKyc " + Convert.ToInt32(Session["Formno"]) + "" + ObjDal.IsoEnd;
            dt = SqlHelper.ExecuteDataset(Application["Connect"].ToString(), CommandType.Text, str).Tables[0];
            if (dt.Rows.Count > 0)
            {
                lblid.Text = dt.Rows[0]["idno"].ToString();
                hdnSessn.Value = Crypto.Encrypt(dt.Rows[0]["idno"].ToString());
                LblRemark.Text = dt.Rows[0]["RejectRemark"].ToString();
                LbLrejectRemark.Text = dt.Rows[0]["RejectReason"].ToString();
                status = dt.Rows[0]["PanVerf"].ToString();
                lblverstatus.Text = dt.Rows[0]["PanVerf"].ToString();
                Lblverdate.Text = dt.Rows[0]["PanProofdate"] == DBNull.Value ? "" : dt.Rows[0]["PanProofdate"].ToString();
                txtpan.Text = dt.Rows[0]["Panno"].ToString();
                // FillCityPinDetail();


                if (string.IsNullOrEmpty(txtpan.Text))
                {
                    txtpan.Enabled = true; // Enable if empty
                }
                else
                {
                    txtpan.Enabled = false; // Disable if not empty
                }
                if (dt.Rows[0]["PanImg"].ToString() == "")
                {
                    pANiMAGE.ImageUrl = "~/images/no_photo.jpg";
                    PanCard.HRef = "~/images/no_photo.jpg";
                }
                else
                {
                    pANiMAGE.ImageUrl = dt.Rows[0]["PanImg"].ToString();
                    LblPanImage.Text = dt.Rows[0]["PanImg"].ToString();
                    PanCard.HRef = dt.Rows[0]["PanImg"].ToString();
                    PanKYCFileUpload.Attributes.Add("class", "input-xxlarge");
                }

                if (!string.IsNullOrEmpty(LblPanImage.Text) && dt.Rows[0]["IsPanVerified"].ToString() != "R")
                {
                    PanKYCFileUpload.Enabled = false;
                    PanKYCFileUpload.Attributes.Add("Class", "input-xxlarge");
                }
                else
                {
                    PanKYCFileUpload.Enabled = true;
                    c++;
                }

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
                txtpan.Enabled = true; // Enable if empty
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

            if (txtpan.Text.Trim().Length < 10)
            {
                ScriptManager.RegisterClientScriptBlock(Page, GetType(), "Close", "<SCRIPT language='javascript'>alert('Invalid Pan no!! ');</SCRIPT>", false);
                return;
            }
            if (PanKYCFileUpload.Enabled)
            {
                if (!PanKYCFileUpload.HasFile)
                {
                    ScriptManager.RegisterClientScriptBlock(Page, GetType(), "Close", "<SCRIPT language='javascript'>alert('Please upload a jpg/jpeg/png image of up to 5 MB size only!! ');</SCRIPT>", false);
                    return;
                }
            }

            if (PanKYCFileUpload.HasFile)
            {
                strextension = System.IO.Path.GetExtension(PanKYCFileUpload.FileName);
                if (strextension.ToUpper() == ".JPG" || strextension.ToUpper() == ".JPEG" || strextension.ToUpper() == ".PNG")
                {
                    System.Drawing.Image img = System.Drawing.Image.FromStream(PanKYCFileUpload.PostedFile.InputStream);
                    int height = img.Height;
                    int width = img.Width;
                    decimal size = Math.Round((decimal)(PanKYCFileUpload.PostedFile.ContentLength) / 1024, 1);

                    if (size > 5120)
                    {
                        string scrname = "<SCRIPT language='javascript'>alert('Please upload jpg/jpeg/png image of up to 5 MB size only!! ');</SCRIPT>";
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "Close", scrname, false);
                        return;
                    }
                    else
                    {
                        string FlPan = "PAN" + DateTime.Now.ToString("yyMMddhhmmssfff") + Session["formno"].ToString() + System.IO.Path.GetExtension(PanKYCFileUpload.PostedFile.FileName);
                        PanKYCFileUpload.PostedFile.SaveAs(Server.MapPath("images/UploadImage/") + FlPan);
                        string savePath3 = Server.MapPath("images/UploadImage/") + FlPan;
                        PanKYCFileUpload.PostedFile.SaveAs(savePath3);
                        CompressAndSaveImage(PanKYCFileUpload.PostedFile.InputStream, savePath3, strextension, 50L); // Quality 50%
                        panProof = "https://" + HttpContext.Current.Request.Url.Host + "/images/UploadImage/" + FlPan;
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
                panProof = LblPanImage.Text;
            }
            DataTable dt;
            string strSq = "Exec sp_FillKyc '" + Session["Formno"] + "'";
            dt1 = SqlHelper.ExecuteDataset(Application["Connect"].ToString(), CommandType.Text, strSq).Tables[0];
            string Remark = "";
            if (dt1.Rows.Count > 0)
            {
                if (ClearInject(dt1.Rows[0]["Panno"].ToString()) != ClearInject(txtpan.Text))
                {
                    Remark += "PANNo Changed From " + ClearInject(dt1.Rows[0]["Panno"].ToString()) + " to " + ClearInject(txtpan.Text) + ",";
                }
                if (ClearInject(dt1.Rows[0]["PanImg"].ToString()) != ClearInject(panProof))
                {
                    Remark += "PanCardImage Changed From " + ClearInject(dt1.Rows[0]["PanImg"].ToString()) + " to " + ClearInject(panProof) + ",";
                }
            }

            int AreaCode = 0;
            string q = "";
            string Qry = "Insert Into TempMemberMaster Select *, 'Update PanCard - " + Context.Request.UserHostAddress.ToString() +
             "', GetDate(), 'U' From M_MemberMaster Where FormNo='" + Convert.ToInt32(Session["FormNo"]) + "';";

            Qry += "Insert Into TempKycVerify Select *, GetDate(), '" + Session["FormNo"] +
                   "' From KycVerify Where FormNo='" + Convert.ToInt32(Session["FormNo"]) + "';";

            Qry += "Insert Into UserHistory(UserId, UserName, PageName, Activity, ModifiedFlds, RecTimeStamp, MemberId) Values " +
                   "(0, '" + Session["MemName"] + "', 'Pancard', 'PanCard Update', '" + Remark +
                   "', GetDate(), '" + Session["FormNo"] + "');";

            string sql = Qry + "Update M_MemberMaster SET " +
                             "Panno='" + txtpan.Text.ToUpper() + "' " +
                             "WHERE FormNo='" + Session["FormNo"] + "';";

            sql += "Update KycVerify SET " +
                   "PanImg='" + panProof + "', " +
                   "PANImgDate=GetDate(), " +
                   "IsPanVerified='P' " +
                   "WHERE FormNo='" + Session["FormNo"] + "';";
            string strTrnFun_Query = "BEGIN TRY BEGIN TRANSACTION " + sql + " COMMIT TRANSACTION END TRY BEGIN CATCH ROLLBACK TRANSACTION END CATCH";
            int result = SqlHelper.ExecuteNonQuery(Application["Connect"].ToString(), CommandType.Text, strTrnFun_Query);
            if (result > 0)
            {
                string script = "<script language='javascript'>alert('KYC Upload successfully.');</script>";
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "Image", script, false);
                LoadImages();
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
    [System.Web.Services.WebMethod]
    public static bool VerifyPan(string panNo)
    {
        try
        {
            DataTable dt = new DataTable();

            string sql = @"
        SELECT COUNT(panno) AS cnt
        FROM KycVerify AS a
        INNER JOIN M_MemberMaster AS b ON a.formno = b.formno
        WHERE panno <> ''
          AND IsPanVerified <> 'R'
          AND panno = @panno";

            using (SqlConnection conn =
                new SqlConnection(HttpContext.Current.Application["Connect"].ToString()))
            {
                SqlDataAdapter da = new SqlDataAdapter(sql, conn);
                da.SelectCommand.Parameters.AddWithValue("@panno", panNo);
                da.Fill(dt);
            }

            if (dt.Rows.Count > 0)
            {
                int count = Convert.ToInt32(dt.Rows[0]["cnt"]);

                // ✔ SAME LOGIC AS YOUR PanVerify()
                if (count > 1 || count == 0)
                    return true;
            }

            return false;
        }
        catch
        {
            return false;
        }
    }

    //protected void txtpan_TextChanged(object sender, EventArgs e)
    //{
    //    if (PanVerify())
    //    {
    //        BtnIdentity.Enabled = true;
    //    }
    //    else
    //    {
    //        BtnIdentity.Enabled = false;
    //        ScriptManager.RegisterStartupScript(this, GetType(), "Key", "alert('Pan card already registered with another ID.');", true);
    //        txtpan.Text = string.Empty;
    //        return;
    //    }
    //}
    //private bool PanVerify()
    //{
    //    try
    //    {
    //        bool result = false;
    //        DataTable dt12 = new DataTable();
    //        DataSet ds12 = new DataSet();
    //        string str12 = "SELECT COUNT(panno) AS cnt FROM KycVerify AS a,m_membermaster AS b WHERE a.formno = b.formno AND Panno <> '' AND Ispanverified<>'R' AND panno = @panno";
    //        string connectionString = Application["Connect"].ToString();
    //        using (SqlConnection conn = new SqlConnection(connectionString))
    //        {
    //            SqlDataAdapter da = new SqlDataAdapter(str12, conn);
    //            da.SelectCommand.Parameters.AddWithValue("@panno", txtpan.Text);
    //            da.Fill(ds12);
    //        }

    //        dt12 = ds12.Tables[0];
    //        if (dt12.Rows.Count > 0)
    //        {
    //            int count = Convert.ToInt32(dt12.Rows[0]["cnt"]);
    //            if (count > 1 || count == 0)
    //            {
    //                result = true;
    //            }
    //            else
    //            {
    //                result = false;
    //            }
    //        }

    //        return result;
    //    }
    //    catch (Exception ex)
    //    {
    //        // Handle exception or log it
    //        return false;
    //    }
    //}
}
