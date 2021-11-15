<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ResetPassword.aspx.cs" Inherits="FastForward.SCMWeb.ResetPassword" %>

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
                <div visible="false" class="alert alert-danger alert-dismissible" role="alert" runat="server" id="divwarnning">
                    <strong>Warning!</strong>
                    <asp:Label ID="lblWarning" runat="server"></asp:Label>
                   
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>

        <asp:HiddenField ID="txtconformmessageValue" runat="server" />
        <div class="vertical-center">
            <div class="container">
               
                    <asp:Panel runat="server" ID="ResetPw">
                        <div class="col-sm-5" id="divResetPw" runat="server">
                            <div class="panel panel-default">
                                <div class="panel-heading">
                                    <asp:Image ID="Image1" runat="server" ImageUrl="~/images/banners/FFNew-125x41.png" />
                                </div>
                                <div class="panel-body">
                                    <asp:UpdatePanel ID="UpdatePanel1" class="resetPanel" UpdateMode="Conditional" runat="server">
                                        <ContentTemplate>
                                            <div class="row row-margin-cls">
                                                <div class="col-sm-4">
                                                    <asp:Label ID="Label3" runat="server" Text="New Password"></asp:Label>
                                                </div>
                                                <div class="col-sm-7 ">
                                                    <asp:TextBox ID="txtNewPw" autocomplete="off" placeholder="New Password" TextMode="Password"  runat="server" class="form-control"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="row row-margin-cls">
                                                <div class="col-sm-4">
                                                    <asp:Label ID="Label1" runat="server" Text="Confirm Password"></asp:Label>
                                                </div>
                                                <div class="col-sm-7 ">
                                                    <asp:TextBox ID="txtConfPw" autocomplete="off" placeholder="Confirm Password"  TextMode="Password" runat="server" class="form-control"></asp:TextBox>
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
                                            <asp:Button ID="btnUpdate" runat="server" CausesValidation="false" class="btn btn-primary" Text="Update" OnClick="btnUpdate_Click" />
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
