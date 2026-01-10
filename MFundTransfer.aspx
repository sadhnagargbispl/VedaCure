<%@ Page Title="" Language="C#" MasterPageFile="~/SitePage.master" AutoEventWireup="true" CodeFile="MFundTransfer.aspx.cs" Inherits="MFundTransfer" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
        function valid() {
            if (document.getElementById("<%=TxtTransferAmt.ClientID%>").value == "") {
                alert("Transfer Amount Field can not be blank");
                document.getElementById("<%=TxtTransferAmt.ClientID%>").focus();
                return false;
            }
            if (parseFloat(document.getElementById("<%=TxtTransferAmt.ClientID%>").value) < '100.00') {
                alert("Transfer Amount Must be Greater Than 100 Rs.");
                return false;
            }
            if (parseFloat(document.getElementById("<%=TxtTransferAmt.ClientID%>").value) > parseFloat(document.getElementById("<%=TxtCredit.ClientID%>").value)) {
                alert("Transfer Amount Must be Less Than Credit Amount");
                return false;
            }
            return true;
        }
    </script>

    <script type="text/javascript" language="javascript">
        function isNumberKey(evt) {
            var charCode = (evt.which) ? evt.which : event.keyCode
            if (charCode > 31 && (charCode < 48 || charCode > 57))
                return false;

            return true;
        }

    </script>

    <script type="text/javascript">
        //Confirmation box sample
        function ConfirmationBox() {
            var result = confirm("Are you sure you want to continue?");
            if (result == true) {
                return true;
            }
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
                            <h5 class="mb-0">Fund Transfer - E Wallet to R Wallet</h5>
                        </div>
                        <div class="card-body">
                            <div class="row g-1">
                                <div class="clr">
                                    <asp:Label ID="errMsg" runat="server" CssClass="error"></asp:Label>
                                    <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="14px"
                                        ForeColor="red"></asp:Label>
                                </div>
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label for="inputdefault">
                                            Available Fund<span class="red">*</span></label>
                                        <asp:HiddenField ID="hdnSessn" runat="server" />
                                        <asp:TextBox ID="TxtCredit" runat="server" CssClass="form-control" ReadOnly="True"></asp:TextBox>
                                    </div>
                                    <div class="form-group">
                                        <label for="inputdefault">
                                            Transfer Fund<span class="red">*</span>
                                        </label>
                                        <asp:TextBox ID="TxtTransferAmt" runat="server" CssClass="form-control" MaxLength="6" onkeypress="return isNumberKey(event);"></asp:TextBox>
                                    </div>
                                    <div class="form-group">
                                        <asp:Button ID="BtnSubmit" runat="server" Text="Submit" class="btn btn-primary" OnClick="BtnSubmit_Click" />
                                        <p style="color: orangered; font-weight: bold">Fund Transfer (Minimum Transfer fund 100/-)</p>
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
