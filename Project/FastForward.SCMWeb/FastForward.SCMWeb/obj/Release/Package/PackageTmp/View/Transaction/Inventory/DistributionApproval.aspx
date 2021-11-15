<%@ Page Title="" Language="C#" MasterPageFile="~/View/AdminSite.Master" AutoEventWireup="true" EnableEventValidation="false" CodeBehind="DistributionApproval.aspx.cs" Inherits="FastForward.SCMWeb.View.Transaction.Inventory.Distribution_approval" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">

    <script src="<%=Request.ApplicationPath%>Js/jquery.min.js"></script>
    <script src="<%=Request.ApplicationPath%>Js/bootstrap.min.js"></script>
    <script src="<%=Request.ApplicationPath%>Js/jquery.toastmessage.js"></script>
    <script src="<%=Request.ApplicationPath%>Js/jquery.min.js"></script>
    <script src="<%=Request.ApplicationPath%>Js/bootstrap.min.js"></script>
    <script src="<%=Request.ApplicationPath%>Js/jquery.toastmessage.js"></script>

    <link href="<%=Request.ApplicationPath%>Css/jquery-ui.theme.min.css" rel="stylesheet" />
    <link href="<%=Request.ApplicationPath%>Css/jquery-ui.min.css" rel="stylesheet" />

    <script>
        var sessionTimeoutWarning = "<%= System.Configuration.ConfigurationSettings.AppSettings["SessionWarning"].ToString()%>";
        var sessionTimeout = "<%= Session.Timeout %>";
        var sTimeout = parseInt(sessionTimeoutWarning) * 60 * 100000;
        setTimeout('SessionWarning()', sTimeout);

        function SessionWarning() {
            var message = "Your session will expire in another " + (parseInt(sessionTimeout) - parseInt(sessionTimeoutWarning)) + " mins! Please Save the data before the session expires";
            alert(message);
        }
        function isNumberKey(evt) {
            var charCode = (evt.which) ? evt.which : evt.keyCode;
            if (charCode != 46 && charCode > 31
              && (charCode < 48 || charCode > 57))
                return false;
            return true;
        };
    </script>
    <script>
        //$(document).ready(function () {
        //    maintainScrollPosition();
        //});
        //function pageLoad() {
        //    maintainScrollPosition();
        //}
        function maintainScrollPosition() {
            $("#dvScroll").scrollTop($('#<%=hfScrollPosition.ClientID%>').val());
            $("#dvScrollItm").scrollTop($('#<%=hfScrollPositionitm.ClientID%>').val());
            //console.log($('#<%=hfScrollPosition.ClientID%>').val());*
        }
        function setScrollPosition(scrollValue) {
            $('#<%=hfScrollPosition.ClientID%>').val(scrollValue);
            $('#<%=hfScrollPositionitm.ClientID%>').val(scrollValue);
        }
        function scrollTop() {
            window.document.body.scrollTop = 0;
            window.document.documentElement.scrollTop = 0;
        };
    </script>
    <script type="text/javascript">
        function pageLoad(sender, args) {

            $("#<%=txtDeliverDate.ClientID %>").datepicker({ dateFormat: "d/M/yy", timeFormat: "hh:mm tt" });
            $("#<%=txtFromDate.ClientID %>").datepicker({ dateFormat: "d/M/yy", timeFormat: "hh:mm tt" });
            $("#<%=txtToDate.ClientID %>").datepicker({ dateFormat: "d/M/yy", timeFormat: "hh:mm tt" });
            jQuery(".txtserexdate").datepicker({ dateFormat: "d/M/yy", timeFormat: "hh:mm tt" });

        };
        function CheckBoxCheckItem(rb) {
            debugger;
            var gv = document.getElementById("<%=grdMRNReqItem.ClientID%>");
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
        function CheckBoxCheck(rb) {
            debugger;
            var gv = document.getElementById("<%=grdRequestDetails.ClientID%>");
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
        function checkDate(sender, args) {

            if ((sender._selectedDate < new Date())) {
                alert("You cannot select a day earlier than today!");
                sender._selectedDate = new Date();
                sender._textbox.set_Value(sender._selectedDate.format(sender._format))
            }
        }

        function SaveConfirm() {

            var selectedvalue = confirm("Do you want to save data?");
            if (selectedvalue) {
                document.getElementById('<%=txtSavelconformmessageValue.ClientID %>').value = "Yes";
            } else {
                document.getElementById('<%=txtSavelconformmessageValue.ClientID %>').value = "No";
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
        function ConfAddProcess() {
            var selectedvalueOrd = confirm("Are you sure do you want to process this document ?");
            if (selectedvalueOrd) {
                return true;
            } else {
                return false;
            }
        };
        function ConfAddProcessCanc() {
            var selectedvalueOrd = confirm("Are you sure do you want to remove process user ?");
            if (selectedvalueOrd) {
                return true;
            } else {
                return false;
            }
        };
        function UpdateConfirm() {
            var selectedvalue = confirm("Do you want to Update data?");
            if (selectedvalue) {

                document.getElementById('<%=txtUpdateconformmessageValue.ClientID %>').value = "Yes";
            } else {

                document.getElementById('<%=txtUpdateconformmessageValue.ClientID %>').value = "No";
            }
        };
        function DeleteConfirm() {
            var selectedvalue = confirm("Do you want to delete data?");
            if (selectedvalue) {
                document.getElementById('<%=txtDeleteconformmessageValue.ClientID %>').value = "Yes";
            } else {
                document.getElementById('<%=txtDeleteconformmessageValue.ClientID %>').value = "No";
            }
        };
        function CancelConfirm() {
            var selectedvalue = confirm("Do you want to cancel MRN?");
            if (selectedvalue) {
                document.getElementById('<%=txtCancelconformmessageValue.ClientID %>').value = "Yes";
            } else {
                document.getElementById('<%=txtCancelconformmessageValue.ClientID %>').value = "No";
            }
        };
        function ACancelConfirm() {
            var selectedvalue = confirm("Do you want to cancel approved MRN?");
            if (selectedvalue) {
                document.getElementById('<%=txtACancelconformmessageValue.ClientID %>').value = "Yes";
            } else {
                document.getElementById('<%=txtACancelconformmessageValue.ClientID %>').value = "No";
            }
        };
    </script>
    <script type="text/javascript">
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
        .divcss {
            overflow-y: scroll;
            height: 200px;
            width: 400px;
        }
    </style>
    <style type="text/css">
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
    <asp:UpdateProgress ID="UpdateProgress3" DisplayAfter="10"
        runat="server" AssociatedUpdatePanelID="MaiUpdate">
        <ProgressTemplate>
            <div class="divWaiting">
                <asp:Label ID="lblWaitM" runat="server"
                    Text="Please wait... " />
                <asp:Image ID="imgWaitM" runat="server"
                    ImageAlign="Middle" ImageUrl="~/images/icons/ajax-loader.gif" />
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <asp:UpdateProgress ID="UpdateProgress2" DisplayAfter="10"
        runat="server" AssociatedUpdatePanelID="UpdatePanel25">
        <ProgressTemplate>
            <div class="divWaitingc">
                <asp:Label ID="lblWaitc" runat="server"
                    Text="Please wait... " />
                <asp:Image ID="imgWaitc" runat="server"
                    ImageAlign="Middle" ImageUrl="~/images/icons/ajax-loader.gif" />
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <asp:UpdatePanel runat="server" ID="MaiUpdate">
        <ContentTemplate>
            <asp:HiddenField ID="txtACancelconformmessageValue" runat="server" />
            <asp:HiddenField ID="txtClearlconformmessageValue" runat="server" />
            <asp:HiddenField ID="txtSavelconformmessageValue" runat="server" />
            <asp:HiddenField ID="txtUpdateconformmessageValue" runat="server" />
            <asp:HiddenField ID="txtApprovalconformmessageValue" runat="server" />
            <asp:HiddenField ID="txtCancelconformmessageValue" runat="server" />
            <asp:HiddenField ID="txtDeleteconformmessageValue" runat="server" />
            <asp:HiddenField ID="HiddenItemLine" runat="server" />
            <asp:HiddenField ID="HiddenItemcode" runat="server" />
            <asp:HiddenField ID="HiddenPOType" runat="server" />
            <div class="panel panel-default marginLeftRight5">
                <div class="panel-body">
                    <div class="row">
                        <div class="col-sm-12">
                            <div class="col-sm-6  buttonrow">
                            </div>
                            <div class="col-sm-6 buttonRow">
                                <div class="col-sm-2">
                                </div>
                                <div class="col-sm-2 paddingLeft0 paddingRight0 ">
                                    <asp:LinkButton ID="lbtnApprovItem" CausesValidation="false" runat="server" OnClick="lbtnApprovItem_Click">
                                                <span class="glyphicon glyphicon-thumbs-up" aria-hidden="true"></span>Approved Items
                                    </asp:LinkButton>
                                </div>
                                <div class="col-sm-1">
                                </div>
                                <div class="col-sm-2 paddingLeft0 paddingRight0">
                                    <asp:LinkButton ID="lbtnAmend" CausesValidation="false" runat="server" OnClientClick="UpdateConfirm()" OnClick="lbtnAmend_Click">
                                        <span class="glyphicon glyphicon-edit" aria-hidden="true"></span>Update
                                    </asp:LinkButton>
                                </div>

                                <div class="col-sm-1 paddingLeft0 paddingRight0 ">
                                    <asp:LinkButton ID="lbtnSave" CausesValidation="false" runat="server" OnClientClick="SaveConfirm()" OnClick="lbtnSave_Click">
                                        <span class="glyphicon glyphicon-saved" aria-hidden="true"></span>Save
                                    </asp:LinkButton>
                                </div>
                                <div class="col-sm-1">
                                </div>
                                <div class="col-sm-2 paddingRight0 paddingLeft0">
                                    <asp:LinkButton ID="lbtnClear" runat="server" OnClientClick="ClearConfirm()" OnClick="lbtnClear_Click">
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


                                                    <div class="col-sm-12 ">
                                                        <asp:LinkButton ID="lbtncancelrequest" CausesValidation="false" runat="server" OnClientClick="CancelConfirm()" OnClick="lbtncancelrequest_Click">
                                        <span class="glyphicon glyphicon-remove" aria-hidden="true"></span>Cancel pending request
                                                        </asp:LinkButton>
                                                    </div>

                                                    <div class="col-sm-12  ">
                                                        <asp:LinkButton ID="lbtncancel" CausesValidation="false" runat="server" OnClientClick="ACancelConfirm()" OnClick="llbtncancel_Click">
                                        <span class="glyphicon glyphicon-remove" aria-hidden="true"></span>Cancel Approval
                                                        </asp:LinkButton>
                                                    </div>
                                                    <div class="col-sm-12   ">
                                                        <asp:LinkButton ID="lbtnExcelUpload" runat="server" OnClick="lbtnExcelUpload_Click">
                                        <span class="glyphicon glyphicon-upload" aria-hidden="true"></span>Excel Upload
                                                        </asp:LinkButton>
                                                    </div>
                                                    <div class="col-sm-12   ">
                                                        <asp:LinkButton ID="lbtnPrint" runat="server" OnClick="lbtnPrint_Click">
                                        <span class="glyphicon glyphicon-upload" aria-hidden="true"></span>Print
                                                        </asp:LinkButton>
                                                    </div>
                                                    <div class="col-sm-12   ">
                                                        <asp:LinkButton ID="lbtnPrintAppDoc" runat="server" OnClick="lbtnPrintAppDoc_Click">
                                                            <span class="glyphicon glyphicon-upload" aria-hidden="true"></span>Print Document
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
                        <div class="col-sm-12 ">
                            <div class="panel panel-default ">
                                <div class="panel-heading paddingtop0 paddingbottom0">
                                    <div class="row  ">
                                        <div class="col-sm-12  ">
                                            <div class="col-sm-2 paddingLeft5">
                                                <strong>MRN Approval </strong>
                                            </div>
                                            <div class="col-sm-10 paddingLeft5 paddingRight0">
                                            </div>

                                        </div>
                                    </div>

                                </div>

                                <div class="panel-body">

                                    <div class="col-sm-9">
                                        <div class="row">
                                            <div class="panel panel-default">
                                                <div class="panel-body">
                                                    <div class="row">
                                                        <div class="col-sm-8">
                                                            <div class="col-sm-3">
                                                                <div class="row">
                                                                    <div class="col-sm-1">
                                                                        <asp:RadioButton AutoPostBack="true" Checked="true" runat="server" ID="rdbRout" GroupName="Search" OnCheckedChanged="rdbRout_CheckedChanged" />
                                                                    </div>
                                                                    <div class="col-sm-9">
                                                                        Route wise
                                                                    </div>
                                                                </div>
                                                                <div class="row">
                                                                    <div class="col-sm-1">
                                                                        <asp:RadioButton AutoPostBack="true" runat="server" ID="rdbshowroom" GroupName="Search" OnCheckedChanged="rdbshowroom_CheckedChanged" />
                                                                    </div>
                                                                    <div class="col-sm-9">
                                                                        Location wise
                                                                    </div>
                                                                </div>
                                                                <div class="row">
                                                                    <div class="col-sm-1">
                                                                        <asp:RadioButton runat="server" AutoPostBack="true" ID="rdbchannel" GroupName="Search" OnCheckedChanged="rdbchannel_CheckedChanged" />
                                                                    </div>
                                                                    <div class="col-sm-9">
                                                                        Channel wise
                                                                    </div>

                                                                </div>

                                                            </div>
                                                            <div class="col-sm-3">
                                                                <div class="row height10">
                                                                </div>
                                                                <div class="row">

                                                                    <div class="col-sm-2 labelText1  ">
                                                                        From
                                                                    </div>
                                                                    <div class="col-sm-8 ">
                                                                        <asp:TextBox ID="txtFromDate" runat="server" CssClass="form-control" onkeypress="return RestrictSpace()"
                                                                            Format="dd/MMM/yyyy">
                                                                        </asp:TextBox>
                                                                    </div>
                                                                    <div class="col-sm-2 paddingLeft0 paddingRight0">
                                                                        <asp:LinkButton ID="lbtnFromDate" runat="server" CausesValidation="false">
                                                            <span class="glyphicon glyphicon-calendar" aria-hidden="true"></span>
                                                                        </asp:LinkButton>
                                                                        <asp:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtFromDate"
                                                                            PopupButtonID="lbtnFromDate" Format="dd/MMM/yyyy">
                                                                        </asp:CalendarExtender>
                                                                    </div>
                                                                </div>
                                                                <div class="row">
                                                                    <div class="col-sm-2 labelText1  ">
                                                                        To
                                                                    </div>
                                                                    <div class="col-sm-8">
                                                                        <asp:TextBox ID="txtToDate" runat="server" CssClass="form-control"
                                                                            Format="dd/MMM/yyyy">
                                                                        </asp:TextBox>
                                                                    </div>
                                                                    <div class="col-sm-2 paddingLeft0 paddingRight0">
                                                                        <asp:LinkButton ID="lbtnToDate" runat="server" CausesValidation="false">
                                                            <span class="glyphicon glyphicon-calendar" aria-hidden="true"></span>
                                                                        </asp:LinkButton>
                                                                        <asp:CalendarExtender ID="CalendarExtender4" runat="server" TargetControlID="txtToDate"
                                                                            PopupButtonID="lbtnToDate" Format="dd/MMM/yyyy">
                                                                        </asp:CalendarExtender>
                                                                    </div>

                                                                </div>
                                                            </div>
                                                            <div class="col-sm-4">
                                                                <div class="row height10">
                                                                </div>
                                                                <div class="row">

                                                                    <div class="col-sm-4 labelText1 paddingLeft5 paddingRight0 ">
                                                                        <asp:Label runat="server" Text="Route" ID="lblsearch"></asp:Label>
                                                                    </div>
                                                                    <div class="col-sm-5 paddingLeft5 paddingRight0">
                                                                        <asp:TextBox runat="server" ID="txtsearch" Style="text-transform: uppercase;" AutoPostBack="true" Enabled="true" CausesValidation="false" CssClass="form-control" OnTextChanged="txtsearch_TextChanged"></asp:TextBox>
                                                                    </div>
                                                                    <div class="col-sm-1 paddingLeft0 Lwidth">
                                                                        <asp:LinkButton ID="lbtnsearchOp" runat="server" CausesValidation="false" OnClick="lbtnsearchOp_Click">
                                                        <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                                        </asp:LinkButton>
                                                                    </div>
                                                                    <div class="col-sm-1 labelText1 paddingLeft5 paddingRight5" runat="server" id="All">
                                                                        All
                                                                    </div>
                                                                    <div class="col-sm-1  paddingRight0 paddingLeft5">
                                                                        <asp:CheckBox ID="chkSearchall" AutoPostBack="true" Checked="true" runat="server" Visible="true" Width="5px" OnCheckedChanged="chkSearchall_CheckedChanged"></asp:CheckBox>
                                                                    </div>
                                                                </div>
                                                                <div class="row">
                                                                    <div class="col-sm-4 labelText1 paddingLeft5  ">
                                                                        Prefer Loc
                                                                    </div>
                                                                    <div class="col-sm-5 paddingLeft5 paddingRight0">
                                                                        <asp:TextBox runat="server" ID="txtPreferLocation" Style="text-transform: uppercase;" Enabled="false" AutoPostBack="true" CausesValidation="false" CssClass="form-control"></asp:TextBox>
                                                                    </div>
                                                                    <div class="col-sm-1 paddingLeft0 Lwidth">
                                                                        <asp:LinkButton ID="lbtnPreferlocation" runat="server" CausesValidation="false" OnClick="lbtnPreferlocation_Click">
                                                                        <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                                        </asp:LinkButton>
                                                                    </div>
                                                                    <div class="col-sm-1 labelText1 paddingLeft5 paddingRight5">
                                                                        All
                                                                    </div>
                                                                    <div class="col-sm-1  paddingRight0 paddingLeft5">
                                                                        <asp:CheckBox ID="chkprelocAll" AutoPostBack="true" Checked="true" runat="server" Width="5px" OnCheckedChanged="chkprelocAll_CheckedChanged"></asp:CheckBox>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="col-sm-1">
                                                                <div class="row height10">
                                                                </div>
                                                                <div class="row height10">
                                                                </div>
                                                                <div class="row">
                                                                    <div class="col-sm-12 width250">
                                                                        <div class="col-sm-1 paddingLeft0 paddingRight0 ">
                                                                            <asp:LinkButton ID="lbtnFilter" runat="server" CausesValidation="false" OnClick="lbtnFilter_Click">
                                                        <span class="glyphicon glyphicon-search fontsize20" aria-hidden="true"></span>
                                                                            </asp:LinkButton>
                                                                        </div>
                                                                    </div>

                                                                </div>
                                                            </div>
                                                        </div>

                                                        <div class="col-sm-4 paddingLeft0">
                                                            <div class="panel panel-default">
                                                                <div class="row ">
                                                                    <div class="col-sm-12 ">
                                                                        <div class="col-sm-3 labelText1 paddingLeft0 paddingRight0">
                                                                            Showroom 
                                                                        </div>
                                                                        <div class="col-sm-8 ">
                                                                            <asp:TextBox runat="server" ID="txtShowroom" CausesValidation="false" AutoPostBack="true" CssClass="form-control" OnTextChanged="txtShowroom_TextChanged"></asp:TextBox>
                                                                        </div>
                                                                        <div class="col-sm-1 paddingLeft0 ">
                                                                            <asp:LinkButton ID="lbtnShowroom" runat="server" CausesValidation="false" OnClick="lbtnShowroom_Click">
                                                                    <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                                            </asp:LinkButton>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class="row">
                                                                    <div class="col-sm-12 ">
                                                                        <div class="col-sm-3 labelText1 paddingLeft0 paddingRight0">
                                                                            Approval # 
                                                                        </div>
                                                                        <div class="col-sm-8">
                                                                            <asp:TextBox runat="server" Enabled="false" ID="txtApproval" AutoPostBack="true" CausesValidation="false" CssClass="form-control"></asp:TextBox>
                                                                        </div>
                                                                        <div class="col-sm-1 paddingLeft0 ">
                                                                            <asp:LinkButton ID="lbtnApprovalMRN" runat="server" CausesValidation="false" OnClick="lbtnApprovalMRN_Click">
                                                                    <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                                            </asp:LinkButton>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class="row">
                                                                    <div class="col-sm-2 labelText1">
                                                                        <asp:CheckBox ID="chkcomahhoc" AutoPostBack="true" runat="server" OnCheckedChanged="chkcomahhoc_CheckedChanged"></asp:CheckBox>
                                                                    </div>
                                                                    <div class="col-sm-10 labelText1 paddingLeft0 paddingRight0">
                                                                        <asp:Label runat="server" ID="lblexcel" Text="S/R Requesting"></asp:Label>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>

                                                    </div>
                                                    <div class="row">
                                                        <div class="col-sm-12">
                                                            <div class="panel panel-default">
                                                                <asp:HiddenField ID="hfScrollPosition" Value="0" runat="server" />
                                                                <div id="dvScroll" class="panel-body panelscollbar height200" onscroll="setScrollPosition(this.scrollTop);">
                                                                    <asp:GridView ID="grdRequestDetails" CssClass="table table-hover table-striped" runat="server" GridLines="None" ShowHeaderWhenEmpty="True"
                                                                        EmptyDataText="No data found..." AutoGenerateColumns="False">
                                                                        <Columns>
                                                                            <asp:TemplateField HeaderText="" Visible="true">
                                                                                <%--  <HeaderTemplate>
                                                                                    <asp:CheckBox ID="allchk" runat="server" Width="5px" onclick="CheckAllgrdReq(this)"></asp:CheckBox>
                                                                                </HeaderTemplate>--%>
                                                                                <ItemTemplate>
                                                                                    <asp:CheckBox ID="chk_Req" AutoPostBack="true" runat="server" onclick="CheckBoxCheck(this);" OnCheckedChanged="chk_Req_CheckedChanged_Click" Checked="false" Width="5px" />
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField>
                                                                                <ItemTemplate>
                                                                                    <asp:LinkButton Visible='<%# !Eval("Itr_req_wp").ToString().Equals("1")%>'  ID="lbtnProcess" OnClientClick="return ConfAddProcess();" runat="server" OnClick="lbtnProcess_Click">
                                                                                     <span class="glyphicon glyphicon-arrow-down" aria-hidden="true"></span>
                                                                                    </asp:LinkButton>
                                                                                    <asp:LinkButton Visible='<%# !Eval("Itr_req_wp").ToString().Equals("0")%>'  ID="lbtnProcessCancel" OnClientClick="return ConfAddProcessCanc();" runat="server" OnClick="lbtnProcessCancel_Click">
                                                                                     <span class="glyphicon glyphicon-minus" aria-hidden="true"></span>
                                                                                    </asp:LinkButton>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            
                                                                            <asp:TemplateField HeaderText="Seq#" Visible="false">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="col_itr_seq_no" runat="server" Text='<%# Bind("itr_seq_no") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="S/R request #">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="col_itr_req_no" runat="server" Text='<%# Bind("itr_req_no") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Req.Date">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="col_itr_dt" runat="server" Text='<%# Bind("itr_cre_dt") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="S/R Code">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="col_itr_loc" runat="server" Text='<%# Bind("itr_loc") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Req.Type">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="col_itr_sub_tp" runat="server" Text='<%# Bind("itr_sub_tp") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Delivery Date">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="col_itr_exp_dt" runat="server" Text='<%# Bind("itr_exp_dt", "{0:dd/MMM/yyyy}") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="" Visible="false">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="col_itr_note" runat="server" Text='<%# Bind("itr_note") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                             <asp:TemplateField HeaderText="" Visible="false">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="col_itr_cre_by" runat="server" Text='<%# Bind("itr_cre_by") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Pre.Loc" Visible="true">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="col_itr_issue_from" runat="server" Text='<%# Bind("itr_issue_from") %>'></asp:Label>
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
                                        <div class="row ">
                                            <div class="col-sm-8 paddingLeft0">

                                                <div class="row">
                                                    <div class="col-sm-4 paddingRight0">
                                                        Item
                                                    </div>

                                                    <div class="col-sm-1 Lwidth paddingLeft0">
                                                    </div>
                                                    <div class="col-sm-1 paddingRight0 paddingLeft0">
                                                        Req.qty
                                                    </div>
                                                    <div class="col-sm-1 paddingRight0 paddingLeft0">
                                                        Req.Com
                                                    </div>
                                                    <div class="col-sm-1 Textboxwidth paddingRight0 paddingLeft0">
                                                        Prefer.location
                                                    </div>
                                                    <div class="col-sm-2 Textboxwidth paddingRight0 paddingLeft0">
                                                        purchase type
                                                    </div>
                                                    <div class="col-sm-1 Textboxwidth paddingRight0 paddingLeft0">
                                                        Approved qty
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-sm-4  paddingRight0 ">
                                                        <asp:TextBox runat="server" ID="txtItem" Style="text-transform: uppercase" OnTextChanged="txtItem_TextChanged" Enabled="true" TabIndex="103" AutoPostBack="true" CausesValidation="false" CssClass="form-control"></asp:TextBox>
                                                    </div>
                                                    <div class="col-sm-1 Lwidth paddingLeft0">
                                                        <asp:LinkButton ID="lbtnItemSearch" runat="server" OnClick="lbtnItemSearch_Click" CausesValidation="false">
                                                <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                        </asp:LinkButton>
                                                    </div>
                                                    <div class="col-sm-1 paddingRight0 paddingLeft0 ">
                                                        <asp:TextBox runat="server" ID="txtRequestqty" Enabled="false" onkeypress="return isNumberKey(event)" Style="text-align: right" CausesValidation="false" CssClass="form-control"></asp:TextBox>
                                                    </div>
                                                    <asp:Panel runat="server" DefaultButton="btnRCompnay">
                                                        <div class="col-sm-1 paddingRight0 paddingLeft0 ">
                                                            <asp:TextBox runat="server" ID="txtRequestcompany" Enabled="false" onblur="__doPostBack('txtRequestcompany','OnBlur');" AutoPostBack="true" CausesValidation="false" CssClass="form-control"></asp:TextBox>
                                                        </div>
                                                        <asp:Button ID="btnRCompnay" runat="server" Text="Submit" Style="display: none;" />
                                                    </asp:Panel>

                                                    <asp:Panel runat="server">
                                                        <div class="col-sm-1 Textboxwidth80 paddingRight0 paddingLeft0">
                                                            <%-- onblur="__doPostBack('txtPrefexlocation','OnBlur');"--%>
                                                            <asp:TextBox runat="server" Style="text-transform: uppercase; text-align: right" OnTextChanged="btnPrefexLoc_Click" AutoPostBack="true" ID="txtPrefexlocation" TabIndex="104" CausesValidation="false" CssClass="form-control"></asp:TextBox>
                                                        </div>
                                                        <asp:Button ID="btnPrefexLoc" runat="server" Text="Submit" Style="display: none;" />
                                                    </asp:Panel>
                                                    <div class="col-sm-1 Lwidth paddingLeft0">
                                                        <asp:LinkButton ID="lbtnPrefeslocation" runat="server" CausesValidation="false" OnClick="lbtnPrefeslocation_Click">
                                                <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                        </asp:LinkButton>
                                                    </div>
                                                    <div class="col-sm-2 paddingRight0 paddingLeft0">
                                                        <asp:DropDownList ID="ddlPurchasetype" CausesValidation="false" runat="server" CssClass="form-control">
                                                        </asp:DropDownList>
                                                    </div>
                                                    <asp:Panel runat="server" DefaultButton="btnApproval">

                                                        <div class="col-sm-1  paddingRight0 paddingLeft0">
                                                            <asp:TextBox runat="server" TabIndex="105" ID="txtApprovalqty" onkeypress="return isNumberKey(event)" CausesValidation="false" Style="text-align: right" CssClass="form-control"></asp:TextBox>
                                                        </div>
                                                        <asp:Button ID="btnApproval" runat="server" Text="Submit" OnClick="btnApproval_Click" Style="display: none;" />
                                                    </asp:Panel>
                                                    <div class="col-sm-1 Lwidth paddingLeft5 paddingRight5">
                                                        <asp:LinkButton ID="lbtnAddItem" TabIndex="106" runat="server" CausesValidation="false" OnClick="lbtnAddItem_Click">
                                                <span class="glyphicon glyphicon-plus" aria-hidden="true"></span>
                                                        </asp:LinkButton>
                                                    </div>

                                                </div>

                                            </div>
                                            <div class="col-sm-4 ">
                                                <div class="row">

                                                    <div class="col-sm-3 labelText1 paddingLeft5 paddingRight0 ">
                                                        <asp:Label runat="server" Text="Route" ID="lbllocrout"></asp:Label>
                                                    </div>
                                                    <div class="col-sm-3 paddingLeft5 paddingRight0">
                                                       <%-- <asp:TextBox runat="server" ID="txtroutstock" Style="text-transform: uppercase;" AutoPostBack="true" Enabled="false" CausesValidation="false" CssClass="form-control"></asp:TextBox>--%>
                                                         <asp:DropDownList ID="droproutstock" Visible="true" CausesValidation="false" AutoPostBack="true" runat="server" CssClass="form-control"></asp:DropDownList>
                                                    </div>
                                                    <div class="col-sm-1 paddingLeft0 Lwidth">
                                                        <asp:LinkButton ID="LinkButton2" Visible="false" runat="server" CausesValidation="false">
                                                        <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                        </asp:LinkButton>
                                                    </div>

                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">

                                            <div class="col-sm-8 paddingLeft0">
                                                <div class="panel panel-default">
                                                    <div class="panel-heading paddingtop0 paddingbottom0">
                                                        List of Items
                                                    </div>

                                                    <asp:HiddenField ID="hfScrollPositionitm" Value="0" runat="server" />
                                                    <div id="dvScrollItm" class="panel-body panelscollbar height150" onscroll="setScrollPosition(this.scrollTop);">
                                                        <asp:GridView ID="grdMRNReqItem" ShowHeader="true" CssClass="table table-hover table-striped" runat="server" GridLines="None" ShowHeaderWhenEmpty="True"
                                                            EmptyDataText="No data found..." AutoGenerateColumns="False" OnRowDataBound="grdMRNReqItem_DataBound" OnRowCreated="GridView1_RowCreated" >
                                                            <Columns>
                                                                <%--<asp:buttonfield buttontype="Link" commandname="Add" text="Add" ItemStyle-Width="1px"/>--%>

                                                                <asp:TemplateField HeaderText="" Visible="true">
                                                                    <ItemTemplate>
                                                                        <asp:CheckBox ID="chk_ReqItem" AutoPostBack="true" runat="server" onclick="CheckBoxCheckItem(this);" Checked="false" Width="10px" OnCheckedChanged="chk_ReqItem_CheckedChanged_Click" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:CommandField ShowSelectButton="True" Visible="False" />
                                                                <asp:TemplateField HeaderText="Dis.Item">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="col_ITRI_ITM_CD" runat="server" Text='<%# Bind("ITRI_ITM_CD") %>' Width="120px"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Req.Item">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="col_itri_mitm_cd" runat="server" Text='<%# Bind("itri_mitm_cd") %>' Width="100px"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField Visible="false">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="col_itri_line_no" runat="server" Text='<%# Bind("itri_line_no") %>' Width="10px"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Model" Visible="false">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="col_mi_model" runat="server" Text='<%# Bind("Mst_item_model") %>' Width="80px"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Remark" Visible="false">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="col_itri_note" runat="server" Text='<%# Bind("itri_note") %>' Width="200px"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Reservation #" Visible="false">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="col_Itri_res_noe" runat="server" Text='<%# Bind("Itri_res_no") %>' Width="80px"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Shop stock" Visible="false">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="col_itri_shop_qty" runat="server" Text='<%# Bind("itri_shop_qty") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Right" Width="70px" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="">
                                                                    <ItemStyle HorizontalAlign="Right" Width="25px" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Forward sales" Visible="false">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="col_itri_fd_qty" runat="server" Text='<%# Bind("itri_fd_qty") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Right" Width="85px" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="">
                                                                    <ItemStyle HorizontalAlign="Right" Width="50px" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Buffer limit" Visible="false">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="col_itri_buffer" runat="server" Text='<%# Bind("itri_buffer") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Right" Width="68px" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="">
                                                                    <ItemStyle HorizontalAlign="Right" Width="30px" />
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="Req.qty">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="col_itri_bqty" runat="server" Text='<%# Bind("itri_bqty") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Right" Width="50px" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="">
                                                                    <ItemStyle HorizontalAlign="Right" Width="5px" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Prev.Sales qty" Visible="false">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="col_Itri_Prev_sales_qty" runat="server" Text='<%# Bind("Itri_Prev_sales_qty") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Right" Width="85px" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Company">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="col_itri_com" runat="server" Text='<%# Bind("itri_com") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Center" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Loc">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="col_itri_loc" runat="server" Text='<%# Bind("itri_loc") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Center" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="App.Qty">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="col_itri_app_qty" runat="server" Text='<%# Bind("itri_app_qty") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Right" Width="50px" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Status">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="col_Approv_status" runat="server" Text='<%# Bind("Approv_status") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Status" Visible="false">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="col_Price" runat="server" Text='<%# Bind("Itri_unit_price") %>' Visible="false"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField Visible="true">
                                                                    <ItemTemplate>
                                                                        <asp:LinkButton ID="lbtncancelitem" runat="server" CausesValidation="false" OnClick="lbtncancelitem_Click">
                                                                     <span class="glyphicon glyphicon-remove" aria-hidden="true"></span>
                                                                        </asp:LinkButton>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Model" Visible="true">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblTmp_itm_model" runat="server" Text='<%# Bind("Tmp_itm_model") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="UOM" Visible="true">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblTmp_itm_uom" runat="server" Text='<%# Bind("Tmp_itm_uom") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                            </Columns>
                                                        </asp:GridView>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-sm-4 paddingLeft0 paddingRight0">
                                                <div class="panel panel-default">
                                                    <div class="panel-heading paddingtop0 paddingbottom0">
                                                        Stock Availability
                                                    </div>
                                                    <div class="panel-body panelscollbar  height150">
                                                        <asp:GridView ID="grdInventoryBalance" CssClass="table table-hover table-striped" runat="server" GridLines="None" ShowHeaderWhenEmpty="True"
                                                            EmptyDataText="No data found..." AutoGenerateColumns="False">
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="Loc">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="col_INL_LOC" runat="server" Text='<%# Bind("INL_LOC") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Status" Visible="false">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="col_INL_ITM_STUS" runat="server" Text='<%# Bind("INL_ITM_STUS") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Status">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="Mis_desc" runat="server" Text='<%# Bind("Mis_desc") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="In hand">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="col_BL_QTY" runat="server" Text='<%# Bind("inl_qty") %>'></asp:Label>
                                                                    </ItemTemplate>

                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="Res. Qty">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="col_INL_RES_QTY" runat="server" Text='<%# Bind("INL_RES_QTY") %>'></asp:Label>
                                                                    </ItemTemplate>

                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="Fre.Qty">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="col_INL_FREE_QTY" runat="server" Text='<%# Bind("INL_FREE_QTY") %>'></asp:Label>
                                                                    </ItemTemplate>

                                                                </asp:TemplateField>

                                                            </Columns>
                                                        </asp:GridView>
                                                    </div>
                                                </div>
                                            </div>

                                        </div>
                                        <div class="row ">

                                            <div class="col-sm-8">
                                                <div class="col-sm-4">
                                                </div>
                                                <div class="col-sm-2">
                                                    Total:
                                                </div>
                                                <div class="col-sm-2">
                                                    <asp:Label ID="lbtnTotalreq" runat="server"></asp:Label>
                                                </div>
                                                <div class="col-sm-2 paddingLeft0">
                                                    <asp:Button ID="btncancel" runat="server" Text="Cancel all Item" class="btn btn-danger btn-xs" OnClick="btncancel_Click" />
                                                </div>

                                                <div class="col-sm-2 paddingLeft0">
                                                    <asp:Button ID="btnApproveall" runat="server" Text="Approve all Item" class="btn btn-default btn-xs" OnClick="btnApproveall_Click" />
                                                </div>
                                            </div>
                                            <div class="col-sm-4">
                                                <div class="col-sm-5 paddingLeft0">
                                                </div>
                                                <div class="col-sm-7 paddingLeft0">
                                                    <asp:LinkButton ID="lbtnviewrout" runat="server" OnClick="lbtnviewrout_Click">View Route Stock
                                                        
                                                    </asp:LinkButton>
                                                </div>
                                            </div>

                                        </div>
                                    </div>

                                    <div class="col-sm-3 ">
                                        <div class="row">
                                            <div class="panel panel-default">
                                                <div class="panel-body">
                                                    <div class="row">
                                                        <div class="col-sm-3 paddingRight0">
                                                            Date :
                                                        </div>
                                                        <div class="col-sm-7">
                                                            <asp:Label runat="server" ID="lblSearchDate" Text="..." ForeColor="#A513D0"></asp:Label>
                                                        </div>
                                                    </div>
                                                    <div class="row height5">
                                                    </div>
                                                    <div class="row">
                                                        <div class="col-sm-3 paddingRight0">
                                                            Request by:
                                                        </div>
                                                        <div class="col-sm-7">
                                                            <<asp:Label runat="server" ID="lblShowroom" Text="..." ForeColor="#A513D0"></asp:Label>>
                                                             <asp:Label runat="server" ID="lblshowroomname" Text="..." ForeColor="#A513D0"></asp:Label>
                                                        </div>
                                                    </div>
                                                    <div class="row height5">
                                                    </div>
                                                    <div class="row">
                                                        <div class="col-sm-3 paddingRight0">
                                                            Request # :
                                                        </div>
                                                        <div class="col-sm-7">
                                                            <asp:Label runat="server" ID="lblrequestNo" Text="..." ForeColor="#A513D0"></asp:Label>
                                                        </div>
                                                    </div>
                                                    <div class="row height5">
                                                    </div>
                                                    <div class="row">
                                                        <div class="col-sm-3 paddingRight0">
                                                            Req. type :
                                                        </div>
                                                        <div class="col-sm-3">
                                                            <asp:Label runat="server" ID="lblRequesttype" Text="..." ForeColor="#A513D0"></asp:Label>
                                                        </div>
                                                         <div class="col-sm-3 paddingRight0">
                                                            Pref. Loc :
                                                        </div>
                                                        <div class="col-sm-3">
                                                            <asp:Label runat="server" ID="lblPreferloc" Text="..." ForeColor="#A513D0"></asp:Label>
                                                        </div>
                                                    </div>
                                                    <div class="row height5">
                                                    </div>
                                                    <div class="row">
                                                        <div class="col-sm-3 paddingRight0">
                                                            Max Acc(HS):
                                                        </div>
                                                        <div class="col-sm-3">
                                                            <asp:Label runat="server" ID="lbmaxhpaccounts" Text="..." ForeColor="#A513D0"></asp:Label>
                                                        </div>
                                                         <div class="col-sm-3 paddingRight0">
                                                           Curr Acc :
                                                        </div>
                                                        <div class="col-sm-3">
                                                            <asp:Label runat="server" ID="lbcurrenthpacc" Text="..." ForeColor="#A513D0"></asp:Label>
                                                        </div>
                                                    </div>
                                                   <%-- <div class="row height5">
                                                    </div>--%>
                                                     <div class="row">
                                                        <div class="col-sm-3 paddingRight0">
                                                            Max Val(HS):
                                                        </div>
                                                        <div class="col-sm-3">
                                                            <asp:Label runat="server" ID="lbmaxval" Text="..." ForeColor="#A513D0"></asp:Label>
                                                        </div>
                                                         <div class="col-sm-3 paddingRight0">
                                                           Curr Val :
                                                        </div>
                                                        <div class="col-sm-3">
                                                            <asp:Label runat="server" ID="lbcurrentval" Text="..." ForeColor="#A513D0"></asp:Label>
                                                        </div>
                                                    </div>
                                                   <%-- <div class="row height5">
                                                    </div>--%>
                                                    <div class="row">
                                                        <div class="col-sm-3 paddingRight0">
                                                            Autho. by :
                                                        </div>
                                                        <div class="col-sm-3">
                                                            <asp:Label runat="server" ID="lblAuthorizedby" Text="..." ForeColor="#A513D0"></asp:Label>
                                                        </div>
                                                         <div class="col-sm-3 paddingRight0">
                                                            Company :
                                                        </div>
                                                        <div class="col-sm-3">
                                                            <asp:Label runat="server" ID="lblCompany" Text="..." ForeColor="#A513D0"></asp:Label>
                                                        </div>
                                                    </div>
                                                    <div class="row height5">
                                                    </div>
                                                   
                                                    <div class="row">
                                                        <div class="col-sm-3 labelText1  ">
                                                            Delivery Date
                                                        </div>
                                                        <div class="col-sm-4">
                                                            <asp:TextBox ID="txtDeliverDate" OnTextChanged="txtDeliverDate_TextChanged" TabIndex="100" AutoPostBack="true" runat="server" CssClass="form-control" onkeypress="return RestrictSpace()"
                                                                Format="dd/MMM/yyyy">
                                                            </asp:TextBox>


                                                        </div>
                                                        <div class="col-sm-2 paddingLeft0 paddingRight0">
                                                            <asp:LinkButton ID="lbtnDeliverDate" runat="server" CausesValidation="false">
                                                            <span class="glyphicon glyphicon-calendar" aria-hidden="true"></span>
                                                            </asp:LinkButton>
                                                            <asp:CalendarExtender ID="CalendarExtender3" OnClientDateSelectionChanged="checkDate" runat="server" TargetControlID="txtDeliverDate"
                                                                PopupButtonID="lbtnDeliverDate" Format="dd/MMM/yyyy">
                                                            </asp:CalendarExtender>
                                                        </div>
                                                    </div>
                                                    <div class="row height10">
                                                    </div>

                                                    <div class="row">
                                                        <div class="col-sm-3 paddingRight0">
                                                            Ramark :
                                                        </div>
                                                        <div class="col-sm-9">
                                                            <asp:TextBox runat="server" TextMode="MultiLine" ID="txtremark" TabIndex="101" CausesValidation="false" CssClass="form-control"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="row">
                                                        <div class="col-sm-4">
                                                            Base Item Code
                                                        </div>
                                                        <div class="col-sm-7">
                                                            <asp:Label runat="server" ID="lblbaseitem" Text="..." ForeColor="#A513D0"></asp:Label>
                                                        </div>

                                                    </div>
                                                    <%--<div class="row height5">
                                                    </div>--%>
                                                    <div class="row">
                                                        <div class="col-sm-3">
                                                            Item Des.
                                                        </div>
                                                        <div class="col-sm-8">
                                                            <asp:Label runat="server" ID="lblitemdes" Text="..." ForeColor="#A513D0"></asp:Label>
                                                        </div>

                                                    </div>
                                                   <%-- <div class="row height5">
                                                    </div>--%>
                                                    <div class="row">
                                                        <div class="col-sm-3   ">
                                                            Model
                                                        </div>
                                                        <div class="col-sm-8">
                                                            <asp:Label runat="server" ID="lblModel" Text="..." ForeColor="#A513D0"></asp:Label>
                                                        </div>

                                                    </div>
                                                    <div class="row">
                                                        <div class="col-sm-12">
                                                            <div class="col-sm-6 padding0">
                                                                <div class="col-sm-6 padding0">
                                                                    No of accounts(Monthly) :
                                                                </div>
                                                                <div class="col-sm-6 padding0">
                                                                    <asp:Label ID="lblMonthlyAcc" Text="" runat="server" />
                                                                </div>
                                                            </div>
                                                            <div class="col-sm-6 padding0">
                                                                <div class="col-sm-6 padding0">
                                                                    Value(Monthly) :
                                                                </div>
                                                                <div class="col-sm-6 padding0">
                                                                    <asp:Label ID="lblMonthlyVal" Text="" runat="server" />
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>

                                                </div>
                                            </div>
                                        </div>

                                        <div class="row">
                                            <div class="panel panel-default">
                                                <div class="panel-body">
                                                    <div class="row height5">
                                                    </div>
                                                    <div class="row">
                                                        <div class="col-sm-1   ">
                                                            <asp:CheckBox ID="chkGIT" runat="server" Width="5px"></asp:CheckBox>
                                                        </div>
                                                        <div class="col-sm-5">
                                                            With GIT Shop stock
                                                        </div>
                                                        <div class="col-sm-5">
                                                            <div class="row">
                                                                <div class="col-sm-7">
                                                                    Shop stock
                                                                </div>
                                                                <div class="col-sm-4">
                                                                    <asp:Label runat="server" ID="lblshop" Text="..." ForeColor="#A513D0"></asp:Label>
                                                                </div>
                                                            </div>
                                                            <div class="row">
                                                                <div class="col-sm-7">
                                                                    GIT 
                                                                </div>
                                                                <div class="col-sm-4">
                                                                    <asp:Label runat="server" ID="lblGIT" Text="..." ForeColor="#A513D0"></asp:Label>
                                                                </div>
                                                            </div>
                                                            <div class="row">
                                                                 <div class="col-sm-7">
                                                                    Total
                                                                </div>
                                                                <div class="col-sm-4">
                                                                    <asp:Label runat="server" ID="lblshopstock" Text="..." ForeColor="#A513D0"></asp:Label>
                                                                </div>
                                                            </div>

                                                            
                                                        </div>
                                                    </div>
                                                    
                                                    <div class="row">
                                                        <div class="col-sm-1   ">
                                                            <asp:CheckBox ID="chkFwdsale" Checked="true" runat="server" Width="5px"></asp:CheckBox>
                                                        </div>
                                                        <div class="col-sm-5">
                                                            With Forward sales
                                                        </div>
                                                        <div class="col-sm-5">
                                                            <asp:Label runat="server" ID="lblForwardsale" Text="..." ForeColor="#A513D0"></asp:Label>
                                                        </div>
                                                    </div>
                                                    <div class="row">
                                                        <div class="col-sm-1 ">
                                                            <asp:CheckBox ID="chkPresaleqty" runat="server" Width="5px"></asp:CheckBox>
                                                        </div>
                                                        <div class="col-sm-5 ">
                                                            Previous Sales Qty
                                                        </div>
                                                        <div class="col-sm-5 ">
                                                           <asp:Label ID="lbpreqty" runat="server" ForeColor="#A513D0"></asp:Label>
                                                        </div>
                                                    </div>
                                                    <div class="row height5">
                                                    </div>
                                                    <div class="row">
                                                        <asp:Panel runat="server" Visible="true">
                                                            <div class="col-sm-2 labelText1">
                                                                Season 
                                                            </div>
                                                            <div class="col-sm-3 labelText1">
                                                                <asp:DropDownList ID="ddlseason" Visible="false" CausesValidation="false" AutoPostBack="true" runat="server" CssClass="form-control">
                                                                </asp:DropDownList>
                                                            </div>
                                                            <div class="col-sm-3 labelText1">
                                                                Buffer limit

                                                            </div>
                                                            <div class="col-sm-4">
                                                                <asp:Label ID="lblBufferLimit" runat="server" Text="..." ForeColor="#A513D0"></asp:Label>
                                                            </div>
                                                        </asp:Panel>
                                                    </div>
                                                    <div class="row">
                                                        <div class="col-sm-11">
                                                            <asp:GridView ID="grdSeasson" CssClass="table table-hover table-striped" runat="server" GridLines="None" ShowHeaderWhenEmpty="True"
                                                                EmptyDataText="No data found..." AutoGenerateColumns="False">
                                                                <Columns>
                                                                    <asp:TemplateField HeaderText="Season">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="col_mbc_season" runat="server" Text='<%# Bind("mbc_season") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText=" Buffer limit">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="col_MBC_QTY" runat="server" Text='<%# Bind("MBC_QTY") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                </Columns>
                                                            </asp:GridView>
                                                        </div>
                                                    </div>

                                                    <div class="row">
                                                        <div class="col-sm-4 labelText1">
                                                            Last Month Sale:
                                                        </div>
                                                        <div class="col-sm-2 labelText1">
                                                            <asp:Label ID="lblsale" runat="server"></asp:Label>
                                                        </div>

                                                    </div>
                                                
                                                     <div class="row">
                                                        <div class="col-sm-4 labelText1">
                                                            Advance Reciept:
                                                        </div>
                                                        <div class="col-sm-2 labelText1">
                                                            <asp:Label ID="lbadvancerec" runat="server"></asp:Label>
                                                        </div>
                                                         

                                                    </div>
                                               <div class="row">
                                                        
                                                         <div class="col-sm-4 labelText1">
                                                            No of Rec:
                                                        </div>
                                                        <div class="col-sm-2 labelText1">
                                                            <asp:Label ID="lbreccount" runat="server"></asp:Label>
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
                    <div class="row">
                        <div class="col-sm-12">
                            <div class="row" runat="server" id="divMrnBalance">
                                <div class="col-sm-12">
                                    <div class="col-sm-2 padding0 " style="width:20%">
                                        <div class="panel panel-default">
                                            <div class="row">
                                                <div class="col-sm-12">
                                                    <div class="col-sm-6 paddingRight0" >
                                                        <strong>Approved Bank Guaranty</strong>
                                                    </div>
                                                    <div class="col-sm-6 labelText1" style="padding-right:3px; ">
                                                        <asp:TextBox Enabled="false" CssClass="form-control text-right" runat="server" ID="txtBGValue" />
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-12">
                                                    <div class="col-sm-6 labelText1">
                                                        <strong>Insured Value</strong>
                                                    </div>
                                                    <div class="col-sm-6 labelText1" style="padding-right:3px;">
                                                        <asp:TextBox Enabled="false" CssClass="form-control text-right" runat="server" ID="txtStockVal" />
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-2 padding0 " style="width:12%">
                                        <div class="panel panel-default">
                                            <div class="row">
                                                <div class="col-sm-12 text-center labelText1">
                                                    <strong>Showroom Stock Value</strong>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-12">
                                                    <div class="row">
                                                    <div class="col-sm-12 labelText1" style="padding-right:20px; padding-left:20px;">
                                                        <asp:TextBox Enabled="false" CssClass="form-control text-right" runat="server" ID="txtShStkVal" />
                                                    </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-2 padding0 " style="width:12%">
                                        <div class="panel panel-default">
                                            <div class="row">
                                                <div class="col-sm-12 text-center labelText1">
                                                    <strong>Approved MRN Value</strong>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-12">
                                                    <div class="row">
                                                    <div class="col-sm-12 labelText1" style="padding-right:20px; padding-left:20px;">
                                                        <asp:TextBox Enabled="false" CssClass="form-control  text-right" runat="server" ID="txtAppMrnVal" />
                                                    </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-2 padding0 " style="width:12%">
                                        <div class="panel panel-default">
                                            <div class="row">
                                                <div class="col-sm-12 text-center labelText1">
                                                    <strong>Free Value</strong>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-12">
                                                    <div class="row">
                                                    <div class="col-sm-12 labelText1" style="padding-right:20px; padding-left:20px;">
                                                        <asp:TextBox Enabled="false" CssClass="form-control  text-right" runat="server" ID="txtFreeVal" />
                                                    </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-2 padding0 " style="width:12%">
                                        <div class="panel panel-default">
                                            <div class="row">
                                                <div class="col-sm-12 text-center labelText1">
                                                    <strong>Current MRN Value</strong>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-12">
                                                    <div class="row">
                                                    <div class="col-sm-12 labelText1" style="padding-right:20px; padding-left:20px;">
                                                        <asp:TextBox Enabled="false" CssClass="form-control  text-right" runat="server" ID="txtCurrMrnVal" />
                                                    </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-2 padding0 " style="width:8%">
                                        <div class="panel panel-default">
                                            <div class="row">
                                                <div class="col-sm-12 text-center labelText1">
                                                    <strong>Main Item Qty</strong>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-12">
                                                    <div class="row">
                                                    <div class="col-sm-12 labelText1" style="padding-right:20px; padding-left:20px;">
                                                        <asp:TextBox Enabled="false" CssClass="form-control  text-right" runat="server" ID="txtTotMrnCom" />
                                                    </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-2 padding0 " style="width:12%">
                                        <div class="panel panel-default">
                                            <div class="row">
                                                <div class="col-sm-12 text-center labelText1">
                                                    <strong>Component Item Quantity</strong>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-12">
                                                    <div class="row">
                                                    <div class="col-sm-12 labelText1" style="padding-right:20px; padding-left:20px;">
                                                        <asp:TextBox Enabled="false" CssClass="form-control  text-right" runat="server" ID="txtTotQtyMain" />
                                                    </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-2 padding0 " style="width:10%">
                                        <div class="panel panel-default">
                                            <div class="row">
                                                <div class="col-sm-12 text-center labelText1">
                                                    <strong>Total Retail value</strong>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-12">
                                                    <div class="row">
                                                    <div class="col-sm-12 labelText1" style="padding-right:20px; padding-left:20px;">
                                                        <asp:TextBox Enabled="false" CssClass="form-control  text-right" runat="server" ID="txtSalesValues" />
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




    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <asp:Button ID="Button4" runat="server" Text="Button" Style="display: none;" />
            <asp:ModalPopupExtender ID="ApprovedPopup" runat="server" Enabled="True" TargetControlID="Button4"
                PopupControlID="pnlApprovepopup" PopupDragHandleControlID="PopupHeader" Drag="true" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
        <ContentTemplate>
            <asp:Panel runat="server" ID="pnlApprovepopup" DefaultButton="lbtnSearch">
                <div runat="server" id="Div5" class="panel panel-default height400 width1085">

                    <asp:Label ID="Label3" runat="server" Text="Label" Visible="false"></asp:Label>
                    <div class="panel panel-default">
                        <div class="panel-heading">
                            <asp:LinkButton ID="LinkButton3" runat="server">
                             <span class="glyphicon glyphicon-remove" aria-hidden="true">Close</span>
                            </asp:LinkButton>
                            <%--<span>Commen Search</span>--%>
                            <div class="col-sm-11">
                            </div>
                            <div class="col-sm-1">
                            </div>
                        </div>
                        <div class="panel-body">
                            <div class="row">
                                <div class="col-sm-12 ">
                                    <div class="panel panel-default">
                                        <div class="panel-heading">
                                            <div class="row  ">
                                                <div class="col-sm-12  ">
                                                    <div class="col-sm-5 paddingLeft5">
                                                        Approved Item Details
                                                    </div>


                                                </div>
                                            </div>
                                        </div>
                                        <div class="panel-body">
                                            <div class="col-sm-12">
                                                <div class="row">


                                                    <div class="col-sm-8 paddingLeft0">

                                                        <div class="row">
                                                            <div class="col-sm-4 paddingRight0">
                                                                Item code
                                                            </div>

                                                            <div class="col-sm-1 Lwidth paddingLeft0">
                                                            </div>
                                                            <div class="col-sm-1 paddingRight0 paddingLeft0">
                                                                Req.qty
                                                            </div>
                                                            <div class="col-sm-1 paddingRight0 paddingLeft0">
                                                                Req.Com
                                                            </div>
                                                            <div class="col-sm-1 Textboxwidth paddingRight0 paddingLeft0">
                                                                Prefer.location
                                                            </div>
                                                            <div class="col-sm-2 Textboxwidth paddingRight0 paddingLeft0">
                                                                purchase type
                                                            </div>
                                                            <div class="col-sm-1 Textboxwidth paddingRight0 paddingLeft0">
                                                                Approved qty
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-sm-4  paddingRight0 ">
                                                                <asp:TextBox runat="server" ID="txtpopupItem" Style="text-transform: uppercase" Enabled="false" AutoPostBack="true" CausesValidation="false" CssClass="form-control"></asp:TextBox>
                                                            </div>
                                                            <div class="col-sm-1 Lwidth paddingLeft0">
                                                                <asp:LinkButton ID="sd" runat="server" Visible="false" CausesValidation="false">
                                                <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                                </asp:LinkButton>
                                                            </div>
                                                            <div class="col-sm-1 paddingRight0 paddingLeft0 ">
                                                                <asp:TextBox runat="server" ID="txtpopupRequestqty" Enabled="false" onkeypress="return isNumberKey(event)" Style="text-align: right" CausesValidation="false" CssClass="form-control"></asp:TextBox>
                                                            </div>
                                                            <div class="col-sm-1 paddingRight0 paddingLeft0 ">
                                                                <asp:TextBox runat="server" ID="txtpopupIcompany" Enabled="false" onkeypress="return isNumberKey(event)" Style="text-align: right" CausesValidation="false" CssClass="form-control"></asp:TextBox>
                                                            </div>


                                                            <asp:Panel runat="server">
                                                                <div class="col-sm-1 paddingRight0 paddingLeft0">
                                                                    <%-- onblur="__doPostBack('txtPrefexlocation','OnBlur');"--%>
                                                                    <asp:TextBox runat="server" Style="text-transform: uppercase; text-align: right" OnTextChanged="txtPrefexlocationpopup_TextChanged" AutoPostBack="true" ID="txtPrefexlocationpopup" CausesValidation="false" CssClass="form-control"></asp:TextBox>
                                                                </div>
                                                                <asp:Button ID="Button5" runat="server" Text="Submit" Style="display: none;" />
                                                            </asp:Panel>
                                                            <div class="col-sm-1 Lwidth paddingLeft0">
                                                                <asp:LinkButton ID="LinkButton5" Visible="false" runat="server" CausesValidation="false">
                                                <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                                </asp:LinkButton>
                                                            </div>
                                                            <div class="col-sm-2">
                                                                <asp:DropDownList ID="ddlPurchasetypepopup" CausesValidation="false" runat="server" CssClass="form-control">
                                                                </asp:DropDownList>
                                                            </div>
                                                            <asp:Panel runat="server" DefaultButton="btnApproval">

                                                                <div class="col-sm-1  paddingRight0 paddingLeft0">
                                                                    <asp:TextBox runat="server" ID="txtpopupApprovalqty" onkeypress="return isNumberKey(event)" CausesValidation="false" Style="text-align: right" CssClass="form-control"></asp:TextBox>
                                                                </div>
                                                                <asp:Button ID="Button6" runat="server" Text="Submit" OnClick="btnApproval_Click" Style="display: none;" />
                                                            </asp:Panel>
                                                            <div class="col-sm-1 Lwidth paddingLeft5 paddingRight5">
                                                                <asp:LinkButton ID="lbtnappAddItem" TabIndex="106" runat="server" CausesValidation="false" OnClick="lbtnappAddItem_Click">
                                                <span class="glyphicon glyphicon-plus" aria-hidden="true"></span>
                                                                </asp:LinkButton>
                                                            </div>

                                                        </div>

                                                    </div>

                                                </div>
                                                <div class="row">
                                                    <div class="col-sm-8 paddingLeft0">

                                                        <div class="panel-body  panelscollbar height200">
                                                            <asp:GridView ID="grdApprovMRNitem" ShowHeader="true" CssClass="table table-hover table-striped" runat="server" GridLines="None" ShowHeaderWhenEmpty="True"
                                                                EmptyDataText="No data found..." AutoGenerateColumns="False">
                                                                <Columns>
                                                                    <%--<asp:buttonfield buttontype="Link" commandname="Add" text="Add" ItemStyle-Width="1px"/>--%>

                                                                    <asp:TemplateField HeaderText="" Visible="true">
                                                                        <ItemTemplate>
                                                                            <asp:CheckBox ID="chk_AReqItem" AutoPostBack="true" runat="server" onclick="CheckBoxCheckItem(this);" Checked="false" Width="10px" OnCheckedChanged="chk_AReqItem_CheckedChanged_Click" />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:CommandField ShowSelectButton="True" Visible="False" />
                                                                    <asp:TemplateField HeaderText="Dis.Item">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="col_AITRI_ITM_CD" runat="server" Text='<%# Bind("ITRI_ITM_CD") %>' Width="80px"></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Req.Item">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="col_Aitri_mitm_cd" runat="server" Text='<%# Bind("itri_mitm_cd") %>' Width="80px"></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField Visible="false">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="col_Aitri_line_no" runat="server" Text='<%# Bind("itri_line_no") %>' Width="10px"></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Model" Visible="false">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="col_Ami_model" runat="server" Text='<%# Bind("Mst_item_model") %>' Width="80px"></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Remark" Visible="false">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="col_Aitri_note" runat="server" Text='<%# Bind("itri_note") %>' Width="200px"></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Reservation #" Visible="false">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="col_Itri_res_noe" runat="server" Text='<%# Bind("Itri_res_no") %>' Width="80px"></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Shop stock" Visible="false">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="col_Aitri_shop_qty" runat="server" Text='<%# Bind("itri_shop_qty") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <ItemStyle HorizontalAlign="Right" Width="70px" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="">
                                                                        <ItemStyle HorizontalAlign="Right" Width="25px" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Forward sales" Visible="false">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="col_Aitri_fd_qty" runat="server" Text='<%# Bind("itri_fd_qty") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <ItemStyle HorizontalAlign="Right" Width="85px" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="">
                                                                        <ItemStyle HorizontalAlign="Right" Width="50px" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Buffer limit" Visible="false">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="col_Aitri_buffer" runat="server" Text='<%# Bind("itri_buffer") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <ItemStyle HorizontalAlign="Right" Width="68px" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="">
                                                                        <ItemStyle HorizontalAlign="Right" Width="30px" />
                                                                    </asp:TemplateField>

                                                                    <asp:TemplateField HeaderText="Req.qty">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="col_Aitri_bqty" runat="server" Text='<%# Bind("itri_qty") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <ItemStyle HorizontalAlign="Right" Width="50px" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="">
                                                                        <ItemStyle HorizontalAlign="Right" Width="5px" />
                                                                    </asp:TemplateField>
                                                                   
                                                                    <asp:TemplateField HeaderText="Company">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="col_Aitri_com" runat="server" Text='<%# Bind("itri_com") %>'></asp:Label>
                                                                        </ItemTemplate>

                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Loc">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="col_Aitri_loc" runat="server" Text='<%# Bind("itri_loc") %>'></asp:Label>
                                                                        </ItemTemplate>

                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="App.Qty">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="col_Aitri_app_qty" runat="server" Text='<%# Bind("itri_app_qty") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <ItemStyle HorizontalAlign="Right" Width="50px" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Status" Visible="false">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="col_Approv_status" runat="server" Text='<%# Bind("Approv_status") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Pick.qty">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="col_pick_qty" runat="server" Text='<%# Bind("Itri_mqty") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <ItemStyle HorizontalAlign="Right" Width="50px" />
                                                                    </asp:TemplateField>
                                                                     <asp:TemplateField HeaderText="Res No">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="col_res_no" runat="server" Text='<%# Bind("itri_res_no") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField>
                                                                        <ItemTemplate>
                                                                            <asp:LinkButton ID="lbtngrdItemsDalete" runat="server" CausesValidation="false" OnClick="lbtngrdItemsDalete_Click1">
                                                                                <span aria-hidden="true" class="glyphicon glyphicon-trash"></span>
                                                                            </asp:LinkButton>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <%-- <asp:TemplateField>
                                                                        <ItemTemplate>
                                                                            <asp:LinkButton ID="lbtngrdPickqty" runat="server" CausesValidation="false" OnClick="lbtngrdPickqty_Click">
                                                                                <span aria-hidden="true" class="glyphicon glyphicon-arrow-right"></span>
                                                                            </asp:LinkButton>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>--%>
                                                                </Columns>
                                                            </asp:GridView>
                                                        </div>
                                                    </div>
                                                    <div class="col-sm-4 ">
                                                        <div class="row">
                                                            <div class="col-sm-4">
                                                                Shop stock
                                                            </div>
                                                            <div class="col-sm-1">
                                                                :
                                                            </div>
                                                            <div class="col-sm-2">
                                                                <asp:Label ID="lblpopupshopstock" runat="server"></asp:Label>
                                                            </div>
                                                        </div>
                                                        <div class="row height10">
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-sm-4">
                                                                Forward sales
                                                            </div>
                                                            <div class="col-sm-1">
                                                                :
                                                            </div>
                                                            <div class="col-sm-2">
                                                                <asp:Label ID="lblpopupForwardsale" runat="server"></asp:Label>
                                                            </div>
                                                        </div>
                                                        <div class="row height10">
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-sm-4">
                                                                Buffer limit
                                                            </div>
                                                            <div class="col-sm-1">
                                                                :
                                                            </div>
                                                            <div class="col-sm-2">
                                                                <asp:Label ID="lblpopupBufferLimit" runat="server"></asp:Label>
                                                            </div>
                                                        </div>
                                                                                                                <div class="row height10">
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-sm-4">
                                                                Pick Qty :
                                                            </div>
                                                            <div class="col-sm-1">
                                                                :
                                                            </div>
                                                            <div class="col-sm-2">
                                                                <asp:Label ID="lbpickqty" runat="server"></asp:Label>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-sm-6">
                                                    </div>
                                                    <div class="col-sm-1">
                                                        Total
                                                    </div>
                                                    <div class="col-sm-2">
                                                        <asp:Label ID="lbtnapptotal" runat="server"></asp:Label>
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

    <asp:UpdatePanel ID="UpdatePanel21" runat="server">
        <ContentTemplate>
            <asp:Button ID="Button7" runat="server" Text="Button" Style="display: none;" />
            <asp:ModalPopupExtender ID="PopupConfBox" runat="server" Enabled="True" TargetControlID="Button7"
                PopupControlID="pnlConfBox" PopupDragHandleControlID="PopupHeader" Drag="true" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>
        </ContentTemplate>
    </asp:UpdatePanel>
     <asp:UpdateProgress ID="UpdateProgress5" DisplayAfter="10"
        runat="server" AssociatedUpdatePanelID="UpdatePanel25">
        <ProgressTemplate>
            <div class="divWaiting">
                <asp:Label ID="lblWait8" runat="server"
                    Text="Please wait... " />
                <asp:Image ID="imgWait8" runat="server"
                    ImageAlign="Middle" ImageUrl="~/images/banners/hidden_progress_bar.gif" />
            </div>
        </ProgressTemplate>

    </asp:UpdateProgress>
  

    <asp:UpdatePanel ID="UpdatePanel25" runat="server">
        <ContentTemplate>
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
        </ContentTemplate>
    </asp:UpdatePanel>



    <asp:UpdatePanel ID="UpdatePanel8" runat="server">
        <ContentTemplate>
            <asp:Button ID="Button3" runat="server" Text="Button" Style="display: none;" />
            <asp:ModalPopupExtender ID="UserPopup" runat="server" Enabled="True" TargetControlID="Button3"
                PopupControlID="pnlpopup" PopupDragHandleControlID="PopupHeader" Drag="true" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>

        </ContentTemplate>

    </asp:UpdatePanel>
    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
        <ContentTemplate>
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
                                <asp:Label ID="lblpanelname" runat="server"></asp:Label>
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
                                    <asp:Panel runat="server" ID="searpnl">
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
                                    </asp:Panel>
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

        </ContentTemplate>

    </asp:UpdatePanel>

    <asp:UpdatePanel ID="UpdatePanel5" runat="server">
        <ContentTemplate>
            <asp:Button ID="Button2" runat="server" Text="Button" Style="display: none;" />
            <asp:ModalPopupExtender ID="UserDPopoup" runat="server" Enabled="True" TargetControlID="Button2"
                PopupControlID="pnlDpopup" PopupDragHandleControlID="PopupHeader" Drag="true" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>

        </ContentTemplate>

    </asp:UpdatePanel>
    <asp:UpdatePanel ID="UpdatePanel7" runat="server">
        <ContentTemplate>
            <asp:Panel runat="server" ID="pnlDpopup" DefaultButton="lbtnSearch">
                <div runat="server" id="Div2" class="panel panel-default height400 width700">

                    <asp:Label ID="Label2" runat="server" Text="Label" Visible="false"></asp:Label>
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
                            <div class="col-sm-12" id="Div4" runat="server">
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
                                            <asp:TextBox runat="server" TabIndex="200" Enabled="true" ID="txtFDate" CausesValidation="false" CssClass="form-control"></asp:TextBox>
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
                                            <asp:TextBox runat="server" Enabled="true" TabIndex="201" ID="txtTDate" CausesValidation="false" CssClass="form-control"></asp:TextBox>
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
                                        <asp:ScriptManagerProxy ID="ScriptManagerProxy5" runat="server"></asp:ScriptManagerProxy>
                                        <asp:UpdatePanel ID="UpdatePanel11" runat="server">
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
                                        <asp:ScriptManagerProxy ID="ScriptManagerProxy7" runat="server"></asp:ScriptManagerProxy>
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

    <asp:UpdatePanel ID="UpdatePanel15" runat="server">

        <ContentTemplate>

            <asp:Button ID="Button8" runat="server" Text="Button" Style="display: none;" />
            <asp:ModalPopupExtender ID="excelUpload" runat="server" Enabled="True" TargetControlID="Button8"
                PopupControlID="pnlexcel" CancelControlID="lbtnexClose" PopupDragHandleControlID="PopupHeader" Drag="true" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>


        </ContentTemplate>

    </asp:UpdatePanel>

    <asp:Panel runat="server" ID="pnlexcel">
        <div runat="server" id="dv" class="panel panel-default height45 width700 ">

            <div class="panel panel-default">
                <div class="panel-heading">
                    <asp:LinkButton ID="lbtnexClose" runat="server">
                             <span class="glyphicon glyphicon-remove" aria-hidden="true">Close</span>
                    </asp:LinkButton>
                    <%--<span>Commen Search</span>--%>
                    <div class="col-sm-11">
                        <strong>Excel Upload</strong>
                    </div>
                    <div class="col-sm-1">
                    </div>
                </div>
                <div class="panel-body">
                    <div class="col-sm-12 paddingLeft0 paddingRight0">
                        <div class="row">
                            <div class="col-sm-12">
                                <asp:Label ID="Label4" runat="server" ForeColor="Red" Text="Label" Visible="false"></asp:Label>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-12 height10">
                            </div>
                        </div>

                        <div class="row">

                            <div class="col-sm-12" id="Div6" runat="server">
                                <div class="col-sm-5 paddingRight5">
                                    <asp:FileUpload ID="fileupexcelupload" runat="server" />

                                </div>

                                <div class="col-sm-2 paddingRight5">
                                    <asp:Button ID="btnUpload1" class="btn btn-warning btn-xs" runat="server" Text="Upload"
                                        OnClick="btnUpload_Click" />
                                </div>


                            </div>
                              <div class="col-sm-12 ">
                             <asp:Label ID="lblExcelUploadError" runat="server" ForeColor="Red" Text="Label" Visible="false"></asp:Label>
                              </div>

                            <%--<div class="row">--%>
                            <div class="col-sm-12 ">
                                <div id="divUpcompleted" visible="false" runat="server" class="alert alert-info alert-success" role="alert">
                                    <div class="col-sm-9">
                                        <strong>Alert!</strong>
                                        <asp:Label ID="Label5" Text="Excel file upload completed. Do you want to process?" runat="server"></asp:Label>
                                    </div>
                                    <div class="col-sm-3">
                                        <asp:Button ID="btnprocess" class="btn btn-success btn-xs" runat="server" Text="Process" OnClick="btnprocess_Click" />
                                    </div>
                                </div>
                            </div>


                        </div>
                    </div>
                </div>
            </div>
        </div>

    </asp:Panel>


    <asp:UpdatePanel ID="UpdatePanel4" runat="server">
        <ContentTemplate>
            <asp:Button ID="Button1" runat="server" Text="Button" Style="display: none;" />
            <asp:ModalPopupExtender ID="MdlpopRootinv" runat="server" Enabled="True" TargetControlID="Button1"
                PopupControlID="pnlroutpopup" Drag="true" CancelControlID="lbtnlocrcan" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdatePanel ID="UpdatePanel14" runat="server">
        <ContentTemplate>
            <asp:Panel runat="server" ID="Panel1" DefaultButton="lbtnSearch">
                <div runat="server" id="pnlroutpopup" class="panel panel-default height400 width900">

                    <asp:Label ID="Label1" runat="server" Text="Label" Visible="false"></asp:Label>
                    <div class="panel panel-default">
                        <div class="panel-heading">
                            <asp:LinkButton ID="lbtnlocrcan" runat="server">
                             <span class="glyphicon glyphicon-remove" aria-hidden="true">Close</span>
                            </asp:LinkButton>
                            <%--<span>Commen Search</span>--%>
                            <div class="col-sm-11">
                            </div>
                            <div class="col-sm-1">
                            </div>
                        </div>
                        <div class="panel-body">
                            <div class="panel panel-default">

                                <div class="row">
                                    <div class="col-sm-12">
                                        <div class="panel-heading paddingtop0 paddingbottom0">
                                            Route Stock Availability
                                        </div>
                                        <div class="panel-body panelscollbar  height150">
                                            <asp:GridView ID="grdRoutstock" CssClass="table table-hover table-striped" runat="server" GridLines="None" ShowHeaderWhenEmpty="True"
                                                EmptyDataText="No data found..." AutoGenerateColumns="False">
                                                <Columns>
                                                    <asp:TemplateField>
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lbtncomselect" runat="server" CausesValidation="false" OnClick="lbtncomselect_Click">
                                                <span class="glyphicon glyphicon-hand-right" aria-hidden="true"></span>
                                                            </asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Loc">
                                                        <ItemTemplate>
                                                            <asp:Label ID="col_INL_LOC" runat="server" Text='<%# Bind("INL_LOC") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Status" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="col_INL_ITM_STUS" runat="server" Text='<%# Bind("INL_ITM_STUS") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Status">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Mis_desc" runat="server" Text='<%# Bind("Mis_desc") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="In hand">
                                                        <ItemTemplate>
                                                            <asp:Label ID="col_BL_QTY" runat="server" Text='<%# Bind("inl_qty") %>'></asp:Label>
                                                        </ItemTemplate>

                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Res. Qty">
                                                        <ItemTemplate>
                                                            <asp:Label ID="col_INL_RES_QTY" runat="server" Text='<%# Bind("INL_RES_QTY") %>'></asp:Label>
                                                        </ItemTemplate>

                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Fre.Qty">
                                                        <ItemTemplate>
                                                            <asp:Label ID="col_INL_FREE_QTY" runat="server" Text='<%# Bind("INL_FREE_QTY") %>'></asp:Label>
                                                        </ItemTemplate>

                                                    </asp:TemplateField>
                                                     <asp:TemplateField HeaderText="Route">
                                                        <ItemTemplate>
                                                            <asp:Label ID="col_Inl_cre_by" runat="server" Text='<%# Bind("Inl_cre_by") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-12">


                                        <div id="dvScroll2" class="panel-body panelscollbar height150">
                                            <asp:GridView ID="grdRoutserial" CssClass="table table-hover table-striped" runat="server" GridLines="None" ShowHeaderWhenEmpty="True"
                                                EmptyDataText="No data found..." AutoGenerateColumns="False">
                                                <Columns>
                                                    <asp:TemplateField Visible="true">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lbtnselectSeri" runat="server" CausesValidation="false" OnClick="lbtnselectSeri_Click">
                                                                     <span class="glyphicon glyphicon-ok" aria-hidden="true"></span>
                                                            </asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Serial #">
                                                        <ItemTemplate>
                                                            <asp:Label ID="col_ins_ser_1" runat="server" Text='<%# Bind("ins_ser_1") %>' Width="150px"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Serial II" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="col_ins_ser_2" runat="server" Text='<%# Bind("ins_ser_2") %>' Width="150px"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Serial III" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="col_ins_ser_3" runat="server" Text='<%# Bind("ins_ser_3") %>' Width="150px"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Serial ID">
                                                        <ItemTemplate>
                                                            <asp:Label ID="col_ins_ser_id" runat="server" Text='<%# Bind("ins_ser_id") %>' Width="150px"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Item" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="col_ins_itm_cd" runat="server" Text='<%# Bind("ins_itm_cd") %>' Width="150px"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="status" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="col_ins_itm_stus" runat="server" Text='<%# Bind("ins_itm_stus") %>' Width="150px"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Reason">
                                                        <ItemTemplate>
                                                            <asp:DropDownList ID="col_code" CssClass="form-control" runat="server" Width="150px"></asp:DropDownList>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Remark">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="col_remark" TextMode="MultiLine" CssClass="form-control" runat="server" Width="250px"></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Exp.Date">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtserexdate" Enabled="true" CausesValidation="false"
                                                                runat="server" CssClass="txtserexdate form-control" Width="80px"></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="col_Ser_tp" runat="server" Text='<%# Bind("Ser_tp") %>' Width="150px"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </div>

                                    </div>
                                </div>
                                <div class="row height20">
                                </div>
                                <div class="row">
                                    <div class="col-sm-1 ">
                                    </div>
                                    <div class="col-sm-1 paddingRight0">
                                        Approve Qty
                                    </div>
                                    <div class="col-sm-1">
                                        <asp:Label runat="server" ID="lblAppqty" Text="..." ForeColor="#A513D0"></asp:Label>
                                    </div>
                                    <div class="col-sm-1 paddingRight0">
                                        Select Qty
                                    </div>
                                    <div class="col-sm-1">
                                        <asp:Label runat="server" ID="lblAelectQty" Text="0" ForeColor="#A513D0"></asp:Label>
                                    </div>
                                    <div class="col-sm-1 paddingRight0">
                                        Select Loc.
                                    </div>
                                    <div class="col-sm-1">
                                        <asp:Label runat="server" ID="lblserloc" Visible="true" Text="..." ForeColor="#A513D0"></asp:Label>
                                    </div>
                                    <div class="col-sm-12 width250">
                                        <asp:Button ID="btncont" runat="server" Text="Continue" class="btn btn-default btn-sm" Width="160px" OnClick="btncont_Click" />
                                    </div>
                                </div>
                            </div>
                        </div>

                    </div>

                </div>
            </asp:Panel>

        </ContentTemplate>
    </asp:UpdatePanel>


    <asp:UpdatePanel ID="UpdatePanel16" runat="server">
        <ContentTemplate>
            <asp:Button ID="Button9" runat="server" Text="Button" Style="display: none;" />
            <asp:ModalPopupExtender ID="MdlItemSear" runat="server" Enabled="True" TargetControlID="Button9"
                PopupControlID="pnlItempopup" CancelControlID="lbtnitmsearch" PopupDragHandleControlID="PopupHeader" Drag="true" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>

        </ContentTemplate>

    </asp:UpdatePanel>
    <asp:UpdatePanel ID="UpdatePanel17" runat="server">
        <ContentTemplate>
            <asp:Panel runat="server" ID="pnlItempopup" DefaultButton="lbtnSearch">
                <div runat="server" id="Div1" class="panel panel-default height400 width700">
                    <asp:Label ID="Label7" runat="server" Text="Label" Visible="false"></asp:Label>
                    <div class="panel panel-default">
                        <div class="panel-heading">
                            <asp:LinkButton ID="lbtnitmsearch" runat="server">
                             <span class="glyphicon glyphicon-remove" aria-hidden="true">Close</span>
                            </asp:LinkButton>
                            <%--<span>Commen Search</span>--%>
                            <div class="col-sm-11">
                                Item Details
                            </div>
                            <div class="col-sm-1">
                            </div>
                        </div>
                        <div class="panel-body">
                            <div class="col-sm-12" id="Div7" runat="server">
                                <div class="row">
                                    <div class="col-sm-12">
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-12 height5">
                                    </div>
                                </div>
                                <div class="row">
                                    <asp:Panel runat="server" ID="Panel3">
                                        <div class="col-sm-12" id="Div8" runat="server">

                                            <div class="col-sm-2 labelText1">
                                                Search by key
                                            </div>
                                            <asp:ScriptManagerProxy ID="ScriptManagerProxy1" runat="server"></asp:ScriptManagerProxy>
                                            <asp:UpdatePanel ID="UpdatePanel18" runat="server">
                                                <ContentTemplate>
                                                    <div class="col-sm-3 paddingRight5">
                                                        <asp:DropDownList ID="ddlitemserchoption" CausesValidation="false" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlitemserchoption_SelectedIndexChanged" AutoPostBack="true">
                                                            <asp:ListItem Text="Select" Selected="True" Value="9"></asp:ListItem>
                                                             <asp:ListItem Text="Combine Item"  Value="0"></asp:ListItem>
                                                            <asp:ListItem Text="Similer Item" Value="1"></asp:ListItem>
                                                            <asp:ListItem Text="Replace Item" Value="2"></asp:ListItem>
                                                             <asp:ListItem Text="New Item" Value="3"></asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>

                                            <asp:ScriptManagerProxy ID="ScriptManagerProxy2" runat="server"></asp:ScriptManagerProxy>
                                            <asp:UpdatePanel ID="UpdatePanel19" runat="server">
                                                <ContentTemplate>
                                                    <div class="col-sm-1 paddingLeft0">
                                                        <asp:LinkButton ID="lbtnsearcItempop" runat="server" OnClick="lbtnsearcItempop_Click">
                                            <span class="glyphicon glyphicon-search" aria-hidden="true"  ></span>
                                                        </asp:LinkButton>
                                                    </div>
                                                </ContentTemplate>

                                                <Triggers>
                                                    <asp:AsyncPostBackTrigger ControlID="txtSearchbyword" />
                                                </Triggers>
                                            </asp:UpdatePanel>
                                            <div class="col-sm-2 labelText1">
                                            </div>

                                            <div class="col-sm-3 paddingRight5">
                                                <asp:TextBox ID="txtsearcItembyword" Visible="true" CausesValidation="false" placeholder="Search by word" class="form-control" AutoPostBack="true" runat="server" OnTextChanged="txtsearcItembyword_TextChanged"></asp:TextBox>
                                            </div>
                                            <div class="col-sm-1 paddingLeft0">
                                                        <asp:LinkButton ID="lbtnnewitmsearch" runat="server" OnClick="lbtnnewitmsearch_Click">
                                            <span class="glyphicon glyphicon-search" aria-hidden="true"  ></span>
                                                        </asp:LinkButton>
                                                    </div>
                                        </div>
                                    </asp:Panel>
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
                                        <asp:ScriptManagerProxy ID="ScriptManagerProxy3" runat="server"></asp:ScriptManagerProxy>
                                        <asp:UpdatePanel ID="UpdatePanel20" runat="server">
                                            <ContentTemplate>
                                                <asp:GridView ID="grdItem" CausesValidation="false" runat="server" GridLines="None" CssClass="table table-hover table-striped" PagerStyle-CssClass="cssPager" OnSelectedIndexChanged="grdItem_SelectedIndexChanged">
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

        </ContentTemplate>

    </asp:UpdatePanel>



    <asp:UpdatePanel ID="UpdatePanel23" runat="server">
        <ContentTemplate>
            <asp:Button ID="Button10" runat="server" Text="Button" Style="display: none;" />
            <asp:ModalPopupExtender ID="Mdlvaliresult" runat="server" Enabled="True" TargetControlID="Button10"
                PopupControlID="pnlvalipopup" CancelControlID="lbtnvalidcancl" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>

        </ContentTemplate>

    </asp:UpdatePanel>
    <asp:UpdatePanel ID="UpdatePanel24" runat="server">
        <ContentTemplate>
            <asp:Panel runat="server" ID="Panel2">
                <div runat="server" id="pnlvalipopup" class="panel panel-default height400 width700">
                    <asp:Label ID="Label9" runat="server" Text="Label" Visible="false"></asp:Label>
                    <div class="panel panel-default">
                        <div class="panel-heading">
                            <asp:LinkButton ID="lbtnvalidcancl" runat="server">
                             <span class="glyphicon glyphicon-remove" aria-hidden="true">Close</span>
                            </asp:LinkButton>
                            <%--<span>Commen Search</span>--%>
                            <div class="col-sm-11">
                                <asp:Label ID="Label10" runat="server"></asp:Label>
                            </div>
                            <div class="col-sm-1">
                            </div>
                        </div>
                        <div class="panel-body">
                            <div class="col-sm-12" id="Div11" runat="server">
                                <div class="row">
                                    <div class="col-sm-12">
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
                                        <asp:ScriptManagerProxy ID="ScriptManagerProxy12" runat="server"></asp:ScriptManagerProxy>
                                        <asp:UpdatePanel ID="UpdatePanel27" runat="server">
                                            <ContentTemplate>
                                                <asp:GridView ID="grdvalid" CausesValidation="false" runat="server" GridLines="None" CssClass="table table-hover table-striped" PagerStyle-CssClass="cssPager" OnSelectedIndexChanged="grdItem_SelectedIndexChanged">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Description">
                                                            <ItemTemplate>
                                                                <asp:Label ID="col_PARA_CARIT" runat="server" Text='<%# Bind("PARA_CRIT") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Status" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="col_Approv_status" runat="server" Text='<%# Bind("para_stus") %>'></asp:Label>
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
     <%-- pnl search --%>
    <asp:UpdatePanel runat="server" ID="upSearch">
        <ContentTemplate>
            <asp:Button ID="btn1" runat="server" Text="Button" Style="display: none;" />
            <asp:ModalPopupExtender ID="PopupSearch" runat="server" Enabled="True" TargetControlID="btn1"
                PopupControlID="pnlSearch" PopupDragHandleControlID="PopupHeader" Drag="true" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:Panel runat="server" ID="pnlSearch" DefaultButton="lbtnSearch">
        <div runat="server" id="Div9" class="panel panel-primary" style="padding: 5px;">
            <div class="panel panel-default" style="height: 350px; width: 700px;">
                <div class="panel-heading" style="height: 25px;">
                    <div class="col-sm-11">
                         <div class="col-sm-12">
                            <div class="row">
                                <div class="col-sm-12 padding0">
                                    <strong>Similar Item Details</strong>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-sm-1">
                        <asp:LinkButton ID="btnCls" runat="server" OnClick="btnCls_Click">
                             <span class="glyphicon glyphicon-remove"  aria-hidden="true">Close</span>
                        </asp:LinkButton>
                    </div>
                </div>
                <div class="panel-body">
                    <div class="row ">
                    </div>
                    <div class="row">
                        <div class="col-sm-12">
                            <asp:Panel runat="server" Visible="false">
                                <div class="col-sm-2 labelText1">
                                    Search by key
                                </div>
                                <asp:ScriptManagerProxy ID="ScriptManagerProxy8" runat="server"></asp:ScriptManagerProxy>
                                <asp:UpdatePanel ID="UpdatePanel26" runat="server">
                                    <ContentTemplate>
                                        <div class="col-sm-3 paddingRight5">
                                            <asp:DropDownList ID="ddlSerByKey" runat="server" class="form-control" AutoPostBack="false">
                                            </asp:DropDownList>
                                        </div>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                                <div class="col-sm-2 labelText1">
                                    Search by word
                                </div>
                                <div class="col-sm-3 paddingRight5">
                                    <asp:UpdatePanel ID="UpdatePanel28" runat="server">
                                        <ContentTemplate>
                                            <asp:TextBox ID="txtSerByKey" CausesValidation="false" class="form-control" AutoPostBack="true" OnTextChanged="txtSerByKey_TextChanged" runat="server"></asp:TextBox>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                           
                            <asp:ScriptManagerProxy ID="ScriptManagerProxy11" runat="server"></asp:ScriptManagerProxy>
                            <asp:UpdatePanel ID="UpdatePanel29" runat="server">
                                <ContentTemplate>
                                    <div class="col-sm-1 paddingLeft0">
                                        <asp:LinkButton ID="lbtnSeNew" runat="server" OnClick="lbtnSeNew_Click">
                                     <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                        </asp:LinkButton>
                                    </div>
                                </ContentTemplate>

                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="txtSerByKey" />
                                </Triggers>
                            </asp:UpdatePanel>
                                 </asp:Panel>
                        </div>
                    </div>
                    <div class="row">
                       
                    </div>
                    <div class="row">
                        <div class="col-sm-12">
                            <asp:ScriptManagerProxy ID="ScriptManagerProxy13" runat="server"></asp:ScriptManagerProxy>
                            <asp:UpdatePanel ID="UpdatePanel30" runat="server">
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
    </asp:Panel>


     <%-- pnl search --%>
    <asp:UpdatePanel runat="server" ID="UpdatePanel31">
        <ContentTemplate>
            <asp:Button ID="btnSim1" runat="server" Text="Button" Style="display: none;" />
            <asp:ModalPopupExtender ID="popSimItm" runat="server" Enabled="True" TargetControlID="btnSim1"
                PopupControlID="pnlSimItm" PopupDragHandleControlID="PopupHeader" Drag="true" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:Panel runat="server" ID="pnlSimItm" DefaultButton="lbtnSearch">
        <div runat="server" id="Div12" class="panel panel-primary" style="padding: 5px;">
            <div class="panel panel-default" style="height: 350px; width: 700px;">
                <div class="panel-heading" style="height: 25px;">
                    <div class="col-sm-11">
                         <div class="col-sm-12">
                            <div class="row">
                                <div class="col-sm-12 padding0">
                                    <strong>Similar Item Details</strong>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-sm-1">
                        <asp:LinkButton ID="btnSimClose" runat="server" OnClick="btnSimClose_Click">
                             <span class="glyphicon glyphicon-remove"  aria-hidden="true">Close</span>
                        </asp:LinkButton>
                    </div>
                </div>
                <div class="panel-body">
                    <div class="row ">
                    </div>
                    <div class="row">
                        <div class="col-sm-12">
                            
                        </div>
                    </div>
                    <div class="row">
                       
                    </div>
                    <div class="row">
                        <div class="col-sm-12">
                            <asp:ScriptManagerProxy ID="ScriptManagerProxy16" runat="server"></asp:ScriptManagerProxy>
                            <asp:UpdatePanel ID="UpdatePanel35" runat="server">
                                <ContentTemplate>
                                    <asp:GridView ID="dgvSimilerItm" CausesValidation="false" runat="server" AllowPaging="True" AutoGenerateColumns="false"
                                        GridLines="None" CssClass="table table-hover table-striped" PageSize="10" PagerStyle-CssClass="cssPager" ShowHeaderWhenEmpty="true"
                                        EmptyDataText="No data found..."
                                        OnPageIndexChanging="dgvSimilerItm_PageIndexChanging">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Item Code">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblMisi_sim_itm_cd" runat="server" Text='<%# Bind("Misi_sim_itm_cd") %>' Width="100%"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Width="100px" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Item Description">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblTmp_itm_desc" runat="server" Text='<%# Bind("Tmp_itm_desc") %>' Width="100%"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Width="150px" />
                                            </asp:TemplateField>
                                            <asp:TemplateField >
                                                <HeaderTemplate>
                                                    <asp:Label ID="lblIssHdr" Text="" runat="server" />
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblTmp_Iss_loc_bal" runat="server" Text='<%# Bind("Tmp_Iss_loc_bal","{0:N2}") %>' Width="100%"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Width="80px" />
                                                <HeaderStyle Width="80px" />
                                            </asp:TemplateField>
                                            <asp:TemplateField >
                                                <HeaderTemplate>
                                                    <asp:Label ID="lblReqHdr"  Width="100%" Text="" runat="server" />
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblTmp_req_loc_bal" runat="server" Text='<%# Bind("Tmp_req_loc_bal","{0:N2}") %>' Width="100%"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Width="80px" CssClass="gridHeaderAlignRight" />
                                                <HeaderStyle Width="80px" CssClass="gridHeaderAlignRight"/>
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
    </asp:Panel>
    <asp:UpdatePanel ID="UpdatePanel32" runat="server">
        <ContentTemplate>
            <asp:Button ID="btnconfbox1" runat="server" Text="Button" Style="display: none;" />
            <asp:ModalPopupExtender ID="popPrint" runat="server" Enabled="True" TargetControlID="btnconfbox1"
                PopupControlID="pnlPrint" PopupDragHandleControlID="PopupHeader" Drag="true" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>
        </ContentTemplate>
    </asp:UpdatePanel>
     <asp:UpdateProgress ID="UpdateProgress4" DisplayAfter="10"
        runat="server" AssociatedUpdatePanelID="UpdatePanel33">
        <ProgressTemplate>
            <div class="divWaiting">
                <asp:Label ID="lblWait55" runat="server"
                    Text="Please wait... " />
                <asp:Image ID="imgWait55" runat="server"
                    ImageAlign="Middle" ImageUrl="~/images/icons/ajax-loader.gif" />
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <asp:Panel ID="pnlPrint" runat="server" align="center">
        <div runat="server" id="Div13" class="panel panel-info height120 width250">
            <asp:Label ID="Label8" runat="server" Text="Label" Visible="false"></asp:Label>
            <asp:ScriptManagerProxy ID="ScriptManagerProxy14" runat="server"></asp:ScriptManagerProxy>
            <asp:UpdatePanel ID="UpdatePanel33" runat="server">
                <ContentTemplate>

                    <div class="panel panel-heading">
                        <span>Alert</span>
                    </div>
                    <div class="panel-body">
                        <div class="row">
                            <div class="col-sm-12">
                                <asp:Label ID="lblPrintLbl" runat="server"></asp:Label>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-12">
                                <asp:Label ID="Label12" runat="server"></asp:Label>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-12">
                                <asp:Label ID="Label13" runat="server"></asp:Label>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-12">
                                <asp:Label ID="Label14" runat="server"></asp:Label>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-12">
                                <asp:Label ID="Label15" runat="server"></asp:Label>
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
                                <asp:Button ID="btnPrintDoc" runat="server" Text="yes" Width="80px" CausesValidation="false" class="btn btn-primary btn-xs" OnClick="btnPrintDoc_Click" />
                            </div>
                            <div class="col-sm-2 ">
                                <asp:Button ID="btnPrintDocNo" runat="server" Text="No" Width="80px" CausesValidation="false" class="btn btn-primary btn-xs" OnClick="btnPrintDocNo_Click" />
                            </div>
                        </div>
                    </div>

                </ContentTemplate>
            </asp:UpdatePanel>
              <%-- Value Exceed popup --%>
    <asp:UpdatePanel ID="UpdatePanel34" runat="server">
        <ContentTemplate>
            <asp:Button ID="Button11" runat="server" Text="Button" Style="display: none;" />
            <asp:ModalPopupExtender ID="popConfFree" runat="server" Enabled="True" TargetControlID="Button11"
                PopupControlID="Panel4" PopupDragHandleControlID="PopupHeader" Drag="true" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>
        </ContentTemplate>
    </asp:UpdatePanel>
     <asp:UpdateProgress ID="UpdateProgress6" DisplayAfter="10"
        runat="server" AssociatedUpdatePanelID="UpdatePanel37">
        <ProgressTemplate>
            <div class="divWaiting">
                <asp:Label ID="lblWssait8" runat="server"
                    Text="Please wait... " />
                <asp:Image ID="imgssWait8" runat="server"
                    ImageAlign="Middle" ImageUrl="~/images/banners/hidden_progress_bar.gif" />
            </div>
        </ProgressTemplate>

    </asp:UpdateProgress>
  

    <asp:UpdatePanel ID="UpdatePanel36" runat="server">
        <ContentTemplate>
            <asp:Panel ID="Panel4" runat="server" align="center">
                <div runat="server" id="Div14" class="panel panel-info height120 width250">
                    <asp:Label ID="Label11" runat="server" Text="Label" Visible="false"></asp:Label>
                    <asp:ScriptManagerProxy ID="ScriptManagerProxy15" runat="server"></asp:ScriptManagerProxy>
                    <asp:UpdatePanel ID="UpdatePanel37" runat="server">
                        <ContentTemplate>

                            <div class="panel panel-heading">
                                <span>Alert</span>
                            </div>
                            <div class="panel-body">
                                <div class="row">
                                    <div class="col-sm-12">
                                        <asp:Label ID="Label16" runat="server"></asp:Label>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-12">
                                        <asp:Label ID="Label17" runat="server"></asp:Label>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-12">
                                        <asp:Label ID="Label18" runat="server"></asp:Label>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-12">
                                        <asp:Label ID="Label19" runat="server"></asp:Label>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-12">
                                        <asp:Label ID="Label20" runat="server"></asp:Label>
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
                                        <asp:Button ID="lbtnFreeOK" runat="server" Text="yes" Width="80px" CausesValidation="false" class="btn btn-primary btn-xs" OnClick="lbtnFreeOK_Click" />
                                    </div>
                                    <div class="col-sm-2 ">
                                        <asp:Button ID="lbtnFreeNo" runat="server" Text="No" Width="80px" CausesValidation="false" class="btn btn-primary btn-xs" OnClick="lbtnFreeNo_Click" />
                                    </div>
                                </div>
                            </div>

                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
        </div>
    </asp:Panel>

  
    <script>
        Sys.Application.add_load(fun);
        function fun() {
            $(document).ready(function () {
                maintainScrollPosition();
            });
        }
    </script>
</asp:Content>
