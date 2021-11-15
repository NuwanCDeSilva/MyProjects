<%@ Page Title="" Language="C#" MasterPageFile="~/View/AdminSite.Master" AutoEventWireup="true" CodeBehind="BillOfLading.aspx.cs" Inherits="FastForward.SCMWeb.View.Transaction.Imports.BL.BillOfLading" %>

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
        };

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

        function scrollTop() {
            window.document.body.scrollTop = 0;
            window.document.documentElement.scrollTop = 0;
        };

        function ConfirmClearForm() {
            var selectedvalueOrd = confirm("Do you want to clear all details ?");
            if (selectedvalueOrd) {
                return true;
            } else {
                return false;
            }
        };

        function ConfirmSave() {
            var selectedvalueOrd = confirm("Do you want to save ?");
            if (selectedvalueOrd) {
                return true;
            } else {
                return false;
            }
        };
        function ConfirmReset() {
            var selectedvalueOrd = confirm("Do you want to reset ?");
            if (selectedvalueOrd) {
                return true;
            } else {
                return false;
            }
        };
        function ConfirmUpdate() {
            var selectedvalueOrd = confirm("Do you want to update ?");
            if (selectedvalueOrd) {
                return true;
            } else {
                return false;
            }
        };

        function ConfirmCancel() {
            var selectedvalueOrd = confirm("Do you want to cancel ?");
            if (selectedvalueOrd) {
                document.getElementById('<%=hdfCancel.ClientID %>').value = "Yes";
            } else {
                document.getElementById('<%=hdfCancel.ClientID %>').value = "No";
            }
        };
        function ConfirmApprove() {
            var resut = confirm("Do you want to approve ?");
            if (resut) {
                document.getElementById('<%=hdfAppro.ClientID %>').value = "Yes";
            } else {
                document.getElementById('<%=hdfAppro.ClientID %>').value = "No";
            }
        };

        function ConfirmDelOrfina() {
            var selectedvalueOrd = confirm("Do you want to delete ?");
            if (selectedvalueOrd) {
                document.getElementById('<%=hdfDelofrF.ClientID %>').value = "Yes";
            } else {
                document.getElementById('<%=hdfDelofrF.ClientID %>').value = "No";
            }
        };

        function ConfirmDelSI() {
            var selectedvalueOrd = confirm("Do you want to delete ?");
            if (selectedvalueOrd) {
                document.getElementById('<%=hdfDelSI.ClientID %>').value = "Yes";
            } else {
                document.getElementById('<%=hdfDelSI.ClientID %>').value = "No";
            }
        };
        function ConfirmDelConta() {
            var selectedvalueOrd = confirm("Do you want to delete ?");
            if (selectedvalueOrd) {
                document.getElementById('<%=hdfDelConta.ClientID %>').value = "Yes";
            } else {
                document.getElementById('<%=hdfDelConta.ClientID %>').value = "No";
            }
        };
        function ConfDelAll() {
            var selectedvalueOrd = confirm("Do you want to delete all ?");
            if (selectedvalueOrd) {
                return true;
            } else {
                return false;
            }
        };
        function ConfirmDelinvoice() {
            var selectedvalueOrd = confirm("Do you want to delete ?");
            if (selectedvalueOrd) {
                document.getElementById('<%=hdfDelInvoiceItem.ClientID %>').value = "Yes";
            } else {
                document.getElementById('<%=hdfDelInvoiceItem.ClientID %>').value = "No";
            }
        };
        function checkDate(sender, args) {
            var _date = sender._selectedDate;
            var d = new Date(_date);
            d.setMonth(d.getMonth() + 1);
            document.getElementById('<%=txtexDate.ClientID %>').value = d.format(sender._format);

        }
        function checkDate2(sender, args) {
            var _date = sender._selectedDate;
            var d = new Date(_date);
            d.setMonth(d.getMonth() + 1);
            document.getElementById('<%=txtCreditAllowDate.ClientID %>').value = d.format(sender._format);

        }
        function checkDateEx(sender, args) {
            var _date = jQuery("#BodyContent_txtissuedate").val();
            var _dateIss = sender._selectedDate;
            var d = new Date(_date); var d = new Date(_date);
            if ((sender._selectedDate < d)) {
                //alert(d.format(sender._format));
                sender._textbox.set_Value(sender._selectedDate.format(sender._format))
            }
            //var d = new Date(_date);
            //if ((sender._selectedDate < d)) {
            //   alert("You cannot select a day earlier than issue date !");
            ////    sender._textbox.set_Value(sender._selectedDate.format(sender._format))
            //}
        }
        function checkDateTest(sender, args) {
            //if ((sender._selectedDate < new Date())) {
            //    sender._selectedDate = new Date();
            //    sender._textbox.set_Value(sender._selectedDate.format(sender._format))
            //}
            document.getElementById('<%=txtexDate.ClientID %>').value = sender._selectedDate.format(sender._format);
            //alert(sender._selectedDate);
            //sender._selectedDate = new Date();
            //sender._textbox.set_Value(sender._selectedDate.format(sender._format))
        }
        // function Exp
        function ChangeExpDate() {
            var _date = jQuery("#BodyContent_txtissuedate").val();
            //jQuery("#BodyContent_txtexDate").val(_date);
            var d = new Date(_date);
            d.setMonth(d.getMonth() + 1);
            jQuery("#BodyContent_txtexDate").val((d.getMonth() + 1) + '/' + d.getDate() + '/' + d.getFullYear());
            //document.form1.textbox1.value = (d.getMonth() + 1) + '/' + d.getDate() + '/' + d.getFullYear();

        }
        function ConfirmUpdateBL() {
            var selectedvalueOrd = confirm("Do you want to update ?");
            if (selectedvalueOrd) {
                document.getElementById('<%=hdfUpdate.ClientID %>').value = "Yes";
            } else {
                document.getElementById('<%=hdfUpdate.ClientID %>').value = "No";
            }
        };
        <%--       function ConfQtyUpdate() {
            var selectedvalueOrd = confirm("This item is already added. Do you want to update the quantity?");
            if (selectedvalueOrd) {
                document.getElementById('<%=hdfDelInvoiceItem.ClientID %>').value = "Yes";
            } else {
                document.getElementById('<%=hdfDelInvoiceItem.ClientID %>').value = "No";
            }
        };--%>

    </script>

    <script type="text/javascript">

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
            if (jQuery('.toast-item-wrapper') != null) {
                jQuery('.toast-item-wrapper').remove();
            }
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
        runat="server" AssociatedUpdatePanelID="pnlMain">
        <ProgressTemplate>
            <div class="divWaiting">
                <asp:Label ID="lblWait" runat="server"
                    Text="Please wait... " />
                <asp:Image ID="imgWait" runat="server"
                    ImageAlign="Middle" ImageUrl="~/images/icons/ajax-loader.gif" />
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>

    <div class="panel panel-default marginLeftRight5 paddingbottom0">
        <div class="panel-body paddingtopbottom0">
            <asp:UpdatePanel runat="server" ID="pnlMain">
                <ContentTemplate>
                    <asp:HiddenField ID="HiddenField1" runat="server" />
                    <asp:HiddenField ID="isNewRecord" runat="server" />
                    <asp:HiddenField ID="hdfDelofrF" runat="server" />
                    <asp:HiddenField ID="hdfDelSI" runat="server" />
                    <asp:HiddenField ID="hdfDelConta" runat="server" />
                    <asp:HiddenField ID="hdfDelInvoiceItem" runat="server" />
                    <asp:HiddenField ID="hdfHEaderStatus" runat="server" />
                    <asp:HiddenField ID="hdfAppro" runat="server" />
                    <asp:HiddenField ID="hdfCancel" runat="server" />
                    <asp:HiddenField ID="hdfUpdate" runat="server" />
                    <asp:HiddenField ID="hdfPiNo" runat="server" />
                    <asp:HiddenField ID="hdfPiSeqNo" runat="server" />
                    <div class="row">
                        <div class="col-sm-4">
                            <div id="divWarning" visible="false" class="alert alert-danger alert-dismissible" role="alert" runat="server">
                                <div class="row">
                                    <div class="col-sm-11 buttonrow">
                                        <strong>Alert!</strong>
                                        <asp:Label ID="lblWarning" runat="server"></asp:Label>
                                    </div>
                                    <div class="col-sm-1  buttonrow">
                                        <div class="col-sm-3  buttonrow">
                                            <asp:LinkButton ID="lbtndicalertclose" runat="server" CausesValidation="false" CssClass="floatright">
                                        <span class="glyphicon glyphicon-ok" aria-hidden="true"></span>
                                            </asp:LinkButton>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div id="divSuccess" runat="server" visible="false" class="alert alert-success alert-dismissible" role="alert">
                                <div class="row">
                                    <div class="col-sm-11 buttonrow">
                                        <strong>Success!</strong>
                                        <asp:Label ID="lblSuccess" runat="server"></asp:Label>
                                    </div>
                                    <div class="col-sm-1  buttonrow">
                                        <div class="col-sm-3  buttonrow">
                                            <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="false" CssClass="floatright">
                                        <span class="glyphicon glyphicon-ok" aria-hidden="true"></span>
                                            </asp:LinkButton>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div id="divAlert" runat="server" visible="false" class="alert alert-info alert-dismissible" role="alert">
                                <div class="row">
                                    <div class="col-sm-11 buttonrow">
                                        <strong>Alert!</strong>
                                        <asp:Label ID="lblAlert" runat="server"></asp:Label>
                                    </div>
                                    <div class="col-sm-1  buttonrow">
                                        <div class="col-sm-3  buttonrow">
                                            <asp:LinkButton ID="LinkButton2" runat="server" CausesValidation="false" CssClass="floatright">
                                        <span class="glyphicon glyphicon-ok" aria-hidden="true"></span>
                                            </asp:LinkButton>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-sm-8  buttonRow">
                            <div class="row">
                                <div class="col-sm-4">
                                </div>
                                <div class="col-sm-1 paddingRight0" runat="server">
                                    <asp:LinkButton ID="btnreset" CausesValidation="false" Visible="false" runat="server" CssClass="floatRight" OnClientClick=" return ConfirmReset()" OnClick="btnreset_Click">
                                        <span class="glyphicon glyphicon-repeat" aria-hidden="true"></span>Reset
                                    </asp:LinkButton>
                                </div>
                                <div class="col-sm-1 paddingRight0 paddingLeft0" runat="server" id="divSave">
                                    <asp:LinkButton ID="btnSave" CausesValidation="false" runat="server" CssClass="floatRight" OnClientClick=" return ConfirmSave()" OnClick="btnSave_Click">
                                        <span class="glyphicon glyphicon-saved" aria-hidden="true"></span>Save
                                    </asp:LinkButton>
                                </div>
                                <div class="col-sm-1 paddingRight0 paddingLeft0">
                                    <asp:LinkButton ID="btnUpdateAdvnDet" CausesValidation="false" runat="server" OnClientClick="return ConfirmUpdate()" OnClick="btnUpdateAdvnDet_Click">
                                        <span class="glyphicon glyphicon-edit" aria-hidden="true"></span>Update
                                    </asp:LinkButton>
                                </div>
                                <div class="col-sm-1 paddingRight0 paddingLeft0" runat="server" id="divApprove">
                                    <asp:LinkButton ID="btnApprove" CausesValidation="false" runat="server" OnClientClick="return ConfirmApprove();" OnClick="btnApprove_Click">
                                        <span class="glyphicon glyphicon-thumbs-up" aria-hidden="true"></span>Approve
                                    </asp:LinkButton>
                                </div>
                                <div class="col-sm-1 paddingRight0 paddingLeft0">
                                    <asp:LinkButton ID="btnCancel" CausesValidation="false" runat="server" CssClass="floatRight" OnClientClick="return ConfirmCancel();" OnClick="btnCancel_Click">
                                        <span class="glyphicon glyphicon-remove" aria-hidden="true"></span>Cancel
                                    </asp:LinkButton>
                                </div>
                                <div class="col-sm-1 paddingRight0">
                                    <asp:LinkButton ID="lblClear" runat="server" OnClientClick="return ConfirmClearForm();" OnClick="lblClear_Click">
                                        <span class="glyphicon glyphicon-refresh" aria-hidden="true"></span>Clear
                                    </asp:LinkButton>
                                </div>
                                <div class="col-sm-1 paddingRight0">
                                    <div class="dropdown">
                                        <a id="dLabel" data-toggle="dropdown" role="button" aria-haspopup="true" aria-expanded="false">
                                            <span class="glyphicon glyphicon-menu-hamburger"></span>
                                        </a>
                                        <div class="dropdown-menu menupopup" aria-labelledby="dLabel">
                                            <div class="row">
                                                <div class="col-sm-12">
                                                    <div class="col-sm-12 paddingRight0">
                                                        <asp:LinkButton ID="lbtncancel" CausesValidation="false" runat="server" OnClick="lbtncancel_Click">
                                                        <span class="glyphicon glyphicon-asterisk" aria-hidden="true"></span>More Details
                                                        </asp:LinkButton>
                                                    </div>
                                                    <div class="col-sm-12 paddingRight0">
                                                        <asp:LinkButton ID="LinkButton4" CausesValidation="false" OnClientClick="return ConfirmUpdate()" runat="server" OnClick="lbwupdate_Click">
                                                        <span class="glyphicon glyphicon-asterisk" aria-hidden="true"></span>Wharf update
                                                        </asp:LinkButton>
                                                    </div>
                                                    <div class="col-sm-12 paddingRight0">
                                                        <asp:LinkButton ID="LinkButton3" CausesValidation="false" runat="server" OnClick="lbtnPrint_Click">
                                                        <span class="glyphicon glyphicon-asterisk" aria-hidden="true"></span>Print
                                                        </asp:LinkButton>
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
                        <div class="col-sm-12">
                            <div class="panel panel-default marginBottom0">
                                <div class="panel-heading paddingtop0 paddingbottom0 marginBottom0">
                                    <div class="row">
                                        <div class="col-sm-6 labelText1 padding0">
                                            Bill of Lading
                                        </div>
                                        <div class="col-sm-6 labelText1 padding0 paddingtopbottom0" style="padding-top: 0px;">
                                            <div class="row">
                                                <div class="col-sm-4 labelText1">
                                                    <div class="row">
                                                        <div class="col-sm-6 labelText1">
                                                            Total FOC B/L
                                                            <asp:CheckBox ID="chkTotalFOCShipment" AutoPostBack="true" Text=" " runat="server" OnCheckedChanged="chkTotalFOCShipment_CheckedChanged" />
                                                        </div>
                                                        <div class="col-sm-6 labelText1">
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-sm-4 labelText1">
                                                    <div class="row">
                                                        <div class="col-sm-10 labelText1">
                                                            Is Outside Clearing Party
                                                            <asp:CheckBox ID="chkBypassEntry" Text=" " runat="server" />
                                                        </div>
                                                        <div class="col-sm-2 labelText1">
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-sm-4 labelText1">
                                                    <div class="col-sm-4  paddingLeft0">
                                                        Status
                                                    </div>
                                                    <div class="col-sm-6 paddingLeft0">
                                                        <asp:TextBox ID="txtStatus" Style="color: red;" ReadOnly="true" MaxLength="100" CausesValidation="false" runat="server" class="form-control"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="panel-body">
                                    <div class="row">
                                        <div class="col-sm-4">
                                            <div class="col-sm-12 paddingLeft0 paddingRight0">
                                                <div class="col-sm-6 padding0">
                                                    <div class="col-sm-5  paddingLeft0">
                                                        Doc #
                                                    </div>
                                                    <div class="col-sm-6 padding0">
                                                        <asp:TextBox ID="txtDocNumber" CausesValidation="false" AutoPostBack="true" runat="server" class="form-control" OnTextChanged="txtDocNumber_TextChanged"></asp:TextBox>
                                                    </div>
                                                    <div class="col-sm-1 paddingLeft0">
                                                        <asp:LinkButton ID="btnDocNumber" runat="server" OnClick="btnDocNumber_Click">
                                                            <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                        </asp:LinkButton>
                                                    </div>
                                                </div>
                                                <div class="col-sm-6 padding0">
                                                    <div class="col-sm-5 padding0">
                                                        B/L #
                                                    </div>
                                                    <div class="col-sm-6 padding0">
                                                        <asp:TextBox ID="txtBLNumber" CausesValidation="false" AutoPostBack="true" runat="server" class="form-control" OnTextChanged="txtBLNumber_TextChanged"></asp:TextBox>
                                                    </div>
                                                    <div class="col-sm-1 paddingLeft0">
                                                        <%--<asp:LinkButton ID="btnBLNumber" runat="server">
                                                            <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                        </asp:LinkButton>--%>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-sm-12 height5"></div>
                                            <div class="col-sm-12 paddingLeft0 paddingRight0">
                                                <div class="col-sm-6 padding0">
                                                    <div class="col-sm-5 padding0">
                                                        B/L Date
                                                    </div>
                                                    <div class="col-sm-6 padding0">
                                                        <asp:TextBox ID="txtShipmentDate" Enabled="false" CausesValidation="false" runat="server" AutoPostBack="true" CssClass="form-control" OnTextChanged="txtShipmentDate_TextChanged"></asp:TextBox>
                                                    </div>
                                                    <div class="col-sm-1 paddingLeft0">
                                                        <asp:LinkButton ID="btnShipmentDate" runat="server" CausesValidation="false">
                                                            <span class="glyphicon glyphicon-calendar" aria-hidden="true"></span>
                                                        </asp:LinkButton>
                                                        <asp:CalendarExtender ID="CalendarExtender3" runat="server" TargetControlID="txtShipmentDate"
                                                            PopupButtonID="btnShipmentDate" Format="dd/MMM/yyyy">
                                                        </asp:CalendarExtender>
                                                    </div>
                                                </div>
                                                <div class="col-sm-6 padding0">
                                                    <div class="col-sm-5 padding0">
                                                        Reference #
                                                    </div>
                                                    <div class="col-sm-6 padding0">
                                                        <asp:TextBox ID="txtReferenceNum" CausesValidation="false" runat="server" class="form-control"></asp:TextBox>
                                                    </div>
                                                    <div class="col-sm-1 paddingLeft0">
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-sm-12 height5"></div>
                                            <div class="col-sm-12 paddingLeft0 paddingRight0">
                                                <div class="col-sm-6 padding0">
                                                    <div class="col-sm-5  paddingLeft0">
                                                        Supplier UOM
                                                    </div>
                                                    <div class="col-sm-6 padding0">
                                                        <asp:DropDownList ID="ddlUOM" class="form-control" runat="server" AppendDataBoundItems="true">
                                                            <asp:ListItem Text="--Select--" Value="0" />
                                                        </asp:DropDownList>
                                                    </div>
                                                    <div class="col-sm-1 paddingLeft0">
                                                    </div>
                                                </div>
                                                <div class="col-sm-6 padding0">

                                                    <div class="col-sm-5 padding0">
                                                        Doc Rec. Date
                                                    </div>
                                                    <div class="col-sm-6 padding0">
                                                        <asp:TextBox ID="txtDocReceivedDate" Enabled="false" CausesValidation="false" runat="server" CssClass="form-control"></asp:TextBox>
                                                    </div>
                                                    <div class="col-sm-1 paddingLeft0">
                                                        <asp:LinkButton ID="btnDocReceivedDate" runat="server" CausesValidation="false">
                                                            <span class="glyphicon glyphicon-calendar" aria-hidden="true"></span>
                                                        </asp:LinkButton>
                                                        <asp:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtDocReceivedDate"
                                                            PopupButtonID="btnDocReceivedDate" Format="dd/MMM/yyyy">
                                                        </asp:CalendarExtender>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-sm-12 height5"></div>
                                            <div class="col-sm-12 paddingLeft0 paddingRight0">
                                                <div class="col-sm-6 padding0">
                                                    <div class="col-sm-5  padding0">
                                                        Total Packages
                                                    </div>
                                                    <div class="col-sm-6 padding0">
                                                        <asp:TextBox ID="txtTotalPackages" CausesValidation="false" runat="server" class="form-control"></asp:TextBox>
                                                    </div>
                                                    <div class="col-sm-1 paddingLeft0">
                                                    </div>
                                                </div>
                                                <div class="col-sm-6 padding0">
                                                    <div class="col-sm-5  padding0">
                                                        ETD
                                                    </div>
                                                    <div class="col-sm-6 padding0">
                                                        <asp:TextBox ID="txtETD" Enabled="false" CausesValidation="false" runat="server" CssClass="form-control"></asp:TextBox>
                                                    </div>
                                                    <div class="col-sm-1 paddingLeft0">
                                                        <asp:LinkButton ID="btnETD" runat="server" CausesValidation="false">
                                                                    <span class="glyphicon glyphicon-calendar" aria-hidden="true"></span>
                                                        </asp:LinkButton>
                                                        <asp:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtETD"
                                                            PopupButtonID="btnETD" Format="dd/MMM/yyyy" OnClientDateSelectionChanged="checkDate2">
                                                        </asp:CalendarExtender>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-sm-12 height5"></div>
                                            <div class="col-sm-12 paddingLeft0 paddingRight0">
                                                <div class="col-sm-6 padding0">
                                                    <div class="col-sm-5  padding0">
                                                        Trade Terms
                                                    </div>
                                                    <div class="col-sm-6 padding0">
                                                        <%--<asp:TextBox ID="txtPlaceOfLanding" CausesValidation="false" runat="server" class="form-control"></asp:TextBox>--%>
                                                        <asp:DropDownList ID="ddlTradeTerms" AutoPostBack="true" runat="server" class="form-control" OnSelectedIndexChanged="ddlTradeTerms_SelectedIndexChanged">
                                                        </asp:DropDownList>
                                                    </div>
                                                    <div class="col-sm-1 paddingLeft0">
                                                    </div>
                                                </div>
                                                <div class="col-sm-6 padding0">
                                                    <div class="col-sm-5 padding0">
                                                        ETA
                                                    </div>
                                                    <div class="col-sm-6 padding0">
                                                        <asp:TextBox ID="txtETA" Enabled="false" CausesValidation="false" runat="server" CssClass="form-control"></asp:TextBox>
                                                    </div>
                                                    <div class="col-sm-1 paddingLeft0">
                                                        <asp:LinkButton ID="btnETA" runat="server" CausesValidation="false">
                                                            <span class="glyphicon glyphicon-calendar" aria-hidden="true"></span>
                                                        </asp:LinkButton>
                                                        <asp:CalendarExtender ID="CalendarExtender4" runat="server" TargetControlID="txtETA"
                                                            PopupButtonID="btnETA" Format="dd/MMM/yyyy">
                                                        </asp:CalendarExtender>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-sm-2 padding0">
                                                Bank Name
                                            </div>
                                            <asp:Label runat="server" ID="lblbank"></asp:Label>
                                        </div>
                                        <div class="col-sm-4 paddingLeft0 paddingRight0">
                                            <div class="col-sm-12 paddingLeft0 paddingRight0">
                                                <div class="col-sm-6 padding0">
                                                    <div class="col-sm-5  paddingLeft0">
                                                        Place Of Loading
                                                    </div>
                                                    <div class="col-sm-6 padding0">
                                                        <%-- <asp:DropDownList ID="ddlTradeTerms" AutoPostBack="true" runat="server" class="form-control" OnSelectedIndexChanged="ddlTradeTerms_SelectedIndexChanged">
                                                        </asp:DropDownList>--%>
                                                        <asp:TextBox ID="txtPlaceOfLanding" CausesValidation="false" runat="server" class="form-control"></asp:TextBox>
                                                    </div>
                                                    <div class="col-sm-1 paddingLeft0">
                                                    </div>
                                                </div>
                                                <div class="col-sm-6 padding0">
                                                    <div class="col-sm-5  paddingLeft0 paddingRight0">
                                                        Location Of Goods
                                                    </div>
                                                    <div class="col-sm-6 padding0">
                                                        <asp:TextBox ID="txtlocationOfGoods" CausesValidation="false" AutoPostBack="true" runat="server" class="form-control"></asp:TextBox>
                                                    </div>
                                                    <div class="col-sm-1 paddingLeft0">
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-sm-12 height5"></div>
                                            <div class="col-sm-12 paddingLeft0 paddingRight0">
                                                <div class="col-sm-6 padding0">
                                                    <div class="col-sm-5  paddingLeft0">
                                                        Consignee
                                                    </div>
                                                    <div class="col-sm-6 padding0">
                                                        <asp:TextBox ID="txtConsignee" CausesValidation="false" AutoPostBack="true" runat="server" class="form-control" OnTextChanged="txtConsignee_TextChanged" ReadOnly="true"></asp:TextBox>
                                                    </div>
                                                    <div class="col-sm-1 paddingLeft0">
                                                    </div>
                                                </div>
                                                <div class="col-sm-6 padding0">
                                                    <div class="col-sm-11 padding0">
                                                        <asp:TextBox ID="txtConsigneeDesc" CausesValidation="false" runat="server" class="form-control" ReadOnly="true"></asp:TextBox>
                                                    </div>
                                                    <div class="col-sm-1 paddingLeft0">
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-sm-12 height5"></div>
                                            <div class="col-sm-12 paddingLeft0 paddingRight0">
                                                <div class="col-sm-6 padding0">
                                                    <div class="col-sm-5  paddingLeft0">
                                                        Declarant
                                                    </div>
                                                    <div class="col-sm-6 padding0">
                                                        <asp:TextBox ID="txtDeclarant" CausesValidation="false" AutoPostBack="true" runat="server" class="form-control" OnTextChanged="txtDeclarant_TextChanged"></asp:TextBox>
                                                    </div>
                                                    <div class="col-sm-1 paddingLeft0">
                                                        <asp:LinkButton ID="btnDeclarant" runat="server" OnClick="btnDeclarant_Click">
                                                            <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                        </asp:LinkButton>
                                                    </div>
                                                </div>
                                                <div class="col-sm-6 padding0">
                                                    <div class="col-sm-5  paddingLeft0">
                                                        Exporter
                                                    </div>
                                                    <div class="col-sm-6 padding0">
                                                        <asp:TextBox ID="txtExporter" CausesValidation="false" AutoPostBack="true" runat="server" class="form-control" OnTextChanged="txtExporter_TextChanged"></asp:TextBox>
                                                        <asp:Label ID="lblCurCode" Text="text" Visible="false" runat="server" />
                                                    </div>
                                                    <div class="col-sm-1 paddingLeft0">
                                                        <asp:LinkButton ID="btnExporter" runat="server" OnClick="btnExporter_Click">
                                                            <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                        </asp:LinkButton>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-sm-12 height5"></div>
                                            <div class="col-sm-12 paddingLeft0 paddingRight0">
                                                <div class="col-sm-6 padding0">
                                                    <div class="col-sm-5  paddingLeft0">
                                                        Agent
                                                    </div>
                                                    <div class="col-sm-6 padding0">
                                                        <asp:TextBox ID="txtAgent" CausesValidation="false" AutoPostBack="true" runat="server" class="form-control" OnTextChanged="txtAgent_TextChanged"></asp:TextBox>
                                                    </div>
                                                    <div class="col-sm-1 paddingLeft0">
                                                        <asp:LinkButton ID="btnAgent" runat="server" OnClick="btnAgent_Click">
                                                            <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                        </asp:LinkButton>
                                                    </div>
                                                </div>
                                                <div class="col-sm-6 padding0">
                                                    <div class="col-sm-5  paddingLeft0">
                                                        Currency Code
                                                    </div>
                                                    <div class="col-sm-4 padding0">
                                                        <asp:TextBox ID="txtCurrenyCode" CausesValidation="false" AutoPostBack="true" runat="server" class="form-control" ReadOnly="true"></asp:TextBox>
                                                    </div>
                                                    <div class="col-sm-1 paddingLeft0">

                                                        <asp:Label ID="lbtnrate" runat="server" ForeColor="#A513D0"></asp:Label>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-sm-12 height5"></div>
                                            <div class="col-sm-12 paddingLeft0 paddingRight0">
                                                <div class="col-sm-6 padding0">
                                                    <div class="col-sm-5  padding0">
                                                        Mode Of Shipment
                                                    </div>
                                                    <div class="col-sm-6 padding0">
                                                        <asp:DropDownList ID="ddlModeofShipment" runat="server" class="form-control">
                                                            <asp:ListItem Text=" - - Select - - " Value="0" Selected="True"></asp:ListItem>
                                                            <asp:ListItem Text="Air" Value="A"></asp:ListItem>
                                                            <asp:ListItem Text="Sea" Value="S"></asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                    <div class="col-sm-1 paddingLeft0">
                                                    </div>
                                                </div>
                                                <div class="col-sm-6 padding0">
                                                    <div class="col-sm-5  padding0">
                                                        Credit Allow Date
                                                         <asp:TextBox ID="txtPackingListNo" Visible="false" CausesValidation="false" runat="server" class="form-control"></asp:TextBox>
                                                    </div>
                                                    <div class="col-sm-6 padding0">
                                                        <asp:TextBox ID="txtCreditAllowDate" Enabled="false" CausesValidation="false" runat="server" class="form-control"></asp:TextBox>

                                                    </div>
                                                    <div class="col-sm-1 paddingLeft0">
                                                        <asp:LinkButton ID="LinkButton6" runat="server" CausesValidation="false">
                                                            <span class="glyphicon glyphicon-calendar" aria-hidden="true"></span>
                                                        </asp:LinkButton>
                                                        <asp:CalendarExtender ID="CalendarExtender8" runat="server" TargetControlID="txtCreditAllowDate"
                                                            PopupButtonID="LinkButton6" Format="dd/MMM/yyyy">
                                                        </asp:CalendarExtender>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-sm-4 paddingLeft0">
                                            <div class="col-sm-12 paddingLeft0 paddingRight0">
                                                <div class="col-sm-6 padding0">
                                                    <div class="col-sm-5 padding0">
                                                        Vessel
                                                    </div>
                                                    <div class="col-sm-6 padding0">
                                                        <asp:TextBox ID="txtVessel" CausesValidation="false" runat="server" class="form-control"></asp:TextBox>
                                                    </div>
                                                    <div class="col-sm-1 paddingLeft0">
                                                    </div>
                                                </div>
                                                <div class="col-sm-6 paddingLeft0">
                                                    <div class="col-sm-5 padding0">
                                                        Voyage
                                                    </div>
                                                    <div class="col-sm-6 padding0">
                                                        <asp:TextBox ID="txtVoyage" CausesValidation="false" runat="server" class="form-control"></asp:TextBox>
                                                    </div>
                                                    <div class="col-sm-1 paddingLeft0">
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-sm-12 height5"></div>
                                            <div class="col-sm-12 paddingLeft0 paddingRight0">
                                                <div class="col-sm-6 padding0">
                                                    <div class="col-sm-5  padding0">
                                                        From Port
                                                    </div>
                                                    <div class="col-sm-6 padding0">
                                                        <asp:DropDownList ID="ddlFromPort" class="form-control" runat="server">
                                                        </asp:DropDownList>
                                                    </div>
                                                    <div class="col-sm-1 paddingLeft0">
                                                    </div>
                                                </div>
                                                <div class="col-sm-6 paddingLeft0">
                                                    <div class="col-sm-5 padding0">
                                                        To Port
                                                    </div>
                                                    <div class="col-sm-6 padding0">
                                                        <asp:DropDownList ID="ddlToPort" class="form-control" runat="server">
                                                        </asp:DropDownList>
                                                    </div>
                                                    <div class="col-sm-1 paddingLeft0">
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-sm-12 height5"></div>
                                            <div class="col-sm-12 paddingLeft0 paddingRight0">
                                                <div class="col-sm-6 padding0">
                                                    <div class="col-sm-5  padding0">
                                                        Final Port
                                                    </div>
                                                    <div class="col-sm-6 padding0">
                                                        <asp:DropDownList ID="ddlFinalPort" class="form-control" runat="server">
                                                        </asp:DropDownList>
                                                    </div>
                                                    <div class="col-sm-1 paddingLeft0">
                                                    </div>
                                                </div>
                                                <div class="col-sm-6 paddingLeft0">
                                                    <div class="col-sm-5 padding0">
                                                        Caring Type
                                                    </div>
                                                    <div class="col-sm-6 padding0">
                                                        <asp:DropDownList ID="ddlCaringType" CssClass="form-control" runat="server" AppendDataBoundItems="true">
                                                            <asp:ListItem Text="Select" Value="0" />
                                                        </asp:DropDownList>
                                                    </div>
                                                    <div class="col-sm-1 paddingLeft0">
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-sm-12 height5"></div>
                                            <div class="col-sm-12 paddingLeft0 paddingRight0">
                                                <div class="col-sm-6 padding0">
                                                    <div class="col-sm-5  padding0">
                                                        Country Of Origin
                                                    </div>
                                                    <div class="col-sm-6 padding0">
                                                        <asp:TextBox ID="txtCountyOfOrigin" CausesValidation="false" AutoPostBack="true" runat="server" class="form-control" OnTextChanged="txtCountyOfOrigin_TextChanged"></asp:TextBox>
                                                    </div>
                                                    <div class="col-sm-1 paddingLeft0">
                                                        <asp:LinkButton ID="btnCountyOfOrigin" runat="server" OnClick="btnCountyOfOrigin_Click">
                                                            <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                        </asp:LinkButton>
                                                    </div>
                                                </div>
                                                <div class="col-sm-6 paddingLeft0">
                                                    <div class="col-sm-5 padding0">
                                                        Exporter Country
                                                    </div>
                                                    <div class="col-sm-6 padding0">
                                                        <asp:TextBox ID="txtExporterCounty" CausesValidation="false" AutoPostBack="true" runat="server" class="form-control" OnTextChanged="txtExporterCounty_TextChanged"></asp:TextBox>
                                                    </div>
                                                    <div class="col-sm-1 paddingLeft0">
                                                        <asp:LinkButton ID="btnExporterCounty" runat="server" OnClick="btnExporterCounty_Click">
                                                            <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                        </asp:LinkButton>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-sm-12 height5"></div>
                                            <div class="col-sm-12 paddingLeft0 paddingRight0">
                                                <div class="col-sm-2 paddingLeft0">
                                                    Remark
                                                </div>
                                                <div class="col-sm-9" style="padding-left: 18px; padding-right: 32px;">
                                                    <asp:TextBox ID="txtRemark" MaxLength="100" CausesValidation="false" runat="server" class="form-control"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-sm-4 " style="padding-right: 0px;" runat="server" id="divOrderFinancingDetails">
                            <div class="panel panel-default marginBottom0">
                                <div class="panel-heading paddingtopbottom0">
                                    <div class="row">
                                        <div class="col-sm-6 labelText1">
                                            Order Financing Details
                                        </div>
                                        <div class="col-sm-2 padding0 labelText1">
                                            Shipment #
                                        </div>
                                        <div class="col-sm-4" style="margin-top: 2px;">
                                            <asp:UpdatePanel runat="server">
                                                <ContentTemplate>
                                                    <asp:TextBox runat="server" CssClass="form-control txtShSeq" ID="txtShSeq" Height="17px" />
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                    </div>
                                </div>
                                <div class="panel-body">
                                    <div class="row">
                                        <div class="col-sm-12">
                                            <div class="row">
                                                <div class="col-sm-3 labelText1 paddingRight0">
                                                    Order Financing #
                                                </div>
                                                <div class="col-sm-4 padding0">
                                                    <asp:TextBox ID="txtFinancialDocumentNo" CausesValidation="false" AutoPostBack="true" runat="server" class="form-control" OnTextChanged="txtFinancialDocumentNo_TextChanged"></asp:TextBox>
                                                </div>
                                                <div class="col-sm-1 padding0">
                                                    <asp:LinkButton ID="btnFinancialDocumentNo" runat="server" OnClick="btnFinancialDocumentNo_Click">
                                                            <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                    </asp:LinkButton>
                                                </div>

                                                <div class="col-sm-1" style="padding-left: 3px; padding-right: 6px;">
                                                    <asp:LinkButton ID="btnFinancialDocumentNoView" runat="server" OnClick="btnFinancialDocumentNoView_Click">
                                                            <span class="glyphicon glyphicon-plus" aria-hidden="true"></span>
                                                    </asp:LinkButton>
                                                </div>
                                                <div class="col-sm-1 labelText1" style="padding-left: 0px; padding-right: 0px;" runat="server" visible="false">
                                                    Seq #
                                                </div>
                                                <div class="col-sm-2 labelText1" runat="server" visible="false">
                                                    <asp:Label Text="5" ID="lblSeqNo" Style="text-align: right;" runat="server" />
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-12">
                                                    <asp:GridView ID="dgvFinancialDocs" CssClass="table table-hover table-striped" runat="server" GridLines="None" ShowHeaderWhenEmpty="True"
                                                        EmptyDataText="No data found..." AutoGenerateColumns="False" OnRowCommand="dgvFinancialDocs_RowCommand">
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="Select">
                                                                <ItemTemplate>
                                                                    <asp:CheckBox ID="chkSelect" runat="server" AutoPostBack="True" OnCheckedChanged="chkSelect_CheckedChanged" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Doc #">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblif_doc_no" runat="server" Text='<%# Bind("if_doc_no") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Reff. #">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblif_ref_no" runat="server" Text='<%# Bind("if_ref_no") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:BoundField HeaderText='Date' DataField="if_doc_dt" DataFormatString="{0:dd/MMM/yyyy}" HtmlEncode="false" />
                                                            <asp:BoundField HeaderText='Amount' DataField="if_tot_amt" DataFormatString="{0:N2}" HtmlEncode="false" />
                                                            <asp:TemplateField>
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="btnDelOrderFinance" CausesValidation="false" OnClientClick="return ConfirmDelOrfina()" OnClick="btnDelOrderFinance_Click" runat="server">
                                                                 <span class="glyphicon glyphicon-trash" aria-hidden="true"></span>
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
                            </div>
                        </div>
                        <div class="col-sm-4" style="padding-left: 1px; padding-right: 1px;">
                            <div class="panel panel-default marginBottom0">
                                <div class="panel-heading paddingtopbottom0">Invoice Details</div>
                                <div class="panel-body">
                                    <div class="row">
                                        <div class="col-sm-12">
                                            <div class="row">
                                                <div class="col-sm-5">
                                                    <div class="col-sm-5 labelText1 paddingLeft0 ">
                                                        Invoice #
                                                    </div>
                                                    <div class="col-sm-6 paddingRight5 paddingLeft0">
                                                        <asp:TextBox ID="txtInvoiceNo" CausesValidation="false" runat="server" class="form-control"></asp:TextBox>
                                                    </div>
                                                    <div class="col-sm-1 paddingRight5 paddingLeft0">
                                                    </div>
                                                    <div class="col-sm-3 paddingLeft0">
                                                    </div>
                                                </div>
                                                <div class="col-sm-5 paddingLeft0">
                                                    <div class="col-sm-3 labelText1 paddingLeft0">
                                                        Date
                                                    </div>
                                                    <div class="col-sm-7 paddingRight5 paddingLeft0">
                                                        <asp:TextBox ID="txtInvoiceDate" Enabled="false" CausesValidation="false" runat="server" CssClass="form-control"></asp:TextBox>
                                                    </div>
                                                    <div class="col-sm-2 paddingLeft0 ">
                                                        <asp:LinkButton ID="btnInvoiceDate" runat="server" CausesValidation="false">
                                                            <span class="glyphicon glyphicon-calendar" aria-hidden="true"></span>
                                                        </asp:LinkButton>
                                                        <asp:CalendarExtender ID="btnDateInvoiceDate" runat="server" TargetControlID="txtInvoiceDate"
                                                            PopupButtonID="btnInvoiceDate" Format="dd/MMM/yyyy">
                                                        </asp:CalendarExtender>
                                                    </div>
                                                </div>
                                                <div class="col-sm-1 paddingLeft0">
                                                    <asp:LinkButton ID="btnInvoiceAdd" runat="server" OnClick="btnInvoiceAdd_Click">
                                                            <span class="glyphicon glyphicon-plus" aria-hidden="true"></span>
                                                    </asp:LinkButton>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-12">
                                                    <asp:GridView ID="dgvInvoiceNums" CssClass="table table-hover table-striped" runat="server" GridLines="None" ShowHeaderWhenEmpty="True"
                                                        EmptyDataText="No data found..." AutoGenerateColumns="False" OnRowDeleting="dgvInvoiceNums_RowDeleting">
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="invoice #">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblIBS_SI_NO" runat="server" Text='<%# Bind("IBS_SI_NO") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Date">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblIBS_DT" runat="server" Text='<%# Bind("IBS_DT") %>' DataFormatString="{0:dd/MMM/yyyy}"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Status" Visible="false">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblIBS_ACT" runat="server" Text='<%# Bind("IBS_ACT") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="btnSIDelete" CausesValidation="false" OnClientClick="return ConfirmDelSI()" CommandName="Delete" runat="server">
                                                                 <span class="glyphicon glyphicon-trash" aria-hidden="true"></span>
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
                            </div>
                        </div>
                        <div class="col-sm-4 paddingLeft0">
                            <div class="panel panel-default marginBottom0">
                                <div class="panel-heading paddingtopbottom0">Container Details</div>
                                <div class="panel-body">
                                    <div class="row">
                                        <div class="col-sm-12">
                                            <div class="col-sm-2 labelText1 padding0">
                                                Type
                                            </div>
                                            <div class="col-sm-3 paddingRight5 padding0">
                                                <asp:DropDownList ID="ddlContainersType" runat="server" class="form-control">
                                                </asp:DropDownList>
                                            </div>

                                            <div class="col-sm-2 labelText1 padding0">
                                                Container #
                                            </div>
                                            <div class="col-sm-4 paddingRight5">
                                                <asp:TextBox ID="txtContainerNo" CausesValidation="false" runat="server" class="form-control" MaxLength="15"></asp:TextBox>
                                            </div>
                                            <div class="col-sm-1 paddingRight5">
                                                <asp:LinkButton ID="btnContainerAdd" runat="server" OnClick="btnContainerAdd_Click">
                                                            <span class="glyphicon glyphicon-plus" aria-hidden="true" style="font-size:15px"></span>
                                                </asp:LinkButton>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-sm-12">
                                            <asp:GridView ID="dgvContainers" CssClass="table table-hover table-striped" runat="server" GridLines="None" ShowHeaderWhenEmpty="True"
                                                EmptyDataText="No data found..." AutoGenerateColumns="False" OnRowDeleting="dgvContainers_RowDeleting">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Container Type">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblIbc_tp" runat="server" Text='<%# Bind("Ibc_tp") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Container #">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblIbc_desc" runat="server" Text='<%# Bind("Ibc_desc") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Status" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblIbc_act" runat="server" Text='<%# Bind("Ibc_act") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField>
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="btnContaiDelete" CausesValidation="false" CommandName="Delete" runat="server" OnClientClick="return ConfirmDelConta()" OnClick="btnContaiDelete_Click">
                                                                 <span class="glyphicon glyphicon-trash" aria-hidden="true"></span>
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
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>

            <div class="row">
                <div class="col-sm-12">
                    <div class="panel panel-default marginBottom0">
                        <div class="panel-heading paddingtopbottom0">Shipment Item Details</div>
                        <div class="panel-body marginBottom0 margintop0 paddingbottom0 paddingtop0">
                            <div class="row">
                                <div class="col-sm-12">
                                    <ul id="myTab" class="nav nav-tabs">
                                        <li class="active"><a href="#ItemDetails" data-toggle="tab">Item Details</a></li>
                                        <li><a href="#Costing" data-toggle="tab">Costing</a></li>
                                    </ul>
                                    <div id="myTabContent" class="tab-content">
                                        <div class="tab-pane fade in active" id="ItemDetails">
                                            <div class="row">
                                                <div class="col-sm-12">
                                                    <div class="panel panel-default marginBottom0">
                                                        <div class="panel-heading paddingtopbottom0" style="padding-bottom: 0px; padding-top: 0px">Performance Invoice Details</div>
                                                        <div class="row">
                                                            <div class="panel-body paddingtopbottom0">
                                                                <div class="col-sm-12">
                                                                    <div class="panel panel-default">
                                                                        <asp:UpdatePanel runat="server">
                                                                            <ContentTemplate>
                                                                                <div class="panel-heading">
                                                                                    <div class="row">
                                                                                        <div class="col-sm-3 ">
                                                                                            <div class="col-sm-6 paddingLeft5">
                                                                                                <div class="row paddingLeft0">
                                                                                                    <div class="col-sm-2 labelText1 paddingLeft0">
                                                                                                        Item
                                                                                                    </div>
                                                                                                    <div class="col-sm-8 paddingRight5 paddingLeft0">
                                                                                                        <asp:TextBox ID="txtItem" Style="text-transform: uppercase" AutoPostBack="true" CausesValidation="false" runat="server" class="form-control" OnTextChanged="txtItem_TextChanged"></asp:TextBox>
                                                                                                    </div>
                                                                                                    <div class="col-sm-2 paddingLeft0">
                                                                                                        <asp:LinkButton ID="btnItemSearch" runat="server" OnClick="btnItemSearch_Click">
                                                                                                        <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                                                                        </asp:LinkButton>
                                                                                                    </div>

                                                                                                </div>
                                                                                            </div>
                                                                                            <div class="col-sm-6">
                                                                                                <div class="row">
                                                                                                    <div class="col-sm-3 labelText1">
                                                                                                        Qty
                                                                                                    </div>
                                                                                                    <div class="col-sm-8 paddingRight5">
                                                                                                        <asp:TextBox ID="txtQty" AutoPostBack="true" Style="text-align: right" onkeydown="return jsDecimals(event);" CausesValidation="false" runat="server" class="form-control" OnTextChanged="txtQty_TextChanged" MaxLength="10"></asp:TextBox>
                                                                                                    </div>
                                                                                                    <div class="col-sm-1 paddingLeft0">
                                                                                                    </div>
                                                                                                </div>
                                                                                            </div>
                                                                                        </div>
                                                                                        <div class="col-sm-4 paddingLeft0">
                                                                                            <div class="col-sm-6">
                                                                                                <div class="row">
                                                                                                    <div class="col-sm-5 labelText1 paddingLeft0">
                                                                                                        Price Basis
                                                                                                    </div>
                                                                                                    <div class="col-sm-7 paddingRight5 paddingLeft0">
                                                                                                        <asp:DropDownList ID="ddlPriceType" runat="server" class="form-control">
                                                                                                            <asp:ListItem Text=" - - Select - - " Value="0" Selected="True"></asp:ListItem>
                                                                                                            <asp:ListItem Text="Charge" Value="C"></asp:ListItem>
                                                                                                            <asp:ListItem Text="FOC" Value="F"></asp:ListItem>
                                                                                                        </asp:DropDownList>
                                                                                                    </div>
                                                                                                </div>
                                                                                            </div>
                                                                                            <div class="col-sm-6">
                                                                                                <div class="row">
                                                                                                    <div class="col-sm-5 labelText1">
                                                                                                        Unit Price
                                                                                                    </div>
                                                                                                    <div class="col-sm-7 paddingRight5 paddingLeft0">
                                                                                                        <asp:TextBox ID="txtUnitPrice" AutoPostBack="true" Style="text-align: right" onkeydown="return jsDecimals(event);" CausesValidation="false" runat="server" class="form-control" OnTextChanged="txtUnitPrice_TextChanged"></asp:TextBox>
                                                                                                    </div>
                                                                                                </div>
                                                                                            </div>
                                                                                        </div>
                                                                                        <div class="col-sm-2 paddingLeft0">
                                                                                            <div class="col-sm-12">
                                                                                                <div class="row">
                                                                                                    <div class="col-sm-4 labelText1 paddingLeft0">
                                                                                                        Sub Total
                                                                                                    </div>
                                                                                                    <div class="col-sm-8 paddingRight5">
                                                                                                        <asp:TextBox ID="txtUnitTotal" ReadOnly="true" Style="text-align: right" onkeydown="return jsDecimals(event);" CausesValidation="false" runat="server" class="form-control"></asp:TextBox>
                                                                                                    </div>
                                                                                                </div>
                                                                                            </div>
                                                                                        </div>
                                                                                        <div class="col-sm-3 paddingLeft0">
                                                                                            <div class="col-sm-8 paddingLeft0">
                                                                                                <div class="row">
                                                                                                    <div class="col-sm-3 labelText1">
                                                                                                        Tag 
                                                                                                    </div>
                                                                                                    <div class="col-sm-9 paddingRight5">
                                                                                                        <asp:DropDownList ID="ddlTag" AutoPostBack="true" OnSelectedIndexChanged="ddlTag_SelectedIndexChanged" runat="server" class="form-control">
                                                                                                            <asp:ListItem Text=" - - Select - - " Value="0" Selected="True"></asp:ListItem>
                                                                                                            <asp:ListItem Text="Normal" Value="N" />
                                                                                                            <asp:ListItem Text="Special Project" Value="S" />
                                                                                                        </asp:DropDownList>
                                                                                                    </div>
                                                                                                </div>
                                                                                            </div>
                                                                                            <div class="col-sm-4 padding0">
                                                                                                <div class="col-sm-3 padding0">
                                                                                                    <asp:LinkButton ID="btnAddNewtems" runat="server" OnClick="btnAddNewtems_Click">
                                                                                                        <span class="glyphicon glyphicon-plus" aria-hidden="true"></span>
                                                                                                    </asp:LinkButton>
                                                                                                </div>
                                                                                                <div class="col-sm-9 padding0">
                                                                                                    <asp:Button Text="Excel Upload" ID="btnExcelDataUpload" OnClick="btnExcelDataUpload_Click" runat="server" />
                                                                                                </div>
                                                                                            </div>
                                                                                        </div>
                                                                                    </div>
                                                                                    <div class="row">
                                                                                        <div class="col-sm-2">
                                                                                            <div class="col-sm-2">
                                                                                                <asp:CheckBox ID="chkItem" runat="server" Width="5px"></asp:CheckBox>
                                                                                            </div>
                                                                                            O/F Items
                                                                                        </div>
                                                                                    </div>
                                                                                    <div class="row">
                                                                                        <div class="panel-body">
                                                                                            <div class="col-sm-1 labelText1">Description :</div>
                                                                                            <div class="col-sm-3 labelText1">
                                                                                                <asp:Label ID="lblDescription" Style="color: purple; font-weight: bold;" Text="text" runat="server" />
                                                                                            </div>
                                                                                            <div class="col-sm-1 labelText1">Brand :</div>
                                                                                            <div class="col-sm-3 labelText1">
                                                                                                <asp:Label ID="lblBrand" Style="color: purple; font-weight: bold;" Text="text" runat="server" />
                                                                                            </div>
                                                                                            <div class="col-sm-1 labelText1">UOM :</div>
                                                                                            <div class="col-sm-3 labelText1">
                                                                                                <asp:Label ID="lblUOM" Style="color: purple; font-weight: bold;" Text="text" runat="server" />
                                                                                            </div>
                                                                                        </div>
                                                                                    </div>
                                                                                </div>
                                                                            </ContentTemplate>
                                                                        </asp:UpdatePanel>
                                                                    </div>
                                                                </div>
                                                                <div class="col-sm-12">
                                                                    <asp:ScriptManagerProxy ID="ScriptManagerProxy10" runat="server"></asp:ScriptManagerProxy>
                                                                    <asp:UpdatePanel runat="server">
                                                                        <ContentTemplate>
                                                                            <asp:GridView ID="dgvInvoiceDetails" CssClass="table table-hover table-striped" runat="server" GridLines="None" ShowHeaderWhenEmpty="True"
                                                                                EmptyDataText="No data found..." AutoGenerateColumns="False"
                                                                                OnRowCancelingEdit="dgvInvoiceDetails_RowCancelingEdit" OnRowDeleting="dgvInvoiceDetails_RowDeleting" OnRowEditing="dgvInvoiceDetails_RowEditing" OnRowUpdating="dgvInvoiceDetails_RowUpdating">
                                                                                <Columns>
                                                                                    <asp:BoundField HeaderText='Seq' DataField="Ibi_seq_no" Visible="false" />
                                                                                    <asp:TemplateField HeaderText="Tmp_ibi_line" Visible="false">
                                                                                        <ItemTemplate>
                                                                                            <div style="margin-top: -2px">
                                                                                                <asp:Label ID="lblTmp_ibi_line" runat="server" Text='<%# Bind("Tmp_ibi_line") %>'></asp:Label>
                                                                                            </div>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="Ibi_line" Visible="false">
                                                                                        <ItemTemplate>
                                                                                            <div style="margin-top: -2px">
                                                                                                <asp:Label ID="lblIbi_line" runat="server" Text='<%# Bind("Ibi_line") %>'></asp:Label>
                                                                                            </div>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:BoundField HeaderText='Ibi_doc_no ' DataField="Ibi_doc_no" Visible="false" />
                                                                                    <asp:BoundField HeaderText='Ibi_ref_line ' DataField="Ibi_ref_line" Visible="false" />
                                                                                    <asp:BoundField HeaderText='Ibi_f_line' DataField="Ibi_f_line" Visible="false" />
                                                                                    <asp:TemplateField HeaderText="PI #">
                                                                                        <EditItemTemplate>
                                                                                            <asp:TextBox ID="txtIbi_pi_no" ReadOnly="true" runat="server" Text='<%# Bind("Ibi_pi_no") %>'></asp:TextBox>
                                                                                            <asp:Label ID="lblIbi_pi_no2" runat="server" Text='<%# Bind("Ibi_pi_no") %>' Visible="false"></asp:Label>
                                                                                        </EditItemTemplate>
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lblIbi_pi_no" runat="server" Text='<%# Bind("Ibi_pi_no") %>'></asp:Label>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="PI #" Visible="false">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lblIbi_pi_line" runat="server" Text='<%# Bind("Ibi_pi_line") %>'></asp:Label>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>

                                                                                    <asp:TemplateField HeaderText="Item Code">
                                                                                        <EditItemTemplate>
                                                                                            <asp:TextBox ID="txtIbi_itm_cd" runat="server" Text='<%# Bind("Ibi_itm_cd") %>'></asp:TextBox>
                                                                                            <asp:LinkButton ID="btnItemSearchGrid" runat="server" OnClick="btnItemSearchGrid_Click">
                                                                                                         <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                                                            </asp:LinkButton>
                                                                                        </EditItemTemplate>
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lblIbi_itm_cd" runat="server" Text='<%# Bind("Ibi_itm_cd") %>'></asp:Label>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:BoundField HeaderText='Item Description' DataField="MI_SHORTDESC" ReadOnly="true" />
                                                                                    <%--<asp:BoundField HeaderText='Part No' DataField="mi_part_no" ReadOnly="true" />--%>
                                                                                    <asp:BoundField HeaderText='Item Model' DataField="Ibi_model" ReadOnly="true" />
                                                                                    <asp:BoundField HeaderText='Color' DataField="MI_COLOR_EXT" ReadOnly="true" /> 
                                                                                    <asp:TemplateField HeaderText="Price Basis">
                                                                                        <EditItemTemplate>
                                                                                            <asp:DropDownList ID="ddlSelectPriceBase" runat="server">
                                                                                                <asp:ListItem Text="Charge" Value="C" />
                                                                                                <asp:ListItem Text="FOC" Value="F" />
                                                                                            </asp:DropDownList>
                                                                                        </EditItemTemplate>
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lblIbi_tp_Desc" runat="server" Text='<%# Bind("Ibi_tp_Desc") %>'></asp:Label>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="Price Basis" Visible="false">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lblibi_tp" runat="server" Text='<%# Bind("ibi_tp") %>'></asp:Label>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:BoundField HeaderText='UOM' DataField="MI_ITM_UOM" ReadOnly="true" />
                                                                                    <asp:TemplateField HeaderText="Tag">
                                                                                        <EditItemTemplate>
                                                                                            <asp:DropDownList ID="ddlItemTag" runat="server">
                                                                                                <asp:ListItem Text="Normal" Value="N" />
                                                                                                <asp:ListItem Text="Special Project" Value="S" />
                                                                                            </asp:DropDownList>
                                                                                        </EditItemTemplate>
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lblIbi_tag_Desc" runat="server" Text='<%# Bind("Ibi_tag_Desc") %>'></asp:Label>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="tag" Visible="false">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lblIbi_tag" runat="server" Text='<%# Bind("Ibi_tag") %>'></asp:Label>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>

                                                                                    <asp:TemplateField HeaderText="tag" Visible="false">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lblIbi_qty_back" runat="server" Text='<%# Bind("Ibi_qty") %>'></asp:Label>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="Project Name">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lblIbi_project_name" runat="server" Text='<%# Bind("Ibi_project_name") %>'></asp:Label>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="Order Qty">
                                                                                        <EditItemTemplate>
                                                                                            <asp:TextBox ID="txtIbi_qty" Style="text-align: right" runat="server" DataFormatString="{0:N2}" onkeydown="return jsDecimals(event);" Text='<%# Bind("Ibi_qty","{0:N2}") %>'></asp:TextBox>
                                                                                        </EditItemTemplate>
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lblIbi_qty" DataFormatString="{0:N2}" runat="server" Text='<%# Bind("Ibi_qty","{0:N2}") %>'></asp:Label>
                                                                                        </ItemTemplate>
                                                                                        <HeaderStyle HorizontalAlign="Right" />
                                                                                        <HeaderStyle CssClass="gridHeaderAlignRight" />
                                                                                        <ItemStyle HorizontalAlign="Right" />
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="Unit Price">
                                                                                        <EditItemTemplate>
                                                                                            <asp:TextBox ID="txtIbi_pi_unit_rt" Style="text-align: right" DataFormatString="{0:N5}" onkeydown="return jsDecimals(event);" runat="server" Text='<%# Bind("Ibi_unit_rt") %>'></asp:TextBox>
                                                                                        </EditItemTemplate>
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lblIbi_pi_unit_rt" DataFormatString="{0:N5}" runat="server" Text='<%# Bind("Ibi_unit_rt","{0:N5}") %>'></asp:Label>
                                                                                        </ItemTemplate>
                                                                                        <HeaderStyle HorizontalAlign="Right" />
                                                                                        <HeaderStyle CssClass="gridHeaderAlignRight" />
                                                                                        <ItemStyle HorizontalAlign="Right" />
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="Sub Total">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lblIbi_anal_5" DataFormatString="{0:N5}" runat="server" Text='<%# Bind("Ibi_anal_5","{0:N5}") %>'></asp:Label>
                                                                                        </ItemTemplate>
                                                                                        <HeaderStyle HorizontalAlign="Right" />
                                                                                        <HeaderStyle CssClass="gridHeaderAlignRight" />
                                                                                        <ItemStyle HorizontalAlign="Right" />
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="" Visible="true">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label Width="10px" runat="server" Text=' '></asp:Label>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="LC #" Visible="true">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lblLcNo" runat="server" Text='<%# Bind("Ibi_fin_no") %>'></asp:Label>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="Seq #" Visible="true">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lblSeqNo" runat="server" Text='<%# Bind("Ibi_anal_1") %>'></asp:Label>
                                                                                        </ItemTemplate>
                                                                                        <EditItemTemplate>
                                                                                            <asp:TextBox ID="txtSeqNo" Style="text-align: right" DataFormatString="{0:N5}" onkeydown="return jsDecimals(event);" runat="server" Text='<%# Bind("Ibi_anal_1") %>'></asp:TextBox>
                                                                                        </EditItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField>
                                                                                        <ItemTemplate>
                                                                                            <div id="editbtndiv" style="width: 1px">
                                                                                                <asp:LinkButton ID="lbtnedititem" CausesValidation="false" CommandName="Edit" runat="server" OnClick="lbtnedititem_Click">
                                                                                                        <span class="glyphicon glyphicon-pencil" aria-hidden="true" style="font-size:8px"></span>
                                                                                                </asp:LinkButton>
                                                                                            </div>
                                                                                        </ItemTemplate>
                                                                                        <EditItemTemplate>
                                                                                            <asp:LinkButton ID="lbtnUpdateitem" CausesValidation="false" runat="server" OnClick="OnUpdate">
                                                                                                        <span class="glyphicon glyphicon-ok" aria-hidden="true" style="font-style:oblique" ></span>
                                                                                            </asp:LinkButton>
                                                                                            <asp:LinkButton ID="lbtncanceledit" CausesValidation="false" OnClick="OnCancel" runat="server">
                                                                                                        <span class="glyphicon glyphicon-remove" aria-hidden="true"></span>
                                                                                            </asp:LinkButton>
                                                                                        </EditItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField>
                                                                                        <HeaderTemplate>
                                                                                            <asp:LinkButton ID="lbtnDelAllItem" CausesValidation="false" CommandName="Delete" runat="server"
                                                                                                OnClick="lbtnDelAllItem_Click" OnClientClick="return ConfDelAll()">
                                                                                                        <span class="glyphicon glyphicon-trash" aria-hidden="true"></span>
                                                                                            </asp:LinkButton>
                                                                                        </HeaderTemplate>
                                                                                        <ItemTemplate>
                                                                                            <asp:LinkButton ID="btnRecDelete" CausesValidation="false" CommandName="Delete" runat="server" OnClick="btnRecDelete_Click" OnClientClick="return ConfirmDelinvoice()">
                                                                                                        <span class="glyphicon glyphicon-trash" aria-hidden="true"></span>
                                                                                            </asp:LinkButton>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField>
                                                                                        <ItemTemplate>
                                                                                            <asp:LinkButton ID="lbtnItemChg" CausesValidation="false" runat="server" OnClick="lbtnItemChg_Click">
                                                                                                        <span class="glyphicon glyphicon-edit" aria-hidden="true"></span>
                                                                                            </asp:LinkButton>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="Is New Item" Visible="false">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lblisNewItem" runat="server" Text='<%# Bind("isNewItem") %>'></asp:Label>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="Ibi_stus" Visible="false">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lblIbi_stus" runat="server" Text='<%# Bind("Ibi_stus") %>'></asp:Label>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>

                                                                                </Columns>
                                                                            </asp:GridView>
                                                                        </ContentTemplate>
                                                                    </asp:UpdatePanel>
                                                                </div>
                                                                <div class="col-sm-12">
                                                                    <div class="panel panel-default padding0 marginBottom0">
                                                                        <div class="panel-heading">
                                                                            <div class="row">
                                                                                <div class="col-sm-12">
                                                                                    <div class="col-sm-6">
                                                                                    </div>
                                                                                    <div class="col-sm-6">
                                                                                        <div class="row">
                                                                                            <asp:UpdatePanel runat="server">
                                                                                                <ContentTemplate>
                                                                                                    <div class="col-sm-3 labelText1">
                                                                                                        Total Order Qty
                                                                                                    </div>
                                                                                                    <div class="col-sm-3">
                                                                                                        <asp:TextBox ID="txtTotalOrderQty" ReadOnly="true" CausesValidation="false" runat="server" Style="text-align: right" class="form-control"></asp:TextBox>
                                                                                                    </div>
                                                                                                    <div class="col-sm-3 labelText1 ">
                                                                                                        Total Order Value In
                                                                                                    </div>
                                                                                                    <div class="col-sm-3">
                                                                                                        <asp:TextBox ID="txtBlValue" ReadOnly="true" Visible="false" CausesValidation="false" runat="server" Style="text-align: right" class="form-control"></asp:TextBox>
                                                                                                        <asp:TextBox ID="txtTotalOrderValueIn" ReadOnly="true" CausesValidation="false" runat="server" Style="text-align: right" class="form-control"></asp:TextBox>
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
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="tab-pane fade" id="Costing">
                                            <div class="row">
                                                <div class="col-sm-12">
                                                    <div class="panel panel-default">
                                                        <div class="panel-heading">Costing Details</div>
                                                        <div class="row">
                                                            <div class="panel-body">
                                                                <div class="col-sm-6">
                                                                    <asp:ScriptManagerProxy ID="ScriptManagerProxy1" runat="server"></asp:ScriptManagerProxy>
                                                                    <asp:UpdatePanel runat="server">
                                                                        <ContentTemplate>
                                                                            <asp:GridView ID="dgvCostItems" CssClass="table table-hover table-striped" runat="server" GridLines="None" ShowHeaderWhenEmpty="True"
                                                                                EmptyDataText="No data found..." AutoGenerateColumns="False" OnRowEditing="dgvCostItems_RowEditing" OnRowUpdating="dgvCostItems_RowUpdating" OnRowCancelingEdit="dgvCostItems_RowCancelingEdit" OnRowDeleting="dgvCostItems_RowDeleting">
                                                                                <Columns>
                                                                                    <asp:BoundField HeaderText='Category' DataField="Ibcs_ele_cat" Visible="false" />
                                                                                    <asp:BoundField HeaderText='Type' DataField="Ibcs_ele_tp" Visible="false" />
                                                                                    <asp:TemplateField HeaderText="Code">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lblIbcs_ele_cd" runat="server" Text='<%# Bind("Ibcs_ele_cd") %>'></asp:Label>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="Value">
                                                                                        <EditItemTemplate>
                                                                                            <asp:TextBox ID="txtIbcs_amt" Style="text-align: right" DataFormatString="{0:N2}" onkeydown="return jsDecimals(event);" runat="server" Text='<%# Bind("Ibcs_amt") %>' MaxLength="10"></asp:TextBox>
                                                                                        </EditItemTemplate>
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lblIbcs_amt" runat="server" Text='<%# Bind("Ibcs_amt") %>'></asp:Label>
                                                                                        </ItemTemplate>
                                                                                        <HeaderStyle HorizontalAlign="Right" />
                                                                                        <HeaderStyle CssClass="gridHeaderAlignRight" />
                                                                                        <ItemStyle HorizontalAlign="Right" />
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField>
                                                                                        <ItemTemplate>
                                                                                            <div id="editbtndiv2" style="width: 1px">
                                                                                                <asp:LinkButton ID="lbtnedititem" CausesValidation="false" CommandName="Edit" runat="server" OnClick="lbtnedititem_Click1">
                                                                                                        <span class="glyphicon glyphicon-pencil" aria-hidden="true"></span>
                                                                                                </asp:LinkButton>
                                                                                            </div>
                                                                                        </ItemTemplate>
                                                                                        <EditItemTemplate>
                                                                                            <asp:LinkButton ID="lbtnUpdateitem" CausesValidation="false" runat="server" OnClick="OnUpdateCost">
                                                                                                        <span class="glyphicon glyphicon-ok" aria-hidden="true"></span>
                                                                                            </asp:LinkButton>
                                                                                            <asp:LinkButton ID="lbtncanceledit" CausesValidation="false" OnClick="OnCancelCost" runat="server">
                                                                                                        <span class="glyphicon glyphicon-remove" aria-hidden="true"></span>
                                                                                            </asp:LinkButton>
                                                                                        </EditItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="Status" Visible="false">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lblibcs_act" runat="server" Text='<%# Bind("ibcs_act") %>'></asp:Label>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                </Columns>
                                                                            </asp:GridView>
                                                                        </ContentTemplate>
                                                                    </asp:UpdatePanel>
                                                                </div>
                                                                <div class="col-sm-6">
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
            <%-- Item code chag --%>
            <asp:UpdatePanel runat="server" ID="UpdatePanel4">
                <ContentTemplate>
                    <asp:Button ID="btnItmCdChg" runat="server" Text="Button" Style="display: none;" />
                    <asp:ModalPopupExtender ID="popItmCdChg" runat="server" Enabled="True" TargetControlID="btnItmCdChg"
                        PopupControlID="pnlItmCdChg" PopupDragHandleControlID="PopupHeader" Drag="true" BackgroundCssClass="modalBackground">
                    </asp:ModalPopupExtender>
                </ContentTemplate>
            </asp:UpdatePanel>
            <asp:UpdateProgress ID="UpdateProgress3" DisplayAfter="10" runat="server" AssociatedUpdatePanelID="upItmCdChg">
                <ProgressTemplate>
                    <div class="divWaiting">
                        <asp:Label ID="lbldWaidt10" runat="server"
                            Text="Please wait... " />
                        <asp:Image ID="imgdWaidt10" runat="server"
                            ImageAlign="Middle" ImageUrl="~/images/icons/ajax-loader.gif" />
                    </div>
                </ProgressTemplate>
            </asp:UpdateProgress>
            <asp:Panel runat="server" ID="pnlItmCdChg">
                <asp:UpdatePanel runat="server" ID="upItmCdChg">
                    <ContentTemplate>
                        <div runat="server" id="Div7" class="panel panel-primary" style="padding: 1px;">
                            <div class="panel panel-default" style="height: 90px; width: 350px;">
                                <div class="panel-heading" style="height: 25px;">
                                    <div class="col-sm-8 padding0">
                                        <strong>Change Item Code</strong>
                                    </div>
                                </div>
                                <div class="panel panel-body">
                                    <div class="row" style="height: 10px;">
                                    </div>
                                    <div class="row">
                                        <div class="col-sm-12">
                                            <div class="col-sm-2 padding0 labelText1">
                                                Item Code
                                            </div>
                                            <div class="col-sm-8 padding0">
                                                <asp:TextBox ID="txtchgItmCd" AutoPostBack="true" OnTextChanged="txtchgItmCd_TextChanged" CssClass="form-control" runat="server" />
                                            </div>
                                            <div class="col-sm-1 padding3">
                                                <asp:LinkButton ID="lbtnSerItemCd" runat="server" OnClick="lbtnSerItemCd_Click">
                                                      <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                </asp:LinkButton>
                                            </div>

                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-sm-12">
                                            <div class="col-sm-7 labelText1">
                                            </div>
                                            <div class="col-sm-2 text-right padding0 labelText1">
                                                <asp:Button Text="Update" ID="btnItmCdChgOk" OnClick="btnItmCdChgOk_Click" runat="server" />
                                            </div>
                                            <div class="col-sm-2 text-right padding0 labelText1">
                                                <asp:Button Text="Cancel" ID="ItmCdChgNo" OnClick="ItmCdChgNo_Click" runat="server" />
                                            </div>
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
                    <asp:Button ID="Button3" runat="server" Text="Button" Style="display: none;" />
                    <asp:ModalPopupExtender ID="mpUserPopup" runat="server" Enabled="True" TargetControlID="Button3"
                        PopupControlID="pnlpopup" PopupDragHandleControlID="divHeader" Drag="true" BackgroundCssClass="modalBackground">
                    </asp:ModalPopupExtender>
                    <asp:Panel runat="server" ID="pnlpopup" DefaultButton="lbtnSearch" Style="display: none;">
                        <div runat="server" id="test" class="panel panel-default height400 width700">
                            <asp:Label ID="lblvalue" runat="server" Text="Label" Visible="false"></asp:Label>
                            <div class="panel panel-default">
                                <div class="panel-heading" id="divHeader">
                                    <asp:LinkButton ID="btnClose" runat="server" OnClick="btnClose_Click">
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
                                            <div class="col-sm-3 paddingRight5">
                                                <asp:DropDownList ID="ddlSearchbykey" runat="server" class="form-control">
                                                </asp:DropDownList>
                                            </div>

                                            <div class="col-sm-2 labelText1">
                                                Search by word
                                            </div>
                                            <asp:UpdatePanel runat="server">
                                                <ContentTemplate>
                                                    <div class="col-sm-4 paddingRight5">
                                                        <asp:TextBox ID="txtSearchbyword" CausesValidation="false" placeholder="Search by word" class="form-control" AutoPostBack="true" runat="server" OnTextChanged="txtSearchbyword_TextChanged"></asp:TextBox>
                                                    </div>
                                                    <div class="col-sm-1 paddingLeft0">
                                                        <asp:LinkButton ID="lbtnSearch" runat="server" OnClick="lbtnSearch_Click">
                                                    <span class="glyphicon glyphicon-search" aria-hidden="true"  ></span>
                                                        </asp:LinkButton>
                                                    </div>
                                                </ContentTemplate>
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
                                            <asp:GridView ID="grdResult" CausesValidation="false" runat="server" AllowPaging="True" GridLines="None" CssClass="table table-hover table-striped" PageSize="10" PagerStyle-CssClass="cssPager" OnSelectedIndexChanged="grdResult_SelectedIndexChanged" OnPageIndexChanging="grdResult_PageIndexChanging">
                                                <Columns>
                                                    <asp:ButtonField Text="select" CommandName="Select" ItemStyle-Width="10" />
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </asp:Panel>
                </ContentTemplate>
            </asp:UpdatePanel>

            <asp:UpdatePanel runat="server" ID="pnlAdddata">
                <ContentTemplate>
                    <asp:Button ID="Button2" runat="server" Text="Button" Style="display: none;" />
                    <asp:ModalPopupExtender ID="mpPerfomaInvoices" runat="server" Enabled="True" TargetControlID="Button2"
                        PopupControlID="pnlPerfomaInvoices" CancelControlID="btnClosePIs" PopupDragHandleControlID="div2" Drag="true" BackgroundCssClass="modalBackground">
                    </asp:ModalPopupExtender>
                </ContentTemplate>
            </asp:UpdatePanel>
            <asp:Panel runat="server" ID="pnlPerfomaInvoices">
                <asp:UpdateProgress ID="UpdateProgress2" DisplayAfter="10"
                    runat="server" AssociatedUpdatePanelID="upPnlAddData">
                    <ProgressTemplate>
                        <div class="divWaiting">
                            <asp:Label ID="lblWait2" runat="server"
                                Text="Please wait... " />
                            <asp:Image ID="imgWait2" runat="server"
                                ImageAlign="Middle" ImageUrl="~/images/icons/ajax-loader.gif" />
                        </div>
                    </ProgressTemplate>
                </asp:UpdateProgress>
                <asp:UpdatePanel runat="server" ID="upPnlAddData">
                    <ContentTemplate>
                        <div runat="server" id="Div1" class="panel panel-primary Mheight padding0">
                            <div class="panel panel-default">
                                <div class="panel-heading" id="div2" style="height: 28px">
                                    <div class="col-sm-5">
                                        Performa Invoices
                                    </div>
                                    <div class="col-sm-5">
                                    </div>
                                    <div class="col-sm-1">
                                        <asp:LinkButton ID="btnAdd" runat="server" OnClick="btnAdd_Click">
                                            <span class="glyphicon glyphicon-plus" aria-hidden="true">Add</span>
                                        </asp:LinkButton>
                                    </div>
                                    <div class="col-sm-1">
                                        <asp:LinkButton ID="btnClosePIs" runat="server" OnClick="btnClosePIs_Click">
                                            <span class="glyphicon glyphicon-remove" aria-hidden="true">Close</span>
                                        </asp:LinkButton>
                                    </div>
                                </div>
                                <div class="panel-body">
                                    <div class="col-sm-12">
                                        <asp:UpdatePanel runat="server">
                                            <ContentTemplate>
                                                <asp:GridView ID="dgvPerfomaInvoices" CssClass="table table-hover table-striped" runat="server" GridLines="None" ShowHeaderWhenEmpty="True" EmptyDataText="No data found..." AutoGenerateColumns="False">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Select">
                                                            <HeaderTemplate>
                                                                <asp:CheckBox ID="chkSelectAllPis" AutoPostBack="true" runat="server" OnCheckedChanged="chkSelectAllPis_CheckedChanged" />
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:CheckBox ID="chkSelectPIs" runat="server" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Seq Num" Visible="False">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblIfp_seq_no" runat="server" Text='<%# Bind("Ifp_seq_no") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Doc Number" Visible="False">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblIfp_doc_no" runat="server" Text='<%# Bind("Ifp_doc_no") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:BoundField HeaderText='Line Num' DataField="Ifp_line" Visible="false" />
                                                        <asp:TemplateField HeaderText="PI Seq" Visible="False">
                                                            <ItemTemplate>
                                                                <asp:Label ID="Label3" runat="server" Text='<%# Bind("Ifp_pi_seq_no") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="PI Num">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblIfp_pi_no" runat="server" Text='<%# Bind("Ifp_pi_no") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:BoundField HeaderText='Total Amount' DataField="Ifp_tot_amt" DataFormatString="{0:N2}" HtmlEncode="False" />
                                                        <asp:BoundField HeaderText='Total Deal Amount' DataField="Ifp_tot_amt_deal" DataFormatString="{0:N2}" HtmlEncode="false" />
                                                        <asp:BoundField HeaderText='Date' DataField="Ifp_cre_dt" DataFormatString="{0:dd/MMM/yyyy}" HtmlEncode="false" />
                                                    </Columns>
                                                </asp:GridView>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </asp:Panel>


        </div>
    </div>

    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <asp:Button ID="tbnSaveMPosd" runat="server" Text="btnConf" Style="display: none;" />
            <asp:ModalPopupExtender ID="MoPopupGu" runat="server" Enabled="True" TargetControlID="tbnSaveMPosd"
                PopupControlID="pnlguupdate" CancelControlID="lbtncancelre" Drag="true" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>
        </ContentTemplate>
    </asp:UpdatePanel>

    <asp:Panel runat="server" ID="pnlguupdate" Style="display: none">
        <div runat="server" id="Div17" class="panel panel-default width700 height100">
            <asp:UpdatePanel runat="server">
                <ContentTemplate>
                    <div class="panel panel-default">
                        <div class="panel-heading">


                            <asp:LinkButton ID="lbtncancelre" runat="server">
                             <span class="glyphicon glyphicon-remove" aria-hidden="true">Close</span>
                            </asp:LinkButton>
                            <div class="col-sm-11">
                                More Details
                            </div>
                            <div class="col-sm-1">
                            </div>

                        </div>
                        <div class="panel-body">



                            <div class="row">
                                <div class="col-sm-4 paddingRight5">
                                    Shipping guarantee No
                                </div>
                                <div class="col-sm-6 paddingRight5">
                                    <asp:TextBox ID="txtguno" CausesValidation="false" class="form-control" runat="server"></asp:TextBox>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-12 height5">
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-4 paddingRight5">
                                    Issue Date
                                </div>
                                <div class="col-sm-4 paddingRight5">
                                    <asp:TextBox ID="txtissuedate" Enabled="false" CausesValidation="false" runat="server" CssClass="txtissuedate form-control"></asp:TextBox>
                                </div>
                                <div class="col-sm-1 paddingLeft0">
                                    <asp:LinkButton ID="lbtnissdate" runat="server" CausesValidation="false">
                                                            <span class="glyphicon glyphicon-calendar" aria-hidden="true"></span>
                                    </asp:LinkButton>
                                    <asp:CalendarExtender ID="CalendarExtender6" runat="server" TargetControlID="txtissuedate"
                                        OnClientDateSelectionChanged="checkDate"
                                        PopupButtonID="lbtnissdate" Format="dd/MMM/yyyy">
                                    </asp:CalendarExtender>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-12 height5">
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-4 paddingRight5">
                                    Expire Date
                                </div>
                                <div class="col-sm-4 paddingRight5">
                                    <asp:TextBox ID="txtexDate" Enabled="false" CausesValidation="false" runat="server" CssClass="txtexDate form-control"></asp:TextBox>
                                </div>
                                <div class="col-sm-1 paddingLeft0">
                                    <asp:LinkButton ID="lbtnexdate" runat="server" CausesValidation="false">
                                                            <span class="glyphicon glyphicon-calendar" aria-hidden="true"></span>
                                    </asp:LinkButton>
                                    <asp:CalendarExtender ID="CalendarExtender5" runat="server" TargetControlID="txtexDate"
                                        PopupButtonID="lbtnexdate" Format="dd/MMM/yyyy">
                                    </asp:CalendarExtender>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-12 height5">
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-4 paddingRight5">
                                    Cancel Date
                                </div>
                                <div class="col-sm-1 labelText1">

                                    <asp:CheckBox ID="chkIscancel" Text=" " AutoPostBack="true" runat="server" OnCheckedChanged="chkIscancel_CheckedChanged" />
                                </div>
                                <asp:Panel runat="server" ID="pnlcancl" Visible="false">
                                    <div class="col-sm-4 paddingRight5">
                                        <asp:TextBox ID="txtCancel" Enabled="false" CausesValidation="false" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                    <div class="col-sm-1 paddingLeft0">
                                        <asp:LinkButton ID="lbtncanceldate" runat="server" CausesValidation="false">
                                                            <span class="glyphicon glyphicon-calendar" aria-hidden="true"></span>
                                        </asp:LinkButton>
                                        <asp:CalendarExtender ID="CalendarExtender7" runat="server" TargetControlID="txtCancel"
                                            PopupButtonID="lbtncanceldate" Format="dd/MMM/yyyy">
                                        </asp:CalendarExtender>
                                    </div>
                                </asp:Panel>
                            </div>
                            <div class="row">
                                <div class="col-sm-12 height20">
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-4 ">
                                </div>
                                <div class="col-sm-2 ">
                                    <asp:Button ID="btnupdate" runat="server" Text="Update" Width="80px" CausesValidation="false" OnClientClick="return ConfirmUpdateBL()" class="btn btn-default btn-xs" OnClick="btnupdate_Click" />
                                </div>
                            </div>

                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </asp:Panel>




    <asp:UpdatePanel ID="UpdatePanel21" runat="server">
        <ContentTemplate>
            <asp:Button ID="Button7" runat="server" Text="Button" Style="display: none;" />
            <asp:ModalPopupExtender ID="PopupConfBox" runat="server" Enabled="True" TargetControlID="Button7"
                PopupControlID="pnlConfBox" PopupDragHandleControlID="PopupHeader" Drag="true" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:Panel ID="pnlConfBox" runat="server" align="center">
        <div runat="server" id="Div10" class="panel panel-info height120 width250">
            <asp:Label ID="Label6" runat="server" Text="Label" Visible="false"></asp:Label>
            <asp:ScriptManagerProxy ID="ScriptManagerProxy19" runat="server"></asp:ScriptManagerProxy>
            <asp:UpdatePanel ID="UpdatePanel22" runat="server">
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
                                <asp:Button ID="btnok" runat="server" Text="yes" Width="80px" CausesValidation="false" class="btn btn-primary btn-xs" OnClick="btnok_Click" />
                            </div>
                            <div class="col-sm-2 ">
                                <asp:Button ID="btnno" runat="server" Text="No" Width="80px" CausesValidation="false" class="btn btn-primary btn-xs" OnClick="btnno_Click" />
                            </div>
                        </div>
                    </div>

                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </asp:Panel>
    <asp:UpdatePanel ID="UpdatePanel25" runat="server">
        <ContentTemplate>
            <asp:Button ID="Button1" runat="server" Text="Button" Style="display: none;" />
            <asp:ModalPopupExtender ID="SbuPopup" runat="server" Enabled="True" TargetControlID="Button1"
                PopupControlID="pnlSBU" PopupDragHandleControlID="PopupHeader" Drag="true" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>
        </ContentTemplate>
    </asp:UpdatePanel>

    <asp:Panel ID="pnlSBU" runat="server" align="center">
        <asp:Label ID="Label5" runat="server" Text="Label" Visible="false"></asp:Label>
        <asp:ScriptManagerProxy ID="ScriptManagerProxy11" runat="server"></asp:ScriptManagerProxy>
        <asp:UpdatePanel ID="UpdatePanel26" runat="server">
            <ContentTemplate>
                <div class="panel panel-danger">
                    <div class="panel-heading">
                        <span>Alert</span>
                    </div>
                    <div class="panel-body">
                        <div class="row">
                            <div class="col-sm-12">
                                <asp:Label ID="lblSbuMsg1" runat="server"></asp:Label>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-12">
                                <asp:Label ID="lblSbuMsg2" runat="server"></asp:Label>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-12">
                                <asp:Label ID="lMssg2" runat="server"></asp:Label>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-12">
                                <asp:Label ID="Label10" runat="server"></asp:Label>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-12">
                                <asp:Label ID="Label11" runat="server"></asp:Label>
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
                                <asp:Button ID="btnSbu" runat="server" Text="Ok" CausesValidation="false" class="btn btn-primary" OnClick="btnSbu_Click" />
                            </div>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </asp:Panel>


    <script>
        Sys.Application.add_load(func);
        function func() {

            $('.txtShSeq').keypress(function (evt) {
                // var ch = (evt.which) ? evt.which : evt.keyCode;
                var charCode = evt.which;
                var str = $(this).val();
                //console.log(charCode);
                if ((charCode == 8) || (charCode == 9) || (charCode == 0)) {
                    return true;
                }
                else if (str.length < 2) {
                    if (charCode > 47 && charCode < 58) {
                        return true;
                    }
                    else {
                        return false;
                    }
                }
                else {
                    $(this).value = str.substr(0, 2);
                    alert('Maximum 2 characters are allowed ');
                    return false;
                }
            });
        }
    </script>




    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <asp:Button ID="Button4" runat="server" Text="Button" Style="display: none;" />
            <asp:ModalPopupExtender ID="Userconfmsg" runat="server" Enabled="True" TargetControlID="Button4"
                PopupControlID="pnlConfmsg" PopupDragHandleControlID="PopupHeader" Drag="true" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:Panel ID="Panel1" runat="server" align="center">
        <div runat="server" id="pnlConfmsg" class="panel panel-info height120 width250">
            <asp:Label ID="Label1" runat="server" Text="Label" Visible="false"></asp:Label>
            <asp:ScriptManagerProxy ID="ScriptManagerProxy2" runat="server"></asp:ScriptManagerProxy>
            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                <ContentTemplate>

                    <div class="panel panel-heading">
                        <span>Alert</span>
                    </div>
                    <div class="panel-body">
                        <div class="row">
                            <div class="col-sm-12">
                                <asp:Label ID="lblconfimmsg" runat="server"></asp:Label>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-12">
                                <asp:Label ID="Label4" runat="server"></asp:Label>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-12">
                                <asp:Label ID="Label7" runat="server"></asp:Label>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-12">
                                <asp:Label ID="Label8" runat="server"></asp:Label>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-12">
                                <asp:Label ID="Label9" runat="server"></asp:Label>
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
                                <asp:Button ID="Button5" runat="server" Text="yes" Width="80px" CausesValidation="false" class="btn btn-primary btn-xs" OnClick="btnconfok_Click" />
                            </div>
                            <div class="col-sm-2 ">
                                <asp:Button ID="Button6" runat="server" Text="No" Width="80px" CausesValidation="false" class="btn btn-primary btn-xs" OnClick="btnconfno_Click" />
                            </div>
                        </div>
                    </div>

                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </asp:Panel>





    <asp:ScriptManagerProxy ID="ScriptManagerProxy7" runat="server"></asp:ScriptManagerProxy>
    <asp:UpdatePanel ID="UpdatePanel8" runat="server">
        <ContentTemplate>
            <asp:Button ID="Button8" runat="server" Text="Button" Style="display: none;" />
            <asp:ModalPopupExtender ID="OderplaneItempopoup" runat="server" Enabled="True" TargetControlID="Button8"
                PopupControlID="pnlOderpopup" PopupDragHandleControlID="PopupHeader" Drag="true" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>

        </ContentTemplate>

    </asp:UpdatePanel>

    <asp:Panel runat="server" ID="pnlOderpopup" DefaultButton="lbtnSearch">
        <div runat="server" id="Div3" class="panel panel-default height400 width700">
            <asp:Label ID="Label2" runat="server" Text="Label" Visible="false"></asp:Label>
            <div class="panel panel-default">
                <div class="panel-heading">
                    <asp:LinkButton ID="LinkButton5" runat="server">
                             <span class="glyphicon glyphicon-remove" aria-hidden="true">Close</span>
                    </asp:LinkButton>
                    <%--<span>Commen Search</span>--%>
                    <div class="col-sm-11">
                    </div>
                    <div class="col-sm-1">
                    </div>
                </div>
                <div class="panel-body">
                    <div class="col-sm-12" id="Div4" runat="server">
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
                                <asp:ScriptManagerProxy ID="ScriptManagerProxy6" runat="server"></asp:ScriptManagerProxy>
                                <asp:UpdatePanel ID="UpdatePanel11" runat="server">
                                    <ContentTemplate>
                                        <asp:GridView ID="grdOderItem" AutoGenerateColumns="false" CausesValidation="false" runat="server" GridLines="None" CssClass="table table-hover table-striped" PageSize="10" PagerStyle-CssClass="cssPager" AllowPaging="True" OnPageIndexChanging="grdResult_PageIndexChanging">
                                            <Columns>
                                                <%--<asp:ButtonField Text="select" CommandName="Select" ItemStyle-Width="10" />--%>
                                                <asp:TemplateField HeaderText="Select">

                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="btndgvpendSelect" CausesValidation="false" OnClick="btndgvpendSelect_Click" runat="server">
                                                            <span class="glyphicon glyphicon-arrow-right" aria-hidden="true"></span>
                                                        </asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Item Code">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblItemCode" runat="server" Text='<%# Bind("ibi_itm_cd") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Line No" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblLineNo" runat="server" Text='<%# Bind("ibi_line") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Qty">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblQty" runat="server" Text='<%# Bind("ibi_model") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
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
                                        <asp:Button ID="lbtnUploadExcelFile" class="btn btn-warning btn-xs" runat="server" Text="Upload"
                                            OnClick="lbtnUploadExcelFile_Click" />
                                    </div>
                                </div>
                            </asp:Panel>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </asp:Panel>
    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
        <ContentTemplate>
            <asp:Button ID="btn15" runat="server" Text="Button" Style="display: none;" />
            <asp:ModalPopupExtender ID="popupErro" runat="server" Enabled="True" TargetControlID="btn15"
                PopupControlID="pnlExcelErro" PopupDragHandleControlID="PopupHeader" Drag="true" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>
        </ContentTemplate>
    </asp:UpdatePanel>

    <asp:UpdatePanel runat="server" ID="UpdatePanel6">
        <ContentTemplate>
            <asp:Panel runat="server" ID="pnlExcelErro">
                <div runat="server" id="Div5" class="panel panel-primary" style="padding: 5px;">
                    <div class="panel panel-default" style="width: 1100px;">
                        <div class="panel-heading" style="height: 25px;">
                            <div class="col-sm-11 padding0">
                                <strong>Excel Incorrect Data</strong>
                            </div>
                            <div class="col-sm-1">
                            </div>
                        </div>
                        <div class="panel panel-body padding0">
                            <div class="row">
                                <div class="col-sm-12">
                                    <div class="row">
                                        <div class="col-sm-12">
                                            <div class="row buttonRow">
                                                <div class="col-sm-12">
                                                    <div class="col-sm-7">
                                                    </div>

                                                    <div class="col-sm-5 padding0">
                                                        <div class="col-sm-4">
                                                        </div>
                                                        <div class="col-sm-4">
                                                            <%-- <asp:LinkButton ID="lbtnExcSave" OnClientClick="return ConfSave()" runat="server" OnClick="lbtnExcSave_Click">
                                                      <span class="glyphicon glyphicon-saved"  aria-hidden="true"></span>Save
                                                            </asp:LinkButton>--%>
                                                        </div>
                                                        <div class="col-sm-4 text-right">
                                                            <asp:LinkButton ID="lbtnExcClose" runat="server" OnClick="lbtnExcClose_Click">
                                                                    <span class="glyphicon glyphicon-remove"  aria-hidden="true"></span>Close
                                                            </asp:LinkButton>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-12">
                                                    <div class="panel panel-default">
                                                        <div class="panel panel-body padding0">
                                                            <div class="row">
                                                                <div class="col-sm-12">
                                                                    <div style="height: 400px; overflow-y: auto;">
                                                                        <asp:GridView ID="dgvError" CssClass="table table-hover table-striped" runat="server" GridLines="None" ShowHeaderWhenEmpty="True"
                                                                            EmptyDataText="No data found..." AutoGenerateColumns="False"
                                                                            PagerStyle-CssClass="cssPager">
                                                                            <Columns>
                                                                                <asp:TemplateField HeaderText="Excel Line">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblSad_itm_line" Text='<%# Bind("Sad_itm_line","{0:#00}") %>' runat="server" Width="100%" />
                                                                                    </ItemTemplate>
                                                                                    <ItemStyle Width="60px" />
                                                                                    <HeaderStyle Width="60px" />
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="Err Data">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblSad_itm_cd" Text='<%# Bind("Sad_itm_cd") %>' runat="server" Width="100%" />
                                                                                    </ItemTemplate>
                                                                                    <ItemStyle Width="50px" />
                                                                                    <HeaderStyle Width="50px" />
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="Error">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblTmp_err" Text='<%# Bind("errorMsg") %>' runat="server" Width="100%" />
                                                                                    </ItemTemplate>
                                                                                    <ItemStyle Width="100px" />
                                                                                    <HeaderStyle Width="100px" />
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
    <%-- pnl save order plan excel --%>
    <asp:UpdatePanel runat="server" ID="UpdatePanel15">
        <ContentTemplate>
            <asp:Button ID="btnpop1" runat="server" Text="Button" Style="display: none;" />
            <asp:ModalPopupExtender ID="popOpExcSave" runat="server" Enabled="True" TargetControlID="btnpop1"
                PopupControlID="pnlOpExcSave" PopupDragHandleControlID="PopupHeader" Drag="true" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdateProgress ID="UpdateProgress7" DisplayAfter="10" runat="server" AssociatedUpdatePanelID="upOpExcSave">
        <ProgressTemplate>
            <div class="divWaiting">
                <asp:Label ID="lblWaidt10" runat="server"
                    Text="Please wait... " />
                <asp:Image ID="imgWaidt10" runat="server"
                    ImageAlign="Middle" ImageUrl="~/images/icons/ajax-loader.gif" />
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <asp:Panel runat="server" ID="pnlOpExcSave">
        <asp:UpdatePanel runat="server" ID="upOpExcSave">
            <ContentTemplate>
                <div runat="server" id="Div6" class="panel panel-primary" style="padding: 1px;">
                    <div class="panel panel-default" style="height: 40px; width: 500px;">
                        <div class="panel-heading" style="height: 40px;">
                            <div class="col-sm-8">
                                <strong>Upload excel data</strong>
                            </div>
                            <div class="col-sm-2 text-right padding0">
                                <asp:Button Text="Upload" ID="btnGenOrdPlans" OnClick="btnGenOrdPlans_Click" runat="server" />
                            </div>
                            <div class="col-sm-2 text-right padding0">
                                <asp:Button Text="Cancel" ID="btnCancelProcess" OnClick="btnCancelProcess_Click" runat="server" />
                            </div>
                        </div>
                    </div>
                </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </asp:Panel>


</asp:Content>
