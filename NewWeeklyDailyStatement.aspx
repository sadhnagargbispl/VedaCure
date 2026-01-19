<%@ Page Language="C#" AutoEventWireup="true" CodeFile="NewWeeklyDailyStatement.aspx.cs" Inherits="NewWeeklyDailyStatement" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Income Statement</title>
    <link type="text/css" href="Include/basic.css" rel="stylesheet" media="screen" />
    <link href="App_Themes/Thm_Grass/Default.css" type="text/css" rel="stylesheet" />

    <style>
        body {
            font-family: Arial, sans-serif;
            margin: 0;
            padding: 10px;
            background: #f0f0f0;
        }

        .main {
            width: 780px !important; /* ← Sabse perfect size (A4 ke liye best) */
            max-width: 96%;
            margin: 15px auto;
            background: white;
            border: 2.5px solid #333;
            border-radius: 10px;
            overflow: hidden;
            box-shadow: 0 5px 20px rgba(0,0,0,0.15);
            padding: 12px;
        }

        .header-main {
            background: #2c3e50;
            color: white;
            font-weight: bold;
            text-align: center;
            height: 34px;
            font-size: 16px;
            padding: 8px 0;
        }

        .sub-header {
            background: #6b7971;
            color: #ffffff;
            font-weight: bold;
            text-align: center;
            height: 26px;
            font-size: 13.5px;
            padding: 4px 0;
        }

        table {
            border-collapse: collapse;
            width: 100%;
            margin-bottom: 8px;
        }

        td {
            border: 1px solid #999;
            padding: 4px 7px;
            font-size: 12.8px;
        }

        .label {
            width: 110px;
            background: #f8f9fa;
            font-weight: bold;
            font-size: 12.8px;
        }

        .text-right {
            text-align: right;
        }

        .bold {
            font-weight: bold;
        }

        /* Logo Perfect Size */
        .logo {
            width: 135px !important;
            margin: 10px auto !important;
            display: block !important;
        }

        /* Back & Print Links */
        .print-link {
            text-align: right;
            padding: 5px 10px;
            background: white;
            font-size: 13px;
        }

            .print-link a {
                color: blue;
                text-decoration: underline;
                margin-left: 15px;
                font-weight: bold;
            }

        /* Income Details – Super Compact */
        .income-title {
            background: #2c3e50 !important;
            color: white;
            font-size: 16px;
            height: 36px;
            text-align: center;
        }

        .earn-head, .deduct-head {
            background: #6b7971 !important;
            color: white;
            font-weight: bold;
            font-size: 13.5px;
            height: 30px;
        }

        .total-earn {
            background: #6b7971 !important;
            font-weight: bold;
            font-size: 14px;
            color: white;
            height: 34px;
        }

        .net-payable {
            background: #2c3e50 !important;
            color: white;
            font-weight: bold;
            font-size: 18px;
            height: 48px;
        }

            .net-payable td:last-child {
                font-size: 24px !important;
            }

        .payable-row td {
            padding: 3px 7px;
            font-size: 12.8px;
        }

        /* Signature */
        div[style*="text-align: right"] {
            padding: 10px 30px 5px 0 !important;
        }

        /* Print mein bhi perfect */
        @media print {
            body {
                padding: 0;
                background: white;
            }

            .main {
                width: 100% !important;
                border: 1px solid #000;
                box-shadow: none;
                margin: 0;
                padding: 8px;
            }

            .no-print {
                display: none;
            }
        }

        /* Mobile Responsive */
        @media (max-width: 800px) {
            .main {
                width: 98% !important;
                padding: 8px;
            }

            td {
                font-size: 12px;
                padding: 3px 5px;
            }

            .logo {
                width: 110px !important;
            }
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="main">

            <!-- Logo -->
            <div style="text-align: center;">
                <img src="images/LOGO/logo.png" class="logo" width="200px" />
            </div>

            <!-- Print -->
            <div class="print-link no-print">
                <a href="index.aspx">Back</a>&nbsp;&nbsp;&nbsp;
                <a href="javascript:window.print()">Print</a>

            </div>

            <!-- Title -->
            <table>
                <tr>
                    <td class="header-main">Statement</td>
                </tr>
            </table>

            <!-- Distributor + Payout -->
            <table cellpadding="0" cellspacing="0">
                <tr>
                    <td width="50%" class="sub-header">Distributor Detail</td>
                    <td width="50%" class="sub-header">Payout Detail</td>
                </tr>
                <tr>
                    <td valign="top" style="padding: 10px;">
                        <table cellpadding="5" cellspacing="0" width="100%">
                            <tr>
                                <td class="label">Name</td>
                                <td>
                                    <div id="MemName" runat="server"></div>
                                </td>
                            </tr>
                            <tr>
                                <td class="label">ID</td>
                                <td>
                                    <div id="IDNO" runat="server"></div>
                                </td>
                            </tr>
                            <tr>
                                <td class="label">Address</td>
                                <td>
                                    <div id="Add" runat="server"></div>
                                </td>
                            </tr>
                            <tr>
                                <td class="label">Mob. No.</td>
                                <td>
                                    <div id="Mobile" runat="server"></div>
                                </td>
                            </tr>
                            <tr>
                                <td class="label">City</td>
                                <td>
                                    <div id="City" runat="server"></div>
                                </td>
                            </tr>
                            <tr>
                                <td class="label">District</td>
                                <td>
                                    <div id="District" runat="server"></div>
                                </td>
                            </tr>
                            <tr>
                                <td class="label">Pin Code</td>
                                <td>
                                    <div id="PinCode" runat="server"></div>
                                </td>
                            </tr>
                            <tr>
                                <td class="label">State</td>
                                <td>
                                    <div id="State" runat="server"></div>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td valign="top" style="padding: 10px;">
                        <table cellpadding="5" cellspacing="0" width="100%">
                            <tr>
                                <td class="label">Period</td>
                                <td>
                                    <div id="FromDate" runat="server"></div>
                                    To
                                    <div id="ToDate" runat="server"></div>
                                </td>
                            </tr>
                            <tr>
                                <td class="label">Rank</td>
                                <td>
                                    <div id="rank" runat="server"></div>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>

            <br />

            <!-- Super Attractive Income Details -->
            <table cellpadding="0" cellspacing="0">
                <tr>
                    <td colspan="4" class="header-main">Income Details</td>
                </tr>
                <tr>
                    <td width="35%" class="earn-head">Earnings</td>
                    <td width="15%" class="earn-head">Amount (Rs.)</td>
                    <td width="35%" class="deduct-head">Deductions</td>
                    <td width="15%" class="deduct-head">Amount (Rs.)</td>
                </tr>

                <!-- Income Rows -->
                <tr>
                    <td>Generation Income from first purchase</td>
                    <td class="text-right bold">
                        <div id="generationinc" runat="server">&nbsp;</div>
                    </td>
                    <td>TDS</td>
                    <td class="text-right bold">
                        <div id="tdsAmount" runat="server"></div>
                    </td>
                </tr>
                <tr>
                    <td>Global pool income From first purchase</td>
                    <td class="text-right">
                        <div id="globalpool" runat="server">&nbsp;</div>
                    </td>
                    <td>Administration Charges</td>
                    <td class="text-right">
                        <div id="AdminCharges" runat="server"></div>
                    </td>
                </tr>
                <tr style="display: none;">
                    <td>Generation Bonus</td>
                    <td class="text-right">
                        <div id="generationbonus" runat="server">&nbsp;</div>
                    </td>
                    <td rowspan="4"></td>
                    <td rowspan="4"></td>
                </tr>
                <tr>
                    <td>Repurchase Matching Income</td>
                    <td class="text-right">
                        <div id="RePairIncome" runat="server">&nbsp;</div>
                    </td>
                </tr>
                <tr>
                    <td>Star Growth Income</td>
                    <td class="text-right">
                        <div id="GrowthIncome" runat="server">&nbsp;</div>
                    </td>
                </tr>
                <tr style="display: none;">
                    <td>Ambassador Fund Income (CTO)</td>
                    <td class="text-right">
                        <div id="AmbassadorFund" runat="server">&nbsp;</div>
                    </td>
                </tr>

                <!-- Total -->
                <tr class="total-earn">
                    <td><strong>Total Earnings</strong></td>
                    <td class="text-right"><strong>
                        <div id="NetIncome" runat="server"></div>
                    </strong></td>
                    <td><strong>Total Deduction</strong></td>
                    <td class="text-right"><strong>
                        <div id="Deduction" runat="server"></div>
                    </strong></td>
                </tr>

                <!-- Payable Section -->
                <tr class="payable-row">
                    <td colspan="2"></td>
                    <td style="padding-left: 15px;">Amount Payable<br>
                        Brought Forward<br>
                        Total Amount Payable<br>
                        Carried Forward
                    </td>
                    <td class="text-right bold">
                        <div id="Payable" runat="server"></div>
                        <br>
                        <div id="PrevBal" runat="server"></div>
                        <br>
                        <div id="TotPayable" runat="server"></div>
                        <br>
                        <div id="ClsBal" runat="server"></div>
                    </td>
                </tr>

                <!-- Net Payable - Most Attractive -->
                <tr class="net-payable">
                    <td colspan="3" style="text-align: center; font-size: 20px;">Net Payable Amount (Rs.)</td>
                    <td class="text-right" style="font-size: 26px;">
                        <div id="ChqAmount" runat="server"></div>
                    </td>
                </tr>
            </table>
            <div runat="server" id="DivStarGrowthIncome" visible="false">
                <table class="table table-bordered " cellspacing="0" cellpadding="3" align="Center" rules="all" border="1" style="width: 100%; border-collapse: collapse;">
                    <tr>
                        <td colspan="4" class="header-main">Star Growth Income</td>
                    </tr>
                    <tr class="GridHeader">
                        <td width="35%" class="earn-head">Rank</td>
                        <td width="35%" class="earn-head">Slab</td>
                        <td width="35%" class="earn-head">Business</td>
                        <td width="35%" class="earn-head">Income</td>
                    </tr>
                    <asp:Repeater ID="RptDirects" runat="server">
                        <ItemTemplate>
                            <tr class="GridItem">
                                <td><%# Eval("Rank") %></td>
                                <td><%# Eval("Slab") %></td>
                                <td><%# Eval("Business") %></td>
                                <td><%# Eval("Income") %></td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </table>
            </div>
            <div runat="server" id="DivGlobalPoolIncome" visible="false">
                <table class="table table-bordered " cellspacing="0" cellpadding="3" align="Center" rules="all" border="1" style="width: 100%; border-collapse: collapse;">
                    <tr>
                        <td colspan="4" class="header-main">Global Pool Income</td>
                    </tr>
                    <tr class="GridHeader">
                        <td width="35%" class="earn-head">Company Turnover</td>
                        <td width="35%" class="earn-head">No. of Achiever</td>
                        <td width="35%" class="earn-head">Global Pool Income</td>
                    </tr>
                    <tr class="GridItem">
                        <td>
                            <div id="DivMonthlySelfLeftBV" runat="server"></div>
                        </td>
                        <td>
                            <div id="Divnoofachievers" runat="server"></div>
                        </td>
                        <td>
                            <div id="Divincome" runat="server"></div>
                        </td>
                    </tr>
                </table>
            </div>
            <div runat="server" id="DivGenerationIncome" visible="false">
                <table class="table table-bordered " cellspacing="0" cellpadding="3" align="Center" rules="all" border="1" style="width: 100%; border-collapse: collapse;">
                    <tr>
                        <td colspan="4" class="header-main">Generation Income</td>
                    </tr>
                    <tr class="GridHeader">
                        <td width="35%" class="earn-head">Level</td>
                        <td width="35%" class="earn-head">From ID</td>
                        <td width="35%" class="earn-head">From Name</td>
                        <td width="35%" class="earn-head">Income</td>
                    </tr>
                    <asp:Repeater ID="RptGenerationIncome" runat="server">
                        <ItemTemplate>
                            <tr class="GridItem">
                                <td><%# Eval("Mlevel") %></td>
                                <td><%# Eval("idno") %></td>
                                <td><%# Eval("memfirstname") %></td>
                                <td><%# Eval("Comm") %></td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </table>
            </div>
            <!-- Signature -->
            <div style="text-align: right; padding: 0 40px 40px 0;">
                <div style="width: 320px; border-top: 4px solid #000; height: 10px;"></div>
                <div style="text-align: center; font-weight: bold; font-size: 15px; margin-top: 8px;">Authorised Signatory</div>
            </div>

            <!-- सब IDs बिल्कुल पहले जैसे हैं -->
            <div style="display: none;">
                <div id="PairIncome" runat="server"></div>
                <div id="PairIncentive" runat="server"></div>
                <div id="BTFund" runat="server"></div>
                <div id="BikeFund" runat="server"></div>
                <div id="LSTFund" runat="server"></div>
                <div id="RepurchaseFund" runat="server"></div>
                <div id="rankroyalty" runat="server"></div>
                <asp:GridView ID="GvData" runat="server"></asp:GridView>
                <asp:GridView ID="GrdLevel" runat="server"></asp:GridView>
                <asp:GridView ID="GrdMachigbv" runat="server"></asp:GridView>
                <asp:GridView ID="GrdIncentive" runat="server"></asp:GridView>
            </div>

        </div>
    </form>
</body>
</html>
