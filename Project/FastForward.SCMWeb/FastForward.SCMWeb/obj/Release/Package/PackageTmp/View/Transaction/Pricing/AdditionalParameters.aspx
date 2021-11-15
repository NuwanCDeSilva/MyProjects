<%@ Page Title="Additonal Parameters" Language="C#" MasterPageFile="~/View/AdminSite.Master" AutoEventWireup="true" CodeBehind="AdditionalParameters.aspx.cs" Inherits="FastForward.SCMWeb.View.Transaction.Pricing.AdditionalParameters" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">

    <script type="text/javascript">
        function jsDecimals(e) {
            var evt = (e) ? e : window.event;
            var key = (evt.keyCode) ? evt.keyCode : evt.which;
            if (key != null) {
                key = parseInt(key, 10);
                if ((key < 48 || key > 57) && (key < 96 || key > 105)) {
                    if (!jsIsUserFriendlyChar(key, "Decimals")) {
                        return false;
                    }
                }
                else {
                    if (evt.shiftKey) {
                        return false;
                    }
                }
            }
            return true;
        }

        function jsIsUserFriendlyChar(val, step) {
            if (val == 8 || val == 9 || val == 13 || val == 45 || val == 46) {
                return true;
            }
            if ((val > 16 && val < 21) || (val > 34 && val < 41)) {
                return true;
            }
            if (step == "Decimals") {
                if (val == 190 || val == 110) {
                    return true;
                }
            }
            return false;
        };

        function showSuccessToast() {
            if (jQuery('.toast-item-wrapper') != null) {
                jQuery('.toast-item-wrapper').remove();
            }
            $().toastmessage('showSuccessToast', "Success Dialog which is fading away ...");
        }

        function showStickySuccessToast(value) {
            if (jQuery('.toast-item-wrapper') != null) {
                jQuery('.toast-item-wrapper').remove();
            }
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

        function showNoticeToast() {
            $().toastmessage('showNoticeToast', "Notice  Dialog which is fading away ...");
        }

        function showStickyNoticeToast(value) {
            if (jQuery('.toast-item-wrapper') != null) {
                jQuery('.toast-item-wrapper').remove();
            }
            $().toastmessage('showToast', {
                text: value,
                sticky: true,
                position: 'top-center',
                type: 'notice',
                closeText: '',
                close: function () { console.log("toast is closed ..."); }
            });
        }

        function showWarningToast() {
            if (jQuery('.toast-item-wrapper') != null) {
                jQuery('.toast-item-wrapper').remove();
            }
            $().toastmessage('showWarningToast', "Warning Dialog which is fading away ...");
        }

        function showStickyWarningToast(value) {
            if (jQuery('.toast-item-wrapper') != null) {
                jQuery('.toast-item-wrapper').remove();
            }
            $().toastmessage('showToast', {
                text: value,
                sticky: true,
                position: 'top-center',
                type: 'warning',
                closeText: '',
                close: function () {
                    console.log("toast is closed ...");
                }
            });
        }

        function showErrorToast() {
            $().toastmessage('showErrorToast', "Error Dialog which is fading away ...");
        }

        function showStickyErrorToast(value) {

            $().toastmessage('showToast', {
                text: value,
                sticky: true,
                position: 'top-center',
                type: 'error',
                closeText: '',
                close: function () {
                    console.log("toast is closed ...");
                }
            });
        };

    </script>

    <script type="text/javascript">
        function ConfirmSave() {
            var result = confirm("Do you want to save?");
            if (result) {
                return true;
            }
            else {
                return false;
            }
        };

        function ConfirmClearForm() {
            var result = confirm("Do you want to clear?");
            if (result) {
                return true;
            }
            else {
                return false;
            }
        };

        function ConfirmDeliem() {
            var result = confirm("Do you want to delete?");
            if (result) {
                return true;
            }
            else {
                return false;
            }
        };

        function ConfirmDelProf() {
            var result = confirm("Do you want to delete?");
            if (result) {
                return true;
            }
            else {
                return false;
            }
        };

    </script>

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

        .rbl input[type="radio"] {
            margin-left: 10px;
            margin-right: 1px;
        }

        .chkChoice input {
            margin-left: 0px;
        }

        .chkChoice td {
            padding-left: 20px;
        }
        .marginTop {
            margin-top:3px;
        }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" runat="server">
    <asp:UpdateProgress ID="UpdateProgress3" DisplayAfter="0" runat="server" AssociatedUpdatePanelID="upMainBtn">
        <ProgressTemplate>
            <div class="divWaiting">
                <asp:Label ID="lblWait3" runat="server"
                    Text="Please wait... " />
                <asp:Image ID="imgWait3" runat="server"
                    ImageAlign="Middle" ImageUrl="~/images/icons/ajax-loader.gif" />
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>

    <asp:UpdateProgress ID="UpdateProgress4" DisplayAfter="0" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
        <ProgressTemplate>
            <div class="divWaiting">
                <asp:Label ID="lblWait4" runat="server"
                    Text="Please wait... " />
                <asp:Image ID="imgWait4" runat="server"
                    ImageAlign="Middle" ImageUrl="~/images/icons/ajax-loader.gif" />
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>

    <asp:UpdateProgress ID="UpdateProgress5" DisplayAfter="0" runat="server" AssociatedUpdatePanelID="UpdatePanel2">
        <ProgressTemplate>
            <div class="divWaiting">
                <asp:Label ID="lblWait5" runat="server"
                    Text="Please wait... " />
                <asp:Image ID="imgWait5" runat="server"
                    ImageAlign="Middle" ImageUrl="~/images/icons/ajax-loader.gif" />
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>

    <asp:UpdateProgress ID="asdasd" DisplayAfter="0" runat="server" AssociatedUpdatePanelID="upAutodateUp123">
        <ProgressTemplate>
            <div class="divWaiting">
                <asp:Label ID="lblWait6" runat="server"
                    Text="Please wait... " />
                <asp:Image ID="imgWait6" runat="server"
                    ImageAlign="Middle" ImageUrl="~/images/icons/ajax-loader.gif" />
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>

    <div class="panel panel-default marginLeftRight5">
        <div class="row">
            <asp:ScriptManagerProxy ID="ScriptManagerProxy2" runat="server"></asp:ScriptManagerProxy>
            <asp:UpdatePanel runat="server">
                <ContentTemplate>
                    <asp:UpdatePanel runat="server">
                        <ContentTemplate>
                            <asp:HiddenField runat="server" ID="hdfSeletedTab" />
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
        <div class="row">
            <div class="panel-body">
                <div class="col-sm-12">
                    <div>
                        <ul id="myTab" class="nav nav-tabs" role="tablist">
                            <li role="presentation" class="active"><a href="#AdditionalParameters" aria-controls="AdditionalParameters" role="tab" data-toggle="tab">Additional Parameters</a></li>
                            <li role="presentation"><a href="#SetupAutoDefinition" aria-controls="SetupAutoDefinition" role="tab" data-toggle="tab">Setup Auto Category Definition</a></li>
                            <li role="presentation"><a href="#PriceClone" aria-controls="PriceClone" role="tab" data-toggle="tab">Price Clone</a></li>
                            <li role="presentation"><a href="#SimilarItem" aria-controls="SimilarItem" role="tab" data-toggle="tab">Similar Item Definition</a></li>
                            <li role="presentation"><a href="#PromotionalVoucher" aria-controls="PromotionalVoucher" role="tab" data-toggle="tab">Promotional Voucher Definition</a></li>
                            <li role="presentation"><a href="#DiscountAndEditing" aria-controls="DiscountAndEditing" role="tab" data-toggle="tab">Discount And Editing</a></li>
                        </ul>
                        <div class="tab-content">
                            <div role="tabpanel" class="tab-pane active" id="AdditionalParameters">
                                <asp:ScriptManagerProxy ID="ScriptManagerProxy3" runat="server"></asp:ScriptManagerProxy>
                                <asp:UpdatePanel runat="server">
                                    <ContentTemplate>
                                        <div class="col-sm-12">
                                            <div class="row">
                                                <div class="panel panel-default ">
                                                    <div class="panel-body">
                                                        <div class="col-sm-12">
                                                            <div class="panel panel-default ">
                                                                <div class="panel-heading pannelheading height16 paddingtop0">
                                                                    <b>Special Definition</b>
                                                                </div>
                                                                <div class="panel-body">
                                                                    <div class="col-sm-12 buttonRow">
                                                                        <asp:UpdatePanel ID="upMainBtn" runat="server">
                                                                            <ContentTemplate>
                                                                                <div class="col-sm-10 ">
                                                                                </div>
                                                                                <div class="col-sm-1 padding0">
                                                                                    <asp:LinkButton ID="btnAddUpdate" CausesValidation="false" runat="server" CssClass="floatRight" OnClientClick="return ConfirmSave();" OnClick="btnAddUpdate_Click">
                                                                        <span class="glyphicon glyphicon-saved" aria-hidden="true" ></span>Update
                                                                                    </asp:LinkButton>
                                                                                </div>
                                                                                <div class="col-sm-1 padding0">
                                                                                    <asp:LinkButton ID="btnAddClear" CausesValidation="false" runat="server" CssClass="floatRight" OnClientClick="return ConfirmClearForm();" OnClick="btnAddClear_Click">
                                                                        <span class="glyphicon glyphicon-refresh" aria-hidden="true" ></span>Clear
                                                                                    </asp:LinkButton>
                                                                                </div>
                                                                            </ContentTemplate>
                                                                        </asp:UpdatePanel>
                                                                    </div>
                                                                    <div class="col-sm-12">
                                                                        <div class="col-sm-2">
                                                                            Price Book
                                                                        </div>
                                                                        <div class="col-sm-4 paddingLeft0">
                                                                            <div class="col-sm-11">
                                                                                <asp:TextBox ID="txtAddBook" runat="server" CssClass="form-control" AutoPostBack="true" OnTextChanged="txtAddBook_TextChanged" />
                                                                            </div>
                                                                            <div class="col-sm-1">
                                                                                <asp:LinkButton ID="btnSearchAddbook" runat="server" CssClass="floatRight" OnClick="btnSearchAddbook_Click">
                                                                                            <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                                                </asp:LinkButton>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                    <div class="col-sm-12 height5"></div>
                                                                    <div class="col-sm-12">
                                                                        <div class="col-sm-2">
                                                                            Price Level
                                                                        </div>
                                                                        <div class="col-sm-4 paddingLeft0">
                                                                            <div class="col-sm-11">
                                                                                <asp:TextBox ID="txtAddLevel" runat="server" CssClass="form-control" AutoPostBack="true" OnTextChanged="txtAddLevel_TextChanged" />
                                                                            </div>
                                                                            <div class="col-sm-1">
                                                                                <asp:LinkButton ID="btnSearchAddLevel" runat="server" CssClass="floatRight" OnClick="btnSearchAddLevel_Click">
                                                                                            <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                                                </asp:LinkButton>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                    <div class="col-sm-12 height5"></div>
                                                                    <div class="col-sm-12">
                                                                        <div class="col-sm-2">
                                                                            Message
                                                                        </div>
                                                                        <div class="col-sm-8">
                                                                            <asp:TextBox ID="txtMsg" runat="server" CssClass="form-control" />
                                                                        </div>
                                                                    </div>
                                                                    <div class="col-sm-12 height5"></div>
                                                                    <div class="col-sm-12">
                                                                        <div class="col-sm-2">
                                                                            Aging
                                                                        </div>
                                                                        <div class="col-sm-3">
                                                                            <asp:CheckBox ID="chkAge" Text="" runat="server" />
                                                                        </div>
                                                                        <div class="col-sm-1">
                                                                        </div>
                                                                    </div>
                                                                    <div class="col-sm-12 height5"></div>
                                                                    <div class="col-sm-12">
                                                                        <div class="col-sm-2">
                                                                            Customer Creation Mandatory
                                                                        </div>
                                                                        <div class="col-sm-3">
                                                                            <asp:CheckBox ID="chkCusMan" Text="" runat="server" />
                                                                        </div>
                                                                        <div class="col-sm-1">
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="panel panel-default ">
                                                                <div class="panel-heading pannelheading height16 paddingtop0">
                                                                    <b>Setup Default Parameters</b>
                                                                </div>
                                                                <div class="panel-body">
                                                                    <div class="col-sm-12 buttonRow">
                                                                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                                            <ContentTemplate>
                                                                                <div class="col-sm-10 ">
                                                                                </div>
                                                                                <div class="col-sm-1 padding0">
                                                                                    <asp:LinkButton ID="btnUpdateDefPara" CausesValidation="false" runat="server" CssClass="floatRight" OnClientClick="return ConfirmSave();" OnClick="btnUpdateDefPara_Click">
                                                                        <span class="glyphicon glyphicon-saved" aria-hidden="true" ></span>Update
                                                                                    </asp:LinkButton>
                                                                                </div>
                                                                                <div class="col-sm-1 padding0">
                                                                                    <asp:LinkButton ID="btnClearDefPara" CausesValidation="false" runat="server" CssClass="floatRight" OnClientClick="return ConfirmClearForm();" OnClick="btnClearDefPara_Click">
                                                                        <span class="glyphicon glyphicon-refresh" aria-hidden="true" ></span>Clear
                                                                                    </asp:LinkButton>
                                                                                </div>
                                                                            </ContentTemplate>
                                                                        </asp:UpdatePanel>
                                                                    </div>
                                                                    <div class="col-sm-12">
                                                                        <div class="col-sm-2">
                                                                            Profit Center
                                                                        </div>
                                                                        <div class="col-sm-4">
                                                                            <div class="col-sm-11">
                                                                                <asp:TextBox ID="TextBoxLocation" runat="server" CssClass="form-control" AutoPostBack="true" OnTextChanged="TextBoxLocation_TextChanged" />
                                                                            </div>
                                                                            <div class="col-sm-1">
                                                                                <asp:LinkButton ID="btnProfitCenter" runat="server" CssClass="floatRight" OnClick="btnProfitCenter_Click">
                                                                                            <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                                                </asp:LinkButton>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                    <div class="col-sm-12 height10"></div>
                                                                    <div class="col-sm-12">
                                                                        <div class="col-sm-4">
                                                                            <div class="panel panel-default ">
                                                                                <div class="panel-heading pannelheading height16 paddingtop0">
                                                                                    <b>Default Parameters</b>
                                                                                </div>
                                                                                <div class="panel-body">
                                                                                    <div class="col-sm-12">
                                                                                        <div class="col-sm-5  paddingLeft0">
                                                                                            Default Price Book
                                                                                        </div>
                                                                                        <div class="col-sm-6 padding0">
                                                                                            <asp:TextBox ID="txtDPricebook" runat="server" CssClass="form-control" AutoPostBack="true" OnTextChanged="txtDPricebook_TextChanged" />
                                                                                        </div>
                                                                                        <div class="col-sm-1 paddingLeft0">
                                                                                            <asp:LinkButton ID="btnDPriceBook" runat="server" CssClass="floatRight" OnClick="btnDPriceBook_Click">
                                                                                            <span class="glyphicon glyphicon-arrow-right" aria-hidden="true"></span>
                                                                                            </asp:LinkButton>
                                                                                        </div>
                                                                                    </div>
                                                                                    <div class="col-sm-12 height5"></div>
                                                                                    <div class="col-sm-12">
                                                                                        <div class="col-sm-5  paddingLeft0">
                                                                                            Default Price Level
                                                                                        </div>
                                                                                        <div class="col-sm-6 padding0">
                                                                                            <asp:TextBox ID="txtDPriceLevel" runat="server" CssClass="form-control" AutoPostBack="true" OnTextChanged="txtDPriceLevel_TextChanged" />
                                                                                        </div>
                                                                                        <div class="col-sm-1 paddingLeft0">
                                                                                            <asp:LinkButton ID="btnDPriceLevel" runat="server" CssClass="floatRight" OnClick="btnDPriceLevel_Click">
                                                                                            <span class="glyphicon glyphicon-arrow-right" aria-hidden="true"></span>
                                                                                            </asp:LinkButton>
                                                                                        </div>
                                                                                    </div>
                                                                                    <div class="col-sm-12 height5"></div>
                                                                                    <div class="col-sm-12">
                                                                                        <div class="col-sm-5  paddingLeft0">
                                                                                            Default Item Status
                                                                                        </div>
                                                                                        <div class="col-sm-6 padding0">
                                                                                            <asp:TextBox ID="txtItemStatus" runat="server" CssClass="form-control" AutoPostBack="true" OnTextChanged="txtItemStatus_TextChanged" />
                                                                                        </div>
                                                                                        <div class="col-sm-1 paddingLeft0">
                                                                                            <asp:LinkButton ID="btnItemStatusSearch" runat="server" CssClass="floatRight" OnClick="btnItemStatusSearch_Click">
                                                                                            <span class="glyphicon glyphicon-arrow-right" aria-hidden="true"></span>
                                                                                            </asp:LinkButton>
                                                                                        </div>
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                        <div class="col-sm-4">
                                                                            <div class="panel panel-default ">
                                                                                <div class="panel-heading pannelheading height16 paddingtop0">
                                                                                    <b>Alert Promotion</b>
                                                                                </div>
                                                                                <div class="panel-body">
                                                                                    <div class="col-sm-12">
                                                                                        <div class="col-sm-5  paddingLeft0">
                                                                                            Price Book
                                                                                        </div>
                                                                                        <div class="col-sm-6 padding0">
                                                                                            <asp:TextBox ID="txtAlertPriceBook" runat="server" CssClass="form-control" AutoPostBack="true" OnTextChanged="txtAlertPriceBook_TextChanged" />
                                                                                        </div>
                                                                                        <div class="col-sm-1 paddingLeft0">
                                                                                            <asp:LinkButton ID="btnAlertPbookSearch" runat="server" CssClass="floatRight" OnClick="btnAlertPbookSearch_Click">
                                                                                            <span class="glyphicon glyphicon-arrow-right" aria-hidden="true"></span>
                                                                                            </asp:LinkButton>
                                                                                        </div>
                                                                                    </div>
                                                                                    <div class="col-sm-12 height5"></div>
                                                                                    <div class="col-sm-12">
                                                                                        <div class="col-sm-5  paddingLeft0">
                                                                                            Price Level
                                                                                        </div>
                                                                                        <div class="col-sm-6 padding0">
                                                                                            <asp:TextBox ID="txtAlertPriceLevel" runat="server" CssClass="form-control" AutoPostBack="true" OnTextChanged="txtAlertPriceLevel_TextChanged" />
                                                                                        </div>
                                                                                        <div class="col-sm-1 paddingLeft0">
                                                                                            <asp:LinkButton ID="btnAlertPlevelSearch" runat="server" CssClass="floatRight" OnClick="btnAlertPlevelSearch_Click">
                                                                                            <span class="glyphicon glyphicon-arrow-right" aria-hidden="true"></span>
                                                                                            </asp:LinkButton>
                                                                                        </div>
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                        <div class="col-sm-4">
                                                                            <div class="panel panel-default ">
                                                                                <div class="panel-heading pannelheading height16 paddingtop0">
                                                                                    <b>
                                                                                        <asp:Label ID="lblSelection" Text="Select" runat="server" />
                                                                                    </b>
                                                                                </div>
                                                                                <div class="panel-body">
                                                                                    <asp:GridView ID="dgvPriceBookDetails" runat="server" CssClass="table table-hover table-striped" GridLines="None" ShowHeaderWhenEmpty="True" EmptyDataText="No data found..." OnSelectedIndexChanged="dgvPriceBookDetails_SelectedIndexChanged">
                                                                                        <Columns>
                                                                                            <asp:ButtonField Text="Select" CommandName="Select" ItemStyle-Width="10" />
                                                                                        </Columns>
                                                                                    </asp:GridView>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                            <div role="tabpanel" class="tab-pane" id="SetupAutoDefinition">
                                <asp:ScriptManagerProxy ID="ScriptManagerProxy5" runat="server"></asp:ScriptManagerProxy>
                                <asp:UpdatePanel runat="server">
                                    <ContentTemplate>
                                        <div class="col-sm-12">
                                            <div class="row">
                                                <div class="col-sm-12 buttonRow">
                                                    <asp:ScriptManagerProxy ID="ScriptManagerProxy1" runat="server"></asp:ScriptManagerProxy>
                                                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                                        <ContentTemplate>
                                                            <div class="col-sm-10 ">
                                                            </div>
                                                            <div class="col-sm-1 padding0">
                                                                <asp:LinkButton ID="btnUpdate" CausesValidation="false" runat="server" CssClass="floatRight" OnClientClick="return ConfirmSave();" OnClick="btnUpdate_Click">
                                                                        <span class="glyphicon glyphicon-saved" aria-hidden="true" ></span>Save
                                                                </asp:LinkButton>
                                                            </div>
                                                            <div class="col-sm-1 padding0">
                                                                <asp:LinkButton ID="btnClear" CausesValidation="false" runat="server" CssClass="floatRight" OnClientClick="return ConfirmClearForm();" OnClick="btnClear_Click">
                                                                        <span class="glyphicon glyphicon-refresh" aria-hidden="true" ></span>Clear
                                                                </asp:LinkButton>
                                                            </div>
                                                            <asp:TextBox ID="txtBrand" Visible="false" runat="server" />
                                                            <asp:TextBox ID="txtCircular" Visible="false" runat="server" />
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </div>
                                                <div class="col-sm-12 height5"></div>
                                                <div class="col-sm-4">
                                                    <div class="panel panel-default ">
                                                        <div class="panel-heading pannelheading height16 paddingtop0">
                                                            <b>Main Parameters</b>
                                                        </div>
                                                        <div class="panel-body">
                                                            <div class="col-sm-12">
                                                                <div class="col-sm-5  paddingLeft0">
                                                                    Circular #
                                                                </div>
                                                                <div class="col-sm-6 padding0">
                                                                    <asp:TextBox ID="txtBaseCircular" MaxLength="10" Style="text-transform: uppercase" runat="server" CssClass="form-control" AutoPostBack="true" OnTextChanged="txtBaseCircular_TextChanged" />
                                                                </div>
                                                                <div class="col-sm-1 paddingLeft0">
                                                                    <asp:LinkButton ID="btnSrhCircular" runat="server" CssClass="floatRight" OnClick="btnSrhCircular_Click">
                                                                                            <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                                    </asp:LinkButton>
                                                                </div>
                                                            </div>
                                                            <div class="col-sm-12 height5"></div>
                                                            <div class="col-sm-12">
                                                                <div class="col-sm-5  paddingLeft0">
                                                                    Price Book
                                                                </div>
                                                                <div class="col-sm-6 padding0">
                                                                    <asp:TextBox ID="txtNpb" Style="text-transform: uppercase" runat="server" CssClass="form-control" AutoPostBack="true" OnTextChanged="txtNpb_TextChanged" />
                                                                </div>
                                                                <div class="col-sm-1 paddingLeft0">
                                                                    <asp:LinkButton ID="btnSrhPbd" runat="server" CssClass="floatRight" OnClick="btnSrhPbd_Click">
                                                                                            <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                                    </asp:LinkButton>
                                                                </div>
                                                            </div>
                                                            <div class="col-sm-12 height5"></div>
                                                            <div class="col-sm-12">
                                                                <div class="col-sm-5  paddingLeft0">
                                                                    Price Level
                                                                </div>
                                                                <div class="col-sm-6 padding0">
                                                                    <asp:TextBox ID="txtNpl" Style="text-transform: uppercase" runat="server" CssClass="form-control" AutoPostBack="true" OnTextChanged="txtNpl_TextChanged" />
                                                                </div>
                                                                <div class="col-sm-1 paddingLeft0">
                                                                    <asp:LinkButton ID="btnSrhPbld" runat="server" CssClass="floatRight" OnClick="btnSrhPbld_Click">
                                                                                            <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                                    </asp:LinkButton>
                                                                </div>
                                                            </div>
                                                            <div class="col-sm-12 height5"></div>
                                                            <div class="col-sm-12">
                                                                <div class="col-sm-5  paddingLeft0">
                                                                    Based Price Book
                                                                </div>
                                                                <div class="col-sm-6 padding0">
                                                                    <asp:TextBox ID="txtBasepb" Style="text-transform: uppercase" runat="server" CssClass="form-control" AutoPostBack="true" OnTextChanged="txtBasepb_TextChanged" />
                                                                </div>
                                                                <div class="col-sm-1 paddingLeft0">
                                                                    <asp:LinkButton ID="btnSrhPbdBase" runat="server" CssClass="floatRight" OnClick="btnSrhPbdBase_Click">
                                                                                            <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                                    </asp:LinkButton>
                                                                </div>
                                                            </div>
                                                            <div class="col-sm-12 height5"></div>
                                                            <div class="col-sm-12">
                                                                <div class="col-sm-5  paddingLeft0">
                                                                    Based  Price Level
                                                                </div>
                                                                <div class="col-sm-6 padding0">
                                                                    <asp:TextBox ID="txtBasepbl" Style="text-transform: uppercase" runat="server" CssClass="form-control" AutoPostBack="true" OnTextChanged="txtBasepbl_TextChanged" />
                                                                </div>
                                                                <div class="col-sm-1 paddingLeft0">
                                                                    <asp:LinkButton ID="btnSrhPbldBase" runat="server" CssClass="floatRight" OnClick="btnSrhPbldBase_Click">
                                                                                            <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                                    </asp:LinkButton>
                                                                </div>
                                                            </div>
                                                            <div class="col-sm-12 height5"></div>
                                                            <div class="col-sm-12">
                                                                <div class="col-sm-5  paddingLeft0">
                                                                    Date From
                                                                </div>
                                                                <div class="col-sm-6 padding0">
                                                                    <asp:TextBox ID="dtpBaseFrom" runat="server" CssClass="form-control" Format="dd/MMM/yyyy" onkeypress="return RestrictSpace()" Enabled="false"></asp:TextBox>
                                                                    <asp:CalendarExtender ID="dtpBaseFromcal" runat="server" TargetControlID="dtpBaseFrom"
                                                                        PopupButtonID="dtpBaseFrombtn" Format="dd/MMM/yyyy">
                                                                    </asp:CalendarExtender>
                                                                </div>
                                                                <div class="col-sm-1 paddingLeft0">
                                                                    <asp:LinkButton ID="dtpBaseFrombtn" CausesValidation="false" runat="server">
                                                                        <span class="glyphicon glyphicon-calendar" aria-hidden="true"></span>
                                                                    </asp:LinkButton>
                                                                </div>
                                                            </div>
                                                            <div class="col-sm-12 height5"></div>
                                                            <div class="col-sm-12">
                                                                <div class="col-sm-5  paddingLeft0">
                                                                    To
                                                                </div>
                                                                <div class="col-sm-6 padding0">
                                                                    <asp:TextBox ID="dtpBaseTo" runat="server" CssClass="form-control" Format="dd/MMM/yyyy" onkeypress="return RestrictSpace()" Enabled="false"></asp:TextBox>
                                                                    <asp:CalendarExtender ID="dtpBaseToCal" runat="server" TargetControlID="dtpBaseTo"
                                                                        PopupButtonID="dtpBaseToBtn" Format="dd/MMM/yyyy">
                                                                    </asp:CalendarExtender>
                                                                </div>
                                                                <div class="col-sm-1 paddingLeft0">
                                                                    <asp:LinkButton ID="dtpBaseToBtn" CausesValidation="false" runat="server">
                                                                        <span class="glyphicon glyphicon-calendar" aria-hidden="true"></span>
                                                                    </asp:LinkButton>
                                                                </div>
                                                            </div>
                                                            <div class="col-sm-12 height5"></div>
                                                            <div class="col-sm-12">
                                                                <div class="col-sm-5  paddingLeft0">
                                                                    Active
                                                                </div>
                                                                <div class="col-sm-6 padding0">
                                                                    <asp:DropDownList ID="cmbActivebase" runat="server" CssClass="form-control">
                                                                        <asp:ListItem Text="Select" Value="-1" />
                                                                        <asp:ListItem Text="Yes" Value="1" />
                                                                        <asp:ListItem Text="No" Value="0" />
                                                                    </asp:DropDownList>
                                                                </div>
                                                                <div class="col-sm-1 paddingLeft0">
                                                                </div>
                                                            </div>
                                                            <div class="col-sm-12 height5"></div>
                                                            <div class="col-sm-12">
                                                                <div class="col-sm-5  paddingLeft0">
                                                                    Category
                                                                </div>
                                                                <div class="col-sm-6 padding0">
                                                                    <asp:DropDownList ID="cmbCate" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="cmbCate_SelectedIndexChanged">
                                                                        <asp:ListItem Text="Select" Value="0" />
                                                                        <asp:ListItem Text="Item" Value="I" />
                                                                        <asp:ListItem Text="Charge" Value="C" />
                                                                    </asp:DropDownList>
                                                                </div>
                                                                <div class="col-sm-1 paddingLeft0">
                                                                </div>
                                                            </div>
                                                            <div class="col-sm-12 height5"></div>
                                                        </div>
                                                    </div>

                                                </div>
                                                <div class="col-sm-8">
                                                    <div class="panel panel-default ">
                                                        <div class="panel-heading pannelheading height16 paddingtop0">
                                                            <b>Category Selection</b>
                                                        </div>
                                                        <div class="panel-body">
                                                            <div class="col-sm-12">
                                                                <div class="col-sm-1">
                                                                    Upload
                                                                </div>
                                                                <div class="col-sm-7">
                                                                    <asp:FileUpload ID="FileUploadControl" runat="server" />
                                                                </div>
                                                                <div class="col-sm-4">
                                                                    <asp:LinkButton ID="lbtnExcelUpload" runat="server" OnClick="btnUploadItem_Click">
                                                                            <span class="glyphicon glyphicon-upload" aria-hidden="true"></span>Excel Upload
                                                                    </asp:LinkButton>
                                                                </div>
                                                            </div>
                                                            <div class="col-sm-12 height5"></div>
                                                            <div class="col-sm-12">
                                                                <div class="col-sm-5 padding0">
                                                                    <asp:UpdatePanel ID="upAutodateUp123" runat="server">
                                                                        <ContentTemplate>
                                                                            <div class="col-sm-12">
                                                                                <div class="col-sm-4  paddingLeft0">
                                                                                    Type
                                                                                </div>
                                                                                <div class="col-sm-6 padding0">
                                                                                    <asp:DropDownList ID="cmbSelectCat" CssClass="form-control" runat="server" AutoPostBack="true" OnSelectedIndexChanged="cmbSelectCat_SelectedIndexChanged">
                                                                                        <asp:ListItem Text="Select" Value="-1" />
                                                                                        <asp:ListItem Text="ITEM" Value="3" />
                                                                                        <asp:ListItem Text="BRAND & SUB CAT" Value="4" />
                                                                                        <asp:ListItem Text="BRAND & CAT" Value="5" />
                                                                                        <asp:ListItem Text="BRAND & MAIN CAT" Value="6" />
                                                                                        <asp:ListItem Text="BRAND" Value="7" />
                                                                                        <asp:ListItem Text="SUB CAT" Value="8" />
                                                                                        <asp:ListItem Text="CAT" Value="9" />
                                                                                        <asp:ListItem Text="MAIN CAT" Value="10" />
                                                                                    </asp:DropDownList>
                                                                                </div>
                                                                                <div class="col-sm-1">
                                                                                    <asp:LinkButton ID="btnAddCat" runat="server" CssClass="floatRight" OnClick="btnAddCat_Click">
                                                                                            <span class="glyphicon glyphicon-arrow-right" aria-hidden="true"></span>
                                                                                    </asp:LinkButton>
                                                                                </div>
                                                                            </div>
                                                                            <div class="col-sm-12 height5"></div>
                                                                            <div class="col-sm-12">
                                                                                <div class="col-sm-4 paddingLeft0">
                                                                                    Brand
                                                                                </div>
                                                                                <div class="col-sm-6 padding0">
                                                                                    <asp:TextBox ID="txtbbrd" runat="server" CssClass="form-control" AutoPostBack="true" OnTextChanged="txtbbrd_TextChanged" />
                                                                                </div>
                                                                                <div class="col-sm-1">
                                                                                    <asp:LinkButton ID="btnsrhpbbrand" runat="server" CssClass="floatRight" OnClick="btnsrhpbbrand_Click">
                                                                                            <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                                                    </asp:LinkButton>
                                                                                </div>
                                                                            </div>
                                                                            <div class="col-sm-12 height5"></div>
                                                                            <div class="col-sm-12">
                                                                                <div class="col-sm-4  paddingLeft0">
                                                                                    Main Category
                                                                                </div>
                                                                                <div class="col-sm-6 padding0">
                                                                                    <asp:TextBox ID="txtCate1" runat="server" CssClass="form-control" AutoPostBack="true" OnTextChanged="txtCate1_TextChanged" />
                                                                                </div>
                                                                                <div class="col-sm-1">
                                                                                    <asp:LinkButton ID="btnsrhpbcat1" runat="server" CssClass="floatRight" OnClick="btnsrhpbcat1_Click">
                                                                                            <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                                                    </asp:LinkButton>
                                                                                </div>
                                                                            </div>
                                                                            <div class="col-sm-12 height5"></div>
                                                                            <div class="col-sm-12">
                                                                                <div class="col-sm-4  paddingLeft0">
                                                                                    Category
                                                                                </div>
                                                                                <div class="col-sm-6 padding0">
                                                                                    <asp:TextBox ID="txtCate2" runat="server" CssClass="form-control" AutoPostBack="true" OnTextChanged="txtCate2_TextChanged" />
                                                                                </div>
                                                                                <div class="col-sm-1">
                                                                                    <asp:LinkButton ID="btnsrhpbcat2" runat="server" CssClass="floatRight" OnClick="btnsrhpbcat2_Click">
                                                                                            <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                                                    </asp:LinkButton>
                                                                                </div>
                                                                            </div>
                                                                            <div class="col-sm-12 height5"></div>
                                                                            <div class="col-sm-12">
                                                                                <div class="col-sm-4  paddingLeft0">
                                                                                    Sub Category
                                                                                </div>
                                                                                <div class="col-sm-6 padding0">
                                                                                    <asp:TextBox ID="txtCate3" runat="server" CssClass="form-control" AutoPostBack="true" OnTextChanged="txtCate3_TextChanged" />
                                                                                </div>
                                                                                <div class="col-sm-1">
                                                                                    <asp:LinkButton ID="btnsrhpbcat3" runat="server" CssClass="floatRight" OnClick="btnsrhpbcat3_Click">
                                                                                            <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                                                    </asp:LinkButton>
                                                                                </div>
                                                                            </div>
                                                                            <div class="col-sm-12 height5"></div>
                                                                            <div class="col-sm-12">
                                                                                <div class="col-sm-4  paddingLeft0">
                                                                                    Item
                                                                                </div>
                                                                                <div class="col-sm-6 padding0">
                                                                                    <asp:TextBox ID="txtBaseItem" Style="text-transform: uppercase" runat="server" CssClass="form-control" AutoPostBack="true" OnTextChanged="txtBaseItem_TextChanged" />
                                                                                </div>
                                                                                <div class="col-sm-1">
                                                                                    <asp:LinkButton ID="btnItem" runat="server" CssClass="floatRight" OnClick="btnItem_Click">
                                                                                            <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                                                    </asp:LinkButton>
                                                                                </div>
                                                                            </div>
                                                                            <div class="col-sm-12 height5"></div>
                                                                            <div class="col-sm-12">
                                                                                <div class="col-sm-4  paddingLeft0">
                                                                                    Charge
                                                                                </div>
                                                                                <div class="col-sm-6 padding0">
                                                                                    <asp:TextBox ID="txtCharge" runat="server" CssClass="form-control" AutoPostBack="true" OnTextChanged="txtCharge_TextChanged" />
                                                                                </div>
                                                                                <div class="col-sm-1">
                                                                                    <asp:LinkButton ID="btnItemCharge" runat="server" CssClass="floatRight" OnClick="btnItemCharge_Click">
                                                                                            <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                                                    </asp:LinkButton>
                                                                                </div>
                                                                            </div>
                                                                            <div class="col-sm-12 height5"></div>
                                                                            <div class="col-sm-12">
                                                                                <div class="col-sm-4  paddingLeft0">
                                                                                    Margin
                                                                                </div>
                                                                                <div class="col-sm-6 padding0">
                                                                                    <asp:TextBox ID="txtMarkup" onkeydown="return jsDecimals(event);" Style="text-align: right" runat="server" CssClass="form-control" AutoPostBack="true" OnTextChanged="txtMarkup_TextChanged" />
                                                                                </div>
                                                                                <div class="col-sm-1 padding0">
                                                                                    %
                                                                                </div>
                                                                            </div>
                                                                        </ContentTemplate>
                                                                    </asp:UpdatePanel>
                                                                </div>
                                                                <div class="col-sm-7">
                                                                    <div class="col-sm-12" style="height: 178px;">
                                                                        <asp:GridView ID="grvSalesTypes" AutoGenerateColumns="false" runat="server" CssClass="table table-hover table-striped" GridLines="None" ShowHeaderWhenEmpty="True" EmptyDataText="No data found...">
                                                                            <Columns>
                                                                                <asp:TemplateField HeaderText=" " Visible="false">
                                                                                    <ItemTemplate>
                                                                                        <asp:CheckBox ID="chkSelectPC" Text=" " runat="server" />
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="Item">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblSpdd_item" runat="server" Text='<%# Bind("Spdd_item") %>'></asp:Label>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:BoundField DataField="Spdd_item" HeaderText="Item" Visible="false" />
                                                                                <asp:BoundField DataField="Spdd_brand" HeaderText="Brand" />
                                                                                <asp:BoundField DataField="Spdd_Des" HeaderText="Description" Visible="false" />
                                                                                <asp:BoundField DataField="Spdd_margin" DataFormatString="{0:N2}" HeaderText="Margin" />
                                                                                <asp:BoundField DataField="Spdd_ch_code" HeaderText="Charge" />
                                                                                <asp:TemplateField HeaderText=" ">
                                                                                    <ItemTemplate>
                                                                                        <asp:LinkButton ID="btnDeleteSalesTypes" runat="server" CssClass="floatleft" OnClientClick="return ConfirmDelProf()" OnClick="btnDeleteSalesTypes_Click">
                                                                                                    <span class="glyphicon glyphicon-remove" aria-hidden="true"></span>
                                                                                        </asp:LinkButton>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="active" Visible="false">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblSpdd_active" runat="server" Text='<%# Bind("Spdd_active") %>'></asp:Label>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                            </Columns>
                                                                        </asp:GridView>
                                                                    </div>
                                                                    <div class="col-sm-12">
                                                                        <div class="col-sm-3">
                                                                            <asp:Button ID="btnAllpb" Text="All" runat="server" OnClick="btnAllpb_Click" Width="80px" Visible="false" />
                                                                        </div>
                                                                        <div class="col-sm-3">
                                                                            <asp:Button ID="btnAllnone" Text="None" runat="server" OnClick="btnAllnone_Click" Width="80px" Visible="false" />
                                                                        </div>
                                                                        <div class="col-sm-3">
                                                                            <asp:Button ID="btnAllclr" Text="Clear" runat="server" OnClick="btnAllclr_Click" Width="80px" />
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:PostBackTrigger ControlID="lbtnExcelUpload" />
                                    </Triggers>
                                </asp:UpdatePanel>
                            </div>
                            <div role="tabpanel" class="tab-pane" id="PriceClone">
                                <asp:ScriptManagerProxy ID="ScriptManagerProxy6" runat="server"></asp:ScriptManagerProxy>
                                <asp:UpdatePanel runat="server">
                                    <ContentTemplate>
                                        <div class="col-sm-12">
                                            <div class="row">
                                                <div class="col-sm-12 buttonRow">
                                                    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                                        <ContentTemplate>
                                                            <div class="col-sm-9 ">
                                                            </div>
                                                            <div class="col-sm-1 padding0">
                                                                <asp:LinkButton ID="btnAgingSave" CausesValidation="false" runat="server" CssClass="floatRight" OnClientClick="return ConfirmSave();" OnClick="btnAgingSave_Click">
                                                                        <span class="glyphicon glyphicon-saved" aria-hidden="true" ></span>Save
                                                                </asp:LinkButton>
                                                            </div>
                                                            <div class="col-sm-1 padding0">
                                                                <asp:LinkButton ID="btnAgeView" CausesValidation="false" runat="server" CssClass="floatRight" OnClick="btnAgeView_Click">
                                                                        <span class="glyphicon glyphicon-eye-open" aria-hidden="true" ></span>View
                                                                </asp:LinkButton>
                                                            </div>
                                                            <div class="col-sm-1 padding0">
                                                                <asp:LinkButton ID="btnAgingClear" CausesValidation="false" runat="server" CssClass="floatRight" OnClientClick="return ConfirmClearForm();" OnClick="btnAgingClear_Click">
                                                                        <span class="glyphicon glyphicon-refresh" aria-hidden="true" ></span>Clear
                                                                </asp:LinkButton>
                                                            </div>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </div>
                                                <div class="col-sm-12 height5"></div>
                                                <div class="col-sm-12">
                                                    <div class="panel panel-default ">
                                                        <div class="panel-body">
                                                            <div class="col-sm-6">
                                                                <div class="col-sm-12">
                                                                    <div class="col-sm-5  paddingLeft0">
                                                                        Original Price Book
                                                                    </div>
                                                                    <div class="col-sm-6 padding0">
                                                                        <asp:TextBox ID="txtAgeOriPb" Style="text-transform: uppercase" runat="server" CssClass="form-control" AutoPostBack="true" OnTextChanged="txtAgeOriPb_TextChanged" />
                                                                    </div>
                                                                    <div class="col-sm-1 paddingLeft0">
                                                                        <asp:LinkButton ID="btnAgeOriPb" runat="server" CssClass="floatRight" OnClick="btnAgeOriPb_Click">
                                                                                            <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                                        </asp:LinkButton>
                                                                    </div>
                                                                </div>
                                                                <div class="col-sm-12 height5"></div>
                                                                <div class="col-sm-12">
                                                                    <div class="col-sm-5  paddingLeft0">
                                                                        Original  Price Level
                                                                    </div>
                                                                    <div class="col-sm-6 padding0">
                                                                        <asp:TextBox ID="txtAgeOriPlevel" Style="text-transform: uppercase" runat="server" CssClass="form-control" AutoPostBack="true" OnTextChanged="txtAgeOriPlevel_TextChanged" />
                                                                    </div>
                                                                    <div class="col-sm-1 paddingLeft0">
                                                                        <asp:LinkButton ID="btnAgeOriPlevl" runat="server" CssClass="floatRight" OnClick="btnAgeOriPlevl_Click">
                                                                                            <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                                        </asp:LinkButton>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="col-sm-6">
                                                                <div class="col-sm-12">
                                                                    <div class="col-sm-5  paddingLeft0">
                                                                        Clone Price Book
                                                                    </div>
                                                                    <div class="col-sm-6 padding0">
                                                                        <asp:TextBox ID="txtAgeCloPb" Style="text-transform: uppercase" runat="server" CssClass="form-control" AutoPostBack="true" OnTextChanged="txtAgeCloPb_TextChanged" />
                                                                    </div>
                                                                    <div class="col-sm-1 paddingLeft0">
                                                                        <asp:LinkButton ID="btnAgeCloPb" runat="server" CssClass="floatRight" OnClick="btnAgeCloPb_Click">
                                                                                            <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                                        </asp:LinkButton>
                                                                    </div>
                                                                </div>
                                                                <div class="col-sm-12 height5"></div>
                                                                <div class="col-sm-12">
                                                                    <div class="col-sm-5  paddingLeft0">
                                                                        Clone Price Level
                                                                    </div>
                                                                    <div class="col-sm-6 padding0">
                                                                        <asp:TextBox ID="txtAgeCloPlevl" Style="text-transform: uppercase" runat="server" CssClass="form-control" AutoPostBack="true" OnTextChanged="txtAgeCloPlevl_TextChanged" />
                                                                    </div>
                                                                    <div class="col-sm-1 paddingLeft0">
                                                                        <asp:LinkButton ID="btnAgeCloPlevel" runat="server" CssClass="floatRight" OnClick="btnAgeCloPlevel_Click">
                                                                                            <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                                        </asp:LinkButton>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-sm-12 height5"></div>
                                                <div class="col-sm-12">
                                                    <div class="panel panel-default ">
                                                        <div class="panel-heading pannelheading height16 paddingtop0">
                                                            <b>Item Details</b>
                                                        </div>
                                                        <div class="panel-body">
                                                            <div class="col-sm-8 paddingLeft0">
                                                                <asp:UpdateProgress ID="upaddItemsasd3" DisplayAfter="0" runat="server" AssociatedUpdatePanelID="upaddItems">
                                                                    <ProgressTemplate>
                                                                        <div class="divWaiting">
                                                                            <asp:Label ID="lblWaitupaddItems" runat="server"
                                                                                Text="Please wait... " />
                                                                            <asp:Image ID="imgWaitupaddItems" runat="server"
                                                                                ImageAlign="Middle" ImageUrl="~/images/icons/ajax-loader.gif" />
                                                                        </div>
                                                                    </ProgressTemplate>
                                                                </asp:UpdateProgress>
                                                                <asp:UpdatePanel ID="upaddItems" runat="server">
                                                                    <ContentTemplate>
                                                                        <div class="panel panel-default ">
                                                                            <div class="panel-body padding0">
                                                                                <div class="col-sm-5 paddingLeft0">
                                                                                    <div class="col-sm-12">
                                                                                        <div class="col-sm-5  paddingLeft0">
                                                                                            Brand
                                                                                        </div>
                                                                                        <div class="col-sm-7 padding0">
                                                                                            <div class="col-sm-10 padding0">
                                                                                                <asp:TextBox ID="txtAgeBrand" runat="server" CssClass="form-control" AutoPostBack="true" OnTextChanged="txtAgeBrand_TextChanged" />
                                                                                            </div>
                                                                                            <div class="col-sm-2 padding0">
                                                                                                <asp:LinkButton ID="btnBrand" runat="server" CssClass="floatRight" OnClick="btnBrand_Click">
                                                                                            <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                                                                </asp:LinkButton>
                                                                                            </div>
                                                                                        </div>
                                                                                    </div>
                                                                                    <div class="col-sm-12 height5"></div>
                                                                                    <div class="col-sm-12">
                                                                                        <div class="col-sm-5  paddingLeft0">
                                                                                            Main Category
                                                                                        </div>
                                                                                        <div class="col-sm-7 padding0">
                                                                                            <div class="col-sm-10 padding0">
                                                                                                <asp:TextBox ID="txtAgeCate1" runat="server" CssClass="form-control" AutoPostBack="true" OnTextChanged="txtAgeCate1_TextChanged" />
                                                                                            </div>
                                                                                            <div class="col-sm-2 padding0">
                                                                                                <asp:LinkButton ID="btnMainCat" runat="server" CssClass="floatRight" OnClick="btnMainCat_Click">
                                                                                            <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                                                                </asp:LinkButton>
                                                                                            </div>
                                                                                        </div>
                                                                                    </div>
                                                                                    <div class="col-sm-12 height5"></div>
                                                                                    <div class="col-sm-12">
                                                                                        <div class="col-sm-5  paddingLeft0">
                                                                                            Category
                                                                                        </div>
                                                                                        <div class="col-sm-7 padding0">
                                                                                            <div class="col-sm-10 padding0">
                                                                                                <asp:TextBox ID="txtAgeCate2" runat="server" CssClass="form-control" AutoPostBack="true" OnTextChanged="txtAgeCate2_TextChanged" />
                                                                                            </div>
                                                                                            <div class="col-sm-2 padding0">
                                                                                                <asp:LinkButton ID="btnCat" runat="server" CssClass="floatRight" OnClick="btnCat_Click">
                                                                                            <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                                                                </asp:LinkButton>
                                                                                            </div>
                                                                                        </div>
                                                                                    </div>
                                                                                    <div class="col-sm-12 height5"></div>
                                                                                    <div class="col-sm-12">
                                                                                        <div class="col-sm-5  paddingLeft0">
                                                                                            Item
                                                                                        </div>
                                                                                        <div class="col-sm-7 padding0">
                                                                                            <div class="col-sm-10 padding0">
                                                                                                <asp:TextBox ID="txtItemCD" runat="server" CssClass="form-control" AutoPostBack="true" OnTextChanged="txtItemCD_TextChanged" />
                                                                                            </div>
                                                                                            <div class="col-sm-2 padding0">
                                                                                                <asp:LinkButton ID="btnItemCode" runat="server" CssClass="floatRight" OnClick="btnItemCode_Click">
                                                                                            <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                                                                </asp:LinkButton>
                                                                                            </div>
                                                                                        </div>
                                                                                    </div>
                                                                                    <div class="col-sm-12 height5"></div>
                                                                                    <div class="col-sm-12">
                                                                                        <asp:Button ID="btnAddItems" Text="Add Items" runat="server" OnClick="btnAddItems_Click" />
                                                                                    </div>
                                                                                </div>
                                                                                <div class="col-sm-5 padding0">
                                                                                    <div class="col-sm-12 GridScroll padding0">
                                                                                        <asp:GridView ID="grvItemList" AutoGenerateColumns="false" runat="server" CssClass="table table-hover table-striped" GridLines="None" ShowHeaderWhenEmpty="True" EmptyDataText="No data found...">
                                                                                            <Columns>
                                                                                                <asp:TemplateField HeaderText=" ">
                                                                                                    <ItemTemplate>
                                                                                                        <asp:CheckBox ID="chkSelectPC" Text=" " runat="server" />
                                                                                                        <asp:LinkButton ID="btnDelgrvItemList" runat="server" CssClass="floatleft" OnClientClick="return ConfirmDelProf()" OnClick="btnDelgrvItemList_Click" Visible="false">
                                                                                                    <span class="glyphicon glyphicon-remove" aria-hidden="true"></span>
                                                                                                        </asp:LinkButton>
                                                                                                    </ItemTemplate>
                                                                                                </asp:TemplateField>
                                                                                                <asp:BoundField DataField="code" HeaderText="Item Code" />
                                                                                                <asp:BoundField DataField="descript" HeaderText="Description" />
                                                                                            </Columns>
                                                                                        </asp:GridView>
                                                                                    </div>
                                                                                </div>
                                                                                <div class="col-sm-2 padding0" style="text-align: center">
                                                                                    <div class="col-sm-12">
                                                                                        <asp:Button ID="btnItemClear" Text="Clear" runat="server" OnClick="btnItemClear_Click" Width="80px" />
                                                                                    </div>
                                                                                    <div class="col-sm-12 height5"></div>
                                                                                    <div class="col-sm-12">
                                                                                        <asp:Button ID="btnItemNone" Text="None" runat="server" OnClick="btnItemNone_Click" Width="80px" />
                                                                                    </div>
                                                                                    <div class="col-sm-12 height5"></div>
                                                                                    <div class="col-sm-12">
                                                                                        <asp:Button ID="btnItemAll" Text="All" runat="server" OnClick="btnItemAll_Click" Width="80px" />
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                    </ContentTemplate>
                                                                </asp:UpdatePanel>
                                                            </div>
                                                            <div class="col-sm-4">
                                                                <div class="col-sm-12">
                                                                    <div class="col-sm-5  paddingLeft0">
                                                                        Circular
                                                                    </div>
                                                                    <div class="col-sm-6 padding0">
                                                                        <asp:TextBox ID="txtAgeCircular" Style="text-transform: uppercase" runat="server" CssClass="form-control" AutoPostBack="true" OnTextChanged="txtAgeCircular_TextChanged" />
                                                                    </div>
                                                                    <div class="col-sm-1 paddingLeft0">
                                                                    </div>
                                                                </div>
                                                                <div class="col-sm-12 height5"></div>
                                                                <div class="col-sm-12">
                                                                    <div class="col-sm-5  paddingLeft0">
                                                                        Discount Rate
                                                                    </div>
                                                                    <div class="col-sm-6 padding0">
                                                                        <asp:TextBox ID="txtAgeDisc" onkeydown="return jsDecimals(event);" runat="server" CssClass="form-control" />
                                                                    </div>
                                                                    <div class="col-sm-1 paddingLeft0">
                                                                    </div>
                                                                </div>
                                                                <div class="col-sm-12 height5"></div>
                                                                <div class="col-sm-12">
                                                                    <div class="col-sm-5  paddingLeft0">
                                                                        From
                                                                    </div>
                                                                    <div class="col-sm-6 padding0">
                                                                        <asp:TextBox ID="dtAgFrom" runat="server" CssClass="form-control" ReadOnly="true" />
                                                                        <asp:CalendarExtender ID="dtAgFromCal" runat="server" TargetControlID="dtAgFrom"
                                                                            PopupButtonID="dtAgFrombtn" Format="dd/MMM/yyyy">
                                                                        </asp:CalendarExtender>
                                                                    </div>
                                                                    <div class="col-sm-1 paddingLeft0">
                                                                        <asp:LinkButton ID="dtAgFrombtn" CausesValidation="false" runat="server">
                                                                        <span class="glyphicon glyphicon-calendar" aria-hidden="true"></span>
                                                                        </asp:LinkButton>
                                                                    </div>
                                                                </div>
                                                                <div class="col-sm-12 height5"></div>
                                                                <div class="col-sm-12">
                                                                    <div class="col-sm-5  paddingLeft0">
                                                                        To
                                                                    </div>
                                                                    <div class="col-sm-6 padding0">
                                                                        <asp:TextBox ID="dtAgTo" runat="server" CssClass="form-control" ReadOnly="true" />
                                                                        <asp:CalendarExtender ID="dtAgToCal" runat="server" TargetControlID="dtAgTo"
                                                                            PopupButtonID="dtAgTobtn" Format="dd/MMM/yyyy">
                                                                        </asp:CalendarExtender>
                                                                    </div>
                                                                    <div class="col-sm-1 paddingLeft0">
                                                                        <asp:LinkButton ID="dtAgTobtn" CausesValidation="false" runat="server">
                                                                        <span class="glyphicon glyphicon-calendar" aria-hidden="true"></span>
                                                                        </asp:LinkButton>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-sm-12 height5"></div>
                                                <div class="col-sm-12">
                                                    <div class="col-sm-1">
                                                        Upload
                                                    </div>
                                                    <div class="col-sm-6">
                                                        <asp:FileUpload ID="FileUpload2" runat="server" />
                                                    </div>
                                                    <div class="col-sm-3">
                                                        <asp:LinkButton ID="btnAgeUploadNew" runat="server" OnClick="btnAgeUpload_Click">
                                                             <span class="glyphicon glyphicon-upload" aria-hidden="true"></span>Excel Upload
                                                        </asp:LinkButton>
                                                    </div>
                                                </div>
                                                <div class="col-sm-12 height5"></div>
                                                <div class="col-sm-12">
                                                    <div class="panel panel-default ">
                                                        <div class="panel-body">
                                                            <asp:GridView ID="gvPreview" AutoGenerateColumns="false" runat="server" CssClass="table table-hover table-striped" GridLines="None" ShowHeaderWhenEmpty="True" EmptyDataText="No data found...">
                                                                <Columns>
                                                                    <asp:TemplateField HeaderText=" " Visible="false">
                                                                        <ItemTemplate>
                                                                            <asp:CheckBox ID="chkSelectPC" Text=" " runat="server" />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:BoundField DataField="Sapd_pb_tp_cd" HeaderText="PB" />
                                                                    <asp:BoundField DataField="Sapd_pbk_lvl_cd" HeaderText="Plevel" />
                                                                    <asp:BoundField DataField="SAPD_ITM_CD" HeaderText="Item" />
                                                                    <asp:BoundField DataField="Sapd_avg_cost" DataFormatString="{0:n}" HeaderText="Orig. Price" />
                                                                    <asp:BoundField DataField="SAPD_ITM_PRICE" DataFormatString="{0:n}" HeaderText="New Price" />
                                                                    <asp:BoundField DataField="Sapd_cre_by" HeaderText="Cre By" />
                                                                    <asp:TemplateField HeaderText=" " Visible="false">
                                                                        <ItemTemplate>
                                                                            <asp:LinkButton ID="btnRemoveProfitCenter" runat="server" CssClass="floatleft" OnClientClick="return ConfirmDelProf()">
                                                                                                    <span class="glyphicon glyphicon-remove" aria-hidden="true"></span>
                                                                            </asp:LinkButton>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                </Columns>
                                                            </asp:GridView>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:PostBackTrigger ControlID="btnAgeUploadNew" />
                                    </Triggers>
                                </asp:UpdatePanel>
                            </div>
                            <div role="tabpanel" class="tab-pane" id="SimilarItem">

                                <asp:UpdateProgress DisplayAfter="0" runat="server" AssociatedUpdatePanelID="upMainITem">
                                    <ProgressTemplate>
                                        <div class="divWaiting">
                                            <asp:Label runat="server"
                                                Text="Please wait... " />
                                            <asp:Image runat="server"
                                                ImageAlign="Middle" ImageUrl="~/images/icons/ajax-loader.gif" />
                                        </div>
                                    </ProgressTemplate>
                                </asp:UpdateProgress>

                                <asp:UpdateProgress DisplayAfter="0" runat="server" AssociatedUpdatePanelID="UpdatePanel4">
                                    <ProgressTemplate>
                                        <div class="divWaiting">
                                            <asp:Label runat="server"
                                                Text="Please wait... " />
                                            <asp:Image runat="server"
                                                ImageAlign="Middle" ImageUrl="~/images/icons/ajax-loader.gif" />
                                        </div>
                                    </ProgressTemplate>
                                </asp:UpdateProgress>

                                <asp:UpdateProgress DisplayAfter="0" runat="server" AssociatedUpdatePanelID="upApplicablesimilaritems">
                                    <ProgressTemplate>
                                        <div class="divWaiting">
                                            <asp:Label runat="server"
                                                Text="Please wait... " />
                                            <asp:Image runat="server"
                                                ImageAlign="Middle" ImageUrl="~/images/icons/ajax-loader.gif" />
                                        </div>
                                    </ProgressTemplate>
                                </asp:UpdateProgress>

                                <asp:UpdateProgress DisplayAfter="0" runat="server" AssociatedUpdatePanelID="UPProfitcenterallocation">
                                    <ProgressTemplate>
                                        <div class="divWaiting">
                                            <asp:Label runat="server"
                                                Text="Please wait... " />
                                            <asp:Image runat="server"
                                                ImageAlign="Middle" ImageUrl="~/images/icons/ajax-loader.gif" />
                                        </div>
                                    </ProgressTemplate>
                                </asp:UpdateProgress>

                                <asp:UpdateProgress DisplayAfter="0" runat="server" AssociatedUpdatePanelID="UpPromotioncodes">
                                    <ProgressTemplate>
                                        <div class="divWaiting">
                                            <asp:Label runat="server"
                                                Text="Please wait... " />
                                            <asp:Image runat="server"
                                                ImageAlign="Middle" ImageUrl="~/images/icons/ajax-loader.gif" />
                                        </div>
                                    </ProgressTemplate>
                                </asp:UpdateProgress>

                                <asp:ScriptManagerProxy ID="ScriptManagerProxy7" runat="server"></asp:ScriptManagerProxy>
                                <asp:UpdatePanel runat="server">
                                    <ContentTemplate>
                                        <div class="row">
                                            <div class="col-sm-12">
                                                <div class="panel panel-default ">
                                                    <div class="panel-heading pannelheading height16 paddingtop0">
                                                        <b>Similar Item Definition</b>
                                                    </div>
                                                    <div class="panel-body">
                                                        <div class="col-sm-12 buttonRow">
                                                            <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                                                <ContentTemplate>
                                                                    <div class="col-sm-10 ">
                                                                    </div>
                                                                    <div class="col-sm-1 padding0">
                                                                        <asp:LinkButton ID="btnSaveSimilar" CausesValidation="false" runat="server" CssClass="floatRight" OnClientClick="return ConfirmSave();" OnClick="btnSaveSimilar_Click">
                                                                        <span class="glyphicon glyphicon-saved" aria-hidden="true" ></span>Save
                                                                        </asp:LinkButton>
                                                                    </div>
                                                                    <div class="col-sm-1 padding0">
                                                                        <asp:LinkButton ID="btnClearSimilar" CausesValidation="false" runat="server" CssClass="floatRight" OnClientClick="return ConfirmClearForm();" OnClick="btnClearSimilar_Click">
                                                                        <span class="glyphicon glyphicon-refresh" aria-hidden="true" ></span>Clear
                                                                        </asp:LinkButton>
                                                                    </div>
                                                                </ContentTemplate>
                                                            </asp:UpdatePanel>
                                                        </div>
                                                        <div class="col-sm-4 paddingLeft0">
                                                            <div class="panel panel-default ">
                                                                <asp:UpdatePanel ID="upMainITem" runat="server">
                                                                    <ContentTemplate>
                                                                        <div class="panel-heading pannelheading height16 paddingtop0">
                                                                            <b>Main Item</b>
                                                                        </div>
                                                                        <div class="panel-body">
                                                                            <div class="col-sm-11 padding0">
                                                                                <div class="col-sm-12 padding0">
                                                                                    <div class="col-sm-3 paddingLeft0">
                                                                                        Main Item
                                                                                    </div>
                                                                                    <div class="col-sm-6 padding0">
                                                                                        <asp:TextBox ID="txtMainItem" runat="server" CssClass="form-control" AutoPostBack="true" OnTextChanged="txtMainItem_TextChanged" Style="text-transform: uppercase" />
                                                                                    </div>
                                                                                    <div class="col-sm-1">
                                                                                        <asp:LinkButton ID="btnSearchMainItem" runat="server" CssClass="floatRight" OnClick="btnSearchMainItem_Click">
                                                                                            <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                                                        </asp:LinkButton>
                                                                                    </div>
                                                                                </div>
                                                                                <div class="col-sm-12 height5"></div>
                                                                                <div class="col-sm-12 padding0">
                                                                                    <div class="col-sm-3 paddingLeft0">
                                                                                        Model
                                                                                    </div>
                                                                                    <div class="col-sm-6 padding0">
                                                                                        <asp:TextBox ID="lblMainModel" runat="server" CssClass="form-control" AutoPostBack="true" ReadOnly="true" />
                                                                                    </div>
                                                                                    <div class="col-sm-1">
                                                                                    </div>
                                                                                </div>
                                                                                <div class="col-sm-12 height5"></div>
                                                                                <div class="col-sm-12 padding0">
                                                                                    <div class="col-sm-3 paddingLeft0">
                                                                                        Description
                                                                                    </div>
                                                                                    <div class="col-sm-6 padding0">
                                                                                        <asp:TextBox ID="lblMainDesc" runat="server" CssClass="form-control" AutoPostBack="true" ReadOnly="true" />
                                                                                    </div>
                                                                                    <div class="col-sm-1">
                                                                                    </div>
                                                                                </div>
                                                                                <div class="col-sm-12 height5"></div>
                                                                                <div class="col-sm-12 padding0">
                                                                                    <div class="col-sm-3 paddingLeft0">
                                                                                        Valid From
                                                                                    </div>
                                                                                    <div class="col-sm-6 padding0">
                                                                                        <asp:TextBox ID="dtpSFrom" runat="server" CssClass="form-control" AutoPostBack="true" ReadOnly="true" />
                                                                                    </div>
                                                                                    <div class="col-sm-1">
                                                                                        <asp:LinkButton ID="dtpSFrombtn" runat="server" CssClass="floatRight">
                                                                                            <span class="glyphicon glyphicon-calendar" aria-hidden="true"></span>
                                                                                        </asp:LinkButton>
                                                                                        <asp:CalendarExtender ID="dtpSFromcal" runat="server" TargetControlID="dtpSFrom"
                                                                                            PopupButtonID="dtpSFrombtn" Format="dd/MMM/yyyy">
                                                                                        </asp:CalendarExtender>
                                                                                    </div>
                                                                                </div>
                                                                                <div class="col-sm-12 height5"></div>
                                                                                <div class="col-sm-12 padding0">
                                                                                    <div class="col-sm-3 paddingLeft0">
                                                                                        To
                                                                                    </div>
                                                                                    <div class="col-sm-6 padding0">
                                                                                        <asp:TextBox ID="dtpSTo" runat="server" CssClass="form-control" AutoPostBack="true" ReadOnly="true" />
                                                                                    </div>
                                                                                    <div class="col-sm-1">
                                                                                        <asp:LinkButton ID="dtpSTobtn" runat="server" CssClass="floatRight">
                                                                                            <span class="glyphicon glyphicon-calendar" aria-hidden="true"></span>
                                                                                        </asp:LinkButton>
                                                                                        <asp:CalendarExtender ID="dtpSToCal" runat="server" TargetControlID="dtpSTo"
                                                                                            PopupButtonID="dtpSTobtn" Format="dd/MMM/yyyy">
                                                                                        </asp:CalendarExtender>
                                                                                    </div>
                                                                                </div>
                                                                                <div class="col-sm-12 height5"></div>
                                                                                <div class="col-sm-12 padding0">
                                                                                    <div class="col-sm-3 paddingLeft0">
                                                                                        Invoice No
                                                                                    </div>
                                                                                    <div class="col-sm-6 padding0">
                                                                                        <asp:TextBox ID="txtInvoiceNo" runat="server" CssClass="form-control" AutoPostBack="true" OnTextChanged="txtInvoiceNo_TextChanged" />
                                                                                    </div>
                                                                                    <div class="col-sm-1">
                                                                                        <asp:LinkButton ID="btnSearchInvoice" runat="server" CssClass="floatRight" OnClick="btnSearchInvoice_Click">
                                                                                            <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                                                        </asp:LinkButton>
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                            <div class="col-sm-1">
                                                                                <asp:LinkButton ID="btnSimHis" runat="server" CssClass="floatRight" OnClick="btnSimHis_Click">
                                                                            <span class="glyphicon glyphicon-collapse-down" aria-hidden="true">Load</span>
                                                                                </asp:LinkButton>
                                                                            </div>
                                                                        </div>
                                                                    </ContentTemplate>
                                                                </asp:UpdatePanel>
                                                            </div>
                                                        </div>
                                                        <div class="col-sm-8 padding0">
                                                            <div class="panel panel-default ">
                                                                <asp:UpdatePanel ID="upApplicablesimilaritems" runat="server">
                                                                    <ContentTemplate>
                                                                        <div class="panel-heading pannelheading height16 paddingtop0">
                                                                            <b>Applicable Similar Items</b>
                                                                        </div>
                                                                        <div class="panel-body">
                                                                            <div class="col-sm-4 padding0">
                                                                                <div class="col-sm-12">
                                                                                    <div class="col-sm-4 paddingLeft0">
                                                                                        Main Cate
                                                                                    </div>
                                                                                    <div class="col-sm-6 padding0">
                                                                                        <asp:TextBox ID="txtMainCate" runat="server" CssClass="form-control" AutoPostBack="true" OnTextChanged="txtMainCate_TextChanged" />
                                                                                    </div>
                                                                                    <div class="col-sm-2">
                                                                                        <asp:LinkButton ID="btnSearchMainCate" runat="server" CssClass="floatRight" OnClick="btnSearchMainCate_Click">
                                                                                            <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                                                        </asp:LinkButton>
                                                                                    </div>
                                                                                </div>
                                                                                <div class="col-sm-12 height5"></div>
                                                                                <div class="col-sm-12">
                                                                                    <div class="col-sm-4 paddingLeft0">
                                                                                        Sub Cate
                                                                                    </div>
                                                                                    <div class="col-sm-6 padding0">
                                                                                        <asp:TextBox ID="txtSubCate" runat="server" CssClass="form-control" AutoPostBack="true" OnTextChanged="txtSubCate_TextChanged" />
                                                                                    </div>
                                                                                    <div class="col-sm-2">
                                                                                        <asp:LinkButton ID="btnSearchSubCate" runat="server" CssClass="floatRight" OnClick="btnSearchSubCate_Click">
                                                                                            <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                                                        </asp:LinkButton>
                                                                                    </div>
                                                                                </div>
                                                                                <div class="col-sm-12 height5"></div>
                                                                                <div class="col-sm-12">
                                                                                    <div class="col-sm-4 paddingLeft0">
                                                                                        Range
                                                                                    </div>
                                                                                    <div class="col-sm-6 padding0">
                                                                                        <asp:TextBox ID="txtItemRange" runat="server" CssClass="form-control" AutoPostBack="true" OnTextChanged="txtItemRange_TextChanged" />
                                                                                    </div>
                                                                                    <div class="col-sm-2">
                                                                                        <asp:LinkButton ID="btnSearchRange" runat="server" CssClass="floatRight" OnClick="btnSearchRange_Click">
                                                                                            <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                                                        </asp:LinkButton>
                                                                                    </div>
                                                                                </div>
                                                                                <div class="col-sm-12 height5"></div>
                                                                                <div class="col-sm-12">
                                                                                    <div class="col-sm-4 paddingLeft0">
                                                                                        Brand
                                                                                    </div>
                                                                                    <div class="col-sm-6 padding0">
                                                                                        <asp:TextBox ID="txtBrand_SI" runat="server" CssClass="form-control" AutoPostBack="true" OnTextChanged="txtBrand_SI_TextChanged" />
                                                                                    </div>
                                                                                    <div class="col-sm-2">
                                                                                        <asp:LinkButton ID="btnSearchBrand" runat="server" CssClass="floatRight" OnClick="btnSearchBrand_Click">
                                                                                            <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                                                        </asp:LinkButton>
                                                                                    </div>
                                                                                </div>
                                                                                <div class="col-sm-12 height5"></div>
                                                                                <div class="col-sm-12">
                                                                                    <div class="col-sm-4 paddingLeft0">
                                                                                        Item
                                                                                    </div>
                                                                                    <div class="col-sm-6 padding0">
                                                                                        <asp:TextBox ID="txtItem" runat="server" CssClass="form-control" AutoPostBack="true" OnTextChanged="txtItem_TextChanged" />
                                                                                    </div>
                                                                                    <div class="col-sm-2">
                                                                                        <asp:LinkButton ID="btnSearchSimilarItem" runat="server" CssClass="floatRight" OnClick="btnSearchSimilarItem_Click">
                                                                                            <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                                                        </asp:LinkButton>
                                                                                    </div>
                                                                                </div>
                                                                                <div class="col-sm-12 height5"></div>
                                                                                <div class="col-sm-12">
                                                                                    <div class="col-sm-3 padding0">
                                                                                        <asp:Button ID="btnSelectAllItem" Text="All" runat="server" Width="100%" OnClick="btnSelectAllItem_Click" />
                                                                                    </div>
                                                                                    <div class="col-sm-3 padding0">
                                                                                        <asp:Button ID="btnUnselectItem" Text="None" runat="server" Width="100%" OnClick="btnUnselectItem_Click" />
                                                                                    </div>
                                                                                    <div class="col-sm-3 padding0">
                                                                                        <asp:Button ID="btnClearItem" Text="Clear" runat="server" Width="100%" OnClick="btnClearItem_Click" />
                                                                                    </div>
                                                                                    <div class="col-sm-3 padding0">
                                                                                        <asp:Button ID="btnLoadProducts" Text="Add" Width="100%" runat="server" OnClick="btnLoadProducts_Click" />
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                            <div class="col-sm-8 padding0">
                                                                                <div class="col-sm-12 GridScroll" style="height: 125px;">
                                                                                    <asp:CheckBoxList runat="server" ID="lstItem"
                                                                                        RepeatColumns="4"
                                                                                        RepeatDirection="Vertical"
                                                                                        RepeatLayout="Table"
                                                                                        TextAlign="Right"
                                                                                        ForeColor="#333"
                                                                                        Font-Bold="false"
                                                                                        CssClass="chkChoice">
                                                                                    </asp:CheckBoxList>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                    </ContentTemplate>
                                                                </asp:UpdatePanel>
                                                            </div>
                                                        </div>

                                                        <div class="col-sm-6 paddingLeft0">
                                                            <div class="panel panel-default ">
                                                                <asp:UpdatePanel ID="UPProfitcenterallocation" runat="server">
                                                                    <ContentTemplate>
                                                                        <div class="panel-heading pannelheading height16 paddingtop0">
                                                                            <b>Profit Center Allocation</b>
                                                                        </div>
                                                                        <div class="panel-body">
                                                                            <div class="col-sm-6 padding0">
                                                                                <div class="col-sm-12 padding0">
                                                                                    <div class="col-sm-3 paddingLeft0">
                                                                                        Channel
                                                                                    </div>
                                                                                    <div class="col-sm-7 padding0">
                                                                                        <asp:TextBox ID="txtSChannel" runat="server" CssClass="form-control" AutoPostBack="true" OnTextChanged="txtSChannel_TextChanged" />
                                                                                    </div>
                                                                                    <div class="col-sm-2">
                                                                                        <asp:LinkButton ID="btnSearchSChannel" runat="server" CssClass="floatRight" OnClick="btnSearchSChannel_Click">
                                                                                            <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                                                        </asp:LinkButton>
                                                                                    </div>
                                                                                </div>
                                                                                <div class="col-sm-12 height5"></div>
                                                                                <div class="col-sm-12 padding0">
                                                                                    <div class="col-sm-3 paddingLeft0">
                                                                                        Sub Channel
                                                                                    </div>
                                                                                    <div class="col-sm-7 padding0">
                                                                                        <asp:TextBox ID="txtSSubChannel" runat="server" CssClass="form-control" AutoPostBack="true" OnTextChanged="txtSSubChannel_TextChanged" />
                                                                                    </div>
                                                                                    <div class="col-sm-2">
                                                                                        <asp:LinkButton ID="btnSearchSSubChannel" runat="server" CssClass="floatRight" OnClick="btnSearchSSubChannel_Click">
                                                                                            <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                                                        </asp:LinkButton>
                                                                                    </div>
                                                                                </div>
                                                                                <div class="col-sm-12 height5"></div>
                                                                                <div class="col-sm-12 padding0">
                                                                                    <div class="col-sm-3 paddingLeft0">
                                                                                        Profit Center
                                                                                    </div>
                                                                                    <div class="col-sm-7 padding0">
                                                                                        <asp:TextBox ID="txtSPc" runat="server" CssClass="form-control" AutoPostBack="true" Style="text-transform: uppercase" OnTextChanged="txtSPc_TextChanged" />
                                                                                    </div>
                                                                                    <div class="col-sm-2">
                                                                                        <asp:LinkButton ID="btnSearchSPc" runat="server" CssClass="floatRight" OnClick="btnSearchSPc_Click">
                                                                                            <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                                                        </asp:LinkButton>
                                                                                    </div>
                                                                                </div>
                                                                                <div class="col-sm-12 height5"></div>
                                                                                <div class="col-sm-12 padding0">
                                                                                    <div class="col-sm-3 padding0">
                                                                                        <asp:Button ID="btnSSelectAll" Text="Select All" runat="server" Width="100%" OnClick="btnSSelectAll_Click" />
                                                                                    </div>
                                                                                    <div class="col-sm-3 padding0">
                                                                                        <asp:Button ID="btnSUnselect" Text="Unselect" runat="server" Width="100%" OnClick="btnSUnselect_Click" />
                                                                                    </div>
                                                                                    <div class="col-sm-3 padding0">
                                                                                        <asp:Button ID="btnSClear" Text="Clear" runat="server" Width="100%" OnClick="btnSClear_Click" />
                                                                                    </div>
                                                                                    <div class="col-sm-3 padding0">
                                                                                        <asp:Button ID="btnAddSPC" Text="Add" runat="server" Width="100%" OnClick="btnAddSPC_Click" />
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                            <div class="col-sm-6 padding0">
                                                                                <div class="col-sm-12 GridScroll" style="height: 93px;">
                                                                                    <asp:CheckBoxList runat="server" ID="lstSpc"
                                                                                        RepeatColumns="4"
                                                                                        RepeatDirection="Vertical"
                                                                                        RepeatLayout="Table"
                                                                                        TextAlign="Right"
                                                                                        ForeColor="#333"
                                                                                        Font-Bold="false"
                                                                                        CssClass="chkChoice">
                                                                                    </asp:CheckBoxList>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                    </ContentTemplate>
                                                                </asp:UpdatePanel>
                                                            </div>
                                                        </div>
                                                        <div class="col-sm-6 padding0">
                                                            <div class="panel panel-default ">
                                                                <asp:UpdatePanel ID="UpPromotioncodes" runat="server">
                                                                    <ContentTemplate>
                                                                        <div class="panel-heading pannelheading height16 paddingtop0">
                                                                            <b>Promotion codes</b>
                                                                        </div>
                                                                        <div class="panel-body">
                                                                            <div class="col-sm-6 padding0">
                                                                                <div class="col-sm-12 padding0">
                                                                                    <div class="col-sm-3 paddingLeft0">
                                                                                        Circular #
                                                                                    </div>
                                                                                    <div class="col-sm-7 padding0">
                                                                                        <asp:TextBox ID="txtSCir" runat="server" CssClass="form-control" AutoPostBack="true" OnTextChanged="txtSCir_TextChanged" />
                                                                                    </div>
                                                                                    <div class="col-sm-2">
                                                                                        <asp:LinkButton ID="btnSearchSCir" runat="server" CssClass="floatRight" OnClick="btnSearchSCir_Click">
                                                                                            <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                                                        </asp:LinkButton>
                                                                                    </div>
                                                                                </div>
                                                                                <div class="col-sm-12 height5"></div>
                                                                                <div class="col-sm-12 padding0">
                                                                                    <div class="col-sm-3 padding0">
                                                                                        Promotion Code
                                                                                    </div>
                                                                                    <div class="col-sm-7 padding0">
                                                                                        <asp:TextBox ID="txtSpromo" runat="server" CssClass="form-control" AutoPostBack="true" OnTextChanged="txtSpromo_TextChanged" />
                                                                                    </div>
                                                                                    <div class="col-sm-2">
                                                                                        <asp:LinkButton ID="btnSearchSPromo" runat="server" CssClass="floatRight" OnClick="btnSearchSPromo_Click">
                                                                                            <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                                                        </asp:LinkButton>
                                                                                    </div>
                                                                                </div>
                                                                                <div class="col-sm-12 height5"></div>
                                                                                <div class="col-sm-12 padding0">
                                                                                    <div class="col-sm-3 padding0">
                                                                                        <asp:Button ID="btnSPSelect" Text="Select All" runat="server" Width="100%" OnClick="btnSPSelect_Click" />
                                                                                    </div>
                                                                                    <div class="col-sm-3 padding0">
                                                                                        <asp:Button ID="btnSPUnselect" Text="Unselect" runat="server" Width="100%" OnClick="btnSPUnselect_Click" />
                                                                                    </div>
                                                                                    <div class="col-sm-3 padding0">
                                                                                        <asp:Button ID="btnSPClear" Text="Clear" runat="server" Width="100%" OnClick="btnSPClear_Click" />
                                                                                    </div>
                                                                                    <div class="col-sm-3 padding0">
                                                                                        <asp:Button ID="btnSPromoAdd" Text="Load" runat="server" Width="100%" OnClick="btnSPromoAdd_Click" />
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                            <div class="col-sm-6 padding0">
                                                                                <div class="col-sm-12 GridScroll" style="height: 93px;">
                                                                                    <asp:CheckBoxList runat="server" ID="lstsPromo"
                                                                                        RepeatColumns="3"
                                                                                        RepeatDirection="Vertical"
                                                                                        RepeatLayout="Table"
                                                                                        TextAlign="Right"
                                                                                        ForeColor="#333"
                                                                                        Font-Bold="false"
                                                                                        CssClass="chkChoice">
                                                                                    </asp:CheckBoxList>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                    </ContentTemplate>
                                                                </asp:UpdatePanel>
                                                            </div>
                                                        </div>
                                                        <div class="col-sm-12 paddingLeft0">
                                                            <div class="panel-heading pannelheading height16 paddingtop0">
                                                                <div class="col-sm-4 paddingLeft0">
                                                                    <b>Definition Details</b>
                                                                </div>
                                                                <div class="col-sm-8 paddingLeft0" style="float: right">
                                                                    <asp:Button ID="btnSimApply" Text="Apply" runat="server" Style="text-align: right; float: right" OnClick="btnSimApply_Click" />
                                                                </div>
                                                            </div>
                                                            <div class="panel-body">
                                                                <asp:GridView ID="dgvSimDet" AutoGenerateColumns="False" runat="server" CssClass="table table-hover table-striped" GridLines="None" ShowHeaderWhenEmpty="True" EmptyDataText="No data found...">
                                                                    <Columns>
                                                                        <asp:TemplateField HeaderText="Company">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="Label1" runat="server" Text='<%# Bind("misi_com") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Similar Item">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="Label2" runat="server" Text='<%# Bind("misi_sim_itm_cd") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="From Date">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="Label3" runat="server" Text='<%# Bind("misi_from" ,"{0:dd/MMM/yyyy}") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="To Date">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="Label4" runat="server" Text='<%# Bind("misi_to" ,"{0:dd/MMM/yyyy}") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="PC">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="Label5" runat="server" Text='<%# Bind("misi_pc") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Ref. Doc">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="Label6" runat="server" Text='<%# Bind("misi_doc_no") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Promo Code">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="Label7" runat="server" Text='<%# Bind("misi_promo") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Seq">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblmisi_seq_no" runat="server" Text='<%# Bind("misi_seq_no") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Active">
                                                                            <ItemTemplate>
                                                                                <asp:CheckBox ID="chkSelectPC" Text=" " runat="server" />
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Active" Visible="false">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblMisi_act" runat="server" Text='<%# Bind("Misi_act") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                    </Columns>
                                                                </asp:GridView>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                            <div role="tabpanel" class="tab-pane" id="PromotionalVoucher">
                                <asp:ScriptManagerProxy ID="ScriptManagerProxy8" runat="server"></asp:ScriptManagerProxy>
                                <asp:UpdatePanel runat="server">
                                    <ContentTemplate>
                                        <div class="row">
                                            <div class="col-sm-12">
                                                <div class="panel panel-default ">
                                                    <div class="panel-heading pannelheading height16 paddingtop0">
                                                        <b>Promotional Vouchers</b>
                                                    </div>
                                                    <div class="panel-body">
                                                        <div class="col-sm-12">
                                                            <div class="row">
                                                                <div class="col-sm-12">
                                                                    <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                                                                        <ContentTemplate>
                                                                            <div class="col-sm-9 ">
                                                                                <div class="col-sm-4">
                                                                                    <div class="col-sm-5 paddingLeft0">
                                                                                        Definition Type
                                                                                    </div>
                                                                                    <div class="col-sm-6 padding0">
                                                                                        <asp:DropDownList ID="cmbProVouDefType" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="cmbProVouDefType_SelectedIndexChanged">
                                                                                            <asp:ListItem Text="Select" />
                                                                                            <asp:ListItem Text="Voucher Type(s)     " />
                                                                                            <asp:ListItem Text="Product Wise        " />
                                                                                            <asp:ListItem Text="Brand Wise          " />
                                                                                            <asp:ListItem Text="Main Category Wise  " />
                                                                                            <asp:ListItem Text="Sub Category Wise   " />
                                                                                            <asp:ListItem Text="Price Book Wise     " />
                                                                                        </asp:DropDownList>
                                                                                    </div>
                                                                                    <div class="col-sm-1 paddingLeft0">
                                                                                    </div>
                                                                                </div>
                                                                                <div class="col-sm-4">
                                                                                    <div class="col-sm-4 paddingLeft0">
                                                                                        Voucher Code :
                                                                                    </div>
                                                                                    <div class="col-sm-6 padding0">
                                                                                        <asp:TextBox ID="txtVochCode_pv" runat="server" CssClass="form-control" AutoPostBack="true" ReadOnly="true" OnTextChanged="txtVochCode_pv_TextChanged" />
                                                                                    </div>
                                                                                    <div class="col-sm-2 paddingLeft0">
                                                                                        <asp:LinkButton ID="btnVouchAtt_pv" runat="server" CssClass="floatRight" OnClick="btnVouchAtt_pv_Click">
                                                                                            <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                                                        </asp:LinkButton>
                                                                                    </div>
                                                                                </div>
                                                                                <div class="col-sm-4">
                                                                                    <asp:Label ID="lblVouchDesc" Text="" runat="server" />
                                                                                </div>
                                                                            </div>
                                                                            <div class="col-sm-1 padding0 buttonRow">
                                                                                <asp:LinkButton ID="btnProVouSave" CausesValidation="false" runat="server" CssClass="floatRight" OnClientClick="return ConfirmSave();" OnClick="btnProVouSave_Click">
                                                                        <span class="glyphicon glyphicon-saved" aria-hidden="true" ></span>Save
                                                                                </asp:LinkButton>
                                                                            </div>
                                                                            <div class="col-sm-1 padding0 buttonRow">
                                                                                <asp:LinkButton ID="btnPDCancel" CausesValidation="false" runat="server" CssClass="floatRight" OnClick="btnPDCancel_Click">
                                                                        <span class="glyphicon glyphicon-remove" aria-hidden="true" ></span>Cancel
                                                                                </asp:LinkButton>
                                                                            </div>
                                                                            <div class="col-sm-1 padding0 buttonRow">
                                                                                <asp:LinkButton ID="btnProVouClear" CausesValidation="false" runat="server" CssClass="floatRight" OnClientClick="return ConfirmClearForm();" OnClick="btnProVouClear_Click">
                                                                        <span class="glyphicon glyphicon-refresh" aria-hidden="true" ></span>Clear
                                                                                </asp:LinkButton>
                                                                            </div>
                                                                        </ContentTemplate>
                                                                    </asp:UpdatePanel>
                                                                </div>
                                                                <div class="col-sm-12 padding0">
                                                                    <div class="panel panel-default ">
                                                                        <div class="panel-body">
                                                                            <div>

                                                                                <div>

                                                                                    <!-- Nav tabs -->
                                                                                    <ul id="tabControl" class="nav nav-tabs" role="tablist">
                                                                                        <li role="presentation" class="active"><a href="#home12" aria-controls="home12" role="tab" data-toggle="tab">Product / Brand/ Category / Price Book Definition</a></li>
                                                                                        <li role="presentation"><a href="#profile12" aria-controls="profile12" role="tab" data-toggle="tab">Voucher Type Definition</a></li>
                                                                                    </ul>

                                                                                    <!-- Tab panes -->
                                                                                    <div class="tab-content">
                                                                                        <div role="tabpanel" class="tab-pane active" id="home12">
                                                                                            <asp:Panel ID="pnlProductBrandCategory" runat="server">
                                                                                                <div class="col-sm-12 height5"></div>
                                                                                                <div class="col-sm-6">
                                                                                                    <div class="col-sm-6">
                                                                                                        <div class="col-sm-12">
                                                                                                            <div class="col-sm-4 paddingLeft0">
                                                                                                                Price Book
                                                                                                            </div>
                                                                                                            <div class="col-sm-6 padding0">
                                                                                                                <asp:TextBox ID="txtPB_pv" Style="text-transform: uppercase" runat="server" CssClass="form-control" AutoPostBack="true" OnTextChanged="txtPB_pv_TextChanged" />
                                                                                                            </div>
                                                                                                            <div class="col-sm-2">
                                                                                                                <asp:LinkButton ID="btnPB_spv" runat="server" CssClass="floatRight" OnClick="btnPB_spv_Click">
                                                                                                            <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                                                                                </asp:LinkButton>
                                                                                                            </div>
                                                                                                        </div>
                                                                                                        <div class="col-sm-12 height5"></div>
                                                                                                        <div class="col-sm-12">
                                                                                                            <div class="col-sm-4 paddingLeft0">
                                                                                                                Main Cate
                                                                                                            </div>
                                                                                                            <div class="col-sm-6 padding0">
                                                                                                                <asp:TextBox ID="txtCat1_pv" runat="server" CssClass="form-control" AutoPostBack="true" OnTextChanged="txtCat1_pv_TextChanged" />
                                                                                                            </div>
                                                                                                            <div class="col-sm-2">
                                                                                                                <asp:LinkButton ID="btnCat1_spv" runat="server" CssClass="floatRight" OnClick="btnCat1_spv_Click">
                                                                                                            <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                                                                                </asp:LinkButton>
                                                                                                            </div>
                                                                                                        </div>
                                                                                                        <div class="col-sm-12 height5"></div>
                                                                                                        <div class="col-sm-12">
                                                                                                            <div class="col-sm-4 paddingLeft0">
                                                                                                                Range
                                                                                                            </div>
                                                                                                            <div class="col-sm-6 padding0">
                                                                                                                <asp:TextBox ID="txtCat3_pv" runat="server" CssClass="form-control" AutoPostBack="true" OnTextChanged="txtCat3_pv_TextChanged" />
                                                                                                            </div>
                                                                                                            <div class="col-sm-2">
                                                                                                                <asp:LinkButton ID="btnCat3_spv" runat="server" CssClass="floatRight" OnClick="btnCat3_spv_Click">
                                                                                                            <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                                                                                </asp:LinkButton>
                                                                                                            </div>
                                                                                                        </div>
                                                                                                        <div class="col-sm-12 height5"></div>
                                                                                                        <div class="col-sm-12">
                                                                                                            <div class="col-sm-4 paddingLeft0">
                                                                                                                Item
                                                                                                            </div>
                                                                                                            <div class="col-sm-6 padding0">
                                                                                                                <asp:TextBox ID="txtItem_pv" runat="server" CssClass="form-control" AutoPostBack="true" OnTextChanged="txtItem_pv_TextChanged" />
                                                                                                            </div>
                                                                                                            <div class="col-sm-2">
                                                                                                                <asp:LinkButton ID="btnItem_spv" runat="server" CssClass="floatRight" OnClick="btnItem_spv_Click">
                                                                                                            <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                                                                                </asp:LinkButton>
                                                                                                            </div>
                                                                                                        </div>
                                                                                                    </div>
                                                                                                    <div class="col-sm-6">
                                                                                                        <div class="col-sm-12">
                                                                                                            <div class="col-sm-4 paddingLeft0">
                                                                                                                Price Level
                                                                                                            </div>
                                                                                                            <div class="col-sm-6 padding0">
                                                                                                                <asp:TextBox ID="txtPL_pv" runat="server" CssClass="form-control" AutoPostBack="true" OnTextChanged="txtPL_pv_TextChanged" />
                                                                                                            </div>
                                                                                                            <div class="col-sm-2">
                                                                                                                <asp:LinkButton ID="btnPL_spv" runat="server" CssClass="floatRight" OnClick="btnPL_spv_Click">
                                                                                                            <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                                                                                </asp:LinkButton>
                                                                                                            </div>
                                                                                                        </div>
                                                                                                        <div class="col-sm-12 height5"></div>
                                                                                                        <div class="col-sm-12">
                                                                                                            <div class="col-sm-4 paddingLeft0">
                                                                                                                Sub Cate
                                                                                                            </div>
                                                                                                            <div class="col-sm-6 padding0">
                                                                                                                <asp:TextBox ID="txtCat2_pv" runat="server" CssClass="form-control" AutoPostBack="true" OnTextChanged="txtCat2_pv_TextChanged" />
                                                                                                            </div>
                                                                                                            <div class="col-sm-2">
                                                                                                                <asp:LinkButton ID="btnCat2_spv" runat="server" CssClass="floatRight" OnClick="btnCat2_spv_Click">
                                                                                                            <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                                                                                </asp:LinkButton>
                                                                                                            </div>
                                                                                                        </div>
                                                                                                        <div class="col-sm-12 height5"></div>
                                                                                                        <div class="col-sm-12">
                                                                                                            <div class="col-sm-4 paddingLeft0">
                                                                                                                Brand
                                                                                                            </div>
                                                                                                            <div class="col-sm-6 padding0">
                                                                                                                <asp:TextBox ID="txtBrand_pv" runat="server" CssClass="form-control" AutoPostBack="true" OnTextChanged="txtBrand_pv_TextChanged" />
                                                                                                            </div>
                                                                                                            <div class="col-sm-2">
                                                                                                                <asp:LinkButton ID="btnBrand_spv" runat="server" CssClass="floatRight" OnClick="btnBrand_spv_Click">
                                                                                                            <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                                                                                </asp:LinkButton>
                                                                                                            </div>
                                                                                                        </div>
                                                                                                        <div class="col-sm-12 height5"></div>
                                                                                                        <div class="col-sm-12">
                                                                                                            <div class="col-sm-4 paddingLeft0">
                                                                                                            </div>
                                                                                                            <div class="col-sm-6 padding0">
                                                                                                                <asp:Button ID="btnLoadPara_pv" Text="Load" runat="server" Width="100%" OnClick="btnLoadPara_pv_Click" />
                                                                                                            </div>
                                                                                                            <div class="col-sm-2">
                                                                                                            </div>
                                                                                                        </div>
                                                                                                    </div>
                                                                                                    <div class="col-sm-12 height5"></div>
                                                                                                    <div class="col-sm-12">
                                                                                                        <asp:UpdatePanel runat="server">
                                                                                                            <ContentTemplate>

                                                                                                                <div class="col-sm-2">
                                                                                                                    Excel File
                                                                                                                </div>
                                                                                                                <div class="col-sm-4">
                                                                                                                    <asp:FileUpload ID="FileUpload3" runat="server" />
                                                                                                                </div>
                                                                                                                <div class="col-sm-2">
                                                                                                                </div>
                                                                                                                <div class="col-sm-2">
                                                                                                                    <asp:Button ID="btnUploadFile_spv" Text="Upload" Width="100%" runat="server" OnClick="btnUploadFile_spv_Click" />
                                                                                                                </div>
                                                                                                            </ContentTemplate>
                                                                                                            <Triggers>
                                                                                                                <asp:PostBackTrigger ControlID="btnUploadFile_spv" />
                                                                                                            </Triggers>
                                                                                                        </asp:UpdatePanel>
                                                                                                    </div>
                                                                                                </div>
                                                                                                <div class="col-sm-6">
                                                                                                    <div class="col-sm-12 GridScroll" style="height: 100px;">
                                                                                                        <asp:Panel ID="pnlListItems" runat="server">
                                                                                                            <asp:CheckBoxList runat="server" ID="lstPDItems"
                                                                                                                RepeatColumns="4"
                                                                                                                RepeatDirection="Vertical"
                                                                                                                RepeatLayout="Table"
                                                                                                                TextAlign="Right"
                                                                                                                ForeColor="#333"
                                                                                                                Font-Bold="false"
                                                                                                                CssClass="chkChoice">
                                                                                                            </asp:CheckBoxList>
                                                                                                        </asp:Panel>
                                                                                                        <asp:Panel ID="pnlSubItems" runat="server">
                                                                                                            <asp:GridView ID="gvVouCat" AutoGenerateColumns="false" runat="server" CssClass="table table-hover table-striped" GridLines="None" ShowHeaderWhenEmpty="True" EmptyDataText="No data found...">
                                                                                                                <Columns>
                                                                                                                    <asp:TemplateField HeaderText=" ">
                                                                                                                        <ItemTemplate>
                                                                                                                            <asp:CheckBox ID="chkSelectPC" Text=" " runat="server" />
                                                                                                                        </ItemTemplate>
                                                                                                                    </asp:TemplateField>
                                                                                                                    <asp:TemplateField HeaderText="Item Code">
                                                                                                                        <ItemTemplate>
                                                                                                                            <asp:Label ID="lblRic2_cd" runat="server" Text='<%# Bind("Ric2_cd") %>'></asp:Label>
                                                                                                                        </ItemTemplate>
                                                                                                                    </asp:TemplateField>
                                                                                                                    <asp:BoundField DataField="Ric2_cd1" HeaderText="Description" />
                                                                                                                    <asp:TemplateField HeaderText=" ">
                                                                                                                        <ItemTemplate>
                                                                                                                            <asp:LinkButton ID="btnDeleteSubItems" CausesValidation="false" OnClientClick="return ConfirmDeliem()" OnClick="btnDeleteSubItems_Click" runat="server">
                                                                                                                        <span class="glyphicon glyphicon-trash" aria-hidden="true"></span>
                                                                                                                            </asp:LinkButton>
                                                                                                                        </ItemTemplate>
                                                                                                                    </asp:TemplateField>
                                                                                                                </Columns>
                                                                                                            </asp:GridView>
                                                                                                        </asp:Panel>
                                                                                                    </div>
                                                                                                    <div class="col-sm-12">
                                                                                                        <div class="col-sm-4 padding0">
                                                                                                            <asp:Button ID="btnSelectAll_pv" Text="Select All" runat="server" Width="100%" OnClick="btnSelectAll_pv_Click" />
                                                                                                        </div>
                                                                                                        <div class="col-sm-4 padding0">
                                                                                                            <asp:Button ID="btnUnselectAll_pv" Text="Unselect" runat="server" Width="100%" OnClick="btnUnselectAll_pv_Click" />
                                                                                                        </div>
                                                                                                        <div class="col-sm-4 padding0">
                                                                                                            <asp:Button ID="btnClear_pv" Text="Clear" runat="server" Width="100%" OnClick="btnClear_pv_Click" />
                                                                                                        </div>
                                                                                                    </div>
                                                                                                </div>
                                                                                                <div class="col-sm-12 height5"></div>
                                                                                                <div class="col-sm-7 padding0">
                                                                                                    <div class="panel panel-default ">
                                                                                                        <div class="panel-heading pannelheading height16 paddingtop0">
                                                                                                            <b>Applicable Profit Centers and Sub Channels</b>
                                                                                                        </div>
                                                                                                        <div class="panel-body">
                                                                                                            <div class="col-sm-5 padding0">
                                                                                                                <div class="col-sm-12">
                                                                                                                    <div class="col-sm-4 paddingLeft0">
                                                                                                                        Define By
                                                                                                                    </div>
                                                                                                                    <div class="col-sm-6 padding0">
                                                                                                                        <asp:DropDownList ID="cmbDefby_pv" CssClass="form-control" runat="server" AutoPostBack="true" OnSelectedIndexChanged="cmbDefby_pv_SelectedIndexChanged">
                                                                                                                            <asp:ListItem Text="Sub Channel" />
                                                                                                                            <asp:ListItem Text="Profit Center" />
                                                                                                                        </asp:DropDownList>
                                                                                                                    </div>
                                                                                                                    <div class="col-sm-2">
                                                                                                                    </div>
                                                                                                                </div>
                                                                                                                <div class="col-sm-12 height5"></div>
                                                                                                                <div class="col-sm-12">
                                                                                                                    <div class="col-sm-4 paddingLeft0">
                                                                                                                        Channel
                                                                                                                    </div>
                                                                                                                    <div class="col-sm-6 padding0">
                                                                                                                        <asp:TextBox ID="txtChnnl_pv" Style="text-transform: uppercase" runat="server" CssClass="form-control" AutoPostBack="true" OnTextChanged="txtChnnl_pv_TextChanged" />
                                                                                                                    </div>
                                                                                                                    <div class="col-sm-2">
                                                                                                                        <asp:LinkButton ID="btnChnnl_spv" runat="server" CssClass="floatRight" OnClick="btnChnnl_spv_Click">
                                                                                                            <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                                                                                        </asp:LinkButton>
                                                                                                                    </div>
                                                                                                                </div>
                                                                                                                <div class="col-sm-12 height5"></div>
                                                                                                                <div class="col-sm-12">
                                                                                                                    <div class="col-sm-4 paddingLeft0">
                                                                                                                        Sub Channel
                                                                                                                    </div>
                                                                                                                    <div class="col-sm-6 padding0">
                                                                                                                        <asp:TextBox ID="txtSChnnl_pv" Style="text-transform: uppercase" runat="server" CssClass="form-control" AutoPostBack="true" OnTextChanged="txtSChnnl_pv_TextChanged" />
                                                                                                                    </div>
                                                                                                                    <div class="col-sm-2">
                                                                                                                        <asp:LinkButton ID="btnSChnnl_spv" runat="server" CssClass="floatRight" OnClick="btnSChnnl_spv_Click">
                                                                                                            <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                                                                                        </asp:LinkButton>
                                                                                                                    </div>
                                                                                                                </div>
                                                                                                                <div class="col-sm-12 height5"></div>
                                                                                                                <div class="col-sm-12">
                                                                                                                    <div class="col-sm-4 paddingLeft0">
                                                                                                                        Profit Center
                                                                                                                    </div>
                                                                                                                    <div class="col-sm-6 padding0">
                                                                                                                        <asp:TextBox ID="txtPC_pv" Style="text-transform: uppercase" runat="server" CssClass="form-control" AutoPostBack="true" OnTextChanged="txtPC_pv_TextChanged" />
                                                                                                                    </div>
                                                                                                                    <div class="col-sm-2">
                                                                                                                        <asp:LinkButton ID="btnPC_spv" runat="server" CssClass="floatRight" OnClick="btnPC_spv_Click">
                                                                                                            <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                                                                                        </asp:LinkButton>
                                                                                                                    </div>
                                                                                                                </div>
                                                                                                                <div class="col-sm-12 height5"></div>
                                                                                                                <div class="col-sm-12">
                                                                                                                    <div class="col-sm-3 padding0">
                                                                                                                        <asp:Button ID="btnLocSelectAll_spc" Text="Select All" runat="server" Width="100%" OnClick="btnLocSelectAll_spc_Click" />
                                                                                                                    </div>
                                                                                                                    <div class="col-sm-3 padding0">
                                                                                                                        <asp:Button ID="btnLocUnselect_spc" Text="Unselect" runat="server" Width="100%" OnClick="btnLocUnselect_spc_Click" />
                                                                                                                    </div>
                                                                                                                    <div class="col-sm-3 padding0">
                                                                                                                        <asp:Button ID="btnLocClr_spc" Text="Clear" runat="server" Width="100%" OnClick="btnLocClr_spc_Click" />
                                                                                                                    </div>
                                                                                                                    <div class="col-sm-3 padding0">
                                                                                                                        <asp:Button ID="btnSelectLocs_pv" Text="Load" runat="server" Width="100%" OnClick="btnSelectLocs_pv_Click" />
                                                                                                                    </div>
                                                                                                                </div>
                                                                                                            </div>
                                                                                                            <div class="col-sm-7 padding0">
                                                                                                                <div class="col-sm-12 GridScroll" style="height: 100px;">
                                                                                                                    <asp:CheckBoxList runat="server" ID="lstLocations"
                                                                                                                        RepeatColumns="4"
                                                                                                                        RepeatDirection="Vertical"
                                                                                                                        RepeatLayout="Table"
                                                                                                                        TextAlign="Right"
                                                                                                                        ForeColor="#333"
                                                                                                                        Font-Bold="false"
                                                                                                                        CssClass="rbl">
                                                                                                                    </asp:CheckBoxList>
                                                                                                                </div>
                                                                                                            </div>
                                                                                                        </div>
                                                                                                    </div>
                                                                                                </div>
                                                                                                <div class="col-sm-5">
                                                                                                    <div class="panel panel-default ">
                                                                                                        <div class="panel-heading pannelheading height16 paddingtop0">
                                                                                                            <b>Main Parameters</b>
                                                                                                        </div>
                                                                                                        <div class="panel-body">
                                                                                                            <div class="col-sm-12">
                                                                                                                <div class="col-sm-4 paddingLeft0">
                                                                                                                    Circular #  :
                                                                                                                </div>
                                                                                                                <div class="col-sm-6 padding0">
                                                                                                                    <asp:TextBox ID="txtCircular_pv" Style="text-transform: uppercase" runat="server" CssClass="form-control" AutoPostBack="true" OnTextChanged="txtCircular_pv_TextChanged" />
                                                                                                                </div>
                                                                                                                <div class="col-sm-2">
                                                                                                                    <asp:LinkButton ID="btnSearchCircular_pv" runat="server" CssClass="floatRight" OnClick="btnSearchCircular_pv_Click">
                                                                                                                    <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                                                                                    </asp:LinkButton>
                                                                                                                </div>
                                                                                                            </div>
                                                                                                            <div class="col-sm-12 height5"></div>
                                                                                                            <div class="col-sm-6">
                                                                                                                <div class="col-sm-4 paddingLeft0">
                                                                                                                    Valid From
                                                                                                                </div>
                                                                                                                <div class="col-sm-6 padding0">
                                                                                                                    <asp:TextBox ID="dtpFromDate__pv" runat="server" CssClass="form-control" AutoPostBack="true" />
                                                                                                                </div>
                                                                                                                <div class="col-sm-2">
                                                                                                                    <asp:CalendarExtender ID="dtpFromDate__pvCal" runat="server" TargetControlID="dtpFromDate__pv"
                                                                                                                        PopupButtonID="dtpFromDate__pvbtn" Format="dd/MMM/yyyy">
                                                                                                                    </asp:CalendarExtender>
                                                                                                                    <asp:LinkButton ID="dtpFromDate__pvbtn" CausesValidation="false" runat="server">
                                                                                                                    <span class="glyphicon glyphicon-calendar" aria-hidden="true"></span>
                                                                                                                    </asp:LinkButton>
                                                                                                                </div>
                                                                                                            </div>
                                                                                                            <div class="col-sm-6">
                                                                                                                <div class="col-sm-4 paddingLeft0">
                                                                                                                    To
                                                                                                                </div>
                                                                                                                <div class="col-sm-6 padding0">
                                                                                                                    <asp:TextBox ID="dtpToDate_pv" runat="server" CssClass="form-control" AutoPostBack="true" />
                                                                                                                </div>
                                                                                                                <div class="col-sm-2">
                                                                                                                    <asp:CalendarExtender ID="dtpToDate_pvCal" runat="server" TargetControlID="dtpToDate_pv"
                                                                                                                        PopupButtonID="dtpToDate_pvBtn" Format="dd/MMM/yyyy">
                                                                                                                    </asp:CalendarExtender>
                                                                                                                    <asp:LinkButton ID="dtpToDate_pvBtn" runat="server" CssClass="floatRight">
                                                                                                                 <span class="glyphicon glyphicon-calendar" aria-hidden="true"></span>
                                                                                                                    </asp:LinkButton>
                                                                                                                </div>
                                                                                                            </div>
                                                                                                            <div class="col-sm-12 height5"></div>
                                                                                                            <div class="col-sm-6">
                                                                                                                <div class="col-sm-4 paddingLeft0">
                                                                                                                    Discount
                                                                                                                </div>
                                                                                                                <div class="col-sm-6 padding0">
                                                                                                                    <asp:DropDownList ID="cmbDisType_pv" CssClass="form-control" runat="server">
                                                                                                                        <asp:ListItem Text="VALUE" />
                                                                                                                        <asp:ListItem Text="RATE" />
                                                                                                                    </asp:DropDownList>
                                                                                                                </div>
                                                                                                                <div class="col-sm-2">
                                                                                                                </div>
                                                                                                            </div>
                                                                                                            <div class="col-sm-6">
                                                                                                                <div class="col-sm-4 paddingLeft0">
                                                                                                                </div>
                                                                                                                <div class="col-sm-6 padding0">
                                                                                                                    <asp:TextBox ID="txtDis_pv" onkeydown="return jsDecimals(event);" runat="server" CssClass="form-control" AutoPostBack="true" />
                                                                                                                </div>
                                                                                                                <div class="col-sm-2">
                                                                                                                </div>
                                                                                                            </div>
                                                                                                            <div class="col-sm-12 height5"></div>
                                                                                                            <div class="col-sm-6">
                                                                                                                <div class="col-sm-4 paddingLeft0">
                                                                                                                    Invoice
                                                                                                                </div>
                                                                                                                <div class="col-sm-6 padding0">
                                                                                                                    <asp:DropDownList ID="cmbPVInvoiceType" CssClass="form-control" runat="server">
                                                                                                                        <asp:ListItem Text="CS" />
                                                                                                                        <asp:ListItem Text="CRED" />
                                                                                                                        <asp:ListItem Text="HS" />
                                                                                                                    </asp:DropDownList>
                                                                                                                </div>
                                                                                                                <div class="col-sm-2">
                                                                                                                </div>
                                                                                                            </div>
                                                                                                            <div class="col-sm-6">
                                                                                                                <div class="col-sm-4 paddingLeft0 paddingRight0">
                                                                                                                    Circular Status
                                                                                                                </div>
                                                                                                                <div class="col-sm-6 padding0">
                                                                                                                    <asp:TextBox ID="lblPVStatus" runat="server" CssClass="form-control" AutoPostBack="true" ReadOnly="true" Text="Active" />
                                                                                                                </div>
                                                                                                                <div class="col-sm-2">
                                                                                                                </div>
                                                                                                            </div>
                                                                                                            <div class="col-sm-12 height5"></div>
                                                                                                            <div class="col-sm-4">
                                                                                                                <asp:Button ID="btnRedeemItem_pv" Text="Redeem Items/Company" runat="server" Width="100%" OnClick="btnRedeemItem_pv_Click" />
                                                                                                            </div>
                                                                                                            <div class="col-sm-4">
                                                                                                                <asp:Button ID="btnGv" Text="GV Definition" runat="server" Width="100%" OnClick="btnGv_Click" />
                                                                                                            </div>
                                                                                                            <div class="col-sm-4">
                                                                                                                <asp:Button ID="btnCommAdd__pv" Text="Apply" runat="server" Width="100%" OnClientClick="return confirm('Confirm to apply details ?')" OnClick="btnCommAdd__pv_Click" />
                                                                                                            </div>
                                                                                                        </div>
                                                                                                    </div>
                                                                                                </div>
                                                                                                <div class="col-sm-12 padding0">
                                                                                                    <div class="panel panel-default ">
                                                                                                        <div class="panel-heading pannelheading height16 paddingtop0">
                                                                                                            <div class="col-sm-6 padding0">
                                                                                                                <b>Define Details</b>
                                                                                                            </div>
                                                                                                            <div class="col-sm-6 padding0">
                                                                                                                <div class="col-sm-6 padding0">
                                                                                                                    <div class="col-sm-5  paddingLeft0">
                                                                                                                        Current processing row
                                                                                                                    </div>
                                                                                                                    <div class="col-sm-6 padding0">
                                                                                                                        <asp:Label ID="lblcount" Text="text" runat="server" />
                                                                                                                    </div>
                                                                                                                    <div class="col-sm-1 paddingLeft0">
                                                                                                                    </div>
                                                                                                                </div>
                                                                                                                <div class="col-sm-6 padding0">
                                                                                                                    <div class="col-sm-5  paddingLeft0">
                                                                                                                        Current updating row
                                                                                                                    </div>
                                                                                                                    <div class="col-sm-6 padding0">
                                                                                                                        <asp:Label ID="lblSaveCount" Text="text" runat="server" />
                                                                                                                    </div>
                                                                                                                    <div class="col-sm-1 paddingLeft0">
                                                                                                                    </div>
                                                                                                                </div>
                                                                                                            </div>
                                                                                                        </div>
                                                                                                    </div>
                                                                                                    <div class="panel-body GridScroll">
                                                                                                        <div class="GHead" id="GHead"></div>
                                                                                                        <div style="height: 80px; overflow: auto;">
                                                                                                            <asp:GridView ID="dgvDefDetails_pv" runat="server" GridLines="None" EmptyDataText="No data found..." CssClass="dgvDefDetails_pv table table-hover table-striped" AutoGenerateColumns="False" Style="margin-bottom: 0px;">
                                                                                                                <Columns>
                                                                                                                    <asp:TemplateField HeaderText="" Visible="false">
                                                                                                                        <ItemTemplate>
                                                                                                                            <asp:CheckBox ID="chkSelectPC" Text=" " runat="server" />
                                                                                                                        </ItemTemplate>
                                                                                                                    </asp:TemplateField>
                                                                                                                    <asp:BoundField DataField="Spd_circular_no" HeaderText="Circular" />
                                                                                                                    <asp:BoundField DataField="Spd_pty_tp" HeaderText="Party Type" />
                                                                                                                    <asp:BoundField DataField="Spd_pty_cd" HeaderText="Party Value" />
                                                                                                                    <asp:BoundField DataField="Spd_from_dt" HeaderText="From Date" DataFormatString="{0:dd/MMM/yyyy}" />
                                                                                                                    <asp:BoundField DataField="Spd_to_dt" HeaderText="To Date" DataFormatString="{0:dd/MMM/yyyy}" />
                                                                                                                    <asp:BoundField DataField="Spd_pb" HeaderText="Price Book" />
                                                                                                                    <asp:BoundField DataField="Spd_pb_lvl" HeaderText="Prive Level" />
                                                                                                                    <asp:BoundField DataField="Spd_main_cat" HeaderText="Main Cate" />
                                                                                                                    <asp:BoundField DataField="Spd_cat" HeaderText="Sub Cate" />
                                                                                                                    <asp:BoundField DataField="Spd_brd" HeaderText="Brand" />
                                                                                                                    <asp:BoundField DataField="Spd_itm" HeaderText="Item" />
                                                                                                                    <asp:TemplateField HeaderText="">
                                                                                                                        <ItemTemplate>
                                                                                                                            <asp:LinkButton ID="btnRemoveDetails" runat="server" CssClass="floatRight" OnClick="btnRemoveDetails_Click">
                                                                                                                                <span class="glyphicon glyphicon-remove" aria-hidden="true"></span>
                                                                                                                            </asp:LinkButton>
                                                                                                                        </ItemTemplate>
                                                                                                                    </asp:TemplateField>
                                                                                                                </Columns>
                                                                                                            </asp:GridView>
                                                                                                        </div>
                                                                                                    </div>
                                                                                                </div>
                                                                                            </asp:Panel>
                                                                                        </div>
                                                                                        <div role="tabpanel" class="tab-pane" id="profile12">
                                                                                            <div class="panel panel-default ">
                                                                                                <asp:Panel ID="pnlVoucherTypes" runat="server">
                                                                                                    <div class="panel-body">
                                                                                                        <div class="col-sm-12">
                                                                                                            <div class="col-sm-6">
                                                                                                                <div class="col-sm-4 paddingLeft0">
                                                                                                                    Voucher Code
                                                                                                                </div>
                                                                                                                <div class="col-sm-8 padding0">
                                                                                                                    <div class="col-sm-11">
                                                                                                                        <asp:TextBox ID="txtProVouType" MaxLength="10" Style="text-transform: uppercase" runat="server" CssClass="form-control" AutoPostBack="true" OnTextChanged="txtProVouType_TextChanged" />
                                                                                                                    </div>
                                                                                                                    <div class="col-sm-1">
                                                                                                                        <asp:LinkButton ID="btnProVouType_spv" runat="server" CssClass="floatRight" OnClick="btnProVouType_spv_Click">
                                                                                                                 <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                                                                                        </asp:LinkButton>
                                                                                                                    </div>
                                                                                                                </div>
                                                                                                            </div>
                                                                                                            <div class="col-sm-6">
                                                                                                                <div class="col-sm-8">
                                                                                                                    <asp:CheckBox ID="chkIssueQtywise" Text="Allow to issue vouchers qty wise" runat="server" />
                                                                                                                </div>
                                                                                                                <div class="col-sm-4">
                                                                                                                    <asp:RadioButtonList ID="optVou" runat="server" RepeatDirection="Horizontal">
                                                                                                                        <asp:ListItem Text="Active" />
                                                                                                                        <asp:ListItem Text="Inactive" />
                                                                                                                    </asp:RadioButtonList>
                                                                                                                </div>
                                                                                                            </div>
                                                                                                            <div class="col-sm-12 height5"></div>
                                                                                                            <div class="col-sm-12">
                                                                                                                <div class="col-sm-2 paddingLeft0">
                                                                                                                    Description
                                                                                                                </div>
                                                                                                                <div class="col-sm-10 paddingLeft10">
                                                                                                                    <asp:TextBox ID="txtProVouTypeDesc" runat="server" CssClass="form-control" />
                                                                                                                </div>
                                                                                                            </div>
                                                                                                            <div class="col-sm-12 height5"></div>
                                                                                                            <div class="col-sm-6">
                                                                                                                <div class="col-sm-4 paddingLeft0">
                                                                                                                    Setup SMS Alert
                                                                                                                </div>
                                                                                                                <div class="col-sm-6">
                                                                                                                    <asp:CheckBox ID="chkSMS" Text=" " runat="server" AutoPostBack="true" OnCheckedChanged="chkSMS_CheckedChanged" />
                                                                                                                </div>
                                                                                                                <div class="col-sm-2">
                                                                                                                </div>
                                                                                                            </div>
                                                                                                            <div class="col-sm-6">
                                                                                                                <div class="col-sm-4 paddingLeft0" style="padding-left: 17px;">
                                                                                                                    Minimum Value
                                                                                                                </div>
                                                                                                                <div class="col-sm-6 padding0">
                                                                                                                    <asp:TextBox ID="txtMinVal" onkeydown="return jsDecimals(event);" runat="server" CssClass="form-control" />
                                                                                                                </div>
                                                                                                                <div class="col-sm-2">
                                                                                                                </div>
                                                                                                            </div>
                                                                                                            <div class="col-sm-12 height5"></div>
                                                                                                            <div class="col-sm-12">
                                                                                                                <div class="col-sm-2 paddingLeft0">
                                                                                                                    Purchase point
                                                                                                                </div>
                                                                                                                <div class="col-sm-10 paddingLeft10">
                                                                                                                    <asp:TextBox ID="txtPurSMS" runat="server" CssClass="form-control" />
                                                                                                                </div>
                                                                                                            </div>
                                                                                                            <div class="col-sm-12 height5"></div>
                                                                                                            <div class="col-sm-12">
                                                                                                                <div class="col-sm-2 paddingLeft0">
                                                                                                                    Redeem point
                                                                                                                </div>
                                                                                                                <div class="col-sm-10 paddingLeft10">
                                                                                                                    <asp:TextBox ID="txtRedeemSMS" runat="server" CssClass="form-control" />
                                                                                                                </div>
                                                                                                            </div>
                                                                                                            <div class="col-sm-12 height5"></div>
                                                                                                            <div class="col-sm-12">
                                                                                                                <div class="col-sm-2 paddingLeft0">
                                                                                                                    Conditions
                                                                                                                </div>
                                                                                                                <div class="col-sm-10 paddingLeft10">
                                                                                                                    <asp:TextBox ID="txtCond" runat="server" CssClass="form-control" TextMode="MultiLine" />
                                                                                                                </div>
                                                                                                            </div>
                                                                                                        </div>
                                                                                                    </div>
                                                                                                    <div class="col-sm-12 height5"></div>
                                                                                                    <div class="panel-body">
                                                                                                        <div class="panel panel-default ">
                                                                                                            <asp:ScriptManagerProxy ID="ScriptManagerProxy12" runat="server"></asp:ScriptManagerProxy>
                                                                                                            <asp:UpdatePanel runat="server">
                                                                                                                <ContentTemplate>
                                                                                                                    <div class="panel-heading pannelheading height16 paddingtop0">
                                                                                                                        <b>Invoice / Price / Pay Types</b>
                                                                                                                    </div>
                                                                                                                    <div class="col-sm-6">
                                                                                                                        <div class="panel-body">
                                                                                                                            <div class="col-sm-12">
                                                                                                                                <div class="col-sm-1 padding0">
                                                                                                                                    <asp:RadioButton ToolTip="SALE" GroupName="grp" ID="optSaleTp" Text=" " runat="server" />
                                                                                                                                </div>
                                                                                                                                <div class="col-sm-3 padding0">
                                                                                                                                    Invoice Type
                                                                                                                                </div>
                                                                                                                                <div class="col-sm-3 padding0">
                                                                                                                                    <asp:DropDownList ID="ddlhpSalesAccept" CausesValidation="false" runat="server" CssClass="form-control">
                                                                                                                                    </asp:DropDownList>
                                                                                                                                    <%--<asp:TextBox ID="txtSaleTp" CssClass="form-control" runat="server" AutoPostBack="true" OnTextChanged="txtSaleTp_TextChanged" />--%>
                                                                                                                                </div>
                                                                                                                                <div class="col-sm-2 padding0">
                                                                                                                                    <%-- <asp:LinkButton ID="btn_srch_sale_tp" CausesValidation="false" runat="server" CssClass="floatRight" OnClick="btn_srch_sale_tp_Click">
                                                                                                                            <span class="glyphicon glyphicon-search" aria-hidden="true" ></span>
                                                                                                                                    </asp:LinkButton>--%>
                                                                                                                                </div>
                                                                                                                                <div class="col-sm-3 padding0">
                                                                                                                                </div>
                                                                                                                            </div>
                                                                                                                            <div class="col-sm-12 height5"></div>
                                                                                                                            <div class="col-sm-12">
                                                                                                                                <div class="col-sm-1 padding0">
                                                                                                                                    <asp:RadioButton ToolTip="PRICE" GroupName="grp" ID="optPriceTp" Text=" " runat="server" />
                                                                                                                                </div>
                                                                                                                                <div class="col-sm-3 padding0">
                                                                                                                                    Price Type
                                                                                                                                </div>
                                                                                                                                <div class="col-sm-3 padding0">
                                                                                                                                    <asp:DropDownList ID="cmbPriceTp" runat="server" CssClass="form-control"></asp:DropDownList>
                                                                                                                                </div>
                                                                                                                                <div class="col-sm-2 padding0">
                                                                                                                                </div>
                                                                                                                                <div class="col-sm-3 padding0">
                                                                                                                                </div>
                                                                                                                            </div>
                                                                                                                            <div class="col-sm-12 height5"></div>
                                                                                                                            <div class="col-sm-12">
                                                                                                                                <div class="col-sm-1 padding0">
                                                                                                                                    <asp:RadioButton GroupName="grp" ToolTip="PAY" ID="optPayTp" Text=" " runat="server" />
                                                                                                                                </div>
                                                                                                                                <div class="col-sm-3 padding0">
                                                                                                                                    Pay Mode
                                                                                                                                </div>
                                                                                                                                <div class="col-sm-3 padding0">
                                                                                                                                    <asp:DropDownList ID="comboBoxPayModes" runat="server" CssClass="form-control"></asp:DropDownList>
                                                                                                                                </div>
                                                                                                                                <div class="col-sm-2 padding0">
                                                                                                                                </div>
                                                                                                                                <div class="col-sm-3 padding0">
                                                                                                                                </div>
                                                                                                                            </div>
                                                                                                                            <div class="col-sm-12 height5"></div>
                                                                                                                            <div class="col-sm-12">
                                                                                                                                <div class="col-sm-1 padding0">
                                                                                                                                </div>
                                                                                                                                <div class="col-sm-3 padding0">
                                                                                                                                    Period
                                                                                                                                </div>
                                                                                                                                <div class="col-sm-3 padding0">
                                                                                                                                    <asp:TextBox ID="txtPrd" onkeydown="return jsDecimals(event);" runat="server" CssClass="form-control" />
                                                                                                                                </div>
                                                                                                                                <div class="col-sm-2 padding0">
                                                                                                                                </div>
                                                                                                                                <div class="col-sm-3 padding0">
                                                                                                                                    <asp:Button ID="btnAddSaleTp" Text="Add" runat="server" OnClick="btnAddSaleTp_Click" />
                                                                                                                                </div>
                                                                                                                            </div>
                                                                                                                            <div class="col-sm-12">
                                                                                                                            </div>
                                                                                                                        </div>
                                                                                                                    </div>
                                                                                                                    <div class="col-sm-6 GridScroll" style="width: 50% !important">
                                                                                                                        <asp:GridView ID="grvVouPara" AutoGenerateColumns="false" runat="server" CssClass="table table-hover table-striped" GridLines="None" ShowHeaderWhenEmpty="True" EmptyDataText="No data found...">
                                                                                                                            <Columns>
                                                                                                                                <asp:BoundField DataField="SPDP_TP" HeaderText="Type" />
                                                                                                                                <asp:BoundField DataField="SPDP_SALE_TP" HeaderText="Invoice Type" />
                                                                                                                                <asp:BoundField DataField="SPDP_PRICE_TP" HeaderText="Price Type" Visible="false" />
                                                                                                                                <asp:TemplateField HeaderText="Price Type">
                                                                                                                                    <ItemTemplate>
                                                                                                                                        <asp:Label ID="lblSPDP_PRICE_TP" runat="server" Text='<%# Bind("SPDP_PRICE_TP") %>'></asp:Label>
                                                                                                                                    </ItemTemplate>
                                                                                                                                </asp:TemplateField>
                                                                                                                                <asp:BoundField DataField="SPDP_PAY_TP" HeaderText="Pay Mode" />
                                                                                                                                <asp:BoundField DataField="SPDP_PAY_PRD" HeaderText="Period" />
                                                                                                                                <asp:TemplateField HeaderText=" ">
                                                                                                                                    <ItemTemplate>
                                                                                                                                        <asp:LinkButton ID="btnRemoveVouchpara" runat="server" CssClass="floatleft" OnClientClick="return ConfirmDelProf()" OnClick="btnRemoveVouchpara_Click">
                                                                                                                                <span class="glyphicon glyphicon-trash" aria-hidden="true"></span>
                                                                                                                                        </asp:LinkButton>
                                                                                                                                    </ItemTemplate>
                                                                                                                                </asp:TemplateField>
                                                                                                                            </Columns>
                                                                                                                        </asp:GridView>
                                                                                                                    </div>
                                                                                                                </ContentTemplate>
                                                                                                            </asp:UpdatePanel>
                                                                                                        </div>
                                                                                                    </div>
                                                                                                </asp:Panel>
                                                                                            </div>
                                                                                        </div>
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                            <div role="tabpanel" class="tab-pane" id="DiscountAndEditing">
                                <asp:ScriptManagerProxy ID="ScriptManagerProxy13" runat="server"></asp:ScriptManagerProxy>
                                <asp:UpdatePanel runat="server">
                                    <ContentTemplate>
                                        <div class="row">
                                            <div class="col-sm-12">
                                                <div class="panel panel-default " style="padding-bottom:1px;padding-top:1px;margin-bottom:1px; margin-top:1px;">
                                                   <div class="panel-heading pannelheading height16 paddingtop0">
                                                        <b>Discount And Editing</b>
                                                    </div>
                                                    <div class="panel panel-body " style="padding-bottom:1px;padding-top:1px; margin-bottom:1px; margin-top:1px;">
                                                        <div class="row">
                                                            <div class="buttonRow">
                                                                <div class="col-sm-10 ">
                                                                    <div class="col-sm-10 ">
                                                                    </div>
                                                                    <div class="col-sm-1 padding0">
                                                                        <asp:LinkButton ID="lbtnSaveDis" CausesValidation="false" runat="server" CssClass="floatRight" OnClientClick="return ConfirmSave();" OnClick="lbtnSaveDis_Click">
                                                                        <span class="glyphicon glyphicon-saved" aria-hidden="true" ></span>Update
                                                                        </asp:LinkButton>
                                                                    </div>
                                                                    <div class="col-sm-1 ">
                                                                        <asp:LinkButton ID="lbtnClearDis" CausesValidation="false" runat="server" CssClass="floatRight" OnClientClick="return ConfirmClearForm();" OnClick="lbtnClearDis_Click">
                                                                        <span class="glyphicon glyphicon-refresh" aria-hidden="true" ></span>Clear
                                                                        </asp:LinkButton>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-sm-12">
                                                            <div class="col-sm-10">
                                                                <div class="panel panel-default" style="padding-bottom:1px;padding-top:1px;margin-bottom:1px; margin-top:1px;">
                                                                    <div class="panel panel-body" style="padding-bottom:1px;padding-top:1px;margin-bottom:1px; margin-top:1px;">
                                                                        <div class="row">
                                                                            <div class="col-sm-7">
                                                                                <div class="row">
                                                                                    <div class="col-sm-12">
                                                                                        <div class="col-sm-3 labelText1">
                                                                                            Profit Center
                                                                                        </div>
                                                                                        <div class="col-sm-3" style="padding-right: 4px;">
                                                                                            <asp:TextBox ID="txtProfCenter" OnTextChanged="txtProfCenter_TextChanged" Style="text-transform: uppercase"
                                                                                                CssClass="txtProfCenter form-control" runat="server" AutoPostBack="true" />
                                                                                        </div>
                                                                                        <div class="col-sm-1 padding0">
                                                                                            <asp:LinkButton ID="lbtnSeProfitCenter" runat="server" CausesValidation="false" OnClick="lbtnSeProfitCenter_Click">
                                                                                 <span aria-hidden="true" class="glyphicon glyphicon-search"></span>
                                                                                            </asp:LinkButton>
                                                                                        </div>
                                                                                        <div class="col-sm-5">
                                                                                        </div>
                                                                                    </div>
                                                                                </div>
                                                                                <div class="row">
                                                                                    <div class="col-sm-12">
                                                                                        <div class="col-sm-3 labelText1">
                                                                                            Invoice Type
                                                                                        </div>
                                                                                        <div class="col-sm-3" style="padding-right: 4px;">
                                                                                            <asp:TextBox ID="txtInvType" CssClass="txtInvType form-control"
                                                                                                OnTextChanged="txtInvType_TextChanged" runat="server" />
                                                                                        </div>
                                                                                        <div class="col-sm-1 padding0">
                                                                                            <asp:LinkButton ID="lbtnSeInvTp" runat="server" CausesValidation="false" OnClick="lbtnSeInvTp_Click">
                                                                                 <span aria-hidden="true" class="glyphicon glyphicon-search"></span>
                                                                                            </asp:LinkButton>
                                                                                        </div>
                                                                                        <div class="col-sm-5">
                                                                                        </div>
                                                                                    </div>
                                                                                </div>
                                                                                <div class="row">
                                                                                    <div class="col-sm-12">
                                                                                        <div class="col-sm-3 labelText1">
                                                                                            Price Book
                                                                                        </div>
                                                                                        <div class="col-sm-3" style="padding-right: 4px;">
                                                                                            <asp:TextBox ID="txtPriceBook" CssClass="txtPriceBook form-control" AutoPostBack="true"
                                                                                                OnTextChanged="txtPriceBook_TextChanged" runat="server" />
                                                                                        </div>
                                                                                        <div class="col-sm-1 padding0">
                                                                                            <asp:LinkButton ID="lbtnSePriceBook" runat="server" CausesValidation="false" OnClick="lbtnSePriceBook_Click">
                                                                                 <span aria-hidden="true" class="glyphicon glyphicon-search"></span>
                                                                                            </asp:LinkButton>
                                                                                        </div>
                                                                                        <div class="col-sm-5">
                                                                                        </div>
                                                                                    </div>
                                                                                </div>
                                                                                <div class="row">
                                                                                    <div class="col-sm-12">
                                                                                        <div class="col-sm-3 labelText1">
                                                                                            Price Level
                                                                                        </div>
                                                                                        <div class="col-sm-3" style="padding-right: 4px;">
                                                                                            <asp:TextBox ID="txtPriceLevel" CssClass="txtPriceLevel form-control" AutoPostBack="true"
                                                                                                OnTextChanged="txtPriceLevel_TextChanged" runat="server" />
                                                                                        </div>
                                                                                        <div class="col-sm-1 padding0">
                                                                                            <asp:LinkButton ID="lbtnSePrLevel" runat="server" CausesValidation="false" OnClick="lbtnSePrLevel_Click">
                                                                                 <span aria-hidden="true" class="glyphicon glyphicon-search"></span>
                                                                                            </asp:LinkButton>
                                                                                        </div>
                                                                                        <div class="col-sm-5">
                                                                                        </div>
                                                                                    </div>
                                                                                </div>
                                                                                <div class="row">
                                                                                    <div class="col-sm-12">
                                                                                        <div class="col-sm-3 labelText1">
                                                                                            Allow Discount
                                                                                        </div>
                                                                                        <div class="col-sm-3 marginTop">
                                                                                            <asp:CheckBox ID="chkAllowDiscount" OnCheckedChanged="chkAllowDiscount_CheckedChanged" Text=""
                                                                                                AutoPostBack="true" runat="server" />
                                                                                        </div>
                                                                                        <div class="col-sm-1">
                                                                                        </div>
                                                                                        <div class="col-sm-5">
                                                                                        </div>
                                                                                    </div>
                                                                                </div>
                                                                                <div class="row">
                                                                                    <div class="col-sm-12">
                                                                                        <div class="col-sm-3 labelText1">
                                                                                            Discount Rate
                                                                                        </div>
                                                                                        <div class="col-sm-3" style="padding-right: 4px;">
                                                                                            <asp:TextBox ID="txtDisRate" CssClass="txtDisRate form-control"
                                                                                                runat="server" />
                                                                                        </div>
                                                                                        <div class="col-sm-1">
                                                                                        </div>
                                                                                        <div class="col-sm-5">
                                                                                        </div>
                                                                                    </div>

                                                                                </div>
                                                                                <div class="row">
                                                                                    <div class="col-sm-12">
                                                                                        <div class="col-sm-3 labelText1">
                                                                                            Allow Edit Price
                                                                                        </div>
                                                                                        <div class="col-sm-3 marginTop" style="padding-left: 16px;">
                                                                                            <asp:CheckBox ID="chkAllowEditPrice" Text="" AutoPostBack="true"
                                                                                                OnCheckedChanged="chkAllowEditPrice_CheckedChanged" runat="server" />
                                                                                        </div>
                                                                                        <div class="col-sm-1">
                                                                                        </div>
                                                                                        <div class="col-sm-5">
                                                                                        </div>
                                                                                    </div>
                                                                                </div>
                                                                                <div class="row">
                                                                                    <div class="col-sm-12">
                                                                                        <div class="col-sm-3 labelText1">
                                                                                            Edit Rate
                                                                                        </div>
                                                                                        <div class="col-sm-3" style="padding-right: 4px;">
                                                                                            <asp:TextBox ID="txtEditRate" CssClass="txtEditRate form-control"
                                                                                                runat="server" />
                                                                                        </div>
                                                                                        <div class="col-sm-1">
                                                                                        </div>
                                                                                        <div class="col-sm-5">
                                                                                        </div>
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                        <div class="row">
                                                                            <br />
                                                                            <div class="col-sm-12">
                                                                                <div style="height: 200px; overflow-y: auto; overflow-x: hidden;">
                                                                                    <asp:GridView ID="dgvDiscount" CssClass="table table-hover table-striped" runat="server"
                                                                                        GridLines="None" PagerStyle-CssClass="cssPager" ShowHeaderWhenEmpty="true"
                                                                                        EmptyDataText="No data found..." AutoGenerateColumns="False">
                                                                                        <EditRowStyle BackColor="MidnightBlue" />
                                                                                        <Columns>
                                                                                            <asp:TemplateField>
                                                                                                <ItemTemplate>
                                                                                                    <div>
                                                                                                        <asp:LinkButton ID="btnDisSelect" runat="server" CausesValidation="false"
                                                                                                            OnClick="btnDisSelect_Click">
                                                                                                Select
                                                                                                        </asp:LinkButton>
                                                                                                    </div>
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>
                                                                                            <asp:TemplateField HeaderText="Profit Center">
                                                                                                <ItemTemplate>
                                                                                                    <asp:Label ID="lblProCenter" Text='<%# Bind("Sadd_pc") %>' Width="100px" runat="server"></asp:Label>
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>
                                                                                            <asp:TemplateField HeaderText="Invoice Type">
                                                                                                <ItemTemplate>
                                                                                                    <asp:Label ID="lblInvTp" Text='<%# Bind("Sadd_doc_tp") %>' Width="100px" runat="server"></asp:Label>
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>
                                                                                            <asp:TemplateField HeaderText="Price Book">
                                                                                                <ItemTemplate>
                                                                                                    <asp:Label ID="lblPrBook" Text='<%# Bind("Sadd_pb") %>' Width="120px" runat="server"></asp:Label>
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>
                                                                                            <asp:TemplateField HeaderText="Price Level">
                                                                                                <ItemTemplate>
                                                                                                    <asp:Label ID="lblPrLevel" Text='<%# Bind("Sadd_p_lvl") %>' Width="100px" runat="server"></asp:Label>
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>

                                                                                            <asp:TemplateField HeaderText="Allow Discount">
                                                                                                <ItemTemplate>
                                                                                                    <asp:CheckBox ID="chkAllowDis" Checked='<%# Convert.ToBoolean(Eval("sadd_is_disc"))?true:false %>' Enabled="false" runat="server"></asp:CheckBox>
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>
                                                                                            <asp:TemplateField HeaderText="Discount Rate">
                                                                                                <ItemTemplate>
                                                                                                    <asp:Label ID="lblDisRate" Text='<%# Bind("Sadd_disc_rt","{0:N2}") %>' runat="server"></asp:Label>
                                                                                                </ItemTemplate>
                                                                                                <ItemStyle CssClass="gridHeaderAlignRight" />
                                                                                                <HeaderStyle CssClass="gridHeaderAlignRight" />
                                                                                            </asp:TemplateField>
                                                                                            <asp:TemplateField HeaderText="">
                                                                                                <ItemTemplate>
                                                                                                    <asp:Label Text=' ' Width="20px" runat="server"></asp:Label>
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>
                                                                                            <asp:TemplateField HeaderText="Allow Edit Price">
                                                                                                <ItemTemplate>
                                                                                                    <asp:CheckBox ID="chkAllowEdPrice" Enabled="false" Checked='<%# Convert.ToBoolean(Eval("Sadd_is_edit"))?true:false %>' runat="server"></asp:CheckBox>
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>
                                                                                            <asp:TemplateField HeaderText="Edit Rate">
                                                                                                <ItemTemplate>
                                                                                                    <asp:Label ID="lblEditRate" Text='<%# Bind("Sadd_edit_rt","{0:N2}") %>' runat="server"></asp:Label>
                                                                                                </ItemTemplate>
                                                                                                <ItemStyle CssClass="gridHeaderAlignRight" />
                                                                                                <HeaderStyle CssClass="gridHeaderAlignRight" />
                                                                                            </asp:TemplateField>
                                                                                            <asp:TemplateField HeaderText="">
                                                                                                <ItemTemplate>
                                                                                                    <asp:Label Text=' ' Width="20px" runat="server"></asp:Label>
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>
                                                                                        </Columns>
                                                                                        <PagerStyle CssClass="cssPager"></PagerStyle>
                                                                                    </asp:GridView>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <asp:Button ID="btnSearchbtn" runat="server" Text="Button" Style="display: none;" />
            <asp:ModalPopupExtender ID="mpSearch" runat="server" TargetControlID="btnSearchbtn"
                PopupControlID="pnlpopup" PopupDragHandleControlID="divSearchheader" Drag="true" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>
        </ContentTemplate>
    </asp:UpdatePanel>

    <asp:Panel runat="server" ID="pnlpopup" DefaultButton="lbtnSearch">
        <asp:ScriptManagerProxy ID="ScriptManagerProxy17" runat="server"></asp:ScriptManagerProxy>
        <asp:UpdatePanel runat="server">
            <ContentTemplate>
                <div runat="server" id="test" class="panel panel-default height400 width850">
                    <asp:Label ID="lblvalue" runat="server" Text="Label" Visible="false"></asp:Label>
                    <div class="panel panel-default">
                        <div class="panel-heading height30" id="divSearchheader">
                            <div class="col-sm-11">
                            </div>
                            <div class="col-sm-1">
                                <asp:LinkButton ID="btnCloseSearchMP" runat="server" OnClick="btnCloseSearchMP_Click">
                             <span class="glyphicon glyphicon-remove" aria-hidden="true">Close</span>
                                </asp:LinkButton>
                            </div>
                        </div>
                        <div class="panel-body">
                            <div class="col-sm-12" id="Div10" runat="server">
                                <div class="row">
                                    <div class="col-sm-12">
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-12 height5">
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-12">
                                        <div class="col-sm-2 labelText1">
                                            Search by key
                                        </div>
                                        <asp:ScriptManagerProxy ID="ScriptManagerProxy10" runat="server"></asp:ScriptManagerProxy>
                                        <asp:UpdatePanel ID="UpdatePanel10" runat="server">
                                            <ContentTemplate>
                                                <div class="col-sm-3 paddingRight5">
                                                    <asp:DropDownList ID="ddlSearchbykey" runat="server" class="form-control">
                                                    </asp:DropDownList>
                                                </div>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                        <div class="col-sm-2 labelText1">
                                            Search by word
                                        </div>
                                        <div class="col-sm-4 paddingRight5">
                                            <asp:TextBox ID="txtSearchbyword" CausesValidation="false" placeholder="Search by word" class="form-control" AutoPostBack="true" runat="server" OnTextChanged="txtSearchbyword_TextChanged"></asp:TextBox>
                                        </div>
                                        <asp:ScriptManagerProxy ID="ScriptManagerProxy9" runat="server"></asp:ScriptManagerProxy>
                                        <asp:UpdatePanel ID="UpdatePanel9" runat="server">
                                            <ContentTemplate>
                                                <div class="col-sm-1 paddingLeft0">
                                                    <asp:LinkButton ID="lbtnSearch" runat="server" OnClick="lbtnSearch_Click">
                                            <span class="glyphicon glyphicon-search" aria-hidden="true"  ></span>
                                                    </asp:LinkButton>
                                                </div>
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:AsyncPostBackTrigger ControlID="txtSearchbyword" />
                                            </Triggers>
                                        </asp:UpdatePanel>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-12 height5">
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-12 height5">
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-12">
                                        <asp:ScriptManagerProxy ID="ScriptManagerProxy4" runat="server"></asp:ScriptManagerProxy>
                                        <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                                            <ContentTemplate>
                                                <asp:GridView ID="grdResult" CausesValidation="false" AllowPaging="true" runat="server" GridLines="None" CssClass="table table-hover table-striped" PageSize="7" PagerStyle-CssClass="cssPager" OnPageIndexChanging="grdResult_PageIndexChanging" OnSelectedIndexChanged="grdResult_SelectedIndexChanged">
                                                    <Columns>
                                                        <asp:ButtonField Text="Select" CommandName="Select" ItemStyle-Width="10" />
                                                    </Columns>
                                                </asp:GridView>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <asp:HiddenField ID="hdfTabIndex" runat="server" />
                <asp:HiddenField ID="hdfTabIndex2" runat="server" />
            </ContentTemplate>
        </asp:UpdatePanel>
    </asp:Panel>

    <script type="text/javascript">
        Sys.Application.add_load(fun);
        Sys.Application.add_load(fun2);
        function fun() {
            $('#myTab a').click(function (e) {
                e.preventDefault();
                $(this).tab('show');
                document.getElementById('<%=hdfTabIndex.ClientID %>').value = $(this).attr('href');
            });

            $(document).ready(function () {
                var tab = document.getElementById('<%= hdfTabIndex.ClientID%>').value;
                $('#myTab a[href="' + tab + '"]').tab('show');
            });
        };

        function fun2() {
            $('#tabControl a').click(function (e) {
                e.preventDefault();
                $(this).tab('show');
                document.getElementById('<%=hdfTabIndex2.ClientID %>').value = $(this).attr('href');
            });

            $(document).ready(function () {
                var tab = document.getElementById('<%= hdfTabIndex2.ClientID%>').value;
                $('#tabControl a[href="' + tab + '"]').tab('show');
            });
        };
    </script>

    <script type="text/javascript">
        function SetTab1() {
            $('#tabControl a[href="#home12"]').tab('show');
        };
        function SetTab2() {
            $('#tabControl a[href="#profile12"]').tab('show');
        };


    </script>

    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <asp:Button ID="btnpnlRedeemItems" runat="server" Text="Button" Style="display: none;" />
            <asp:ModalPopupExtender ID="pnlRedeemItemsMP" runat="server" TargetControlID="btnpnlRedeemItems"
                PopupControlID="pnlRedeemItems" PopupDragHandleControlID="pnlRedeemItemsDiv" Drag="true" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>
        </ContentTemplate>
    </asp:UpdatePanel>

    <asp:Panel runat="server" ID="pnlRedeemItems" DefaultButton="lbtnSearch" Style="display: none;">
        <asp:ScriptManagerProxy ID="ScriptManagerProxy11" runat="server"></asp:ScriptManagerProxy>
        <asp:UpdatePanel runat="server">
            <ContentTemplate>
                <div runat="server" id="Div1" class="panel panel-default height400 width850">
                    <asp:Label ID="Label8" runat="server" Text="Label" Visible="false"></asp:Label>
                    <div class="panel panel-default">
                        <div class="panel-heading height30" id="pnlRedeemItemsDiv">
                            <div class="col-sm-11">
                                <b>Redeem Items/ Company/ Price Book, Level</b>
                            </div>
                            <div class="col-sm-1">
                                <asp:LinkButton ID="btnClosePVRedeem" runat="server" OnClick="btnClosePVRedeem_Click">
                             <span class="glyphicon glyphicon-remove" aria-hidden="true">Close</span>
                                </asp:LinkButton>
                            </div>
                        </div>
                        <div class="panel-body">
                            <div class="col-sm-12" id="Div2" runat="server">
                                <asp:Panel ID="pnlRedeemItemsInner" runat="server">
                                    <div class="col-sm-12">
                                        <div class="col-sm-6 padding0">
                                            <div class="col-sm-4 paddingLeft0">
                                                Item
                                            </div>
                                            <div class="col-sm-6 padding0">
                                                <asp:TextBox ID="txtItem_rd" Style="text-transform: uppercase" runat="server" CssClass="form-control" AutoPostBack="true" OnTextChanged="txtItem_rd_TextChanged" />
                                            </div>
                                            <div class="col-sm-2">
                                                <asp:LinkButton ID="btnItem_rd" runat="server" CssClass="floatRight" OnClick="btnItem_rd_Click">
                                            <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                </asp:LinkButton>
                                            </div>
                                        </div>
                                        <div class="col-sm-6 padding0">
                                            <div class="col-sm-4 paddingLeft0">
                                            </div>
                                            <div class="col-sm-6 padding0">
                                                <asp:Button ID="btnLoadPara_rd" Text="Load" runat="server" OnClick="btnLoadPara_rd_Click" />
                                            </div>
                                            <div class="col-sm-2">
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-12 height5"></div>
                                    <div class="col-sm-12">
                                        <div class="col-sm-1 padding0">
                                            Excel Upload
                                        </div>
                                        <div class="col-sm-7">
                                            <asp:FileUpload ID="FileUpload1" runat="server" />
                                        </div>
                                        <div class="col-sm-4 padding0">
                                            <asp:Button Text="Excel Upload" runat="server" OnClick="btnUploadFile_rd_Click" />
                                            <asp:LinkButton ID="btnUploadFile_rd" runat="server" OnClick="btnUploadFile_rd_Click" Visible="false">
                                            <span class="glyphicon glyphicon-upload" aria-hidden="true"></span>Excel Upload
                                            </asp:LinkButton>
                                        </div>
                                    </div>
                                    <div class="col-sm-12 height5"></div>
                                    <div class="col-sm-12">
                                        <div class="col-sm-7">
                                            <asp:GridView ID="grvRedeemItems" AutoGenerateColumns="false" runat="server" CssClass="table table-hover table-striped" GridLines="None" ShowHeaderWhenEmpty="True" EmptyDataText="No data found...">
                                                <Columns>
                                                    <asp:BoundField DataField="code" HeaderText="Item Code" />
                                                    <asp:BoundField DataField="BRAND" HeaderText="Brand" />
                                                    <asp:BoundField DataField="CAT1" HeaderText="Main Category" />
                                                    <asp:BoundField DataField="CAT2" HeaderText="Sub Category" />
                                                    <asp:TemplateField HeaderText=" ">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="btnRemoveProfitCenter" runat="server" CssClass="floatleft" OnClientClick="return ConfirmDelProf()" OnClick="btnRemoveProfitCenter_Click">
                                                            <span class="glyphicon glyphicon-trash" aria-hidden="true"></span>
                                                            </asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                        <div class="col-sm-5">
                                            <div class="panel panel-default ">
                                                <div class="panel-heading pannelheading height16 paddingtop0">
                                                    <b>Redeem Company / Price Book, Level</b>
                                                </div>
                                                <div class="panel-body">
                                                    <div class="col-sm-12">
                                                        <div class="col-sm-4 paddingLeft0">
                                                            Company
                                                        </div>
                                                        <div class="col-sm-6 padding0">
                                                            <asp:DropDownList ID="cmbRdmAllowCompany" runat="server" CssClass="form-control"></asp:DropDownList>
                                                        </div>
                                                        <div class="col-sm-2">
                                                        </div>
                                                    </div>
                                                    <div class="col-sm-12 height5"></div>
                                                    <div class="col-sm-12">
                                                        <div class="col-sm-4 paddingLeft0">
                                                            Price Book
                                                        </div>
                                                        <div class="col-sm-6 padding0">
                                                            <asp:TextBox ID="txtRdmComPB" runat="server" CssClass="form-control" AutoPostBack="true" OnTextChanged="txtRdmComPB_TextChanged" />
                                                        </div>
                                                        <div class="col-sm-2">
                                                            <asp:LinkButton ID="btnRdmComPB" runat="server" CssClass="floatRight" OnClick="btnRdmComPB_Click">
                                                            <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                            </asp:LinkButton>
                                                        </div>
                                                    </div>
                                                    <div class="col-sm-12 height5"></div>
                                                    <div class="col-sm-12">
                                                        <div class="col-sm-4 paddingLeft0">
                                                            Price Level
                                                        </div>
                                                        <div class="col-sm-6 padding0">
                                                            <asp:TextBox ID="txtRdmComPBLvl" runat="server" CssClass="form-control" AutoPostBack="true" OnTextChanged="txtRdmComPBLvl_TextChanged" />
                                                        </div>
                                                        <div class="col-sm-2">
                                                            <asp:LinkButton ID="btnRdmComPBLvl" runat="server" CssClass="floatRight" OnClick="btnRdmComPBLvl_Click">
                                                             <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                            </asp:LinkButton>
                                                        </div>
                                                    </div>
                                                    <div class="col-sm-12 height5"></div>
                                                    <div class="col-sm-12">
                                                        <div class="col-sm-4 paddingLeft0">
                                                            Valid period
                                                        </div>
                                                        <div class="col-sm-6 padding0">
                                                            <asp:TextBox ID="txtPVRDMValiedPeriod" onkeydown="return jsDecimals(event);" runat="server" CssClass="form-control" />
                                                        </div>
                                                        <div class="col-sm-2">
                                                            Days
                                                        </div>
                                                    </div>

                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </asp:Panel>
                                <div class="col-sm-12 height5"></div>
                                <div class="col-sm-12">
                                    <div class="col-sm-10">
                                    </div>
                                    <div class="col-sm-1">
                                        <asp:Button ID="btnRedmPnlConfirm" Text="Confirm" runat="server" OnClientClick="return confirm('Do you want to confirm?')" OnClick="btnRedmPnlConfirm_Click" />
                                    </div>
                                    <div class="col-sm-1">
                                        <asp:Button ID="btnRedmPnlClear" Text="Clear" runat="server" OnClientClick="return confirm('Do you want to clear?')" OnClick="btnRedmPnlClear_Click" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <asp:HiddenField ID="HiddenField1" runat="server" />
            </ContentTemplate>
        </asp:UpdatePanel>
    </asp:Panel>

    <asp:UpdatePanel ID="UpdatePanel15" runat="server">
        <ContentTemplate>
            <asp:Button ID="pnlGVbtn" runat="server" Text="Button" Style="display: none;" />
            <asp:ModalPopupExtender ID="pnlGVMP" runat="server" Enabled="True" TargetControlID="pnlGVbtn"
                PopupControlID="pnlexcelComWar" PopupDragHandleControlID="pnlGVbtndiv" Drag="true" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>
        </ContentTemplate>
    </asp:UpdatePanel>

    <asp:Panel runat="server" ID="pnlexcelComWar">
        <div runat="server" id="Div9" class="panel panel-default height45 width700 ">
            <div class="panel panel-default">
                <div id="pnlGVbtndiv" class="panel-heading">
                    Gift Voucher Upload
                    <asp:LinkButton ID="btnGVClose" runat="server" OnClick="btnGVClose_Click" Style="float: right">
                             <span class="glyphicon glyphicon-remove" aria-hidden="true">Close</span>
                    </asp:LinkButton>
                    <div class="col-sm-11">
                        <asp:Label ID="lblalert2" runat="server" ForeColor="Red" Text="Label" Visible="false"></asp:Label>
                        <asp:Label ID="lblsuccess2" runat="server" ForeColor="Green" Visible="false"></asp:Label>
                    </div>
                    <div class="col-sm-1">
                    </div>
                </div>
                <div class="panel-body">
                    <div class="col-sm-12 paddingLeft0 paddingRight0">
                        <div class="row">
                            <div class="col-sm-12">
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-12 height5">
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-12" id="Div3" runat="server">
                                <div class="col-sm-5 paddingRight5">
                                    <asp:FileUpload ID="fileupexcelupload2" runat="server" />
                                </div>
                                <div class="col-sm-2 paddingRight5">
                                    <asp:Button ID="btnGvUpload" runat="server" Text="Upload" OnClientClick="return confirm('Do you want to upload the Gift voucher?')" OnClick="btnGvUpload_Click" />
                                </div>
                            </div>
                            <div class="col-sm-12 ">
                                <div id="divUpcompleted2" visible="false" runat="server" class="alert alert-info alert-success" role="alert">
                                    <div class="col-sm-9">
                                        <strong>Alert!</strong>
                                        <asp:Label ID="Label6" Text="Excel file upload completed. Do you want to process?" runat="server"></asp:Label>
                                    </div>
                                    <div class="col-sm-3">
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </asp:Panel>

    <script src="<%=Request.ApplicationPath%>Js/jquery.min.js"></script>
    <script src="<%=Request.ApplicationPath%>Js/bootstrap.min.js"></script>
    <script src="<%=Request.ApplicationPath%>Js/jquery.toastmessage.js"></script>


    <script>
        Sys.Application.add_load(func);
        function func() {
            $('.txtDisRate ,.txtEditRate').keypress(function (evt) {
                // var ch = (evt.which) ? evt.which : evt.keyCode;
                var ch = evt.which;
                var str = $(this).val();
                console.log(ch);
                if (ch == 46) {
                    if (str.indexOf(".") == -1) {
                        return true;
                    } else {
                        return false;
                    }
                }
                else if ((ch == 8) || (ch == 9) || (ch == 46) || (ch == 0)) {
                    return true;
                }
                else if (ch > 47 && ch < 58) {
                    return true;
                }
                else {
                    return false;
                }
            })
        }
    </script>

</asp:Content>
