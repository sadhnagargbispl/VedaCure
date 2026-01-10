<%@ Page Title="" Language="C#" MasterPageFile="~/SitePage.master" AutoEventWireup="true" CodeFile="Rptwithdrawls.aspx.cs" Inherits="Rptwithdrawls" EnableEventValidation="false" %>


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
                            <h5 class="mb-0">Fund Withdrawal Report</h5>
                        </div>
                        <div class="card-body">
                            <div class="row g-1">
                                <div class="clr">
                                    <asp:Label ID="errMsg" runat="server" CssClass="error"></asp:Label>
                                </div>


                                <div class="col-md-12" runat="server" id="DivH" visible="true">
                                     <div class="table-responsive" style="overflow: scroll;">
                                    <asp:DataGrid ID="GrdDirects" runat="server" CssClass="table table-bordered " CellPadding="3"
                                        HorizontalAlign="Center" AutoGenerateColumns="true" AllowPaging="True" Width="100%" OnPageIndexChanged="GrdDirects_PageIndexChanged">
                                        <AlternatingItemStyle CssClass="GridAltItem"></AlternatingItemStyle>
                                        <ItemStyle CssClass="GridItem"></ItemStyle>
                                        <HeaderStyle CssClass="GridHeader"></HeaderStyle>
                                        <PagerStyle CssClass="GridPager"></PagerStyle>
                                        <Columns>
                                           
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
