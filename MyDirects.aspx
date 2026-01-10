<%@ Page Title="" Language="C#" MasterPageFile="~/SitePage.master" AutoEventWireup="true" CodeFile="MyDirects.aspx.cs" Inherits="MyDirects" EnableEventValidation="false" %>

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
                            <h5 class="mb-0">Direct Members</h5>
                        </div>
                        <div class="card-body">
                            <div class="row g-1">
                                <div class="clr">
                                    <asp:Label ID="errMsg" runat="server" CssClass="error"></asp:Label>
                                </div>

                                <div class="col-md-3">
                                    <div class="form-group">
                                        <asp:Label ID="lblStartDate" runat="server" Text="Choose Level: "></asp:Label>
                                        <asp:DropDownList ID="DDLLevel" runat="server" class="form-control">
                                        </asp:DropDownList>
                                    </div>
                                </div>

                                <div class="col-md-3" id="div4" runat="server" style="margin-top: 20px;">
                                    <div class="form-group" style="margin-top: 9px;">
                                        <asp:Button ID="BtnSearch" runat="server" Text="Search" TabIndex="3" class="btn btn-primary" OnClick="BtnSearch_Click" />

                                    </div>
                                </div>
                                <div class="col-md-12">

                                    <div class="table-responsive">
                                        <asp:DataGrid ID="GrdDirects" runat="server" CellPadding="3"
                                            HorizontalAlign="Center" AutoGenerateColumns="False" Width="100%" AllowPaging="true" PageSize="25" CssClass="table table-bordered " OnPageIndexChanged="GrdDirects_PageIndexChanged">
                                            <AlternatingItemStyle CssClass="GridAltItem"></AlternatingItemStyle>
                                            <ItemStyle CssClass="GridItem"></ItemStyle>
                                            <HeaderStyle CssClass="GridHeader"></HeaderStyle>
                                            <PagerStyle CssClass="GridPager"></PagerStyle>
                                            <Columns>
                                                <asp:TemplateColumn>
                                                    <ItemTemplate>
                                                        <%#Container.DataSetIndex + 1%>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <asp:BoundColumn DataField="IDNo" HeaderText="ID No"></asp:BoundColumn>
                                                <asp:BoundColumn DataField="MemName" HeaderText="Member Name"></asp:BoundColumn>
                                                <asp:BoundColumn DataField="Doj" HeaderText="Date Of joining"></asp:BoundColumn>
                                                <asp:BoundColumn DataField="MLevel" HeaderText="Level"></asp:BoundColumn>
                                                <asp:BoundColumn DataField="RefIdNo" HeaderText="Placement ID"></asp:BoundColumn>
                                                <asp:BoundColumn DataField="ActiveStatus" HeaderText="Active Status"></asp:BoundColumn>
                                                <asp:BoundColumn DataField="UpgradeDate" HeaderText="Date Of Activation"></asp:BoundColumn>
                                                <asp:BoundColumn DataField="Bv" HeaderText="Self Current Repurchase BV"></asp:BoundColumn>
                                                <%-- <asp:BoundColumn DataField="SelfTotalPV" HeaderText="Self Total BV"></asp:BoundColumn>   --%>
                                                <asp:BoundColumn DataField="kitname" HeaderText="Package"></asp:BoundColumn>
                                                <%--<asp:BoundColumn DataField="currentdwnPurchase" HeaderText="Current Session <br/>Downline Purchase"></asp:BoundColumn> 
       <asp:BoundColumn DataField="TotalDwnPurchase" HeaderText="Total Downline Purchase"></asp:BoundColumn>    --%>
                                                <%--<asp:BoundColumn DataField="SelfIncentive" HeaderText="Self Incentive Point"></asp:BoundColumn> --%>

                                                <asp:BoundColumn DataField="Designation" HeaderText="Designation"></asp:BoundColumn>
                                                <asp:BoundColumn DataField="LDBRank" HeaderText="Leader"></asp:BoundColumn>
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

