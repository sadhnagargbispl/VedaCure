<%@ Page Title="" Language="C#" MasterPageFile="~/SitePage.master" AutoEventWireup="true" CodeFile="NewWeeklyBinaryIncome.aspx.cs" Inherits="NewWeeklyBinaryIncome" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script>
        function openPopup(element) {
            var url = element.href;
            hs.htmlExpand(element, {
                objectType: 'iframe',
                width: 620,
                height: 450,
                marginTop: 0
            });
            return false;
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
            </ul>
            <div class="row">
                <div class="col-12 col-sm-12 col-lg-12">
                    <div class="card">
                        <div class="card-header">
                            <h5 class="mb-0">My Income</h5>
                        </div>
                        <div class="card-body">
                            <div class="row g-1">
                                <div class="clr">
                                    <asp:Label ID="errMsg" runat="server" CssClass="error"></asp:Label>
                                </div>


                                <div class="col-md-12" runat="server" id="DivH" visible="true">
                                     <div class="table-responsive" style="overflow: scroll;">
                                    <asp:DataGrid ID="GrdPayout" runat="server" PageSize="10" CssClass="table table-bordered " CellPadding="3"
                                        HorizontalAlign="Center" AutoGenerateColumns="False" AllowPaging="True" Width="100%" OnPageIndexChanged="GrdPayout_PageIndexChanged">
                                        <AlternatingItemStyle CssClass="GridAltItem"></AlternatingItemStyle>
                                        <ItemStyle CssClass="GridItem"></ItemStyle>
                                        <HeaderStyle CssClass="GridHeader"></HeaderStyle>
                                        <PagerStyle CssClass="GridPager"></PagerStyle>
                                        <Columns>
                                            <asp:BoundColumn DataField="SessID" HeaderText="SessID"></asp:BoundColumn>
                                            <asp:BoundColumn DataField="PayoutDate" HeaderText="Session Time"></asp:BoundColumn>

                                            <asp:TemplateColumn HeaderText="Generation Income form first purchase">
                                                <ItemTemplate>
                                                    <a href='<%#"ViewTeamInfinity.aspx?SessId=" + Eval("SessId") %>' onclick="return openPopup(this)"
                                                        style="color: Blue;">
                                                        <asp:Label ID="Label1" runat="server" ForeColor="Blue" Text='<%# Eval("GenerationInc") %>'></asp:Label>
                                                    </a>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>

                                            <asp:BoundColumn DataField="globalpool" HeaderText="Global Pool Income  From First Purchase"></asp:BoundColumn>
                                          <%--  <asp:BoundColumn DataField="GenerationBonus" HeaderText="Generation<br/>Bouns"></asp:BoundColumn>--%>
                                            <asp:BoundColumn DataField="RePairIncome" HeaderText="Repurchase Matching Income"></asp:BoundColumn>
                                            <asp:BoundColumn DataField="GrowthIncome" HeaderText="Star Growth Income"></asp:BoundColumn>
                                           <%-- <asp:BoundColumn DataField="AmbassadorFund" HeaderText="Ambassador<br/>Fund Income (CTO)"></asp:BoundColumn>--%>

                                            <asp:BoundColumn DataField="NetIncome" HeaderText="Gross Income"></asp:BoundColumn>
                                            <asp:BoundColumn DataField="TdsAmount" HeaderText="TDS Amount"></asp:BoundColumn>
                                            <asp:BoundColumn DataField="AdminCharge" HeaderText="Admin Charge"></asp:BoundColumn>
                                            <asp:BoundColumn DataField="Deduction" HeaderText="Total Deduction"></asp:BoundColumn>
                                            <asp:BoundColumn DataField="PrevBal" HeaderText="Previous Balance"></asp:BoundColumn>
                                            <asp:BoundColumn DataField="ChqAmt" HeaderText="Net Incentive"></asp:BoundColumn>
                                            <asp:BoundColumn DataField="ClsBal" HeaderText="Closing Balance"></asp:BoundColumn>
                                            <asp:TemplateColumn HeaderText="Statement">
                                                <ItemTemplate>
                                                    <a href='<%# "NewWeeklyDailyStatement.aspx?PayoutNo=" + Eval("SessID")%>' target="_blank">Statement</a>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
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

