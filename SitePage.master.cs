using DocumentFormat.OpenXml.Drawing;
using DocumentFormat.OpenXml.Office2016.Drawing.Charts;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class SitePage : System.Web.UI.MasterPage
{
    DAL Obj;
    // DataTable (similar to: Dim Dt As DataTable)
    DataTable Dt;
    // Equivalent to: Dim objGen As clsGeneral = New clsGeneral
    clsGeneral objGen = new clsGeneral();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Application["WebStatus"] != null)
            {
                if (Application["WebStatus"].ToString() == "N")
                {
                    Session.Abandon();
                    Response.Redirect("default.aspx", false);
                }
            }
            if (!Page.IsPostBack)
            {
                DataTable Dt = new DataTable();
                string str = "";

                if (Session["Status"] != null && Session["Status"].ToString() == "OK")
                {
                    LoadProfileImage();
                    //LoadTeam();
                    string Strrank = "select  idno,UPPER(MemFirstName + MemLastName) as memname,replace(convert(varchar,upgradedate,106),' ','-') as DOA,ActiveStatus,isblock from m_membermaster where formno = '" + Session["Formno"].ToString() + "'";
                    Dt = SqlHelper.ExecuteDataset(Application["Connect"].ToString(), CommandType.Text, Strrank).Tables[0];
                    if (Dt.Rows.Count > 0)
                    {
                        LblId.Text = Dt.Rows[0]["idno"].ToString();
                        LblName.Text = Dt.Rows[0]["memname"].ToString();
                        Label1.Text = Dt.Rows[0]["idno"].ToString();
                        Label2.Text = Dt.Rows[0]["memname"].ToString();
                        Label3.Text = Dt.Rows[0]["DOA"].ToString();
                        if (Dt.Rows[0]["isblock"].ToString() == "Y")
                        {
                            Session.Abandon();
                            Response.Redirect("default.aspx", false);
                        }

                    }
                    //  string lnt = Crypto.Encrypt("uid=" + Session["IDNo"].ToString() + "&pwd=" + Session["MemPassw"].ToString() + "&mobile=" + Session["MobileNo"].ToString());
                }
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertMessage", "alert('" + ex.Message + "')", true);
        }
    }

    private string Encryptdata(string Data)
    {

        string strmsg = string.Empty;
        try
        {
            byte[] encode = new byte[Data.Length];
            encode = Encoding.UTF8.GetBytes(Data);
            strmsg = Convert.ToBase64String(encode);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        return strmsg;
    }
    private void CompressAndSaveImage(Stream inputStream, string savePath, string extension, long quality = 75L)
    {
        // बहुत जरूरी: FileUpload के बाद stream का position 0 पर reset करना
        if (inputStream.CanSeek)
            inputStream.Position = 0;

        using (System.Drawing.Image originalImage = System.Drawing.Image.FromStream(inputStream))
        {
            // 200x200 में resize + center crop
            using (System.Drawing.Image thumbnail = ResizeToSquare(originalImage, 200))
            {
                ImageCodecInfo codec = null;
                EncoderParameters encoderParams = null;

                string lowerExt = extension.ToLowerInvariant();

                if (lowerExt == ".jpg" || lowerExt == ".jpeg")
                {
                    codec = ImageCodecInfo.GetImageEncoders()
                        .FirstOrDefault(c => c.MimeType == "image/jpeg");

                    encoderParams = new EncoderParameters(1);
                    encoderParams.Param[0] = new EncoderParameter(
                        System.Drawing.Imaging.Encoder.Quality, quality);
                }
                else if (lowerExt == ".png")
                {
                    codec = ImageCodecInfo.GetImageEncoders()
                        .FirstOrDefault(c => c.MimeType == "image/png");
                    // PNG में quality काम नहीं करती, इसलिए null रहेगा
                }
                else
                {
                    throw new ArgumentException("Only JPG/JPEG and PNG allowed");
                }

                // हमेशा उसी extension के साथ save करो जो original थी
                thumbnail.Save(savePath, codec, encoderParams);
            }
        }
    }

    // Helper: Image को 200x200 square में convert करता है (aspect ratio बनाए रखते हुए + center crop
    private System.Drawing.Image ResizeToSquare(System.Drawing.Image source, int size)
    {
        float ratio = Math.Max((float)size / source.Width, (float)size / source.Height);
        int newWidth = (int)Math.Round(source.Width * ratio);
        int newHeight = (int)Math.Round(source.Height * ratio);

        using (Bitmap temp = new Bitmap(newWidth, newHeight))
        using (Graphics g = Graphics.FromImage(temp))
        {
            g.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

            g.DrawImage(source, 0, 0, newWidth, newHeight);

            int cropX = (newWidth - size) / 2;
            int cropY = (newHeight - size) / 2;

            Bitmap final = new Bitmap(size, size);
            using (Graphics gf = Graphics.FromImage(final))
            {
                gf.DrawImage(temp,
                    new System.Drawing.Rectangle(0, 0, size, size),
                    new System.Drawing.Rectangle(cropX, cropY, size, size),
                    GraphicsUnit.Pixel);
            }
            return final;
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (!FileUpload1.HasFile) return;

        string originalFileName = System.IO.Path.GetFileName(FileUpload1.PostedFile.FileName);
        string extension = System.IO.Path.GetExtension(originalFileName).ToLowerInvariant();

        if (extension != ".jpg" && extension != ".jpeg" && extension != ".png")
        {
            ShowAlert("केवल JPG, JPEG और PNG फाइलें ही अपलोड करें।");
            return;
        }

        if (FileUpload1.PostedFile.ContentLength > 5 * 1024 * 1024) // 5 MB
        {
            ShowAlert("कृपया 5 MB से छोटी फाइल ही अपलोड करें।");
            return;
        }

        try
        {
            string folderPath = Server.MapPath("~/images/UploadImage/ProfilePic/");
            if (!Directory.Exists(folderPath))
                Directory.CreateDirectory(folderPath);

            // Original नाम और extension ही रखेंगे
            string savePath = System.IO.Path.Combine(folderPath, originalFileName);

            // अगर पहले से फाइल है तो overwrite कर दो (या चाहें तो unique बना सकते हैं)
            // यहाँ हम overwrite कर रहे हैं क्योंकि प्रोफाइल पिक एक ही होती है
            if (File.Exists(savePath))
                File.Delete(savePath);

            // Resize 200x200 + Compress + Original extension में save
            CompressAndSaveImage(FileUpload1.PostedFile.InputStream, savePath, extension, 75L);

            // Database में भी original filename ही सेव करो
            using (SqlConnection con = new SqlConnection(Application["Connect"].ToString()))
            {
                string query = "UPDATE m_membermaster SET fld5 = @pic WHERE formno = @formno";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@pic", originalFileName);
                    cmd.Parameters.AddWithValue("@formno", Session["formno"]?.ToString() ?? "");

                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }

            // UI अपडेट
            string imageUrl = "~/images/UploadImage/ProfilePic/" + originalFileName;
            Image2.ImageUrl = imageUrl;
            photoPreview.Src = imageUrl;
            ImageProfile.ImageUrl = imageUrl;

            LoadProfileImage(); // अगर जरूरत हो
        }
        catch (Exception ex)
        {
            ShowAlert("Error: " + ex.Message);
        }
    }

    private void ShowAlert(string msg)
    {
        string script = $"<script>alert('{msg.Replace("'", "\\'")}');</script>";
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", script, false);
    }
    //protected void btnSave_Click(object sender, EventArgs e)
    //{
    //    if (FileUpload1.HasFile)
    //    {
    //        string strextension = System.IO.Path.GetExtension(FileUpload1.FileName);
    //        if (strextension.ToUpper() == ".JPG" || strextension.ToUpper() == ".JPEG" || strextension.ToUpper() == ".PNG")
    //        {
    //            System.Drawing.Image img = System.Drawing.Image.FromStream(FileUpload1.PostedFile.InputStream);
    //            int height = img.Height;
    //            int width = img.Width;
    //            decimal size = Math.Round((decimal)(FileUpload1.PostedFile.ContentLength) / 1024, 1);
    //            if (size > 5120)
    //            {
    //                string scrname = "<SCRIPT language='javascript'>alert('Please upload jpg/jpeg/png image of up to 5 MB size only!! ');</SCRIPT>";
    //                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "Close", scrname, false);
    //                return;
    //            }
    //            else
    //            {
    //                string fileName = System.IO.Path.GetFileName(FileUpload1.PostedFile.FileName);
    //                string folderPath = Server.MapPath("~/images/UploadImage/ProfilePic/");
    //                if (!Directory.Exists(folderPath))
    //                {
    //                    Directory.CreateDirectory(folderPath);
    //                }
    //                string savePath = folderPath + fileName;
    //                FileUpload1.SaveAs(savePath);
    //                CompressAndSaveImage(FileUpload1.PostedFile.InputStream, savePath, strextension, 50L); // Quality 50%
    //                string dbPath = fileName;
    //                using (SqlConnection con = new SqlConnection(Application["Connect"].ToString()))
    //                {
    //                    SqlCommand cmd = new SqlCommand("UPDATE m_membermaster SET fld5 = '" + dbPath + "' WHERE formno = '" + Session["formno"] + "'", con);
    //                    con.Open();
    //                    cmd.ExecuteNonQuery();
    //                }
    //                Image2.ImageUrl = dbPath;
    //                photoPreview.Src = dbPath;
    //                LoadProfileImage();
    //            }
    //        }
    //        else
    //        {
    //            string scrname = "<SCRIPT language='javascript'>alert('You can upload only .jpg, .jpeg, and .png extension files!! ');</SCRIPT>";
    //            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "Close", scrname, false);
    //            return;
    //        }
    //    }




    //}
    private void LoadProfileImage()
    {
        using (SqlConnection con = new SqlConnection(Application["Connect"].ToString()))
        {
            string s = "SELECT fld5 as ProfilePic FROM m_membermaster WHERE formno = '" + Session["formno"] + "'";
            Dt = SqlHelper.ExecuteDataset(Application["Connect"].ToString(), CommandType.Text, s).Tables[0];
            if (Dt.Rows.Count > 0)
            {
                if (Dt.Rows[0]["ProfilePic"].ToString() == "")
                {
                    string defaultImg = "https://www.iconpacks.net/icons/2/free-user-icon-3296-thumb.png";
                    Image2.ImageUrl = defaultImg;
                    photoPreview.Src = defaultImg;
                    ImageProfile.ImageUrl = defaultImg;
                }
                else
                {
                    Image2.ImageUrl = "/images/UploadImage/ProfilePic/" + Dt.Rows[0]["ProfilePic"].ToString();
                    photoPreview.Src = "/images/UploadImage/ProfilePic/" + Dt.Rows[0]["ProfilePic"].ToString();
                    ImageProfile.ImageUrl = "/images/UploadImage/ProfilePic/" + Dt.Rows[0]["ProfilePic"].ToString();
                }
            }
            else
            {
                string defaultImg = "https://www.iconpacks.net/icons/2/free-user-icon-3296-thumb.png";
                Image2.ImageUrl = defaultImg;
                photoPreview.Src = defaultImg;
                ImageProfile.ImageUrl = defaultImg;
            }
        }
    }
}
