<%@ Page Title="" Language="C#" MasterPageFile="~/SitePage.master" AutoEventWireup="true" CodeFile="PinreceivedDetails.aspx.cs" Inherits="PinreceivedDetails" EnableEventValidation="false" %>

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
                            <h5 class="mb-0">Pin Received Detail</h5>
                        </div>
                        <div class="card-body">
                            <div class="row g-1">
                                <div class="clr">
                                    <asp:Label ID="errMsg" runat="server" CssClass="error"></asp:Label>
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

                                <div class="col-md-12" runat="server" id="DivH" visible="true">
                                     <div class="table-responsive" style="overflow: scroll;">
                                    <asp:DataGrid ID="DgReceivedPin" runat="server" PageSize="20" CssClass="table table-bordered" AutoGenerateColumns="False"
                                        AllowPaging="True" Width="100%" OnPageIndexChanged="DgPayment_PageIndexChanged">
                                        <AlternatingItemStyle CssClass="GridAltItem"></AlternatingItemStyle>
                                        <ItemStyle CssClass="GridItem"></ItemStyle>
                                        <HeaderStyle CssClass="GridHeader"></HeaderStyle>
                                        <PagerStyle CssClass="GridPager"></PagerStyle>
                                        <Columns>
                                            <asp:BoundColumn DataField="SNo" HeaderText="SNo."></asp:BoundColumn>
                                            <asp:BoundColumn DataField="FromIDNo" HeaderText="Transfer From IE"></asp:BoundColumn>
                                            <asp:BoundColumn DataField="FromMemName" HeaderText="Transfer From Entrepreneur"></asp:BoundColumn>
                                            <asp:BoundColumn DataField="ToIdno" HeaderText="Transfer To IE"></asp:BoundColumn>
                                            <asp:BoundColumn DataField="ToMemName" HeaderText="Transfer To Entrepreneur"></asp:BoundColumn>
                                            <asp:BoundColumn DataField="kitname" HeaderText="Product Name"></asp:BoundColumn>
                                            <asp:BoundColumn DataField="ScratchNo" HeaderText="Scratch No"></asp:BoundColumn>
                                            <asp:BoundColumn DataField="ToDate" HeaderText="Date"></asp:BoundColumn>
                                            <asp:BoundColumn DataField="PinStatus" HeaderText="Status"></asp:BoundColumn>
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
