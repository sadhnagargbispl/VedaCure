<%@ Page Title="" Language="C#" MasterPageFile="~/SitePage.master" AutoEventWireup="true" CodeFile="NewJoining.aspx.cs" Inherits="NewJoining" EnableEventValidation="false" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        input {
            text-transform: uppercase;
        }
    </style>
    <script type="text/javascript" language="javascript">
        function isNumberKey(evt) {
            var charCode = (evt.which) ? evt.which : event.keyCode
            if (charCode > 31 && (charCode < 48 || charCode > 57))
                return false;

            return true;
        }
        function DivOnOff() {


            if (document.getElementById("<%= chkterms.ClientID %>").checked == true) {


                document.getElementById("DivTerms").style.display = "block";



            }
            else {
                document.getElementById("DivTerms").style.display = "none";
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
                            <h5 class="mb-0">Epin Verification</h5>
                        </div>
                        <div class="card-body">
                            <asp:HiddenField ID="HdnCheckTrnns" runat="server" />
                            <div class="row g-1">
                                <div class="clr">
                                    <asp:Label ID="errMsg" runat="server" CssClass="error"></asp:Label>
                                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                        <ContentTemplate>
                                            <asp:Label ID="Label1" runat="server" CssClass="error"></asp:Label>
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="txtRefralId" EventName="TextChanged" />
                                            <asp:AsyncPostBackTrigger ControlID="txtUplinerId" EventName="TextChanged" />
                                            <asp:AsyncPostBackTrigger ControlID="txtPIN" EventName="TextChanged" />
                                            <asp:AsyncPostBackTrigger ControlID="txtScratch" EventName="TextChanged" />
                                        </Triggers>
                                    </asp:UpdatePanel>
                                </div>
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label for="inputdefault">
                                            Epin No.<span class="red">*</span></label>
                                        <asp:TextBox ID="txtPIN" CssClass="form-control" TabIndex="1" runat="server" onkeypress="return isNumberKey(event);"
                                            AutoPostBack="true" ValidationGroup="eSponsor" OnTextChanged="txtPIN_TextChanged"></asp:TextBox>
                                    </div>
                                    <div class="form-group">
                                        <label for="inputdefault">
                                            Scratch No.<span class="red">*</span>
                                        </label>
                                        <asp:TextBox ID="txtScratch" runat="server" CssClass="form-control" TabIndex="2"
                                            AutoPostBack="true" ValidationGroup="eSponsor" OnTextChanged="txtScratch_TextChanged"></asp:TextBox>
                                    </div>
                                    <div class="form-group">
                                        <label for="inputdefault">
                                            Referral ID<span class="red">*</span></label>
                                        <asp:TextBox ID="txtRefralId" class="form-control" TabIndex="3" runat="server" AutoPostBack="True"
                                            ValidationGroup="eSponsor" OnTextChanged="txtRefralId_TextChanged"></asp:TextBox>
                                        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                            <ContentTemplate>
                                                <asp:Label ID="lblRefralNm" runat="server" ForeColor="#D11F7B"></asp:Label>
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:AsyncPostBackTrigger ControlID="txtRefralId" EventName="TextChanged" />
                                            </Triggers>
                                        </asp:UpdatePanel>
                                    </div>
                                    <div class="form-group" id="rwSpnsr" runat="server">
                                        <label for="inputdefault">
                                            Placement ID<span class="red">*</span></label>
                                        <asp:TextBox ID="txtUplinerId" class="form-control" TabIndex="4" runat="server"
                                            AutoPostBack="True" ValidationGroup="eSponsor" OnTextChanged="txtUplinerId_TextChanged"></asp:TextBox>
                                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                            <ContentTemplate>
                                                <asp:Label ID="lblUplnrNm" runat="server" ForeColor="#D11F7B"></asp:Label>
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:AsyncPostBackTrigger ControlID="txtUplinerId" EventName="TextChanged" />
                                            </Triggers>
                                        </asp:UpdatePanel>
                                    </div>
                                    <div class="form-group">
                                        <label for="inputdefault">
                                            Leg<span class="red">*</span></label>
                                        <asp:RadioButtonList ID="RbtnLegNo" runat="server" Visible="True" TabIndex="3" RepeatDirection="Horizontal" OnSelectedIndexChanged="RbtnLegNo_SelectedIndexChanged" AutoPostBack="true" />
                                    </div>
                                    <div id="dv_Main" runat="server"></div>
                                    <div class="form-group">
                                        <label for="inputdefault">
                                            Name<span class="red">*</span></label>
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
                                                    runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label for="inputdefault">
                                            Father/HUSBAND's Name</label>
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
                                    </div>
                                    <div class="form-group">
                                        <label for="inputdefault">
                                            Date of Birth</label>
                                        <asp:TextBox ID="TxtDob" runat="server" CssClass="form-control"></asp:TextBox>
                                        <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="TxtDob"
                                            Format="dd-MMM-yyyy"></ajaxToolkit:CalendarExtender>
                                    </div>
                                    <div class="form-group">
                                        <label for="inputdefault">
                                            Address<span class="red">*</span></label>
                                        <asp:TextBox ID="txtAddLn1" CssClass="form-control" TabIndex="12" runat="server"
                                            ValidationGroup="eInformation"></asp:TextBox>
                                    </div>
                                    <div class="form-group">
                                        <label for="inputdefault">
                                            State</label>
                                        <asp:DropDownList ID="CmbState" runat="server" CssClass="form-control" TabIndex="13">
                                        </asp:DropDownList>
                                    </div>
                                    <div class="form-group">
                                        <label for="inputdefault">
                                            District</label>
                                        <asp:TextBox ID="ddlDistrict" CssClass="form-control" TabIndex="14" runat="server"></asp:TextBox>
                                    </div>
                                    <div class="form-group">
                                        <label for="inputdefault">
                                            City</label>
                                        <asp:TextBox ID="ddlTehsil" CssClass="form-control" TabIndex="15" runat="server"></asp:TextBox>
                                    </div>
                                    <div class="form-group">
                                        <label for="inputdefault">
                                            Pin code</label>
                                        <asp:TextBox ID="txtPinCode" CssClass="form-control" onkeypress="return isNumberKey(event);"
                                            TabIndex="16" runat="server" MaxLength="6"></asp:TextBox>
                                    </div>
                                    <div class="form-group">
                                        <label for="inputdefault">
                                            Mobile No.<span class="red">*</span></label>
                                        <asp:TextBox ID="txtMobileNo" onkeypress="return isNumberKey(event);" CssClass="form-control"
                                            TabIndex="17" runat="server" MaxLength="10" ValidationGroup="eInformation"></asp:TextBox>
                                    </div>

                                    <div class="form-group">
                                        <label for="inputdefault">
                                            E-Mail ID</label>
                                        <asp:TextBox ID="txtEMailId" CssClass="form-control" TabIndex="19" runat="server"
                                            ValidationGroup="eInformation"></asp:TextBox>
                                    </div>
                                    <div class="form-group">
                                        <label for="inputdefault">
                                            Password<span class="red">*</span></label>
                                        <asp:TextBox ID="TxtPasswd" CssClass="form-control" TabIndex="28" runat="server"
                                            TextMode="Password"></asp:TextBox>
                                    </div>
                                    <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                        <ContentTemplate>
                                            <asp:Label ID="lblErrEpin" runat="server" CssClass="error"></asp:Label>
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="txtRefralId" EventName="TextChanged" />
                                            <asp:AsyncPostBackTrigger ControlID="txtUplinerId" EventName="TextChanged" />
                                            <asp:AsyncPostBackTrigger ControlID="txtPIN" EventName="TextChanged" />
                                            <asp:AsyncPostBackTrigger ControlID="txtScratch" EventName="TextChanged" />
                                        </Triggers>
                                    </asp:UpdatePanel>
                                    <div class="form-group">
                                        <center>
                                            <asp:CheckBox ID="chkterms" runat="server" onclick="DivOnOff('DivTerms',this.checked);"
                                                TabIndex="28" />
                                            <font face="Verdana" color="#000000" size="1"><b>I Agree With <a href="#" target="_blank">Terms And Condition</a></b></font>
                                        </center>
                                        <div id="DivTerms" style="display: none">
                                            <asp:Button ID="CmdSave" runat="server" Text="Submit" CssClass="btn btn-primary " TabIndex="29"
                                                ValidationGroup="eInformation" OnClick="CmdSave_Click" />
                                            &nbsp;<asp:Button ID="CmdCancel" runat="server" Text="Cancel" CssClass="btn btn-primary "
                                                ValidationGroup="eCancel" TabIndex="30" Visible="false" />
                                        </div>

                                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
                                            ShowSummary="False" ValidationGroup="eSponsor" />
                                        <asp:ValidationSummary ID="ValidationSummary2" runat="server" ShowMessageBox="True"
                                            ShowSummary="False" ValidationGroup="eInformation" />
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" Display="None" ControlToValidate="txtPIN"
                                            runat="server" ErrorMessage="Epin No. Required" SetFocusOnError="true" ValidationGroup="eSponsor"></asp:RequiredFieldValidator>
                                        &nbsp;
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" Display="None" ControlToValidate="txtScratch"
                            runat="server" ErrorMessage="Scratch No. Required" SetFocusOnError="true" ValidationGroup="eSponsor"></asp:RequiredFieldValidator>
                                        &nbsp;<asp:RegularExpressionValidator ID="RegularExpressionValidator10" runat="server"
                                            ErrorMessage="Special characters are not allowed" ControlToValidate="txtScratch"
                                            ValidationExpression="^[a-zA-Z0-9 _]*$" Display="None" ValidationGroup="eSponsor" />&nbsp;
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" Display="None" ControlToValidate="txtRefralId"
                            runat="server" ErrorMessage="SponsorID Required" SetFocusOnError="true" ValidationGroup="eSponsor"></asp:RequiredFieldValidator>
                                        &nbsp;<asp:RegularExpressionValidator ID="RegularExpressionValidator8" runat="server"
                                            ErrorMessage="Special characters are not allowed" ControlToValidate="txtRefralId"
                                            ValidationExpression="^[a-zA-Z0-9 _]*$" Display="None" ValidationGroup="eSponsor" />&nbsp;
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" Display="None" ControlToValidate="txtUplinerId"
                            runat="server" ErrorMessage="PlaceUnderID Required" SetFocusOnError="true" ValidationGroup="eSponsor"></asp:RequiredFieldValidator>
                                        &nbsp;<asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server"
                                            ErrorMessage="Special characters are not allowed" ControlToValidate="txtUplinerId"
                                            ValidationExpression="^[a-zA-Z0-9 _]*$" Display="None" ValidationGroup="eSponsor" />&nbsp;
                            <asp:RequiredFieldValidator runat="server" ID="RFV123" ValidationGroup="eSponsor" ControlToValidate="RbtnLegNo" ErrorMessage="Choose Position" Display="None" />
                                        &nbsp;
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" Display="None" ControlToValidate="txtFrstNm"
                            runat="server" ErrorMessage="Member Name Required" SetFocusOnError="true" ValidationGroup="eInformation"></asp:RequiredFieldValidator>
                                        &nbsp;<asp:RegularExpressionValidator ID="RegularExpressionValidator7" runat="server"
                                            ErrorMessage="Special characters are not allowed" ControlToValidate="txtFrstNm"
                                            ValidationExpression="^[a-zA-Z0-9 _]*$" SetFocusOnError="true" Display="None"
                                            ValidationGroup="eInformation" />
                                        &nbsp;<asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server"
                                            ErrorMessage="Special characters are not allowed" ControlToValidate="txtFNm"
                                            ValidationExpression="^[a-zA-Z0-9 _]*$" SetFocusOnError="true" Display="None"
                                            ValidationGroup="eInformation" />&nbsp;
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator10" Display="None" ControlToValidate="txtAddLn1"
                            runat="server" ErrorMessage="Address1 Required" SetFocusOnError="true" ValidationGroup="eInformation"></asp:RequiredFieldValidator>&nbsp;
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator13" Display="None" ControlToValidate="TxtMobileNo"
                            runat="server" ErrorMessage="Mobile No. Required" SetFocusOnError="true" ValidationGroup="eInformation"></asp:RequiredFieldValidator>&nbsp;
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator6" runat="server" Display="None"
                            ControlToValidate="TxtMobileNo" ErrorMessage="Minimum 10 Digits" ValidationExpression="^[0-9]{10,10}$"
                            SetFocusOnError="true" ValidationGroup="eInformation"></asp:RegularExpressionValidator>&nbsp;
                       
                                       <%-- <asp:RegularExpressionValidator ID="RegularExpressionValidator14" runat="server"
                            Display="None" ControlToValidate="txtPhNo" ErrorMessage="Minimum 10 Digits with STD Code"
                            ValidationExpression="^[0-9]{10,10}$" SetFocusOnError="true" ValidationGroup="eInformation"></asp:RegularExpressionValidator>&nbsp;
                                       --%>
                                        <asp:RegularExpressionValidator ID="EmailExpressionValidator" runat="server" ControlToValidate="txtEMailId"
                                            ErrorMessage="Enter Valid Email ID!" Display="None" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                            SetFocusOnError="true" ValidationGroup="eInformation"></asp:RegularExpressionValidator>&nbsp;
                        
                                       <%-- <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ErrorMessage="Please check PAN Format"
                            Display="None" SetFocusOnError="true" ControlToValidate="txtPanNo" ValidationExpression="[A-Za-z]{5}\d{4}[A-Za-z]{1}"
                            ValidationGroup="eInformation"></asp:RegularExpressionValidator>&nbsp;--%>

                                        <%--     <asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server" Display="None"
                            ErrorMessage="Special characters are not allowed" ControlToValidate="txtNominee"
                            SetFocusOnError="true" ValidationExpression="^[a-zA-Z0-9 _]*$" ValidationGroup="eInformation" />&nbsp;
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator17" runat="server"
                            Display="None" ErrorMessage="Special characters are not allowed" ControlToValidate="txtRelation"
                            SetFocusOnError="true" ValidationExpression="^[a-zA-Z0-9 _]*$" ValidationGroup="eInformation" />&nbsp;
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator20" runat="server"
                            ErrorMessage="Special characters not allowed" ControlToValidate="TxtBranchName"
                            ValidationExpression="^[a-zA-Z0-9 _]*$" Display="None" SetFocusOnError="true"
                            ValidationGroup="eInformation" />&nbsp;--%>
                                        <%--   <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ErrorMessage="Special characters not allowed"
                            ControlToValidate="txtIfsCode" ValidationExpression="^[a-zA-Z0-9 _]*$" Display="None"
                            SetFocusOnError="true" ValidationGroup="eInformation" />&nbsp;      --%>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator12" Display="None" ControlToValidate="TxtPasswd"
                                            runat="server" ErrorMessage="Password Required" SetFocusOnError="true" ValidationGroup="eInformation"></asp:RequiredFieldValidator>

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

