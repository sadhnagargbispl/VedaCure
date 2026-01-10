<%@ Page Title="" Language="C#" MasterPageFile="~/SitePage.master" AutoEventWireup="true" CodeFile="MyPurchase.aspx.cs" Inherits="MyPurchase" %>


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
                            <h5 class="mb-0">My Purchase</h5>
                        </div>
                        <div class="card-body">
                            <div class="row g-1">
                                <div class="clr">
                                    <asp:Label ID="errMsg" runat="server" CssClass="error"></asp:Label>
                                </div>


                                <div class="col-md-12" runat="server" id="DivH" visible="true">
                                     <div class="table-responsive" style="overflow: scroll;">
                                    <asp:DataGrid ID="GrdDirects" runat="server" PageSize="10" CssClass="table table-bordered"
                                        CellPadding="3" HorizontalAlign="Center" AutoGenerateColumns="False" AllowPaging="True"
                                        Width="100%" >
                                        <AlternatingItemStyle CssClass="GridAltItem"></AlternatingItemStyle>
                                        <ItemStyle CssClass="GridItem"></ItemStyle>
                                        <HeaderStyle CssClass="GridHeader"></HeaderStyle>
                                        <PagerStyle CssClass="GridPager"></PagerStyle>
                                        <Columns>
                                            <asp:TemplateColumn HeaderText="Bill No.">
                                                <ItemTemplate>
                                                    <a href='<%# "GstBill.aspx?BillNo=" + Eval("UserBillNo")  %>' target="_blank">
                                                        <asp:Label ID="Label1" runat="server" Text='<%# Eval("UserBillNo")%>'></asp:Label>
                                                    </a>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                        </Columns>
                                        <Columns>
                                            <asp:BoundColumn DataField="BillDate" HeaderText="Bill Date<br/>">
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle>
                                            </asp:BoundColumn>

                                            <asp:BoundColumn DataField="BvValue" HeaderText="BV<br/>" Visible="false">
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle>
                                            </asp:BoundColumn>
                                            <asp:BoundColumn DataField="BvValue" HeaderText="BV<br/>">
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle>
                                            </asp:BoundColumn>
                                            <asp:BoundColumn DataField="Amount" HeaderText="Bill Amount<br/>">
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle>
                                            </asp:BoundColumn>
                                            <asp:BoundColumn DataField="TaxAmount" HeaderText="Tax Amount<br/>">
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle>
                                            </asp:BoundColumn>
                                            <asp:BoundColumn DataField="NetPayable" HeaderText="Net Payable">
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle>
                                            </asp:BoundColumn>
                                        </Columns>
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
