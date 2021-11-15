<%@ Page Title="" Language="C#" MasterPageFile="~/View/AdminSite.Master" AutoEventWireup="true" CodeBehind="DisposalEntryNew.aspx.cs" MaintainScrollPositionOnPostback="true" Inherits="FastForward.SCMWeb.View.Transaction.Inventory.DisposalEntryNew" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/UserControls/ucPaymodes.ascx" TagPrefix="uc1" TagName="ucPaymodes" %>
<%@ Register Src="~/UserControls/ucOutScan.ascx" TagPrefix="uc1" TagName="ucOutScan" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script type="text/javascript">
        btnClear_Click

        function ConfirmStatusChange() {
            var resasd = confirm("Do you want to change status?");
            if (resasd) {
                document.getElementById('<%=hdfSaveCon.ClientID %>').value = "Yes";
            } else {
                document.getElementById('<%=hdfSaveCon.ClientID %>').value = "No";
            }
        }

        function checkDate(sender, args) {
            if (sender._selectedDate > new Date()) {
                showStickyWarningToast("Cannot select a day later than today!");
                sender._selectedDate = new Date();
                // set the date back to the current date
                sender._textbox.set_Value(sender._selectedDate.format(sender._format));
            }

        }

        function checkDate2(sender, args) {
            var frmDt = document.getElementById('<%= txtValidFrom.ClientID %>').value;
            if (sender._selectedDate < new Date(frmDt)) {
                showStickyWarningToast("Cannot select a day earlier than from date!");
                sender._selectedDate = new Date();
                // set the date back to the current date
                sender._textbox.set_Value(sender._selectedDate.format(sender._format));
            }

        }

        function ConfirmItmSave() {
            var resasd = confirm("Do you want to save items?");
            if (resasd) {
                document.getElementById('<%=hdfSaveCon.ClientID %>').value = "Yes";
            } else {
                document.getElementById('<%=hdfSaveCon.ClientID %>').value = "No";
            }
        }
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

        function CloseAll() {
            if (jQuery('.toast-item-wrapper') != null) {
                jQuery('.toast-item-wrapper').remove();
            }
        }
    </script>

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
            var resasd = confirm("Do you want to create a job?");
            if (resasd) {
                document.getElementById('<%=hdfSaveCon.ClientID %>').value = "Yes";
            } else {
                document.getElementById('<%=hdfSaveCon.ClientID %>').value = "No";
            }
        };

        function confApprove() {
            var resasd = confirm("Do you want to approve the job?");
            if (resasd) {
                document.getElementById('<%=hdfSaveCon.ClientID %>').value = "Yes";
            } else {
                document.getElementById('<%=hdfSaveCon.ClientID %>').value = "No";
            }
        };

        function confDispose() {
            var resasd = "";
            if (document.getElementById('<%=chkDspsNotScan.ClientID %>').checked) {
                resasd = confirm("Do you want to dispose scanned items?");
            }
            else {
                resasd = confirm("Do you want to dispose all  items ?");
            }

            if (resasd) {
                document.getElementById('<%=hdfSaveCon.ClientID %>').value = "Yes";
            } else {
                document.getElementById('<%=hdfSaveCon.ClientID %>').value = "No";
            }
        };

        function confUpdate() {
            var resasd = confirm("Do you want to update the job?");
            if (resasd) {
                document.getElementById('<%=hdfSaveCon.ClientID %>').value = "Yes";
            } else {
                document.getElementById('<%=hdfSaveCon.ClientID %>').value = "No";
            }
        };

        function confClear() {
            var resasd = confirm("Do you want to clear data?");
            if (resasd) {
                document.getElementById('<%=hdfSaveCon.ClientID %>').value = "Yes";
            } else {
                document.getElementById('<%=hdfSaveCon.ClientID %>').value = "No";
            }
        };
        function ConfirmClear() {
            var selectedvalueOrd = confirm("Do you want to clear data ?");
            if (selectedvalueOrd) {
                return true;
            } else {
                return false;
            }
        };
        function confPrint() {
            var resasd = confirm("Do you want to print the document?");
            if (resasd) {
                document.getElementById('<%=hdfSaveCon.ClientID %>').value = "Yes";
            } else {
                document.getElementById('<%=hdfSaveCon.ClientID %>').value = "No";
            }
        };

        function ConfirmDspItemDelete() {
            var resasd = confirm("Do you want to delete the item?");
            if (resasd) {
                document.getElementById('<%=hdfSaveCon.ClientID %>').value = "Yes";
            } else {
                document.getElementById('<%=hdfSaveCon.ClientID %>').value = "No";
            }
        }



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

        function isNumberKey(evt) {
            var charCode = (evt.which) ? evt.which : evt.keyCode;
            if (charCode != 46 && charCode > 31
              && (charCode < 48 || charCode > 57))
                return false;

            return true;
        };

    </script>
    <style type="text/css">
        .DatePanel {
            position: absolute;
            background-color: #FFFFFF;
            . border: 1px solid #646464;
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
    <style>
        /*.panel-default{
            padding:0px 0px 0px 0px; 
            margin-bottom:0px; 
        }
        .panel-body{
            padding-bottom:1px;padding-top:0px;
            margin-bottom:0px; 
        }*/
        .panel {
            margin-bottom: 0px !important;
            padding-bottom: 0px !important;
        }
    </style>
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" runat="server">
    <asp:UpdateProgress ID="UpdateProgress1" DisplayAfter="10"
        runat="server" AssociatedUpdatePanelID="UpdatePanel1">
        <ProgressTemplate>
            <div class="divWaiting">
                <asp:Label ID="lblWaitNew" runat="server"
                    Text="Please wait... " />
                <asp:Image ID="imgWaitNew" runat="server"
                    ImageAlign="Middle" ImageUrl="~/images/icons/ajax-loader.gif" />
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <asp:UpdateProgress ID="UpdatePrddogress1" DisplayAfter="10"
        runat="server" AssociatedUpdatePanelID="upPnlPda">
        <ProgressTemplate>
            <div class="divWaiting">
                <asp:Label ID="lblWadditNew" runat="server"
                    Text="Please wait... " />
                <asp:Image ID="imgWadditNew" runat="server"
                    ImageAlign="Middle" ImageUrl="~/images/icons/ajax-loader.gif" />
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <asp:UpdateProgress ID="UpdatePsrogress1" DisplayAfter="10"
        runat="server" AssociatedUpdatePanelID="upPnlPdaJob">
        <ProgressTemplate>
            <div class="divWaiting">
                <asp:Label ID="lblWattitNew" runat="server"
                    Text="Please wait... " />
                <asp:Image ID="imgWattitNew" runat="server"
                    ImageAlign="Middle" ImageUrl="~/images/icons/ajax-loader.gif" />
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <asp:UpdateProgress ID="updsd" DisplayAfter="10"
        runat="server" AssociatedUpdatePanelID="upPendingJobs">
        <ProgressTemplate>
            <div class="divWaiting">
                <asp:Label ID="lbldddWaitNew2" runat="server"
                    Text="Please wait... " />
                <asp:Image ID="imgdddWaitNew2" runat="server"
                    ImageAlign="Middle" ImageUrl="~/images/icons/ajax-loader.gif" />
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <asp:UpdateProgress ID="UpdateProgress2" DisplayAfter="10"
        runat="server" AssociatedUpdatePanelID="jobDetPnl">
        <ProgressTemplate>
            <div class="divWaiting">
                <asp:Label ID="lblWaitNew2" runat="server"
                    Text="Please wait... " />
                <asp:Image ID="imgWaitNew2" runat="server"
                    ImageAlign="Middle" ImageUrl="~/images/icons/ajax-loader.gif" />
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <asp:UpdateProgress ID="UpdateProgress4" DisplayAfter="10"
        runat="server" AssociatedUpdatePanelID="up2">
        <ProgressTemplate>
            <div class="divWaiting">
                <asp:Label ID="lblWaitNew4" runat="server"
                    Text="Please wait... " />
                <asp:Image ID="imgWaitNew4" runat="server"
                    ImageAlign="Middle" ImageUrl="~/images/icons/ajax-loader.gif" />
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>

    <%--  <asp:UpdatePanel runat="server" ID="mainUpdatePnl">
        <ContentTemplate>--%>
    <div class="col-sm-12">


        <div class="panel panel-default">
            <div class="panel panel-body paddingtopbottom0">
                <asp:ScriptManagerProxy ID="ScriptManagerProxy1" runat="server"></asp:ScriptManagerProxy>
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <asp:HiddenField ID="hdfSaveCon" runat="server" />
                        <asp:HiddenField ID="seqNo" runat="server" />
                        <asp:HiddenField ID="excelErrorValue" runat="server" />
                        <asp:Label ID="lblPrint" runat="server" Visible="false"></asp:Label>
                        <div class="row">
                            <div class="col-sm-12">
                                <div class="col-sm-6  buttonrow">
                                    <%--<asp:Label ID="lblH1" runat="server" Text="Label"></asp:Label>
                                <asp:Label ID="lblBackDateInfor" runat="server"></asp:Label>--%>
                                </div>
                                <div class="col-sm-6  buttonRow">
                                    <div class="col-sm-2 paddingRight0">
                                        <asp:LinkButton ID="btnSerialPrint" runat="server" CssClass="floatRight" OnClick="btnSerialPrint_Click">
                                        <span class="glyphicon glyphicon-print" aria-hidden="true"></span>Serial Print
                                        </asp:LinkButton>
                                    </div>
                                    <div class="col-sm-2 paddingRight0">
                                        <asp:LinkButton ID="btnApprove" CausesValidation="true" runat="server" CssClass="floatRight" OnClientClick="return confApprove()" OnClick="btnApprove_Click">
                                        <span class="glyphicon glyphicon-saved" aria-hidden="true"></span>Approve
                                        </asp:LinkButton>
                                    </div>
                                    <div class="col-sm-2 paddingRight0">
                                        <asp:LinkButton ID="btnDispose" CausesValidation="true" runat="server" CssClass="floatRight" OnClientClick="return confDispose()" OnClick="btnDispose_Click">
                                                    <span class="glyphicon glyphicon-saved" aria-hidden="true"></span>Dispose
                                        </asp:LinkButton>

                                    </div>
                                    <div class="col-sm-2 paddingRight0">
                                        <asp:LinkButton ID="btnUpdate" CausesValidation="true" runat="server" CssClass="floatRight" OnClientClick="return confUpdate()" OnClick="btnUpdate_Click">
                                        <span class="glyphicon glyphicon-saved" aria-hidden="true"></span>Update Job
                                        </asp:LinkButton>
                                    </div>
                                    <div class="col-sm-2">
                                        <asp:LinkButton ID="btnPrint" runat="server" CssClass="floatRight" OnClick="btnPrint_Click">
                                        <span class="glyphicon glyphicon-print" aria-hidden="true"></span>Print
                                        </asp:LinkButton>
                                    </div>
                                    <div class="col-sm-2">
                                        <asp:LinkButton ID="btnClear" runat="server" CssClass="floatRight" OnClientClick="return ConfirmClear();" OnClick="btnClear_Click">
                                        <span class="glyphicon glyphicon-refresh" aria-hidden="true"></span>Clear
                                        </asp:LinkButton>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>

                <div class="col-sm-12 padding0">
                    <div class="panel panel-default marginBottom0">
                        <div class="panel-heading" style="height: 25px; padding-left: 0px; padding-right: 0px;">
                            <div class="col-sm-12">
                                <div class="col-sm-8 padding0">
                                    <strong>Disposal Entry</strong>
                                </div>
                                <div class="col-sm-4 padding0">
                                    <div class="col-sm-12">
                                        <div class="col-sm-4 padding0">
                                            <div class="col-sm-1">
                                                <div style="margin-bottom: -3px;">
                                                    <asp:CheckBox ID="chkDspsNotScan" runat="server" Text="" AutoPostBack="true" OnCheckedChanged="chkDspsNotScan_CheckedChanged" />
                                                </div>
                                            </div>
                                            <div class="col-sm-9 padding3">
                                                Only Scan
                                            </div>
                                        </div>
                                        <div class="col-sm-4 padding0">
                                            <div class="col-sm-1">
                                                <div style="margin-bottom: -3px;">
                                                    <asp:UpdatePanel runat="server" ID="upPnlPdaJob">
                                                        <ContentTemplate>
                                                            <asp:CheckBox ID="chkJOB" runat="server" Text="" AutoPostBack="true" OnCheckedChanged="chkJOB_CheckedChanged" />
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>

                                                </div>
                                            </div>
                                            <div class="col-sm-8 padding3">
                                                PDA Current job
                                            </div>
                                        </div>
                                        <div class="col-sm-4 padding0">
                                            <div class="col-sm-1">
                                                <div style="margin-bottom: -3px;">
                                                    <asp:CheckBox ID="chkSendToPDA" Text="" runat="server" />
                                                </div>
                                            </div>
                                            <div class="col-sm-8 padding3">
                                                Sent to PDA
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="panel panel-body" style="padding-left: 0px; padding-right: 0px;">
                            <div class="col-sm-12 padding0">
                                <div class="row">
                                    <div class="col-sm-12">
                                        <div class="col-sm-8 padding0">
                                            <asp:UpdatePanel runat="server" ID="pendingJobsPnl">
                                                <ContentTemplate>
                                                    <div class="panel panel-default marginBottom0">
                                                        <div class="panel-heading height20 paddingtopbottom0">
                                                            <div class="col-sm-12">
                                                                <div class="col-sm-9">
                                                                    <asp:Panel ID="pnlBtn" runat="server">
                                                                        <asp:LinkButton ID="btnColapse" Visible="true" Text="Pending Jobs" runat="server">Jobs Details
                                                                        <span class="glyphicon glyphicon-arrow-down" aria-hidden="true"></span>
                                                                        </asp:LinkButton>
                                                                    </asp:Panel>
                                                                </div>
                                                                <div class="col-sm-3">
                                                                    <div class="col-sm-2">
                                                                        <asp:CheckBox Text="" ID="chkPenJob" runat="server" />
                                                                    </div>
                                                                    <div class="col-sm-9">
                                                                        Pending Jobs
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="panel panel-body">
                                                            <asp:Panel ID="pnlCollaps" runat="server">
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
                                                                            <div class="col-sm-12 padding0 panelscoll1" style="height: 125px;">
                                                                                <asp:GridView ID="dgvPendingJobs" AutoGenerateColumns="False" TabIndex="39" runat="server" CssClass="table table-hover table-striped" GridLines="None" ShowHeaderWhenEmpty="True" EmptyDataText="No data found...">
                                                                                    <Columns>
                                                                                        <%--<asp:BoundField DataField="DH_SEQ" HeaderText="SEQ" Visible="false" />--%>
                                                                                        <asp:TemplateField HeaderText="">
                                                                                            <ItemTemplate>
                                                                                                <asp:CheckBox Text="" AutoPostBack="true" ID="chkSelectDoc" runat="server" Width="100%" OnCheckedChanged="chkSelectDoc_CheckedChanged" />
                                                                                            </ItemTemplate>
                                                                                            <ItemStyle Width="1%" />
                                                                                            <HeaderStyle Width="1%" />
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="SEQ" Visible="false">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="lblDH_SEQ" Width="100%" runat="server" Text='<%# Bind("DH_SEQ") %>' />
                                                                                            </ItemTemplate>
                                                                                            <ItemStyle Width="0%" />
                                                                                            <HeaderStyle Width="0%" />
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="Document">
                                                                                            <ItemTemplate>
                                                                                                <asp:LinkButton ID="btnPendingJObSelect" Width="100%" Text='<%# Bind("DH_DOC_NO") %>' runat="server" OnClick="btnPendingJObSelect_Click" />
                                                                                            </ItemTemplate>
                                                                                            <ItemStyle Width="15%" />
                                                                                            <HeaderStyle Width="15%" />
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="Date">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="lblDH_DOC_DT" Width="100%" runat="server" Text='<%# Bind("DH_DOC_DT","{0:dd/MMM/yyyy}") %>' />
                                                                                            </ItemTemplate>
                                                                                            <ItemStyle Width="8%" />
                                                                                            <HeaderStyle Width="8%" />
                                                                                        </asp:TemplateField>
                                                                                        <%--<asp:BoundField DataField="DH_REF_NO" HeaderText="Reference" />--%>
                                                                                        <asp:TemplateField HeaderText="Reference">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="lblDH_REF_NO" Width="100%" runat="server" Text='<%# Bind("DH_REF_NO") %>' />
                                                                                            </ItemTemplate>
                                                                                            <ItemStyle Width="20%" />
                                                                                            <HeaderStyle Width="20%" />
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="Max Value">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="lblDH_MAX_VAL" Width="100%" runat="server" Text='<%# Bind("DH_MAX_VAL","{0:N2}") %>' />
                                                                                            </ItemTemplate>
                                                                                            <ItemStyle HorizontalAlign="Right" Width="10%" />
                                                                                            <HeaderStyle CssClass="gridHeaderAlignRight" Width="10%" />
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="Charge">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="lblDH_CHG" Width="100%" runat="server" Text='<%# Bind("DH_CHG","{0:N2}") %>' />
                                                                                            </ItemTemplate>
                                                                                            <ItemStyle HorizontalAlign="Right" Width="10%" />
                                                                                            <HeaderStyle CssClass="gridHeaderAlignRight" Width="10%" />
                                                                                        </asp:TemplateField>
                                                                                        <%--<asp:BoundField HeaderText=" " />--%>
                                                                                        <%--<asp:BoundField DataField="DH_RECIPT_NO" HeaderText="Receipt No" />--%>
                                                                                        <%--<asp:BoundField DataField="DH_RMK" HeaderText="Remark" />--%>
                                                                                        <asp:TemplateField HeaderText="Remark">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="lblDH_RMK" ToolTip='<%# Bind("DH_RMK") %>' Width="100%" runat="server"
                                                                                                    Text='<%# Bind("Tmp_dh_rmk") %>' />
                                                                                            </ItemTemplate>
                                                                                            <ItemStyle Width="40%" />
                                                                                            <HeaderStyle Width="40%" />
                                                                                        </asp:TemplateField>
                                                                                    </Columns>
                                                                                </asp:GridView>
                                                                            </div>
                                                                        </div>
                                                                    </ContentTemplate>
                                                                </asp:UpdatePanel>
                                                            </asp:Panel>
                                                        </div>
                                                    </div>
                                                    <asp:CollapsiblePanelExtender runat="server" ID="cpe" TargetControlID="pnlCollaps" CollapseControlID="btnColapse" ExpandControlID="btnColapse" Collapsed="false" CollapsedSize="0" ExpandedSize="133" ExpandedText="(Collapse...)" CollapsedText="(Expand...)">
                                                    </asp:CollapsiblePanelExtender>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                        <div class="col-sm-4 padding0">
                                            <div class="panel panel-default marginBottom0">
                                                <div class="panel-heading " style="height: 23px;">
                                                    <strong>Job Details</strong>
                                                </div>
                                                <div class="panel panel-body paddingbottom0">
                                                    <asp:UpdatePanel runat="server" ID="jobDetPnl">
                                                        <ContentTemplate>
                                                            <asp:Panel ID="pnlJobData" runat="server">
                                                                <div class="col-sm-12 padding0">
                                                                    <div class="col-sm-6 padding0">
                                                                        <div class="col-sm-4 padding0 labelText1">
                                                                            Valid From:
                                                                        </div>
                                                                        <div class="col-sm-5 padding0">
                                                                            <asp:TextBox ID="txtValidFrom" runat="server" CssClass="form-control" OnTextChanged="txtValidFrom_TextChanged" />
                                                                            <asp:CalendarExtender ID="txtValidFromCal" runat="server" TargetControlID="txtValidFrom"
                                                                                PopupButtonID="btnValidFrom" Format="dd/MMM/yyyy" OnClientDateSelectionChanged="checkDate">
                                                                            </asp:CalendarExtender>
                                                                        </div>
                                                                        <div class="col-sm-2">
                                                                            <asp:LinkButton ID="btnValidFrom" TabIndex="1" CausesValidation="false" runat="server">
                                                     <span class="glyphicon glyphicon-calendar" aria-hidden="true"></span>
                                                                            </asp:LinkButton>
                                                                        </div>
                                                                    </div>
                                                                    <div class="col-sm-6 padding0">
                                                                        <div class="col-sm-4 padding0">
                                                                            Valid To:
                                                                        </div>
                                                                        <div class="col-sm-5 padding0">
                                                                            <asp:TextBox ID="txtValidTo" runat="server" CssClass="form-control" OnTextChanged="txtValidTo_TextChanged" />
                                                                            <asp:CalendarExtender ID="txtValidToCal" runat="server" TargetControlID="txtValidTo"
                                                                                PopupButtonID="btnValidTo" Format="dd/MMM/yyyy" OnClientDateSelectionChanged="checkDate2">
                                                                            </asp:CalendarExtender>
                                                                        </div>
                                                                        <div class="col-sm-2">
                                                                            <asp:LinkButton ID="btnValidTo" TabIndex="1" CausesValidation="false" runat="server">
                                                     <span class="glyphicon glyphicon-calendar" aria-hidden="true"></span>
                                                                            </asp:LinkButton>
                                                                        </div>
                                                                    </div>

                                                                </div>
                                                                <div class="col-sm-12 padding0">
                                                                    <div class="col-sm-6 padding0 ">
                                                                        <div class="col-sm-4 padding0 ">
                                                                            Date:
                                                                        </div>
                                                                        <div class="col-sm-5 padding0">
                                                                            <asp:TextBox ID="txtDate" runat="server" CssClass="form-control" ReadOnly="true" />
                                                                            <%--<asp:CalendarExtender ID="txtDateCal" runat="server" TargetControlID="txtDate"
                                                            PopupButtonID="btnDate" Format="dd/MMM/yyyy">
                                                        </asp:CalendarExtender>--%>
                                                                        </div>
                                                                        <div class="col-sm-2">
                                                                            <asp:LinkButton ID="btnDate" TabIndex="1" CausesValidation="false" runat="server">
                                                     <span class="glyphicon glyphicon-calendar" aria-hidden="true"></span>
                                                                            </asp:LinkButton>
                                                                        </div>
                                                                    </div>
                                                                    <div class="col-sm-6 padding0">
                                                                        <div class="col-sm-4 padding0">
                                                                            Job #:
                                                                        </div>
                                                                        <div class="col-sm-7 padding0">
                                                                            <asp:TextBox ID="txtJobNo" runat="server" CssClass="form-control" AutoPostBack="true" OnTextChanged="txtJobNo_TextChanged" />
                                                                        </div>
                                                                        <div class="col-sm-1 padding0">
                                                                            <asp:LinkButton ID="lbnJobNo" runat="server" CausesValidation="false" OnClick="lbnJobNo_Click">
                                                        <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                                            </asp:LinkButton>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class="col-sm-12 padding0">
                                                                    <div class="col-sm-6 padding0 labelText1">
                                                                        <div class="col-sm-4 padding0">
                                                                            Amount:
                                                                        </div>
                                                                        <div class="col-sm-7 padding0">
                                                                            <asp:TextBox ID="txtJobAmount" runat="server" CssClass="form-control" onkeydown="return jsDecimals(event);" OnTextChanged="txtJobAmount_TextChanged" />
                                                                        </div>
                                                                    </div>
                                                                    <div class="col-sm-6 padding0">
                                                                        <asp:Button ID="btnCheckCost" runat="server" Text="Check Cost" OnClick="btnCheckCost_Click" />
                                                                    </div>
                                                                    <div class="col-sm-12 padding0">
                                                                        <%--<asp:Button ID="btnCreateJob" runat="server" Text="Create" />--%>
                                                                        <div class="col-sm-6 padding0 labelText1">
                                                                            <div class="col-sm-4 padding0">
                                                                                Customer:
                                                                            </div>
                                                                            <div class="col-sm-7 padding0">
                                                                                <asp:TextBox ID="textCust" runat="server" CssClass="form-control" AutoPostBack="true" OnTextChanged="textCust_TextChanged" />
                                                                            </div>
                                                                            <div class="col-sm-1 padding0">
                                                                                <asp:LinkButton ID="lbtnCust" runat="server" CausesValidation="false" OnClick="lbtnCust_Click">
                                                                <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                                                </asp:LinkButton>
                                                                            </div>
                                                                        </div>
                                                                        <div class="col-sm-6 padding0">
                                                                            <asp:Button ID="btnPaymnt" runat="server" Text="Payment" OnClick="btnPaymnt_Click" />
                                                                        </div>
                                                                    </div>
                                                                    <div class="col-sm-12 padding0 labelText1">
                                                                        <div class="col-sm-2 padding0 ">
                                                                            Remark:
                                                                        </div>
                                                                        <div class="col-sm-10 padding0">
                                                                            <asp:TextBox ID="txtRemark" runat="server" CssClass="form-control" />
                                                                        </div>


                                                                    </div>
                                                                    <div class="col-sm-12 padding0 labelText1">
                                                                        <div class="col-sm-6 padding0">
                                                                            <div class="col-sm-4 padding0">
                                                                                Ref:
                                                                            </div>
                                                                            <div class="col-sm-8 padding0">
                                                                                <asp:TextBox ID="txtReference" runat="server" CssClass="form-control" />
                                                                            </div>
                                                                        </div>
                                                                        <div class="col-sm-3">
                                                                        </div>
                                                                        <div class="col-sm-3 padding0">
                                                                            <asp:Button ID="btnJobCreate" runat="server" Text="Create Job" OnClientClick="return confSave()" OnClick="btnJobCreate_Click" />
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
                                <div class="row">
                                    <div class="col-sm-12">
                                        <div class="col-sm-12">
                                            <div class="panel panel-default">
                                                <asp:UpdatePanel runat="server" ID="UpdatePanel16">
                                                    <ContentTemplate>
                                                        <asp:Panel ID="pnlHideBtn" runat="server">
                                                            <div class="panel panel-heading">
                                                                <asp:LinkButton ID="btnHideArea" Text="Disposal Data" runat="server">Location/Job Data
                                                                    <span class="glyphicon glyphicon-arrow-down" aria-hidden="true"></span> 
                                                                </asp:LinkButton>
                                                            </div>
                                                        </asp:Panel>
                                                        <asp:Panel runat="server" ID="pnlHideArea">
                                                            <asp:UpdatePanel runat="server" ID="upPnlHideArea">
                                                                <ContentTemplate>
                                                                    <div class="col-sm-8 padding0">
                                                                        <div class="col-sm-12" style="padding-left: 0px;">
                                                                            <div class="col-sm-4 padding0">
                                                                                <div class="panel panel-default">
                                                                                    <div class="panel-heading" style="height: 23px;">
                                                                                        <strong>Disposal Locations</strong>
                                                                                    </div>
                                                                                    <div class="panel panel-body paddingbottom0">
                                                                                        <asp:UpdatePanel runat="server">
                                                                                            <ContentTemplate>
                                                                                                <asp:Panel ID="pnlDispatchLoc" runat="server">
                                                                                                    <div class="row">
                                                                                                        <div class="col-sm-12">
                                                                                                            <div class="col-sm-2 padding0 labelText1">
                                                                                                                Location
                                                                                                            </div>
                                                                                                            <div class="col-sm-2 padding0">
                                                                                                                <asp:TextBox ID="txtLocation" CssClass="form-control" runat="server" AutoPostBack="true" Style="text-transform: uppercase" OnTextChanged="txtLocation_TextChanged" />

                                                                                                            </div>
                                                                                                            <div class="col-sm-6" style="padding-left: 2px; padding-right: 0px;">
                                                                                                                <asp:TextBox ID="txtLocDesc" CssClass="form-control" runat="server" ReadOnly="true" />
                                                                                                            </div>
                                                                                                            <div class="col-sm-2 padding3">
                                                                                                                <div class="col-sm-6 padding0">
                                                                                                                    <asp:LinkButton ID="btnLocation" runat="server" CausesValidation="false" OnClick="btnLocation_Click">
                                                                                    <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                                                                                    </asp:LinkButton>
                                                                                                                </div>
                                                                                                                <div class="col-sm-6 padding0">
                                                                                                                    <asp:LinkButton ID="btnAddLocation" runat="server" CausesValidation="false" OnClick="btnAddLocation_Click">
                                                                                    <span class="glyphicon glyphicon-plus" aria-hidden="true"></span>
                                                                                                                    </asp:LinkButton>
                                                                                                                </div>
                                                                                                            </div>
                                                                                                        </div>
                                                                                                    </div>
                                                                                                    <div class="row">
                                                                                                        <div class="col-sm-12">
                                                                                                            <div style="overflow-y: scroll; height: 80px;">
                                                                                                                <asp:GridView ID="dgvLocations" AutoGenerateColumns="False" TabIndex="39" runat="server" CssClass="table table-hover table-striped" GridLines="None" ShowHeaderWhenEmpty="True" EmptyDataText="No data found...">
                                                                                                                    <Columns>
                                                                                                                        <asp:BoundField DataField="DL_SEQ" HeaderText="Seq" Visible="false" />
                                                                                                                        <asp:BoundField DataField="DL_DOC_NO" HeaderText="Doc" Visible="false" />
                                                                                                                        <asp:TemplateField>
                                                                                                                            <ItemTemplate>
                                                                                                                                <asp:LinkButton ID="grdLbtnDltLoc" runat="server" CausesValidation="false" OnClientClick="return ConfirmItemDelete()" OnClick="grdLbtnDltLoc_Click">
                                                                        <span aria-hidden="true" class="glyphicon glyphicon-trash"></span>
                                                                                                                                </asp:LinkButton>
                                                                                                                            </ItemTemplate>
                                                                                                                        </asp:TemplateField>
                                                                                                                        <asp:TemplateField HeaderText="Location">
                                                                                                                            <ItemTemplate>
                                                                                                                                <asp:Label ID="lblDL_LOC" runat="server" Text='<%# Bind("DL_LOC") %>'></asp:Label>
                                                                                                                            </ItemTemplate>
                                                                                                                        </asp:TemplateField>
                                                                                                                        <asp:BoundField DataField="Dl_loc_Desc" HeaderText="Description" />
                                                                                                                        <asp:BoundField DataField="DL_ACT" HeaderText="Status" Visible="false" />
                                                                                                                        <asp:TemplateField>
                                                                                                                            <ItemTemplate>
                                                                                                                                <%--<asp:LinkButton ID="btnDeleteLocation" CausesValidation="false" OnClientClick="return confDeleteLoc()" OnClick="btnDeleteLocation_Click" runat="server">
                                                                                                        <span class="glyphicon glyphicon-trash" aria-hidden="true"></span>
                                                                        </asp:LinkButton>--%>
                                                                                                                            </ItemTemplate>
                                                                                                                        </asp:TemplateField>
                                                                                                                    </Columns>
                                                                                                                </asp:GridView>
                                                                                                            </div>
                                                                                                        </div>
                                                                                                    </div>

                                                                                                </asp:Panel>
                                                                                            </ContentTemplate>
                                                                                        </asp:UpdatePanel>
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                            <div class="col-sm-4 padding0">
                                                                                <div class="panel panel-default">
                                                                                    <div class="panel-heading" style="height: 23px;">
                                                                                        <strong>Allocated Stock Status</strong>
                                                                                    </div>
                                                                                    <div class="panel panel-body">
                                                                                        <asp:UpdatePanel runat="server">
                                                                                            <ContentTemplate>
                                                                                                <asp:Panel ID="pnlDispatchStus" runat="server">
                                                                                                    <div class="row">
                                                                                                        <div class="col-sm-12">
                                                                                                            <div class="col-sm-3 padding0 labelText1">
                                                                                                                Status
                                                                                                            </div>
                                                                                                            <div class="col-sm-7 padding0">
                                                                                                                <asp:TextBox ID="txtStatus" CssClass="form-control" runat="server" AutoPostBack="true" Style="text-transform: uppercase" OnTextChanged="txtStatus_TextChanged" />
                                                                                                                <asp:TextBox ID="txtStatusCd" CssClass="form-control" runat="server" AutoPostBack="true" Style="text-transform: uppercase" Visible="false" />
                                                                                                            </div>

                                                                                                            <div class="col-sm-1 padding3">
                                                                                                                <asp:LinkButton ID="lbtnStatus" runat="server" CausesValidation="false" OnClick="lbtnStatus_Click">
                                                                                    <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                                                                                </asp:LinkButton>
                                                                                                            </div>
                                                                                                            <div class="col-sm-1 padding0">
                                                                                                                <asp:LinkButton ID="lbtnAddStatus" runat="server" CausesValidation="false" OnClick="lbtnAddStatus_Click">
                                                                                    <span class="glyphicon glyphicon-plus" aria-hidden="true"></span>
                                                                                                                </asp:LinkButton>
                                                                                                            </div>
                                                                                                        </div>
                                                                                                    </div>
                                                                                                    <div class="row">
                                                                                                        <div class="col-sm-12">
                                                                                                            <div style="overflow-y: scroll; height: 75px;">
                                                                                                                <asp:GridView ID="dgvStatus" AutoGenerateColumns="False" runat="server" CssClass="table table-hover table-striped" GridLines="None" ShowHeaderWhenEmpty="True" EmptyDataText="No data found...">
                                                                                                                    <Columns>
                                                                                                                        <asp:TemplateField>
                                                                                                                            <ItemTemplate>
                                                                                                                                <asp:LinkButton ID="grdLbtnDltStus" runat="server" CausesValidation="false" OnClientClick="return ConfirmItemDelete()" OnClick="grdLbtnDltStus_Click">
                                                                        <span aria-hidden="true" class="glyphicon glyphicon-trash"></span>
                                                                                                                                </asp:LinkButton>
                                                                                                                            </ItemTemplate>
                                                                                                                        </asp:TemplateField>
                                                                                                                        <asp:TemplateField HeaderText="Code">
                                                                                                                            <ItemTemplate>
                                                                                                                                <asp:Label ID="lblStusCd" runat="server" Text='<%# Bind("Ids_stus") %>'></asp:Label>
                                                                                                                            </ItemTemplate>
                                                                                                                        </asp:TemplateField>
                                                                                                                        <asp:TemplateField HeaderText="Status">
                                                                                                                            <ItemTemplate>
                                                                                                                                <asp:Label ID="lblStusDesc" runat="server" Text='<%# Bind("Ids_stus_desc") %>'></asp:Label>
                                                                                                                            </ItemTemplate>
                                                                                                                        </asp:TemplateField>
                                                                                                                    </Columns>
                                                                                                                </asp:GridView>
                                                                                                            </div>
                                                                                                        </div>
                                                                                                    </div>
                                                                                                </asp:Panel>
                                                                                            </ContentTemplate>
                                                                                        </asp:UpdatePanel>
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                            <div class="col-sm-4 padding0">
                                                                                <div class="panel panel-default">
                                                                                    <div class="panel-heading" style="height: 23px;">
                                                                                        <strong>Stocks At</strong>
                                                                                    </div>
                                                                                    <div class="panel panel-body ">
                                                                                        <asp:UpdatePanel runat="server">
                                                                                            <ContentTemplate>
                                                                                                <asp:Panel ID="pnlStocksAt" runat="server">
                                                                                                    <div class="row">
                                                                                                        <div class="col-sm-12">
                                                                                                            <div class="col-sm-2 padding0 labelText1">
                                                                                                                Location
                                                                                                            </div>
                                                                                                            <div class="col-sm-2 padding0">
                                                                                                                <asp:TextBox ID="txtStockAt" CssClass="form-control" runat="server" AutoPostBack="true" Style="text-transform: uppercase" OnTextChanged="txtStockAt_TextChanged" />
                                                                                                            </div>
                                                                                                            <div class="col-sm-6" style="padding-left: 2px; padding-right: 0px;">
                                                                                                                <asp:TextBox ID="txtStockAtDesc" CssClass="form-control" runat="server" ReadOnly="true" />
                                                                                                            </div>
                                                                                                            <div class="col-sm-2 padding0">
                                                                                                                <div class="col-sm-6 padding3">
                                                                                                                    <asp:LinkButton ID="lbtnStockAt" runat="server" CausesValidation="false" OnClick="lbtnStockAt_Click">
                                                                                    <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                                                                                    </asp:LinkButton>
                                                                                                                </div>
                                                                                                                <div class="col-sm-6 padding0">
                                                                                                                    <asp:LinkButton ID="lbtnAddStockAt" runat="server" CausesValidation="false" OnClick="lbtnAddStockAt_Click">
                                                                                    <span class="glyphicon glyphicon-plus" aria-hidden="true"></span>
                                                                                                                    </asp:LinkButton>
                                                                                                                </div>
                                                                                                            </div>
                                                                                                        </div>
                                                                                                    </div>
                                                                                                    <div class="row">
                                                                                                        <div class="col-sm-12">
                                                                                                            <div style="overflow-y: scroll; height: 75px;">
                                                                                                                <asp:GridView ID="dgvStockAt" AutoGenerateColumns="False" runat="server" CssClass="table table-hover table-striped" GridLines="None" ShowHeaderWhenEmpty="True" EmptyDataText="No data found...">
                                                                                                                    <Columns>
                                                                                                                        <asp:BoundField DataField="IDC_SEQ" HeaderText="Seq" Visible="false" />
                                                                                                                        <asp:BoundField DataField="IDC_DOC_NO" HeaderText="Doc" Visible="false" />
                                                                                                                        <asp:TemplateField>
                                                                                                                            <ItemTemplate>
                                                                                                                                <asp:LinkButton ID="grdLbtnDltCLoc" runat="server" CausesValidation="false" OnClientClick="return ConfirmItemDelete()" OnClick="grdLbtnDltCLoc_Click">
                                                                        <span aria-hidden="true" class="glyphicon glyphicon-trash"></span>
                                                                                                                                </asp:LinkButton>
                                                                                                                            </ItemTemplate>
                                                                                                                        </asp:TemplateField>
                                                                                                                        <asp:TemplateField HeaderText="Location">
                                                                                                                            <ItemTemplate>
                                                                                                                                <asp:Label ID="lblIDC_LOC" runat="server" Text='<%# Bind("IDC_LOC") %>'></asp:Label>
                                                                                                                            </ItemTemplate>
                                                                                                                        </asp:TemplateField>
                                                                                                                        <asp:BoundField DataField="IDC_loc_Desc" HeaderText="Description" />
                                                                                                                        <asp:BoundField DataField="IDC_ACT" HeaderText="Status" Visible="false" />
                                                                                                                        <asp:TemplateField>
                                                                                                                            <ItemTemplate>
                                                                                                                                <%--<asp:LinkButton ID="btnDeleteLocation" CausesValidation="false" OnClientClick="return confDeleteLoc()" OnClick="btnDeleteLocation_Click" runat="server">
                                                                                                        <span class="glyphicon glyphicon-trash" aria-hidden="true"></span>
                                                                        </asp:LinkButton>--%>
                                                                                                                            </ItemTemplate>
                                                                                                                        </asp:TemplateField>
                                                                                                                    </Columns>
                                                                                                                </asp:GridView>
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
                                                                    <div class="col-sm-4 paddingLeft0">
                                                                        <div class="row">
                                                                            <div class="col-sm-12 paddingLeft0">
                                                                                <div class="panel panel-default">
                                                                                    <div class="panel-heading" style="font-weight: bold">
                                                                                        Payment Details
                                                                                    </div>
                                                                                    <div class="panel panel-body padding0" style="height: 102px;">
                                                                                        <div class="row">
                                                                                            <div class="col-sm-12">
                                                                                                <asp:UpdatePanel runat="server">
                                                                                                    <ContentTemplate>

                                                                                                        <asp:Panel ID="pnlPayDet" runat="server">
                                                                                                            <asp:GridView ID="grdPymntDet" CausesValidation="false" runat="server" CssClass="table table-hover table-striped" PageSize="4" PagerStyle-CssClass="cssPager" AllowPaging="True" GridLines="None" ShowHeaderWhenEmpty="True" EmptyDataText="No data found..." AutoGenerateColumns="False" OnPageIndexChanging="grdPymntDet_PageIndexChanging">
                                                                                                                <Columns>
                                                                                                                    <asp:TemplateField HeaderText="Rec No">
                                                                                                                        <ItemTemplate>
                                                                                                                            <asp:Label ID="grdLblRecNo" runat="server" Text='<%# Bind("Sar_receipt_no") %>'></asp:Label>
                                                                                                                        </ItemTemplate>
                                                                                                                    </asp:TemplateField>
                                                                                                                    <asp:TemplateField HeaderText="Date">
                                                                                                                        <ItemTemplate>
                                                                                                                            <asp:Label ID="grdLblRecDt" runat="server" Text='<%# (Convert.ToDateTime(Eval("Sar_receipt_date"))).ToString("dd/MMM/yyyy") %>'></asp:Label>
                                                                                                                        </ItemTemplate>
                                                                                                                    </asp:TemplateField>
                                                                                                                    <asp:TemplateField HeaderText="Customer">
                                                                                                                        <ItemTemplate>
                                                                                                                            <asp:Label ID="grdLblCust" runat="server" Text='<%# Bind("Sar_debtor_cd") %>'></asp:Label>
                                                                                                                        </ItemTemplate>
                                                                                                                    </asp:TemplateField>
                                                                                                                    <asp:TemplateField HeaderText="Amount" ItemStyle-HorizontalAlign="Right">
                                                                                                                        <ItemTemplate>
                                                                                                                            <asp:Label ID="grdLblRecAmt" runat="server" Text='<%# (Convert.ToDecimal(Eval("Sar_tot_settle_amt"))).ToString("N2") %>'></asp:Label>
                                                                                                                        </ItemTemplate>
                                                                                                                    </asp:TemplateField>
                                                                                                                </Columns>
                                                                                                            </asp:GridView>
                                                                                                        </asp:Panel>

                                                                                                    </ContentTemplate>
                                                                                                </asp:UpdatePanel>
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
                                                        <asp:CollapsiblePanelExtender runat="server"
                                                            ID="colHideArea" TargetControlID="pnlHideArea" CollapseControlID="btnHideArea" ExpandControlID="btnHideArea"
                                                            Collapsed="false" CollapsedSize="0" ExpandedSize="133" ExpandedText="(Collapse...)" CollapsedText="(Expand...)">
                                                        </asp:CollapsiblePanelExtender>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-12">
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-12">
                                        <div class="col-sm-8 paddingRight0">
                                            <div class="panel panel-default">
                                                <div class="panel-heading" style="height: 23px;">
                                                    <strong runat="server" id="ItemDetailsDiv">Item Details</strong>
                                                </div>
                                                <div class="panel panel-body padding0">
                                                    <div class="row">
                                                        <div class="col-sm-12">
                                                            <%--<uc1:ucOutScan runat="server" ID="ucOutScan" />--%>
                                                            <asp:UpdatePanel runat="server" ID="itmDetPnl">
                                                                <ContentTemplate>
                                                                    <asp:Panel ID="Defaul" runat="server" Visible="true">
                                                                        <div class="row">
                                                                            <div class="col-sm-4">
                                                                                <div class="row">
                                                                                    <div class="col-sm-1">
                                                                                    </div>

                                                                                </div>
                                                                            </div>
                                                                            <div class="col-sm-6">
                                                                                <div class="row">
                                                                                </div>
                                                                            </div>
                                                                            <div class="col-sm-2">
                                                                                <div class="row">
                                                                                    <div class="col-sm-5 labelText1">
                                                                                    </div>
                                                                                    <div class="col-sm-2">
                                                                                        <asp:LinkButton ID="lbtnAdd" runat="server" OnClick="lbtnAdd_Click">
                                                            <span class="glyphicon glyphicon-plus" aria-hidden="true"></span>
                                                                                        </asp:LinkButton>
                                                                                    </div>
                                                                                    <div class="col-sm-3">
                                                                                        <asp:LinkButton ID="lbtnClear" runat="server" CssClass="floatRight" OnClick="lbtnClear_Click">
                                                            <span class="glyphicon glyphicon-refresh" aria-hidden="true"></span>
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
                                                                            <div class="col-sm-4">
                                                                                <div class="row">

                                                                                    <div class="col-sm-3 paddingRight0" runat="server" id="ItemCodeDiv">
                                                                                        Item code
                                                    <asp:DropDownList ID="ddlItemType" runat="server" class="form-control" AutoPostBack="true" Visible="false">
                                                        <asp:ListItem Text="Item Code" Value="I"></asp:ListItem>
                                                        <asp:ListItem Text="Model #" Value="M"></asp:ListItem>
                                                        <asp:ListItem Text="Part #" Value="P"></asp:ListItem>
                                                    </asp:DropDownList>
                                                                                    </div>

                                                                                    <div class="col-sm-8 paddingRight5">
                                                                                        <asp:TextBox ID="txtItemCode" CausesValidation="false" runat="server" MaxLength="20" class="form-control" AutoPostBack="true" Style="text-transform: uppercase;" OnTextChanged="txtItemCode_TextChanged"></asp:TextBox>
                                                                                    </div>
                                                                                    <div class="col-sm-1 paddingLeft0">
                                                                                        <asp:LinkButton ID="lbtnItemCode" runat="server" OnClick="lbtnItemCode_Click">
                                                        <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                                                        </asp:LinkButton>
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                            <div class="col-sm-3 padding0">
                                                                                <div class="row">
                                                                                    <div class="col-sm-4 labelText1">
                                                                                        Status
                                                                                    </div>
                                                                                    <div class="col-sm-8">
                                                                                        <asp:DropDownList ID="ddlStatus" runat="server" class="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlStatus_SelectedIndexChanged">
                                                                                        </asp:DropDownList>
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                            <div class="col-sm-3 paddingleft0">
                                                                                <div class="row">
                                                                                    <div class="col-sm-2  labelText1">
                                                                                        Qty
                                                                                    </div>
                                                                                    <div class="col-sm-6">
                                                                                        <asp:TextBox ID="txtQty" CausesValidation="false" runat="server" class="form-control textAlignRight"
                                                                                            AutoPostBack="true" OnTextChanged="txtQty_TextChanged"
                                                                                            MaxLength="10" onkeypress="return isNumberKey(event)"></asp:TextBox>
                                                                                    </div>
                                                                                    <div class="col-sm-1 padding0">
                                                                                        <asp:CheckBox Text="" ID="chkOnlyQty" AutoPostBack="true" OnCheckedChanged="chkOnlyQty_CheckedChanged" runat="server" />
                                                                                    </div>
                                                                                    <div class="col-sm-3 padding0">
                                                                                        <asp:Label Text="Only Qty" runat="server" />
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                        <div class="row">
                                                                            <div class="col-sm-4">
                                                                                <div class="row">
                                                                                    <div class="col-sm-3 labelText1">
                                                                                        Disp Loc
                                                                                    </div>
                                                                                    <div class="col-sm-8 paddingRight5">
                                                                                        <asp:TextBox ID="txtItmDispLoc" CausesValidation="false" runat="server" MaxLength="20" class="form-control" AutoPostBack="true" OnTextChanged="txtItmDispLoc_TextChanged" Style="text-transform: uppercase;"></asp:TextBox>
                                                                                    </div>
                                                                                    <div class="col-sm-1 paddingLeft0">
                                                                                        <asp:LinkButton ID="lbtnItmDispLoc" runat="server" OnClick="lbtnItmDispLoc_Click">
                                                        <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                                                        </asp:LinkButton>
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                            <div class="col-sm-4">
                                                                                <div class="row">
                                                                                    <asp:Label ID="lblItmDispLoc" runat="server" ForeColor="#a513d0" Font-Bold="true"></asp:Label>
                                                                                </div>
                                                                            </div>
                                                                            <div class="col-sm-4">
                                                                                <div class="col-sm-3 labelText1">
                                                                                    Curr. Loc
                                                                                </div>
                                                                                <div class="col-sm-8 paddingRight5">
                                                                                    <asp:TextBox ID="txtItmcurLoc" CausesValidation="false" runat="server" MaxLength="20" class="form-control" AutoPostBack="true" OnTextChanged="txtItmcurLoc_TextChanged" Style="text-transform: uppercase;"></asp:TextBox>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                        <div class="row">
                                                                            <div class="col-sm-12 height5">
                                                                            </div>
                                                                        </div>
                                                                        <div class="row">
                                                                            <div class="col-sm-4">
                                                                                <div class="row">

                                                                                    <div class="col-sm-3 paddingRight0">
                                                                                        Serial #I
                                                                                    </div>

                                                                                    <div class="col-sm-8 paddingRight5">
                                                                                        <asp:TextBox ID="txtSerialI" CausesValidation="false" runat="server" MaxLength="20" class="form-control" AutoPostBack="true" OnTextChanged="txtSerialI_TextChanged" ReadOnly="false"></asp:TextBox>
                                                                                    </div>
                                                                                    <div class="col-sm-1 paddingLeft0">
                                                                                        <asp:LinkButton ID="lbtnSerialI" runat="server" OnClick="lbtnSerialI_Click">
                                                        <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                                                        </asp:LinkButton>
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                            <div class="col-sm-4">
                                                                                <div class="row">

                                                                                    <div class="col-sm-3 paddingRight0">
                                                                                        Serial #II
                                                                                    </div>

                                                                                    <div class="col-sm-8 paddingRight5">
                                                                                        <asp:TextBox ID="txtSerialII" CausesValidation="false" runat="server" MaxLength="20" class="form-control" AutoPostBack="true" OnTextChanged="txtItemCode_TextChanged" Style="text-transform: uppercase;" ReadOnly="false"></asp:TextBox>
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                            <div class="col-sm-4" style="display: none;">
                                                                                <div class="row">

                                                                                    <div class="col-sm-3 paddingRight0">
                                                                                        Serial #III
                                                                                    </div>

                                                                                    <div class="col-sm-8 paddingRight5">
                                                                                        <asp:TextBox ID="txtSerialIII" CausesValidation="false" runat="server" MaxLength="20" class="form-control" AutoPostBack="true" OnTextChanged="txtItemCode_TextChanged" Style="text-transform: uppercase;" ReadOnly="false"></asp:TextBox>
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                        <div class="row">
                                                                            <div class="col-sm-4">
                                                                                <div class="row">
                                                                                    <div class="col-sm-4 labelText1 fontWeight900">
                                                                                        Description :
                                                                                    </div>
                                                                                    <div class="col-sm-8 paddingLeft0 paddingRight0 labelText1">
                                                                                        <asp:Label ID="lblDescription" runat="server" Text="" CssClass="Color1 fontWeight900"></asp:Label>
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                            <div class="col-sm-3">
                                                                                <div class="row">
                                                                                    <div class="col-sm-4 labelText1 fontWeight900">
                                                                                        Model :
                                                                                    </div>
                                                                                    <div class="col-sm-8 labelText1">
                                                                                        <asp:Label ID="lblModel" runat="server" Text="" CssClass="Color1 fontWeight900"></asp:Label>
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                            <div class="col-sm-3">
                                                                                <div class="row">
                                                                                    <div class="col-sm-4 labelText1 fontWeight900 ">
                                                                                        Brand :
                                                                                    </div>
                                                                                    <div class="col-sm-8 labelText1">
                                                                                        <asp:Label ID="lblBrand" runat="server" Text="" CssClass="Color1 fontWeight900"></asp:Label>
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                            <div class="col-sm-2">
                                                                                <div class="row">
                                                                                    <div class="col-sm-5 labelText1 fontWeight900">
                                                                                        Part # :
                                                                                    </div>
                                                                                    <div class="col-sm-7 labelText1">
                                                                                        <asp:Label ID="lblPart" runat="server" Text="" CssClass="Color1 fontWeight900"></asp:Label>
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                        <div class="row height10">
                                                                        </div>
                                                                    </asp:Panel>

                                                                </ContentTemplate>
                                                            </asp:UpdatePanel>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-sm-4 paddingLeft0">
                                            <div class="panel panel-default">
                                                <div class="panel panel-heading" style="height: 23px;">

                                                    <div class="col-sm-12">
                                                        <div class="col-sm-7 padding0">
                                                            <strong>PDA Job Details</strong>
                                                        </div>
                                                        <div class="col-sm-4 ">
                                                            <div class="col-sm-2">
                                                                <%--<asp:CheckBox Text="" ID="chkPdaCom" runat="server" />--%>
                                                            </div>
                                                            <div class="col-sm-8">
                                                                <%--PDA Completed--%>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="panel panel-body" style="padding-top: 0px; margin-top: 0px;">
                                                    <div style="height: 128px; overflow-y: scroll;">
                                                        <asp:UpdatePanel runat="server" ID="upPnlPda">
                                                            <ContentTemplate>
                                                                <asp:GridView ID="grdPDACurrentJob" EmptyDataText="No data found..." ShowHeaderWhenEmpty="True" runat="server"
                                                                    GridLines="None" CssClass="table table-hover table-striped" AutoGenerateColumns="false">
                                                                    <Columns>
                                                                        <asp:TemplateField HeaderText="select">
                                                                            <ItemTemplate>
                                                                                <asp:CheckBox AutoPostBack="true" ID="chk_job" OnCheckedChanged="chk_job_CheckedChanged" runat="server" Checked="false" Width="5px" />
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="DOC #">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="col_doc" runat="server" Text='<%# Bind("tuh_doc_no") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="User">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="col_lp" runat="server" Text='<%# Bind("tuh_usr_id") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="seq" Visible="false">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="col_SEQ" runat="server" Text='<%# Bind("tuh_usrseq_no") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="seq" Visible="false">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lbltus_fin_stus" runat="server" Text='<%# Bind("tuh_fin_stus") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Scan Qty" Visible="true">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblDocPickQty" runat="server"></asp:Label>
                                                                            </ItemTemplate>
                                                                            <ItemStyle CssClass="gridHeaderAlignRight" />
                                                                            <HeaderStyle CssClass="gridHeaderAlignRight" />
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
                                <div class="row">
                                    <div class="col-sm-12">
                                        <asp:UpdatePanel runat="server" ID="up2">
                                            <ContentTemplate>
                                                <div class="col-sm-12">
                                                    <div class="panel panel-default paddingbottom0">
                                                        <div class="panel-heading padding0">
                                                            <div class="row">
                                                                <div class="col-sm-2">
                                                                    <strong id="disposalEntryDiv" runat="server">Disposal Items</strong>
                                                                </div>
                                                                <div class="col-sm-2">
                                                                    <asp:Label Text="" ID="lblDocQty" runat="server" />
                                                                </div>
                                                                <div class="col-sm-2">
                                                                    <asp:Label Text="" ID="lblPdaJobQty" runat="server" />
                                                                </div>
                                                                <div class="col-sm-2">
                                                                    <asp:LinkButton ID="lbtnupload" CausesValidation="false" runat="server" CssClass="floatRight" OnClick="lbtnupload_Click">
                                                                <span class="glyphicon glyphicon-upload"></span> Upload Excel
                                                                    </asp:LinkButton>
                                                                </div>
                                                                <div class="col-sm-1">
                                                                    <asp:Button ID="btnChngStus" runat="server" Text="Change Status" OnClientClick="return ConfirmStatusChange()" OnClick="btnChngStus_Click" />
                                                                </div>
                                                                <div class="col-sm-1">
                                                                    <asp:Button ID="btnSaveDispItm" runat="server" Text="Save Items" OnClientClick="return ConfirmItmSave()" OnClick="btnSaveDispItm_Click" />
                                                                </div>
                                                                <div class="col-sm-2">
                                                                    <div class="col-sm-1">
                                                                        <asp:CheckBox Text="" ID="chkAllItem" runat="server" />
                                                                    </div>
                                                                    <div class="col-sm-9 padding0" runat="server" id="allItemDiv">
                                                                        All Item
                                                                    </div>
                                                                </div>
                                                            </div>

                                                        </div>
                                                        <div class="panel panel-body padding0">


                                                            <div class="row">
                                                                <div class="col-sm-12">
                                                                    <div style="height: 90px; overflow-y: scroll">
                                                                        <asp:Panel ID="pnlDispItms" runat="server" Visible="true">
                                                                            <asp:GridView ID="grdDspItms" AutoGenerateColumns="false" runat="server" CssClass="table table-hover table-striped" GridLines="None" ShowHeaderWhenEmpty="True" EmptyDataText="No data found..." AllowPaging="false" PageSize="10" OnPageIndexChanging="grdDspItms_PageIndexChanging">
                                                                                <Columns>
                                                                                    <asp:TemplateField>
                                                                                        <ItemTemplate>
                                                                                            <asp:LinkButton ID="grdLbtnDltItm" runat="server" CausesValidation="false" OnClientClick="return ConfirmDspItemDelete()" OnClick="grdLbtnDltItm_Click">
                                                            <span aria-hidden="true" class="glyphicon glyphicon-trash"></span>
                                                                                            </asp:LinkButton>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="Seq No" Visible="false">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="grdLblSeqNo" runat="server" Text='<%# Bind("Idd_seq") %>' Width="100px"></asp:Label>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="Item">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="grdLblItm" runat="server" Text='<%# Bind("Idd_itm_cd") %>' Width="100px"></asp:Label>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="Model">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="grdLblModel" runat="server" Text='<%# Bind("Idd_itm_model") %>' Width="100px"></asp:Label>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="Brand">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="grdLblBrnd" runat="server" Text='<%# Bind("Idd_itm_brand") %>' Width="100px"></asp:Label>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="Serial I">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="grdLblSerilaI" runat="server" Text='<%# Bind("Idd_ser_1") %>' Width="150px"></asp:Label>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="Serial ID" Visible="false">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="grdLblSerilaID" runat="server" Text='<%# Bind("Idd_ser_id") %>' Width="100px"></asp:Label>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="Cost" Visible="false">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="grdLblCost" runat="server" Text='<%#Bind("Idd_unit_cost","{0:#,##0.00##}")%>' Width="100px"></asp:Label>
                                                                                        </ItemTemplate>
                                                                                        <ItemStyle CssClass="gridHeaderAlignRight" />
                                                                                        <HeaderStyle CssClass="gridHeaderAlignRight" />
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="Status">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="grdLblStus" runat="server" Text='<%# Bind("Idd_stus_desc") %>' Width="100px"></asp:Label>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="Currr. Loc">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="grdLblStockAt" runat="server" Text='<%# Bind("Idd_cur_loc") %>' Width="100px"></asp:Label>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="Disposal Loc">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="grdLblDispLoc" runat="server" Text='<%# Bind("Idd_disp_loc") %>' Width="100px"></asp:Label>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="Qty">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="grdLblQty" runat="server" Text='<%#Bind("Idd_qty","{0:#,##0.00##}")%>' Width="30px"></asp:Label>
                                                                                        </ItemTemplate>
                                                                                        <ItemStyle CssClass="gridHeaderAlignRight" />
                                                                                        <HeaderStyle CssClass="gridHeaderAlignRight" />
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="Balance Qty">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="grdIdd_bqty" runat="server" Text='<%#Bind("Idd_bqty","{0:#,##0.00##}")%>' Width="30px"></asp:Label>
                                                                                        </ItemTemplate>
                                                                                        <ItemStyle CssClass="gridHeaderAlignRight" />
                                                                                        <HeaderStyle CssClass="gridHeaderAlignRight" />
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="Scanned">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="grdLblScanStus" runat="server" Text='<%#Eval("Idd_scan_stus").ToString()=="1" ? "Yes" : "No" %>' Width="50px"></asp:Label>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="Scan Qty">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="grdLblScnQty" runat="server" Text='<%#Bind("Idd_scan_qty","{0:#,##0.00##}")%>' Width="30px"></asp:Label>
                                                                                        </ItemTemplate>
                                                                                        <ItemStyle CssClass="gridHeaderAlignRight" />
                                                                                        <HeaderStyle CssClass="gridHeaderAlignRight" />
                                                                                    </asp:TemplateField>

                                                                                    <%--<asp:TemplateField HeaderText="Disposed">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="grdLblDispStus" runat="server" Text='<%#Eval("Idd_act").ToString()=="1" ? "No" : "Yes" %>' Width="30px"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>--%>
                                                                                </Columns>
                                                                            </asp:GridView>
                                                                        </asp:Panel>
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

                    <asp:UpdateProgress ID="UpdateProgress3" DisplayAfter="10"
                        runat="server" AssociatedUpdatePanelID="itmDetPnl">
                        <ProgressTemplate>
                            <div class="divWaiting">
                                <asp:Label ID="lblWaitNew3" runat="server"
                                    Text="Please wait... " />
                                <asp:Image ID="imgWaitNew3" runat="server"
                                    ImageAlign="Middle" ImageUrl="~/images/icons/ajax-loader.gif" />
                            </div>
                        </ProgressTemplate>
                    </asp:UpdateProgress>

                    <div class="col-sm-8">
                    </div>



                    <div class="col-sm-4">
                    </div>

                </div>
            </div>
        </div>
    </div>
    <%--  </ContentTemplate>
</asp:UpdatePanel>--%>


    <%--   <asp:UpdatePanel ID="pnlPayment" runat="server">
        <ContentTemplate>--%>
    <asp:Button ID="Button2" runat="server" Text="Button" Style="display: none;" />
    <asp:ModalPopupExtender ID="mpPayment" runat="server" Enabled="True" TargetControlID="Button2" Y="75"
        PopupControlID="pnlPaymentpopup" PopupDragHandleControlID="PopupHeader" Drag="true" BackgroundCssClass="modalBackground">
    </asp:ModalPopupExtender>
    <%--    </ContentTemplate>
    </asp:UpdatePanel>--%>

    <asp:UpdateProgress ID="UpdateProgress5" DisplayAfter="10"
        runat="server" AssociatedUpdatePanelID="RecUpdtPnl">
        <ProgressTemplate>
            <div class="divWaiting">
                <asp:Label ID="lblWaitNew5" runat="server"
                    Text="Please wait... " />
                <asp:Image ID="imgWaitNew5" runat="server"
                    ImageAlign="Middle" ImageUrl="~/images/icons/ajax-loader.gif" />
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>

    <asp:Panel runat="server" ID="pnlPaymentpopup">
        <div runat="server" id="Div1" class="panel panel-default height200 width1300">
            <div class="panel panel-default">
                <div id="PopupHeader" class="panel-heading">
                    <div class="col-sm-9" style="font-weight: bold">Payment Methods</div>

                    <asp:LinkButton ID="btnAClose" runat="server" OnClick="btnAClose_Click">
                                <span class="glyphicon glyphicon-remove" aria-hidden="true">Close</span>
                    </asp:LinkButton>
                </div>
                <div class="panel panel-body">
                    <div class="col-sm-12" runat="server">
                        <asp:UpdatePanel ID="RecUpdtPnl" runat="server">
                            <ContentTemplate>
                                <asp:Label ID="lblBackDateInfor" Text="" runat="server" Visible="false" />
                                <uc1:ucPaymodes ID="ucPayModes1" runat="server" />
                                <div class="row">
                                    <div class="col-sm-10"></div>
                                    <div class="col-sm-2">
                                        <asp:LinkButton ID="lbtnRecSave" runat="server" OnClick="lbtnRecSave_Click">
                                                    <span class="glyphicon glyphicon-saved" aria-hidden="true" style="font-weight:bold; font-size:large">Save</span>
                                        </asp:LinkButton>
                                    </div>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </div>
        </div>
    </asp:Panel>
    </ContentTemplate>
        </asp:UpdatePanel>

    <asp:UpdatePanel ID="UpdatePanel8" runat="server">
        <ContentTemplate>
            <asp:Button ID="btnSearchAd" runat="server" Text="Button" Style="display: none;" />
            <asp:ModalPopupExtender ID="mpSearchAdance" runat="server" Enabled="True" TargetControlID="btnSearchAd" Y="50"
                PopupControlID="pnlpopup" PopupDragHandleControlID="PopupHeader" Drag="true" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>
        </ContentTemplate>
    </asp:UpdatePanel>

    <asp:UpdatePanel ID="UpdatePanel9" runat="server">
        <ContentTemplate>
            <asp:Panel runat="server" ID="pnlpopup" Style="display: none;">
                <div runat="server" id="test" class="panel panel-default width800 height450">
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
                    <div class="panel panel-body">
                        <div class="col-sm-12" id="Div3" runat="server">
                            <div class="row">
                                <div class="col-sm-12 height5">
                                </div>
                            </div>
                            <div class="col-sm-12">
                                <div class="col-sm-5">
                                    <div class="col-sm-4 labelText1">
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
                                <div class="col-sm-7">
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
                                                <asp:LinkButton ID="lbtnSearch2" runat="server" OnClick="lbtnSearch2_Click">
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
                        </div>
                    </div>
                </div>
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>

    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
        <ContentTemplate>
            <asp:Button ID="btnMpSearch" runat="server" Text="Button" Style="display: none;" />
            <asp:ModalPopupExtender ID="mpSearch" runat="server" Enabled="True" TargetControlID="btnMpSearch" Y="50"
                PopupControlID="pnlMpSearch" PopupDragHandleControlID="PopupHeader" Drag="true" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>
        </ContentTemplate>
    </asp:UpdatePanel>

    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
        <ContentTemplate>
            <asp:Panel runat="server" ID="pnlMpSearch" Style="display: none;">
                <div runat="server" id="Div2" class="panel panel-default width950 height450">
                    <asp:Label ID="lblvalue2" runat="server" Text="Label" Visible="false"></asp:Label>
                    <div id="PopupHeader" class="panel-heading">
                        <asp:LinkButton ID="lbtnClose_mpSearch" CausesValidation="false" runat="server" OnClick="lbtnClose_mpSearch_Click">
                             <span class="glyphicon glyphicon-remove" aria-hidden="true">Close</span>
                        </asp:LinkButton>
                        <div class="col-sm-11">
                        </div>
                        <div class="col-sm-1">
                        </div>
                    </div>
                    <div class="panel panel-body">
                        <div class="col-sm-5" id="Div4" runat="server">
                            <div class="row">
                                <div class="col-sm-12 height5">
                                </div>
                            </div>
                            <div class="col-sm-12">
                                <div class="row">
                                    <div class="col-sm-2 labelText1">
                                        From
                                    </div>
                                    <div class="col-sm-4 paddingRight5 paddingLeft0">
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
                                    <div class="col-sm-4 paddingRight5 paddingLeft0">
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
                                        <asp:LinkButton ID="lbtnDateS" runat="server" OnClick="lbtnDateS_Click">
                                            <span class="glyphicon glyphicon-search" aria-hidden="true"  ></span>
                                        </asp:LinkButton>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-sm-7">
                            <div class="row">
                                <div class="col-sm-3 labelText1">
                                    Search by key
                                </div>
                                <asp:ScriptManagerProxy ID="ScriptManagerProxy2" runat="server"></asp:ScriptManagerProxy>
                                <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                    <ContentTemplate>
                                        <div class="col-sm-8 paddingRight5">
                                            <asp:DropDownList ID="ddlSearchbykey2" runat="server" class="form-control">
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
                                    <asp:TextBox ID="txtSearchbyword2" CausesValidation="false" placeholder="Search by word" class="form-control" AutoPostBack="true" runat="server" OnTextChanged="txtSearchbyword2_TextChanged"></asp:TextBox>
                                </div>
                                <asp:ScriptManagerProxy ID="ScriptManagerProxy4" runat="server"></asp:ScriptManagerProxy>
                                <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                                    <ContentTemplate>
                                        <div class="col-sm-1 paddingLeft0">
                                            <asp:LinkButton ID="jobNoSearchlbtn" runat="server" OnClick="jobNoSearchlbtn_Click">
                                            <span class="glyphicon glyphicon-search" aria-hidden="true"  ></span>
                                            </asp:LinkButton>
                                        </div>
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="txtSearchbyword2" />
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
                                <asp:ScriptManagerProxy ID="ScriptManagerProxy7" runat="server"></asp:ScriptManagerProxy>
                                <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                                    <ContentTemplate>
                                        <asp:GridView ID="grdResult2" CausesValidation="false" runat="server" CssClass="table table-hover table-striped" PageSize="10" PagerStyle-CssClass="cssPager" AllowPaging="True" OnSelectedIndexChanged="grdResult2_SelectedIndexChanged" OnPageIndexChanging="grdResult2_PageIndexChanging" GridLines="None" ShowHeaderWhenEmpty="True" EmptyDataText="No data found...">
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
                <div runat="server" id="Div5" class="panel panel-default height150 width525">
                    <asp:Label ID="Label14" runat="server" Text="Label" Visible="false"></asp:Label>
                    <div class="panel panel-default">
                        <div class="panel-heading">
                            <asp:LinkButton ID="btncClose" runat="server" CausesValidation="false">
                             <span class="glyphicon glyphicon-remove" aria-hidden="true">Close</span>
                            </asp:LinkButton>
                            <div class="col-sm-11">
                            </div>
                            <div class="col-sm-1">
                            </div>
                        </div>
                        <div class="panel panel-body">
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
                                                    <asp:DropDownList ID="ddlloadingbay" runat="server" TabIndex="2" CssClass="form-control"></asp:DropDownList>
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
                                                    <asp:Button ID="btnsend" runat="server" TabIndex="3" CssClass="btn-info form-control" Text="Send" OnClick="btnsend_Click" />
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

    <asp:UpdatePanel ID="UpdatePanel7" runat="server">
        <ContentTemplate>
            <asp:Button ID="exUplodPnlBtn" runat="server" Text="Button" Style="display: none;" />
            <asp:ModalPopupExtender ID="excelUpload" runat="server" Enabled="True" TargetControlID="exUplodPnlBtn"
                PopupControlID="pnlexcel" CancelControlID="btnClose_excel" PopupDragHandleControlID="PopupHeader" Drag="true" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>

        </ContentTemplate>

    </asp:UpdatePanel>
    <asp:UpdatePanel ID="UpdatePanel13" runat="server">

        <ContentTemplate>

            <asp:Panel runat="server" ID="pnlexcel">
                <div runat="server" id="dv" class="panel panel-default height150 width700 ">

                    <div class="panel panel-default">
                        <div class="panel-heading">
                            <asp:LinkButton ID="btnClose_excel" runat="server">
                             <span class="glyphicon glyphicon-remove" aria-hidden="true">Close</span>
                            </asp:LinkButton>
                            <%--<span>Commen Search</span>--%>
                            <div class="col-sm-11">
                                <strong>Excel Upload</strong>
                            </div>
                            <div class="col-sm-1">
                            </div>
                        </div>
                        <div class="panel panel-body">
                            <div class="col-sm-12 paddingLeft0 paddingRight0">
                                <div class="row">
                                    <div class="col-sm-12">
                                        <asp:UpdatePanel runat="server">
                                            <ContentTemplate>
                                                <asp:Label ID="Label3" runat="server" ForeColor="Red" Text="Label" Visible="false"></asp:Label>
                                                <asp:Label ID="lblsuccess2" runat="server" ForeColor="Green" Visible="false"></asp:Label>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-12 height10">
                                    </div>
                                </div>

                                <div class="row">
                                    <asp:Panel runat="server" ID="pnlupload" Visible="true">
                                        <div class="col-sm-12" id="Div6" runat="server">
                                            <div class="col-sm-8 paddingRight5">
                                                <div class="row">
                                                    <div class="col-sm-7 labelText1">
                                                        <asp:FileUpload ID="fileupexcelupload" CssClass="fileupexcelupload" runat="server" />
                                                    </div>
                                                    <div class="col-sm-2 paddingRight5">
                                                        <asp:Button ID="btnAsyncUpload" runat="server" Text="Async_Upload" Visible="false" />
                                                        <asp:UpdatePanel runat="server" ID="upUploadExcel">
                                                            <ContentTemplate>
                                                                <asp:Button ID="btnupload" class="btn btn-warning btn-xs" runat="server" Text="Upload" OnClick="btnupload_Click" />
                                                            </ContentTemplate>
                                                        </asp:UpdatePanel>
                                                    </div>

                                                </div>

                                            </div>
                                            <div class="row">
                                                <div class="col-sm-12 height20">
                                                </div>
                                            </div>



                                        </div>
                                    </asp:Panel>

                                    <%--<div class="row">--%>
                                    <%-- <div class="col-sm-12 ">
                                <div id="divUpcompleted" visible="false" runat="server" class="alert alert-info alert-success" role="alert">
                                    <div class="col-sm-9">
                                        <strong>Alert!</strong>
                                        <asp:Label ID="Label4" Text="Excel file upload completed. Do you want to process?" runat="server"></asp:Label>
                                    </div>
                                    <div class="col-sm-3">
                                        <asp:Button ID="btnprocess" class="btn btn-success btn-xs" runat="server" Text="Process" OnClick="btnprocess_Click" />
                                    </div>
                                </div>
                            </div>--%>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </asp:Panel>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnAsyncUpload"
                EventName="Click" />
            <asp:PostBackTrigger ControlID="btnupload" />
        </Triggers>
    </asp:UpdatePanel>

    <asp:UpdateProgress ID="UpdateProgress6" DisplayAfter="10"
        runat="server" AssociatedUpdatePanelID="UpdatePanel15">
        <ProgressTemplate>
            <div class="divWaiting">
                <asp:Label ID="lblWaitNew6" runat="server"
                    Text="Please wait... " />
                <asp:Image ID="imgWaitNew6" runat="server"
                    ImageAlign="Middle" ImageUrl="~/images/icons/ajax-loader.gif" />
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>

    <asp:UpdatePanel ID="UpdatePanel14" runat="server">
        <ContentTemplate>
            <asp:Button ID="printPnlBtn" runat="server" Text="Button" Style="display: none;" />
            <asp:ModalPopupExtender ID="mpPrintPanel" runat="server" Enabled="True" TargetControlID="printPnlBtn" Y="100"
                PopupControlID="printPnl" CancelControlID="btnClose_prntPnl" PopupDragHandleControlID="PopupHeader" Drag="true" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>

        </ContentTemplate>

    </asp:UpdatePanel>

    <asp:UpdatePanel ID="UpdatePanel15" runat="server">

        <ContentTemplate>

            <asp:Panel runat="server" ID="printPnl">
                <div runat="server" id="Div7" class="panel panel-default height250 width700 ">

                    <div class="panel panel-default">
                        <div class="panel-heading">
                            <div class="col-sm-9" style="font-weight: bold">Disposal Documents</div>
                            <asp:LinkButton ID="btnClose_prntPnl" runat="server">
                             <span class="glyphicon glyphicon-remove" aria-hidden="true">Close</span>
                            </asp:LinkButton>
                        </div>
                        <div class="panel panel-body">
                            <div class="col-sm-12 padding0">
                                <asp:GridView ID="grdPrintDoc" AutoGenerateColumns="False" runat="server" CssClass="table table-hover table-striped" GridLines="None" ShowHeaderWhenEmpty="True" EmptyDataText="No data found..." AllowPaging="true" PageSize="10" OnPageIndexChanging="grdPrintDoc_PageIndexChanging">
                                    <Columns>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:LinkButton ID="grdLbtnprint" runat="server" CausesValidation="false" OnClientClick="return confPrint()" OnClick="grdLbtnprint_Click">
                                                            <span aria-hidden="true" class="glyphicon glyphicon-print"></span>
                                                </asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Doc No">
                                            <ItemTemplate>
                                                <asp:Label ID="lblDocNo" runat="server" Text='<%# Bind("Ith_doc_no") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Date">
                                            <ItemTemplate>
                                                <asp:Label ID="lblDocDt" runat="server" Text='<%# Convert.ToDateTime(Eval("Ith_doc_date")).ToString("dd/MMM/yyyy") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Job No">
                                            <ItemTemplate>
                                                <asp:Label ID="lbljobNo" runat="server" Text='<%# Bind("Ith_job_no") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Ref">
                                            <ItemTemplate>
                                                <asp:Label ID="lblManRef" runat="server" Text='<%# Bind("Ith_manual_ref") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Remark">
                                            <ItemTemplate>
                                                <asp:Label ID="lblRemark" runat="server" Text='<%# Bind("Ith_remarks") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                </div>
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>


    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <asp:Button ID="Button10" runat="server" Text="Button" Style="display: none;" />
            <asp:ModalPopupExtender ID="MPMULTJobs" runat="server" Enabled="True" TargetControlID="Button10"
                PopupControlID="PDACurrentPnl" PopupDragHandleControlID="PopupHeader" CancelControlID="LinkButton3" Drag="true" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdateProgress ID="UpdateProgress7" DisplayAfter="10" runat="server" AssociatedUpdatePanelID="upPnlJobDataAdd">
        <ProgressTemplate>
            <div class="divWaiting">
                <asp:Label ID="lblWadit10" runat="server"
                    Text="Please wait... " />
                <asp:Image ID="imgWadit10" runat="server"
                    ImageAlign="Middle" ImageUrl="~/images/icons/ajax-loader.gif" />
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <asp:UpdatePanel runat="server" ID="upPnlJobDataAdd">
        <ContentTemplate>

            <asp:Panel runat="server" ID="PDACurrentPnl">
                <div runat="server" id="Div11" class="panel panel-default height150 width525">
                    <asp:Label ID="Label9" runat="server" Text="Label" Visible="false"></asp:Label>
                    <div class="panel panel-default">
                        <div class="panel-heading">
                            <asp:LinkButton ID="LinkButton3" runat="server" CausesValidation="false">
                             <span class="glyphicon glyphicon-remove" aria-hidden="true">Close</span>
                            </asp:LinkButton>
                            <div class="col-sm-11">
                            </div>
                            <div class="col-sm-1">
                            </div>
                        </div>
                        <div class="panel panel-body">
                            <div class="col-sm-12 paddingLeft0 paddingRight0">

                                <div class="row">

                                    <div class="col-sm-12">

                                        <div class="row">
                                            <div class="col-sm-12 height10">
                                                <div class="col-sm-8">
                                                </div>
                                                <div class="col-sm-2 ">
                                                    <asp:Button ID="btngetserial" runat="server" Text="Get Serial" Width="80px" CausesValidation="false" class="btn btn-primary btn-xs" OnClick="Button2_Click" />
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-sm-12 height10">
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-sm-12">
                                                <div class="panel panel-default">
                                                    <div class="panel panel-body" style="height: 175px; overflow: auto;">
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
    <asp:UpdateProgress ID="UpdateProgress8" DisplayAfter="10"
        runat="server" AssociatedUpdatePanelID="upUploadExcel">
        <ProgressTemplate>
            <div class="divWaiting">
                <asp:Label ID="lbupUploadExcellWaitNew" runat="server"
                    Text="Please wait... " />
                <asp:Image ID="imgupUploadExcelWaitNew" runat="server"
                    ImageAlign="Middle" ImageUrl="~/images/icons/ajax-loader.gif" />
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <asp:UpdatePanel ID="UpdatePanel32" runat="server">
        <ContentTemplate>
            <asp:Button ID="Button1" runat="server" Text="Button" Style="display: none;" />
            <asp:ModalPopupExtender ID="SbuPopup" runat="server" Enabled="True" TargetControlID="Button1"
                PopupControlID="pnlSBU" PopupDragHandleControlID="PopupHeader" Drag="true" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>
        </ContentTemplate>
    </asp:UpdatePanel>

    <asp:Panel ID="pnlSBU" runat="server" align="center">
        <asp:Label ID="Label4" runat="server" Text="Label" Visible="false"></asp:Label>
        <asp:ScriptManagerProxy ID="ScriptManagerProxy17" runat="server"></asp:ScriptManagerProxy>
        <asp:UpdatePanel ID="UpdatePanel33" runat="server">
            <ContentTemplate>
                <div class="panel panel-danger">
                    <div class="panel-heading">
                        <span>Alert</span>
                    </div>
                    <div class="panel-body">
                        <div class="row">
                            <div class="col-sm-12">
                                <asp:Label ID="Label5" runat="server"></asp:Label>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-12">
                                <asp:Label ID="lblbinMssg" runat="server"></asp:Label>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-12">
                                <asp:Label ID="lblbinMssg1" runat="server"></asp:Label>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-12">
                                <asp:Label ID="Label1" runat="server"></asp:Label>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-12">
                                <asp:Label ID="Label10" runat="server"></asp:Label>
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
</asp:Content>
