<%@ Page Title="" Language="C#" MasterPageFile="~/View/AdminSite.Master" AutoEventWireup="true" CodeBehind="DispatchPlan.aspx.cs" Inherits="FastForward.SCMWeb.View.Transaction.Inventory.DispatchPlan" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">

    <script src="<%=Request.ApplicationPath%>Js/jquery.min.js"></script>
    <link href="<%=Request.ApplicationPath%>Css/jquery-ui.theme.min.css" rel="stylesheet" />
    <link href="<%=Request.ApplicationPath%>Css/jquery-ui.min.css" rel="stylesheet" />

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

        .divWaiting1 {
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

        .marginleft {
            margin-left: 4px;
        }
    </style>

    <script type="text/javascript">
        function ConfSend() {
            var result = confirm("Are you sure do you want to send ?");
            if (result) {
                return true;
            }
            else {
                return false;
            }
        }
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

        function confClear() {
            var res = confirm("Do you want to clear?");
            if (res) {
                document.getElementById('<%=hdfClear.ClientID %>').value = "1";
            }
            else {
                document.getElementById('<%=hdfClear.ClientID %>').value = "0";
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

        function confSave() {
            var res = confirm("Do you want to save?");
            if (res) {
                document.getElementById('<%=hdfSave.ClientID %>').value = "1";
            }
            else {
                document.getElementById('<%=hdfSave.ClientID %>').value = "0";
            }
        };

        function confDelApproved() {
            var res = confirm("Do you want to delete?");
            if (res) {
                document.getElementById('<%=hdfDelAppr.ClientID %>').value = "1";
            }
            else {
                document.getElementById('<%=hdfDelAppr.ClientID %>').value = "0";
            }
        };

        function confDelApprovedAuto() {
            var res = confirm("Do you want to delete?");
            if (res) {
                document.getElementById('<%=hdfAutoAppDeLitem.ClientID %>').value = "1";
            }
            else {
                document.getElementById('<%=hdfAutoAppDeLitem.ClientID %>').value = "0";
            }
        };

        function confCancel() {

            if (BodyContent_dgvApprovedItems_chkHeaderAllApp.checked) {
                var res = confirm("Do you want to cancel all?");
            } else {
                var res = confirm("Do you want to cancel ?");
            }

            if (res) {
                document.getElementById('<%=hdfCancel.ClientID %>').value = "1";
            }
            else {
                document.getElementById('<%=hdfCancel.ClientID %>').value = "0";
            }
        };
        function ConfirmApproveSave() {
            var res = confirm("Do you want to approve all?");
            if (res) {
                document.getElementById('<%=hdfApproveAll.ClientID %>').value = "1";
            }
            else {
                document.getElementById('<%=hdfApproveAll.ClientID %>').value = "0";
            }
        };

        function ValidateDecimal() {
            var rgexp = new RegExp("^([0-9]*\.?[0-9]{1,2})$");
            //var rgexp = new RegExp("\d+(\.\d{1,2})?");
            var input = document.getElementById('<%=txtAmount.ClientID %>').value;

            if (input.match(rgexp)) {
                document.getElementById('<%=hdfISValiedNumber.ClientID %>').value = "1";
            }
            else {
                document.getElementById('<%=hdfISValiedNumber.ClientID %>').value = "0";
                showStickyWarningToast("Please enter valid Qty");
            }
        };

        function setScrollPosition1(scrollValue) {

            $('#<%=hfScrollPosition1.ClientID%>').val(scrollValue);
        }
        function maintainScrollPosition1() {
            $('.dvScroll').scrollTop($('#<%=hfScrollPosition1.ClientID%>').val());
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
        };

        function pageLoad(sender, args) {
            $("#<%=txtDate.ClientID %>").datetimepicker({ dateFormat: "dd/M/yy", timeFormat: "hh:mm tt" });
            $("#<%=txtToDate.ClientID %>").datetimepicker({ dateFormat: "dd/M/yy", timeFormat: "hh:mm tt" });
        };
    </script>

    <script>
        function setScrollPosition(scrollValue) {

            $('#<%=hfScrollPosition.ClientID%>').val(scrollValue);
        }
        function maintainScrollPosition() {
            $('.divScroll').scrollTop($('#<%=hfScrollPosition.ClientID%>').val());
        }
        function setScrollPosition2(scrollValue) {

            $('#<%=hfScrollPosition2.ClientID%>').val(scrollValue);
        }
        function maintainScrollPosition2() {
            $('.divStock').scrollTop($('#<%=hfScrollPosition2.ClientID%>').val());
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" runat="server">
    <asp:UpdateProgress ID="UpdateProgress1" DisplayAfter="10"
        runat="server" AssociatedUpdatePanelID="upsearchFilter">
        <ProgressTemplate>
            <div class="divWaiting">
                <asp:Label ID="lblWait" runat="server"
                    Text="Please wait... " />
                <asp:Image ID="imgWait" runat="server"
                    ImageAlign="Middle" ImageUrl="~/images/icons/ajax-loader.gif" />
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>

    <asp:UpdateProgress ID="UpdateProgress2" DisplayAfter="40"
        runat="server" AssociatedUpdatePanelID="upHeaderGrid">
        <ProgressTemplate>
            <div class="divWaiting">
                <asp:Label ID="lblWait1" runat="server"
                    Text="Please wait... " />
                <asp:Image ID="imgWait1" runat="server"
                    ImageAlign="Middle" ImageUrl="~/images/icons/ajax-loader.gif" />
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
     <asp:UpdateProgress ID="UpdateProgress8" DisplayAfter="40"
        runat="server" AssociatedUpdatePanelID="addUpPnl">
        <ProgressTemplate>
            <div class="divWaiting">
                <asp:Label ID="lblWait123" runat="server"
                    Text="Please wait... " />
                <asp:Image ID="imgWait123" runat="server"
                    ImageAlign="Middle" ImageUrl="~/images/icons/ajax-loader.gif" />
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <asp:UpdateProgress ID="UpdateProgress5" DisplayAfter="40"
        runat="server" AssociatedUpdatePanelID="pnlButtn">
        <ProgressTemplate>
            <div class="divWaiting">
                <asp:Label ID="lblWait12" runat="server"
                    Text="Please wait... " />
                <asp:Image ID="imgWait12" runat="server"
                    ImageAlign="Middle" ImageUrl="~/images/icons/ajax-loader.gif" />
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>

    <asp:UpdateProgress ID="UpdateProgress3" DisplayAfter="40"
        runat="server" AssociatedUpdatePanelID="upRequestItems">
        <ProgressTemplate>
            <div class="divWaiting">
                <asp:Label ID="lblWait2" runat="server"
                    Text="Please wait... " />
                <asp:Image ID="imgWait2" runat="server"
                    ImageAlign="Middle" ImageUrl="~/images/icons/ajax-loader.gif" />
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>

    <asp:UpdateProgress ID="UpdateProgress4" DisplayAfter="40"
        runat="server" AssociatedUpdatePanelID="upAutoAdd">
        <ProgressTemplate>
            <div class="divWaiting">
                <asp:Label ID="lblWait3" runat="server"
                    Text="Please wait... " />
                <asp:Image ID="imgWait3" runat="server"
                    ImageAlign="Middle" ImageUrl="~/images/icons/ajax-loader.gif" />
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <asp:UpdateProgress ID="UpdateProgress7" DisplayAfter="10"
        runat="server" AssociatedUpdatePanelID="upSendToPda">
        <ProgressTemplate>
            <div class="divWaiting1">
                <asp:Label ID="lblWait31" runat="server"
                    Text="Please wait... " />
                <asp:Image ID="imgWait31" runat="server"
                    ImageAlign="Middle" ImageUrl="~/images/icons/ajax-loader.gif" />
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <%--   <asp:UpdateProgress ID="UpdateProgress5" DisplayAfter="40"
        runat="server" AssociatedUpdatePanelID="upHeaderGrid">
        <ProgressTemplate>
            <div class="divWaiting">
                <asp:Label ID="lblWait1" runat="server"
                    Text="Please wait... " />
                <asp:Image ID="imgWait1" runat="server"
                    ImageAlign="Middle" ImageUrl="~/images/icons/ajax-loader.gif" />
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>--%>
    <div class="panel panel-default marginLeftRight5">
        <div class="panel-body">
            <asp:ScriptManagerProxy ID="ScriptManagerProxy1" runat="server"></asp:ScriptManagerProxy>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <asp:HiddenField ID="hdfClear" runat="server" />
                    <asp:HiddenField ID="hdfSave" runat="server" />
                    <asp:HiddenField ID="hdfSerialDelete" runat="server" />
                    <asp:HiddenField ID="hdfDelAppr" runat="server" />
                    <asp:HiddenField ID="hdfISValiedNumber" runat="server" />
                    <asp:HiddenField ID="hdfApproveAll" runat="server" />
                    <asp:HiddenField ID="hdfAutoAppDeLitem" runat="server" />
                    <asp:HiddenField ID="txtpdasend" runat="server" />
                    <asp:HiddenField ID="hdfCancel" runat="server" />
                    <div class="row">
                        <div class="col-sm-12">
                            <asp:UpdatePanel runat="server" ID="upSendToPda">
                                <ContentTemplate>
                                    <div class="col-sm-2">
                                    </div>
                                    <div class="col-sm-4 padding0">
                                        <div class="col-sm-12 ">
                                            <div class="col-sm-4">
                                                <div class="col-sm-1 labelText1 padding0">
                                                    <asp:CheckBox runat="server" ID="chkpda" AutoPostBack="true" Enabled="false" OnCheckedChanged="chkpda_CheckedChanged" />
                                                </div>
                                                <div class="col-sm-10 padding3 labelText1">
                                                    Send to PDA
                                                </div>
                                            </div>
                                            <div class="col-sm-5">
                                                <asp:LinkButton ID="lbtnAllToPda" runat="server" TabIndex="6" CausesValidation="false" OnClick="lbtnAllToPda_Click">
                                                        Send All To PDA
                                                </asp:LinkButton>
                                            </div>
                                             <div class="col-sm-1 text-center">
                                                <asp:Button Text="Send to scan" ID="Button4" runat="server" OnClick="btnSentScan_Click" />
                                               
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-6  buttonRow">
                                        <div class="col-sm-2 labelText1">
                                        </div>
                                        <div class="col-sm-2 paddingRight0">
                                            <asp:LinkButton ID="lbnPrint" CausesValidation="false" runat="server" CssClass="floatRight" OnClick="lbnPrint_Click">
                                        <span class="glyphicon glyphicon-print" aria-hidden="true"></span>Print
                                            </asp:LinkButton>
                                        </div>

                                        <div class="col-sm-2 paddingRight0">
                                            <asp:LinkButton ID="btnSave" CausesValidation="false" runat="server" CssClass="floatRight" OnClientClick="return confSave()" OnClick="btnSave_Click">
                                        <span class="glyphicon glyphicon-saved" aria-hidden="true"></span>Approve
                                            </asp:LinkButton>
                                        </div>
                                        <div class="col-sm-2">
                                            <asp:LinkButton ID="btnClear" runat="server" CssClass="floatRight" OnClientClick="return confClear()" OnClick="btnClear_Click">
                                        <span class="glyphicon glyphicon-refresh" aria-hidden="true"></span>Clear
                                            </asp:LinkButton>
                                        </div>
                                        <div class="col-sm-2">
                                            <asp:LinkButton ID="lbtncancel" runat="server" CssClass="floatRight" OnClientClick="return confCancel()" OnClick="lbtncancel_Click">
                                        <span class="glyphicon glyphicon-remove" aria-hidden="true"></span>Cancel
                                            </asp:LinkButton>
                                        </div>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
            <asp:ScriptManagerProxy ID="ScriptManagerProxy8" runat="server"></asp:ScriptManagerProxy>
            <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                <ContentTemplate>
                    <div class="row">
                        <div class="col-sm-12">
                            <div class="panel panel-default">
                                <div class="panel-heading paddingtopbottom0">
                                    Dispatch Plan
                                </div>
                                <div class="panel-body">
                                    <asp:UpdatePanel ID="upsearchFilter" runat="server">
                                        <ContentTemplate>
                                            <div class="col-sm-8">
                                                <div class="row">
                                                    <div class="col-sm-4 paddingRight0">
                                                        <div class="col-sm-3 labelText1 paddingLeft0">
                                                            Route
                                                        </div>
                                                        <div class="col-sm-7 paddingRight5 paddingLeft0">
                                                            <div class="col-sm-1 paddingRight0 paddingLeft0">
                                                                <asp:CheckBox ID="chkRouteAll" Text="" runat="server" OnCheckedChanged="chkRouteAll_CheckedChanged" AutoPostBack="true" />
                                                            </div>
                                                            <div class="col-sm-11 paddingRight5 ">
                                                                <asp:TextBox runat="server" AutoPostBack="true" ID="txtRoute" OnTextChanged="txtRoute_TextChanged" CausesValidation="false" CssClass="form-control"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                        <div class="col-sm-1 paddingLeft0">
                                                            <asp:LinkButton ID="btnRoute" runat="server" CausesValidation="false" OnClick="btnRoute_Click">
                                                            <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                            </asp:LinkButton>
                                                        </div>
                                                    </div>
                                                    <div class="col-sm-4 paddingRight0 paddingLeft0">
                                                        <div class="col-sm-4 labelText1 paddingLeft0">
                                                            Main Category
                                                        </div>
                                                        <div class="col-sm-7 paddingRight5 paddingLeft0">
                                                            <div class="col-sm-1 paddingRight0 paddingLeft0">
                                                                <asp:CheckBox ID="chkMainCategoryAll" AutoPostBack="true" Text="" runat="server" OnCheckedChanged="chkMainCategoryAll_CheckedChanged" />
                                                            </div>
                                                            <div class="col-sm-11 paddingRight5 ">
                                                                <asp:TextBox runat="server" AutoPostBack="true" ID="txtMainCategory" OnTextChanged="txtMainCategory_TextChanged" CausesValidation="false" CssClass="form-control"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                        <div class="col-sm-1 paddingLeft0">
                                                            <asp:LinkButton ID="btnMainCategory" runat="server" CausesValidation="false" OnClick="btnMainCategory_Click">
                                                            <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                            </asp:LinkButton>
                                                        </div>
                                                    </div>
                                                    <div class="col-sm-4 paddingRight0">
                                                        <div class="col-sm-3 labelText1 paddingLeft0">
                                                            Item Code
                                                        </div>
                                                        <div class="col-sm-7 paddingRight5 paddingLeft0">
                                                            <div class="col-sm-1 paddingRight0 paddingLeft0">
                                                                <asp:CheckBox ID="chkItemCode" AutoPostBack="true" Text="" runat="server" OnCheckedChanged="chkItemCode_CheckedChanged" />
                                                            </div>
                                                            <div class="col-sm-11 paddingRight5 ">
                                                                <asp:TextBox runat="server" AutoPostBack="true" ID="txtItemCode" OnTextChanged="txtItemCode_TextChanged" CausesValidation="false" CssClass="form-control"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                        <div class="col-sm-1 paddingLeft0">
                                                            <asp:LinkButton ID="btnItemCode" runat="server" CausesValidation="false" OnClick="btnItemCode_Click">
                                                            <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                            </asp:LinkButton>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-sm-4 paddingRight0">
                                                        <div class="col-sm-3 labelText1 paddingLeft0">
                                                            Location
                                                        </div>
                                                        <div class="col-sm-7 paddingRight5 paddingLeft0">
                                                            <div class="col-sm-1 paddingRight0 paddingLeft0">
                                                            </div>
                                                            <div class="col-sm-11 paddingRight5 ">
                                                                <asp:TextBox runat="server" AutoPostBack="true" ID="txtLocation" OnTextChanged="txtLocation_TextChanged" CausesValidation="false" CssClass="form-control"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                        <div class="col-sm-1 paddingLeft0">
                                                            <asp:LinkButton ID="btnLocation" runat="server" CausesValidation="false" OnClick="btnLocation_Click">
                                                            <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                            </asp:LinkButton>
                                                        </div>
                                                    </div>
                                                    <div class="col-sm-4 paddingRight0 paddingLeft0">
                                                        <div class="col-sm-4 labelText1 paddingLeft0">
                                                            Sub Category
                                                        </div>
                                                        <div class="col-sm-7 paddingRight5 paddingLeft0">
                                                            <div class="col-sm-1 paddingRight0 paddingLeft0">
                                                                <asp:CheckBox ID="chkSubCategoryAll" AutoPostBack="true" Text="" runat="server" OnCheckedChanged="chkSubCategoryAll_CheckedChanged" />
                                                            </div>
                                                            <div class="col-sm-11 paddingRight5 ">
                                                                <asp:TextBox runat="server" AutoPostBack="true" ID="txtSubCategory" OnTextChanged="txtSubCategory_TextChanged" CausesValidation="false" CssClass="form-control"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                        <div class="col-sm-1 paddingLeft0">
                                                            <asp:LinkButton ID="btnSubCategory" runat="server" CausesValidation="false" OnClick="btnSubCategory_Click">
                                                            <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                            </asp:LinkButton>
                                                        </div>
                                                    </div>
                                                    <div class="col-sm-4 paddingRight0">
                                                        <div class="col-sm-3 labelText1 paddingLeft0">
                                                            Model
                                                        </div>
                                                        <div class="col-sm-7 paddingRight5 paddingLeft0">
                                                            <div class="col-sm-1 paddingRight0 paddingLeft0">
                                                                <asp:CheckBox ID="chkModelAll" AutoPostBack="true" Text="" runat="server" OnCheckedChanged="chkModelAll_CheckedChanged" />
                                                            </div>
                                                            <div class="col-sm-11 paddingRight5 ">
                                                                <asp:TextBox runat="server" AutoPostBack="true" ID="txtModel" OnTextChanged="txtModel_TextChanged" CausesValidation="false" CssClass="form-control"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                        <div class="col-sm-1 paddingLeft0">
                                                            <asp:LinkButton ID="btnModel" runat="server" CausesValidation="false" OnClick="btnModel_Click">
                                                            <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                            </asp:LinkButton>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-sm-4 padding0">
                                                <div class="col-sm-11">
                                                    <div class="row">
                                                        <div class="col-sm-3 labelText1 paddingRight5 paddingLeft0">
                                                            <div class="col-sm-1 paddingRight0 paddingLeft0">
                                                                <asp:CheckBox ID="chkAppDoc" AutoPostBack="true" Text="" runat="server" OnCheckedChanged="chkAppDoc_CheckedChanged" />
                                                            </div>
                                                            <div class="col-sm-11  paddingRight5 ">
                                                                <asp:Label runat="server" Text="Approved Doc"></asp:Label>
                                                            </div>
                                                        </div>
                                                        <div class="col-sm-2 labelText1 paddingLeft0">
                                                            Req. Type
                                                        </div>
                                                        <div class="col-sm-7 paddingRight5 paddingLeft0">
                                                            <asp:DropDownList runat="server" ID="ddlReqType" CssClass="form-control"></asp:DropDownList>
                                                        </div>

                                                    </div>
                                                    <div class="row">
                                                        <div class="col-sm-6 padding0">
                                                            <div class="col-sm-4 labelText1 padding0">
                                                                From Date
                                                            </div>
                                                            <div class="col-sm-8 padding0">
                                                                <asp:TextBox ID="txtDate" CausesValidation="false" runat="server" class="form-control"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                        <div class="col-sm-6 paddingRight0">
                                                            <div class="col-sm-4 labelText1 padding0">
                                                                To Date
                                                            </div>
                                                            <div class="col-sm-8 padding0">
                                                                <asp:TextBox ID="txtToDate" CausesValidation="false" runat="server" class="form-control"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-sm-1">
                                                    <asp:LinkButton ID="btnSearch" runat="server" CausesValidation="false" OnClick="btnSearch_Click">
                                                    <span class="glyphicon glyphicon-search fontsize20 right5" aria-hidden="true"></span>
                                                    </asp:LinkButton>
                                                </div>
                                            </div>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                            </div>
                            <div class="panel panel-default">
                                <div class="panel-body">
                                    <div class="col-sm-6 paddingLeft0" style="height: 200px">
                                        <div class="panel panel-default" style="height: 200px">
                                            <div class="panel-heading height20 paddingtopbottom0">
                                                <div class="col-sm-2">
                                                    Pen List
                                                </div>
                                                <div class="col-sm-3">
                                                    <div class="col-sm-5 labelText1 paddingLeft0">
                                                        Loc
                                                    </div>
                                                    <div class="col-sm-6 paddingRight5 paddingLeft0">
                                                        <asp:TextBox ID="txtinloc" AutoPostBack="true" runat="server" class="form-control" OnTextChanged="txtinloc_TextChanged"></asp:TextBox>
                                                    </div>
                                                    <div class="col-sm-1 paddingRight5 paddingLeft0">
                                                        <asp:LinkButton ID="lbtnsearchpend" runat="server" OnClick="lbtnsearchpend_Click">
                                                    <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                        </asp:LinkButton>
                                                    </div>
                                                </div>
                                                <div class="col-sm-6">
                                                    <div class="col-sm-5 labelText1 paddingLeft0">
                                                        Request Number
                                                    </div>
                                                    <div class="col-sm-6 paddingRight5 paddingLeft0">
                                                        <asp:TextBox ID="txtRequestNumber" AutoPostBack="true" runat="server" class="form-control" OnTextChanged="txtRequestNumber_TextChanged"></asp:TextBox>
                                                    </div>
                                                    <div class="col-sm-1 paddingRight5 paddingLeft0">
                                                        <asp:LinkButton ID="btnSearchRequest" runat="server" OnClick="btnSearchRequest_Click">
                                                    <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                        </asp:LinkButton>
                                                    </div>
                                                </div>
                                                <div class="col-sm-1">
                                                    <asp:UpdatePanel runat="server" ID="pnlButtn">
                                                        <ContentTemplate>
                                                            <asp:LinkButton ID="btnResetHeaders" runat="server" CausesValidation="false" OnClick="btnResetHeaders_Click" Style="float: right">
                                                    <span class="glyphicon glyphicon-collapse-up" aria-hidden="true"></span>
                                                            </asp:LinkButton>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </div>
                                            </div>
                                            <div class="panel-body">
                                                <asp:ScriptManagerProxy ID="ScriptManagerProxy2" runat="server"></asp:ScriptManagerProxy>
                                                <asp:UpdatePanel ID="upHeaderGrid" runat="server">
                                                    <ContentTemplate>
                                                        <div class="col-sm-12 paddingLeft0 paddingRight0">
                                                            <div class="dvScroll" style="height: 172px; overflow: scroll;" onscroll="setScrollPosition1(this.scrollTop);">
                                                                <%-- <div class="panel-body panelscoll1" style="height: 150px">--%>
                                                                <asp:HiddenField ID="hfScrollPosition" Value="0" runat="server" />
                                                                <asp:GridView ID="dgvRequestHeader" CssClass="table table-hover table-striped" runat="server" GridLines="None" ShowHeaderWhenEmpty="True" PagerStyle-CssClass="cssPager"
                                                                    EmptyDataText="No data found..." AutoGenerateColumns="False" OnPageIndexChanging="dgvRequestHeader_PageIndexChanging" OnRowCommand="dgvRequestHeader_RowCommand" OnRowDataBound="dgvRequestHeader_RowDataBound" OnSorting="dgvRequestHeader_Sorting" AllowSorting="true">
                                                                    <Columns>
                                                                        <asp:TemplateField HeaderText="">
                                                                            <HeaderTemplate>
                                                                                <asp:CheckBox ID="chkHeaderAll" runat="server" OnCheckedChanged="chkHeaderAll_CheckedChanged" AutoPostBack="true" />
                                                                            </HeaderTemplate>
                                                                            <ItemTemplate>
                                                                                <asp:CheckBox ID="chkHeader" Text=" " runat="server" OnCheckedChanged="chkHeader_CheckedChanged" AutoPostBack="true" />
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="">
                                                                            <ItemTemplate>
                                                                                <asp:LinkButton ID="btnItemChange" CausesValidation="false" runat="server" CssClass="floatRight" OnClick="btnItemChange_Click">
                                                                                    <span class="glyphicon glyphicon-edit" aria-hidden="true"></span>
                                                                                </asp:LinkButton>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Request Doc No" SortExpression="ITR_REQ_NO">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblITR_REQ_NO" runat="server" Text='<%# Bind("ITR_REQ_NO") %>' Width="140px" Visible="false"></asp:Label>
                                                                                <asp:LinkButton Text='<%# Bind("ITR_REQ_NO") %>' ID="SelectRequest" runat="server" OnClick="SelectRequest_Click" Width="130px" />
                                                                                <asp:LinkButton Text="Reset" ID="ResetRequest" runat="server" OnClick="ResetRequest_Click" Width="50px" />
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Ref No" SortExpression="ITR_REF">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblITR_REF" runat="server" Text='<%# Bind("ITR_REF") %>' Width="110px"></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Dispatch From" SortExpression="ITR_ISSUE_FROM">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblITR_ISSUE_FROM" runat="server" Text='<%# Bind("ITR_ISSUE_FROM") %>' Width="60px"></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Deliver To" SortExpression="ITR_REC_TO">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblITR_REC_TO" runat="server" Text='<%# Bind("ITR_REC_TO", "{0:dd/MMM/yyyy}") %>' Width="70px"></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Seq no" Visible="false">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblITR_SEQ_NO" runat="server" Text='<%# Bind("ITR_SEQ_NO") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Requested Date" Visible="false">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblitr_dt" runat="server" Text='<%# Bind("itr_dt", "{0:dd/MMM/yyyy}") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Required Date" SortExpression="ITR_EXP_DT">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblITR_EXP_DT" runat="server" Text='<%# Bind("ITR_EXP_DT", "{0:dd/MMM/yyyy}") %>' Width="70px"></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Create Date" SortExpression="itr_cre_dt">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblitr_cre_dt" runat="server"  Text='<%# Bind("itr_cre_dt", "{0:dd/MMM/yyyy hh:mm tt}") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                         <asp:TemplateField HeaderText="Approve Date" SortExpression="Itr_mod_dt">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblitr_mod_dt" runat="server"  Text='<%# Bind("Itr_mod_dt", "{0:dd/MMM/yyyy hh:mm tt}") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Loc" Visible="false">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblITR_LOC" runat="server" Text='<%# Bind("ITR_LOC") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Type" Visible="false">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblItr_tp" runat="server" Text='<%# Bind("Itr_tp") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Customer" Visible="false">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="LblItr_bus_code" runat="server" Text='<%# Bind("Itr_bus_code") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Customer Name" Visible="false">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblItr_anal1" runat="server" Text='<%# Bind("Itr_anal1") %>' Width="150px"></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="direction" Visible="false">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblitr_direct" runat="server" Text='<%# Bind("itr_direct") %>' Width="150px"></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Dealer Name " SortExpression="ITR_REC_TO">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblITR_REC_TO_DESC" runat="server" Text='<%# Bind("Itr_anal1") %>' Width="150px" ></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                    </Columns>
                                                                    <PagerStyle CssClass="cssPager" />
                                                                </asp:GridView>
                                                            </div>
                                                        </div>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-6 paddingLeft0" style="height: 200px">
                                        <div class="panel panel-default">
                                            <div class="panel-heading height20 paddingtopbottom0">
                                                <div class="col-sm-3">Current Stock Details</div>
                                                <div class="col-sm-9">
                                                    <div class="col-sm-2 padding0">Search By</div>
                                                    <div class="col-sm-8 padding0">
                                                         <div class="col-sm-5 padding0">
                                                             <div class="col-sm-2 padding0">
                                                                 <asp:RadioButton ID="radCreateDate" Text="" GroupName="dateCr" runat="server" />
                                                             </div>
                                                             <div class="col-sm-10 padding3">
                                                                 <asp:Label Text="Create Date" runat="server" />
                                                             </div>
                                                         </div>
                                                        <div class="col-sm-6">
                                                            <div class="col-sm-2 padding0">
                                                                 <asp:RadioButton ID="radReqDate" Checked="true" Text="" GroupName="dateCr" runat="server" />
                                                             </div>
                                                             <div class="col-sm-10 padding3">
                                                                 <asp:Label Text="Required Date" runat="server" />
                                                             </div>
                                                         </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="panel-body">
                                                <asp:UpdatePanel runat="server">
                                                    <ContentTemplate>
                                                        <div class="col-sm-12 paddingLeft0 paddingRight0">
                                                            <div class="divStock panel-body panelscoll1" style="height: 150px" onscroll="setScrollPosition2(this.scrollTop);">
                                                                <asp:GridView ID="dgvCurrentStockDetails" CssClass="table table-hover table-striped" runat="server" GridLines="None" ShowHeaderWhenEmpty="True"
                                                                    EmptyDataText="No data found..." AutoGenerateColumns="False">
                                                                    <Columns>
                                                                        <asp:TemplateField HeaderText="Com" Visible="false">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblINL_COM" runat="server" Text='<%# Bind("INL_COM") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Loc">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblINL_LOC" runat="server" Text='<%# Bind("INL_LOC") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Item">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblINL_ITM_CD" runat="server" Text='<%# Bind("INL_ITM_CD") %>' Visible="false"></asp:Label>
                                                                                <asp:LinkButton Text='<%# Bind("INL_ITM_CD") %>' ID="SelectStickItem" runat="server" OnClick="SelectStickItem_Click" />
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Description">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblMI_SHORTDESC" runat="server" Text='<%# Bind("MI_SHORTDESC") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="StatusCode" Visible="false">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblINL_ITM_STUS" runat="server" Text='<%# Bind("INL_ITM_STUS") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Status">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblMIS_DESC" runat="server" Text='<%# Bind("MIS_DESC") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Qty">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblINL_QTY" runat="server" Text='<%# Bind("INL_QTY","{0:N2}") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                            <HeaderStyle CssClass="gridHeaderAlignRight" />
                                                                            <ItemStyle HorizontalAlign="Right" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Free Qty">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblINL_FREE_QTY" runat="server" Text='<%# Bind("INL_FREE_QTY","{0:N2}") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                            <HeaderStyle CssClass="gridHeaderAlignRight" />
                                                                            <ItemStyle HorizontalAlign="Right" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Res. Qty">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblinl_res_qty" runat="server" Text='<%# Bind("inl_res_qty","{0:N2}") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                            <HeaderStyle CssClass="gridHeaderAlignRight" />
                                                                            <ItemStyle HorizontalAlign="Right" />
                                                                        </asp:TemplateField>

                                                                    </Columns>
                                                                </asp:GridView>
                                                            </div>
                                                        </div>
                                                        <div class="col-sm-12  paddingLeft0 paddingRight0">
                                                            <asp:UpdatePanel runat="server" ID="addUpPnl">
                                                                <ContentTemplate>
                                                                    <asp:Panel runat="server" DefaultButton="btnAddQty">
                                                                        <div class="col-sm-4">
                                                                            Add Qty
                                                                        </div>
                                                                        <div class="col-sm-4">
                                                                            <asp:TextBox ID="txtAmount" onkeydown="return jsDecimals(event);" Style="text-align: right" CssClass="form-control" runat="server" />
                                                                        </div>
                                                                        <div class="col-sm-4">
                                                                            <asp:LinkButton ID="btnAddQty" CausesValidation="false" runat="server" CssClass="floatRight" OnClientClick="return ValidateDecimal()" OnClick="btnAddQty_Click">
                                                                         <span class="glyphicon glyphicon-plus" aria-hidden="true"></span>
                                                                            </asp:LinkButton>
                                                                        </div>
                                                                    </asp:Panel>
                                                                </ContentTemplate>
                                                            </asp:UpdatePanel>
                                                        </div>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-6 paddingLeft0" style="height: 220px">
                                        <asp:UpdatePanel ID="upRequestItems" runat="server">
                                            <ContentTemplate>
                                                <div class="panel panel-default">
                                                    <div class="panel-heading height30">
                                                        <div class="col-sm-6">
                                                            Request Details
                                                        </div>
                                                        <div class="col-sm-2">
                                                            <asp:Button ID="btnshow" Text="Summery" runat="server" OnClick="btnshow_Click" />
                                                        </div>
                                                        <div class="col-sm-2">
                                                            <asp:Button ID="btnApproveAll" Text="Approve All" runat="server" OnClick="btnApproveAll_Click" />
                                                        </div>
                                                    </div>
                                                    <div class="panel-body">
                                                        <div class="divScroll panel-body panelscoll1" style="height: 150px" onscroll="setScrollPosition(this.scrollTop);">
                                                            <asp:GridView ID="dgvRequestItems" CssClass="table table-hover table-striped" runat="server" GridLines="None" ShowHeaderWhenEmpty="True"
                                                                EmptyDataText="No data found..." AutoGenerateColumns="False" OnRowCommand="dgvRequestItems_RowCommand">
                                                                <Columns>
                                                                    <asp:TemplateField HeaderText=" ">
                                                                        <HeaderTemplate>
                                                                            <asp:CheckBox ID="chkDetailsAll" runat="server" OnCheckedChanged="chkDetailsAll_CheckedChanged" AutoPostBack="true" />
                                                                        </HeaderTemplate>
                                                                        <ItemTemplate>
                                                                            <asp:CheckBox ID="chkDetail" Text=" " runat="server" OnCheckedChanged="chkDetail_CheckedChanged" AutoPostBack="true" />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Seq" Visible="False">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblItri_seq_no" runat="server" Text='<%# Bind("Itri_seq_no") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Request No.">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblItr_req_no" runat="server" Text='<%# Bind("Itr_req_no") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Line" Visible="False">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblItri_line_no" runat="server" Text='<%# Bind("Itri_line_no") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Item">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblItri_itm_cd" runat="server" Text='<%# Bind("Itri_itm_cd") %>' Visible="false"></asp:Label>
                                                                            <asp:LinkButton Text='<%# Bind("Itri_itm_cd") %>' ID="SelectReqItem" runat="server" OnClick="SelectReqItem_Click" />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Model">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblMi_model" runat="server" Text='<%# Bind("Mi_model") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Description">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblMi_shortdesc" runat="server" Text='<%# Bind("Mi_shortdesc") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="P.No">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblMi_part_no" runat="server" Text='<%# Bind("Mi_part_no") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Status">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblMis_desc" runat="server" Text='<%# Bind("Mis_desc") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Status Code" Visible="False">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblItri_itm_stus" runat="server" Text='<%# Bind("Itri_itm_stus") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Qty" Visible="false">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblItri_qty" runat="server" Text='<%# Bind("Itri_qty","{0:N2}") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle CssClass="gridHeaderAlignRight" />
                                                                        <ItemStyle HorizontalAlign="Right" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Unit Price" Visible="false">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblItri_unit_price" runat="server" Text='<%# Bind("Itri_unit_price") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="App. Qty">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblItri_app_qty" runat="server" Text='<%# Bind("Itri_app_qty","{0:N2}") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle CssClass="gridHeaderAlignRight" />
                                                                        <ItemStyle HorizontalAlign="Right" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Bal. Qty">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblItri_bqty" runat="server" Text='<%# Bind("Itri_bqty","{0:N2}") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle CssClass="gridHeaderAlignRight" />
                                                                        <ItemStyle HorizontalAlign="Right" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Res. No">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblItri_res_no" runat="server" Text='<%# Bind("Itri_res_no") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="UOM">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblMi_itm_uom" runat="server" Text='<%# Bind("Mi_itm_uom") %>'></asp:Label>
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
                                    <div class="col-sm-6 paddingLeft0" style="height: 220px;">
                                        <div class="panel panel-default">
                                            <div class="panel-heading height30">
                                                Approved Stock Details
                                            </div>
                                            <div class="panel-body">
                                                <div class="panel-body panelscoll1" style="height: 150px">
                                                    <asp:GridView ID="dgvApprovedItems" CssClass="table table-hover table-striped" runat="server" GridLines="None" ShowHeaderWhenEmpty="True"
                                                        EmptyDataText="No data found..." AutoGenerateColumns="False" OnRowDeleting="dgvApprovedItems_RowDeleting">
                                                        <Columns>
                                                            <asp:TemplateField>
                                                                <ItemTemplate>
                                                                    <div id="delbtndiv">
                                                                        <asp:LinkButton ID="btnDelAppItem" CausesValidation="false" runat="server" OnClientClick="return confDelApproved()" OnClick="btnDelAppItem_Click">
                                                                     <span class="glyphicon glyphicon-trash" aria-hidden="true" style="font-size:15px"></span> 
                                                                        </asp:LinkButton>
                                                                    </div>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    <span>
                                                                        <asp:CheckBox ID="chkHeaderAllApp" Checked="true" runat="server" OnCheckedChanged="chkHeaderAllApp_CheckedChanged" AutoPostBack="true" />
                                                                        All
                                                                    </span>

                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <div id="delbtndiv">
                                                                        <asp:CheckBox ID="ChkselectItm" runat="server" OnCheckedChanged="ChkselectItm_CheckedChanged" AutoPostBack="true"></asp:CheckBox>
                                                                    </div>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="App Location">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblitri_loc" runat="server" Text='<%# Bind("itri_loc") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="App Item Code">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblitri_itm_cd" runat="server" Text='<%# Bind("itri_itm_cd") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="App Item Status" Visible="False">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblitri_itm_stus" runat="server" Text='<%# Bind("itri_itm_stus") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="App Item Status">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblMis_desc" runat="server" Text='<%# Bind("Mis_desc") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="App Qty">
                                                                <%--<ItemTemplate>
                                                                    <asp:TextBox ID="lblitri_app_qty" onkeydown="return jsDecimals(event);" OnTextChanged="lblitri_app_qty_TextChanged"  Style="text-align: right" AutoPostBack="true" onkeypress="return isNumberKey(event)" CssClass="form-control" runat="server" Text='<%# Bind("itri_app_qty") %>' Width="80px" Visible="true"></asp:TextBox>
                                                                     
                                                                    <asp:Label ID="lblitri_app_qty1" runat="server" Text='<%# Bind("itri_app_qty") %>' Visible="false"></asp:Label>
                                                                    <asp:Label ID="lblitri_app_qtydecimal" runat="server" Text='<%# Bind("mi_is_ser1") %>' Visible="false"></asp:Label>
                                                                </ItemTemplate>--%>
                                                                <EditItemTemplate>
                                                                    <asp:TextBox ID="txtlblitri_app_qty" onkeypress="return isNumberKey(event)" onkeydown="return jsDecimals(event);" runat="server" Text='<%# Bind("itri_app_qty","{0:N2}") %>'></asp:TextBox>
                                                                    <asp:Label ID="lblitri_app_qty1" runat="server" Text='<%# Bind("itri_app_qty") %>' Visible="false"></asp:Label>
                                                                </EditItemTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblitri_app_qty" runat="server" Text='<%# Bind("itri_app_qty","{0:#,##0.####}") %>'></asp:Label>
                                                                    <asp:Label ID="lblitri_app_qty1" runat="server" Text='<%# Bind("itri_app_qty") %>' Visible="false"></asp:Label>
                                                                 
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Right" />
                                                                <HeaderStyle CssClass="gridHeaderAlignRight" />

                                                            </asp:TemplateField>

                                                            <asp:TemplateField ShowHeader="False">
                                                                <ItemTemplate>
                                                                     <asp:LinkButton ID="lbtngrdDOItemstEdit" CausesValidation="false" runat="server" OnClick="lbtngrdDOItemstEdit_Click" Visible='<%# chkAppDoc.Checked %>'>
                                                                          <span class="glyphicon glyphicon-pencil" aria-hidden="true"></span>
                                                                     </asp:LinkButton>
                                                                </ItemTemplate>
                                                                <EditItemTemplate>
                                                                      <asp:LinkButton ID="lbtngrdDOItemstUpdate" runat="server" CausesValidation="false" CommandName="Update" OnClick="lbtngrdDOItemstUpdate_Click">
                                                                          <span class="glyphicon glyphicon-ok" aria-hidden="true"></span>
                                                                      </asp:LinkButton>
                                                                </EditItemTemplate>
                                                                <ItemStyle HorizontalAlign="Right" Width="1%" />
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Seq" Visible="False">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblitri_seq_no" runat="server" Text='<%# Bind("itri_seq_no") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Line" Visible="False">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblitri_line_no" runat="server" Text='<%# Bind("itri_line_no") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Res" Visible="False">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblitri_res_no" runat="server" Text='<%# Bind("itri_res_no") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="bLine" Visible="False">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblItri_base_req_line" runat="server" Text='<%# Bind("Itri_base_req_line") %>'></asp:Label>
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
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="dgvApprovedItems" />
                </Triggers>
            </asp:UpdatePanel>
        </div>
    </div>

    <%-- pnl chg item --%>
    <asp:UpdatePanel runat="server" ID="UpdatePanel15">
        <ContentTemplate>
            <asp:Button ID="btnpop1" runat="server" Text="Button" Style="display: none;" />
            <asp:ModalPopupExtender ID="popItemChg" runat="server" Enabled="True" TargetControlID="btnpop1"
                PopupControlID="pnlItemChg" PopupDragHandleControlID="PopupHeader" Drag="true" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdateProgress ID="UpdateProgress10" DisplayAfter="10" runat="server" AssociatedUpdatePanelID="upItemChg">
        <ProgressTemplate>
            <div class="divWaiting">
                <asp:Label ID="lblWait108" runat="server"
                    Text="Please wait... " />
                <asp:Image ID="imgWait108" runat="server"
                    ImageAlign="Middle" ImageUrl="~/images/icons/ajax-loader.gif" />
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <asp:Panel runat="server" ID="pnlItemChg">
        <asp:UpdatePanel runat="server" ID="upItemChg">
            <ContentTemplate>
                <div runat="server" id="Div5" class="panel panel-primary" style="padding: 5px;">
                    <div class="panel panel-default" style="height: 300px; width: 500px;">
                        <div class="panel-heading" style="height: 25px;">
                            <div class="col-sm-10">
                                <strong>Item Code Change</strong>
                            </div>
                            <div class="col-sm-2 text-right">
                                <asp:LinkButton ID="lbtnItmCdChgCls" runat="server" OnClick="lbtnItmCdChgCls_Click">
                                    <span class="glyphicon glyphicon-remove"  aria-hidden="true">Close</span>
                                </asp:LinkButton>
                            </div>
                        </div>
                        <div class="panel-body padding0">
                            <div class="col-sm-12">
                                <div class="row" style="height:8px;">
                                </div>
                                <div class="row">
                                    <div class="col-sm-12">
                                        <div class="col-sm-3 labelText1">
                                            Request No
                                        </div>
                                        <div class="col-sm-6 labelText1">
                                            <asp:TextBox CssClass="form-control" runat="server" ID="txtItmChgReqNo" OnTextChanged="txtItmChgReqNo_TextChanged" />
                                        </div>
                                        <div class="col-sm-3 labelText1">
                                            <asp:Button ID="btnSaveItmChg" Text="Save" Visible="true" runat="server" OnClick="btnSaveItmChg_Click" />
                                        </div>
                                    </div>
                                    
                                </div>
                                <div class="row" style="height:8px;">
                                </div>
                                <div class="row">
                                    <div class="col-sm-12">
                                        <div class="panel panel-default">
                                            <div class="panel panel-body">
                                                <div style="height: 210px; overflow-y: scroll;">
                                                    <asp:UpdatePanel runat="server">
                                                        <ContentTemplate>
                                                            <asp:GridView ID="dgvItmChg" CssClass="table table-hover table-striped" runat="server" GridLines="None"
                                                                ShowHeaderWhenEmpty="True" PagerStyle-CssClass="cssPager"
                                                                EmptyDataText="No data found..." AutoGenerateColumns="False">
                                                                <Columns>
                                                                    <asp:TemplateField Visible="false">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblitri_line_no" runat="server" Text='<%# Bind("itri_line_no") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Item Code">
                                                                        <ItemTemplate>
                                                                            <asp:LinkButton ID="lbtnitri_itm_cd" OnClick="lbtnitri_itm_cd_Click" CausesValidation="false" runat="server" Text='<%# Bind("itri_itm_cd") %>'>
                                                                    <span class="glyphicon" aria-hidden="true"></span>
                                                                            </asp:LinkButton>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                     <asp:TemplateField HeaderText="Base Item Code" Visible="false">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblItri_old_itm_cd" runat="server" Text='<%# Bind("Itri_old_itm_cd") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Item Stus">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblitri_itm_stus" runat="server" Text='<%# Bind("itri_itm_stus") %>' Width="140px"></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Quentity">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblitri_qty" runat="server" Text='<%# Bind("itri_bqty") %>' Width="140px"></asp:Label>
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
                    </div>
                </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </asp:Panel>
    <%-- end itm chg --%>

    <asp:UpdatePanel ID="Upsearch" runat="server">
        <ContentTemplate>
            <asp:Button ID="btnSeachdefbtn" runat="server" Text="Button" Style="display: none" />
            <asp:ModalPopupExtender ID="mpSearch" runat="server" Enabled="True" TargetControlID="btnSeachdefbtn"
                PopupControlID="pnlSearch" PopupDragHandleControlID="divSearchHdr" Drag="true" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>
        </ContentTemplate>
    </asp:UpdatePanel>

    <asp:Panel runat="server" ID="pnlSearch" DefaultButton="lbtnSearch" Style="display: none">
        <div runat="server" id="test" class="panel panel-default height400 width700">
            <asp:Label ID="lblvalue" runat="server" Text="Label" Visible="false"></asp:Label>
            <div class="panel panel-default">
                <div class="panel-heading" id="divSearchHdr">
                    <asp:LinkButton ID="btnSearchClose" runat="server" OnClick="btnSearchClose_Click">
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

    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
        <ContentTemplate>
            <asp:Button ID="btnAppallDefbun" runat="server" Text="Button" Style="display: none" />
            <asp:ModalPopupExtender ID="mpApproveAll" runat="server" Enabled="True" TargetControlID="btnAppallDefbun"
                PopupControlID="pnlApproveAll" CancelControlID="btnCloseAppAll" PopupDragHandleControlID="divApproveAll" Drag="true" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>
        </ContentTemplate>
    </asp:UpdatePanel>

    <asp:Panel runat="server" ID="pnlApproveAll" Style="display: none">
        <div runat="server" id="Div1" class="panel panel-default width700">
            <asp:Label ID="Label1" runat="server" Text="Label" Visible="false"></asp:Label>
            <asp:UpdatePanel ID="upAutoAdd" runat="server">
                <ContentTemplate>
                    <div class="panel panel-default">
                        <div class="panel-heading" id="divApproveAll" style="height: 28px">
                            <div class="col-sm-11">
                                Approve All
                            </div>
                            <div class="col-sm-1">
                                <asp:LinkButton ID="btnCloseAppAll" runat="server">
                             <span class="glyphicon glyphicon-remove" aria-hidden="true">Close</span>
                                </asp:LinkButton>
                            </div>
                        </div>
                        <div class="panel-body">
                            <div class="col-sm-12 height20"></div>
                            <div class="col-sm-12">
                                <div class="col-sm-2">
                                    Issue Location
                                </div>
                                <div class="col-sm-3">
                                    <asp:DropDownList ID="ddlIsseLocation" CssClass="form-control" runat="server"></asp:DropDownList>
                                </div>
                                <div class="col-sm-2">
                                    Item Status
                                </div>
                                <div class="col-sm-3">
                                    <asp:DropDownList ID="ddlIssueItemStatus" CssClass="form-control" runat="server"></asp:DropDownList>
                                </div>
                                <div class="col-sm-2">
                                    <asp:Button Text="Approve" ID="btnApproveSave" Width="100px" runat="server" OnClientClick="return ConfirmApproveSave()" OnClick="btnApproveSave_Click" />
                                </div>
                            </div>
                            <div class="col-sm-12" style="display: none">
                                <div class="panel-body panelscoll1" style="height: 150px">
                                    <asp:GridView ID="dgvAutoApprove" CssClass="table table-hover table-striped" runat="server" GridLines="None" ShowHeaderWhenEmpty="True"
                                        EmptyDataText="No data found..." AutoGenerateColumns="False" OnRowDeleting="dgvAutoApprove_RowDeleting">
                                        <Columns>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <div id="delbtndiv">
                                                        <asp:LinkButton ID="btnDelAppItemAuto" CausesValidation="false" CommandName="Delete" runat="server" OnClientClick="return confDelApprovedAuto()" OnClick="btnDelAppItemAuto_Click">
                                                                     <span class="glyphicon glyphicon-trash" aria-hidden="true" style="font-size:15px"></span> 
                                                        </asp:LinkButton>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="App Location">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblitri_loc" runat="server" Text='<%# Bind("itri_loc") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="App Item Code">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblitri_itm_cd" runat="server" Text='<%# Bind("itri_itm_cd") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="App Item Status" Visible="False">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblitri_itm_stus" runat="server" Text='<%# Bind("itri_itm_stus") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="App Item Status">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblMis_desc" runat="server" Text='<%# Bind("Mis_desc") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="App Qty">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblitri_app_qty" runat="server" Text='<%# Bind("itri_app_qty") %>'></asp:Label>

                                                    <%--<asp:TextBox ID="lblitri_app_qty" OnTextChanged="lblitri_app_qty_TextChanged" Style="text-align: right" AutoPostBack="true" onkeypress="return isNumberKey(event)" CssClass="form-control" runat="server" Text='<%# Bind("itri_app_qty") %>' Width="80px"></asp:TextBox>--%>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Seq" Visible="False">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblitri_seq_no" runat="server" Text='<%# Bind("itri_seq_no") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Line" Visible="False">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblitri_line_no" runat="server" Text='<%# Bind("itri_line_no") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Res" Visible="False">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblitri_res_no" runat="server" Text='<%# Bind("itri_res_no") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>
                            <div class="col-sm-12" style="display: none">
                                <div class="col-sm-9">
                                </div>
                                <div class="col-sm-3">
                                    <asp:LinkButton ID="btnAUtoApproveAdd" runat="server" CausesValidation="false" OnClick="btnAUtoApproveAdd_Click">
                                        <span class="glyphicon glyphicon-plus fontsize20 right5" aria-hidden="true"></span>
                                    </asp:LinkButton>
                                </div>
                            </div>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </asp:Panel>

    <asp:UpdatePanel ID="UpdatePanel4" runat="server">
        <ContentTemplate>
            <asp:Button ID="Button1" runat="server" Text="Button" Style="display: none" />
            <asp:ModalPopupExtender ID="Popupsummery" runat="server" Enabled="True" TargetControlID="Button1"
                PopupControlID="pnlsummery" CancelControlID="btnsummery" PopupDragHandleControlID="pnlsummery" Drag="true" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>
        </ContentTemplate>
    </asp:UpdatePanel>

    <asp:Panel runat="server" ID="pnlsummery" Style="display: none">
        <div runat="server" id="Div2" class="panel panel-default width700 height350">
            <asp:Label ID="Label2" runat="server" Text="Label" Visible="false"></asp:Label>
            <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                <ContentTemplate>
                    <div class="panel panel-default">
                        <div class="panel-heading" id="divs" style="height: 28px">
                            <div class="col-sm-11">
                                Summery
                            </div>
                            <div class="col-sm-1">
                                <asp:LinkButton ID="btnsummery" runat="server">
                             <span class="glyphicon glyphicon-remove" aria-hidden="true">Close</span>
                                </asp:LinkButton>
                            </div>
                        </div>
                        <div class="panel-body">
                            <div class="col-sm-12 height20"></div>
                            <div class="col-sm-12">
                                <div class="col-sm-2 labelText1">
                                    Status
                                </div>
                                <div class="col-sm-3">
                                    <asp:DropDownList ID="ddlStatus" runat="server" TabIndex="10" AutoPostBack="true" CssClass="form-control" OnSelectedIndexChanged="ddlStatus_SelectedIndexChanged"></asp:DropDownList>
                                </div>
                                <div class="col-sm-1  paddingLeft0 paddingRight0  ">
                                    <asp:CheckBox ID="chkstatus" runat="server" AutoPostBack="true" OnCheckedChanged="chkstatus_CheckedChanged"></asp:CheckBox>
                                </div>
                                <div class="col-sm-3 paddingLeft0   ">
                                    <asp:Label runat="server" ID="lbtnstatus"></asp:Label>
                                </div>
                            </div>
                            <div class="col-sm-12">
                                <div class="panel-body panelscollbar">
                                    <asp:GridView ID="grdsummery" CssClass="table table-hover table-striped" runat="server" GridLines="None" ShowHeaderWhenEmpty="True"
                                        EmptyDataText="No data found..." AutoGenerateColumns="False">
                                        <Columns>
                                            <asp:TemplateField HeaderText=" " Visible="false">
                                                <HeaderTemplate>
                                                    <asp:CheckBox ID="chkHeaderAllS" runat="server"  />
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkHeadersummerys" Text=" " runat="server"  />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Item code">
                                                <ItemTemplate>
                                                    <asp:Label ID="col_Itri_itm_cd" runat="server" Text='<%# Bind("Item") %>' Width="50px"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Req.Qty">
                                                <ItemTemplate>
                                                    <asp:Label ID="col_Qty" runat="server" Text='<%# Bind("Qty") %>' Width="50px"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                              <asp:TemplateField HeaderText="Stock">
                                                <ItemTemplate>
                                                    <asp:Label ID="col_LQty" runat="server" Text='<%# Bind("LQty") %>' Width="50px"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Free">
                                                <ItemTemplate>
                                                    <asp:Label ID="col_Stock" runat="server" Text='<%# Bind("Stock") %>' Width="50px"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                              <asp:TemplateField HeaderText="Res">
                                                <ItemTemplate>
                                                    <asp:Label ID="col_Res" runat="server" Text='<%# Bind("Res") %>' Width="50px"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>
                            <div class="col-sm-12">
                                <div class="col-sm-9">
                                    <asp:Button ID="Button3" Text="Approve All" Visible="false" runat="server" OnClick="btncontinue_Click" />
                                </div>
                                <div class="col-sm-3">
                                    <div class="col-sm-2">
                                        <asp:Button ID="btnsummeryapproval" Text="Approve All" Visible="false" runat="server" OnClick="btnsummeryapproval_Click" />
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
                                        <asp:Panel runat="server" ID="pnldoc" Visible="false">
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
                                        </asp:Panel>
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
            <asp:HiddenField ID="hfScrollPosition2" Value="0" runat="server" />
            <asp:HiddenField ID="hfScrollPosition1" Value="0" runat="server" />

        </ContentTemplate>
    </asp:UpdatePanel>
    <%-- pop up --%>
      <asp:UpdatePanel ID="UpdatePanel32" runat="server">
        <ContentTemplate>
            <asp:Button ID="Button10" runat="server" Text="Button" Style="display: none;" />
            <asp:ModalPopupExtender ID="SbuPopup" runat="server" Enabled="True" TargetControlID="Button10"
                PopupControlID="pnlSBU" PopupDragHandleControlID="PopupHeader" Drag="true" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdateProgress ID="UpdateProgress9" DisplayAfter="10"
        runat="server" AssociatedUpdatePanelID="UpdatePanel33">
        <ProgressTemplate>
            <div class="divWaiting">
                <asp:Label ID="lblWait235" runat="server"
                    Text="Please wait... " />
                <asp:Image ID="imgWait235" runat="server"
                    ImageAlign="Middle" ImageUrl="~/images/banners/hidden_progress_bar.gif" />
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>

    <asp:Panel ID="pnlSBU" runat="server" align="center">
        <asp:Label ID="Label4" runat="server" Text="Label" Visible="false"></asp:Label>
        <asp:ScriptManagerProxy ID="ScriptManagerProxy17" runat="server"></asp:ScriptManagerProxy>
        <asp:UpdatePanel ID="UpdatePanel33" runat="server">
            <ContentTemplate>
                 <div class="panel panel-default">
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
                                <asp:Button ID="lbtnPopOk" OnClick="lbtnPopOk_Click" runat="server" Text="yes" Width="80px" CausesValidation="false" class="btn btn-primary btn-xs" />
                            </div>
                            <div class="col-sm-2 ">
                                <asp:Button ID="lbtnPopNo" OnClick="lbtnPopNo_Click" runat="server" Text="No" Width="80px" CausesValidation="false" class="btn btn-primary btn-xs" />
                            </div>
                        </div>
                    </div>
                     </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </asp:Panel>
     
    <%--All Send to pda --%>
    <asp:UpdatePanel ID="UpdatePanel7" runat="server">
        <ContentTemplate>
            <asp:Button ID="Button2" runat="server" Text="Button" Style="display: none;" />
            <asp:ModalPopupExtender ID="popSendToPda" runat="server" Enabled="True" TargetControlID="Button2"
                PopupControlID="pnlSendToPda" PopupDragHandleControlID="PopupHeader" Drag="true" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>
        </ContentTemplate>
    </asp:UpdatePanel>

    <asp:UpdateProgress ID="UpdateProgress6" DisplayAfter="10"
        runat="server" AssociatedUpdatePanelID="UpdatePanel8">
        <ProgressTemplate>
            <div class="divWaiting">
                <asp:Label ID="lblWait23" runat="server"
                    Text="Please wait... " />
                <asp:Image ID="imgWait23" runat="server"
                    ImageAlign="Middle" ImageUrl="~/images/banners/hidden_progress_bar.gif" />
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>

    <asp:Panel ID="pnlSendToPda" runat="server" align="center">
        <asp:Label ID="Label3" runat="server" Text="Label" Visible="false"></asp:Label>
        <asp:ScriptManagerProxy ID="ScriptManagerProxy3" runat="server"></asp:ScriptManagerProxy>
        <asp:UpdatePanel ID="UpdatePanel8" runat="server">
            <ContentTemplate>
                <div>
                    <div class="panel panel-default">
                        <div class="panel panel-body">
                            <div class="row">
                                <div class="col-sm-12">
                                    <div class="col-sm-5">
                                    </div>
                                    <div class="col-sm-3">
                                    </div>
                                    <div class="col-sm-3 padding0">
                                        <div class="row buttonRow">
                                            <div class="col-sm-12">
                                                <div class="col-sm-6 padding0">
                                                    <asp:LinkButton ID="lbtnSendToPDA" runat="server" OnClientClick="return ConfSend();" OnClick="lbtnSendToPDA_Click">
                                                <span class="glyphicon glyphicon-saved" aria-hidden="true"></span>Send
                                                    </asp:LinkButton>
                                                </div>
                                                <div class="col-sm-6 padding0" style="width: 55px;">
                                                    <asp:LinkButton ID="lbtnSPDaClose" runat="server" OnClick="lbtnSPDaClose_Click" Style="float: right;">
                                                <span class="glyphicon glyphicon-remove" aria-hidden="true"></span>Close
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
                                        <div class="panel panel-heading text-left">
                                            <strong>Pending Documents To Be Sent To PDA</strong>
                                        </div>
                                        <div class="panel panel-body">
                                            <div class="row" style="margin-top: 4px; margin-bottom: 4px;">
                                                <div class="col-sm-12">
                                                    <div class="col-sm-2 labelText1">
                                                        Loading Bay
                                                    </div>
                                                    <div class="col-sm-3">
                                                        <asp:DropDownList ID="ddlSendAllLoadingBay" AppendDataBoundItems="true" runat="server" AutoPostBack="true" TabIndex="2" CssClass="form-control">
                                                            <asp:ListItem Text="--Select--" Value="0" />
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-12">
                                                    <div style="height: 200px; overflow-y: scroll;">
                                                        <asp:GridView ID="dgvPopPendingDoc" CssClass="table table-hover table-striped" runat="server" GridLines="None"
                                                            ShowHeaderWhenEmpty="True" PagerStyle-CssClass="cssPager"
                                                            EmptyDataText="No data found..." AutoGenerateColumns="False">
                                                            <Columns>
                                                                <asp:TemplateField HeaderText=" ">
                                                                    <HeaderTemplate>
                                                                        <asp:CheckBox ID="chkSelectAll" runat="server" OnCheckedChanged="chkSelectAll_CheckedChanged" AutoPostBack="true" />
                                                                    </HeaderTemplate>
                                                                    <ItemTemplate>
                                                                        <asp:CheckBox ID="chkSelect" Text=" " runat="server" OnCheckedChanged="chkSelect_CheckedChanged" AutoPostBack="true" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Request Doc No">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblITR_REQ_NO" runat="server" Text='<%# Bind("ITR_REQ_NO") %>' Width="140px"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Ref No">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblITR_REF" runat="server" Text='<%# Bind("ITR_REF") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Dispatch From">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblITR_ISSUE_FROM" runat="server" Text='<%# Bind("ITR_ISSUE_FROM") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Deliver To">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblITR_REC_TO" runat="server" Text='<%# Bind("ITR_REC_TO", "{0:dd/MMM/yyyy}") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Required Date" SortExpression="ITR_EXP_DT">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblITR_EXP_DT" runat="server" Text='<%# Bind("ITR_EXP_DT", "{0:dd/MMM/yyyy}") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Create Date">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblitr_cre_dt" runat="server" Text='<%# Bind("itr_cre_dt", "{0:dd/MMM/yyyy hh:mm tt}") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Loc" Visible="false">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblITR_LOC" runat="server" Text='<%# Bind("ITR_LOC") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Type" Visible="false">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblItr_tp" runat="server" Text='<%# Bind("Itr_tp") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Customer Name">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblItr_anal1" runat="server" Text='<%# Bind("Itr_anal1") %>' Width="150px"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="direction" Visible="false">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblitr_direct" runat="server" Text='<%# Bind("itr_direct") %>' Width="150px"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <%--<asp:TemplateField HeaderText="Customer" Visible="false">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="LblItr_bus_code" runat="server" Text='<%# Bind("Itr_bus_code") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        
                                                                         <asp:TemplateField HeaderText="direction" Visible="false">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblitr_direct" runat="server" Text='<%# Bind("itr_direct") %>' Width="150px"></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>--%>
                                                            </Columns>
                                                            <PagerStyle CssClass="cssPager" />
                                                        </asp:GridView>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="panel panel-default">
                                        <div class="panel panel-heading text-left">
                                            <strong>PDA Scanned/ Prosessing Documents</strong>
                                        </div>
                                        <div class="panel panel-body">
                                            <div style="height: 150px; overflow-y: scroll;">
                                                <asp:GridView ID="dgvSendDoc" CssClass="table table-hover table-striped" runat="server" GridLines="None"
                                                    ShowHeaderWhenEmpty="True" PagerStyle-CssClass="cssPager"
                                                    EmptyDataText="No data found..." AutoGenerateColumns="False">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Request Doc No">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblITR_REQ_NO" runat="server" Text='<%# Bind("ITR_REQ_NO") %>' Width="140px"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Ref No">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblITR_REF" runat="server" Text='<%# Bind("ITR_REF") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Dispatch From">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblITR_ISSUE_FROM" runat="server" Text='<%# Bind("ITR_ISSUE_FROM") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Deliver To">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblITR_REC_TO" runat="server" Text='<%# Bind("ITR_REC_TO", "{0:dd/MMM/yyyy}") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Required Date" SortExpression="ITR_EXP_DT">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblITR_EXP_DT" runat="server" Text='<%# Bind("ITR_EXP_DT", "{0:dd/MMM/yyyy}") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Create Date">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblitr_cre_dt" runat="server" Text='<%# Bind("itr_cre_dt", "{0:dd/MMM/yyyy hh:mm tt}") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Loc" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblITR_LOC" runat="server" Text='<%# Bind("ITR_LOC") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Type" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblItr_tp" runat="server" Text='<%# Bind("Itr_tp") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Customer Name">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblItr_anal1" runat="server" Text='<%# Bind("Itr_anal1") %>' Width="150px"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="direction" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblitr_direct" runat="server" Text='<%# Bind("itr_direct") %>' Width="150px"></asp:Label>
                                                            </ItemTemplate>
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
                    </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </asp:Panel>


     
      
    <script>
        if (typeof jQuery == 'undefined') {
            alert('jQuery is not loaded');
        }
        Sys.Application.add_load(fun);
        function fun() {
            $(document).ready(function () {
                maintainScrollPosition();
                maintainScrollPosition1(); maintainScrollPosition2();
            });
        }

    </script>
</asp:Content>


