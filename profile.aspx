<%@ Page Title="" Language="C#" MasterPageFile="~/SitePage.master" AutoEventWireup="true" CodeFile="profile.aspx.cs" Inherits="profile" EnableEventValidation="false" %>

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
            <%--            <ul class="breadcrumb breadcrumb-style ">
                <li class="breadcrumb-item">
                    <h4 class="page-title m-b-0">Dashboard</h4>
                </li>
                <li class="breadcrumb-item">
                    <a href="#"><i data-feather="home"></i></a>
                </li>
                <li class="breadcrumb-item active">Dashboard</li>
            </ul>
            --%>

            <div class="row">
                <div class="col-12 col-sm-12 col-lg-12">
                    <div class="card">
                        <div class="card-header">
                            <h4>Profile</h4>
                        </div>

                        <div class="card-body">

                            <%--   <p>Sample Text here.. </p>--%>

                            <div class="row g-3">
                                <div runat="server" id="DvSponsor" visible="false">
                                    <tr>
                                        <td class="title_bar" colspan="2">Your Sponsor Detail
                                        </td>
                                    </tr>
                                    <tr id="rwSpnsr" runat="server">
                                        <td class="left_td">Sponsor ID *
                                        </td>
                                        <td class="right_td">
                                            <asp:TextBox ID="txtUplinerId" class="form-control" TabIndex="1" runat="server"
                                                AutoPostBack="True" Enabled="False"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="left_td">Sponsor Name
                                        </td>
                                        <td class="right_td">
                                            <asp:TextBox ID="lblUplnrNm" class="form-control" runat="server" Enabled="False"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="left_td">Position
                                        </td>
                                        <td class="right_td">
                                            <asp:TextBox ID="lblPosition" class="form-control" runat="server"></asp:TextBox>
                                        </td>
                                    </tr>
                                </div>
                                <div class="col-md-6">
                                    <label for="textInput" class="form-label">Your Name<span class="red">*</span></label>
                                    <%--<input id="textInput" name="textInput" type="text" class="form-control" placeholder="Your Name">--%>
                                    <div class="row">
                                        <div class="col-sm-2">
                                            <asp:DropDownList CssClass="form-control" ID="ddlPreFix" runat="server">
                                                <asp:ListItem Value="Mr." Text="Mr."></asp:ListItem>
                                                <asp:ListItem Value="Mrs." Text="Mrs."></asp:ListItem>
                                                <asp:ListItem Value="Miss" Text="Miss"></asp:ListItem>
                                                <asp:ListItem Value="M/S." Text="M/S."></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-sm-10">
                                            <asp:TextBox ID="txtFrstNm" CssClass="form-control validate[custom[onlyLetterNumberChar]]"
                                                runat="server" ReadOnly="true"></asp:TextBox>
                                        </div>
                                    </div>
                                    <%--          <asp:TextBox ID="txtFrstNm" CssClass="form-control" runat="server" TabIndex="3"
                                        ReadOnly="True"></asp:TextBox>--%>
                                </div>
                                <div class="col-md-6">
                                    <label for="textInput" class="form-label">Father's Name</label>
                                    <div class="row">
                                        <div class="col-sm-2">
                                            <asp:DropDownList CssClass="form-control" ID="CmbType" runat="server">
                                                <asp:ListItem Value="S/O" Text="S/O"></asp:ListItem>
                                                <asp:ListItem Value="D/O" Text="D/O"></asp:ListItem>
                                                <asp:ListItem Value="W/O" Text="W/O"></asp:ListItem>
                                                <asp:ListItem Value="C/O" Text="C/O"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-sm-10" style="padding-left: 0px;">
                                            <asp:TextBox ID="txtFNm" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                    <%-- <asp:TextBox ID="txtFNm" runat="server" TabIndex="5" Width="185px" class="form-control"></asp:TextBox>--%>
                                </div>
                                <div class="col-md-6">
                                    <label for="textInput" class="form-label">Date of Birth</label>
                                    <asp:TextBox ID="TxtDob" runat="server" CssClass="form-control"></asp:TextBox>
                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="TxtDob"
                                        Format="dd-MMM-yyyy"></ajaxToolkit:CalendarExtender>
                                </div>
                                <div class="col-md-6">
                                    <label for="textInput" class="form-label">Address<span class="red">*</span></label>
                                    <asp:TextBox ID="txtAddLn1" CssClass="form-control" TabIndex="9" runat="server"
                                        TextMode="MultiLine" ReadOnly="true"></asp:TextBox>
                                </div>
                                <div class="col-md-6">
                                    <label for="textInput" class="form-label">State</label>
                                    <asp:DropDownList ID="CmbState" runat="server" CssClass="form-control " TabIndex="11"
                                        Enabled="false">
                                    </asp:DropDownList>
                                </div>
                                <div class="col-md-6">
                                    <label for="textInput" class="form-label">District</label>
                                    <asp:TextBox ID="ddlDistrict" CssClass="form-control" TabIndex="12" runat="server"
                                        ReadOnly="true"></asp:TextBox>
                                </div>

                                <div class="col-md-6">
                                    <label for="textInput" class="form-label">City</label>
                                    <asp:TextBox ID="ddlTehsil" CssClass="form-control" TabIndex="13" runat="server"
                                        ReadOnly="true"></asp:TextBox>
                                </div>
                                <div class="col-md-6">
                                    <label for="textInput" class="form-label">Pin code</label>
                                    <asp:TextBox ID="txtPinCode" CssClass="form-control" onkeypress="return isNumberKey(event);"
                                        TabIndex="14" runat="server" MaxLength="6" ReadOnly="true"></asp:TextBox>
                                </div>
                                <div class="col-md-6">
                                    <label for="textInput" class="form-label">Mobile No.<span class="red">*</span></label>
                                    <asp:TextBox ID="txtMobileNo" Enabled="false" onkeypress="return isNumberKey(event);"
                                        CssClass="form-control" TabIndex="15" runat="server" MaxLength="10" ReadOnly="true"></asp:TextBox>
                                </div>
                                <div class="col-md-6">
                                    <label for="textInput" class="form-label">Phone No.</label>
                                    <asp:TextBox ID="txtPhNo" Enabled="false" onkeypress="return isNumberKey(event);"
                                        CssClass="form-control" TabIndex="16" runat="server" MaxLength="10" ReadOnly="true"></asp:TextBox>
                                </div>
                                <div class="col-md-6">
                                    <label for="textInput" class="form-label">E-Mail ID</label>
                                    <asp:TextBox ID="txtEMailId" CssClass="form-control" TabIndex="17" runat="server"
                                        ReadOnly="true"></asp:TextBox>
                                </div>
                                <div class="col-md-6">
                                    <label for="textInput" class="form-label">Nominee Name</label>
                                    <asp:TextBox ID="txtNominee" CssClass="form-control" TabIndex="18" runat="server"></asp:TextBox>
                                </div>
                                <div class="col-md-6">
                                    <label for="textInput" class="form-label">Relation</label>
                                    <asp:TextBox ID="txtRelation" CssClass="form-control" TabIndex="19" runat="server"></asp:TextBox>
                                </div>
                                <div class="col-md-6">
                                    <label for="textInput" class="form-label">Account No.</label>
                                    <asp:TextBox ID="TxtAccountNo" onkeypress="return isNumberKey(event);" CssClass="form-control"
                                        TabIndex="20" runat="server" MaxLength="16" ReadOnly="true"></asp:TextBox>
                                </div>
                                <div class="col-md-6">
                                    <label for="textInput" class="form-label">Bank</label>
                                    <asp:DropDownList ID="CmbBank" runat="server" CssClass="form-control " TabIndex="21"
                                        Enabled="false">
                                    </asp:DropDownList>
                                </div>
                                <div class="col-md-6">
                                    <label for="textInput" class="form-label">Branch Name</label>
                                    <asp:TextBox ID="TxtBranchName" CssClass="form-control" TabIndex="22" runat="server"
                                        ReadOnly="true"></asp:TextBox>
                                </div>
                                <div class="col-md-6">
                                    <label for="textInput" class="form-label">IFSC Code</label>
                                    <asp:TextBox ID="txtIfsCode" runat="server" CssClass="form-control" TabIndex="23"
                                        ReadOnly="true"></asp:TextBox>
                                </div>
                                <div class="col-md-6">
                                    <label for="textInput" class="form-label">PAN No.<span class="red">*</span></label>
                                    <asp:TextBox ID="txtPanNo" CssClass="form-control" TabIndex="24" runat="server"
                                        ReadOnly="true"></asp:TextBox>
                                </div>

                                <div class="col-12 d-flex gap-2">
                                    <%--<button type="submit" class="btn btn-primary">Submit</button>--%>
                                    <asp:Button ID="CmdSave" runat="server" Text="Update Changes" CssClass="btn btn-primary"
                                        TabIndex="27" OnClick="CmdSave_Click" />
                                    &nbsp;<asp:Button ID="CmdCancel" runat="server" Text="Cancel" CssClass="btn btn-primary"
                                        TabIndex="28" ValidationGroup="Form-Reset" OnClick="CmdCancel_Click" Visible="false" />
                                </div>
                            </div>

                            <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
                                ShowSummary="False" />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" Display="None" ControlToValidate="txtFrstNm"
                                runat="server" ErrorMessage="Member Name Required" SetFocusOnError="true"></asp:RequiredFieldValidator>&nbsp;
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="TxtDob"
                            SetFocusOnError="true" ErrorMessage="Date of Birth Required" Display="None"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="TxtDob"
                                Display="None" ErrorMessage="Invalid Date of Birth" Font-Names="arial" Font-Size="10px"
                                SetFocusOnError="True" ValidationExpression="^(?:((31-(Jan|Mar|May|Jul|Aug|Oct|Dec))|((([0-2]\d)|30)-(Jan|Mar|Apr|May|Jun|Jul|Aug|Sep|Oct|Nov|Dec))|(([01]\d|2[0-8])-Feb))|(29-Feb(?=-((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00)))))-((1[6-9]|[2-9]\d)\d{2})$"></asp:RegularExpressionValidator>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator10" Display="None" ControlToValidate="txtAddLn1"
                                runat="server" ErrorMessage="Address1 Required" SetFocusOnError="true"></asp:RequiredFieldValidator>&nbsp;
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator13" Display="None" ControlToValidate="TxtMobileNo"
                            runat="server" ErrorMessage="Mobile No. Required" SetFocusOnError="true"></asp:RequiredFieldValidator>&nbsp;
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator6" runat="server" Display="None"
                            ControlToValidate="TxtMobileNo" ErrorMessage="Minimum 10 Digits" ValidationExpression="^[0-9]{10,10}$"
                            SetFocusOnError="true"></asp:RegularExpressionValidator>&nbsp;
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator14" runat="server"
                            Display="None" ControlToValidate="txtPhNo" ErrorMessage="Minimum 10 Digits with STD Code"
                            ValidationExpression="^[0-9]{10,10}$" SetFocusOnError="true"></asp:RegularExpressionValidator>&nbsp;
                        <asp:RegularExpressionValidator ID="EmailExpressionValidator" runat="server" ControlToValidate="txtEMailId"
                            ErrorMessage="Enter Valid Email ID!" Display="None" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                            SetFocusOnError="true"></asp:RegularExpressionValidator>&nbsp;
                        <%--                             <asp:RequiredFieldValidator ID="ReqFV1" Display="None" ControlToValidate="txtPanNo"
                            runat="server" ErrorMessage="PAN No. Required" SetFocusOnError="true"></asp:RequiredFieldValidator>&nbsp;--%>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ErrorMessage="Please check PAN Format"
                                Display="None" SetFocusOnError="true" ControlToValidate="txtPanNo" ValidationExpression="[A-Za-z]{5}\d{4}[A-Za-z]{1}"></asp:RegularExpressionValidator>&nbsp;
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server" Display="None"
                            ErrorMessage="Special characters are not allowed" ControlToValidate="txtNominee"
                            SetFocusOnError="true" ValidationExpression="^[a-zA-Z0-9 _]*$" />&nbsp;
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator17" runat="server"
                            Display="None" ErrorMessage="Special characters are not allowed" ControlToValidate="txtRelation"
                            SetFocusOnError="true" ValidationExpression="^[a-zA-Z0-9 _]*$" />&nbsp;
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator20" runat="server"
                            ErrorMessage="Special characters not allowed" ControlToValidate="TxtBranchName"
                            ValidationExpression="^[a-zA-Z0-9 _]*$" Display="None" SetFocusOnError="true" />&nbsp;
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ErrorMessage="Special characters not allowed"
                            ControlToValidate="txtIfsCode" ValidationExpression="^[a-zA-Z0-9 _]*$" Display="None"
                            SetFocusOnError="true" />&nbsp;
                        </div>
                    </div>
                </div>
            </div>
        </section>


    </div>
</asp:Content>

