<%@ Page Title="" Language="C#" MasterPageFile="~/View/AdminSite.Master" AutoEventWireup="true" CodeBehind="SalesReversal.aspx.cs" Inherits="FastForward.SCMWeb.View.Transaction.Sales.Sales_Reversal" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="<%=Request.ApplicationPath%>Css/jquery-ui.theme.min.css" rel="stylesheet" />
    <link href="<%=Request.ApplicationPath%>Css/jquery-ui.min.css" rel="stylesheet" />
    <script type="text/javascript">
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
                position: 'top-center',
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
        function ConfClear() {
            var result = confirm("Do you want to clear data");
            if (result) {
                return true;
            }
            else {
                return false;
            }
        }

        function CheckAllItems(oCheckbox) {
            var GridView2 = document.getElementById("<%=grdInvItem.ClientID %>");
            for (i = 1; i < GridView2.rows.length; i++) {
                GridView2.rows[i].cells[0].getElementsByTagName("INPUT")[0].checked = oCheckbox.checked;
            }
        }
        function CheckAllDItems(oCheckbox) {
            var GridView2 = document.getElementById("<%=grdDelDetails.ClientID %>");
            for (i = 1; i < GridView2.rows.length; i++) {
                GridView2.rows[i].cells[0].getElementsByTagName("INPUT")[0].checked = oCheckbox.checked;
            }
        }
        function isNumberKey(evt) {
            var charCode = (evt.which) ? evt.which : evt.keyCode;
            if (charCode != 46 && charCode > 31
              && (charCode < 48 || charCode > 57))
                return false;

            return true;
        }
        function Enable() {
            return;
        }
    </script>
    <script type="text/javascript">
        function ConfirmSendToPDA() {
            var selectedvaldelitm = confirm("Are you sure you want to send ?");
            if (selectedvaldelitm) {
                document.getElementById('<%=txtpdasend.ClientID %>').value = "Yes";
            } else {
                document.getElementById('<%=txtpdasend.ClientID %>').value = "No";
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
        function ApConfirm() {
            var selectedvalue = confirm("You are going to reverse other profit center sales and need to get approval. Do you want to process request ?");
            if (selectedvalue) {
                document.getElementById('<%=txtAppconformmessageValue.ClientID %>').value = "Yes";
            } else {
                document.getElementById('<%=txtAppconformmessageValue.ClientID %>').value = "No";
            }
        };
        function RejectConfirm() {
            var selectedvalue = confirm("Do you want to reject the request ?");
            if (selectedvalue) {
                document.getElementById('<%=txtRejectconformmessageValue.ClientID %>').value = "Yes";
            } else {
                document.getElementById('<%=txtRejectconformmessageValue.ClientID %>').value = "No";
            }
        };
        function ApprovalConfirm() {
            var selectedvalue = confirm("Do you want to approve the request ?");
            if (selectedvalue) {
                document.getElementById('<%=txtApprovalconformmessageValue.ClientID %>').value = "Yes";
            } else {
                document.getElementById('<%=txtApprovalconformmessageValue.ClientID %>').value = "No";
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
        function DeleteJobConfirm() {
            var selectedvalue = confirm("Do you want to remove selected job details  ?");
            if (selectedvalue) {
                document.getElementById('<%=txtDelJobconformmessageValue.ClientID  %>').value = "Yes";
            } else {
                document.getElementById('<%=txtDelJobconformmessageValue.ClientID %>').value = "No";
            }
        };
        function DeleteRegConfirm() {
            var selectedvalue = confirm("Do you want to remove selected registration details ?");
            if (selectedvalue) {
                document.getElementById('<%=txtDelRegconformmessageValue.ClientID  %>').value = "Yes";
            } else {
                document.getElementById('<%=txtDelRegconformmessageValue.ClientID %>').value = "No";
            }
        };

        function CancelConfirm() {
            var selectedvalue = confirm("Do you want to cancel the reversal?");
            if (selectedvalue) {
                document.getElementById('<%=txtCancelconformmessageValue.ClientID %>').value = "Yes";
            } else {
                document.getElementById('<%=txtCancelconformmessageValue.ClientID %>').value = "No";
            }
        };
        function RequestConfirm() {
            var selectedvalue = confirm("Do you want to request a reversal?");
            if (selectedvalue) {
                document.getElementById('<%=txtRequestconformmessageValue.ClientID %>').value = "Yes";
            } else {
                document.getElementById('<%=txtRequestconformmessageValue.ClientID %>').value = "No";
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
        function ConfirmLoc() {
            var selectedvalueOrdPlace = confirm("Request location and your profit center is not match.Do you want to continue ?");
            if (selectedvalueOrdPlace) {
                document.getElementById('<%=diffLocSave.ClientID %>').value = "Yes";
            } else {
                document.getElementById('<%=diffLocSave.ClientID %>').value = "No";
            }
        };
    </script>
    <script type="text/javascript">

        <%--        function pageLoad(sender, args) {
            $("#<%=txtSRNDate.ClientID %>").datepicker({ dateFormat: "d/M/yy", timeFormat: "hh:mm tt" });

        };--%>

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

        .dropdownpalan {
            left: -126px !important;
            top: 25px !important;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" runat="server">
    <asp:UpdateProgress ID="UpdateProgress2" DisplayAfter="10"
        runat="server" AssociatedUpdatePanelID="UpdatePanel3">

        <ProgressTemplate>
            <div class="divWaiting">
                <asp:Label ID="lblWait1" runat="server"
                    Text="Please wait... " />
                <asp:Image ID="imgWait1" runat="server"
                    ImageAlign="Middle" ImageUrl="~/images/icons/ajax-loader.gif" />
            </div>
        </ProgressTemplate>

    </asp:UpdateProgress>
    <asp:UpdateProgress ID="UpdateProgress3" DisplayAfter="10"
        runat="server" AssociatedUpdatePanelID="UpdatePanel1">

        <ProgressTemplate>
            <div class="divWaiting">
                <asp:Label ID="lblWait2" runat="server"
                    Text="Please wait... " />
                <asp:Image ID="imgWait2" runat="server"
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

                            <div class="col-sm-1">
                                <strong>Send to PDA</strong>
                            </div>

                            <div class="col-sm-1">
                                <asp:CheckBox runat="server" ID="chkpda" Enabled="false" AutoPostBack="true" OnCheckedChanged="chkpda_CheckedChanged" />
                            </div>

                            <div class="col-sm-2">
                                <div class="row paddingLeft0">
                                    <asp:UpdatePanel ID="upInvoiceLoad" runat="server">
                                        <ContentTemplate>
                                            <div class="col-sm-4 labelText1 paddingLeft0">
                                                Reversal No
                                            </div>
                                            <div class="col-sm-7">
                                                <asp:TextBox ID="txtInvoiceNo" runat="server" Style="text-transform: uppercase" TabIndex="5"
                                                    CssClass="form-control" ReadOnly="false" AutoPostBack="true"
                                                    OnTextChanged="txtInvoiceNo_TextChanged"></asp:TextBox>
                                            </div>
                                            <div class="col-sm-1 paddingLeft0">
                                                <asp:LinkButton ID="lbtnsupplier" runat="server" CausesValidation="false" OnClick="lbtnsupplier_Click">
                                                        <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                </asp:LinkButton>
                                            </div>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                            </div>







                            <div class="col-sm-2  buttonrow">

                                <div id="WarningGRN" visible="false" runat="server" class="alert alert-danger alert-dismissible" role="alert">
                                    <div class="col-sm-11">
                                        <asp:Label ID="lblH1" runat="server" Text="Label"></asp:Label>

                                    </div>
                                </div>
                            </div>
                            <div class="col-sm-8  buttonRow">
                                <%--<div class="col-sm-4">
                                </div>--%>
                                <div class="col-sm-2 paddingRight0 paddingLeft0">
                                    <asp:LinkButton ID="lbtnTempSave" CausesValidation="false" Visible="false" runat="server" OnClientClick="TempSaveConfirm()" OnClick="lbtnTempSave_Click">
                                        <span class="glyphicon glyphicon-saved" aria-hidden="true"></span>Temporary Save
                                    </asp:LinkButton>
                                </div>

                                <div class="col-sm-2 ">
                                    <asp:LinkButton ID="lbtnRequest" CausesValidation="false" runat="server" OnClientClick="RequestConfirm()" OnClick="lbtnRequest_Click">
                                        <span class="glyphicon glyphicon-share" aria-hidden="true"></span>Request
                                    </asp:LinkButton>
                                </div>

                                <div class="col-sm-1 paddingRight0 paddingLeft0">
                                    <asp:LinkButton ID="lbtnReverse" CausesValidation="false" runat="server" OnClientClick="TempSaveConfirm()" OnClick="lbtnReverse_Click">
                                        <span class="glyphicon glyphicon-saved" aria-hidden="true"></span>Reverse
                                    </asp:LinkButton>
                                </div>
                                <div class="col-sm-1 paddingRight0 paddingLeft0">
                                    <asp:LinkButton ID="lbtnApprovalSave" CausesValidation="false" CssClass="floatRight" runat="server" OnClientClick="ApprovalConfirm()" OnClick="lbtnApprovalSave_Click">
                                        <span class="glyphicon glyphicon-thumbs-up" aria-hidden="true"></span>Approve
                                    </asp:LinkButton>
                                </div>
                                <div class="col-sm-1 paddingRight0 paddingLeft0">
                                    <asp:LinkButton ID="lbtnClear" runat="server" CssClass="floatRight" OnClientClick="ClearConfirm()" OnClick="lbtnClear_Click">
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
                                                        <asp:LinkButton ID="lbtnReject" CausesValidation="false" runat="server" OnClientClick="RejectConfirm()" OnClick="lbtnReject_Click">
                                        <span class="glyphicon glyphicon-share" aria-hidden="true"></span>Reject
                                                        </asp:LinkButton>
                                                    </div>

                                                    <div class="col-sm-12  ">
                                                        <asp:LinkButton ID="lbtnReversalSave" CausesValidation="false" runat="server" OnClientClick="SaveConfirm()" OnClick="lbtnReversalSave_Click">
                                        <span class="glyphicon glyphicon-saved" aria-hidden="true"></span>Save
                                                        </asp:LinkButton>
                                                    </div>







                                                    <div class="col-sm-12">
                                                        <asp:LinkButton ID="lbtnCancel" CausesValidation="false" runat="server" OnClick="lbtnCancel_Click">
                                        <span class="glyphicon glyphicon-remove" aria-hidden="true"></span>Cancel
                                                        </asp:LinkButton>
                                                    </div>



                                                    <div class="col-sm-12">
                                                        <asp:LinkButton ID="lbtnprint" CausesValidation="false" runat="server" OnClientClick="ConfirmPrint()" OnClick="lbtnPrint_Click">
                                    <span class="glyphicon glyphicon-print" aria-hidden="true"></span>Print
                                                        </asp:LinkButton>
                                                    </div>



                                                    <div class="col-sm-12">
                                                        <asp:LinkButton ID="lbtnprintserial" CausesValidation="false" runat="server" OnClientClick="ConfirmPrint()" OnClick="lbtnprintserial_Click">
                                    <span class="glyphicon glyphicon-print" aria-hidden="true"></span>Print Serail
                                                        </asp:LinkButton>
                                                    </div>


                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-sm-2">
                                    <asp:LinkButton ID="lbtnappTypePrint" CausesValidation="false" runat="server" OnClick="lbtnappTypePrint_Click" Visible="true">
                                        <span class="glyphicon glyphicon-print" aria-hidden="true"></span>Print Approval
                                    </asp:LinkButton>
                                </div>
                                <div class="col-sm-2" style="padding-right: 20px">
                                    <asp:DropDownList runat="server" ID="appType" Visible="true">
                                        <asp:ListItem Text="Select Type" Value="-1" Enabled="true"></asp:ListItem>
                                        <asp:ListItem Text="Invoice wise" Value="I"></asp:ListItem>
                                        <asp:ListItem Text="Serial wise" Value="S"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>

                    </div>
                    <div class="row">
                        <div class="col-sm-12">
                            <asp:Label ID="lblBackDateInfor" Font-Size="12px" runat="server" ForeColor="Red"></asp:Label>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
            <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                <ContentTemplate>
                    <asp:HiddenField ID="txtpdasend" runat="server" />
                    <asp:HiddenField ID="hdnprint" runat="server" />
                    <asp:HiddenField ID="txtClearlconformmessageValue" runat="server" />
                    <asp:HiddenField ID="txtRejectconformmessageValue" runat="server" />
                    <asp:HiddenField ID="txtSavelconformmessageValue" runat="server" />
                    <asp:HiddenField ID="txtUpdateconformmessageValue" runat="server" />
                    <asp:HiddenField ID="txtApprovalconformmessageValue" runat="server" />
                    <asp:HiddenField ID="txtCancelconformmessageValue" runat="server" />
                    <asp:HiddenField ID="txtDeleteconformmessageValue" runat="server" />
                    <asp:HiddenField ID="txtDelJobconformmessageValue" runat="server" />
                    <asp:HiddenField ID="txtDelRegconformmessageValue" runat="server" />
                    <asp:HiddenField ID="txtAppconformmessageValue" runat="server" />
                    <asp:HiddenField ID="txtRequestconformmessageValue" runat="server" />
                    <asp:HiddenField ID="diffLocSave" runat="server" />
                    <asp:HiddenField ID="txterrorno" runat="server" />
                    <div class="row">
                        <div class="col-sm-7 paddingRight5 ">
                            <div class="panel panel-default">
                                <div class="panel-heading paddingtopbottom0">
                                    <strong>Sales Reversal Note</strong>
                                </div>
                                <div class="panel-body">
                                    <div class="row">
                                        <div class="col-sm-4">
                                            <div class="row">
                                                <div class="col-sm-3 labelText1 ">
                                                    Date
                                                </div>
                                                <div class="col-sm-9 labelText1 paddingRight5 paddingLeft0">
                                                    <asp:Label ID="lblInvDate" runat="server"></asp:Label>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-12 height5">
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-3 labelText1 ">
                                                    Name 
                                                </div>
                                                <div class="col-sm-9 labelText1 paddingRight5 paddingLeft0">
                                                    <asp:Label ID="lblInvCusName" runat="server"></asp:Label>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-12 height5">
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-3 labelText1 ">
                                                    Address 
                                                </div>
                                                <div class="col-sm-9 labelText1 paddingRight5 paddingLeft0">
                                                    <asp:Label ID="lblInvCusAdd1" runat="server"></asp:Label>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-12 height5">
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-3 labelText1 ">
                                                </div>
                                                <div class="col-sm-9 paddingRight5 paddingLeft0">
                                                    <asp:Label ID="lblInvCusAdd2" runat="server"></asp:Label>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-12 height5">
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-sm-3 ">
                                            <div class="row">
                                                <div class="col-sm-3 labelText1 paddingLeft0">
                                                    Category
                                                </div>
                                                <div class="col-sm-8 paddingRight5 paddingLeft0">
                                                    <asp:TextBox runat="server" ID="txtSubType" AutoPostBack="true" CausesValidation="false" CssClass="form-control" OnTextChanged="txtSubType_TextChanged"></asp:TextBox>
                                                </div>
                                                <div class="col-sm-1 paddingLeft5 paddingRight5">
                                                    <asp:LinkButton ID="lbtncategory" runat="server" CausesValidation="false" OnClick="lbtncategory_Click">
                                                        <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                    </asp:LinkButton>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-12 height5">
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-3 labelText1 paddingLeft0">
                                                </div>
                                                <div class="col-sm-9 labelText1 paddingRight5 paddingLeft0">
                                                    <asp:Label ID="lblSubDesc" runat="server"></asp:Label>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-12 height5">
                                                </div>
                                            </div>

                                        </div>
                                        <div class="col-sm-5 ">
                                            <div class="row">
                                                <div class="col-sm-3 labelText1 paddingLeft5">
                                                    Customer
                                                </div>
                                                <div class="col-sm-7 paddingRight5 paddingLeft0">
                                                    <asp:TextBox runat="server" ID="txtCusCode" CausesValidation="false" AutoPostBack="true" CssClass="form-control" OnTextChanged="txtCusCode_TextChanged"></asp:TextBox>
                                                </div>
                                                <div class="col-sm-1 paddingLeft5 paddingRight0 ">
                                                    <asp:LinkButton ID="lbtnCustomer" runat="server" CausesValidation="false" OnClick="lbtnCustomer_Click">
                                                        <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                    </asp:LinkButton>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-12 height5">
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-3 labelText1 paddingLeft5">
                                                    Invoice #
                                                </div>
                                                <div class="col-sm-4 paddingRight5 paddingLeft0">
                                                    <asp:TextBox runat="server" ID="txtInvoice" AutoPostBack="true" CausesValidation="false" OnTextChanged="txtInvoice_TextChanged" CssClass="form-control"></asp:TextBox>
                                                </div>
                                                <div class="col-sm-1 paddingLeft5 paddingRight0 ">
                                                    <asp:LinkButton ID="lbtnInvoice" runat="server" CausesValidation="false" OnClick="lbtnInvoice_Click">
                                                        <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                    </asp:LinkButton>
                                                </div>
                                                <div class="col-sm-1  paddingLeft0 paddingRight0  ">
                                                    <asp:CheckBox ID="chkOthSales" runat="server" AutoPostBack="true" OnCheckedChanged="chkOthSales_CheckedChanged"></asp:CheckBox>
                                                </div>
                                                <div class="col-sm-3 paddingLeft0   ">
                                                    Other PC Sales
                                                </div>

                                            </div>
                                            <div class="row">
                                                <div class="col-sm-3 labelText1 paddingLeft5">
                                                    load Do serials
                                                </div>
                                                <div class="col-sm-12 ">
                                                    <div class="col-sm-1  paddingLeft0 paddingRight0  ">
                                                        <asp:CheckBox ID="chkserial" runat="server" Checked="true"></asp:CheckBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-5 labelText1 paddingLeft0">
                                                    Original profit center: 
                                                </div>
                                                <div class="col-sm-7 labelText1 paddingRight5 paddingLeft0">
                                                    <asp:Label ID="lblSalePc" runat="server"></asp:Label>
                                                </div>
                                            </div>
                                        </div>

                                    </div>
                                    <div class="row">
                                        <div class="col-sm-12 ">
                                            <div class="col-sm-1 labelText1 paddingLeft0">
                                                Remarks
                                            </div>
                                            <div class="col-sm-11   paddingLeft0">
                                                <asp:TextBox runat="server" ID="txtRemarks" CausesValidation="false" CssClass="form-control"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-sm-3 paddingLeft0">
                                            <div class="col-sm-5 labelText1 ">
                                                Request # :
                                            </div>
                                            <div class="col-sm-7 labelText1  paddingLeft0 paddingRight0">
                                                <asp:Label ID="lblReq" runat="server"></asp:Label>
                                            </div>
                                        </div>
                                        <div class="col-sm-3 paddingLeft0">
                                            <div class="col-sm-6 labelText1 ">
                                                Request PC :
                                            </div>
                                            <div class="col-sm-6 labelText1 ">
                                                <asp:Label ID="lblReqPC" runat="server"></asp:Label>
                                            </div>
                                        </div>
                                        <div class="col-sm-3 paddingLeft0">
                                            <div class="col-sm-8 labelText1 ">
                                                Req. return location :
                                            </div>
                                            <div class="col-sm-4 labelText1 ">
                                                <asp:Label ID="lblReturnLoc" runat="server"></asp:Label>
                                            </div>
                                        </div>
                                        <div class="col-sm-3 paddingLeft0">
                                            <div class="col-sm-5 labelText1 ">
                                                Status :
                                            </div>
                                            <div class="col-sm-7 labelText1 ">
                                                <asp:Label ID="lblStatus" ForeColor="Red" runat="server"></asp:Label>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-sm-5  paddingLeft5">
                            <div class="panel panel-default">
                                <div class="panel-heading paddingtopbottom0">
                                    <div class="row">
                                        <div class="col-sm-6 labelText1">
                                            <strong>Pending Request(s)</strong>
                                        </div>

                                        <div class="col-sm-2 labelText1 paddingLeft0   ">
                                            Approval Pending
                                        </div>
                                        <div class="col-sm-1   paddingRight0  ">
                                            <asp:CheckBox ID="chkApp" runat="server"></asp:CheckBox>
                                        </div>
                                        <div class="col-sm-1 paddingLeft5 paddingRight0 ">
                                            <asp:LinkButton ID="lbtnRefresh" runat="server" CausesValidation="false" OnClick="lbtnRefresh_Click">
                                                        <span class="glyphicon glyphicon-refresh" aria-hidden="true"></span>
                                            </asp:LinkButton>
                                        </div>
                                    </div>

                                </div>
                                <div class="panel-body">
                                    <div class="row">
                                        <div class="col-sm-12 height10 ">
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-sm-2 labelText1 ">
                                            Profit center
                                        </div>

                                        <div class="col-sm-2 paddingLeft5 paddingRight0">
                                            <asp:TextBox runat="server" ID="txtReqLoc" CausesValidation="false" CssClass="form-control" OnTextChanged="txtReqLoc_TextChanged"></asp:TextBox>
                                        </div>
                                        <div class="col-sm-1 paddingLeft5 paddingRight0 ">
                                            <asp:LinkButton ID="lbtnproftcenter" runat="server" CausesValidation="false" OnClick="lbtnproftcenter_Click">
                                                        <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                            </asp:LinkButton>
                                        </div>
                                        <div class="col-sm-3 labelText1 ">
                                            Completed Reversal #
                                        </div>
                                        <div class="col-sm-1   paddingRight0  ">
                                            <asp:CheckBox ID="chkFin" runat="server" AutoPostBack="true" OnCheckedChanged="chkFin_CheckedChanged"></asp:CheckBox>
                                        </div>
                                        <div class="col-sm-3 paddingLeft5 paddingRight0">
                                            <asp:TextBox runat="server" ID="txtRevNo" Visible="false" CausesValidation="false" CssClass="form-control" OnTextChanged="txtReqLoc_TextChanged"></asp:TextBox>
                                        </div>

                                        <asp:Panel runat="server" ID="Panel1" Visible="false">
                                            <div class="col-sm-2 labelText1 ">
                                                Temporary 
                                            </div>
                                            <div class="col-sm-3 paddingLeft5 paddingRight0">
                                                <asp:TextBox runat="server" ID="txtTemp" CausesValidation="false" CssClass="form-control"></asp:TextBox>
                                            </div>
                                            <div class="col-sm-1 paddingLeft5 paddingRight0 ">
                                                <asp:LinkButton ID="lbtnTemp" runat="server" CausesValidation="false" OnClick="lbtnTemp_Click">
                                                        <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                </asp:LinkButton>
                                            </div>
                                        </asp:Panel>
                                    </div>

                                    <div class="row">
                                        <div class="col-sm-12 ">
                                            <div class="col-sm-12 panelscollbar height100">
                                                <asp:GridView ID="grdPendings" EmptyDataText="No data found..." ShowHeaderWhenEmpty="True" runat="server" GridLines="None" CssClass="table table-hover table-striped" AutoGenerateColumns="false">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Select">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lbtnPendings" runat="server" CausesValidation="false" Width="5px" OnClick="lbtnPendings_Click">
                                                                        <span class="glyphicon glyphicon-arrow-down" aria-hidden="true" ></span>
                                                                </asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Req. No">
                                                            <ItemTemplate>
                                                                <asp:Label ID="col_reqNo" runat="server" Text='<%# Bind("grah_ref") %>' Width="150px"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Profit center">
                                                            <ItemTemplate>
                                                                <asp:Label ID="col_pc" runat="server" Text='<%# Bind("grah_loc") %>' Width="100px"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Invoice #">
                                                            <ItemTemplate>
                                                                <asp:Label ID="col_Inv" runat="server" Text='<%# Bind("grah_fuc_cd") %>' Width="150px"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Req. Date">
                                                            <ItemTemplate>
                                                                <asp:Label ID="col_reqDate" runat="server" Text='<%# Bind("grah_cre_dt","{0:dd/MMM/yyyy}") %>' Width="80px"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Status">
                                                            <ItemTemplate>
                                                                <asp:Label ID="col_Status" runat="server" Text='<%# Bind("grah_app_stus") %>' Width="20px"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Remarks">
                                                            <ItemTemplate>
                                                                <asp:Label ID="col_remarks" runat="server" Text='<%# Bind("grah_remaks") %>' Width="200px"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Type" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="col_Type" runat="server" Text='<%# Bind("grah_oth_loc") %>' Width="5px"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Sub Type" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="col_SubType" runat="server" Text='<%# Bind("grah_sub_type") %>' Width="5px"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Original Sales PC">
                                                            <ItemTemplate>
                                                                <asp:Label ID="col_OthPC" runat="server" Text='<%# Bind("grah_oth_pc") %>' Width="100px"></asp:Label>
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
                    <div class="row">
                        <div class="col-sm-12">
                            <div class="panel panel-default">
                                <div class="panel-heading paddingtopbottom0">
                                    Reversal Details
                                </div>
                                <div class="panel-body padding0" style="max-height: 80px; overflow-y: scroll">
                                    <asp:GridView ID="grdInvItem" EmptyDataText="No data found..." ShowHeaderWhenEmpty="True" runat="server" GridLines="None" CssClass="table table-hover table-striped">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Select" Visible="true">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lbtnItemsselect" runat="server" CausesValidation="false" Width="5px" OnClick="lbtnItemsselect_Click">
                                                                        <span class="glyphicon glyphicon-arrow-down" aria-hidden="true" ></span>
                                                    </asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Filter" Visible="true">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lbtnItemsfilter" runat="server" CausesValidation="false" Width="5px" OnClick="lbtnItemsfilter_Click">
                                                                        <span class="glyphicon glyphicon-arrow-down" aria-hidden="true" ></span>
                                                    </asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="">
                                                <ItemTemplate>
                                                    <asp:Label ID="col_invLine" runat="server" Text='<%# Bind("sad_itm_line") %>' Width="100px"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Item">
                                                <ItemTemplate>
                                                    <asp:Label ID="col_invItem" runat="server" Text='<%# Bind("sad_itm_cd") %>' Width="100px"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Model">
                                                <ItemTemplate>
                                                    <asp:Label ID="col_Mi_model" runat="server" Text='<%# Bind("Mi_model") %>' Width="100px"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Status" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="col_invStus" runat="server" Text='<%# Bind("sad_itm_stus") %>' Width="100px"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Status">
                                                <ItemTemplate>
                                                    <asp:Label ID="col_Sad_itm_stus_desc" runat="server" Text='<%# Bind("Sad_itm_stus_desc") %>' Width="100px"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Qty">
                                                <ItemTemplate>
                                                    <asp:Label ID="col_invQty" runat="server" Text='<%# Bind("sad_qty","{0:n}") %>' Width="100px"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Reverse Qty">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="col_invRevQty" OnTextChanged="col_invRevQty_TextChanged" Style="text-align: right" AutoPostBack="true" onkeypress="return isNumberKey(event)" CssClass="form-control" runat="server" Text='<%# Bind("sad_srn_qty","{0:n}") %>' Width="80px"></asp:TextBox>

                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Unit Price">
                                                <ItemTemplate>
                                                    <asp:Label ID="col_invUnitRt" runat="server" Text='<%# Bind("sad_unit_rt","{0:n}") %>' Width="100px"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Amount">
                                                <ItemTemplate>
                                                    <asp:Label ID="col_invAmt" runat="server" Text='<%# Bind("sad_unit_amt","{0:n}") %>' Width="100px"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Discount">
                                                <ItemTemplate>
                                                    <asp:Label ID="col_invDis" runat="server" Text='<%# Bind("sad_disc_amt","{0:n}") %>' Width="100px"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Tax Amount">
                                                <ItemTemplate>
                                                    <asp:Label ID="col_invTax" runat="server" Text='<%# Bind("sad_itm_tax_amt","{0:n}") %>' Width="100px"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Total">
                                                <ItemTemplate>
                                                    <asp:Label ID="col_invTot" runat="server" Text='<%# Bind("sad_tot_amt","{0:n}") %>' Width="100px"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Delete" Visible="false">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lbtnDeleteItem" runat="server" CausesValidation="false" Width="5px" OnClientClick="DeleteConfirm()" OnClick="lbtnDeleteItem_Click">
                                                <span class="glyphicon glyphicon-trash" aria-hidden="true" ></span>
                                                    </asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="DO Qty" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="col_revDOQty" runat="server" Text='<%# Bind("sad_fws_ignore_qty") %>' Width="100px"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="DO" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_sad_do_qty" runat="server" Text='<%# Bind("sad_do_qty") %>' Width="100px"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>
                        </div>

                    </div>
                    <div class="row">
                        <div class="col-sm-12">
                            <div class="panel panel-default">
                                <div class="panel-body">
                                    <div class="row">
                                        <div class="col-sm-2 paddingRight0">
                                            <div class="col-sm-3 labelText1 paddingLeft0">
                                                Item
                                            </div>
                                            <div class="col-sm-9 labelText1">
                                                <asp:TextBox runat="server" Enabled="false" ID="txtSItem" CausesValidation="false" CssClass="form-control"></asp:TextBox>
                                            </div>

                                        </div>
                                        <div class="col-sm-2 paddingRight0 paddingLeft5">
                                            <div class="col-sm-1 labelText1 paddingLeft0">
                                                Serial
                                            </div>
                                            <div class="col-sm-11 labelText1 paddingRight5 ">
                                                <asp:TextBox runat="server" ID="txtSSerial" CausesValidation="false" CssClass="form-control"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-sm-1 Lwidth ">
                                            <asp:LinkButton ID="lbtnFilterSerial" runat="server" CausesValidation="false" OnClick="lbtnFilterSerial_Click">
                                                        <span class="glyphicon glyphicon-filter" aria-hidden="true"></span>
                                            </asp:LinkButton>
                                        </div>
                                        <div class="col-sm-1 paddingRight0 paddingLeft5">
                                            <div class="col-sm-1 labelText1 paddingLeft0">
                                                Qty
                                            </div>
                                            <div class="col-sm-11 labelText1 paddingRight5 ">
                                                <asp:TextBox runat="server" ID="txtqty" onkeydown="return jsDecimals(event);" CausesValidation="false" CssClass="form-control"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-sm-1 Lwidth ">
                                        </div>
                                        <div class="col-sm-1">
                                            <asp:Button ID="btnAllSerial" runat="server" CssClass="btn btn-primary btn-xs" Text="All" OnClick="btnAllSerial_Click" />
                                        </div>
                                        <div class="col-sm-2 ">
                                            <div class="col-sm-5 labelText1 paddingLeft0">
                                                Change Status
                                            </div>
                                            <div class="col-sm-7 labelText1">
                                                <asp:DropDownList ID="ddlChangestatus" CausesValidation="false" AutoPostBack="true" runat="server" CssClass="form-control">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-sm-1  ">
                                            <asp:LinkButton ID="lbtnAddReversalSerial" runat="server" CausesValidation="false" OnClick="lbtnAddReversalSerial_Click">
                                                        <span class="glyphicon glyphicon-plus" aria-hidden="true"></span>
                                            </asp:LinkButton>
                                        </div>
                                        <div class="row">
                                            <div class="col-sm-1 labelText1 paddingLeft5">
                                                Difference locations
                                            </div>
                                            <div class="col-sm-1">
                                                 <div class="col-sm-1  paddingLeft0 paddingRight0  ">
                                                     <asp:CheckBox ID="chkDiffLoc" runat="server" Checked="False"></asp:CheckBox>
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
                                <div class="panel-heading paddingtopbottom0" style="height: 16px;">
                                    <div class="col-sm-12">
                                        <div class="col-sm-6">
                                            Delivery Details
                                        </div>
                                        <div class="col-sm-6">
                                            <asp:LinkButton ID="lbtnSerVar" ToolTip="Sub serial verify..." CausesValidation="false" runat="server"
                                                OnClick="lbtnSerVar_Click">
                                                         <span class="glyphicon " aria-hidden="true" style="font-size:15px"></span> Serial Verification
                                            </asp:LinkButton>
                                        </div>
                                    </div>
                                </div>
                                <div class="panel-body panelscollbar height80">
                                    <asp:GridView ID="grdDelDetails" EmptyDataText="No data found..." ShowHeaderWhenEmpty="True" runat="server" GridLines="None" CssClass="table table-hover table-striped" AutoGenerateColumns="false">
                                        <Columns>
                                            <asp:TemplateField HeaderText="" Visible="true">
                                                <HeaderTemplate>
                                                    <asp:CheckBox ID="allDchk" runat="server" Width="5px" onclick="CheckAllDItems(this)"></asp:CheckBox>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chk_DItem" runat="server" Checked="false" Width="30px" />
                                                </ItemTemplate>
                                                <HeaderStyle Width="20px" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Location">
                                                <ItemTemplate>
                                                    <asp:Label ID="col_loc" runat="server" Text='<%# Bind("tus_loc") %>' Width="100px"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="DO #">
                                                <ItemTemplate>
                                                    <asp:Label ID="col_doc" runat="server" Text='<%# Bind("tus_doc_no") %>' Width="100px"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Serial">
                                                <ItemTemplate>
                                                    <asp:Label ID="col_Serial" runat="server" Text='<%# Bind("tus_ser_1") %>' Width="200px"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Other serial">
                                                <ItemTemplate>
                                                    <asp:Label ID="col_Serial1" runat="server" Text='<%# Bind("tus_ser_2") %>' Width="100px"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Invoice #">
                                                <ItemTemplate>
                                                    <asp:Label ID="col_baseDoc" runat="server" Text='<%# Bind("tus_base_doc_no") %>' Width="100px"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="DO Date">
                                                <ItemTemplate>
                                                    <asp:Label ID="col_doDate" runat="server" Text='<%# Bind("tus_doc_dt", "{0:dd/MMM/yyyy}") %>' Width="100px"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Item">
                                                <ItemTemplate>
                                                    <asp:Label ID="col_item" runat="server" Text='<%# Bind("tus_itm_cd") %>' Width="100px"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Status" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="col_itmstatus" runat="server" Text='<%# Bind("tus_itm_stus") %>' Width="100px"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Status">
                                                <ItemTemplate>
                                                    <asp:Label ID="col_Tus_itm_stus_Desc" runat="server" Text='<%# Bind("Tus_itm_stus_Desc") %>' Width="100px"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="App Stus">
                                                <ItemTemplate>
                                                    <asp:DropDownList ID="col_appstatus" CssClass="form-control" runat="server" Width="100px"></asp:DropDownList>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Qty">
                                                <ItemTemplate>
                                                    <asp:Label ID="col_Qty" runat="server" Text='<%# Bind("tus_qty","{0:n}") %>' Width="100px"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Line" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="col_BaseLine" runat="server" Text='<%# Bind("Tus_base_itm_line") %>' Width="100px"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Delete" Visible="false">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lbtnSerDel" runat="server" CausesValidation="false" Width="5px">
                                                <span class="glyphicon glyphicon-trash" aria-hidden="true" ></span>
                                                    </asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Serial ID" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="col_SerID" runat="server" Text='<%# Bind("Tus_ser_id") %>' Width="100px"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Wara" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="col_Wara" runat="server" Text='<%# Bind("Tus_warr_no") %>' Width="100px"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="lineno" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="col_Tus_temp_itm_line" runat="server" Text='<%# Bind("Tus_temp_itm_line") %>' Width="100px"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="seriId" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="col_Tus_ser_id" runat="server" Text='<%# Bind("Tus_ser_id") %>' Width="100px"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>
                        </div>
                        <div class="col-sm-6">
                            <div class="panel panel-default">
                                <div class="panel-heading paddingtopbottom0">
                                    Reversal serial Details
                                </div>
                                <div class="panel-body panelscollbar height80">
                                    <asp:GridView ID="grdSelectReversal" EmptyDataText="No data found..." ShowHeaderWhenEmpty="True" runat="server" GridLines="None" CssClass="table table-hover table-striped" AutoGenerateColumns="false">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Select" Visible="true">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lbtnSerSelect" OnClick="lbtnSerSelect_Click" runat="server" CausesValidation="false" Width="5px">
                                                <span class="glyphicon glyphicon-arrow-down" aria-hidden="true" ></span>
                                                    </asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Location">
                                                <ItemTemplate>
                                                    <asp:Label ID="colR_loc" runat="server" Text='<%# Bind("tus_loc") %>' Width="100px"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="DO #">
                                                <ItemTemplate>
                                                    <asp:Label ID="colR_doc" runat="server" Text='<%# Bind("tus_doc_no") %>' Width="100px"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Serial">
                                                <ItemTemplate>
                                                    <asp:Label ID="colR_Serial" runat="server" Text='<%# Bind("tus_ser_1") %>' Width="100px"></asp:Label>
                                                    <%--<asp:Label ID="colR_Serial" runat="server" ToolTip='<%#Bind("tus_ser_1") %>' Text='<%#Eval("tus_ser_1").ToString().Length > 12? (Eval("tus_ser_1") as string).Substring(0,12) + " ..." : Eval("tus_ser_1") %>' Width="100px"></asp:Label>--%>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Other serial">
                                                <ItemTemplate>
                                                    <asp:Label ID="colR_Serial1" runat="server" Text='<%# Bind("tus_ser_2") %>' Width="100px"></asp:Label>
                                                    <%--<asp:Label ID="colR_Serial1" runat="server" ToolTip='<%#Bind("tus_ser_2") %>' Text='<%#Eval("tus_ser_2").ToString().Length > 12? (Eval("tus_ser_2") as string).Substring(0,12) + " ..." : Eval("tus_ser_2") %>' Width="100px"></asp:Label>--%>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Invoice #">
                                                <ItemTemplate>
                                                    <asp:Label ID="colR_baseDoc" runat="server" Text='<%# Bind("tus_base_doc_no") %>' Width="100px"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="DO Date">
                                                <ItemTemplate>
                                                    <asp:Label ID="colR_doDate" runat="server" Text='<%# Bind("tus_doc_dt", "{0:dd/MMM/yyyy}") %>' Width="100px"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Item">
                                                <ItemTemplate>
                                                    <asp:Label ID="colR_item" runat="server" Text='<%# Bind("tus_itm_cd") %>' Width="100px"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Status" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="colR_itmstatus" runat="server" Text='<%# Bind("tus_itm_stus") %>' Width="100px"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Status">
                                                <ItemTemplate>
                                                    <asp:Label ID="col_Tus_itm_stus_Desc" runat="server" Text='<%# Bind("Tus_itm_stus_Desc") %>' Width="100px"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="App Stus">
                                                <ItemTemplate>
                                                    <%--<asp:DropDownList ID="colR_appstatus" CssClass="form-control" runat="server" Width="100px"></asp:DropDownList>--%>
                                                    <%--<asp:DropDownList ID="colR_appstatus" CssClass="form-control" Text='<%# Bind("tus_appstatus") ></asp:DropDownLis--%>
                                                    <%--<asp:DropDownList ID="colR_appstatus" CssClass="form-control" runat="server" Text='<%# Bind("Tus_appstatus") %>' Width="100px"></asp:DropDownList>--%>
                                                   <%--Below two lines rempved by Wimal @ 03/Aug/2018--%>
                                                   <%----<asp:Label ID="colR_appstatus" Visible="false" runat="server" Text='<%# Bind("tus_appstatus") %>' Width="100px"></asp:Label>--%>
                                                    <%--<asp:Label ID="Label7" runat="server" Text='<%# Bind("Tus_new_status_Desc") %>' Width="100px"></asp:Label>--%>
                                                    <%--<asp:DropDownList ID="colR_appstatus" CssClass="form-control" Text='<%# Bind("tus_appstatus") %>' runat="server" Width="100px"></asp:DropDownList>--%>
                                                      <asp:Label ID="colR_appstatus" Visible="false" runat="server" Text='<%# Bind("tus_appstatus") %>' Width="100px"></asp:Label>
                                                    <asp:Label ID="Label7" runat="server" Text='<%# Bind("Tus_new_status_Desc") %>' Width="100px"></asp:Label>
                                                    <%--<asp:DropDownList ID="colR_appstatus" CssClass="form-control" Text='<%# Bind("tus_appstatus") %>' runat="server" Width="100px"></asp:DropDownList>--%>
                                                     </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Qty">
                                                <ItemTemplate>
                                                    <asp:Label ID="colR_Qty" runat="server" Text='<%# Bind("tus_qty","{0:n}") %>' Width="100px"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Line" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="colR_BaseLine" runat="server" Text='<%# Bind("Tus_base_itm_line") %>' Width="100px"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Serial ID" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="colR_SerID" runat="server" Text='<%# Bind("Tus_ser_id") %>' Width="100px"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Wara" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="colR_Wara" runat="server" Text='<%# Bind("Tus_warr_no") %>' Width="100px"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="line" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="colTus_itm_line" runat="server" Text='<%# Bind("Tus_itm_line") %>' Width="100px"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField Visible="true">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lbtnSerRDel" runat="server" OnClientClick="DeleteConfirm()" CausesValidation="false" Width="5px" OnClick="lbtnSerRDel_Click">
                                                <span class="glyphicon glyphicon-trash" aria-hidden="true" ></span>
                                                    </asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-sm-12 ">
                            <div class="panel panel-default">
                                <div class="panel-heading paddingtopbottom0">
                                    Additional details
                                </div>
                                <div class="panel-body">
                                    <div class="row">
                                        <div class="col-sm-2 paddingRight0">
                                            <div class="col-sm-3 labelText1 paddingLeft0">
                                                Rev. Date 
                                            </div>
                                            <div class="col-sm-7 paddingRight5 paddingLeft0">
                                                <asp:TextBox runat="server" ID="txtSRNDate" Enabled="false" CausesValidation="false" CssClass="form-control"></asp:TextBox>
                                            </div>
                                            <div class="col-sm-1 paddingLeft0">
                                                <asp:LinkButton ID="lbtnSRNDate" runat="server" Visible="false" CausesValidation="false">
                                                            <span class="glyphicon glyphicon-calendar" aria-hidden="true"></span>
                                                </asp:LinkButton>

                                                <asp:CalendarExtender ID="CalendarExtender3" runat="server" TargetControlID="txtSRNDate"
                                                    PopupButtonID="lbtnSRNDate" Format="dd/MMM/yyyy">
                                                </asp:CalendarExtender>
                                            </div>
                                        </div>
                                        <div class="col-sm-4 paddingRight0">
                                            <div class="col-sm-4 labelText1 paddingLeft0">
                                                Manual reference
                                            </div>
                                            <div class="col-sm-5 paddingLeft5 paddingRight0">
                                                <asp:TextBox runat="server" ID="txtManRef" CausesValidation="false" CssClass="form-control"></asp:TextBox>
                                            </div>
                                            <div class="col-sm-1  paddingRight0">
                                                <asp:CheckBox ID="chkIsMan" runat="server" AutoPostBack="true" OnCheckedChanged="chkIsMan_CheckedChanged1"></asp:CheckBox>
                                            </div>
                                            <div class="col-sm-2 labelText1 paddingLeft0 paddingRight0  ">
                                                Is Manual
                                            </div>
                                        </div>
                                        <div class="col-sm-3 paddingRight0">
                                            <div class="col-sm-6 labelText1  paddingLeft5 paddingRight0">
                                                Actual item transfer location
                                            </div>
                                            <div class="col-sm-4 paddingLeft5 paddingRight0">
                                                <asp:TextBox runat="server" ID="txtActLoc" CausesValidation="false" CssClass="form-control"></asp:TextBox>
                                            </div>
                                            <div class="col-sm-1 paddingLeft5 paddingRight0 ">
                                                <asp:LinkButton ID="lbtnSearchActLoc" runat="server" OnClick="lbtnSearchActLoc_Click" CausesValidation="false">
                                                        <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                </asp:LinkButton>
                                            </div>
                                        </div>

                                    </div>
                                    <div class="row buttonRow">
                                        <div class="col-sm-7">
                                            <div class="col-sm-1 labelText1 paddingLeft0">
                                                Remarks
                                            </div>
                                            <div class="col-sm-11 labelText1 paddingRight5 paddingLeft0">
                                                <asp:TextBox runat="server" ID="txtSRNremarks" CausesValidation="false" CssClass="form-control"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-sm-5 paddingRight0">
                                            <div class="col-sm-3 paddingRight0">
                                                <asp:LinkButton ID="lbtnNewItem" CausesValidation="false" runat="server" CssClass="floatRight" OnClientClick="TempSaveConfirm()" OnClick="lbtnNewItem_Click">
                                        <span class="glyphicon glyphicon-retweet" aria-hidden="true"></span>Re-report Items
                                                </asp:LinkButton>
                                            </div>
                                            <div class="col-sm-3 paddingRight0">
                                                <asp:LinkButton ID="lbtnAttSerApp" CausesValidation="false" runat="server" CssClass="floatRight" OnClientClick="TempSaveConfirm()" OnClick="lbtnAttSerApp_Click">
                                        <span class="glyphicon glyphicon-thumbs-up" aria-hidden="true"></span>Service Approval
                                                </asp:LinkButton>
                                            </div>
                                            <div class="col-sm-4 paddingRight0">
                                                <asp:LinkButton ID="lbtnRegDetails" Visible="false" CausesValidation="false" runat="server" CssClass="floatRight" OnClientClick="TempSaveConfirm()" OnClick="lbtnRegDetails_Click">
                                        <span class="glyphicon glyphicon-euro" aria-hidden="true"></span>Cash Refund Requests
                                                </asp:LinkButton>
                                            </div>
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



    <asp:UpdatePanel ID="UpdatePanel11" runat="server">
        <ContentTemplate>
            <asp:Button ID="Button4" runat="server" Text="Button" Style="display: none;" />
            <asp:ModalPopupExtender ID="UserCashRefundRequestsPopup" runat="server" Enabled="True" TargetControlID="Button4"
                PopupControlID="pnlCashRefundRequestsPopup" CancelControlID="LinkButton5" PopupDragHandleControlID="PopupHeader" Drag="true" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>

        </ContentTemplate>

    </asp:UpdatePanel>


    <asp:Panel runat="server" ID="pnlCashRefundRequestsPopup">
        <div runat="server" id="Div4" class="panel panel-default height400 width800">
            <asp:Label ID="Label4" runat="server" Text="Label" Visible="false"></asp:Label>
            <div class="panel panel-default">
                <div class="panel-heading">
                    <asp:LinkButton ID="LinkButton5" runat="server">
                             <span class="glyphicon glyphicon-remove" aria-hidden="true">Close</span>
                    </asp:LinkButton>
                    <%--<span>Commen Search</span>--%>
                    <div class="col-sm-11">
                        <asp:Label ID="lblRefError" ForeColor="Red" runat="server"></asp:Label>
                    </div>
                    <div class="col-sm-1">
                    </div>
                </div>
                <div class="panel-body">
                    <div class="bs-example">
                        <ul class="nav nav-tabs" id="myTab">
                            <li class="active"><a href="#RRefunds" data-toggle="tab">Registration Refunds</a></li>
                            <li><a href="#IRefunds" data-toggle="tab">Insuarance Refunds</a></li>
                            <li onclick="document.getElementById('<%= lbtntab.ClientID %>').click();"><a href="#SRefunds" data-toggle="tab">Sales Payment Refunds</a></li>
                        </ul>
                    </div>
                    <div class="row">
                        <div class="col-sm-12 height5">
                            <asp:LinkButton ID="lbtntab" runat="server" Visible="false" CausesValidation="false" OnClick="lbtntab_Click">
                                                      
                            </asp:LinkButton>
                        </div>
                    </div>
                    <div class="tab-content">
                        <div class="tab-pane active" id="RRefunds">
                            <asp:ScriptManagerProxy ID="ScriptManagerProxy3" runat="server"></asp:ScriptManagerProxy>
                            <asp:UpdatePanel ID="UpdatePanel12" runat="server">
                                <ContentTemplate>
                                    <div class="panel panel-default">
                                        <div class="panel-body">
                                            <div class="col-sm-12">

                                                <div class="row">
                                                    <div class="col-sm-8">
                                                        <div class="row">
                                                            <div class="col-sm-1  paddingRight0">
                                                                <asp:CheckBox ID="chkRevReg" OnCheckedChanged="chkRevReg_CheckedChanged" AutoPostBack="true" runat="server"></asp:CheckBox>
                                                            </div>
                                                            <div class="col-sm-5 labelText1 paddingLeft0 paddingRight0  ">
                                                                Refund Registration
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-sm-12 height5">
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-sm-3 labelText1 ">
                                                                Item 
                                                            </div>
                                                            <div class="col-sm-3 paddingLeft5 paddingRight0">
                                                                <asp:TextBox runat="server" ID="txtRevRegItem" AutoPostBack="true" CausesValidation="false" CssClass="form-control" OnTextChanged="txtRevRegItem_TextChanged"></asp:TextBox>
                                                            </div>
                                                            <div class="col-sm-1 paddingLeft5 paddingRight0 ">
                                                                <asp:LinkButton ID="lbtnRevRegItem" runat="server" CausesValidation="false" OnClick="lbtnRevRegItem_Click">
                                                        <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                                </asp:LinkButton>
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-sm-12 height5">
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-sm-3 labelText1 ">
                                                                Engine # 
                                                            </div>
                                                            <div class="col-sm-3 paddingLeft5 paddingRight0">
                                                                <asp:TextBox runat="server" ID="txtRevEngine" CausesValidation="false" CssClass="form-control"></asp:TextBox>
                                                            </div>
                                                            <div class="col-sm-1 paddingLeft5 paddingRight0 ">
                                                                <asp:LinkButton ID="lbtnRevEngine" runat="server" CausesValidation="false" OnClick="lbtnRevEngine_Click">
                                                        <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                                </asp:LinkButton>
                                                            </div>
                                                            <div class="col-sm-2">
                                                                <asp:LinkButton ID="lbtnRevRegAdd" runat="server" CausesValidation="false" OnClick="lbtnRevRegAdd_Click">
                                                <span class="glyphicon glyphicon-plus" aria-hidden="true"></span>
                                                                </asp:LinkButton>
                                                            </div>
                                                            <div class="col-sm-2 paddingLeft0 paddingRight0">
                                                                <asp:Button ID="btnGetRegAll" runat="server" CssClass="btn btn-default btn-xs" Text="Get All" OnClick="btnGetRegAll_Click" />
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="col-sm-4">
                                                        <div class="row">
                                                            <div class="col-sm-4 labelText1 ">
                                                                Request # 
                                                            </div>
                                                            <div class="col-sm-8 labelText1 paddingLeft0 paddingRight0">
                                                                <asp:Label ID="lblRegReq" runat="server"></asp:Label>
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-sm-12 height5">
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-sm-4 labelText1 ">
                                                                Status 
                                                            </div>
                                                            <div class="col-sm-4 labelText1 ">
                                                                <asp:Label ID="lblRegStatus" runat="server"></asp:Label>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-sm-12 height5">
                                                    </div>
                                                </div>
                                                <div class="row">

                                                    <div class="col-sm-12 panelscollbar height170 paddingRight0">
                                                        <asp:ScriptManagerProxy ID="ScriptManagerProxy6" runat="server"></asp:ScriptManagerProxy>
                                                        <asp:UpdatePanel ID="UpdatePanel13" runat="server">
                                                            <ContentTemplate>
                                                                <asp:GridView ID="grdRegDetails" CausesValidation="false" EmptyDataText="No data found..." ShowHeaderWhenEmpty="True" runat="server" GridLines="None" CssClass="table table-hover table-striped" PageSize="8" PagerStyle-CssClass="cssPager" AllowPaging="True" OnSelectedIndexChanged="grdResult_SelectedIndexChanged" OnPageIndexChanging="grdResult_PageIndexChanging">
                                                                    <Columns>

                                                                        <asp:TemplateField HeaderText="Receipt #">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="col_regRec" runat="server" Text='<%# Bind("P_srvt_ref_no") %>' Width="100px"></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Receipt Date">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="col_regRecDt" runat="server" Text='<%# Bind("P_svrt_dt", "{0:dd/MMM/yyyy}") %>' Width="100px"></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Invoice #">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="col_regInv" runat="server" Text='<%# Bind("p_svrt_inv_no") %>' Width="100px"></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Item">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="col_regItm" runat="server" Text='<%# Bind("p_srvt_itm_cd") %>' Width="100px"></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Model">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="col_regModel" runat="server" Text='<%# Bind("p_svrt_model") %>' Width="100px"></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Engine #">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="col_regEngine" runat="server" Text='<%# Bind("p_svrt_engine") %>' Width="100px"></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Chassis #">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="col_regChassis" runat="server" Text='<%# Bind("p_svrt_chassis") %>' Width="100px"></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="Amount">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="col_regVal" runat="server" Text='<%# Bind("p_svrt_reg_val","{0:n}") %>' Width="100px"></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Send to RMV" Visible="false">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="col_regRMV" runat="server" Text='<%# Bind("p_srvt_rmv_stus") %>' Width="100px"></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Delete" Visible="false">
                                                                            <ItemTemplate>
                                                                                <asp:LinkButton ID="lbtnRegDetailssDel" runat="server" OnClientClick="DeleteRegConfirm()" CausesValidation="false" Width="5px" OnClick="lbtnRegDetailssDel_Click">
                                                                        <span class="glyphicon glyphicon-trash" aria-hidden="true" ></span>
                                                                                </asp:LinkButton>
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
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                        <div class="tab-pane" id="IRefunds">
                            <asp:ScriptManagerProxy ID="ScriptManagerProxy7" runat="server"></asp:ScriptManagerProxy>
                            <asp:UpdatePanel ID="UpdatePanel14" runat="server">
                                <ContentTemplate>
                                    <div class="panel panel-default">
                                        <div class="panel-body">
                                            <div class="col-sm-12">

                                                <div class="row">
                                                    <div class="col-sm-8">
                                                        <div class="row">
                                                            <div class="col-sm-1  paddingRight0">
                                                                <asp:CheckBox ID="chkRevIns" AutoPostBack="true" runat="server" OnCheckedChanged="chkRevIns_CheckedChanged"></asp:CheckBox>
                                                            </div>
                                                            <div class="col-sm-5 labelText1 paddingLeft0 paddingRight0  ">
                                                                Refund Registration
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-sm-12 height5">
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-sm-3 labelText1 ">
                                                                Item 
                                                            </div>
                                                            <div class="col-sm-3 paddingLeft5 paddingRight0">
                                                                <asp:TextBox runat="server" ID="txtRevInsItem" AutoPostBack="true" CausesValidation="false" CssClass="form-control" OnTextChanged="txtRevInsItem_TextChanged"></asp:TextBox>
                                                            </div>
                                                            <div class="col-sm-1 paddingLeft5 paddingRight0 ">
                                                                <asp:LinkButton ID="lbtnRevInsItem" runat="server" CausesValidation="false" OnClick="lbtnRevInsItem_Click">
                                                        <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                                </asp:LinkButton>
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-sm-12 height5">
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-sm-3 labelText1 ">
                                                                Engine # 
                                                            </div>
                                                            <div class="col-sm-3 paddingLeft5 paddingRight0">
                                                                <asp:TextBox runat="server" ID="txtRevInsEngine" CausesValidation="false" CssClass="form-control"></asp:TextBox>
                                                            </div>
                                                            <div class="col-sm-1 paddingLeft5 paddingRight0 ">
                                                                <asp:LinkButton ID="lbtnRevInsEngine" runat="server" CausesValidation="false" OnClick="lbtnRevInsEngine_Click">
                                                        <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                                </asp:LinkButton>
                                                            </div>
                                                            <div class="col-sm-2">
                                                                <asp:LinkButton ID="lbtnRevInsAdd" runat="server" CausesValidation="false" OnClick="lbtnRevInsAdd_Click">
                                                <span class="glyphicon glyphicon-plus" aria-hidden="true"></span>
                                                                </asp:LinkButton>
                                                            </div>
                                                            <div class="col-sm-2 paddingLeft0 paddingRight0">
                                                                <asp:Button ID="btnGetInsAll" runat="server" CssClass="btn btn-default btn-xs" Text="Get All" OnClick="btnGetInsAll_Click" />
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="col-sm-4">
                                                        <div class="row">
                                                            <div class="col-sm-4 labelText1 ">
                                                                Request # 
                                                            </div>
                                                            <div class="col-sm-8 labelText1 ">
                                                                <asp:Label ID="lblInsReq" runat="server"></asp:Label>
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-sm-12 height5">
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-sm-4 labelText1 ">
                                                                Status 
                                                            </div>
                                                            <div class="col-sm-4 labelText1 ">
                                                                <asp:Label ID="lblInsStatus" runat="server"></asp:Label>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-sm-12 height5">
                                                    </div>
                                                </div>
                                                <div class="row">

                                                    <div class="col-sm-12 panelscollbar height170 paddingRight0">
                                                        <asp:ScriptManagerProxy ID="ScriptManagerProxy11" runat="server"></asp:ScriptManagerProxy>
                                                        <asp:UpdatePanel ID="UpdatePanel15" runat="server">
                                                            <ContentTemplate>
                                                                <asp:GridView ID="grdInsDetails" CausesValidation="false" EmptyDataText="No data found..." ShowHeaderWhenEmpty="True" runat="server" GridLines="None" CssClass="table table-hover table-striped" PageSize="8" PagerStyle-CssClass="cssPager" AllowPaging="True" OnSelectedIndexChanged="grdResult_SelectedIndexChanged" OnPageIndexChanging="grdResult_PageIndexChanging">
                                                                    <Columns>

                                                                        <asp:TemplateField HeaderText="Receipt #">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="col_regRec" runat="server" Text='<%# Bind("P_srvt_ref_no") %>' Width="100px"></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Receipt Date">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="col_regRecDt" runat="server" Text='<%# Bind("P_svrt_dt", "{0:dd/MMM/yyyy}") %>' Width="100px"></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Invoice #">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="col_regInv" runat="server" Text='<%# Bind("p_svrt_inv_no") %>' Width="100px"></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Item">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="col_regItm" runat="server" Text='<%# Bind("p_srvt_itm_cd") %>' Width="100px"></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Model">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="col_regModel" runat="server" Text='<%# Bind("p_svrt_model") %>' Width="100px"></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Engine #">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="col_regEngine" runat="server" Text='<%# Bind("p_svrt_engine") %>' Width="100px"></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Chassis #">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="col_regChassis" runat="server" Text='<%# Bind("p_svrt_chassis") %>' Width="100px"></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="Amount">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="col_regVal" runat="server" Text='<%# Bind("p_svrt_reg_val","{0:n}") %>' Width="100px"></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Send to RMV" Visible="false">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="col_regRMV" runat="server" Text='<%# Bind("p_srvt_rmv_stus") %>' Width="100px"></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Delete" Visible="false">
                                                                            <ItemTemplate>
                                                                                <asp:LinkButton ID="lbtnRereportItemsDel" runat="server" CausesValidation="false" Width="5px">
                                                                        <span class="glyphicon glyphicon-trash" aria-hidden="true" ></span>
                                                                                </asp:LinkButton>
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
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                        <div class="tab-pane" id="SRefunds">
                            <asp:ScriptManagerProxy ID="ScriptManagerProxy12" runat="server"></asp:ScriptManagerProxy>
                            <asp:UpdatePanel ID="UpdatePanel16" runat="server">
                                <ContentTemplate>
                                    <div class="panel panel-default">
                                        <div class="panel-body">
                                            <div class="col-sm-12">

                                                <div class="row">
                                                    <div class="col-sm-8">
                                                        <div class="row">
                                                            <div class="col-sm-1  paddingRight0">
                                                                <asp:CheckBox ID="chkCashRefund" runat="server"></asp:CheckBox>
                                                            </div>
                                                            <div class="col-sm-5 labelText1 paddingLeft0 paddingRight0  ">
                                                                Refund Registration
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-sm-12 height5">
                                                            </div>
                                                        </div>
                                                    </div>

                                                </div>
                                                <div class="row">
                                                    <div class="col-sm-12 height10">
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-sm-4">
                                                        <div class="row">
                                                            <div class="col-sm-6 labelText1 ">
                                                                Total Invoice Amount 
                                                            </div>
                                                            <div class="col-sm-6 paddingLeft5 paddingRight0">
                                                                <asp:Label ID="lblTotInvAmt" Style="text-align: right" runat="server"></asp:Label>
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-sm-12 height5">
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-sm-6 labelText1 ">
                                                                Total Revesal Amount
                                                            </div>
                                                            <div class="col-sm-6 paddingLeft5 paddingRight0">
                                                                <asp:Label ID="lblTotalRevAmt" Style="text-align: right" runat="server"></asp:Label>
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-sm-12 height5">
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-sm-6 labelText1 ">
                                                                Total Payment Amount  
                                                            </div>
                                                            <div class="col-sm-6 paddingLeft5 paddingRight0">
                                                                <asp:Label ID="lblTotPayAmt" Style="text-align: right" runat="server"></asp:Label>
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-sm-12 height5">
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-sm-6 labelText1 ">
                                                                Outstanding Amount
                                                            </div>
                                                            <div class="col-sm-6 paddingLeft5 paddingRight0">
                                                                <asp:Label ID="lblOutAmt" Style="text-align: right" runat="server"></asp:Label>
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-sm-12 height20">
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-sm-6 labelText1 ">
                                                                Total Return Amount
                                                            </div>
                                                            <div class="col-sm-6 paddingLeft5 paddingRight0">
                                                                <asp:Label ID="lblTotRetAmt" runat="server"></asp:Label>
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-sm-12 height5">
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-sm-6 labelText1 ">
                                                                Credit Amount
                                                            </div>
                                                            <div class="col-sm-6 paddingLeft5 paddingRight0">
                                                                <asp:Label ID="lblCrAmt" runat="server"></asp:Label>
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-sm-12 height5">
                                                            </div>
                                                        </div>
                                                    </div>

                                                    <div class="col-sm-8 panelscollbar height170 paddingRight0">
                                                        <asp:ScriptManagerProxy ID="ScriptManagerProxy13" runat="server"></asp:ScriptManagerProxy>
                                                        <asp:UpdatePanel ID="UpdatePanel17" runat="server">
                                                            <ContentTemplate>
                                                                <asp:GridView ID="grdPaymentDetails" CausesValidation="false" EmptyDataText="No data found..." ShowHeaderWhenEmpty="True" runat="server" GridLines="None" CssClass="table table-hover table-striped" PageSize="10" PagerStyle-CssClass="cssPager" AllowPaging="True">
                                                                    <Columns>
                                                                        <asp:TemplateField HeaderText="Ref #">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="col_recSeq" runat="server" Width="100px"></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Receipt #">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="col_recNo" runat="server" Width="100px"></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Date">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="col_recDT" runat="server" Width="100px"></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Pay Type">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="col_recPayTp" runat="server" Width="100px"></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Payment Ref">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="col_recPayRef" runat="server" Width="100px"></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Amount">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="col_recAmt" runat="server" Width="100px"></asp:Label>
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
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </asp:Panel>



    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
        <ContentTemplate>
            <asp:Button ID="Button1" runat="server" Text="Button" Style="display: none;" />
            <asp:ModalPopupExtender ID="UserRereportPopoup" runat="server" Enabled="True" TargetControlID="Button1"
                PopupControlID="pnlRereport" CancelControlID="btnRereportClose" PopupDragHandleControlID="PopupHeader" Drag="true"
                BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>

        </ContentTemplate>

    </asp:UpdatePanel>

    <asp:UpdatePanel ID="UpdatePanel18" runat="server">
        <ContentTemplate>
            <asp:Panel runat="server" ID="pnlRereport" DefaultButton="lbtnSearch">
                <div runat="server" id="Div1" class="panel panel-default height400 width950">
                    <asp:Label ID="Label1" runat="server" Text="Label" Visible="false"></asp:Label>
                    <div class="panel panel-default">

                        <div class="panel-heading">
                            <asp:LinkButton ID="btnRereportClose" runat="server">
                             <span class="glyphicon glyphicon-remove" aria-hidden="true">Close</span>
                            </asp:LinkButton>
                            <%--<span>Commen Search</span>--%>
                            <div class="col-sm-11">
                                Re-report Items

                                <asp:Label ID="lbReerrors" ForeColor="Red" runat="server"></asp:Label>
                            </div>
                            <div class="col-sm-1">
                            </div>
                        </div>
                        <div class="panel-body">
                            <asp:Label ID="lblInvLine" runat="server"></asp:Label>
                            <div class="crow">
                                <div class="col-sm-12">
                                    <div class="row">
                                        <div class="col-sm-6">
                                            <div class="row">
                                                <div class="col-sm-4 labelText1 ">
                                                    Reverse Item 
                                                </div>
                                                <div class="col-sm-8 labelText1 ">
                                                    <asp:Label ID="lblRevItem" runat="server"></asp:Label>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-12 height5">
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-4 labelText1 ">
                                                    Reported price
                                                </div>
                                                <div class="col-sm-4 labelText1 ">
                                                    <asp:Label ID="lblRepPrice" runat="server"></asp:Label>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-12 height5">
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-4 labelText1 ">
                                                    Re-Report Item
                                                </div>
                                                <div class="col-sm-4 paddingLeft5 paddingRight0">
                                                    <asp:TextBox runat="server" ID="txtItem" AutoPostBack="true" CausesValidation="false" CssClass="form-control" OnTextChanged="txtItem_TextChanged"></asp:TextBox>
                                                </div>
                                                <div class="col-sm-1 paddingLeft5 paddingRight0 ">
                                                    <asp:LinkButton ID="lbtnSearch_Item" runat="server" CausesValidation="false" OnClick="lbtnSearch_Item_Click">
                                                        <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                    </asp:LinkButton>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-12 height5">
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-4 labelText1 ">
                                                    New Price
                                                </div>
                                                <div class="col-sm-4 paddingLeft5 paddingRight0">
                                                    <asp:TextBox runat="server" onkeypress="return isNumberKey(event)" AutoPostBack="true" ID="txtNewPrice" OnTextChanged="txtNewPrice_TextChanged" CausesValidation="false" CssClass="form-control"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-sm-6">
                                            <div class="row">
                                                <div class="col-sm-3 labelText1 ">
                                                </div>
                                                <div class="col-sm-4 paddingLeft5 paddingRight0">
                                                    <asp:TextBox runat="server" Visible="false" ID="txtNewSch" CausesValidation="false" CssClass="form-control"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-12 height5">
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-3 labelText1 ">
                                                    New Serial 
                                                </div>
                                                <div class="col-sm-4 paddingLeft5 paddingRight0">
                                                    <asp:TextBox runat="server" ID="txtNewSerial" CausesValidation="false" AutoPostBack="true" CssClass="form-control" OnTextChanged="txtNewSerial_TextChanged"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-12 height5">
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-3 labelText1 ">
                                                    Qty 
                                                </div>
                                                <div class="col-sm-4 paddingLeft5 paddingRight0">
                                                    <asp:TextBox runat="server" ID="txtRQty" onkeypress="return isNumberKey(event)" AutoPostBack="true" CausesValidation="false" CssClass="form-control" OnTextChanged="txtRQty_TextChanged"></asp:TextBox>
                                                </div>
                                                <div class="col-sm-2">
                                                    <asp:LinkButton ID="lbtnAddItem" runat="server" CausesValidation="false" OnClick="lbtnAddItem_Click">
                                                <span class="glyphicon glyphicon-plus" aria-hidden="true"></span>
                                                    </asp:LinkButton>
                                                </div>
                                                <div class="col-sm-2 paddingLeft0 paddingRight0">
                                                    <asp:Button ID="btnAll" Visible="false" runat="server" CssClass="btn btn-default btn-xs" Text="Apply Same" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-sm-12 height5">
                                        </div>
                                    </div>
                                    <div class="row">

                                        <div class="col-sm-12 panelscollbar height170 paddingRight0">
                                            <asp:ScriptManagerProxy ID="ScriptManagerProxy5" runat="server"></asp:ScriptManagerProxy>
                                            <asp:UpdatePanel ID="UpdatePanel7" runat="server">
                                                <ContentTemplate>
                                                    <asp:GridView ID="grdRereportItems" CausesValidation="false" EmptyDataText="No data found..." ShowHeaderWhenEmpty="True" runat="server" GridLines="None" CssClass="table table-hover table-striped" PageSize="8" PagerStyle-CssClass="cssPager" AllowPaging="True" OnSelectedIndexChanged="grdResult_SelectedIndexChanged" OnPageIndexChanging="grdResult_PageIndexChanging">
                                                        <Columns>

                                                            <asp:TemplateField HeaderText="Reverse Item">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="col_OldItm" runat="server" Text='<%# Bind("GRAD_ANAL1") %>' Width="100px"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Re report Item">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="col_RepItem" runat="server" Text='<%# Bind("GRAD_ANAL2") %>' Width="100px"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="New Serial">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="col_RepSerial" runat="server" Text='<%# Bind("grad_anal3") %>' Width="100px"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="New Scheme" Visible="false">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="col_repScheme" runat="server" Text='<%# Bind("grad_anal5") %>' Width="100px"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="New Price">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="col_repPrice" runat="server" Text='<%# Bind("grad_anal7","{0:n}") %>' Width="100px"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Reverse Line" Visible="false">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="col_RevLine" runat="server" Text='<%# Bind("GRAD_ANAL8") %>' Width="100px"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Req. Qty">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="col_newQty" runat="server" Text='<%# Bind("grad_anal9","{0:n}") %>' Width="40px"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField Visible="true">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="lbtnRereportItemsDel" runat="server" CausesValidation="false" Width="5px" OnClientClick="DeleteConfirm()" OnClick="lbtnRereportItemsDel_Click">
                                                                        <span class="glyphicon glyphicon-trash" aria-hidden="true" ></span>
                                                                    </asp:LinkButton>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                    </asp:GridView>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-sm-12 height20">
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-sm-10 labelText1 ">
                                        </div>
                                        <div class="col-sm-2 paddingLeft0 paddingRight0">
                                            <asp:Button ID="btnReOk" runat="server" CssClass="btn btn-default btn-xs" Text="Confirm" OnClick="btnReOk_Click" />
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



    <asp:UpdatePanel ID="UpdatePanel4" runat="server">
        <ContentTemplate>
            <asp:Button ID="Button2" runat="server" Text="Button" Style="display: none;" />
            <asp:ModalPopupExtender ID="UserServiceApprovalPopoup" runat="server" Enabled="True" TargetControlID="Button2"
                PopupControlID="pnlServiceApproval" CancelControlID="btnpnlServiceApprovalClose" PopupDragHandleControlID="PopupHeader" Drag="true"
                BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>

        </ContentTemplate>

    </asp:UpdatePanel>

    <asp:UpdatePanel ID="UpdatePanel19" runat="server">
        <ContentTemplate>
            <asp:Panel runat="server" ID="pnlServiceApproval" DefaultButton="lbtnSearch">
                <div runat="server" id="Div2" class="panel panel-default height400 width700">
                    <asp:Label ID="Label2" runat="server" Text="Label" Visible="false"></asp:Label>

                    <div class="panel-heading">
                        <asp:LinkButton ID="btnpnlServiceApprovalClose" runat="server">
                             <span class="glyphicon glyphicon-remove" aria-hidden="true">Close</span>
                        </asp:LinkButton>
                        <%--<span>Commen Search</span>--%>
                        <div class="col-sm-11">
                            Service Approval
                                <asp:Label ID="lblSAerror" ForeColor="Red" runat="server" Visible="false"></asp:Label>
                        </div>
                        <div class="col-sm-1">
                        </div>
                    </div>
                    <div class="panel-body">
                        <div class="row">
                            <div class="col-sm-12">
                                <asp:Label ID="Label3" runat="server" Visible="false"></asp:Label>
                                <div class="row">
                                    <div class="col-sm-6">
                                        <div class="row">
                                            <div class="col-sm-4 labelText1 ">
                                                Item 
                                            </div>
                                            <div class="col-sm-8 labelText1 ">
                                                <asp:Label ID="lblSerItem" runat="server"></asp:Label>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-sm-12 height5">
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-sm-4 labelText1 ">
                                                Job No.
                                            </div>
                                            <div class="col-sm-4 paddingLeft5 paddingRight0">
                                                <asp:TextBox runat="server" ID="txtJobNo" CausesValidation="false" CssClass="form-control"></asp:TextBox>
                                            </div>
                                            <div class="col-sm-1 paddingLeft5 paddingRight0 ">
                                                <asp:LinkButton ID="lbtnSearchJobNo" runat="server" CausesValidation="false" OnClick="lbtnSearchJobNo_Click">
                                                        <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                </asp:LinkButton>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-sm-12 height5">
                                            </div>
                                        </div>

                                    </div>
                                    <div class="col-sm-6">
                                        <div class="row">
                                            <div class="col-sm-3 labelText1 ">
                                                Serial :
                                            </div>
                                            <div class="col-sm-4 paddingLeft5 paddingRight0">
                                                <asp:Label ID="lblSerial" runat="server"></asp:Label>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-sm-12 height5">
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-sm-3 labelText1 ">
                                                Warranty 
                                            </div>
                                            <div class="col-sm-4 paddingLeft5 paddingRight0">
                                                <asp:Label ID="lblWarranty" runat="server"></asp:Label>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-sm-12 height5">
                                            </div>
                                        </div>

                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-12 height5">
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-4 labelText1 ">
                                        Service Remarks 
                                    </div>
                                    <div class="col-sm-6 paddingLeft5 paddingRight0">
                                        <asp:Label ID="lblSerRem" runat="server"></asp:Label>
                                    </div>
                                    <div class="col-sm-2">
                                        <asp:LinkButton ID="lbtnSerAppAdd" runat="server" CausesValidation="false" OnClick="lbtnSerAppAdd_Click">
                                                <span class="glyphicon glyphicon-plus" aria-hidden="true"></span>
                                        </asp:LinkButton>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-12 height5">
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-2"></div>
                                    <div class="col-sm-11 panelscollbar height200">
                                        <asp:ScriptManagerProxy ID="ScriptManagerProxy2" runat="server"></asp:ScriptManagerProxy>
                                        <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                                            <ContentTemplate>
                                                <asp:GridView ID="grdSerApp" CausesValidation="false" EmptyDataText="No data found..." ShowHeaderWhenEmpty="True" runat="server" GridLines="None" CssClass="table table-hover table-striped" PageSize="8" PagerStyle-CssClass="cssPager" AllowPaging="True" OnSelectedIndexChanged="grdResult_SelectedIndexChanged" OnPageIndexChanging="grdResult_PageIndexChanging">
                                                    <Columns>

                                                        <asp:TemplateField HeaderText="Item">
                                                            <ItemTemplate>
                                                                <asp:Label ID="col_SerItem" runat="server" Text='<%# Bind("gras_anal2") %>' Width="100px"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Serial">
                                                            <ItemTemplate>
                                                                <asp:Label ID="col_SerSerial" runat="server" Text='<%# Bind("gras_anal3") %>' Width="100px"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="JobNo">
                                                            <ItemTemplate>
                                                                <asp:Label ID="col_JobNo" runat="server" Text='<%# Bind("gras_anal5") %>' Width="100px"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField Visible="true">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lbtnSerAppDel" OnClick="lbtnSerAppDel_Click" runat="server" OnClientClick="DeleteJobConfirm()" CausesValidation="false" Width="5px">
                                                                        <span class="glyphicon glyphicon-trash" aria-hidden="true" ></span>
                                                                </asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-12 height20">
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-10 labelText1 ">
                                    </div>
                                    <div class="col-sm-2 paddingLeft0 paddingRight0">
                                        <asp:Button ID="Button5" runat="server" CssClass="btn btn-default btn-xs" Text="Confirm" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

                </div>
            </asp:Panel>

        </ContentTemplate>

    </asp:UpdatePanel>

    <asp:UpdatePanel ID="UpdatePanel26" runat="server">
        <ContentTemplate>
            <asp:Button ID="Button8" runat="server" Text="Button" Style="display: none;" />
            <asp:ModalPopupExtender ID="userCancelPopoup" runat="server" Enabled="True" TargetControlID="Button8"
                PopupControlID="pnlCancelpopup" CancelControlID="LinkButton2" PopupDragHandleControlID="PopupHeader" Drag="true" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>

        </ContentTemplate>

    </asp:UpdatePanel>
    <asp:UpdatePanel ID="UpdatePanel27" runat="server">
        <ContentTemplate>
            <asp:Panel runat="server" ID="pnlCancelpopup" DefaultButton="lbtnSearch">
                <div runat="server" id="Div7" class="panel panel-default height60 width700">

                    <asp:Label ID="Label6" runat="server" Text="Label" Visible="false"></asp:Label>
                    <div class="panel panel-default">
                        <div class="panel-heading">
                            <asp:LinkButton ID="LinkButton2" runat="server">
                             <span class="glyphicon glyphicon-remove" aria-hidden="true">Close</span>
                            </asp:LinkButton>
                            <%--<span>Commen Search</span>--%>
                            <div class="col-sm-11">
                                <asp:Label ID="lblErrorcancel" ForeColor="Red" runat="server" Text=""></asp:Label>
                            </div>
                            <div class="col-sm-1">
                            </div>
                        </div>
                        <div class="panel-body">
                            <div class="col-sm-12" id="Div8" runat="server">
                                <div class="row">
                                    <div class="col-sm-12 height5">
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-6">
                                        <div class="row">
                                            <div class="col-sm-3 labelText1">
                                                Reversal No
                                            </div>
                                            <div class="col-sm-8 paddingRight5">
                                                <asp:TextBox ID="txtCancelInvoice" CausesValidation="false" ReadOnly="true" placeholder="Search by word" class="form-control" runat="server"></asp:TextBox>
                                            </div>
                                            <asp:ScriptManagerProxy ID="ScriptManagerProxy17" runat="server"></asp:ScriptManagerProxy>
                                            <asp:UpdatePanel ID="UpdatePanel29" runat="server">
                                                <ContentTemplate>
                                                    <div class="col-sm-1 paddingLeft0">
                                                        <asp:LinkButton ID="lbtCancelCir" runat="server" OnClick="lbtCancelCir_Click">
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
                                    <div class="col-sm-2 buttonRow">
                                        <asp:LinkButton ID="lbtnCancelProcess" runat="server" OnClientClick="CancelConfirm()" CausesValidation="false" OnClick="lbtnCancelProcess_Click">
                                            <span class="glyphicon glyphicon-saved" aria-hidden="true" ></span>Process
                                        </asp:LinkButton>
                                    </div>
                                    <div class="col-sm-2 buttonRow">
                                        <asp:LinkButton ID="lbtnCancelClear" runat="server" CausesValidation="false" OnClick="lbtnCancelClear_Click">
                                            <span class="glyphicon glyphicon-refresh" aria-hidden="true" ></span>Clear
                                        </asp:LinkButton>
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
            </asp:Panel>
        </ContentTemplate>

    </asp:UpdatePanel>


    <asp:UpdatePanel ID="UpdatePanel8" runat="server">
        <ContentTemplate>
            <asp:Button ID="Button3" runat="server" Text="Button" Style="display: none;" />
            <asp:ModalPopupExtender ID="UserPopoup" runat="server" Enabled="True" TargetControlID="Button3"
                PopupControlID="pnlpopup" PopupDragHandleControlID="PopupHeader" Drag="true" CancelControlID="btnClose"
                BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>

        </ContentTemplate>

    </asp:UpdatePanel>

    <asp:Panel runat="server" ID="pnlpopup" DefaultButton="lbtnSearch">
        <div runat="server" id="test" class="panel panel-default height400 width700">
            <asp:Label ID="lblvalue" runat="server" Text="Label" Visible="false"></asp:Label>
            <div class="panel panel-default">
                <div class="panel-heading">
                    <asp:LinkButton ID="btnClose" runat="server">
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
                            <div class="col-sm-12 paddingRight0 paddingLeft0">
                                <asp:ScriptManagerProxy ID="ScriptManagerProxy4" runat="server"></asp:ScriptManagerProxy>
                                <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                                    <ContentTemplate>
                                        <asp:GridView ID="grdResult" CausesValidation="false" EmptyDataText="No data found..." ShowHeaderWhenEmpty="True" runat="server" GridLines="None" CssClass="table table-hover table-striped" PageSize="8" PagerStyle-CssClass="cssPager" AllowPaging="True" OnSelectedIndexChanged="grdResult_SelectedIndexChanged" OnPageIndexChanging="grdResult_PageIndexChanging">
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

    <asp:UpdatePanel ID="UpdatePanel21" runat="server">
        <ContentTemplate>
            <asp:Button ID="Button7" runat="server" Text="Button" Style="display: none;" />
            <asp:ModalPopupExtender ID="UserDPopoup" runat="server" Enabled="True" TargetControlID="Button7"
                PopupControlID="pnlDpopup" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>

        </ContentTemplate>

    </asp:UpdatePanel>

    <asp:UpdatePanel ID="UpdatePanel22" runat="server">
        <ContentTemplate>
            <asp:Panel runat="server" ID="pnlDpopup" DefaultButton="lbtnSearch">
                <div runat="server" id="Div5" class="panel panel-default height400 width800">

                    <asp:Label ID="Label5" runat="server" Text="Label" Visible="false"></asp:Label>
                    <div class="panel panel-default">
                        <div class="panel-heading">

                            <asp:LinkButton ID="LinkButton1" runat="server" OnClick="btnDClose_Click">
                                            <span class="glyphicon glyphicon-remove" aria-hidden="true"  >Close</span>
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
                                            <asp:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtFDate"
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
                                            <asp:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtTDate"
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
                                        <asp:UpdatePanel ID="UpdatePanel25" runat="server">
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

    <%-- Serial Varification --%>
    <asp:UpdatePanel ID="upSerVar" runat="server">
        <ContentTemplate>
            <asp:Button ID="btnVar" runat="server" Text="Button" Style="display: none;" />
            <asp:ModalPopupExtender ID="popSerVar" runat="server" Enabled="True" TargetControlID="btnVar"
                PopupControlID="pnlSerVar" PopupDragHandleControlID="PopupHeader" Drag="true" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:Panel runat="server" ID="pnlSerVar">
        <div class="row" style="width: 900px;">
            <div class="panel panel-default">
                <div class="panel panel-heading height30">
                    <div class="col-sm-12 padding0">
                        <div class="col-sm-10">
                            <strong>Serial Verification</strong>
                        </div>
                        <div class="col-sm-1 padding0">
                            <asp:LinkButton ID="lbtnSerVarClear" runat="server" OnClick="lbtnSerVarClear_Click" OnClientClick="return ConfClear()">
                                <span class="glyphicon glyphicon-refresh" aria-hidden="true">Clear</span>
                            </asp:LinkButton>
                        </div>
                        <div class="col-sm-1">
                            <asp:LinkButton ID="lbtnSerVarClose" runat="server" OnClick="lbtnSerVarClose_Click" Style="float: right;">
                                <span class="glyphicon glyphicon-remove" aria-hidden="true">Close</span>
                            </asp:LinkButton>
                        </div>
                    </div>
                </div>
                <div class="panel panel-body">
                    <div class="row">
                        <div class="col-sm-12">
                            <asp:UpdatePanel runat="server">
                                <ContentTemplate>
                                    <asp:Label Text="" Visible="false" ForeColor="Red" Font-Bold="true" ID="lblSerVarError" runat="server" />
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-sm-12">

                            <div class="col-sm-3 labelText1">
                                Main Item Serial 1
                            </div>
                            <asp:Panel runat="server" DefaultButton="lbtnSubSerLoad">
                                <div class="col-sm-3">
                                    <asp:UpdatePanel runat="server">
                                        <ContentTemplate>
                                            <asp:TextBox runat="server" AutoPostBack="true" onblur="doPostBack(this)" ID="txtMainItmSer1" CssClass="form-control"
                                                OnTextChanged="txtMainItmSer1_TextChanged"></asp:TextBox>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                                <div class="col-sm-4">
                                    <asp:LinkButton ID="lbtnSubSerLoad" CausesValidation="false" Style="display: none;" runat="server" OnClick="txtMainItmSer1_TextChanged">
                                    </asp:LinkButton>
                                </div>
                            </asp:Panel>
                        </div>
                    </div>
                    <br />
                    <div class="row">
                        <div class="col-sm-8">
                            <div class="panel panel-default">
                                <div class="panel panel-body" style="height: 188px; overflow: auto;">
                                    <asp:UpdatePanel runat="server">
                                        <ContentTemplate>
                                            <asp:GridView ID="dgvSubSerPick" CssClass="table table-hover table-striped" runat="server" GridLines="None" PagerStyle-CssClass="cssPager"
                                                EmptyDataText="No data found..." ShowHeaderWhenEmpty="true"
                                                AutoGenerateColumns="False">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Item Code">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblItmCode" runat="server" Text='<%# Bind("Irsms_itm_cd") %>' Width="50px"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Sub Serial">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblSubSerial" runat="server" Text='<%# Bind("Irsms_sub_ser") %>' Width="50px"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="">
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="chkPickSubSer" Checked='<%#Convert.ToBoolean(Eval("SubSerIsAvailable")) %>' runat="server" Enabled="false" />
                                                        </ItemTemplate>
                                                        <HeaderStyle Width="10px" />
                                                        <ItemStyle Width="10px" />
                                                    </asp:TemplateField>
                                                </Columns>
                                                <PagerStyle CssClass="cssPager"></PagerStyle>
                                            </asp:GridView>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                            </div>
                        </div>
                        <div class="col-sm-4">
                            <asp:Panel runat="server">
                                <div class="col-sm-4 labelText1">
                                    Sub Serial 
                                </div>
                                <div class="col-sm-8">
                                    <asp:TextBox AutoPostBack="true" onblur="doPostBack(this)" OnTextChanged="txtSubSerial_TextChanged" runat="server" ID="txtSubSerial" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="col-sm-1">
                                    <asp:Button ID="btnI" runat="server" OnClick="txtSubSerial_TextChanged" Text="Submit" Style="display: none;" />
                                </div>
                            </asp:Panel>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-sm-12">
                            <div class="panel panel-default">
                                <div class="panel panel-body" style="height: 175px; overflow: auto;">
                                    <asp:UpdatePanel runat="server">
                                        <ContentTemplate>
                                            <asp:GridView ID="dgvPopSerial" EmptyDataText="No data found..." ShowHeaderWhenEmpty="True" runat="server"
                                                GridLines="None" CssClass="table table-hover table-striped" AutoGenerateColumns="false">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Location">
                                                        <ItemTemplate>
                                                            <asp:Label ID="col_loc" runat="server" Text='<%# Bind("tus_loc") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="DO #">
                                                        <ItemTemplate>
                                                            <asp:Label ID="col_doc" runat="server" Text='<%# Bind("tus_doc_no") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Invoice #">
                                                        <ItemTemplate>
                                                            <asp:Label ID="col_baseDoc" runat="server" Text='<%# Bind("tus_base_doc_no") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="DO Date">
                                                        <ItemTemplate>
                                                            <asp:Label ID="col_doDate" runat="server" Text='<%# Bind("tus_doc_dt", "{0:dd/MMM/yyyy}") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Item">
                                                        <ItemTemplate>
                                                            <asp:Label ID="col_item" runat="server" Text='<%# Bind("tus_itm_cd") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Status" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="col_itmstatus" runat="server" Text='<%# Bind("tus_itm_stus") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Status">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblStsDesc" runat="server" Text='<%# Bind("tus_itm_stus") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Status" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="col_Tus_itm_stus_Desc" runat="server" Text='<%# Bind("Tus_itm_stus_Desc") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="App Stus" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:DropDownList ID="col_appstatus" CssClass="form-control" runat="server"></asp:DropDownList>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Qty">
                                                        <ItemTemplate>
                                                            <asp:Label ID="col_Qty" runat="server" Text='<%# Bind("tus_qty","{0:n}") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Serial">
                                                        <ItemTemplate>
                                                            <asp:Label ID="col_Serial" runat="server" Text='<%# Bind("tus_ser_1") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Other serial">
                                                        <ItemTemplate>
                                                            <asp:Label ID="col_Serial1" runat="server" Text='<%# Bind("tus_ser_2") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Line" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="col_BaseLine" runat="server" Text='<%# Bind("Tus_base_itm_line") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Delete" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lbtnSerDel" runat="server" CausesValidation="false" Width="5px">
                                                <span class="glyphicon glyphicon-trash" aria-hidden="true" ></span>
                                                            </asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Serial ID" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="col_SerID" runat="server" Text='<%# Bind("Tus_ser_id") %>' Width="100px"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Warranty #" Visible="true">
                                                        <ItemTemplate>
                                                            <asp:Label ID="col_Wara" runat="server" Text='<%# Bind("Tus_warr_no") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="lineno" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="col_Tus_temp_itm_line" runat="server" Text='<%# Bind("Tus_temp_itm_line") %>' Width="100px"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="seriId" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="col_Tus_ser_id" runat="server" Text='<%# Bind("Tus_ser_id") %>' Width="100px"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="">
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="chkPickSer" Checked='<%#Convert.ToBoolean(Eval("Tus_isSelect")) %>' runat="server" Enabled="false" />
                                                        </ItemTemplate>
                                                        <HeaderStyle Width="10px" />
                                                        <ItemStyle Width="10px" />
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

    <asp:UpdatePanel ID="UpdatePanel20" runat="server">
        <ContentTemplate>
            <asp:Button ID="btnconfbox" runat="server" Text="Button" Style="display: none;" />
            <asp:ModalPopupExtender ID="PopupConfBox" runat="server" Enabled="True" TargetControlID="btnconfbox"
                PopupControlID="pnlConfBox" PopupDragHandleControlID="PopupHeader" Drag="true" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:Panel ID="pnlConfBox" runat="server" align="center">
        <div runat="server" id="Div9" class="panel panel-info height120 width250">
            <asp:Label ID="Label8" runat="server" Text="Label" Visible="false"></asp:Label>
            <asp:ScriptManagerProxy ID="ScriptManagerProxy19" runat="server"></asp:ScriptManagerProxy>
            <asp:UpdatePanel ID="UpdatePanel28" runat="server">
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
                                <asp:Button ID="Button6" runat="server" Text="yes" Width="80px" CausesValidation="false" class="btn btn-primary btn-xs" OnClick="Button1_Click" />
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
    <%-- Dulaj 2018-Oct-11 --%>
    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <asp:Button ID="Button13" runat="server" Text="btnConfex" Style="display: none;" />
            <asp:ModalPopupExtender ID="ModalPopupExtenderOutstanding" runat="server" Enabled="True" TargetControlID="Button13"
                PopupControlID="pnlConfirmationoutstanding" PopupDragHandleControlID="divCOnfOutstading" Drag="true" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>
        </ContentTemplate>
    </asp:UpdatePanel>

      <asp:Panel runat="server" ID="pnlConfirmationoutstanding" Style="display: none" Width="400px" Height="115px">
        <div runat="server" id="Div14" class="panel panel-primary">
            <asp:Label ID="Label7" runat="server" Text="Label" Visible="false"></asp:Label>
            <asp:UpdatePanel runat="server">
                <ContentTemplate>
                    <div class="panel panel-default">
                        <div class="panel-heading" id="divCOnfOutstading">
                            Confirmation
                    <asp:LinkButton ID="LinkButton4" runat="server" OnClick="btnConfClose_ClickExcel">
                             <span class="glyphicon glyphicon-remove" style="float:right" aria-hidden="true">Close</span>
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
                                <div class="col-sm-12">
                                    <asp:Label ID="Label11" Text="Invoice number has request(s).Do you want to continue?" runat="server" />
                                    <asp:HiddenField ID="HiddenField4" runat="server" />
                                    <asp:HiddenField ID="HiddenField5" runat="server" />
                                    <asp:HiddenField ID="HiddenField6" runat="server" />
                                </div>
                                <div class="col-sm-12">
                                    <div class="col-sm-3">
                                    </div>
                                    <div class="col-sm-3">
                                        <asp:Button ID="Button14" CssClass="form-control" Text="Yes" runat="server" OnClick="btnConOutfYes_Click" />
                                    </div>
                                    <div class="col-sm-3">
                                    </div>
                                    <div class="col-sm-3">
                                        <asp:Button ID="Button15" CssClass="form-control" Text="No" runat="server" OnClick="btnConfNoOut_Click" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div>                  
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </asp:Panel>

    <%--Add Udaya Serial--%>
    <asp:UpdatePanel ID="UpdatePanel30" runat="server">
        <ContentTemplate>
            <asp:Button ID="Button11" runat="server" Text="Button" Style="display: none;" />
            <asp:ModalPopupExtender ID="popUpSerial" runat="server" Enabled="True" TargetControlID="Button11"
                PopupControlID="pnlSerial" PopupDragHandleControlID="PopupHeader" Drag="true" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:Panel ID="pnlSerial" runat="server" align="center">
        <div runat="server" id="Div12" class="panel panel-info height120 width250">
            <asp:Label ID="Label10" runat="server" Text="Label" Visible="false"></asp:Label>
            <asp:ScriptManagerProxy ID="ScriptManagerProxy16" runat="server"></asp:ScriptManagerProxy>
            <asp:UpdatePanel ID="UpdatePanel31" runat="server">
                <ContentTemplate>

                    <div class="panel panel-heading">
                        <span>Alert</span>
                    </div>
                    <div class="panel-body">
                        <div class="row">
                            <div class="col-sm-12">
                                <asp:Label ID="lblMssgSer" runat="server"></asp:Label>
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
                                <asp:Button ID="btnSerialYes" runat="server" Text="yes" Width="80px" CausesValidation="false" class="btn btn-primary btn-xs" OnClick="btnSerialYes_Click" />
                            </div>
                            <div class="col-sm-2 ">
                                <asp:Button ID="btnSerialNo" runat="server" Text="No" Width="80px" CausesValidation="false" class="btn btn-primary btn-xs" OnClick="btnSerialNo_Click" />
                            </div>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </asp:Panel>

    <%--Add Udaya Inv Print--%>
    <asp:UpdatePanel ID="UpdatePanel32" runat="server">
        <ContentTemplate>
            <asp:Button ID="Button12" runat="server" Text="Button" Style="display: none;" />
            <asp:ModalPopupExtender ID="popupInv" runat="server" Enabled="True" TargetControlID="Button12"
                PopupControlID="pnlInv" PopupDragHandleControlID="PopupHeader" Drag="true" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:Panel ID="pnlInv" runat="server" align="center">
        <div runat="server" id="Div13" class="panel panel-info height120 width250">
            <asp:Label ID="Label17" runat="server" Text="Label" Visible="false"></asp:Label>
            <asp:ScriptManagerProxy ID="ScriptManagerProxy18" runat="server"></asp:ScriptManagerProxy>
            <asp:UpdatePanel ID="UpdatePanel33" runat="server">
                <ContentTemplate>

                    <div class="panel panel-heading">
                        <span>Alert</span>
                    </div>
                    <div class="panel-body">
                        <div class="row">
                            <div class="col-sm-12">
                                <asp:Label ID="lblMssgInv" runat="server"></asp:Label>
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
                            <div class="col-sm-12">
                                <asp:Label ID="Label21" runat="server"></asp:Label>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-12">
                                <asp:Label ID="Label22" runat="server"></asp:Label>
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
                                <asp:Button ID="btnInvYes" runat="server" Text="yes" Width="80px" CausesValidation="false" class="btn btn-primary btn-xs" OnClick="btnInvYes_Click" />
                            </div>
                            <div class="col-sm-2 ">
                                <asp:Button ID="btnInvNo" runat="server" Text="No" Width="80px" CausesValidation="false" class="btn btn-primary btn-xs" OnClick="btnInvNo_Click" />
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
                <div runat="server" id="Div10" class="panel panel-default height150 width525">
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

                                                <div class="col-sm-5 labelText1">
                                                    Location
                                                </div>
                                                <div class="col-sm-7">
                                                    <asp:TextBox ID="txtpdaloc" runat="server" TabIndex="1" CssClass="form-control"></asp:TextBox>
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
                                                </div>
                                                <div class="col-sm-7">
                                                    Multiple user
                                                     <asp:CheckBox ID="chkmultiuser" runat="server" Checked="false"></asp:CheckBox>
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

    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <asp:Button ID="Button10" runat="server" Text="Button" Style="display: none;" />
            <asp:ModalPopupExtender ID="MPMULTIINVOICE" runat="server" Enabled="True" TargetControlID="Button10"
                PopupControlID="INVOICEPanel" PopupDragHandleControlID="PopupHeader" CancelControlID="LinkButton3" Drag="true" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdatePanel runat="server">
        <ContentTemplate>

            <asp:Panel runat="server" ID="INVOICEPanel">
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
                        <div class="panel-body">
                            <div class="col-sm-12 paddingLeft0 paddingRight0">

                                <div class="row">

                                    <div class="col-sm-12">

                                        <div class="row">
                                            <div class="col-sm-12 height10">
                                            </div>
                                        </div>

                                        <div class="row">
                                            <div class="col-sm-12">
                                                <div class="panel panel-default">
                                                    <div class="panel panel-body" style="height: 175px; overflow: auto;">
                                                        <asp:UpdatePanel runat="server">
                                                            <ContentTemplate>
                                                                <asp:GridView ID="grdinvoice" EmptyDataText="No data found..." ShowHeaderWhenEmpty="True" runat="server"
                                                                    GridLines="None" CssClass="table table-hover table-striped" AutoGenerateColumns="false">
                                                                    <Columns>

                                                                        <asp:TemplateField HeaderText="Invoice #">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="col_Invoice" runat="server" Visible="false" Text='<%# Bind("Invoiceno") %>'></asp:Label>
                                                                                <asp:LinkButton Text='<%# Bind("Invoiceno") %>' ID="SelectReqItem" runat="server" OnClick="SelectReqItem_Click" />
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="GRNA">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="col_GRNA" runat="server" Text='<%# Bind("GRNA") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="seq" Visible="false">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="col_SEQ" runat="server" Text='<%# Bind("SEQ") %>'></asp:Label>
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
            </asp:Panel>

        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
