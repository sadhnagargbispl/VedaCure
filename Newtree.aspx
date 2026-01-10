<%@ Page Language="C#" AutoEventWireup="true" CodeFile="NewTree.aspx.cs" Inherits="NewTree" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>NewTree</title>
    <link href="css/tree.css" type="text/css" rel="stylesheet" />
    <style type="text/css">
        #dhtmltooltip {
            BORDER-RIGHT: black 1px solid;
            PADDING-RIGHT: 2px;
            BORDER-TOP: black 1px solid;
            PADDING-LEFT: 2px;
            Z-INDEX: 100;
            LEFT: -300px;
            VISIBILITY: hidden;
            PADDING-BOTTOM: 2px;
            BORDER-LEFT: black 1px solid;
            WIDTH: 150px;
            PADDING-TOP: 2px;
            BORDER-BOTTOM: black 1px solid;
            POSITION: absolute;
            BACKGROUND-COLOR: Yellow
        }

        #dhtmlpointer {
            Z-INDEX: 101;
            LEFT: -300px;
            VISIBILITY: hidden;
            POSITION: absolute
        }
    </style>
    <link href="dtree/dtree.css" type="text/css" rel="stylesheet" />
    <script src="dtree/dtree.js" type="text/javascript"></script>
    <script src="dtree/vertdtree.js" type="text/javascript"></script>
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR" />
    <meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE" />
    <meta content="JavaScript" name="vs_defaultClientScript" />
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema" />
    <script type="text/javascript">

        var offsetfromcursorX = 22 //Customize x offset of tooltip
        var offsetfromcursorY = 20 //Customize y offset of tooltip

        var offsetdivfrompointerX = 22 //Customize x offset of tooltip div relative to pointer image
        var offsetdivfrompointerY = 24 //Customize y offset of tooltip div relative to pointer image. Tip: Set it to (height_of_pointer_image-1).

        document.write('<div id="dhtmltooltip"></div>') //write out tooltip div
        document.write('<img id="dhtmlpointer" >') //write out pointer image

        var ie = document.all
        var ns6 = document.getElementById && !document.all
        var enabletip = false
        if (ie || ns6)
            var tipobj = document.all ? document.all["dhtmltooltip"] : document.getElementById ? document.getElementById("dhtmltooltip") : ""

        var pointerobj = document.all ? document.all["dhtmlpointer"] : document.getElementById ? document.getElementById("dhtmlpointer") : ""

        function ietruebody() {
            return (document.compatMode && document.compatMode != "BackCompat") ? document.documentElement : document.body
        }

        function ddrivetip(thetext, thewidth, thecolor) {
            if (ns6 || ie) {
                if (typeof thewidth != "undefined") tipobj.style.width = thewidth + "px"
                if (typeof thecolor != "undefined" && thecolor != "")
                    tipobj.style.backgroundColor = thecolor
                else
                    tipobj.style.backgroundColor = "black"
                tipobj.innerHTML = thetext
                enabletip = true
                return false
            }
        }

        function positiontip(e) {
            if (enabletip) {
                var nondefaultpos = false
                var curX = (ns6) ? e.pageX : event.clientX + ietruebody().scrollLeft;
                var curY = (ns6) ? e.pageY : event.clientY + ietruebody().scrollTop;
                //Find out how close the mouse is to the corner of the window
                var winwidth = ie && !window.opera ? ietruebody().clientWidth : window.innerWidth - 20
                var winheight = ie && !window.opera ? ietruebody().clientHeight : window.innerHeight - 20

                var rightedge = ie && !window.opera ? winwidth - event.clientX - offsetfromcursorX : winwidth - e.clientX - offsetfromcursorX
                var bottomedge = ie && !window.opera ? winheight - event.clientY - offsetfromcursorY : winheight - e.clientY - offsetfromcursorY

                var leftedge = (offsetfromcursorX < 0) ? offsetfromcursorX * (-1) : -1000

                //if the horizontal distance isn't enough to accomodate the width of the context menu
                if (rightedge < tipobj.offsetWidth) {
                    //move the horizontal position of the menu to the left by it's width
                    tipobj.style.left = curX - tipobj.offsetWidth + "px"
                    nondefaultpos = true
                }
                else if (curX < leftedge)
                    tipobj.style.left = "5px"
                else {
                    //position the horizontal position of the menu where the mouse is positioned
                    tipobj.style.left = curX + offsetfromcursorX - offsetdivfrompointerX + "px"
                    pointerobj.style.left = curX + offsetfromcursorX + "px"
                }

                //same concept with the vertical position
                if (bottomedge < tipobj.offsetHeight) {
                    tipobj.style.top = curY - tipobj.offsetHeight - offsetfromcursorY + "px"
                    nondefaultpos = true
                }
                else {
                    tipobj.style.top = curY + offsetfromcursorY + offsetdivfrompointerY + "px"
                    pointerobj.style.top = curY + offsetfromcursorY + "px"
                }
                tipobj.style.visibility = "visible"
                if (!nondefaultpos)
                    pointerobj.style.visibility = "visible"
                else
                    pointerobj.style.visibility = "hidden"
            }
        }

        function hideddrivetip() {
            if (ns6 || ie) {
                enabletip = false
                tipobj.style.visibility = "hidden"
                pointerobj.style.visibility = "hidden"
                tipobj.style.left = "-1000px"
                tipobj.style.backgroundColor = ''
                tipobj.style.width = ''
            }
        }

        document.onmousemove = positiontip



    </script>
</head>
<body>
    <form id="form1" runat="server">
        <center>
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
            <div>
                <div style="vertical-align: top; position: absolute; top: 8px; left: 0px;">
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
                <asp:Button ID="BtnStepAbove" runat="server" Text="1 Step Above" class="submit-btn"
                    Style="height: 24px; width: 92px;" OnClick="BtnStepabove_Click" />
            </td>
        </tr>
    </tbody>
</table>
                    <table id="Table1" cellpadding="0" cellspacing="1" border="0" width="300px" style="vertical-align: top; padding-left: 10px"
                        runat="server">
                        <tr id="Tr1" runat="server" style="font-weight: bold; font-size: 10px; font-family: Verdana;">
                            <td id="td11" runat="server" style="width: 15%; height: 50Px">
                                <asp:Image ID="img11" runat="server" Height="55px" Width="55px" Visible="false" />
                            </td>
                            <td id="td12" runat="server" style="width: 15%; height: 50Px">
                                <asp:Image ID="img12" runat="server" Height="55px" Width="55px" Visible="false" />
                            </td>
                            <td id="td13" runat="server" style="width: 15%; height: 50Px">
                                <asp:Image ID="img13" runat="server" Height="55px" Width="55px" Visible="false" />
                            </td>
                            <td id="td14" runat="server" style="width: 15%; height: 50Px">
                                <asp:Image ID="img14" runat="server" Height="55px" Width="55px" Visible="false" />
                            </td>
                            <td id="td15" runat="server" style="width: 15%; height: 50Px">
                                <asp:Image ID="img15" runat="server" Height="55px" Width="55px" Visible="false" />
                            </td>
                            <td id="td16" runat="server" style="width: 15%; height: 50Px">
                                <asp:Image ID="img16" runat="server" Height="55px" Width="55px" Visible="false" />
                            </td>
                            <td id="td17" runat="server" style="width: 15%; height: 50Px">
                                <asp:Image ID="img17" runat="server" Height="55px" Width="55px" Visible="false" />
                            </td>
                            <td id="td18" runat="server" style="width: 15%; height: 50Px">
                                <asp:Image ID="img18" runat="server" Height="55px" Width="55px" Visible="false" />
                            </td>
                        </tr>
                        <tr style="font-weight: bold; font-size: 10px; font-family: Verdana;">
                            <td id="td21" style="width: 15%" align="center" runat="server"></td>
                            <td id="td22" style="width: 15%" align="center" runat="server"></td>
                            <td id="td23" style="width: 15%" align="center" runat="server"></td>
                            <td id="td24" style="width: 15%" align="center" runat="server"></td>
                            <td id="td25" style="width: 15%" align="center" runat="server"></td>
                            <td id="td26" style="width: 15%" align="center" runat="server"></td>
                            <td id="td27" style="width: 15%" align="center" runat="server"></td>
                            <td id="td28" style="width: 15%" align="center" runat="server"></td>
                        </tr>
                    </table>
                </div>
                <center>
                </center>
            </div>
        </center>
        <%--   <center>
         
    
             <div style="vertical-align:top; position: absolute; top: 8px; left: 0px;">
             <div style="width:150px" >
        
            </div>       
                 
		    </div>
		</center> --%>
    </form>
</body>
</html>
