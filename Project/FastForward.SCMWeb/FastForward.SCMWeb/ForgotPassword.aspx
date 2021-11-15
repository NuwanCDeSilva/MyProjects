<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ForgotPassword.aspx.cs" Inherits="FastForward.SCMWeb.ForgotPassword" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html>
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
                <div visible="false" class="alert alert-danger alert-dismissible" role="alert" runat="server" id="divwarnning">
                    <strong>Warning! </strong>
                    <asp:Label ID="lblWarning" runat="server"></asp:Label>
                </div>
                <div visible="false" class="alert alert-info" role="alert" runat="server" id="divInfo">
                        <strong>Info! </strong>
                        <asp:Label ID="lblInfo" runat="server"></asp:Label>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
            <ContentTemplate>
                <div visible="false" class="alert alert-danger alert-dismissible" role="alert" runat="server" id="div1">
                    <strong>Warning! </strong>
                    <asp:Label ID="Label2" runat="server"></asp:Label>
                </div>
                <div visible="false" class="alert alert-info" role="alert" runat="server" id="div2">
                        <strong>Info! </strong>
                        <asp:Label ID="Label4" runat="server"></asp:Label>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
        <asp:HiddenField ID="txtconformmessageValue" runat="server" />
        <div class="vertical-center">
            <div class="container">
               
                    <asp:Panel runat="server" ID="ForgotPasswordd">
                        <div class="col-sm-5" id="divForgotPassword" runat="server">
                            <div class="panel panel-default">
                                <div class="panel-heading">
                                    <asp:Image ID="Image1" runat="server" ImageUrl="~/images/banners/FFNew-125x41.png" />
                                </div>
                                <div class="panel-body">
                                    <asp:UpdatePanel ID="UpdatePanel1" class="resetPanel" UpdateMode="Conditional" runat="server">
                                        <ContentTemplate>
                                            <div class="row row-margin-cls">
                                                <div class="col-sm-4">
                                                    <asp:Label ID="Label3" runat="server" Text="User name"></asp:Label>
                                                </div>
                                                <div class="col-sm-7 ">
                                                    <asp:TextBox ID="txtUserId" placeholder="User name"  runat="server" AutoPostBack="true" class="form-control"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="row  row-margin-cls">
                                                <div class="col-sm-4">
                                                    <asp:Label ID="Label1" runat="server" Text="Email"></asp:Label>
                                                </div>
                                                <div class="col-sm-7 ">
                                                    <asp:TextBox ID="txtEmail" placeholder="Email"  runat="server" AutoPostBack="true" class="form-control"></asp:TextBox>
                                                </div>
                                            </div>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>

                                    <div class="row">
                                        <div class="col-sm-4">
                                            <br />
                                            &nbsp;<br />
                                        </div>
                                        <div class="col-sm-7 ">
                                            &nbsp;
                                            <asp:Button ID="btnReset" runat="server" CausesValidation="false" class="btn btn-primary" Text="Reset" OnClick="btnReset_Click" />
                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                            
                                        </div>
                                    </div>
                                </div>
                                <div class="panel-footer">© 2015 Sirius Technologies Service (Pvt) Ltd </div>
                            </div>
                        </div>
                    </asp:Panel>
                </div>
        </div>
    </form>
</body>
</html>
