<%@ Page Title="" Language="C#" MasterPageFile="~/SitePage.master" AutoEventWireup="true" CodeFile="Downline.aspx.cs" Inherits="Downline" EnableEventValidation="false" %>

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
                            <h5 class="mb-0">Member Downline</h5>
                        </div>
                        <div class="card-body">
                            <div class="row g-1">
                                <div class="clr">
                                    <asp:Label ID="errMsg" runat="server" CssClass="error"></asp:Label>
                                </div>


                                <div class="col-md-3" id="joiningtype" runat="server">
                                    <div class="form-group">
                                        Level
                                                           <asp:Label ID="lbltype" runat="server" Text="Search Type: "></asp:Label>
                                        <asp:DropDownList ID="DDlSearch" runat="server" class="form-control">
                                            <asp:ListItem Text="Joining" Value="J"></asp:ListItem>
                                            <asp:ListItem Text="Activation" Value="A"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-3" id="startdate" runat="server">
                                    <div class="form-group">
                                        <asp:Label ID="lblStartDate" runat="server" Text="Choose Start Date : "></asp:Label>
                                        <asp:TextBox ID="txtStartDate" runat="server" class="form-control"></asp:TextBox>
                                        <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtStartDate" Format="dd-MMM-yyyy"></ajaxToolkit:CalendarExtender>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtStartDate" ErrorMessage="Invalid Start Date" Font-Names="arial" Font-Size="10px" SetFocusOnError="True" ValidationExpression="^(?:((31-(Jan|Mar|May|Jul|Aug|Oct|Dec))|((([0-2]\d)|30)-(Jan|Mar|Apr|May|Jun|Jul|Aug|Sep|Oct|Nov|Dec))|(([01]\d|2[0-8])-Feb))|(29-Feb(?=-((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00)))))-((1[6-9]|[2-9]\d)\d{2})$" ValidationGroup="Form-submit"> </asp:RegularExpressionValidator>

                                    </div>
                                </div>
                                <div class="col-md-3" id="enddate" runat="server">
                                    <asp:Label ID="lblEndDate" runat="server" Text="Choose End Date : "></asp:Label>
                                    <asp:TextBox ID="txtEndDate" runat="server" class="form-control"></asp:TextBox>
                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtEndDate" Format="dd-MMM-yyyy"></ajaxToolkit:CalendarExtender>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtEndDate" ErrorMessage="Invalid End Date" Font-Names="arial" Font-Size="10px" SetFocusOnError="True" ValidationExpression="^(?:((31-(Jan|Mar|May|Jul|Aug|Oct|Dec))|((([0-2]\d)|30)-(Jan|Mar|Apr|May|Jun|Jul|Aug|Sep|Oct|Nov|Dec))|(([01]\d|2[0-8])-Feb))|(29-Feb(?=-((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00)))))-((1[6-9]|[2-9]\d)\d{2})$" ValidationGroup="Form-submit"></asp:RegularExpressionValidator>

                                </div>

                                <div class="col-md-3" style="margin-top: 20px;" id="idbtnsearch" runat="server">
                                    <div class="form-group" style="margin-top: 9px;">
                                        <asp:Button ID="BtnSubmit" runat="server" Text="Search" TabIndex="3" class="btn btn-primary" OnClick="BtnSearch_Click" />
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-group">
                                        Search By
                                        <asp:RadioButtonList ID="rbleg" RepeatDirection="Horizontal" runat="server" >
                                            <asp:ListItem Text="Both" Value="0" Selected="True"></asp:ListItem>
                                            <asp:ListItem Text="Left" Value="1"> </asp:ListItem>
                                            <asp:ListItem Text="Right" Value="2"> </asp:ListItem>
                                        </asp:RadioButtonList>
                                    </div>
                                </div>


                                <div id="DivSideA" runat="server" class="col-md-12">
                                    <h4>Left Downline</h4>
                                    <div class="form-group">
                                        <asp:Button ID="BtnExportA" runat="server" Text="Export" CssClass="btn btn-primary" OnClick="BtnExportA_Click" />
                                    </div>
                                    <div class="spacedivider2"></div>

                                    <div class="table-responsive">
                                        <asp:DataGrid ID="GrdDirects1" runat="server" CssClass="table table-bordered" AutoGenerateColumns="False"
                                            AllowPaging="True" OnPageIndexChanged="GrdDirects1_PageIndexChanged">
                                            <AlternatingItemStyle CssClass="GridAltItem"></AlternatingItemStyle>
                                            <ItemStyle CssClass="GridItem"></ItemStyle>
                                            <HeaderStyle CssClass="GridHeader"></HeaderStyle>
                                            <PagerStyle CssClass="GridPager"></PagerStyle>
                                            <Columns>
                                                <asp:BoundColumn DataField="mlevel" HeaderText="Level"></asp:BoundColumn>
                                                <asp:BoundColumn DataField="IDNO" HeaderText="ID No"></asp:BoundColumn>
                                                <asp:BoundColumn DataField="MemName" HeaderText="Member Name"></asp:BoundColumn>
                                                <asp:BoundColumn DataField="Sponsor" HeaderText="Placement ID"></asp:BoundColumn>
                                                <asp:BoundColumn DataField="RefFormno" HeaderText="Referal ID"></asp:BoundColumn>
                                                <asp:BoundColumn DataField="RefName" HeaderText="Referal Name"></asp:BoundColumn>
                                                <asp:BoundColumn DataField="doj" HeaderText="Date Of Joining"></asp:BoundColumn>
                                                <asp:BoundColumn DataField="KitName" HeaderText="Package"></asp:BoundColumn>
                                                <asp:BoundColumn DataField="TopupDate" HeaderText="Topup Date"></asp:BoundColumn>
                                            </Columns>
                                            <PagerStyle Mode="NumericPages" CssClass="PagerStyle"></PagerStyle>
                                        </asp:DataGrid>
                                    </div>
                                </div>

                                <div id="DivSideB" runat="server" class="col-md-12">
                                    <h4>Right Downline</h4>
                                    <div class="form-group">
                                        <asp:Button ID="BtnExportB" runat="server" Text="Export" CssClass="btn btn-primary" OnClick="BtnExportB_Click" />
                                    </div>
                                    <div class="spacedivider2"></div>

                                    <div class="table-responsive">
                                        <asp:DataGrid ID="GrdDirects2" runat="server" CssClass="table table-bordered " AutoGenerateColumns="False"
                                            AllowPaging="True" OnPageIndexChanged="GrdDirects2_PageIndexChanged">
                                            <AlternatingItemStyle CssClass="GridAltItem"></AlternatingItemStyle>
                                            <ItemStyle CssClass="GridItem"></ItemStyle>
                                            <HeaderStyle CssClass="GridHeader"></HeaderStyle>
                                            <PagerStyle CssClass="GridPager"></PagerStyle>
                                            <Columns>
                                                <asp:BoundColumn DataField="mlevel" HeaderText="Level"></asp:BoundColumn>
                                                <asp:BoundColumn DataField="IDNO" HeaderText="ID No"></asp:BoundColumn>
                                                <asp:BoundColumn DataField="MemName" HeaderText="Member Name"></asp:BoundColumn>
                                                <asp:BoundColumn DataField="Sponsor" HeaderText="Placement ID"></asp:BoundColumn>
                                                <asp:BoundColumn DataField="RefFormno" HeaderText="Referal ID"></asp:BoundColumn>
                                                <asp:BoundColumn DataField="RefName" HeaderText="Referal Name"></asp:BoundColumn>
                                                <asp:BoundColumn DataField="doj" HeaderText="Date Of Joining"></asp:BoundColumn>
                                                <asp:BoundColumn DataField="KitName" HeaderText="Package"></asp:BoundColumn>
                                                <asp:BoundColumn DataField="TopupDate" HeaderText="Topup Date"></asp:BoundColumn>
                                            </Columns>
                                            <PagerStyle Mode="NumericPages" CssClass="PagerStyle"></PagerStyle>
                                        </asp:DataGrid>

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

