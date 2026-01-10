<%@ Page Title="" Language="C#" MasterPageFile="~/SitePage.master" AutoEventWireup="true" CodeFile="KycbankDetail.aspx.cs" Inherits="KycbankDetail" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/font-awesome/4.7.0/css/font-awesome.min.css">

    <script type="text/javascript" src="https://fonts.googleapis.com/css?family=Roboto"></script>

    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/font-awesome/4.7.0/css/font-awesome.min.css">

    <script type="text/javascript" src="https://fonts.googleapis.com/css?family=Roboto"></script>
    <style type="text/css">
        input {
            text-transform: uppercase;
        }
    </style>
    <style type="text/css">
        .style1 {
            height: 15%;
            width: 358px;
        }

        .style2 {
            height: 2px;
            width: 304px;
        }

        .style3 {
            height: 2px;
            width: 358px;
        }
    </style>
    <script type="text/javascript">
        function FnVillageChange(val) {

            if (val == "381264") {

                document.getElementById("divVillage").style.display = "block";

            }
            else {
                document.getElementById("divVillage").style.display = "none";
            }

        }
    </script>
    <script type="text/javascript">
        function SaveButton() {

            var bool = true;
            if ((document.getElementById("<%=Txtacno.ClientID %>").value != "") || (document.getElementById("<%=Txtcode.ClientID %>").value != "")) {
                var accountno = document.getElementById("<%= Txtacno.ClientID %>").value;
                var ifsccode = document.getElementById("<%= Txtcode.ClientID %>").value;
                if (accountno == "") {
                    alert('Enter Account No.');
                    bool = false;
                }
//                if (document.getElementById("<%= DDLAccountType.ClientID %>").value == 0 && bool == true) {
                //                    alert('Choose Account Type.');
                //                    bool = false;
                //                }
                if (document.getElementById("<%= cmbbank.ClientID %>").value == 0 && bool == true) {
                    alert('Choose Bank Name.');
                    bool = false;
                }
                if (document.getElementById("<%=Txtbranch.ClientID %>").value == "" && bool == true) {
                    alert('Enter Branch Name.');
                    bool = false;
                }
                if (ifsccode == "" && bool == true) {
                    alert('Enter IFSC Code.');
                    bool = false;
                }


            }

            return bool;

        }

    </script>
    <script>
        function openPopup(element) {
            var url = element.href;
            hs.htmlExpand(element, {
                objectType: 'iframe',
                width: 620,
                height: 450,
                marginTop: 0
            });
            return false;
        }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="main-content">
        <style>
            .red {
                color: red;
                font-size: 1.5em;
                padding-left: 4px;
                font-weight: bold;
            }
        </style>
        <section class="section">
            <ul class="breadcrumb breadcrumb-style ">
                <%--      <li class="breadcrumb-item">
        <h4 class="page-title m-b-0">Dashboard</h4>
    </li>
    <li class="breadcrumb-item">
        <a href="#"><i data-feather="home"></i></a>
    </li>
    <li class="breadcrumb-item active">Dashboard</li>--%>
            </ul>
            <div class="row">
                <div class="col-12 col-sm-12 col-lg-12">
                    <div class="card">
                        <div class="card-header">
                            <h5 class="mb-0">Bank Detail</h5>
                        </div>
                        <div class="card-body">
                            <div class="row g-1">
                                <div class="clr">
                                    <asp:Label ID="errMsg" runat="server" CssClass="error"></asp:Label>
                                </div>
                                <div class="col-md-6">
                                    <div class="form-group">
                                        Dear
                                        <%=Session["MemName"]%>
                                        <asp:HiddenField ID="hdnSessn" runat="server" />
                                        (<asp:Label ID="lblid" runat="server"></asp:Label>) , Update Your KYC (<asp:Label
                                            ID="LblIdproofText" runat="server"></asp:Label>)
                                        <br />
                                    </div>


                                    <div class="profile-bar-simple red-border clearfix">
                                        <%--  <h6>Bank Detail
                                        </h6>--%>
                                    </div>
                                    <div class="form-group" id="Accountype" runat="server">
                                        <label for="inputdefault">
                                            Account Type</label>
                                        <asp:DropDownList ID="DDLAccountType" runat="server" CssClass="form-control">
                                            <asp:ListItem Text="CHOOSE ACCOUNT TYPE" Value="0" Selected="True"></asp:ListItem>
                                            <asp:ListItem Text="SAVING ACCOUNT" Value="SAVING ACCOUNT"></asp:ListItem>
                                            <asp:ListItem Text="CURRENT ACCOUNT" Value="CURRENT ACCOUNT"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="form-group">
                                        <label for="inputdefault">
                                            Account No:</label>
                                        <asp:TextBox ID="Txtacno" runat="server" CssClass="form-control validate[required,custom[onlyNumberSp]]" MaxLength="17"></asp:TextBox>
                                    </div>
                                    <div class="form-group">
                                        <label for="inputdefault">
                                            Bank:</label>
                                        <asp:DropDownList ID="cmbbank" runat="server" CssClass="form-control" OnSelectedIndexChanged="CmbBank_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </div>
                                    <div class="form-group" id="divBank" runat="server" visible="false">
                                        <label for="inputdefault">
                                            Bank Name</label>
                                        <asp:TextBox ID="Txtbank" CssClass="form-control" runat="server"></asp:TextBox>
                                    </div>
                                    <div class="form-group">
                                        <label for="inputdefault">
                                            Branch Name :</label>
                                        <asp:TextBox ID="Txtbranch" runat="server" CssClass="form-control validate[required,custom[onlyLetterNumberChar]]"></asp:TextBox>
                                    </div>
                                    <div class="form-group">
                                        <label for="inputdefault">
                                            IFSC Code :</label>
                                        <div class="form-group">
                                            <asp:TextBox ID="Txtcode" runat="server" CssClass="form-control validate[required,custom[ifsccode]]"></asp:TextBox>
                                            <%--<asp:RegularExpressionValidator
                                        ID="regexIFSC"
                                        runat="server"
                                        ControlToValidate="Txtcode"
                                        ErrorMessage="Invalid IFSC Code"
                                        ValidationExpression="^[A-Z]{4}0[A-Z0-9]{6}$"
                                        ForeColor="Red"
                                        Display="Dynamic" />--%>
                                            <asp:RequiredFieldValidator
                                                ID="requiredIFSC"
                                                runat="server"
                                                ControlToValidate="Txtcode"
                                                ErrorMessage="IFSC Code is required"
                                                ForeColor="Red"
                                                Display="Dynamic" />
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label for="inputdefault">
                                            Bank KYC Upload</label>
                                    </div>
                                    <div class="form-group">
                                        <asp:FileUpload ID="BankKYCFileUpload3" runat="server" CssClass="form-control validate[required]" />
                                        <asp:Label ID="LblBankImage" runat="server" Visible="false"></asp:Label>
                                    </div>


                                    <div class="form-group">
                                        <div class="col-sm-offset-3 col-sm-9">
                                            <asp:Button ID="BtnIdentity" runat="server" ValidationGroup="eInformation" CssClass="btn btn-danger"
                                                Text="Submit" TabIndex="7" OnClientClick="return ValidateBankKycAjax();" OnClick="BtnIdentity_Click" />
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-2"></div>
                                <div class="col-md-4">
                                    <!-- Genex Business -->
                                    <div id="ctl00_ContentPlaceHolder1_divgenexbusiness" class="clearfix gen-profile-box">
                                        <div class="profile-bar-simple red-border clearfix">
                                            <h6>Uploaded Images
                                            </h6>
                                        </div>
                                        <div class="col-md-12">
                                            <%-- <div class="col-md-6">
<div class="image">--%>

                                            <script src="popupassets/popper.min.js"></script>

                                            <script src="popupassets/lib.js"></script>

                                            <script src="popupassets/jquery.flagstrap.min.js"></script>

                                            <script type="text/javascript" src="popupassets/jquery.themepunch.tools.min.js"></script>

                                            <script type="text/javascript" src="popupassets/jquery.themepunch.revolution.min.js"></script>

                                            <script src="js/functions1.js"></script>


                                            <div class="col-md-12">
                                                Bank Address Proof
                                                <br />
                                                <a id="BankProof" runat="server" class="fbox" rel="group" onclick="return openPopup(this)">
                                                    <asp:Image ID="bANKiMAGE" Width="150px" Height="150px" runat="server" />
                                                </a>
                                            </div>

                                        </div>
                                        <div class="col-md-12">
                                            <div id="DivVerify" runat="server">
                                                <br />
                                                <asp:Label ID="LblVerification" Text="Verification Status :  " Font-Bold="true" runat="server"></asp:Label>
                                                <asp:Label ID="lblverstatus" runat="server"></asp:Label>
                                                <br />
                                                <asp:Label ID="VerifyDate" runat="server" Text="Verify/Reject Date : " Visible="false"
                                                    Style="font-weight: bold"></asp:Label>
                                                <asp:Label ID="Lblverdate" runat="server"></asp:Label>
                                                <br />
                                                <asp:Label ID="LblVerfRemark" Text="Reject Remark : " Visible="false" runat="server"
                                                    Style="font-weight: bold"></asp:Label>
                                                <asp:Label ID="LblRemark" runat="server"></asp:Label>
                                                <br />
                                                <asp:Label ID="LblVerfReason" Text="Reject Reason : " Visible="false" runat="server"
                                                    Style="font-weight: bold"></asp:Label>
                                                <asp:Label ID="LbLrejectRemark" runat="server"></asp:Label>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>


                        </div>
                    </div>
                </div>
            </div>
        </section>


    </div>
<script>
    function ValidateBankKycAjax() {

        // Account Type
        if (document.getElementById('<%= DDLAccountType.ClientID %>').value === "0") {
        alert("Please select Account Type");
        return false;
    }

    // Account No
    var accNo = document.getElementById('<%= Txtacno.ClientID %>').value.trim();
    if (accNo === "" || accNo.length < 9) {
        alert("Please enter valid Account Number");
        return false;
    }

    // Bank
    var bankDDL = document.getElementById('<%= cmbbank.ClientID %>');
    if (bankDDL.value === "0" || bankDDL.selectedIndex === -1) {
        alert("Please select Bank");
        return false;
    }

    // If bank = OTHERS → Bank Name mandatory
    var bankText = bankDDL.options[bankDDL.selectedIndex].text.toUpperCase();
    if (bankText === "OTHERS") {
        var bankName = document.getElementById('<%= Txtbank.ClientID %>').value.trim();
        if (bankName === "") {
            alert("Please enter Bank Name");
            document.getElementById('<%= Txtbank.ClientID %>').focus();
            return false;
        }
    }

    // Branch
    if (document.getElementById('<%= Txtbranch.ClientID %>').value.trim() === "") {
        alert("Please enter Branch Name");
        return false;
    }

    // IFSC
    var ifsc = document.getElementById('<%= Txtcode.ClientID %>').value.trim().toUpperCase();
    var ifscRegex = /^[A-Z]{4}0[A-Z0-9]{6}$/;
    if (!ifscRegex.test(ifsc)) {
        alert("Invalid IFSC Code");
        return false;
    }

    // Bank KYC Image
    var bankImg = document.getElementById('<%= BankKYCFileUpload3.ClientID %>');
        if (!bankImg.disabled && bankImg.files.length === 0) {
            alert("Please upload Bank KYC Image");
            return false;
        }

        // ✅ All validations passed → allow postback
        return true;
    }
</script>




    <script type="text/javascript" src='https://ajax.aspnetcdn.com/ajax/jQuery/jquery-1.8.3.min.js'></script>

    <script type="text/javascript" src='https://cdnjs.cloudflare.com/ajax/libs/twitter-bootstrap/3.0.3/js/bootstrap.min.js'></script>

    <script src="assets/jquery.inbox.js"></script>


    <script type="text/javascript" src="popupassets/jquery.fancybox.pack.js"></script>

    <script type="text/javascript" src="popupassets/jquery.fancybox.pack1.js"></script>

    <link rel="stylesheet" href="popupassets/jquery.fancybox.css" type="text/css" media="screen" />

    <script type="text/javascript">
        $(document).ready(function () {
            $(".fbox").fancybox({
                openEffect: 'elastic',
                closeEffect: 'elastic'
            });
        });
    </script>
</asp:Content>
