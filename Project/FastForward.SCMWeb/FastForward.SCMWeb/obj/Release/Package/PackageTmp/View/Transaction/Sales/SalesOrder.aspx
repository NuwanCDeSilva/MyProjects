<%@ Page Title="" Language="C#" MasterPageFile="~/View/AdminSite.Master" AutoEventWireup="true" CodeBehind="SalesOrder.aspx.cs" EnableEventValidation="false" Inherits="FastForward.SCMWeb.View.Transaction.Sales.SalesOrder" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/UserControls/ucCustomer.ascx" TagPrefix="uc1" TagName="ucCustomer" %>



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
        }

        function ConfirmClearForm() {
            var selectedvalueOrd = confirm("Do you want to clear all details ?");
            if (selectedvalueOrd) {
                document.getElementById('<%=txtconfirmclear.ClientID %>').value = "Yes";
            } else {
                document.getElementById('<%=txtconfirmclear.ClientID %>').value = "No";
            }
        };

        function ConfirmPlaceOrder() {
            var selectedvalueOrdPlace = confirm("Do you want to save ?");
            if (selectedvalueOrdPlace) {
                document.getElementById('<%=txtconfirmplaceord.ClientID %>').value = "Yes";
            } else {
                document.getElementById('<%=txtconfirmplaceord.ClientID %>').value = "No";
            }
        };

        function ConfirmApproveOrder() {
            var selectedvalueOrdPlace = confirm("Do you want to approve ?");
            if (selectedvalueOrdPlace) {
                document.getElementById('<%=txtapprove.ClientID %>').value = "Yes";
            } else {
                document.getElementById('<%=txtapprove.ClientID %>').value = "No";
            }
        };

        function ConfirmCancelOrder() {
            var selectedvalueOrdPlace = confirm("Do you want to cancel ?");
            if (selectedvalueOrdPlace) {
                document.getElementById('<%=txtcancel.ClientID %>').value = "Yes";
            } else {
                document.getElementById('<%=txtcancel.ClientID %>').value = "No";
            }
        };

        function ConfirmRejectOrder() {
            var selectedvalueOrdPlace = confirm("Do you want to reject ?");
            if (selectedvalueOrdPlace) {
                document.getElementById('<%=txtreject.ClientID %>').value = "Yes";
            } else {
                document.getElementById('<%=txtreject.ClientID %>').value = "No";
            }
        };

        function showSuccessToast() {
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

        function Enable() {
            return;
        }

    </script>

    <%--    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script src="//ajax.googleapis.com/ajax/libs/jquery/2.1.1/jquery.min.js"></script>
    <script src="../../Js/jquery-1.7.2.min.js"></script>--%>

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
        runat="server" AssociatedUpdatePanelID="UpdatePanelMain">

        <ProgressTemplate>
            <div class="divWaiting">
                <asp:Label ID="lblWait" runat="server"
                    Text="Please wait... " />
                <asp:Image ID="imgWait" runat="server"
                    ImageAlign="Middle" ImageUrl="~/images/icons/ajax-loader.gif" />
            </div>
        </ProgressTemplate>

    </asp:UpdateProgress>

    <asp:UpdatePanel ID="UpdatePanelMain" runat="server">

        <ContentTemplate>

            <asp:HiddenField ID="txtconfirmclear" runat="server" />
            <asp:HiddenField ID="txtconfirmplaceord" runat="server" />
            <asp:HiddenField ID="txtapprove" runat="server" />
            <asp:HiddenField ID="txtcancel" runat="server" />
            <asp:HiddenField ID="txtreject" runat="server" />

            <div class="panel panel-default marginLeftRight5">

                <div class="row">

                    <div class="col-sm-8  buttonrow">

                        <asp:UpdatePanel runat="server">
                            <ContentTemplate>


                                <%--                                <div visible="false" class="alert alert-success" role="alert" runat="server" id="divok">

                                    <div class="col-sm-11  buttonrow ">
                                        <strong>Well done!</strong>
                                        <asp:Label ID="lblok" runat="server"></asp:Label>
                                    </div>

                                    <div class="col-sm-1  buttonrow">
                                        <div class="col-sm-3  buttonrow">
                                            <asp:LinkButton ID="lbtndivokclose" runat="server" CausesValidation="false" CssClass="floatright" OnClick="lbtndivokclose_Click">
                                        <span class="glyphicon glyphicon-remove " aria-hidden="true" ></span>
                                            </asp:LinkButton>
                                        </div>
                                    </div>

                                </div>

                                <div visible="false" class="alert alert-danger" role="alert" runat="server" id="divalert">
                                    <div class="col-sm-11  buttonrow ">
                                        <strong>Alert!</strong>
                                        <asp:Label ID="lblalert" runat="server"></asp:Label>
                                    </div>
                                    <div class="col-sm-1  buttonrow">
                                        <div class="col-sm-3  buttonrow">
                                            <asp:LinkButton ID="lbtndicalertclose" runat="server" CausesValidation="false" CssClass="floatright" OnClick="lbtndicalertclose_Click">
                                        <span class="glyphicon glyphicon-ok" aria-hidden="true"></span>
                                            </asp:LinkButton>
                                        </div>
                                    </div>
                                </div>

                                <div visible="false" class="alert alert-info" role="alert" runat="server" id="Divinfo">
                                    <div class="col-sm-11  buttonrow ">
                                        <strong>Alert!</strong>
                                        <asp:Label ID="lblinfo" runat="server"></asp:Label>
                                    </div>
                                    <div class="col-sm-1  buttonrow">
                                        <div class="col-sm-3  buttonrow">
                                            <asp:LinkButton ID="lbtndivinfoclose" runat="server" CausesValidation="false" CssClass="floatright" OnClick="lbtndivinfoclose_Click">
                                        <span class="glyphicon glyphicon-ok" aria-hidden="true"></span>
                                            </asp:LinkButton>
                                        </div>
                                    </div>
                                </div>--%>
                            </ContentTemplate>
                        </asp:UpdatePanel>

                    </div>

                    <asp:UpdatePanel runat="server">
                        <ContentTemplate>

                            <div class="col-sm-4  buttonRow paddingRight5">

                                <div class="col-sm-3 paddingRight0">
                                    <asp:LinkButton ID="btnSave" CausesValidation="false" runat="server" CssClass="floatRight" OnClientClick="ConfirmPlaceOrder();" OnClick="btnSave_Click">
                            <span class="glyphicon glyphicon-saved" aria-hidden="true" ></span>Save/Process
                                    </asp:LinkButton>
                                </div>

                                <div class="col-sm-3">
                                    <asp:LinkButton ID="lbtnapprove" runat="server" CssClass="floatRight" CausesValidation="false" OnClientClick="ConfirmApproveOrder();" OnClick="lbtnapprove_Click">
                            <span class="glyphicon glyphicon-thumbs-up" aria-hidden="true"></span>Approve
                                    </asp:LinkButton>
                                </div>

                                <div class="col-sm-3">
                                    <asp:LinkButton ID="lbtnprintord" CausesValidation="false" runat="server" CssClass="floatRight" OnClientClick="ConfirmClearForm();" OnClick="lbtnprintord_Click">
                                                        <span class="glyphicon glyphicon-refresh" aria-hidden="true" ></span>Clear
                                    </asp:LinkButton>
                                </div>

                                <div class="col-sm-3 ">
                                    <div class="dropdown">
                                        <a id="dLabel" data-toggle="dropdown" role="button" aria-haspopup="true" aria-expanded="false">
                                            <span class="glyphicon glyphicon-menu-hamburger floatRight"></span>
                                        </a>
                                        <div class="dropdown-menu menupopup dropdownmenusalesorder" aria-labelledby="dLabel">
                                            <div class="row">

                                                <div class="col-sm-12">

                                                    <div class="col-sm-12 paddingRight0 ">
                                                        <asp:LinkButton ID="lbtncancel" CausesValidation="false" runat="server" OnClientClick="ConfirmCancelOrder();" OnClick="lbtncancel_Click">
                                                        <span class="glyphicon glyphicon-remove" aria-hidden="true"></span>Cancel
                                                        </asp:LinkButton>
                                                    </div>

                                                    <div class="row">
                                                        <div class="col-sm-12 height10">
                                                        </div>
                                                    </div>

                                                    <div class="col-sm-12 paddingRight0 ">
                                                        <asp:LinkButton ID="lbtnreject" CausesValidation="false" runat="server" OnClientClick="ConfirmRejectOrder();" OnClick="lbtnreject_Click">
                                                        <span class="glyphicon glyphicon-thumbs-down" aria-hidden="true"></span>Reject
                                                        </asp:LinkButton>
                                                    </div>

                                                    <div class="row">
                                                        <div class="col-sm-12 height10">
                                                        </div>
                                                    </div>

                                                    <div class="col-sm-12 paddingRight0">
                                                        <asp:LinkButton ID="lbtnkititem" CausesValidation="false" runat="server" OnClick="lbtnkititem_Click">
                                                        <span class="glyphicon glyphicon-registration-mark" aria-hidden="true"></span>Request
                                                        </asp:LinkButton>
                                                    </div>

                                                    <div class="row">
                                                        <div class="col-sm-12 height10">
                                                        </div>
                                                    </div>

                                                    <div class="col-sm-12 paddingRight0">
                                                        <asp:LinkButton ID="lbtncustomer" CausesValidation="false" runat="server" OnClick="lbtncustomer_Click">
                                                        <span class="glyphicon glyphicon-user" aria-hidden="true" ></span>Customer
                                                        </asp:LinkButton>
                                                    </div>

                                                    <div class="row">
                                                        <div class="col-sm-12 height10">
                                                        </div>
                                                    </div>

                                                    <div class="col-sm-12 paddingRight0">
                                                        <asp:LinkButton ID="lbtndiscount" CausesValidation="false" runat="server" OnClick="lbtndiscount_Click">
                                                        <span class="glyphicon glyphicon-usd" aria-hidden="true" ></span>Discount
                                                        </asp:LinkButton>
                                                    </div>

                                                    <div class="row">
                                                        <div class="col-sm-12 height10">
                                                        </div>
                                                    </div>

                                                    <div class="col-sm-12 paddingRight0">
                                                        <asp:LinkButton ID="lbtnupload" CausesValidation="false" runat="server" OnClick="lbtnupload_Click">
                                                        <span class="glyphicon glyphicon-road" aria-hidden="true" ></span>Delivery
                                                        </asp:LinkButton>
                                                    </div>

                                                </div>

                                                <div class="row">
                                                    <div class="col-sm-12 height10">
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

                <div class="row">

                    <div class="panel-body">

                        <div class="col-sm-12">

                            <div class="panel panel-default paneldefaultheightorderplan salesinvfirstpnl" id="1s">
                                <div class="panel-heading pannelheading ">
                                    <strong>Sales Order</strong>
                                </div>

                                <asp:UpdatePanel runat="server">
                                    <ContentTemplate>
                                        <div class="panel-body" id="2ss">

                                            <div class="row">

                                                <div class="col-sm-12">

                                                    <div class="col-sm-2">
                                                        <div class="row">

                                                            <div class="col-sm-3 labelText1">
                                                                Date
                                                            </div>

                                                            <div>
                                                                <div class="col-sm-8">
                                                                    <asp:TextBox ID="txtdate" runat="server" CssClass="form-control" Format="dd/MMM/yyyy"
                                                                        onkeypress="return RestrictSpace()" Enabled="false"></asp:TextBox>
                                                                    <asp:CalendarExtender ID="CalendarExtender3" runat="server" TargetControlID="txtdate"
                                                                        PopupButtonID="lbtnimgselectdate" Format="dd/MMM/yyyy">
                                                                    </asp:CalendarExtender>
                                                                </div>

                                                                <div id="caldv" class="col-sm-1 paddingLeft0">
                                                                    <asp:LinkButton ID="lbtnimgselectdate" TabIndex="1" CausesValidation="false" runat="server">
                                                        <span class="glyphicon glyphicon-calendar" aria-hidden="true"></span>
                                                                    </asp:LinkButton>
                                                                </div>
                                                            </div>

                                                        </div>
                                                    </div>

                                                    <div class="col-sm-2">
                                                        <div class="row">

                                                            <div class="col-sm-2 labelText1">
                                                                Type
                                                            </div>
                                                            <div class="col-sm-9">
                                                                <asp:DropDownList ID="cmbInvType" runat="server" AutoPostBack="true" TabIndex="2" CssClass="form-control"></asp:DropDownList>
                                                            </div>
                                                        </div>
                                                    </div>

                                                    <div class="col-sm-2">
                                                        <div class="row">

                                                            <div class="col-sm-4 labelText1">
                                                                Doc.Ref.No
                                                            </div>
                                                            <div class="col-sm-8">
                                                                <asp:TextBox ID="txtdocrefno" runat="server" TabIndex="3" CssClass="form-control"></asp:TextBox>
                                                            </div>

                                                        </div>
                                                    </div>

                                                    <div class="col-sm-2">
                                                        <div class="row">

                                                            <div class="col-sm-4 labelText1">
                                                                Manual Ref
                                                            </div>
                                                            <div class="col-sm-8 ">
                                                                <asp:TextBox ID="txtManualRefNo" runat="server" TabIndex="4" CssClass="form-control"></asp:TextBox>
                                                            </div>

                                                        </div>
                                                    </div>

                                                    <div class="col-sm-2">
                                                        <div class="row">

                                                            <div class="col-sm-2 labelText1">
                                                                Or.No
                                                            </div>

                                                            <div class="col-sm-9 ">
                                                                <asp:TextBox ID="txtInvoiceNo" runat="server" CssClass="form-control" AutoPostBack="true" OnTextChanged="txtInvoiceNo_TextChanged"></asp:TextBox>
                                                                <asp:Label ID="lblordstus" runat="server" ForeColor="Red" Font-Bold="true" Visible="false"></asp:Label>
                                                            </div>

                                                            <div class="col-sm-1 paddingLeft0">
                                                                <asp:LinkButton ID="lbtnsupplier" runat="server" TabIndex="5" CausesValidation="false" OnClick="lbtnsupplier_Click">
                                                        <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                                </asp:LinkButton>
                                                            </div>

                                                        </div>
                                                    </div>

                                                    <div class="col-sm-2">
                                                        <div class="row">

                                                            <div class="col-sm-4 labelText1">
                                                                Currency
                                                            </div>

                                                            <div class="col-sm-7 ">
                                                                <asp:TextBox ID="txtcurrency" runat="server" CssClass="form-control" AutoPostBack="true"></asp:TextBox>
                                                                <asp:Label ID="lblcurrency" Text="Select Currency" runat="server"></asp:Label>
                                                            </div>

                                                            <div class="col-sm-1 paddingLeft0">
                                                                <asp:LinkButton ID="lbtncurrency" runat="server" TabIndex="6" CausesValidation="false" OnClick="lbtncurrency_Click">
                                                        <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                                </asp:LinkButton>
                                                            </div>

                                                        </div>
                                                    </div>

                                                </div>

                                            </div>

                                        </div>
                                    </ContentTemplate>
                                </asp:UpdatePanel>

                                <div class="row">
                                    <div class="col-sm-12 height5">
                                    </div>
                                </div>


                                <asp:UpdatePanel runat="server">
                                    <ContentTemplate>
                                        <div class="row">

                                            <div class="col-sm-6">

                                                <div class="panel panel-default paneldefaultheightorderplan" id="1">
                                                    <div class="panel-heading pannelheading ">
                                                        Customer
                                                    </div>
                                                    <div class="panel-body" id="2">

                                                        <div class="row">

                                                            <div class="col-sm-12">

                                                                <div class="col-sm-4">
                                                                    <div class="row">

                                                                        <div class="col-sm-3 labelText1">
                                                                            Code
                                                                        </div>
                                                                        <div class="col-sm-7 paddingRight5">
                                                                            <asp:TextBox ID="txtCustomer" runat="server" CssClass="form-control" AutoPostBack="true" OnTextChanged="txtCustomer_TextChanged"></asp:TextBox>
                                                                        </div>
                                                                        <div class="col-sm-2 paddingLeft0">
                                                                            <asp:LinkButton ID="lbtncode" CausesValidation="false" TabIndex="7" runat="server" OnClick="lbtncode_Click">
                                                        <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                                            </asp:LinkButton>
                                                                        </div>
                                                                    </div>

                                                                </div>

                                                                <div class="col-sm-4">
                                                                    <div class="row">

                                                                        <div class="col-sm-3 labelText1">
                                                                            NIC
                                                                        </div>
                                                                        <div class="col-sm-7 paddingRight5">
                                                                            <asp:TextBox ID="txtNIC" runat="server" CssClass="form-control" AutoPostBack="true" OnTextChanged="txtNIC_TextChanged"></asp:TextBox>
                                                                        </div>
                                                                        <div class="col-sm-2 paddingLeft0">
                                                                            <asp:LinkButton ID="btnSearch_NIC" runat="server" TabIndex="8" OnClick="btnSearch_NIC_Click">
                                                        <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                                            </asp:LinkButton>
                                                                        </div>
                                                                    </div>

                                                                </div>

                                                                <div class="col-sm-4">
                                                                    <div class="row">

                                                                        <div class="col-sm-3 labelText1">
                                                                            Mobile
                                                                        </div>
                                                                        <div class="col-sm-7 paddingRight5">
                                                                            <asp:TextBox ID="txtMobile" runat="server" CssClass="form-control" AutoPostBack="true" OnTextChanged="txtMobile_TextChanged"></asp:TextBox>
                                                                        </div>
                                                                        <div class="col-sm-2 paddingLeft0">
                                                                            <asp:LinkButton ID="btnSearch_Mobile" runat="server" TabIndex="9" CausesValidation="false" OnClick="btnSearch_Mobile_Click">
                                                        <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                                            </asp:LinkButton>
                                                                        </div>
                                                                    </div>

                                                                </div>

                                                                <%--    <div class="col-sm-3">
                                                                    <div class="row">

                                                                        <div class="col-sm-3 labelText1">
                                                                            Loyalty No
                                                                        </div>
                                                                        <div class="col-sm-7 paddingRight5">
                                                                            <asp:TextBox ID="txtLoyalty" runat="server" AutoPostBack="true" CssClass="form-control" OnTextChanged="txtLoyalty_TextChanged"></asp:TextBox>
                                                                        </div>
                                                                        <div class="col-sm-2 paddingLeft0">
                                                                            <asp:LinkButton ID="btnSearch_Loyalty" runat="server" TabIndex="10" CausesValidation="false" OnClick="btnSearch_Loyalty_Click">
                                                        <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                                            </asp:LinkButton>
                                                                        </div>
                                                                    </div>

                                                                </div>--%>
                                                            </div>

                                                        </div>

                                                        <div class="row">
                                                            <div class="col-sm-12 height5">
                                                            </div>
                                                        </div>

                                                        <div class="row">

                                                            <div class="col-sm-12">

                                                                <div class="col-sm-3">
                                                                    <div class="row">

                                                                        <div class="col-sm-3 labelText1">
                                                                            Name
                                                                        </div>
                                                                        <div class="col-sm-7 paddingRight5">
                                                                            <asp:DropDownList runat="server" ID="cmbTitle" AutoPostBack="true" TabIndex="11" CssClass="form-control" OnSelectedIndexChanged="cmbTitle_SelectedIndexChanged">

                                                                                <asp:ListItem>MR.</asp:ListItem>
                                                                                <asp:ListItem>MRS.</asp:ListItem>
                                                                                <asp:ListItem>MS.</asp:ListItem>
                                                                                <asp:ListItem>MISS.</asp:ListItem>
                                                                                <asp:ListItem>DR.</asp:ListItem>
                                                                                <asp:ListItem>REV.</asp:ListItem>
                                                                            </asp:DropDownList>
                                                                        </div>
                                                                        <div class="col-sm-2 paddingLeft0">
                                                                            <asp:TextBox ID="txtCusName" runat="server" Width="328px" TabIndex="12" CssClass="form-control"></asp:TextBox>
                                                                        </div>
                                                                    </div>
                                                                </div>

                                                            </div>

                                                        </div>

                                                        <div class="row">
                                                            <div class="col-sm-12 height5">
                                                            </div>
                                                        </div>

                                                        <div class="row">

                                                            <div class="col-sm-12">

                                                                <div class="col-sm-3">
                                                                    <div class="row">

                                                                        <div class="col-sm-3 labelText1">
                                                                            Address
                                                                        </div>
                                                                        <div class="col-sm-6 paddingRight5">
                                                                            <asp:TextBox ID="txtAddress1" runat="server" TabIndex="13" CssClass="form-control salesinvoaddresstxt"></asp:TextBox>
                                                                        </div>
                                                                    </div>
                                                                </div>

                                                            </div>

                                                        </div>

                                                        <div class="row">
                                                            <div class="col-sm-12 height5">
                                                            </div>
                                                        </div>

                                                        <div class="row">

                                                            <div class="col-sm-12">

                                                                <div class="col-sm-3">

                                                                    <div class="row">

                                                                        <div class="col-sm-3 labelText1">
                                                                        </div>
                                                                        <div class="col-sm-6 paddingRight5">
                                                                            <asp:TextBox ID="txtAddress2" runat="server" TabIndex="14" CssClass="form-control salesinvoaddresstxt"></asp:TextBox>
                                                                        </div>

                                                                    </div>

                                                                </div>

                                                            </div>

                                                        </div>

                                                        <div class="row">
                                                            <div class="col-sm-12 height5">
                                                            </div>
                                                        </div>

                                                        <div class="row">

                                                            <div class="col-sm-6">
                                                                <div class="row">

                                                                    <div class="col-sm-3 labelText1">
                                                                    </div>
                                                                    <div class="col-sm-3 labelText1">
                                                                    </div>
                                                                    <div class="col-sm-6 paddingRight5">
                                                                        <asp:LinkButton ID="LinkButton3" runat="server" OnClick="LinkButton7_Click" Visible="false">
                                                                    <span class="glyphicon glyphicon-king" aria-hidden="true"></span>
                                                                        </asp:LinkButton>
                                                                    </div>
                                                                </div>

                                                            </div>


                                                            <div class="col-sm-6">

                                                                <div class="row">

                                                                    <div class="col-sm-4 labelText1">
                                                                        Dispatch Location
                                                                    </div>
                                                                    <div class="col-sm-7 paddingRight5">
                                                                        <asp:TextBox ID="txtlocation" runat="server" CssClass="form-control"></asp:TextBox>
                                                                    </div>
                                                                    <div class="col-sm-1 paddingLeft0">
                                                                        <asp:LinkButton ID="lbtnloc" runat="server" TabIndex="15" CausesValidation="false" OnClick="lbtnloc_Click">
                                                        <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                                        </asp:LinkButton>
                                                                    </div>
                                                                </div>

                                                            </div>


                                                        </div>

                                                    </div>
                                                </div>

                                            </div>

                                            <div class="col-sm-3">

                                                <div class="panel panel-default paneldefaultheightorderplan" id="3">
                                                    <div class="panel-heading pannelheading ">
                                                    </div>
                                                    <div class="panel-body" id="4">

                                                        <div class="row">

                                                            <div class="col-sm-12">

                                                                <div class="col-sm-12">
                                                                    <div class="row">

                                                                        <div class="col-sm-8 labelText1">
                                                                            Account Balance
                                                                        </div>
                                                                        <div class="col-sm-4">
                                                                            <asp:Label runat="server" ID="lblAccountBalance"></asp:Label>
                                                                        </div>

                                                                    </div>
                                                                </div>

                                                            </div>

                                                        </div>

                                                        <div class="row">
                                                            <div class="col-sm-12 height5">
                                                            </div>
                                                        </div>

                                                        <div class="row">

                                                            <div class="col-sm-12">

                                                                <div class="col-sm-12">
                                                                    <div class="row">

                                                                        <div class="col-sm-8 labelText1">
                                                                            Available Credit
                                                                        </div>
                                                                        <div class="col-sm-4">
                                                                            <asp:Label runat="server" ID="lblAvailableCredit"></asp:Label>
                                                                        </div>

                                                                    </div>
                                                                </div>

                                                            </div>

                                                        </div>

                                                        <div class="row">
                                                            <div class="col-sm-12 height5">
                                                            </div>
                                                        </div>

                                                        <div class="row">

                                                            <div class="col-sm-12">

                                                                <div class="col-sm-12">
                                                                    <div class="row">

                                                                        <div class="col-sm-8 labelText1">
                                                                            Tax Payable
                                                                        </div>
                                                                        <div class="col-sm-4">
                                                                            <asp:CheckBox runat="server" TabIndex="16" ID="chkTaxPayable" AutoPostBack="true" />
                                                                        </div>

                                                                    </div>
                                                                </div>

                                                            </div>

                                                        </div>

                                                        <div class="row">
                                                            <div class="col-sm-12 height5">
                                                            </div>
                                                        </div>

                                                        <div class="row">

                                                            <div class="col-sm-12">

                                                                <div class="col-sm-12">
                                                                    <div class="row">

                                                                        <div class="col-sm-8 labelText1">
                                                                            SVat Status
                                                                        </div>
                                                                        <div class="col-sm-4">
                                                                            <asp:Label runat="server" ID="lblSVatStatus"></asp:Label>
                                                                        </div>

                                                                    </div>
                                                                </div>

                                                            </div>

                                                        </div>

                                                        <div class="row">
                                                            <div class="col-sm-12 height5">
                                                            </div>
                                                        </div>

                                                        <div class="row">

                                                            <div class="col-sm-12">

                                                                <div class="col-sm-12">
                                                                    <div class="row">

                                                                        <div class="col-sm-8 labelText1">
                                                                            Exempt Status
                                                                        </div>
                                                                        <div class="col-sm-4">
                                                                            <asp:Label runat="server" ID="lblVatExemptStatus"></asp:Label>
                                                                        </div>

                                                                    </div>
                                                                </div>

                                                            </div>

                                                        </div>

                                                        <div class="row">
                                                            <div class="col-sm-12 height5">
                                                            </div>
                                                        </div>

                                                    </div>
                                                </div>

                                            </div>

                                            <div class="col-sm-3">

                                                <div class="panel panel-default paneldefaultheightorderplan" id="5">
                                                    <div class="panel-heading pannelheading ">
                                                    </div>
                                                    <div class="panel-body" id="6">

                                                        <div class="row">

                                                            <div class="col-sm-12">

                                                                <div class="col-sm-12">
                                                                    <div class="row">

                                                                        <div class="col-sm-3 labelText1">
                                                                            Sales Exe.
                                                                        </div>
                                                                        <div class="col-sm-9">
                                                                            <asp:DropDownList ID="cmbExecutive" TabIndex="17" runat="server" AutoPostBack="true" CssClass="form-control"></asp:DropDownList>
                                                                        </div>

                                                                    </div>
                                                                </div>

                                                            </div>

                                                        </div>

                                                        <div class="row">
                                                            <div class="col-sm-12 height5">
                                                            </div>
                                                        </div>

                                                        <div class="row">

                                                            <div class="col-sm-12">

                                                                <div class="col-sm-12">
                                                                    <div class="row">

                                                                        <div class="col-sm-3 labelText1">
                                                                            PO #
                                                                        </div>
                                                                        <div class="col-sm-9">
                                                                            <asp:TextBox runat="server" TabIndex="18" ID="txtPoNo" CssClass="form-control" OnTextChanged="txtPoNo_TextChanged"></asp:TextBox>
                                                                        </div>

                                                                    </div>
                                                                </div>

                                                            </div>

                                                        </div>

                                                        <div class="row">
                                                            <div class="col-sm-12 height5">
                                                            </div>
                                                        </div>
                                                        <asp:Panel runat="server" Visible="false">
                                                            <div class="row">

                                                                <div class="col-sm-12">

                                                                    <div class="col-sm-12">
                                                                        <div class="row">

                                                                            <div class="col-sm-10 labelText1">
                                                                                <strong>Generate Purchase Order</strong>
                                                                            </div>
                                                                            <div class="col-sm-1">
                                                                                <asp:CheckBox ID="chkpo" runat="server" />
                                                                            </div>

                                                                        </div>
                                                                    </div>

                                                                </div>

                                                            </div>
                                                        </asp:Panel>
                                                        <div class="row">
                                                            <div class="col-sm-12 height5">
                                                            </div>
                                                        </div>


                                                        <div class="row">

                                                            <div class="col-sm-12">

                                                                <div class="col-sm-12">
                                                                    <div class="row">

                                                                        <div class="col-sm-3 labelText1">
                                                                            Quotation 
                                                                        </div>

                                                                        <div class="col-sm-1 labelText1">
                                                                            <asp:CheckBox runat="server" ID="chkQuotation" TabIndex="20" AutoPostBack="true" OnCheckedChanged="chkQuotation_CheckedChanged" />
                                                                        </div>

                                                                        <div class="col-sm-8">
                                                                            <asp:TextBox runat="server" ID="txtQuotation" TabIndex="21" CssClass="form-control" AutoPostBack="true" OnTextChanged="txtQuotation_TextChanged"></asp:TextBox>
                                                                        </div>

                                                                    </div>
                                                                </div>

                                                            </div>

                                                        </div>

                                                        <div class="row">
                                                            <div class="col-sm-12 height5">
                                                            </div>
                                                        </div>

                                                        <div class="row">

                                                            <div class="col-sm-12">

                                                                <div class="col-sm-12">
                                                                    <div class="row">

                                                                        <div class="col-sm-3 labelText1">
                                                                            Promotor
                                                                        </div>
                                                                        <div class="col-sm-9">
                                                                            <asp:DropDownList runat="server" TabIndex="22" ID="cmbTechnician" AutoPostBack="true" CssClass="form-control" OnSelectedIndexChanged="cmbTechnician_SelectedIndexChanged"></asp:DropDownList>
                                                                        </div>

                                                                    </div>
                                                                </div>

                                                            </div>

                                                        </div>

                                                        <div class="row">
                                                            <div class="col-sm-12 height5">
                                                            </div>
                                                        </div>

                                                        <div class="row" style="visibility: hidden">

                                                            <div class="col-sm-12">

                                                                <div class="col-sm-12">
                                                                    <div class="row">

                                                                        <div class="col-sm-3 labelText1">
                                                                        </div>
                                                                        <div class="col-sm-9">
                                                                            <asp:TextBox runat="server" ID="txtPromotor" TabIndex="23" CssClass="form-control"></asp:TextBox>
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



                                <asp:UpdatePanel runat="server">
                                    <ContentTemplate>
                                        <div class="row">

                                            <div class="col-sm-12">

                                                <div class="panel panel-default " id="y5">

                                                    <div class="panel-heading pannelheading">

                                                        <div class="row">
                                                            <div class="col-sm-2">
                                                                Item Details
                                                            </div>
                                                            <div class="col-sm-1">
                                                                <asp:CheckBox ID="chkPickGV" TabIndex="24" runat="server" Visible="false" AutoPostBack="true" />
                                                            </div>
                                                        </div>

                                                    </div>

                                                    <div class="panel-body" id="d6">

                                                        <div class="row">
                                                            <div class="col-sm-12">

                                                                <div class="col-sm-2">

                                                                    <div class="row">

                                                                        <div class="col-sm-4 labelText1">
                                                                            Serial
                                                                        </div>
                                                                        <div class="col-sm-7 paddingRight5">
                                                                            <asp:TextBox ID="txtSerialNo" runat="server" AutoPostBack="true" CssClass="form-control" OnTextChanged="txtSerialNo_TextChanged"></asp:TextBox>
                                                                        </div>
                                                                        <div class="col-sm-1 paddingLeft0">
                                                                            <asp:LinkButton ID="btnSearch_Serial" TabIndex="25" runat="server" CausesValidation="false" OnClick="btnSearch_Serial_Click">
                                                        <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                                            </asp:LinkButton>
                                                                        </div>
                                                                    </div>

                                                                </div>

                                                                <div class="col-sm-2">

                                                                    <div class="row">

                                                                        <div class="col-sm-4 labelText1">
                                                                            Item
                                                                        </div>
                                                                        <div class="col-sm-7 paddingRight5">
                                                                            <asp:TextBox ID="txtItem" runat="server" AutoPostBack="true" CssClass="form-control" OnTextChanged="txtItem_TextChanged"></asp:TextBox>
                                                                        </div>
                                                                        <div class="col-sm-1 paddingLeft0">
                                                                            <asp:LinkButton ID="btnSearch_Item" runat="server" TabIndex="26" CausesValidation="false" OnClick="btnSearch_Item_Click">
                                                        <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                                            </asp:LinkButton>
                                                                        </div>
                                                                    </div>

                                                                </div>

                                                                <div class="col-sm-2">

                                                                    <div class="row">

                                                                        <div class="col-sm-4 labelText1">
                                                                            Book
                                                                        </div>
                                                                        <div class="col-sm-8 paddingRight5">
                                                                            <asp:DropDownList ID="cmbBook" runat="server" TabIndex="27" AutoPostBack="true" CssClass="form-control"></asp:DropDownList>
                                                                        </div>
                                                                    </div>

                                                                </div>

                                                                <div class="col-sm-2">

                                                                    <div class="row">

                                                                        <div class="col-sm-4 labelText1">
                                                                            Level
                                                                        </div>
                                                                        <div class="col-sm-8 paddingRight5">
                                                                            <asp:DropDownList ID="cmbLevel" runat="server" TabIndex="28" AutoPostBack="true" CssClass="form-control" OnSelectedIndexChanged="cmbLevel_SelectedIndexChanged"></asp:DropDownList>
                                                                        </div>
                                                                    </div>

                                                                </div>

                                                                <div class="col-sm-2">

                                                                    <div class="row">

                                                                        <div class="col-sm-4 labelText1">
                                                                            Status
                                                                        </div>
                                                                        <div class="col-sm-8 paddingRight5">
                                                                            <asp:DropDownList ID="cmbStatus" runat="server" TabIndex="29" AutoPostBack="true" CssClass="form-control" OnSelectedIndexChanged="cmbStatus_SelectedIndexChanged"></asp:DropDownList>
                                                                        </div>
                                                                    </div>

                                                                </div>

                                                                <div class="col-sm-2">

                                                                    <div class="row">

                                                                        <div class="col-sm-4 labelText1">
                                                                            Qty
                                                                        </div>
                                                                        <div class="col-sm-8 paddingRight5">
                                                                            <asp:TextBox ID="txtQty" runat="server" TabIndex="30" onkeydown="return jsDecimals(event);" AutoPostBack="true" OnTextChanged="txtQty_TextChanged" CssClass="form-control"></asp:TextBox>
                                                                        </div>

                                                                    </div>

                                                                </div>

                                                            </div>

                                                        </div>

                                                        <div class="row">
                                                            <div class="col-sm-12 height5">
                                                            </div>
                                                        </div>

                                                        <div class="row">

                                                            <div class="col-sm-12">

                                                                <div class="col-sm-2">

                                                                    <div class="row">

                                                                        <div class="col-sm-4 labelText1">
                                                                            Unit Price
                                                                        </div>
                                                                        <div class="col-sm-8 paddingRight5">
                                                                            <asp:TextBox ID="txtUnitPrice" runat="server" TabIndex="31" AutoPostBack="true" OnTextChanged="txtUnitPrice_TextChanged" onkeydown="return jsDecimals(event);" CssClass="form-control"></asp:TextBox>
                                                                        </div>
                                                                    </div>

                                                                </div>

                                                                <div class="col-sm-2">

                                                                    <div class="row">

                                                                        <div class="col-sm-4 labelText1">
                                                                            Unit Amt
                                                                        </div>
                                                                        <div class="col-sm-8 paddingRight5">
                                                                            <asp:TextBox ID="txtUnitAmt" runat="server" TabIndex="32" ReadOnly="true" onkeydown="return jsDecimals(event);" CssClass="form-control"></asp:TextBox>
                                                                        </div>
                                                                    </div>

                                                                </div>

                                                                <div class="col-sm-2">

                                                                    <div class="row">

                                                                        <div class="col-sm-4 labelText1">
                                                                            Dis.Rate %
                                                                        </div>
                                                                        <div class="col-sm-8 paddingRight5">
                                                                            <asp:TextBox ID="txtDisRate" runat="server" TabIndex="33" onkeydown="return jsDecimals(event);" CssClass="form-control" AutoPostBack="true" OnTextChanged="txtDisRate_TextChanged"></asp:TextBox>
                                                                        </div>
                                                                    </div>

                                                                </div>

                                                                <div class="col-sm-2">

                                                                    <div class="row">

                                                                        <div class="col-sm-4 labelText1">
                                                                            Dis.Amt
                                                                        </div>
                                                                        <div class="col-sm-8 paddingRight5">
                                                                            <asp:TextBox ID="txtDisAmt" runat="server" TabIndex="34" onkeydown="return jsDecimals(event);" CssClass="form-control" AutoPostBack="true" OnTextChanged="txtDisAmt_TextChanged"></asp:TextBox>
                                                                        </div>
                                                                    </div>

                                                                </div>

                                                                <div class="col-sm-2">

                                                                    <div class="row">

                                                                        <div class="col-sm-4 labelText1">
                                                                            Tax
                                                                        </div>
                                                                        <div class="col-sm-8 paddingRight5">
                                                                            <asp:TextBox ID="txtTaxAmt" runat="server" TabIndex="35" onkeydown="return jsDecimals(event);" CssClass="form-control"></asp:TextBox>
                                                                        </div>
                                                                    </div>

                                                                </div>

                                                                <%--<div class="col-sm-2">
                                                                    <div class="row">
                                                                        <div class="col-sm-4 labelText1">
                                                                            Res No
                                                                        </div>

                                                                        <div class="col-sm-7 paddingRight5">
                                                                            <asp:TextBox ID="txtresno" runat="server" AutoPostBack="true" CssClass="form-control"></asp:TextBox>
                                                                        </div>

                                                                    </div>

                                                                    <div class="col-sm-1 labelText1" style="margin-left: 170px; margin-top:-25px;">
                                                                        <asp:LinkButton ID="btnSearchReservation" runat="server" OnClick="btnSearchReservation_Click">
                                                                                    <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                                        </asp:LinkButton>
                                                                    </div>

                                                                </div>--%>

                                                                <div class="col-sm-2">

                                                                    <div class="row">

                                                                        <div class="col-sm-4 labelText1">
                                                                            Line Amt
                                                                        </div>
                                                                        <div class="col-sm-7 paddingRight5">
                                                                            <asp:TextBox ID="txtLineTotAmt" runat="server" TabIndex="36" onkeydown="return jsDecimals(event);" CssClass="form-control"></asp:TextBox>
                                                                        </div>

                                                                        <div class="col-sm-1 labelText1" style="margin-left: -10px">
                                                                            <asp:LinkButton ID="lbtnadditems" runat="server" TabIndex="38" CausesValidation="false" OnClick="lbtnadditems_Click">
                                                        <span class="glyphicon glyphicon-plus" aria-hidden="true"></span>
                                                                            </asp:LinkButton>
                                                                        </div>

                                                                    </div>

                                                                </div>

                                                                <div class="row">
                                                                    <div class="col-sm-12 height5">
                                                                        <asp:Label Text=" " ID="lblSelectRevervation" runat="server" Visible="false" />
                                                                        <asp:Label Text=" " ID="lblSelectRevLine" runat="server" Visible="false" />
                                                                    </div>
                                                                </div>

                                                                <div id="hiddndv" runat="server">
                                                                    <div class="col-sm-2">

                                                                        <div class="row">

                                                                            <div class="col-sm-4 labelText1">
                                                                                Res.No
                                                                            </div>
                                                                            <div class="col-sm-7 paddingRight5">
                                                                                <asp:TextBox ID="txtresno" runat="server" AutoPostBack="true" CssClass="form-control" OnTextChanged="txtresno_TextChanged"></asp:TextBox>
                                                                            </div>

                                                                            <div class="col-sm-1 paddingLeft0">
                                                                                <asp:LinkButton ID="LinkButton27" runat="server" TabIndex="37" OnClick="LinkButton27_Click">
                                                        <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                                                </asp:LinkButton>
                                                                            </div>

                                                                        </div>

                                                                    </div>

                                                                    <div class="col-sm-2">

                                                                        <div class="row">
                                                                        </div>

                                                                    </div>

                                                                    <div class="row">
                                                                        <div class="col-sm-12 height5">
                                                                        </div>
                                                                    </div>
                                                                </div>

                                                                <div class="row">
                                                                    <div class="panel-body">
                                                                        <div class="col-sm-12">
                                                                            <div class="panel panel-default">

                                                                                <div class="panel-heading panelHeadingInfoBar">

                                                                                    <div class="col-sm-3">

                                                                                        <div class="row">

                                                                                            <div class="col-sm-3 labelText1">
                                                                                                Description:
                                                                                            </div>

                                                                                            <div class="col-sm-9" style="margin-top: 3px">
                                                                                                <asp:Label ID="lblItemDescription" runat="server" ForeColor="#A513D0"></asp:Label>
                                                                                            </div>

                                                                                        </div>

                                                                                    </div>

                                                                                    <div class="col-sm-3">

                                                                                        <div class="row">

                                                                                            <div class="col-sm-2 labelText1">
                                                                                                Model:
                                                                                            </div>
                                                                                            <div class="col-sm-10" style="margin-top: 3px">
                                                                                                <asp:Label ID="lblItemModel" runat="server" ForeColor="#A513D0"></asp:Label>
                                                                                            </div>

                                                                                        </div>

                                                                                    </div>

                                                                                    <div class="col-sm-3">

                                                                                        <div class="row">

                                                                                            <div class="col-sm-2 labelText1">
                                                                                                Brand:
                                                                                            </div>
                                                                                            <div class="col-sm-10" style="margin-top: 3px">
                                                                                                <asp:Label ID="lblItemBrand" runat="server" ForeColor="#A513D0"></asp:Label>
                                                                                            </div>

                                                                                        </div>

                                                                                    </div>

                                                                                    <div class="col-sm-3">

                                                                                        <div class="row">

                                                                                            <div class="col-sm-4 labelText1">
                                                                                                Serial Status:
                                                                                            </div>
                                                                                            <div class="col-sm-8" style="margin-top: 3px">
                                                                                                <asp:Label ID="lblItemSerialStatus" runat="server" ForeColor="#A513D0"></asp:Label>
                                                                                            </div>

                                                                                        </div>

                                                                                    </div>

                                                                                </div>

                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>

                                                                <div class="row">
                                                                    <div class="panel-body">
                                                                        <div class="col-sm-12">

                                                                            <div class="row">

                                                                                <div class="col-sm-12">

                                                                                    <ul id="myTab" class="nav nav-tabs">

                                                                                        <li class="active">
                                                                                            <a href="#Item" data-toggle="tab">Item</a>
                                                                                        </li>

                                                                                        <li>
                                                                                            <a href="#Serial" data-toggle="tab">Serial</a>
                                                                                        </li>

                                                                                        <li>
                                                                                            <a href="#GiftVoucher" data-toggle="tab" style="visibility: hidden">Gift Voucher</a>
                                                                                        </li>

                                                                                        <li>
                                                                                            <a href="#BuyBackDetail" data-toggle="tab" style="visibility: hidden">Buy Back Detail</a>
                                                                                        </li>

                                                                                        <span style="display: inline-block; width: 40px;"></span>

                                                                                        <asp:LinkButton ID="LinkButton10" runat="server" CausesValidation="false" Visible="false" Text="<b>Buy Back</b>" OnClick="LinkButton10_Click">
                                                                            
                                                                                        </asp:LinkButton>

                                                                                        <span style="display: inline-block; width: 40px;"></span>

                                                                                        <asp:Label runat="server" ID="lblLvlMsg"></asp:Label>

                                                                                    </ul>



                                                                                </div>

                                                                            </div>

                                                                            <div class="row">

                                                                                <div class="col-sm-12">

                                                                                    <div id="myTabContent" class="tab-content">



                                                                                        <div class="tab-pane fade in active" id="Item">
                                                                                            <asp:ScriptManagerProxy ID="ScriptManagerProxy7" runat="server"></asp:ScriptManagerProxy>
                                                                                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                                                                <ContentTemplate>
                                                                                                    <div class="row ">
                                                                                                        <div class="col-sm-12 ">

                                                                                                            <div class="row">
                                                                                                                <div class="col-sm-12 height5">
                                                                                                                </div>
                                                                                                            </div>

                                                                                                            <div class="panelscoll">

                                                                                                                <asp:GridView ID="gvInvoiceItem" AutoGenerateColumns="false" TabIndex="39" runat="server" CssClass="table table-hover table-striped" OnRowDataBound="gvInvoiceItem_RowDataBound" OnRowDeleting="gvInvoiceItem_RowDeleting" OnSelectedIndexChanged="gvInvoiceItem_SelectedIndexChanged" GridLines="None" ShowHeaderWhenEmpty="True" EmptyDataText="No data found...">



                                                                                                                    <Columns>

                                                                                                                        <asp:BoundField DataField="soi_itm_line" HeaderText="No" ItemStyle-Width="150" ReadOnly="true" />
                                                                                                                        <asp:BoundField DataField="soi_itm_cd" HeaderText="Item" ItemStyle-Width="150" ReadOnly="true" />

                                                                                                                        <asp:BoundField DataField="description" HeaderText="Description" ItemStyle-Width="150" ReadOnly="true" />
                                                                                                                        <asp:BoundField DataField="soi_itm_stus" HeaderText="Status" ItemStyle-Width="150" ReadOnly="true" />
                                                                                                                        <asp:BoundField DataField="soi_qty" DataFormatString="{0:n}" HeaderText="Qty" ItemStyle-Width="150" ReadOnly="true" />
                                                                                                                     <%--   <asp:BoundField DataField="soi_unit_rt" HeaderText="Unit Price" DataFormatString="{0:n}" ItemStyle-Width="150" ReadOnly="true" />--%>


                                                                                                                         <asp:TemplateField HeaderText="Unit Price">
                                                                                                                    <ItemTemplate>
                                                                                                                        <asp:Label ID="InvItm_UPrice" Visible="false" runat="server" Text='<%# Bind("soi_unit_rt","{0:N2}") %>'></asp:Label>
                                                                                                                        <asp:LinkButton ID="lbtnItemPrice" Text='<%# Bind("soi_unit_rt","{0:N2}") %>' runat="server" OnClick="lbtnItemPrice_Click" />
                                                                                                                    </ItemTemplate>
                                                                                                                    <ItemStyle Width="150px" />
                                                                                                                    <ItemStyle HorizontalAlign="Right" />
                                                                                                                    <HeaderStyle CssClass="gridHeaderAlignRight" />
                                                                                                                </asp:TemplateField>

                                                                                                                        <asp:BoundField DataField="soi_unit_amt" HeaderText="Unit Amt" DataFormatString="{0:n}" ItemStyle-Width="150" ReadOnly="true" />
                                                                                                                       <%-- <asp:BoundField DataField="soi_disc_rt" HeaderText="Dis.Rate" DataFormatString="{0:n}" ItemStyle-Width="150" ReadOnly="true" ItemStyle-HorizontalAlign="Center" />--%>
                                                                                                                        <asp:TemplateField HeaderText="Dis.Rate">
                                                                                                                            <ItemTemplate>
                                                                                                                                <asp:Label ID="lblsad_disc_rt" runat="server" Text='<%# Bind("soi_disc_rt","{0:N2}") %>' Visible="false"></asp:Label>
                                                                                                                                <asp:LinkButton ID="btnInvItemDisRate" Text='<%# Bind("soi_disc_rt","{0:N2}") %>' runat="server" OnClick="btnInvItemDisRate_Click" />
                                                                                                                            </ItemTemplate>
                                                                                                                            <ItemStyle Width="150px" />
                                                                                                                            <ItemStyle HorizontalAlign="Right" />
                                                                                                                            <HeaderStyle CssClass="gridHeaderAlignRight" />
                                                                                                                        </asp:TemplateField>
                                                                                                                        <asp:BoundField DataField="soi_disc_amt" HeaderText="Dis.Amt" DataFormatString="{0:n}" ItemStyle-Width="150" ReadOnly="true" ItemStyle-HorizontalAlign="Center" />
                                                                                                                        <asp:BoundField DataField="soi_itm_tax_amt" HeaderText="Tax Amt" DataFormatString="{0:n}" ItemStyle-Width="150" ReadOnly="true" ItemStyle-HorizontalAlign="Center" />
                                                                                                                        <asp:BoundField DataField="soi_tot_amt" HeaderText="Line Amt" DataFormatString="{0:n}" ItemStyle-Width="150" ReadOnly="true" />
                                                                                                                        <asp:BoundField DataField="soi_pbook" HeaderText="Book" ItemStyle-Width="150" ReadOnly="true" />
                                                                                                                        <asp:BoundField DataField="soi_pb_lvl" HeaderText="Level" ItemStyle-Width="150" ReadOnly="true" ItemStyle-HorizontalAlign="Left" />
                                                                                                                        <asp:BoundField DataField="soi_res_no" HeaderText="Reservation No" ItemStyle-Width="150" ReadOnly="true" />
                                                                                                                        <%--<asp:BoundField DataField="itri_seq_no" HeaderText="Seq No" ItemStyle-Width="1" Visible="false" ReadOnly="true" />--%>
                                                                                                                        <asp:TemplateField HeaderText="Seq No" Visible="TRUE">
                                                                                                                            <ItemTemplate>
                                                                                                                                <asp:Label ID="itri_seq_no" runat="server" Text='<%# Bind("itri_seq_no") %>' Width="100px"></asp:Label>
                                                                                                                            </ItemTemplate>
                                                                                                                        </asp:TemplateField>
                                                                                                                        <asp:TemplateField>
                                                                                                                            <ItemTemplate>

                                                                                                                                <div id="delbtndiv">
                                                                                                                                    <asp:LinkButton ID="lbtndelitem_Click" CausesValidation="false" CommandName="Delete" runat="server">
                                                                                                                                    <span class="glyphicon glyphicon-trash" aria-hidden="true"></span>
                                                                                                                                    </asp:LinkButton>
                                                                                                                                </div>

                                                                                                                            </ItemTemplate>
                                                                                                                        </asp:TemplateField>
                                                                                                                    </Columns>

                                                                                                                </asp:GridView>

                                                                                                            </div>

                                                                                                        </div>
                                                                                                    </div>
                                                                                                </ContentTemplate>
                                                                                            </asp:UpdatePanel>
                                                                                        </div>

                                                                                        <div class="tab-pane fade" id="Serial">
                                                                                            <asp:ScriptManagerProxy ID="ScriptManagerProxy1" runat="server"></asp:ScriptManagerProxy>
                                                                                            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                                                                                <ContentTemplate>
                                                                                                    <div class="row">

                                                                                                        <div class="col-sm-12 ">
                                                                                                            <div class="row">
                                                                                                                <div class="col-sm-12 height5">
                                                                                                                </div>
                                                                                                            </div>

                                                                                                            <div class="panelscoll">

                                                                                                                <asp:GridView ID="gvPopSerial" AutoGenerateColumns="false" TabIndex="40" runat="server" CssClass="table table-hover table-striped" OnRowDataBound="gvPopSerial_RowDataBound" OnRowDeleting="gvPopSerial_RowDeleting" GridLines="None" ShowHeaderWhenEmpty="True" EmptyDataText="No data found...">



                                                                                                                    <Columns>
                                                                                                                        <asp:BoundField DataField="sose_itm_line" HeaderText="No" ItemStyle-Width="100px" ReadOnly="true" />
                                                                                                                        <asp:BoundField DataField="sose_itm_cd" HeaderText="Item" ItemStyle-Width="130px" ReadOnly="true" />
                                                                                                                        <asp:BoundField DataField="Model" HeaderText="Model" ItemStyle-Width="130px" ReadOnly="true" />
                                                                                                                        <asp:BoundField DataField="Status" HeaderText="Status" ItemStyle-Width="100px" ReadOnly="true" />
                                                                                                                        <asp:BoundField DataField="StatusDesc" HeaderText="Status Description" ItemStyle-Width="150px" ReadOnly="true" />
                                                                                                                        <asp:BoundField DataField="Qty" HeaderText="Qty" DataFormatString="{0:n}" ItemStyle-Width="125px" ReadOnly="true" />
                                                                                                                        <asp:BoundField DataField="sose_ser_1" HeaderText="Serial 1" ItemStyle-Width="135px" ReadOnly="true" />
                                                                                                                        <asp:BoundField DataField="sose_ser_2" HeaderText="Serial 2" ItemStyle-Width="135px" ReadOnly="true" />
                                                                                                                        <asp:BoundField DataField="Warranty" HeaderText="Warranty" ItemStyle-Width="160px" ReadOnly="true" />

                                                                                                                        <asp:TemplateField>
                                                                                                                            <ItemTemplate>

                                                                                                                                <div id="delbtndiv">
                                                                                                                                    <asp:LinkButton ID="lbtndelserial" CausesValidation="false" CommandName="Delete" runat="server">
                                                                                                                                    <span class="glyphicon glyphicon-trash" aria-hidden="true"></span>
                                                                                                                                    </asp:LinkButton>
                                                                                                                                </div>

                                                                                                                            </ItemTemplate>
                                                                                                                        </asp:TemplateField>

                                                                                                                    </Columns>

                                                                                                                </asp:GridView>

                                                                                                            </div>
                                                                                                        </div>
                                                                                                    </div>
                                                                                                </ContentTemplate>
                                                                                            </asp:UpdatePanel>
                                                                                        </div>

                                                                                        <div class="tab-pane fade" id="GiftVoucher">
                                                                                            <asp:ScriptManagerProxy ID="ScriptManagerProxy2" runat="server"></asp:ScriptManagerProxy>
                                                                                            <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                                                                                <ContentTemplate>

                                                                                                    <div class="row">

                                                                                                        <div class="col-sm-12 ">
                                                                                                            <div class="row">
                                                                                                                <div class="col-sm-12 height5">
                                                                                                                </div>
                                                                                                            </div>

                                                                                                            <div class="panelscoll">

                                                                                                                <asp:GridView ID="gvGiftVoucher" AutoGenerateColumns="false" runat="server" CssClass="table table-hover table-striped" GridLines="None" ShowHeaderWhenEmpty="True" EmptyDataText="No data found...">



                                                                                                                    <Columns>
                                                                                                                        <asp:BoundField DataField="Item" HeaderText="Item" ItemStyle-Width="150" ReadOnly="true" />
                                                                                                                        <asp:BoundField DataField="Model" HeaderText="Model" ItemStyle-Width="150" ReadOnly="true" />
                                                                                                                        <asp:BoundField DataField="Status" HeaderText="Status" ItemStyle-Width="150" ReadOnly="true" />
                                                                                                                        <asp:BoundField DataField="Qty" HeaderText="Qty" DataFormatString="{0:n}" ItemStyle-Width="150" ReadOnly="true" />
                                                                                                                        <asp:BoundField DataField="Page" HeaderText="Page" ItemStyle-Width="150" ReadOnly="true" />
                                                                                                                        <asp:BoundField DataField="Book" HeaderText="Book" ItemStyle-Width="150" ReadOnly="true" />

                                                                                                                        <asp:TemplateField>
                                                                                                                            <ItemTemplate>

                                                                                                                                <div id="editbtndiv">
                                                                                                                                    <asp:LinkButton ID="lbtnedititem" CausesValidation="false" CommandName="Edit" runat="server">
                                                        <span class="glyphicon glyphicon-pencil" aria-hidden="true" style="font-size:15px"></span>Edit
                                                                                                                                    </asp:LinkButton>
                                                                                                                                </div>

                                                                                                                            </ItemTemplate>
                                                                                                                            <EditItemTemplate>

                                                                                                                                <asp:LinkButton ID="lbtnUpdateitem" CausesValidation="false" runat="server">
                                                        <span class="glyphicon glyphicon-edit" aria-hidden="true" style="font-size:15px"></span>Update
                                                                                                                                </asp:LinkButton>


                                                                                                                                <asp:LinkButton ID="lbtncanceledit" CausesValidation="false" runat="server">
                                                        <span class="glyphicon glyphicon-remove-circle" aria-hidden="true" style="font-size:15px"></span>Cancel
                                                                                                                                </asp:LinkButton>

                                                                                                                            </EditItemTemplate>
                                                                                                                        </asp:TemplateField>

                                                                                                                        <asp:TemplateField>
                                                                                                                            <ItemTemplate>

                                                                                                                                <div id="delbtndiv">
                                                                                                                                    <asp:LinkButton ID="lbtndelitem" CausesValidation="false" CommandName="Delete" runat="server">
                                                        <span class="glyphicon glyphicon-trash" aria-hidden="true" style="font-size:15px"></span>Delete
                                                                                                                                    </asp:LinkButton>
                                                                                                                                </div>

                                                                                                                            </ItemTemplate>
                                                                                                                        </asp:TemplateField>

                                                                                                                    </Columns>

                                                                                                                </asp:GridView>

                                                                                                            </div>
                                                                                                        </div>
                                                                                                    </div>


                                                                                                </ContentTemplate>
                                                                                            </asp:UpdatePanel>
                                                                                        </div>

                                                                                        <div class="tab-pane fade" id="BuyBackDetail">
                                                                                            <asp:ScriptManagerProxy ID="ScriptManagerProxy3" runat="server"></asp:ScriptManagerProxy>
                                                                                            <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                                                                                <ContentTemplate>

                                                                                                    <div class="row">

                                                                                                        <div class="col-sm-12 ">

                                                                                                            <div class="row">
                                                                                                                <div class="col-sm-12 height5">
                                                                                                                </div>
                                                                                                            </div>

                                                                                                            <div class="panelscoll">

                                                                                                                <asp:GridView ID="gvBuyBack" AutoGenerateColumns="false" runat="server" CssClass="table table-hover table-striped" GridLines="None" ShowHeaderWhenEmpty="True" EmptyDataText="No data found...">



                                                                                                                    <Columns>
                                                                                                                        <asp:BoundField DataField="Item" HeaderText="Item" ItemStyle-Width="150" ReadOnly="true" />
                                                                                                                        <asp:BoundField DataField="Description" HeaderText="Description" ItemStyle-Width="150" ReadOnly="true" />
                                                                                                                        <asp:BoundField DataField="Model" HeaderText="Model" ItemStyle-Width="150" ReadOnly="true" />
                                                                                                                        <asp:BoundField DataField="Status" HeaderText="Status" ItemStyle-Width="150" ReadOnly="true" />
                                                                                                                        <asp:BoundField DataField="Qty" HeaderText="Qty" DataFormatString="{0:n}" ItemStyle-Width="150" ReadOnly="true" />
                                                                                                                        <asp:BoundField DataField="Serial1" HeaderText="Serial 1" ItemStyle-Width="150" ReadOnly="true" />
                                                                                                                        <asp:BoundField DataField="Serial1" HeaderText="Serial 2" ItemStyle-Width="150" ReadOnly="true" />

                                                                                                                        <asp:TemplateField>
                                                                                                                            <ItemTemplate>

                                                                                                                                <div id="editbtndiv">
                                                                                                                                    <asp:LinkButton ID="lbtnedititem" CausesValidation="false" CommandName="Edit" runat="server">
                                                        <span class="glyphicon glyphicon-pencil" aria-hidden="true" style="font-size:15px"></span>Edit
                                                                                                                                    </asp:LinkButton>
                                                                                                                                </div>

                                                                                                                            </ItemTemplate>
                                                                                                                            <EditItemTemplate>

                                                                                                                                <asp:LinkButton ID="lbtnUpdateitem" CausesValidation="false" runat="server">
                                                        <span class="glyphicon glyphicon-edit" aria-hidden="true" style="font-size:15px"></span>Update
                                                                                                                                </asp:LinkButton>


                                                                                                                                <asp:LinkButton ID="lbtncanceledit" CausesValidation="false" runat="server">
                                                        <span class="glyphicon glyphicon-remove-circle" aria-hidden="true" style="font-size:15px"></span>Cancel
                                                                                                                                </asp:LinkButton>

                                                                                                                            </EditItemTemplate>
                                                                                                                        </asp:TemplateField>

                                                                                                                        <asp:TemplateField>
                                                                                                                            <ItemTemplate>

                                                                                                                                <div id="delbtndiv">
                                                                                                                                    <asp:LinkButton ID="lbtndelitem" CausesValidation="false" CommandName="Delete" runat="server">
                                                        <span class="glyphicon glyphicon-trash" aria-hidden="true" style="font-size:15px"></span>Delete
                                                                                                                                    </asp:LinkButton>
                                                                                                                                </div>

                                                                                                                            </ItemTemplate>
                                                                                                                        </asp:TemplateField>

                                                                                                                    </Columns>

                                                                                                                </asp:GridView>

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

                                                                <div class="row">
                                                                    <div class="panel-body">
                                                                        <div class="col-sm-12">
                                                                            <div class="panel panel-default">

                                                                                <div class="panel-heading panelHeadingInfoBar">

                                                                                    <div class="col-sm-2">

                                                                                        <div class="row">

                                                                                            <div class="col-sm-5 labelText1">
                                                                                                Sub Total:
                                                                                            </div>

                                                                                            <div class="col-sm-7" style="margin-top: 3px">
                                                                                                <asp:Label ID="lblGrndSubTotal" runat="server" ForeColor="#A513D0"></asp:Label>
                                                                                            </div>

                                                                                        </div>

                                                                                    </div>

                                                                                    <div class="col-sm-2">

                                                                                        <div class="row">

                                                                                            <div class="col-sm-2 labelText1">
                                                                                                Discount:
                                                                                            </div>
                                                                                            <div class="col-sm-10" style="margin-top: -18px; margin-left: 75px;">
                                                                                                <asp:Label ID="lblGrndDiscount" runat="server" ForeColor="#A513D0"></asp:Label>
                                                                                            </div>

                                                                                        </div>

                                                                                    </div>

                                                                                    <div class="col-sm-2">

                                                                                        <div class="row">

                                                                                            <div class="col-sm-6 labelText1">
                                                                                                After Discount:
                                                                                            </div>
                                                                                            <div class="col-sm-6" style="margin-top: 3px">
                                                                                                <asp:Label ID="lblGrndAfterDiscount" runat="server" ForeColor="#A513D0"></asp:Label>
                                                                                            </div>

                                                                                        </div>

                                                                                    </div>

                                                                                    <div class="col-sm-2">

                                                                                        <div class="row">

                                                                                            <div class="col-sm-4 labelText1">
                                                                                                Tax:
                                                                                            </div>
                                                                                            <div class="col-sm-8" style="margin-top: 3px">
                                                                                                <asp:Label ID="lblGrndTax" runat="server" ForeColor="#A513D0"></asp:Label>
                                                                                            </div>

                                                                                        </div>

                                                                                    </div>

                                                                                    <div class="col-sm-2">

                                                                                        <div class="row">

                                                                                            <div class="col-sm-6 labelText1">
                                                                                                Total Amount:
                                                                                            </div>
                                                                                            <div class="col-sm-6" style="margin-top: 3px">
                                                                                                <asp:Label ID="lblGrndTotalAmount" runat="server" ForeColor="#A513D0"></asp:Label>
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

                                <div class="row">

                                    <div class="col-sm-12">

                                        <div class="panel panel-default " id="y55">

                                            <div class="panel-heading pannelheading">

                                                <div class="row">
                                                    <div class="col-sm-4">
                                                        Inventory Balance Break-up
                                                    </div>
                                                </div>

                                            </div>

                                            <div class="panel-body" id="dd6">

                                                <div class="row">
                                                    <div class="col-sm-12">

                                                        <div class="panelscoll">

                                                            <asp:GridView ID="grvwarehouseitms" AutoGenerateColumns="false" runat="server" CssClass="table table-hover table-striped" GridLines="None" ShowHeaderWhenEmpty="True" EmptyDataText="No data found...">



                                                                <Columns>

                                                                    <asp:BoundField DataField="mis_desc" HeaderText="Status" ItemStyle-Width="150" ReadOnly="true" />
                                                                    <asp:BoundField DataField="inl_qty" HeaderText="In Hand" DataFormatString="{0:n}" ItemStyle-Width="150" ReadOnly="true" />
                                                                    <asp:BoundField DataField="inl_res_qty" HeaderText="Reserve" DataFormatString="{0:n}" ItemStyle-Width="150" ReadOnly="true" />
                                                                    <asp:BoundField DataField="inl_free_qty" HeaderText="Free" DataFormatString="{0:n}" ItemStyle-Width="150" ReadOnly="true" />

                                                                </Columns>

                                                            </asp:GridView>

                                                        </div>


                                                    </div>

                                                </div>

                                            </div>

                                        </div>


                                    </div>


                                </div>

                                <%--<div id="hiddenpaumodeuc" style="visibility:hidden">
                                    <div class="row">
                                        <div class="col-sm-12">
                                            <div class="panel-body">
                                                <div class="row">
                                                    <div class="col-sm-12">
                                                        <uc1:ucPaymodes runat="server" ID="ucPayModes1" />
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>--%>
                            </div>

                        </div>

                    </div>

                </div>

            </div>

        </ContentTemplate>

    </asp:UpdatePanel>


    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <asp:Button ID="Button3" runat="server" Text="Button" Style="display: none;" />
            <asp:ModalPopupExtender ID="SIPopup" runat="server" Enabled="True" TargetControlID="Button3"
                PopupControlID="pnlpopup" CancelControlID="btnClose" PopupDragHandleControlID="PopupHeader" Drag="true" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>

        </ContentTemplate>
    </asp:UpdatePanel>

    <asp:Panel runat="server" ID="pnlpopup" DefaultButton="lbtnSearch">
        <div runat="server" id="test" class="panel panel-primary height400 width950">

            <asp:Label ID="lblvalue" runat="server" Text="Label" Visible="false"></asp:Label>
            <div class="panel panel-default">
                <div class="panel-heading">
                    <asp:LinkButton ID="btnClose" runat="server">
                             <span class="glyphicon glyphicon-remove" aria-hidden="true">Close</span>
                    </asp:LinkButton>
                    <div class="col-sm-11">
                    </div>
                    <div class="col-sm-1">
                    </div>
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

                                    <div id="resultgrd" class="panelscoll POPupResultspanelscroll">
                                        <asp:GridView ID="grdResult" CausesValidation="false" AllowPaging="true" runat="server" GridLines="None" CssClass="table table-hover table-striped" PageSize="7" PagerStyle-CssClass="cssPager" OnPageIndexChanging="grdResult_PageIndexChanging" OnSelectedIndexChanged="grdResult_SelectedIndexChanged">
                                            <Columns>
                                                <asp:ButtonField Text="Select" CommandName="Select" ItemStyle-Width="10" />
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </asp:Panel>



    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <asp:Button ID="btn2" runat="server" Text="Button" Style="display: none;" />
            <asp:ModalPopupExtender ID="MpDelivery" runat="server" Enabled="True" TargetControlID="btn2"
                PopupControlID="pnldel" CancelControlID="btnClose" PopupDragHandleControlID="PopupHeaderKit" Drag="true" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>

        </ContentTemplate>
    </asp:UpdatePanel>

    <asp:Panel runat="server" ID="pnldel" DefaultButton="lbtnSearch">
        <div runat="server" id="Div1" class="panel panel-primary Mheight">

            <asp:Label ID="Label7" runat="server" Text="Label" Visible="false"></asp:Label>

            <div class="panel panel-default">
                <div class="panel-heading">
                    <asp:LinkButton ID="LinkButton13" runat="server">
                             <span class="glyphicon glyphicon-remove" aria-hidden="true">Close</span>
                    </asp:LinkButton>
                    <div class="col-sm-11">
                    </div>
                    <div class="col-sm-1">
                    </div>
                </div>
                <div class="panel-body">

                    <%--<div class="row">
                        <div class="col-sm-12">

                            <div class="col-sm-6">
                                <div class="row">

                                    <div class="col-sm-6 labelText1">
                                                Delivery From Any Location
                                            </div>
                                            <div class="col-sm-6 paddingRight5">
                                                <asp:CheckBox runat="server" />
                                            </div>
                                </div>
                            </div>

                        </div>
                    </div>--%>

                    <%--                    <div class="row">
                        <div class="col-sm-12 height5">
                        </div>
                    </div>--%>


                    <asp:UpdatePanel runat="server">
                        <ContentTemplate>


                            <div class="row">
                                <div class="col-sm-12">

                                    <div class="col-sm-6">
                                        <div class="row">

                                            <div class="col-sm-6 labelText1">
                                                Delivery Code
                                            </div>
                                            <div class="col-sm-5 paddingRight5">
                                                <asp:TextBox ID="txtdellocation" runat="server" CssClass="form-control"></asp:TextBox>
                                            </div>

                                            <div class="col-sm-1 paddingLeft0">
                                                <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="false" OnClick="LinkButton1_Click">
                                                        <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                </asp:LinkButton>
                                            </div>

                                        </div>
                                    </div>

                                    <div class="col-sm-6">
                                        <div class="row">

                                            <div class="col-sm-6 labelText1">
                                                Customer Code
                                            </div>
                                            <div class="col-sm-5 paddingRight5">
                                                <asp:TextBox ID="txtdelcuscode" runat="server" CssClass="form-control"></asp:TextBox>
                                            </div>
                                            <div class="col-sm-1 paddingLeft0">
                                                <asp:LinkButton ID="LinkButton2" runat="server" CausesValidation="false" OnClick="LinkButton2_Click">
                                                        <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                </asp:LinkButton>
                                            </div>
                                        </div>
                                    </div>



                                </div>
                            </div>

                            <div class="row">
                                <div class="col-sm-12 height5">
                                </div>
                            </div>

                            <div class="row">
                                <div class="col-sm-12">

                                    <div class="col-sm-12">
                                        <div class="row">

                                            <div class="col-sm-3 labelText1">
                                                Name
                                            </div>
                                            <div class="col-sm-9 paddingRight5">
                                                <asp:TextBox ID="txtdelname" runat="server" CssClass="form-control"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>

                                </div>
                            </div>

                            <div class="row">
                                <div class="col-sm-12 height5">
                                </div>
                            </div>

                            <div class="row">
                                <div class="col-sm-12">

                                    <div class="col-sm-12">
                                        <div class="row">

                                            <div class="col-sm-3 labelText1">
                                                Address
                                            </div>
                                            <div class="col-sm-9 paddingRight5">
                                                <asp:TextBox ID="txtdelad1" runat="server" CssClass="form-control"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div class="row">
                                <div class="col-sm-12 height5">
                                </div>
                            </div>

                            <div class="row">
                                <div class="col-sm-12">

                                    <div class="col-sm-12">
                                        <div class="row">

                                            <div class="col-sm-3 labelText1">
                                            </div>
                                            <div class="col-sm-9 paddingRight5">
                                                <asp:TextBox ID="txtdelad2" runat="server" CssClass="form-control"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div class="row">
                                <div class="col-sm-12 height5">
                                </div>
                            </div>

                            <div class="row">
                                <div class="col-sm-12">

                                    <div class="col-sm-12">
                                        <div class="row">

                                            <div class="col-sm-3 labelText1">
                                                Town
                                            </div>

                                            <asp:ScriptManagerProxy ID="ScriptManagerProxy11" runat="server"></asp:ScriptManagerProxy>
                                            <asp:UpdatePanel runat="server">
                                                <ContentTemplate>
                                                    <div class="col-sm-4 paddingRight5">
                                                        <asp:DropDownList ID="ddltown" runat="server" AutoPostBack="true" CssClass="form-control" OnSelectedIndexChanged="ddltown_SelectedIndexChanged"></asp:DropDownList>
                                                    </div>
                                                </ContentTemplate>

                                            </asp:UpdatePanel>

                                        </div>
                                    </div>
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

                            <div id="dvhiddendel" runat="server">
                                <div class="row">
                                    <div class="col-sm-12">

                                        <div class="col-sm-12">
                                            <div class="row">

                                                <div class="col-sm-4 labelText1">
                                                    <asp:LinkButton ID="lbtndconfirm" CausesValidation="false" runat="server" OnClick="lbtndconfirm_Click">
                                                        <span class="glyphicon glyphicon-ok" aria-hidden="true" ></span>Confirm
                                                    </asp:LinkButton>
                                                </div>

                                                <div class="col-sm-4 labelText1">
                                                    <asp:LinkButton ID="lbtndreset" CausesValidation="false" runat="server" OnClick="lbtndreset_Click">
                                                        <span class="glyphicon glyphicon-repeat" aria-hidden="true" ></span>Reset
                                                    </asp:LinkButton>
                                                </div>

                                                <div class="col-sm-4 labelText1">
                                                    <asp:LinkButton ID="lbtndclear" CausesValidation="false" runat="server" OnClick="lbtndclear_Click">
                                                        <span class="glyphicon glyphicon-refresh" aria-hidden="true" ></span>Clear
                                                    </asp:LinkButton>
                                                </div>

                                            </div>
                                        </div>
                                    </div>
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

                        </ContentTemplate>
                    </asp:UpdatePanel>



                </div>
            </div>

        </div>
    </asp:Panel>



    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <asp:Button ID="btnpv" runat="server" Text="Button" Style="display: none;" />
            <asp:ModalPopupExtender ID="MPPV" runat="server" Enabled="True" TargetControlID="btnpv"
                PopupControlID="pnlpv" CancelControlID="btnClose" PopupDragHandleControlID="PopupHeaderKits" Drag="true" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>

        </ContentTemplate>
    </asp:UpdatePanel>

    <asp:Panel runat="server" ID="pnlpv" DefaultButton="lbtnSearch">
        <div runat="server" id="Div2ss" class="panel panel-primary Mheight">

            <asp:Label ID="Label8" runat="server" Text="Label" Visible="false"></asp:Label>
            <div class="panel panel-default">
                <div class="panel-heading">
                    <asp:LinkButton ID="LinkButton18" runat="server">
                             <span class="glyphicon glyphicon-remove" aria-hidden="true">Close</span>
                    </asp:LinkButton>
                    <div class="col-sm-11">
                    </div>
                    <div class="col-sm-1">
                    </div>
                </div>
                <div class="panel-body">
                    <div class="row">
                        <div class="col-sm-12">

                            <div class="col-sm-12">
                                <div class="row">

                                    <div class="col-sm-4 labelText1">
                                        Promotion Voucher
                                    </div>

                                    <div class="col-sm-7 labelText1">
                                        <asp:TextBox runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>

                                    <div class="col-sm-1 paddingRight5">
                                        <asp:LinkButton ID="LinkButton19" CausesValidation="false" CssClass="floatRight" runat="server">
                                                                        <span class="glyphicon glyphicon-plus" aria-hidden="true"></span>
                                        </asp:LinkButton>
                                    </div>
                                </div>

                            </div>



                        </div>
                    </div>

                </div>
            </div>
        </div>
    </asp:Panel>



    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <asp:Button ID="btndis" runat="server" Text="Button" Style="display: none;" />
            <asp:ModalPopupExtender ID="MPDis" runat="server" Enabled="True" TargetControlID="btndis"
                PopupControlID="pnldis" CancelControlID="btnClose" PopupDragHandleControlID="PopupHeaderKits" Drag="true" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>

        </ContentTemplate>
    </asp:UpdatePanel>

    <asp:Panel runat="server" ID="pnldis" DefaultButton="lbtnSearch">
        <div runat="server" id="Div2" class="panel panel-primary Mheight">

            <asp:Label ID="Label9" runat="server" Text="Label" Visible="false"></asp:Label>
            <div class="panel panel-default">
                <div class="panel-heading">
                    <asp:LinkButton ID="LinkButton20" runat="server">
                             <span class="glyphicon glyphicon-remove" aria-hidden="true">Close</span>
                    </asp:LinkButton>
                    <div class="col-sm-11">
                    </div>
                    <div class="col-sm-1">
                    </div>
                </div>
                <div class="panel-body">
                    <div class="row">
                        <div class="col-sm-12">

                            <div class="col-sm-12">

                                <div class="row">

                                    <div class="col-sm-12 labelText1">
                                        Discount Request Detail
                                    </div>
                                </div>

                                <div class="row">
                                    <div class="col-sm-12 height5">
                                    </div>
                                </div>

                                <div class="row">

                                    <div class="col-sm-3 labelText1">
                                        Category
                                    </div>

                                    <div class="col-sm-3 paddingRight5">
                                        <asp:DropDownList ID="ddlDisCategory" AutoPostBack="true" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlDisCategory_SelectedIndexChanged">
                                            <asp:ListItem>Customer</asp:ListItem>
                                            <asp:ListItem>Item</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>

                                    <div class="col-sm-2 labelText1">
                                        Rate
                                    </div>

                                    <div class="col-sm-3 paddingRight5">
                                        <asp:TextBox runat="server" ID="txtDisAmount" CssClass="form-control"></asp:TextBox>
                                    </div>

                                    <div class="col-sm-1 paddingRight5">
                                        <asp:LinkButton ID="LinkButton21" CausesValidation="false" runat="server" OnClick="LinkButton21_Click">
                                                        <span class="glyphicon glyphicon-arrow-right" aria-hidden="true"></span>
                                        </asp:LinkButton>
                                    </div>

                                </div>

                                <div class="row">
                                    <div class="col-sm-12 height5">
                                    </div>
                                </div>

                                <div class="row">
                                    <div class="col-sm-12">
                                        <asp:ScriptManagerProxy ID="ScriptManagerProxy5" runat="server"></asp:ScriptManagerProxy>
                                        <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                                            <ContentTemplate>

                                                <div id="resultssgrd" class="panelscoll POPupResultspanelscroll">
                                                    <asp:GridView ID="gvDisItem" CausesValidation="false" AutoGenerateColumns="False" runat="server" GridLines="None" CssClass="table table-hover table-striped" OnRowEditing="gvDisItem_RowEditing">
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="Select">
                                                                <ItemTemplate>
                                                                    <asp:CheckBox ID="DisItem_Select" Text=" " runat="server" Enabled="false" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Item">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblSgdd_itm" runat="server" Text='<%# Bind("Sgdd_itm") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Type">
                                                                <ItemTemplate>
                                                                    <asp:DropDownList ID="ddlDiscReqType" runat="server" Enabled="false">
                                                                        <asp:ListItem Text="Rate" />
                                                                        <asp:ListItem Text="Amount" />
                                                                    </asp:DropDownList>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Value">
                                                                <%--             <EditItemTemplate>
                                                                    <asp:TextBox ID="txtDisItem_Amount" onkeydown="return jsDecimals(event);" runat="server"></asp:TextBox>
                                                                </EditItemTemplate>--%>
                                                                <ItemTemplate>
                                                                    <%--<asp:Label ID="lblDisItem_Amount" runat="server"></asp:Label>--%>
                                                                    <asp:TextBox ID="txtDisItem_Amount" Style="text-align: right" onkeydown="return jsDecimals(event);" Text='<%# Bind("sgdd_disc_rt") %>' runat="server"></asp:TextBox>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Book">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblSgdd_pb" runat="server" Text='<%# Bind("Sgdd_pb") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Level">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblSgdd_pb_lvl" runat="server" Text='<%# Bind("Sgdd_pb_lvl") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Status" Visible="false">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblSgdd_stus" runat="server" Text='<%# Bind("sgdd_stus") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <ItemTemplate>
                                                                    <div id="editbtndiv" style="width: 1px">
                                                                        <asp:LinkButton ID="btnDiscountEditItem" CausesValidation="false" CommandName="Edit" runat="server" OnClick="btnDiscountEditItem_Click" Visible="false">
                                                                     <span class="glyphicon glyphicon-pencil" aria-hidden="true" style="font-size:8px"></span>
                                                                        </asp:LinkButton>
                                                                    </div>
                                                                </ItemTemplate>
                                                                <EditItemTemplate>
                                                                    <asp:LinkButton ID="btnDiscountUpdate" CausesValidation="false" runat="server" OnClick="btnDiscountUpdate_Click">
                                                                     <span class="glyphicon glyphicon-ok" aria-hidden="true" style="font-style:oblique" ></span>
                                                                    </asp:LinkButton>
                                                                    <asp:LinkButton ID="btnDiscountCancelEdit" CausesValidation="false" OnClick="btnDiscountCancelEdit_Click" runat="server">
                                                                      <span class="glyphicon glyphicon-remove" aria-hidden="true"></span>
                                                                    </asp:LinkButton>
                                                                </EditItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                    </asp:GridView>
                                                </div>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>
                                </div>

                                <div class="row">
                                    <div class="col-sm-12 height5">
                                    </div>
                                </div>

                                <div class="col-sm-12">
                                    <asp:Button ID="btnLoadDisReqs" Text="View Requests" runat="server" OnClick="btnLoadDisReqs_Click" />
                                </div>

                            </div>



                        </div>
                    </div>

                </div>
            </div>
        </div>
    </asp:Panel>



    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <asp:Button ID="btnbb" runat="server" Text="Button" Style="display: none;" />
            <asp:ModalPopupExtender ID="MPBB" runat="server" Enabled="True" TargetControlID="btnbb"
                PopupControlID="pnlbb" CancelControlID="btnClose" PopupDragHandleControlID="PopupHeaderKits" Drag="true" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>

        </ContentTemplate>
    </asp:UpdatePanel>

    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <asp:Panel runat="server" ID="pnlbb" DefaultButton="lbtnSearch">
                <div runat="server" id="Div3" class="panel panel-primary Mheight">

                    <asp:Label ID="Label10" runat="server" Text="Label" Visible="false"></asp:Label>
                    <div class="panel panel-default">
                        <div class="panel-heading">
                            <asp:LinkButton ID="LinkButton22" runat="server">
                             <span class="glyphicon glyphicon-remove" aria-hidden="true">Close</span>
                            </asp:LinkButton>
                            <div class="col-sm-11">
                            </div>
                            <div class="col-sm-1">
                            </div>
                        </div>

                        <div class="panel-body">
                            <div class="row">
                                <div class="col-sm-12">

                                    <div class="col-sm-12">

                                        <div class="row">

                                            <div class="col-sm-2 labelText1">
                                                Item
                                            </div>

                                            <div class="col-sm-2">
                                                <asp:TextBox runat="server" CssClass="form-control"></asp:TextBox>
                                            </div>

                                            <div class="col-sm-2 labelText1">
                                                Qty
                                            </div>

                                            <div class="col-sm-2">
                                                <asp:TextBox runat="server" CssClass="form-control"></asp:TextBox>
                                            </div>

                                            <div class="col-sm-2 labelText1">
                                                Serial 1
                                            </div>
                                            <div class="col-sm-2">
                                                <asp:TextBox runat="server" CssClass="form-control"></asp:TextBox>
                                            </div>


                                        </div>

                                        <div class="row">
                                            <div class="col-sm-12 height5">
                                            </div>
                                        </div>

                                        <div class="row">

                                            <div class="col-sm-2 labelText1">
                                                Serial 2
                                            </div>

                                            <div class="col-sm-2">
                                                <asp:TextBox runat="server" CssClass="form-control"></asp:TextBox>
                                            </div>

                                            <div class="col-sm-2 labelText1">
                                                Warranty
                                            </div>

                                            <div class="col-sm-2">
                                                <asp:TextBox runat="server" CssClass="form-control"></asp:TextBox>
                                            </div>

                                            <div class="col-sm-2 labelText1">
                                                <asp:LinkButton ID="LinkButton24" CausesValidation="false" runat="server">
                                                        <span class="glyphicon glyphicon-plus" aria-hidden="true" ></span>
                                                </asp:LinkButton>
                                            </div>

                                            <div class="col-sm-2">
                                                <asp:LinkButton ID="LinkButton23" CausesValidation="false" runat="server">
                                                        <span class="glyphicon glyphicon-ok" aria-hidden="true" ></span>Confirm
                                                </asp:LinkButton>
                                            </div>
                                        </div>

                                        <div class="row">
                                            <div class="col-sm-12 height5">
                                            </div>
                                        </div>

                                        <div class="row">
                                            <div class="panel-body">
                                                <div class="col-sm-12">
                                                    <div class="panel panel-default">

                                                        <div class="panel-heading panelHeadingInfoBar">

                                                            <div class="col-sm-4">

                                                                <div class="row">

                                                                    <div class="col-sm-3 labelText1">
                                                                        Description:
                                                                    </div>

                                                                    <div class="col-sm-9" style="margin-top: 3px">
                                                                        <asp:Label ID="Label11" runat="server" ForeColor="#A513D0"></asp:Label>
                                                                    </div>

                                                                </div>

                                                            </div>

                                                            <div class="col-sm-4">

                                                                <div class="row">

                                                                    <div class="col-sm-2 labelText1">
                                                                        Model:
                                                                    </div>
                                                                    <div class="col-sm-10" style="margin-top: 3px">
                                                                        <asp:Label ID="Label12" runat="server" ForeColor="#A513D0"></asp:Label>
                                                                    </div>

                                                                </div>

                                                            </div>

                                                            <div class="col-sm-4">

                                                                <div class="row">

                                                                    <div class="col-sm-2 labelText1">
                                                                        Brand:
                                                                    </div>
                                                                    <div class="col-sm-10" style="margin-top: 3px">
                                                                        <asp:Label ID="Label13" runat="server" ForeColor="#A513D0"></asp:Label>
                                                                    </div>

                                                                </div>

                                                            </div>

                                                        </div>

                                                    </div>
                                                </div>
                                            </div>
                                        </div>

                                    </div>

                                    <div class="row">
                                        <div class="col-sm-12 height5">
                                        </div>
                                    </div>

                                    <div class="row">
                                        <div class="col-sm-12">
                                            <asp:ScriptManagerProxy ID="ScriptManagerProxy6" runat="server"></asp:ScriptManagerProxy>
                                            <asp:UpdatePanel ID="UpdatePanel7" runat="server">
                                                <ContentTemplate>

                                                    <div id="resultssgrsd" class="panelscoll POPupResultspanelscroll">
                                                        <asp:GridView ID="GridView2" CausesValidation="false" AllowPaging="true" runat="server" GridLines="None" CssClass="table table-hover table-striped" PageSize="7" PagerStyle-CssClass="cssPager" OnPageIndexChanging="grdResult_PageIndexChanging" OnSelectedIndexChanged="grdResult_SelectedIndexChanged">
                                                            <Columns>
                                                                <asp:ButtonField Text="Select" CommandName="Select" ItemStyle-Width="10" />
                                                            </Columns>
                                                        </asp:GridView>
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
            </asp:Panel>

        </ContentTemplate>
    </asp:UpdatePanel>


    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <asp:Button ID="btnResevmp" runat="server" Text="Button" Style="display: none;" />
            <asp:ModalPopupExtender ID="mpReservations" runat="server" Enabled="True" TargetControlID="btnResevmp"
                PopupControlID="pnlReservations" CancelControlID="btnCLoseReservation" PopupDragHandleControlID="divReservation" Drag="true" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>
        </ContentTemplate>
    </asp:UpdatePanel>

    <asp:Panel runat="server" ID="pnlReservations" DefaultButton="lbtnSearch">
        <div runat="server" id="Div11" class="panel panel-default height400 width700">
            <asp:UpdateProgress ID="UpdateProgress4" DisplayAfter="10"
                runat="server" AssociatedUpdatePanelID="upReservations">
                <ProgressTemplate>
                    <div class="divWaiting">
                        <asp:Label ID="lblWait4" runat="server" Text="Please wait... " />
                        <asp:Image ID="imgWait4" runat="server" ImageAlign="Middle" ImageUrl="~/images/icons/ajax-loader.gif" />
                    </div>
                </ProgressTemplate>
            </asp:UpdateProgress>
            <asp:UpdatePanel ID="upReservations" runat="server">
                <ContentTemplate>
                    <div class="panel panel-default">
                        <div class="panel-heading" id="divReservation" style="height: 30px;">
                            <div class="col-sm-10">
                                <asp:Button Text="Confirm" ID="btnReservationConfirm" runat="server" OnClick="btnReservationConfirm_Click" />
                            </div>
                            <div class="col-sm-2">
                                <asp:LinkButton ID="btnCLoseReservation" runat="server" OnClick="btnCLoseReservation_Click" Style="float: right">
                             <span class="glyphicon glyphicon-remove" aria-hidden="true">Close</span>
                                </asp:LinkButton>
                            </div>
                        </div>
                        <div class="panel-body">
                            <div class="col-sm-12" runat="server">
                                <asp:GridView ID="dgvReservation" AutoGenerateColumns="false" CausesValidation="false" runat="server" GridLines="None" CssClass="table table-hover table-striped">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Select">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkSelect" Text=" " AutoPostBack="true" runat="server" OnCheckedChanged="chkSelect_CheckedChanged" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Seq" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblirs_seq" runat="server" Text='<%# Bind("IRS_SEQ") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="150px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Doc No">
                                            <ItemTemplate>
                                                <asp:Label ID="lblirs_res_no" runat="server" Text='<%# Bind("IRS_RES_NO") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="150px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Date">
                                            <ItemTemplate>
                                                <asp:Label ID="lblirs_res_dt" runat="server" Text='<%# Bind("IRS_RES_DT", "{0:dd/MMM/yyyy}") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="150px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Cutomer Code">
                                            <ItemTemplate>
                                                <asp:Label ID="lblirs_cust_cd" runat="server" Text='<%# Bind("IRS_CUST_CD") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="150px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Remark">
                                            <ItemTemplate>
                                                <asp:Label ID="lblirs_rmk" runat="server" Text='<%# Bind("IRS_RMK") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="150px" />
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </asp:Panel>



    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <asp:Button ID="btnexcel" runat="server" Text="Button" Style="display: none;" />
            <asp:ModalPopupExtender ID="mpexcel" runat="server" Enabled="True" TargetControlID="btnexcel"
                PopupControlID="pnlpopupExcel" CancelControlID="btnClose" PopupDragHandleControlID="PopupHeaderExcel" Drag="true" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>

        </ContentTemplate>
    </asp:UpdatePanel>

    <asp:Panel runat="server" ID="pnlpopupExcel" DefaultButton="lbtnSearch">
        <div runat="server" id="Div5" class="panel panel-primary Mheight">

            <asp:Label ID="Label3" runat="server" Text="Label" Visible="false"></asp:Label>
            <div class="panel panel-default">
                <div class="panel-heading">
                    <asp:LinkButton ID="LinkButton8" runat="server">
                             <span class="glyphicon glyphicon-remove" aria-hidden="true">Close</span>
                    </asp:LinkButton>
                    <div class="col-sm-11">
                    </div>
                    <div class="col-sm-1">
                    </div>
                </div>
                <div class="panel-body">
                    <div class="row">
                        <div class="col-sm-12">

                            <asp:UpdatePanel runat="server">
                                <ContentTemplate>
                                    <asp:Panel runat="server" ID="pnlpopupExcelss">

                                        <div class="col-sm-12">

                                            <div class="panel panel-default">

                                                <div class="panel-heading">
                                                    <strong>
                                                        <asp:Label ID="lblcaption" runat="server"></asp:Label>
                                                    </strong>
                                                </div>

                                                <div class="panel-body">

                                                    <div class="row">

                                                        <div id="dvitm" runat="server">
                                                            <div class="col-sm-12">

                                                                <div class="panelscoll">

                                                                    <asp:GridView ID="dgvItem" AutoGenerateColumns="false" runat="server" CssClass="table table-hover table-striped" GridLines="None" ShowHeaderWhenEmpty="True" EmptyDataText="No data found..." OnSelectedIndexChanged="gvitems_SelectedIndexChanged">

                                                                        <Columns>

                                                                            <asp:CommandField ShowSelectButton="true" ButtonType="Link" />

                                                                            <asp:TemplateField HeaderText="Item Code">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblitemcodepopup" runat="server" Text='<%# Bind("ITEM") %>' Width="100px"></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField HeaderText="Description">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lbldesc" runat="server" Text='<%# Bind("DESCRIPTION") %>' Width="100px"></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField HeaderText="Model">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblmodel" runat="server" Text='<%# Bind("MODEL") %>' Width="100px"></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField HeaderText="Brand">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblbrand" runat="server" Text='<%# Bind("BRAND") %>' Width="100px"></asp:Label>
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
                                        </div>
                                    </asp:Panel>

                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>

                </div>
            </div>
        </div>
    </asp:Panel>

        <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <asp:Button ID="btnMpdiscoutInv" runat="server" Text="Button" Style="display: none;" />
            <asp:ModalPopupExtender ID="mpDiscountRate" runat="server" Enabled="True" TargetControlID="btnMpdiscoutInv"
                PopupControlID="pnlDiscountRate" PopupDragHandleControlID="DivpnlDiscountRate" Drag="true" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:Panel runat="server" ID="pnlDiscountRate" >
        <asp:UpdatePanel runat="server">
            <ContentTemplate>
                <div class="panel panel-default">
                    <div class="panel-heading height30" id="DivpnlDiscountRate" style="height: 30px;">
                        <div class="col-sm-10">
                        </div>
                        <div class="col-sm-2">
                            <asp:LinkButton ID="btnCLoseDisRate" runat="server" Style="float: right" OnClick="btnCLoseDisRate_Click">
                             <span class="glyphicon glyphicon-remove" aria-hidden="true">Close</span>
                            </asp:LinkButton>
                        </div>
                    </div>
                    <div class="panel-body" style="height: 30px;">
                        <div class="row">
                            <div class="col-sm-12">
                                <div class="col-sm-12">
                                    <div class="row">
                                        <div class="col-sm-5 labelText1 paddingLeft0">
                                            Discount Rate
                                        </div>
                                        <div class="col-sm-5 labelText1 paddingLeft0">
                                            <asp:TextBox ID="txtDisRateInvItem" Style="text-align: right;" onkeydown="return jsDecimals(event);" runat="server" CssClass="form-control" />
                                        </div>
                                        <div class="col-sm-2 paddingRight5 paddingLeft0">
                                            <asp:LinkButton ID="btnApplyDiscountRate" CausesValidation="false" CssClass="floatRight" runat="server" OnClick="btnApplyDiscountRate_Click">
                                         <span class="glyphicon glyphicon-plus" aria-hidden="true"></span>
                                            </asp:LinkButton>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <asp:Label Text="-1" ID="lblDiscountRowNum" runat="server" Visible="false" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </asp:Panel>
      <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <asp:Button ID="Button4" runat="server" Text="Button" Style="display: none;" />
            <asp:ModalPopupExtender ID="MpPriceEdit" runat="server" Enabled="True" TargetControlID="Button4"
                PopupControlID="pnlPriceEdit" PopupDragHandleControlID="divPrice" Drag="true" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>
        </ContentTemplate>
    </asp:UpdatePanel>

    <asp:Panel runat="server" ID="pnlPriceEdit" DefaultButton="lbtnApplyUnitRate">
        <asp:UpdatePanel runat="server">
            <ContentTemplate>
                <div class="panel panel-default width250">
                

             
                     <div class="panel-heading height30" id="divPrice" style="height: 30px;">
                        <div class="col-sm-10">
                        </div>
                        <div class="col-sm-2">
                            <asp:LinkButton ID="lbtnPriceClose" runat="server" Style="float: right" OnClick="lbtnPriceClose_Click">
                             <span class="glyphicon glyphicon-remove" aria-hidden="true">Close</span>
                            </asp:LinkButton>
                        </div>
                    </div>
                    <div class="panel-body" style="height: 30px;">
                        <div class="row">
                            <div class="col-sm-12">
                                <div class="col-sm-12">
                                    <div class="row">
                                        <div class="col-sm-5 labelText1 paddingLeft0">
                                            Unit price
                                        </div>
                                        <div class="col-sm-5 labelText1 paddingLeft0">
                                            <asp:TextBox ID="txtPriceEdit" Style="text-align: right;" onkeydown="return jsDecimals(event);" runat="server" CssClass="form-control" />
                                        </div>
                                        <div class="col-sm-2 paddingRight5 paddingLeft0">
                                            <asp:LinkButton ID="lbtnApplyUnitRate" CausesValidation="false" CssClass="floatRight" runat="server" OnClick="lbtnApplyUnitRate_Click">
                                         <span class="glyphicon glyphicon-plus" aria-hidden="true"></span>
                                            </asp:LinkButton>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <asp:Label Text="-1" ID="Label17" runat="server" Visible="false" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </asp:Panel>
    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <asp:Button ID="btncus" runat="server" Text="Button" Style="display: none;" />
            <asp:ModalPopupExtender ID="CustomerPopoup" runat="server" Enabled="True" TargetControlID="btncus"
                PopupControlID="CustomerPanel" PopupDragHandleControlID="PopupHeader" Drag="true" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>
        </ContentTemplate>
    </asp:UpdatePanel>

    <asp:Panel runat="server" ID="CustomerPanel">
        <div runat="server" id="Div4" class="panel panel-default height600 width700">
            <asp:Label ID="Label14" runat="server" Text="Label" Visible="false"></asp:Label>
            <div class="panel panel-default">
                <div class="panel-heading">
                    <asp:LinkButton ID="btncClose" runat="server" CausesValidation="false" OnClick="btncClose_Click">
                             <span class="glyphicon glyphicon-remove" aria-hidden="true">Close</span>
                    </asp:LinkButton>
                    <div class="col-sm-11">
                    </div>
                    <div class="col-sm-1">
                    </div>
                </div>
                <div class="panel-body">
                    <div class="col-sm-12 paddingLeft0 paddingRight0">

                        <uc1:ucCustomer runat="server" ID="ucCustomer" />

                    </div>
                </div>
            </div>

        </div>
    </asp:Panel>

    <script src="<%=Request.ApplicationPath%>Js/jquery.min.js"></script>
    <script src="<%=Request.ApplicationPath%>Js/bootstrap.min.js"></script>
    <script src="<%=Request.ApplicationPath%>Js/jquery.toastmessage.js"></script>

</asp:Content>
