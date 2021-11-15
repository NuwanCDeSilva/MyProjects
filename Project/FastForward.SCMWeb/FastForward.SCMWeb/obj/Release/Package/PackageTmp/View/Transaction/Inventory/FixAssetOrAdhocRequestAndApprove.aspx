<%@ Page Title="" Language="C#" MasterPageFile="~/View/AdminSite.Master" AutoEventWireup="true" CodeBehind="FixAssetOrAdhocRequestAndApprove.aspx.cs" Inherits="FastForward.SCMWeb.View.Transaction.Inventory.FixAssetOrAdhocRequestAndApprove" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/UserControls/ucPaymodes.ascx" TagPrefix="uc1" TagName="ucPaymodes" %>
<%@ Register Src="~/UserControls/ucTransportMethode.ascx" TagPrefix="uc1" TagName="ucTransportMethode" %>


<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
        <script src="<%=Request.ApplicationPath%>Js/jquery.min.js"></script>
    <script src="<%=Request.ApplicationPath%>Js/bootstrap.min.js"></script>
    <script src="<%=Request.ApplicationPath%>Js/jquery.toastmessage.js"></script>
    <script>
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
                position: 'top-left',
                type: 'notice',
                closeText: '',
                close: function () { console.log("toast is closed ..."); }
            });
        }
        function showWarningToast() {
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
            if (jQuery('.toast-item-wrapper') != null) {
                jQuery('.toast-item-wrapper').remove();
            }
            $().toastmessage('showErrorToast', "Error Dialog which is fading away ...");
        }
        function showStickyErrorToast(value) {
            if (jQuery('.toast-item-wrapper') != null) {
                jQuery('.toast-item-wrapper').remove();
            }
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
        }
    </script>

    <script type="text/javascript">
        function ConfSendReq() {
            var selectedvalueOrd = confirm("Do you want to send request ?");
            if (selectedvalueOrd) {
                return true;
            } else {
                return false;
            }
        };
        function ConfAppFGAP() {
            var selectedvalueOrd = confirm("Do you want to approve ?");
            if (selectedvalueOrd) {
                return true;
            } else {
                return false;
            }
        };
        function ConfirmClear() {
            var selectedvalueOrd = confirm("Do you want to clear all details ?");
            if (selectedvalueOrd) {
                return true;
            } else {
                return false;
            }
        };
        function ConfirmSave() {
            var selectedvalueOrd = confirm("Do you want to clear all details ?");
            if (selectedvalueOrd) {
                return true;
            } else {
                return false;
            }
        };
        function ConfirmCancel() {
            var selectedvalueOrd = confirm("Are you sure you want to cancel ?");
            if (selectedvalueOrd) {
                return true;
            } else {
                return false;
            }
        };
        function ConfirmReject() {
            var selectedvalueOrd = confirm("Are you sure you want to Reject ?");
            if (selectedvalueOrd) {
                return true;
            } else {
                return false;
            }
        };
        function ConfirmApprove() {
            var selectedvalueOrd = confirm("Are you sure you want to approve ?");
            if (selectedvalueOrd) {
                return true;
            } else {
                return false;
            }
        };
        function ConfirmConfirm() {
            var selectedvalueOrd = confirm("Are you sure you want to confirm ?");
            if (selectedvalueOrd) {
                return true;
            } else {
                return false;
            }
        };

        function ConfirmDelete() {
            var selectedvalueOrd = confirm("Are you sure you want to delete ?");
            if (selectedvalueOrd) {
                return true;
            } else {
                return false;
            }
        };
        
    </script>
    <style>
        .panel {
            margin-top:0px; margin-bottom:0px;padding-bottom:0px; padding-top:0px;
        }
        .GridHeight {
            height:110px; overflow:auto;
        }
        .panel-body {
            margin-top:0px; margin-bottom:0px;padding-bottom:0px; padding-top:0px;
        }
    </style>

    <style type="text/css">
        .DatePanel {
            position: absolute;
            background-color: #FFFFFF;
            border: 1px solid #646464;
            color: #000000;
            z-index: 1;
            font-family: tahoma,verdana,helvetica;
            font-size: 11px;
            padding: 4px;
            text-align: center;
            cursor: default;
            line-height: 20px;
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
    <asp:UpdateProgress ID="UpdateProgress1" DisplayAfter="10"
        runat="server" AssociatedUpdatePanelID="upPanelMain">
        <ProgressTemplate>
            <div class="divWaiting">
                <asp:Label ID="lblWait" runat="server"
                    Text="Please wait... " />
                <asp:Image ID="imgWait" runat="server"
                    ImageAlign="Middle" ImageUrl="~/images/icons/ajax-loader.gif" />
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <asp:UpdatePanel runat="server" ID="upPanelMain">
        <ContentTemplate>
            <div class="row">
                <div class="panel panel-default">
                    <div class="col-sm-12">
                        <div class="row">
                            <div class="col-sm-12">
                                <div class="row buttonRow">
                                    <div class="col-sm-12">
                                        <div class="col-sm-9">
                                        </div>
                                        <div class="col-sm-3 padding0">
                                            <div class="col-sm-5">
                                                <asp:LinkButton ID="lbtnSendReq" OnClientClick="return ConfAppFGAP()" CausesValidation="false" runat="server" CssClass="floatRight" Visible="false" OnClick="lbtnSendReq_Click">
                                                    <span class="glyphicon glyphicon-saved" aria-hidden="true" ></span>Approve FGAP
                                                </asp:LinkButton>
                                                <asp:LinkButton ID="lbtnSave" OnClientClick="return ConfSendReq()" CausesValidation="false" runat="server" CssClass="floatRight"  OnClick="lbtnSendReq_Click">
                                        <span class="glyphicon glyphicon-saved" aria-hidden="true" ></span>Send Requests
                                                </asp:LinkButton>
                                            </div>
                                            <div class="col-sm-5">
                                                <asp:LinkButton OnClick="lbtnCancel_Click" ID="lbtnCancel" CausesValidation="false" runat="server" CssClass="floatRight" OnClientClick="return ConfirmCancel();">
                                        <span class="glyphicon glyphicon-remove" aria-hidden="true" ></span>Cancel Request
                                                </asp:LinkButton>
                                            </div>
                                            <div class="col-sm-2 padding0">
                                                <asp:LinkButton ID="lbtnClear" runat="server" CausesValidation="false" CssClass="floatRight"
                                                    OnClick="lbtnClear_Click" OnClientClick="return ConfirmClear();">
                                        <span class="glyphicon glyphicon-refresh" aria-hidden="true" ></span>Clear
                                                </asp:LinkButton>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-12">
                                <div class="panel panel-default">
                                    <div class="panel panel-heading">
                                        <strong><b>Fixed-Assets Transfer/ FGAP</b></strong>
                                    </div>
                                    <div class="panel panel-body">
                                        <div class="row" style="margin-top: 2px;">
                                            <div class="col-sm-12">
                                                <div class="col-sm-4 padding0">
                                                    <div class="col-sm-3  labelText1">
                                                        Transation Type
                                                    </div>
                                                    <div class="col-sm-3 padding0">
                                                        <asp:DropDownList ID="ddlTransActionTp" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlTransActionTp_SelectedIndexChanged" class="form-control">
                                                        </asp:DropDownList>
                                                    </div>
                                                    <div class="col-sm-1 labelText1">
                                                        Action
                                                    </div>
                                                    <div class="col-sm-4 paddingRight0">
                                                        <asp:DropDownList ID="ddlAction" AutoPostBack="true" OnSelectedIndexChanged="ddlAction_SelectedIndexChanged" runat="server" class="form-control">
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                                <div class="col-sm-2">
                                                    <asp:Panel runat="server" ID="pnlSendLoc">
                                                        <div class="col-sm-5 padding0 labelText1">
                                                            Send Location
                                                        </div>
                                                        <div class="col-sm-6 padding0">
                                                            <asp:TextBox ID="txtSendLoc" AutoPostBack="true" OnTextChanged="txtSendLoc_TextChanged" runat="server" class="form-control">
                                                            </asp:TextBox>
                                                        </div>
                                                        <div class="col-sm-1" style="padding-left: 3px; padding-right: 3px;">
                                                            <asp:LinkButton ID="lbtnSeSendLoc" runat="server" Visible="true" OnClick="lbtnSeSendLoc_Click">
                                                                                <span class="glyphicon glyphicon-search"></span>
                                                            </asp:LinkButton>
                                                        </div>
                                                    </asp:Panel>
                                                </div>
                                                <div class="col-sm-3 ">
                                                    <div class="col-sm-2 labelText1 padding0">
                                                        <asp:Label ID="lblRefNo" Text="" runat="server" />
                                                    </div>
                                                    <asp:Panel runat="server" DefaultButton="lbtnSeRefNo">
                                                        <div class="col-sm-6 padding0">
                                                            <asp:TextBox ID="txtRefNo" AutoPostBack="true" OnTextChanged="txtRefNo_TextChanged" runat="server" class="form-control">
                                                            </asp:TextBox>
                                                        </div>
                                                        <div class="col-sm-1" style="padding-left: 3px; padding-right: 0px;">
                                                            <asp:LinkButton OnClick="lbtnSeRefNo_Click" ID="lbtnSeRefNo" runat="server" Visible="true">
                                                                                <span class="glyphicon glyphicon-search"></span>
                                                            </asp:LinkButton>
                                                        </div>
                                                        
                                                            <asp:Button ID="lbtnRefNoOk" OnClick="lbtnRefNoOk_Click" runat="server" CssClass="buttoncolor btn-sm" Text="OK" Style="line-height: 6px; display:none;"></asp:Button>
                                                        
                                                    </asp:Panel>
                                                </div>
                                               
                                                <div class="col-sm-3 padding0">
                                                    <div class="col-sm-3 padding0">
                                                        Document Status
                                                        </div>
                                                    <div class="col-sm-4 paddingRight0">
                                                        <asp:TextBox ID="txtStatus" AutoPostBack="true" OnTextChanged="txtStatus_TextChanged" Text="NEW" ReadOnly="true" runat="server" class="form-control">
                                                        </asp:TextBox>
                                                    </div>
                                                    <div class="col-sm-2">
                                                        Date
                                                    </div>
                                                    <div class="col-sm-3 padding0">
                                                        <asp:Label runat="server" ID="txtDate" CssClass="form-control" />
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-sm-12">
                                                <div class="panel panel-default">
                                                    <div class="panel panel-heading">
                                                        <strong>Request Item Details</strong>
                                                    </div>
                                                    <div class="panel panel-body">
                                                        <div class="row">
                                                            <div class="col-sm-2">
                                                                <div class="col-sm-4 padding0 labelText1">
                                                                    Item Code
                                                                </div>
                                                                <div class="col-sm-7 padding0">
                                                                    <asp:TextBox AutoPostBack="true" ID="txtItmCode" OnTextChanged="txtItmCode_TextChanged" runat="server" class="txtItmCode form-control">
                                                                    </asp:TextBox>
                                                                </div>
                                                                <div class="col-sm-1" style="padding-left: 3px; padding-right: 3px;">
                                                                    <asp:LinkButton ID="lbtnSeItmCode" OnClick="lbtnSeItmCode_Click" runat="server" Visible="true">
                                                                                <span class="glyphicon glyphicon-search"></span>
                                                                    </asp:LinkButton>
                                                                </div>
                                                            </div>
                                                            <div class="col-sm-7">
                                                                <div class="col-sm-1  labelText1">
                                                                    Model
                                                                </div>
                                                                <div class="col-sm-3">
                                                                    <asp:TextBox ID="txtItmModel" runat="server" class="form-control">
                                                                    </asp:TextBox>
                                                                </div>
                                                                <div class="col-sm-2 padding0 labelText1 text-right">
                                                                    Description
                                                                </div>
                                                                <div class="col-sm-4" style="padding-left: 3px; padding-right: 0px;">
                                                                    <asp:TextBox ID="txtItmDes" runat="server" class="form-control">
                                                                    </asp:TextBox>
                                                                </div>
                                                            </div>
                                                            <div class="col-sm-3">
                                                                <div class="col-sm-3 labelText1">
                                                                    Qty
                                                                </div>
                                                                <asp:Panel runat="server">
                                                                    <div class="col-sm-6">
                                                                        <asp:TextBox ID="txtItmQty" AutoPostBack="true" 
                                                                             onpaste="return false"                                                                            OnTextChanged="txtItmQty_TextChanged" runat="server" 
                                                                            class="form-control txtItmQty rightMc">
                                                                        </asp:TextBox>
                                                                    </div>
                                                                    <div class="col-sm-1" style="padding-left: 3px; padding-right: 0px; margin-top: -5px;">
                                                                        <div class="buttonRow">
                                                                            <asp:LinkButton ID="lbtnAddItm" runat="server" Visible="true" OnClick="lbtnAddItm_Click">
                                                                  <span class="glyphicon glyphicon-arrow-down"></span>
                                                                            </asp:LinkButton>
                                                                        </div>
                                                                    </div>
                                                                </asp:Panel>
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <asp:Panel runat="server" ID="pnlPriceDetails">
                                                                <div class="col-sm-9 padding0">
                                                                    <div class="col-sm-3 padding0">
                                                                        <div class="col-sm-4 labelText1">
                                                                            Price Book
                                                                        </div>
                                                                        <div class="col-sm-5 padding0">
                                                                            <asp:TextBox ID="txtPriceBook" AutoPostBack="true" OnTextChanged="txtPriceBook_TextChanged" runat="server" class="form-control">
                                                                            </asp:TextBox>
                                                                        </div>
                                                                        <div class="col-sm-1" style="padding-left: 3px; padding-right: 0px;">
                                                                            <asp:LinkButton ID="lbtnSePriceBook" runat="server" Visible="true" OnClick="lbtnSePriceBook_Click">
                                                                        <span class="glyphicon glyphicon-search"></span>
                                                                            </asp:LinkButton>
                                                                        </div>
                                                                    </div>
                                                                    <div class="col-sm-3 padding0">
                                                                        <div class="col-sm-4 labelText1 text-right padding0">
                                                                            Price Book Level
                                                                        </div>
                                                                        <div class="col-sm-5" style="padding-left: 3px; padding-right: 0px;">
                                                                            <asp:TextBox ID="txtPbLvl" runat="server" class="form-control"
                                                                                AutoPostBack="true" OnTextChanged="txtPbLvl_TextChanged">
                                                                            </asp:TextBox>
                                                                        </div>
                                                                        <div class="col-sm-1" style="padding-left: 3px; padding-right: 0px;">
                                                                            <asp:LinkButton ID="lbtnSePbLvl" runat="server" Visible="true" OnClick="lbtnSePbLvl_Click">
                                                          <span class="glyphicon glyphicon-search"></span>
                                                                            </asp:LinkButton>
                                                                        </div>
                                                                    </div>
                                                                    <div class="col-sm-3 padding0">
                                                                        <div class="col-sm-4 labelText1 text-right">
                                                                            Location
                                                                        </div>
                                                                        <div class="col-sm-5" style="padding-left: 3px; padding-right: 0px;">
                                                                            <asp:TextBox ID="txtLocation" AutoPostBack="true" OnTextChanged="txtLocation_TextChanged"  runat="server" class="form-control">
                                                                            </asp:TextBox>
                                                                        </div>
                                                                        <div class="col-sm-1" style="padding-left: 3px; padding-right: 0px;">
                                                                            <asp:LinkButton ID="lbtnSeLocation" runat="server" Visible="true" OnClick="lbtnSeLocation_Click">
                                                          <span class="glyphicon glyphicon-search"></span>
                                                                            </asp:LinkButton>
                                                                        </div>
                                                                    </div>
                                                                    <div class="col-sm-3 padding0">
                                                                        <div class="col-sm-4 labelText1">
                                                                            Profit Center
                                                                        </div>
                                                                        <div class="col-sm-5" style="padding-left: 3px; padding-right: 0px;">
                                                                            <asp:TextBox ID="txtProfitCenter" runat="server" AutoPostBack="true" OnTextChanged="txtProfitCenter_TextChanged" class="form-control">
                                                                            </asp:TextBox>
                                                                        </div>
                                                                        <div class="col-sm-1" style="padding-left: 3px; padding-right: 0px;">
                                                                            <asp:LinkButton ID="lbtnSePrCenter" runat="server" Visible="true" OnClick="lbtnSePrCenter_Click">
                                                          <span class="glyphicon glyphicon-search"></span>
                                                                            </asp:LinkButton>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class="col-sm-2">
                                                                    <div class="col-sm-4 labelText1">
                                                                        Unit Price
                                                                    </div>
                                                                    <div class="col-sm-8">
                                                                        <asp:TextBox ID="txtUPrice" ReadOnly="true" runat="server" class="form-control">
                                                                        </asp:TextBox>
                                                                    </div>
                                                                </div>
                                                            </asp:Panel>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-sm-12">
                                                <div class="panel panel-default">
                                                    <div class="panel panel-body">
                                                        <div class="GridHeight">
                                                            <asp:GridView ID="dgvItmDes" CssClass="table table-hover table-striped" runat="server" GridLines="None"
                                                                PagerStyle-CssClass="cssPager" EmptyDataText="No data found..." ShowHeaderWhenEmpty="true" AutoGenerateColumns="False"
                                                                AllowPaging="false" PageSize="3">
                                                                <Columns>
                                                                    <asp:TemplateField>
                                                                        <ItemTemplate>
                                                                            <asp:LinkButton ID="lbtnAddGridItem" OnClick="lbtnAddGridItem_Click" Width="10px" runat="server" Visible="true">
                                                                <span class="glyphicon glyphicon-arrow-down"></span>
                                                                            </asp:LinkButton>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField>
                                                                        <ItemTemplate>
                                                                            <asp:LinkButton ID="lbtnDelGridItem" OnClick="lbtnDelGridItem_Click" 
                                                                                OnClientClick="return ConfirmDelete()"
                                                                                runat="server" Width="10px" Visible="true">
                                                                <span class="glyphicon glyphicon-trash"></span>
                                                                            </asp:LinkButton>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Item Code">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblItmCd" Text='<%# Bind("Iadd_claim_itm") %>' Width="150px" runat="server" />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Model">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblModel" Text='<%# Bind("Iadd_anal2") %>'  Width="150px" runat="server" />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Description">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblDesc" Text='<%# Bind("Iadd_anal3") %>'  Width="300px" runat="server" />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Qty">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblQty" Text='<%# Bind("Iadd_anal1","{0:N2}") %>' runat="server" />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    
                                                                    <asp:TemplateField Visible="false">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblLine" Text='<%# Bind("Iadd_line") %>' runat="server" />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField Visible="false">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblSts" Text='<%# Bind("Iadd_stus") %>' runat="server" />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField Visible="false">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblUnitPrice" Text='<%# Bind("Iadd_app_val") %>' runat="server" />
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
                                        <div class="row">
                                            <div class="col-sm-12">
                                                <div class="panel panel-default">
                                                    <div class="panel panel-heading" style="height:22px;">
                                                        <div class="col-sm-3 padding0">
                                                            <strong>Approval Item Details</strong>
                                                        </div>
                                                        <div class="col-sm-7">
                                                            Available Serial List
                                                        </div>
                                                    </div>
                                                    <div class="panel panel-body">
                                                        <div class="col-sm-3 padding0">
                                                            <div>
                                                                <br />
                                                                <div class="col-sm-3 padding0">
                                                                    Filter by status
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <asp:DropDownList ID="ddlFilterSts" AutoPostBack="true" OnSelectedIndexChanged="ddlFilterSts_SelectedIndexChanged" runat="server" class="form-control">
                                                                    </asp:DropDownList>
                                                                </div>
                                                                <div class="col-sm-1">
                                                                    <asp:LinkButton ID="lbtnSeSts" runat="server" Visible="true" OnClick="lbtnSeSts_Click">
                                                             <span class="glyphicon glyphicon-search"></span>
                                                                    </asp:LinkButton>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="col-sm-7">
                                                            <div class="panel panel-default">
                                                                <div class="panel panel-body">
                                                                    <div class="GridHeight">
                                                                        <asp:GridView ID="dgvAvailableSerials" CssClass="table table-hover table-striped" runat="server" GridLines="None"
                                                                            PagerStyle-CssClass="cssPager" EmptyDataText="No data found..." ShowHeaderWhenEmpty="true" AutoGenerateColumns="False"
                                                                            OnDataBound="dgvAvailableSerials_DataBound"
                                                                            OnDataBinding="dgvAvailableSerials_DataBinding"
                                                                            AllowPaging="false" PageSize="3">
                                                                            <Columns>
                                                                                <asp:TemplateField>
                                                                                    <ItemTemplate>
                                                                                        <asp:LinkButton ID="lbtnAddAvaSerial" runat="server" Visible="true" OnClick="lbtnAddAvaSerial_Click">
                                                                                        <span class="glyphicon glyphicon-arrow-down"></span>
                                                                                        </asp:LinkButton>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="Item Code">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblItmCd" Text='<%# Bind("Tus_itm_cd") %>' runat="server" />
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="Serial #">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblSer1" Text='<%# Bind("Tus_ser_1") %>' runat="server" />
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                 <asp:TemplateField HeaderText="Status">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblStsDesc" Text='<%# Bind("Tus_itm_stus") %>' runat="server" />
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="Status" Visible="false">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblSts" Text='<%# Bind("Tus_itm_stus") %>' runat="server" />
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="Serial ID">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblSerId" Text='<%# Bind("Tus_ser_id") %>' runat="server" />
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="Document #">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblLine" Text='<%# Bind("Tus_doc_no") %>' runat="server" />
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                            </Columns>
                                                                            <PagerStyle CssClass="cssPager"></PagerStyle>
                                                                        </asp:GridView>
                                                                    </div>

                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="col-sm-2">
                                                            <div class="row buttonRow">
                                                                <asp:LinkButton ID="lbtnAppAvaSer" runat="server" Visible="true" OnClick="lbtnAppAvaSer_Click" OnClientClick="return ConfirmApprove()">
                                                             <span class="glyphicon glyphicon-ok-circle"></span>Approve
                                                                </asp:LinkButton>
                                                            </div>
                                                            <div class="row buttonRow">
                                                                <asp:LinkButton OnClick="lbtnRejAvaSer_Click" ID="lbtnRejAvaSer" runat="server" Visible="true">
                                                             <span class="glyphicon glyphicon-remove"></span>Reject
                                                                </asp:LinkButton>
                                                            </div>
                                                            <div class="row buttonRow">
                                                                <asp:LinkButton ID="lbtnAddAvaSer" OnClick="lbtnAddAvaSer_Click" runat="server" Visible="false" OnClientClick="return ConfirmReject()">
                                                             <span class="glyphicon glyphicon-arrow-down"></span>Add
                                                                </asp:LinkButton>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-sm-12">
                                                <div class="panel panel-default">
                                                    <div class="panel panel-heading" style="height:22px;">
                                                        <div class="col-sm-3 paddingLeft0">
                                                            <strong>Transfering Item Details</strong>
                                                        </div>
                                                        <div class="col-sm-7">
                                                            Selected Serial List
                                                        </div>
                                                    </div>
                                                    <div class="panel panel-body">
                                                        <div class="col-sm-3 paddingLeft0">
                                                            <asp:Panel runat="server" ID="pnlPayDetails">
                                                                <div class="row">
                                                                    <div class="col-sm-12">
                                                                        <div class="row">
                                                                            <div class="col-sm-4 labelText1">
                                                                                Approved Value
                                                                            </div>
                                                                            <div class="col-sm-4 labelText1">
                                                                                Price Defference
                                                                            </div>
                                                                            <div class="col-sm-4  labelText1">
                                                                                <asp:Button ID="lbtnPaymentComplete" Text="Payment Compleate" runat="server" OnClick="lbtnPaymentComplete_Click" />
                                                                            </div>
                                                                        </div>
                                                                        <div class="row">
                                                                            <div class="col-sm-4">
                                                                                <asp:TextBox ID="txtAppVal" ReadOnly="true" runat="server" CssClass="form-control text-right" />
                                                                            </div>
                                                                            <div class="col-sm-4">
                                                                                <asp:TextBox ID="txtPriceDef" ReadOnly="true" runat="server" CssClass="form-control text-right" />
                                                                            </div>
                                                                            <div class="col-sm-4">
                                                                            </div>
                                                                        </div>
                                                                        <div class="row">
                                                                            <div class="col-sm-4 labelText1">
                                                                                Approved Value
                                                                            </div>
                                                                            <div class="col-sm-4 labelText1">
                                                                                Price Defference
                                                                            </div>
                                                                            <div class="col-sm-4">
                                                                            </div>
                                                                        </div>
                                                                        <div class="row">
                                                                            <div class="col-sm-4">
                                                                                <asp:TextBox ID="txtCollectVal" ReadOnly="true" runat="server" CssClass="form-control text-right" />
                                                                            </div>
                                                                            <div class="col-sm-4">
                                                                                <asp:TextBox ID="txtRecVal" ReadOnly="true" runat="server" CssClass="form-control text-right" />
                                                                            </div>
                                                                            <div class="col-sm-4">
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </asp:Panel>
                                                        </div>
                                                        <div class="col-sm-7">
                                                            <div class="panel panel-default">
                                                                <div class="panel panel-body">
                                                                    <div class="GridHeight">
                                                                        <asp:GridView ID="dgvApproveItms" CssClass="table table-hover table-striped" runat="server" GridLines="None"
                                                                            PagerStyle-CssClass="cssPager" EmptyDataText="No data found..." ShowHeaderWhenEmpty="true" AutoGenerateColumns="False"
                                                                            AllowPaging="false" PageSize="3">
                                                                            <Columns>
                                                                                <asp:TemplateField>
                                                                                    <ItemTemplate>
                                                                                        <asp:LinkButton ID="lbtnDelApproveItm" OnClientClick="return ConfirmDelete()" OnClick="lbtnDelApproveItm_Click" runat="server" Visible="true">
                                                                                            <span class="glyphicon glyphicon-trash"></span>
                                                                                        </asp:LinkButton>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="Item Code">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblItmCd" Text='<%# Bind("Tus_itm_cd") %>' runat="server" />
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="Serial #">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblSer1" Text='<%# Bind("Tus_ser_1") %>' runat="server" />
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="Status">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblStsDesc" Text='<%# Bind("Tus_itm_stus") %>' runat="server" />
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="Status" Visible="false">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblSts" Text='<%# Bind("Tus_itm_stus") %>' runat="server" />
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="Serial ID">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblSerId" Text='<%# Bind("Tus_ser_id") %>' runat="server" />
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="Document #">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblLine" Text='<%# Bind("Tus_doc_no") %>' runat="server" />
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                            </Columns>
                                                                            <PagerStyle CssClass="cssPager"></PagerStyle>
                                                                        </asp:GridView>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="col-sm-2">
                                                            
                                                            <div class="row buttonRow">
                                                                <asp:LinkButton ID="lbtnConfAppser" OnClick="lbtnConfAppser_Click" OnClientClick="return ConfirmConfirm()" runat="server" Visible="true">
                                                             <span class="glyphicon glyphicon-saved"></span>Confirm
                                                                </asp:LinkButton>
                                                            </div>
                                                            <div class="row buttonRow">
                                                                <asp:LinkButton ID="lbtnRejAppSer" runat="server"  OnClick="lbtnRejAppSer_Click" OnClientClick="return ConfirmReject()">
                                                             <span class="glyphicon glyphicon-remove"></span>Reject
                                                                </asp:LinkButton>
                                                            </div>
                                                            <div class="row buttonRow">
                                                                <asp:LinkButton ID="lbtnDelAppser" runat="server" Visible="false">
                                                             <span class="glyphicon glyphicon-ok-sign"></span>Approve
                                                                </asp:LinkButton>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-sm-12">
                                                <asp:Panel runat="server" ID="pnlPayMode">
                                                    <div>
                                                        <uc1:ucPaymodes runat="server" ID="ucPaymodes" />
                                                    </div>
                                                </asp:Panel>
                                            </div>
                                        </div>
                                        <div class="row" style="padding-top:2px;">
                                            <div class="col-sm-12">
                                                <div class="col-sm-2 labelText1">
                                                    Remarks
                                                </div>
                                                <div class="col-sm-6 ">
                                                    <asp:TextBox runat="server" class="form-control" ID="txtRemarks" />
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

 <asp:UpdatePanel runat="server" ID="update">
        <ContentTemplate>
            <asp:Button ID="Button3" runat="server" Text="Button" Style="display: none;" />
            <asp:ModalPopupExtender ID="PopupSearch" runat="server" Enabled="True" TargetControlID="Button3"
                PopupControlID="testPanel" CancelControlID="btnClose" PopupDragHandleControlID="PopupHeader" Drag="true" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:Panel runat="server" ID="testPanel" DefaultButton="lbtnSearch">
        <div runat="server" id="test" class="panel panel-primary Mheight">
            <asp:Label ID="Label3" runat="server" Text="Label" Visible="false"></asp:Label>
            <div class="panel panel-default" style="height:350px;">
                <div class="panel-heading">
                    <asp:LinkButton ID="btnClose" runat="server">
                             <span class="glyphicon glyphicon-remove" style="margin-left:560px" aria-hidden="true">Close</span>
                    </asp:LinkButton>
                </div>
                <div class="panel-body">
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
                                        <asp:DropDownList ID="cmbSearchbykey" runat="server" class="form-control" AutoPostBack="false">
                                        </asp:DropDownList>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                            <div class="col-sm-2 labelText1">
                                Search by word
                            </div>

                            <asp:Label ID="lblSearchType" runat="server" Text="" Visible="False"></asp:Label>
                            <div class="col-sm-4 paddingRight5">
                                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                    <ContentTemplate>
                                        <asp:TextBox ID="txtSearchbyword" CausesValidation="false" class="form-control" AutoPostBack="false" runat="server"></asp:TextBox>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                            <asp:ScriptManagerProxy ID="ScriptManagerProxy9" runat="server"></asp:ScriptManagerProxy>
                            <asp:UpdatePanel ID="UpdatePanel9" runat="server">
                                <ContentTemplate>
                                    <div class="col-sm-1 paddingLeft0">
                                        <asp:LinkButton ID="lbtnSearch" runat="server" OnClick="lbtnSearch_Click">
                                     <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
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
                            <asp:ScriptManagerProxy ID="ScriptManagerProxy1" runat="server"></asp:ScriptManagerProxy>
                            <asp:UpdatePanel ID="UpdatePanel8" runat="server">
                                <ContentTemplate>
                                    <asp:GridView ID="dgvResult" CausesValidation="false" runat="server" AllowPaging="True" GridLines="None" 
                                        CssClass="table table-hover table-striped" PageSize="10" PagerStyle-CssClass="cssPager" 
                                        ShowHeaderWhenEmpty="true" EmptyDataText="No data found..."
                                        OnPageIndexChanging="dgvResult_PageIndexChanging" OnSelectedIndexChanged="dgvResult_SelectedIndexChanged">
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
    </asp:Panel>

     
    <script>
        if (typeof jQuery == 'undefined') {
            alert('jQuery is not loaded');
        }
        Sys.Application.add_load(func);
        function func() {
            $('.txtItmQty').mousedown(function (e) {
                if (e.button == 2) {
                    alert('This functionality is disabled !');
                    return false;
                } else {
                    return true;
                }
            });

            $('.txtItmQty').keypress(function (evt) {
                // var ch = (evt.which) ? evt.which : evt.keyCode;
                var charCode = evt.which;
                var str = $(this).val();
                //console.log(charCode);
                if ((charCode == 8) || (charCode == 9) || (charCode == 0)) {
                    return true;
                }
                else if (str.length < 3) {
                    if (charCode > 47 && charCode < 58) {
                        return true;
                    }
                    else {
                        return false;
                    }
                }
                else {
                    $(this).value = str.substr(0, 3);
                    alert('Maximum 3 characters are allowed ');
                    return false;
                }
            });

            $('.txtItmQty').keypress(function (evt) {
                var charCode = (evt.which) ? evt.which : evt.which;
                console.log(charCode);
                var str = $('.txtItmCode').val();
                if (str == "" && charCode != 0) {
                    showStickyWarningToast('Please enter item code frist !');
                    $('.txtItmCode').focus();
                    return false;
                }
            });
        }
    </script>
</asp:Content>
