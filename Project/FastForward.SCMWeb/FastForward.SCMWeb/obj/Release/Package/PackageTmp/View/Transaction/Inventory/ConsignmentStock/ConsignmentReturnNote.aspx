<%@ Page Title="" Language="C#" MasterPageFile="~/View/AdminSite.Master" AutoEventWireup="true" CodeBehind="ConsignmentReturnNote.aspx.cs" Inherits="FastForward.SCMWeb.View.Transaction.Inventory.Consignment_Stock.ConsignmentReturnNote" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/UserControls/ucOutScan.ascx" TagPrefix="uc1" TagName="ucOutScan" %>


<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">

    <script>
        function SaveConfirm() {
            var selectedvalue = confirm("Do you want to save data?");
            if (selectedvalue) {
                document.getElementById('<%=txtSavelconformmessageValue.ClientID %>').value = "Yes";
            } else {
                document.getElementById('<%=txtSavelconformmessageValue.ClientID %>').value = "No";
            }
        };
        function TempSaveConfirm() {
            var selectedvalue = confirm("Do you want to temporary save data?");
            if (selectedvalue) {
                document.getElementById('<%=txtTempSavelconformmessageValue.ClientID %>').value = "Yes";
            } else {
                document.getElementById('<%=txtTempSavelconformmessageValue.ClientID %>').value = "No";
            }
        };

        function ConfirmSave() {
            var selectedvalueOrd = confirm("Do you want to temp save ?");
            if (selectedvalueOrd) {
                return true;
            } else {
                return false;
            }
        };
        function ConfirmTempSave() {
            var varrr = confirm("Do you want to save ?");
            if (varrr) {
                return true;
            } else {
                return false;
            }
        };
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
        }


        function showSuccessToast() {
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
            $().toastmessage('showNoticeToast', "Notice  Dialog which is fading away ...");
        }

        function showStickyNoticeToast(value) {
            if (jQuery('.toast-item-wrapper') != null) {
                jQuery('.toast-item-wrapper').remove();
            }
            $().toastmessage('showToast', {
                text: value,
                sticky: true,
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

        function ConfClear() {
            var selectedvalueOrd = confirm("Do you want to clear all details ?");
            if (selectedvalueOrd) {
                return true;
            } else {
                return false;
            }
        };
        function ConfirmClearForm() {
            var selectedvalueOrd = confirm("Do you want to clear all details ?");
            if (selectedvalueOrd) {
                document.getElementById('<%=txtconfirmclear.ClientID %>').value = "Yes";
            } else {
                document.getElementById('<%=txtconfirmclear.ClientID %>').value = "No";
            }
        };

        function ConfirmPlaceOrder() {
            conso.log('Test');
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

        function ConfirmSendToPDA() {
            var selectedvaldelitm = confirm("Are you sure you want to send ?");
            if (selectedvaldelitm) {
                document.getElementById('<%=txtpdasend.ClientID %>').value = "Yes";
            } else {
                document.getElementById('<%=txtpdasend.ClientID %>').value = "No";
            }
        };

        function Enable() {
            return;
        }

    </script>

    <style type="text/css">
        body {
            /*overflow-x: hidden;*/
        }
    </style>

    <style type="text/css">
        body {
            overflow-y: hidden;
        }
    
        .panel {
            margin-bottom:0px;
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
            <asp:HiddenField ID="txtTempSavelconformmessageValue" runat="server" />
            <asp:HiddenField ID="txtSavelconformmessageValue" runat="server" />

            <div class="panel panel-default marginLeftRight5">

                <div class="row">
                   

                    <div class="col-sm-8  buttonrow">

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

                    <div class="col-sm-4  buttonRow crnbuttonrowmargin">

                        <div id="Information" runat="server" visible="false" class="alert alert-success alert-info" role="alert">
                            <div class="col-sm-11">
                                <strong>Info!</strong>
                                <asp:Label ID="lblBackDateInfor" runat="server"></asp:Label>
                                <asp:Label ID="lblH1" runat="server" Text="Label"></asp:Label>
                            </div>
                            <div class="col-sm-1">
                            </div>

                        </div>

                         <div class="col-sm-2 paddingRight0">
                            <asp:LinkButton ID="lbtnprint" CausesValidation="false" runat="server" CssClass="floatRight"  OnClick="lbtnprint_Click">
                                                        <span class="glyphicon glyphicon-print" aria-hidden="true"></span>Print
                            </asp:LinkButton>
                        </div>
                        <div class="col-sm-3 paddingRight0">
                            <asp:LinkButton ID="lbtntempsave" Visible="true" CausesValidation="false" runat="server" CssClass="floatRight" OnClientClick="TempSaveConfirm();" OnClick="lbtntempsave_Click">
                            <span class="glyphicon glyphicon-saved" aria-hidden="true"></span>Temp Save
                            </asp:LinkButton>
                        </div>

                        <div class="col-sm-2 paddingRight0">
                            <asp:LinkButton ID="lbtnsave" CausesValidation="false" runat="server" CssClass="floatRight" OnClientClick="SaveConfirm();" OnClick="lbtnsave_Click">
                            <span class="glyphicon glyphicon-saved" aria-hidden="true"></span>Save
                            </asp:LinkButton>
                        </div>

                        <div class="col-sm-3 paddingRight0">
                            <asp:LinkButton ID="lbtncancel" CausesValidation="false" runat="server" CssClass="floatRight" OnClientClick="ConfirmCancel();" OnClick="lbtncancel_Click">
                                                        <span class="glyphicon glyphicon-remove" aria-hidden="true"></span>Cancel
                            </asp:LinkButton>
                        </div>

                        <div class="col-sm-2 paddingRight0">
                            <asp:LinkButton ID="lbtnclear" runat="server" CssClass="floatRight" OnClientClick="return ConfClear();" OnClick="lbtnclear_Click">
                            <span class="glyphicon glyphicon-refresh" aria-hidden="true"></span>Clear
                            </asp:LinkButton>
                        </div>

                        <%--      <div class="col-sm-3 paddingRight0 ">
                            <asp:LinkButton ID="btnPrint" CausesValidation="false" runat="server" CssClass="floatRight">
                                                        <span class="glyphicon glyphicon-print" aria-hidden="true"></span>Print 
                            </asp:LinkButton>
                        </div>--%>
                    </div>

                </div>

                <div class="row">

                    <div class="panel-body">

                        <div class="col-sm-9">

                            <div class="panel panel-default">

                                <div class="panel-heading pannelheading paddingtopbottom0">
                                    <strong>Consignment Return Note</strong>
                                </div>

                                <div class="panel-body">

                                    <div class="col-sm-3">
                                        <div class="row">

                                            <div class="col-sm-4 labelText1">
                                                Date
                                            </div>

                                            <div>
                                                <div class="col-sm-7">
                                                    <asp:TextBox ID="dtpDate" runat="server" CssClass="form-control" Format="dd/MMM/yyyy"
                                                        onkeypress="return RestrictSpace()" Enabled="false"></asp:TextBox>
                                                    <asp:CalendarExtender ID="calexdate" runat="server" TargetControlID="dtpDate"
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

                                    <div class="col-sm-3">
                                        <div class="row">

                                            <div class="col-sm-3 labelText1">
                                                Supplier
                                            </div>

                                            <div class="col-sm-8 paddingRight5">
                                                <asp:TextBox ID="txtSupplierCd" runat="server" CssClass="form-control" AutoPostBack="true" OnTextChanged="txtSupplierCd_TextChanged"></asp:TextBox>
                                            </div>
                                            <div class="col-sm-1 paddingLeft0">
                                                <asp:LinkButton ID="btnSearchSupp" runat="server" TabIndex="2" OnClick="btnSearchSupp_Click">
                                                        <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                </asp:LinkButton>
                                            </div>

                                        </div>
                                    </div>

                                    <div class="col-sm-3">
                                        <div class="row">

                                            <div class="col-sm-4 labelText1">
                                                Manual Ref 
                                            </div>

                                            <div class="col-sm-8 paddingRight5">
                                                <asp:TextBox ID="txtManualRef" TabIndex="3" runat="server" CssClass="form-control"></asp:TextBox>
                                            </div>

                                        </div>
                                    </div>

                                    <div class="col-sm-3">
                                        <div class="row">

                                            <div class="col-sm-4 labelText1">
                                                Other Ref 
                                            </div>

                                            <div class="col-sm-8 paddingRight5">
                                                <asp:TextBox ID="txtOtherRef" runat="server" TabIndex="4" CssClass="form-control"></asp:TextBox>
                                            </div>

                                        </div>
                                    </div>

                                    <div class="row">
                                        <div class="col-sm-12 height5">
                                        </div>
                                    </div>

                                    <div class="col-sm-3">
                                        <div class="row">

                                            <div class="col-sm-4 labelText1">
                                                Type 
                                            </div>

                                            <div class="col-sm-8 paddingRight5">
                                                <asp:DropDownList runat="server" TabIndex="5" AutoPostBack="true" CssClass="form-control" Enabled="false" ID="ddlAdjType" OnSelectedIndexChanged="ddlAdjType_SelectedIndexChanged">
                                                    <asp:ListItem>ADJ+</asp:ListItem>
                                                    <asp:ListItem>ADJ-</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>

                                            <div class="paddingRight5 labelText1">
                                                <asp:Label runat="server" ID="lblSubTypeDesc"></asp:Label>
                                            </div>

                                        </div>
                                    </div>

                                    <div class="col-sm-3">
                                        <div class="row">

                                            <div class="col-sm-3 labelText1">
                                                Sub Type
                                            </div>

                                            <div class="col-sm-8 paddingRight5">
                                                <asp:TextBox ID="txtAdjSubType" runat="server" CssClass="form-control" AutoPostBack="true" ReadOnly="true" OnTextChanged="txtAdjSubType_TextChanged"></asp:TextBox>
                                            </div>
                                            <div class="col-sm-1 paddingLeft0">
                                                <asp:LinkButton ID="btnSearch_AdjSubType" runat="server" TabIndex="6" OnClick="btnSearch_AdjSubType_Click" Visible="false">
                                                        <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                </asp:LinkButton>
                                            </div>

                                        </div>
                                    </div>

                                    <div class="col-sm-3">
                                        <div class="row">

                                            <div class="col-sm-4 labelText1">
                                                Remarks 
                                            </div>

                                            <div class="col-sm-8 paddingRight5">
                                                <asp:TextBox ID="txtRemarks" TabIndex="7" Width="385px" Height="33px" runat="server" CssClass="form-control" TextMode="MultiLine" Style="resize: none"></asp:TextBox>
                                            </div>

                                        </div>
                                    </div>

                                </div>

                            </div>

                        </div>

                        <div class="col-sm-3">

                            <div class="panel panel-default">

                                <div class="panel-heading pannelheading " style="height:22px;">
                                    <div class="col-sm-4 padding0   ">
                                    Document Search
                                    </div>
                                    <div class="col-sm-8">
                                        <div class="col-sm-8 padding0" style="text-align:right">
                                            <strong>Send to PDA</strong>
                                        </div>

                                        <div class="col-sm-1" style="padding-left:3px; padding-right:0px;">
                                            <asp:CheckBox runat="server" ID="chkpda" Enabled="false" AutoPostBack="true" OnCheckedChanged="chkpda_CheckedChanged" />
                                        </div>
                                    </div>
                                </div>

                                <div class="panel-body">

                                    <div class="col-sm-12">
                                        <div class="row">

                                            <div class="col-sm-2 labelText1">
                                                Type 
                                            </div>

                                            <div class="col-sm-8 paddingRight5">
                                                <asp:DropDownList runat="server" AutoPostBack="true" CssClass="form-control" Enabled="false" ID="ddlAdjTypeSearch">
                                                    <asp:ListItem>ADJ+</asp:ListItem>
                                                    <asp:ListItem>ADJ-</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>

                                        </div>
                                    </div>

                                    <div class="row">
                                        <div class="col-sm-12 height5">
                                        </div>
                                    </div>

                                    <div class="col-sm-12">
                                        <div class="row">

                                            <div class="col-sm-2 labelText1">
                                                Doc # 
                                            </div>

                                            <div class="col-sm-7 paddingRight5">
                                                <asp:TextBox ID="txtDocumentNo" runat="server" AutoPostBack="true" OnTextChanged="txtDocumentNo_TextChanged" CssClass="form-control"></asp:TextBox>
                                            </div>

                                            <div class="col-sm-1 paddingLeft0">
                                                <asp:LinkButton ID="btnSearch_DocumentNo" runat="server" OnClick="btnSearch_DocumentNo_Click">
                                                        <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                </asp:LinkButton>
                                            </div>

                                            <div class="col-sm-2" style="margin-top: -14px">
                                                <div id="tempchk" class="tempchkboxmargin">
                                                    <asp:CheckBox runat="server" ID="chktemp" AutoPostBack="true" Text="Temp" OnCheckedChanged="chktemp_CheckedChanged" />
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

                    <div class="panel-body paddingbottom0 paddingtop0">

                        <div class="col-sm-12">

                            <div class="panel panel-default">

                                <div class="panel-heading pannelheading paddingtopbottom0">
                                    Request Bulk Search
                                </div>
                                <asp:Panel runat="server" ID="panelReqestAdd">
                                <div class="panel-body">

                                    <div class="col-sm-3 padding0">
                                    <div class="col-sm-6 padding0">
                                        <div class="row">

                                            <div class="col-sm-2 labelText1">
                                                From
                                            </div>

                                            <div>
                                                <div class="col-sm-7">
                                                    <asp:TextBox ID="txtfromdate" runat="server" CssClass="form-control" Format="dd/MMM/yyyy"
                                                        onkeypress="return RestrictSpace()" Enabled="false"></asp:TextBox>
                                                    <asp:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtfromdate"
                                                        PopupButtonID="lbtnfindfromdate" Format="dd/MMM/yyyy">
                                                    </asp:CalendarExtender>
                                                </div>

                                                <div id="caldvs" class="col-sm-1 paddingLeft3" style="margin-left: -10px; margin-top: -2px">
                                                    <asp:LinkButton ID="lbtnfindfromdate" TabIndex="10" CausesValidation="false" runat="server">
                                                        <span class="glyphicon glyphicon-calendar" aria-hidden="true"></span>
                                                    </asp:LinkButton>
                                                </div>
                                            </div>

                                        </div>
                                    </div>

                                    <div class="col-sm-6 padding0">
                                        <div class="row">

                                            <div class="col-sm-2 labelText1">
                                                To
                                            </div>

                                            <div>
                                                <div class="col-sm-7">
                                                    <asp:TextBox ID="txttodate" runat="server" CssClass="form-control" Format="dd/MMM/yyyy"
                                                        onkeypress="return RestrictSpace()" Enabled="false"></asp:TextBox>
                                                    <asp:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txttodate"
                                                        PopupButtonID="lbtntodate" Format="dd/MMM/yyyy">
                                                    </asp:CalendarExtender>
                                                </div>

                                                <div id="calddvs" class="col-sm-1 paddingLeft3" style="margin-left: -10px; margin-top: -2px">
                                                    <asp:LinkButton ID="lbtntodate" TabIndex="11" CausesValidation="false" runat="server">
                                                        <span class="glyphicon glyphicon-calendar" aria-hidden="true"></span>
                                                    </asp:LinkButton>
                                                </div>
                                            </div>

                                        </div>
                                    </div>
                                    </div>
                                    <div class="col-sm-3">
                                        <div class="row">

                                            <div class="col-sm-2 labelText1 padding0">
                                                Request #
                                            </div>

                                            <div class="col-sm-9 padding0">
                                                <asp:TextBox ID="txtfindreq" runat="server" CssClass="form-control" AutoPostBack="true" OnTextChanged="txtfindreq_TextChanged"></asp:TextBox>
                                            </div>
                                            <div class="col-sm-1 paddingLeft3">
                                                <asp:LinkButton ID="lbtnfindreq" runat="server" TabIndex="13" OnClick="lbtnfindreq_Click">
                                                        <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                </asp:LinkButton>
                                            </div>

                                        </div>
                                    </div>

                                    <div class="col-sm-3">
                                        <div class="row">

                                            <div class="col-sm-4 labelText1">
                                                Sequence No: 
                                            </div>

                                            <div class="col-sm-4 paddingRight5">
                                                <asp:TextBox ID="txtUserSeqNo" TabIndex="8" ReadOnly="true" runat="server" CssClass="form-control"></asp:TextBox>
                                            </div>

                                            <div class="col-sm-4 paddingRight5">
                                                <asp:DropDownList runat="server" ID="ddlSeqNo" AutoPostBack="true" CssClass="form-control" TabIndex="9" 
                                                    AppendDataBoundItems="true"
                                                    OnSelectedIndexChanged="ddlSeqNo_SelectedIndexChanged">
                                                    <asp:ListItem Value="0" Text=""></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>

                                        </div>
                                    </div>

                                    <div class="col-sm-1">
                                        <div class="row">

                                            <div class="col-sm-12">
                                                <asp:LinkButton ID="lbtnfindall" runat="server" TabIndex="14" OnClick="lbtnfindall_Click">
                                                        <span class="glyphicon glyphicon-search" aria-hidden="true" style="font-size:20px"></span>Search All
                                                </asp:LinkButton>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-2 padding0">
                                        <div class="row">

                                            <div class="col-sm-5 padding0">
                                                <asp:LinkButton ID="lbtnaddselected" runat="server" TabIndex="15" OnClick="lbtnaddselected_Click">
                                                        <span class="glyphicon glyphicon-plus" aria-hidden="true" style="font-size:20px"></span>Add Selected
                                                </asp:LinkButton>
                                            </div>
                                            <div class="col-sm-7 padding0" style="margin-top:2px;">
                                                <div class="col-sm-4 padding0 labelText1" >
                                                    Batch # 
                                                </div>
                                                 <div class="col-sm-8 padding0" style="margin-top:2px;">
                                                     <asp:Label ID="lblBatchNo" runat="server" CssClass=""/>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    
                                    <div class="row">
                                        <div class="col-sm-12 height5">
                                        </div>
                                    </div>

                                    <div class="col-sm-12">

                                        <div class="panel panel-default " id="dvContentsOrder1">

                                            <div class="panel-body" id="panelbodydiv1">

                                                <div class="row">
                                                    <div class="col-sm-12">

                                                        <div class="panelscoll75">

                                                            <asp:GridView ID="gvsearchdata" AutoGenerateColumns="false" runat="server" CssClass="table table-hover table-striped" GridLines="None" ShowHeaderWhenEmpty="True" EmptyDataText="No data found...">

                                                                <Columns>

                                                                    <asp:TemplateField>
                                                                        <HeaderTemplate>
                                                                            <asp:CheckBox ID="chkHeader" runat="server" AutoPostBack="true" OnCheckedChanged="chkHeader_CheckedChanged" />
                                                                        </HeaderTemplate>
                                                                        <ItemTemplate>
                                                                            <asp:CheckBox ID="chkselect" runat="server" />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>

                                                                    <asp:TemplateField HeaderText="Request #">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lbldocno" runat="server" Text='<%# Bind("inb_doc_no") %>' Width="150px"></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>

                                                                    <asp:TemplateField HeaderText="Request Date">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lbldocdate" runat="server" Text='<%# Bind("inb_doc_dt", "{0:dd/MMM/yyyy}") %>' Width="150px"></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>

                                                                    <asp:TemplateField HeaderText="Item">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblitemcode" runat="server" Text='<%# Bind("inb_itm_cd") %>' Width="150px"></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>

                                                                    <asp:TemplateField HeaderText="Free Qty">
                                                                        <ItemTemplate>
                                                                            <div class="grid_label_right_align">
                                                                                <asp:Label ID="lblfreeqty" runat="server" Text='<%# Bind("inb_free_qty","{0:n}") %>' Width="150px"></asp:Label>
                                                                            </div>
                                                                        </ItemTemplate>
                                                                        <ItemStyle HorizontalAlign="Center" />
                                                                        <HeaderStyle HorizontalAlign="Right" />
                                                                        <%--<ItemStyle Width="6%" HorizontalAlign="Right" />
                                                                        <HeaderStyle HorizontalAlign="Right" />--%>
                                                                    </asp:TemplateField>

                                                                    <asp:TemplateField HeaderText="Supplier Code">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblsupplier" runat="server" Text='<%# Bind("its_orig_supp") %>' Width="150px"></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>

                                                                    <asp:TemplateField HeaderText="Supplier Name">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblsuppliername" runat="server" Text='<%# Bind("mbe_name") %>' Width="200px"></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>

                                                                    <asp:TemplateField HeaderText="Item Status" Visible="false">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblitmstatus" runat="server" Text='<%# Bind("inb_itm_stus") %>' Width="1px"></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>

                                                                    <%--<asp:TemplateField HeaderText="Serial ID" Visible="true">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblserid" runat="server" Text='<%# Bind("its_ser_id") %>' Width="1px"></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>--%>

                                                                 <%--   <asp:TemplateField HeaderText="Serial 1" Visible="false">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblser1" runat="server" Text='<%# Bind("its_ser_1") %>' Width="1px"></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>--%>

                                                                 <%--   <asp:TemplateField HeaderText="Serial 2" Visible="false">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblser2" runat="server" Text='<%# Bind("its_ser_2") %>' Width="1px"></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>--%>

                                                                    <asp:TemplateField HeaderText="Short Description" Visible="false">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblshortdesc" runat="server" Text='<%# Bind("mi_shortdesc") %>' Width="1px"></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>

                                                                    <asp:TemplateField HeaderText="Model" Visible="false">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblmodel" runat="server" Text='<%# Bind("mi_model") %>' Width="1px"></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>

                                                                    <asp:TemplateField HeaderText="Unit Price" Visible="false">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblunitprice" runat="server" Text='<%# Bind("its_unit_cost") %>' Width="1px"></asp:Label>
                                                                        </ItemTemplate>
                                                                        <ItemStyle Width="6%" HorizontalAlign="Right" />
                                                                        <HeaderStyle HorizontalAlign="Right" />
                                                                    </asp:TemplateField>

                                                                    <asp:TemplateField HeaderText="Bin" Visible="false">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblbin" runat="server" Text='<%# Bind("its_bin") %>' Width="1px"></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                     <asp:TemplateField HeaderText="Company" Visible="false">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblComCd" runat="server" Text='<%# Bind("INS_COM") %>' Width="1px"></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Loc" Visible="false">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblLocCd" runat="server" Text='<%# Bind("INS_LOC") %>' Width="1px"></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>

                                                                </Columns>
                                                                <SelectedRowStyle BackColor="Silver" />
                                                            </asp:GridView>

                                                        </div>


                                                    </div>

                                                </div>

                                            </div>

                                        </div>

                                    </div>

                                </div>
                                    </asp:Panel>
                            </div>
                        </div>
                    </div>
                </div>

                <div class="row">

                    <div class="panel-body">
                        <div class="col-sm-12">
                            <div class="panel panel-default">

                                <div class="panel-heading pannelheading paddingtopbottom0">
                                    Item Details
                                   <%-- <div class="col-sm-12">
                                        <div class="row">
                                            <div class="col-sm-3 paddingLeft0">
                                                <div class="col-sm-1">
                                                    <div class="row ">
                                                        <asp:LinkButton ID="lbtnadditem" runat="server" CausesValidation="false" OnClick="lbtnadditem_Click">
                                                <span class="glyphicon glyphicon-plus" aria-hidden="true"></span>
                                                        </asp:LinkButton>
                                                    </div>
                                                </div>
                                                <div class="col-sm-10 labelText1 paddingLeft0">
                                                    Item Details
                                                </div>
                                            </div>
                                            <div class="col-sm-8 labelText1">
                                            </div>
                                        </div>
                                    </div>--%>
                                    
                                </div>

                                <div class="panel-body">
                                    <asp:Panel runat="server" ID="PanelItemAdd">
                                    <div class="col-sm-3">

                                        <div class="row">

                                            <div class="col-sm-3 labelText1">
                                                Item
                                            </div>

                                            <div class="col-sm-8 paddingRight5">
                                                <asp:TextBox ID="txtItem" runat="server" AutoPostBack="true" OnTextChanged="txtItem_TextChanged" CssClass="form-control"></asp:TextBox>
                                            </div>

                                            <div class="col-sm-1 paddingLeft0">
                                                <asp:LinkButton ID="btnSearch_Item" runat="server" TabIndex="16" OnClick="btnSearch_Item_Click">
                                                        <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                </asp:LinkButton>
                                            </div>

                                        </div>

                                    </div>

                                    <div class="col-sm-3">

                                        <div class="row">

                                            <div class="col-sm-3 labelText1">
                                                Status
                                            </div>
                                            <div class="col-sm-9">
                                                <asp:DropDownList ID="ddlStatus" runat="server" TabIndex="17" Enabled="false"
                                                    AutoPostBack="true" CssClass="form-control"></asp:DropDownList>
                                            </div>

                                        </div>

                                    </div>

                                    <div class="col-sm-3" runat="server" visible="false">

                                        <div class="row"  >

                                            <div class="col-sm-3 labelText1">
                                                Unit Cost
                                            </div>
                                            <div class="col-sm-9">
                                                <asp:TextBox ID="txtUnitCost" runat="server" TabIndex="18" CssClass="form-control" AutoPostBack="true" OnTextChanged="txtUnitCost_TextChanged" onkeydown="return jsDecimals(event);" Style="text-align: right"></asp:TextBox>
                                            </div>

                                        </div>
                                    </div>

                                    <div class="col-sm-2">

                                        <div class="row">

                                            <div class="col-sm-3 labelText1">
                                                Qty
                                            </div>
                                            <div class="col-sm-9">
                                                <asp:TextBox ID="txtQty" runat="server" TabIndex="19" CssClass="txtQty form-control" onkeydown="return jsDecimals(event);" OnTextChanged="txtQty_TextChanged"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-1">

                                        <div class="row">
                                            <asp:LinkButton ID="btnAddItem" CausesValidation="false" TabIndex="20" CssClass="floatRight" runat="server" OnClick="btnAddItem_Click">
                                                        <span class="glyphicon glyphicon-plus" aria-hidden="true"></span>
                                            </asp:LinkButton>
                                        </div>
                                    </div>

                                    <div class="row">
                                        <div class="panel-body">
                                            <div class="col-sm-12">
                                                <div class="panel panel-default">

                                                    <div class="panel-heading panelHeadingInfoBar">

                                                        <div class="col-sm-2">

                                                            <div class="row">

                                                                <div class="col-sm-4 labelText1">
                                                                    Description-
                                                                </div>

                                                                <div class="col-sm-8 prnlbldescription">
                                                                    <asp:Label ID="lblItemDescription" runat="server" ForeColor="#A513D0"></asp:Label>
                                                                </div>

                                                            </div>

                                                        </div>

                                                        <div class="col-sm-2">

                                                            <div class="row">

                                                                <div class="col-sm-4 labelText1">
                                                                </div>
                                                                <div class="col-sm-8" style="margin-top: 3px">
                                                                </div>

                                                            </div>

                                                        </div>

                                                        <div class="col-sm-2">

                                                            <div class="row">

                                                                <div class="col-sm-4 labelText1">
                                                                    Model-
                                                                </div>
                                                                <div class="col-sm-8" style="margin-top: 3px">
                                                                    <asp:Label ID="lblItemModel" runat="server" ForeColor="#A513D0"></asp:Label>
                                                                </div>

                                                            </div>

                                                        </div>

                                                        <div class="col-sm-2">

                                                            <div class="row">

                                                                <div class="col-sm-4 labelText1">
                                                                    Brand-
                                                                </div>
                                                                <div class="col-sm-8" style="margin-top: 3px">
                                                                    <asp:Label ID="lblItemBrand" runat="server" ForeColor="#A513D0"></asp:Label>
                                                                </div>

                                                            </div>

                                                        </div>

                                                        <div class="col-sm-4">

                                                            <div class="row">

                                                                <div class="col-sm-4 labelText1">
                                                                    Serial Status-
                                                                </div>
                                                                <div class="col-sm-8" style="margin-top: 3px">
                                                                    <asp:Label ID="lblItemSerialStatus" runat="server" ForeColor="#A513D0"></asp:Label>
                                                                </div>

                                                            </div>

                                                        </div>

                                                    </div>

                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    </asp:Panel>
                                    <div class="">
                                        <div class="row">

                                            <div class="panel-body paddingbottom0 paddingtop0">

                                                <div class="col-sm-12">

                                                    <div class="panel panel-default " id="dvContentsOrder1sds">

                                                       

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
                                                                                        <div class="" style="height:123px; overflow-y:auto; overflow-x:hidden;">

                                                                                            <asp:GridView ID="grdItems" AutoGenerateColumns="false" runat="server" CssClass="table table-hover table-striped" GridLines="None" ShowHeaderWhenEmpty="True" EmptyDataText="No data found...">

                                                                                                <Columns>
                                                                                                    <asp:TemplateField HeaderText="">
                                                                                                        <ItemTemplate>
                                                                                                            <asp:LinkButton ID="lbtnadditem" runat="server" CausesValidation="false" OnClick="lbtnadditem_Click">
                                                                                                                <span class="glyphicon glyphicon-upload" aria-hidden="true"></span>
                                                                                                            </asp:LinkButton>
                                                                                                        </ItemTemplate>
                                                                                                    </asp:TemplateField>
                                                                                                    <asp:TemplateField HeaderText="">
                                                                                                        <ItemTemplate>
                                                                                                            <asp:LinkButton ID="lbtnAddSerial" runat="server" CausesValidation="false" OnClick="lbtnAddSerial_Click">
                                                                                                    <span class="glyphicon glyphicon-plus" aria-hidden="true"></span>
                                                                                                            </asp:LinkButton>
                                                                                                        </ItemTemplate>
                                                                                                    </asp:TemplateField>
                                                                                                    

                                                                                                    <asp:TemplateField HeaderText="">
                                                                                                        <ItemTemplate>
                                                                                                            <asp:LinkButton ID="lbtndeleteitm" runat="server" CausesValidation="false" OnClientClick="ConfirmDelete();" OnClick="lbtndeleteitm_Click">
                                                                                                    <span class="glyphicon glyphicon-trash" aria-hidden="true"></span>
                                                                                                            </asp:LinkButton>
                                                                                                        </ItemTemplate>
                                                                                                    </asp:TemplateField>

                                                                                                    <asp:TemplateField HeaderText="#" Visible="false">
                                                                                                        <ItemTemplate>
                                                                                                            <asp:Label ID="lblitri_line_no" runat="server" Text='<%# Bind("itri_line_no") %>' Width="50px"></asp:Label>
                                                                                                        </ItemTemplate>
                                                                                                    </asp:TemplateField>

                                                                                                    <asp:TemplateField HeaderText="Item">
                                                                                                        <ItemTemplate>
                                                                                                            <asp:Label ID="lblitri_itm_cd" runat="server" Text='<%# Bind("itri_itm_cd") %>' Width="100px"></asp:Label>
                                                                                                        </ItemTemplate>
                                                                                                    </asp:TemplateField>

                                                                                                    <asp:TemplateField HeaderText="Description">
                                                                                                        <ItemTemplate>
                                                                                                            <asp:Label ID="lbldescgrd" runat="server" Text='<%# Bind("mi_longdesc") %>' Width="200px"></asp:Label>
                                                                                                        </ItemTemplate>
                                                                                                    </asp:TemplateField>

                                                                                                    <asp:TemplateField HeaderText="Model">
                                                                                                        <ItemTemplate>
                                                                                                            <asp:Label ID="lblmodelgrd" runat="server" Text='<%# Bind("mi_model") %>' Width="100px"></asp:Label>
                                                                                                        </ItemTemplate>
                                                                                                    </asp:TemplateField>

                                                                                                    <asp:TemplateField HeaderText="Status Code" Visible="false">
                                                                                                        <ItemTemplate>
                                                                                                            <asp:Label ID="lblstatus" runat="server" Text='<%# Bind("itri_itm_stus") %>' Width="100px"></asp:Label>
                                                                                                        </ItemTemplate>
                                                                                                    </asp:TemplateField>
                                                                                                    
                                                                                                    <asp:TemplateField HeaderText="Status">
                                                                                                        <ItemTemplate>
                                                                                                            <asp:Label ID="lblstatusdesc" runat="server" Text='<%# Bind("Mis_desc") %>' Width="100px"></asp:Label>
                                                                                                        </ItemTemplate>
                                                                                                    </asp:TemplateField>

                                                                                                    <asp:TemplateField HeaderText="Unit Cost" Visible="false">
                                                                                                        <ItemTemplate>
                                                                                                            <asp:Label ID="lblcost" runat="server" Text='<%# Bind("itri_unit_price") %>' Width="55px"></asp:Label>
                                                                                                        </ItemTemplate>
                                                                                                        <ItemStyle Width="8%" HorizontalAlign="Center" />
                                                                                                        <HeaderStyle HorizontalAlign="Right" />
                                                                                                    </asp:TemplateField>

                                                                                                    <asp:TemplateField HeaderText="Return Qty">
                                                                                                        <ItemTemplate>
                                                                                                            <asp:Label ID="lblapqtygrd" runat="server" Text='<%# Bind("itri_app_qty","{0:n}") %>' Width="25px"></asp:Label>
                                                                                                        </ItemTemplate>
                                                                                                        <ItemStyle Width="8%" HorizontalAlign="Center" />
                                                                                                        <HeaderStyle HorizontalAlign="Right" />
                                                                                                    </asp:TemplateField>

                                                                                                    <asp:TemplateField HeaderText="Pick Qty">
                                                                                                        <ItemTemplate>
                                                                                                            <asp:Label ID="lblitri_qty" runat="server" Text='<%# Bind("Itri_bqty","{0:n}") %>' Width="15px"></asp:Label>
                                                                                                        </ItemTemplate>
                                                                                                        <ItemStyle Width="5%" HorizontalAlign="Center" />
                                                                                                        <HeaderStyle HorizontalAlign="Right" />
                                                                                                    </asp:TemplateField>

                                                                                                    <asp:TemplateField HeaderText="Request" Visible="false">
                                                                                                        <ItemTemplate>
                                                                                                            <asp:Label ID="lblnote" runat="server" Text='<%# Bind("itri_note") %>'></asp:Label>
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

                                                                                        <div class="" style="height:115px; overflow-y:auto; overflow-x:hidden;">

                                                                                            <asp:GridView ID="grdSerial" AutoGenerateColumns="false" runat="server" CssClass="table table-hover table-striped" GridLines="None" ShowHeaderWhenEmpty="True" EmptyDataText="No data found...">

                                                                                                <Columns>

                                                                                                    <asp:TemplateField HeaderText="">
                                                                                                        <ItemTemplate>
                                                                                                            <asp:LinkButton ID="lbtndelser" runat="server" CausesValidation="false" OnClientClick="ConfirmDelete();" OnClick="lbtndelser_Click">
                                                                                                    <span class="glyphicon glyphicon-trash" aria-hidden="true"></span>
                                                                                                            </asp:LinkButton>
                                                                                                        </ItemTemplate>
                                                                                                    </asp:TemplateField>

                                                                                                    <asp:TemplateField HeaderText="Item">
                                                                                                        <ItemTemplate>
                                                                                                            <asp:Label ID="lblitri_itm_cd" runat="server" Text='<%# Bind("tus_itm_cd") %>' Width="100px"></asp:Label>
                                                                                                        </ItemTemplate>
                                                                                                    </asp:TemplateField>

                                                                                                    <asp:TemplateField HeaderText="Model">
                                                                                                        <ItemTemplate>
                                                                                                            <asp:Label ID="lblmodelser" runat="server" Text='<%# Bind("tus_itm_model") %>' Width="100px"></asp:Label>
                                                                                                        </ItemTemplate>
                                                                                                    </asp:TemplateField>

                                                                                                    <asp:TemplateField HeaderText="Status Code" Visible="false">
                                                                                                        <ItemTemplate>
                                                                                                            <asp:Label ID="lblstatus" runat="server" Text='<%# Bind("tus_itm_stus") %>' Width="100px"></asp:Label>
                                                                                                        </ItemTemplate>
                                                                                                    </asp:TemplateField>

                                                                                                    <asp:TemplateField HeaderText="Status">
                                                                                                        <ItemTemplate>
                                                                                                            <asp:Label ID="lblserstus" runat="server" Text='<%# Bind("Tus_itm_stus_Desc") %>' Width="120px"></asp:Label>
                                                                                                        </ItemTemplate>
                                                                                                    </asp:TemplateField>

                                                                                                    <asp:TemplateField HeaderText="Qty">
                                                                                                        <ItemTemplate>
                                                                                                            <asp:Label ID="lblqtygrd2" runat="server" Text='<%# Bind("tus_qty","{0:n}") %>'></asp:Label>
                                                                                                        </ItemTemplate>
                                                                                                        <ItemStyle Width="4%" CssClass="gridHeaderAlignRight" />
                                                                                                        <HeaderStyle CssClass="gridHeaderAlignRight"/>
                                                                                                    </asp:TemplateField>

                                                                                                      <asp:TemplateField HeaderText="" Visible="true">
                                                                                                        <ItemTemplate>
                                                                                                            <asp:Label  runat="server" Text='' Width="50px"></asp:Label>
                                                                                                        </ItemTemplate>
                                                                                                    </asp:TemplateField>
                                                                                                    <asp:TemplateField HeaderText="Serial 1">
                                                                                                        <ItemTemplate>
                                                                                                            <asp:Label ID="lblser1" runat="server" Text='<%# Bind("tus_ser_1") %>' Width="150px"></asp:Label>
                                                                                                        </ItemTemplate>
                                                                                                    </asp:TemplateField>

                                                                                                    <asp:TemplateField HeaderText="Serial 2">
                                                                                                        <ItemTemplate>
                                                                                                            <asp:Label ID="lblser2grd2" runat="server" Text='<%# Bind("tus_ser_2") %>' Width="150px"></asp:Label>
                                                                                                        </ItemTemplate>
                                                                                                    </asp:TemplateField>

                                                                                                    <asp:TemplateField HeaderText="Serial 3" Visible="false">
                                                                                                        <ItemTemplate>
                                                                                                            <asp:Label ID="lblser2grd3" runat="server" Text='<%# Bind("tus_ser_3") %>'></asp:Label>
                                                                                                        </ItemTemplate>
                                                                                                    </asp:TemplateField>

                                                                                                    <asp:TemplateField HeaderText="Bin" Visible="false">
                                                                                                        <ItemTemplate>
                                                                                                            <asp:Label ID="lblbin" runat="server" Text='<%# Bind("tus_bin") %>'></asp:Label>
                                                                                                        </ItemTemplate>
                                                                                                    </asp:TemplateField>

                                                                                                    <asp:TemplateField HeaderText="Serial ID" Visible="false">
                                                                                                        <ItemTemplate>
                                                                                                            <asp:Label ID="lblserialid" runat="server" Text='<%# Bind("tus_ser_id") %>' Width="100px"></asp:Label>
                                                                                                        </ItemTemplate>
                                                                                                    </asp:TemplateField>

                                                                                                    <asp:TemplateField HeaderText="Request" Visible="false">
                                                                                                        <ItemTemplate>
                                                                                                            <asp:Label ID="lblrequest2" runat="server" Text='<%# Bind("tus_base_doc_no") %>' Width="100px"></asp:Label>
                                                                                                        </ItemTemplate>
                                                                                                    </asp:TemplateField>

                                                                                                    <asp:TemplateField HeaderText="BaseLineNo" Visible="false">
                                                                                                        <ItemTemplate>
                                                                                                            <asp:Label ID="lblbaseline" runat="server" Text='<%# Bind("tus_base_itm_line") %>'></asp:Label>
                                                                                                        </ItemTemplate>
                                                                                                    </asp:TemplateField>

                                                                                                    <asp:TemplateField HeaderText="Supplier">
                                                                                                        <ItemTemplate>
                                                                                                            <asp:Label ID="lblsup" runat="server" Text='<%# Bind("Tus_orig_supp") %>' Width="200px"></asp:Label>
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
                        <asp:Panel runat="server" DefaultButton="lbtnSearch">
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
                                <asp:UpdatePanel runat="server">
                                    <ContentTemplate>
                                <asp:TextBox ID="txtSearchbyword" CausesValidation="false" placeholder="Search by word" 
                                    class="form-control" runat="server" ></asp:TextBox>
                           </ContentTemplate>
                                </asp:UpdatePanel> </div>
                            <asp:ScriptManagerProxy ID="ScriptManagerProxy9" runat="server"></asp:ScriptManagerProxy>
                            <asp:UpdatePanel ID="UpdatePanel9" runat="server">
                                <ContentTemplate>
                                    <div class="col-sm-1 paddingLeft0">
                                        <asp:LinkButton ID="lbtnSearch" runat="server" OnClick="lbtnSearch_Click">
                                            <span class="glyphicon glyphicon-search" aria-hidden="true"  ></span>
                                        </asp:LinkButton>
                                    </div>
                                </ContentTemplate>
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
                                                <asp:GridView ID="grdResultD" CausesValidation="false" runat="server" EmptyDataText="No data found..." 
                                                    ShowHeaderWhenEmpty="True" GridLines="None" CssClass="table table-hover table-striped"
                                                     PageSize="8" PagerStyle-CssClass="cssPager" AllowPaging="True" OnSelectedIndexChanged="grdResultD_SelectedIndexChanged" OnPageIndexChanging="grdResultD_PageIndexChanging">
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
            <asp:Button ID="btn2" runat="server" Text="Button" Style="display: none;" />
            <asp:ModalPopupExtender ID="MpDelivery" runat="server" Enabled="True" TargetControlID="btn2"
                PopupControlID="pnldel" PopupDragHandleControlID="PopupHeaderKit" Drag="true" BackgroundCssClass="modalBackground">
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
                                    <uc1:ucOutScan runat="server" ID="ucOutScan" />
                                </div>

                            </div>


                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>

        </div>
    </asp:Panel>
    <script src="<%=Request.ApplicationPath%>Js/jquery.min.js"></script>
    <script src="<%=Request.ApplicationPath%>Js/bootstrap.min.js"></script>

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
     <div class="row">
        <div class="col-sm-12">
            <asp:UpdatePanel runat="server" ID="UpdatePanel3">
                <ContentTemplate>
                    <asp:Button ID="Button12" runat="server" Text="Button" Style="display: none;" />
                    <asp:ModalPopupExtender ID="popupSaveData" runat="server" Enabled="True" TargetControlID="Button12"
                        PopupControlID="pnl_1" CancelControlID="LinkButton3" PopupDragHandleControlID="PopupHeader" Drag="true" BackgroundCssClass="modalBackground">
                    </asp:ModalPopupExtender>

                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
    <asp:Panel runat="server" ID="pnl_1" DefaultButton="lbtnSearch">
        <div class="row">
            <div class="col-sm-12">
                <div runat="server" id="Div5" class="panel panel-info width700 height200">
                    <asp:Label ID="Label2" runat="server" Text="Label" Visible="false"></asp:Label>
                    <div class="panel panel-default">
                        <div class="panel-heading" style="height:28px;">
                            <div class="row">
                                <div class="col-sm-12">
                                    <div class="col-sm-11" style="color: red;">
                                        <strong><b>The Serials Are Already Scanned</b></strong>
                                    </div>

                                    <div class="col-sm-1">
                                        <div style="margin-top:-3px;">
                                            <asp:LinkButton ID="LinkButton3" runat="server">
                                    <span class="glyphicon glyphicon-remove" aria-hidden="true">Close</span>
                                        </asp:LinkButton>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="panel-body">
                            <div class="row">
                                <div class="col-sm-12">
                                    <div class="row">
                                        <div class="col-sm-12">
                                            <asp:ScriptManagerProxy ID="ScriptManagerProxy3" runat="server"></asp:ScriptManagerProxy>
                                            <asp:UpdatePanel ID="UpdatePanel8" runat="server">
                                                <ContentTemplate>
                                                    <div style="max-height: 400px; overflow-y: auto;">
                                                        <asp:GridView ID="dgvSerSave" CausesValidation="false" runat="server" GridLines="None" AutoGenerateColumns="false"
                                                            ShowHeaderWhenEmpty="true" EmptyDataText="No data found..."
                                                            CssClass="table table-hover table-striped" PagerStyle-CssClass="cssPager">
                                                            <Columns>
                                                                 <asp:TemplateField HeaderText="Serial ID" Visible="false">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblSer" Text='<%# Bind("Tus_ser_id") %>' runat="server" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Company">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblComp" Text='<%# Bind("Tus_com") %>' runat="server" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Location">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblLocCd" Text='<%# Bind("Tus_loc") %>' runat="server" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Bin">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblBin" Text='<%# Bind("Tus_bin") %>' runat="server" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Item">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblItm" Text='<%# Bind("Tus_itm_cd") %>' runat="server" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Status">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblSatus" Text='<%# Bind("Tus_itm_stus") %>' runat="server" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Serial 1">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblSer1" Text='<%# Bind("Tus_ser_1") %>' runat="server" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Serial 2">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblSer2" Text='<%# Bind("Tus_ser_2") %>' runat="server" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Sequence ">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblSeqNo" Text='<%# Bind("Tus_usrseq_no") %>' runat="server" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="User Id">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblUserId" Text='<%# Bind("Tus_cre_by") %>' runat="server" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                               
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
                    </div>
                </div>
                <div>
                    <%-- <div class="row">
                                <div class="col-sm-12">
                                    <%--<ul id="myDocTab" class="nav nav-tabs">
                                        <li class="active"><a data-toggle="tab" href="#documentTab">Item</a></li>
                                        <li><a data-toggle="tab" href="#serialTab">Serial </a></li>
                                    </ul>
                                   <%-- <div class="tab-content">
                                        <div class="tab-pane fade in active" id="documentTab">--%>
                    <%--  <div class="row">
                                                <div class="col-sm-8">

                                                    <div style="max-height: 400px; overflow-y: auto;">
                                                        <asp:ScriptManagerProxy ID="ScriptManagerProxy8" runat="server"></asp:ScriptManagerProxy>
                                                        <asp:UpdatePanel ID="UpdatePanel18" runat="server">
                                                            <ContentTemplate>
                                                                <asp:GridView ID="dgvItem" CausesValidation="false" runat="server" GridLines="None" AutoGenerateColumns="false"
                                                                    ShowHeaderWhenEmpty="true" EmptyDataText="No data found..."
                                                                    CssClass="table table-hover table-striped" PagerStyle-CssClass="cssPager">
                                                                    <Columns>
                                                                        <%--<asp:TemplateField HeaderText="Company">
                                                                       <ItemTemplate>
                                                                           <asp:Label ID="lblComp" Text='<%# Bind("Tui_pic_itm_cd") %>' runat="server" />
                                                                       </ItemTemplate>
                                                                   </asp:TemplateField>
                                                                   <asp:TemplateField HeaderText="Location">
                                                                       <ItemTemplate>
                                                                           <asp:Label ID="lblLocCd" Text='<%# Bind("Tus_loc") %>' runat="server" />
                                                                       </ItemTemplate>
                                                                   </asp:TemplateField>--%>
                    <%-- <asp:TemplateField HeaderText="Item Code">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblItm" Text='<%# Bind("Tui_req_itm_cd") %>' runat="server" />
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Batch #">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblBin" Text='<%# Bind("TUI_BATCH") %>' runat="server" />
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <%-- <asp:TemplateField HeaderText="Serial">
                                                                       <ItemTemplate>
                                                                           <asp:Label ID="lblSer" Text='<%# Bind("Tus_ser_id") %>' runat="server" />
                                                                       </ItemTemplate>
                                                                   </asp:TemplateField>--%>
                    <%--  </Columns>
                                                                </asp:GridView>
                                                            </ContentTemplate>
                                                        </asp:UpdatePanel>
                                                    </div>

                                                </div>
                                            </div>--%>
                    <%-- </div>
                                        <div class="tab-pane " id="serialTab">--%>

                    <%--</div>
                                    </div>--%>
                </div>
            </div>
        </div>
    </asp:Panel>
    <asp:UpdatePanel ID="UpdatePanel14" runat="server">
        <ContentTemplate>
            <asp:Button ID="Button2" runat="server" Text="Button" Style="display: none;" />
            <asp:ModalPopupExtender ID="UserAdPopup" runat="server" Enabled="True" TargetControlID="Button2"
                PopupControlID="pnlASearchpopup" PopupDragHandleControlID="PopupHeader" Drag="true" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>

        </ContentTemplate>

    </asp:UpdatePanel>
    
    <asp:UpdatePanel ID="UpdatePanel6" runat="server">
        <ContentTemplate>

            <asp:Panel runat="server" ID="pnlASearchpopup">
                <div runat="server" id="Div6" class="panel panel-default height400 width700">
                    <asp:Label ID="lblAvalue" runat="server" Text="Label" Visible="false"></asp:Label>
                    <div class="panel panel-default">
                        <div class="panel-heading" style="height:24px;">
                            
                            <%--<span>Commen Search</span>--%>
                            <div class="col-sm-11">
                               <b> Serial Advanced Search</b>
                            </div>
                            <div class="col-sm-1">
                                <asp:LinkButton ID="btnAClose" runat="server" OnClick="btnAClose_Click">
                             <span class="glyphicon glyphicon-remove" aria-hidden="true">Close</span>
                            </asp:LinkButton>
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
                                    <div class="col-sm-12" id="Div8" runat="server">
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
                                        <asp:Panel runat="server" DefaultButton="lbtnSearchA">
                                            <div class="col-sm-2 labelText1">
                                                Search by word
                                            </div>
                                            <div class="col-sm-4 paddingRight5">
                                                <asp:TextBox ID="txtSearchbywordA" CausesValidation="false" placeholder="Search by word" class="form-control"
                                                    AutoPostBack="false" runat="server"></asp:TextBox>
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
                                            </asp:UpdatePanel>
                                        </asp:Panel>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-12 height5">
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-8">
                                    </div>
                                    <div class="col-sm-2">
                                        <asp:Button ID="btnAllItem" runat="server" Visible="true" CssClass="btn btn-primary btn-xs" Text="Add All Items"
                                             OnClick="btnAllItem_Click" />
                                        <%-- <asp:LinkButton ID="LinkButton3" runat="server" OnClick="btnAdvanceAddItem_Click">
                                            <span class="glyphicon glyphicon-plus" aria-hidden="true"></span>Add Item
                                                    </asp:LinkButton>--%>
                                    </div>
                                    <div class="col-sm-2">
                                        <asp:UpdatePanel runat="server" ID="pnlAddSerial">
                                            <ContentTemplate>
                                                <asp:Button ID="btnAdvanceAddItem" runat="server" Visible="true" CssClass="btn btn-primary btn-xs" Text="Add Serial" 
                                            OnClick="btnAdvanceAddItem_Click" />
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                        
                                        <%-- <asp:LinkButton ID="LinkButton3" runat="server" OnClick="btnAdvanceAddItem_Click">
                                            <span class="glyphicon glyphicon-plus" aria-hidden="true"></span>Add Item
                                                    </asp:LinkButton>--%>
                                    </div>

                                </div>
                                <div class="row">
                                    <div class="col-sm-12 height5">
                                        
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-12" style="overflow-y:scroll;height:300px;">
                                        <asp:ScriptManagerProxy ID="ScriptManagerProxy13" runat="server"></asp:ScriptManagerProxy>
                                        <asp:UpdatePanel ID="UpdatePanel17" runat="server">
                                            <ContentTemplate>
                                                <asp:GridView ID="grdAdSearch" AutoGenerateColumns="false" CausesValidation="false" EmptyDataText="No data found..." 
                                                    ShowHeaderWhenEmpty="True" runat="server" GridLines="None" CssClass="table table-hover table-striped" PageSize="10000" 
                                                    PagerStyle-CssClass="cssPager" AllowPaging="True" OnSelectedIndexChanged="grdAdSearch_SelectedIndexChanged" 
                                                    OnPageIndexChanging="grdAdSearch_PageIndexChanging">
                                                    <Columns>
                                                        <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                                            <HeaderTemplate>
                                                                        <%--<asp:CheckBox ID="CheckBox1" runat="server" Width="5px" onclick="CheckAll(this)"></asp:CheckBox>--%>
                                                                        <asp:CheckBox ID="allchk"  AutoPostBack="true" runat="server" Width="5px" OnCheckedChanged="allchk_CheckedChanged"></asp:CheckBox>
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                        <asp:CheckBox Checked='<%#Convert.ToBoolean(Eval("Is_select")) %>' ID="selectchk" AutoPostBack="true" OnCheckedChanged="selectchk_CheckedChanged" runat="server" Width="5px"></asp:CheckBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                         <asp:TemplateField HeaderText="">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblTemp" runat="server" Text='' Width="10px"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Item Code">
                                                            <ItemTemplate>
                                                                <asp:Label ID="Col_ItemCode" runat="server" Text='<%# Bind("Item") %>' Width="100px"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Serial 1">
                                                            <ItemTemplate>
                                                                <asp:Label ID="Col_Serial1" runat="server" Text='<%# Bind("Serial_1") %>' Width="100px"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Serial 2">
                                                            <ItemTemplate>
                                                                <asp:Label ID="Col_Serial2" runat="server" Text='<%# Bind("Serial_2") %>' Width="100px"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Qty" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="Col_inb_qty" runat="server" Text='<%# Bind("Quentity") %>' Width="30px"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Supplier">
                                                            <ItemTemplate>
                                                                <asp:Label ID="Col_Supplier" runat="server" Text='<%# Bind("Supplier") %>' Width="100px"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Cost" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="Col_ins_unit_cost" runat="server" Text='<%# Bind("UnitCost") %>' Width="100px"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="status" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="Col_ins_itm_stus" runat="server" Text='<%# Bind("ItemStus") %>' Width="100px"></asp:Label>
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
    
    <asp:UpdateProgress ID="UpdateProgress3" DisplayAfter="10"
        runat="server" AssociatedUpdatePanelID="UpdatePanel17">
        <ProgressTemplate>
            <div class="divWaiting">
                <asp:Label ID="lblWait661" runat="server"
                    Text="Please wait... " />
                <asp:Image ID="imgWait661" runat="server"
                    ImageAlign="Middle" ImageUrl="~/images/icons/ajax-loader.gif" />
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>

   <asp:UpdateProgress ID="updateProgress456" DisplayAfter="10"
        runat="server" AssociatedUpdatePanelID="pnlAddSerial">
        <ProgressTemplate>
            <div class="divWaiting">
                <asp:Label ID="lblWait661456" runat="server"
                    Text="Please wait... " />
                <asp:Image ID="imgWait661456" runat="server"
                    ImageAlign="Middle" ImageUrl="~/images/icons/ajax-loader.gif" />
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>

    <script>
        Sys.Application.add_load(func);
        function func() {
            $('.txtQty').mousedown(function (e) {
                if (e.button == 2) {
                    alert('This functionality is disabled !');
                    return false;
                } else {
                    return true;
                }
            });
        }
    </script>
</asp:Content>


