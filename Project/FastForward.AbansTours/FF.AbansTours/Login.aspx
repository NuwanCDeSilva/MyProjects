<%@ Page Title="" Language="C#" MasterPageFile="~/Basic.Master" AutoEventWireup="true"
    CodeBehind="Login.aspx.cs" Inherits="FF.AbansTours.Login" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true" AsyncPostBackTimeout="600">
    </asp:ScriptManager>
    <style type="text/css">
        .divWaiting {
            position: absolute;
            background-color: #FAFAFA;
            z-index: 2147483647 !important;
            opacity: 0.8;
            overflow: hidden;
            text-align: center;
            top: 0;
            left: 0;
            height: 100%;
            width: 100%;
            padding-top: 20%;
        }
    </style>
    <asp:UpdateProgress ID="UpdateProgress1" DisplayAfter="10"
        runat="server" AssociatedUpdatePanelID="UpdatePanel2">
        <ProgressTemplate>
            <div class="divWaiting">
                <asp:Label ID="lblWait" runat="server"
                    Text="Please wait... " />
                <asp:Image ID="imgWait" runat="server"
                    ImageAlign="Middle" ImageUrl="~/images/ajax-loader.gif" />
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <asp:UpdatePanel ID="messages" runat="server">
        <ContentTemplate>
            <div visible="false" class="alert alert-danger" role="alert" runat="server" id="Div1">
                <div class="row">
                    <div class="col-sm-11  buttonrow ">
                        <strong>Oh snap!</strong>
                        <asp:Label ID="lblDivMessage" runat="server"></asp:Label>
                    </div>
                    <div class="col-sm-1  buttonrow">
                        <div class="col-sm-3  buttonrow">
                            <asp:LinkButton ID="LinkButton2" runat="server" CausesValidation="false" CssClass="floatright" OnClick="LinkButton2_Click">
                                        <span class="glyphicon glyphicon-ok" aria-hidden="true"></span>
                            </asp:LinkButton>
                        </div>
                    </div>

                </div>
                <div id="divConfirmsBtns" runat="server" visible="false" class="row" style="text-align: center">
                    <asp:Button ID="btn1" Text="btn1" Width="100px" runat="server" />
                    <asp:Button ID="btn2" Text="btn2" Width="100px" runat="server" />
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <script type="text/javascript">
        function validate(e) { var keycode = (e.which) ? e.which : e.keyCode; if (e.keyCode == 13) { alert("Enter Key"); return false; } }
    </script>
    <div id="cl-wrapper" class="login-container">
        <div class="middle-login">
            <div class="block-flat">
                <div class="header div_text_shadow" style="text-align: center; height: 70px;">
                    <h2>
                        <%--  <img src="~/images/logoATous.png" alt="Abans Tours" />--%>
                        Abans Tours
                    </h2>
                </div>
                <div>




                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <div id="ledger_value" class="content">
                                <h5 class="title">Login Access</h5>
                                <h5 class="title">User Name<asp:TextBox ID="txtUserName" runat="server" Height="23px" Width="369px"
                                    AutoPostBack="true" OnTextChanged="txtUserName_TextChanged" AutoCompleteType="Disabled"
                                    Style="font-size: small"></asp:TextBox>
                                </h5>
                                <div class="form-group">
                                    <h5 class="title">Password<asp:TextBox ID="txtPassword" runat="server" Height="23px" TextMode="Password"
                                        AutoPostBack="false" Width="369px"></asp:TextBox>
                                    </h5>
                                    <h5 class="title">Company
                                        <asp:DropDownList ID="ddlCompany" runat="server" Height="23px" Width="369px">
                                        </asp:DropDownList>
                                    </h5>
                                </div>
                                <div class="form-group">
                                    <asp:Label ID="lblmsg" CssClass="control-label" ForeColor="Red" runat="server" Text=""></asp:Label>
                                </div>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <div class="foot">
                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                            <ContentTemplate>
                                <asp:Button ID="btnLogin" CssClass="btn btn-small btn-primary btn-round" runat="server"
                                    Text="Login" OnClick="btnLogin_Click" />
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </div>
            <div style="text-align: center; color: red;">
                <asp:Label ID="lblMessage" Text=" " runat="server" Visible="false" />
            </div>
            <div style="text-align: center;">
                <asp:Image ID="imgLoading" ImageUrl="~/images/Processing.gif" Width="30px" Height="30px"
                    runat="server" />
            </div>
            <div style="text-align: center">
                <a href="#">2015 Sirius Technologies Service (Pvt) Ltd</a>
            </div>
        </div>
    </div>
    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
        <ContentTemplate>
            <asp:Panel ID="pnlWait" runat="server" Width="300px" Height="200px" Style="display: none">
                <div>
                    <asp:Label ID="lblText" Text="lblText" runat="server" />
                </div>
                <div>
                    <asp:Button ID="btnClose" Text="Close" runat="server" OnClick="btnClose_Click" Width="100px" />
                </div>
            </asp:Panel>
            <asp:Button ID="btnMDprint" runat="server" Text="D3" Style="display: none" />
            <asp:ModalPopupExtender ID="mpPleaseWait" runat="server" Enabled="True"
                PopupControlID="pnlWait" TargetControlID="btnMDprint" BackgroundCssClass="modalBackground"
                PopupDragHandleControlID="pnlWait">
            </asp:ModalPopupExtender>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
