<%@ Page Title="" Language="C#" MasterPageFile="~/SitePage.master" AutoEventWireup="true" CodeFile="PinGenerate.aspx.cs" Inherits="PinGenerate" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript" language="javascript">
        function isNumberKey(evt) {
            var charCode = (evt.which) ? evt.which : event.keyCode
            if (charCode > 31 && (charCode < 48 || charCode > 57))
                return false;

            return true;
        }
        function setKitAmt() {
            var AvlBal = '<%= Session["Balance"] %>';
            var qty = document.getElementById("<%= TxtQty.ClientID %>").value;
            var kMrp = document.getElementById("<%= Txtpackage.ClientID %>").value;

            if (document.getElementById("<%= Txtpackage.ClientID %>").value == '')
                qty = '0';

            if (document.getElementById("<%= Txtpackage.ClientID %>").value == '')
                kMrp = '0';

            var amt = parseFloat(qty) * parseFloat(kMrp);
            if (parseFloat(AvlBal) >= parseFloat(amt)) {
                document.getElementById("<%= TxtTotalAmount.ClientID %>").value = amt;
                return true;

            }
            else {
                alert('Total amount could not be greater than available balance.');
                return false;
            }


        }


    </script>
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
                            <h5 class="mb-0">Pin Generate</h5>
                        </div>
                        <div class="card-body">
                            <div class="row g-1">
                                <div class="clr">
                                    <asp:Label ID="LblError" runat="server" CssClass="error"></asp:Label>
                                </div>
                                <div class="col-md-6">
                                   <%-- <asp:Label ID="lblAvailable" runat="server" CssClass="error"></asp:Label>--%>
                                    <div class="form-group">
                                        <label for="inputdefault">
                                            E Wallet Balance<span class="red">*</span>
                                        </label>
                                        <%--<asp:TextBox ID="TxtSpName" runat="server" CssClass="form-control" Enabled="False" ReadOnly="True" __designer:wfdid="w1"></asp:TextBox>--%>
                                        <asp:TextBox ID="lblAvailable" runat="server" CssClass="form-control" onkeypress="return isNumberKey(event);" Enabled="false"></asp:TextBox>

                                    </div>
                                    <div class="form-group">
                                        <label for="inputdefault">
                                            Select Package<span class="red">*</span></label>
                                        <asp:DropDownList ID="CmbKit" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="cmbkit_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </div>
                                    <div class="form-group">
                                        <label for="inputdefault">
                                            Package Amount<span class="red">*</span>
                                        </label>
                                        <%--<asp:TextBox ID="TxtSpName" runat="server" CssClass="form-control" Enabled="False" ReadOnly="True" __designer:wfdid="w1"></asp:TextBox>--%>
                                        <asp:TextBox ID="Txtpackage" runat="server" CssClass="form-control" onkeypress="return isNumberKey(event);" Enabled="false"></asp:TextBox>

                                    </div>
                                    <div class="form-group">
                                        <label for="inputdefault">
                                            Quantity<span class="red">*</span></label>
                                        <asp:TextBox ID="TxtQty" runat="server" CssClass="form-control" onkeypress="return isNumberKey(event);" onchange="setKitAmt();"></asp:TextBox>
                                        &nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" CssClass="validation"
                                            ControlToValidate="TxtQty" ErrorMessage="Please Enter Package Qty " ForeColor="Red"></asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group">
                                        <label for="inputdefault">
                                            Total Amount<span class="red">*</span></label>
                                        <asp:TextBox ID="TxtTotalAmount" runat="server" CssClass="form-control" onkeypress="return isNumberKey(event);" Enabled="false"></asp:TextBox>
                                    </div>
                                    <div class="form-group">
                                        <asp:Button ID="BtnGenerate" runat="server" Text="Submit" class="btn btn-primary" OnClick="BtnGenerate_Click" />
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
