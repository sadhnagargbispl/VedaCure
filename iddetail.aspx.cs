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
public partial class iddetail : System.Web.UI.Page
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
                    FillState();
                    FillIdtypeMaster();
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
    private void FillIdtypeMaster()
    {
        try
        {
            string strQuery = "";
            Obj = new DAL(Application["Connect"].ToString());
            strQuery = "SELECT Id, IdType FROM M_IdTypeMaster WHERE ACTIVESTATUS='Y'";
            tmpTable = SqlHelper.ExecuteDataset(Application["Connect"].ToString(), CommandType.Text, strQuery).Tables[0];
            DDLAddressProof.DataSource = tmpTable;
            DDLAddressProof.DataValueField = "Id";
            DDLAddressProof.DataTextField = "IdType";
            DDLAddressProof.DataBind();
            DDLAddressProof.SelectedIndex = 0;
            for (int s = 0; s < tmpTable.Rows.Count; s++)
            {
                if (tmpTable.Rows[s]["Id"].ToString() != "0")
                {
                    LblIdproofText.Text += tmpTable.Rows[s]["IdType"].ToString() + ",";
                }
            }

            if (!string.IsNullOrEmpty(LblIdproofText.Text))
            {
                LblIdproofText.Text = LblIdproofText.Text.Remove(LblIdproofText.Text.Length - 1, 1);
            }
        }
        catch (Exception ex)
        {
            string path = HttpContext.Current.Request.Url.AbsoluteUri;
            string text = path + ":  " + DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss:fff ") + Environment.NewLine;
            Obj.WriteToFile(text + ex.Message);
            Response.Write("Try later.");
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
            str = "SELECT a.IDNo, a.MemFirstName AS MemName, a.Address1, a.City, a.Tehsil, a.District,0 as  AreaCode, " +
            "CityCode, DistrictCode, a.StateCode, a.Pincode, b.IdProofNo, b.AddrProof, " +
            "CASE WHEN b.IsAddrssverified <> 'N' THEN " +
            "REPLACE(CONVERT(VARCHAR, b.AddrssVerifyDate, 106), ' ', '-') ELSE '' END AS AddrProofDate, " +
            "b.IsAddrssverified, " +
            "CASE WHEN b.IsAddrssverified = 'Y' THEN 'Verified' " +
            "WHEN b.IsAddrssverified = 'P' THEN 'Pending' " +
            "WHEN b.IsAddrssverified = 'R' THEN 'Rejected' " +
            "ELSE 'Verification Due' END AS idVerf, " +
            "CASE WHEN b.IsAddrssverified = 'R' THEN b.AddrssRemark ELSE '' END AS RejectRemark, " +
            "b.BackAddressProof, b.BackAddressDate, c.IdType, c.Id, ISNULL(f.Reason, '') AS RejectReason " +
            "FROM M_MemberMaster AS a " +
            "INNER JOIN KycVerify AS b ON a.FormNo = b.FormNo " +
            "LEFT JOIN M_KycReject AS f ON f.Kid = b.AddressRejectId " +
            "INNER JOIN M_IdTypeMaster AS c ON b.IdType = c.Id AND c.ActiveStatus = 'Y' " +
            "WHERE b.FormNo = '" + Session["FormNo"] + "'";

            //str = ObjDal.Isostart + "Exec sp_FillKyc " + Convert.ToInt32(Session["Formno"]) + "" + ObjDal.IsoEnd;
            dt = SqlHelper.ExecuteDataset(Application["Connect"].ToString(), CommandType.Text, str).Tables[0];
            if (dt.Rows.Count > 0)
            {
                lblid.Text = dt.Rows[0]["idno"].ToString();
                hdnSessn.Value = Crypto.Encrypt(dt.Rows[0]["idno"].ToString());
                txtaddrs.Text = dt.Rows[0]["Address1"].ToString();
                Txtpincode.Text = dt.Rows[0]["Pincode"].ToString();
                Txtcity.Text = dt.Rows[0]["City"].ToString();
                Txtdistrict.Text = dt.Rows[0]["District"].ToString();
                StateCode.Value = dt.Rows[0]["Statecode"].ToString();
                HDistrictCode.Value = dt.Rows[0]["Districtcode"].ToString();
                HCityCode.Value = dt.Rows[0]["Citycode"].ToString();
                //ddlState.SelectedItem.Text = dt.Rows[0]["StateName"].ToString();
                lblverstatus.Text = dt.Rows[0]["idVerf"].ToString();
                DDLAddressProof.SelectedValue = dt.Rows[0]["Id"].ToString();
                DDlVillage.SelectedValue = dt.Rows[0]["areacode"].ToString();
                Lblverdate.Text = dt.Rows[0]["AddrProofDate"] == DBNull.Value ? "" : dt.Rows[0]["AddrProofDate"].ToString();
                LblRemark.Text = dt.Rows[0]["RejectRemark"].ToString();
                LbLrejectRemark.Text = dt.Rows[0]["RejectReason"].ToString();
                status = dt.Rows[0]["Idverf"].ToString();
                TxtIdProofNo.Text = dt.Rows[0]["IdProofNo"].ToString();

                
                if (string.IsNullOrEmpty(TxtIdProofNo.Text))
                {
                    TxtIdProofNo.Enabled = true; // Enable if empty
                }
                else
                {
                    TxtIdProofNo.Enabled = false; // Disable if not empty
                }

                if (string.IsNullOrEmpty(txtaddrs.Text))
                {
                    txtaddrs.Enabled = true; // Enable if empty
                }
                else
                {
                    txtaddrs.Enabled = false; // Disable if not empty
                }

                if (string.IsNullOrEmpty(Txtpincode.Text))
                {
                    Txtpincode.Enabled = true; // Enable if empty
                }
                else
                {
                    Txtpincode.Enabled = false; // Disable if not empty
                }
                if (string.IsNullOrEmpty(ddlState.SelectedItem.Text) || ddlState.SelectedItem.Text.ToString() == "--Choose State Name--")
                {
                    ddlState.Enabled = true; // Enable if empty
                }
                else
                {
                    ddlState.Enabled = false; // Disable if not empty
                }
                if (string.IsNullOrEmpty(Txtdistrict.Text))
                {
                    Txtdistrict.Enabled = true; // Enable if empty
                }
                else
                {
                    Txtdistrict.Enabled = false; // Disable if not empty
                }
                if (string.IsNullOrEmpty(Txtcity.Text))
                {
                    Txtcity.Enabled = true; // Enable if empty
                }
                else
                {
                    Txtcity.Enabled = false; // Disable if not empty
                }
                if (string.IsNullOrEmpty(DDLAddressProof.SelectedItem.Text) || DDLAddressProof.SelectedItem.Text.ToString() == "--Choose Id Proof--")
                {
                    DDLAddressProof.Enabled = true; // Enable if empty
                }
                else
                {
                    DDLAddressProof.Enabled = false; // Disable if not empty
                }

                if (dt.Rows[0]["AddrProof"].ToString() == "")
                {
                    ShowIdentity.ImageUrl = "~/images/no_photo.jpg";
                    FrontAddress.HRef = "~/images/no_photo.jpg";
                }
                else
                {
                    ShowIdentity.ImageUrl = dt.Rows[0]["AddrProof"].ToString();
                    FrontAddress.HRef = dt.Rows[0]["AddrProof"].ToString();
                    lblimage.Text = dt.Rows[0]["AddrProof"].ToString();
                    Fuidentity.Attributes.Add("class", "input-xxlarge");
                }

                if (dt.Rows[0]["BackAddressProof"].ToString() == "")
                {
                    showBackImage.ImageUrl = "~/images/no_photo.jpg";
                    BackAddress.HRef = "~/images/no_photo.jpg";
                }
                else
                {
                    showBackImage.ImageUrl = dt.Rows[0]["BackAddressProof"].ToString();
                    LblBackImage.Text = dt.Rows[0]["BackAddressProof"].ToString();
                    BackAddress.HRef = dt.Rows[0]["BackAddressProof"].ToString();
                    FileUpload1.Attributes.Add("class", "input-xxlarge");
                }



                if (!string.IsNullOrEmpty(dt.Rows[0]["AddrProof"].ToString()) && dt.Rows[0]["IsAddrssVerified"].ToString() != "R")
                {
                    Fuidentity.Enabled = false;
                    Fuidentity.Attributes.Add("Class", "input-xxlarge");
                }
                else
                {
                    Fuidentity.Enabled = true;
                    c++;
                }

                if (!string.IsNullOrEmpty(dt.Rows[0]["BackAddressProof"].ToString()) && dt.Rows[0]["IsAddrssVerified"].ToString() != "R")
                {
                    FileUpload1.Enabled = false;
                    FileUpload1.Attributes.Add("Class", "input-xxlarge");
                }
                else
                {
                    FileUpload1.Enabled = true;
                    c++;
                }
            }
            if (dt.Rows[0]["IsAddrssVerified"].ToString() != "R" && c == 0)
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
                TxtIdProofNo.Enabled = true; // Enable if empty
                txtaddrs.Enabled = true; // Enable if empty

                Txtpincode.Enabled = true; // Enable if empty
                ddlState.Enabled = true; // Enable if empty
                Txtdistrict.Enabled = true; // Enable if empty
                Txtcity.Enabled = true; // Enable if empty
                DDLAddressProof.Enabled = true; // Enable if empty


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
    private void FillState()
    {
        try
        {
            var obj = new DAL(Application["Connect"].ToString());
            string query = "SELECT StateCode, StateName FROM M_STateDivMaster WHERE ActiveStatus = 'Y' AND RowStatus = 'Y' ORDER BY StateCode";
            DataTable dt = SqlHelper.ExecuteDataset(Application["Connect"].ToString(), CommandType.Text, query).Tables[0];
            ddlState.DataSource = dt;
            ddlState.DataValueField = "StateCode";
            ddlState.DataTextField = "StateName";
            ddlState.DataBind();
        }
        catch (Exception ex)
        {

        }
    }
    private void FillCityPinDetail()
    {
        DataTable dt = new DataTable();
        string sql = string.Empty;
        if (!string.IsNullOrWhiteSpace(Txtpincode.Text))
        {
            int pincode = Convert.ToInt32(Txtpincode.Text);
            if (pincode != 0)
            {
                sql = "select a.Statename, b.DistrictName, c.CityName, d.VillageName, d.Pincode, a.StateCode, b.DistrictCode" +
                      " ,c.CityCode, d.VillageCode from M_STateDivMaster as a with (nolock) " +
                      "inner join M_DistrictMaster as b with (nolock) on a.StateCode = b.StateCode and a.ActivEstatus = 'Y' and b.ActiveStatus = 'Y' " +
                      "inner join M_CityStatemaster as c with (nolock) on b.DistrictCode = c.DistrictCode and c.ActivEstatus = 'Y' " +
                      "inner join M_VillageMaster as d with (nolock) on c.CityCode = d.CityCode and d.ActiveStatus = 'Y' " +
                      "where d.Pincode = '" + pincode + "' " +
                      "union all " +
                      "select '' as StateName, '' as DistrictName, '' as CityName, 'Others', '', 0, 0, 0, 381264";
            }
            dt = SqlHelper.ExecuteDataset(Application["Connect"].ToString(), CommandType.Text, sql).Tables[0];
            if (dt.Rows.Count > 0)
            {
                Txtcity.Text = dt.Rows[0]["CityName"].ToString();
                Txtdistrict.Text = dt.Rows[0]["DistrictName"].ToString();
                StateCode.Value = dt.Rows[0]["StateCode"].ToString();
                HDistrictCode.Value = dt.Rows[0]["DistrictCode"].ToString();
                HCityCode.Value = dt.Rows[0]["CityCode"].ToString();
                ddlState.SelectedItem.Text = dt.Rows[0]["StateName"].ToString();

                DDlVillage.DataSource = dt;
                DDlVillage.DataValueField = "VillageCode";
                DDlVillage.DataTextField = "VillageName";
                DDlVillage.DataBind();
                DDlVillage.SelectedIndex = 0;
                DDlVillage.Focus();
                //DDlVillage.DataSource = dt;
                //DDlVillage.DataValueField = "VillageCode";
                //DDlVillage.DataTextField = "VillageName";
                //DDlVillage.DataBind();
                //DDlVillage.SelectedIndex = 0;
            }
        }
    }
    protected void Txtpincode_TextChanged(object sender, EventArgs e)
    {
        DataTable dt = new DataTable();
        try
        {
            string scrname = string.Empty;
            string sql = string.Empty;

            if (!string.IsNullOrWhiteSpace(Txtpincode.Text))
            {
                int pincode = Convert.ToInt32(Txtpincode.Text);

                if (pincode != 0)
                {
                    sql ="select a.Statename, b.DistrictName, c.CityName, d.VillageName, d.Pincode, a.StateCode, b.DistrictCode" +
                      " ,c.CityCode, d.VillageCode from M_STateDivMaster as a with (nolock) " +
                      "inner join M_DistrictMaster as b with (nolock) on a.StateCode = b.StateCode and a.ActivEstatus = 'Y' and b.ActiveStatus = 'Y' " +
                      "inner join M_CityStatemaster as c with (nolock) on b.DistrictCode = c.DistrictCode and c.ActivEstatus = 'Y' " +
                      "inner join M_VillageMaster as d with (nolock) on c.CityCode = d.CityCode and d.ActiveStatus = 'Y' " +
                      "where d.Pincode = '" + pincode + "' " +
                      "union all " +
                      "select '' as StateName, '' as DistrictName, '' as CityName, 'Others', '', 0, 0, 0, 381264";

                    dt = SqlHelper.ExecuteDataset(Application["Connect"].ToString(), CommandType.Text, sql).Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        ddlState.SelectedItem.Text = dt.Rows[0]["StateName"].ToString();
                        StateCode.Value = dt.Rows[0]["StateCode"].ToString();
                        Txtdistrict.Text = dt.Rows[0]["DistrictName"].ToString();
                        HDistrictCode.Value = dt.Rows[0]["DistrictCode"].ToString();
                        Txtcity.Text = dt.Rows[0]["CityName"].ToString();
                        HCityCode.Value = dt.Rows[0]["CityCode"].ToString();

                        DDlVillage.DataSource = dt;
                        DDlVillage.DataValueField = "VillageCode";
                        DDlVillage.DataTextField = "VillageName";
                        DDlVillage.DataBind();
                        DDlVillage.SelectedIndex = 0;
                        DDlVillage.Focus();
                    }
                    else
                    {
                        Txtpincode.Focus();
                        ddlState.Items.Clear();
                        StateCode.Value = "0";
                        Txtcity.Text = string.Empty;
                        HCityCode.Value = "0";
                        Txtdistrict.Text = string.Empty;
                        HDistrictCode.Value = "0";
                        DDlVillage.Items.Clear();

                        scrname = "<SCRIPT language='javascript'>alert('Pincode Not exist.');</SCRIPT>";
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Permanent Pincode Not exist.');", true);
                    }
                }
            }
        }
        catch (Exception ex)
        {
            string path = HttpContext.Current.Request.Url.AbsoluteUri;
            string text = path + ": " + DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss:fff") + Environment.NewLine;
            Obj.WriteToFile(text + ex.Message);
            Response.Write("Try later.");
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

            if (Fuidentity.Enabled)
            {
                if (!Fuidentity.HasFile)
                {
                    ScriptManager.RegisterClientScriptBlock(Page, GetType(), "Close", "<SCRIPT language='javascript'>alert('Please upload a jpg/jpeg/png image of up to 5 MB size only!! ');</SCRIPT>", false);
                    return;
                }
            }

            if (Fuidentity.HasFile)
            {
                strextension = System.IO.Path.GetExtension(Fuidentity.FileName);
                if (strextension.ToUpper() == ".JPG" || strextension.ToUpper() == ".JPEG" || strextension.ToUpper() == ".PNG")
                {
                    System.Drawing.Image img = System.Drawing.Image.FromStream(Fuidentity.PostedFile.InputStream);
                    int height = img.Height;
                    int width = img.Width;
                    decimal size = Math.Round((decimal)(Fuidentity.PostedFile.ContentLength) / 1024, 1);
                    if (size > 5120)
                    {
                        string scrname = "<SCRIPT language='javascript'>alert('Please upload jpg/jpeg/png image of up to 5 MB size only!! ');</SCRIPT>";
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "Close", scrname, false);
                        return;
                    }
                    else
                    {
                        flAddrs = "FA" + DateTime.Now.ToString("yyMMddhhmmssfff") + Session["formno"].ToString() + System.IO.Path.GetExtension(Fuidentity.PostedFile.FileName);
                        //Fuidentity.PostedFile.SaveAs(Server.MapPath("images/UploadImage/") + flAddrs);
                        string savePath = Server.MapPath("images/UploadImage/") + flAddrs;
                        Fuidentity.PostedFile.SaveAs(savePath);
                        CompressAndSaveImage(Fuidentity.PostedFile.InputStream, savePath, strextension, 50L); // Quality 50%
                        adrsProof = "https://" + HttpContext.Current.Request.Url.Host + "/images/UploadImage/" + flAddrs;
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
                adrsProof = lblimage.Text;
            }

            if (FileUpload1.Enabled)
            {
                if (!FileUpload1.HasFile)
                {
                    ScriptManager.RegisterClientScriptBlock(Page, GetType(), "Close", "<SCRIPT language='javascript'>alert('Please upload a jpg/jpeg/png image of up to 5 MB size only!! ');</SCRIPT>", false);
                    return;
                }
            }
            if (FileUpload1.HasFile)
            {
                strextension = System.IO.Path.GetExtension(FileUpload1.FileName);
                if (strextension.ToUpper() == ".JPG" || strextension.ToUpper() == ".JPEG" || strextension.ToUpper() == ".PNG")
                {
                    System.Drawing.Image img = System.Drawing.Image.FromStream(FileUpload1.PostedFile.InputStream);
                    int height = img.Height;
                    int width = img.Width;
                    decimal size = Math.Round((decimal)(FileUpload1.PostedFile.ContentLength) / 1024, 1);

                    if (size > 5120)
                    {
                        string scrname = "<SCRIPT language='javascript'>alert('Please upload jpg/jpeg/png image of up to 5 MB size only!! ');</SCRIPT>";
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "Close", scrname, false);
                        return;
                    }
                    else
                    {
                        string FlBackAddrs = "BA" + DateTime.Now.ToString("yyMMddhhmmssfff") + Session["formno"].ToString() + System.IO.Path.GetExtension(FileUpload1.PostedFile.FileName);
                        //FileUpload1.PostedFile.SaveAs(Server.MapPath("images/UploadImage/") + FlBackAddrs);
                        string savePath2 = Server.MapPath("images/UploadImage/") + FlBackAddrs;
                        FileUpload1.PostedFile.SaveAs(savePath2);
                        CompressAndSaveImage(FileUpload1.PostedFile.InputStream, savePath2, strextension, 50L); // Quality 50%
                        backAdrsProof = "https://" + HttpContext.Current.Request.Url.Host + "/images/UploadImage/" + FlBackAddrs;
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
                backAdrsProof = LblBackImage.Text;
            }


            DataTable dt;
            string strSq = "Exec sp_FillKyc '" + Session["Formno"] + "'";
            dt1 = SqlHelper.ExecuteDataset(Application["Connect"].ToString(), CommandType.Text, strSq).Tables[0];
            string Remark = "";
            if (dt1.Rows.Count > 0)
            {
                if (ClearInject(dt1.Rows[0]["Address1"].ToString()) != ClearInject(txtaddrs.Text))
                {
                    Remark += "Address Changed From " + ClearInject(dt1.Rows[0]["Address1"].ToString()) + " to " + ClearInject(txtaddrs.Text) + ",";
                }
                if (ClearInject(dt1.Rows[0]["City"].ToString()) != ClearInject(Txtcity.Text))
                {
                    Remark += "City Changed From " + ClearInject(dt1.Rows[0]["City"].ToString()) + " to " + ClearInject(Txtcity.Text) + ",";
                }
                if (ClearInject(dt1.Rows[0]["District"].ToString()) != ClearInject(Txtdistrict.Text))
                {
                    Remark += "District Changed From " + ClearInject(dt1.Rows[0]["District"].ToString()) + " to " + ClearInject(Txtdistrict.Text) + ",";
                }

                if (ClearInject(dt1.Rows[0]["PinCode"].ToString()) != ClearInject(Txtpincode.Text))
                {
                    Remark += "PinCode Changed From " + ClearInject(dt1.Rows[0]["PinCode"].ToString()) + " to " + ClearInject(Txtpincode.Text) + ",";
                }
                if (ClearInject(dt1.Rows[0]["AddrProof"].ToString()) != ClearInject(adrsProof))
                {
                    Remark += "AddressProof Changed From " + ClearInject(dt1.Rows[0]["AddrProof"].ToString()) + " to " + ClearInject(adrsProof) + ",";
                }
                if (ClearInject(dt1.Rows[0]["BackAddressProof"].ToString()) != ClearInject(backAdrsProof))
                {
                    Remark += "BackAddressProof Changed From " + ClearInject(dt1.Rows[0]["BackAddressProof"].ToString()) + " to " + ClearInject(backAdrsProof) + ",";
                }
                if (ClearInject(dt1.Rows[0]["IdProofNo"].ToString()) != ClearInject(TxtIdProofNo.Text.Trim()))
                {
                    Remark += "AddressProofNo Changed From " + ClearInject(dt1.Rows[0]["IdProofNo"].ToString()) + " to " + ClearInject(TxtIdProofNo.Text.Trim()) + ",";
                }
            }
            if (DDLAddressProof.SelectedValue == "0")
            {
                string scrname = "<SCRIPT language='javascript'>alert('Choose ID Proof Type.');</SCRIPT>";
                RegisterStartupScript("MyAlert", scrname);
                return;
            }

            int AreaCode = 0;
            string q = "";
           
            string Qry = "Insert Into TempMemberMaster Select *,'Update Address Proof - " + Context.Request.UserHostAddress.ToString() +
             "',GetDate(),'U' From M_MemberMaster Where FormNo='" + Convert.ToInt32(Session["FormNo"]) + "';";

            Qry += "Insert Into TempKycVerify Select *,GetDate(),'" + Session["FormNo"] +
                   "' From KycVerify Where FormNo='" + Convert.ToInt32(Session["FormNo"]) + "';";

            Qry += "Insert Into UserHistory(UserId,UserName,PageName,Activity,ModifiedFlds,RecTimeStamp,MemberId) Values " +
                   "(0,'" + Session["MemName"] + "','AddressProof Detail','AddressProof Detail Update','" + Remark +
                   "',GetDate(),'" + Session["FormNo"] + "');";

            string sql = Qry + " Update m_MemberMaster SET " +
                          "Address1='" + txtaddrs.Text.ToUpper() + "'," +
                          "Tehsil='" + Txtcity.Text.ToUpper() + "'," +
                          "City='" + Txtcity.Text.ToUpper() + "'," +
                          "District='" + Txtdistrict.Text.ToUpper() + "'," +
                          "StateCode='" + ddlState.SelectedValue + "'," +
                          "Pincode='" + Txtpincode.Text + "'," +
                          "CityCode='" + HCityCode.Value + "'," +
                          "DistrictCode='" + HDistrictCode.Value + "' " +
                          "WHERE FormNo='" + Session["FormNo"] + "';";

            sql += " Update KycVerify SET " +
                   "Idtype='" + DDLAddressProof.SelectedValue + "'," +
                   "IdProofNo='" + TxtIdProofNo.Text.Trim().ToUpper() + "'," +
                   "AddrProof='" + adrsProof + "'," +
                   "BackAddressProof='" + backAdrsProof + "'," +
                   "BackAddressDate=GetDate()," +
                   "IsaddrssVerified='P' " +
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
    protected void ddlState_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlState.SelectedValue == "0")
        {
            BtnIdentity.Enabled = false;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('please select State name.!!');", true);
            return;
        }
        else
        {
            BtnIdentity.Enabled = true;
        }
    }
    [System.Web.Services.WebMethod]
    public static bool VerifyAadhaar(string idProofNo)
    {
        try
        {
            DataTable dt = new DataTable();
            string sql = @"SELECT COUNT(IdProofNo) AS cnt
                       FROM KycVerify AS a
                       INNER JOIN M_MemberMaster AS b ON a.FormNo = b.FormNo
                       WHERE IdProofNo <> ''
                         AND IsAddrssVerified <> 'R'
                         AND IdProofNo = @IdProofNo";

            using (SqlConnection conn =
                new SqlConnection(HttpContext.Current.Application["Connect"].ToString()))
            {
                SqlDataAdapter da = new SqlDataAdapter(sql, conn);
                da.SelectCommand.Parameters.AddWithValue("@IdProofNo", idProofNo);
                da.Fill(dt);
            }

            if (dt.Rows.Count > 0)
            {
                int count = Convert.ToInt32(dt.Rows[0]["cnt"]);

                // ✔ same logic as your AAdharVerify()
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
    //protected void TxtIdProofNo_TextChanged(object sender, EventArgs e)
    //{
    //    if (AAdharVerify())
    //    {
    //        BtnIdentity.Enabled = true;
    //    }
    //    else
    //    {
    //        BtnIdentity.Enabled = false;
    //        ScriptManager.RegisterStartupScript(this, GetType(), "Key", "alert('AAdhar Card already registered with another ID.');", true);
    //        TxtIdProofNo.Text = string.Empty;
    //        return;
    //    }
    //}
    //private bool AAdharVerify()
    //{
    //    try
    //    {
    //        bool result = false;
    //        DataTable dt12 = new DataTable();
    //        DataSet ds12 = new DataSet();
    //        string str12 = "SELECT COUNT(IdProofNo) AS cnt FROM KycVerify AS a,m_membermaster AS b WHERE a.formno = b.formno AND IdProofNo <> '' AND IsAddrssverified<>'R' AND IdProofNo = @IdProofNo";
    //        string connectionString = Application["Connect"].ToString();
    //        using (SqlConnection conn = new SqlConnection(connectionString))
    //        {
    //            SqlDataAdapter da = new SqlDataAdapter(str12, conn);
    //            da.SelectCommand.Parameters.AddWithValue("@IdProofNo", TxtIdProofNo.Text);
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
