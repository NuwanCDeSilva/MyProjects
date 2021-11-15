<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="FastForward.SCMWeb.Login" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="Css/bootstrap.css" rel="stylesheet" />
    <link href="Css/style.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel4" runat="server">
            <ContentTemplate>
                <%-- <div visible="false" class="col-sm-12 alert alert-info alert-dismissible" role="alert" runat="server" id="DivAsk">
                    <div class="col-sm-11  buttonrow ">
                        <strong>Alert!</strong>
                        <asp:Label ID="lblAsk" runat="server"></asp:Label>
                    </div>
                    <div class="col-sm-1  buttonrow">
                        <div class="col-sm-3  buttonrow">
                            <asp:LinkButton ID="lbtColse" runat="server" CausesValidation="false" OnClick="lbtColse_Click" CssClass="floatright">
                                        <span class="glyphicon glyphicon-remove " aria-hidden="true" ></span>
                            </asp:LinkButton>
                        </div>
                        <div class="col-sm-3  buttonrow">
                            <asp:LinkButton ID="lbtnSave" runat="server" CausesValidation="false" OnClick="lbtnSave_Click1" CssClass="floatright">
                                        <span class="glyphicon glyphicon-ok" aria-hidden="true"></span>
                            </asp:LinkButton>
                        </div>
                    </div>
                </div>--%>
                <div visible="false" class="alert alert-danger alert-dismissible" role="alert" runat="server" id="divwarnning">
                    <strong>Warning!</strong>
                    <asp:Label ID="lblWarning" runat="server"></asp:Label>
                   
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>



        <asp:HiddenField ID="txtconformmessageValue" runat="server" />
        <div class="vertical-center">
            <div class="container">
               


                    <asp:Panel runat="server" ID="LoginP">
                        <div class="col-sm-5" id="divlogin" runat="server">
                            <div class="panel panel-default">
                                <div class="panel-heading">
                                    <asp:Image ID="Image1" runat="server" ImageUrl="~/images/banners/FFNew-125x41.png" />
                                </div>
                                <div class="panel-body">
                                    <asp:UpdatePanel ID="UpdatePanel1" UpdateMode="Conditional" runat="server">
                                        <ContentTemplate>
                                            <div class="row">
                                                <div class="col-sm-4">
                                                    <asp:Label ID="Label1" runat="server" Text="User name"></asp:Label>
                                                </div>
                                                <div class="col-sm-7 ">
                                                    <asp:TextBox ID="txtUserName" placeholder="Username" OnTextChanged="txtUserName_TextChanged" runat="server" AutoPostBack="true" class="form-control"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-12 height5">
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-4">
                                                    <asp:Label ID="Label2" runat="server" Text="Password"></asp:Label>
                                                </div>
                                                <div class="col-sm-7 ">
                                                    <asp:TextBox ID="txtPassword" runat="server" class="form-control" placeholder="Password" TextMode="Password"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-12 height5">
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-4">
                                                    <asp:Label ID="Label3" runat="server" Text="Company"></asp:Label>
                                                </div>
                                                <div class="col-sm-7 ">
                                                    <asp:DropDownList ID="ddlCompany" runat="server" AppendDataBoundItems="True" class="form-control" placeholder="Select Company">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-12 height22">
                                                </div>
                                            </div>

                                        </ContentTemplate>
                                    </asp:UpdatePanel>


                                    <div class="row">
                                        <div class="col-sm-4">
                                        </div>
                                        <div class="col-sm-7 ">
                                            <asp:Button ID="btnLogin" runat="server" CausesValidation="false" class="btn btn-primary" OnClick="btnLogin_Click" Text="Login" />
                                            <asp:HyperLink ID="fgtPasswd" runat="server" NavigateUrl="ForgotPassword.aspx" Target="_blank">Forgot password?</asp:HyperLink>
                                        </div>
                                    </div>


                                </div>
                                <div class="panel-footer">© 2018 Sirius Technologies Service (Pvt) Ltd </div>
                            </div>
                        </div>
                    </asp:Panel>


                    <div class="col-sm-6" id="divChangePassword" runat="server" visible="false">
                        <div class="panel panel-default">
                            <div class="panel-heading">Change Password</div>
                            <div class="panel-body">
                                  <div class="row">
                                                <div class="col-sm-6">
                                                    <asp:Label ID="Label20" runat="server" Text="User name"></asp:Label>
                                                </div>
                                                <div class="col-sm-6 ">
                                                    <asp:TextBox ID="usrName" placeholder="Username" runat="server" AutoPostBack="true" class="form-control"></asp:TextBox>
                                                </div>
                                            </div>
                                  <div class="row">
                                    <div class="col-sm-6">
                                        <asp:Label ID="Label7" runat="server" Text="Current password"></asp:Label>

                                    </div>
                                    <div class="col-sm-6 ">
                                        <asp:TextBox ID="crntPasswd" placeholder="Current password" class="form-control" runat="server" MaxLength="15" TextMode="Password" OnTextChanged="txtNewPassword_TextChanged"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-6">
                                        <asp:Label ID="Label4" runat="server" Text="New password"></asp:Label>

                                    </div>
                                    <div class="col-sm-6 ">
                                        <asp:TextBox ID="txtNewPassword" placeholder="New password" class="form-control" runat="server" MaxLength="15" TextMode="Password" OnTextChanged="txtNewPassword_TextChanged"></asp:TextBox>
                                    </div>
                                </div>

                                <div class="row">
                                    <div class="col-sm-6">
                                        <asp:Label ID="Label5" runat="server" Text="Confirm new password"></asp:Label>
                                    </div>
                                    <div class="col-sm-6">
                                        <asp:TextBox ID="txtConfirmNewPassword" runat="server" placeholder="Confirm new password" class="form-control" MaxLength="15" TextMode="Password"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-6">
                                    </div>
                                    <div class="col-sm-6 ">
                                        <asp:Button ID="btnSubmit" runat="server" CausesValidation="false" class="btn btn-primary" OnClick="btnResetLogin_Click" Text="Login" />
                                    </div>
                                </div>
                            </div>
                            <div class="panel-footer">© 2018 Sirius Technologies Service (Pvt) Ltd </div>
                        </div>
                    </div>


                </div>
           
        </div>
        <asp:Label ID="lblAlertValue" runat="server" Visible="false"></asp:Label>


        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
            <ContentTemplate>
                <asp:Button ID="Button3" runat="server" Text="Button" Style="display: none;" />
                <asp:ModalPopupExtender ID="UserPopoup" runat="server" Enabled="True" TargetControlID="Button3"
                    PopupControlID="test" PopupDragHandleControlID="PopupHeader" Drag="true" BackgroundCssClass="modalBackground">
                </asp:ModalPopupExtender>
            </ContentTemplate>
        </asp:UpdatePanel>
        <asp:Panel ID="test" runat="server" align="center">
            <asp:Label ID="Label6" runat="server" Text="Label" Visible="false"></asp:Label>
            <asp:ScriptManagerProxy ID="ScriptManagerProxy3" runat="server"></asp:ScriptManagerProxy>
            <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                <ContentTemplate>
                    <div class="panel panel-default">
                        <div class="panel-heading">
                            <span>Alert</span>
                        </div>
                        <div class="panel-body">
                            <div class="row">
                                <div class="col-sm-12">
                                    <asp:Label ID="lblMssg" runat="server"></asp:Label>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-12">
                                    <asp:Label ID="lblMssg1" runat="server"></asp:Label>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-12">
                                    <asp:Label ID="lblMssg2" runat="server"></asp:Label>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-12">
                                    <asp:Label ID="lblMssg3" runat="server"></asp:Label>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-12">
                                    <asp:Label ID="lblMssg4" runat="server"></asp:Label>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-12 height22">
                                     <asp:Label ID="lblpsw" runat="server" Visible="false"></asp:Label>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-2">
                                </div>
                                <div class="col-sm-4">
                                    <asp:Button ID="Button1" runat="server" Text="yes" CausesValidation="false" class="btn btn-primary" OnClick="Button1_Click" />
                                </div>
                                <div class="col-sm-4 ">
                                    <asp:Button ID="Button2" runat="server" Text="No" CausesValidation="false" class="btn btn-primary" OnClick="Button2_Click" />
                                </div>
                            </div>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </asp:Panel>
    </form>
</body>
</html>
