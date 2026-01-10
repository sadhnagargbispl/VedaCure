<%@ Page Title="" Language="C#" MasterPageFile="~/SitePage.master" AutoEventWireup="true" CodeFile="EpinDetail.aspx.cs" Inherits="EpinDetail" EnableEventValidation="false" %>

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
                            <h5 class="mb-0">EPin Detail</h5>
                        </div>
                        <div class="card-body">
                            <div class="row g-1">
                                <div class="clr">
                                    <asp:Label ID="errMsg" runat="server" CssClass="error"></asp:Label>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-group">
                                        Select
                                        <asp:RadioButtonList ID="rbtnStatus" runat="server" CssClass="form-control " RepeatDirection="Horizontal"
                                            AutoPostBack="True" Font-Bold="True" OnSelectedIndexChanged="rbtnStatus_SelectedIndexChanged">
                                            <asp:ListItem Selected="True">BOTH</asp:ListItem>
                                            <asp:ListItem>USED</asp:ListItem>
                                            <asp:ListItem>UN-USED</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-group">
                                        Package Wise Detail
        <asp:DropDownList ID="CmbKit" runat="server" CssClass="form-control ">
        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-4" id="joiningtype" style="margin-top: 25px;">
                                    <div class="form-group">
                                        <asp:Button ID="btnSubmit" runat="server" Text="Search" CssClass="btn btn-primary " OnClick="btnSubmit_Click" />
                                    </div>
                                </div>
                                <div id="DivTopup" runat="server" visible="false">
                                    <table align="center" border="1px" style="background-color: #5AA9CA; color: #fff;">
                                        <tr>
                                            <td align="center" colspan="2">
                                                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                    <ContentTemplate>
                                                        <asp:Label ID="Label2" runat="server" CssClass="error"></asp:Label>
                                                    </ContentTemplate>
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="TxtIDNo" EventName="TextChanged" />
                                                    </Triggers>
                                                </asp:UpdatePanel>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left">
                                                <strong>IDNo</strong>*
                                            </td>
                                            <td align="left">
                                                <asp:TextBox ID="TxtIDNo" runat="server" AutoPostBack="true"></asp:TextBox>
                                                <br />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left">
                                                <strong>ScratchNo</strong>
                                            </td>
                                            <td align="left">
                                                <asp:Label ID="lblPinNo" runat="server" Visible="false"></asp:Label>
                                                <asp:TextBox ID="TxtScratchNo" runat="server" Enabled="False" ReadOnly="True"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr id="trcommand" runat="server">
                                            <td style="height: 26px"></td>
                                            <td style="height: 26px" align="left">
                                                <asp:Button ID="btnTopup" runat="server" Text="Topup" class="buttonBG" Style="height: 24px; width: 64px;" />
                                                &nbsp;<asp:Button ID="btnCancel" runat="server" Text="Cancel" class="buttonBG" Style="height: 24px; width: 64px;" />
                                                &nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2">
                                                <div id="divconfirm" runat="server">
                                                    <table cellspacing="2" cellpadding="2" width="100%" border="1px" style="background-color: #5AA9CA; color: #fff;">
                                                        <tbody>
                                                            <tr>
                                                                <td colspan="2">
                                                                    <strong>CONFIRM TOPUP</strong>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="left">
                                                                    <strong>IDNo</strong>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="TxtIDNo1" runat="server" CssClass="inputbox_long" ReadOnly="True"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="left">
                                                                    <strong>Name</strong>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="TxtName" runat="server" CssClass="inputbox_long" ReadOnly="True"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="left">
                                                                    <strong>Topup By</strong>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="TxtPackage" runat="server" CssClass="inputbox_long" ReadOnly="True"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr id="TrDelivery" runat="server">
                                                                <td align="left">
                                                                    <strong>Delivery</strong>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:RadioButtonList ID="RbtDelivery" runat="server" AutoPostBack="True" RepeatColumns="3"
                                                                        RepeatDirection="Horizontal" RepeatLayout="Flow">
                                                                        <asp:ListItem Text="By Hand" Value="H"></asp:ListItem>
                                                                        <asp:ListItem Text="By Courier" Value="C" Selected="True"></asp:ListItem>
                                                                        <asp:ListItem Text="By SpeedPost" Value="S"></asp:ListItem>
                                                                    </asp:RadioButtonList>
                                                                </td>
                                                            </tr>
                                                            <tr id="trDeliveryCenter" runat="server" visible="false">
                                                                <td align="left">
                                                                    <strong>Delivery Center</strong>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:DropDownList ID="DDlDeliveryCenter" runat="server" Style="font: 14px 'Open Sans', sans-serif; font-weight: normal; font-style: normal; line-height: 23px; color: #727272; width: 160px;">
                                                                    </asp:DropDownList>
                                                                </td>
                                                            </tr>
                                                            <tr id="trDeliveryAddress" runat="server">
                                                                <td align="left">
                                                                    <strong>Delivery Address</strong>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="TxtDeliveryAddress" runat="server" CssClass="inputbox_long"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td valign="bottom" align="left">&nbsp;
                                                                </td>
                                                                <td valign="middle" align="left">
                                                                    <asp:Button ID="BtnConfirm" runat="server" Text="Confirm" class="buttonBG" Style="height: 24px; width: 68px;" />
                                                                    &nbsp;<asp:Button ID="Button1" runat="server" Text="Cancel" class="buttonBG" Style="height: 24px; width: 64px;" />
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                </div>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                                <div class="col-md-12" runat="server" id="DivH" visible="true">
                                    <%-- <asp:Label ID="Label1" runat="server" Text="Total Records"></asp:Label>
                                    <asp:Label ID="lbltotal" runat="server"></asp:Label>--%>
                                    <div class="table-responsive" style="overflow: scroll;">
                                        <asp:DataGrid ID="DgPayment" runat="server" CssClass="table table-bordered " AutoGenerateColumns="False"
                                            AllowPaging="True" PageSize="20" OnPageIndexChanged="DgPayment_PageIndexChanged" OnItemDataBound="DgPayment_ItemDataBound" OnItemCommand="DgPayment_ItemCommand">
                                            <AlternatingItemStyle CssClass="GridAltItem"></AlternatingItemStyle>
                                            <ItemStyle CssClass="GridItem"></ItemStyle>
                                            <HeaderStyle CssClass="GridHeader"></HeaderStyle>
                                            <PagerStyle CssClass="GridPager"></PagerStyle>
                                            <Columns>
                                                <asp:BoundColumn DataField="SNo" HeaderText="SNo."></asp:BoundColumn>
                                                <asp:TemplateColumn HeaderText="PinNo">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCardNo" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "CardNo") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn HeaderText="ScratchNo">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblScratchNo" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "ScratchNo") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <asp:BoundColumn DataField="ProductName" HeaderText="Product Name"></asp:BoundColumn>
                                                <asp:BoundColumn DataField="BillDate" HeaderText="Generated On"></asp:BoundColumn>
                                                <asp:TemplateColumn HeaderText="epinStatus">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblStatus" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Status") %>'></asp:Label>

                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <asp:BoundColumn DataField="UsedBy" HeaderText="Used By"></asp:BoundColumn>
                                                <asp:BoundColumn DataField="MemName" HeaderText="Name"></asp:BoundColumn>
                                                <asp:BoundColumn DataField="UsedDate" HeaderText="Used Date"></asp:BoundColumn>
                                                <asp:TemplateColumn HeaderText="IsTopup" Visible="false">
                                                    <ItemTemplate>


                                                        <asp:Label ID="IsTopup" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "IsTopup") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn HeaderText="Action">
                                                    <ItemTemplate>
                                                        <asp:Button ID="btnRegister" runat="server" Text="Join Now" CommandArgument="Join"
                                                            Visible="false" />
                                                        <asp:Button ID="btnTopup" runat="server" Text="Topup" CommandArgument="Topup" Visible="True" OnClick="btnTopup_Click" />
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <%-- <asp:ButtonColumn ButtonType="PushButton" CommandName="Join" HeaderText="Registration"
                                                        Text="Join Now"></asp:ButtonColumn>
                                                    <asp:ButtonColumn ButtonType="PushButton" CommandName="Topup" HeaderText="Topup"
                                                        Text="Topup"></asp:ButtonColumn>--%>
                                            </Columns>
                                        </asp:DataGrid>
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
</asp:Content>

