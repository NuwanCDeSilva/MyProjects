<%@ Page Title="" Language="C#" MasterPageFile="~/View/AdminSite.Master" AutoEventWireup="true" CodeBehind="PurchaseOrder.aspx.cs" Inherits="FastForward.SCMWeb.View.Transaction.Local_Purchasing.PurchaseOrder" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script type="text/javascript">
        function checkDate(sender, args) {

            if ((sender._selectedDate < new Date())) {
                alert("You cannot select a day earlier than today!");
                sender._selectedDate = new Date();
                sender._textbox.set_Value(sender._selectedDate.format(sender._format))
            }
        }
        function showSuccessToast() {
            $().toastmessage('showSuccessToast', "Success Dialog which is fading away ...");
        }
        function showStickySuccessToast(value) {
            $().toastmessage('showToast', {
                text: value,
                sticky: false,
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
                sticky: false,
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

        function showErrorToast() {
            $().toastmessage('showErrorToast', "Error Dialog which is fading away ...");
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
    </script>
    <style type="text/css">
        .disabledbtn {
            background-color: #000000;
            color: #FFFFFF;
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
    <script>
        function ConfirmSavePo() {
            var selectedvalueOrd = confirm("Do you want to save this order ?");
            if (selectedvalueOrd) {
                return true;
            } else {
                return false;
            }
        };
        function ClearConfirm() {
            var selectedvalue = confirm("Do you want to clear data?");
            if (selectedvalue) {
                document.getElementById('<%=txtClearlconformmessageValue.ClientID %>').value = "Yes";
            } else {
                document.getElementById('<%=txtClearlconformmessageValue.ClientID %>').value = "No";
            }
        };
        function SaveConfirm() {
            var selectedvalue = confirm("Do you want to save this order ?");
            if (selectedvalue) {
                document.getElementById('<%=txtSavelconformmessageValue.ClientID %>').value = "Yes";
            } else {
                document.getElementById('<%=txtSavelconformmessageValue.ClientID %>').value = "No";
            }
        };
        function PrinteConfirm() {
            var selectedvalue = confirm("Do you want to Print data?");
            if (selectedvalue) {
                document.getElementById('<%=txtPrintconformmessageValue.ClientID %>').value = "Yes";
            } else {
                document.getElementById('<%=txtPrintconformmessageValue.ClientID %>').value = "No";
            }
        };
        function ApprovalConfirm() {
            var selectedvalue = confirm("Do you want to Approval this order?");
            if (selectedvalue) {
                document.getElementById('<%=txtApprovalconformmessageValue.ClientID %>').value = "Yes";
            } else {
                document.getElementById('<%=txtApprovalconformmessageValue.ClientID %>').value = "No";
            }
        };
        function CancelConfirm() {
            var selectedvalue = confirm("Do you want to cancel this order ?");
            if (selectedvalue) {
                document.getElementById('<%=txtCancelconformmessageValue.ClientID %>').value = "Yes";
            } else {
                document.getElementById('<%=txtCancelconformmessageValue.ClientID %>').value = "No";
            }
        };
        function DeleteConfirm() {
            var selectedvalue = confirm("Do you want to Delete data?");
            if (selectedvalue) {
                document.getElementById('<%=txtDeleteconformmessageValue.ClientID %>').value = "Yes";
            } else {
                document.getElementById('<%=txtDeleteconformmessageValue.ClientID %>').value = "No";
            }
        };
        function RemoveConfirm() {
            var selectedvalue = confirm("Do you want to remove selected item details ?");
            if (selectedvalue) {
                document.getElementById('<%=txtRemoveconformmessageValue.ClientID %>').value = "Yes";
            } else {
                document.getElementById('<%=txtRemoveconformmessageValue.ClientID %>').value = "No";
            }
        };
        function Enable() {
            return;
        };
        function ConfirmGenarate() {
            var selectedvalueOrd = confirm("Do you want to Generate ?");
            if (selectedvalueOrd) {
            } else {
                return false;
            }
        };
    </script>
    <script type="text/javascript">

        function CheckAll(oCheckbox) {
            var GridView2 = document.getElementById("<%=grdReqItems.ClientID %>");
            for (i = 1; i < GridView2.rows.length; i++) {
                GridView2.rows[i].cells[0].getElementsByTagName("INPUT")[0].checked = oCheckbox.checked;
            }
        }
        function CheckAllgrdDel(oCheckbox) {
            var GridView2 = document.getElementById("<%=grdDel.ClientID %>");
            for (i = 1; i < GridView2.rows.length; i++) {
                GridView2.rows[i].cells[0].getElementsByTagName("INPUT")[0].checked = oCheckbox.checked;
            }
        }
        function CheckAllgrdPOItems(oCheckbox) {
            var GridView2 = document.getElementById("<%=grdPOItems.ClientID %>");
            for (i = 1; i < GridView2.rows.length; i++) {
                GridView2.rows[i].cells[0].getElementsByTagName("INPUT")[0].checked = oCheckbox.checked;
            }
        }
        function CheckBoxCheck(rb) {
            if (!jQuery(".chkMultiReq span").is(":checked")) {
                debugger;
                var gv = document.getElementById("<%=grdReq.ClientID%>");
                var chk = gv.getElementsByTagName("input");
                var row = rb.parentNode.parentNode;
                for (var i = 0; i < chk.length; i++) {
                    if (chk[i].type == "checkbox") {
                        if (chk[i].checked && chk[i] != rb) {
                            chk[i].checked = false;
                            break;
                        }
                    }
                }
            }

        }

        <%--function CheckBoxCheck(rb) {
            debugger;
           var gv = document.getElementById("<%=grdReq.ClientID%>");
            var chk = gv.getElementsByTagName("input");
            var row = rb.parentNode.parentNode;
            if (!(document.getElementById('.chkMultiReq').checked)) {
                for (var i = 0; i < chk.length; i++) {
                    if (chk[i].type == "checkbox") {
                        if (chk[i].checked && chk[i] != rb) {
                            chk[i].checked = false;
                            break;
                        }
                    }
                }
            }
        }--%>

        function CheckBoxCheck2(rb) {
            debugger;
            var gv = document.getElementById("<%=grdReqItems.ClientID%>");
            var chk = gv.getElementsByTagName("input");
            var row = rb.parentNode.parentNode;
            for (var i = 0; i < chk.length; i++) {
                if (chk[i].type == "checkbox") {
                    if (chk[i].checked && chk[i] != rb) {
                        chk[i].checked = false;
                        break;
                    }
                }
            }
        }
        function CheckBoxCheck3(rb) {
            debugger;
            var gv = document.getElementById("<%=grdQuo.ClientID%>");
            var chk = gv.getElementsByTagName("input");
            var row = rb.parentNode.parentNode;
            for (var i = 0; i < chk.length; i++) {
                if (chk[i].type == "checkbox") {
                    if (chk[i].checked && chk[i] != rb) {
                        chk[i].checked = false;
                        break;
                    }
                }
            }
        }
        function filterDigits(eventInstance) {
            eventInstance = eventInstance || window.event;
            key = eventInstance.keyCode || eventInstance.which;
            if ((key < 58) && (key > 47) || key == 45 || key == 8) {
                return true;
            }

            else {
                if (eventInstance.preventDefault)
                    eventInstance.preventDefault();
                eventInstance.returnValue = false;
                return false;

            } //if
        } //filterDigits

        function isNumberKey(evt) {
            var charCode = (evt.which) ? evt.which : evt.keyCode;
            if (charCode != 46 && charCode > 31
              && (charCode < 48 || charCode > 57))
                return false;

            return true;
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" runat="server">
    <asp:UpdateProgress ID="UpdateProgress2" DisplayAfter="10"
        runat="server" AssociatedUpdatePanelID="UpdatePanel1">
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
            <asp:ScriptManagerProxy ID="ScriptManagerProxy1" runat="server"></asp:ScriptManagerProxy>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <div class="row">
                        <div class="col-sm-12">
                            <div class="row">
                                <div class="col-sm-4">
                                </div>
                                <div class="col-sm-8">
                                    <div class="row buttonRow">
                                        <div class="col-sm-2"></div>
                                        <div class="col-sm-4 paddingRight0">
                                            <asp:LinkButton ID="lbtnGenarate" OnClientClick="return ConfirmGenarate();" CausesValidation="false"
                                                runat="server" CssClass="floatRight" OnClick="lbtnGenarate_Click">
                                        <span class="glyphicon glyphicon-upload" aria-hidden="true"></span>Generate by getting lowest price
                                            </asp:LinkButton>
                                        </div>
                                        <div class="col-sm-6">
                                            <div class="col-sm-2">
                                            </div>
                                            <div class="col-sm-2 padding0">
                                                <asp:LinkButton ID="lbtnApproval" OnClientClick="ApprovalConfirm()" CausesValidation="false" runat="server" CssClass="floatRight" OnClick="lbtnApproval_Click">
                                        <span class="glyphicon glyphicon-thumbs-up" aria-hidden="true"></span>Approval
                                                </asp:LinkButton>
                                            </div>

                                            <div class="col-sm-2 paddingRight0">
                                                <asp:LinkButton ID="lbtnSave" CausesValidation="false" runat="server" CssClass="floatRight" OnClick="lbtnAdd_Click" OnClientClick="SaveConfirm()">
                                        <span class="glyphicon glyphicon-saved" aria-hidden="true"></span>Save
                                                </asp:LinkButton>
                                            </div>
                                            <div class="col-sm-2 paddingLeft0 paddingRight0">
                                                <asp:LinkButton ID="lbtnCancel" OnClientClick="CancelConfirm()" CausesValidation="false" runat="server" CssClass="floatRight" OnClick="lbtnCancel_Click">
                                        <span class="glyphicon glyphicon-remove" aria-hidden="true"></span>Cancel
                                                </asp:LinkButton>
                                            </div>

                                            <div class="col-sm-2 paddingLeft0 paddingRight0">
                                                <asp:LinkButton ID="lbtnPrintPO" OnClientClick="lbtnPrintPO_Click" CausesValidation="false" runat="server" CssClass="floatRight" OnClick="lbtnPrintPO_Click">
                                        <span class="glyphicon glyphicon-print" aria-hidden="true"></span>Print-PO
                                                </asp:LinkButton>
                                            </div>

                                            <div class="col-sm-2 paddingLeft0 paddingRight0">
                                                <asp:LinkButton ID="lbtnClear" runat="server" CssClass="floatRight" OnClientClick="ClearConfirm()" OnClick="lbtnClear_Click">
                                        <span class="glyphicon glyphicon-refresh" aria-hidden="true"></span>Clear
                                                </asp:LinkButton>
                                            </div>
                                            <div class="col-sm-2 paddingRight0">
                                                <asp:LinkButton ID="lbtnPrint" Visible="false" CausesValidation="false" runat="server" CssClass="floatRight" OnClientClick="PrinteConfirm()">
                                        <span class="glyphicon glyphicon-edit" aria-hidden="true"></span>Print
                                                </asp:LinkButton>
                                            </div>

                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-8">
                                    <div id="WarningPorder" visible="false" runat="server" class="alert alert-danger alert-dismissible" role="alert">
                                        <div class="col-sm-11 padding0">
                                            <strong>Alert!</strong>
                                            <asp:Label ID="lblWPorder" runat="server"></asp:Label>
                                        </div>
                                        <div class="col-sm-1 padding0">
                                            <asp:LinkButton ID="lbtnWPorderok" runat="server" CausesValidation="false" CssClass="floatright" OnClick="lbtnWPorderok_Click">
                                        <span class="glyphicon glyphicon-ok" aria-hidden="true"></span>
                                            </asp:LinkButton>
                                        </div>

                                    </div>

                                    <div id="SuccessPorder" runat="server" visible="false" class="alert alert-success alert-dismissible" role="alert">
                                        <div class="col-sm-11 padding0">
                                            <strong>Success!</strong>
                                            <asp:Label ID="lblSPorder" runat="server"></asp:Label>

                                        </div>
                                        <div class="col-sm-1 padding0" style="text-align: right;">
                                            <asp:LinkButton ID="LinkButton7" runat="server" CausesValidation="false" CssClass="floatright" OnClick="lbtnWPorderok_Click">
                                        <span class="glyphicon glyphicon-ok" aria-hidden="true"></span>
                                            </asp:LinkButton>
                                        </div>

                                    </div>
                                </div>
                                <div class="col-sm-12 paddingRight5" id="div19" runat="server">
                                    <asp:Label Text="lblBackDateInfor" ID="lblBackDateInfor" Font-Size="12px" CssClass="labelText1" runat="server" ForeColor="Red" />
                                </div>
                            </div>
                        </div>
                    </div>
                    <asp:HiddenField ID="txtClearlconformmessageValue" runat="server" />
                    <asp:HiddenField ID="txtSavelconformmessageValue" runat="server" />
                    <asp:HiddenField ID="txtPrintconformmessageValue" runat="server" />
                    <asp:HiddenField ID="txtApprovalconformmessageValue" runat="server" />
                    <asp:HiddenField ID="txtCancelconformmessageValue" runat="server" />
                    <asp:HiddenField ID="txtDeleteconformmessageValue" runat="server" />
                    <asp:HiddenField ID="txtRemoveconformmessageValue" runat="server" />
                    <div class="row">
                        <div class="col-sm-5 paddingRight5">
                            <div class="panel panel-default">
                                <%--<div class="panel-heading">
                                </div>--%>
                                <div class="panel-body">
                                    <div class="col-sm-4 paddingLeft0">
                                        <div class="row">
                                            <div class="col-sm-6 labelText1">
                                                Type
                                            </div>
                                            <div class="col-sm-6 paddingLeft0 paddingRight0">
                                                <asp:DropDownList ID="ddlMainType" AutoPostBack="True" CausesValidation="false" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlMainType_SelectedIndexChanged">
                                                    <asp:ListItem Text=" - - Select - - " Value="0" Selected="True"></asp:ListItem>
                                                    <asp:ListItem Text="Local" Value="L" />
                                                    <asp:ListItem Text="Imports" Value="I" />
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-sm-12 height5">
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-sm-6 labelText1">
                                                Sub Type
                                            </div>
                                            <div class="col-sm-6 paddingLeft0 paddingRight0">
                                                <asp:DropDownList ID="ddlType" AutoPostBack="True" CausesValidation="false" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlType_SelectedIndexChanged">
                                                    <asp:ListItem Text=" - - Select - - " Value="0" Selected="True"></asp:ListItem>
                                                    <asp:ListItem Text="NORMAL" Value="N" />
                                                    <asp:ListItem Text="SERVICE" Value="S" />
                                                    <asp:ListItem Text="CONSIGNMENT" Value="C" />
                                                </asp:DropDownList>
                                            </div>
                                        </div>

                                        <div class="row">
                                            <div class="col-sm-12 height5">
                                            </div>
                                        </div>

                                        <div class="row">
                                            <div class="col-sm-6 labelText1">
                                                Payment Term
                                            </div>
                                            <div class="col-sm-6 paddingLeft0 paddingRight0">
                                                <asp:DropDownList ID="ddlPayTerm" CausesValidation="false" AutoPostBack="true" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlPayTerm_SelectedIndexChanged">
                                                    <asp:ListItem Text="CREDIT" Value="0" />
                                                    <asp:ListItem Text="CASH" Value="1" />
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-sm-12 height5">
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-sm-4 labelText1">
                                                Remarks
                                            </div>
                                            <div class="col-sm-8 paddingLeft0 paddingRight0">
                                                <asp:TextBox runat="server" TextMode="MultiLine" MaxLength="216" ID="txtRemarks" CausesValidation="false" CssClass="form-control"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-sm-12 height5">
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-4">
                                        <div class="row">
                                            <div class="col-sm-6 labelText1">
                                                Supplier
                                            </div>
                                            <div class="col-sm-5 paddingLeft0 paddingRight0">
                                                <asp:TextBox runat="server" ID="txtSupCode" AutoPostBack="true" CausesValidation="false" Enabled="false" CssClass="form-control" OnTextChanged="txtSupCode_TextChanged"></asp:TextBox>
                                            </div>
                                            <div class="col-sm-1 paddingLeft0 paddingRight0 Lwidth">
                                                <asp:LinkButton ID="lbtnSupCode" runat="server" CausesValidation="false" OnClick="lbtnSupCode_Click">
                                                        <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                </asp:LinkButton>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-sm-12 height5">
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-sm-6 labelText1">
                                                Supplier Ref.
                                            </div>
                                            <div class="col-sm-6 paddingLeft0 paddingRight0">
                                                <asp:TextBox runat="server" ID="txtSupRef" AutoPostBack="true" MaxLength="30" CausesValidation="false" CssClass="form-control" OnTextChanged="txtSupRef_TextChanged"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-sm-12 height5">
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-sm-6 labelText1">
                                                Credit Period
                                            </div>
                                            <div class="col-sm-6 paddingLeft0 paddingRight0">
                                                <asp:TextBox runat="server" ID="txtCreditPeriod" Enabled="true" AutoPostBack="true" MaxLength="5" onkeypress="return isNumberKey(event)" CausesValidation="false" CssClass="form-control" OnTextChanged="txtCreditPeriod_TextChanged"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-sm-12 height5">
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-sm-6 labelText1">
                                                Currency
                                            </div>
                                            <div class="col-sm-4 paddingLeft0 paddingRight0">
                                                <asp:TextBox runat="server" ID="txtCurrency" AutoPostBack="true" CausesValidation="false" CssClass="form-control" OnTextChanged="txtCurrency_TextChanged"></asp:TextBox>
                                            </div>
                                            <div class="col-sm-1 paddingLeft0">
                                                <asp:LinkButton ID="lbtnCurrency" runat="server" CausesValidation="false" OnClick="lbtnCurrency_Click">
                                                        <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                </asp:LinkButton>
                                            </div>

                                        </div>
                                        <div class="row">
                                            <div class="col-sm-12 height5">
                                                <div class="col-sm-6 labelText1">
                                                </div>
                                                <div class="col-sm-6 labelText1">
                                                    <asp:Label ID="lblEx" ForeColor="Red" runat="server"></asp:Label>
                                                </div>

                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-4">
                                        <div class="row">
                                            <div class="col-sm-4 labelText1">
                                                Order #
                                            </div>
                                            <div class="col-sm-6 paddingLeft0 paddingRight0">
                                                <asp:TextBox runat="server" AutoPostBack="true" ID="txtPurNo" CausesValidation="false" CssClass="form-control" OnTextChanged="txtPurNo_TextChanged"></asp:TextBox>
                                            </div>
                                            <div class="col-sm-1 paddingLeft0">
                                                <asp:LinkButton ID="lbtnPurNo" runat="server" CausesValidation="false" OnClick="lbtnPurNo_Click">
                                                        <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                </asp:LinkButton>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-sm-12 height5">
                                            </div>
                                        </div>

                                        <div class="row">
                                            <div class="col-sm-4 labelText1">
                                                Job #
                                            </div>
                                            <div class="col-sm-6 paddingLeft0 paddingRight0">
                                                <asp:TextBox runat="server" ID="txtJob" CausesValidation="false" CssClass="form-control" OnTextChanged="txtJob_TextChanged"></asp:TextBox>
                                            </div>
                                            <div class="col-sm-1 paddingLeft0">
                                                <asp:LinkButton ID="lbtntJob" runat="server" CausesValidation="false" OnClick="lbtntJob_Click">
                                                        <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                </asp:LinkButton>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-sm-12 height5">
                                            </div>
                                        </div>

                                        <div class="row" runat="server" visible="false">
                                            <div class="col-sm-10 labelText1">
                                                Base to consignment GRN
                                            </div>
                                            <div class="col-sm-1 paddingLeft0 paddingRight0">
                                                <asp:CheckBox ID="chkBaseToConsGrn" runat="server" Visible="false" Width="5px"></asp:CheckBox>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-sm-12 height5">
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-sm-4 labelText1">
                                                Date
                                            </div>
                                            <div class="col-sm-6 paddingRight5 paddingLeft0">
                                                <asp:TextBox runat="server" ReadOnly="true" ID="txtPoDate" CausesValidation="false" CssClass="form-control"></asp:TextBox>
                                            </div>
                                            <div class="col-sm-1 paddingLeft0">
                                                <asp:LinkButton ID="lbtnPIDate" runat="server" CausesValidation="false">
                                                <span class="glyphicon glyphicon-calendar" aria-hidden="true"></span>
                                                </asp:LinkButton>
                                                <asp:CalendarExtender ID="CalendarExtender3" runat="server" TargetControlID="txtPoDate"
                                                    PopupButtonID="lbtnPIDate" Format="dd/MMM/yyyy">
                                                </asp:CalendarExtender>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-sm-10 labelText1">
                                                <asp:Label ID="lblStatus" runat="server" ForeColor="Red"></asp:Label>
                                            </div>

                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-sm-3 paddingLeft5 paddingRight0">
                            <asp:Panel runat="server" ID="pnlReq">
                                <div class="panel panel-default">
                                    <div class="panel-heading">
                                        Request Details
                                    </div>
                                    <div class="panel-body" style="height: 118px;">
                                        <div class="row">
                                            <div class="col-sm-2 labelText1">
                                                From
                                            </div>
                                            <div class="col-sm-3 paddingRight5 paddingLeft0">
                                                <asp:TextBox runat="server" ReadOnly="true" ID="txtFrom" CausesValidation="false" CssClass="form-control"></asp:TextBox>
                                            </div>
                                            <div class="col-sm-1 paddingLeft0">
                                                <asp:LinkButton ID="lbtnFrom" runat="server" CausesValidation="false">
                                                <span class="glyphicon glyphicon-calendar" aria-hidden="true"></span>
                                                </asp:LinkButton>
                                                <asp:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtFrom"
                                                    PopupButtonID="lbtnFrom" Format="dd/MMM/yyyy">
                                                </asp:CalendarExtender>
                                            </div>
                                            <div class="col-sm-1 labelText1">
                                                To
                                            </div>
                                            <div class="col-sm-3 paddingRight5 paddingLeft0">
                                                <asp:TextBox runat="server" ReadOnly="true" ID="txtTo" CausesValidation="false" CssClass="form-control"></asp:TextBox>
                                            </div>
                                            <div class="col-sm-1 paddingLeft0">
                                                <asp:LinkButton ID="lbtnTo" runat="server" CausesValidation="false">
                                                <span class="glyphicon glyphicon-calendar" aria-hidden="true"></span>
                                                </asp:LinkButton>
                                                <asp:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtTo"
                                                    PopupButtonID="lbtnTo" Format="dd/MMM/yyyy">
                                                </asp:CalendarExtender>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-sm-12 height5">
                                            </div>
                                        </div>

                                        <div class="row">
                                            <div class="col-sm-3 labelText1">
                                                Profit Center
                                            </div>
                                            <div class="col-sm-6 paddingLeft0 paddingRight0">
                                                <asp:TextBox runat="server" ID="txtprofitCenter" CausesValidation="false" CssClass="form-control"></asp:TextBox>
                                            </div>
                                            <div class="col-sm-1 paddingLeft0">
                                                <asp:LinkButton ID="lbtnprofitCenter" runat="server" CausesValidation="false" OnClick="lbtnprofitCenter_Click">
                                                        <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                </asp:LinkButton>
                                            </div>

                                        </div>
                                        <div class="row">
                                            <div class="col-sm-12 height5">
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-sm-2 labelText1">
                                                All
                                            </div>
                                            <div class="col-sm-1 paddingLeft0 paddingRight0">
                                                <asp:CheckBox ID="chkAll" runat="server" Width="5px"></asp:CheckBox>
                                            </div>
                                            <div class="col-sm-2 labelText1">
                                                Supplier
                                            </div>
                                            <div class="col-sm-4 paddingLeft0 paddingRight0">
                                                <asp:TextBox runat="server" ID="txtReqSupplerS" AutoPostBack="true" Enabled="false" CausesValidation="false" CssClass="form-control" OnTextChanged="txtSupCode_TextChanged"></asp:TextBox>
                                            </div>
                                            <div class="col-sm-1 paddingLeft0 Lwidth">
                                                <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="false" OnClick="lbtnSupCode_Click">
                                                        <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                </asp:LinkButton>
                                            </div>
                                            <div class="col-sm-2">
                                            </div>
                                            <div class="col-sm-1">
                                                <asp:LinkButton ID="lbtnView" runat="server" CausesValidation="false" OnClick="lbtnView_Click">
                                                        <span class="glyphicon glyphicon-hand-right" aria-hidden="true"></span>
                                                </asp:LinkButton>
                                            </div>
                                        </div>

                                        <div class="row">
                                            <div class="col-sm-12 chkMultiReqDiv height5">
                                                <asp:CheckBox ID="chkMultiReq" CssClass="chkMultiReq" Checked="true" AutoPostBack="true"
                                                    OnCheckedChanged="chkMultiReq_CheckedChanged" Text="Multiple Request" runat="server" />
                                                <asp:CheckBox ID="chkReqDate" AutoPostBack="true" runat="server" Text="Request Date" OnCheckedChanged="chkReqDate_CheckedChanged"></asp:CheckBox>
                                                <asp:CheckBox ID="chkModDate" AutoPostBack="true" runat="server" Text="Approved Date" OnCheckedChanged="chkModDate_CheckedChanged"></asp:CheckBox>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </asp:Panel>
                        </div>

                        <div class="col-sm-4 ">
                            <div class="panel panel-default">
                                <%-- <div class="panel-heading">
                                    Request Details
                                </div>--%>
                                <asp:Panel runat="server" ID="divReq">
                                    <div class="panel-body  panelscoll1">
                                        <asp:GridView ID="grdReq" EmptyDataText="No data found..." ShowHeaderWhenEmpty="True" runat="server" GridLines="None" CssClass="table table-hover table-striped" AutoGenerateColumns="false">
                                            <Columns>
                                                <asp:TemplateField HeaderText="" Visible="true">
                                                    <HeaderTemplate>
                                                        <asp:CheckBox ID="chkall_Req" AutoPostBack="true" OnCheckedChanged="chkall_Req_CheckedChanged" runat="server" CssClass="allPOItems" />
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="chk_Req" AutoPostBack="true" CssClass="chk_Req" runat="server" Checked="false" Width="5px"
                                                            OnCheckedChanged="chk_Req_CheckedChanged" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Request #">
                                                    <ItemTemplate>
                                                        <asp:Label ID="col_ReqNo" runat="server" Text='<%# Bind("itr_req_no") %>' Width="110px"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Date">
                                                    <ItemTemplate>
                                                        <asp:Label ID="col_reqDate" runat="server" Text='<%# Bind("itr_dt", "{0:dd/MMM/yyyy}") %>' Width="50px"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Location">
                                                    <ItemTemplate>
                                                        <asp:Label ID="col_reqLoc" runat="server" Text='<%# Bind("itr_loc") %>' Width="20px"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </asp:Panel>
                                <div class="row">
                                    <div class="col-sm-12 height5">
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-12 height5">
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">

                        <div class="col-sm-12 paddingRight0">
                            <div class="panel panel-default">
                                <div class="panel-heading">
                                    <div class="row">
                                        <div class="col-sm-6" id="divCenter" runat="server">
                                            Item Details
                                        </div>
                                        <div class="col-sm-4">
                                        </div>
                                        <div class="col-sm-2">
                                            <asp:LinkButton ID="lbtnItemDelete" runat="server" CausesValidation="false" OnClick="lbtnItemDelete_Click" OnClientClick="RemoveConfirm()">
                                                <span class="glyphicon glyphicon-trash" aria-hidden="true"></span>
                                            </asp:LinkButton>
                                        </div>
                                    </div>


                                </div>
                                <div class="panel-body">
                                    <div class="row">
                                        <div class="col-sm-12">
                                            <div class="col-sm-4 padding0">
                                                <div class="col-sm-4 padding0" id="ItemCodeDiv" runat="server">
                                                    Item Code
                                                </div>
                                                <div class="col-sm-5 padding0">
                                                    Description
                                                </div>
                                                <div class="col-sm-3 padding0">
                                                    Status
                                                </div>
                                            </div>
                                            <div class="col-sm-2 padding0">
                                                <div class="col-sm-6 padding0">
                                                    Warr Peri
                                                </div>
                                                <div class="col-sm-6 padding0">
                                                    Warr Remk
                                                </div>
                                            </div>
                                            <div class="col-sm-6 padding0">
                                                <div class="col-sm-1 padding0">
                                                    Qty
                                                </div>
                                                <div class="col-sm-2 padding0">
                                                    Unit Price
                                                </div>
                                                <div class="col-sm-2 padding0">
                                                    Amount
                                                </div>
                                                <div class="col-sm-1 padding0">
                                                    Dis.%
                                                </div>
                                                <div class="col-sm-1 padding0">
                                                    Dis.Amt
                                                </div>
                                                <div class="col-sm-2 padding0">
                                                    Tax
                                                </div>
                                                <div class="col-sm-2 padding0">
                                                    Total
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-sm-12">
                                            <div class="col-sm-4 padding0">
                                                <div class="col-sm-4  padding0">
                                                    <div class="col-sm-10  padding0">
                                                        <asp:TextBox runat="server" TabIndex="100" ID="txtItem" AutoPostBack="true" CausesValidation="false" CssClass="form-control" OnTextChanged="txtItem_TextChanged"></asp:TextBox>
                                                    </div>
                                                    <div class="col-sm-2  padding0">
                                                        <asp:LinkButton ID="lbtnItemSearch" runat="server" CausesValidation="false" OnClick="lbtnItemSearch_Click">
                                                            <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                        </asp:LinkButton>
                                                    </div>
                                                </div>
                                                <div class="col-sm-4 padding0">
                                                    <asp:TextBox runat="server" TabIndex="100" ID="txtItmDes" CausesValidation="false" CssClass="form-control"></asp:TextBox>
                                                </div>
                                                <div class="col-sm-4 padding0 ">
                                                    <asp:DropDownList ID="ddlStatus" AppendDataBoundItems="true" CausesValidation="false" runat="server" CssClass="form-control">
                                                        <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                                    </asp:DropDownList>
                                                    <%--<asp:TextBox runat="server" ID="txtStatus" AutoPostBack="true" CausesValidation="false" CssClass="form-control" OnTextChanged="txtStatus_TextChanged"></asp:TextBox>--%>
                                                </div>
                                            </div>
                                            <%--<div class="col-sm-1 Lwidth paddingLeft0" >
                                            <asp:LinkButton ID="lbtnstatusSearch" runat="server" CausesValidation="false" OnClick="lbtnstatusSearch_Click">
                                                <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                            </asp:LinkButton>
                                        </div>--%>
                                               <div class="col-sm-2 padding0">
                                                <div class="col-sm-6 padding0">
                                                    <asp:TextBox runat="server" TabIndex="106" ID="txtwarrper" CausesValidation="false" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                                                </div>
                                                <div class="col-sm-6 padding0">
                                                    <asp:TextBox runat="server" TabIndex="107" ID="txtwarrremk" CausesValidation="false" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-sm-6 padding0">
                                                <div class="col-sm-1 padding0">
                                                    <asp:TextBox runat="server" TabIndex="101" ID="txtQty" onkeypress="return isNumberKey(event)" AutoPostBack="true" Style="text-align: right" CausesValidation="false" CssClass="form-control" OnTextChanged="txtQty_TextChanged"></asp:TextBox>
                                                </div>
                                                <div class="col-sm-2 padding0">
                                                    <asp:TextBox runat="server" TabIndex="102" ID="txtUnitPrice" onkeypress="return isNumberKey(event)" AutoPostBack="true" Style="text-align: right" CausesValidation="false" CssClass="form-control" OnTextChanged="txtUnitPrice_TextChanged"></asp:TextBox>
                                                </div>
                                                <div class="col-sm-2 padding0">
                                                    <asp:TextBox runat="server" ID="txtAmount" onkeypress="return isNumberKey(event)" AutoPostBack="true" Style="text-align: right" ReadOnly="true" CausesValidation="false" CssClass="form-control" OnTextChanged="txtAmount_TextChanged"></asp:TextBox>
                                                </div>
                                                <div class="col-sm-1 padding0">
                                                    <asp:TextBox runat="server" TabIndex="103" ID="txtDisRate" onkeypress="return isNumberKey(event)" AutoPostBack="true" Style="text-align: right" CausesValidation="false" CssClass="form-control" OnTextChanged="txtDisRate_TextChanged"></asp:TextBox>
                                                </div>
                                                <div class="col-sm-1 padding0">
                                                    <asp:TextBox runat="server" TabIndex="104" ID="txtDiscount" onkeypress="return isNumberKey(event)" AutoPostBack="true" Style="text-align: right" CausesValidation="false" CssClass="form-control" OnTextChanged="txtDiscount_TextChanged"></asp:TextBox>
                                                </div>
                                                <div class="col-sm-2 padding0">
                                                    <asp:TextBox runat="server" ID="txtTax" onkeypress="return isNumberKey(event)" Enabled="false" AutoPostBack="true" Style="text-align: right" CausesValidation="false" CssClass="form-control"></asp:TextBox>
                                                </div>
                                                <div class="col-sm-3 padding0">
                                                    <div class="col-sm-10 padding0">
                                                        <asp:TextBox runat="server" ID="txtTotal" onkeypress="return isNumberKey(event)" CausesValidation="false" Style="text-align: right" ReadOnly="true" CssClass="form-control"></asp:TextBox>
                                                    </div>
                                                    <div class="col-sm-2 padding0">
                                                        <asp:LinkButton ID="lbtnAddItem" TabIndex="105" runat="server" CausesValidation="false" OnClick="lbtnAddItem_Click">
                                                        <span class="glyphicon glyphicon-plus" aria-hidden="true"></span>
                                                        </asp:LinkButton>
                                                    </div>
                                                </div>

                                            </div>
                                         
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-sm-12">
                                            <div class="panel panel-default">
                                                <%-- <div class="panel-heading">
                                    Request Details
                                </div>--%>
                                                <div class="panel-body height80 panelscoll">
                                                    <asp:GridView ID="grdPOItems" EmptyDataText="No data found..." ShowHeaderWhenEmpty="True" runat="server" GridLines="None" CssClass="table table-hover table-striped" AutoGenerateColumns="false">
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="" Visible="true">
                                                                <HeaderTemplate>
                                                                    <asp:CheckBox ID="allchk" runat="server" Width="5px" onclick="CheckAllgrdPOItems(this)"></asp:CheckBox>
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:CheckBox ID="chk_POItems" runat="server" Checked="false" Width="5px" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="#">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="col_seq" runat="server" Text='<%# Bind("pod_line_no") %>' Width="5px"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Add" Visible="true">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="lbtnPOItemsAdd" runat="server" CausesValidation="false" Width="5px" OnClick="lbtnPOItemsAdd_Click">
                                                                        <span class="glyphicon glyphicon-download" aria-hidden="true" ></span>
                                                                    </asp:LinkButton>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Item">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="col_Item" runat="server" Text='<%# Bind("pod_itm_cd") %>' Width="100px"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Description">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblPod_item_desc" runat="server" Text='<%# Bind("Pod_item_desc") %>' Width="100px"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Status">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="col_Status" runat="server" Text='<%# Bind("pod_itm_stus") %>' Width="10px"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Qty">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="col_Qty" runat="server" Style="text-align: right" Text='<%# Bind("pod_qty") %>' Width="10px"></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle CssClass="gridHeaderAlignRight" />
                                                                <HeaderStyle CssClass="gridHeaderAlignRight" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Unit Amount">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="col_UnitPrice" runat="server" Style="text-align: right" Text='<%# Bind("pod_unit_price","{0:n}") %>' Width="40px"></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle CssClass="gridHeaderAlignRight" />
                                                                <HeaderStyle CssClass="gridHeaderAlignRight" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Amount">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="col_Amount" runat="server" Style="text-align: right" Text='<%# Bind("pod_line_val","{0:n}") %>' Width="40px"></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle CssClass="gridHeaderAlignRight" />
                                                                <HeaderStyle CssClass="gridHeaderAlignRight" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Dis. Rate">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="col_DisRt" runat="server" Style="text-align: right" Text='<%# Bind("pod_dis_rt","{0:n}") %>' Width="40px"></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle CssClass="gridHeaderAlignRight" />
                                                                <HeaderStyle CssClass="gridHeaderAlignRight" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Dis. Amount">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="col_DisAmt" runat="server" Style="text-align: right" Text='<%# Bind("pod_dis_amt","{0:n}") %>' Width="40px"></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle CssClass="gridHeaderAlignRight" />
                                                                <HeaderStyle CssClass="gridHeaderAlignRight" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Tax Amount">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="col_Tax" runat="server" Style="text-align: right" Text='<%# Bind("pod_line_tax","{0:n}") %>' Width="40px"></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle CssClass="gridHeaderAlignRight" />
                                                                <HeaderStyle CssClass="gridHeaderAlignRight" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Total">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="col_Total" runat="server" Style="text-align: center" Text='<%# Bind("pod_line_amt","{0:n}") %>' Width="50px"></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle CssClass="gridHeaderAlignRight" />
                                                                <HeaderStyle CssClass="gridHeaderAlignRight" />
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
                    <div class="row">
                        <div class="col-sm-8 paddingRight0">
                            <div class="row">
                                <div class="col-sm-4">
                                    <%--<div class="col-sm-3 labelText1">
                                        Allow Costing
                                    </div>--%>
                                    <div class="col-sm-6 paddingLeft0 paddingRight0">
                                        <asp:CheckBox ID="chkACosting" Visible="false" runat="server" Width="5px"></asp:CheckBox>
                                    </div>
                                </div>
                                <div class="col-sm-4">
                                    <div class="row">
                                        <div class="col-sm-6 labelText1">
                                            Claimable Percentage :
                                        </div>
                                        <div class="col-sm-2 labelText1">
                                        </div>
                                        <div class="col-sm-2 paddingLeft0 paddingRight0 text-right">
                                            <asp:Label ID="lblclmPre" runat="server" Text="0.00"></asp:Label>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-sm-6 labelText1">
                                            Claimable Tax Amt :
                                        </div>
                                        <div class="col-sm-2 labelText1">
                                        </div>
                                        <div class="col-sm-2 paddingLeft0 paddingRight0 text-right">
                                            <asp:Label ID="lblclmTaxAmt" runat="server" Text="0.00"></asp:Label>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-sm-6 labelText1">
                                            Final Unit Price
                                        </div>
                                        <div class="col-sm-2 labelText1">
                                        </div>
                                        <div class="col-sm-2 paddingLeft0 paddingRight0 text-right">
                                            <asp:Label ID="lblfinalUnitPrc" runat="server" Text="0.00"></asp:Label>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-sm-4">
                                    <div class="row">
                                        <div class="col-sm-6 labelText1 ">
                                            Amount before discount
                                        </div>
                                        <div class="col-sm-2 labelText1">
                                        </div>
                                        <div class="col-sm-2 paddingLeft0 paddingRight0 text-right">
                                            <asp:Label ID="lblSubTot" runat="server" Text="0.00"></asp:Label>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-sm-6 labelText1">
                                            Discount amount
                                        </div>
                                        <div class="col-sm-2 labelText1">
                                        </div>
                                        <div class="col-sm-2 paddingLeft0 paddingRight0 text-right">
                                            <asp:Label ID="lblDisAmt" runat="server" Text="0.00"></asp:Label>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-sm-6 labelText1">
                                            After discount
                                        </div>
                                        <div class="col-sm-2 labelText1">
                                        </div>
                                        <div class="col-sm-2 paddingLeft0 paddingRight0 text-right">
                                            <asp:Label ID="lblTotAfterDis" runat="server" Text="0.00"></asp:Label>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-sm-6 labelText1">
                                            Tax amount
                                        </div>
                                        <div class="col-sm-2 labelText1">
                                        </div>
                                        <div class="col-sm-2 paddingLeft0 paddingRight0 text-right">
                                            <asp:Label ID="lblTaxAmt" runat="server" Text="0.00"></asp:Label>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-sm-6 labelText1">
                                            Total amount
                                        </div>
                                        <div class="col-sm-2 labelText1">
                                        </div>
                                        <div class="col-sm-2 paddingLeft0 paddingRight0 text-right">
                                            <asp:Label ID="lblTotal" runat="server" Text="0.00"></asp:Label>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-12 ">
                                    <div class="panel panel-default">
                                        <div class="panel-heading">
                                            <div class="row">
                                                <div class="col-sm-6">
                                                    Delivery Shedule
                                                </div>
                                                <div class="col-sm-2">
                                                    <asp:Label ID="lblItemLine" runat="server"></asp:Label>
                                                </div>
                                                <div class="col-sm-1">
                                                    <asp:LinkButton ID="lbtngrdDele" runat="server" CausesValidation="false" OnClientClick="RemoveConfirm()" OnClick="lbtngrdDele_Click">
                                                <span class="glyphicon glyphicon-trash" aria-hidden="true"></span>
                                                    </asp:LinkButton>
                                                </div>
                                                <div class="col-sm-1 paddingLeft0 paddingRight0">
                                                    <asp:LinkButton ID="lbtnAddDelAll" runat="server" CausesValidation="false" OnClick="lbtnAddDelAll_Click">
                                                        <span class="glyphicon glyphicon-arrow-down" aria-hidden="true"></span> ApplyAll
                                                    </asp:LinkButton>
                                                </div>
                                            </div>

                                        </div>
                                        <div class="panel-body">
                                            <div class="row">
                                                <div class="col-sm-1" runat="server" id="deliveryItemDiv">
                                                    Item
                                                </div>
                                                <div class="col-sm-2">
                                                    <asp:Label ID="lblDelItem" runat="server" Text="" ForeColor="#660033"></asp:Label>
                                                </div>
                                                <div class="col-sm-1">
                                                    Qty
                                                </div>
                                                <div class="col-sm-1">
                                                    <asp:Label ID="lblDelStatus" Visible="false" runat="server" ForeColor="#660033"></asp:Label>
                                                    <asp:TextBox runat="server" ID="txtDelQty" onkeypress="return isNumberKey(event)" CausesValidation="false" CssClass="form-control"></asp:TextBox>
                                                </div>


                                                <div class="col-sm-1">
                                                    Location
                                                </div>
                                                <div class="col-sm-1 paddingLeft0">
                                                    <asp:TextBox runat="server" ID="txtDelLoca" CausesValidation="false" Enabled="false" CssClass="form-control"></asp:TextBox>
                                                </div>

                                                <div class="col-sm-1 paddingLeft0 Lwidth">
                                                    <asp:LinkButton ID="lbtnDelLoca" runat="server" CausesValidation="false" OnClick="lbtnDelLoca_Click">
                                                        <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                    </asp:LinkButton>
                                                </div>
                                                <div class="col-sm-1">
                                                    Ex.Date
                                                </div>
                                                <div class="col-sm-2 paddingLeft0">
                                                    <asp:TextBox runat="server" ID="txtexDate" CausesValidation="false" Enabled="false" CssClass="form-control"></asp:TextBox>
                                                </div>
                                                <div class="col-sm-1 paddingLeft0">
                                                    <asp:LinkButton ID="lbtnEXTo" runat="server" CausesValidation="false">
                                                <span class="glyphicon glyphicon-calendar" aria-hidden="true"></span>
                                                    </asp:LinkButton>
                                                    <asp:CalendarExtender ID="CalendarExtender4" OnClientDateSelectionChanged="checkDate" runat="server" TargetControlID="txtexDate"
                                                        PopupButtonID="lbtnEXTo" Format="dd/MMM/yyyy">
                                                    </asp:CalendarExtender>
                                                </div>
                                                <div class="col-sm-1 Lwidth ">
                                                    <asp:LinkButton ID="lbtnAddDel" runat="server" CausesValidation="false" OnClick="lbtnAddDel_Click">
                                                        <span class="glyphicon glyphicon-plus" aria-hidden="true"></span>
                                                    </asp:LinkButton>
                                                </div>

                                            </div>
                                            <div class="row">
                                                <div class="col-sm-12">
                                                    <div class="panel panel-default">

                                                        <div class=" panel-body height50 panelscoll">
                                                            <asp:GridView ID="grdDel" EmptyDataText="No data found..." ShowHeaderWhenEmpty="True" runat="server" GridLines="None" CssClass="table table-hover table-striped" AutoGenerateColumns="false">
                                                                <Columns>
                                                                    <asp:TemplateField HeaderText="" Visible="true">
                                                                        <HeaderTemplate>
                                                                            <asp:CheckBox ID="allchk" runat="server" Width="5px" onclick="CheckAllgrdDel(this)"></asp:CheckBox>
                                                                        </HeaderTemplate>
                                                                        <ItemTemplate>
                                                                            <asp:CheckBox ID="chk_Deltems" runat="server" Checked="false" Width="5px" />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="#">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="col_dLNo" runat="server" Text='<%# Bind("Podi_line_no") %>' Width="10px"></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="#">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="col_dNo" runat="server" Text='<%# Bind("podi_del_line_no") %>' Width="10px"></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Item">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="col_dItem" runat="server" Text='<%# Bind("podi_itm_cd") %>' Width="100px"></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Status">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="col_dStatus" runat="server" Text='<%# Bind("podi_itm_stus") %>' Width="20px"></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Location">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="col_dLoc" runat="server" Text='<%# Bind("podi_loca") %>' Width="50px"></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="EX.Date">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="col_podi_exp_dt" runat="server" Text='<%# Bind("Podi_ex_dt","{0:dd/MMM/yyyy}") %>' Width="100px"></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Qty">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="col_dQty" runat="server" Text='<%# Bind("podi_qty") %>' Width="50px"></asp:Label>
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
                            </div>
                        </div>
                        <asp:Panel runat="server" ID="pnlItem">
                            <div class="col-sm-4" runat="server" id="ReqItemsPanel">
                                <div class="panel panel-default">
                                    <div class="panel-heading" id="itemdetailsDiv" runat="server">
                                       Item Details 
                                    </div>
                                    <div class="panel-body panelscoll1">
                                        <asp:GridView ID="grdReqItems" EmptyDataText="No data found..." ShowHeaderWhenEmpty="True" runat="server" GridLines="None" CssClass="table table-hover table-striped" AutoGenerateColumns="false">
                                            <Columns>
                                                <asp:TemplateField HeaderText="" Visible="true">
                                                    <HeaderTemplate>
                                                        <asp:CheckBox ID="allPOItems" AutoPostBack="true" OnCheckedChanged="allPOItems_CheckedChanged" runat="server" CssClass="allPOItems" />
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="chk_ReqItem" AutoPostBack="true" runat="server" Checked="false" Width="5px" OnCheckedChanged="chk_ReqItem_CheckedChanged" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Item">
                                                    <ItemTemplate>
                                                        <asp:Label ID="col_reqItem" runat="server" Text='<%# Bind("itri_itm_cd") %>' Width="40px"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Model">
                                                    <ItemTemplate>
                                                        <asp:Label ID="col_reqModel" runat="server" Text='<%# Bind("mi_model") %>' Width="60px"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Qty">
                                                    <ItemTemplate>
                                                        <asp:Label ID="col_Qty" runat="server" Text='<%# Bind("itri_qty") %>' Width="10px"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="App.Qty">
                                                    <ItemTemplate>
                                                        <asp:Label ID="col_reqQty" runat="server" Text='<%# Bind("itri_app_qty") %>' Width="10px"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Q.view" Visible="true">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lbtnReqItemsQ" runat="server" CausesValidation="false" Width="2px" OnClick="lbtnReqItemsQ_Click">
                                                <span class="glyphicon glyphicon-tag" aria-hidden="true" ></span>
                                                        </asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="status" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="col_ItemStatus" runat="server" Text='<%# Bind("itri_itm_stus") %>' Width="10px"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="line" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="col_ItemLine" runat="server" Text='<%# Bind("itri_line_no") %>' Width="10px"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="seq" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="col_Seq" runat="server" Text='<%# Bind("itr_seq_no") %>' Width="10px"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="B.Qty">
                                                    <ItemTemplate>
                                                        <asp:Label ID="col_BQty" runat="server" Text='<%# Bind("itri_bqty") %>' Width="10px"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Request #" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="col_ReqINo" runat="server" Text='<%# Bind("itr_req_no") %>' Width="110px"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Poqty" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="col_poqty" runat="server" Text='<%# Bind("itri_po_qty") %>' Width="110px"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                    <div class="row">
                                        <div class="col-sm-12 height5">
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </asp:Panel>

                        <div class="col-sm-4">
                            <asp:Panel runat="server" ID="QDetailDiv">
                                <div class="panel panel-default" style="height: 120px">
                                    <div class="panel-heading">
                                        Quotation Details
                                    </div>
                                    <div class="panel-body panelscollbar">
                                        <asp:GridView ID="grdQuo" EmptyDataText="No data found..." OnRowDataBound="grdQuo_RowDataBound" OnSelectedIndexChanged="grdQuo_SelectedIndexChanged" ShowHeaderWhenEmpty="True" runat="server" GridLines="None" CssClass="table table-hover table-striped" AutoGenerateColumns="false">
                                            <Columns>
                                                <asp:CommandField ShowSelectButton="true" ButtonType="Link" />
                                                <asp:TemplateField HeaderText="" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="chk_Quo" runat="server" Checked="false" AutoPostBack="true" onclick="CheckBoxCheck3(this);" Width="5px" OnCheckedChanged="chk_Quo_CheckedChanged" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Supplier">
                                                    <ItemTemplate>
                                                        <asp:Label ID="col_qh_party_cd" runat="server" Visible="true" Text='<%# Bind("qh_party_cd") %>' Width="100%"></asp:Label>
                                                        <asp:Label ID="Label4" runat="server" Visible="false" Text='<%# Bind("qh_party_name") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Qutation #">
                                                    <ItemTemplate>
                                                        <asp:Label ID="col_QuoNo" runat="server" Text='<%# Bind("qh_no") %>' Width="100%"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Reference" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="col_Ref" runat="server" Text='<%# Bind("qh_ref") %>' Width="100%"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Valid Form">
                                                    <ItemTemplate>
                                                        <asp:Label ID="col_FromDate" runat="server" Text='<%# Bind("qh_frm_dt", "{0:M-dd-yyyy}") %>' Width="100%"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Valid To">
                                                    <ItemTemplate>
                                                        <asp:Label ID="col_ToDate" runat="server" Text='<%# Bind("qh_ex_dt", "{0:M-dd-yyyy}") %>' Width="100%"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Price">
                                                    <ItemTemplate>
                                                        <asp:Label ID="col_qPrice" runat="server" Text='<%# Bind("QH_ANAL_5") %>' Width="100%"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                    <div class="row">
                                        <div class="col-sm-12 height80">
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-sm-12 height30">
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-sm-12 height10">
                                        </div>
                                    </div>
                                </div>
                            </asp:Panel>
                        </div>

                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>

    <asp:UpdatePanel ID="UpdatePanel8" runat="server">
        <ContentTemplate>
            <asp:Button ID="Button3" runat="server" Text="Button" Style="display: none;" />
            <asp:ModalPopupExtender ID="UserPopoup" runat="server" Enabled="True" TargetControlID="Button3"
                PopupControlID="pnlpopup" PopupDragHandleControlID="PopupHeader" Drag="true" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>

        </ContentTemplate>

    </asp:UpdatePanel>

    <asp:Panel runat="server" ID="pnlpopup" DefaultButton="lbtnSearch">
        <div runat="server" id="test" class="panel panel-default height400 width700">

            <asp:Label ID="lblvalue" runat="server" Text="Label" Visible="false"></asp:Label>
            <div class="panel panel-default">
                <div class="panel-heading">
                    <asp:LinkButton ID="btnClose" runat="server" OnClick="btnClose_Click">
                             <span class="glyphicon glyphicon-remove" aria-hidden="true">Close</span>
                    </asp:LinkButton>
                    <%--<span>Commen Search</span>--%>
                    <div class="col-sm-11">
                    </div>
                    <div class="col-sm-1">
                    </div>
                </div>
                <div class="panel-body">
                    <div class="col-sm-12" id="Div3" runat="server">
                        <div class="row">
                            <div class="col-sm-12">
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-12 height5">
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-12" id="search" runat="server">
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
                                <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                    <ContentTemplate>
                                        <asp:GridView ID="grdResult" CausesValidation="false" runat="server" GridLines="None" CssClass="table table-hover table-striped" PageSize="10" PagerStyle-CssClass="cssPager" AllowPaging="True" OnSelectedIndexChanged="grdResult_SelectedIndexChanged" OnPageIndexChanging="grdResult_PageIndexChanging">
                                            <Columns>
                                                <asp:ButtonField Text="select" CommandName="Select" ItemStyle-Width="10" />

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






    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
        <ContentTemplate>
            <asp:Button ID="btnQ" runat="server" Text="Button" Style="display: none;" />
            <asp:ModalPopupExtender ID="QuotationPopup" runat="server" Enabled="True" TargetControlID="btnQ"
                PopupControlID="pnlQuotationPopup" PopupDragHandleControlID="PopupHeader" Drag="true" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>
        </ContentTemplate>

    </asp:UpdatePanel>
    <asp:UpdatePanel ID="UpdatePanel4" runat="server">
        <ContentTemplate>
            <asp:Panel runat="server" ID="pnlQuotationPopup" DefaultButton="lbtnSearch">
                <div runat="server" id="Div1" class="panel panel-default height400 width700">

                    <asp:Label ID="Label1" runat="server" Text="Label" Visible="false"></asp:Label>
                    <div class="panel panel-default">
                        <div class="panel-heading">
                            <asp:LinkButton ID="LinkButton2" runat="server">
                             <span class="glyphicon glyphicon-remove" aria-hidden="true">Close</span>
                            </asp:LinkButton>
                            <%--<span>Commen Search</span>--%>
                            <div class="col-sm-11">
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
                                <asp:Panel runat="server">
                                    <div class="row">
                                        <div class="col-sm-12" id="Div2" runat="server">
                                            <div class="col-sm-3 labelText1">
                                                Order by lowest price
                                            </div>
                                            <div class="col-sm-1 paddingLeft0 paddingRight0">
                                                <asp:RadioButton ID="rdbLprice" AutoPostBack="true" runat="server" Width="5px" GroupName="short" OnCheckedChanged="rdbLprice_CheckedChanged" />

                                            </div>
                                            <div class="col-sm-3 labelText1">
                                                Order by latest date  
                                            </div>
                                            <div class="col-sm-1 paddingLeft0 paddingRight0">
                                                <asp:RadioButton ID="rdbLDate" AutoPostBack="true" runat="server" Width="5px" GroupName="short" OnCheckedChanged="rdbLDate_CheckedChanged" />

                                            </div>

                                        </div>
                                    </div>
                                </asp:Panel>

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
                                        <asp:ScriptManagerProxy ID="ScriptManagerProxy5" runat="server"></asp:ScriptManagerProxy>
                                        <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                                            <ContentTemplate>
                                                <asp:GridView ID="grdItemQ" CausesValidation="false" runat="server" AutoGenerateColumns="false" GridLines="None" CssClass="table table-hover table-striped" PageSize="10" PagerStyle-CssClass="cssPager" AllowPaging="True" OnPageIndexChanging="grdItemQ_PageIndexChanging" OnSelectedIndexChanged="grdItemQ_SelectedIndexChanged">
                                                    <Columns>
                                                        <asp:ButtonField Text="select" CommandName="Select" ItemStyle-Width="10" />
                                                        <asp:TemplateField HeaderText="Quotation #">
                                                            <ItemTemplate>
                                                                <asp:Label ID="qh_no" runat="server" Text='<%# Bind("qh_no") %>' Width="100px"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Supplier">
                                                            <ItemTemplate>
                                                                <asp:Label ID="qh_party_cd" runat="server" Text='<%# Bind("qh_party_cd") %>' Width="50px"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="From Date">
                                                            <ItemTemplate>
                                                                <asp:Label ID="qh_frm_dt" runat="server" Text='<%# Bind("qh_frm_dt" , "{0:M-dd-yyyy}") %>' Width="60px"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Expire Date">
                                                            <ItemTemplate>
                                                                <asp:Label ID="qh_ex_dt" runat="server" Text='<%# Bind("qh_ex_dt" , "{0:M-dd-yyyy}") %>' Width="60px"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Unit Price">
                                                            <ItemTemplate>
                                                                <asp:Label ID="qd_unit_price" runat="server" Text='<%# Bind("qd_unit_price") %>' Width="10px"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Qty">
                                                            <ItemTemplate>
                                                                <asp:Label ID="qd_to_qty" runat="server" Text='<%# Bind("qd_to_qty") %>' Width="5px"></asp:Label>
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
        </ContentTemplate>
    </asp:UpdatePanel>



    <asp:UpdatePanel ID="UpdatePanel7" runat="server">
        <ContentTemplate>
            <asp:Button ID="Button5" runat="server" Text="Button" Style="display: none;" />
            <asp:ModalPopupExtender ID="UserDPopoup" runat="server" Enabled="True" TargetControlID="Button5"
                PopupControlID="pnlDpopup" CancelControlID="btnDClose" PopupDragHandleControlID="PopupHeader" Drag="true" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>

        </ContentTemplate>

    </asp:UpdatePanel>

    <asp:UpdatePanel ID="UpdatePanel5" runat="server">
        <ContentTemplate>
            <asp:Panel runat="server" ID="pnlDpopup" DefaultButton="lbtnSearch">
                <div runat="server" id="Div5" class="panel panel-default height400 width700">

                    <asp:Label ID="Label3" runat="server" Text="Label" Visible="false"></asp:Label>
                    <div class="panel panel-default">
                        <div class="panel-heading">
                            <asp:LinkButton ID="btnDClose" runat="server">
                             <span class="glyphicon glyphicon-remove" aria-hidden="true">Close</span>
                            </asp:LinkButton>
                            <%--<span>Commen Search</span>--%>
                            <div class="col-sm-11">
                            </div>
                            <div class="col-sm-1">
                            </div>
                        </div>
                        <div class="panel-body">
                            <div class="col-sm-12" id="Div6" runat="server">
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
                                            <asp:TextBox runat="server" Enabled="false" ID="txtFDate" CausesValidation="false" CssClass="form-control"></asp:TextBox>
                                        </div>
                                        <div class="col-sm-1 paddingLeft0">
                                            <asp:LinkButton ID="lbtnFDate" runat="server" CausesValidation="false">
                                                <span class="glyphicon glyphicon-calendar" aria-hidden="true"></span>
                                            </asp:LinkButton>
                                            <asp:CalendarExtender ID="CalendarExtender7" runat="server" TargetControlID="txtFDate"
                                                PopupButtonID="lbtnFDate" Format="dd/MMM/yyyy">
                                            </asp:CalendarExtender>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-sm-2 labelText1">
                                            To
                                        </div>
                                        <div class="col-sm-6 paddingRight5 paddingLeft0">
                                            <asp:TextBox runat="server" Enabled="false" ID="txtTDate" CausesValidation="false" CssClass="form-control"></asp:TextBox>
                                        </div>
                                        <div class="col-sm-1 paddingLeft0">
                                            <asp:LinkButton ID="lbtnTDate" runat="server" CausesValidation="false">
                                                <span class="glyphicon glyphicon-calendar" aria-hidden="true"></span>
                                            </asp:LinkButton>
                                            <asp:CalendarExtender ID="CalendarExtender8" runat="server" TargetControlID="txtTDate"
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
                                        <asp:ScriptManagerProxy ID="ScriptManagerProxy11" runat="server"></asp:ScriptManagerProxy>
                                        <asp:UpdatePanel ID="UpdatePanel20" runat="server">
                                            <ContentTemplate>
                                                <div class="col-sm-8 paddingRight5">
                                                    <asp:DropDownList ID="ddlSearchbykeyD" runat="server" class="form-control">
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
                                            <asp:TextBox ID="txtSearchbywordD" CausesValidation="false" placeholder="Search by word" class="form-control" AutoPostBack="true" runat="server" OnTextChanged="txtSearchbywordD_TextChanged"></asp:TextBox>
                                        </div>
                                        <asp:ScriptManagerProxy ID="ScriptManagerProxy12" runat="server"></asp:ScriptManagerProxy>
                                        <asp:UpdatePanel ID="UpdatePanel21" runat="server">
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
                                        <asp:ScriptManagerProxy ID="ScriptManagerProxy13" runat="server"></asp:ScriptManagerProxy>
                                        <asp:UpdatePanel ID="UpdatePanel22" runat="server">
                                            <ContentTemplate>
                                                <asp:GridView ID="grdResultD" CausesValidation="false" runat="server" EmptyDataText="No data found..." ShowHeaderWhenEmpty="True" GridLines="None" CssClass="table table-hover table-striped" PageSize="5" PagerStyle-CssClass="cssPager" AllowPaging="True" OnSelectedIndexChanged="grdResultD_SelectedIndexChanged" OnPageIndexChanging="grdResultD_PageIndexChanging">
                                                    <Columns>
                                                        <asp:ButtonField Text="Select" CommandName="Select" ItemStyle-Width="10" />

                                                    </Columns>
                                                </asp:GridView>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>
                                </div>

                                <asp:UpdateProgress ID="UpdateProgress1" DisplayAfter="10"
                                    runat="server" AssociatedUpdatePanelID="UpdatePanel7">

                                    <ProgressTemplate>
                                        <div class="divWaiting">
                                            <asp:Label ID="lblWait" runat="server"
                                                Text="Please wait... " />
                                            <asp:Image ID="imgWait" runat="server"
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
    <div class="row">
        <div class="col-sm-12">
            <asp:UpdatePanel runat="server" ID="update">
                <ContentTemplate>
                    <asp:Button ID="Button11" runat="server" Text="Button" Style="display: none;" />
                    <asp:ModalPopupExtender ID="PopupSearch" runat="server" Enabled="True" TargetControlID="Button11"
                        PopupControlID="testPanel" CancelControlID="lbtnCls" PopupDragHandleControlID="PopupHeader" Drag="true" BackgroundCssClass="modalBackground">
                    </asp:ModalPopupExtender>

                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
    <asp:Panel runat="server" ID="testPanel" DefaultButton="lbtnSearch">
        <div class="row">
            <div class="col-sm-12">
                <div runat="server" id="Div10" class="panel panel-info width700" style="overflow-x: hidden; overflow-y: auto;">
                    <asp:Label ID="lblTmp" runat="server" Text="Label" Visible="false"></asp:Label>
                    <div class="panel panel-default">
                        <div class="panel-heading">
                            <div class="row">
                                <div class="col-sm-12">
                                    <div class="col-sm-9">
                                        <strong><b>PO Auto Generation</b></strong>
                                    </div>
                                    <div class="col-sm-2">
                                    </div>
                                    <div class="col-sm-1">
                                        <asp:LinkButton ID="lbtnCls" runat="server">
                                    <span class="glyphicon glyphicon-remove" aria-hidden="true">Close</span>
                                        </asp:LinkButton>
                                    </div>
                                </div>
                            </div>

                        </div>
                        <div class="panel-body">
                            <div class="row ">
                                <div class="col-sm-10 ">
                                    <div id="divDanger1" visible="false" runat="server" class="alert alert-danger alert-dismissible" role="alert">
                                        <div class="col-sm-11">
                                            <strong>Alert!</strong>
                                            <asp:Label ID="lblDanger" runat="server"></asp:Label>
                                        </div>
                                        <div class="col-sm-1">
                                            <asp:LinkButton ID="lbtnDanger" runat="server" CausesValidation="false" CssClass="floatright" OnClick="lbtnDanger_Click">
                                        <span class="glyphicon glyphicon-ok" aria-hidden="true"></span>
                                            </asp:LinkButton>
                                        </div>
                                    </div>
                                    <div id="divSucces1" runat="server" visible="false" class="alert alert-success alert-dismissible" role="alert">
                                        <div class="col-sm-11">
                                            <strong>Success!</strong>
                                            <asp:Label ID="lblSuccess" runat="server"></asp:Label>
                                        </div>
                                        <div class="col-sm-1">
                                            <asp:LinkButton ID="lbtnSucces" runat="server" CausesValidation="false" CssClass="floatright" OnClick="lbtnSucces_Click">
                                        <span class="glyphicon glyphicon-ok" aria-hidden="true"></span>
                                            </asp:LinkButton>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-sm-2 buttonRow">
                                    <asp:LinkButton ID="lbtnSavePo" CausesValidation="false" runat="server"
                                        OnClick="lbtnSavePo_Click">
                                            <span class="glyphicon glyphicon-saved" aria-hidden="true"  ></span>Save
                                    </asp:LinkButton>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-12" style="max-height: 400px; overflow-y: auto;">
                                    <asp:UpdatePanel runat="server">
                                        <ContentTemplate>
                                            <asp:GridView ID="dgvSupDetails" CausesValidation="false" runat="server" GridLines="None" AutoGenerateColumns="false"
                                                ShowHeaderWhenEmpty="true" EmptyDataText="No data found..."
                                                CssClass="table table-hover table-striped" PagerStyle-CssClass="cssPager"
                                                OnPageIndexChanging="dgvSupDetails_PageIndexChanging">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="">
                                                        <HeaderTemplate>
                                                            <asp:CheckBox ID="chkSelectAll" runat="server" CssClass="chkSelectAll" />
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:CheckBox Width="20px" ID="chkPoselect" CssClass="chkPoselect" runat="server"></asp:CheckBox>
                                                        </ItemTemplate>
                                                        <ItemStyle />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Supplier">
                                                        <ItemTemplate>
                                                            <asp:Label Text='<%# Bind("Qh_party_cd") %>' ToolTip='<%# Bind("Qh_party_name") %>' runat="server" ID="lblSupplier" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Item Code">
                                                        <ItemTemplate>
                                                            <asp:Label Text='<%# Bind("Qd_itm_cd") %>' ToolTip='<%# Bind("Qd_itm_des") %>' runat="server" ID="lblItem" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <%-- <asp:TemplateField HeaderText="From Date"> 
                                                <ItemTemplate>
                                                    <asp:Label ID="lblFrDate" Text='<%# Bind("Qh_frm_dt","{0:dd-MMM-yyyy}") %>' runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="To Date"> 
                                                <ItemTemplate>
                                                    <asp:Label ID="lblToDate" Text='<%# Bind("Qh_ex_dt","{0:dd-MMM-yyyy}") %>' runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>--%>
                                                    <asp:TemplateField HeaderText="Order Qty">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" ID="lblFrQty" Text='<%# Bind("qd_frm_qty","{0:N2}") %>' />
                                                        </ItemTemplate>
                                                        <HeaderStyle CssClass="gridHeaderAlignRight" />
                                                        <ItemStyle CssClass="gridHeaderAlignRight" />
                                                    </asp:TemplateField>


                                                    <%--  <asp:TemplateField HeaderText="To Qty">
                                                <ItemTemplate>
                                                    <asp:Label runat="server" ID="lblToQty" Text='<%# Bind("qd_to_qty","{0:N2}") %>' />
                                                </ItemTemplate>
                                                <HeaderStyle CssClass="gridHeaderAlignRight" />
                                                <ItemStyle CssClass="gridHeaderAlignRight" />
                                            </asp:TemplateField>--%>
                                                    <asp:TemplateField HeaderText="Unit price">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" ID="lblPrice" Text='<%# Bind("Qh_anal_5","{0:N2}") %>' />
                                                        </ItemTemplate>
                                                        <HeaderStyle CssClass="gridHeaderAlignRight" />
                                                        <ItemStyle CssClass="gridHeaderAlignRight" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" ID="lblUom" Text='<%# Bind("qh_ref") %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Item Code" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label Text='<%# Bind("Tmp_req_no") %>' runat="server" ID="lblTmp_req_no" />
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
        </div>
    </asp:Panel>

    <div class="row">
        <div class="col-sm-12">
            <asp:UpdatePanel runat="server" ID="UpdatePanel11">
                <ContentTemplate>
                    <asp:Button ID="Button12" runat="server" Text="Button" Style="display: none;" />
                    <asp:ModalPopupExtender ID="popupSaveData" runat="server" Enabled="True" TargetControlID="Button12"
                        PopupControlID="Panel1" CancelControlID="lbtnCls" PopupDragHandleControlID="PopupHeader" Drag="true" BackgroundCssClass="modalBackground">
                    </asp:ModalPopupExtender>

                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
    <asp:Panel runat="server" ID="Panel1" DefaultButton="lbtnSearch">
        <div class="row">
            <div class="col-sm-12">
                <div runat="server" id="Div4" class="panel panel-info width450" style="overflow-x: hidden; overflow-y: auto;">
                    <asp:Label ID="Label2" runat="server" Text="Label" Visible="false"></asp:Label>
                    <div class="panel panel-default">
                        <div class="panel-heading">
                            <div class="row">
                                <div class="col-sm-12">
                                    <div class="col-sm-9">
                                        <strong><b>Purchase orders generated successfully</b></strong>
                                    </div>

                                    <div class="col-sm-1">
                                        <asp:LinkButton ID="LinkButton3" runat="server">
                                    <span class="glyphicon glyphicon-remove" aria-hidden="true">Close</span>
                                        </asp:LinkButton>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="panel-body">
                            <div class="row ">
                            </div>
                            <div class="row">
                                <div class="col-sm-12" style="max-height: 400px; overflow-y: auto;">
                                    <asp:UpdatePanel runat="server">
                                        <ContentTemplate>
                                            <asp:GridView ID="dgvPoSaved" CausesValidation="false" runat="server" GridLines="None" AutoGenerateColumns="false"
                                                ShowHeaderWhenEmpty="true" EmptyDataText="No data found..."
                                                CssClass="table table-hover table-striped" PagerStyle-CssClass="cssPager"
                                                OnPageIndexChanging="dgvSupDetails_PageIndexChanging">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Purchase Order # ">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblPoNo" Text='<%# Bind("PoNo") %>' runat="server" />
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
        </div>
    </asp:Panel>

    <asp:UpdatePanel ID="UpdatePanel35" runat="server">
        <ContentTemplate>
            <asp:Button ID="Button15" runat="server" Text="Button" Style="display: none;" />
            <asp:ModalPopupExtender ID="MDPDOConf" runat="server" Enabled="True" TargetControlID="Button15"
                PopupControlID="pnlDO" PopupDragHandleControlID="PopupHeader" Drag="true" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>
        </ContentTemplate>
    </asp:UpdatePanel>

    <asp:Panel ID="pnlDO" runat="server" align="center">
        <asp:Label ID="Label8" runat="server" Text="Label" Visible="false"></asp:Label>
        <asp:ScriptManagerProxy ID="ScriptManagerProxy18" runat="server"></asp:ScriptManagerProxy>
        <asp:UpdatePanel ID="UpdatePanel40" runat="server">
            <ContentTemplate>
                <div class="panel panel-danger">
                    <div class="panel-heading">
                        <span>Alert</span>
                    </div>
                    <div class="panel-body">
                        <div class="row">
                            <div class="col-sm-12">
                                <asp:Label ID="Label11" runat="server"></asp:Label>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-12">
                                <strong>
                                    <asp:Label ID="txt1" Text="" runat="server"></asp:Label></strong>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-12">
                                <strong>
                                    <asp:Label ID="txt2" Text="Do you want to generate Sales Order ?" runat="server"></asp:Label></strong>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-12">
                                <asp:Label ID="Label15" runat="server"></asp:Label>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-12">
                                <asp:Label ID="Label16" runat="server"></asp:Label>
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
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </asp:Panel>


    <script>
        Sys.Application.add_load(func);
        function func() {
            //$('.chkMultiReqDiv input').click(function () {
            //        var checked = $(this).prop("checked");
            //        if (!checked) {
            //            $('.chk_Req input').prop('checked', false);
            //        }
            //});

            //$('.chk_Req input').click(function () {
            //    if (!jQuery(".chkMultiReqDiv input").is(":checked")) {
            //        var checked = $(this).prop("checked");
            //        if (checked) {
            //            $('.chk_Req input').prop('checked', false);
            //            $(this).prop('checked', true);
            //        }
            //    }
            //});
            $('.chkSelectAll input').click(function () {
                var checked = $(this).prop("checked");
                if (checked) {
                    $('.chkPoselect input').prop('checked', true);
                } else {
                    $('.chkPoselect input').prop('checked', false);
                }
            });

            $('.chkPoselect input').click(function () {
                var checked = $(this).prop("checked");
                if (!checked) {
                    $('.chkSelectAll input').prop('checked', false);
                }
            });


        }
    </script>


</asp:Content>
