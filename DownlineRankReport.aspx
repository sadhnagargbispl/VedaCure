<%@ Page Title="" Language="C#" MasterPageFile="~/SitePage.master" AutoEventWireup="true" CodeFile="DownlineRankReport.aspx.cs" Inherits="DownlineRankReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
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
                           <h5 class="mb-0">Downline Rank Report </h5>
                       </div>
                       <div class="card-body">
                        <div class="clearfix gen-profile-box">
                            <div class="profile-bar clearfix" style="background: #fff;">
                                <div class="clearfix">
                                    <br>
                                    <div class="centered">
                                        <div class="clr">
                                            <asp:Label ID="errMsg" runat="server" CssClass="error"></asp:Label>
                                        </div>

                                        <div class="row align-items-end">


    <div class="col-md-3">
        <label>Search By</label>
        <asp:DropDownList ID="rbtnsearch" runat="server" CssClass="form-control">
            <asp:ListItem Text="Both" Selected="True" Value="L"></asp:ListItem>
            <asp:ListItem Text="Left" Value="1"></asp:ListItem>
            <asp:ListItem Text="Right" Value="2"></asp:ListItem>
        </asp:DropDownList>
    </div>

  
    <div class="col-md-3">
        <label>Rank</label>
        <asp:DropDownList ID="ddlstate" runat="server" CssClass="form-control">
        </asp:DropDownList>
    </div>
 

    <div class="col-md-3">
        <div id="divSearch" runat="server">
            <label>Member ID</label>
            <asp:TextBox ID="txtMemberID" runat="server" CssClass="form-control"   oninput="this.value = this.value.replace(/\s/g, '')"></asp:TextBox>
        </div>
    </div>


    <div class="col-md-3">
        <asp:Button ID="BtnSubmit" runat="server" Text="Search"  OnClick="BtnSearch_Click"
            CssClass="btn btn-primary w-50" />
    </div>

</div>
                                        <br>
                                        <div class="col-md-12">
                                            <div id="DivSideA" runat="server">
                                                <asp:Label ID="Label1" runat="server" Text="Total Records"></asp:Label>
                                                <asp:Label ID="lbltotal" runat="server"></asp:Label>
                                                <div class="table-responsive">
                                                <asp:GridView ID="RptDirects" runat="server" CssClass="table table-striped table-bordered"
                                                    CellPadding="2" HorizontalAlign="Center" AutoGenerateColumns="true" Width="100%"
                                                    EmptyDataText="No Data Display">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="S.No." HeaderStyle-Width="40px">
                                                            <ItemTemplate>
                                                                <%#Container.DataItemIndex + 1%>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                      
                                                    </Columns>
                                                </asp:GridView>
                                                    
                                                </div>
                                            </div>
                                        </div>
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

    <script type="text/javascript" src="assets/jquery.min.js"></script>

    <%--    <script type="text/javascript" src="assets/datatable.css"></script>--%>

    <script type="text/javascript" src="assets/jquery.dataTables.min.js"></script>

    <%--  <script type="text/javascript" src="js/plugins/datatables/jquery.dataTables.min.js"></script>--%>

    <script type="text/javascript" src="assets/tableExport.js"></script>

    <script type="text/javascript">
        var jq = $.noConflict();
        function pageLoad(sender, args) {
            debugger;

            jq(document).ready(function() {
                jq('#customers2').DataTable();

            });
        }


    </script>
</asp:Content>

