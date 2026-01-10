using System;
using System.Data;
using System.Data.SqlClient;
using System.Net;
using System.IO;
using System.Globalization;
using System.Web.UI.WebControls;

public partial class EpinDetail : System.Web.UI.Page
{
    cls_DataAccess dbConnect;
    SqlConnection Conn;
    SqlCommand Comm;
    SqlDataAdapter Adp;
    SqlDataReader dRead;
    DataTable Dt;
    string StrQuery;
    string ScrName;
    DAL obj;

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        FillDetail();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Status"] != null && Session["Status"].ToString() == "OK")
        {
            Conn = new SqlConnection(Application["Connect"].ToString());
            Conn.Open();

            dbConnect = new cls_DataAccess(Application["Connect"].ToString());

            if (!Page.IsPostBack)
            {
                Fillkit();
                FillDetail();
                Fill_DeliveryCenter();
                divconfirm.Visible = false;
            }
        }
        else
        {
            Response.Redirect("logout.aspx");
        }
    }

    private void Fillkit()
    {
        Comm = new SqlCommand("Select KitID,KitName From (Select 0 As KitID,'-- ALL --' As KitName Union Select KitID,KitName+' ('+cast(KitAmount As Varchar)+')'  as KitName From M_KitMaster Where ActiveStatus='Y') as temp Order By Kitid ", Conn);

        Adp = new SqlDataAdapter(Comm);
        Dt = new DataTable();
        Adp.Fill(Dt);

        CmbKit.DataSource = Dt;
        CmbKit.DataValueField = "KitID";
        CmbKit.DataTextField = "Kitname";
        CmbKit.DataBind();

        Comm.Cancel();
    }

    private void FillDetail()
    {
        try
        {
            string Condition = "";

            if (CmbKit.SelectedValue != "0")
            {
                Condition += " And KitID=" + CmbKit.SelectedValue;
            }

            if (rbtnStatus.SelectedValue == "USED")
            {
                Condition += " And [Status]='Used'";
            }
            else if (rbtnStatus.SelectedValue == "UN-USED")
            {
                Condition += " And [Status]='UnUsed'";
            }

            string StrQuery = "Select Row_Number() Over(Order by CardNo) As SNo,* From V#EpinStatus Where ReqFormNo='" + Session["IDNo"] + "'" + Condition;

            Comm = new SqlCommand(StrQuery, Conn);
            Adp = new SqlDataAdapter(Comm);
            Dt = new DataTable();
            Adp.Fill(Dt);

            DgPayment.CurrentPageIndex = 0;
            DgPayment.DataSource = Dt;
            DgPayment.DataBind();

            Session["epinData"] = Dt;
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message);
        }
    }
    private string ClearInject(string StrObj)
    {
        try
        {
            StrObj = StrObj.Replace(";", "").Replace("'", "").Replace("=", "");
            return StrObj;
        }
        catch (Exception)
        {
            return StrObj;
        }
    }

    protected void DgPayment_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        DataGridItem dgi = e.Item;
        if ((dgi.ItemType == ListItemType.Item) || (dgi.ItemType == ListItemType.AlternatingItem))
        {
            Button btnRegister = (Button)e.Item.FindControl("btnRegister");
            Button btnTopup = (Button)e.Item.FindControl("btnTopup");

            Label lblIsTopup = (Label)e.Item.FindControl("IsTopup");
            Label lblStatus = (Label)e.Item.FindControl("lblStatus");

            if (lblIsTopup != null && lblStatus != null)
            {
                if (lblIsTopup.Text == "Y" && lblStatus.Text == "UnUsed")
                {
                    if (btnRegister != null) btnRegister.Visible = false;
                    if (btnTopup != null) btnTopup.Visible = true;
                }
                else if (lblIsTopup.Text == "N" && lblStatus.Text == "UnUsed")
                {
                    if (btnRegister != null) btnRegister.Visible = true;
                    if (btnTopup != null) btnTopup.Visible = false;
                }
                else if (lblStatus.Text == "Used")
                {
                    if (btnRegister != null) btnRegister.Visible = false;
                    if (btnTopup != null) btnTopup.Visible = false;
                }
            }
        }
    }

    protected void DgPayment_ItemCommand(object source, DataGridCommandEventArgs e)
    {
        ClrCtrl();
        string sql = "";
        if (e.CommandArgument.ToString() == "Topup" || e.CommandArgument.ToString() == "Join")
        {
            string PinNo = ((Label)e.Item.FindControl("lblCardNo")).Text;
            string ScratchNo = ((Label)e.Item.FindControl("lblScratchNo")).Text;

            Label lblStatus = (Label)e.Item.FindControl("lblStatus");
            if (lblStatus != null && lblStatus.Text == "UnUsed")
            {
                if (e.CommandArgument.ToString() == "Topup")
                {
                    Label lblIsTopup = (Label)e.Item.FindControl("IsTopup");
                    if (lblIsTopup != null && lblIsTopup.Text == "Y")
                    {
                        DivTopup.Visible = true;
                        lblPinNo.Text = PinNo;
                        TxtScratchNo.Text = ScratchNo;

                        sql = "Select a.KitName,b.FormNo,b.ScratchNo,b.GeneratedBy,b.UsedBy,a.Allowtopup,b.ProdId,a.MACAdrs,a.KitAmount,a.KitId,a.RP,a.Bv FROM M_KitMaster as a,M_FormGeneration as b WHERE a.KitID=b.ProdID AND b.FormNo='" + lblPinNo.Text.Trim() + "' AND a.Allowtopup='Y' and a.RowStatus='Y' AND b.Usedby='0' AND GeneratedBy='Y'";

                        obj = new DAL(Application["Connect"].ToString());
                        DataTable Dt_ = new DataTable();
                        Dt_ = obj.GetData(sql);

                        if (Dt_.Rows.Count > 0)
                        {
                            Session["NewKitName"] = Dt_.Rows[0]["KitName"];
                            Session["NewKitAmount"] = Dt_.Rows[0]["KitAmount"];
                            Session["NewKitId"] = Dt_.Rows[0]["KitId"];
                            Session["NewKitBv"] = Dt_.Rows[0]["BV"];
                        }
                        else
                        {
                            ScrName = "<SCRIPT language='javascript'>alert('Invalid Topup Pin.');</SCRIPT>";
                            this.ClientScript.RegisterStartupScript(this.GetType(), "MyAlert", ScrName);
                        }
                    }
                    else
                    {
                        ScrName = "<SCRIPT language='javascript'>alert('Invalid Topup Pin.');</SCRIPT>";
                        this.ClientScript.RegisterStartupScript(this.GetType(), "MyAlert", ScrName);
                    }
                }
                else // Join
                {
                    Response.Redirect("NewJoining.aspx?pin=" + PinNo + "&scratch=" + ScratchNo, false);
                }
            }
            else
            {
                ScrName = "<SCRIPT language='javascript'>alert('ePin Already Used.');</SCRIPT>";
                this.ClientScript.RegisterStartupScript(this.GetType(), "MyAlert", ScrName);
            }
        }
    }

    protected void RbtDelivery_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (RbtDelivery.SelectedValue == "C" || RbtDelivery.SelectedValue == "S")
            {
                trDeliveryCenter.Visible = false;
                trDeliveryAddress.Visible = true;
            }
            else
            {
                trDeliveryCenter.Visible = true;
                trDeliveryAddress.Visible = false;
            }
        }
        catch (Exception) { }
    }

    protected void DgPayment_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
    {
        DgPayment.CurrentPageIndex = e.NewPageIndex;
        DgPayment.DataSource = Session["epinData"];
        DgPayment.DataBind();
    }

    protected void btnTopup_Click(object sender, EventArgs e)
    {
        if (IsValidEntry() == true)
        {
            trcommand.Visible = false;
            divconfirm.Visible = true;
        }
    }

    private void ClrCtrl()
    {
        errMsg.Text = "";
        TxtIDNo.Text = "";
        lblPinNo.Text = "";
        TxtScratchNo.Text = "";
        TxtIDNo1.Text = "";
        TxtName.Text = "";
        TxtPackage.Text = "";
        trcommand.Visible = true;
        divconfirm.Visible = false;
        DivTopup.Visible = false;
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        ClrCtrl();
    }

    protected void TxtIDNo_TextChanged(object sender, EventArgs e)
    {
        IsValidID();
    }

    private bool IsValidID()
    {
        bool BoolResult = false;
        string IsBlock = "";
        obj = new DAL(Application["Connect"].ToString());
        errMsg.Text = "";

        TxtIDNo.Text = TxtIDNo.Text.Replace(";", "").Replace("'", "").Replace("=", "").Trim();
        StrQuery = "Select MemFirstName + ' ' + MemLastName as MemName,IsBlock,b.KitAmount from M_MemberMaster as a,M_KitMaster as b where a.KitId=b.KitId and B.RowStatus='Y' and IDNo='" + TxtIDNo.Text + "'";
        Dt = obj.GetData(StrQuery);

        if (Dt.Rows.Count > 0)
        {
            IsBlock = Dt.Rows[0]["IsBlock"].ToString();
            Session["Block"] = IsBlock;
            if (IsBlock == "Y")
            {
                errMsg.Text = "Id Already Block";
            }
            else
            {
                if (Convert.ToDecimal(Session["NewKitAmount"]) > Convert.ToDecimal(Dt.Rows[0]["KitAmount"]))
                {
                    BoolResult = true;
                }
                else
                {
                    errMsg.Text = "Id Can Not Topup By This Package.";
                    BoolResult = false;
                }
            }
        }
        else
        {
            errMsg.Text = "Invalid ID!!";
        }
        return BoolResult;
    }

    private void Fill_DeliveryCenter()
    {
        try
        {
            DataTable DtLocal = new DataTable();
            obj = new DAL(Application["Connect"].ToString());
            StrQuery = "Select PartyCode,PartyName FROM " + Application["InvDB"] + "..M_Ledgermaster WHERE GroupId Not In(5,21) and OnWebSite='Y' ";
            DtLocal = obj.GetData(StrQuery);

            DDlDeliveryCenter.DataSource = DtLocal;
            DDlDeliveryCenter.DataTextField = "PartyName";
            DDlDeliveryCenter.DataValueField = "PartyCode";
            DDlDeliveryCenter.DataBind();
        }
        catch (Exception) { }
    }

    protected bool Updtmaster()
    {
        string strQry = "";
        string Result = "";
        SqlDataReader Dr = null;
        SqlDataReader Dr1 = null;
        double TotalOrder = 0;
        double TotalQty = 0;
        double TotalAmount = 0;

        string Orderno = "";
        string formno = "";
        string Partycode = "";
        string Address = "";

        try
        {
            string deliveryAddress = "";
            string Deliverycenter = "";
            if (RbtDelivery.SelectedValue == "C" || RbtDelivery.SelectedValue == "S")
            {
                deliveryAddress = TxtDeliveryAddress.Text;
                Deliverycenter = RbtDelivery.SelectedItem.Text;
            }
            else
            {
                deliveryAddress = "";
                Deliverycenter = DDlDeliveryCenter.SelectedItem.Text;
            }

            Result = "Hello";
            string s = "";
            int OTP_ = 0;
            int l = 0;
            string shipingStatus = "";
            Random Rs = new Random();

        lbl:
            OTP_ = Rs.Next(200001, 999999);

            s = "select * from TrnOrder where Orderno='" + OTP_ + "' ";
            Comm = new SqlCommand(s, dbConnect.cnnObject);
            Dr = Comm.ExecuteReader();
            if (!Dr.Read())
            {
                Orderno = OTP_.ToString();
                Dr.Close();
                StrQuery = "";
                strQry = "Exec Sp_Activate '" + ClearInject(TxtIDNo.Text) + "'," + lblPinNo.Text + "," + Orderno + ";";

                if (Convert.ToDouble(Session["NewKitAmount"]) > 0)
                {
                    string qryForm = "select formno,b.KitName,b.Rp,b.Bv,a.address1 from M_MemberMaster as a,M_KitMaster as b where a.KitId=b.KitId and b.RowStatus='Y' and  Idno='" + ClearInject(TxtIDNo.Text) + "'";
                    Dt = new DataTable();
                    obj = new DAL(Application["Connect"].ToString());
                    Dt = obj.GetData(qryForm);
                    if (Dt.Rows.Count > 0)
                    {
                        formno = Dt.Rows[0]["Formno"].ToString();
                        Session["Newaddress"] = Dt.Rows[0]["Address1"].ToString();
                    }

                    shipingStatus = "Y";
                    strQry = strQry + "Insert Into TrnorderDetail(OrderNo,FormNo,ProductID,Qty,Rate,NetAmount,RecTimeStamp,DispDate," +
                                                " DispStatus,DispQty," +
                                           " RemQty,DispAmt,MRP,DP,ProductName,ImgPath,RP,BV,FSEssId,ProdType)" +
                                     " Select '" + Orderno + "','" + formno + "',a.ProdId,'1',b.KitAmount," +
                                     " b.KitAmount,Getdate(),'','N',0,'1',0," +
                                     " a.MRP,a.DP,a.ProductName,'',b.RP,b.bv," +
                                     " '1','P' from " + Application["InvDB"] + "..M_ProductMaster as a,M_KitMaster as b where a.brandCode=b.KitId  and a.activeStatus='Y' and b.ActiveStatus='Y' and b.RowStatus='Y' and  b.kitId='" + Session["NewKitId"] + "';";

                    string qry2 = "Select * from " + Application["InvDB"] + "..M_ProductMaster as a,M_KitProductDetail as b where  a.ProdId=b.ProdId  and b.activeStatus='Y' and b.RowStatus='Y' and b.KitId='" + Session["NewKitId"] + "'";
                    Dt = new DataTable();
                    obj = new DAL(Application["Connect"].ToString());
                    Dt = obj.GetData(qry2);
                    if (Dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < Dt.Rows.Count; i++)
                        {
                            if (shipingStatus == "Y")
                            {
                                strQry = strQry + "Insert Into TrnorderDetail(OrderNo,FormNo,ProductID,Qty,Rate,NetAmount,RecTimeStamp,DispDate," +
                                " DispStatus,DispQty," +
                           " RemQty,DispAmt,MRP,DP,ProductName,ImgPath,RP,BV,FSEssId,Prodtype)" +
                     " Values('" + Orderno + "','" + formno + "','" + Convert.ToInt32(Dt.Rows[i]["ProdId"]) + "','" + Convert.ToInt32(Dt.Rows[i]["Qty"]) + "','" + Convert.ToDecimal(Dt.Rows[i]["DP"]) + "'," +
                     " '" + (Convert.ToDecimal(Dt.Rows[i]["DP"]) * Convert.ToDecimal(Dt.Rows[i]["Qty"])) + "',Getdate(),'','N',0,'" + Convert.ToInt32(Dt.Rows[i]["Qty"]) + "',0," +
                     " '" + Convert.ToDecimal(Dt.Rows[i]["MRP"]) + "','" + Convert.ToDecimal(Dt.Rows[i]["DP"]) + "','" + Dt.Rows[i]["ProductName"].ToString() + "','','0','0','1','P' );";
                                TotalAmount = TotalAmount + (Convert.ToDouble(Dt.Rows[i]["MRP"]) * Convert.ToDouble(Dt.Rows[i]["Qty"]));
                            }
                            else
                            {
                                strQry = strQry + "Insert Into TrnorderDetail(OrderNo,FormNo,ProductID,Qty,Rate,NetAmount,RecTimeStamp,DispDate," +
                                                                   " DispStatus,DispQty," +
                                                              " RemQty,DispAmt,MRP,DP,ProductName,ImgPath,RP,BV,FSEssId,Prodtype)" +
                                                        " Values('" + Orderno + "','" + formno + "','" + Convert.ToInt32(Dt.Rows[i]["ProdId"]) + "','" + Convert.ToInt32(Dt.Rows[i]["Qty"]) + "','" + Convert.ToDecimal(Dt.Rows[i]["DP"]) + "'," +
                                                        " '" + Convert.ToDecimal(Dt.Rows[i]["DP"]) + "',Getdate(),'','N',0,'" + Convert.ToInt32(Dt.Rows[i]["Qty"]) + "',0," +
                                                        " '" + Convert.ToDecimal(Dt.Rows[i]["MRP"]) + "','" + Convert.ToDecimal(Dt.Rows[i]["DP"]) + "','" + Dt.Rows[i]["ProductName"].ToString() + "','','0','0','1','P' );";
                                TotalAmount = TotalAmount + (Convert.ToDouble(Dt.Rows[i]["DP"]) * Convert.ToDouble(Dt.Rows[i]["Qty"]));
                            }
                            TotalOrder = TotalOrder + 1;
                            TotalQty = TotalQty + Convert.ToDouble(Dt.Rows[i]["Qty"]);
                        }
                    }

                    if (RbtDelivery.SelectedValue == "H")
                    {
                        Partycode = DDlDeliveryCenter.SelectedValue;
                        Address = Session["Newaddress"].ToString();
                    }
                    else
                    {
                        Partycode = "WR";
                        Address = ClearInject(TxtDeliveryAddress.Text);
                    }

                    strQry = strQry + "Insert INTO TrnOrder(OrderNo,OrderDate,MemFirstName,MemLastName,Address1,Address2,CountryID,CountryName,StateCode,City,PinCode," +
                      " Mobl,EMail,FormNo,UserType,Passw,PayMode,ChDDNo,ChDate,ChAmt,BankName,BranchName,Remark,OrderAmt,OrderItem," +
                      " OrderQty,ActiveStatus,HostIp,RecTimeStamp,IsTransfer,DispatchDate,DispatchStatus,DispatchQty,RemainQty," +
                      " DispatchAmount,Shipping,SessID,RewardPoint,CourierName,DocketNo,OrderFor,IsConfirm,OrderType,Discount,OldShipping,ShippingStatus,IdNo,FSessId,BankAmt,OtherAmt,WalletAmt,KitName,Bv,TravelPoint)" +
                      " select '" + Orderno + "',Cast(Convert(varchar,GETDATE(),106) as Datetime),MemFirstName , MemLastName , '" + Address + "' , Address2 , CountryID , '" + lblPinNo.Text + "' , StateCode , City , Case when PinCode='' then 0 else Pincode end as Pincode ," +
                      " Mobl, EMail ,'" + formno + "','', Passw ,'',0,'',0,'','','Top Up With:" + Session["NewKitName"] + "','" + Convert.ToDecimal(Session["NewKitAmount"]) + "','" + TotalOrder + "','" + TotalQty + "'," +
                      "'Y','" + RbtDelivery.SelectedValue + "',Getdate(),'Y','','N',0,'" + TotalQty + "',0,0,'" + Session["CurrentSessn"] + "','" + Convert.ToDecimal(Session["NewRewardPoint"]) + "','',0,'" + Partycode + "','Y','T',0,0,'" + shipingStatus + "','" + TxtIDNo.Text + "','1','0','0',0,'" + Session["NewKitName"] + "','" + Convert.ToDecimal(Session["NewKitBv"]) + "','" + Convert.ToDecimal(Session["NewTravelPoint"]) + "' from M_memberMaster where formno='" + formno + "';";

                    strQry = strQry + " Insert into " + Application["InvDB"] + "..TrnPaymentConfirmation(SNo,ConfirmBy,OrderNo,FormNo,OrderAmt,IsConfirm,RecTimeStamp,UserID,OrderFor," +
                                    " IDNO,ActiveStatus,OrdType,FSessId)select Case When Max(SNo) Is Null Then '1001' Else Max(SNo)+1 END as SNo,'WR','" + Orderno + "'," +
                                  "  '" + formno + "','" + Session["NewKitAmount"] + "','Y',Getdate(),0,'WR','" + TxtIDNo.Text + "','Y','D',1 from  " + Application["InvDB"] + "..TrnPaymentConfirmation;";
                    Dr.Close();

                    string K = " Begin Try Begin Transaction " + strQry + " Commit Transaction  End Try   BEGIN CATCH       ROLLBACK Transaction END CATCH      ";
                    Comm = new SqlCommand(K, dbConnect.cnnObject);
                    l = Comm.ExecuteNonQuery();
                }
            }
            else
            {
                Dr.Close();
                goto lbl;
            }

            if (l > 0)
            {
                ScrName = "<SCRIPT language='javascript'>alert('Successfuly TopUp');</SCRIPT>";
                this.ClientScript.RegisterStartupScript(this.GetType(), "MyAlert", ScrName);

                Comm = new SqlCommand("Select A.*,B.KitName from m_MemberMaster As A,M_KitMaster As B where A.KitID=B.KitID And A.IDNo = '" + TxtIDNo.Text + "'", dbConnect.cnnObject);
                dRead = Comm.ExecuteReader();
                if (dRead.Read())
                {
                    Session["UPGRDID"] = dRead["IDNo"].ToString();
                    Session["UPGRDName"] = dRead["MemFirstName"].ToString();
                    Session["UPGRDMobileNo"] = dRead["Mobl"].ToString();
                    Session["UPGRDKit"] = dRead["KitName"].ToString();
                }
                dRead.Close();

                string sms = "Dear " + Session["UPGRDName"] + ", Your order no is " + Orderno + ".Please keep it safe.This will be use while you collecting package . Best of luck, Regards: " + Session["CompName"] + "";

                sendsms(Session["UPGRDMobileNo"].ToString(), sms);
                return true;
            }

            return false;
        }
        catch (Exception ex)
        {
            errMsg.Text = ex.Message + "Error In Updation";
            return false;
        }
    }

    private void sendsms(object MobilenoObj, string sms)
    {
        try
        {
            string Mobileno = MobilenoObj != null ? MobilenoObj.ToString() : "";
            WebClient client = new WebClient();
            string baseurl = " http://www.apiconnecto.com/API/SMSHttp.aspx?UserId=" + Session["SmsId"] + "&pwd=" + Session["SmsPass"] + "&Message=" + sms + "&Contacts=" + Mobileno + "&SenderId=" + Session["ClientId"] + "";
            Stream data = client.OpenRead(baseurl);
            StreamReader reader = new StreamReader(data);
            string s = reader.ReadToEnd();
            data.Close();
            reader.Close();
        }
        catch (Exception ex)
        {
            // MsgBox is not available on server; ignore or log
        }
    }

    protected bool IsValidEntry()
    {
        bool IsValid = false;
        dbConnect = new cls_DataAccess(Application["Connect"].ToString());
        dbConnect.OpenConnection();
        string Block = Convert.ToString(Session["Block"]);

        string IDNo = TxtIDNo.Text.Replace("'", "").Replace("=", "").Replace(";", "").Trim();
        string PinNo = lblPinNo.Text.Replace("'", "").Replace("=", "").Replace(";", "").Trim();
        string ScratchNo = TxtScratchNo.Text.Replace("'", "").Replace("=", "").Replace(";", "").Trim();

        if (Block == "Y")
        {
            ScrName = "<SCRIPT language='javascript'>alert('Id Already Block.');</SCRIPT>";
            this.ClientScript.RegisterStartupScript(this.GetType(), "MyAlert", ScrName);
            return false;
        }

        Comm = new SqlCommand("Select * from m_MemberMaster where IsTopUp = 'N' and idno ='" + IDNo + "' ", dbConnect.cnnObject);
        dRead = Comm.ExecuteReader();
        if (!dRead.Read())
        {
            ScrName = "<SCRIPT language='javascript'>alert('Not Exists Or Already Topup.');</SCRIPT>";
            this.ClientScript.RegisterStartupScript(this.GetType(), "MyAlert", ScrName);
            return false;
        }
        else
        {
            Session["PrevKitId"] = dRead["KitId"];
            Session["PrevPIN"] = dRead["CardNo"];
            Session["PrevStatus"] = dRead["ActiveStatus"];
        }
        dRead.Close();

        Comm = new SqlCommand("Select *, MemFirstName + ' ' + MemLastName as MemName from m_MemberMaster where IdNo = '" + IDNo + "'", dbConnect.cnnObject);
        dRead = Comm.ExecuteReader();
        if (!dRead.Read())
        {
            ScrName = "<SCRIPT language='javascript'>alert('Invalid IdNo.');</SCRIPT>";
            this.ClientScript.RegisterStartupScript(this.GetType(), "MyAlert", ScrName);
            return false;
        }
        else
        {
            TxtIDNo1.Text = dRead["IDNo"].ToString();
            TxtName.Text = dRead["MemName"].ToString();
        }
        dRead.Close();

        Comm = new SqlCommand("Select km.Pv,km.kitid,km.KitName, JoinStatus from m_formgeneration sno inner join m_KitMaster km on km.kitid = sno.prodid where FormNo = " + PinNo + " and ScratchNo = '" + ScratchNo + "' and IsIssued = 'N' and AllowTopUp = 'Y' and sno.ActiveStatus='Y' ", dbConnect.cnnObject);
        dRead = Comm.ExecuteReader();
        if (!dRead.Read())
        {
            ScrName = "<SCRIPT language='javascript'>alert('Check PIN or ScratchNo.');</SCRIPT>";
            this.ClientScript.RegisterStartupScript(this.GetType(), "MyAlert", ScrName);
            return false;
        }
        else
        {
            TxtPackage.Text = dRead["KitName"].ToString();
            Session["KitId"] = dRead["KitId"];
            Session["JStatus"] = dRead["JoinStatus"];
            Session["BV"] = dRead["PV"];
        }
        dRead.Close();

        if (Convert.ToString(Session["Kitid"]) == Convert.ToString(Session["PrevKitId"]))
        {
            ScrName = "<SCRIPT language='javascript'>alert('Can not Upgrade by Same Kit');</SCRIPT>";
            this.ClientScript.RegisterStartupScript(this.GetType(), "MyAlert", ScrName);
            return false;
        }

        return true;
    }

    protected void BtnConfirm_Click(object sender, EventArgs e)
    {
        if (IsValidID())
        {
            if (IsValidEntry() == true)
            {
                if (Updtmaster())
                {
                    ClrCtrl();
                    FillDetail();
                }
            }
        }
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        TxtIDNo1.Text = "";
        TxtName.Text = "";
        TxtPackage.Text = "";
        divconfirm.Visible = false;
        trcommand.Visible = true;
    }

    protected void rbtnStatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillDetail();
    }
}
