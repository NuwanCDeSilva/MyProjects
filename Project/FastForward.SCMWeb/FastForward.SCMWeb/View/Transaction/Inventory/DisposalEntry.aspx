<%@ Page Title="" Language="C#" MasterPageFile="~/View/AdminSite.Master" AutoEventWireup="true" CodeBehind="DisposalEntry.aspx.cs" Inherits="FastForward.SCMWeb.View.Transaction.Inventory.DisposalEntry" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/UserControls/ucOutScan.ascx" TagPrefix="uc1" TagName="ucOutScan" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">

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
            position: fixed;
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

        function confSave() {
            var resasd = confirm("Do you want to save?");
            if (resasd) {
                document.getElementById('<%=hdfSaveCon.ClientID %>').value = "Yes";
            } else {
                document.getElementById('<%=hdfSaveCon.ClientID %>').value = "No";
            }
        };

        function confClear() {
            var res = confirm("Do you want to clear?");
            if (res) {
                return true;
            }
            else {
                return false;
            }
        };

        function confDeleteLoc() {
            var res = confirm("Do you want to delete this location?");
            if (res) {
                return true;
            }
            else {
                return false;
            }
        };

        function DelItemConfirm() {
            var res = confirm("Do you want to delete this item?");
            if (res) {
                return true;
            }
            else {
                return false;
            }
        };

        function DelSerialConfirm() {
            var res = confirm("Do you want to delete this serial?");
            if (res) {
                return true;
            }
            else {
                return false;
            }
        };

        function ConfirmItemDelete() {
            var selectedvalueOrdPlace = confirm("Do you want to delete ?");
            if (selectedvalueOrdPlace) {
                return true;
            } else {
                return false;
            }
        };

        function ConfirmSerialDelete() {
            var selectedvalueOrdPlace = confirm("Do you want to delete ?");
            if (selectedvalueOrdPlace) {
                return true;
            } else {
                return false;
            }
        };

        function ConfirmSendToPDA() {
            var result = confirm("Do you want to send to PDA ?");
            if (result) {
                return true;
            } else {
                return false;
            }
        };

    </script>

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

        function CloseAll() {
            if (jQuery('.toast-item-wrapper') != null) {
                jQuery('.toast-item-wrapper').remove();
            }
        }
    </script>

    <script>
        function CollapseShow() {
            $(document).ready(function () {
                $('#collapseOne').collapse('show');
            });
        };

        function CollapseHide() {
            $(document).ready(function () {
                $('#collapseOne').collapse('hide');
            });
        };

        window.onload = function () {
            CollapseHide();
        };

    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" runat="server">
    <div class="panel panel-default marginLeftRight5 marginBottom0">
        <div class="panel-body paddingtopbottom0">
            <asp:ScriptManagerProxy ID="ScriptManagerProxy1" runat="server"></asp:ScriptManagerProxy>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <asp:HiddenField ID="hdfSaveCon" runat="server" />
                    <div class="row">
                        <div class="col-sm-12">
                            <div class="col-sm-8  buttonrow">
                                <asp:Label ID="lblH1" runat="server" Text="Label"></asp:Label>
                                <asp:Label ID="lblBackDateInfor" runat="server"></asp:Label>
                            </div>
                            <div class="col-sm-4  buttonRow">
                                <div class="col-sm-3 paddingRight0">
                                </div>
                                <div class="col-sm-3 paddingRight0">
                                    <asp:LinkButton ID="btnSave" CausesValidation="true" runat="server" CssClass="floatRight" OnClientClick="return confSave()" OnClick="btnSave_Click">
                                        <span class="glyphicon glyphicon-saved" aria-hidden="true"></span>Save
                                    </asp:LinkButton>
                                </div>
                                <div class="col-sm-3 paddingRight0">
                                    <asp:LinkButton ID="btnUpdate" CausesValidation="true" runat="server" CssClass="floatRight" OnClientClick="return confirm('Do you want to update')" OnClick="btnUpdate_Click">
                                        <span class="glyphicon glyphicon-saved" aria-hidden="true"></span>Update Job
                                    </asp:LinkButton>
                                </div>
                                <div class="col-sm-3">
                                    <asp:LinkButton ID="btnClear" runat="server" CssClass="floatRight" OnClientClick="return confClear()" OnClick="btnClear_Click">
                                        <span class="glyphicon glyphicon-refresh" aria-hidden="true"></span>Clear
                                    </asp:LinkButton>
                                </div>
                            </div>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
            <asp:ScriptManagerProxy ID="ScriptManagerProxy8" runat="server"></asp:ScriptManagerProxy>
            <div class="row">
                <div class="col-sm-12">
                    <div class="panel panel-default">
                        <div class="panel-heading height16 paddingtop0">
                            <b>Disposal Entry</b>
                        </div>
                        <div class="panel-body paddingtopbottom0">
                            <div class="col-sm-12 padding0 paddingtopbottom0">
                                <asp:UpdatePanel runat="server">
                                    <ContentTemplate>
                                        <div class="panel panel-default marginBottom0">
                                            <div class="panel-heading height20 paddingtopbottom0">
                                                <asp:Panel ID="pnlBtn" runat="server">
                                                    <asp:LinkButton ID="btnColapse" Text="Pending Jobs" runat="server">Pending Jobs
                                                        <span class="glyphicon glyphicon-arrow-down" aria-hidden="true"></span>
                                                    </asp:LinkButton>
                                                    <asp:CheckBox ID="chkSendToPDA" Text="Sent to PDA" runat="server" Style="float: right;" />
                                                </asp:Panel>
                                            </div>
                                            <div class="panel-body">
                                                <asp:Panel ID="pnlCollaps" runat="server">

                                                    <asp:UpdateProgress ID="upp1" DisplayAfter="10"
                                                        runat="server" AssociatedUpdatePanelID="upPendingJobs">
                                                        <ProgressTemplate>
                                                            <div class="divWaiting">
                                                                <asp:Label ID="lblWait3" runat="server" Text="Please wait... " />
                                                                <asp:Image ID="imgWait3" runat="server" ImageAlign="Middle" ImageUrl="~/images/icons/ajax-loader.gif" />
                                                            </div>
                                                        </ProgressTemplate>
                                                    </asp:UpdateProgress>


                                                    <asp:UpdatePanel ID="upPendingJobs" runat="server">
                                                        <ContentTemplate>
                                                            <div class="col-sm-12 padding0">
                                                                <div class="col-sm-3 padding0">
                                                                    <div class="col-sm-3 labelText1 paddingLeft0">
                                                                        From
                                                                    </div>
                                                                    <div class="col-sm-7 paddingRight5 paddingLeft0">
                                                                        <asp:TextBox ID="txtFrom" runat="server" CssClass="form-control" />
                                                                        <asp:CalendarExtender ID="txtFromCal" runat="server" TargetControlID="txtFrom"
                                                                            PopupButtonID="btnFrom" Format="dd/MMM/yyyy">
                                                                        </asp:CalendarExtender>
                                                                    </div>
                                                                    <div class="col-sm-1 paddingLeft0">
                                                                        <asp:LinkButton ID="btnFrom" TabIndex="1" CausesValidation="false" runat="server">
                                                     <span class="glyphicon glyphicon-calendar" aria-hidden="true"></span>
                                                                        </asp:LinkButton>
                                                                    </div>
                                                                </div>
                                                                <div class="col-sm-3 padding0">
                                                                    <div class="col-sm-3 labelText1 paddingLeft0">
                                                                        To
                                                                    </div>
                                                                    <div class="col-sm-7 paddingRight5 paddingLeft0">
                                                                        <asp:TextBox ID="txtTo" runat="server" CssClass="form-control" />
                                                                        <asp:CalendarExtender ID="txtToCAl" runat="server" TargetControlID="txtTo"
                                                                            PopupButtonID="btnTo" Format="dd/MMM/yyyy">
                                                                        </asp:CalendarExtender>
                                                                    </div>
                                                                    <div class="col-sm-1 paddingLeft0">
                                                                        <asp:LinkButton ID="btnTo" TabIndex="1" CausesValidation="false" runat="server">
                                                     <span class="glyphicon glyphicon-calendar" aria-hidden="true"></span>
                                                                        </asp:LinkButton>
                                                                    </div>
                                                                </div>
                                                                <div class="col-sm-3 padding0">
                                                                    <div class="col-sm-3 labelText1 paddingLeft0">
                                                                        <asp:Label ID="lblJobNumber" Text="Job Number" runat="server" />
                                                                    </div>
                                                                    <div class="col-sm-7 paddingRight5 paddingLeft0">
                                                                        <asp:TextBox ID="txtJobNumberPending" runat="server" CssClass="form-control" />
                                                                    </div>
                                                                    <div class="col-sm-1 paddingLeft0">
                                                                        <asp:LinkButton ID="btnJobNumberPending" runat="server" CausesValidation="false" Visible="false">
                                                        <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                                        </asp:LinkButton>
                                                                    </div>
                                                                </div>
                                                                <div class="col-sm-3 padding0">
                                                                    <asp:Button ID="btnSearchSearchPending" Text="Search" runat="server" OnClick="btnSearchSearchPending_Click" />
                                                                </div>
                                                            </div>
                                                            <div class="col-sm-12 padding0">
                                                                <div class="col-sm-12 padding0 panelscoll1" style="height: 119px;">
                                                                    <asp:GridView ID="dgvPendingJobs" AutoGenerateColumns="False" TabIndex="39" runat="server" CssClass="table table-hover table-striped" GridLines="None" ShowHeaderWhenEmpty="True" EmptyDataText="No data found...">
                                                                        <Columns>
                                                                            <asp:BoundField DataField="DH_SEQ" HeaderText="SEQ" Visible="false" />
                                                                            <asp:TemplateField HeaderText="Document">
                                                                                <ItemTemplate>
                                                                                    <asp:LinkButton ID="btnPendingJObSelect" Text='<%# Bind("DH_DOC_NO") %>' runat="server" OnClick="btnPendingJObSelect_Click" />
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Date">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblDH_DOC_DT" runat="server" Text='<%# Bind("DH_DOC_DT","{0:dd/MMM/yyyy}") %>' />
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:BoundField DataField="DH_REF_NO" HeaderText="Reference" />
                                                                            <asp:TemplateField HeaderText="Max Value">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblDH_MAX_VAL" runat="server" Text='<%# Bind("DH_MAX_VAL","{0:N2}") %>' />
                                                                                </ItemTemplate>
                                                                                <ItemStyle HorizontalAlign="Right" />
                                                                                <HeaderStyle CssClass="gridHeaderAlignRight" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Charge">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblDH_CHG" runat="server" Text='<%# Bind("DH_CHG","{0:N2}") %>' />
                                                                                </ItemTemplate>
                                                                                <ItemStyle HorizontalAlign="Right" />
                                                                                <HeaderStyle CssClass="gridHeaderAlignRight" />
                                                                            </asp:TemplateField>
                                                                            <asp:BoundField HeaderText=" " />
                                                                            <asp:BoundField DataField="DH_RECIPT_NO" HeaderText="Receipt No" />
                                                                            <asp:BoundField DataField="DH_RMK" HeaderText="Remark" />
                                                                        </Columns>
                                                                    </asp:GridView>
                                                                </div>
                                                                <div class="col-sm-12 padding0">
                                                                    <div class="col-sm-10 padding0">
                                                                    </div>
                                                                    <div class="col-sm-2 padding0">
                                                                    </div>
                                                                </div>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </asp:Panel>
                                            </div>
                                        </div>
                                        <asp:CollapsiblePanelExtender runat="server" ID="cpe" TargetControlID="pnlCollaps" CollapseControlID="btnColapse" ExpandControlID="btnColapse" Collapsed="true" CollapsedSize="0" ExpandedSize="138" ExpandedText="(Collapse...)" CollapsedText="(Expand...)">
                                        </asp:CollapsiblePanelExtender>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>

                            <div class="col-sm-12 padding0">
                                <div class="panel panel-default marginBottom0">
                                    <div class="panel-heading height16 paddingtop0">
                                        Job Details
                                    </div>
                                    <div class="panel-body paddingbottom0">
                                        <asp:UpdatePanel runat="server">
                                            <ContentTemplate>
                                                <asp:Panel ID="pnlJobSave" runat="server">
                                                    <div class="col-sm-12 padding0">
                                                        <div class="col-sm-6 padding0">
                                                            <div class="col-sm-12 padding0">
                                                                <div class="col-sm-6 padding0">
                                                                    <div class="col-sm-3 labelText1 paddingLeft0">
                                                                        Date
                                                                    </div>
                                                                    <div class="col-sm-7 paddingRight5 paddingLeft0">
                                                                        <asp:TextBox ID="txtDate" runat="server" CssClass="form-control" />
                                                                        <asp:CalendarExtender ID="txtateCal" runat="server" TargetControlID="txtDate"
                                                                            PopupButtonID="btnDate" Format="dd/MMM/yyyy">
                                                                        </asp:CalendarExtender>
                                                                    </div>
                                                                    <div class="col-sm-1 paddingLeft0">
                                                                        <asp:LinkButton ID="btnDate" TabIndex="1" CausesValidation="false" runat="server">
                                                                        <span class="glyphicon glyphicon-calendar" aria-hidden="true"></span>
                                                                        </asp:LinkButton>
                                                                    </div>
                                                                </div>
                                                                <div class="col-sm-6 padding0">
                                                                    <div class="col-sm-3 labelText1 paddingLeft0">
                                                                        Job Number
                                                                    </div>
                                                                    <div class="col-sm-7 paddingRight5 paddingLeft0">
                                                                        <asp:TextBox ID="txtJobNumber" runat="server" CssClass="form-control" AutoPostBack="true" OnTextChanged="txtJobNumber_TextChanged" />
                                                                    </div>
                                                                    <div class="col-sm-1 paddingLeft0">
                                                                        <asp:LinkButton ID="btnJobnumber" runat="server" CausesValidation="false" OnClick="btnJobnumber_Click1">
                                                                        <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                                        </asp:LinkButton>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="col-sm-12 padding0">
                                                                <div class="col-sm-6 padding0">
                                                                    <div class="col-sm-3 labelText1 paddingLeft0">
                                                                        Valid From
                                                                    </div>
                                                                    <div class="col-sm-7 paddingRight5 paddingLeft0">
                                                                        <asp:TextBox ID="txtValidFrom" runat="server" CssClass="form-control" />
                                                                        <asp:CalendarExtender ID="txtValidFromCal" runat="server" TargetControlID="txtValidFrom"
                                                                            PopupButtonID="btnValidFrom" Format="dd/MMM/yyyy">
                                                                        </asp:CalendarExtender>
                                                                    </div>
                                                                    <div class="col-sm-1 paddingLeft0">
                                                                        <asp:LinkButton ID="btnValidFrom" TabIndex="1" CausesValidation="false" runat="server">
                                                     <span class="glyphicon glyphicon-calendar" aria-hidden="true"></span>
                                                                        </asp:LinkButton>
                                                                    </div>
                                                                </div>
                                                                <div class="col-sm-6 padding0">
                                                                    <div class="col-sm-3 labelText1 paddingLeft0">
                                                                        Valid To
                                                                    </div>
                                                                    <div class="col-sm-7 paddingRight5 paddingLeft0">
                                                                        <asp:TextBox ID="txtValidTo" runat="server" CssClass="form-control" />
                                                                        <asp:CalendarExtender ID="txtValidToCal" runat="server" TargetControlID="txtValidTo"
                                                                            PopupButtonID="btnValidTo" Format="dd/MMM/yyyy">
                                                                        </asp:CalendarExtender>
                                                                    </div>
                                                                    <div class="col-sm-1 paddingLeft0">
                                                                        <asp:LinkButton ID="btnValidTo" TabIndex="1" CausesValidation="false" runat="server">
                                                     <span class="glyphicon glyphicon-calendar" aria-hidden="true"></span>
                                                                        </asp:LinkButton>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="col-sm-12 padding0">
                                                                <div class="col-sm-6 padding0">
                                                                    <div class="col-sm-3 labelText1 paddingLeft0">
                                                                        Max Value
                                                                    </div>
                                                                    <div class="col-sm-7 paddingRight5 paddingLeft0">
                                                                        <asp:TextBox ID="txtMaxValue" MaxLength="12" Style="text-align: right" runat="server" onkeydown="return jsDecimals(event);" CssClass="form-control" />
                                                                    </div>
                                                                    <div class="col-sm-1 paddingLeft0">
                                                                    </div>
                                                                </div>
                                                                <div class="col-sm-6 padding0">
                                                                    <div class="col-sm-5 padding0">
                                                                        <div class="col-sm-2 padding0">
                                                                            <asp:CheckBox ID="chkRestrict" Text="" runat="server" />
                                                                        </div>
                                                                        <div class="col-sm-9 padding0">
                                                                            Restrict
                                                                        </div>
                                                                    </div>
                                                                    <div class="col-sm-6 padding0">
                                                                        <div class="col-sm-3 labelText1 paddingLeft0">
                                                                            Status
                                                                        </div>
                                                                        <div class="col-sm-7 paddingRight5 paddingLeft0">
                                                                            <asp:TextBox ID="txtStatus" ReadOnly="true" runat="server" CssClass="form-control" Style="color: red;" />
                                                                        </div>
                                                                        <div class="col-sm-1 paddingLeft0">
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="col-sm-12 padding0">
                                                                <div class="col-sm-6 padding0">
                                                                    <div class="col-sm-3 labelText1 paddingLeft0">
                                                                        Charge
                                                                    </div>
                                                                    <div class="col-sm-7 paddingRight5 paddingLeft0">
                                                                        <asp:TextBox ID="txtCharge" MaxLength="12" Style="text-align: right" runat="server" onkeydown="return jsDecimals(event);" CssClass="form-control" />
                                                                    </div>
                                                                    <div class="col-sm-1 paddingLeft0">
                                                                    </div>
                                                                </div>
                                                                <div class="col-sm-6 padding0">
                                                                    <div class="col-sm-3 labelText1 paddingLeft0">
                                                                        Ref.
                                                                    </div>
                                                                    <div class="col-sm-7 paddingRight5 paddingLeft0">
                                                                        <asp:TextBox ID="txtReference" runat="server" CssClass="form-control" />
                                                                    </div>
                                                                    <div class="col-sm-1 paddingLeft0">
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="col-sm-12 padding0">
                                                                <div class="col-sm-6 padding0">
                                                                    <div class="col-sm-12 padding0">
                                                                        <div class="col-sm-3 labelText1 paddingLeft0">
                                                                            Payment Type
                                                                        </div>
                                                                        <div class="col-sm-7 paddingRight5 paddingLeft0">
                                                                            <asp:DropDownList ID="ddlPaymentType" CssClass="form-control" runat="server"></asp:DropDownList>
                                                                        </div>
                                                                        <div class="col-sm-1 paddingLeft0">
                                                                        </div>
                                                                    </div>
                                                                    <div class="col-sm-12 padding0">
                                                                        <div class="col-sm-3 labelText1 paddingLeft0">
                                                                            Receipt
                                                                        </div>
                                                                        <div class="col-sm-7 paddingRight5 paddingLeft0">
                                                                            <asp:TextBox ID="txtReceipt" ReadOnly="true" runat="server" CssClass="form-control" />
                                                                        </div>
                                                                        <div class="col-sm-1 paddingLeft0">
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class="col-sm-6 padding0">
                                                                    <div class="col-sm-12 padding0">
                                                                        <div class="col-sm-3 labelText1 paddingLeft0">
                                                                            Remark
                                                                        </div>
                                                                        <div class="col-sm-7 paddingRight5 paddingLeft0">
                                                                            <asp:TextBox ID="txtRemark" runat="server" MaxLength="150" TextMode="MultiLine" CssClass="form-control" Height="42px" />
                                                                        </div>
                                                                        <div class="col-sm-1 paddingLeft0">
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="col-sm-6 padding0">
                                                            <div class="panel panel-default">
                                                                <div class="panel-heading height30">
                                                                    Allow Location to Dispatch
                                                                </div>
                                                                <div class="panel-body">
                                                                    <div class="col-sm-12 padding0">
                                                                        <div class="col-sm-2 padding0">
                                                                            Location
                                                                        </div>
                                                                        <div class="col-sm-2 padding0">
                                                                            <asp:TextBox ID="txtLocation" CssClass="form-control" runat="server" AutoPostBack="true" OnTextChanged="txtLocation_TextChanged" Style="text-transform: uppercase" />
                                                                        </div>
                                                                        <div class="col-sm-6 padding0">
                                                                            <asp:TextBox ID="txtLocDesc" CssClass="form-control" runat="server" ReadOnly="true" />
                                                                        </div>
                                                                        <div class="col-sm-2 padding0">
                                                                            <div class="col-sm-6 padding0">
                                                                                <asp:LinkButton ID="btnLocation" runat="server" CausesValidation="false" OnClick="btnLocation_Click">
                                                                        <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                                                </asp:LinkButton>
                                                                            </div>
                                                                            <div class="col-sm-6 padding0">
                                                                                <asp:LinkButton ID="btnAddLocationnew" runat="server" CausesValidation="false" OnClick="btnAddLocationnew_Click">
                                                                        <span class="glyphicon glyphicon-plus" aria-hidden="true"></span>
                                                                                </asp:LinkButton>
                                                                                <asp:Button ID="btnAddLocation" Visible="false" Text="Add" runat="server" OnClick="btnAddLocation_Click" />
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                    <div class="col-sm-12 padding0">
                                                                        <asp:GridView ID="dgvLocations" AutoGenerateColumns="False" TabIndex="39" runat="server" CssClass="table table-hover table-striped" GridLines="None" ShowHeaderWhenEmpty="True" EmptyDataText="No data found...">
                                                                            <Columns>
                                                                                <asp:BoundField DataField="DL_SEQ" HeaderText="Seq" Visible="false" />
                                                                                <asp:BoundField DataField="DL_DOC_NO" HeaderText="Doc" Visible="false" />
                                                                                <asp:TemplateField HeaderText="Location">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblDL_LOC" runat="server" Text='<%# Bind("DL_LOC") %>'></asp:Label>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:BoundField DataField="Dl_loc_Desc" HeaderText="Decription" />
                                                                                <asp:BoundField DataField="DL_ACT" HeaderText="Status" Visible="false" />
                                                                                <asp:TemplateField>
                                                                                    <ItemTemplate>
                                                                                        <asp:LinkButton ID="btnDeleteLocation" CausesValidation="false" OnClientClick="return confDeleteLoc()" OnClick="btnDeleteLocation_Click" runat="server">
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
                                                </asp:Panel>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>
                                </div>
                            </div>

                            <div class="col-sm-12 padding0">
                                <div class="panel panel-default padding0">
                                    <div class="panel-heading padding0">
                                        Item Details
                                    </div>
                                    <div class="panel-body padding0">
                                        <div class="col-sm-12 padding0">
                                            <asp:UpdatePanel runat="server">
                                                <ContentTemplate>
                                                    <asp:Panel ID="pnlUserControl" runat="server">
                                                        <uc1:ucOutScan runat="server" ID="ucOutScan" />
                                                    </asp:Panel>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-sm-12 padding0">
                            <div class="panel panel-default paddingbottom0">
                                <div class="panel-body">
                                    <div class="row">
                                        <div class="panel-body paddingbottom0 paddingtop0">
                                            <div class="col-sm-12">
                                                <div class="row">
                                                    <div class="col-sm-12">
                                                        <ul id="myTab2" class="nav nav-tabs">
                                                            <li class="active"><a href="#Item" data-toggle="tab">Item</a></li>
                                                            <li><a href="#Serial" data-toggle="tab">Serial</a></li>
                                                            <asp:CheckBox Text="get Similar Items" ID="chkChangeSimilarItem" AutoPostBack="true" runat="server" Style="float: right" Visible="false" />
                                                            <asp:CheckBox Text="Change Status" ID="chkChangeStatus" AutoPostBack="true" runat="server" Style="float: right" Visible="false" />
                                                        </ul>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-sm-12">
                                                        <div id="myTabContent" class="tab-content">
                                                            <div class="tab-pane fade in active" id="Item">
                                                                <asp:ScriptManagerProxy ID="ScriptManagerProxy3" runat="server"></asp:ScriptManagerProxy>
                                                                <asp:UpdatePanel ID="upGvItems" runat="server">
                                                                    <ContentTemplate>
                                                                        <div class="row ">
                                                                            <div class="col-sm-12 ">
                                                                                <div class="row">
                                                                                    <div class="col-sm-12 ">
                                                                                        <asp:GridView ID="grdItems" CssClass="table table-hover table-striped" runat="server" GridLines="None" ShowHeaderWhenEmpty="True"
                                                                                            EmptyDataText="No data found..." AutoGenerateColumns="False">
                                                                                            <Columns>
                                                                                                <asp:TemplateField Visible="false">
                                                                                                    <ItemTemplate>
                                                                                                        <asp:LinkButton ID="lbtnGrdSerial" runat="server" CausesValidation="false" OnClick="lbtnGrdSerial_Click">
                                                                                                            <span aria-hidden="true" class="glyphicon glyphicon-plus"></span>
                                                                                                        </asp:LinkButton>
                                                                                                    </ItemTemplate>
                                                                                                    <ItemStyle Width="5%" HorizontalAlign="Center" />
                                                                                                </asp:TemplateField>
                                                                                                <asp:TemplateField>
                                                                                                    <ItemTemplate>
                                                                                                        <asp:LinkButton ID="lbtngrdItemsDalete" runat="server" CausesValidation="false" OnClientClick="return ConfirmItemDelete()" OnClick="lbtngrdItemsDalete_Click">
                                                                                                            <span aria-hidden="true" class="glyphicon glyphicon-trash"></span>
                                                                                                        </asp:LinkButton>
                                                                                                    </ItemTemplate>
                                                                                                </asp:TemplateField>
                                                                                                <asp:TemplateField HeaderText="LineNo" Visible="false">
                                                                                                    <ItemTemplate>
                                                                                                        <asp:Label ID="lblitri_line_no" runat="server" Text='<%# Bind("itri_line_no") %>'></asp:Label>
                                                                                                    </ItemTemplate>
                                                                                                </asp:TemplateField>
                                                                                                <asp:TemplateField HeaderText="Item">
                                                                                                    <ItemTemplate>
                                                                                                        <asp:Label ID="lblitri_itm_cd" runat="server" Text='<%# Bind("itri_itm_cd") %>'></asp:Label>
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
                                                                                                <asp:TemplateField HeaderText="Status" Visible="false">
                                                                                                    <ItemTemplate>
                                                                                                        <asp:Label ID="lblitri_itm_stus" runat="server" Text='<%# Bind("itri_itm_stus") %>'></asp:Label>
                                                                                                    </ItemTemplate>
                                                                                                </asp:TemplateField>
                                                                                                <asp:TemplateField HeaderText="Status">
                                                                                                    <ItemTemplate>
                                                                                                        <asp:Label ID="lblitri_itm_stus_desc" runat="server" Text='<%# Bind("itri_itm_stus_desc") %>'></asp:Label>
                                                                                                    </ItemTemplate>
                                                                                                </asp:TemplateField>
                                                                                                <asp:TemplateField HeaderText="Unit Cost" Visible="false">
                                                                                                    <ItemTemplate>
                                                                                                        <asp:Label ID="lblitri_unit_price" runat="server" Text='<%# Bind("itri_unit_price") %>' Visible="false"></asp:Label>
                                                                                                    </ItemTemplate>
                                                                                                    <ItemStyle HorizontalAlign="Right" />
                                                                                                    <HeaderStyle CssClass="gridHeaderAlignRight" />
                                                                                                </asp:TemplateField>
                                                                                                <asp:TemplateField HeaderText="App. Qty">
                                                                                                    <ItemTemplate>
                                                                                                        <asp:Label ID="lblitri_app_qty" runat="server" Text='<%# Bind("itri_app_qty","{0:N2}") %>'></asp:Label>
                                                                                                    </ItemTemplate>
                                                                                                    <ItemStyle HorizontalAlign="Right" />
                                                                                                    <HeaderStyle CssClass="gridHeaderAlignRight" />
                                                                                                </asp:TemplateField>
                                                                                                <asp:TemplateField HeaderText="Pick Qty">
                                                                                                    <ItemTemplate>
                                                                                                        <asp:Label ID="lblitri_qty" runat="server" Text='<%# Bind("itri_qty","{0:N2}") %>'></asp:Label>
                                                                                                    </ItemTemplate>
                                                                                                    <ItemStyle HorizontalAlign="Right" />
                                                                                                    <HeaderStyle CssClass="gridHeaderAlignRight" />
                                                                                                </asp:TemplateField>
                                                                                                <asp:TemplateField HeaderText="Request" Visible="false">
                                                                                                    <ItemTemplate>
                                                                                                        <asp:Label ID="lblitri_note" runat="server" Text='<%# Bind("itri_note") %>'></asp:Label>
                                                                                                    </ItemTemplate>
                                                                                                </asp:TemplateField>
                                                                                                <asp:TemplateField HeaderText="supplier" Visible="false">
                                                                                                    <ItemTemplate>
                                                                                                        <asp:Label ID="lblItri_supplier" runat="server" Text='<%# Bind("Itri_supplier") %>'></asp:Label>
                                                                                                    </ItemTemplate>
                                                                                                </asp:TemplateField>
                                                                                                <asp:TemplateField HeaderText="batchno" Visible="false">
                                                                                                    <ItemTemplate>
                                                                                                        <asp:Label ID="Itri_batchno" runat="server" Text='<%# Bind("Itri_batchno") %>'></asp:Label>
                                                                                                    </ItemTemplate>
                                                                                                </asp:TemplateField>
                                                                                                <asp:TemplateField HeaderText="grndate" Visible="false">
                                                                                                    <ItemTemplate>
                                                                                                        <asp:Label ID="lblItri_grndate" runat="server" Text='<%# Bind("Itri_grndate") %>'></asp:Label>
                                                                                                    </ItemTemplate>
                                                                                                </asp:TemplateField>
                                                                                                <asp:TemplateField HeaderText="expdate" Visible="false">
                                                                                                    <ItemTemplate>
                                                                                                        <asp:Label ID="lblItri_expdate" runat="server" Text='<%# Bind("Itri_expdate") %>'></asp:Label>
                                                                                                    </ItemTemplate>
                                                                                                </asp:TemplateField>
                                                                                            </Columns>
                                                                                        </asp:GridView>
                                                                                    </div>
                                                                                    <asp:Button ID="btnSetGrid" Text="idSetGrid" Visible="false" OnClick="btnSetGrid_Click" runat="server" />
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                    </ContentTemplate>
                                                                </asp:UpdatePanel>
                                                            </div>
                                                            <div class="tab-pane fade" id="Serial">
                                                                <asp:ScriptManagerProxy ID="ScriptManagerProxy5" runat="server"></asp:ScriptManagerProxy>
                                                                <asp:UpdatePanel ID="UpdatePanel7" runat="server">
                                                                    <ContentTemplate>
                                                                        <div class="row">
                                                                            <div class="col-sm-12 ">
                                                                                <div class="row">
                                                                                    <div class="col-sm-12 ">
                                                                                        <asp:GridView ID="grdSerial" CssClass="table table-hover table-striped" runat="server" GridLines="None" ShowHeaderWhenEmpty="True"
                                                                                            EmptyDataText="No data found..." AutoGenerateColumns="False">
                                                                                            <Columns>
                                                                                                <asp:TemplateField>
                                                                                                    <ItemTemplate>
                                                                                                        <asp:LinkButton ID="lbtngrdSerialDalete" runat="server" CausesValidation="false" OnClientClick="return ConfirmSerialDelete()" OnClick="lbtngrdSerialDalete_Click">
                                                                                                                <span aria-hidden="true" class="glyphicon glyphicon-trash"></span>
                                                                                                        </asp:LinkButton>
                                                                                                    </ItemTemplate>
                                                                                                </asp:TemplateField>
                                                                                                <asp:TemplateField HeaderText="Item">
                                                                                                    <ItemTemplate>
                                                                                                        <asp:Label ID="ser_Item" runat="server" Text='<%# Bind("tus_itm_cd") %>'></asp:Label>
                                                                                                    </ItemTemplate>
                                                                                                </asp:TemplateField>
                                                                                                <asp:TemplateField HeaderText="Model">
                                                                                                    <ItemTemplate>
                                                                                                        <asp:Label ID="ser_Model" runat="server" Text='<%# Bind("tus_itm_model") %>'></asp:Label>
                                                                                                    </ItemTemplate>
                                                                                                </asp:TemplateField>
                                                                                                <asp:TemplateField HeaderText="Status" Visible="false">
                                                                                                    <ItemTemplate>
                                                                                                        <asp:Label ID="ser_Status" runat="server" Text='<%# Bind("tus_itm_stus") %>'></asp:Label>
                                                                                                    </ItemTemplate>
                                                                                                </asp:TemplateField>
                                                                                                <asp:TemplateField HeaderText="Status">
                                                                                                    <ItemTemplate>
                                                                                                        <asp:Label ID="ser_Status_desc" runat="server" Text='<%# Bind("tus_itm_stus_desc") %>'></asp:Label>
                                                                                                    </ItemTemplate>
                                                                                                </asp:TemplateField>
                                                                                                <asp:TemplateField HeaderText="Qty">
                                                                                                    <ItemTemplate>
                                                                                                        <asp:Label ID="ser_Qty" runat="server" Text='<%# Bind("tus_qty","{0:N2}") %>'></asp:Label>
                                                                                                    </ItemTemplate>
                                                                                                    <ItemStyle HorizontalAlign="Right" />
                                                                                                    <HeaderStyle CssClass="gridHeaderAlignRight" />
                                                                                                </asp:TemplateField>
                                                                                                <asp:TemplateField HeaderText="Unit Cost" Visible="false">
                                                                                                    <ItemTemplate>
                                                                                                        <asp:Label ID="ser_UnitCost" runat="server" Text='<%# Bind("tus_unit_cost","{0:N2}") %>'></asp:Label>
                                                                                                    </ItemTemplate>
                                                                                                    <ItemStyle HorizontalAlign="Right" />
                                                                                                    <HeaderStyle CssClass="gridHeaderAlignRight" />
                                                                                                </asp:TemplateField>
                                                                                                <asp:BoundField HeaderText=" " />
                                                                                                <asp:TemplateField HeaderText="Serial 1">
                                                                                                    <ItemTemplate>
                                                                                                        <asp:Label ID="ser_Serial1" runat="server" Text='<%# Bind("tus_ser_1") %>'></asp:Label>
                                                                                                    </ItemTemplate>
                                                                                                </asp:TemplateField>
                                                                                                <asp:TemplateField HeaderText="Serial 2">
                                                                                                    <ItemTemplate>
                                                                                                        <asp:Label ID="ser_Serial2" runat="server" Text='<%# Bind("tus_ser_2") %>'></asp:Label>
                                                                                                    </ItemTemplate>
                                                                                                </asp:TemplateField>
                                                                                                <asp:TemplateField HeaderText="Serial 3">
                                                                                                    <ItemTemplate>
                                                                                                        <asp:Label ID="ser_Serial3" runat="server" Text='<%# Bind("tus_ser_3") %>'></asp:Label>
                                                                                                    </ItemTemplate>
                                                                                                </asp:TemplateField>
                                                                                                <asp:TemplateField HeaderText="Bin" Visible="false">
                                                                                                    <ItemTemplate>
                                                                                                        <asp:Label ID="ser_Bin" runat="server" Text='<%# Bind("tus_bin") %>'></asp:Label>
                                                                                                    </ItemTemplate>
                                                                                                </asp:TemplateField>
                                                                                                <asp:TemplateField HeaderText="Serial ID" Visible="false">
                                                                                                    <ItemTemplate>
                                                                                                        <asp:Label ID="ser_SerialID" runat="server" Text='<%# Bind("tus_ser_id") %>'></asp:Label>
                                                                                                    </ItemTemplate>
                                                                                                </asp:TemplateField>
                                                                                                <asp:TemplateField HeaderText="Request" Visible="false">
                                                                                                    <ItemTemplate>
                                                                                                        <asp:Label ID="ser_requestno" runat="server" Text='<%# Bind("tus_base_doc_no") %>'></asp:Label>
                                                                                                    </ItemTemplate>
                                                                                                </asp:TemplateField>
                                                                                                <asp:TemplateField HeaderText="BaseLineNo" Visible="false">
                                                                                                    <ItemTemplate>
                                                                                                        <asp:Label ID="ser_BaseLineNo" runat="server" Text='<%# Bind("tus_base_itm_line") %>'></asp:Label>
                                                                                                    </ItemTemplate>
                                                                                                </asp:TemplateField>
                                                                                                    <asp:TemplateField HeaderText="BaseLineNo" Visible="false">
                                                                                                    <ItemTemplate>
                                                                                                        <asp:Label ID="lbltus_itm_line" runat="server" Text='<%# Bind("tus_itm_line") %>'></asp:Label>
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

    <asp:UpdatePanel ID="UpdatePanel8" runat="server">
        <ContentTemplate>
            <asp:Button ID="btnSearchAd" runat="server" Text="Button" Style="display: none;" />
            <asp:ModalPopupExtender ID="mpSearchAdance" runat="server" Enabled="True" TargetControlID="btnSearchAd"
                PopupControlID="pnlpopup" PopupDragHandleControlID="PopupHeader" Drag="true" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>
        </ContentTemplate>
    </asp:UpdatePanel>

    <asp:UpdatePanel ID="UpdatePanel9" runat="server">
        <ContentTemplate>
            <asp:Panel runat="server" ID="pnlpopup" DefaultButton="lbtnSearch" Style="display: none;">
                <div runat="server" id="test" class="panel panel-default width950 height450">
                    <asp:Label ID="lblvalue" runat="server" Text="Label" Visible="false"></asp:Label>
                    <div id="PopupHeader" class="panel-heading">
                        <asp:LinkButton ID="btnSchAdvClose" CausesValidation="false" runat="server" OnClick="btnSchAdvClose_Click">
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
                            <asp:Panel ID="pnlSearchByDate" runat="server">
                                <div class="col-sm-5">
                                    <div class="row">
                                        <div class="col-sm-2 labelText1">
                                            From
                                        </div>
                                        <div class="col-sm-6 paddingRight5 paddingLeft0">
                                            <asp:TextBox runat="server" ID="txtFDate" CausesValidation="false" CssClass="form-control readOnlyText"></asp:TextBox>
                                        </div>
                                        <div class="col-sm-1 paddingLeft0">
                                            <asp:LinkButton ID="lbtnFDate" runat="server" CausesValidation="false">
                                                <span class="glyphicon glyphicon-calendar" aria-hidden="true"></span>
                                            </asp:LinkButton>
                                            <asp:CalendarExtender ID="CalendarExtender3" runat="server" TargetControlID="txtFDate"
                                                PopupButtonID="lbtnFDate" Format="dd/MMM/yyyy">
                                            </asp:CalendarExtender>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-sm-2 labelText1">
                                            To
                                        </div>
                                        <div class="col-sm-6 paddingRight5 paddingLeft0">
                                            <asp:TextBox runat="server" ID="txtTDate" CausesValidation="false" CssClass="form-control readOnlyText"></asp:TextBox>
                                        </div>
                                        <div class="col-sm-1 paddingLeft0">
                                            <asp:LinkButton ID="lbtnTDate" runat="server" CausesValidation="false">
                                                <span class="glyphicon glyphicon-calendar" aria-hidden="true"></span>
                                            </asp:LinkButton>
                                            <asp:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtTDate"
                                                PopupButtonID="lbtnTDate" Format="dd/MMM/yyyy">
                                            </asp:CalendarExtender>
                                        </div>
                                        <div class="col-sm-1 paddingLeft0">
                                            <asp:LinkButton ID="lbtnDateS" runat="server" Visible="false">
                                            <span class="glyphicon glyphicon-search" aria-hidden="true"  ></span>
                                            </asp:LinkButton>
                                        </div>
                                    </div>
                                </div>
                            </asp:Panel>
                            <div class="col-sm-7">
                                <div class="row">
                                    <div class="col-sm-3 labelText1">
                                        Search by key
                                    </div>
                                    <asp:ScriptManagerProxy ID="ScriptManagerProxy10" runat="server"></asp:ScriptManagerProxy>
                                    <asp:UpdatePanel ID="UpdatePanel10" runat="server">
                                        <ContentTemplate>
                                            <div class="col-sm-8 paddingRight5">
                                                <asp:DropDownList ID="ddlSearchbykey" runat="server" class="form-control">
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
                                        <asp:TextBox ID="txtSearchbyword" CausesValidation="false" placeholder="Search by word" class="form-control" AutoPostBack="true" runat="server" OnTextChanged="txtSearchbyword_TextChanged"></asp:TextBox>
                                    </div>
                                    <asp:ScriptManagerProxy ID="ScriptManagerProxy9" runat="server"></asp:ScriptManagerProxy>
                                    <asp:UpdatePanel ID="UpdatePanel11" runat="server">
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
                                    <asp:ScriptManagerProxy ID="ScriptManagerProxy6" runat="server"></asp:ScriptManagerProxy>
                                    <asp:UpdatePanel ID="UpdatePanel12" runat="server">
                                        <ContentTemplate>
                                            <asp:GridView ID="grdResult" CausesValidation="false" runat="server" CssClass="table table-hover table-striped" PageSize="10" PagerStyle-CssClass="cssPager" AllowPaging="True" OnSelectedIndexChanged="grdResult_SelectedIndexChanged" OnPageIndexChanging="grdResult_PageIndexChanging" GridLines="None" ShowHeaderWhenEmpty="True" EmptyDataText="No data found...">
                                                <Columns>
                                                    <asp:ButtonField Text="Select" CommandName="Select" ItemStyle-Width="10" />
                                                </Columns>
                                            </asp:GridView>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                            </div>
                            <asp:UpdateProgress ID="UpdateProgress1" DisplayAfter="10"
                                runat="server" AssociatedUpdatePanelID="UpdatePanel12">
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
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>

    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <asp:TextBox ID="txtOtherRef" CausesValidation="false" runat="server" MaxLength="30" class="form-control" Visible="false"></asp:TextBox>
            <asp:TextBox ID="txtUserSeqNo" CausesValidation="false" runat="server" onkeydown="return jsDecimals(event);" MaxLength="10" class="form-control textAlignRight" Visible="false"></asp:TextBox>
        </ContentTemplate>
    </asp:UpdatePanel>

    <asp:UpdatePanel ID="UpdatePanel14" runat="server">
        <ContentTemplate>
            <asp:Button ID="Button2" runat="server" Text="Button" Style="display: none;" />
            <asp:ModalPopupExtender ID="UserAdPopup" runat="server" Enabled="True" TargetControlID="Button2"
                PopupControlID="pnlASearchpopup" PopupDragHandleControlID="PopupHeaderAd" Drag="true" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>
        </ContentTemplate>
    </asp:UpdatePanel>

    <asp:UpdatePanel ID="UpdatePanel18" runat="server">
        <ContentTemplate>
            <asp:Panel runat="server" ID="pnlASearchpopup" DefaultButton="lbtnSearch">
                <div runat="server" id="Div4" class="panel panel-default height400 width700">
                    <asp:Label ID="lblAvalue" runat="server" Text="Label" Visible="false"></asp:Label>
                    <div class="panel panel-default">
                        <div id="PopupHeaderAd" class="panel-heading">
                            <asp:LinkButton ID="btnAClose" runat="server" OnClick="btnAClose_Click">
                                <span class="glyphicon glyphicon-remove" aria-hidden="true">Close</span>
                            </asp:LinkButton>
                            <%--<span>Commen Search</span>--%>
                            <div class="col-sm-11">
                            </div>
                            <div class="col-sm-1">
                            </div>
                        </div>
                        <div class="panel-body">
                            <div class="col-sm-12" id="Div5" runat="server">
                                <div class="row">
                                    <div class="col-sm-12">
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-12 height5">
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-12" id="Div6" runat="server">
                                        <div class="col-sm-2 labelText1">
                                            Search by key
                                        </div>
                                        <asp:ScriptManagerProxy ID="ScriptManagerProxy11" runat="server"></asp:ScriptManagerProxy>
                                        <asp:UpdatePanel ID="UpdatePanel15" runat="server">
                                            <ContentTemplate>
                                                <div class="col-sm-3 paddingRight5">
                                                    <asp:DropDownList ID="ddlSearchbykeyA" runat="server" class="form-control">
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
                                        <asp:ScriptManagerProxy ID="ScriptManagerProxy12" runat="server"></asp:ScriptManagerProxy>
                                        <asp:UpdatePanel ID="UpdatePanel16" runat="server">
                                            <ContentTemplate>
                                                <div class="col-sm-1 paddingLeft0">
                                                    <asp:LinkButton ID="lbtnSearchA" runat="server" OnClick="lbtnSearchA_Click">
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
                                    <div class="col-sm-11">
                                    </div>
                                    <div class="col-sm-1">
                                        <asp:Button ID="btnAdvanceAddItem" runat="server" CssClass="btn btn-primary btn-xs" Text="Add" OnClick="btnAdvanceAddItem_Click" />
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-12 height5">
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-12">
                                        <asp:ScriptManagerProxy ID="ScriptManagerProxy13" runat="server"></asp:ScriptManagerProxy>
                                        <asp:UpdatePanel ID="UpdatePanel17" runat="server">
                                            <ContentTemplate>
                                                <asp:GridView ID="grdAdSearch" AutoGenerateColumns="false" CausesValidation="false" EmptyDataText="No data found..." ShowHeaderWhenEmpty="True" runat="server" GridLines="None" CssClass="table table-hover table-striped" PageSize="10" PagerStyle-CssClass="cssPager" AllowPaging="True" OnSelectedIndexChanged="grdAdSearch_SelectedIndexChanged" OnPageIndexChanging="grdAdSearch_PageIndexChanging">
                                                    <Columns>
                                                        <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                                            <HeaderTemplate>
                                                                <asp:CheckBox ID="allchk" runat="server" Width="5px" onclick="CheckAll(this)"></asp:CheckBox>
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:CheckBox ID="selectchk" runat="server" Width="5px"></asp:CheckBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Item Code">
                                                            <ItemTemplate>
                                                                <asp:Label ID="Col_ItemCode" runat="server" Text='<%# Bind("Item") %>' Width="100px"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Serial">
                                                            <ItemTemplate>
                                                                <asp:Label ID="Col_Serial1" runat="server" Text='<%# Bind("Serial") %>' Width="100px"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Qty">
                                                            <ItemTemplate>
                                                                <asp:Label ID="Col_inb_qty" runat="server" Text='<%# Bind("inb_qty") %>' Width="30px"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Supplier">
                                                            <ItemTemplate>
                                                                <asp:Label ID="Col_Supplier" runat="server" Text='<%# Bind("Supplier") %>' Width="100px"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Cost" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="Col_ins_unit_cost" runat="server" Text='<%# Bind("ins_unit_cost") %>' Width="100px"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="status" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="Col_ins_itm_stus" runat="server" Text='<%# Bind("ins_itm_stus") %>' Width="100px"></asp:Label>
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

    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <asp:Button ID="btncus" runat="server" Text="Button" Style="display: none;" />
            <asp:ModalPopupExtender ID="mpSendToPDA" runat="server" Enabled="True" TargetControlID="btncus"
                PopupControlID="CustomerPanel" PopupDragHandleControlID="divPDAHdr" Drag="true" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>
        </ContentTemplate>
    </asp:UpdatePanel>

    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <asp:Panel runat="server" ID="CustomerPanel">
                <div runat="server" id="Div1" class="panel panel-default height150 width525">
                    <asp:Label ID="Label14" runat="server" Text="Label" Visible="false"></asp:Label>
                    <div class="panel panel-default">
                        <div id="divPDAHdr" class="panel-heading">
                            <asp:LinkButton ID="btnClosePDA" runat="server" CausesValidation="false" OnClick="btnClosePDA_Click">
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
                                                    <asp:DropDownList ID="ddlloadingbay" runat="server" CssClass="form-control"></asp:DropDownList>
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
                                                    <asp:Button ID="btnsend" runat="server" TabIndex="3" CssClass="btn-info form-control" Text="Send" OnClientClick="return ConfirmSendToPDA();" OnClick="btnsend_Click" />
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
    <script src="<%=Request.ApplicationPath%>Js/bootstrap.min.js"></script>
</asp:Content>
