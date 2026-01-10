<%@ Page Title="" Language="C#" MasterPageFile="~/SitePage.master" AutoEventWireup="true" CodeFile="Totalteam.aspx.cs" Inherits="Totalteam" EnableEventValidation="false" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
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
            </ul>
            <div class="row">
                <div class="col-12 col-sm-12 col-lg-12">
                    <div class="card">
                        <div class="card-header">
                            <h5 class="mb-0">Total Team Status</h5>
                        </div>
                        <div class="card-body">
                            <div class="row g-1">
                                <div class="clr">
                                    <asp:Label ID="errMsg" runat="server" CssClass="error"></asp:Label>
                                </div>

                                <div class="col-md-3">
                                    <div class="form-group">
                                        <asp:Label ID="lblStartDate" runat="server" Text="Choose Start Date : "></asp:Label>
                                        <asp:TextBox ID="txtStartDate" runat="server" class="form-control"></asp:TextBox>
                                        <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtStartDate"
                                            Format="dd-MMM-yyyy"></ajaxToolkit:CalendarExtender>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtStartDate"
                                            ErrorMessage="Invalid Start Date" Font-Names="arial" Font-Size="10px" SetFocusOnError="True"
                                            ValidationExpression="^(?:((31-(Jan|Mar|May|Jul|Aug|Oct|Dec))|((([0-2]\d)|30)-(Jan|Mar|Apr|May|Jun|Jul|Aug|Sep|Oct|Nov|Dec))|(([01]\d|2[0-8])-Feb))|(29-Feb(?=-((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00)))))-((1[6-9]|[2-9]\d)\d{2})$"
                                            ValidationGroup="Form-submit"></asp:RegularExpressionValidator>
                                    </div>
                                </div>
                                <div class="col-md-3" id="lbllevel" runat="server">
                                    <div class="form-group">
                                        <asp:Label ID="lblEndDate" runat="server" Text="Choose End Date : "></asp:Label>
                                        <asp:TextBox ID="txtEndDate" runat="server" class="form-control"></asp:TextBox>
                                        <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtEndDate"
                                            Format="dd-MMM-yyyy"></ajaxToolkit:CalendarExtender>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtEndDate"
                                            ErrorMessage="Invalid End Date" Font-Names="arial" Font-Size="10px" SetFocusOnError="True"
                                            ValidationExpression="^(?:((31-(Jan|Mar|May|Jul|Aug|Oct|Dec))|((([0-2]\d)|30)-(Jan|Mar|Apr|May|Jun|Jul|Aug|Sep|Oct|Nov|Dec))|(([01]\d|2[0-8])-Feb))|(29-Feb(?=-((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00)))))-((1[6-9]|[2-9]\d)\d{2})$"
                                            ValidationGroup="Form-submit"></asp:RegularExpressionValidator>
                                    </div>
                                </div>
                                <div class="col-md-3" id="div4" runat="server" style="margin-top: 20px;">
                                    <div class="form-group" style="margin-top: 9px;">
                                        <asp:Button ID="Btnsubmit" runat="server" Text="Search" TabIndex="3" class="btn btn-primary" OnClick="Btnsubmit_Click" />

                                    </div>
                                </div>
                                <div class="col-md-12">
                                     <div class="table-responsive" style="overflow: scroll;">
                                    <table cellpadding="2" cellspacing="2" border="0" id="customers" class="table table-bordered table-striped">
                                        <tr>
                                            <th></th>
                                            <th>Left
                                            </th>
                                            <th>Right
                                            </th>
                                            <th>Total
                                            </th>
                                        </tr>
                                        <tr class="alt" style="display: none;">
                                            <td>TOTAL REGISTRATIONS
                                            </td>
                                            <td id="LeftReg" runat="server"></td>
                                            <td id="RightReg" runat="server"></td>
                                            <td id="TotalReg" runat="server">0
                                            </td>
                                        </tr>
                                        <tr class="alt" style="display: none;">
                                            <td>TOTAL ACTIVATION
                                            </td>
                                            <td id="LeftActivation" runat="server"></td>
                                            <td id="RightActivation" runat="server"></td>
                                            <td id="TotalActv" runat="server">0
                                            </td>
                                        </tr>
                                        <tr class="alt" style="display: none;">
                                            <td>FIRST PURCHASE BV
                                            </td>
                                            <td id="TdLeftBv" runat="server"></td>
                                            <td id="TdRightBv" runat="server"></td>
                                            <td id="TotalTeamBv" runat="server">0
                                            </td>
                                        </tr>
                                        <tr class="alt">
                                            <td>REPURCHASE BV                                            </td>
                                            <td id="TdLeftPremium" runat="server"></td>
                                            <td id="TdRightPremium" runat="server"></td>
                                            <td id="TdTotalPremium" runat="server">0
                                            </td>
                                        </tr>
                                        <tr class="alt" style="display: none;">
                                            <td>TOTAL BV
                                            </td>
                                            <td id="TdLeftSmartPack" runat="server"></td>
                                            <td id="TdRightSmartPack" runat="server"></td>
                                            <td id="TdTotalsmartPack" runat="server">0
                                            </td>
                                        </tr>

                                    </table>
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
