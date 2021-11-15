<%@ Page Title="" Language="C#" MasterPageFile="~/View/AdminSite.Master" AutoEventWireup="true" CodeBehind="ConsignmentReceiveNote.aspx.cs" Inherits="FastForward.SCMWeb.View.Transaction.Inventory.Consignment_Stock.ConsignmentReceiveNote" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/UserControls/ucInScan.ascx" TagPrefix="uc1" TagName="ucInScan" %>



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

        function ConfirmSendToPDA() {
            var selectedvaldelitm = confirm("Are you sure you want to send ?");
            if (selectedvaldelitm) {
                document.getElementById('<%=txtpdasend.ClientID %>').value = "Yes";
            } else {
                document.getElementById('<%=txtpdasend.ClientID %>').value = "No";
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

        function ConfirmCancel() {
            var selectedvaldelitm = confirm("Do you want to cancel ?");
            if (selectedvaldelitm) {
                document.getElementById('<%=txtcancenconfirm.ClientID %>').value = "Yes";
            } else {
                document.getElementById('<%=txtcancenconfirm.ClientID %>').value = "No";
            }
        };

        function ConfirmDelete() {
            var selectedvaldelitm = confirm("Do you want to remove ?");
            if (selectedvaldelitm) {
                document.getElementById('<%=txtconfirmdelete.ClientID %>').value = "Yes";
            } else {
                document.getElementById('<%=txtconfirmdelete.ClientID %>').value = "No";
            }
        };

        function Enable() {
            return;
        }

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
                sticky: false,
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

        document.documentElement.style.overflowX = 'hidden';

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
        runat="server" AssociatedUpdatePanelID="mainpnl">

        <ProgressTemplate>
            <div class="divWaiting">
                <asp:Label ID="lblWait" runat="server"
                    Text="Please wait... " />
                <asp:Image ID="imgWait" runat="server"
                    ImageAlign="Middle" ImageUrl="~/images/icons/ajax-loader.gif" />
            </div>
        </ProgressTemplate>

    </asp:UpdateProgress>

    <asp:UpdatePanel ID="mainpnl" runat="server">
        <ContentTemplate>

            <asp:HiddenField ID="txtconfirmclear" runat="server" />
            <asp:HiddenField ID="txtconfirmplaceord" runat="server" />
            <asp:HiddenField ID="txtcancenconfirm" runat="server" />
            <asp:HiddenField ID="txtconfirmdelete" runat="server" />
            <asp:HiddenField ID="txtpdasend" runat="server" />

            <div class="panel panel-default marginLeftRight5">

                <div class="row">

                    <div class="col-sm-12">

                        <div class="col-sm-1">
                            <strong>Send to PDA</strong>
                        </div>

                        <div class="col-sm-1">
                            <asp:CheckBox runat="server" ID="chkpda" Enabled="false" />
                        </div>

                        <div class="col-sm-3">
                            <asp:Button Text="Send to scan" ID="btnSentScan" Visible="true" runat="server" OnClick="btnSentScan_Click" OnClientClick="SendscanConfirm() " />

                        </div>
                    </div>

                </div>
                <div class="row">
                    <div class="col-sm-4  buttonrow">

                        <div visible="false" class="alert alert-success" role="alert" runat="server" id="divok">

                            <div class="col-sm-11  buttonrow ">
                                <strong>Well done!</strong>
                                <asp:Label ID="lblok" runat="server"></asp:Label>
                            </div>

                            <div class="col-sm-1  buttonrow">
                                <div class="col-sm-3  buttonrow">
                                    <asp:LinkButton ID="lbtndivokclose" runat="server" CausesValidation="false" CssClass="floatright">
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
                                    <asp:LinkButton ID="lbtndicalertclose" runat="server" CausesValidation="false" CssClass="floatright">
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
                                    <asp:LinkButton ID="lbtndivinfoclose" runat="server" CausesValidation="false" CssClass="floatright">
                                        <span class="glyphicon glyphicon-ok" aria-hidden="true"></span>
                                    </asp:LinkButton>
                                </div>
                            </div>
                        </div>

                    </div>
                    <div class="col-sm-2"></div>
                    <div class="col-sm-6  buttonRow ">


                        <div class="col-sm-2 paddingRight0">
                            <asp:LinkButton ID="lbtnprint" CausesValidation="false" runat="server" CssClass="floatleft" OnClick="lbtnprint_Click">
                            <span class="glyphicon glyphicon-print" aria-hidden="true"></span>Print
                            </asp:LinkButton>
                        </div>
                        <div class="col-sm-2 paddingRight0">
                            <asp:LinkButton ID="LinkButton2" CausesValidation="false" runat="server" CssClass="floatleft" OnClientClick="ConfirmPlaceOrder();" OnClick="LinkButton2_Click">
                            <span class="glyphicon glyphicon-saved" aria-hidden="true"></span>Temp Save
                            </asp:LinkButton>
                        </div>

                        <div class="col-sm-2 paddingRight15">
                            <asp:LinkButton ID="lbtnsave" CausesValidation="false" runat="server" CssClass="floatleft" OnClientClick="ConfirmPlaceOrder();" OnClick="lbtnsave_Click">
                            <span class="glyphicon glyphicon-saved" aria-hidden="true"></span>Save
                            </asp:LinkButton>
                        </div>

                        <div class="col-sm-2 paddingLeft15" style="margin-left: -20px">
                            <asp:LinkButton ID="lbtncancel" CausesValidation="false" runat="server" CssClass="floatleft" OnClientClick="ConfirmCancel();" OnClick="lbtncancel_Click">
                                                        <span class="glyphicon glyphicon-remove" aria-hidden="true"></span>Cancel
                            </asp:LinkButton>
                        </div>

                        <div class="col-sm-2">
                            <asp:LinkButton ID="LinkButton1" runat="server" CssClass="floatRight" OnClientClick="ConfirmClearForm();" OnClick="LinkButton1_Click">
                            <span class="glyphicon glyphicon-refresh" aria-hidden="true"></span>Clear
                            </asp:LinkButton>
                        </div>
                        <div class="col-sm-2">
                            <div id="Information" runat="server" visible="false" class="alert alert-success alert-info" role="alert">
                                <div class="col-sm-11">
                                    <strong>Info!</strong>
                                    <asp:Label ID="lblBackDateInfor" runat="server"></asp:Label>
                                    <asp:Label ID="lblH1" runat="server" Text="Label"></asp:Label>
                                </div>
                            </div>
                            <div>
                                <div>
                                </div>
                            </div>

                        </div>
                    </div>
                </div>

                <div class="row paddingtopbottom0">

                    <div class="panel-body paddingtopbottom0">

                        <div class="col-sm-12">

                            <div class="panel panel-default">

                                <div class="panel-heading pannelheading paddingtopbottom0">
                                    <b>Consignment Receive Note</b>
                                </div>

                                <div class="panel-body">
                                    <div class="row">
                                        <div class="col-sm-2">
                                            <div class="row">

                                                <div class="col-sm-3 labelText1">
                                                    From
                                                </div>

                                                <div>
                                                    <div class="col-sm-8">
                                                        <asp:TextBox ID="dtpFromDate" runat="server" CssClass="form-control" Format="dd/MMM/yyyy"
                                                            onkeypress="return RestrictSpace()" Enabled="false"></asp:TextBox>
                                                        <asp:CalendarExtender ID="calexdate" runat="server" TargetControlID="dtpFromDate"
                                                            PopupButtonID="lbtnfrm" Format="dd/MMM/yyyy">
                                                        </asp:CalendarExtender>
                                                    </div>

                                                    <div id="caldv" class="col-sm-1 paddingLeft0" style="margin-left: -10px; margin-top: -2px">
                                                        <asp:LinkButton ID="lbtnfrm" TabIndex="1" CausesValidation="false" runat="server">
                                                        <span class="glyphicon glyphicon-calendar" aria-hidden="true"></span>
                                                        </asp:LinkButton>
                                                    </div>
                                                </div>

                                            </div>
                                        </div>

                                        <div class="col-sm-2">
                                            <div class="row">

                                                <div class="col-sm-3 labelText1">
                                                    To
                                                </div>

                                                <div>
                                                    <div class="col-sm-8">
                                                        <asp:TextBox ID="dtpToDate" runat="server" CssClass="form-control" Format="dd/MMM/yyyy"
                                                            onkeypress="return RestrictSpace()" Enabled="false"></asp:TextBox>
                                                        <asp:CalendarExtender ID="calexreq" runat="server" TargetControlID="dtpToDate"
                                                            PopupButtonID="lbtnto" Format="dd/MMM/yyyy">
                                                        </asp:CalendarExtender>
                                                    </div>

                                                    <div id="caldv2" class="col-sm-1 paddingLeft0" style="margin-left: -10px; margin-top: -2px">
                                                        <asp:LinkButton ID="lbtnto" TabIndex="2" CausesValidation="false" runat="server">
                                                        <span class="glyphicon glyphicon-calendar" aria-hidden="true"></span>
                                                        </asp:LinkButton>
                                                    </div>
                                                </div>

                                            </div>
                                        </div>

                                        <div class="col-sm-2">
                                            <div class="row">

                                                <div class="col-sm-3 labelText1">
                                                    Supplier
                                                </div>

                                                <div class="col-sm-8 paddingRight5">
                                                    <asp:TextBox ID="txtFindSupplier" runat="server" ReadOnly="true" CssClass="form-control" AutoPostBack="true" OnTextChanged="txtFindSupplier_TextChanged"></asp:TextBox>
                                                </div>
                                                <div class="col-sm-1 paddingLeft0">
                                                    <asp:LinkButton ID="btnSearch_Supplier" runat="server" TabIndex="3" OnClick="btnSearch_Supplier_Click">
                                                        <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                    </asp:LinkButton>
                                                </div>

                                            </div>
                                        </div>

                                        <div class="col-sm-3">
                                            <div class="row">

                                                <div class="col-sm-2 labelText1">
                                                    Doc No
                                                </div>

                                                <div class="col-sm-1 labelText1">
                                                    <asp:CheckBox runat="server" ID="chktemp" AutoPostBack="true" OnCheckedChanged="chktemp_CheckedChanged" />
                                                </div>

                                                <div class="col-sm-1 labelText1 paddingLeft10">
                                                    <strong>Temp</strong>
                                                </div>

                                                <div class="col-sm-7 paddingRight5">
                                                    <asp:TextBox ID="txtFindPONoDoc" runat="server" CssClass="form-control" AutoPostBack="true" OnTextChanged="txtFindPONoDoc_TextChanged"></asp:TextBox>
                                                </div>
                                                <div class="col-sm-1 paddingLeft0">
                                                    <asp:LinkButton ID="btnSearch_PO" runat="server" TabIndex="4" OnClick="btnSearch_PO_Click">
                                                        <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                    </asp:LinkButton>
                                                </div>

                                            </div>
                                        </div>

                                        <div class="col-sm-2">
                                            <div class="row">

                                                <div class="col-sm-4 labelText1">
                                                    Request No
                                                </div>

                                                <div class="col-sm-7 paddingRight5">
                                                    <asp:TextBox ID="txtFindPONo" runat="server" CssClass="form-control" AutoPostBack="true" OnTextChanged="txtFindPONo_TextChanged"></asp:TextBox>
                                                </div>
                                                <div class="col-sm-1 paddingLeft0 ">
                                                    <asp:LinkButton ID="LinkButton3" runat="server" TabIndex="4" OnClick="LinkButton3_Click">
                                                        <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                    </asp:LinkButton>
                                                </div>

                                            </div>
                                        </div>

                                        <div class="col-sm-1">
                                            <div class="row">

                                                <%--   <div class="col-sm-9 labelText1">
                                                
                                            </div>--%>

                                                <div class="col-sm-1">
                                                    <asp:LinkButton ID="btnGetPO" runat="server" TabIndex="5" OnClick="btnGetPO_Click">
                                                        <span class="glyphicon glyphicon-search" aria-hidden="true" style="font-size:20px"></span>
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
                                        <div class="col-sm-12">
                                            <asp:TextBox runat="server" ID="txtUserSeqNo" Visible="false" Width="1px"></asp:TextBox>
                                            <div class="panel panel-default " id="dvContentsOrder1">
                                                <div class="panel-body ">

                                                    <div class="GHead" id="GHead"></div>
                                                    <div style="height: 85px; margin-top: -16px; overflow: auto;">
                                                        <asp:GridView ID="dvPendingPO" AutoGenerateColumns="false" runat="server" OnRowDataBound="dvPendingPO_RowDataBound" OnSelectedIndexChanged="dvPendingPO_SelectedIndexChanged" CssClass="dvPendingPO table table-hover table-striped" GridLines="None" EmptyDataText="No data found...">

                                                            <Columns>

                                                                <asp:CommandField ShowSelectButton="true" ButtonType="Link" />

                                                                <asp:TemplateField HeaderText="Request No">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblreqno" runat="server" Text='<%# Bind("POH_DOC_NO") %>' Width="150px"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                                <%--                                                                    <asp:TemplateField HeaderText="Profit Center" Visible="false">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblprofit" runat="server" Text='<%# Bind("POH_PROFIT_CD") %>' Width="100px"></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>--%>

                                                                <asp:TemplateField HeaderText="Request Date">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblreqdate" runat="server" Text='<%# Bind("POH_DT", "{0:dd/MMM/yyyy}") %>' Width="100px"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="Supplier Code">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblsupcode" runat="server" Text='<%# Bind("POH_SUPP") %>' Width="100px"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="Supplier Name">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblsupname" runat="server" Text='<%# Bind("MBE_NAME") %>' Width="200px"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="Ref.No">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblrefno" runat="server" Text='<%# Bind("POH_REF") %>' Width="150px"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="Remarks">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblremarks" runat="server" Text='<%# Bind("POH_REMARKS") %>' Width="200px"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                            </Columns>
                                                            <SelectedRowStyle BackColor="LightCyan" />
                                                        </asp:GridView>
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

                    </div>

                </div>

                <div class="row paddingtopbottom0 ">

                    <div class="panel-body paddingtopbottom0">

                        <div class="col-sm-12">

                            <div class="panel panel-default">

                                <div class="panel-heading pannelheading paddingtopbottom0">
                                    General Details
                                </div>

                                <div class="panel-body">

                                    <div class="col-sm-3">
                                        <div class="row">

                                            <div class="col-sm-3 labelText1">
                                                Date
                                            </div>

                                            <div>
                                                <div class="col-sm-8">
                                                    <asp:TextBox ID="dtpDODate" runat="server" CssClass="form-control" Format="dd/MMM/yyyy"
                                                        onkeypress="return RestrictSpace()" Enabled="false"></asp:TextBox>
                                                    <asp:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="dtpDODate"
                                                        PopupButtonID="lbtnfrm2" Format="dd/MMM/yyyy">
                                                    </asp:CalendarExtender>
                                                </div>

                                                <div id="calddv" class="col-sm-1 paddingLeft0" style="margin-left: -10px; margin-top: -2px">
                                                    <asp:LinkButton ID="lbtnfrm2" TabIndex="6" CausesValidation="false" runat="server">
                                                        <span class="glyphicon glyphicon-calendar" aria-hidden="true"></span>
                                                    </asp:LinkButton>
                                                </div>
                                            </div>

                                        </div>
                                    </div>

                                    <div class="col-sm-3">
                                        <div class="row">

                                            <div class="col-sm-5 labelText1">
                                                Supplier
                                            </div>

                                            <div class="col-sm-7 paddingRight5">
                                                <asp:TextBox ID="txtSuppCode" runat="server" CssClass="form-control" TabIndex="7"></asp:TextBox>
                                            </div>

                                        </div>
                                    </div>

                                    <div class="col-sm-3">
                                        <div class="row">

                                            <div class="col-sm-3 labelText1">
                                            </div>

                                            <div class="col-sm-9 paddingRight5">
                                                <asp:TextBox ID="txtSuppName" runat="server" CssClass="form-control" TabIndex="8"></asp:TextBox>
                                            </div>

                                        </div>
                                    </div>

                                    <div class="col-sm-3">
                                        <div class="row">

                                            <div class="col-sm-4 labelText1">
                                                Manual Ref #
                                            </div>

                                            <div class="col-sm-3 paddingRight5">
                                                <asp:TextBox ID="txtManualRefNo" Width="75px" runat="server" AutoPostBack="true" OnTextChanged="txtManualRefNo_TextChanged" CssClass="form-control"></asp:TextBox>
                                            </div>


                                            <div class="col-sm-5">
                                                <asp:CheckBox runat="server" ID="chkManualRef" Text="By Manual Doc" OnCheckedChanged="chkManualRef_CheckedChanged" AutoPostBack="true" TabIndex="10" />
                                            </div>

                                        </div>
                                    </div>

                                    <div class="col-sm-3">
                                        <div class="row">

                                            <div class="col-sm-3 labelText1">
                                                Request No
                                            </div>

                                            <div class="col-sm-9 paddingRight5">
                                                <asp:TextBox ID="txtPONo" runat="server" CssClass="form-control" TabIndex="11"></asp:TextBox>
                                            </div>

                                        </div>
                                    </div>

                                    <div class="col-sm-3">
                                        <div class="row">

                                            <div class="col-sm-5 labelText1">
                                                Request Date
                                            </div>

                                            <div>
                                                <div class="col-sm-6">
                                                    <asp:TextBox ID="dtpPODate" runat="server" CssClass="form-control" Format="dd/MMM/yyyy"
                                                        onkeypress="return RestrictSpace()" Enabled="false"></asp:TextBox>
                                                    <asp:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="dtpPODate"
                                                        PopupButtonID="lbtnfrm2dd" Format="dd/MMM/yyyy">
                                                    </asp:CalendarExtender>
                                                </div>

                                                <div id="calddvss" class="col-sm-1 paddingLeft0" style="margin-left: -10px; margin-top: -2px">
                                                    <asp:LinkButton ID="lbtnfrm2dd" TabIndex="12" CausesValidation="false" runat="server">
                                                        <span class="glyphicon glyphicon-calendar" aria-hidden="true"></span>
                                                    </asp:LinkButton>
                                                </div>
                                            </div>

                                        </div>
                                    </div>

                                    <div class="col-sm-3">
                                        <div class="row">

                                            <div class="col-sm-3 labelText1">
                                                Referance #
                                            </div>

                                            <div class="col-sm-9 paddingRight5">
                                                <asp:TextBox ID="txtPORefNo" runat="server" ReadOnly="true" CssClass="form-control" TabIndex="13"></asp:TextBox>
                                            </div>

                                        </div>
                                    </div>

                                    <div class="col-sm-3">
                                        <div class="row">

                                            <div class="col-sm-4 labelText1">
                                                Vehicle No
                                            </div>

                                            <div class="col-sm-8 paddingRight5">
                                                <asp:TextBox ID="txtVehicleNo" runat="server" CssClass="form-control" MaxLength="20" TabIndex="14"></asp:TextBox>
                                            </div>

                                        </div>
                                    </div>

                                    <div class="row">
                                        <div class="col-sm-12 height5">
                                        </div>
                                    </div>

                                    <div class="col-sm-3">
                                        <div class="row">

                                            <div class="col-sm-3 labelText1">
                                                Remarks
                                            </div>

                                            <div class="col-sm-9">
                                                <asp:TextBox ID="txtRemarks" runat="server" MaxLength="200" TabIndex="15" CssClass="form-control" Width="1195px" Height="25px" TextMode="MultiLine" Style="resize: none"></asp:TextBox>
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

                            <div class="panel panel-default " id="dvContentsOrder1sds">

                                <div class="panel-heading pannelheading paddingtopbottom0">
                                    Item Details
                                </div>

                                <div class="panel-body" id="panelbodydivd1ss">

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

                                                </ul>



                                            </div>

                                        </div>

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



                                                                <div class="panelscollmedium">

                                                                    <asp:GridView ID="grdItems" AutoGenerateColumns="false" runat="server" CssClass="table table-hover table-striped" GridLines="None"
                                                                        ShowHeaderWhenEmpty="True" EmptyDataText="No data found...">

                                                                        <Columns>

                                                                            <asp:TemplateField HeaderText="">
                                                                                <ItemTemplate>
                                                                                    <asp:LinkButton ID="lbtnadd" runat="server" CausesValidation="false" OnClick="lbtnadd_Click">
                                                                                                    <span class="glyphicon glyphicon-plus" aria-hidden="true"></span>
                                                                                    </asp:LinkButton>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField HeaderText="Seq.No" Visible="false">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblseqnoitm" runat="server" Text='<%# Bind("PODI_SEQ_NO") %>' Width="1px"></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField HeaderText="#" Visible="false">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblpodiline" runat="server" Text='<%# Bind("PODI_LINE_NO") %>' Width="1px"></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>

                                                                            <%--          <asp:TemplateField HeaderText="##" Visible="false">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblpodiline2" runat="server" Text='<%# Bind("PODI_DEL_LINE_NO") %>' Width="50px"></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>--%>

                                                                            <asp:TemplateField HeaderText="Item Code">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblitri_itm_cd" runat="server" Text='<%# Bind("PODI_ITM_CD") %>' Width="75px"></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField HeaderText="Description">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lbldesc" runat="server" Text='<%# Bind("MI_LONGDESC") %>' Width="175px"></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField HeaderText="Model">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblmodel" runat="server" Text='<%# Bind("MI_MODEL") %>' Width="110px"></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField HeaderText="Brand" Visible="false">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblbrand" runat="server" Text='<%# Bind("MI_BRAND") %>' Width="1px"></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField HeaderText="PO Qty">
                                                                                <ItemTemplate>
                                                                                    <div style="margin-left: -55px">
                                                                                        <asp:Label ID="lblitri_qty" runat="server" Text='<%# Bind("PODI_QTY","{0:N2}") %>' Width="100px"></asp:Label>
                                                                                    </div>
                                                                                </ItemTemplate>
                                                                                <ItemStyle HorizontalAlign="Center" />
                                                                                <HeaderStyle HorizontalAlign="Right" />
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField HeaderText="Remaining Qty">
                                                                                <ItemTemplate>
                                                                                    <div style="margin-left: -45px">
                                                                                        <asp:Label ID="lblremainqty" runat="server" Text='<%# Bind("PODI_BAL_QTY","{0:N2}") %>' Width="100px"></asp:Label>
                                                                                    </div>
                                                                                </ItemTemplate>
                                                                                <ItemStyle HorizontalAlign="Center" />
                                                                                <HeaderStyle HorizontalAlign="Right" />
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField HeaderText="Pick Qty">
                                                                                <ItemTemplate>
                                                                                    <div style="margin-left: -50px">
                                                                                        <asp:Label ID="lblpickqty" runat="server" Text='<%# Bind("GRN_QTY","{0:N2}") %>' Width="100px"></asp:Label>
                                                                                    </div>
                                                                                </ItemTemplate>
                                                                                <ItemStyle HorizontalAlign="Center" />
                                                                                <HeaderStyle HorizontalAlign="Right" />
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField HeaderText="Del.Loc" Visible="false">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblloc" runat="server" Text='<%# Bind("PODI_LOCA") %>' Width="1px"></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField HeaderText="Remarks" Visible="false">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblremarks" runat="server" Text='<%# Bind("PODI_REMARKS") %>' Width="1px"></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField HeaderText="Unit Cost" Visible="false">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblcost" runat="server" Text='<%# Bind("UNIT_PRICE") %>' Width="1px"></asp:Label>
                                                                                </ItemTemplate>
                                                                                <ItemStyle Width="8%" HorizontalAlign="Center" />
                                                                                <HeaderStyle HorizontalAlign="Right" />
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

                                                                <div class="panelscollmedium">

                                                                    <asp:GridView ID="grdSerial" AutoGenerateColumns="false" runat="server" CssClass="table table-hover table-striped" GridLines="None" ShowHeaderWhenEmpty="True" EmptyDataText="No data found...">

                                                                        <Columns>

                                                                            <asp:TemplateField HeaderText="Seq.No" Visible="false">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblseqno" runat="server" Text='<%# Bind("TUS_USRSEQ_NO") %>' Width="50px"></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField HeaderText="">
                                                                                <ItemTemplate>
                                                                                    <asp:LinkButton ID="lbtnadd" runat="server" CausesValidation="false" OnClientClick="ConfirmDelete();" OnClick="lbtnadd_Click1">
                                                                                                    <span class="glyphicon glyphicon-trash" aria-hidden="true"></span>
                                                                                    </asp:LinkButton>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField HeaderText="Bin">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblbin" runat="server" Text='<%# Bind("TUS_BIN") %>' Width="75px"></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField HeaderText="Item Code">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblitri_itm_cd" runat="server" Text='<%# Bind("TUS_ITM_CD") %>' Width="175px"></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField HeaderText="Description">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lbldesc" runat="server" Text='<%# Bind("TUS_ITM_DESC") %>' Width="200px"></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField HeaderText="Model">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblmodelser" runat="server" Text='<%# Bind("TUS_ITM_MODEL") %>' Width="100px"></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField HeaderText="Brand">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblbrand" runat="server" Text='<%# Bind("TUS_ITM_BRAND") %>' Width="100px"></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField HeaderText="Item Status" Visible="false">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblstatus" runat="server" Text='<%# Bind("TUS_ITM_STUS") %>' Width="100px"></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField HeaderText="Item Status">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblstatustext" runat="server" Text='<%# Bind("Mis_desc") %>' Width="100px"></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField HeaderText="Serial 1">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblser1" runat="server" Text='<%# Bind("TUS_SER_1") %>' Width="75px"></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField HeaderText="Serial 2">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblser2" runat="server" Text='<%# Bind("TUS_SER_2") %>' Width="100px"></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField HeaderText="Serial 3">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblser3" runat="server" Text='<%# Bind("tus_ser_3") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField HeaderText="Warr.No">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblwarno" runat="server" Text='<%# Bind("TUS_WARR_NO") %>' Width="100px"></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField HeaderText="Serial ID" Visible="false">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblserialid" runat="server" Text='<%# Bind("TUS_SER_ID") %>'></asp:Label>
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
        <div runat="server" id="test" class="panel panel-primary Mheight">

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
        <div runat="server" id="Div1" class="panel panel-primary height230 width1330">

            <asp:Label ID="Label7" runat="server" Text="Label" Visible="false"></asp:Label>

            <div class="panel panel-default">
                <div class="panel-heading">
                    <asp:LinkButton ID="LinkButton13" runat="server" OnClick="LinkButton13_Click">
                             <span class="glyphicon glyphicon-remove" aria-hidden="true">Close</span>
                    </asp:LinkButton>
                    <div class="col-sm-11">
                    </div>
                    <div class="col-sm-1">
                    </div>
                </div>
                <div class="panel-body">


                    <asp:UpdatePanel runat="server">
                        <ContentTemplate>

                            <div class="row">

                                <div class="col-sm-12">
                                    <uc1:ucInScan runat="server" ID="ucInScan" />
                                </div>

                            </div>


                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>

        </div>
    </asp:Panel>

    <asp:UpdatePanel ID="UpdatePanel4" runat="server">
        <ContentTemplate>
            <asp:Button ID="Button1" runat="server" Text="Button" Style="display: none;" />
            <asp:ModalPopupExtender ID="UserDPopoup" runat="server" Enabled="True" TargetControlID="Button1"
                PopupControlID="pnlDpopup" PopupDragHandleControlID="PopupHeader" Drag="true" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>

        </ContentTemplate>

    </asp:UpdatePanel>

    <asp:UpdatePanel ID="UpdatePanel7" runat="server">
        <ContentTemplate>
            <asp:Panel runat="server" ID="pnlDpopup" DefaultButton="lbtnSearch">
                <div runat="server" id="Div2" class="panel panel-default height400 width950">

                    <asp:Label ID="Label1" runat="server" Text="Label" Visible="false"></asp:Label>
                    <div class="panel panel-default">
                        <div class="panel-heading">
                            <asp:LinkButton ID="btnDClose" runat="server" OnClick="btnDClose_Click">
                             <span class="glyphicon glyphicon-remove" aria-hidden="true">Close</span>
                            </asp:LinkButton>
                            <div class="col-sm-11">
                            </div>
                            <div class="col-sm-1">
                            </div>
                        </div>
                        <div class="panel-body">
                            <div class="col-sm-12" id="Div3" runat="server">
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
                                            <asp:CalendarExtender ID="CalendarExtender4" runat="server" TargetControlID="txtFDate"
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
                                        <asp:ScriptManagerProxy ID="ScriptManagerProxy5" runat="server"></asp:ScriptManagerProxy>
                                        <asp:UpdatePanel ID="UpdatePanel11" runat="server">
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
                                        <asp:ScriptManagerProxy ID="ScriptManagerProxy6" runat="server"></asp:ScriptManagerProxy>
                                        <asp:UpdatePanel ID="UpdatePanel12" runat="server">
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
                                        <asp:ScriptManagerProxy ID="ScriptManagerProxy2" runat="server"></asp:ScriptManagerProxy>
                                        <asp:UpdatePanel ID="UpdatePanel13" runat="server">
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

                                <asp:UpdateProgress ID="UpdateProgress2" DisplayAfter="10"
                                    runat="server" AssociatedUpdatePanelID="UpdatePanel7">

                                    <ProgressTemplate>
                                        <div class="divWaiting">
                                            <asp:Label ID="lblWait2" runat="server"
                                                Text="Please wait... " />
                                            <asp:Image ID="imgWait2" runat="server"
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




    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <asp:Button ID="btncus" runat="server" Text="Button" Style="display: none;" />
            <asp:ModalPopupExtender ID="MPPDA" runat="server" Enabled="True" TargetControlID="btncus"
                PopupControlID="CustomerPanel" PopupDragHandleControlID="PopupHeader" Drag="true" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>
        </ContentTemplate>
    </asp:UpdatePanel>

    <asp:UpdatePanel runat="server">
        <ContentTemplate>

            <asp:Panel runat="server" ID="CustomerPanel">
                <div runat="server" id="Div4" class="panel panel-default height150 width525">
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

                                <div class="row">

                                    <div class="col-sm-12">

                                        <div class="col-sm-12">
                                            <div class="row">

                                                <div class="col-sm-5 labelText1">
                                                    Document No
                                                </div>
                                                <div class="col-sm-7">
                                                    <asp:TextBox ID="txtdocname" runat="server" TabIndex="1" CssClass="form-control"></asp:TextBox>
                                                </div>

                                            </div>
                                        </div>

                                        <div class="row">
                                            <div class="col-sm-12 height10">
                                            </div>
                                        </div>

                                        <div class="col-sm-12">
                                            <div class="row">

                                                <div class="col-sm-5 labelText1">
                                                    Loading Bay
                                                </div>
                                                <div class="col-sm-7">
                                                    <asp:DropDownList ID="ddlloadingbay" runat="server" AutoPostBack="true" TabIndex="2" CssClass="form-control" OnSelectedIndexChanged="ddlloadingbay_SelectedIndexChanged"></asp:DropDownList>
                                                </div>

                                            </div>
                                        </div>

                                        <div class="row">
                                            <div class="col-sm-12 height10">
                                            </div>
                                        </div>

                                        <div class="col-sm-12">
                                            <div class="row">

                                                <div class="col-sm-5">
                                                </div>

                                                <div class="col-sm-7">
                                                    <asp:Button ID="btnsend" runat="server" TabIndex="3" CssClass="btn-info form-control" Text="Send" OnClientClick="ConfirmSendToPDA();" OnClick="btnsend_Click" />
                                                </div>

                                            </div>
                                        </div>

                                        <div class="row">
                                            <div class="col-sm-12 height10">
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

                </div>
            </asp:Panel>

        </ContentTemplate>
    </asp:UpdatePanel>

    <script src="<%=Request.ApplicationPath%>Js/jquery.min.js"></script>

    <script>
        Sys.Application.add_load(initSomething);
        function initSomething() {
            if (typeof jQuery == 'undefined') {
                alert('jQuery is not loaded');
            }
            $('.dvPendingPO').ready(function () {
                var gridHeader = $('#<%=dvPendingPO.ClientID%>').clone(true); // Here Clone Copy of Gridview with style
                $(gridHeader).find("tr:gt(0)").remove(); // Here remove all rows except first row (header row)
                var v = 0;
                $('#<%=dvPendingPO.ClientID%> tbody tr td').each(function (i) {
                    // Here Set Width of each th from gridview to new table(clone table) th 
                    if (v < $(this).width()) {
                        v = $(this).width();
                    }
                    console.log((v).toString());
                    $("th:nth-child(" + (i + 1) + ")", gridHeader).css('width', ($(this).width()).toString() + "px");
                });
                $('.GHead').append(gridHeader);
                $('.GHead').css('position', 'inherit');
                $('.GHead').css('width', '99%');
                $('.GHead').css('top', $('#<%=dvPendingPO.ClientID%>').offset().top);
                jQuery('#BodyContent_dvPendingPO tbody').children('tr').eq(1).remove();

            });
        }
    </script>
    <script src="<%=Request.ApplicationPath%>Js/jquery.min.js"></script>
    <script src="<%=Request.ApplicationPath%>Js/bootstrap.min.js"></script>
</asp:Content>
