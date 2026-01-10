<%@ Page Title="" Language="C#" MasterPageFile="~/SitePage.master" AutoEventWireup="true" CodeFile="Pancard.aspx.cs" Inherits="Pancard" %>


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
                            <h5 class="mb-0">PAN Card Detail</h5>
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
                                        <%--  <h6>PAN Card Detail
                                        </h6>--%>
                                    </div>
                                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                        <ContentTemplate>
                                            <div class="form-group">
                                                <label for="inputdefault">
                                                    Pan Card No. :</label>
                                                <%--AutoPostBack ="true"--%>
                                                <asp:TextBox ID="txtpan" runat="server" CssClass="form-control validate[required,custom[panno]]"></asp:TextBox>
                                            </div>
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="txtpan" EventName="TextChanged" />

                                        </Triggers>
                                    </asp:UpdatePanel>
                                    <div class="form-group">
                                        <label for="inputdefault">
                                            PanCard Upload :</label>
                                        <asp:FileUpload ID="PanKYCFileUpload" runat="server" CssClass="form-control validate[required]" />
                                        <asp:Label ID="LblPanImage" runat="server" Visible="false"></asp:Label>
                                    </div>



                                    <div class="form-group">
                                        <div class="col-sm-offset-3 col-sm-9">
                                            <asp:Button ID="BtnIdentity" runat="server" ValidationGroup="eInformation" CssClass="btn btn-danger"
                                                Text="Submit" TabIndex="7" OnClientClick="return ValidatePanAjax();" OnClick="BtnIdentity_Click" />
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
                                                Pan Card
                                                <br />
                                                <a id="PanCard" runat="server" class="fbox" rel="group" onclick="return openPopup(this)">
                                                    <asp:Image ID="pANiMAGE" Width="150px" Height="150px" runat="server" />
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

            $('#<%= txtpan.ClientID %>').on('blur', function () {

                var pan = $(this).val().trim().toUpperCase();
                if (pan === '') return;

                // PAN format check
                var panRegex = /^[A-Z]{5}[0-9]{4}[A-Z]{1}$/;
                if (!panRegex.test(pan)) {
                    alert("Invalid PAN format (ABCDE1234F)");
                    $(this).val('').focus();
                    return;
                }

                $(this).val(pan); // auto uppercase

                // 🔹 AJAX PAN verify
                $.ajax({
                    type: "POST",
                    url: "Pancard.aspx/VerifyPan",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    data: JSON.stringify({ panNo: pan }),
                    success: function (res) {
                        if (res.d === false) {
                            alert("Pan card already registered with another ID.");
                            $('#<%= txtpan.ClientID %>').val('').focus();
                        $('#<%= BtnIdentity.ClientID %>').prop('disabled', true);
                    } else {
                        $('#<%= BtnIdentity.ClientID %>').prop('disabled', false);
                    }
                },
                error: function () {
                    alert("Unable to verify PAN right now.");
                    $('#<%= BtnIdentity.ClientID %>').prop('disabled', true);
                }
            });
            });

        });
    </script>
    <script>
        function ValidatePanAjax() {

            var panCtrl = document.getElementById('<%= txtpan.ClientID %>');
        var pan = panCtrl.value.trim().toUpperCase();

        // 🔴 Empty check
        if (pan === "") {
            alert("Please enter PAN number");
            panCtrl.focus();
            return false;
        }

        // 🔴 PAN format check
        var panRegex = /^[A-Z]{5}[0-9]{4}[A-Z]{1}$/;
        if (!panRegex.test(pan)) {
            alert("Invalid PAN format (ABCDE1234F)");
            panCtrl.value = "";
            panCtrl.focus();
            return false;
        }
        var panImg = document.getElementById('<%= PanKYCFileUpload.ClientID %>');
            if (panImg.files.length === 0) {
                alert("Please upload PAN image");
                return false;
            }


            // ✅ PAN OK → allow BtnIdentity_Click (postback)
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
