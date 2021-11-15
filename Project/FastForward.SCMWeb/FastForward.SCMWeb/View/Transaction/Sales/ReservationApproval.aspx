<%@ Page Title="" Language="C#" MasterPageFile="~/View/AdminSite.Master" AutoEventWireup="true" CodeBehind="ReservationApproval.aspx.cs" Inherits="FastForward.SCMWeb.View.Transaction.Sales.ReservationApproval" %>

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
        }

        function ConfirmClear() {
            var selectedvalueOrd = confirm("Do you want to clear all details ?");
            if (selectedvalueOrd) {
                document.getElementById('<%=hdnClear.ClientID %>').value = "Yes";
            } else {
                document.getElementById('<%=hdnClear.ClientID %>').value = "No";
            }
        };

        function ConfirmSave() {
            var selectedvalueOrdPlace = confirm("Do you want to save ?");
            if (selectedvalueOrdPlace) {
                document.getElementById('<%=hdnSave.ClientID %>').value = "Yes";
            }
            else {
                document.getElementById('<%=hdnSave.ClientID %>').value = "No";
            }
        };

        function ConfirmUpdate() {
            var selectedvalueOrdPlace = confirm("Do you want to Update ?");
            if (selectedvalueOrdPlace) {
                document.getElementById('<%=hdnUpdate.ClientID %>').value = "Yes";
            }
            else {
                document.getElementById('<%=hdnUpdate.ClientID %>').value = "No";
            }
        };

        function ConfirmApprove() {
            var selectedvalueOrdPlace = confirm("Do you want to Approve ?");
            if (selectedvalueOrdPlace) {
                document.getElementById('<%=hdnApprove.ClientID %>').value = "Yes";
            } else {
                document.getElementById('<%=hdnApprove.ClientID %>').value = "No";
            }
        };

        function DeleteConfirm() {
            var selectedvalue = confirm("Do you want to remove the selected  details ?");
            if (selectedvalue) {
                document.getElementById('<%=txtDeleteconformmessageValue.ClientID  %>').value = "Yes";
            } else {
                document.getElementById('<%=txtDeleteconformmessageValue.ClientID %>').value = "No";
            }
        };
        function ConfirmCancel() {
            var selectedvalueOrdPlace = confirm("Do you want to cancel ?");
            if (selectedvalueOrdPlace) {
                document.getElementById('<%=hdnCancel.ClientID %>').value = "Yes";
            } else {
                document.getElementById('<%=hdnCancel.ClientID %>').value = "No";
            }
        };
        function Enable() {
            return;
        };
        function CheckBoxCheck(rb) {
            debugger;
            var gv = document.getElementById("<%=grdInventoryBalance.ClientID%>");
            var chk = gv.getElementsByTagName("input");
            var row = rb.parentNode.parentNode;
            for (var i = 0; i < chk.length; i++) {
                if (chk[i].type == "radio") {
                    if (chk[i].checked && chk[i] != rb) {
                        chk[i].checked = false;
                        break;
                    }
                }
            }
        }
        function CheckBoxCheckBL(rb) {
            debugger;
            var gv = document.getElementById("<%=grdPendingOrderBalance.ClientID%>");
            var chk = gv.getElementsByTagName("input");
            var row = rb.parentNode.parentNode;
            for (var i = 0; i < chk.length; i++) {
                if (chk[i].type == "radio") {
                    if (chk[i].checked && chk[i] != rb) {
                        chk[i].checked = false;
                        break;
                    }
                }
            }
        }
    </script>
    <script type="text/javascript">
        function closeDialog() {
            $(this).showStickySuccessToast("close");
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
        function showStickyNoticeToast(value) {
            $().toastmessage('showToast', {
                text: value,
                sticky: true,
                position: 'top-left',
                type: 'notice',
                closeText: '',
                close: function () { console.log("toast is closed ..."); }
            });
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

        .panel {
            margin-bottom: 1px;
            margin-top: 0px;
            padding-top: 1px;
            padding-bottom: 0px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" runat="server">
    <asp:UpdateProgress ID="UpdateProgress7" DisplayAfter="10" runat="server" AssociatedUpdatePanelID="mainUpPnl">
        <ProgressTemplate>
            <div class="divWaiting">
                <asp:Label ID="lblWait10" runat="server"
                    Text="Please wait... " />
                <asp:Image ID="imgWait10" runat="server"
                    ImageAlign="Middle" ImageUrl="~/images/icons/ajax-loader.gif" />
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <asp:UpdatePanel runat="server" ID="mainUpPnl">
        <ContentTemplate>
            <asp:HiddenField ID="hdnSave" runat="server" />
            <asp:HiddenField ID="hdnUpdate" runat="server" />
            <asp:HiddenField ID="hdnApprove" runat="server" />
            <asp:HiddenField ID="hdnCancel" runat="server" />
            <asp:HiddenField ID="hdnClear" runat="server" />
            <asp:HiddenField ID="txtDeleteconformmessageValue" runat="server" />
            <div class="panel panel-default marginLeftRight5 paddingbottom0">
                <div class="panel-body paddingtopbottom0">
                    <div class="row">
                        <div class="col-sm-7  buttonrow">
                            <div id="divWarning" visible="false" runat="server" class="alert alert-danger alert-dismissible" role="alert">
                                <strong>Warning!</strong>
                                <asp:Label ID="lblWarning" runat="server"></asp:Label>
                                <asp:LinkButton ID="lbtnlWarning" runat="server" CausesValidation="false" CssClass="floatRight">
                                        <span class="glyphicon glyphicon-ok" aria-hidden="true"></span>
                                </asp:LinkButton>
                            </div>
                            <div id="divSuccess" runat="server" visible="false" class="alert alert-success alert-dismissible" role="alert">
                                <strong>Success!</strong>
                                <asp:Label ID="lblSuccess" runat="server"></asp:Label>
                                <asp:LinkButton ID="lbtnSuccess" runat="server" CausesValidation="false" CssClass="floatRight">
                                        <span class="glyphicon glyphicon-ok" aria-hidden="true"></span>
                                </asp:LinkButton>
                            </div>
                            <div id="divAlert" runat="server" visible="false" class="alert alert-info alert-dismissible" role="alert">
                                <strong>Alert!</strong>
                                <asp:Label ID="lblAlert" runat="server"></asp:Label>
                                <asp:LinkButton ID="lbtnAlert" runat="server" CausesValidation="false" CssClass="floatRight">
                                        <span class="glyphicon glyphicon-ok" aria-hidden="true"></span>
                                </asp:LinkButton>
                            </div>
                        </div>
                        <div class="col-sm-4  buttonRow">
                            <div class="col-sm-3 paddingRight0">
                            </div>

                            <div class="col-sm-3 paddingRight0">
                                <asp:LinkButton ID="lbtnUpdate" CausesValidation="false" Visible="true" runat="server" CssClass="floatRight" OnClientClick="ConfirmUpdate()" OnClick="lbtnUpdate_Click">
                            <span class="glyphicon glyphicon-edit" aria-hidden="true"></span>Update
                                </asp:LinkButton>
                            </div>
                            <div class="col-sm-3 paddingRight0">
                                <asp:LinkButton ID="lbtnSave" CausesValidation="false" runat="server" CssClass="floatRight" OnClientClick="ConfirmSave()" OnClick="lbtnSave_Click">
                            <span class="glyphicon glyphicon-saved" aria-hidden="true"></span>Save
                                </asp:LinkButton>
                            </div>
                            <div class="col-sm-3 paddingRight0">
                                <asp:LinkButton ID="lbtnCancel" CausesValidation="false" runat="server" CssClass="floatRight" OnClick="lbtnCancel_Click">
                            <span class="glyphicon glyphicon-remove" aria-hidden="true"></span>Cancel
                                </asp:LinkButton>
                            </div>
                        </div>
                        <div class="col-sm-1  buttonRow paddingLeft0">
                            <div class="col-sm-12 paddingLeft5">
                                <asp:LinkButton ID="lbtnClear" runat="server" CssClass="floatRight" OnClientClick="ConfirmClear()" OnClick="lbtnClear_Click">
                            <span class="glyphicon glyphicon-refresh" aria-hidden="true"></span>Clear
                                </asp:LinkButton>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-sm-12">
                            <div class="panel panel-default">
                                <div class="panel-heading">Reservation Approval</div>
                                <div class="panel-body">
                                    <div class="row">
                                        <div class="col-sm-6">
                                            <div class="panel panel-default">
                                                <div class="panel-heading">Request</div>
                                                <div class="panel-body">
                                                    <div class="row">
                                                        <div class="col-sm-12 height5">
                                                        </div>
                                                    </div>
                                                    <div class="row">
                                                        <div class="col-sm-11">
                                                            <div class="panel panel-default">
                                                                <div class="panel-body">
                                                                    <div class="row">
                                                                        <div class="col-sm-6">
                                                                            <div class="col-sm-4 labelText1">
                                                                                Customer
                                                                            </div>
                                                                            <div class="col-sm-7 paddingRight5">
                                                                                <asp:TextBox ID="txtCustomer" CausesValidation="false" runat="server" class="form-control" AutoPostBack="true" OnTextChanged="txtCustomer_TextChanged"></asp:TextBox>
                                                                            </div>
                                                                            <div class="col-sm-1 paddingLeft0">
                                                                                <asp:LinkButton ID="lbtnCustomer" runat="server" OnClick="lbtnCustomer_Click">
                                                                                    <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                                                </asp:LinkButton>
                                                                            </div>
                                                                        </div>
                                                                        <div class="col-sm-6">
                                                                            <asp:Label ID="lblCustomerName" Text="" runat="server"></asp:Label>
                                                                        </div>
                                                                    </div>
                                                                    <div class="row">
                                                                        <div class="col-sm-12 height5">
                                                                        </div>
                                                                    </div>
                                                                    <div class="row">
                                                                        <div class="col-sm-6">
                                                                            <div class="col-sm-4 labelText1">
                                                                                Channel
                                                                            </div>
                                                                            <div class="col-sm-7 paddingRight5">
                                                                                <asp:TextBox ID="txtChannel" CausesValidation="false" runat="server" class="form-control" AutoPostBack="true" OnTextChanged="txtChannel_TextChanged"></asp:TextBox>
                                                                            </div>
                                                                            <div class="col-sm-1 paddingLeft0">
                                                                                <asp:LinkButton ID="lbtnChannel" runat="server" OnClick="lbtnChannel_Click">
                                                                                    <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                                                </asp:LinkButton>
                                                                            </div>
                                                                        </div>
                                                                        <div class="col-sm-6">
                                                                            <div class="col-sm-4 labelText1">
                                                                                Profit Center
                                                                            </div>
                                                                            <div class="col-sm-7 paddingRight5">
                                                                                <asp:TextBox ID="txtProfitCenter" CausesValidation="false" runat="server" class="form-control" AutoPostBack="true" OnTextChanged="txtProfitCenter_TextChanged"></asp:TextBox>
                                                                            </div>
                                                                            <div class="col-sm-1 paddingLeft0">
                                                                                <asp:LinkButton ID="lbtnProfitCenter" runat="server" OnClick="lbtnProfitCenter_Click">
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
                                                                        <div class="col-sm-6">
                                                                            <div class="col-sm-4 labelText1">
                                                                                From Date
                                                                            </div>
                                                                            <div class="col-sm-7 paddingRight5">
                                                                                <asp:TextBox ID="txtFromDate" runat="server" CssClass="form-control" onkeypress="return RestrictSpace()"
                                                                                    Format="dd/MMM/yyyy" Enabled="false"></asp:TextBox>
                                                                            </div>
                                                                            <div class="col-sm-1 paddingLeft0">
                                                                                <asp:LinkButton ID="lbtnFromDate" runat="server" TabIndex="1" CausesValidation="false">
                                                                                    <span class="glyphicon glyphicon-calendar" aria-hidden="true"></span>
                                                                                </asp:LinkButton>
                                                                                <asp:CalendarExtender ID="calexdate1" runat="server" TargetControlID="txtFromDate"
                                                                                    PopupButtonID="lbtnFromDate" Format="dd/MMM/yyyy">
                                                                                </asp:CalendarExtender>
                                                                            </div>
                                                                        </div>
                                                                        <div class="col-sm-6">
                                                                            <div class="col-sm-4 labelText1">
                                                                                To Date
                                                                            </div>
                                                                            <div class="col-sm-7 paddingRight5">
                                                                                <asp:TextBox ID="txtToDate" runat="server" CssClass="form-control" onkeypress="return RestrictSpace()"
                                                                                    Format="dd/MMM/yyyy" Enabled="false"></asp:TextBox>
                                                                            </div>
                                                                            <div class="col-sm-1 paddingLeft0">
                                                                                <asp:LinkButton ID="lbtnToDate" runat="server" TabIndex="1" CausesValidation="false">
                                                                                    <span class="glyphicon glyphicon-calendar" aria-hidden="true"></span>
                                                                                </asp:LinkButton>
                                                                                <asp:CalendarExtender ID="calexdate" runat="server" TargetControlID="txtToDate"
                                                                                    PopupButtonID="lbtnToDate" Format="dd/MMM/yyyy">
                                                                                </asp:CalendarExtender>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="col-sm-1 paddingLeft0">
                                                            <div class="panel panel-default">
                                                                <div class="panel panel-heading height85 paddingLeft0 paddingRight0 marginBottom0">
                                                                    <asp:LinkButton ID="lbtnSearchRequest" CausesValidation="false" runat="server" CssClass="floatRight" OnClick="lbtnSearchRequest_Click">
                                                                        <span class="glyphicon glyphicon-search search1" aria-hidden="true"></span>
                                                                    </asp:LinkButton>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="row">
                                                        <div class="col-sm-12 height5">
                                                        </div>
                                                    </div>
                                                    <div class="row">
                                                        <div class="col-sm-12 GridScroll175">
                                                            <asp:GridView ID="grdRequest" CssClass="table table-hover table-striped" runat="server" GridLines="None" ShowHeaderWhenEmpty="True"
                                                                EmptyDataText="No data found..." AutoGenerateColumns="False" OnSelectedIndexChanged="grdRequest_SelectedIndexChanged">
                                                                <Columns>
                                                                    <asp:TemplateField HeaderText="Code" Visible="false">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblLineNo" runat="server" Text='<%# Bind("ITR_SEQ_NO") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:ButtonField Text="Select" CommandName="Select" ItemStyle-Width="15" />
                                                                    <asp:BoundField DataField="ITR_REQ_NO" HeaderText="Request No" ReadOnly="true" />
                                                                    <asp:BoundField DataField="ITR_DT" HeaderText="Request Date" DataFormatString="{0:dd/MMM/yyyy}" ReadOnly="true" />
                                                                    <asp:BoundField DataField="ITR_BUS_CODE" HeaderText="Customer" ReadOnly="true" />
                                                                    <asp:BoundField DataField="ITR_CRE_BY" HeaderText="Request By" ReadOnly="true" />
                                                                    <asp:BoundField DataField="itr_anal1" HeaderText="Request Type" ReadOnly="true" />
                                                                     <asp:TemplateField HeaderText="Code" Visible="false">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblITR_REQ_NO" runat="server" Text='<%# Bind("ITR_REQ_NO") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField>
                                                                        <ItemTemplate>
                                                                            <div style="margin-top:-3px;">
                                                                                <asp:LinkButton ID="lbtnShowAppAll" ToolTip="Approve All" runat="server" CausesValidation="false" OnClick="lbtnShowAppAll_Click">
                                                                                 <span class="glyphicon glyphicon-new-window" aria-hidden="true"></span>
                                                                            </asp:LinkButton>
                                                                            </div>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                </Columns>
                                                            </asp:GridView>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-sm-6 paddingLeft0">
                                            <div class="row">
                                                <div class="col-sm-12">
                                                    <div class="panel panel-default">
                                                        <div class="panel-body">
                                                            <div class="row">
                                                                <div class="col-sm-12">
                                                                    <div class="col-sm-6">
                                                                        <div class="row">
                                                                            <div class="col-sm-4 labelText1">
                                                                                Reservation #
                                                                            </div>
                                                                            <div class="col-sm-7 paddingRight5">
                                                                                <asp:TextBox ID="txtReservationNo" CausesValidation="false" ReadOnly="true" runat="server" AutoPostBack="true" class="form-control" OnTextChanged="txtReservationNo_TextChanged"></asp:TextBox>
                                                                            </div>
                                                                            <div class="col-sm-1 paddingLeft0">
                                                                                <asp:LinkButton ID="lbtReservationNo" runat="server" OnClick="lbtReservationNo_Click">
                                                                                    <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                                                </asp:LinkButton>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                    <div class="col-sm-6">
                                                                        <div class="row">
                                                                            <div class="col-sm-4 labelText1">
                                                                                Reservation Date
                                                                            </div>
                                                                            <div class="col-sm-7 paddingRight5">
                                                                                <asp:TextBox ID="txtReservationDate" runat="server" class="form-control" ReadOnly="True"></asp:TextBox>
                                                                            </div>
                                                                            <div class="col-sm-1 paddingLeft0">
                                                                                <asp:LinkButton ID="lbtntxtReservationDate" runat="server" CausesValidation="false">
                                                                                    <span class="glyphicon glyphicon-calendar" aria-hidden="true"></span>
                                                                                </asp:LinkButton>
                                                                                <asp:CalendarExtender ID="CalendarExtender3" runat="server" TargetControlID="txtReservationDate"
                                                                                    PopupButtonID="lbtntxtReservationDate" Format="dd/MMM/yyyy">
                                                                                </asp:CalendarExtender>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                    <div class="row">
                                                                        <div class="col-sm-12 height5">
                                                                        </div>
                                                                    </div>
                                                                    <div class="col-sm-6">
                                                                        <div class="row">
                                                                            <div class="col-sm-4 labelText1">
                                                                                Customer
                                                                            </div>
                                                                            <div class="col-sm-8 paddingRight5">
                                                                                <asp:Label ID="lblCustomer" Text="" runat="server"></asp:Label>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                    <div class="col-sm-6">
                                                                        <div class="row">
                                                                            <div class="col-sm-4 labelText1">
                                                                                Reservation Type
                                                                            </div>
                                                                            <div class="col-sm-8 paddingRight5 labelText1">
                                                                                <asp:Label ID="lblreservationtype" runat="server"></asp:Label>
                                                                                <asp:Label ID="lvltypevalue" Visible="false" runat="server"></asp:Label>
                                                                                <asp:DropDownList ID="ddlReservationType" Visible="false" runat="server" class="form-control">
                                                                                </asp:DropDownList>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                     <div class="col-sm-6">
                                                                        <div class="row">
                                                                            <div class="col-sm-4 labelText1">
                                                                                Expected Date
                                                                            </div>
                                                                            <div class="col-sm-7 paddingRight5">
                                                                                <asp:TextBox ID="txtExpectedDate" runat="server" class="form-control" Enabled="false"></asp:TextBox>
                                                                            </div>
                                                                            <div class="col-sm-1 paddingLeft0">
                                                                                <asp:LinkButton ID="lbtntxtExpectedDate" runat="server" CausesValidation="false">
                                                                                    <span class="glyphicon glyphicon-calendar" aria-hidden="true"></span>
                                                                                </asp:LinkButton>
                                                                                <asp:CalendarExtender ID="CalendarExtender6" runat="server" TargetControlID="txtExpectedDate"
                                                                                    PopupButtonID="lbtntxtExpectedDate" Format="dd/MMM/yyyy">
                                                                                </asp:CalendarExtender>
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
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-12 height5">
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-12">
                                                    <div class="panel panel-default">
                                                        <div class="panel-heading">Inventory Balance</div>
                                                        <div class="panel-body">
                                                            <div class="row">
                                                                <div class="col-sm-12 height5">
                                                                </div>
                                                            </div>
                                                            <div class="row">
                                                                <div class="col-sm-12">
                                                                    <div class="panel panel-default">
                                                                        <div class="panel-heading">
                                                                            <div class="row">
                                                                                <div class="col-sm-12 paddingLeft0">
                                                                                    <div class="col-sm-6">
                                                                                        <div class="col-sm-1">
                                                                                            <asp:RadioButton ID="rbtnInStock" AutoPostBack="true" runat="server" OnCheckedChanged="rbtnInStock_CheckedChanged" />
                                                                                        </div>
                                                                                        <div class="col-sm-4 labelText1">
                                                                                            In Stock
                                                                                        </div>
                                                                                        <div class="col-sm-1">
                                                                                            <asp:RadioButton ID="rbtnPenddingOrders" AutoPostBack="true" runat="server" OnCheckedChanged="rbtnPenddingOrders_CheckedChanged" />
                                                                                        </div>
                                                                                        <div class="col-sm-5 labelText1">
                                                                                            Pending Orders
                                                                                        </div>
                                                                                    </div>
                                                                                    <div class="col-sm-6">
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
                                                            <asp:Panel ID="Panel1" runat="server">
                                                                <div class="row">
                                                                    <div class="col-sm-11">
                                                                        <div class="panel panel-default">
                                                                            <div class="panel-body">
                                                                                <asp:Panel ID="Panel3" runat="server" Visible="false">
                                                                                    <div class="row">
                                                                                        <div class="col-sm-6">
                                                                                            <div class="col-sm-4 labelText1">
                                                                                                Type
                                                                                            </div>
                                                                                            <div class="col-sm-8">
                                                                                                <asp:DropDownList ID="ddlOrderType" runat="server" class="form-control">
                                                                                                    <asp:ListItem Text=" - - Select - - " Value="0" Selected="True"></asp:ListItem>
                                                                                                    <asp:ListItem Text="Local" Value="L"></asp:ListItem>
                                                                                                    <asp:ListItem Text="Import" Value="I"></asp:ListItem>
                                                                                                </asp:DropDownList>
                                                                                            </div>
                                                                                        </div>
                                                                                        <div class="col-sm-6">
                                                                                            <div class="col-sm-4 labelText1">
                                                                                                Supplier
                                                                                            </div>
                                                                                            <div class="col-sm-7 paddingRight5">
                                                                                                <asp:TextBox ID="txtSupplier" CausesValidation="false" runat="server" AutoPostBack="true" class="form-control" OnTextChanged="txtSupplier_TextChanged"></asp:TextBox>
                                                                                            </div>
                                                                                            <div class="col-sm-1 paddingLeft0">
                                                                                                <asp:LinkButton ID="lbtnSupplier" runat="server" OnClick="lbtnSupplier_Click">
                                                                                                <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                                                                </asp:LinkButton>
                                                                                            </div>
                                                                                        </div>
                                                                                    </div>
                                                                                </asp:Panel>
                                                                                <div class="row">
                                                                                    <div class="col-sm-12 height5">
                                                                                    </div>
                                                                                </div>
                                                                                <div class="row">
                                                                                    <div class="col-sm-4 paddingRight0">
                                                                                        <div class="col-sm-4 labelText1">
                                                                                            BL #
                                                                                        </div>
                                                                                        <div class="col-sm-7 paddingRight5 paddingLeft5">
                                                                                            <asp:TextBox ID="txtOrderNo" CausesValidation="false" ReadOnly="true" runat="server" AutoPostBack="true" class="form-control"></asp:TextBox>
                                                                                        </div>
                                                                                        <div class="col-sm-1 paddingLeft0">
                                                                                            <asp:LinkButton ID="lbtnOrderNo" runat="server" OnClick="lbtnOrderNo_Click">
                                                                                                <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                                                            </asp:LinkButton>
                                                                                        </div>
                                                                                    </div>
                                                                                    <div class="col-sm-8 paddingLeft0">
                                                                                        <div class="col-sm-12">
                                                                                            <div class="row">
                                                                                                <div class="col-sm-6 paddingLeft0 paddingRight0">
                                                                                                    <div class="col-sm-5 labelText1">
                                                                                                        From Date
                                                                                                    </div>
                                                                                                    <div class="col-sm-6 paddingRight5 paddingLeft5">
                                                                                                        <asp:TextBox ID="txtIBFromDate" runat="server" CssClass="form-control" onkeypress="return RestrictSpace()"
                                                                                                            Format="dd/MMM/yyyy" Enabled="false"></asp:TextBox>
                                                                                                    </div>
                                                                                                    <div class="col-sm-1 paddingLeft0">
                                                                                                        <asp:LinkButton ID="lbtnIBFromDate" runat="server" TabIndex="1" CausesValidation="false">
                                                                                                            <span class="glyphicon glyphicon-calendar" aria-hidden="true"></span>
                                                                                                        </asp:LinkButton>
                                                                                                        <asp:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtIBFromDate"
                                                                                                            PopupButtonID="lbtnIBFromDate" Format="dd/MMM/yyyy">
                                                                                                        </asp:CalendarExtender>
                                                                                                    </div>
                                                                                                </div>
                                                                                                <div class="col-sm-6 paddingLeft0 paddingRight0">
                                                                                                    <div class="col-sm-4 labelText1">
                                                                                                        To Date
                                                                                                    </div>
                                                                                                    <div class="col-sm-7 paddingRight5 paddingLeft5">
                                                                                                        <asp:TextBox ID="txtIBToDate" runat="server" CssClass="form-control" onkeypress="return RestrictSpace()"
                                                                                                            Format="dd/MMM/yyyy" Enabled="false"></asp:TextBox>
                                                                                                    </div>
                                                                                                    <div class="col-sm-1 paddingLeft0 paddingRight0">
                                                                                                        <asp:LinkButton ID="lbtnIBToDate" runat="server" TabIndex="1" CausesValidation="false">
                                                                                                            <span class="glyphicon glyphicon-calendar" aria-hidden="true"></span>
                                                                                                        </asp:LinkButton>
                                                                                                        <asp:CalendarExtender ID="CalendarExtender4" runat="server" TargetControlID="txtIBToDate"
                                                                                                            PopupButtonID="lbtnIBToDate" Format="dd/MMM/yyyy">
                                                                                                        </asp:CalendarExtender>
                                                                                                    </div>
                                                                                                </div>
                                                                                            </div>
                                                                                        </div>
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                    <div class="col-sm-1 paddingLeft0">
                                                                        <div class="panel panel-default">
                                                                            <div class="panel panel-heading height59 paddingLeft0 paddingRight0 marginBottom0">
                                                                                <asp:LinkButton ID="lbtnPenddingOrders" CausesValidation="false" runat="server" CssClass="floatRight" OnClick="lbtnPenddingOrders_Click">
                                                                                    <span class="glyphicon glyphicon-search search3" aria-hidden="true"></span>
                                                                                </asp:LinkButton>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </asp:Panel>
                                                            <asp:Panel ID="Panel2" runat="server">
                                                                <div class="row">
                                                                    <div class="col-sm-12">
                                                                        <div class="col-sm-6 paddingLeft5">
                                                                            <div class="col-sm-4 labelText1 paddingLeft0">
                                                                                location  Type
                                                                            </div>
                                                                            <div class="col-sm-8 paddingLeft0">
                                                                                <asp:DropDownList ID="ddlWarehouseType" runat="server" class="form-control">
                                                                                </asp:DropDownList>
                                                                            </div>
                                                                        </div>
                                                                        <div class="col-sm-1 paddingLeft0">
                                                                            <div class="panel panel-default">
                                                                                <div class="panel panel-heading height20 marginBottom0">
                                                                                    <asp:LinkButton ID="lbtnInStock" CausesValidation="false" runat="server" CssClass="floatRight margintop-7" OnClick="lbtnInStock_Click">
                                                                                    <span class="glyphicon glyphicon-search search2" aria-hidden="true"></span>
                                                                                    </asp:LinkButton>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                        <div class="col-sm-5">
                                                                            <div class="col-sm-2 paddingRight0">
                                                                            </div>
                                                                            <div class="col-sm-4 labelText1 paddingLeft0">
                                                                            </div>
                                                                            <div class="col-sm-2 paddingRight0">
                                                                            </div>
                                                                            <div class="col-sm-4 labelText1 paddingLeft0">
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
                                                                <div class="col-sm-12 GridScroll120">
                                                                    <asp:GridView ID="grdInventoryBalance" CssClass="table table-hover table-striped" runat="server" GridLines="None" ShowHeaderWhenEmpty="True"
                                                                        EmptyDataText="No data found..." AutoGenerateColumns="False">
                                                                        <Columns>
                                                                            <asp:TemplateField>
                                                                                <ItemTemplate>
                                                                                    <asp:RadioButton ID="rbtnInventoryBalance" runat="server" onclick="CheckBoxCheck(this);" GroupName="InventoryBalance" AutoPostBack="true"
                                                                                        OnCheckedChanged="rbtnInventoryBalance_CheckedChanged" />
                                                                                </ItemTemplate>
                                                                                <ItemStyle HorizontalAlign="Center" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Code" Visible="true">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblCode" runat="server" Text='<%# Bind("ML_LOC_CD") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:BoundField DataField="ML_LOC_DESC" HeaderText="Location" ReadOnly="true" />
                                                                            <%--<asp:BoundField DataField="MIS_DESC" HeaderText="Status" ReadOnly="true" />--%>
                                                                             <asp:TemplateField HeaderText="Status" Visible="true">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblStatus" runat="server" Text='<%# Bind("MIS_DESC") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:BoundField DataField="INL_QTY" HeaderText="Phy.Qty" ReadOnly="true" />
                                                                          
                                                                            <asp:BoundField DataField="INL_FREE_QTY" HeaderText="Free Qty" ReadOnly="true" />
                                                                            <asp:BoundField DataField="INL_RES_QTY" HeaderText="Approve Qty" ReadOnly="true" />
                                                                        </Columns>
                                                                    </asp:GridView>

                                                                    <asp:GridView ID="grdPendingOrderBalance" CssClass="table table-hover table-striped" runat="server" GridLines="None" ShowHeaderWhenEmpty="True"
                                                                        EmptyDataText="No data found..." AutoGenerateColumns="False">
                                                                        <Columns>
                                                                            <asp:TemplateField>
                                                                                <ItemTemplate>
                                                                                    <asp:RadioButton ID="rbtnOrderBalance" runat="server" onclick="CheckBoxCheckBL(this);" GroupName="BLBalance" AutoPostBack="true"
                                                                                        OnCheckedChanged="rbtnOrderBalance_CheckedChanged" />
                                                                                </ItemTemplate>
                                                                                <ItemStyle HorizontalAlign="Center" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="BL #">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="col_ib_bl_no" runat="server" Text='<%# Bind("ib_bl_no") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Item Code">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="col_ibi_itm_cd" runat="server" Text='<%# Bind("ibi_itm_cd") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Description">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="col_mi_shortdesc" runat="server" Text='<%# Bind("mi_shortdesc") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Qty">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="col_ibi_qty" runat="server" Text='<%# Bind("ibi_qty") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Cusdec.Qty">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="col_ibi_req_qty" runat="server" Text='<%# Bind("ibi_req_qty") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Res.Qty">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="col_ibi_req_res_qty" runat="server" Text='<%# Bind("ibi_req_res_qty") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                         <asp:TemplateField HeaderText="line no" Visible="false">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblibi_line" runat="server" Text='<%# Bind("ibi_line") %>'></asp:Label>
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
                                    <div class="row">
                                        <div class="col-sm-6">
                                            <div class="panel panel-default">
                                                <div class="panel-body">
                                                    <div class="row">
                                                        <div class="col-sm-12 GridScroll170">
                                                            <asp:GridView ID="grdRequestItem" CssClass="table table-hover table-striped" runat="server" GridLines="None" ShowHeaderWhenEmpty="True"
                                                                EmptyDataText="No data found..." AutoGenerateColumns="False">
                                                                <Columns>
                                                                    <%--<asp:TemplateField HeaderText="SeqNo" Visible="false">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblSeqNo" runat="server" Text='<%# Bind("ITRI_SEQ_NO") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>--%>
                                                                    <asp:TemplateField>
                                                                        <ItemTemplate>
                                                                            <asp:CheckBox ID="chkRequestItem" runat="server" AutoPostBack="true" OnCheckedChanged="chkRequestItem_CheckedChanged" />
                                                                        </ItemTemplate>
                                                                        <ItemStyle HorizontalAlign="Center" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField>
                                                                        <ItemTemplate>
                                                                            <asp:LinkButton ID="lbtnGrdSerial" runat="server" CausesValidation="false" OnClick="lbtnGrdSerial_Click">
                                                                                <span aria-hidden="true" class="glyphicon glyphicon-th-list"></span>
                                                                            </asp:LinkButton>
                                                                        </ItemTemplate>
                                                                        <ItemStyle HorizontalAlign="Center" />
                                                                    </asp:TemplateField>
                                                                    <asp:BoundField DataField="ITRI_ITM_CD" HeaderText="Item Code" ReadOnly="true" />
                                                                    <asp:TemplateField HeaderText="Code" Visible="false">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblLineNo" runat="server" Text='<%# Bind("ITRI_LINE_NO") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:BoundField DataField="" HeaderText="Chan. Item Code" ReadOnly="true" Visible="false" />
                                                                    <asp:BoundField DataField="MI_MODEL" HeaderText="Model" ReadOnly="true" />
                                                                    <asp:TemplateField HeaderText="Code" Visible="false">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblCode" runat="server" Text='<%# Bind("MIS_CD") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:BoundField DataField="MIS_DESC" HeaderText="Status" ReadOnly="true" />
                                                                    <asp:BoundField DataField="ITRI_QTY" HeaderText="Qty" ReadOnly="true" />
                                                                    <asp:BoundField DataField="ITRI_APP_QTY" HeaderText="A. Qty" ReadOnly="true" />
                                                                      <asp:TemplateField HeaderText="Code" Visible="false">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblitri_loc" runat="server" Text='<%# Bind("itri_loc") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                      <asp:TemplateField HeaderText="Code" Visible="false">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblitri_com" runat="server" Text='<%# Bind("itri_com") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Approve.Qty">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txtReqQty" class="form-control" runat="server" onkeydown="return jsDecimals(event);"></asp:TextBox>
                                                                        </ItemTemplate>
                                                                        <ItemStyle Width="60px" HorizontalAlign="Center" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField>
                                                                        <ItemTemplate>
                                                                            <asp:LinkButton ID="lbtnRequestItemAdd" runat="server" CausesValidation="false" OnClick="lbtnRequestItemAdd_Click">
                                                                                <span aria-hidden="true" class="glyphicon glyphicon-plus"></span>
                                                                            </asp:LinkButton>
                                                                        </ItemTemplate>
                                                                        <ItemStyle Width="5%" HorizontalAlign="Center" />
                                                                    </asp:TemplateField>
                                                                </Columns>
                                                            </asp:GridView>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-sm-6 paddingLeft0">
                                            <div class="panel panel-default">
                                                <div class="panel-heading">Reservation Items</div>
                                                <div class="panel-body">
                                                    <div class="row">
                                                        <div class="col-sm-12 height140">
                                                            <asp:GridView ID="grdReservationItems" CssClass="table table-hover table-striped" runat="server" GridLines="None" ShowHeaderWhenEmpty="True"
                                                                EmptyDataText="No data found..." AutoGenerateColumns="False">
                                                                <Columns>
                                                                    <asp:BoundField DataField="IRD_ITM_CD" HeaderText="Item Code" ReadOnly="true" />
                                                                    <asp:BoundField DataField="MI_MODEL" HeaderText="Model" ReadOnly="true" />
                                                                    <asp:BoundField DataField="MIS_DESC" HeaderText="Status" ReadOnly="true" />
                                                                    <asp:BoundField DataField="IRD_RES_QTY" HeaderText="Qty" ReadOnly="true" />
                                                                    <asp:TemplateField>
                                                                        <ItemTemplate>
                                                                            <asp:LinkButton ID="lbtnGrdItemDalete" OnClientClick="DeleteConfirm()" runat="server" Visible='<%#GetDaleteVisibility()%>' CausesValidation="false" OnClick="lbtnGrdItemDalete_Click">
                                                                                <span aria-hidden="true" class="glyphicon glyphicon-trash"></span>
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
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

    <asp:ScriptManagerProxy ID="ScriptManagerProxy7" runat="server"></asp:ScriptManagerProxy>
    <asp:UpdatePanel ID="UpdatePanel8" runat="server">
        <ContentTemplate>
            <asp:Button ID="Button3" runat="server" Text="Button" Style="display: none;" />
            <asp:ModalPopupExtender ID="UserPopup" runat="server" Enabled="True" TargetControlID="Button3"
                PopupControlID="pnlpopup" PopupDragHandleControlID="PopupHeader" Drag="true" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>

        </ContentTemplate>

    </asp:UpdatePanel>
    <%-- Style="display: none"--%>
    <asp:Panel runat="server" ID="pnlpopup" DefaultButton="lbtnSearch">
        <div runat="server" id="test" class="panel panel-default height400 width1085">
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
                                <asp:UpdatePanel ID="UpdatePanel6" runat="server">
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



    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <asp:Button ID="btnSerial" runat="server" Text="" Style="display: none;" />
            <asp:ModalPopupExtender ID="PopupSerial" runat="server" Enabled="True" TargetControlID="btnSerial"
                PopupControlID="pnlSerial" CancelControlID="lbtnSerialClose" PopupDragHandleControlID="PopupHeader" Drag="true" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>
            <asp:Panel runat="server" ID="pnlSerial" DefaultButton="lbtnSearch">
                <div runat="server" id="Div1" class="panel panel-primary Mheight">
                    <asp:Label ID="lblSerialvalue" runat="server" Text="" Visible="false"></asp:Label>
                    <div class="panel panel-default">
                        <div class="panel-heading">
                            <asp:LinkButton ID="lbtnSerialClose" runat="server">
                                <span class="glyphicon glyphicon-remove" aria-hidden="true">Close</span>
                            </asp:LinkButton>
                            <div class="col-sm-11">
                                Serial Details
                            </div>
                            <div class="col-sm-1">
                            </div>
                        </div>
                        <div class="panel-body">
                            <div class="row">
                                <div class="col-sm-12">
                                    <div class="panel panel-default">
                                       
                                        <div class="panel-body">
                                            <div class="row">
                                                <div class="col-sm-12">
                                                    <asp:GridView ID="grdSerial" CausesValidation="false" runat="server" GridLines="None" CssClass="table table-hover table-striped"
                                                        AutoGenerateColumns="False" ShowHeaderWhenEmpty="True" EmptyDataText="No data found...">
                                                        <Columns>
                                                            <asp:BoundField DataField="ITRS_SER_1" HeaderText="Serial 1" ReadOnly="true" />
                                                            <asp:BoundField DataField="ITRS_SER_2" HeaderText="Serial 2" ReadOnly="true" />
                                                            <%--<asp:TemplateField>
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="lbtnSerialDalete" runat="server" CausesValidation="false">
                                                                        <span aria-hidden="true" class="glyphicon glyphicon-trash"></span>
                                                                    </asp:LinkButton>
                                                                </ItemTemplate>
                                                                <ItemStyle Width="5%" HorizontalAlign="Center" />
                                                            </asp:TemplateField>--%>
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
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>

      <asp:UpdatePanel ID="UpdatePanel21" runat="server">
        <ContentTemplate>
            <asp:Button ID="Button7" runat="server" Text="Button" Style="display: none;" />
            <asp:ModalPopupExtender ID="UserDPopoup" runat="server" Enabled="True" TargetControlID="Button7"
                PopupControlID="pnlDpopup" CancelControlID="btnDClose" PopupDragHandleControlID="PopupHeader" Drag="true" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>

        </ContentTemplate>

    </asp:UpdatePanel>

    <asp:UpdatePanel ID="UpdatePanel22" runat="server">
        <ContentTemplate>
            <asp:Panel runat="server" ID="pnlDpopup" DefaultButton="lbtnSearch">
                <div runat="server" id="Div6" class="panel panel-default height400 width700">

                    <asp:Label ID="Label5" runat="server" Text="Label" Visible="false"></asp:Label>
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
                            <div class="col-sm-12" id="Div7" runat="server">
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
                                            <asp:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtFDate"
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
                                            <asp:CalendarExtender ID="CalendarExtender5" runat="server" TargetControlID="txtTDate"
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
                                        <asp:ScriptManagerProxy ID="ScriptManagerProxy8" runat="server"></asp:ScriptManagerProxy>
                                        <asp:UpdatePanel ID="UpdatePanel23" runat="server">
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
                                        <asp:ScriptManagerProxy ID="ScriptManagerProxy14" runat="server"></asp:ScriptManagerProxy>
                                        <asp:UpdatePanel ID="UpdatePanel24" runat="server">
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
                                        <asp:ScriptManagerProxy ID="ScriptManagerProxy15" runat="server"></asp:ScriptManagerProxy>
                                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
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
                                    runat="server" AssociatedUpdatePanelID="UpdatePanel22">

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
     <asp:UpdatePanel ID="UpdatePanel25" runat="server">
        <ContentTemplate>
            <asp:Button ID="Button1" runat="server" Text="Button" Style="display: none;" />
            <asp:ModalPopupExtender ID="SbuPopup" runat="server" Enabled="True" TargetControlID="Button1"
                PopupControlID="pnlSBU" PopupDragHandleControlID="PopupHeader" Drag="true" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>
        </ContentTemplate>
    </asp:UpdatePanel>

    <asp:Panel ID="pnlSBU" runat="server" align="center">
        <asp:Label ID="Label1" runat="server" Text="Label" Visible="false"></asp:Label>
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
<%-- pnl show appr all--%>
   <asp:UpdatePanel runat="server" ID="UpdatePanel15">
        <ContentTemplate>
            <asp:Button ID="btnpop1" runat="server" Text="Button" Style="display: none;" />
            <asp:ModalPopupExtender ID="popAppAllRes" runat="server" Enabled="True" TargetControlID="btnpop1"
                PopupControlID="pnlAppAllRes" PopupDragHandleControlID="PopupHeader" Drag="true" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdateProgress ID="UpdateProgress2" DisplayAfter="10" runat="server" AssociatedUpdatePanelID="upAppAllRes">
        <ProgressTemplate>
            <div class="divWaiting">
                <asp:Label ID="lblWaistres10" runat="server"
                    Text="Please wait... " />
                <asp:Image ID="imgWasitres10" runat="server"
                    ImageAlign="Middle" ImageUrl="~/images/icons/ajax-loader.gif" />
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <asp:Panel runat="server" ID="pnlAppAllRes">
        <asp:UpdatePanel runat="server" ID="upAppAllRes">
            <ContentTemplate>
                <div runat="server" id="Div5" class="panel panel-primary" style="padding: 5px;">
                    <div class="panel panel-default" style="height: 400px; width: 750px;">
                        <div class="panel-heading" style="height: 25px;">
                            <div class="col-sm-10">
                                <strong>Reservation Approval</strong>
                            </div>
                            <div class="col-sm-2 text-right">
                                <asp:LinkButton ID="lbtnAppAllResCls"  runat="server" OnClick="lbtnAppAllResCls_Click">
                                    <span class="glyphicon glyphicon-remove"  aria-hidden="true">Close</span>
                                </asp:LinkButton>
                            </div>
                        </div>
                        <div class="panel-body">
                            <div class="row">
                                <div class="col-sm-12">
                                    <div class="col-sm-4">
                                        <div class="col-sm-3">
                                            Location    
                                        </div>
                                        <div class="col-sm-6">
                                            <asp:TextBox ID="txtAllresLoc" OnTextChanged="txtAllresLoc_TextChanged" AutoPostBack="true" Style="text-transform: uppercase" 
                                              CssClass="form-control"  runat="server" />    
                                        </div>
                                        <div class="col-sm-1 padding03">
                                            <asp:LinkButton ID="lbtnAllResSerLoc" runat="server" CausesValidation="false" OnClick="lbtnAllResSerLoc_Click">
                                                        <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                            </asp:LinkButton>
                                        </div>
                                    </div>
                                    <div class="col-sm-4">
                                        <asp:Button Text="Approve All" OnClientClick="ConfirmSave()" runat="server" OnClick="lbtnAppAllCls_Click"/>
                                    </div>
                                </div>
                            </div>
                            <br />
                            <div class="row">
                                <div class="col-sm-12">
                                    <div style="height: 200px; overflow-y: scroll;">
                                        <asp:GridView ID="dgvAppAllRes" CssClass="table table-hover table-striped" runat="server" GridLines="None"
                                            ShowHeaderWhenEmpty="True" PagerStyle-CssClass="cssPager"
                                            EmptyDataText="No data found..." AutoGenerateColumns="False">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Item Code" Visible="True">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblitri_itm_cd" runat="server" Text='<%# Bind("itri_itm_cd") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="15%" />
                                                    <HeaderStyle Width="15%" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Description" Visible="True">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblMi_shortdesc" runat="server" Text='<%# Bind("Mi_shortdesc") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="20%" />
                                                    <HeaderStyle Width="20%" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Item Status" Visible="True">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblMis_desc" runat="server" Text='<%# Bind("Mis_desc") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="10%" />
                                                    <HeaderStyle Width="10%" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Item Status" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblitri_itm_stus" runat="server" Text='<%# Bind("itri_itm_stus") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Request Quantity" Visible="True">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblitri_bqty" runat="server" Text='<%# Bind("itri_bqty") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="10%" CssClass="gridHeaderAlignRight" />
                                                    <HeaderStyle Width="10%" CssClass="gridHeaderAlignRight" />
                                                </asp:TemplateField>
                                            </Columns>
                                            <PagerStyle CssClass="cssPager" />
                                        </asp:GridView>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </asp:Panel>

    <%-- pnl search --%>
    <asp:UpdatePanel runat="server" ID="upSearch">
        <ContentTemplate>
            <asp:Button ID="btn1" runat="server" Text="Button" Style="display: none;" />
            <asp:ModalPopupExtender ID="PopupSearch" runat="server" Enabled="True" TargetControlID="btn1"
                PopupControlID="pnlSearch" PopupDragHandleControlID="PopupHeader" Drag="true" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdateProgress ID="UpdateProgress3" DisplayAfter="10" runat="server" AssociatedUpdatePanelID="upPnlSerch">
        <ProgressTemplate>
            <div class="divWaiting">
                <asp:Label ID="lblWasit2" runat="server"
                    Text="Please wait... " />
                <asp:Image ID="imgsWait2" runat="server"
                    ImageAlign="Middle" ImageUrl="~/images/icons/ajax-loader.gif" />
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <asp:Panel runat="server" ID="pnlSearch" DefaultButton="lbtnSearch">
        <asp:UpdatePanel runat="server" ID="upPnlSerch">
            <ContentTemplate>
                <div runat="server" id="Div2" class="panel panel-primary" style="padding: 5px;">
                    <div class="panel panel-default" style="height: 350px; width: 700px;">
                        <div class="panel-heading" style="height: 25px;">
                            <div class="col-sm-10"></div>
                            <div class="col-sm-1">
                                <asp:LinkButton ID="lbtnClsClose" runat="server" OnClick="lbtnClsClose_Click">
                             <span class="glyphicon glyphicon-remove"  aria-hidden="true">Close</span>
                                </asp:LinkButton>
                            </div>
                        </div>
                        <div class="panel-body">
                            <div class="row height16">
                            </div>
                            <div class="row">
                                <div class="col-sm-6">
                                    <div class="col-sm-4 labelText1">
                                        Search by key
                                    </div>
                                    <asp:ScriptManagerProxy ID="ScriptManagerProxy1" runat="server"></asp:ScriptManagerProxy>
                                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                        <ContentTemplate>
                                            <div class="col-sm-6 paddingRight5">
                                                <asp:DropDownList ID="ddlSerByKey" runat="server" class="form-control" AutoPostBack="false">
                                                </asp:DropDownList>
                                            </div>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>

                                <div class="col-sm-6">
                                    <div class="col-sm-4 labelText1">
                                        Search by word
                                    </div>
                                    <div class="col-sm-6 paddingRight5">
                                        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                            <ContentTemplate>
                                                <asp:TextBox ID="txtSerByKey" CausesValidation="false" class="form-control" AutoPostBack="true" runat="server" OnTextChanged="btnSearch_Click"></asp:TextBox>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>
                                    <asp:ScriptManagerProxy ID="ScriptManagerProxy2" runat="server"></asp:ScriptManagerProxy>
                                    <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                        <ContentTemplate>
                                            <div class="col-sm-1 paddingLeft0">
                                                <asp:LinkButton ID="btnSearch" runat="server" OnClick="btnSearch_Click">
                                                    <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                </asp:LinkButton>
                                            </div>
                                        </ContentTemplate>

                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="txtSerByKey" />
                                        </Triggers>
                                    </asp:UpdatePanel>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-12 height16">
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-12">
                                    <asp:ScriptManagerProxy ID="ScriptManagerProxy3" runat="server"></asp:ScriptManagerProxy>
                                    <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                                        <ContentTemplate>
                                            <asp:GridView ID="dgvResult" CausesValidation="false" runat="server" AllowPaging="True"
                                                GridLines="None" CssClass="table table-hover table-striped" PageSize="10" PagerStyle-CssClass="cssPager" ShowHeaderWhenEmpty="true"
                                                EmptyDataText="No data found..."
                                                OnPageIndexChanging="dgvResult_PageIndexChanging" OnSelectedIndexChanged="dgvResult_SelectedIndexChanged">
                                                <Columns>
                                                    <asp:ButtonField Text="Select" CommandName="Select" ItemStyle-Width="12" />
                                                </Columns>
                                            </asp:GridView>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </asp:Panel>
</asp:Content>
