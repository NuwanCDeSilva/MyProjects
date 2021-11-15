﻿<%@ Page Title="" Language="C#" MasterPageFile="~/View/AdminSite.Master" AutoEventWireup="true" CodeBehind="SalesOrderWeb.aspx.cs" Inherits="FastForward.SCMWeb.View.Transaction.Sales.SalesOrderWeb" %>
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
        function ConfirmUpdateSalesOrder() {
            var selectedvalueOrdPlace = confirm("Do you want to update ?");
            if (selectedvalueOrdPlace) {
                document.getElementById('<%=txtconfirmplaceord.ClientID %>').value = "Yes";
            } else {
                document.getElementById('<%=txtconfirmplaceord.ClientID %>').value = "No";
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

        function ConfirmApproveOrder() {
            var selectedvalueOrdPlace = confirm("Do you want to approve ?");
            if (selectedvalueOrdPlace) {
                document.getElementById('<%=txtapprove.ClientID %>').value = "Yes";
            } else {
                document.getElementById('<%=txtapprove.ClientID %>').value = "No";
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

        function validate() {
            var selectedvalueOrdPlace = confirm("There is no specific discount promotion available. Do you want to save?");
            if (selectedvalueOrdPlace) {
                document.getElementById('<%=txtSavePromotion.ClientID %>').value = "1";
                document.getElementById('<%= btnSave.ClientID %>').click();
                <%=ClientScript.GetPostBackEventReference(upSaveRow, "")%>;

            } else {
                document.getElementById('<%=txtSavePromotion.ClientID %>').value = "0";
            }
        };

        function selectNormalSerialized() {
            var selectedvalueOrdPlace = confirm("This item is having normal serialized price. Do you need to select normal serialized price?");
            if (selectedvalueOrdPlace) {
                document.getElementById('<%=hdfnormalSerialized.ClientID %>').value = "1";
                __doPostBack('<%= txtItem.ClientID %>', '');

            } else {
                document.getElementById('<%=hdfnormalSerialized.ClientID %>').value = "0";
            }
        };

        function continueWithNormalPerializedPrice() {
            var selectedvalueOrdPlace = confirm("This item having normal serialized price. Do you need to continue with normal serialized price?");
            if (selectedvalueOrdPlace) {
                document.getElementById('<%=hdfcontinueWithNormalSerializedPrice.ClientID %>').value = "1";
                __doPostBack('<%= txtItem.ClientID %>', '');

            } else {
                document.getElementById('<%=hdfcontinueWithNormalSerializedPrice.ClientID %>').value = "0";
            }
        };


        function selectPromotionalSerializedPrice() {
            var selectedvalueOrdPlace = confirm("This item is having promotional serialized price.Do you need to select promotional serialized price?");
            if (selectedvalueOrdPlace) {
                document.getElementById('<%=hdfselectPromotionalSerializedPrice.ClientID %>').value = "1";
                __doPostBack('<%= txtItem.ClientID %>', '');

            } else {
                document.getElementById('<%=hdfselectPromotionalSerializedPrice.ClientID %>').value = "0";
            }
        };

        function continueWithTheAvailablePromotions() {
            var selectedvalueOrdPlace = confirm("This item is having promotions. Do you need to continue with the available promotions?");
            if (selectedvalueOrdPlace) {
                document.getElementById('<%=hdfcontinueWithTheAvailablePromotions.ClientID %>').value = "1";
                __doPostBack('<%= txtItem.ClientID %>', '');

            } else {
                document.getElementById('<%=hdfcontinueWithTheAvailablePromotions.ClientID %>').value = "0";
            }
        };

        function ConfirmDeleteItem() {
            var selectedValue = confirm("Do you want to delete this item?");
            if (selectedValue) {
                document.getElementById('<% =hdfDeleteItem.ClientID %>').value = "1";
            }
            else {
                document.getElementById('<% =hdfDeleteItem.ClientID %>').value = "0";
            }
        };

        function RefreshIt(selectObj) {
            __doPostBack('<%= Page.ClientID %>', selectObj.ClientID);
        };

        function confimationAddAnother() {
            var result = confirm("Do you need to add another item?");
            if (result) {
                document.getElementById('<% =txtItem.ClientID %>').focus();
            }
        };

        function ConfirmDeleteSerial() {
            var selectedValue = confirm("Do you want to delete this serial?");
            if (selectedValue) {
                document.getElementById('<% =hdfDelSerial.ClientID %>').value = "1";
            }
            else {
                document.getElementById('<% =hdfDelSerial.ClientID %>').value = "0";
            }
        };

        function ConfirmDeleteBuyBack() {
            var selectedValue = confirm("Do you want to delete this item?");
            if (selectedValue) {
                document.getElementById('<% =hdfDeleBuyBack.ClientID %>').value = "1";
            }
            else {
                document.getElementById('<% =hdfDeleBuyBack.ClientID %>').value = "0";
            }
        };

        function setFocusToBtn(e) {
            var evt = (e) ? e : window.event;
            var key = (evt.keyCode) ? evt.keyCode : evt.which;
            if (key == 9) {
                var adBtn = document.getElementById('<% = lbtnadditems.ClientID %>').value;
                adBtn.focus();
            }
            else if (key == 113) {
                openSerailSearch();
            }
        };

        function SetFocus() {
            var bnItm = document.getElementById('<% =btnSearch_Item.ClientID %>');
            bnItm.focus();
            var ttItem = document.getElementById('<% =txtItem.ClientID %>');
            ttItem.focus();
        };

        function keyDownItem(e) {
            var evt = (e) ? e : window.event;
            var key = (evt.keyCode) ? evt.keyCode : evt.which;
            if (key == 113) {
                openItemSearch();
            }
        };

        function openItemSearch() {
            var bnItm = document.getElementById('<% =btnSearch_Item.ClientID %>');
            bnItm.focus();
            document.getElementById('<% = btnSearch_Item.ClientID %>').click();
        };

        function openSerailSearch() {
            var bnItm = document.getElementById('<% =btnSearch_Serial.ClientID %>');
            bnItm.focus();
            document.getElementById('<% = btnSearch_Serial.ClientID %>').click();
        };



        function scrollWin() {
            window.scrollBy(0, 50);
        };

        function scrollTop() {
            $("div").scrollTop(1000);
        };

        function CheckDeliveryLater() {
            alert("ok");
        };

        function wantToSavePO() {
            var result = confirm("Do you want to save purchase order with the invoice?");
            if (result) {
                document.getElementById('<% =hdfSavePO.ClientID %>').value = "YY";
                document.getElementById('<%= btnSave.ClientID %>').click();
                <%=ClientScript.GetPostBackEventReference(upSaveRow, "")%>;
            }
            else {
                document.getElementById('<% =hdfSavePO.ClientID %>').value = "NN";
            }
        };
         function TxtItemkeyPress(e) {
             alert(e.keyCode);
             //if (e.keyCode == 13) {
             //    __doPostBack('txtItem', '');
             //}
         };
         
         function TxtQtykeyPress(e) {
             if (e.keyCode == 13) {
                 __doPostBack('txtQty', '');
             }
         };
         function TxtQtyFocusOut() {
             $('#BodyContent_lbtnadditems').focus();
         };
    </script>

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

        .dropdownpalan {
            left: -126px !important;
            top: 25px !important;
        }
    </style>

    <script type="text/javascript">
        function showSuccessToast() {
            $().toastmessage('showSuccessToast', "Success Dialog which is fading away ...");
        }
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
        function showNoticeToast() {
            $().toastmessage('showNoticeToast', "Notice  Dialog which is fading away ...");
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
        function showWarningToast() {
            $().toastmessage('showWarningToast', "Warning Dialog which is fading away ...");
        }
        function showStickyWarningToast(value) {
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
        }
    </script>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" runat="server">

    <asp:UpdateProgress ID="UpdateProgress3" DisplayAfter="10"
        runat="server" AssociatedUpdatePanelID="upsaveConf2">
        <ProgressTemplate>
            <div class="divWaiting">
                <asp:Label ID="lblWait3" runat="server" Text="Please wait... " />
                <asp:Image ID="imgWait3" runat="server" ImageAlign="Middle" ImageUrl="~/images/icons/ajax-loader.gif" />
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>

    <asp:UpdateProgress ID="UpdateProgress5" DisplayAfter="10"
        runat="server" AssociatedUpdatePanelID="upAdvanceReBasesd">
        <ProgressTemplate>
            <div class="divWaiting">
                <asp:Label ID="lblWait5" runat="server" Text="Please wait... " />
                <asp:Image ID="imgWait5" runat="server" ImageAlign="Middle" ImageUrl="~/images/icons/ajax-loader.gif" />
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>

    <asp:UpdateProgress ID="uPPPromVouch" DisplayAfter="10"
        runat="server" AssociatedUpdatePanelID="upPromVouch">
        <ProgressTemplate>
            <div class="divWaiting">
                <asp:Label ID="lblWaitPP" runat="server" Text="Please wait... " />
                <asp:Image ID="imgWaitPP" runat="server" ImageAlign="Middle" ImageUrl="~/images/icons/ajax-loader.gif" />
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>

    <asp:UpdateProgress ID="UpdateProgress1" DisplayAfter="10"
        runat="server" AssociatedUpdatePanelID="upSaveRow">
        <ProgressTemplate>
            <div class="divWaiting">
                <asp:Label ID="lblWait" runat="server" Text="Please wait... " />
                <asp:Image ID="imgWait" runat="server" ImageAlign="Middle" ImageUrl="~/images/icons/ajax-loader.gif" />
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>

    <asp:UpdateProgress ID="upPInvoiceLoad" DisplayAfter="10"
        runat="server" AssociatedUpdatePanelID="upInvoiceLoad">
        <ProgressTemplate>
            <div class="divWaiting">
                <asp:Label ID="lblWaitINvoLo" runat="server" Text="Please wait... " />
                <asp:Image ID="imgWaitINvoLo" runat="server" ImageAlign="Middle" ImageUrl="~/images/icons/ajax-loader.gif" />
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>

    <asp:UpdateProgress ID="UpPdatePanel18" DisplayAfter="10"
        runat="server" AssociatedUpdatePanelID="UpdatePanel18">
        <ProgressTemplate>
            <div class="divWaiting">
                <asp:Label ID="lblWaitSerch" runat="server" Text="Please wait... " />
                <asp:Image ID="imgWaitSerch" runat="server" ImageAlign="Middle" ImageUrl="~/images/icons/ajax-loader.gif" />
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>


    <%--    <asp:UpdatePanel ID="UpdatePanelMain" runat="server">
        <ContentTemplate>--%>
    <asp:HiddenField ID="txtconfirmclear" runat="server" />
    <asp:HiddenField ID="txtconfirmplaceord" runat="server" />
    <asp:HiddenField ID="txtcancel" runat="server" />
    <asp:HiddenField ID="txtreject" runat="server" />
    <asp:HiddenField ID="txtAddnewItem" runat="server" />

    <asp:HiddenField ID="txtSavePromotion" runat="server" />
    <asp:HiddenField ID="hdfnormalSerialized" runat="server" />
    <asp:HiddenField ID="hdfcontinueWithNormalSerializedPrice" runat="server" />
    <asp:HiddenField ID="hdfselectPromotionalSerializedPrice" runat="server" />
    <asp:HiddenField ID="hdfcontinueWithTheAvailablePromotions" runat="server" />
    <asp:HiddenField ID="hdfDeleteItem" runat="server" />
    <asp:HiddenField ID="hdfDelSerial" runat="server" />
    <asp:HiddenField ID="hdfShowCustomer" runat="server" />
    <asp:HiddenField ID="hdfDeleBuyBack" runat="server" />
    <asp:HiddenField ID="txtapprove" runat="server" />

    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <asp:HiddenField ID="hdfSavePO" runat="server" EnableViewState="true" ViewStateMode="Enabled" />
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdatePanel runat="server" ID="mainupdate">
        <ContentTemplate>
            <div class="panel panel-default marginLeftRight5">
                <div class="row">
                    <div class="col-sm-12 buttonrow">
                        <asp:UpdatePanel runat="server">
                            <ContentTemplate>
                                <div class="col-sm-12 buttonRow paddingRight5" id="divTopCheck" runat="server">
                                    <div class="col-sm-5 buttonRow padding0">
                                    </div>

                                    <div class="col-sm-2 buttonRow padding0">
                                        <asp:CheckBox Text="" ID="chkDeliverLater" AutoPostBack="true" Visible="false" Checked="true" runat="server" OnCheckedChanged="chkDeliverLater_CheckedChanged" />
                                        <asp:CheckBox Text="Delivery now without serials" Style="font-size: inherit" ID="chkDeliverNow" Checked="False" runat="server" Visible="false" />
                                        <asp:LinkButton ID="lnkProcessRegistration" Text="Registration Details" runat="server" OnClick="lnkProcessRegistration_Click" Visible="false" />
                                    </div>
                                    <div class="col-sm-5 buttonRow padding0">
                                        <asp:UpdatePanel ID="upSaveRow" runat="server">
                                            <ContentTemplate>

                                                <div class="col-sm-3 paddingRight0">
                                                    <asp:LinkButton ID="btnSave" CausesValidation="false" runat="server" CssClass="floatRight" OnClientClick="ConfirmPlaceOrder();" OnClick="btnSave_Click">
                                        <span class="glyphicon glyphicon-saved" aria-hidden="true" ></span>Save/Process
                                                    </asp:LinkButton>
                                                    <asp:Button ID="btnSave_temp" Text="text" runat="server" Visible="false" OnClick="btnSave_temp_Click" />
                                                </div>
                                                <div class="col-sm-2 paddingRight0">
                                                    <asp:LinkButton ID="btnUpdate" CausesValidation="false" runat="server" CssClass="floatRight" OnClientClick="ConfirmUpdateSalesOrder()" OnClick="btnUpdate_Click">
                                        <span class="glyphicon glyphicon-save" aria-hidden="true" ></span>Update
                                                    </asp:LinkButton>
                                                </div>
                                                <div class="col-sm-2 paddingRight0 paddingLeft0">
                                                    <asp:LinkButton ID="lbtnapprove" runat="server" CssClass="floatRight" CausesValidation="false" OnClientClick="ConfirmApproveOrder();" OnClick="lbtnapprove_Click">
                            <span class="glyphicon glyphicon-thumbs-up" aria-hidden="true"></span>Approve
                                                    </asp:LinkButton>
                                                </div>
                                                <div class="col-sm-2 paddingRight0">
                                                    <asp:LinkButton ID="lbtnupload" CausesValidation="false" runat="server" OnClick="lbtnupload_Click">
                                                        <span class="glyphicon glyphicon-road" aria-hidden="true" ></span>Delivery
                                                    </asp:LinkButton>
                                                </div>
                                                <div class="col-sm-2 paddingRight0 paddingLeft0">
                                                    <asp:LinkButton ID="lbtnprintord" CausesValidation="false" runat="server" CssClass="floatRight" OnClientClick="ConfirmClearForm();" OnClick="lbtnprintord_Click">
                                         <span class="glyphicon glyphicon-refresh" aria-hidden="true" ></span>Clear
                                                    </asp:LinkButton>
                                                </div>

                                                <div class="col-sm-1">
                                                    <div class="dropdown">
                                                        <a id="dLabel" data-toggle="dropdown" role="button" aria-haspopup="true" aria-expanded="false">
                                                            <span class="glyphicon glyphicon-menu-hamburger floatRight"></span>
                                                        </a>
                                                        <div class="dropdown-menu menupopup dropdownpalan" aria-labelledby="dLabel" style="top: 25px !important; left: -126px !important;">
                                                            <div class="row">
                                                                <div class="col-sm-12">
                                                                    <div class="col-sm-12 paddingRight0">
                                                                        <asp:LinkButton ID="lbtncancel" CausesValidation="false" runat="server" OnClientClick="ConfirmCancelOrder();" OnClick="lbtncancel_Click">
                                                        <span class="glyphicon glyphicon-remove" aria-hidden="true"></span>Cancel
                                                                        </asp:LinkButton>
                                                                    </div>
                                                                    <div class="row">
                                                                        <div class="col-sm-12 height10">
                                                                        </div>
                                                                    </div>
                                                                    <div class="col-sm-12 paddingRight0">
                                                                        <asp:LinkButton ID="lbtnreject" CausesValidation="false" runat="server" OnClientClick="ConfirmRejectOrder();" OnClick="lbtnreject_Click">
                                                        <span class="glyphicon glyphicon-thumbs-down" aria-hidden="true"></span>Reject
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
                                                                        <asp:LinkButton ID="lbtnREqitem" CausesValidation="false" runat="server" OnClick="lbtnREqitem_Click">
                                                        <span class="glyphicon glyphicon-registration-mark" aria-hidden="true"></span>Request
                                                                        </asp:LinkButton>
                                                                    </div>
                                                                    <div class="row">
                                                                        <div class="col-sm-12 height10">
                                                                        </div>
                                                                    </div>
                                                                    <div class="col-sm-12 paddingRight0">
                                                                        <asp:LinkButton ID="lbtnPrint" CausesValidation="false" runat="server" OnClick="lbtnPrint_Click">
                                                        <span class="glyphicon glyphicon-print" aria-hidden="true"></span>Print
                                                                        </asp:LinkButton>
                                                                    </div>
                                                                    <div class="col-sm-12 paddingRight0">
                                                                        <asp:LinkButton ID="btnTest" CausesValidation="false" runat="server" OnClick="btnTest_Click1" Visible="false">
                                                        <span class="glyphicon glyphicon-paperclip" aria-hidden="true" ></span>test btn
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
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>
                                </div>
                                <div class="col-sm-12 paddingRight5" id="div19" runat="server">
                                    <asp:Label Text="lblBackDateInfor" ID="lblBackDateInfor" Font-Size="12px" CssClass="labelText1" runat="server" ForeColor="Red" />
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
                <div class="row" id="divMainRow">
                    <div class="panel-body paddingbottom0">
                        <div class="col-sm-12">
                            <div class="panel panel-default paneldefaultheightorderplan salesinvfirstpnl" id="1s">
                                <div class="panel-heading pannelheading height16 paddingtop0">
                                    Sales Order
                                </div>
                                <asp:UpdatePanel runat="server">
                                    <ContentTemplate>
                                        <div class="panel-body paddingbottom0" id="2ss">
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
                                                                    <asp:LinkButton ID="lbtnimgselectdate"  CausesValidation="false" runat="server">
                                                                        <span class="glyphicon glyphicon-calendar" aria-hidden="true"></span>
                                                                    </asp:LinkButton>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>

                                                    <div class="col-sm-1">
                                                        <div class="row">
                                                            <div class="col-sm-3 labelText1">
                                                                Type
                                                            </div>
                                                            <div class="col-sm-9 paddingRight0">
                                                                <asp:DropDownList ID="cmbInvType" runat="server" AutoPostBack="true"  CssClass="form-control" OnSelectedIndexChanged="cmbInvType_SelectedIndexChanged" TabIndex="1"></asp:DropDownList>
                                                            </div>
                                                        </div>
                                                    </div>

                                                    <div class="col-sm-3">
                                                        <div class="row">
                                                            <div class="col-sm-3 labelText1">
                                                                Doc.Ref.No
                                                            </div>
                                                            <div class="col-sm-9">
                                                                <asp:TextBox ID="txtdocrefno" Enabled="false" Style="text-transform: uppercase" runat="server" CssClass="form-control"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>

                                                    <div class="col-sm-2">
                                                        <div class="row">
                                                            <div class="col-sm-4 labelText1">
                                                                Manual Ref
                                                            </div>
                                                            <div class="col-sm-8 ">
                                                                <asp:TextBox ID="txtManualRefNo" Enabled="true" Style="text-transform: uppercase" runat="server" TabIndex="1" CssClass="form-control"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>

                                                    <div class="col-sm-2">
                                                        <div class="row paddingLeft0">
                                                            <asp:UpdatePanel ID="upInvoiceLoad" runat="server">
                                                                <ContentTemplate>
                                                                    <div class="col-sm-3 labelText1 paddingLeft0">
                                                                        Order No
                                                                    </div>
                                                                    <div class="col-sm-8">
                                                                        <asp:TextBox ID="txtInvoiceNo" runat="server" Style="text-transform: uppercase" 
                                                                            CssClass="form-control" ReadOnly="false" AutoPostBack="true"
                                                                            OnTextChanged="txtInvoiceNo_TextChanged"></asp:TextBox>
                                                                    </div>
                                                                    <div class="col-sm-1 paddingLeft0">
                                                                        <asp:LinkButton ID="lbtnsupplier" runat="server" CausesValidation="false" OnClick="lbtnsupplier_Click">
                                                        <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                                        </asp:LinkButton>
                                                                    </div>
                                                                </ContentTemplate>
                                                            </asp:UpdatePanel>
                                                        </div>
                                                    </div>
                                                    <div class="col-sm-2">
                                                        <div class="row">
                                                            <div class="col-sm-4 labelText1">
                                                                Currency
                                                            </div>
                                                            <div class="col-sm-7 ">
                                                                <asp:TextBox ID="txtcurrency" runat="server" CssClass="form-control" AutoPostBack="true"></asp:TextBox>
                                                            </div>
                                                            <div class="col-sm-1 paddingLeft0">
                                                                <asp:LinkButton ID="lbtncurrency" runat="server" CausesValidation="false" OnClick="lbtncurrency_Click">
                                                        <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                                </asp:LinkButton>
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <asp:Label ID="lblcurrency" Text="Select Currency" runat="server"></asp:Label>
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
                                            <div class="col-sm-6 paddingRight0">
                                                <div class="panel panel-default" id="1" style="height: 137px">
                                                    <div class="panel-heading pannelheading height16 paddingtop0 ">
                                                        Customer
                                                        <asp:Label Text="" Visible="false" ID="lblPostCount" runat="server" />
                                                    </div>
                                                    <div class="panel-body" id="2">
                                                        <div class="row">
                                                            <div class="col-sm-12">
                                                                <div class="col-sm-3">
                                                                    <div class="row">
                                                                        <div class="col-sm-3 labelText1">
                                                                            Code
                                                                        </div>
                                                                        <div class="col-sm-7 paddingRight5">
                                                                            <asp:TextBox ID="txtCustomer" Style="text-transform: uppercase" runat="server" TabIndex="4"
                                                                                CssClass="form-control" AutoPostBack="true" OnTextChanged="txtCustomer_TextChanged"></asp:TextBox>
                                                                        </div>
                                                                        <div class="col-sm-2 paddingLeft0">
                                                                            <asp:LinkButton ID="lbtncode" CausesValidation="false" runat="server" OnClick="lbtncode_Click" TabIndex="5">
                                                                                <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                                            </asp:LinkButton>
                                                                        </div>
                                                                    </div>
                                                                </div>

                                                                <div class="col-sm-3">
                                                                    <div class="row">
                                                                        <div class="col-sm-3 labelText1">
                                                                            NIC
                                                                        </div>
                                                                        <div class="col-sm-7 paddingRight5">
                                                                            <asp:TextBox ID="txtNIC" Style="text-transform: uppercase" runat="server"
                                                                                 CssClass="form-control" AutoPostBack="true" OnTextChanged="txtNIC_TextChanged"></asp:TextBox>
                                                                        </div>
                                                                        <div class="col-sm-2 paddingLeft0">
                                                                            <asp:LinkButton ID="btnSearch_NIC" runat="server" OnClick="btnSearch_NIC_Click">
                                                                                <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                                            </asp:LinkButton>
                                                                        </div>
                                                                    </div>
                                                                </div>

                                                                <div class="col-sm-3">
                                                                    <div class="row">

                                                                        <div class="col-sm-3 labelText1">
                                                                            Mobile
                                                                        </div>
                                                                        <div class="col-sm-7 paddingRight5">
                                                                            <asp:TextBox ID="txtMobile" runat="server" CssClass="form-control" MaxLength="10"
                                                                                 AutoPostBack="true" OnTextChanged="txtMobile_TextChanged"></asp:TextBox>
                                                                        </div>
                                                                        <div class="col-sm-2 paddingLeft0">
                                                                            <asp:LinkButton ID="btnSearch_Mobile" runat="server" CausesValidation="false" OnClick="btnSearch_Mobile_Click">
                                                                                <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                                            </asp:LinkButton>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <asp:Panel runat="server" Visible="false">
                                                                    <div class="col-sm-3">
                                                                        <div class="row">
                                                                            <div class="col-sm-4 labelText1 padding0">
                                                                                Loyalty No
                                                                            </div>
                                                                            <div class="col-sm-6 paddingRight5 padding0">
                                                                                <asp:TextBox ID="txtLoyalty" runat="server" AutoPostBack="true" CssClass="form-control"
                                                                                    OnTextChanged="txtLoyalty_TextChanged"></asp:TextBox>
                                                                            </div>
                                                                            <div class="col-sm-2 paddingLeft0">
                                                                                <asp:LinkButton ID="btnSearch_Loyalty" runat="server" CausesValidation="false" OnClick="btnSearch_Loyalty_Click">
                                                                                <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                                                </asp:LinkButton>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </asp:Panel>
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
                                                                            <asp:DropDownList runat="server" ID="cmbTitle" AutoPostBack="true" CssClass="form-control">
                                                                                <asp:ListItem Value="0">Select</asp:ListItem>
                                                                                <asp:ListItem>MR.</asp:ListItem>
                                                                                <asp:ListItem>MRS.</asp:ListItem>
                                                                                <asp:ListItem>MS.</asp:ListItem>
                                                                                <asp:ListItem>MISS.</asp:ListItem>
                                                                                <asp:ListItem>DR.</asp:ListItem>
                                                                                <asp:ListItem>REV.</asp:ListItem>
                                                                            </asp:DropDownList>
                                                                        </div>
                                                                        <div class="col-sm-2 paddingLeft0">
                                                                            <asp:TextBox ID="txtCusName" runat="server" Style="text-transform: uppercase" Width="328px" CssClass="form-control"
                                                                                AutoPostBack="true" OnTextChanged="txtCusName_TextChanged"></asp:TextBox>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>




                                                        <div class="row">
                                                            <div class="col-sm-1 labelText1">
                                                                Address
                                                            </div>
                                                            <div class="col-sm-5 paddingRight5">
                                                                <asp:TextBox ID="txtAddress1" runat="server" Style="text-transform: uppercase" 
                                                                    CssClass="form-control " AutoPostBack="true" OnTextChanged="txtAddress1_TextChanged"></asp:TextBox>
                                                            </div>
                                                            <div class="col-sm-5 paddingRight5">
                                                                <asp:TextBox ID="txtAddress2" runat="server" Style="text-transform: uppercase"  CssClass="form-control" AutoPostBack="true" OnTextChanged="txtAddress2_TextChanged"></asp:TextBox>
                                                            </div>

                                                        </div>
                                                        <div class="row">
                                                            <div class="col-sm-6">
                                                                <div class="row">
                                                                    <div class="col-sm-5 labelText1">
                                                                        Dispatch Location
                                                                    </div>
                                                                    <div class="col-sm-5 paddingRight5">
                                                                        <asp:TextBox ID="txtPerTown" Style="text-transform: uppercase"  runat="server" AutoPostBack="true" CssClass="form-control" OnTextChanged="txtPerTown_TextChanged" TabIndex="6"></asp:TextBox>
                                                                    </div>
                                                                    <div class="col-sm-2 paddingLeft0">
                                                                        <asp:LinkButton ID="btnTownSearch" runat="server" CausesValidation="false" OnClick="btnTownSearch_Click" TabIndex="7">
                                                                                <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                                        </asp:LinkButton>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="col-sm-6">
                                                                <div class="row">

                                                                    <div class="col-sm-12">
                                                                        <asp:TextBox ID="txtRemarks" placeholder="Remark" runat="server" TextMode="MultiLine"  CssClass="form-control"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-sm-3 paddingRight0">
                                                <div class="panel panel-default" id="3" style="height: 137px">
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
                                                                            <asp:CheckBox runat="server"  ID="chkTaxPayable" AutoPostBack="true" />
                                                                            <asp:Label runat="server" ID="txttaxPay"></asp:Label>
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
                                                <div class="panel panel-default" id="5" style="height: 137px">
                                                    <div class="panel-body" id="6">
                                                        <div class="row">
                                                            <div class="col-sm-12">
                                                                <div class="col-sm-12">
                                                                    <div class="row">
                                                                        <div class="col-sm-3 labelText1">
                                                                            Sales Exe.s
                                                                        </div>
                                                                        <div class="col-sm-5 paddingRight5">
                                                                            <asp:TextBox ID="txtexcutive" runat="server" AutoPostBack="true" CssClass="form-control" OnTextChanged="txtexcutive_TextChanged" TabIndex="2"></asp:TextBox>
                                                                        </div>
                                                                        <div class="col-sm-2 paddingLeft0">
                                                                            <asp:LinkButton ID="lbtnEx" runat="server" CausesValidation="false" OnClick="lbtnEx_Click" TabIndex="3">
                                                                                <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                                            </asp:LinkButton>
                                                                        </div>
                                                                        <div class="col-sm-1">
                                                                            <asp:DropDownList ID="cmbExecutive" Visible="false"  runat="server" AutoPostBack="true" CssClass="form-control"></asp:DropDownList>
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
                                                                        <div class="col-sm-9">
                                                                            <asp:Label ID="lblSalesEx" runat="server" ForeColor="#A513D0"></asp:Label>

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
                                                                            <asp:TextBox runat="server"  ID="txtPoNo" CssClass="form-control" AutoPostBack="true" OnTextChanged="txtPoNo_TextChanged"></asp:TextBox>
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
                                                                            Quotation
                                                                        </div>
                                                                        <div class="col-sm-1 labelText1">
                                                                            <asp:CheckBox runat="server" ID="chkQuotation"  AutoPostBack="true" OnCheckedChanged="chkQuotation_CheckedChanged" />
                                                                        </div>
                                                                        <div class="col-sm-8">
                                                                            <asp:TextBox runat="server" ID="txtQuotation"  CssClass="form-control" AutoPostBack="true" OnTextChanged="txtQuotation_TextChanged"></asp:TextBox>
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
                                                                            Promoter
                                                                        </div>
                                                                        <div class="col-sm-9">
                                                                            <asp:DropDownList runat="server" ID="cmbTechnician" AutoPostBack="true" CssClass="form-control" OnSelectedIndexChanged="cmbTechnician_SelectedIndexChanged"></asp:DropDownList>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <asp:TextBox runat="server" ID="txtPromotor" CssClass="form-control" Visible="false"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </ContentTemplate>
                                </asp:UpdatePanel>

                                <asp:UpdateProgress ID="UpdateProgress2" DisplayAfter="1" runat="server" AssociatedUpdatePanelID="upItemSelect">
                                    <ProgressTemplate>
                                        <div class="divWaiting">
                                            <asp:Label ID="lblWait2" runat="server" Text="Please wait... " />
                                            <asp:Image ID="imgWait2" runat="server" ImageAlign="Middle" ImageUrl="~/images/icons/ajax-loader.gif" />
                                        </div>
                                    </ProgressTemplate>
                                </asp:UpdateProgress>

                                <asp:UpdatePanel ID="upItemSelect" runat="server">
                                    <ContentTemplate>
                                        <div class="row">
                                            <div class="col-sm-12">
                                                <div class="panel panel-default " id="y5">
                                                    <div class="panel-heading pannelheading height16 paddingtop0">
                                                        <div class="row">
                                                            <div class="col-sm-1 paddingRight0">
                                                                Item details
                                                            </div>
                                                            <div class="col-sm-5">
                                                                <div class="col-sm-12">
                                                                    <div class="col-sm-1">
                                                                        <asp:CheckBox AutoPostBack="true" OnCheckedChanged="chkModel_CheckedChanged" Text="" ID="chkModel" runat="server" TabIndex="8" />
                                                                    </div>
                                                                    <div class="col-sm-8 paddingLeft0">
                                                                        Model
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="col-sm-4">
                                                                <div class="col-sm-12">
                                                                    <asp:LinkButton ID="lbtnExcUpload" TabIndex="25" runat="server" CausesValidation="false" OnClick="lbtnExcUpload_Click">
                                                                                <span class="glyphicon" aria-hidden="true"></span>Excel Upload
                                                                    </asp:LinkButton>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="panel-body" id="d6">
                                                        <div class="row">
                                                            <div class="col-sm-12">
                                                                <div class="col-sm-5 padding0">
                                                                    <div class="col-sm-2 padding0">
                                                                        <div class="col-sm-10 padding0">
                                                                            Serial
                                                                        </div>
                                                                        <div class="col-sm-2 padding0">
                                                                            <asp:LinkButton ID="btnSearch_Serial" TabIndex="25" runat="server" CausesValidation="false" OnClick="btnSearch_Serial_Click">
                                                                                <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                                            </asp:LinkButton>
                                                                        </div>
                                                                        <div class="col-sm-12 padding0">
                                                                            <asp:UpdatePanel runat="server">
                                                                                <ContentTemplate>
                                                                                    <asp:TextBox ID="txtSerialNo" runat="server" AutoPostBack="true" CssClass="form-control" onkeydown="setFocusToBtn(event)" OnTextChanged="txtSerialNo_TextChanged" onblur="__doPostBack('txtSerialNo','OnBlur');"></asp:TextBox>
                                                                                </ContentTemplate>
                                                                            </asp:UpdatePanel>
                                                                            <asp:Label Text=" " ID="lblSelectRevervation" runat="server" Visible="false" />

                                                                        </div>
                                                                    </div>
                                                                    <div class="col-sm-2 padding0">
                                                                        <div class="col-sm-10 padding0">
                                                                            <asp:Label ID="lblItmCd" Text="Item" runat="server" />
                                                                        </div>
                                                                        <div class="col-sm-2 padding0">
                                                                            <asp:LinkButton ID="btnSearch_Item" runat="server"  CausesValidation="false" OnClick="btnSearch_Item_Click">
                                                                                <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                                            </asp:LinkButton>
                                                                        </div>
                                                                        <div class="col-sm-12 padding0">
                                                                            <asp:Panel runat="server" ID="pnl1">
                                                                                <asp:UpdatePanel runat="server">
                                                                                    <ContentTemplate>
                                                                                        <asp:TextBox ID="txtItem" TabIndex="-1" runat="server" Style="text-transform: uppercase" CssClass="txtItem form-control" 
                                                                                            AutoPostBack="true" 
                                                                                            OnTextChanged="txtItem_TextChanged"></asp:TextBox>
                                                                                         <asp:TextBox ID="txtModel" Visible="false" TabIndex="-1" runat="server" Style="text-transform: uppercase" CssClass="txtModel form-control" 
                                                                                            AutoPostBack="true" 
                                                                                            OnTextChanged="txtModel_TextChanged"></asp:TextBox>
                                                                                   <%--     <asp:TextBox ID="TextBox1" TabIndex="-1" runat="server" Style="text-transform: uppercase" CssClass="txtItem form-control" 
                                                                                            onkeypress ="return TxtItemkeyPress(event)" onfocusout="TxtItemFocusOut()"
                                                                                            OnTextChanged="txtItem_TextChanged"></asp:TextBox>--%>
                                                                                    </ContentTemplate>
                                                                                </asp:UpdatePanel>
                                                                                <%--<asp:LinkButton OnClick="txtItem_TextChanged" ID="lbtnItemtextChg" Text="" runat="server" Visible="true" />--%>
                                                                                 </asp:Panel>
                                                                            <asp:Label Text=" " ID="lblSelectRevLine" runat="server" Visible="false" />
                                                                        </div>
                                                                    </div>
                                                                    <div class="col-sm-2 padding0">
                                                                        <div class="col-sm-12 padding0">
                                                                            Book
                                                                        </div>
                                                                        <div class="col-sm-12 padding0">
                                                                            <div class="col-sm-12 padding0">
                                                                                <asp:DropDownList ID="cmbBook" runat="server" TabIndex="27" AutoPostBack="true" CssClass="form-control" OnSelectedIndexChanged="cmbBook_SelectedIndexChanged"></asp:DropDownList>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                    <div class="col-sm-2 padding0">
                                                                        <div class="col-sm-12 padding0">
                                                                            Level
                                                                        </div>
                                                                        <div class="col-sm-12 padding0">
                                                                            <div class="col-sm-12 padding0">
                                                                                <asp:DropDownList ID="cmbLevel" runat="server" TabIndex="28" AutoPostBack="true" CssClass="form-control" OnSelectedIndexChanged="cmbLevel_SelectedIndexChanged"></asp:DropDownList>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                    <div class="col-sm-2 padding0">
                                                                        <div class="col-sm-12 padding0">
                                                                            Status
                                                                        </div>
                                                                        <div class="col-sm-12 padding0">
                                                                            <div class="col-sm-12 padding0">
                                                                                <asp:DropDownList ID="cmbStatus" runat="server" TabIndex="29" AutoPostBack="true" CssClass="form-control"></asp:DropDownList>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                    <div class="col-sm-2 padding0">
                                                                        <div class="col-sm-12 padding0">
                                                                            Qty
                                                                        </div>
                                                                        <div class="col-sm-12 padding0">
                                                                            <div class="col-sm-12 padding0">
                                                                                <asp:UpdatePanel runat="server">
                                                                                    <ContentTemplate>
                                                                                        <asp:Panel runat="server" ID="pnl2" >
                                                                                            <asp:TextBox ID="txtQty" runat="server" TabIndex="-1" onkeydown="return jsDecimals(event);" Style="text-align: right"  
                                                                                                 AutoPostBack="true" onfocusout="TxtQtyFocusOut()"
                                                                                                OnTextChanged="txtQty_TextChanged" CssClass="txtQty form-control"></asp:TextBox>
<%--                                                                                            <asp:LinkButton OnClick="lbtnTxtQtyChg_Click" ID="lbtnTxtQtyChg" Visible="false" Text="" runat="server" />--%>
                                                                                         <%--   <asp:TextBox ID="TextBox1" runat="server" TabIndex="-1" onkeydown="return jsDecimals(event);" Style="text-align: right"  
                                                                                                 onkeypress ="return TxtQtykeyPress(event)" onfocusout="TxtQtyFocusOut()"
                                                                                                OnTextChanged="txtQty_TextChanged" CssClass="txtQty form-control"></asp:TextBox>--%>
                                                                                        </asp:Panel>
                                                                                    </ContentTemplate>
                                                                                </asp:UpdatePanel>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class="col-sm-6 padding0">
                                                                    <asp:ScriptManagerProxy ID="ScriptManagerProxy8" runat="server"></asp:ScriptManagerProxy>
                                                                    <asp:UpdatePanel runat="server">
                                                                        <ContentTemplate>
                                                                            <div class="col-sm-2 padding0">
                                                                                <div class="col-sm-12 padding0">
                                                                                    Unit Price
                                                                                </div>
                                                                                <div class="col-sm-12 padding0">
                                                                                    <div class="col-sm-12 padding0">
                                                                                        <asp:Panel runat="server" DefaultButton="lbtntxtUnitPrice">
                                                                                            <asp:TextBox ID="txtUnitPrice" runat="server" TabIndex="31" AutoPostBack="true" OnTextChanged="txtUnitPrice_TextChanged" Style="text-align: right" onkeydown="return jsDecimals(event);" CssClass="form-control"></asp:TextBox>
                                                                                            <asp:LinkButton OnClick="lbtntxtUnitPrice_Click" id="lbtntxtUnitPrice" Text="" visible="false" runat="server" />
                                                                                        </asp:Panel>
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                            <div class="col-sm-2 padding0">
                                                                                <div class="col-sm-12 padding0">
                                                                                    Unit Amount
                                                                                </div>
                                                                                <div class="col-sm-12 padding0">
                                                                                    <div class="col-sm-12 padding0">
                                                                                        <asp:TextBox ID="txtUnitAmt" runat="server" TabIndex="32" ReadOnly="true" onkeydown="return jsDecimals(event);" Style="text-align: right" CssClass="form-control"></asp:TextBox>
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                            <div class="col-sm-2 padding0">
                                                                                <div class="col-sm-12 padding0">
                                                                                    Dis Rate %
                                                                                </div>
                                                                                <div class="col-sm-12 padding0">
                                                                                    <div class="col-sm-12 padding0">
                                                                                        <asp:Panel runat="server" DefaultButton="lbtntxtDisRate">
                                                                                            <asp:TextBox ID="txtDisRate" runat="server" TabIndex="33" onkeydown="return jsDecimals(event);" Style="text-align: right" CssClass="form-control" AutoPostBack="true" OnTextChanged="txtDisRate_TextChanged"></asp:TextBox>
                                                                                            <asp:LinkButton visible="false" OnClick="lbtntxtDisRate_Click" id="lbtntxtDisRate" Text="" runat="server" />
                                                                                        </asp:Panel>
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                            <div class="col-sm-2 padding0">
                                                                                <div class="col-sm-12 padding0">
                                                                                    Dis Amount
                                                                                </div>
                                                                                <div class="col-sm-12 padding0">
                                                                                    <div class="col-sm-12 padding0">
                                                                                        <asp:Panel runat="server" DefaultButton="lbtntxtDisAmt">
                                                                                            <asp:TextBox ID="txtDisAmt" runat="server" TabIndex="34" onkeydown="return jsDecimals(event);" Style="text-align: right" CssClass="form-control" AutoPostBack="true" OnTextChanged="txtDisAmt_TextChanged"></asp:TextBox>
                                                                                            <asp:LinkButton visible="false" OnClick="lbtntxtDisAmt_Click" ID="lbtntxtDisAmt" Text="" runat="server" />
                                                                                        </asp:Panel>
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                            <div class="col-sm-2 padding0">
                                                                                <div class="col-sm-12 padding0">
                                                                                    TAX
                                                                                </div>
                                                                                <div class="col-sm-12 padding0">
                                                                                    <div class="col-sm-12 padding0">
                                                                                        <asp:TextBox ID="txtTaxAmt" runat="server" TabIndex="35" onkeydown="return jsDecimals(event);" Style="text-align: right" CssClass="form-control"></asp:TextBox>
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                            <div class="col-sm-2 padding0">
                                                                                <div class="col-sm-12 padding0">
                                                                                    <div class="col-sm-10 padding0">
                                                                                        Res No
                                                                                    </div>

                                                                                    <div class="col-sm-2 padding0">
                                                                                        <asp:LinkButton ID="btnSearchReservation" runat="server" TabIndex="37" OnClick="btnSearchReservation_Click">
                                                                                    <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                                                        </asp:LinkButton>
                                                                                    </div>
                                                                                </div>
                                                                                <div class="col-sm-12 padding0">
                                                                                    <div class="col-sm-12 padding0">
                                                                                        <asp:TextBox ID="txtresno" runat="server" AutoPostBack="true" CssClass="form-control" OnTextChanged="txtresno_TextChanged"></asp:TextBox>
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                        </ContentTemplate>
                                                                    </asp:UpdatePanel>
                                                                </div>
                                                                <div class="col-sm-1 padding0">
                                                                    <div class="col-sm-10 padding0">
                                                                        <div class="col-sm-12 padding0">
                                                                            Line Amount
                                                                        </div>
                                                                        <div class="col-sm-12 padding0">
                                                                            <div class="col-sm-12 padding0">
                                                                                <asp:TextBox ID="txtLineTotAmt" runat="server" TabIndex="36" onkeydown="return jsDecimals(event);" Style="text-align: right" CssClass="form-control"></asp:TextBox>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                    <div class="col-sm-2 padding0" style="padding-top: 4px; padding-left: 4px;">
                                                                        <asp:LinkButton ID="lbtnadditems" CssClass="lbtnadditems" runat="server" TabIndex="38" CausesValidation="false" OnClick="lbtnadditems_Click">
                                                                     <span class="glyphicon glyphicon-plus" style="font-size:20px;"  aria-hidden="true"></span>
                                                                        </asp:LinkButton>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-sm-12">
                                                                <div class="row">
                                                                    <div class="col-sm-12 height5">
                                                                    </div>
                                                                </div>
                                                                <div class="row">
                                                                    <div class="col-sm-12">
                                                                        <div class="panel panel-default">
                                                                            <div class="panel-heading panelHeadingInfoBar" style="height: 22px; padding-top: 0;">
                                                                                <div class="col-sm-3 paddingLeft0">
                                                                                    <div class="row">
                                                                                        <div class="col-sm-3 labelText1">
                                                                                            Description:
                                                                                        </div>
                                                                                        <div class="col-sm-9 paddingRight0" style="margin-top: 3px">
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
                                                                <div class="row">
                                                                    <div class="panel-body">
                                                                        <div class="col-sm-12">
                                                                            <div class="row">
                                                                                <div class="col-sm-12">
                                                                                    <ul id="myTab" class="nav nav-tabs height22 paddingtop0">
                                                                                        <li class="active">
                                                                                            <a href="#Item" data-toggle="tab">Item</a>
                                                                                        </li>
                                                                                        <li>
                                                                                            <a href="#Serial" data-toggle="tab">Serial</a>
                                                                                        </li>
                                                                                        <span style="display: inline-block; width: 40px;"></span>
                                                                                        <asp:LinkButton ID="LinkButton10" runat="server" CausesValidation="false" Text="<b>Buy Back</b>" Visible="false" OnClick="LinkButton10_Click">
                                                                                        </asp:LinkButton>
                                                                                        <%--  <asp:Button Text="Buy Back Items" OnClick="LinkButton10_Click" Width="150px" ID="btnBuyBack" runat="server" Style="float: right" />--%>
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
                                                                                                                <asp:GridView ID="gvInvoiceItem" AutoGenerateColumns="False" TabIndex="39" runat="server" CssClass="table table-hover table-striped" OnRowDataBound="gvInvoiceItem_RowDataBound" OnRowDeleting="gvInvoiceItem_RowDeleting" OnSelectedIndexChanged="gvInvoiceItem_SelectedIndexChanged" GridLines="None" ShowHeaderWhenEmpty="True" EmptyDataText="No data found...">
                                                                                                                    <Columns>
                                                                                                                        <%--<asp:BoundField DataField="itri_seq_no" HeaderText="Seq No" ItemStyle-Width="150" ReadOnly="true" />--%>
                                                                                                                        <asp:TemplateField HeaderText="No">
                                                                                                                            <ItemTemplate>
                                                                                                                                <asp:Label ID="lblsad_itm_line" runat="server" Text='<%# Bind("sad_itm_line") %>'></asp:Label>
                                                                                                                            </ItemTemplate>
                                                                                                                            <ItemStyle Width="50px" />
                                                                                                                        </asp:TemplateField>
                                                                                                                        <asp:TemplateField HeaderText="Item">
                                                                                                                            <ItemTemplate>
                                                                                                                                <asp:Label ID="InvItm_Item" runat="server" Text='<%# Bind("sad_itm_cd") %>'></asp:Label>
                                                                                                                            </ItemTemplate>
                                                                                                                            <ItemStyle Width="150px" />
                                                                                                                        </asp:TemplateField>
                                                                                                                        <asp:TemplateField HeaderText="Description">
                                                                                                                            <ItemTemplate>
                                                                                                                                <asp:Label ID="lblMi_longdesc" runat="server" Text='<%# Bind("Mi_longdesc") %>'></asp:Label>
                                                                                                                            </ItemTemplate>
                                                                                                                            <ItemStyle Width="250px" />
                                                                                                                        </asp:TemplateField>
                                                                                                                        <asp:TemplateField HeaderText="Model">
                                                                                                                            <ItemTemplate>
                                                                                                                                <asp:Label ID="lblMi_model" runat="server" Text='<%# Bind("Mi_model") %>'></asp:Label>
                                                                                                                            </ItemTemplate>
                                                                                                                            <ItemStyle Width="100px" />
                                                                                                                        </asp:TemplateField>
                                                                                                                        <asp:TemplateField HeaderText="Status">
                                                                                                                            <ItemTemplate>
                                                                                                                                <asp:Label ID="lblsad_itm_stus" runat="server" Text='<%# Bind("sad_itm_stus") %>' Visible="false"></asp:Label>
                                                                                                                                <asp:Label ID="lblsad_itm_stusText" runat="server" Text='<%# Bind("Sad_itm_stus_desc") %>' Visible="true"></asp:Label>
                                                                                                                            </ItemTemplate>
                                                                                                                            <ItemStyle Width="150px" />
                                                                                                                        </asp:TemplateField>

                                                                                                                        <asp:TemplateField HeaderText="Qty" HeaderStyle-BackColor="AliceBlue">
                                                                                                                            <ItemTemplate>
                                                                                                                                <asp:Label ID="InvItm_Qty" runat="server" Text='<%# Bind("sad_qty","{0:N2}") %>'></asp:Label>
                                                                                                                            </ItemTemplate>
                                                                                                                            <ItemStyle Width="100px" BackColor="AliceBlue" />
                                                                                                                            <HeaderStyle CssClass="gridHeaderAlignRight" />
                                                                                                                            <ItemStyle HorizontalAlign="Right" />
                                                                                                                        </asp:TemplateField>
                                                                                                                        <asp:TemplateField HeaderText="Unit Price">
                                                                                                                            <ItemTemplate>
                                                                                                                                <asp:Label ID="InvItm_UPrice" Visible="false" runat="server" Text='<%# Bind("sad_unit_rt","{0:N2}") %>'></asp:Label>
                                                                                                                                <asp:LinkButton ID="lbtnItemPrice" Text='<%# Bind("sad_unit_rt","{0:N2}") %>' runat="server" OnClick="lbtnItemPrice_Click" />
                                                                                                                            </ItemTemplate>
                                                                                                                            <ItemStyle Width="150px" />
                                                                                                                            <ItemStyle HorizontalAlign="Right" />
                                                                                                                            <HeaderStyle CssClass="gridHeaderAlignRight" />
                                                                                                                        </asp:TemplateField>
                                                                                                                        <asp:TemplateField HeaderText="Unit Amt" HeaderStyle-BackColor="AliceBlue">
                                                                                                                            <ItemTemplate>
                                                                                                                                <asp:Label ID="lblsad_unit_amt" runat="server" Text='<%# Bind("sad_unit_amt","{0:N2}") %>'></asp:Label>
                                                                                                                            </ItemTemplate>
                                                                                                                            <ItemStyle Width="150px" BackColor="AliceBlue" />
                                                                                                                            <ItemStyle HorizontalAlign="Right" />
                                                                                                                            <HeaderStyle CssClass="gridHeaderAlignRight" />
                                                                                                                        </asp:TemplateField>
                                                                                                                        <asp:TemplateField HeaderText="Dis.Rate">
                                                                                                                            <ItemTemplate>
                                                                                                                                <asp:Label ID="lblsad_disc_rt" runat="server" Text='<%# Bind("sad_disc_rt","{0:N2}") %>' Visible="false"></asp:Label>
                                                                                                                                <asp:LinkButton ID="btnInvItemDisRate" Text='<%# Bind("sad_disc_rt","{0:N2}") %>' runat="server" OnClick="btnInvItemDisRate_Click" />
                                                                                                                            </ItemTemplate>
                                                                                                                            <ItemStyle Width="150px" />
                                                                                                                            <ItemStyle HorizontalAlign="Right" />
                                                                                                                            <HeaderStyle CssClass="gridHeaderAlignRight" />
                                                                                                                        </asp:TemplateField>
                                                                                                                        <asp:TemplateField HeaderText="Dis.Amt" HeaderStyle-BackColor="AliceBlue">
                                                                                                                            <ItemTemplate>
                                                                                                                                <asp:Label ID="InvItm_DisAmt" runat="server" Text='<%# Bind("sad_disc_amt","{0:N2}") %>'></asp:Label>
                                                                                                                            </ItemTemplate>
                                                                                                                            <ItemStyle Width="150px" BackColor="AliceBlue" />
                                                                                                                            <ItemStyle HorizontalAlign="Right" />
                                                                                                                            <HeaderStyle CssClass="gridHeaderAlignRight" />
                                                                                                                        </asp:TemplateField>
                                                                                                                        <asp:TemplateField HeaderText="Tax Amt">
                                                                                                                            <ItemTemplate>
                                                                                                                                <asp:Label ID="InvItm_TaxAmt" runat="server" Text='<%# Bind("sad_itm_tax_amt","{0:N2}") %>'></asp:Label>
                                                                                                                            </ItemTemplate>
                                                                                                                            <ItemStyle Width="150px" />
                                                                                                                            <ItemStyle HorizontalAlign="Right" />
                                                                                                                            <HeaderStyle CssClass="gridHeaderAlignRight" />
                                                                                                                        </asp:TemplateField>
                                                                                                                        <asp:TemplateField HeaderText="Line Amt" HeaderStyle-BackColor="AliceBlue">
                                                                                                                            <ItemTemplate>
                                                                                                                                <asp:Label ID="lblsad_tot_amt" runat="server" Text='<%# Bind("sad_tot_amt","{0:N2}") %>'></asp:Label>
                                                                                                                            </ItemTemplate>
                                                                                                                            <ItemStyle Width="150px" BackColor="AliceBlue" />
                                                                                                                            <ItemStyle HorizontalAlign="Right" />
                                                                                                                            <HeaderStyle CssClass="gridHeaderAlignRight" />
                                                                                                                        </asp:TemplateField>
                                                                                                                        <asp:TemplateField HeaderText="Book">
                                                                                                                            <ItemTemplate>
                                                                                                                                <asp:Label ID="InvItm_Book" runat="server" Text='<%# Bind("sad_pbook") %>'></asp:Label>
                                                                                                                            </ItemTemplate>
                                                                                                                            <ItemStyle Width="150px" />
                                                                                                                        </asp:TemplateField>
                                                                                                                        <asp:TemplateField HeaderText="Level">
                                                                                                                            <ItemTemplate>
                                                                                                                                <asp:Label ID="InvItm_Level" runat="server" Text='<%# Bind("sad_pb_lvl") %>'></asp:Label>
                                                                                                                            </ItemTemplate>
                                                                                                                            <ItemStyle Width="150px" />
                                                                                                                        </asp:TemplateField>
                                                                                                                        <asp:TemplateField HeaderText="Reservation No" Visible="false">
                                                                                                                            <ItemTemplate>
                                                                                                                                <asp:Label ID="lblsad_res_no" runat="server" Text='<%# Bind("sad_res_no") %>'></asp:Label>
                                                                                                                            </ItemTemplate>
                                                                                                                            <ItemStyle Width="150px" />
                                                                                                                        </asp:TemplateField>
                                                                                                                        <asp:TemplateField HeaderText="Job Line" Visible="false">
                                                                                                                            <ItemTemplate>
                                                                                                                                <asp:Label ID="InvItm_JobLine" runat="server" Text='<%# Bind("sad_job_line") %>'></asp:Label>
                                                                                                                            </ItemTemplate>
                                                                                                                            <ItemStyle Width="150px" />
                                                                                                                        </asp:TemplateField>
                                                                                                                        <asp:TemplateField HeaderText="Res.No" Visible="true">
                                                                                                                            <EditItemTemplate>
                                                                                                                                <asp:TextBox ID="txtGridResNo" runat="server" Text='<%# Bind("sad_res_no") %>'></asp:TextBox>
                                                                                                                            </EditItemTemplate>
                                                                                                                            <ItemTemplate>
                                                                                                                                <asp:Label ID="InvItm_ResNo" runat="server" Text='<%# Bind("sad_res_no") %>'></asp:Label>
                                                                                                                            </ItemTemplate>
                                                                                                                            <ItemStyle Width="150px" />
                                                                                                                        </asp:TemplateField>
                                                                                                                        <asp:TemplateField HeaderText="Res.Line No" Visible="false">
                                                                                                                            <ItemTemplate>
                                                                                                                                <asp:Label ID="InvItm_ResLine" runat="server" Text='<%# Bind("sad_res_line_no") %>'></asp:Label>
                                                                                                                            </ItemTemplate>
                                                                                                                            <ItemStyle Width="150px" />
                                                                                                                        </asp:TemplateField>
                                                                                                                        <asp:TemplateField HeaderText="Promotion Code" Visible="false">
                                                                                                                            <ItemTemplate>
                                                                                                                                <asp:Label ID="InvItm_PromoCd" runat="server" Text='<%# Bind("sad_promo_cd") %>'></asp:Label>
                                                                                                                            </ItemTemplate>
                                                                                                                            <ItemStyle Width="150px" />
                                                                                                                        </asp:TemplateField>
                                                                                                                        <asp:TemplateField HeaderText="pdseq" Visible="false">
                                                                                                                            <ItemTemplate>
                                                                                                                                <asp:Label ID="InvItm_PbSeq" runat="server" Text='<%# Bind("sad_seq") %>'></asp:Label>
                                                                                                                            </ItemTemplate>
                                                                                                                            <ItemStyle Width="150px" />
                                                                                                                        </asp:TemplateField>
                                                                                                                        <asp:TemplateField HeaderText="PdItemseq" Visible="false">
                                                                                                                            <ItemTemplate>
                                                                                                                                <asp:Label ID="InvItm_PbLineSeq" runat="server" Text='<%# Bind("sad_itm_seq") %>'></asp:Label>
                                                                                                                            </ItemTemplate>
                                                                                                                            <ItemStyle Width="150px" />
                                                                                                                        </asp:TemplateField>
                                                                                                                        <asp:TemplateField>
                                                                                                                            <ItemTemplate>
                                                                                                                                <div id="delbtndiv">
                                                                                                                                    <asp:LinkButton ID="lbtndelitem" CausesValidation="false" CommandName="Delete" runat="server" OnClientClick="ConfirmDeleteItem();" OnClick="lbtndelitem_Click">
                                                                                                                                    <span class="glyphicon glyphicon-trash" aria-hidden="true" style="font-size:15px"></span> 
                                                                                                                                    </asp:LinkButton>
                                                                                                                                </div>
                                                                                                                            </ItemTemplate>
                                                                                                                        </asp:TemplateField>
                                                                                                                        <asp:TemplateField>
                                                                                                                            <ItemTemplate>
                                                                                                                                <asp:LinkButton ID="btnAddSerials" CausesValidation="false" runat="server" OnClick="btnAddSerials_Click" Visible="false">
                                                                                                                                    <span class="glyphicon glyphicon-tag" aria-hidden="true" style="font-size:15px"></span> 
                                                                                                                                </asp:LinkButton>
                                                                                                                            </ItemTemplate>
                                                                                                                        </asp:TemplateField>
                                                                                                                        <asp:TemplateField>
                                                                                                                            <ItemTemplate>
                                                                                                                                <asp:LinkButton ID="lbtnViewinv" CausesValidation="false" ToolTip="Press to view inventory balance brekup" runat="server" OnClick="lbtnViewinv_Click">
                                                                                                                                    <span class="glyphicon glyphicon-tag" aria-hidden="true" style="font-size:15px"></span> 
                                                                                                                                </asp:LinkButton>
                                                                                                                            </ItemTemplate>
                                                                                                                        </asp:TemplateField>
                                                                                                                        <asp:TemplateField>
                                                                                                                            <ItemTemplate>
                                                                                                                                <asp:LinkButton ID="lbtnadReq" CausesValidation="false" runat="server" OnClick="lbtnadReq_Click" Visible="false">
                                                                                                                                    <span class="glyphicon glyphicon-pencil" aria-hidden="true" style="font-size:15px"></span> 
                                                                                                                                </asp:LinkButton>
                                                                                                                            </ItemTemplate>
                                                                                                                        </asp:TemplateField>
                                                                                                                        <asp:TemplateField ShowHeader="False">
                                                                                                                            <ItemTemplate>
                                                                                                                                <asp:LinkButton ID="lbtnResEdit" CausesValidation="false" runat="server" OnClick="lbtnResEdit_Click">
                                                                                                                                    <span class="glyphicon glyphicon-pencil" aria-hidden="true"></span>
                                                                                                                                </asp:LinkButton>
                                                                                                                            </ItemTemplate>
                                                                                                                            <EditItemTemplate>
                                                                                                                                <asp:LinkButton ID="lbtnResUpdate" runat="server" CausesValidation="false" CommandName="Update" OnClick="lbtnResUpdate_Click">
                                                                                                                                    <span class="glyphicon glyphicon-ok" aria-hidden="true"></span>
                                                                                                                                </asp:LinkButton>
                                                                                                                            </EditItemTemplate>
                                                                                                                            <ItemStyle HorizontalAlign="Right" Width="1%" />
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
                                                                                                                <asp:GridView ID="gvPopSerial" AutoGenerateColumns="False" TabIndex="40" runat="server" CssClass="table table-hover table-striped" OnRowDataBound="gvPopSerial_RowDataBound" OnRowDeleting="gvPopSerial_RowDeleting" GridLines="None" ShowHeaderWhenEmpty="True" EmptyDataText="No data found...">
                                                                                                                    <Columns>
                                                                                                                        <asp:TemplateField HeaderText="No" Visible="False">
                                                                                                                            <ItemTemplate>
                                                                                                                                <asp:Label ID="Label1" runat="server" Text='<%# Bind("tus_itm_line") %>'></asp:Label>
                                                                                                                            </ItemTemplate>
                                                                                                                        </asp:TemplateField>
                                                                                                                        <asp:TemplateField HeaderText="Base Item Line" Visible="False">
                                                                                                                            <ItemTemplate>
                                                                                                                                <asp:Label ID="popSer_BaseItemLine" runat="server" Text='<%# Bind("tus_base_itm_line") %>'></asp:Label>
                                                                                                                            </ItemTemplate>
                                                                                                                        </asp:TemplateField>
                                                                                                                        <asp:TemplateField HeaderText="Item">
                                                                                                                            <ItemTemplate>
                                                                                                                                <asp:Label ID="popSer_Item" runat="server" Text='<%# Bind("tus_itm_cd") %>'></asp:Label>
                                                                                                                            </ItemTemplate>
                                                                                                                        </asp:TemplateField>
                                                                                                                        <asp:TemplateField HeaderText="Model">
                                                                                                                            <ItemTemplate>
                                                                                                                                <asp:Label ID="popSer_Model" runat="server" Text='<%# Bind("Tus_itm_model") %>'></asp:Label>
                                                                                                                            </ItemTemplate>
                                                                                                                        </asp:TemplateField>
                                                                                                                        <asp:TemplateField HeaderText="Status">
                                                                                                                            <ItemTemplate>
                                                                                                                                <asp:Label ID="popSer_Status" runat="server" Text='<%# Bind("Tus_itm_stus") %>' Visible="false"></asp:Label>
                                                                                                                                <asp:Label ID="popSer_StatusDesc" runat="server" Text='<%# Bind("Tus_itm_stus_Desc") %>'></asp:Label>
                                                                                                                            </ItemTemplate>
                                                                                                                        </asp:TemplateField>
                                                                                                                        <asp:TemplateField HeaderText="Qty">
                                                                                                                            <ItemTemplate>
                                                                                                                                <asp:Label ID="popSer_Qty" runat="server" Text='<%# Bind("Tus_qty","{0:N2}") %>'></asp:Label>
                                                                                                                            </ItemTemplate>
                                                                                                                            <ItemStyle HorizontalAlign="Right" />
                                                                                                                            <HeaderStyle CssClass="gridHeaderAlignRight" />
                                                                                                                        </asp:TemplateField>

                                                                                                                        <asp:TemplateField HeaderText=" ">
                                                                                                                            <ItemTemplate>
                                                                                                                                <asp:Label ID="lblSpace" runat="server" Text='  '></asp:Label>
                                                                                                                            </ItemTemplate>
                                                                                                                            <ItemStyle HorizontalAlign="Right" />
                                                                                                                            <HeaderStyle CssClass="gridHeaderAlignRight" />
                                                                                                                        </asp:TemplateField>

                                                                                                                        <asp:TemplateField HeaderText="Serial 1">
                                                                                                                            <ItemTemplate>
                                                                                                                                <asp:Label ID="popSer_Serial1" runat="server" Text='<%# Bind("Tus_ser_1") %>'></asp:Label>
                                                                                                                            </ItemTemplate>
                                                                                                                        </asp:TemplateField>
                                                                                                                        <asp:TemplateField HeaderText="Serial 2">
                                                                                                                            <ItemTemplate>
                                                                                                                                <asp:Label ID="popSer_Serial2" runat="server" Text='<%# Bind("Tus_ser_2") %>'></asp:Label>
                                                                                                                            </ItemTemplate>
                                                                                                                        </asp:TemplateField>
                                                                                                                        <asp:TemplateField HeaderText="Warranty">
                                                                                                                            <ItemTemplate>
                                                                                                                                <asp:Label ID="popSer_Warranty" runat="server" Text='<%# Bind("Tus_warr_no") %>'></asp:Label>
                                                                                                                            </ItemTemplate>
                                                                                                                        </asp:TemplateField>
                                                                                                                        <asp:TemplateField HeaderText="SerialID" Visible="false">
                                                                                                                            <ItemTemplate>
                                                                                                                                <asp:Label ID="popSer_SerialID" runat="server" Text='<%# Bind("Tus_serial_id") %>'></asp:Label>
                                                                                                                            </ItemTemplate>
                                                                                                                        </asp:TemplateField>
                                                                                                                        <asp:TemplateField HeaderText="New Status" Visible="false">
                                                                                                                            <ItemTemplate>
                                                                                                                                <asp:Label ID="popSer_NewStatus" runat="server" Text='<%# Bind("Tus_new_status") %>'></asp:Label>
                                                                                                                            </ItemTemplate>
                                                                                                                        </asp:TemplateField>
                                                                                                                        <asp:TemplateField HeaderText="Unit Price" Visible="false">
                                                                                                                            <ItemTemplate>
                                                                                                                                <asp:Label ID="popSer_UnitPrice" runat="server" Text='<%# Bind("Tus_unit_price") %>'></asp:Label>
                                                                                                                            </ItemTemplate>
                                                                                                                        </asp:TemplateField>
                                                                                                                        <asp:TemplateField>
                                                                                                                            <ItemTemplate>
                                                                                                                                <div id="delbtndiv">
                                                                                                                                    <asp:LinkButton ID="lbtndelitem" CausesValidation="false" CommandName="Delete" runat="server" OnClientClick="return ConfirmDeleteSerial()" OnClick="btnDelSerials">
                                                                                                                                    <span class="glyphicon glyphicon-trash" aria-hidden="true" style="font-size:15px"></span> 
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
                                                                                            <div class="col-sm-4 labelText1">
                                                                                                Discount:
                                                                                            </div>
                                                                                            <div class="col-sm-8" style="margin-top: 3px">
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
        </ContentTemplate>
    </asp:UpdatePanel>
    <%-- Other Controls--%>
    <%--    </ContentTemplate>
    </asp:UpdatePanel>--%>

    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <asp:Button ID="Button3" runat="server" Text="Button" Style="display: none;" />
            <asp:ModalPopupExtender ID="SIPopup" runat="server" TargetControlID="Button3"
                PopupControlID="pnlpopup" PopupDragHandleControlID="divSearchheader" Drag="true" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>
        </ContentTemplate>
    </asp:UpdatePanel>

    <asp:Panel runat="server" ID="pnlpopup" DefaultButton="lbtnSearch">
        <asp:ScriptManagerProxy ID="ScriptManagerProxy17" runat="server"></asp:ScriptManagerProxy>
        <asp:UpdatePanel runat="server" ID="pnlpopupUpdate">
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
                                     <asp:UpdateProgress ID="UpdateProgress7" DisplayAfter="10"
                                    runat="server" AssociatedUpdatePanelID="UpdatePanel5">

                                    <ProgressTemplate>
                                        <div class="divWaiting">
                                            <asp:Label ID="lblWaitnew" runat="server"
                                                Text="Please wait... " />
                                            <asp:Image ID="lblWaitnewImage" runat="server"
                                                ImageAlign="Middle" ImageUrl="~/images/banners/hidden_progress_bar.gif" />
                                        </div>
                                    </ProgressTemplate>

                                </asp:UpdateProgress>
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
            </ContentTemplate>
        </asp:UpdatePanel>
    </asp:Panel>

    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <asp:Button ID="btn2" runat="server" Text="Button" Style="display: none;" />
            <asp:ModalPopupExtender ID="MpDelivery" runat="server" Enabled="True" TargetControlID="btn2"
                PopupControlID="pnldel" CancelControlID="btnClose" PopupDragHandleControlID="divDelverHdr" Drag="true" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>
        </ContentTemplate>
    </asp:UpdatePanel>

    <asp:Panel runat="server" ID="pnldel" DefaultButton="lbtnSearch">
        <div runat="server" id="Div1">
            <asp:Label ID="Label7" runat="server" Text="Label" Visible="false"></asp:Label>
            <div class="panel panel-default">
                <div class="panel-heading height30" id="divDelverHdr">
                    <div class="col-sm-11">
                    </div>
                    <div class="col-sm-1">
                        <asp:LinkButton ID="LinkButton13" runat="server" Style="float: right">
                            <span class="glyphicon glyphicon-remove" aria-hidden="true">Close</span>
                        </asp:LinkButton>
                    </div>
                </div>
                <div class="panel-body">
                    <asp:Panel ID="pnlDeliverBOdy" runat="server">
                        <asp:UpdatePanel runat="server">
                            <ContentTemplate>
                                <div class="row">
                                    <div class="col-sm-12">
                                        <div class="row">
                                            <div class="col-sm-3 labelText1">
                                            </div>
                                            <div class="col-sm-9 paddingRight5">
                                                <asp:CheckBox ID="chkOpenDelivery" Visible="false" Checked="true" Text="Delivery on any location" runat="server" AutoPostBack="true" OnCheckedChanged="chkOpenDelivery_CheckedChanged" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-12">
                                        <div class="col-sm-6">
                                            <div class="row">
                                                <div class="col-sm-5 labelText1">
                                                    Delivery Code
                                                </div>
                                                <div class="col-sm-5 paddingRight5">
                                                    <asp:TextBox ID="txtdellocation" runat="server" CssClass="form-control"></asp:TextBox>
                                                </div>
                                                <div class="col-sm-2 paddingRight5">
                                                    <asp:LinkButton ID="btnSearchDelLocation" runat="server" CausesValidation="false" OnClick="btnSearchDelLocation_Click">
                                                        <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                    </asp:LinkButton>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-sm-6">
                                            <div class="row">
                                                <div class="col-sm-5 labelText1">
                                                    Customer Code
                                                </div>
                                                <div class="col-sm-5 paddingRight5">
                                                    <asp:TextBox ID="txtdelcuscode" runat="server" CssClass="form-control"></asp:TextBox>
                                                </div>
                                                <div class="col-sm-2 paddingRight5">
                                                    <asp:LinkButton ID="btnDelCustomerSearch" runat="server" CausesValidation="false" OnClick="btnDelCustomerSearch_Click">
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
                                    <div class="col-sm-12" style="display: none">
                                        <div class="col-sm-4 labelText1">
                                            Dispatch Location
                                        </div>
                                        <div class="col-sm-7 paddingRight5">
                                            <asp:TextBox ID="txtlocation" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                        <div class="col-sm-1 paddingLeft0">
                                            <asp:LinkButton ID="lbtnloc" runat="server"  CausesValidation="false" OnClick="lbtnloc_Click">
                                             <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                            </asp:LinkButton>
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
                                                <div class="col-sm-4 labelText1">
                                                    <asp:LinkButton ID="lbtndconfirm" CausesValidation="false" runat="server" OnClick="lbtndconfirm_Click">
                                                        <span class="glyphicon glyphicon-ok paddingRight5" aria-hidden="true" ></span>   Confirm
                                                    </asp:LinkButton>
                                                </div>
                                                <div class="col-sm-4 labelText1">
                                                    <asp:LinkButton ID="lbtndreset" CausesValidation="false" runat="server" OnClick="lbtndreset_Click">
                                                        <span class="glyphicon glyphicon-repeat paddingRight5" aria-hidden="true" ></span>   Reset
                                                    </asp:LinkButton>
                                                </div>
                                                <div class="col-sm-4 labelText1">
                                                    <asp:LinkButton ID="lbtndclear" CausesValidation="false" runat="server" OnClick="lbtndclear_Click">
                                                        <span class="glyphicon glyphicon-refresh paddingRight5" aria-hidden="true" ></span>   Clear
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
                                    <div class="col-sm-12 height5">
                                    </div>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </asp:Panel>
                </div>
            </div>
        </div>
    </asp:Panel>

    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <asp:Button ID="btnpv" runat="server" Text="Button" Style="display: none;" />
            <asp:ModalPopupExtender ID="MPPV" runat="server" Enabled="True" TargetControlID="btnpv"
                PopupControlID="pnlpv" CancelControlID="btnClose" PopupDragHandleControlID="divPVHedr" Drag="true" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>
        </ContentTemplate>
    </asp:UpdatePanel>

    <asp:Panel runat="server" ID="pnlpv" DefaultButton="btnPromoVou">
        <asp:UpdatePanel ID="upPromVouch" runat="server">
            <ContentTemplate>
                <div class="panel panel-default">
                    <div class="panel-heading height30" id="divPVHedr" style="height: 30px;">
                        <div class="col-sm-10">
                        </div>
                        <div class="col-sm-2">
                            <asp:LinkButton ID="btnPromoVouClose" runat="server" Style="float: right" OnClick="btnPromoVouClose_Click">
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
                                            Promotion Voucher
                                        </div>
                                        <div class="col-sm-5 labelText1 paddingLeft0">
                                            <asp:TextBox ID="txtPromoVouNo_new" onkeydown="return jsDecimals(event);" runat="server" CssClass="form-control" />
                                        </div>
                                        <div class="col-sm-2 paddingRight5 paddingLeft0">
                                            <asp:LinkButton ID="btnPromoVou" CausesValidation="false" CssClass="floatRight" runat="server" OnClick="btnPromoVou_Click">
                                         <span class="glyphicon glyphicon-plus" aria-hidden="true"></span>
                                            </asp:LinkButton>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <asp:Label Text="lblPromoVouUsedFlag" ID="lblPromoVouUsedFlag" runat="server" />
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
            <asp:Button ID="btndis" runat="server" Text="Button" Style="display: none;" />
            <asp:ModalPopupExtender ID="MPDis" runat="server" Enabled="True" TargetControlID="btndis"
                PopupControlID="pnldis" CancelControlID="btnClose" PopupDragHandleControlID="divhedrDisco" Drag="true" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>
        </ContentTemplate>
    </asp:UpdatePanel>

    <asp:Panel runat="server" ID="pnldis" DefaultButton="lbtnSearch" Height="230px" Width="600px">
        <asp:UpdatePanel runat="server">
            <ContentTemplate>
                <div class="panel panel-default">
                    <div class="panel-heading height30" id="divhedrDisco" style="height: 30px">
                        <div class="col-sm-5">
                            Discount Request Details
                        </div>
                        <div class="col-sm-5">
                            <asp:Button ID="btnLoadDisReqs" Text="View Requests" runat="server" OnClick="btnLoadDisReqs_Click" />
                        </div>
                        <div class="col-sm-2">
                            <asp:LinkButton ID="LinkButton20" runat="server" Style="float: right">
                             <span class="glyphicon glyphicon-remove" aria-hidden="true">Close</span>
                            </asp:LinkButton>
                        </div>
                    </div>
                    <div class="panel-body">
                        <div class="row">
                            <div class="col-sm-12">
                                <asp:UpdatePanel runat="server">
                                    <ContentTemplate>
                                        <div class="col-sm-12">
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
                                                    <asp:TextBox ID="txtDisAmount" runat="server" onkeydown="return jsDecimals(event);" CssClass="form-control"></asp:TextBox>
                                                </div>
                                                <div class="col-sm-1 paddingRight5">
                                                    <asp:LinkButton ID="btnRequest" CausesValidation="false" runat="server" OnClick="btnRequest_Click">
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
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </div>
                                            </div>
                                        </div>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </asp:Panel>

    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <asp:Button ID="btnbb" runat="server" Text="Button" Style="display: none;" />
            <asp:ModalPopupExtender ID="mpBuyBack" runat="server" Enabled="True" TargetControlID="btnbb"
                PopupControlID="pnlBuyBack" CancelControlID="btnClose" PopupDragHandleControlID="divBuyBackHeader" Drag="true" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>
        </ContentTemplate>
    </asp:UpdatePanel>

    <asp:Panel runat="server" ID="pnlBuyBack" DefaultButton="lbtnSearch">
        <asp:UpdatePanel runat="server">
            <ContentTemplate>
                <div class="panel panel-default" style="width: 865px;">
                    <div class="panel-heading height30" id="divBuyBackHeader">
                        <asp:LinkButton ID="btnBuyBackClose" runat="server" OnClick="btnBuyBackClose_Click">
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
                                    <asp:UpdatePanel runat="server">
                                        <ContentTemplate>
                                            <div class="col-sm-4">
                                                <div class="col-sm-3 paddingLeft0">
                                                    Item
                                                </div>
                                                <div class="col-sm-7 paddingLeft0">
                                                    <asp:TextBox ID="txtBBItem" runat="server" AutoPostBack="true" CssClass="form-control" OnTextChanged="txtBBItem_TextChanged"></asp:TextBox>
                                                </div>
                                                <div class="col-sm-2 paddingLeft0">
                                                    <asp:LinkButton ID="btnBBItemSearch" runat="server" TabIndex="26" CausesValidation="false" OnClick="btnBBItemSearch_Click">
                                                    <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                    </asp:LinkButton>
                                                </div>
                                            </div>
                                            <div class="col-sm-4 paddingLeft0">
                                                <div class="col-sm-3 paddingLeft0 paddingRight0">
                                                    Qty
                                                </div>
                                                <div class="col-sm-7 paddingLeft0">
                                                    <asp:TextBox ID="txtBBQty" runat="server" onkeydown="return jsDecimals(event);" CssClass="form-control" OnTextChanged="txtBBQty_TextChanged" Style="text-align: right"></asp:TextBox>
                                                </div>
                                                <div class="col-sm-2 paddingLeft5 paddingLeft0">
                                                </div>
                                            </div>
                                            <div class="col-sm-4 paddingLeft0">
                                                <div class="col-sm-3 paddingLeft0 paddingRight0">
                                                    Serial 1
                                                </div>
                                                <div class="col-sm-7 paddingLeft0">
                                                    <asp:TextBox ID="txtBBSerial1" runat="server" AutoPostBack="true" CssClass="form-control" OnTextChanged="txtBBSerial1_TextChanged"></asp:TextBox>
                                                </div>
                                                <div class="col-sm-2 paddingLeft0">
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-12 height5">
                                                </div>
                                            </div>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>

                                </div>
                                <div class="col-sm-12">
                                    <div class="col-sm-4">
                                        <div class="col-sm-3 paddingLeft0 paddingRight0">
                                            Serial 2
                                        </div>
                                        <div class="col-sm-7 paddingLeft0">
                                            <asp:TextBox ID="txtBBSerial2" runat="server" CssClass="form-control" OnTextChanged="txtBBSerial2_TextChanged"></asp:TextBox>
                                        </div>
                                        <div class="col-sm-2 paddingLeft0">
                                        </div>
                                    </div>
                                    <div class="col-sm-4 paddingLeft0" style="display: none">
                                        <div class="col-sm-3 paddingLeft0 paddingRight0">
                                            Warranty
                                        </div>
                                        <div class="col-sm-7 paddingLeft5">
                                            <asp:TextBox ID="txtBBWarranty" runat="server" CssClass="form-control" OnTextChanged="txtBBWarranty_TextChanged"></asp:TextBox>
                                        </div>
                                        <div class="col-sm-2 paddingLeft0">
                                        </div>
                                    </div>
                                    <div class="col-sm-4 paddingLeft0">
                                        <div class="col-sm-6 paddingLeft0 labelText1">
                                            <div class="col-sm-2 padding0">
                                                <asp:LinkButton ID="btnBBAddItem" CausesValidation="false" runat="server" OnClick="btnBBAddItem_Click">
                                                        <span class="glyphicon glyphicon-plus" aria-hidden="true" ></span> 
                                                </asp:LinkButton>
                                            </div>
                                            <div class="col-sm-10 padding0">
                                                Add Item
                                            </div>
                                        </div>
                                        <div class="col-sm-6 paddingLeft0 labelText1">
                                            <div class="col-sm-2 padding0">
                                                <asp:LinkButton ID="btnBBConfirm" CausesValidation="false" runat="server" OnClick="btnBBConfirm_Click">
                                                        <span class="glyphicon glyphicon-ok" aria-hidden="true" ></span> 
                                                </asp:LinkButton>
                                            </div>
                                            <div class="col-sm-10 padding0">
                                                Confirm
                                            </div>
                                        </div>
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
                                                                <asp:Label ID="lblBBDescription" runat="server" ForeColor="#A513D0"></asp:Label>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="col-sm-4">
                                                        <div class="row">
                                                            <div class="col-sm-2 labelText1">
                                                                Model:
                                                            </div>
                                                            <div class="col-sm-10" style="margin-top: 3px">
                                                                <asp:Label ID="lblBBModel" runat="server" ForeColor="#A513D0"></asp:Label>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="col-sm-4">
                                                        <div class="row">
                                                            <div class="col-sm-2 labelText1">
                                                                Brand:
                                                            </div>
                                                            <div class="col-sm-10" style="margin-top: 3px">
                                                                <asp:Label ID="lblBBBrand" runat="server" ForeColor="#A513D0"></asp:Label>
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
                                                    <asp:GridView ID="gvAddBuyBack" CausesValidation="false" AutoGenerateColumns="false" runat="server" GridLines="None" CssClass="table table-hover table-striped" PagerStyle-CssClass="cssPager">
                                                        <Columns>
                                                            <asp:TemplateField>
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="btnBBNewItemDelete" CausesValidation="false" CommandName="Delete" runat="server" OnClick="btnBBNewItemDelete_Click">
                                                                 <span class="glyphicon glyphicon-trash" aria-hidden="true"></span>
                                                                    </asp:LinkButton>
                                                                </ItemTemplate>
                                                                <ItemStyle Width="20px" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Item">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblTus_itm_cd" runat="server" Text='<%# Bind("Tus_itm_cd") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle Width="120px" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Description">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblTus_itm_desc" runat="server" Text='<%# Bind("Tus_itm_desc") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle Width="150px" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Model">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblTus_itm_model" runat="server" Text='<%# Bind("Tus_itm_model") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle Width="95px" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Status">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblTus_itm_stus" runat="server" Text='<%# Bind("Tus_itm_stus") %>' Visible="false"></asp:Label>
                                                                    <asp:Label ID="lblTus_itm_stus_desc" runat="server" Text='<%# Bind("Tus_itm_stus_desc") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle Width="95px" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Qty">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblTus_qty" runat="server" Text='<%# Bind("Tus_qty") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle Width="50px" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Serial 1">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblTus_ser_1" runat="server" Text='<%# Bind("Tus_ser_1") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle Width="170px" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Serial 2">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblTus_ser_2" runat="server" Text='<%# Bind("Tus_ser_2") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle Width="170px" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Warranty">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblTus_warr_no" runat="server" Text='<%# Bind("Tus_warr_no") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle Width="170px" />
                                                            </asp:TemplateField>
                                                        </Columns>
                                                        <PagerStyle CssClass="cssPager" />
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
                <div class="panel-heading height30">
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
                        <asp:UpdatePanel runat="server">
                            <ContentTemplate>
                                <uc1:ucCustomer runat="server" ID="ucCustomer" />
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </div>
        </div>
    </asp:Panel>

    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <asp:Button ID="btnhdMultiItems" runat="server" Text="btnhdMultiItems" Style="display: none;" />
            <asp:ModalPopupExtender ID="mpMultipleItems" runat="server" Enabled="True" TargetControlID="btnhdMultiItems"
                PopupControlID="pnlMultipleItems" PopupDragHandleControlID="Div5" Drag="true" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>
        </ContentTemplate>
    </asp:UpdatePanel>

    <asp:Panel runat="server" ID="pnlMultipleItems" Style="display: none">
        <%--  <div runat="server" id="Div5" class="panel panel-primary Mheight">--%>
        <asp:Label ID="Label1" runat="server" Text="Label" Visible="false"></asp:Label>
        <div class="panel panel-default">
            <div class="panel-heading" id="Div5">
                Multiple Combine Items
                    <asp:LinkButton ID="btnMpMuliItlClose" runat="server" Style="float: right">
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
                    <div class="col-sm-12">
                        <asp:ScriptManagerProxy ID="ScriptManagerProxy13" runat="server"></asp:ScriptManagerProxy>
                        <asp:UpdatePanel ID="UpdatePanel12" runat="server">
                            <ContentTemplate>
                                <asp:GridView ID="dgvMultipleItems" AutoGenerateColumns="false" CausesValidation="false" AllowPaging="false" runat="server" GridLines="None" CssClass="table table-hover table-striped" PagerStyle-CssClass="cssPager" OnRowCommand="dgvMultipleItems_RowCommand" OnSelectedIndexChanged="dgvMultipleItems_SelectedIndexChanged">
                                    <Columns>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:LinkButton ID="btnMuliItemSelect" OnClick="btnMuliItemSelect_Click" CausesValidation="false" CommandName="Select" runat="server">
                                                        <span class="glyphicon glyphicon-play-circle" aria-hidden="true"></span>
                                                </asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Item">
                                            <ItemTemplate>
                                                <asp:Label ID="lblITEM" runat="server" Text='<%# Bind("ITEM") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="DESCRIPTION" HeaderText="Description" />
                                        <asp:BoundField DataField="MODEL" HeaderText="Model" />
                                        <asp:BoundField DataField="BRAND" HeaderText="Brand" />
                                    </Columns>
                                    <PagerStyle CssClass="cssPager" />
                                </asp:GridView>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </div>
        </div>
        <%--  </div>--%>
    </asp:Panel>

    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <asp:Button ID="btnConf" runat="server" Text="btnConf" Style="display: none;" />
            <asp:ModalPopupExtender ID="mpConfirmation" runat="server" Enabled="True" TargetControlID="btnConf"
                PopupControlID="pnlConfirmation" PopupDragHandleControlID="divCOnf" Drag="true" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>
        </ContentTemplate>
    </asp:UpdatePanel>

    <asp:Panel runat="server" ID="pnlConfirmation" Style="display: none" Width="400px" Height="115px">
        <div runat="server" id="Div6" class="panel panel-primary">
            <asp:Label ID="Label2" runat="server" Text="Label" Visible="false"></asp:Label>
            <asp:UpdatePanel runat="server">
                <ContentTemplate>
                    <div class="panel panel-default">
                        <div class="panel-heading" id="divCOnf">
                            Confirmation
                    <asp:LinkButton ID="btnConfClose" runat="server" OnClick="btnConfClose_Click">
                             <span class="glyphicon glyphicon-remove" style="float:right" aria-hidden="true">Close</span>
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
                                <div class="col-sm-12">
                                    <asp:Label ID="lblConfText" Text="" runat="server" />
                                    <asp:HiddenField ID="hdfConfItem" runat="server" />
                                    <asp:HiddenField ID="hdfConfStatus" runat="server" />
                                    <asp:HiddenField ID="hdfConf" runat="server" />
                                </div>
                                <div class="col-sm-12">
                                    <div class="col-sm-3">
                                    </div>
                                    <div class="col-sm-3">
                                        <asp:Button ID="btnConfYes" CssClass="form-control" Text="Yes" runat="server" OnClick="btnConfYes_Click" />
                                    </div>
                                    <div class="col-sm-3">
                                    </div>
                                    <div class="col-sm-3">
                                        <asp:Button ID="btnConfNo" CssClass="form-control" Text="No" runat="server" OnClick="btnConfNo_Click" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </asp:Panel>

    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <asp:Button ID="btnPromositon" runat="server" Text="btnConf" Style="display: none;" />
            <asp:ModalPopupExtender ID="mpPriceNPromotion" runat="server" Enabled="True" TargetControlID="btnPromositon"
                PopupControlID="pnlPriceNPromotion">
            </asp:ModalPopupExtender>
        </ContentTemplate>
    </asp:UpdatePanel>

    <asp:ScriptManagerProxy ID="ScriptManagerProxy14" runat="server"></asp:ScriptManagerProxy>
    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <asp:Panel runat="server" ID="pnlPriceNPromotion" Style="display: none" Width="1000px" Height="500px">
                <div runat="server" id="Div7" class="panel panel-primary">
                    <asp:Label ID="Label3" runat="server" Text="Label" Visible="false"></asp:Label>
                    <div class="panel panel-default">
                        <div class="panel-heading" id="divPromhdr">
                            Promotions
                                <asp:LinkButton ID="btnPromoCLose" runat="server" Style="float: right" OnClick="btnPromoCLose_Click">
                                    <span class="glyphicon glyphicon-remove" style="float:right" aria-hidden="true">Close</span>
                                </asp:LinkButton>
                            <div class="col-sm-11">
                            </div>
                            <div class="col-sm-1">
                            </div>
                        </div>
                        <div class="panel-body">
                            <div class="col-sm-12 padding0">
                                <div class="col-sm-7">
                                    <div class="row">
                                        <div class="panel panel-default">
                                            <div class="panel-heading">
                                                Available Normal Price
                                            <div class="col-sm-11">
                                            </div>
                                                <div class="col-sm-1">
                                                </div>
                                            </div>
                                            <div class="panel-body">
                                                <div class="col-sm-12">
                                                    <%--gvNormalPrice--%>
                                                    <asp:Panel runat="server" ScrollBars="Both">
                                                        <asp:GridView ID="gvNormalPrice" AutoGenerateColumns="False" CausesValidation="false" runat="server" GridLines="None" CssClass="table table-hover table-striped" PagerStyle-CssClass="cssPager" ShowHeaderWhenEmpty="True"
                                                            EmptyDataText="No data found...">
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="Select">
                                                                    <ItemTemplate>
                                                                        <asp:CheckBox ID="chkSelectNormalPrice" AutoPostBack="true" runat="server" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:BoundField DataField="Serial" HeaderText="Serial" Visible="false" />
                                                                <asp:BoundField DataField="sapd_itm_cd" HeaderText="Item" />
                                                                <asp:BoundField DataField="Sapd_itm_price" HeaderText="U.Price" DataFormatString="{0:n}" />
                                                                <asp:BoundField DataField="Sapd_circular_no" HeaderText="Circular" />
                                                                <asp:BoundField DataField="PriceType" HeaderText="PriceType" />
                                                                <asp:BoundField DataField="Type" HeaderText="Type" />
                                                                <asp:TemplateField HeaderText="Ref Date">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblValidTill" runat="server" Text='<%# Bind("ValidTill", "{0:dd/MMM/yyyy}") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:BoundField DataField="PbSeq" HeaderText="PbSeq" Visible="false" />
                                                                <asp:BoundField DataField="PbLineSeq" HeaderText="PbLineSeq" Visible="false" />
                                                                <asp:BoundField DataField="Promo.CD" HeaderText="Promo.CD" />
                                                                <asp:BoundField DataField="IsFixQty" HeaderText="IsFixQty" Visible="false" DataFormatString="{0:n}" />
                                                                <asp:BoundField DataField="BkpUPrice" HeaderText="BkpUPrice" Visible="false" DataFormatString="{0:n}" />
                                                                <asp:BoundField DataField="Sapd_warr_remarks" HeaderText="WarrantyRmk" Visible="false" />
                                                                <asp:BoundField DataField="Book" HeaderText="Book" Visible="false" />
                                                                <asp:BoundField DataField="Level" HeaderText="Level" Visible="false" />
                                                            </Columns>
                                                            <PagerStyle CssClass="cssPager" />
                                                        </asp:GridView>
                                                    </asp:Panel>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="panel panel-default">
                                            <div class="panel-heading">
                                                Available Promotions
                                            <div class="col-sm-11">
                                            </div>
                                                <div class="col-sm-1">
                                                </div>
                                            </div>
                                            <div class="panel-body">
                                                <div class="row">
                                                    <div class="col-sm-12 ">
                                                        <%--gvPromotionPrice--%>
                                                        <asp:ScriptManagerProxy ID="ScriptManagerProxy12" runat="server"></asp:ScriptManagerProxy>
                                                        <asp:UpdatePanel runat="server">
                                                            <ContentTemplate>
                                                                <div class="panel-body  panelscollbar height100">
                                                                    <asp:GridView ID="gvPromotionPrice" AutoGenerateColumns="False" CausesValidation="false" runat="server" GridLines="None" CssClass="table table-hover table-striped" PagerStyle-CssClass="cssPager" ShowHeaderWhenEmpty="True"
                                                                        EmptyDataText="No data found...">
                                                                        <Columns>
                                                                            <asp:TemplateField HeaderText="Select">
                                                                                <ItemTemplate>
                                                                                    <asp:CheckBox ID="chkSelectPromPrice" AutoPostBack="true" OnCheckedChanged="chkSelectPromPrice_CheckedChanged" runat="server" Enabled="false" />
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Item">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblSapd_itm_cd" runat="server" Text='<%# Bind("Sapd_itm_cd") %>' Visible="false"></asp:Label>
                                                                                    <asp:LinkButton ID="btnSapd_itm_cd" Text='<%# Bind("Sapd_itm_cd") %>' runat="server" OnClick="btnSapd_itm_cd_Click" />
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="U.Price">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblSapd_itm_price" runat="server" Text='<%# Bind("Sapd_itm_price","{0:n}") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Circular">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblSapd_circular_no" runat="server" Text='<%# Bind("Sapd_circular_no") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="PriceType">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblSapd_price_type" runat="server" Text='<%# Bind("Sapd_price_type") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Type">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="Label5" runat="server" Text='<%# Bind("Sarpt_cd") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Valid Till">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblSapd_to_date" runat="server" Text='<%# Bind("Sapd_to_date", "{0:dd/MMM/yyyy}") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="PbSeq" Visible="false">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblPbSeq" runat="server" Text='<%# Bind("Sapd_pb_seq") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="sapd_seq_no" Visible="false">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblsapd_seq_no" runat="server" Text='<%# Bind("sapd_seq_no") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="PbLineSeq" Visible="false">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblPbLineSeq" runat="server" Text='<%# Bind("sapd_seq_no") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <%--<asp:BoundField DataField="PbLineSeq" HeaderText="PbLineSeq" Visible="false" />
                                                                                <asp:BoundField DataField="Sapd_promo_cd" HeaderText="Promo.CD" />--%>
                                                                            <asp:TemplateField HeaderText="Promo.CD">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblSapd_promo_cd" runat="server" Text='<%# Bind("Sapd_promo_cd") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="IsFixQty" Visible="False">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblIsFixQty" runat="server" Text='<%# Bind("sapd_is_fix_qty") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="BkpUPrice" Visible="False">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblBkpUPrice" runat="server" Text='<%# Bind("sapd_cre_by","{0:n}") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="WarrantyRmk" Visible="False">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblSapd_warr_remarks" runat="server" Text='<%# Bind("Sapd_warr_remarks") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Book" Visible="False">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblBook" runat="server" Text='<%# Bind("sapd_pb_tp_cd") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Level" Visible="False">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblLevel" runat="server" Text='<%# Bind("sapd_pbk_lvl_cd") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="from" Visible="False">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblsapd_qty_from" runat="server" Text='<%# Bind("sapd_qty_from") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="To" Visible="False">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblsapd_qty_to" runat="server" Text='<%# Bind("sapd_qty_to") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                        </Columns>
                                                                        <PagerStyle CssClass="cssPager" />
                                                                    </asp:GridView>
                                                                </div>
                                                            </ContentTemplate>
                                                        </asp:UpdatePanel>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="panel panel-default">
                                            <div class="panel-heading">
                                                Promotion Items
                                            <div class="col-sm-11">
                                            </div>
                                                <div class="col-sm-1">
                                                </div>
                                            </div>
                                            <div class="panel-body  panelscollbar height80">
                                                <div class="row">
                                                    <div class="col-sm-12">
                                                        <%--gvPromotionItem--%>
                                                        <%-- <asp:Panel runat="server" ScrollBars="Both">--%>
                                                        <asp:GridView ID="gvPromotionItem" AutoGenerateColumns="False" CausesValidation="false" runat="server" GridLines="None" CssClass="table table-hover table-striped" PagerStyle-CssClass="cssPager" ShowHeaderWhenEmpty="True"
                                                            EmptyDataText="No data found..." OnRowDataBound="gvPromotionItem_RowDataBound">
                                                            <Columns>
                                                                <asp:TemplateField>
                                                                    <ItemTemplate>
                                                                        <asp:LinkButton ID="btnPromotionItemSimiler" CausesValidation="false" Visible="false" CommandName="Select" OnClick="btnPromotionItemSimiler_Click" CommandArgument="<%# Container.DataItemIndex %>" runat="server">
                                                                                <span class="glyphicon glyphicon-tasks" aria-hidden="true"></span>
                                                                        </asp:LinkButton>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField Visible="false">
                                                                    <ItemTemplate>
                                                                        <asp:LinkButton ID="btnPromotionItemSelect" CausesValidation="false" OnClick="btnPromotionItemSelect_click" CommandName="Select" CommandArgument="<%# Container.DataItemIndex %>" runat="server">
                                                                                <span class="glyphicon glyphicon-play-circle" aria-hidden="true"></span>
                                                                        </asp:LinkButton>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="No">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblsapc_itm_line" runat="server" Text='<%# Bind("sapc_itm_line") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Item">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblsapc_itm_cd" runat="server" Text='<%# Bind("sapc_itm_cd") %>' Visible="false"></asp:Label>
                                                                        <asp:LinkButton ID="btnsapc_itm_cd" Text='<%# Bind("sapc_itm_cd") %>' runat="server" OnClick="btnsapc_itm_cd_Click" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Similar Item">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblSimiler_item" runat="server" Text='<%# Bind("Similer_item") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Description">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblmi_longdesc" runat="server" Text='<%# Bind("mi_longdesc") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Model">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblmi_model" runat="server" Text='<%# Bind("mi_model") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Serial" Visible="false">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblsapc_sub_ser" runat="server" Text='<%# Bind("sapc_sub_ser") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Status">
                                                                    <ItemTemplate>
                                                                        <asp:DropDownList ID="PromItm_Status" AutoPostBack="true" OnSelectedIndexChanged="PromItm_Status_SelectedIndexChanged" CssClass="form-control" runat="server"></asp:DropDownList>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Qty">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblsapc_qty" runat="server" Text='<%# Bind("sapc_qty","{0:n}") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Unit Price">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblsapc_price" runat="server" Text='<%# Bind("sapc_price","{0:n}") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="PB Seq" Visible="false">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblsapc_pb_seq" runat="server" Text='<%# Bind("sapc_pb_seq") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="PB Line Seq" Visible="false">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblsapc_main_line" runat="server" Text='<%# Bind("sapc_main_line") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Main Item" Visible="false">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblsapc_main_itm_cd" runat="server" Text='<%# Bind("sapc_main_itm_cd") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Main Item Serial" Visible="false">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblsapc_main_ser" runat="server" Text='<%# Bind("sapc_main_ser") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <%--           <asp:TemplateField HeaderText="PromItm_BkpUnitPrice" Visible="false">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblsapc_price" runat="server" Text='<%# Bind("sapc_price") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>--%>
                                                                <asp:TemplateField HeaderText="PromItm_increse" Visible="false">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblsapc_increse" runat="server" Text='<%# Bind("sapc_increse") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                            </Columns>
                                                            <PagerStyle CssClass="cssPager" />
                                                        </asp:GridView>
                                                        <%-- </asp:Panel>--%>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-sm-2">
                                            Availability
                                        </div>
                                        <div class="col-sm-2">
                                            <asp:Label Text="0.00" ID="lblPriNProAvailableQty" ForeColor="#A513D0" runat="server" />
                                        </div>
                                        <div class="col-sm-2">
                                            Status
                                        </div>
                                        <div class="col-sm-2">
                                            <asp:Label Text="0.00" ID="lblPriNProAvailableStatusQty" ForeColor="#A513D0" runat="server" />
                                        </div>
                                        <div class="col-sm-2">
                                            <asp:Button ID="btnPriNProConfirm" OnClick="btnPromConfirm_Click" Text="Confirm" runat="server" />
                                        </div>
                                        <div class="col-sm-2">
                                            <asp:Button ID="btnPriNProCancel" Text="Cancel" OnClick="btnPriNProCancel_Click" runat="server" />
                                        </div>
                                    </div>
                                </div>
                                <div class="col-sm-5">
                                    <div class="row">
                                        <div class="panel panel-default">
                                            <div class="panel-heading">
                                                Pick serial for promotion
                                            <div class="col-sm-11">
                                            </div>
                                                <div class="col-sm-1">
                                                </div>
                                            </div>
                                            <div class="panel-body">
                                                <div class="row">
                                                    <div class="col-sm-12">
                                                        <asp:Panel runat="server" ScrollBars="Both" Height="400px">
                                                            <asp:GridView ID="gvPromotionSerial" AutoGenerateColumns="False" CausesValidation="false" runat="server" GridLines="None" CssClass="table table-hover table-striped" PagerStyle-CssClass="cssPager" ShowHeaderWhenEmpty="True"
                                                                EmptyDataText="No data found...">
                                                                <Columns>
                                                                    <asp:TemplateField HeaderText="Select">
                                                                        <ItemTemplate>
                                                                            <asp:CheckBox ID="chkSelectPromSerial" AutoPostBack="true" runat="server" />
                                                                        </ItemTemplate>
                                                                        <ItemStyle Width="10%" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Item">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblTus_itm_cd" runat="server" Text='<%# Bind("Tus_itm_cd") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <ItemStyle Width="10%" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Serial 1">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblTus_ser_1" runat="server" Text='<%# Bind("Tus_ser_1") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <ItemStyle Width="10%" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Serial 2">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblTus_ser_2" runat="server" Text='<%# Bind("Tus_ser_2") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <ItemStyle Width="10%" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Warranty">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lbltus_warr_no" runat="server" Text='<%# Bind("tus_warr_no") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <ItemStyle Width="10%" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Status">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblTus_itm_stus" runat="server" Text='<%# Bind("Tus_itm_stus") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <ItemStyle Width="10%" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Serial ID">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblTus_ser_id" runat="server" Text='<%# Bind("Tus_ser_id") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <ItemStyle Width="10%" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Serial 3">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblTus_ser_3" runat="server" Text='<%# Bind("Tus_ser_3") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <ItemStyle Width="10%" />
                                                                    </asp:TemplateField>
                                                                </Columns>
                                                                <PagerStyle CssClass="cssPager" />
                                                            </asp:GridView>
                                                        </asp:Panel>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-sm-2">
                                                        Availability
                                                    </div>
                                                    <div class="col-sm-6">
                                                        <div class="col-sm-10 paddingLeft0 paddingRight0">
                                                            <asp:TextBox ID="txtPriNProSerialSearch" runat="server" CssClass="form-control" AutoPostBack="true" OnTextChanged="txtPriNProSerialSearch_TextChanged" />
                                                        </div>
                                                        <div class="col-sm-2  paddingLeft0 paddingRight0">
                                                            <asp:LinkButton ID="btnSearchFreSerials" runat="server"  CausesValidation="false" OnClick="btnSearchFreSerials_Click">
                                                        <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                            </asp:LinkButton>
                                                        </div>
                                                    </div>
                                                    <div class="col-sm-2">
                                                        <asp:Button ID="btnPriNProSerClear" Text="Clear" runat="server" OnClick="btnPriNProSerClear_Click" />
                                                    </div>
                                                    <div class="col-sm-2">
                                                        <asp:Button ID="btnPriNProSerConfirm" Text="Confirm" OnClick="btnPriNProSerConfirm_Click" runat="server" />
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
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>

    <asp:Panel runat="server" Visible="false">
    </asp:Panel>



    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <asp:Button ID="btnGruop" runat="server" Text="btnGruop" Style="display: none;" />
            <asp:ModalPopupExtender ID="mpGroup" runat="server" Enabled="True" TargetControlID="btnGruop"
                PopupControlID="pnlGroup" PopupDragHandleControlID="divGrop" Drag="true">
            </asp:ModalPopupExtender>
        </ContentTemplate>
    </asp:UpdatePanel>

    <asp:Panel runat="server" ID="pnlGroup" Style="display: none" Width="400px" Height="115px">
        <div runat="server" id="Div8" class="panel panel-primary">
            <asp:Label ID="Label4" runat="server" Text="Label" Visible="false"></asp:Label>
            <asp:UpdatePanel runat="server">
                <ContentTemplate>
                    <div class="panel panel-default">
                        <div class="panel-heading" id="divGrop">
                            Group Sales
                    <asp:LinkButton ID="btnCloseGroup" runat="server" OnClick="btnCloseGroup_Click">
                             <span class="glyphicon glyphicon-remove" style="float:right" aria-hidden="true">Close</span>
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
                                <div class="col-sm-4 labelText1">
                                    Select
                                </div>
                                <div class="col-sm-7 paddingRight5">
                                    <asp:TextBox ID="txtGroup" runat="server" AutoPostBack="true" Style="text-transform: uppercase" OnTextChanged="txtGroup_TextChanged" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="col-sm-1 paddingLeft0">
                                    <asp:LinkButton ID="btnGroupSearch" runat="server" OnClick="btnGroupSearch_Click" TabIndex="26" CausesValidation="false">
                                    <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                    </asp:LinkButton>
                                </div>
                            </div>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </asp:Panel>

    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <asp:Button ID="btnDiscountConf" runat="server" Text="btnConf" Style="display: none;" />
            <asp:ModalPopupExtender ID="mpConfDiscount" runat="server" Enabled="True" TargetControlID="btnDiscountConf"
                PopupControlID="pblConfDiscount" PopupDragHandleControlID="divCOnfDiscount" Drag="true" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>
        </ContentTemplate>
    </asp:UpdatePanel>

    <asp:Panel runat="server" ID="pblConfDiscount" Style="display: none" Width="400px" Height="115px">
        <div runat="server" id="Div9" class="panel panel-primary">
            <asp:Label ID="Label11" runat="server" Text="Label" Visible="false"></asp:Label>
            <asp:UpdatePanel runat="server">
                <ContentTemplate>
                    <div class="panel panel-default">
                        <div class="panel-heading" id="divCOnfDiscount">
                            Confirmation
                    <asp:LinkButton ID="btnConfDiscClose" runat="server" OnClick="btnConfDiscClose_Click">
                             <span class="glyphicon glyphicon-remove" style="float:right" aria-hidden="true">Close</span>
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
                                <div class="col-sm-12">
                                    <asp:Label ID="lblConfDiscount" Text="" runat="server" />
                                    <asp:HiddenField ID="HiddenField1" runat="server" />
                                    <asp:HiddenField ID="HiddenField2" runat="server" />
                                    <asp:HiddenField ID="HiddenField3" runat="server" />
                                </div>
                                <div class="col-sm-12">
                                    <div class="col-sm-3">
                                    </div>
                                    <div class="col-sm-3">
                                        <asp:Button ID="btnConfDisYes" Text="Yes" runat="server" OnClick="btnConfDisYes_Click" />
                                    </div>
                                    <div class="col-sm-3">
                                    </div>
                                    <div class="col-sm-3">
                                        <asp:Button ID="btnConfDisNo" Text="No" runat="server" OnClick="btnConfDisNo_Click" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </asp:Panel>

    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <asp:Button ID="btnaddnewiemcom" runat="server" Text="btnConf" Style="display: none;" />
            <asp:ModalPopupExtender ID="mpAddNewItem" runat="server" Enabled="True" TargetControlID="btnaddnewiemcom"
                PopupControlID="pnlNewItemAdd" PopupDragHandleControlID="divCOnfAddNewItem" Drag="true" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>
        </ContentTemplate>
    </asp:UpdatePanel>

    <asp:Panel runat="server" ID="pnlNewItemAdd" Style="display: none" Width="400px" Height="115px">
        <div runat="server" id="Div3" class="panel panel-primary">
            <asp:UpdatePanel runat="server">
                <ContentTemplate>
                    <div class="panel panel-default">
                        <div class="panel-heading" id="divCOnfAddNewItem">
                            Confirmation
                    <asp:LinkButton ID="btnClosenewitm" runat="server" OnClick="btnClosenewitm_Click">
                             <span class="glyphicon glyphicon-remove" style="float:right" aria-hidden="true">Close</span>
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
                                <div class="col-sm-12">
                                    Do you want to add another item?
                                </div>
                                <div class="col-sm-12">
                                    <div class="col-sm-3">
                                    </div>
                                    <div class="col-sm-3">
                                        <asp:Button ID="btnAddnewYes" CssClass="form-control" Text="Yes" runat="server" OnClick="ItemFocus_Click" />
                                    </div>
                                    <div class="col-sm-3">
                                    </div>
                                    <div class="col-sm-3">
                                        <asp:Button ID="btnAddnewNO" CssClass="form-control" Text="No" runat="server" OnClick="btnAddnewNO_Click" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </asp:Panel>

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
            <asp:Button ID="btnconfsave2" runat="server" Text="btnConf" Style="display: none;" />
            <asp:ModalPopupExtender ID="mpConfSaveDisco" runat="server" Enabled="True" TargetControlID="btnconfsave2"
                PopupControlID="pnlConfSaveProm" PopupDragHandleControlID="DivpnlConfSaveProm" Drag="true" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>
        </ContentTemplate>
    </asp:UpdatePanel>

    <asp:Panel runat="server" ID="pnlConfSaveProm" Style="display: none" Width="400px" Height="115px">
        <div runat="server" id="Div2" class="panel panel-primary">
            <asp:UpdatePanel runat="server" ID="upsaveConf2">
                <ContentTemplate>
                    <div class="panel panel-default">
                        <div class="panel-heading" id="DivpnlConfSaveProm">
                            Confirmation
                    <asp:LinkButton ID="btnconfsaveclose" runat="server" OnClick="btnconfsaveclose_Click">
                             <span class="glyphicon glyphicon-remove" style="float:right" aria-hidden="true">Close</span>
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
                                <div class="col-sm-12">
                                    There is no specific discount promotion available. Do you want to save?
                                </div>
                                <div class="col-sm-12">
                                    <div class="col-sm-3">
                                    </div>
                                    <div class="col-sm-3">
                                        <asp:Button ID="btnYesConfSavePromo" CssClass="form-control" Text="Yes" runat="server" OnClick="btnYesConfSavePromo_Click" />
                                    </div>
                                    <div class="col-sm-3">
                                    </div>
                                    <div class="col-sm-3">
                                        <asp:Button ID="btnNoConfSavePromo" CssClass="form-control" Text="No" runat="server" OnClick="btnNoConfSavePromo_Click" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </asp:Panel>

    <%-- <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <asp:Button ID="btnaddnewcong" runat="server" Text="btnConf" Style="display: none;" />
            <asp:ModalPopupExtender ID="mpAddConf" runat="server" Enabled="True" TargetControlID="btnaddnewcong"
                PopupControlID="pnladdConf" PopupDragHandleControlID="divCOnfaddnew" Drag="true" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>
        </ContentTemplate>
    </asp:UpdatePanel>

    <asp:Panel runat="server" ID="pnladdConf" Style="display: none" Width="400px" Height="115px">
        <div runat="server" id="Div2" class="panel panel-primary">
            <asp:Label ID="Label8" runat="server" Text="Label" Visible="false"></asp:Label>
            <asp:UpdatePanel runat="server">
                <ContentTemplate>
                    <div class="panel panel-default">
                        <div class="panel-heading" id="divCOnfaddnew">
                            Confirmation
                            <div class="col-sm-11">
                            </div>
                            <div class="col-sm-1">
                            </div>
                        </div>
                        <div class="panel-body">
                            <div class="row">
                                <div class="col-sm-12">
                                    Do you want to add another item?
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-12">
                                </div>
                                <div class="col-sm-12">
                                    <div class="col-sm-3">
                                    </div>
                                    <div class="col-sm-3">
                                        <asp:Button ID="btnaddnewYes" CssClass="form-control" Text="Yes" runat="server" OnClick="btnaddnewYes_Click1" />
                                    </div>
                                    <div class="col-sm-3">
                                    </div>
                                    <div class="col-sm-3">
                                        <asp:Button ID="btnaddnewNNo" CssClass="form-control" Text="No" runat="server" OnClick="btnaddnewNNo_Click" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </asp:Panel>--%>

    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <asp:Button ID="btnBasedonadvre" runat="server" Text="btnConf" Style="display: none;" />
            <asp:ModalPopupExtender ID="mpAdavce" runat="server" Enabled="True" TargetControlID="btnBasedonadvre"
                PopupControlID="pnlADVR" PopupDragHandleControlID="DivpnlADVR" Drag="true" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>
        </ContentTemplate>
    </asp:UpdatePanel>

    <asp:Panel runat="server" ID="pnlADVR" Style="display: none" Width="400px" Height="238px">
        <div runat="server" id="Div12" class="panel panel-primary">
            <asp:UpdatePanel runat="server" ID="UpdatePanel8">
                <ContentTemplate>
                    <div class="panel panel-default">
                        <div class="panel-heading height30" id="DivpnlADVR">
                            <div class="col-sm-11">
                                Search Advance Receipt
                            </div>
                            <div class="col-sm-1">
                                <asp:LinkButton ID="btnCloseAdvcRe" runat="server" OnClick="btnCloseAdvcRe_Click">
                             <span class="glyphicon glyphicon-remove" style="float:right" aria-hidden="true">Close</span>
                                </asp:LinkButton>
                            </div>
                        </div>
                        <div class="panel-body">
                            <div class="row">
                                <div class="col-sm-12">
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-12">
                                    <asp:UpdatePanel ID="upAdvanceReBasesd" runat="server">
                                        <ContentTemplate>
                                            <div class="col-sm-8">
                                                <asp:TextBox ID="txtADVRNumber" runat="server" CssClass="form-control" AutoPostBack="true" OnTextChanged="txtADVRNumber_TextChanged"></asp:TextBox>
                                            </div>
                                            <div class="col-sm-1 paddingLeft0">
                                                <asp:LinkButton ID="btnSearchADVR" runat="server" TabIndex="5" CausesValidation="false" OnClick="btnSearchADVR_Click">
                                                        <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                </asp:LinkButton>
                                            </div>
                                            <div class="col-sm-3 paddingLeft0">
                                                <asp:Button ID="btnLoadADV" Text="Load" runat="server" OnClick="btnLoadADV_Click" />
                                            </div>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                                <div class="col-sm-12">
                                    <asp:GridView ID="dgvReceiptItems" AutoGenerateColumns="false" CausesValidation="false" runat="server" GridLines="None" CssClass="table table-hover table-striped">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Receipt" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblsari_rec_no" runat="server" Text='<%# Bind("sari_rec_no") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Item">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblsari_item" runat="server" Text='<%# Bind("sari_item") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Description" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblsari_item_desc" runat="server" Text='<%# Bind("sari_item_desc") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Model" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblsari_model" runat="server" Text='<%# Bind("sari_model") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Serial">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblsari_serial" runat="server" Text='<%# Bind("sari_serial") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Qty">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblsari_qty" runat="server" Text='<%# Bind("sari_qty") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Price Book" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblsari_pb" runat="server" Text='<%# Bind("sari_pb") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Level" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblsari_pb_lvl" runat="server" Text='<%# Bind("sari_pb_lvl") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </asp:Panel>

    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <asp:Button ID="btnSilItmswt" runat="server" Text="btnConf" Style="display: none;" />
            <asp:ModalPopupExtender ID="mpSimilrItmes" runat="server" Enabled="True" TargetControlID="btnSilItmswt"
                PopupControlID="pnlSimilerItems" PopupDragHandleControlID="divpnlSimilerItems" Drag="true" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>
        </ContentTemplate>
    </asp:UpdatePanel>

    <asp:Panel runat="server" ID="pnlSimilerItems" Style="display: none" Width="400px" Height="115px">
        <div runat="server" id="Div13" class="panel panel-primary">
            <asp:UpdatePanel runat="server" ID="UpdatePanel11">
                <ContentTemplate>
                    <asp:HiddenField ID="hdfOriginalItem" runat="server" />
                    <div class="panel panel-default">
                        <div class="panel-heading" id="divpnlSimilerItems">
                            Pick Similar Item
                                <asp:LinkButton ID="btnCloseSimiler" runat="server" OnClick="btnCloseSimiler_Click">
                                    <span class="glyphicon glyphicon-remove" style="float:right" aria-hidden="true">Close</span>
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
                                <div class="col-sm-12">
                                    <asp:GridView ID="dgvSimilerItemPick" AutoGenerateColumns="false" runat="server" CssClass="table table-hover table-striped" GridLines="None" ShowHeaderWhenEmpty="True" EmptyDataText="No data found...">
                                        <Columns>
                                            <asp:BoundField DataField="MISI_SEQ_NO" HeaderText="Item" Visible="false" />
                                            <asp:BoundField DataField="MISI_TP" HeaderText="Item" Visible="false" />
                                            <asp:BoundField DataField="MISI_COM" HeaderText="Item" Visible="false" />
                                            <asp:BoundField DataField="MISI_ITM_CD" HeaderText="Item" Visible="false" />
                                            <asp:TemplateField HeaderText="Item Code">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblMISI_SIM_ITM_CD" runat="server" Text='<%# Bind("MISI_SIM_ITM_CD") %>' Visible="false"></asp:Label>
                                                    <asp:LinkButton ID="btnMISI_SIM_ITM_CD" Text='<%# Bind("MISI_SIM_ITM_CD") %>' runat="server" OnClick="btnMISI_SIM_ITM_CD_Click" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="MI_LONGDESC" HeaderText="Item" />
                                            <asp:BoundField DataField="MI_MODEL" HeaderText="Model" />
                                            <asp:BoundField DataField="MI_BRAND" HeaderText="Brand" />
                                            <asp:BoundField DataField="MISI_FROM" HeaderText="Item" Visible="false" />
                                            <asp:BoundField DataField="MISI_TO" HeaderText="Item" Visible="false" />
                                            <asp:BoundField DataField="MISI_PC" HeaderText="Item" Visible="false" />
                                            <asp:BoundField DataField="MISI_LOC" HeaderText="Item" Visible="false" />
                                            <asp:BoundField DataField="MISI_DOC_NO" HeaderText="Item" Visible="false" />
                                            <asp:BoundField DataField="MISI_PROMO" HeaderText="Item" Visible="false" />
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </asp:Panel>

    <asp:UpdatePanel ID="UpdatePanel14" runat="server">
        <ContentTemplate>
            <asp:Button ID="btnSerialPick" runat="server" Text="Button" Style="display: none;" />
            <asp:ModalPopupExtender ID="mpPickSerial" runat="server" Enabled="True" TargetControlID="btnSerialPick"
                PopupControlID="pnlPickSerial" PopupDragHandleControlID="divPSPHdr" Drag="true" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>
        </ContentTemplate>
    </asp:UpdatePanel>

    <asp:UpdatePanel ID="UpdatePanel18" runat="server">
        <ContentTemplate>
            <asp:Panel runat="server" ID="pnlPickSerial" Style="display: none">
                <div runat="server" id="Div14" class="panel panel-default height400 width850">
                    <div class="panel panel-default">
                        <div class="panel-heading" id="divPSPHdr">
                            <asp:LinkButton ID="btnPSPClose" runat="server" OnClick="btnPSPClose_Click">
                             <span class="glyphicon glyphicon-remove" aria-hidden="true">Close</span>
                            </asp:LinkButton>
                            <div class="col-sm-11">
                            </div>
                            <div class="col-sm-1">
                            </div>
                        </div>
                        <div class="panel-body">
                            <div class="col-sm-12" id="Div15" runat="server">
                                <div class="row">
                                    <div class="col-sm-12">
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-12 height5">
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-10" id="Div16" runat="server">
                                        <div class="col-sm-2 labelText1">
                                            Search by key
                                        </div>
                                        <asp:ScriptManagerProxy ID="ScriptManagerProxy11" runat="server"></asp:ScriptManagerProxy>
                                        <asp:UpdatePanel ID="UpdatePanel15" runat="server">
                                            <ContentTemplate>
                                                <div class="col-sm-3 paddingRight5">
                                                    <asp:DropDownList ID="ddlSearchbykeyA" runat="server" class="form-control">
                                                        <asp:ListItem Text="Serial 1" />
                                                        <asp:ListItem Text="Serial 2" />
                                                    </asp:DropDownList>
                                                </div>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                        <div class="col-sm-2 labelText1">
                                            Search by word
                                        </div>
                                        <div class="col-sm-4 paddingRight5">
                                            <asp:TextBox ID="txtSearchbywordA" CausesValidation="false" placeholder="Search by word" class="form-control" AutoPostBack="true" runat="server" OnTextChanged="txtSearchbywordA_TextChanged"></asp:TextBox>
                                        </div>
                                        <asp:ScriptManagerProxy ID="ScriptManagerProxy15" runat="server"></asp:ScriptManagerProxy>
                                        <asp:UpdatePanel ID="UpdatePanel16" runat="server">
                                            <ContentTemplate>
                                                <div class="col-sm-1 paddingLeft0">
                                                    <asp:LinkButton ID="lbtnSearchA" runat="server" OnClick="lbtnSearchA_Click">
                                            <span class="glyphicon glyphicon-search" aria-hidden="true"  ></span>
                                                    </asp:LinkButton>
                                                </div>
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:AsyncPostBackTrigger ControlID="txtSearchbywordA" />
                                            </Triggers>
                                        </asp:UpdatePanel>
                                    </div>
                                    <div class="col-sm-2 padding0">
                                        <asp:Button ID="btnAdvanceAddItem" Visible="false" runat="server" CssClass="btn btn-primary btn-xs" Text="Add" OnClick="btnAdvanceAddItem_Click" />
                                        <asp:LinkButton ID="btnAdvanceAddItemNew" runat="server" OnClick="btnAdvanceAddItem_Click">
                                            <span class="glyphicon glyphicon-plus" aria-hidden="true"  ></span>
                                        </asp:LinkButton>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-12 height5">
                                    </div>
                                </div>
                                <div class="row">
                                    <asp:Label ID="lblPopupItemCode" Text="" runat="server" Visible="false" />
                                    <asp:Label ID="lblInvoiceLine" Text="" runat="server" Visible="false" />
                                    <asp:Label ID="lblItemStatusSer" Text="" runat="server" Visible="false" />
                                    <div class="col-sm-2">
                                        <asp:TextBox runat="server" ID="txtPopupQty" Text="0.00" CssClass="form-control" Visible="false" />
                                    </div>
                                    <div class="col-sm-2">
                                        <asp:Label Text="0.00" ID="lblPopupQty" runat="server" Visible="false" />
                                    </div>
                                    <div class="col-sm-2">
                                        <asp:Label Text="0.00" ID="lblScanQty" runat="server" Visible="false" />
                                    </div>
                                    <div class="col-sm-2">
                                        <asp:Label Text="0.00" ID="lblApprovedQty" runat="server" Visible="false" />
                                    </div>
                                    <div class="col-sm-5">
                                    </div>
                                    <div class="col-sm-1" style="float: right">
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-12 height5">
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-12">
                                        <asp:ScriptManagerProxy ID="ScriptManagerProxy16" runat="server"></asp:ScriptManagerProxy>
                                        <asp:UpdatePanel ID="UpdatePanel17" runat="server">
                                            <ContentTemplate>
                                                <asp:GridView ID="grdAdSearch" AutoGenerateColumns="False" CausesValidation="false" EmptyDataText="No data found..." ShowHeaderWhenEmpty="True"
                                                    runat="server"
                                                    GridLines="None" CssClass="table table-hover table-striped" PagerStyle-CssClass="cssPager" AllowPaging="True" PageSize="7"
                                                    OnSelectedIndexChanged="grdAdSearch_SelectedIndexChanged" OnPageIndexChanging="grdAdSearch_PageIndexChanging">
                                                    <Columns>
                                                        <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                                            <ItemTemplate>
                                                                <asp:CheckBox ID="selectchk" runat="server" Width="5px"></asp:CheckBox>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Center" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Item">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblTus_itmPickSer" runat="server" Text='<%# Bind("Tus_itm_Cd") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Description">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblTus_itm_desc" runat="server" Text='<%# Bind("Tus_itm_desc") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Serial 1">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblTus_ser_1" runat="server" Text='<%# Bind("Tus_ser_1") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Serial 2">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblTus_ser_2" runat="server" Text='<%# Bind("Tus_ser_2") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Warranty">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblTus_warr_no" runat="server" Text='<%# Bind("Tus_warr_no") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Item Status">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblTus_itm_stus" runat="server" Text='<%# Bind("Tus_itm_stus") %>' Visible="false"></asp:Label>
                                                                <asp:Label ID="lblTus_itm_stus_descSER" runat="server" Text='<%# Bind("Tus_itm_stus_desc") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Serial ID">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblTus_ser_id" runat="server" Text='<%# Bind("Tus_ser_id") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="BIN">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblTus_bin" runat="server" Text='<%# Bind("Tus_bin") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Doc Date" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbltus_doc_dt" runat="server" Text='<%# Bind("tus_doc_dt","{0:dd/MMM/yyyy}") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Supplier" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbltus_exist_supp" runat="server" Text='<%# Bind("tus_exist_supp") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                    <PagerStyle CssClass="cssPager" />
                                                </asp:GridView>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
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
            <asp:Button ID="btnMpdiscoutInv" runat="server" Text="Button" Style="display: none;" />
            <asp:ModalPopupExtender ID="mpDiscountRate" runat="server" Enabled="True" TargetControlID="btnMpdiscoutInv"
                PopupControlID="pnlDiscountRate" PopupDragHandleControlID="DivpnlDiscountRate" Drag="true" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>
        </ContentTemplate>
    </asp:UpdatePanel>

    <asp:Panel runat="server" ID="pnlDiscountRate">
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
            <asp:Button ID="tbnSaveMPosd" runat="server" Text="btnConf" Style="display: none;" />
            <asp:ModalPopupExtender ID="mpSavePO" runat="server" Enabled="True" TargetControlID="tbnSaveMPosd"
                PopupControlID="pnlSavePO" PopupDragHandleControlID="divSavPO" Drag="true" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>
        </ContentTemplate>
    </asp:UpdatePanel>

    <asp:Panel runat="server" ID="pnlSavePO" Style="display: none" Width="400px" Height="115px">
        <div runat="server" id="Div17" class="panel panel-primary">
            <asp:UpdatePanel runat="server">
                <ContentTemplate>
                    <div class="panel panel-default">
                        <div class="panel-heading" id="divSavPO">
                            Confirmation
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
                                <div class="col-sm-12">
                                    Do you want to save purchase order along with the invoice?
                                </div>
                                <div class="col-sm-12">
                                    <div class="col-sm-3">
                                    </div>
                                    <div class="col-sm-3">
                                        <asp:Button ID="btnSavePOYes" CssClass="form-control" Text="Yes" runat="server" OnClick="btnSavePOYes_Click" />
                                    </div>
                                    <div class="col-sm-3">
                                    </div>
                                    <div class="col-sm-3">
                                        <asp:Button ID="btnSavePONo" CssClass="form-control" Text="No" runat="server" OnClick="btnSavePONo_Click" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </asp:Panel>

    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <asp:Button ID="btnbaseodsalesOr" runat="server" Text="btnConf" Style="display: none;" />
            <asp:ModalPopupExtender ID="mpLoadSalesOrder" runat="server" Enabled="True" TargetControlID="btnbaseodsalesOr"
                PopupControlID="pnlLoadSalesOrder" PopupDragHandleControlID="DivpnlLoadSalesOrder" Drag="true" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>
        </ContentTemplate>
    </asp:UpdatePanel>

    <asp:Panel runat="server" ID="pnlLoadSalesOrder" Style="display: none" Width="400px" Height="238px">
        <div runat="server" id="Div18" class="panel panel-primary">
            <asp:UpdatePanel runat="server" ID="UpdatePanel13">
                <ContentTemplate>
                    <div class="panel panel-default">
                        <div class="panel-heading height30" id="DivpnlLoadSalesOrder">
                            <div class="col-sm-11">
                                Search Sales Order
                            </div>
                            <div class="col-sm-1">
                                <asp:LinkButton ID="btnCloseSalesorder" runat="server" OnClick="btnCloseSalesorder_Click">
                             <span class="glyphicon glyphicon-remove" style="float:right" aria-hidden="true">Close</span>
                                </asp:LinkButton>
                            </div>
                        </div>
                        <div class="panel-body">
                            <div class="row">
                                <div class="col-sm-12">
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-12">
                                    <asp:UpdatePanel ID="UpdatePanel19" runat="server">
                                        <ContentTemplate>
                                            <div class="col-sm-8">
                                                <asp:TextBox ID="txtSalesOrderSearch" runat="server" CssClass="form-control" OnTextChanged="txtSalesOrderSearch_TextChanged"></asp:TextBox>
                                            </div>
                                            <div class="col-sm-1 paddingLeft0">
                                                <asp:LinkButton ID="btnSearchSalesOrder" runat="server" TabIndex="5" CausesValidation="false" OnClick="btnSearchSalesOrder_Click">
                                                        <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                </asp:LinkButton>
                                            </div>
                                            <div class="col-sm-3 paddingLeft0">
                                                <asp:Button ID="btnLoadSalesOrder" Text="Load" runat="server" OnClick="btnLoadSalesOrder_Click" />
                                            </div>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                            </div>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </asp:Panel>

    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <asp:Button ID="btnConf2" runat="server" Text="btnConf" Style="display: none;" />
            <asp:ModalPopupExtender ID="mpConfirmation2" runat="server" Enabled="True" TargetControlID="btnConf2"
                PopupControlID="pnlConfirmation2" PopupDragHandleControlID="divCOnf2" Drag="true" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>
        </ContentTemplate>
    </asp:UpdatePanel>

    <asp:Panel runat="server" ID="pnlConfirmation2" Style="display: none" Width="400px" Height="115px">
        <div runat="server" id="Div22" class="panel panel-primary">
            <asp:Label ID="Label13" runat="server" Text="Label" Visible="false"></asp:Label>
            <asp:UpdatePanel runat="server">
                <ContentTemplate>
                    <div class="panel panel-default">
                        <div class="panel-heading" id="divCOnf2">
                            Confirmation
                    <asp:LinkButton ID="LinkButton1" runat="server" OnClick="btnConfClose2_Click">
                             <span class="glyphicon glyphicon-remove" style="float:right" aria-hidden="true">Close</span>
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
                                <div class="col-sm-12">
                                    <asp:Label ID="lblConfText2" Text="" runat="server" />
                                    <asp:HiddenField ID="hdfConf2" runat="server" />
                                    <asp:HiddenField ID="hdfConfItem2" runat="server" />
                                    <asp:HiddenField ID="hdfConfStatus2" runat="server" />
                                </div>
                                <div class="col-sm-12">
                                    <div class="col-sm-3">
                                    </div>
                                    <div class="col-sm-3">
                                        <asp:Button ID="btnConfYes2" CssClass="form-control" Text="Yes" runat="server" OnClick="btnConfYes2_Click" />
                                    </div>
                                    <div class="col-sm-3">
                                    </div>
                                    <div class="col-sm-3">
                                        <asp:Button ID="btnConfNo2" CssClass="form-control" Text="No" runat="server" OnClick="btnConfNo2_Click" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </asp:Panel>


    <asp:UpdatePanel ID="UpdatePanel25" runat="server">
        <ContentTemplate>
            <asp:Button ID="btnconfbox" runat="server" Text="Button" Style="display: none;" />
            <asp:ModalPopupExtender ID="PopupConfBox" runat="server" Enabled="True" TargetControlID="btnconfbox"
                PopupControlID="pnlConfBox" PopupDragHandleControlID="PopupHeader" Drag="true" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>
        </ContentTemplate>
    </asp:UpdatePanel>

    <asp:Panel ID="pnlConfBox" runat="server" align="center">
        <div runat="server" id="Div20" class="panel panel-info height120 width250">
            <asp:Label ID="Label6" runat="server" Text="Label" Visible="false"></asp:Label>
            <asp:ScriptManagerProxy ID="ScriptManagerProxy19" runat="server"></asp:ScriptManagerProxy>
            <asp:UpdatePanel ID="UpdatePanel26" runat="server">
                <ContentTemplate>

                    <div class="panel panel-heading">
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
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-1">
                            </div>
                            <div class="col-sm-4">
                                <asp:Button ID="Button8" runat="server" Text="yes" Width="80px" CausesValidation="false" class="btn btn-primary btn-xs" OnClick="Button1_Click" />
                            </div>
                            <div class="col-sm-2 ">
                                <asp:Button ID="Button9" runat="server" Text="No" Width="80px" CausesValidation="false" class="btn btn-primary btn-xs" OnClick="Button2_Click" />
                            </div>
                        </div>
                    </div>

                </ContentTemplate>
            </asp:UpdatePanel>
        </div>

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
    <asp:UpdatePanel ID="UpdatePanel20" runat="server">
        <ContentTemplate>
            <asp:Button ID="Button2" runat="server" Text="Button" Style="display: none;" />
            <asp:ModalPopupExtender ID="UserDPopoup" runat="server" Enabled="True" TargetControlID="Button2"
                PopupControlID="pnlDpopup" PopupDragHandleControlID="PopupHeader" Drag="true" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>

        </ContentTemplate>

    </asp:UpdatePanel>

    <asp:UpdatePanel ID="UpdatePanel21" runat="server">
        <ContentTemplate>
            <asp:Panel runat="server" ID="pnlDpopup" DefaultButton="lbtnSearch">
                <div runat="server" id="Div21" class="panel panel-default height400 width700">

                    <asp:Label ID="Label12" runat="server" Text="Label" Visible="false"></asp:Label>
                    <div class="panel panel-default">
                        <div class="panel-heading">
                            <asp:LinkButton ID="btnDClose" runat="server" OnClick="btnDClose_Click">
                             <span class="glyphicon glyphicon-remove" aria-hidden="true">Close</span>
                            </asp:LinkButton>
                            <%--<span>Commen Search</span>--%>
                            <div class="col-sm-11">
                            </div>
                            <div class="col-sm-1">
                            </div>
                        </div>
                        <div class="panel-body">
                            <div class="col-sm-12" id="Div23" runat="server">
                                <div class="row">
                                    <div class="col-sm-12 height5">
                                    </div>
                                </div>
                                <div class="col-sm-5">
                                    <div class="row">
                                        <div class="col-sm-2 labelText1">
                                            From
                                        </div>
                                        <div class="col-sm-6 paddingRight5 paddingLeft0">
                                            <asp:TextBox runat="server" TabIndex="200" Enabled="false" ID="txtFDate" CausesValidation="false" CssClass="form-control"></asp:TextBox>
                                        </div>
                                        <div class="col-sm-1 paddingLeft0">
                                            <asp:LinkButton ID="lbtnFDate" runat="server" CausesValidation="false">
                                                <span class="glyphicon glyphicon-calendar" aria-hidden="true"></span>
                                            </asp:LinkButton>
                                            <asp:CalendarExtender ID="CalendarExtender5" runat="server" TargetControlID="txtFDate"
                                                PopupButtonID="lbtnFDate" Format="dd/MMM/yyyy">
                                            </asp:CalendarExtender>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-sm-2 labelText1">
                                            To
                                        </div>
                                        <div class="col-sm-6 paddingRight5 paddingLeft0">
                                            <asp:TextBox runat="server" Enabled="false" TabIndex="201" ID="txtTDate" CausesValidation="false" CssClass="form-control"></asp:TextBox>
                                        </div>
                                        <div class="col-sm-1 paddingLeft0">
                                            <asp:LinkButton ID="lbtnTDate" runat="server" CausesValidation="false">
                                                <span class="glyphicon glyphicon-calendar" aria-hidden="true"></span>
                                            </asp:LinkButton>
                                            <asp:CalendarExtender ID="CalendarExtender6" runat="server" TargetControlID="txtTDate"
                                                PopupButtonID="lbtnTDate" Format="dd/MMM/yyyy">
                                            </asp:CalendarExtender>

                                        </div>
                                        <div class="col-sm-1 paddingLeft0">
                                            <asp:LinkButton ID="lbtnDateS" runat="server" OnClick="lbtnDateS_Click">
                                            <span class="glyphicon glyphicon-search" aria-hidden="true"  ></span>
                                            </asp:LinkButton>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-sm-7">

                                    <div class="row">

                                        <div class="col-sm-3 labelText1">
                                            Search by key
                                        </div>
                                        <asp:ScriptManagerProxy ID="ScriptManagerProxy18" runat="server"></asp:ScriptManagerProxy>
                                        <asp:UpdatePanel ID="UpdatePanel22" runat="server">
                                            <ContentTemplate>
                                                <div class="col-sm-8 paddingRight5">
                                                    <asp:DropDownList ID="ddlSearchbykeyD" TabIndex="202" runat="server" class="form-control">
                                                    </asp:DropDownList>
                                                </div>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>
                                    <div class="row">
                                        <div class="col-sm-3 labelText1">
                                            Search by word
                                        </div>
                                        <div class="col-sm-8 paddingRight5">
                                            <asp:TextBox ID="txtSearchbywordD" CausesValidation="false" TabIndex="203" placeholder="Search by word" class="form-control" AutoPostBack="true" runat="server" OnTextChanged="txtSearchbywordD_TextChanged"></asp:TextBox>
                                        </div>
                                        <asp:ScriptManagerProxy ID="ScriptManagerProxy2" runat="server"></asp:ScriptManagerProxy>
                                        <asp:UpdatePanel ID="UpdatePanel23" runat="server">
                                            <ContentTemplate>
                                                <div class="col-sm-1 paddingLeft0">
                                                    <asp:LinkButton ID="lbtnSearchD" runat="server" OnClick="lbtnSearchD_Click">
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
                                        <asp:ScriptManagerProxy ID="ScriptManagerProxy20" runat="server"></asp:ScriptManagerProxy>
                                        <asp:UpdatePanel ID="UpdatePanel24" runat="server">
                                            <ContentTemplate>
                                                <asp:GridView ID="grdResultD" CausesValidation="false" runat="server" EmptyDataText="No data found..." ShowHeaderWhenEmpty="True" GridLines="None" CssClass="table table-hover table-striped" PageSize="10" PagerStyle-CssClass="cssPager" AllowPaging="True" OnSelectedIndexChanged="grdResultD_SelectedIndexChanged" OnPageIndexChanging="grdResultD_PageIndexChanging">
                                                    <Columns>
                                                        <asp:ButtonField Text="Select" CommandName="Select" ItemStyle-Width="10" />

                                                    </Columns>
                                                </asp:GridView>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>
                                </div>

                                <asp:UpdateProgress ID="UpdateProgress6" DisplayAfter="10"
                                    runat="server" AssociatedUpdatePanelID="UpdatePanel24">

                                    <ProgressTemplate>
                                        <div class="divWaiting">
                                            <asp:Label ID="lblWait6" runat="server"
                                                Text="Please wait... " />
                                            <asp:Image ID="imgWait6" runat="server"
                                                ImageAlign="Middle" ImageUrl="~/images/banners/hidden_progress_bar.gif" />
                                        </div>
                                    </ProgressTemplate>

                                </asp:UpdateProgress>
                            </div>
                        </div>

                    </div>

                </div>
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>

    <asp:UpdatePanel ID="UpdatePanel27" runat="server">
        <ContentTemplate>
            <asp:Button ID="Button5" runat="server" Text="Button" Style="display: none;" />
            <asp:ModalPopupExtender ID="MdlSalesOrderApp" runat="server" Enabled="True" TargetControlID="Button5"
                PopupControlID="pnlSoAppConfBox" PopupDragHandleControlID="PopupSoHeader" Drag="true" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>
        </ContentTemplate>
    </asp:UpdatePanel>

    <asp:Panel ID="pnlSoAppConfBox" runat="server" align="center">
        <div runat="server" id="PopupSoHeader" class="panel panel-info height140" style="width: 300px;">
            <asp:Label ID="Label16" runat="server" Text="Label" Visible="false"></asp:Label>
            <asp:ScriptManagerProxy ID="ScriptManagerProxy23" runat="server"></asp:ScriptManagerProxy>
            <asp:UpdatePanel ID="UpdatePanel28" runat="server">
                <ContentTemplate>

                    <div class="panel panel-heading">
                        <span>Alert</span>
                    </div>
                    <div class="panel-body">
                        <div class="row">
                            <div class="col-sm-6">
                                <asp:Label ID="Label18" runat="server"></asp:Label>
                            </div>
                            <div class="col-sm-2 paddingRight0">
                                <asp:LinkButton ID="LinkButton2" CausesValidation="false" runat="server">
                                                        <span class="glyphicon glyphicon-ok" aria-hidden="true"></span>
                                </asp:LinkButton>
                            </div>
                        </div>
                         <div class="row">
                            <div class="col-sm-12 height10">
                                
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-6">
                                <asp:Label ID="Label19" runat="server"></asp:Label>
                            </div>
                             <div class="col-sm-2 paddingRight0">
                                <asp:LinkButton ID="LinkButton3" CausesValidation="false" runat="server">
                                                        <span class="glyphicon glyphicon-remove" aria-hidden="true"></span>
                                </asp:LinkButton>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-12">
                                <asp:Label ID="Label20" runat="server"></asp:Label>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-12">
                                <asp:Label ID="Label21" Text="As one of the condition has been failed" runat="server"></asp:Label>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-12">
                                <asp:Label ID="Label22" Text="Account department has to approve the sales order!" runat="server"></asp:Label>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-12 height22">
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-4">
                            </div>
                            <div class="col-sm-4">
                                <asp:Button ID="lbtnSoAppConfirm" runat="server" OnClick="lbtnSoAppConfirm_Click" Text="OK" Width="80px" CausesValidation="false" class="btn btn-primary btn-xs" />
                            </div>
                          
                        </div>
                    </div>

                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </asp:Panel>
         <%-- pnl excel Upload --%>
     <asp:UpdatePanel ID="upExcelUpload" runat="server">
        <ContentTemplate>
            <asp:Button ID="btn10" runat="server" Text="Button" Style="display: none;" />
            <asp:ModalPopupExtender ID="popupExcel" runat="server" Enabled="True" TargetControlID="btn10"
                PopupControlID="pnlExcelUpload" PopupDragHandleControlID="PopupHeader" Drag="true" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>
        </ContentTemplate>
    </asp:UpdatePanel>

    <asp:Panel runat="server" ID="pnlExcelUpload">
        <div class="panel panel-default height45 width700 ">
            <div class="panel panel-default">
                <div class="panel-heading height30">
                    <div class="col-sm-11">
                        Excel Upload
                    </div>
                    <div class="col-sm-1">
                        <asp:LinkButton ID="lbtnExcelUploadClose" runat="server" OnClick="lbtnExcelUploadClose_Click">
                             <span class="glyphicon glyphicon-remove" aria-hidden="true">Close</span>
                        </asp:LinkButton>
                    </div>
                    <%--<span>Commen Search</span>--%>
                </div>
                <div class="panel-body">
                    <div class="col-sm-12 paddingLeft0 paddingRight0">
                        <div class="row height22">
                            <div class="col-sm-11">
                                <asp:UpdatePanel runat="server">
                                    <ContentTemplate>
                                        <asp:Label ID="lblExcelUploadError" runat="server" ForeColor="Red" Text="Label" Visible="false"></asp:Label>
                                        <asp:Label ID="lblExcelUploadSuccess" runat="server" ForeColor="Green" Visible="false"></asp:Label>
                                        <asp:Label ID="lblExcelUploadInfo" runat="server" ForeColor="Blue" Visible="false"></asp:Label>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                            <div class="col-sm-1">
                            </div>
                        </div>
                        <div class="row">
                            <asp:Panel runat="server">
                                <div class="col-sm-12">
                                    <div class="col-sm-10 paddingRight5">
                                        <asp:FileUpload ID="fileUploadExcel" runat="server" />
                                    </div>
                                    <div class="col-sm-2 paddingRight5">
                                        <asp:Button ID="lbtnUploadExcel" class="btn btn-warning btn-xs" runat="server" Text="Upload"
                                            OnClick="lbtnUploadExcel_Click" />
                                    </div>
                                </div>
                            </asp:Panel>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </asp:Panel>

        <%-- pnl excel data upload confirm --%>
     <asp:UpdatePanel ID="UpdatePanel3" runat="server">
        <ContentTemplate>
            <asp:Button ID="Button1" runat="server" Text="Button" Style="display: none;" />
            <asp:ModalPopupExtender ID="popConfExcUpload" runat="server" Enabled="True" TargetControlID="Button1"
                PopupControlID="pnlConfExcUpload" PopupDragHandleControlID="PopupHeader" Drag="true" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>
        </ContentTemplate>
     </asp:UpdatePanel>
    <asp:UpdateProgress ID="UpdateProgress8" DisplayAfter="10" runat="server" AssociatedUpdatePanelID="uppnlexcUploa">
        <ProgressTemplate>
            <div class="divWaiting">
                <asp:Label ID="lblWaitd10" runat="server"
                    Text="Please wait... " />
                <asp:Image ID="imgWaitd10" runat="server"
                    ImageAlign="Middle" ImageUrl="~/images/icons/ajax-loader.gif" />
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <asp:Panel runat="server" ID="pnlConfExcUpload">
        <div class="panel panel-default height45 width700 ">
            <div class="panel panel-default">
                <div class="panel-heading height30">
                    <div class="col-sm-11">
                        Excel Upload
                    </div>
                    <div class="col-sm-1">
                        <asp:LinkButton ID="LinkButton4" runat="server" OnClick="lbtnExcelUploadClose_Click">
                             <span class="glyphicon glyphicon-remove" aria-hidden="true">Close</span>
                        </asp:LinkButton>
                    </div>
                    <%--<span>Commen Search</span>--%>
                </div>
                <div class="panel-body">
                    <div class="col-sm-12 paddingLeft0 paddingRight0">
                        <div class="row height22">
                            <div class="col-sm-11">
                                <asp:UpdatePanel runat="server">
                                    <ContentTemplate>
                                        <asp:Label ID="Label8" runat="server" ForeColor="Red" Text="Label" Visible="false"></asp:Label>
                                        <asp:Label ID="Label9" runat="server" ForeColor="Green" Visible="false"></asp:Label>
                                        <asp:Label ID="Label10" runat="server" ForeColor="Blue" Visible="false"></asp:Label>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                            <div class="col-sm-1">
                            </div>
                        </div>
                        <div class="row">
                            <asp:Panel runat="server">
                                <div class="col-sm-12">
                                    <div class="col-sm-10 paddingRight5">
                                        Are you sure do you want to upload excel data ?
                                    </div>
                                    <div class="col-sm-2 paddingRight5">
                                        <asp:UpdatePanel runat="server" ID="uppnlexcUploa">
                                            <ContentTemplate>
                                                <asp:Button ID="btnExcelDataUploadConf" class="btn btn-warning btn-xs" runat="server" Text="Upload"
                                            OnClick="btnExcelDataUploadConf_Click" />
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
    </asp:Panel>


    <script src="<%=Request.ApplicationPath%>Js/jquery.min.js"></script>
    <script src="<%=Request.ApplicationPath%>Js/bootstrap.min.js"></script>

    <script>
        Sys.Application.add_load(func);
        function func() {
            //$('.txtItem').last().focusout(function () {
            //    alert(e.keyCode);
            //    //if (e.keyCode == 13) {
            //    //    __doPostBack('txtItem', '');
            //    //}
            //});
            $('.txtItem').keydown(function (evt) {
                evt = (evt) ? evt : window.event;
                //var charCode = (evt.which) ? evt.which : evt.keyCode;
                var charCode = evt.which;
                if (charCode == 113) {
                    openItemSearchNew();
                    //__doPostBack('#BodyContent_btnSearch_Item', '');
                }
            });
            $('.txtModel').keydown(function (evt) {
                evt = (evt) ? evt : window.event;
                //var charCode = (evt.which) ? evt.which : evt.keyCode;
                var charCode = evt.which;
                if (charCode == 113) {
                    openItemSearchNew();
                    //__doPostBack('#BodyContent_btnSearch_Item', '');
                }
            });
        }
        function openItemSearchNew() {
            var bnItm = document.getElementById('<% =btnSearch_Item.ClientID %>');
            bnItm.focus();
            document.getElementById('<% = btnSearch_Item.ClientID %>').click();
        };
    </script>

</asp:Content>