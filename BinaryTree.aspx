<%@ Page Title="" Language="C#" MasterPageFile="~/SitePage.master" AutoEventWireup="true" CodeFile="BinaryTree.aspx.cs" Inherits="BinaryTree" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="main-content">
        <style>
            .submit-btn {
                background-color: #436cac;
                width: 83px;
                height: 25px;
                border: none;
                color: #deeafb;
                font: normal 12px Tahoma, Geneva, sans-serif;
            }
        </style>
        <section class="section">
            <ul class="breadcrumb breadcrumb-style ">
            </ul>
            <div class="row">
                <div class="col-12 col-sm-12 col-lg-12">
                    <div class="card">

                        <div class="card-body">
                            <div class="row g-1">
                                <div class="table-responsive" style="overflow: scroll;">
                                    <center>
                                        <table cellpadding="0" cellspacing="0" border="0" class="MainRounded" width="100%">
                                            <tbody valign="top">
                                                <tr>
                                                    <td style="width: 73%" valign="top">
                                                        <table cellpadding="0" cellspacing="0" width="100%">
                                                            <tr valign="top">
                                                                <td align="left">
                                                                    <table id="Table1" style="font-size: 10px; left: 0px; color: black; font-family: Verdana; top: 0px"
                                                                        cellspacing="0" cellpadding="4" border="1">
                                                                        <tbody>
                                                                            <tr valign="top">
                                                                                <td style="font-weight: bolder;">Downline Member Id
                                                                                </td>
                                                                                <td>
                                                                                    <input class="form-input-2" id="DownLineFormNo" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid; border-bottom: 1px solid; width: 110px;"
                                                                                        type="text"
                                                                                        maxlength="15" name="DownLineFormNo" runat="server" />
                                                                                    <asp:HiddenField ID="hfFormNo" runat="server" />
                                                                                    <asp:TextBox ID="TxtSrchByName" runat="server" Visible="false" Width="200px"></asp:TextBox>
                                                                                </td>
                                                                                <td style="font-weight: bolder;">Down Level
                                                                                </td>
                                                                                <td>
                                                                                    <input class="form-input-2" id="deptlevel" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid; border-bottom: 1px solid; width: 62px;"
                                                                                        type="text" maxlength="4"
                                                                                        name="deptlevel" />
                                                                                </td>
                                                                                <td style="width: 40px">
                                                                                    <asp:Button ID="Button1" runat="server" Text="Search" CssClass="submit-btn" Style="height: 24px; width: 64px;" OnClick="Button1_Click" />
                                                                                </td>
                                                                                <td style="width: 40px">
                                                                                    <asp:Button ID="cmdBack" runat="server" Text="Back" class="submit-btn" Style="height: 24px; width: 84px;" OnClick="cmdBack_Click" />
                                                                                </td>
                                                                                <td style="width: 40px">
                                                                                    <asp:Button ID="BtnStepabove" runat="server" Text="1 Step Above" class="submit-btn"
                                                                                        Style="height: 24px; width: 92px;" OnClick="BtnStepabove_Click" />
                                                                                </td>
                                                                            </tr>
                                                                        </tbody>
                                                                    </table>
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="lblLevl" runat="server" Visible="false"></asp:Label>
                                                                </td>
                                                            </tr>

                                                            <tr>
                                                                <td align="center" style="width: 100%; height: 380px;" valign="top">
                                                                    <iframe name="TreeFrame" frameborder="0" scrolling="auto" src="NewTree.aspx"
                                                                        width="100%" height="430" id="TreeFrame" runat="server"></iframe>
                                                                </td>
                                                                <td></td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </center>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </section>
    </div>
</asp:Content>

