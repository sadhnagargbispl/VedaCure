<%@ Page Title="" Language="C#" MasterPageFile="~/SitePage.master" AutoEventWireup="true"
    CodeFile="welcome.aspx.cs" Inherits="welcome" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <!-- BOOTSTRAP & FONTS -->
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/4.3.1/css/bootstrap.min.css" />
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.7.0/css/font-awesome.min.css">

    <!-- PRINT SCRIPT -->
    <script>
        function PrintDiv() {
            var printContent = document.getElementById('dvContents').innerHTML;
            var WinPrint = window.open('', '', 'width=900,height=700');
            WinPrint.document.write('<html><head><title>Welcome Letter</title>');

            WinPrint.document.write('<style>');
            WinPrint.document.write(`
                body { font-family: Segoe UI, Arial; font-size: 14px; }
                table { width: 100%; border-collapse: collapse; }
                td, th { border: 1px solid #777; padding: 8px; }
                h3 { color: #b8860b; text-align:center; }
                .noprint { display:none !important; }
            `);

            WinPrint.document.write('</style></head><body>');
            WinPrint.document.write(printContent);
            WinPrint.document.write('</body></html>');
            WinPrint.document.close();
            WinPrint.focus();
            WinPrint.print();
            WinPrint.close();
            return false;
        }
    </script>

    <!-- MAIN PAGE CSS -->
    <style>
        #dvContents {
            background: #fff;
            padding: 35px;
            border-radius: 10px;
            box-shadow: 0 0 10px #ccc;
            max-width: 900px;
            margin: auto;
        }

        h4.wel_ll {
            text-transform: uppercase;
            font-weight: bold;
        }

        .welcome-text {
            font-size: 14px; /* Medium professional font */
            line-height: 1.7; /* Best readability */
            color: #333; /* Clean text color */
            text-align: justify;
        }

            .welcome-text strong {
                color: #B8860B !important; /* Premium Gold color */
                font-weight: 700 !important; /* Strong bold */
            }

        a {
            color: #ffffff;
            text-decoration: none;
            background-color: transparent;
        }

        .btn-warning {
            color: #ffffff;
            background-color: #ffc107;
            border-color: #ffc107;
        }

        .table:not(.table-sm):not(.table-md):not(.dataTable) td, .table:not(.table-sm):not(.table-md):not(.dataTable) th {
            padding: 0 10px;
            height: 39px;
            vertical-align: middle;
        }
    </style>

</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="main-content">

        <section class="section">
            <div class="row">
                <div class="col-12">

                    <!-- CARD -->
                    <div class="card">
                        <%--    <div class="card-header">
                            <h5><i class="fa fa-trophy"></i>Welcome</h5>
                        </div>--%>

                        <div class="card-body">

                            <!-- BUTTONS -->
                            <div class="mb-2 text-center  noprint">
                                <button type="button" class="btn btn-info btn-xs"><a href="index.aspx">Home</a></button>
                                <button type="button" class="btn btn-warning btn-xs" onclick="PrintDiv()">Print</button>
                                <button type="button" class="btn btn-danger btn-xs"><a href="newJoining.aspx">New Joining</a></button>
                                <%--      <button type="button" class="btn btn-danger btn-xs">Purchase Product</button>
                                <button type="button" class="btn btn-danger btn-xs btn_welcome">Continue Shopping</button>--%>
                            </div>

                            <!-- PRINT AREA -->
                            <div id="dvContents">

                                <!-- LOGO & TITLE -->
                                <div class="row">
                                    <div class="col-md-3">
                                        <img src="Img/Logo/logo.png" class="img-fluid" style="max-height: 90px;" />
                                    </div>
                                    <div class="col-md-9 text-center">
                                        <h4 class="wel_ll text-center">Welcome Letter</h4>
                                    </div>
                                </div>

                                <br />



                                <div class="table-responsive">
                                    <!-- DETAILS TABLE -->
                                    <table class="table table-bordered mt-3">
                                        <tr>
                                            <td><b>ID No</b></td>
                                            <td>
                                                <asp:Label ID="LblIdno" runat="server" CssClass="text-danger font-weight-bold"></asp:Label></td>
                                            <td><b>Name</b></td>
                                            <td>
                                                <asp:Label ID="LblName" runat="server" CssClass="text-danger font-weight-bold"></asp:Label></td>


                                        </tr>

                                        <tr>
                                            <td><b>Address</b></td>
                                            <td>
                                                <asp:Label ID="LblAddress" runat="server"></asp:Label></td>

                                            <td><b>City</b></td>
                                            <td>
                                                <asp:Label ID="LblCity" runat="server"></asp:Label></td>
                                        </tr>

                                        <tr>
                                            <td><b>PinCode</b></td>
                                            <td>
                                                <asp:Label ID="Lblpincode" runat="server"></asp:Label></td>
                                            <td><b>Date Of Joining</b></td>
                                            <td>
                                                <asp:Label ID="lblDoj" runat="server"></asp:Label></td>

                                        </tr>
                                        <tr>
                                            <td><b>Sponsor ID</b></td>
                                            <td>
                                                <asp:Label ID="LblPlacementid" runat="server"></asp:Label></td>

                                            <td><b>Sponsor Name</b></td>
                                            <td>
                                                <asp:Label ID="LblPlacementName" runat="server"></asp:Label></td>
                                        </tr>

                                        <tr>
                                            <td><b>Joining BV</b></td>
                                            <td>
                                                <asp:Label ID="Lblkitbv" runat="server"></asp:Label></td>
                                            <td><b>Joining Type</b></td>
                                            <td>
                                                <asp:Label ID="Lbljoiningtype" runat="server"></asp:Label></td>

                                        </tr>
                                    </table>
                                    <!-- GREETING -->
                                    <h6 class="text-danger"><strong><em>Dear Distributor,</em></strong></h6>

                                    <div class="welcome-text">

                                        <p><strong>Congratulation!!</strong> On your decision to soar sky high with us.</p>

                                        <p>
                                            You are now a part of the opportunity of the millennium.
                                        <strong>ORGANOZ</strong> — an exciting people business. 
A business that has the potential to turn your dreams into reality. 
As you build your business, you will establish lifelong friendships and develop 
a support system unparalleled in any other business.
                                        </p>

                                        <p>
                                            <strong>ORGANOZ</strong> is here to H.E.L.P. 
(High Energy Level Participation) you to be successful.  
We pledge our best effort to provide the continuing support necessary 
to help make your business a total success.
                                        </p>

                                        <p>
                                            The bottom line of <strong>ORGANOZ</strong> — 
When you network with us, we all stand together to win and ensure  
that we positively impact hundreds and thousands of lives 
by spreading the total success attitude.
                                        </p>

                                        <p>
                                            We are confident that you will gain tremendous satisfaction from your involvement 
with <strong>ORGANOZ</strong>, and we wish you every success!
                                        </p>

                                        <p>
                                            Keep it up! See you at the top!
                                        </p>

                                        <p>
                                            <strong>Winning Regards,<br />
                                                ORGANOZ</strong>
                                        </p>

                                        <%-- <h5>Enrollment Details</h5>--%>
                                    </div>
                                    <%--    <br />--%>

                                    <!-- SIGNATURE -->
                                    <%--    <p class="text-dark">
                                    <strong>CMD</strong><br />
                                    <strong><%= Session["CompName"] %></strong>
                                </p>--%>
                                </div>
                            </div>
                        </div>

                    </div>

                </div>
            </div>
        </section>

    </div>

</asp:Content>
