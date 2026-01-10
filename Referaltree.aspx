<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Referaltree.aspx.cs" Inherits="Referaltree" %>

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
            FILTER: progid:DXImageTransform.Microsoft.Shadow(color=gray,direction=135);
            LEFT: 300px;
            VISIBILITY: hidden;
            PADDING-BOTTOM: 2px;
            BORDER-LEFT: black 1px solid;
            WIDTH: 300px;
            PADDING-TOP: 2px;
            BORDER-BOTTOM: black 1px solid;
            POSITION: absolute;
            BACKGROUND-COLOR: lightyellow;
            margin-left: 140px;
        }

        #dhtmlpointer {
            Z-INDEX: 101;
            LEFT: 300px;
            VISIBILITY: hidden;
            POSITION: absolute;
        }

        .style2 {
            FONT-SIZE: 12px
        }
    </style>
    <link href="dtree/dtree.css" type="text/css" rel="stylesheet" />
    <script src="dtree/dtree.js" type="text/javascript"></script>
    <script src="dtree/Referalvertdtree.js" type="text/javascript"></script>
    <script src="Js/generalEvent.js" type="text/javascript"></script>

    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR" />
    <meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE" />
    <meta content="JavaScript" name="vs_defaultClientScript" />
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema" />
    <script type="text/javascript">

        var offsetfromcursorX = 12 //Customize x offset of tooltip
        var offsetfromcursorY = 10 //Customize y offset of tooltip

        var offsetdivfrompointerX = 10 //Customize x offset of tooltip DIV relative to pointer image
        var offsetdivfrompointerY = 12 //Customize y offset of tooltip DIV relative to pointer image. Tip: Set it to (height_of_pointer_image-1).

        document.write('<div id="dhtmltooltip"></div>') //write out tooltip DIV
        document.write('<img id="dhtmlpointer">') //write out pointer image

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
                if (typeof thecolor != "undefined" && thecolor != "") tipobj.style.backgroundColor = thecolor
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
    <script>
        function goBack() {
            window.history.back()
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
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
        <center>
            <div>
                <div style="vertical-align: top; position: absolute; top: 8px; left: 0px;">
                    <table id="Table1" style="font-size: 10px; left: 0px; color: black; font-family: Verdana; top: 0px"
                        cellspacing="0" cellpadding="4" width="350" border="1">
                        <tbody>
                            <tr valign="top">
                                <td style="width: 50px; font-weight: bolder;">&nbsp;Downline Member&nbsp;ID
                                </td>
                                <td style="width: 40px">
                                    <input class="txtbox1" id="DownLineFormNo" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid; border-bottom: 1px solid; width: 110px;"
                                        type="text"
                                        maxlength="15" name="DownLineFormNo" runat="server" />
                                </td>
                                <td style="width: 30px; font-weight: bolder;">Down Level
                                </td>
                                <td style="width: 40px">
                                    <input class="txtbox2" id="deptlevel" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid; border-bottom: 1px solid; width: 62px;"
                                        type="text" maxlength="4"
                                        name="deptlevel" />
                                </td>
                                <td style="width: 40px">
                                    <asp:Button ID="Button1" runat="server" Text="Search" class="buttonBG" Style="height: 24px; width: 64px;" OnClick="Button1_Click" />
                                </td>
                                <%--      <td style="width: 40px">
                                    <asp:ImageButton ImageUrl="~/Images/reset.jpg" ID="Reset" runat="server" /></td>--%>
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
                    <%-- 
                <table id="Table1" cellpadding="0" cellspacing="1" border="0" width="300px" style="vertical-align: top"
                    runat="server">
                    <tr id="Tr2" runat="server" style="font-weight: bold; font-size: 10px; font-family: Verdana;">
                        <td id="td1" runat="server" style="width: 15%; height: 50Px">
                            <asp:Image ID="Image1" runat="server" Visible="false" />
                        </td>
                        <td id="td2" runat="server" style="width: 15%; height: 50Px">
                            <asp:Image ID="Image2" runat="server" Visible="false" />
                        </td>
                        <td id="td3" runat="server" style="width: 15%; height: 50Px">
                            <asp:Image ID="Image3" runat="server" Visible="false" />
                        </td>
                        <td id="td4" runat="server" style="width: 15%; height: 50Px">
                            <asp:Image ID="Image4" runat="server" Visible="false" />
                        </td>
                        <td id="td5" runat="server" style="width: 15%; height: 50Px">
                            <asp:Image ID="Image5" runat="server" Visible="false" />
                        </td>
                        <td id="td6" runat="server" style="width: 15%; height: 50Px">
                            <asp:Image ID="Image6" runat="server" Visible="false" />
                        </td>
                        <td id="td7" runat="server" style="width: 15%; height: 50Px">
                            <asp:Image ID="Image7" runat="server" Visible="false" />
                        </td>
                    </tr>
                    <tr style="font-weight: bold; font-size: 10px; font-family: Verdana;">
                        <td id="td8" style="width: 15%" align="center" runat="server">
                        </td>
                        <td id="td9" style="width: 15%" align="center" runat="server">
                        </td>
                        <td id="td10" style="width: 15%" align="center" runat="server">
                        </td>
                        <td id="td18" style="width: 15%" align="center" runat="server">
                        </td>
                        <td id="td19" style="width: 15%" align="center" runat="server">
                        </td>
                        <td id="td20" style="width: 15%" align="center" runat="server">
                        </td>
                        <td id="td28" style="width: 15%" align="center" runat="server">
                        </td>
                    </tr>
                </table>
            </div>
            <center>
            </center>
        </div>
    </center>
<%--        <center>
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
                                                 <asp:Button ID="Button1" runat="server" Text="Search" class="buttonBG" Style="height: 24px; width: 64px;"/>
                                             </td>
                                             <%--<td style="width:40px"><asp:ImageButton ImageUrl="~/Images/reset.jpg" ID="Reset" runat="server" /></td>--%>
                    <%-- <td style="width: 40px">
                                                 <asp:Button ID="cmdBack" runat="server" Text="Back" class="buttonBG" Style="height: 24px; width: 64px;"  />
                                             </td>
                                             <td style="width: 40px">
                                                 <asp:Button ID="BtnStepAbove" runat="server" Text="1 Step Above" class="buttonBG"
                                                     Style="height: 24px;"  />
                                             </td>
                                         </tr>
                                     </tbody>
                                 </table>
                             </td>
                         </tr>--%>

                    <%-- <tr>
                             <td align="center" style="width: 100%; height: 380px;" valign="top">
                                 <iframe name="TreeFrame" frameborder="0" scrolling="auto" src="Referaltree.aspx"
                                     width="100%" height="430" id="TreeFrame" runat="server"></iframe>
                             </td>
                         </tr>--%>
                    <%-- </table>

                 </td>
             </tr>
         </tbody>
     </table>--%>
                    <%-- <table id="Table2" cellpadding="0" cellspacing="1" border="0" width="300px" style="vertical-align: top"
                    runat="server">
                    <tr id="Tr1" runat="server" style="font-weight: bold; font-size: 10px; font-family: Verdana;">
                        <td id="td11" runat="server" style="width: 15%; height: 50Px">
                            <asp:Image ID="img11" runat="server" Visible="false" />
                        </td>
                        <td id="td12" runat="server" style="width: 15%; height: 50Px">
                            <asp:Image ID="img12" runat="server" Visible="false" />
                        </td>
                        <td id="td13" runat="server" style="width: 15%; height: 50Px">
                            <asp:Image ID="img13" runat="server" Visible="false" />
                        </td>
                        <td id="td14" runat="server" style="width: 15%; height: 50Px">
                            <asp:Image ID="img14" runat="server" Visible="false" />
                        </td>
                        <td id="td15" runat="server" style="width: 15%; height: 50Px">
                            <asp:Image ID="img15" runat="server" Visible="false" />
                        </td>
                        <td id="td16" runat="server" style="width: 15%; height: 50Px">
                            <asp:Image ID="img16" runat="server" Visible="false" />
                        </td>
                        <td id="td17" runat="server" style="width: 15%; height: 50Px">
                            <asp:Image ID="img17" runat="server" Visible="false" />
                        </td>
                    </tr>
                    <tr style="font-weight: bold; font-size: 10px; font-family: Verdana;">
                        <td id="td21" style="width: 15%" align="center" runat="server">
                        </td>
                        <td id="td22" style="width: 15%" align="center" runat="server">
                        </td>
                        <td id="td23" style="width: 15%" align="center" runat="server">
                        </td>
                        <td id="td24" style="width: 15%" align="center" runat="server">
                        </td>
                        <td id="td25" style="width: 15%" align="center" runat="server">
                        </td>
                        <td id="td26" style="width: 15%" align="center" runat="server">
                        </td>
                        <td id="td27" style="width: 15%" align="center" runat="server">
                        </td>
                    </tr>
                </table>
 </center>--%>
    </form>
</body>
</html>
