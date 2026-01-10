<%@ Page Title="" Language="C#" MasterPageFile="~/SitePage.master" AutoEventWireup="true" CodeFile="PinTransfer.aspx.cs" Inherits="PinTransfer" EnableEventValidation="false" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">

        language = "javascript" >
            function isNumberKey(evt) {
                //    if (!(((event.keyCode >= 48 && event.keyCode <= 57) || (event.keyCode >= 96 && event.keyCode <= 105)) || (event.keyCode == 8) || (event.keyCode == 9) || (event.keyCode == 37) || (event.keyCode == 39) || (event.keyCode == 46) || (event.keyCode == 190) || (event.keyCode == 35) || (event.keyCode == 36)))
                //event.returnValue=false;

                var charCode = (evt.which) ? evt.which : event.keyCode
                if (charCode > 31 && charCode < 48 || charCode > 57)
                    if (event.keyCode != 46)
                        if (event.keyCode != 110)

                            return false;

                return true;



            }


    </script>


    <script src="https://cdnjs.cloudflare.com/ajax/libs/jquery/3.3.1/jquery.min.js"></script>
    <style>
        .container i {
            margin-left: -30px;
            cursor: pointer;
        }
    </style>
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
                            <h5 class="mb-0">Pin Transfer</h5>
                        </div>
                        <div class="card-body">
                            <div class="row g-1">
                                <div class="clr">
                                    <asp:Label ID="errMsg" runat="server" CssClass="error"></asp:Label>
                                </div>
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label for="inputdefault">
                                            Id No.<span class="red">*</span></label>
                                        <asp:TextBox ID="TxtSerialno" runat="server" class="form-control" AutoPostBack="true" OnTextChanged="TxtSerialno_TextChanged"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" Display="Dynamic" ControlToValidate="TxtSerialno"
                                            runat="server" ForeColor="Red">IE No. can't left blank</asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group">
                                        <label for="inputdefault">
                                            Entrepreneur Name<span class="red">*</span>
                                        </label>
                                        <asp:TextBox ID="TxtSpName" runat="server" CssClass="form-control" Enabled="False" ReadOnly="True" __designer:wfdid="w1"></asp:TextBox>
                                    </div>
                                    <div class="form-group">
                                        <label for="inputdefault">
                                            Select kit<span class="red">*</span></label>
                                        <asp:DropDownList ID="cmbFillItem" runat="server" CssClass="form-control"></asp:DropDownList>
                                    </div>

                                    <div class="form-group">
                                        <label for="inputdefault">
                                            Quantity<span class="red">*</span></label>
                                        <asp:TextBox ID="txtNormalPin" runat="server" CssClass="form-control" onkeypress="return isNumberKey(event);"></asp:TextBox>
                                        &nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" CssClass="validation"
                                            ControlToValidate="txtNormalPin" ErrorMessage="Please Enter Pin Qty " ForeColor="Red"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtNormalPin"
                                            ErrorMessage="Enter numbers Only." ValidationExpression="^\d+$" ForeColor="Red" />
                                    </div>
                                    <div class="form-group">
                                        <asp:Button ID="cmdSave1" runat="server" Text="Submit" class="btn btn-primary" OnClick="cmdSave1_Click" />
                                    </div>
                                </div>

                            </div>


                        </div>
                    </div>
                </div>
            </div>
        </section>


    </div>


</asp:Content>

