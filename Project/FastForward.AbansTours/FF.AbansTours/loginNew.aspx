<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="loginNew.aspx.cs" Inherits="FF.AbansTours.loginNew" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <link rel="shortcut icon" href="images/webIcon.png">
    <link href='http://fonts.googleapis.com/css?family=Open+Sans:400,300,600,400italic,700,800'
        rel='stylesheet' type='text/css'>
    <link href='http://fonts.googleapis.com/css?family=Raleway:300,200,100' rel='stylesheet'
        type='text/css'>
    <!-- Bootstrap core CSS -->
    <link href="js/bootstrap/dist/css/bootstrap.css" rel="stylesheet">
    <link rel="stylesheet" href="fonts/font-awesome-4/css/font-awesome.min.css">
    <!-- HTML5 shim and Respond.js IE8 support of HTML5 elements and media queries -->
    <!--[if lt IE 9]>
	  <script src="../../assets/js/html5shiv.js"></script>
	  <script src="../../assets/js/respond.min.js"></script>
	<![endif]-->
    <!-- Custom styles for this template -->
    <link href="css/style.css" rel="stylesheet" />
    <style type="text/css">
        .auto-style1
        {
            color: #FF0000;
        }
        
        .btn
        {
            -webkit-border-radius: 11;
            -moz-border-radius: 11;
            border-radius: 8px;
            font-family: Arial;
            color: #ffffff;
            font-size: 15px;
            background: #8e42a1;
            padding: 7px 7px 7px 7px;
            text-decoration: none;
        }
    </style>
</head>
<body class="texture">
    <script type="text/javascript">
        function validate(e) { var keycode = (e.which) ? e.which : e.keyCode; if (e.keyCode == 13) { alert("Enter Key"); return false; } }
    </script>
    <script type="text/javascript">
        function testForEnter() {
            if (event.keyCode == 13) {
                event.cancelBubble = true;
                event.returnValue = false;
                alert("test");
            }
        }
    </script>
    <div id="cl-wrapper" class="login-container">
        <div class="middle-login">
            <div class="block-flat">
                <div class="header">
                    <h3 class="text-center">
                        <img src="images/logoATous.JPG" alt="Total Trainee / Total Allowed" /></h3>
                </div>
                <div>
                    <form id="form1" runat="server">
                    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true" AsyncPostBackTimeout="600">
                    </asp:ScriptManager>
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <div id="ledger_value" class="content">
                                <h5 class="title">
                                    Login Access</h5>
                                <h5 class="title">
                                    User Name<asp:TextBox ID="txtUserName" runat="server" Height="23px" Width="369px"
                                        OnTextChanged="txtUserName_TextChanged" AutoCompleteType="Disabled"></asp:TextBox>
                                </h5>
                                <div class="form-group">
                                    <h5 class="title">
                                        Password<asp:TextBox ID="txtPassword" runat="server" Height="23px" TextMode="Password"
                                            Width="369px"></asp:TextBox>
                                    </h5>
                                    <h5 class="title">
                                        Company
                                        <asp:DropDownList ID="ddlCompany" runat="server" Height="23px" Width="369px">
                                        </asp:DropDownList>
                                    </h5>
                                </div>
                                <div class="form-group">
                                    <asp:Label ID="lblmsg" runat="server" CssClass="auto-style1" Text="Label"></asp:Label>
                                </div>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <div class="foot">
                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                            <ContentTemplate>
                                <asp:Button ID="btnLogin" runat="server" CssClass="btn" Height="28px" Text="Login"
                                    Width="150px" OnClick="btnLogin_Click" />
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                    </form>
                </div>
            </div>
            <div class="text-center out-links" style="text-align: center">
                <a href="#">&copy; 2015 Sirius Technologies Service (Pvt) Ltd</a>
            </div>
        </div>
    </div>
    <script src="js/jquery.js"></script>
    <script type="text/javascript">
        $(function () {
            $("#cl-wrapper").css({ opacity: 1, 'margin-left': 0 });
        });

        $(document).ready(function () {
            $("#ledger_value").contents().find("input:text").keyup(function (e) {
                if (e.keyCode == 13) {
                    e.preventDefault()
                    var tot_fields = $("#ledger_value").contents().find("input:text").length;
                    var cur_field = $("#ledger_value").contents().find("input:text").index(this);
                    var next_index = cur_field + 1;
                    if (next_index != tot_fields) {
                        $("#ledger_value").contents().find("input:text")[next_index].focus();
                        $("#ledger_value").contents().find("input:text")[next_index].select();
                        return false;
                    } else {
                        $("#btnLogin").focus();
                    }
                }
            })
        })
        
  

    </script>
    <!-- Bootstrap core JavaScript
================================================== -->
    <!-- Placed at the end of the document so the pages load faster -->
    <script src="js/behaviour/voice-commands.js"></script>
    <script src="js/bootstrap/dist/js/bootstrap.min.js"></script>
    <script type="text/javascript" src="js/jquery.flot/jquery.flot.js"></script>
    <script type="text/javascript" src="js/jquery.flot/jquery.flot.pie.js"></script>
    <script type="text/javascript" src="js/jquery.flot/jquery.flot.resize.js"></script>
    <script type="text/javascript" src="js/jquery.flot/jquery.flot.labels.js"></script>
</body>
</html>
