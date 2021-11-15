<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LogInPDA.aspx.cs" Inherits="FastForward.SCMPDA.LogInPDA" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>

    <%--<link href="Css/bootstrap.css" rel="stylesheet" />--%>
    <%--<link href="Css/style.css?005" rel="stylesheet" />--%>
    <link href="Css/Login.css?0007" rel="stylesheet" />
    <meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=0" />
</head>
<body>
    <div class="log-bdy">
        <form id="form1" runat="server" class="main-login">
            <div>
                <asp:ScriptManager ID="ScriptManager1" runat="server">
                </asp:ScriptManager>

                <asp:UpdatePanel ID="main" runat="server">
                    <ContentTemplate>

                        <div class="col_sm-12" runat="server" id="maindvlogin">

                            <div class="row">
                                <div class="col-sm-12 labelText1">

                                    <div visible="false" class="alert alert-success" role="alert" runat="server" id="divok">

                                        <div class="col-sm-12">
                                            <asp:Label ID="lblok" runat="server"></asp:Label>
                                            <asp:LinkButton ID="lbtnok" runat="server" CausesValidation="false" CssClass="floatright" OnClick="lbtnok_Click">
                                        <span class="glyphicon glyphicon-remove " aria-hidden="true" ></span>
                                            </asp:LinkButton>
                                        </div>

                                    </div>

                                    <div visible="false" class="alert alert-danger" role="alert" runat="server" id="divalert">

                                        <div class="col-sm-12">
                                            <asp:Label ID="lblalert" runat="server"></asp:Label>
                                            <asp:LinkButton ID="lbtnalert" runat="server" CausesValidation="false" CssClass="floatright" OnClick="lbtnalert_Click">
                                        <span class="glyphicon glyphicon-ok" aria-hidden="true"></span>
                                            </asp:LinkButton>
                                        </div>

                                    </div>

                                    <div visible="false" class="alert alert-info" role="alert" runat="server" id="Divinfo">
                                        <div class="col-sm-12">
                                            <asp:Label ID="lblinfo" runat="server"></asp:Label>
                                            <asp:LinkButton ID="lbtninfo" runat="server" CausesValidation="false" CssClass="floatright" OnClick="lbtninfo_Click">
                                        <span class="glyphicon glyphicon-ok" aria-hidden="true"></span>
                                            </asp:LinkButton>
                                        </div>
                                    </div>

                                </div>
                            </div>

                            <div class="panel panel-default mainpnlmargin">
                                <div class="panel-heading defaultpanelheader">
                                    WELCOME TO SCMII BARCODE SYSTEM
                                </div>

                                <div class="panel-body">
                                    <div class="row">
                                        <div class="col-sm-12  login-body">

                                            <div class="col-sm-12">
                                                <div class="row">

                                                    <div class="col-sm-6 labelText1 login-pge-lbl">
                                                        Username
                                                    </div>

                                                    <div class="col-sm-6">
                                                        <asp:TextBox ID="txtuser" runat="server" TabIndex="1" AutoPostBack="true" OnTextChanged="txtuser_TextChanged" CssClass="form-control ControlText login-pge-txt"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-sm-12">
                                                <div class="row">

                                                    <div class="col-sm-6 labelText1  login-pge-lbl">
                                                        Password
                                                    </div>

                                                    <div class="col-sm-6">
                                                        <asp:TextBox ID="txtpw" runat="server" TextMode="Password" TabIndex="2" CssClass="form-control ControlText login-pge-txt"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-sm-12">
                                                <div class="row">

                                                    <div class="col-sm-6 labelText1  login-pge-lbl">
                                                        Company
                                                    </div>

                                                    <div class="col-sm-6">
                                                        <asp:DropDownList ID="ddlcompany" AutoPostBack="true" TabIndex="3" runat="server" CssClass="form-control ControlText login-pge-drop">
                                                            <asp:ListItem Text="--Select--" />
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-sm-12">
                                                <div class="row">

                                                    <div class="col-sm-6 btn-log-cont">
                                                        <asp:Button ID="btnlogin" runat="server" TabIndex="4" CssClass="btn-primary form-control" Text="Login" OnClick="btnlogin_Click" />
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="row">
                                                <div class="col-sm-12">
                                                </div>
                                            </div>
                                        </div>
                                        <div class="labelText1 logincopyrightlabel">© <%= DateTime.Now.Year %> Sirius Technologies Service (Pvt) Ltd </div>


                                    </div>
                                </div>

                            </div>

                        </div>

                    </ContentTemplate>
                </asp:UpdatePanel>

            </div>
        </form>
        <div id="marqueecontainer" align="left" onMouseover="copyspeed=pausespeed" onMouseout="copyspeed=marqueespeed">
        <div id="vmarquee" style="position: absolute; width: 200px;">
        <table width="204" style="font-family: Tahoma; font-size: 10px" border="0" cellspacing="0" cellpadding="0">
            <%--<tr>
        <td align="center"><img src="images/logo.gif" width="150" height="63" /></td>
      </tr>--%>
            <tr>
                <td align="center">&nbsp;</td>
            </tr>
            <tr>
                <td align="center" valign="top"><strong>SCMII BARCODE SYSTEM</strong></td>
            </tr>
            <tr>
                <td align="center" valign="top">
                    <br />
                    Part of Supply Chain Management II<br />
                    Fast Forward<br />
                </td>
            </tr>
            <tr>
                <td align="center" valign="top">&nbsp;</td>
            </tr>
            <tr>
                <td align="center" valign="top">&nbsp;</td>
            </tr>
            <tr>
                <td align="center" valign="top"><strong>Engineering and Production</strong></td>
            </tr>
            <tr>
                <td align="center" valign="top"><strong>Engineering(2016-2017)
                    <br />
                    By</strong></td>
            </tr>
            <tr>
                <td align="center" valign="top">Nuwan De Silva</td>
            </tr>
            <tr>
                <td align="center" valign="top">
                    <br />
                    <br />
                </td>
            </tr>
            <tr>
                <td align="center" valign="top">
                    <p>
                        <strong>SUPPLY CHAIN MANAGEMENT II<br />
                            FAST FORWARD</strong>
                    </p>
                </td>
            </tr>
            <tr>
                <td align="center" valign="top">
                    <br />
                    <br />
                </td>
            </tr>
            <tr>
                <td align="center" valign="top"><strong>Engineering and Production</strong></td>
            </tr>
            <tr>
                <td align="center" valign="top">Darshana Samarathunga<br />
                    Chamal De Silva</td>
            </tr>
            <tr>
                <td align="center" valign="top">
                    <br />
                    <br />
                </td>
            </tr>
            <tr>
                <td align="center" valign="top"><strong>Program Management</strong></td>
            </tr>
            <tr>
                <td align="center" valign="top">Pradeep Wijesinghe </td>
            </tr>
            <tr>
                <td align="center" valign="top">
                    <br />
                    <br />
                </td>
            </tr>
            <tr>
                <td align="center" valign="top">Copyright &copy;2016<br />
                    All rights reserved<br />
                    Sirius Technology Services(Pvt)Ltd<br />
                    (Abans Group)<br />
                    No: 490,<br />
                    5th Floor, Caltex Building,<br />
                    Colombo 3<br />
                    Tele : 011-2301563-64, 011-2565261
                </td>
            </tr>
        </table>
    </div>
    <script src="<%=Request.ApplicationPath%>js/jquery-1.11.3.min.js"></script>
    <%--<script src="<%=Request.ApplicationPath%>js/bootstrap.min.js"></script>--%>
    <script type="text/javascript">

        /***********************************************
        * Cross browser Marquee II- © Dynamic Drive (www.dynamicdrive.com)
        * This notice MUST stay intact for legal use
        * Visit http://www.dynamicdrive.com/ for this script and 100s more.
        ***********************************************/

        var delayb4scroll = 000 //Specify initial delay before marquee starts to scroll on page (2000=2 seconds)
        var marqueespeed = 1 //Specify marquee scroll speed (larger is faster 1-10)
        var pauseit = 0 //Pause marquee onMousever (0=no. 1=yes)?

        ////NO NEED TO EDIT BELOW THIS LINE////////////

        var copyspeed = marqueespeed
        var pausespeed = (pauseit == 0) ? copyspeed : 0
        var actualheight = ''

        function scrollmarquee() {
            if (parseInt(cross_marquee.style.top) > (actualheight * (-1) + 8))
                cross_marquee.style.top = parseInt(cross_marquee.style.top) - copyspeed + "px"
            else
                cross_marquee.style.top = parseInt(marqueeheight) + 8 + "px"
        }

        function initializemarquee() {
            cross_marquee = document.getElementById("vmarquee")
            cross_marquee.style.top = 0
            marqueeheight = document.getElementById("marqueecontainer").offsetHeight
            actualheight = cross_marquee.offsetHeight
            if (window.opera || navigator.userAgent.indexOf("Netscape/7") != -1) { //if Opera or Netscape 7x, add scrollbars to scroll and exit
                cross_marquee.style.height = marqueeheight + "px"
                cross_marquee.style.overflow = "scroll"
                return
            }
            setTimeout('lefttime=setInterval("scrollmarquee()",30)', delayb4scroll)
        }

        if (window.addEventListener)
            window.addEventListener("load", initializemarquee, false)
        else if (window.attachEvent)
            window.attachEvent("onload", initializemarquee)
        else if (document.getElementById)
            window.onload = initializemarquee


    </script>
</body>
</html>
