<%@ Page Title="" Language="C#" MasterPageFile="~/View/AdminSite.Master" AutoEventWireup="true" CodeBehind="StockAdjustment.aspx.cs" Inherits="FastForward.SCMWeb.View.Transaction.Inventory.StockAdjustment" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/UserControls/ucOutScan.ascx" TagPrefix="uc1" TagName="ucOutScan" %>
<%@ Register Src="~/UserControls/ucInScan.ascx" TagPrefix="uc1" TagName="ucInScan" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script>
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

        function ConfirmTempSave() {
            var selectedvalueOrdPlace = confirm("Do you want to Temporary save ?");
            if (selectedvalueOrdPlace) {
                document.getElementById('<%=hdnTempSave.ClientID %>').value = "Yes";
            }
            else {
                document.getElementById('<%=hdnTempSave.ClientID %>').value = "No";
            }
        };

        <%--function ConfirmUpdate() {
            var selectedvalueOrdPlace = confirm("Do you want to Update ?");
            if (selectedvalueOrdPlace) {
                document.getElementById('<%=hdnUpdate.ClientID %>').value = "Yes";
            }
            else {
                document.getElementById('<%=hdnUpdate.ClientID %>').value = "No";
            }
        };--%>

        function ConfirmApprove() {
            var selectedvalueOrdPlace = confirm("Do you want to Approve ?");
            if (selectedvalueOrdPlace) {
                document.getElementById('<%=hdnApprove.ClientID %>').value = "Yes";
            } else {
                document.getElementById('<%=hdnApprove.ClientID %>').value = "No";
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

        function ConfirmItemDelete() {
            var selectedvalueOrdPlace = confirm("Do you want to delete ?");
            if (selectedvalueOrdPlace) {
                document.getElementById('<%=hdnItemDelete.ClientID %>').value = "Yes";
            } else {
                document.getElementById('<%=hdnItemDelete.ClientID %>').value = "No";
            }
        };

        function ConfirmSerialDelete() {
            var selectedvalueOrdPlace = confirm("Do you want to delete ?");
            if (selectedvalueOrdPlace) {
                document.getElementById('<%=hdnSerialDelete.ClientID %>').value = "Yes";
            } else {
                document.getElementById('<%=hdnSerialDelete.ClientID %>').value = "No";
            }
        };
        function ConfirmPrint() {
            var selectedvalueOrdPlace = confirm("Do you want to print Doument ?");
            if (selectedvalueOrdPlace) {
                document.getElementById('<%=hdnprint.ClientID %>').value = "Yes";
            } else {
                document.getElementById('<%=hdnprint.ClientID %>').value = "No";
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
            padding-bottom:0px;
            padding-top:1px;
            margin-bottom:1px;
            margin-top:0px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" runat="server">
    <br />
    <asp:UpdateProgress ID="UpdateProgress2" DisplayAfter="10"
        runat="server" AssociatedUpdatePanelID="UpdatePanel1">
        <ProgressTemplate>
            <div class="divWaiting">
                <asp:Label ID="lblWait8" runat="server"
                    Text="Please wait... " />
                <asp:Image ID="imgWait8" runat="server"
                    ImageAlign="Middle" ImageUrl="~/images/banners/hidden_progress_bar.gif" />
            </div>
        </ProgressTemplate>

    </asp:UpdateProgress>
     <%--<asp:UpdateProgress ID="UpdateProgress1" DisplayAfter="10"
        runat="server" AssociatedUpdatePanelID="mainPnl">
        <ProgressTemplate>
            <div class="divWaiting">
                <asp:Label ID="lblWait821" runat="server"
                    Text="Please wait... " />
                <asp:Image ID="imgWait821" runat="server"
                    ImageAlign="Middle" ImageUrl="~/images/banners/hidden_progress_bar.gif" />
            </div>
        </ProgressTemplate>

    </asp:UpdateProgress>--%>
    <asp:UpdateProgress ID="UpdateProgress3" DisplayAfter="10" runat="server" AssociatedUpdatePanelID="mainPnl">
        <ProgressTemplate>
            <div class="divWaiting">
                <asp:Label ID="lblWait" runat="server"
                    Text="Please wait... " />
                <asp:Image ID="imgWait" runat="server"
                    ImageAlign="Middle" ImageUrl="~/images/icons/ajax-loader.gif" />
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <asp:UpdatePanel runat="server" ID="mainPnl">
        <ContentTemplate>
            <asp:HiddenField ID="hdnSave" runat="server" />
            <asp:HiddenField ID="hdnTempSave" runat="server" />
            <asp:HiddenField ID="hdnApprove" runat="server" />
            <asp:HiddenField ID="hdnCancel" runat="server" />
            <asp:HiddenField ID="hdnClear" runat="server" />
             <asp:HiddenField ID="hdnprint" runat="server" />
            <asp:HiddenField ID="hdnItemDelete" runat="server" />
            <asp:HiddenField ID="hdnSerialDelete" runat="server" />

            <div class="panel panel-default marginLeftRight5 paddingbottom0">
                <div class="panel-body paddingtopbottom0">
                    <div class="row">
                        <div class="col-sm-6  buttonrow">
                      <%--      <div id="WarningGRN" visible="false" runat="server" class="alert alert-danger alert-dismissible" role="alert">
                                <div class="col-sm-11">
                                    <asp:Label ID="lblH1" runat="server" Text="Label"></asp:Label>
                                    <asp:Label ID="lblBackDateInfor" runat="server"></asp:Label>
                                </div>
                            </div>
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
                            </div>--%>
                            <div id="Information" runat="server" visible="false" class="alert alert-success alert-info" role="alert">

                            <strong>Info!</strong>
                            <asp:Label ID="lblBackDateInfor" runat="server"></asp:Label>
                            <asp:Label ID="lblH1" runat="server" Text=""></asp:Label>

                        </div>
                        </div>
                        <div class="col-sm-5  buttonRow">
                            <div class="col-sm-1 paddingRight0">
                            </div>
                            <div class="col-sm-1 paddingRight0">
                            </div>
                              <div class="col-sm-3 paddingRight0 paddingLeft0">
                                <asp:LinkButton ID="lbtnserialprint" CausesValidation="false" runat="server" CssClass="floatRight" OnClick="lbtnserialprint_Click">
                                    <span class="glyphicon glyphicon-print" aria-hidden="true"></span>Serial Print
                                </asp:LinkButton>
                            </div>
                            <div class="col-sm-3 paddingRight0 paddingLeft0">
                                <asp:LinkButton ID="lbtnTempSave" CausesValidation="false" runat="server" CssClass="floatRight"  OnClick="Disable_Click" Enabled="false">
                                    <span class="glyphicon glyphicon-saved" aria-hidden="true"></span>Temporary Save
                                </asp:LinkButton>
                               <%-- <OnClientClick="ConfirmTempSave()">--%>
                            </div>
                            <div class="col-sm-2 paddingRight0 paddingLeft0">
                                <asp:LinkButton ID="lbtnSave" CausesValidation="false" runat="server" CssClass="floatRight" OnClientClick="ConfirmSave()" OnClick="lbtnSave_Click">
                                    <span class="glyphicon glyphicon-saved" aria-hidden="true"></span>Save
                                </asp:LinkButton>
                            </div>
                            <div class="col-sm-2 paddingRight0 paddingLeft0">
                                <asp:LinkButton ID="lbtnprint" CausesValidation="false" runat="server" CssClass="floatRight" OnClientClick="ConfirmPrint()" OnClick="lbtnPrint_Click">
                                    <span class="glyphicon glyphicon-print" aria-hidden="true"></span>Print
                                </asp:LinkButton>
                            </div>
                         <%--   <div class="col-sm-2 paddingRight0 paddingLeft0">
                                <asp:LinkButton ID="lbtnprintserial" CausesValidation="false" runat="server" CssClass="floatRight" OnClientClick="ConfirmPrint()" OnClick="lbtnprintserial_Click">
                                    <span class="glyphicon glyphicon-print" aria-hidden="true"></span>Print Serail
                                </asp:LinkButton>
                            </div>--%>
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
                                <div class="panel-heading"><strong>Stock Adjustment</strong> </div>
                                <div class="col-sm-3 paddingLeft0 buttonRow">
                                                            <div class="col-sm-12  paddingRight0 paddingLeft0">
                                                                <asp:LinkButton ID="lbtnExcelUpload" runat="server" OnClick="lbtnExcelUpload_Click">
                                        <span class="glyphicon glyphicon-upload" aria-hidden="true"></span>Excel Upload
                                                                </asp:LinkButton>
                                                            </div>

                                                        </div>
                                <div class="panel-body">
                                    <div class="row">
                                        <div class="col-sm-10">
                                            <div class="panel panel-default">
                                                <div class="panel-heading">General Details</div>
                                                <div class="panel-body">
                                                    <div class="row">
                                                        <div class="col-sm-6">
                                                            <div class="row">
                                                                <div class="col-sm-4">
                                                                    <div class="row">
                                                                        <div class="col-sm-3 labelText1">
                                                                            Date
                                                                        </div>
                                                                        <div class="col-sm-8 paddingRight5">
                                                                            <asp:TextBox ID="txtDate" runat="server" CssClass="form-control" onkeypress="return RestrictSpace()"
                                                                                Format="dd/MMM/yyyy" Enabled="false"></asp:TextBox>
                                                                        </div>
                                                                        <div class="col-sm-1 paddingLeft0 paddingRight0">
                                                                            <asp:LinkButton ID="lbtnDate" runat="server" CausesValidation="false">
                                                            <span class="glyphicon glyphicon-calendar" aria-hidden="true"></span>
                                                                            </asp:LinkButton>
                                                                            <asp:CalendarExtender ID="CalendarExtender3" runat="server" TargetControlID="txtDate"
                                                                                PopupButtonID="lbtnDate" Format="dd/MMM/yyyy">
                                                                            </asp:CalendarExtender>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class="col-sm-4">
                                                                    <div class="row">
                                                                        <div class="col-sm-3 labelText1">
                                                                            Type
                                                                        </div>
                                                                        <div class="col-sm-9 paddingLeft0">
                                                                            <asp:DropDownList ID="ddlType" runat="server" AutoPostBack="true" CssClass="form-control" OnSelectedIndexChanged="ddlType_SelectedIndexChanged">
                                                                                <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                                                                                <asp:ListItem Text="ADJ+" Value="+"></asp:ListItem>
                                                                                <asp:ListItem Text="ADJ-" Value="-"></asp:ListItem>
                                                                            </asp:DropDownList>
                                                                            <%--<asp:DropDownList ID="ddlType" runat="server" class="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlType_SelectedIndexChanged">
                                                                <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                                                                <asp:ListItem Text="AJD+" Value="+"></asp:ListItem>
                                                                <asp:ListItem Text="AJD-" Value="-"></asp:ListItem>
                                                            </asp:DropDownList>--%>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class="col-sm-4">
                                                                    <div class="row">
                                                                        <div class="col-sm-4 labelText1 paddingLeft5">
                                                                            Sub Type
                                                                        </div>
                                                                        <div class="col-sm-7 paddingRight5 paddingLeft0">
                                                                            <asp:TextBox ID="txtSubType" CausesValidation="false" runat="server" class="form-control" MaxLength="10" AutoPostBack="true" OnTextChanged="txtSubType_TextChanged"></asp:TextBox>
                                                                        </div>
                                                                        <div class="col-sm-1 paddingLeft0 paddingRight0">
                                                                            <asp:LinkButton ID="lbtSubType" runat="server" OnClick="lbtSubType_Click">
                                                            <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                                            </asp:LinkButton>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="col-sm-6">
                                                            <div class="row">
                                                                <div class="col-sm-6">
                                                                    <div class="row">
                                                                        <div class="col-sm-3 labelText1">
                                                                            Ref #
                                                                        </div>
                                                                        <div class="col-sm-9 paddingLeft0">
                                                                            <asp:TextBox ID="txtRef" CausesValidation="false" runat="server" MaxLength="30" class="form-control"></asp:TextBox>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <div class="row">
                                                                        <div class="col-sm-4 labelText1">
                                                                            Other Ref #
                                                                        </div>
                                                                        <div class="col-sm-8 paddingLeft0">
                                                                            <asp:TextBox ID="txtOtherRef" CausesValidation="false" runat="server" MaxLength="30" class="form-control"></asp:TextBox>
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
                                                            <div class="row">
                                                                <div class="col-sm-1 labelText1">
                                                                    Remarks
                                                                </div>
                                                                <div class="col-sm-11 paddingLeft0">
                                                                    <asp:TextBox ID="txtRemarks" CausesValidation="false" TextMode="MultiLine" Rows="2" runat="server" class="form-control" MaxLength="100"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="panel panel-default">
                                                <div class="panel-body">
                                                    <div class="row">
                                                        <div class="col-sm-4">
                                                            <div class="row">
                                                                <div class="col-sm-3 labelText1">
                                                                    Sequence No -
                                                                </div>
                                                                <div class="col-sm-5 paddingRight0 paddingLeft0">
                                                                    <asp:TextBox ID="txtUserSeqNo" CausesValidation="false" runat="server" onkeydown="return jsDecimals(event);" MaxLength="10"
                                                                        class="form-control textAlignRight"></asp:TextBox>
                                                                </div>
                                                                <div class="col-sm-4 paddingRight0">
                                                                    <asp:DropDownList ID="ddlSeqNo" runat="server" class="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlSeqNo_SelectedIndexChanged">
                                                                    </asp:DropDownList>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="col-sm-8">
                                                            <div class="row">
                                                                <div class="col-sm-3 labelText1">
                                                                </div>
                                                                <div class="col-sm-9">
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-sm-2 paddingLeft0">
                                            <div class="panel panel-default">
                                                <div class="panel-heading">Document Search</div>
                                                <div class="panel-body">
                                                    <div class="row">
                                                        <div class="col-sm-12">
                                                            <div class="row">
                                                                <div class="col-sm-3 labelText1">
                                                                    Type
                                                                </div>
                                                                <div class="col-sm-9">
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="row">
                                                        <div class="col-sm-12">
                                                            <div class="row">
                                                                <div class="col-sm-2 labelText1">
                                                                </div>
                                                                <div class="col-sm-6 paddingLeft0">
                                                                    <asp:DropDownList ID="ddlTypeSearch" runat="server" class="form-control">
                                                                        <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                                                                        <asp:ListItem Text="ADJ+" Value="+"></asp:ListItem>
                                                                        <asp:ListItem Text="ADJ-" Value="-"></asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </div>
                                                                <div class="col-sm-4 labelText1">
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="row">
                                                        <div class="col-sm-12">
                                                            <div class="row">
                                                                <div class="col-sm-5 labelText1">
                                                                    Document #
                                                                </div>
                                                                <div class="col-sm-1 paddingRight5 paddingLeft0">
                                                                </div>
                                                                <div class="col-sm-1 paddingLeft0">
                                                                    <asp:CheckBox ID="chkTemporarySave" runat="server" />
                                                                </div>
                                                                <div class="col-sm-5 paddingLeft0 labelText1">
                                                                    Temporary Save
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="row">
                                                        <div class="col-sm-12">
                                                            <div class="row">
                                                                <div class="col-sm-2 labelText1">
                                                                </div>
                                                                <div class="col-sm-8 paddingRight5 paddingLeft0">
                                                                    <asp:TextBox ID="txtDocumentNo" CausesValidation="false" runat="server" class="form-control" AutoPostBack="true" OnTextChanged="txtDocumentNo_TextChanged"></asp:TextBox>
                                                                </div>
                                                                <div class="col-sm-2 paddingLeft0">
                                                                    <asp:LinkButton ID="lbtnDocumentNo" runat="server" OnClick="lbtnDocumentNo_Click">
                                                            <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
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
                                                        <div class="col-sm-12 height5">
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <asp:UpdatePanel runat="server">
                                        <ContentTemplate>
                                            <uc1:ucOutScan runat="server" ID="ucOutScan" Visible="false" />
                                            <uc1:ucInScan runat="server" ID="ucInScan" Visible="false" />
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                    <div class="row">
                                        <div class="col-sm-12">
                                            <div class="panel panel-default ">
                                                <div class="panel-body">
                                                    <div class="bs-example">
                                                        <ul class="nav nav-tabs" id="myTab">
                                                            <li class="active"><a href="#Item" data-toggle="tab" runat="server" id="ItemLi">Item</a></li>
                                                            <li><a href="#Serial" data-toggle="tab">Serial</a></li>
                                                        </ul>
                                                    </div>
                                                    <div class="row">
                                                        <div class="col-sm-12 height5">
                                                        </div>
                                                    </div>
                                                    <div class="tab-content">
                                                        <div class="tab-pane active" id="Item">
                                                            <asp:GridView ID="grdItems"  CssClass="table table-hover table-striped" runat="server" GridLines="None" ShowHeaderWhenEmpty="True"
                                                                EmptyDataText="No data found..." AutoGenerateColumns="False" OnRowDataBound="grdItems_RowDataBound">
                                                                <Columns>
                                                                     <asp:TemplateField>
                                                                        <ItemTemplate>
                                                                            <asp:LinkButton Visible='<%# ddlType.SelectedValue == "+" %>' ID="lbtnGrdSerial" runat="server" CausesValidation="false" OnClick="lbtnGrdSerial_Click">
                                                                <span aria-hidden="true" class="glyphicon glyphicon-arrow-up"></span>
                                                                            </asp:LinkButton>
                                                                        </ItemTemplate>
                                                                        <ItemStyle HorizontalAlign="Center" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField>
                                                                        <ItemTemplate>
                                                                            <asp:LinkButton ID="lbtngrdItemsDalete" runat="server" CausesValidation="false" OnClientClick="ConfirmItemDelete()" OnClick="lbtngrdItemsDalete_Click">
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
                                                                            <asp:Label ID="lblitri_itm_stusD" runat="server" Text='<%# Bind("Itri_itm_stus_desc") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Unit Cost" Visible="false">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblitri_unit_price" runat="server" Text='<%# Bind("itri_unit_price","{0:#,##0.####}") %>' Visible="false"></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="App. Qty">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblitri_app_qty" runat="server" Text='<%# Bind("itri_app_qty","{0:#,##0.####}") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle CssClass="gridHeaderAlignRight" />
                                                                        <ItemStyle CssClass="gridHeaderAlignRight" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Pick Qty">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblitri_qty" runat="server" Text='<%# Bind("itri_qty","{0:#,##0.####}") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle CssClass="gridHeaderAlignRight" />
                                                                        <ItemStyle CssClass="gridHeaderAlignRight" />
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
                                                        <div class="tab-pane" id="Serial">
                                                            <asp:GridView ID="grdSerial" CssClass="table table-hover table-striped" runat="server" GridLines="None" ShowHeaderWhenEmpty="True" OnRowDataBound="grdSerial_RowDataBound"
                                                                EmptyDataText="No data found..." AutoGenerateColumns="False">
                                                                <Columns>
                                                                      <asp:TemplateField HeaderText="Sub serial">
                                                                        <ItemTemplate>
                                                                            <asp:LinkButton ID="lbtngrdviewsubserial" runat="server" CausesValidation="false"  OnClick="lbtngrdviewsubserial_Click">
                                                                <span aria-hidden="true" class="glyphicon glyphicon-arrow-right"></span>
                                                                            </asp:LinkButton>
                                                                        </ItemTemplate>
                                                                           <ItemStyle  Width="80px" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField>
                                                                        <ItemTemplate>
                                                                            <asp:LinkButton ID="lbtngrdSerialDalete" runat="server" CausesValidation="false" OnClientClick="ConfirmSerialDelete()" OnClick="lbtngrdSerialDalete_Click">
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
                                                                            <asp:Label ID="ser_StatusD" runat="server" Text='<%# Bind("Tus_itm_stus_Desc") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Qty">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="ser_Qty" runat="server" Text='<%# Bind("tus_qty","{0:#,##0.####}") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Unit Cost">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="ser_UnitCost" runat="server" Text='<%# Bind("tus_unit_cost","{0:#,##0.####}") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
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
    
    <asp:UpdatePanel runat="server" ID="grdSelectPnl">
        <ContentTemplate>
            <asp:Button ID="btnhidden" runat="server" Text="Button" Style="display: none;" />
            <asp:ModalPopupExtender ID="UserPopup" runat="server" Enabled="True" TargetControlID="btnhidden"
                PopupControlID="pnlModalPopup" CancelControlID="btnClose" PopupDragHandleControlID="PopupHeader" Drag="true" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>
            <asp:Panel runat="server" ID="pnlModalPopup" DefaultButton="lbtnSearch">
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
                                <div class="col-sm-12 height5">
                                </div>
                            </div>
                            <div class="row" id="DateRow" runat="server" visible="false">
                                <div class="col-sm-12">
                                    <div class="col-sm-2 labelText1">
                                        From Date
                                    </div>
                                    <div class="col-sm-3 paddingRight5">
                                        <div class="col-sm-11 paddingRight5 paddingLeft0">
                                            <asp:TextBox ID="txtFromDateSearch" runat="server" CssClass="form-control" onkeypress="return RestrictSpace()"
                                                Format="dd/MMM/yyyy" Enabled="false"></asp:TextBox>
                                        </div>
                                        <div class="col-sm-1 paddingLeft0 paddingRight0">
                                            <asp:LinkButton ID="lbtnFromDateSearch" runat="server" CausesValidation="false">
                                                            <span class="glyphicon glyphicon-calendar" aria-hidden="true"></span>
                                            </asp:LinkButton>
                                            <asp:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtFromDateSearch"
                                                PopupButtonID="lbtnFromDateSearch" Format="dd/MMM/yyyy">
                                            </asp:CalendarExtender>
                                        </div>
                                    </div>
                                    <div class="col-sm-2 labelText1">
                                        To Date
                                    </div>
                                    <div class="col-sm-4 paddingRight5">
                                        <div class="col-sm-8 paddingRight5 paddingLeft0">
                                            <asp:TextBox ID="txtToDateSearch" runat="server" CssClass="form-control" onkeypress="return RestrictSpace()"
                                                Format="dd/MMM/yyyy" Enabled="false"></asp:TextBox>
                                        </div>
                                        <div class="col-sm-1 paddingLeft0 paddingRight0">
                                            <asp:LinkButton ID="lbtnToDateSearch" runat="server" CausesValidation="false">
                                                            <span class="glyphicon glyphicon-calendar" aria-hidden="true"></span>
                                            </asp:LinkButton>
                                            <asp:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtToDateSearch"
                                                PopupButtonID="lbtnToDateSearch" Format="dd/MMM/yyyy">
                                            </asp:CalendarExtender>
                                        </div>
                                    </div>
                                    <div class="col-sm-1 labelText1">
                                    </div>
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
                                    <div class="col-sm-4 paddingRight5">
                                        <asp:TextBox ID="txtSearchbyword" CausesValidation="true" placeholder="Search by word" class="form-control" AutoPostBack="true" runat="server" OnTextChanged="txtSearchbyword_TextChanged"></asp:TextBox>
                                    </div>
                                    <div class="col-sm-1 paddingLeft0">
                                        <asp:LinkButton ID="lbtnSearch" runat="server" OnClick="lbtnSearch_Click">
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
                                <div class="col-sm-12 height5">
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-12 height5">
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-12">
                                    <asp:GridView ID="grdResult" CausesValidation="false" runat="server" OnSelectedIndexChanged="grdResult_SelectedIndexChanged" AllowPaging="True" OnPageIndexChanging="grdResult_PageIndexChanging" GridLines="None" CssClass="table table-hover table-striped" PageSize="7" PagerStyle-CssClass="cssPager">
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
    <asp:UpdatePanel ID="UpdatePanel5" runat="server">
        <ContentTemplate>
            <asp:Button ID="Button1" runat="server" Text="Button" Style="display: none;" />
            <asp:ModalPopupExtender ID="UserDPopoup" runat="server" Enabled="True" TargetControlID="Button1"
                PopupControlID="pnlDpopup" CancelControlID="btnDClose" PopupDragHandleControlID="PopupHeader" Drag="true" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdatePanel ID="UpdatePanel7" runat="server">
        <ContentTemplate>
            <asp:Panel runat="server" ID="pnlDpopup" DefaultButton="lbtnSearch">
                <div runat="server" id="Div1" class="panel panel-default height400 width950">
                    <asp:Label ID="Label1" runat="server" Text="Label" Visible="false"></asp:Label>
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
                            <div class="col-sm-12" id="Div2" runat="server">
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
                                                <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
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
                                                        <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
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
                                <%--<asp:UpdateProgress ID="UpdateProgress1" DisplayAfter="10"
                                    runat="server" AssociatedUpdatePanelID="UpdatePanel7">
                                    <ProgressTemplate>
                                        <div class="divWaiting">
                                            <asp:Label ID="lblWait" runat="server"
                                                Text="Please wait... " />
                                            <asp:Image ID="imgWait" runat="server"
                                                ImageAlign="Middle" ImageUrl="~/images/banners/hidden_progress_bar.gif" />
                                        </div>
                                    </ProgressTemplate>
                                </asp:UpdateProgress>--%>
                            </div>
                        </div>
                    </div>
                </div>
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdatePanel ID="UpdatePanel14" runat="server">
        <ContentTemplate>
            <asp:Button ID="Button2" runat="server" Text="Button" Style="display: none;" />
            <asp:ModalPopupExtender ID="UserAdPopup" runat="server" Enabled="True" TargetControlID="Button2"
                PopupControlID="pnlASearchpopup" CancelControlID="btnAClose" PopupDragHandleControlID="PopupHeader" Drag="true" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdatePanel ID="UpdatePanel18" runat="server">
        <ContentTemplate>
            <asp:Panel runat="server" ID="pnlASearchpopup" DefaultButton="lbtnSearch">
                <div runat="server" id="Div4" class="panel panel-default height400 width700">
                    <asp:Label ID="lblAvalue" runat="server" Text="Label" Visible="false"></asp:Label>
                    <div class="panel panel-default">
                        <div class="panel-heading">
                            <asp:LinkButton ID="btnAClose" runat="server">
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

    <asp:UpdatePanel ID="UpdatePanel25" runat="server">
        <ContentTemplate>
            <asp:Button ID="btnconfbox" runat="server" Text="Button" Style="display: none;" />
            <asp:ModalPopupExtender ID="PopupConfBox" runat="server" Enabled="True" TargetControlID="btnconfbox"
                PopupControlID="pnlConfBox" PopupDragHandleControlID="PopupHeader" Drag="true" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:Panel ID="pnlConfBox" runat="server" align="center">
        <div runat="server" id="Div3" class="panel panel-info height120 width250">
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


    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <asp:Button ID="Button3" runat="server" Text="Button" Style="display: none;" />
        <asp:ModalPopupExtender ID="userSubSerial" runat="server" Enabled="True" TargetControlID="Button3"
            PopupControlID="pnlpopupSubSerial" CancelControlID="LinkButton3" PopupDragHandleControlID="PopupHeader" Drag="true" BackgroundCssClass="modalBackground">
        </asp:ModalPopupExtender>
    </ContentTemplate>
</asp:UpdatePanel>

<asp:UpdatePanel ID="UpdatePanel2" runat="server">
    <ContentTemplate>
        <asp:Panel runat="server" ID="pnlpopupSubSerial">
            <div class="panel panel-default  height400 width800">
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
                <%--<asp:UpdatePanel ID="UpdatePanel5" runat="server">
                        <ContentTemplate>--%>
                <div class="panel-body">
                    <div class="row">
                        <div class="col-sm-12">
                            <div class="panel panel-default">
                                <div class="panel-body panelscollbar height200">
                                    <asp:GridView ID="GgdsubItem" EmptyDataText="No data found..." runat="server" GridLines="None" CssClass="table table-hover table-striped" AutoGenerateColumns="false">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Update">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lbtnupdate" runat="server" CausesValidation="false" OnClick="lbtnupdate_Click" Width="5px">
                                                                        <span class="glyphicon glyphicon-pencil" aria-hidden="true" ></span>
                                                    </asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Main Item Serial" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="ID" runat="server" Text='<%# Bind("ID") %>' Width="100px"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Main Item Serial">
                                                <ItemTemplate>
                                                    <asp:Label ID="tpss_m_ser" runat="server" Text='<%# Bind("tpss_m_ser") %>' Width="100px"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Item Code">
                                                <ItemTemplate>
                                                    <asp:Label ID="tpss_itm_cd" runat="server" Text='<%# Bind("tpss_itm_cd") %>' Width="150px"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Sub Item type">
                                                <ItemTemplate>
                                                    <asp:Label ID="tpss_tp" runat="server" Text='<%# Bind("tpss_tp") %>' Width="100px"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Sub Item Status">
                                                <ItemTemplate>
                                                    <asp:Label ID="mis_desc" runat="server" Text='<%# Bind("mis_desc") %>' Width="100px"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Sub Item type" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="tpss_itm_stus" runat="server" Text='<%# Bind("tpss_itm_stus") %>' Width="100px"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Sub Item Serial">
                                                <ItemTemplate>
                                                    <asp:Label ID="tpss_sub_ser" runat="server" Text='<%# Bind("tpss_sub_ser") %>' Width="100px"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-sm-6">
                            <div class="panel panel-default">
                                <div class="panel-body">
                                    <div class="row">
                                        <div class="col-sm-3 labelText1">
                                            Sub Product
                                        </div>
                                        <div class="col-sm-9">
                                            <asp:TextBox runat="server" ID="txtSubproduct" Enabled="false" CausesValidation="false" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-sm-3 labelText1">
                                            Item Status
                                        </div>
                                        <div class="col-sm-9">
                                            <asp:DropDownList ID="ddlSIStatus" CausesValidation="false" runat="server" CssClass="form-control">
                                                <asp:ListItem Text=" - - Select - - " Value="0" Selected="True"></asp:ListItem>
                                            </asp:DropDownList>

                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-sm-3 labelText1">
                                            Serial #
                                        </div>
                                        <div class="col-sm-9">
                                            <asp:TextBox runat="server" ID="tstSubSerial" CausesValidation="false" CssClass="form-control" OnTextChanged="tstSubSerial_TextChanged"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-sm-6">
                            <div class="panel panel-default">
                                <div class="panel-body" runat="server" visible="false">
                                    <div class="row">
                                        <div class="col-sm-3 labelText1">
                                            Warranty No
                                        </div>
                                        <div class="col-sm-9">
                                            <asp:TextBox runat="server" ID="txtWNo" Enabled="false" CausesValidation="false" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <%-- </ContentTemplate>
    </asp:UpdatePanel>--%>
            </div>
        </asp:Panel>
    </ContentTemplate>
</asp:UpdatePanel>

     <asp:UpdatePanel ID="UpdatePanel3" runat="server">
    <ContentTemplate>
        <asp:Button ID="Button4" runat="server" Text="Button" Style="display: none;" />
        <asp:ModalPopupExtender ID="userSubSerialview" runat="server" Enabled="True" TargetControlID="Button4"
            PopupControlID="pnlpopupSubSerialview" CancelControlID="LinkButton1" PopupDragHandleControlID="PopupHeader" Drag="true" BackgroundCssClass="modalBackground">
        </asp:ModalPopupExtender>
    </ContentTemplate>
</asp:UpdatePanel>
    <asp:UpdatePanel ID="UpdatePanel4" runat="server">
    <ContentTemplate>
        <asp:Panel runat="server" ID="pnlpopupSubSerialview">
            <div class="panel panel-default   width800">
                <div class="panel-heading">
                    <asp:LinkButton ID="LinkButton1" runat="server">
                             <span class="glyphicon glyphicon-remove" aria-hidden="true">Close</span>
                    </asp:LinkButton>
                    <%--<span>Commen Search</span>--%>
                    <div class="col-sm-11">
                       <strong>Sub Serial List</strong> 
                    </div>
                    <div class="col-sm-1">
                    </div>
                </div>
                <%--<asp:UpdatePanel ID="UpdatePanel5" runat="server">
                        <ContentTemplate>--%>
                <div class="panel-body">
                    <div class="row">
                        <div class="col-sm-12">
                            <div class="panel panel-default">
                                <div class="panel-body panelscollbar height200">
                                    <asp:GridView ID="grdviewsubserials" EmptyDataText="No data found..." runat="server" GridLines="None" CssClass="table table-hover table-striped" AutoGenerateColumns="false">
                                        <Columns>                                           
                                           
                                            
                                            <asp:TemplateField HeaderText="Item Code">
                                                <ItemTemplate>
                                                    <asp:Label ID="irsms_itm_cd" runat="server" Text='<%# Bind("irsms_itm_cd") %>' Width="150px"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Sub Item type">
                                                <ItemTemplate>
                                                    <asp:Label ID="irsms_tp" runat="server" Text='<%# Bind("irsms_tp") %>' Width="100px"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Sub Item Status" >
                                                <ItemTemplate>
                                                    <asp:Label ID="Irsms_itm_stus_text" runat="server" Text='<%# Bind("Irsms_itm_stus_text") %>' Width="100px"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            
                                            <asp:TemplateField HeaderText="Sub Item Serial">
                                                <ItemTemplate>
                                                    <asp:Label ID="irsms_sub_ser" runat="server" Text='<%# Bind("irsms_sub_ser") %>' Width="100px"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>
                        </div>
                    </div>
               
                </div>
                <%-- </ContentTemplate>
    </asp:UpdatePanel>--%>
            </div>
        </asp:Panel>
    </ContentTemplate>
</asp:UpdatePanel>
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
    <asp:UpdatePanel ID="UpdatePanel6" runat="server">
        <ContentTemplate>
           <asp:Button ID="btn15" runat="server" Text="Button" Style="display: none;" />
            <asp:ModalPopupExtender ID="popupErro" runat="server" Enabled="True" TargetControlID="btn15"
                PopupControlID="pnlExcelErro" PopupDragHandleControlID="PopupHeader" Drag="true" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>
        </ContentTemplate>
    </asp:UpdatePanel>

    <asp:UpdatePanel runat="server" ID="UpdatePanel8">
        <ContentTemplate>
            <asp:Panel runat="server" ID="pnlExcelErro">
                <div runat="server" id="Div7" class="panel panel-primary" style="padding: 5px;">
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
    <asp:UpdatePanel runat="server" ID="UpdatePanel9">
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
                <div runat="server" id="Div8" class="panel panel-primary" style="padding: 1px;">
                    <div class="panel panel-default" style="height: 40px; width: 500px;">
                        <div class="panel-heading" style="height: 40px;">
                            <div class="col-sm-8">
                                <strong>Upload excel data</strong>
                            </div>
                            <div class="col-sm-2 text-right padding0">
                                <asp:Button Text="Upload" ID="btnProcess" OnClick="btnProcess_Click" runat="server" />
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



        <script src="<%=Request.ApplicationPath%>Js/jquery.min.js"></script>
    <script src="<%=Request.ApplicationPath%>Js/bootstrap.min.js"></script>
    <script src="<%=Request.ApplicationPath%>Js/jquery.toastmessage.js"></script>

</asp:Content>
