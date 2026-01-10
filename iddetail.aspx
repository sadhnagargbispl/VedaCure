<%@ Page Title="" Language="C#" MasterPageFile="~/SitePage.master" AutoEventWireup="true" CodeFile="iddetail.aspx.cs" Inherits="iddetail" %>


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
                            <h5 class="mb-0">Address Proof</h5>
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
                                        <%--<h6>Address Proof
                                        </h6>--%>
                                    </div>
                                    <div class="form-group">
                                        <label for="pwd">
                                            Address:</label>
                                        <asp:TextBox ID="txtaddrs" runat="server" CssClass="form-control validate[required]"></asp:TextBox>
                                    </div>
                                    <div class="form-group">
                                        <label for="pincode">
                                            Pincode</label>
                                        <asp:TextBox ID="Txtpincode" runat="server" CssClass="form-control validate[required,custom[pincode]]"
                                            AutoPostBack="true" OnTextChanged="Txtpincode_TextChanged"></asp:TextBox>
                                    </div>
                                    <div class="form-group">
                                        <label for="pwd">
                                            State:</label>

                                        <asp:DropDownList ID="ddlState" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlState_SelectedIndexChanged">
                                        </asp:DropDownList>

                                        <asp:HiddenField ID="StateCode" runat="server" />
                                    </div>
                                    <div class="form-group">
                                        <label for="district">
                                            District</label>
                                        <asp:HiddenField ID="HDistrictCode" runat="server" />
                                        <asp:TextBox ID="Txtdistrict" runat="server" CssClass="form-control validate[required]"></asp:TextBox>
                                    </div>
                                    <div class="form-group">
                                        <label for="city">
                                            City</label>
                                        <asp:TextBox ID="Txtcity" runat="server" CssClass="form-control validate[required]"></asp:TextBox>
                                        <asp:HiddenField ID="HCityCode" runat="server" />
                                    </div>
                                    <div class="form-group " style="display: none;">
                                        <label for="inputdefault">
                                            Area</label>
                                        <asp:DropDownList ID="DDlVillage" CssClass="form-control" runat="server" ValidationGroup="eInformation"
                                            autocomplete="off" onchange="FnVillageChange(this.value);">
                                        </asp:DropDownList>
                                    </div>
                                    <div class="form-group" id="divVillage" style="display: none">
                                        <label for="inputdefault">
                                            Area Name</label>
                                        <asp:TextBox ID="TxtVillage" CssClass="form-control" runat="server" autocomplete="off"></asp:TextBox>
                                    </div>
                                    <div class="form-group">
                                        <label for="addressproof">
                                            Address Proof</label>
                                        <asp:DropDownList ID="DDLAddressProof" runat="server" CssClass="form-control">
                                        </asp:DropDownList>
                                    </div>
                                    <div class="form-group">
                                        <label for="idproof">
                                            <asp:Label ID="LblAddresProof" runat="server"></asp:Label>
                                        </label>
                                        <asp:TextBox ID="TxtIdProofNo" CssClass="form-control validate[required]" runat="server" MaxLength="16"></asp:TextBox>
                                    </div>

                                    <div class="form-group">
                                        <label for="email">
                                            Front Address Proof Upload:</label>
                                        <asp:FileUpload ID="Fuidentity" runat="server" CssClass="form-control validate[required]" />
                                        <asp:RequiredFieldValidator ID="rfvImage" runat="server" ErrorMessage="Please Select  Front Address proof.!!"
                                            Enabled="false" ControlToValidate="Fuidentity" ValidationGroup="eInformation">
                                        </asp:RequiredFieldValidator>
                                        <asp:Label ID="lblimage" runat="server" Visible="false"></asp:Label>
                                    </div>
                                    <div class="form-group">
                                        <label for="pwd">
                                            Back Address Proof Upload:</label>
                                        <asp:FileUpload ID="FileUpload1" runat="server" CssClass="form-control validate[required]" />
                                        <asp:RequiredFieldValidator ID="rfvImage1" runat="server" ErrorMessage="Please Select  Back Address proof.!!"
                                            Enabled="false" ControlToValidate="FileUpload1" ValidationGroup="eInformation">
                                        </asp:RequiredFieldValidator>
                                        <asp:Label ID="LblBackImage" runat="server" Visible="false"></asp:Label>
                                    </div>

                                    <div class="form-group">
                                        <div class="col-sm-offset-3 col-sm-9">
                                            <asp:Button ID="BtnIdentity" runat="server" ValidationGroup="eInformation" CssClass="btn btn-danger"
                                                Text="Submit" TabIndex="7" OnClientClick="return ValidateAllKycFields();" OnClick="BtnIdentity_Click" />
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
                                                Front Address Proof
                                                <br />
                                                <a id="FrontAddress" runat="server" class="fbox" rel="group" onclick="return openPopup(this)">
                                                    <asp:Image ID="ShowIdentity" runat="server" Width="150px" Height="150px" />
                                                </a>
                                            </div>
                                            <div class="col-md-12">
                                                Back Address Proof
                                                <br />
                                                <a id="BackAddress" runat="server" class="fbox" rel="group" onclick="return openPopup(this)">
                                                    <asp:Image ID="showBackImage" Width="150px" Height="150px" runat="server" />
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
    <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>

<script>
    $(document).ready(function () {

        $('#<%= TxtIdProofNo.ClientID %>').on('blur', function () {

        var idNo = $(this).val().trim();
        if (idNo === '') return;

        $.ajax({
            type: "POST",
            url: "iddetail.aspx/VerifyAadhaar",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: JSON.stringify({ idProofNo: idNo }),
            success: function (res) {
                if (res.d === false) {
                    alert("Aadhaar Card already registered with another ID.");
                    $('#<%= TxtIdProofNo.ClientID %>').val('').focus();
                    $('#<%= BtnIdentity.ClientID %>').prop('disabled', true);
                } else {
                    $('#<%= BtnIdentity.ClientID %>').prop('disabled', false);
                }
            },
            error: function () {
                alert("Unable to verify Aadhaar right now.");
            }
        });
    });

});
</script>

    <script>
        function ValidateIdProofNo() {

            var idType = document.getElementById('<%= DDLAddressProof.ClientID %>').value;
    var idNoCtrl = document.getElementById('<%= TxtIdProofNo.ClientID %>');
            var idNo = idNoCtrl.value.trim();

            // 🔴 Empty check
            if (idNo === "") {
                alert("Please enter Address Proof Number");
                idNoCtrl.focus();
                return false;
            }
            return true; // ✔ valid
        }
    </script>

    <script>
        function ValidateAllKycFields() {

            // Helper
            function isEmpty(id, msg) {
                var val = document.getElementById(id).value.trim();
                if (val === "") {
                    alert(msg);
                    document.getElementById(id).focus();
                    return true;
                }
                return false;
            }

            // 🔹 Address
            if (isEmpty('<%= txtaddrs.ClientID %>', 'Please enter Address')) return false;

            // 🔹 Pincode
            var pin = document.getElementById('<%= Txtpincode.ClientID %>').value.trim();
            if (pin === "" || pin.length !== 6 || isNaN(pin)) {
                alert("Please enter valid 6 digit Pincode");
                document.getElementById('<%= Txtpincode.ClientID %>').focus();
                return false;
            }

            // 🔹 State
            if (document.getElementById('<%= ddlState.ClientID %>').value === "0") {
                alert("Please select State");
                document.getElementById('<%= ddlState.ClientID %>').focus();
                return false;
            }

            // 🔹 District
            if (isEmpty('<%= Txtdistrict.ClientID %>', 'Please enter District')) return false;

            // 🔹 City
            if (isEmpty('<%= Txtcity.ClientID %>', 'Please enter City')) return false;

            // 🔹 ID Proof Type
            if (document.getElementById('<%= DDLAddressProof.ClientID %>').value === "0") {
                alert("Please select Address Proof Type");
                document.getElementById('<%= DDLAddressProof.ClientID %>').focus();
                return false;
            }
            if (!ValidateIdProofNo()) return false;
            // 🔹 ID Proof No
            if (isEmpty('<%= TxtIdProofNo.ClientID %>', 'Please enter Address Proof Number')) return false;

            // 🔹 FRONT IMAGE
            var front = document.getElementById('<%= Fuidentity.ClientID %>');
            if (!front.disabled && front.files.length === 0) {
                alert("Please upload Front Address Proof image");
                front.focus();
                return false;
            }

            // 🔹 BACK IMAGE
            var back = document.getElementById('<%= FileUpload1.ClientID %>');
            if (!back.disabled && back.files.length === 0) {
                alert("Please upload Back Address Proof image");
                back.focus();
                return false;
            }

            // ✅ ALL VALIDATION PASSED → POSTBACK ALLOWED
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
