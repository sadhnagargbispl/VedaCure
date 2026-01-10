<%@ Page Title="" Language="C#" MasterPageFile="~/SitePage.master" AutoEventWireup="true" CodeFile="RefTree.aspx.cs" Inherits="RefTree" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style>
        .buttonBG {
            font-family: tahoma, arial, sans-serif;
            font-weight: bold;
            letter-spacing: 0.5px;
            font-size: 11px;
            color: #333333;
            background-color: #ca8700;
            padding: 5px 10px 5px 10px;
            border: 1px solid #ca8700;
            text-shadow: 0px 1px 1px #fffcf3;
            text-decoration: none;
            text-align: center;
            border-radius: 4px;
            -moz-border-radius: 4px;
            -webkit-border-radius: 4px;
            background-image: url(Images/ButtonBG.jpg);
            cursor: pointer;
        }
    </style>
    <div class="main-content">

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
                                        <table cellpadding="0" cellspacing="0" border="0" class="MainRounded" width="100%"
                                            bgcolor="#ffffff">
                                            <tbody valign="top">
                                                <tr>
                                                    <td style="width: 100%" valign="top">
                                                        <table cellpadding="0" cellspacing="0" width="100%">
                                                            <tr>
                                                                <td align="left" style="padding-left: 10px">
                                                                    <table id="Table1" style="font-size: 10px; left: 0px; color: black; font-family: Verdana; top: 0px"
                                                                        cellspacing="0" cellpadding="4" width="350" border="1">
                                                                        <tbody>
                                                                            <tr valign="top">
                                                                                <td style="width: 50px;font-weight: bolder;">&nbsp;Downline Member&nbsp;ID
                                                                                </td>
                                                                                <td style="width: 40px">
                                                                                    <input class="txtbox1" id="DownLineFormNo" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid; border-bottom: 1px solid; width: 110px;"
                                                                                        type="text"
                                                                                        maxlength="15" name="DownLineFormNo" runat="server" />
                                                                                </td>
                                                                                <td style="width: 30px;font-weight: bolder;">Down Level
                                                                                </td>
                                                                                <td style="width: 40px">
                                                                                    <input class="txtbox2" id="deptlevel" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid; border-bottom: 1px solid; width: 62px;"
                                                                                        type="text" maxlength="4"
                                                                                        name="deptlevel" />
                                                                                </td>
                                                                                <td style="width: 40px">
                                                                                    <asp:Button ID="Button1" runat="server" Text="Search" class="buttonBG" Style="height: 24px; width: 64px;" OnClick="Button1_Click" />
                                                                                </td>
                                                                                <%--<td style="width:40px"><asp:ImageButton ImageUrl="~/Images/reset.jpg" ID="Reset" runat="server" /></td>--%>
                                                                                <td style="width: 40px">
                                                                                    <asp:Button ID="cmdBack" runat="server" Text="Back" class="buttonBG" Style="height: 24px; width: 64px;" OnClick="cmdBack_Click" />
                                                                                </td>
                                                                                <td style="width: 40px">
                                                                                    <asp:Button ID="BtnStepAbove" runat="server" Text="1 Step Above" class="buttonBG"
                                                                                        Style="height: 24px;" OnClick="BtnStepAbove_Click" />
                                                                                </td>
                                                                            </tr>
                                                                        </tbody>
                                                                    </table>
                                                                </td>
                                                            </tr>

                                                            <tr>
                                                                <td align="center" style="width: 100%; height: 380px;" valign="top">
                                                                    <iframe name="TreeFrame" frameborder="0" scrolling="auto" src="Referaltree.aspx"
                                                                        width="100%" height="430" id="TreeFrame" runat="server"></iframe>
                                                                </td>
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

