<%@ Page Title="" Language="C#" MasterPageFile="~/SitePage.master" AutoEventWireup="true" CodeFile="DownlinePurchase.aspx.cs" Inherits="DownlinePurchase" %>


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
                            <h5 class="mb-0">Member Downline Purchase</h5>
                        </div>
                        <div class="card-body">
                            <div class="row g-1">
                                <div class="clr">
                                    <asp:Label ID="errMsg" runat="server" CssClass="error"></asp:Label>
                                </div>
                                <div class="clearfix gen-profile-box">
                                    <div class="profile-bar clearfix" style="background: #fff;">
                                        <div class="row">
                                            <div class="col-md-2" style="padding-top: 25px;">
                                                <div class="form-group pull-none">
                                                    <asp:DropDownList ID="RbtProduct" runat="server" TabIndex="3" RepeatColumns="2" RepeatDirection="Horizontal" CssClass="form-control"
                                                        AutoPostBack="false" OnSelectedIndexChanged="RbtProduct_SelectedIndexChanged">
                                                        <asp:ListItem Text="Repurchase" Value="R" Selected="True"></asp:ListItem>
                                                        <asp:ListItem Text="Activation" Value="T"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="col-md-2" style="padding-top: 25px;">
                                                <div class="form-group pull-none">
                                                    <asp:DropDownList ID="DDlSelectType" runat="server" TabIndex="3" RepeatColumns="2"
                                                        RepeatDirection="Horizontal" AutoPostBack="false" OnSelectedIndexChanged="DDlSelectType_SelectedIndexChanged" CssClass="form-control">
                                                        <asp:ListItem Text="Generation wise" Value="G" Selected="True"></asp:ListItem>
                                                        <asp:ListItem Text="Matching Wise" Value="M"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="col-md-2">
                                                <div class="form-group pull-none">
                                                    <label for="inputdefault">
                                                        <asp:Label ID="LblLevel" runat="server" Text="Level Wise* "></asp:Label></label>
                                                    <asp:DropDownList ID="DdlLevel" CssClass="form-control" TabIndex="1" runat="server">
                                                    </asp:DropDownList>
                                                    <asp:DropDownList ID="RbtLegNo" runat="server" RepeatColumns="3" RepeatDirection="Horizontal"
                                                        Visible="false" CssClass="form-control">
                                                        <asp:ListItem Text="Both" Value="0" Selected="True"></asp:ListItem>
                                                        <asp:ListItem Text="Group A" Value="1"></asp:ListItem>
                                                        <asp:ListItem Text="Group B" Value="2"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="col-md-2">
                                                <div class="form-group pull-none">
                                                    <label for="inputdefault">
                                                        From:</label>
                                                    <asp:TextBox ID="TxtFromDate" CssClass="form-control" TabIndex="2" runat="server"></asp:TextBox>
                                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="TxtFromDate"
                                                        Format="dd-MMM-yyyy"></ajaxToolkit:CalendarExtender>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="TxtFromDate"
                                                        ErrorMessage="Invalid Start Date" Font-Names="arial" Font-Size="10px" SetFocusOnError="True"
                                                        ValidationExpression="^(?:((31-(Jan|Mar|May|Jul|Aug|Oct|Dec))|((([0-2]\d)|30)-(Jan|Mar|Apr|May|Jun|Jul|Aug|Sep|Oct|Nov|Dec))|(([01]\d|2[0-8])-Feb))|(29-Feb(?=-((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00)))))-((1[6-9]|[2-9]\d)\d{2})$"
                                                        ValidationGroup="Form-submit"></asp:RegularExpressionValidator>
                                                </div>
                                            </div>
                                            <div class="col-md-2">
                                                <div class="form-group pull-none">
                                                    <label for="inputdefault">
                                                        To :</label>
                                                    <asp:TextBox ID="TxtToDate" CssClass="form-control" TabIndex="3" runat="server"></asp:TextBox>
                                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="TxtToDate"
                                                        Format="dd-MMM-yyyy"></ajaxToolkit:CalendarExtender>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="TxtToDate"
                                                        ErrorMessage="Invalid To Date" Font-Names="arial" Font-Size="10px" SetFocusOnError="True"
                                                        ValidationExpression="^(?:((31-(Jan|Mar|May|Jul|Aug|Oct|Dec))|((([0-2]\d)|30)-(Jan|Mar|Apr|May|Jun|Jul|Aug|Sep|Oct|Nov|Dec))|(([01]\d|2[0-8])-Feb))|(29-Feb(?=-((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00)))))-((1[6-9]|[2-9]\d)\d{2})$"
                                                        ValidationGroup="Form-submit"></asp:RegularExpressionValidator>
                                                </div>
                                            </div>
                                            <div class="col-md-2" style="padding-top: 25px;">
                                                <div class="form-group pull-none">
                                                    <div class="form-group pull-none">
                                                        <asp:Button ID="BtnSearch" Text="Search" runat="server" class="btn btn-primary" OnClick="BtnSearch_Click" />
                                                        <asp:Button ID="BtnExportToExcel" runat="server" Text="Export To Excel" class="btn btn-primary" />
                                                    </div>
                                                </div>
                                            </div>
                                        </div>

                                        <div id="divT" runat="server" visible="false"
                                            style="display: flex; align-items: center; gap: 18px; flex-wrap: nowrap; white-space: nowrap;">

                                            <asp:Label ID="Label1" runat="server" Font-Bold="true" Text="Total Record :"></asp:Label>
                                            <asp:Label ID="LblttlRcd" Font-Bold="true" runat="server"></asp:Label>

                                            <span>Left BV :</span>
                                            <asp:Label ID="Lblleft" Font-Bold="true" runat="server"></asp:Label>

                                            <span>Right BV :</span>
                                            <asp:Label ID="Lblright" Font-Bold="true" runat="server"></asp:Label>

                                            <div id="divRoyalty" runat="server" visible="false"
                                                style="display: flex; align-items: center; gap: 10px;">
                                                <span>Left Royalty BV:</span>
                                                <asp:Label ID="LblLeftRoyalty" Font-Bold="true" runat="server" Text="0.00"></asp:Label>

                                                <span>Right Royalty BV:</span>
                                                <asp:Label ID="LblRightRoyalty" Font-Bold="true" runat="server" Text="0.00"></asp:Label>
                                            </div>

                                        </div>


                                        <div class="row" id="divR" runat="server" visible="false"
                                            style="display: flex; align-items: center; white-space: nowrap;">


                                            <div id="divtotal" runat="server"
                                                style="align-items: center; flex-wrap: nowrap; white-space: nowrap;">
                                                <asp:Label ID="Label3" runat="server" Font-Bold="true" Text="Total Record : "></asp:Label>
                                                <asp:Label ID="LblttlRcd1" Font-Bold="true" runat="server"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;
                                                <asp:Label ID="Label2" runat="server" Font-Bold="true" Text="Total BV : "></asp:Label>
                                                <asp:Label ID="lblTotalBV" Font-Bold="true" runat="server"></asp:Label>
                                            </div>

                                            <asp:Label ID="lblleftbv" runat="server"
                                                Style="font-weight: bold; font-size: 14px;"></asp:Label>

                                            <asp:Label ID="lblbv" runat="server"
                                                Style="font-weight: bold; font-size: 14px;"></asp:Label>

                                            <asp:Label ID="lblroyaltileftbv" runat="server"
                                                Style="font-weight: bold; font-size: 14px;"></asp:Label>

                                            <asp:Label ID="lblroyaltirightbv" runat="server"
                                                Style="font-weight: bold; font-size: 14px;"></asp:Label>

                                            <asp:Label ID="LeftSmartcardbv" runat="server"
                                                Style="font-weight: bold; font-size: 14px;"></asp:Label>

                                            <asp:Label ID="rightSmartcardbv" runat="server"
                                                Style="font-weight: bold; font-size: 14px;"></asp:Label>

                                        </div>



                                        <div class="row" style="display: none;">

                                            <div class="col-md-2">
                                                <asp:DropDownList ID="ddlPazeSize" runat="server" CssClass="form-control" AutoPostBack="true">
                                                    <asp:ListItem Text="10" Value="10"></asp:ListItem>
                                                    <asp:ListItem Text="20" Value="20"></asp:ListItem>
                                                    <asp:ListItem Text="50" Value="50"></asp:ListItem>
                                                    <asp:ListItem Text="100" Value="100"></asp:ListItem>
                                                    <asp:ListItem Text="150" Value="150"></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>

                                        <div id="DivSideA" class="table-responsive" runat="server">
                                            <asp:DataGrid ID="GrdDirects" runat="server" PageSize="10" CssClass="table table-striped table-bordered"
                                                CellPadding="3" HorizontalAlign="Center" AutoGenerateColumns="true" AllowPaging="True"
                                                Width="100%" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"
                                                HeaderStyle-VerticalAlign="Middle" ItemStyle-VerticalAlign="Middle" OnPageIndexChanged="GrdDirects_PageIndexChanged">
                                                <Columns>
                                                    <asp:TemplateColumn HeaderText="S.No">
                                                        <ItemTemplate>
                                                            <%#Container.DataSetIndex + 1%>.
                                                        </ItemTemplate>
                                                    </asp:TemplateColumn>
                                                </Columns>
                                                <PagerStyle Mode="NumericPages" CssClass="PagerStyle"></PagerStyle>
                                                <ItemStyle CssClass="RowStyle" HorizontalAlign="Center" VerticalAlign="Top" Wrap="False" />
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

