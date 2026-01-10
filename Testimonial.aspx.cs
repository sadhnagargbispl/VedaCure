using System;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Web.UI;
using System.Drawing.Imaging;
using System.Linq;
using DocumentFormat.OpenXml.Drawing;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Security.Principal;

public partial class Testimonial : System.Web.UI.Page
{
    string ImgFl1 = "";
    DAL ObjDal;
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
        BtnSave.Attributes.Add("onclick", DisableTheButton(Page, BtnSave));
        if (Session["Status"] == null || Session["Status"].ToString() != "OK")
        {
            Response.Redirect("Logout.aspx");
        }

        if (!Page.IsPostBack)
        {
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
    protected void SaveDetail()
    {
        ObjDal = new DAL(Application["Connect"].ToString());
        string uploadRoot = Server.MapPath("~/images/UploadImage/");
        if (!Directory.Exists(uploadRoot))
        {
            Directory.CreateDirectory(uploadRoot);
        }
        if (ImageUpload.PostedFile != null && ImageUpload.PostedFile.FileName != "")
        {
            string strExtension = System.IO.Path.GetExtension(ImageUpload.FileName).ToUpper();

            if (strExtension == ".JPG" || strExtension == ".GIF" || strExtension == ".JPEG" ||
                strExtension == ".BMP" || strExtension == ".PNG")
            {
                System.Drawing.Image img = System.Drawing.Image.FromStream(ImageUpload.PostedFile.InputStream);
                int height = img.Height;
                int width = img.Width;
                decimal size = Math.Round((decimal)(ImageUpload.PostedFile.ContentLength) / 1024, 1);
                if (size > 5120)
                {
                    scrname = "<SCRIPT language='javascript'>alert('Please upload jpg/jpeg/png image of up to 5 MB size only!! ');</SCRIPT>";
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "Close", scrname, false);
                    return;
                }
                else
                {
                    ImgFl1 = "TES_" + DateTime.Now.ToString("yyMMddhhmmssfff") + Session["formno"].ToString() + System.IO.Path.GetExtension(ImageUpload.PostedFile.FileName);
                    //Fuidentity.PostedFile.SaveAs(Server.MapPath("images/UploadImage/") + flAddrs);
                    //string savePath = Server.MapPath("images/UploadImage/") + flAddrs;
                    string fullPath = System.IO.Path.Combine(uploadRoot, ImgFl1);
                    ImageUpload.PostedFile.SaveAs(fullPath);
                    CompressAndSaveImage(ImageUpload.PostedFile.InputStream, fullPath, strExtension, 50L); // Quality 50%
                    //ImgFl1 = "https://" + HttpContext.Current.Request.Url.Host + "/images/UploadImage/" + compId + "/" + FlAddrs;
                }
                //ImgFl1 = DateTime.Now.ToString("yyMMddhhmmssfff") + "_1" +
                //         Path.GetExtension(ImageUpload.PostedFile.FileName);

                //ImageUpload.PostedFile.SaveAs(Server.MapPath("~/images/uploadImage/") + ImgFl1);
            }
            else
            {
                scrname = "<SCRIPT language='javascript'>alert('You can upload only .jpg,.gif,.jpeg,.bmp,.png extension file!! ');</SCRIPT>";
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "Close", scrname, false);
                return;
            }
        }

        string str = "Insert Into M_TestmonialsMaster(FormNo,Heading,Descriptions,imagepath,IsApproved,ApprovedDate,ApproveUserId,ActiveStatus,RecTimeStamp)Values " +
                     "('" + Session["FormNo"] + "','" + txtHeading.Text + "','" + txtDescription.Text + "','" + ImgFl1 + "','N',NULL,'0','Y',GetDate())";

        int updateEffect = ObjDal.SaveData(str);

        if (updateEffect != 0)
        {
            scrname = "<SCRIPT language='javascript'>alert('Save Successfully!! ');</SCRIPT>";
            txtDescription.Text = "";
            txtHeading.Text = "";
        }
        else
        {
            scrname = "<SCRIPT language='javascript'>alert('Data not saved Successfully!! ');</SCRIPT>";
        }

        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "Close", scrname, false);
    }
    protected void BtnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("cpindex.aspx");
    }
    protected void BtnSave_Click(object sender, EventArgs e)
    {
        if (txtHeading.Text == "")
        {
            scrname = "<SCRIPT language='javascript'>alert('Please Enter Heading');</SCRIPT>";
            ScriptManager.RegisterClientScriptBlock(Page, GetType(), "Login Error", scrname, false);
            return;
        }
        else if (txtDescription.Text == "")
        {
            scrname = "<SCRIPT language='javascript'>alert('Please Enter Description.');</SCRIPT>";
            ScriptManager.RegisterClientScriptBlock(Page, GetType(), "Login Error", scrname, false);
            return;
        }
        else if (ImageUpload.PostedFile.FileName == "")
        {
            scrname = "<SCRIPT language='javascript'>alert('Please Choose Image.');</SCRIPT>";
            ScriptManager.RegisterClientScriptBlock(Page, GetType(), "Login Error", scrname, false);
            return;
        }
        else
        {
            SaveDetail();
        }

    }
}
