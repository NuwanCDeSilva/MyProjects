<%@ Page Title="" Language="C#" MaintainScrollPositionOnPostback="true" MasterPageFile="~/View/AdminSite.Master" AutoEventWireup="true" CodeBehind="MaterialRequisitionNoteApprovals.aspx.cs" Inherits="FastForward.SCMWeb.View.Transaction.Inventory.MaterialRequisitionNoteApprovals" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/UserControls/ucLoactionSearch.ascx" TagPrefix="uc1" TagName="ucLoactionSearch" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="<%=Request.ApplicationPath%>Css/jquery-ui.theme.min.css" rel="stylesheet" />
    <link href="<%=Request.ApplicationPath%>Css/jquery-ui.min.css" rel="stylesheet" />

      <script language="javascript" type="text/javascript">


      <%-- var sessionTimeoutWarning = "<%= System.Configuration.ConfigurationSettings.AppSettings["SessionWarning"].ToString()%>";--%>
        var sessionTimeout = "<%= Session.Timeout %>";
        var sTimeout = parseInt(sessionTimeoutWarning) * 60 * 100000;
        setTimeout('SessionWarning()', sTimeout);

        function SessionWarning() {
            var message = "Your session will expire in another " + (parseInt(sessionTimeout) - parseInt(sessionTimeoutWarning)) + " mins! Please Save the data before the session expires";
            alert(message);
        }
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
    <script>
        $(document).ready(function () {
            maintainScrollPosition();
        });
        function pageLoad() {
            maintainScrollPosition();
        }
        function maintainScrollPosition() {
            $("#dvScroll").scrollTop($('#<%=hfScrollPosition.ClientID%>').val());
        }
        function setScrollPosition(scrollValue) {
            $('#<%=hfScrollPosition.ClientID%>').val(scrollValue);
        }
        function scrollTop() {
            window.document.body.scrollTop = 0;
            window.document.documentElement.scrollTop = 0;
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
            var selectedvalueOrd = confirm("Are you sure do you want to remove document process ?");
            if (selectedvalueOrd) {
                return true;
            } else {
                return false;
            }
        };
        function checkDate(sender, args) {

            if ((sender._selectedDate < new Date())) {
                alert("You cannot select a day earlier than today!");
                sender._selectedDate = new Date();
                sender._textbox.set_Value(sender._selectedDate.format(sender._format))
            }
        }
        function pageLoad(sender, args) {

            $("#<%=txtDeliverDate.ClientID %>").datepicker({ dateFormat: "d/M/yy", timeFormat: "hh:mm tt" });
            $("#<%=txtFromDate.ClientID %>").datepicker({ dateFormat: "d/M/yy", timeFormat: "hh:mm tt" });
            $("#<%=txtFDate.ClientID %>").datepicker({ dateFormat: "d/M/yy", timeFormat: "hh:mm tt" });
            $("#<%=txtTDate.ClientID %>").datepicker({ dateFormat: "d/M/yy", timeFormat: "hh:mm tt" });
            $("#<%=txtToDate.ClientID %>").datepicker({ dateFormat: "d/M/yy", timeFormat: "hh:mm tt" });

        };
        function CheckAllgrdReq(oCheckbox) {
            var GridView2 = document.getElementById("<%=grdRequestDetails.ClientID %>");
            for (i = 1; i < GridView2.rows.length; i++) {
                GridView2.rows[i].cells[0].getElementsByTagName("INPUT")[0].checked = oCheckbox.checked;
            }
        }
        function Enable() {
            return;
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
        function isNumberKey(evt) {
            var charCode = (evt.which) ? evt.which : evt.keyCode;
            if (charCode != 46 && charCode > 31
              && (charCode < 48 || charCode > 57))
                return false;
            return true;
        };
        function checkDateE(sender, args) {

            if ((sender._selectedDate < new Date())) {
                alert("You cannot select a day earlier than today!");
                sender._selectedDate = new Date();
                sender._textbox.set_Value(sender._selectedDate.format(sender._format))
            }
        };
        function checkDateF(sender, args) {

            if ((sender._selectedDate > new Date())) {
                alert("You cannot select a day future date !");
                sender._selectedDate = new Date();
                sender._textbox.set_Value(sender._selectedDate.format(sender._format))
            }
        };
        function checkDate(sender, args) {

            if ((sender._selectedDate < new Date())) {
                alert("You cannot select a day earlier than today!");
                sender._selectedDate = new Date();
                sender._textbox.set_Value(sender._selectedDate.format(sender._format))
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
    </script>
    <script type="text/javascript">
        function MutExChkList(chk) {
            var chkList = chk.parentNode.parentNode.parentNode;
            var chks = chkList.getElementsByTagName("input");
            for (var i = 0; i < chks.length; i++) {
                if (chks[i] != chk && chk.checked) {
                    chks[i].checked = false;
                }
            }
        }
    </script>
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
                            <div class="col-sm-6  buttonrow" style="height:30px;">
                            </div>
                            <div class="col-sm-6 buttonRow" style="height:30px;">
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
                                    <asp:LinkButton ID="lbtnAmend" CausesValidation="false" runat="server" OnClick="lbtnAmend_Click" OnClientClick="UpdateConfirm()">
                                        <span class="glyphicon glyphicon-edit" aria-hidden="true"></span>Update
                                    </asp:LinkButton>
                                </div>

                                <div class="col-sm-1 paddingLeft0 paddingRight0 ">
                                    <asp:LinkButton ID="lbtnSave" CausesValidation="false" runat="server" OnClick="lbtnSave_Click" OnClientClick="SaveConfirm()">
                                        <span class="glyphicon glyphicon-saved" aria-hidden="true"></span>Save
                                    </asp:LinkButton>
                                </div>
                                <div class="col-sm-1">
                                </div>
                                <div class="col-sm-2 paddingRight0 paddingLeft0">
                                    <asp:LinkButton ID="lbtnClear" runat="server" OnClick="lbtnClear_Click" OnClientClick="ClearConfirm()">
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
                                                        <asp:LinkButton ID="lbtncancelrequest" CausesValidation="false" runat="server" OnClick="lbtncancelrequest_Click" OnClientClick="CancelConfirm()">
                                        <span class="glyphicon glyphicon-remove" aria-hidden="true"></span>Cancel pending request
                                                        </asp:LinkButton>
                                                    </div>

                                                    <div class="col-sm-12  ">
                                                        <asp:LinkButton ID="lbtncancel" CausesValidation="false" runat="server" OnClick="llbtncancel_Click" OnClientClick="ACancelConfirm()">
                                        <span class="glyphicon glyphicon-remove" aria-hidden="true"></span>Cancel Approval
                                                        </asp:LinkButton>
                                                    </div>
                                                    <div class="col-sm-12   ">
                                                        <asp:LinkButton ID="lbtnExcelUpload" runat="server" OnClick="lbtnExcelUpload_Click">
                                        <span class="glyphicon glyphicon-upload" aria-hidden="true"></span>Excel Upload
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
                        <div class="col-sm-6 paddingRight5">
                            <div class="panel panel-default " style="margin-bottom:1px; margin-top:1px;">
                                <div class="panel-heading" style="height:23px;">
                                    <div class="row  ">
                                        <div class="col-sm-12  ">
                                            <div class="col-sm-2 paddingLeft5">
                                                <strong>MRN Approval </strong>
                                            </div>
                                            <div class="col-sm-2 paddingLeft5 paddingRight0">
                                                Request by:
                                            </div>
                                            <div class="col-sm-5 paddingLeft0">
                                                <asp:Label ID="lblshowroomname" runat="server" Style="color: blue"></asp:Label>
                                            </div>
                                            <div class="col-sm-1 ">
                                                <asp:CheckBox ID="chkcomahhoc" AutoPostBack="true" runat="server" Width="5px" OnCheckedChanged="chkcomahhoc_CheckedChanged"></asp:CheckBox>
                                            </div>
                                            <div class="col-sm-2  paddingLeft0 paddingRight0" >
                                                <asp:Label runat="server" ID="lblexcel" Text="S/R Requesting"></asp:Label>
                                            </div>
                                        </div>
                                    </div>

                                </div>

                                <div class="panel-body" style="padding-bottom:1px; padding-top:1px;margin-bottom:1px; margin-top:1px;">
                                    <div class="col-sm-12">
                                        <div class="row">
                                            <div class="panel panel-default">
                                                <div class="panel-body">
                                                    <div class="row">
                                                        <div class="col-sm-3 paddingRight0">
                                                            <div class="col-sm-3 labelText1 paddingLeft0 paddingRight0">
                                                                Showroom 
                                                            </div>
                                                            <div class="col-sm-8">
                                                                <asp:TextBox runat="server" ID="txtShowroom" CausesValidation="false" AutoPostBack="true" CssClass="form-control" OnTextChanged="txtShowroom_TextChanged"></asp:TextBox>
                                                            </div>
                                                            <div class="col-sm-1 paddingLeft0 paddingRight0">
                                                                <asp:LinkButton ID="lbtnShowroom" runat="server" CausesValidation="false" OnClick="lbtnShowroom_Click">
                                                                    <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                                </asp:LinkButton>
                                                            </div>
                                                        </div>
                                                        <div class="col-sm-5 paddingRight0">
                                                            <div class="col-sm-3 labelText1 paddingLeft0 paddingRight0">
                                                                Approval # 
                                                            </div>
                                                            <div class="col-sm-8">
                                                                <asp:TextBox runat="server" Enabled="false" ID="txtApproval" AutoPostBack="true" CausesValidation="false" CssClass="form-control" OnTextChanged="txtApproval_TextChanged"></asp:TextBox>
                                                            </div>
                                                            <div class="col-sm-1 paddingLeft0 paddingRight0 ">
                                                                <asp:LinkButton ID="lbtnApprovalMRN" runat="server" CausesValidation="false" OnClick="lbtnApprovalMRN_Click">
                                                                    <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                                </asp:LinkButton>
                                                            </div>
                                                        </div>
                                                        <div class="col-sm-4 paddingRight0 paddingLeft0">
                                                            <div class="col-sm-2 labelText1 paddingLeft0">
                                                                Date
                                                            </div>
                                                            <div class="col-sm-7 paddingRight5 paddingLeft0">
                                                                <asp:TextBox runat="server" ReadOnly="true" ID="txtSearchDate" CausesValidation="false" CssClass="form-control"></asp:TextBox>
                                                            </div>
                                                            <div class="col-sm-1 paddingLeft0">
                                                                <asp:LinkButton ID="lbtnSearchDate" runat="server" CausesValidation="false">
                                                            <span class="glyphicon glyphicon-calendar" aria-hidden="true"></span>
                                                                </asp:LinkButton>
                                                                <asp:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtSearchDate"
                                                                    PopupButtonID="lbtnSearchDate" Format="dd/MMM/yyyy">
                                                                </asp:CalendarExtender>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="row">
                                                        <div class="col-sm-12 paddingRight0">
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-sm-6 paddingLeft5">
                                                <div class="row">
                                                    <div class="col-sm-4 labelText1  ">
                                                        Request # 
                                                    </div>
                                                    <div class="col-sm-8">
                                                        <asp:TextBox runat="server" ID="txtrequestNo" CausesValidation="false" CssClass="form-control"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                   <%-- <div class="col-sm-12 height5">
                                                    </div>--%>
                                                </div>
                                                <div class="row">
                                                    <div class="col-sm-4 labelText1  ">
                                                        Prefer location
                                                    </div>
                                                    <div class="col-sm-8">
                                                        <asp:TextBox runat="server" ID="txtPreferloc" CausesValidation="false" CssClass="form-control"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                   <%-- <div class="col-sm-12 height5">
                                                    </div>--%>
                                                </div>
                                                <div class="row">
                                                    <div class="col-sm-4 labelText1  ">
                                                        Authorized by

                                                    </div>
                                                    <div class="col-sm-8">
                                                        <asp:TextBox runat="server" ID="txtAuthorizedby" Enabled="false" CausesValidation="false" CssClass="form-control"></asp:TextBox>
                                                    </div>
                                                </div>

                                            </div>
                                            <div class="col-sm-6 paddingLeft5">
                                                <div class="row">
                                                    <div class="col-sm-4 labelText1">
                                                        Request type
                                                    </div>
                                                    <div class="col-sm-6">
                                                        <asp:TextBox runat="server" ID="txtRequesttype" Enabled="false" CausesValidation="false" CssClass="form-control"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <%--<div class="col-sm-12 height5">
                                                    </div>--%>
                                                </div>
                                                <div class="row">
                                                    <div class="col-sm-4 labelText1  ">
                                                        Delivery Date
                                                    </div>
                                                    <div class="col-sm-6  ">
                                                        <asp:TextBox ID="txtDeliverDate" TabIndex="100" AutoPostBack="true" runat="server" CssClass="form-control" onkeypress="return RestrictSpace()"
                                                            Format="dd/MMM/yyyy" OnTextChanged="txtDeliverDate_TextChanged">
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
                                                <div class="row">
                                                   <%-- <div class="col-sm-12 height5">
                                                    </div>--%>
                                                </div>
                                                <div class="row">
                                                    <div class="col-sm-4 labelText1  ">
                                                        Company

                                                    </div>
                                                    <div class="col-sm-6">
                                                        <asp:TextBox runat="server" ID="txtCompany" Enabled="false" CausesValidation="false" CssClass="form-control"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                           <%-- <div class="col-sm-12 height5">
                                            </div>--%>
                                        </div>
                                        <div class="row">
                                            <div class="col-sm-2 labelText1 paddingLeft5  ">
                                                Remark 
                                            </div>
                                            <div class="col-sm-9 paddingLeft5 paddingRight0">
                                                <asp:TextBox runat="server" ID="txtremark" TabIndex="101"  CausesValidation="false" CssClass="form-control" OnTextChanged="txtremark_TextChanged"></asp:TextBox>
                                            </div>
                                        </div>

                                        <div class="row">
                                            <div class="col-sm-12 padding0">
                                                <div class="panel panel-default">
                                                    <div class="panel panel-heading" style="height:20px;">
                                                       HP Restriction 
                                                    </div>
                                                    <div class="panel panel-body padding0" style="margin-bottom:0px;">
                                                        <div class="col-sm-12">
                                                            <div class="row">
                                                                <div class="col-sm-12">
                                                                    <div class="col-sm-6 padding0">
                                                                        <div class="col-sm-6 padding0">
                                                                            No of accounts(Monthly) :
                                                                        </div>
                                                                        <div class="col-sm-6 padding0 text-left">
                                                                            <asp:Label ID="lblMonthlyAcc" Text="" runat="server" />
                                                                        </div>
                                                                    </div>
                                                                    <div class="col-sm-6 padding0">
                                                                       <div class="col-sm-6 padding0">
                                                                            Value(Monthly) :
                                                                        </div>
                                                                        <div class="col-sm-6 padding0 text-left">
                                                                            <asp:Label ID="lblMonthlyVal" Text="" runat="server" />
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="row">
                                                                <div class="col-sm-12">
                                                                    <div class="col-sm-6 padding0">
                                                                        <div class="col-sm-6 padding0 labelText1">
                                                                            No of accounts(Yearly) :
                                                                        </div>
                                                                        <div class="col-sm-6 padding0 labelText1">
                                                                            <asp:Label ID="lblYearAcc" Text="" runat="server" />
                                                                        </div>
                                                                    </div>
                                                                    <div class="col-sm-6 padding0">
                                                                       <div class="col-sm-6 padding0 labelText1">
                                                                            Value(Yearly) :
                                                                        </div>
                                                                        <div class="col-sm-6 padding0 labelText1">
                                                                            <asp:Label ID="lblYearVal" Text="" runat="server" />
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
                            <div class="row">
                                <div class="col-sm-12 ">
                                    <div class="panel panel-default ">
                                        <div class="panel-heading paddingtop0 paddingbottom0">

                                            <div class="row  ">
                                                <div class="col-sm-12  ">
                                                    <div class="col-sm-2 paddingLeft5 labelText1">
                                                        Inventory Balance
                                                    </div>
                                                    <div class="col-sm-1 paddingLeft5 labelText1">
                                                    </div>
                                                    <div class="col-sm-1 paddingLeft5 labelText1">
                                                    </div>
                                                    <div class="col-sm-2 paddingLeft5 labelText1">
                                                    </div>

                                                    <div class="col-sm-1 paddingLeft5 labelText1">
                                                        Company:
                                                    </div>
                                                    <div class="col-sm-2">
                                                        <div class="col-sm-5 labelText1  ">
                                                            <asp:Label ID="lblcompany" runat="server"></asp:Label>
                                                        </div>
                                                        <div class="col-sm-1 labelText1 ">
                                                            <asp:RadioButton Checked="true" ID="chkCompany" AutoPostBack="true" GroupName="Company" runat="server" OnCheckedChanged="chkCompany_CheckedChanged" />
                                                        </div>

                                                    </div>
                                                    <div class="col-sm-3">
                                                        <div class="col-sm-8 labelText1  ">
                                                            View Other
                                                        </div>
                                                        <div class="col-sm-1  labelText1">
                                                            <asp:RadioButton runat="server" GroupName="Company" AutoPostBack="true" ID="chkOther" OnCheckedChanged="chkOther_CheckedChanged" />
                                                        </div>


                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                       
                                        <div class="panel-body  panelscollbar height70">
                                            <asp:GridView ID="grdInventoryBalance" CssClass="table table-hover table-striped" runat="server" GridLines="None" ShowHeaderWhenEmpty="True"
                                                EmptyDataText="No data found..." AutoGenerateColumns="False">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Location">
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
                                                    <asp:TemplateField HeaderText="Qty in hand">
                                                        <ItemTemplate>
                                                            <asp:Label ID="col_BL_QTY" runat="server" Text='<%# Bind("inl_qty") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Right" Width="70px" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="">

                                                        <ItemStyle HorizontalAlign="Right" Width="20px" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Res. Qty">
                                                        <ItemTemplate>
                                                            <asp:Label ID="col_INL_RES_QTY" runat="server" Text='<%# Bind("INL_RES_QTY") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Right" Width="80px" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="">

                                                        <ItemStyle HorizontalAlign="Right" Width="20px" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Free Qty">
                                                        <ItemTemplate>
                                                            <asp:Label ID="col_INL_FREE_QTY" runat="server" Text='<%# Bind("INL_FREE_QTY") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Right" Width="55px" />
                                                    </asp:TemplateField>

                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </div>
                                </div>
                            </div>

                        </div>
                        <asp:Panel runat="server" ID="pnldefalt" Visible="true">
                            <div class="col-sm-6 paddingLeft5">
                                <asp:Panel runat="server" ID="PendingDiv">
                                    <div class="panel panel-default">
                                        <div class="panel-heading paddingbottom0">
                                            <div class="row  ">
                                                <div class="col-sm-12  ">
                                                    <div class="col-sm-5">
                                                        Pending S/R Request Details
                                                    </div>
                                                    <div class="col-sm-4" runat="server" id="adhoc">
                                                        <div class="col-sm-1 ">
                                                            <asp:CheckBox ID="chkadhoc2" AutoPostBack="true" runat="server" Width="5px" OnCheckedChanged="chkadhoc2_CheckedChanged"></asp:CheckBox>
                                                        </div>
                                                        <div class="col-sm-3 labelText1 paddingLeft0 paddingRight0">
                                                            ad hoc
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="panel-body paddingbottom0">
                                            <div class="row">
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
                                                    <div class="row">
                                                        <div class="col-sm-12 width250">
                                                            <asp:Button ID="btnupload" runat="server" Text="Upload" class="btn btn-default btn-sm" Width="160px" OnClick="btnuploadSear_Click" />
                                                        </div>

                                                    </div>
                                                </div>
                                                <div class="col-sm-9">
                                                    <div class="panel panel-default">
                                                        <div class="panel-heading">
                                                            Search by delivery date
                                                        </div>
                                                        <div class="panel-body">
                                                            <div class="row">
                                                                <div class="col-sm-5">
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
                                                                <div class="col-sm-7">
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
                                                                            <asp:CheckBox ID="chkSearchall" AutoPostBack="true" Checked="true" runat="server" Visible="true" Width="5px" OnCheckedChanged="chkSearchall_CheckedChanged1"></asp:CheckBox>
                                                                        </div>
                                                                    </div>
                                                                    <div class="row">
                                                                        <div class="col-sm-4 labelText1 paddingLeft5  ">
                                                                            Prefer Location 
                                                                        </div>
                                                                        <div class="col-sm-5 paddingLeft5 paddingRight0">
                                                                            <asp:TextBox runat="server" ID="txtPreferLocation"  Style="text-transform: uppercase;" Enabled="false" AutoPostBack="true" CausesValidation="false" CssClass="form-control"></asp:TextBox>
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
                                                            </div>


                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-12">
                                                    <div class="panel panel-default">
                                                          <asp:HiddenField ID="hfScrollPosition" Value="0" runat="server" />
                                                       <div id="dvScroll" class="panel-body panelscollbar height200" onscroll="setScrollPosition(this.scrollTop);">   <asp:GridView ID="grdRequestDetails" CssClass="table table-hover table-striped" runat="server" GridLines="None" ShowHeaderWhenEmpty="True"
                                                                EmptyDataText="No data found..." AutoGenerateColumns="False">
                                                                <Columns>
                                                                    <asp:TemplateField HeaderText="" Visible="true">
                                                                        <HeaderTemplate>
                                                                            <asp:CheckBox ID="allchk" runat="server" Width="5px" onclick="CheckAllgrdReq(this)"></asp:CheckBox>
                                                                        </HeaderTemplate>
                                                                        <ItemTemplate>
                                                                            <asp:CheckBox ID="chk_Req" AutoPostBack="true" runat="server" onclick="CheckBoxCheck(this);" Checked="false" Width="5px" OnCheckedChanged="chk_Req_CheckedChanged_Click" />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                     <asp:TemplateField>
                                                                         <ItemTemplate>
                                                                             <asp:LinkButton Visible='<%# !Eval("Itr_req_wp").ToString().Equals("1")%>' ID="lbtnProcess" OnClientClick="return ConfAddProcess();" runat="server" OnClick="lbtnProcess_Click">
                                                                                     <span class="glyphicon glyphicon-arrow-down" aria-hidden="true"></span>
                                                                             </asp:LinkButton>
                                                                             <asp:LinkButton Visible='<%# !Eval("Itr_req_wp").ToString().Equals("0")%>' ID="lbtnProcessCancel" OnClientClick="return ConfAddProcessCanc();" runat="server" OnClick="lbtnProcessCancel_Click">
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
                                </asp:Panel>

                            </div>
                        </asp:Panel>
                        <asp:Panel runat="server" ID="pnlComadhoc" Visible="false">
                            <div class="col-sm-3 paddingLeft5">
                                <uc1:ucLoactionSearch runat="server" ID="ucLoactionSearch" />
                            </div>

                            <div class="col-sm-1 Lwidth">
                                <asp:LinkButton ID="btnAddLocs" runat="server" OnClick="btnAddLocs_Click">
                                                        <span class="glyphicon glyphicon-triangle-bottom" aria-hidden="true"></span>
                                </asp:LinkButton>
                            </div>

                            <div class="row">

                                <div class="col-sm-2 paddingLeft0 paddingRight0">
                                    <div class="panel panel-default">
                                        <div class="panel-heading"></div>
                                        <div class="panel-body panelscollbar">
                                            <asp:GridView ID="grvLocs" EmptyDataText="No data found..." ShowHeaderWhenEmpty="True" DataKeyNames="LOCATION" CausesValidation="false" runat="server" AllowPaging="True" PageSize="2" GridLines="None" PagerStyle-CssClass="cssPager" CssClass="table table-hover table-striped">
                                                <Columns>


                                                    <asp:TemplateField HeaderText="Code">
                                                        <ItemTemplate>
                                                            <asp:Label ID="LOCATION" runat="server" Text='<%# Bind("LOCATION") %>' Width="30px"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Description">
                                                        <ItemTemplate>
                                                            <asp:Label ID="LOC_DESCRIPTION" runat="server" Text='<%# Bind("LOC_DESCRIPTION") %>' Width="100px"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <%--<asp:TemplateField HeaderText="company">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="l_SEL_COM_CD" runat="server" Text='<%# Bind("SEL_COM_CD") %>' Width="100px"></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>--%>
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </div>
                                </div>

                            </div>

                        </asp:Panel>
                    </div>
                    <div class="row">
                        <div class="col-sm-12 ">
                            <div class="panel panel-default">
                                <div class="panel-heading paddingbottom0 ">
                                    <div class="row  ">
                                        <div class="col-sm-12  ">
                                            <div class="col-sm-2 paddingLeft5">
                                                <strong>List of Items </strong>
                                            </div>
                                            <asp:Panel runat="server" ID="pnlchk">
                                                <asp:Panel runat="server" ID="Panel1" Visible="false">
                                                    <div class="col-sm-6 paddingLeft0 paddingRight0">

                                                        <div class="col-sm-2 labelText1 paddingLeft0 paddingRight0">
                                                            Similar Item
                                                        </div>

                                                        <div class="col-sm-1 labelText1 paddingLeft0 paddingRight0">
                                                            <asp:CheckBox ID="chkSimilarItem" AutoPostBack="true" onclick="MutExChkList(this);" runat="server" Width="5px" OnCheckedChanged="chkSimilarItem_CheckedChanged"></asp:CheckBox>
                                                        </div>
                                                        <div class="col-sm-2 labelText1 paddingLeft0 paddingRight0 ">
                                                            Replace Item
                                                         
                                                        </div>
                                                        <div class="col-sm-1 labelText1 paddingLeft0 paddingRight0">
                                                            <asp:CheckBox ID="chkReplaceItem" onclick="MutExChkList(this);" runat="server" Width="5px"></asp:CheckBox>
                                                        </div>
                                                        <div class="col-sm-1 labelText1 paddingLeft0 paddingRight0">
                                                            ad hoc
                                                       
                                                        </div>
                                                        <div class="col-sm-1 labelText1 paddingLeft0 paddingRight0">
                                                            <asp:CheckBox ID="chkadhoc" AutoPostBack="true" onclick="MutExChkList(this);" runat="server" Width="5px" OnCheckedChanged="chkadhoc_CheckedChanged1"></asp:CheckBox>
                                                        </div>


                                                    </div>
                                                </asp:Panel>
                                                <div class="col-sm-5 paddingLeft0 paddingRight0">
                                                </div>
                                                <div class="col-sm-2">
                                                    <asp:Label runat="server" Style="color: green" Text=" RRC1 Allocation Qty"></asp:Label>
                                                </div>
                                                <div class="col-sm-1">
                                                    <div class="col-sm-12">
                                                        <asp:Button ID="btncancel" runat="server" Text="Cancel all Item" class="btn btn-danger btn-xs" OnClick="btncancel_Click" />
                                                    </div>

                                                    <%-- <div class="col-sm-2 labelText1 paddingLeft0 paddingRight0">
                                                        Cancel all pending item
                                                    </div>--%>
                                                    <div class="col-sm-1 paddingLeft0 paddingRight0">
                                                        <asp:CheckBox ID="chkcancelall" Visible="false" onclick="MutExChkList(this);" runat="server" Width="5px" AutoPostBack="true" OnCheckedChanged="chkcancelall_CheckedChanged"></asp:CheckBox>
                                                    </div>
                                                </div>
                                                <div class="col-sm-1  width25">
                                                </div>
                                                <div class="col-sm-1">
                                                    <div class="col-sm-10 labelText1 paddingLeft0">
                                                        Add all Item
                                                    </div>
                                                    <div class="col-sm-1 paddingLeft0 paddingRight0">
                                                        <asp:CheckBox ID="chkallItem" Visible="true" onclick="MutExChkList(this);" runat="server" Width="5px" AutoPostBack="true" OnCheckedChanged="chkallItem_CheckedChanged"></asp:CheckBox>
                                                    </div>
                                                    <div class="col-sm-1">
                                                        <asp:Button ID="btnapprove" runat="server" Visible="false" Text="Add all Item" class="btn btn-default btn-xs" OnClick="btnapprove_Click" />
                                                    </div>
                                                </div>
                                            </asp:Panel>
                                            <div class="col-sm-1">
                                                <%--<div class="dropdown">
                                                    <a id="dLabel" data-toggle="dropdown" role="button" aria-haspopup="true" aria-expanded="false">
                                                        <span class="glyphicon glyphicon-menu-hamburger floatRight"></span>
                                                    </a>
                                                    <div class="dropdown-menu menupopup dropdownmenuMRN" aria-labelledby="dLabel">
                                                        <div class="row">

                                                            <div class="col-sm-12">

                                                                <div class="col-sm-12 paddingRight0">
                                                                    <asp:LinkButton ID="lbtnApprovItem" CausesValidation="false" runat="server" OnClick="lbtnApprovItem_Click">
                                                                        Approved Items
                                                                    </asp:LinkButton>
                                                                </div>

                                                            </div>
                                                        </div>

                                                    </div>
                                                </div>--%>
                                            </div>


                                        </div>
                                    </div>
                                </div>
                                <div class="panel-body">
                                    <div class="col-sm-12">
                                        <div class="row">
                                            <div class="panel panel-default">
                                                <div class="panel-body">
                                                    <div class="row">
                                                        <div class="col-sm-1  ">
                                                            Item Type
                                                        </div>
                                                        <div class="col-sm-1 paddingRight0 paddingLeft0">
                                                            <asp:DropDownList ID="ddlitemserchoption" CausesValidation="false" AutoPostBack="true" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlitemserchoption_SelectedIndexChanged">
                                                                <asp:ListItem Text="Request Item" Selected="True" Value="0"></asp:ListItem>
                                                                <asp:ListItem Text="Similer Item" Value="1"></asp:ListItem>
                                                                <asp:ListItem Text="Replace Item" Value="2"></asp:ListItem>
                                                                <asp:ListItem Text="New Item" Value="3"></asp:ListItem>

                                                            </asp:DropDownList>
                                                        </div>

                                                        <div class="col-sm-1 paddingRight0 paddingLeft0">
                                                        </div>
                                                        <div class="col-sm-1 Lwidth paddingLeft0">
                                                        </div>
                                                        <div class="col-sm-1 paddingRight0 paddingLeft0">
                                                        </div>
                                                        <div class="col-sm-1 paddingRight0 paddingLeft0 Textboxwidth80">
                                                            <div class="col-sm-1  paddingLeft0 paddingRight0">
                                                                <asp:CheckBox ID="chkGIT" runat="server" Width="5px"></asp:CheckBox>
                                                            </div>
                                                            <div class="col-sm-10 labelText1">
                                                                With GIT
                                                            </div>
                                                        </div>
                                                        <div class="col-sm-1 paddingRight0 paddingLeft0">
                                                            <div class="col-sm-1 paddingLeft0 paddingRight0">
                                                                <asp:CheckBox ID="chkFwdsale" Checked="true" runat="server" Width="5px"></asp:CheckBox>
                                                            </div>
                                                            <div class="col-sm-10 labelText1">
                                                                With 
                                                            </div>
                                                        </div>
                                                        <div class="col-sm-2">
                                                            <div class="col-sm-3 labelText1">
                                                                Season 
                                                            </div>
                                                            <div class="col-sm-5 labelText1">
                                                                <asp:DropDownList ID="ddlseason" CausesValidation="false" AutoPostBack="true" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlseason_SelectedIndexChanged">
                                                                </asp:DropDownList>
                                                            </div>

                                                        </div>
                                                        <div class="col-sm-2 paddingRight0 paddingLeft0">
                                                            <div class="col-sm-1  paddingLeft0 paddingRight0">
                                                                <asp:CheckBox ID="chkPresaleqty" runat="server" Width="5px"></asp:CheckBox>
                                                            </div>
                                                            <div class="col-sm-10 labelText1">
                                                                Previous Sales Qty
                                                            </div>
                                                        </div>

                                                        <div class="col-sm-1 paddingRight0 paddingLeft0">
                                                        </div>
                                                        <div class="col-sm-1 Textboxwidth paddingRight0 paddingLeft0">
                                                        </div>
                                                        <div class="col-sm-1 Textboxwidth paddingRight0 paddingLeft0">
                                                        </div>
                                                        <div class="col-sm-1 Textboxwidth paddingRight0 paddingLeft0">
                                                        </div>
                                                    </div>
                                                    <div class="row">
                                                        <div class="col-sm-1 paddingRight0">
                                                            Item code
                                                        </div>
                                                        <div class="col-sm-1 Lwidth paddingLeft0">
                                                        </div>
                                                        <div class="col-sm-1 paddingRight0 paddingLeft0">
                                                            Model
                                                        </div>
                                                        <div class="col-sm-1 Lwidth paddingLeft0">
                                                        </div>
                                                        <div class="col-sm-2 paddingRight0 paddingLeft0">
                                                            Remark
                                                        </div>
                                                        <div class="col-sm-1 paddingRight0 paddingLeft0 Textboxwidth80">
                                                            Shop stock
                                                        </div>
                                                        <div class="col-sm-1 paddingRight0 paddingLeft0">
                                                            Forward sales
                                                        </div>
                                                        <div class="col-sm-1 paddingRight0 paddingLeft0">
                                                            Buffer limit
                                                        </div>
                                                        <div class="col-sm-1 paddingRight0 paddingLeft0">
                                                            Request qty
                                                        </div>
                                                        <div class="col-sm-1 paddingRight0 paddingLeft0">
                                                            Req.Company
                                                        </div>
                                                        <div class="col-sm-1 Textboxwidth paddingRight0 paddingLeft0">
                                                            Prefer.location
                                                        </div>
                                                        <div class="col-sm-1 Textboxwidth paddingRight0 paddingLeft0">
                                                            purchase type
                                                        </div>
                                                        <div class="col-sm-1 Textboxwidth paddingRight0 paddingLeft0">
                                                            Approved qty
                                                        </div>
                                                    </div>
                                                    <div class="row">
                                                        <div class="col-sm-1  paddingRight0 ">
                                                            <asp:TextBox runat="server" ID="txtItem" Style="text-transform: uppercase" Enabled="true" AutoPostBack="true" CausesValidation="false" CssClass="form-control" OnTextChanged="txtItem_TextChanged"></asp:TextBox>
                                                        </div>
                                                        <div class="col-sm-1 Lwidth paddingLeft0">
                                                            <asp:LinkButton ID="lbtnItemSearch" runat="server" CausesValidation="false" OnClick="lbtnItemSearch_Click">
                                                <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                            </asp:LinkButton>
                                                        </div>
                                                        <div class="col-sm-1 paddingRight0 paddingLeft0">
                                                            <asp:TextBox runat="server" ID="txtModel" Enabled="false" CausesValidation="false" CssClass="form-control"></asp:TextBox>
                                                        </div>
                                                        <asp:Panel runat="server">
                                                            <div class="col-sm-2 paddingRight0 paddingLeft0 ">
                                                                <asp:TextBox runat="server" ID="txtItemRemark" TabIndex="103"  OnTextChanged="btnRemark_Click"  CausesValidation="false" CssClass="form-control"></asp:TextBox>
                                                                <asp:Button ID="btnRemark" runat="server" OnClick="btnRemark_Click" Text="Submit" Style="display: none;" />
                                                            </div>
                                                        </asp:Panel>
                                                        <div class="col-sm-1 paddingRight0 paddingLeft0 Textboxwidth80">
                                                            <asp:TextBox runat="server" ID="txtshopstock" Enabled="false" onkeypress="return isNumberKey(event)" Style="text-align: right" CausesValidation="false" CssClass="form-control"></asp:TextBox>
                                                        </div>
                                                        <div class="col-sm-1 paddingRight0 paddingLeft0 ">
                                                            <asp:TextBox runat="server" ID="txtForwardsale" Enabled="false" onkeypress="return isNumberKey(event)" Style="text-align: right" CausesValidation="false" CssClass="form-control"></asp:TextBox>
                                                        </div>
                                                        <div class="col-sm-1 paddingRight0 paddingLeft0 ">
                                                            <asp:TextBox runat="server" ID="txtBufferLimit" Enabled="false" onkeypress="return isNumberKey(event)" Style="text-align: right" CausesValidation="false" CssClass="form-control"></asp:TextBox>
                                                        </div>
                                                        <div class="col-sm-1 paddingRight0 paddingLeft0 ">
                                                            <asp:TextBox runat="server" ID="txtRequestqty" Enabled="false" onkeypress="return isNumberKey(event)" Style="text-align: right" CausesValidation="false" CssClass="form-control"></asp:TextBox>
                                                        </div>
                                                        <asp:Panel runat="server" DefaultButton="btnRCompnay">
                                                            <div class="col-sm-1 paddingRight0 paddingLeft0 ">
                                                                <asp:TextBox runat="server" ID="txtRequestcompany" onblur="__doPostBack('txtRequestcompany','OnBlur');" AutoPostBack="true" CausesValidation="false" CssClass="form-control"></asp:TextBox>
                                                            </div>
                                                            <asp:Button ID="btnRCompnay" runat="server" OnClick="btnRCompnay_Click" Text="Submit" Style="display: none;" />
                                                        </asp:Panel>
                                                        <div class="col-sm-1 Lwidth paddingLeft0">
                                                            <asp:LinkButton ID="lbtncomapny" runat="server" CausesValidation="false" OnClick="lbtncomapny_Click">
                                                <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                            </asp:LinkButton>
                                                        </div>
                                                        <asp:Panel runat="server" DefaultButton="btnPrefexLoc">
                                                            <div class="col-sm-1 Textboxwidth80 paddingRight0 paddingLeft0">
                                                                <%-- onblur="__doPostBack('txtPrefexlocation','OnBlur');"--%>
                                                                <asp:TextBox runat="server" Style="text-transform: uppercase;text-align: right" ID="txtPrefexlocation" TabIndex="104"  CausesValidation="false" CssClass="form-control"></asp:TextBox>
                                                            </div>
                                                            <asp:Button ID="btnPrefexLoc" runat="server" OnClick="btnPrefexLoc_Click" Text="Submit" Style="display: none;" />
                                                        </asp:Panel>
                                                        <div class="col-sm-1 Lwidth paddingLeft0">
                                                            <asp:LinkButton ID="lbtnPrefeslocation" runat="server" CausesValidation="false" OnClick="lbtnPrefeslocation_Click">
                                                <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                            </asp:LinkButton>
                                                        </div>
                                                        <div class="col-sm-1 paddingRight0 paddingLeft0">
                                                            <asp:DropDownList ID="ddlPurchasetype" CausesValidation="false" runat="server" CssClass="form-control">
                                                            </asp:DropDownList>
                                                        </div>
                                                        <asp:Panel runat="server" DefaultButton="btnApproval">

                                                            <div class="col-sm-1 Textboxwidth paddingRight0 paddingLeft0">
                                                                <asp:TextBox runat="server" TabIndex="105" ID="txtApprovalqty" onkeypress="return isNumberKey(event)" CausesValidation="false" Style="text-align: right" CssClass="form-control"></asp:TextBox>
                                                            </div>
                                                            <asp:Button ID="btnApproval" runat="server" OnClick="btnApproval_Click" Text="Submit" Style="display: none;" />
                                                        </asp:Panel>
                                                        <div class="col-sm-1 Lwidth paddingLeft5 paddingRight5">
                                                            <asp:LinkButton ID="lbtnAddItem" TabIndex="106" runat="server" CausesValidation="false" OnClick="lbtnAddItem_Click">
                                                <span class="glyphicon glyphicon-plus" aria-hidden="true"></span>
                                                            </asp:LinkButton>
                                                        </div>

                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="panel-body  panelscollbar height130">
                                                <asp:GridView ID="grdMRNReqItem" OnRowCreated="GridView1_RowCreated" ShowHeader="true" CssClass="table table-hover table-striped" runat="server" GridLines="None" ShowHeaderWhenEmpty="True"
                                                    EmptyDataText="No data found..." AutoGenerateColumns="False" OnRowDataBound="grdMRNReqItem_DataBound">
                                                  <Columns>
                                                        <%--<asp:buttonfield buttontype="Link" commandname="Add" text="Add" ItemStyle-Width="1px"/>--%>

                                                        <asp:TemplateField HeaderText="" Visible="true">
                                                            <ItemTemplate>
                                                                <asp:CheckBox ID="chk_ReqItem" AutoPostBack="true" runat="server" onclick="CheckBoxCheckItem(this);" Checked="false" Width="5px" OnCheckedChanged="chk_ReqItem_CheckedChanged_Click" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:CommandField ShowSelectButton="True" Visible="False" />
                                                        <asp:TemplateField HeaderText="Item code">
                                                            <ItemTemplate>
                                                                <asp:Label ID="col_ITRI_ITM_CD" runat="server" Text='<%# Bind("ITRI_ITM_CD") %>' Width="120px"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="col_itri_line_no" runat="server" Text='<%# Bind("itri_line_no") %>' Width="10px"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Model">
                                                            <ItemTemplate>
                                                                <asp:Label ID="col_mi_model" runat="server" Text='<%# Bind("Mst_item_model") %>' Width="80px"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Remark">
                                                            <ItemTemplate>
                                                                <asp:Label ID="col_itri_note" runat="server" Text='<%# Bind("itri_note") %>' Width="200px"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Reservation #" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="col_Itri_res_noe" runat="server" Text='<%# Bind("Itri_res_no") %>' Width="80px"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Shop stock">
                                                            <ItemTemplate>
                                                                <asp:Label ID="col_itri_shop_qty" runat="server" Text='<%# Bind("itri_shop_qty") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Right" Width="70px" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="">
                                                            <ItemStyle HorizontalAlign="Right" Width="25px" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Forward sales">
                                                            <ItemTemplate>
                                                                <asp:Label ID="col_itri_fd_qty" runat="server" Text='<%# Bind("itri_fd_qty") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Right" Width="85px" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="">
                                                            <ItemStyle HorizontalAlign="Right" Width="50px" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Buffer limit">
                                                            <ItemTemplate>
                                                                <asp:Label ID="col_itri_buffer" runat="server" Text='<%# Bind("itri_buffer") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Right" Width="68px" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="">
                                                            <ItemStyle HorizontalAlign="Right" Width="30px" />
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Request qty">
                                                            <ItemTemplate>
                                                                <asp:Label ID="col_itri_bqty" runat="server" Text='<%# Bind("itri_bqty") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Right" Width="78px" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="">
                                                            <ItemStyle HorizontalAlign="Right" Width="5px" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Prev.Sales qty">
                                                            <ItemTemplate>
                                                                <asp:Label ID="col_Itri_Prev_sales_qty" runat="server" Text='<%# Bind("Itri_Prev_sales_qty") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Right" Width="85px" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Req.Company">
                                                            <ItemTemplate>
                                                                <asp:Label ID="col_itri_com" runat="server" Text='<%# Bind("itri_com") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Center" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Prefer.location">
                                                            <ItemTemplate>
                                                                <asp:Label ID="col_itri_loc" runat="server" Text='<%# Bind("itri_loc") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Center" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Approve Qty">
                                                            <ItemTemplate>
                                                                <asp:Label ID="col_itri_app_qty" runat="server" Text='<%# Bind("itri_app_qty") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Right" Width="78px" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Status">
                                                            <ItemTemplate>
                                                                <asp:Label ID="col_Approv_status" runat="server" Text='<%# Bind("Approv_status") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField Visible="true">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lbtncancelitem" runat="server" CausesValidation="false" OnClick="lbtncancelitem_Click">
                                                                     <span class="glyphicon glyphicon-remove" aria-hidden="true"></span>
                                                                </asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-sm-2">
                                                <div class="col-sm-7 labelText1  ">
                                                    Last Month Sale:
                                                </div>
                                                <div class="col-sm-4 labelText1">
                                                    <asp:Label ID="lblsale" runat="server"></asp:Label>
                                                </div>
                                            </div>
                                            <div class="col-sm-2 ">
                                                <%-- <div class="col-sm-6 labelText1  ">
                                                    S/R stock value
                                                </div>
                                                <div class="col-sm-5">
                                                </div>--%>
                                            </div>
                                            <div class="col-sm-3 ">
                                                <%-- <div class="col-sm-7 labelText1  ">
                                                    Approved MRN value 
                                                </div>
                                                <div class="col-sm-5">
                                                </div>--%>
                                            </div>
                                            <div class="col-sm-2 paddingLeft5">
                                                <%--<div class="col-sm-5 labelText1  ">
                                                    Free value
                                                </div>
                                                <div class="col-sm-5">
                                                </div>--%>
                                            </div>
                                            <div class="col-sm-3 paddingLeft5">
                                                <%--  <div class="col-sm-5 labelText1  ">
                                                    Current MRN value 
                                                </div>
                                                <div class="col-sm-2">
                                                </div>--%>
                                                <div class="col-sm-2">
                                                    Total:
                                                </div>
                                                <div class="col-sm-2">
                                                    <asp:Label ID="lbtnTotalreq" runat="server"></asp:Label>
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
            <asp:Button ID="Button1" runat="server" Text="Button" Style="display: none;" />
            <asp:ModalPopupExtender ID="ApprovedPopup" runat="server" Enabled="True" TargetControlID="Button1"
                PopupControlID="pnlApprovepopup" PopupDragHandleControlID="PopupHeader" Drag="true" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>
        </ContentTemplate>
    </asp:UpdatePanel>

    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
        <ContentTemplate>
            <asp:Panel runat="server" ID="pnlApprovepopup" DefaultButton="lbtnSearch">
                <div runat="server" id="Div1" class="panel panel-default height400 width1300">

                    <asp:Label ID="Label1" runat="server" Text="Label" Visible="false"></asp:Label>
                    <div class="panel panel-default">
                        <div class="panel-heading">
                            <asp:LinkButton ID="LinkButton1" runat="server" OnClick="btnaClose_Click">
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
                                                    <div class="panel panel-default">
                                                        <div class="panel-body">
                                                            <div class="row">
                                                                <div class="col-sm-1 paddingRight0">
                                                                    Item code
                                                                </div>
                                                                <div class="col-sm-1 Lwidth paddingLeft0">
                                                                </div>
                                                                <div class="col-sm-1 paddingRight0 paddingLeft0">
                                                                    Model
                                                                </div>
                                                                <div class="col-sm-1 Lwidth paddingLeft0">
                                                                </div>
                                                                <div class="col-sm-2 paddingRight0 paddingLeft0">
                                                                    Remark
                                                                </div>
                                                                <div class="col-sm-1 paddingRight0 paddingLeft0 Textboxwidth80">
                                                                    Shop stock
                                                                </div>
                                                                <div class="col-sm-1 paddingRight0 paddingLeft0">
                                                                    Forward sales
                                                                </div>
                                                                <div class="col-sm-1 paddingRight0 paddingLeft0">
                                                                    Buffer limit
                                                                </div>
                                                                <div class="col-sm-1 paddingRight0 paddingLeft0">
                                                                    Request qty
                                                                </div>
                                                                <div class="col-sm-1 paddingRight0 paddingLeft0">
                                                                    Req.Company
                                                                </div>
                                                                <div class="col-sm-1 Textboxwidth paddingRight0 paddingLeft0">
                                                                    Prefer.location
                                                                </div>
                                                                <div class="col-sm-1 Textboxwidth paddingRight0 paddingLeft0">
                                                                    Purchase type
                                                                </div>
                                                                <div class="col-sm-1 Textboxwidth paddingRight0 paddingLeft0">
                                                                    Approved qty
                                                                </div>
                                                            </div>
                                                            <div class="row">
                                                                <div class="col-sm-1  paddingRight0 ">
                                                                    <asp:TextBox runat="server" ID="txtpopupItem" Enabled="false" AutoPostBack="true" CausesValidation="false" CssClass="form-control"></asp:TextBox>
                                                                </div>
                                                                <div class="col-sm-1 Lwidth paddingLeft0">
                                                                    <asp:LinkButton ID="lbtnItemSearch2" runat="server" CausesValidation="false" OnClick="lbtnItemSearch2_Click">
                                                <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                                    </asp:LinkButton>
                                                                </div>
                                                                <div class="col-sm-1 paddingRight0 paddingLeft0">
                                                                    <asp:TextBox runat="server" ID="txtpopupModel" Enabled="false" CausesValidation="false" CssClass="form-control"></asp:TextBox>
                                                                </div>
                                                                <asp:Panel runat="server" DefaultButton="btnRemark2">
                                                                    <div class="col-sm-2 paddingRight0 paddingLeft0 ">
                                                                        <asp:TextBox runat="server" ID="txtpopupItemRemark" CausesValidation="false" CssClass="form-control " Width=""></asp:TextBox>
                                                                        <asp:Button ID="btnRemark2" runat="server" OnClick="btnRemark2_Click" Text="Submit" Style="display: none;" />
                                                                    </div>

                                                                </asp:Panel>
                                                                <div class="col-sm-1 paddingRight0 paddingLeft0 Textboxwidth80">
                                                                    <asp:TextBox runat="server" ID="txtpopupshopstock" Enabled="false" onkeypress="return isNumberKey(event)" Style="text-align: right" CausesValidation="false" CssClass="form-control"></asp:TextBox>
                                                                </div>
                                                                <div class="col-sm-1 paddingRight0 paddingLeft0 ">
                                                                    <asp:TextBox runat="server" ID="txtpopupForwardsale" Enabled="false" onkeypress="return isNumberKey(event)" Style="text-align: right" CausesValidation="false" CssClass="form-control"></asp:TextBox>
                                                                </div>
                                                                <div class="col-sm-1 paddingRight0 paddingLeft0 ">
                                                                    <asp:TextBox runat="server" ID="txtpopupBufferLimit" Enabled="false" onkeypress="return isNumberKey(event)" Style="text-align: right" CausesValidation="false" CssClass="form-control"></asp:TextBox>
                                                                </div>
                                                                <div class="col-sm-1 paddingRight0 paddingLeft0 ">
                                                                    <asp:TextBox runat="server" ID="txtpopupRequestqty" Enabled="false" onkeypress="return isNumberKey(event)" Style="text-align: right" CausesValidation="false" CssClass="form-control"></asp:TextBox>
                                                                </div>
                                                                <asp:Panel runat="server" DefaultButton="btnRCompnay2">
                                                                    <div class="col-sm-1 paddingRight0 paddingLeft0 ">
                                                                        <asp:TextBox runat="server" ID="txtpopupIcompany" Enabled="false" CausesValidation="false" CssClass="form-control"></asp:TextBox>
                                                                    </div>
                                                                    <asp:Button ID="btnRCompnay2" runat="server" OnClick="btnRCompnay2_Click" Text="Submit" Style="display: none;" />
                                                                </asp:Panel>
                                                                <div class="col-sm-1 Lwidth paddingLeft0">
                                                                    <asp:LinkButton ID="lbtncomapny2" runat="server" Visible="false" CausesValidation="false" OnClick="lbtncomapny2_Click">
                                                <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                                    </asp:LinkButton>
                                                                </div>
                                                                <asp:Panel runat="server" DefaultButton="btnPrefexLoc2">
                                                                    <div class="col-sm-1 Textboxwidth80 paddingRight0 paddingLeft0">
                                                                        <asp:TextBox runat="server" ID="txtPrefexlocationpopup" Style="text-transform: uppercase;" Enabled="true" CausesValidation="false" CssClass="form-control"></asp:TextBox>
                                                                    </div>
                                                                    <asp:Button ID="btnPrefexLoc2" runat="server" OnClick="btnPrefexLoc2_Click" Text="Submit" Style="display: none;" />
                                                                </asp:Panel>
                                                                <div class="col-sm-1 Lwidth paddingLeft0">
                                                                    <asp:LinkButton ID="lbtnPrefeslocation3" runat="server" Visible="false" CausesValidation="false" OnClick="lbtnPrefeslocation3_Click">
                                                <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                                    </asp:LinkButton>
                                                                    <div class="col-sm-1 Lwidth paddingLeft0">
                                                                        <asp:LinkButton ID="lbtnPrefeslocation4" runat="server" CausesValidation="false" OnClick="lbtnPrefeslocation4_Click">
                                                <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                                        </asp:LinkButton>
                                                                    </div>
                                                                </div>

                                                                <div class="col-sm-1 paddingRight0 paddingLeft0">
                                                                    <asp:DropDownList ID="ddlPurchasetypepopup" Enabled="false" CausesValidation="false" runat="server" CssClass="form-control">
                                                                    </asp:DropDownList>
                                                                </div>
                                                                <asp:Panel runat="server" DefaultButton="btnApproval">

                                                                    <div class="col-sm-1 Textboxwidth paddingRight0 paddingLeft0">
                                                                        <asp:TextBox runat="server" ID="txtpopupApprovalqty" onkeypress="return isNumberKey(event)" CausesValidation="false" Style="text-align: right" CssClass="form-control"></asp:TextBox>
                                                                    </div>
                                                                    <asp:Button ID="Button6" runat="server" OnClick="btnApproval_Click" Text="Submit" Style="display: none;" />
                                                                </asp:Panel>
                                                                <div class="col-sm-1 Lwidth paddingLeft5 paddingRight5">
                                                                    <asp:LinkButton ID="lbtnappAddItem" runat="server" CausesValidation="false" OnClick="lbtnappAddItem_Click">
                                                                <span class="glyphicon glyphicon-plus" aria-hidden="true"></span>
                                                                    </asp:LinkButton>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="panel-body  panelscollbar height200">
                                                        <asp:GridView ID="grdApprovMRNitem" ShowHeader="true" CssClass="table table-hover table-striped" runat="server" GridLines="None" ShowHeaderWhenEmpty="True"
                                                            EmptyDataText="No data found..." AutoGenerateColumns="False">
                                                            <Columns>
                                                                <%--<asp:buttonfield buttontype="Link" commandname="Add" text="Add" ItemStyle-Width="1px"/>--%>

                                                                <asp:TemplateField HeaderText="" Visible="true">
                                                                    <ItemTemplate>
                                                                        <asp:CheckBox ID="chk_AReqItem" AutoPostBack="true" runat="server" onclick="CheckBoxCheckItem(this);" Checked="false" Width="5px" OnCheckedChanged="chk_AReqItem_CheckedChanged_Click" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:CommandField ShowSelectButton="True" Visible="False" />
                                                                <asp:TemplateField>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="col_AITRI_ITM_CD" runat="server" Text='<%# Bind("ITRI_ITM_CD") %>' Width="80px"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField Visible="false">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="col_Aitri_line_no" runat="server" Text='<%# Bind("itri_line_no") %>' Width="10px"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="col_Ami_model" runat="server" Text='<%# Bind("Mst_item_model") %>' Width="80px"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="col_Aitri_note" runat="server" Text='<%# Bind("itri_note")  %>' Width="80px"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="col_Itri_res_noe" runat="server" Text='<%# Bind("Itri_res_no") %>' Width="80px"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="col_Aitri_shop_qty" runat="server" Text='<%# Bind("itri_shop_qty") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Right" Width="70px" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="">
                                                                    <ItemStyle HorizontalAlign="Right" Width="25px" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="col_Aitri_fd_qty" runat="server" Text='<%# Bind("itri_fd_qty") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Right" Width="85px" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="">
                                                                    <ItemStyle HorizontalAlign="Right" Width="50px" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="col_Aitri_buffer" runat="server" Text='<%# Bind("itri_buffer") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Right" Width="68px" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="">
                                                                    <ItemStyle HorizontalAlign="Right" Width="30px" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="col_Aitri_bqty" runat="server" Text='<%# Bind("itri_bqty") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Right" Width="78px" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="">
                                                                    <ItemStyle HorizontalAlign="Right" Width="5px" />
                                                                </asp:TemplateField>
                                                            
                                                                <asp:TemplateField Visible="false" >
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="col_Aitri_bqty2" runat="server" Text='<%# Bind("Backqty") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                     <ItemStyle HorizontalAlign="Right" Width="85px" />
                                                                </asp:TemplateField>

                                                                <asp:TemplateField>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="col_Aitri_com" runat="server" Text='<%# Bind("itri_com") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Center" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="col_Aitri_loc" runat="server" Text='<%# Bind("itri_loc") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                     <ItemStyle HorizontalAlign="Center" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Qty">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="col_Aitri_app_qty" runat="server" Text='<%# Bind("itri_app_qty") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Right" Width="78px" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="col_PoType" runat="server" Text='<%# Bind("PoType") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField Visible="false">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="col_Approv_status" runat="server" Text='<%# Bind("Approv_status") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                   <asp:TemplateField Visible="true" HeaderText="Approve Qty">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="col_itri_qty" runat="server" Text='<%# Bind("itri_qty") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField>

                                                                    <ItemTemplate>
                                                                        <asp:LinkButton ID="lbtngrdItemsDalete" runat="server" CausesValidation="false" OnClick="lbtngrdItemsDalete_Click1">
                                                                                <span aria-hidden="true" class="glyphicon glyphicon-trash"></span>
                                                                        </asp:LinkButton>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                            </Columns>
                                                        </asp:GridView>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-sm-9">
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
    <%--  --%>
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



    <asp:UpdatePanel ID="UpdatePanel4" runat="server">

        <ContentTemplate>

            <asp:Button ID="Button4" runat="server" Text="Button" Style="display: none;" />
            <asp:ModalPopupExtender ID="excelUpload" runat="server" Enabled="True" TargetControlID="Button4"
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
                                <asp:Label ID="Label3" runat="server" ForeColor="Red" Text="Label" Visible="false"></asp:Label>
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


                            <%--<div class="row">--%>
                            <div class="col-sm-12 ">
                                <div id="divUpcompleted" visible="false" runat="server" class="alert alert-info alert-success" role="alert">
                                    <div class="col-sm-9">
                                        <strong>Alert!</strong>
                                        <asp:Label ID="Label4" Text="Excel file upload completed. Do you want to process?" runat="server"></asp:Label>
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


    <asp:UpdatePanel ID="UpdatePanel21" runat="server">
        <ContentTemplate>
            <asp:Button ID="Button7" runat="server" Text="Button" Style="display: none;" />
            <asp:ModalPopupExtender ID="PopupConfBox" runat="server" Enabled="True" TargetControlID="Button7"
                PopupControlID="pnlConfBox" PopupDragHandleControlID="PopupHeader" Drag="true" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>
        </ContentTemplate>
    </asp:UpdatePanel>

    <asp:UpdateProgress ID="UpdateProgress4" DisplayAfter="10"
        runat="server" AssociatedUpdatePanelID="UpdatePanel22">
        <ProgressTemplate>
            <div class="divWaiting">
                <asp:Label ID="lblWait34" runat="server"
                    Text="Please wait... " />
                <asp:Image ID="imgWait34" runat="server"
                    ImageAlign="Middle" ImageUrl="~/images/banners/hidden_progress_bar.gif" />
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>

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




    <asp:UpdatePanel ID="UpdatePanel14" runat="server">
        <ContentTemplate>
            <asp:Button ID="Button5" runat="server" Text="Button" Style="display: none;" />
            <asp:ModalPopupExtender ID="ModalPopupCom" runat="server" Enabled="True" TargetControlID="Button5"
                PopupControlID="pnlmodpopup" CancelControlID="LinkButton2" PopupDragHandleControlID="PopupHeader" Drag="true" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>
        </ContentTemplate>

    </asp:UpdatePanel>

    <asp:Panel runat="server" ID="pnlmodpopup">
        <div runat="server" id="Div5" class="panel panel-default height400 width700">

            <asp:Label ID="Label5" runat="server" Text="Label" Visible="false"></asp:Label>
            <div class="panel panel-default">
                <div class="panel-heading">
                    <asp:LinkButton ID="LinkButton2" runat="server">
                             <span class="glyphicon glyphicon-remove" aria-hidden="true">Close</span>
                    </asp:LinkButton>
                    <%--<span>Commen Search</span>--%>
                    <div class="col-sm-11">
                        <strong>Combine Item</strong>
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
                        </div>
                        <div class="col-sm-7">
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
                                <asp:UpdatePanel ID="UpdatePanel18" runat="server">
                                    <ContentTemplate>
                                        <asp:GridView ID="grdcom" CausesValidation="false" runat="server" EmptyDataText="No data found..." AutoGenerateColumns="false" ShowHeaderWhenEmpty="True" GridLines="None" CssClass="table table-hover table-striped">
                                            <Columns>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lbtncomselect" runat="server" CausesValidation="false" OnClick="lbtncomselect_Click">
                                                <span class="glyphicon glyphicon-hand-right" aria-hidden="true"></span>
                                                        </asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Combine Item Code">
                                                    <ItemTemplate>
                                                        <asp:Label ID="col_comitm" runat="server" Text='<%# Bind("micp_itm_cd") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Item Code">
                                                    <ItemTemplate>
                                                        <asp:Label ID="col_itm" runat="server" Text='<%# Bind("micp_comp_itm_cd") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                        </div>

                        <asp:UpdateProgress ID="UpdateProgress2" DisplayAfter="10"
                            runat="server" AssociatedUpdatePanelID="UpdatePanel18">

                            <ProgressTemplate>
                                <div class="divWaiting">
                                    <asp:Label ID="lblWaitcom" runat="server"
                                        Text="Please wait... " />
                                    <asp:Image ID="imgWaitcm" runat="server"
                                        ImageAlign="Middle" ImageUrl="~/images/banners/hidden_progress_bar.gif" />
                                </div>
                            </ProgressTemplate>

                        </asp:UpdateProgress>
                    </div>
                </div>

            </div>

        </div>
    </asp:Panel>





</asp:Content>
