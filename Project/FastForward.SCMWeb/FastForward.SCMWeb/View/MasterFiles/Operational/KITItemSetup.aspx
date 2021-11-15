<%@ Page Title="" Language="C#" MasterPageFile="~/View/AdminSite.Master" AutoEventWireup="true" CodeBehind="KITItemSetup.aspx.cs" Inherits="FastForward.SCMWeb.View.MasterFiles.Operational.KITItemSetup" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script src="<%=Request.ApplicationPath%>Js/jquery.min.js"></script>
    <script src="<%=Request.ApplicationPath%>Js/bootstrap.min.js"></script>
    <script src="<%=Request.ApplicationPath%>Js/Sales.js"></script>
    <script src="<%=Request.ApplicationPath%>Js/jquery.toastmessage.js"></script>
    <link href="<%=Request.ApplicationPath%>Css/jquery-ui.theme.min.css" rel="stylesheet" />
    <link href="<%=Request.ApplicationPath%>Css/jquery-ui.min.css" rel="stylesheet" />

    <script type="text/javascript">
        function showStickySuccessToast(value) {
            $().toastmessage('showToast', {
                text: value,
                sticky: true,
                position: 'top-center',
                type: 'success',
                closeText: '',
                close: function () {
                    console.log("toast is closed ...");
                }
            });
        }
        function showStickyNoticeToast(value) {
            $().toastmessage('showToast', {
                text: value,
                sticky: true,
                position: 'top-center',
                type: 'notice',
                closeText: '',
                close: function () { console.log("toast is closed ..."); }
            });
        }
        function showStickyWarningToast(value) {
            $().toastmessage('showToast', {
                text: value,
                sticky: false,
                position: 'top-center',
                type: 'warning',
                closeText: '',
                close: function () {
                    console.log("toast is closed ...");
                }

            });

        }
        function showStickyErrorToast(value) {

            $().toastmessage('showToast', {
                text: value,
                sticky: false,
                position: 'top-center',
                type: 'error',
                closeText: '',
                close: function () {
                    console.log("toast is closed ...");
                }
            });
        }

        function confClear() {
            var res = confirm("Do you want to clear?");
            if (res) {
                return true;
            }
            else {
                return false;
            }
        };

    </script>
    <style type="text/css">
        .checkboxstyle {
            padding-left: 2px;
        }

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
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" runat="server">
    <asp:UpdateProgress ID="UpdateProgress2" DisplayAfter="10"
        runat="server" AssociatedUpdatePanelID="pnlBasePanel">
        <ProgressTemplate>
            <div class="divWaiting">
                <asp:Label ID="lblWait1" runat="server"
                    Text="Please wait... " />
                <asp:Image ID="imgWait1" runat="server"
                    ImageAlign="Middle" ImageUrl="~/images/icons/ajax-loader.gif" />
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>

    <div class="panel panel-default marginLeftRight5">
        <div class="panel-body">
            <asp:UpdatePanel ID="pnlBasePanel" runat="server">
                <ContentTemplate>
                    <div class="row">
                        <div class="col-sm-3">
                            <h1 style="font-size: 11px; margin-top: 2px; font-weight: bolder">KIT Component Setup</h1>
                        </div>
                        <div class="col-sm-8" style="margin-top: 2px">
                            <asp:LinkButton ID="btnClear" runat="server" CssClass="floatRight" OnClientClick="return confClear()" OnClick="btnClear_Click">
                                        <span class="glyphicon glyphicon-refresh" aria-hidden="true"></span>Clear
                            </asp:LinkButton>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-sm-12">
                            <div class="panel panel-default marginLeftRight5">
                                <div class="panel-body">
                                    <div class="col-sm-1 labelText1">
                                        KIT Item
                                    </div>
                                    <div class="col-sm-2 padding0">
                                        <asp:TextBox runat="server" ID="txtkititem" OnTextChanged="txtkititem_TextChanged" AutoPostBack="true" CausesValidation="false" CssClass="form-control"></asp:TextBox>
                                    </div>
                                    <div class="col-sm-1 paddingLeft3">
                                        <asp:LinkButton ID="lbtnsearchkititem" runat="server" CausesValidation="false" OnClick="lbtnsearchkititem_Click">
                                                 <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                        </asp:LinkButton>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
</asp:Content>
