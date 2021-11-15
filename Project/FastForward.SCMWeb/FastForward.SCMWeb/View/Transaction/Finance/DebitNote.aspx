<%@ Page Title="" Language="C#" MasterPageFile="~/View/AdminSite.Master" AutoEventWireup="true" CodeBehind="DebitNote.aspx.cs" Inherits="FastForward.SCMWeb.View.Transaction.Finance.DebitNote" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
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

        function keyDownItem(e) {
            var evt = (e) ? e : window.event;
            var key = (evt.keyCode) ? evt.keyCode : evt.which;
            if (key == 113) {
                openItemSearch();
            }
        };

        function openItemSearch() {
            var bnItm = document.getElementById('<% =btnSearch_Item.ClientID %>');
            bnItm.focus();
            document.getElementById('<% = btnSearch_Item.ClientID %>').click();
        };

    </script>
    <script>
        function ClearConfirm() {
            var selectedvalue = confirm("Do you want to clear data?");
            if (selectedvalue) {
                document.getElementById('<%=txtClearlconformmessageValue.ClientID %>').value = "Yes";
            } else {
                document.getElementById('<%=txtClearlconformmessageValue.ClientID %>').value = "No";
            }
        };
        function SaveConfirm() {
            var selectedvalue = confirm("Do you want to save ?");
            if (selectedvalue) {
                document.getElementById('<%=txtSavelconformmessageValue.ClientID %>').value = "Yes";
            } else {
                document.getElementById('<%=txtSavelconformmessageValue.ClientID %>').value = "No";
            }
        };
        function DeleteConfirm() {

            var selectedvalue = confirm("Do you want to delete item ?");
            if (selectedvalue) {
                document.getElementById('<%=txtDeleteconformmessageValue.ClientID %>').value = "Yes";
            } else {
                document.getElementById('<%=txtDeleteconformmessageValue.ClientID %>').value = "No";
            }
        };
        function CancelConfirm() {

            var selectedvalue = confirm("Do you want to cancel item ?");
            if (selectedvalue) {
                document.getElementById('<%=hdfCancel.ClientID %>').value = "Yes";
            } else {
                document.getElementById('<%=hdfCancel.ClientID %>').value = "No";
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
      <asp:UpdatePanel ID="UpdatePanel14" runat="server">
        <ContentTemplate>
            <asp:Button ID="Button4" runat="server" Text="Button" Style="display: none;" />
            <asp:ModalPopupExtender ID="excelUpload" runat="server" Enabled="True" TargetControlID="Button4"
                PopupControlID="pnlexcel" PopupDragHandleControlID="PopupHeader" Drag="true" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdateProgress ID="UpdateProgress1" DisplayAfter="10"
        runat="server" AssociatedUpdatePanelID="UpdatePanel2">
        <ProgressTemplate>
            <div class="divWaiting">
                <asp:Label ID="lblWait" runat="server"
                    Text="Please wait... " />
                <asp:Image ID="imgWait" runat="server"
                    ImageAlign="Middle" ImageUrl="~/images/icons/ajax-loader.gif" />
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <div class="panel panel-default marginLeftRight5">
        <div class="panel-body">
            <div class="tab-pane" id="creditNote">
                <asp:HiddenField ID="txtClearlconformmessageValue" runat="server" />
                <asp:HiddenField ID="txtSavelconformmessageValue" runat="server" />
                <asp:HiddenField ID="txtDeleteconformmessageValue" runat="server" />
                <asp:HiddenField ID="hdnprint" runat="server" />
                <asp:HiddenField ID="hdfCancel" runat="server" />
                <asp:ScriptManagerProxy ID="ScriptManagerProxy2" runat="server"></asp:ScriptManagerProxy>
                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                    <ContentTemplate>
                        <div class="row">
                            <div class="col-sm-12 ">
                                <div class="panel panel-default">
                                    <div class="panel panel-heading">
                                        <strong><b>Debit Note</b></strong>
                                    </div>
                                    <div class="panel-body">
                                        <div class="row">
                                            <div class="col-sm-6  buttonrow">
                                                <div id="WarnningBin" visible="false" runat="server" class="alert alert-danger alert-dismissible" role="alert">

                                                    <div class="col-sm-11">
                                                        <strong>Alert!</strong>
                                                        <asp:Label ID="lblWarnninglBin" runat="server"></asp:Label>
                                                    </div>
                                                    <div class="col-sm-1">
                                                        <asp:LinkButton ID="LinkButton8" runat="server" CausesValidation="false" CssClass="floatright">
                                        <span class="glyphicon glyphicon-ok" aria-hidden="true"></span>
                                                        </asp:LinkButton>
                                                    </div>
                                                </div>
                                                <div id="SuccessBin" runat="server" visible="false" class="alert alert-success alert-dismissible" role="alert">
                                                    <div class="col-sm-11">
                                                        <strong>Success!</strong>
                                                        <asp:Label ID="lblSuccessBin" runat="server"></asp:Label>

                                                    </div>
                                                    <div class="col-sm-1">
                                                        <asp:LinkButton ID="LinkButton9" runat="server" CausesValidation="false" CssClass="floatright">
                                        <span class="glyphicon glyphicon-ok" aria-hidden="true"></span>
                                                        </asp:LinkButton>
                                                    </div>

                                                </div>
                                                <div id="Div3" runat="server" visible="false" class="alert alert-info alert-dismissible" role="alert">
                                                    <strong>Alert!</strong>
                                                    <asp:Label ID="Label3" runat="server"></asp:Label>
                                                </div>
                                                <div id="Information" runat="server" visible="false" class="alert alert-success alert-info" role="alert">
                                                    <div class="col-sm-11">
                                                        <strong>Info!</strong>
                                                        <asp:Label ID="lblBackDateInfor" runat="server"></asp:Label>
                                                        <asp:Label ID="lblH1" runat="server" Text="Label"></asp:Label>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-sm-6  buttonRow">
                                                <div class="col-sm-3 paddingRight0">
                                                    <asp:LinkButton ID="lbtnSave" CausesValidation="false" runat="server" CssClass="floatRight" OnClientClick="SaveConfirm()" OnClick="lbtnSave_Click">
                                                    <span class="glyphicon glyphicon-saved" aria-hidden="true"></span>Save
                                                    </asp:LinkButton>
                                                </div>
                                                <div class="col-sm-2">
                                                    <asp:LinkButton ID="lbtnCancel" runat="server" CssClass="floatRight" OnClick="lbtnCancel_Click" OnClientClick="CancelConfirm();">
                                                    <span class="glyphicon glyphicon-remove-circle" aria-hidden="true"></span>Cancel
                                                    </asp:LinkButton>
                                                </div>
                                                <div class="col-sm-2">
                                                    <asp:LinkButton ID="lbtnClear" runat="server" CssClass="floatRight" OnClientClick="ClearConfirm()" OnClick="lbtnClear_Click">
                                                    <span class="glyphicon glyphicon-refresh" aria-hidden="true"></span>Clear
                                                    </asp:LinkButton>
                                                </div>
                                                <div class="col-sm-2">
                                                    <asp:LinkButton ID="lblPrint" runat="server" CssClass="floatRight" OnClientClick="ConfirmPrint()" OnClick="lblPrint_Click">
                                                    <span class="glyphicon glyphicon-print" aria-hidden="true"></span>Print
                                                    </asp:LinkButton>
                                                </div>
                                                  <div class="col-sm-3">
                                                    <asp:LinkButton  ID="lbtnExcelUpload" runat="server" OnClick="lbtnExcelUpload_Click">
                                                                    <span class="glyphicon glyphicon-upload" aria-hidden="true"></span>Excel Upload
                                                    </asp:LinkButton>
                                                </div>
                                            </div>
                                        </div>


                                        <div class="row">
                                            <div class="col-sm-12 ">
                                                <div class="panel panel-default">
                                                    <div class="panel-body">
                                                        <div class="panel panel-default" style="background-color: skyblue">
                                                            <div class="panel-body">
                                                                <div class="row">
                                                                    <div class="col-sm-12">
                                                                        <div class="col-sm-1 labelText1" style="width: 103px">
                                                                            Debit Note # :
                                                                        </div>
                                                                        <div class="col-sm-1 paddingLeft0 paddingRight0">
                                                                            <asp:TextBox TabIndex="1" runat="server" ReadOnly="true" ID="txtDbtNte" CausesValidation="false" CssClass="form-control" Width="145px"></asp:TextBox>
                                                                        </div>
                                                                        <div class="col-sm-1 paddingLeft0 Lwidth" style="padding-left:40px">
                                                                            <asp:LinkButton ID="lbtnDbtNte" runat="server" CausesValidation="false" OnClick="lbtnDbtNte_Click">
                                                                        <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                                            </asp:LinkButton>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-sm-12">
                                                                <div class="col-sm-1 labelText1">
                                                                    Invoice # :
                                                                </div>
                                                                <div class="col-sm-1 paddingLeft0 paddingRight0">
                                                                    <asp:TextBox runat="server" ReadOnly="true" ID="txtInvoice" CausesValidation="false" TabIndex="2" CssClass="form-control"></asp:TextBox>
                                                                </div>
                                                                <div class="col-sm-1 paddingLeft5 Lwidth">
                                                                    <asp:LinkButton ID="lbtnInvoice" runat="server" CausesValidation="false" OnClick="lbtnInvoice_Click">
                                                                    <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                                    </asp:LinkButton>
                                                                </div>
                                                                <span id="DivWithInv">
                                                                    <div class="col-sm-1 labelText1">
                                                                        Invoice Type :
                                                                    </div>
                                                                    <div class="col-sm-1 paddingLeft0 paddingRight0">
                                                                        <asp:DropDownList Visible="false" ID="cmbInvType" runat="server" AutoPostBack="true" TabIndex="3" CssClass="form-control"></asp:DropDownList>
                                                                        <asp:Label ID="lblInvType" runat="server"></asp:Label>
                                                                    </div>
                                                                </span>
                                                                <div class="col-sm-1 labelText1">
                                                                    Customer :
                                                                </div>
                                                                <div class="col-sm-1 paddingLeft0 paddingRight0">
                                                                    <asp:TextBox TabIndex="4" runat="server" ID="txtCus" CausesValidation="false" CssClass="form-control" OnTextChanged="txtCus_TextChanged"></asp:TextBox>
                                                                </div>
                                                                <div class="col-sm-1 paddingLeft5 Lwidth">
                                                                    <asp:LinkButton ID="lbtnCustomer" runat="server" CausesValidation="false" OnClick="lbtnCustomer_Click">
                                                                    <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                                    </asp:LinkButton>
                                                                </div>
                                                                <div class="col-sm-1 labelText1">
                                                                    Tax Exempt :
                                                                </div>
                                                                <div class="col-sm-1 paddingLeft0 paddingRight0">
                                                                    <asp:Label ID="lblVatExemptStatus" runat="server" Text=""></asp:Label>
                                                                </div>
                                                                <%--<div class="col-sm-1 labelText1 ">
                                                                    Debit Note # :
                                                                </div>
                                                                <div class="col-sm-1 paddingLeft0 paddingRight0">
                                                                    <asp:TextBox placeHolder="Auto Generate No" ReadOnly="true" runat="server" ID="txtCreNote" CausesValidation="false" CssClass="form-control"></asp:TextBox>
                                                                </div>
                                                                <div class="col-sm-1 paddingLeft5 Lwidth">
                                                                    <asp:LinkButton ID="lbtnDebitNoteNo" runat="server" CausesValidation="false" OnClick="lbtnDebitNoteNo_Click">
                                                        <span class="glyphicon glyphicon-bell" aria-hidden="true"></span>
                                                                    </asp:LinkButton>
                                                                </div>--%>
                                                                <div class="col-sm-2 paddingLeft5 Lwidth"></div>
                                                                <div class="col-sm-1 labelText1">
                                                                    Date :
                                                                </div>
                                                                <div class="col-sm-1 paddingLeft0 paddingRight0">
                                                                    <asp:TextBox ReadOnly="true" TabIndex="5" runat="server" Enabled="true" ID="txtDate" CausesValidation="false" CssClass="form-control" OnTextChanged="txtDate_TextChanged"></asp:TextBox>
                                                                </div>
                                                                <div class="col-sm-2 paddingLeft5 Lwidth">
                                                                    <asp:LinkButton ID="lbtnDate" Visible="true" runat="server" CausesValidation="false">
                                                            <span class="glyphicon glyphicon-calendar" aria-hidden="true"></span>
                                                                    </asp:LinkButton>
                                                                    <asp:CalendarExtender ID="CalendarExtender3" runat="server" TargetControlID="txtDate"
                                                                        PopupButtonID="lbtnDate" Format="dd/MMM/yyyy">
                                                                    </asp:CalendarExtender>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-sm-12">
                                                                <div class="col-sm-1 labelText1">
                                                                    Sales Ex :
                                                                </div>
                                                                <div class="col-sm-1 paddingLeft0 paddingRight0">
                                                                    <asp:TextBox TabIndex="6" runat="server" ID="txtExecutive" CausesValidation="false" CssClass="form-control" OnTextChanged="txtExecutive_TextChanged"></asp:TextBox>
                                                                </div>
                                                                <div class="col-sm-1 paddingLeft5 Lwidth">
                                                                    <asp:LinkButton ID="lbtnEx" runat="server" CausesValidation="false" OnClick="lbtnEx_Click">
                                                        <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                                    </asp:LinkButton>
                                                                </div>

                                                                <span style="padding-left: 180px">
                                                                    <div class="col-sm-1 labelText1">
                                                                        Total Amount :
                                                                    </div>
                                                                    <div class="col-sm-1 paddingLeft0 paddingRight0">
                                                                        <asp:TextBox TabIndex="7" Text="0.00" onkeydown="return jsDecimals(event);" Style="text-align: right" runat="server" ReadOnly="true" ID="txtTotAmt" CausesValidation="false" CssClass="form-control"></asp:TextBox>
                                                                    </div>
                                                                    <div class="col-sm-1 labelText1">
                                                                        Paid Amount :
                                                                    </div>
                                                                    <div class="col-sm-1 paddingLeft0 paddingRight0">
                                                                        <asp:TextBox TabIndex="8" Text="0.00" onkeydown="return jsDecimals(event);" Style="text-align: right" runat="server" ReadOnly="true" ID="txtPaidAmt" CausesValidation="false" CssClass="form-control"></asp:TextBox>
                                                                    </div>
                                                                    <div class="col-sm-2 paddingLeft5 Lwidth"></div>
                                                                    <div class="col-sm-1 labelText1">
                                                                        Balance Amount :
                                                                    </div>
                                                                    <div class="col-sm-1 paddingLeft0 paddingRight0">
                                                                        <asp:TextBox TabIndex="9" Text="0.00" onkeydown="return jsDecimals(event);" Style="text-align: right" runat="server" ReadOnly="true" ID="txtBalAmt" CausesValidation="false" CssClass="form-control"></asp:TextBox>
                                                                    </div>
                                                                </span>
                                                            </div>
                                                        </div>

                                                    </div>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="row" id="creNote">
                                            <div class="col-sm-12 ">
                                                <div class="panel panel-default">
                                                    <div class="panel-body">
                                                        <div class="row">
                                                            <div class="col-sm-12">
                                                                <div class="col-sm-1 labelText1 ">
                                                                    Item :
                                                                </div>
                                                                <div class="col-sm-1 paddingLeft0 paddingRight0">
                                                                    <asp:TextBox ID="txtItem" TabIndex="10" runat="server" AutoPostBack="true" Style="text-transform: uppercase" CssClass="form-control" onKeydown="keyDownItem(event)" OnTextChanged="txtItem_TextChanged"></asp:TextBox>
                                                                </div>
                                                                <div class="col-sm-1 paddingLeft5 Lwidth">
                                                                    <asp:LinkButton ID="btnSearch_Item" runat="server" CausesValidation="false" OnClick="btnSearch_Item_Click">
                                                        <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                                    </asp:LinkButton>
                                                                </div>
                                                                <div class="col-sm-1 labelText1 ">
                                                                    Description :
                                                                </div>
                                                                <div class="col-sm-1 paddingLeft0 paddingRight0">
                                                                    <asp:TextBox ReadOnly="true" ID="txtDescription" runat="server" TabIndex="11" Style="text-align: right" AutoPostBack="true" CssClass="form-control"></asp:TextBox>
                                                                </div>
                                                                <div class="col-sm-1 labelText1 ">
                                                                    Qty :
                                                                </div>
                                                                <div class="col-sm-1 paddingLeft0 paddingRight0">
                                                                    <asp:TextBox MaxLength="10" Text="0.00" ID="txtQty" runat="server" TabIndex="12" onkeydown="return jsDecimals(event);" Style="text-align: right" AutoPostBack="true" CssClass="form-control" OnTextChanged="txtQty_TextChanged"></asp:TextBox>
                                                                </div>
                                                                <div class="col-sm-1 paddingLeft5 Lwidth" style="visibility: hidden">
                                                                </div>
                                                                <div class="col-sm-1 labelText1">
                                                                    Unit Price :
                                                                </div>
                                                                <div class="col-sm-1 paddingLeft0 paddingRight0">
                                                                    <asp:TextBox MaxLength="10" Text="0.00" ID="txtUnitPrice" runat="server" TabIndex="13" AutoPostBack="true" Style="text-align: right" onkeydown="return jsDecimals(event);" CssClass="form-control" OnTextChanged="txtUnitPrice_TextChanged"></asp:TextBox>
                                                                </div>
                                                                <div class="col-sm-2 paddingLeft5 Lwidth"></div>
                                                                <div class="col-sm-1 labelText1">
                                                                    Unit Amount :
                                                                </div>
                                                                <div class="col-sm-1 paddingLeft0 paddingRight0">
                                                                    <asp:TextBox Text="0.00" ID="txtUnitAmt" runat="server" TabIndex="14" ReadOnly="true" onkeydown="return jsDecimals(event);" Style="text-align: right" CssClass="form-control"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-sm-12">
                                                                <div class="col-sm-1 labelText1">
                                                                    Dis Rate % :
                                                                </div>
                                                                <div class="col-sm-1 paddingLeft0 paddingRight0">
                                                                    <asp:TextBox MaxLength="10" Text="0.00" ID="txtDisRate" runat="server" TabIndex="15" onkeydown="return jsDecimals(event);" Style="text-align: right" CssClass="form-control" AutoPostBack="true" OnTextChanged="txtDisRate_TextChanged"></asp:TextBox>
                                                                </div>
                                                                <div class="col-sm-1 paddingLeft5 Lwidth" style="visibility: hidden">
                                                                </div>
                                                                <div class="col-sm-1 labelText1">
                                                                    Dis Amount :
                                                                </div>
                                                                <div class="col-sm-1 paddingLeft0 paddingRight0">
                                                                    <asp:TextBox Text="0.00" ID="txtDisAmt" runat="server" ReadOnly="true" TabIndex="16" onkeydown="return jsDecimals(event);" Style="text-align: right" CssClass="form-control" AutoPostBack="true"></asp:TextBox>
                                                                </div>

                                                                <div class="col-sm-1 labelText1">
                                                                    Tax Amount :
                                                                </div>
                                                                <div class="col-sm-1 paddingLeft0 paddingRight0">
                                                                    <asp:TextBox Text="0.00" ID="txtTaxAmt" ReadOnly="true" runat="server" TabIndex="17" onkeydown="return jsDecimals(event);" Style="text-align: right" CssClass="form-control"></asp:TextBox>
                                                                </div>
                                                                <div class="col-sm-1 paddingLeft5 Lwidth">
                                                                </div>
                                                                <div class="col-sm-1 labelText1">
                                                                    Line Amount :
                                                                </div>
                                                                <div class="col-sm-1 paddingLeft0 paddingRight0">
                                                                    <asp:TextBox Text="0.00" ID="txtLineTotAmt" ReadOnly="true" runat="server" TabIndex="18" onkeydown="return jsDecimals(event);" Style="text-align: right" CssClass="form-control"></asp:TextBox>
                                                                </div>
                                                                <div class="col-sm-1 paddingLeft5 Lwidth">
                                                                </div>
                                                                <div class="col-sm-2 padding0" style="padding-left: 15px; width: 75px">
                                                                    <asp:LinkButton ID="lbtnadditems" runat="server" CausesValidation="false" OnClick="lbtnadditems_Click">
                                                                    <span class="glyphicon glyphicon-plus" style="font-size:20px;"  aria-hidden="true"></span>
                                                                    </asp:LinkButton>
                                                                </div>
                                                                <div class="col-sm-2 padding0">
                                                                    <asp:LinkButton ID="lbtnRefreshDtl" runat="server" CausesValidation="false" OnClick="lbtnRefreshDtl_Click">
                                                                    <span class="glyphicon glyphicon-refresh" style="font-size:20px;"  aria-hidden="true"></span>
                                                                    </asp:LinkButton>
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
                                                    <div class=" panel-body height200 panelscollbar">
                                                        <asp:GridView ID="grdInvoices" EmptyDataText="No data found..." ShowHeaderWhenEmpty="True" runat="server" GridLines="Both" CssClass="table table-hover table-striped" AutoGenerateColumns="false" OnSelectedIndexChanged="grdInvoices_SelectedIndexChanged">
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="">
                                                                    <ItemTemplate>
                                                                        <asp:LinkButton ID="lbtnDetaltecost" CausesValidation="false" CommandName="Delete" runat="server" OnClientClick="DeleteConfirm()" OnClick="lbtnDetaltecost_Click">
                                                                            <span class="glyphicon glyphicon-trash" aria-hidden="true" style="font-size:15px"></span> 
                                                                        </asp:LinkButton>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Item" Visible="true">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="col_Item" runat="server" Text='<%# Bind("Item") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <ItemStyle CssClass="gridHeaderAlignCenter" />
                                                                    <HeaderStyle CssClass="gridHeaderAlignCenter" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Description" Visible="true">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="col_Description" runat="server" Text='<%# Bind("Description") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <ItemStyle CssClass="gridHeaderAlignCenter" />
                                                                    <HeaderStyle CssClass="gridHeaderAlignCenter" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Qty" Visible="true">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="col_Qty" runat="server" Text='<%# Bind("Qty","{0:n}") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <ItemStyle CssClass="gridHeaderAlignRight" Width="80px" />
                                                                    <HeaderStyle CssClass="gridHeaderAlignCenter" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Unit Price" Visible="true">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="col_UnitPrice" runat="server" Text='<%# Bind("UnitPrice","{0:n}") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <ItemStyle CssClass="gridHeaderAlignRight" Width="80px" />
                                                                    <HeaderStyle CssClass="gridHeaderAlignCenter" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Unit Amount" Visible="true">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="col_Total" runat="server" Text='<%# Bind("Total","{0:n}") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <ItemStyle CssClass="gridHeaderAlignRight" Width="80px" />
                                                                    <HeaderStyle CssClass="gridHeaderAlignCenter" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Discount Rate %" Visible="true">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="col_DisRate" runat="server" Text='<%# Bind("DisRate","{0:n}") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <ItemStyle CssClass="gridHeaderAlignRight" Width="80px" />
                                                                    <HeaderStyle CssClass="gridHeaderAlignCenter" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Discount Amount" Visible="true">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="col_DisAmt" runat="server" Text='<%# Bind("DisAmt","{0:n}") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <ItemStyle CssClass="gridHeaderAlignRight" Width="80px" />
                                                                    <HeaderStyle CssClass="gridHeaderAlignCenter" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Tax Amount" Visible="true">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="col_TaxAmt" runat="server" Text='<%# Bind("Tax","{0:n}") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <ItemStyle CssClass="gridHeaderAlignRight" Width="80px" />
                                                                    <HeaderStyle CssClass="gridHeaderAlignCenter" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Line Amount" Visible="true">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="col_LineAmt" runat="server" Text='<%# Bind("LineAmt","{0:n}") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <ItemStyle CssClass="gridHeaderAlignRight" Width="80px" />
                                                                    <HeaderStyle CssClass="gridHeaderAlignCenter" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="CusCode" Visible="false">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="col_CusCode" runat="server" Text='<%# Bind("CusCode") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="SalesExCode" Visible="false">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="col_SalesExCode" runat="server" Text='<%# Bind("SalesExCode") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>


                                                                <asp:TemplateField HeaderText="CreditNoteNo" Visible="false">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="col_DebitNoteNo" runat="server" Text='<%# Bind("DebitNoteNo") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Date" Visible="false">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="col_Date" runat="server" Text='<%# Bind("Date") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="InvoiceNo" Visible="false">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="col_InvoiceNo" runat="server" Text='<%# Bind("InvoiceNo") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="InvoiceType" Visible="false">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="col_InvoiceType" runat="server" Text='<%# Bind("InvoiceType") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="InvAmt" Visible="false">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="col_InvAmt" runat="server" Text='<%# Bind("InvAmt") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="InvAmtPaid" Visible="false">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="col_InvAmtPaid" runat="server" Text='<%# Bind("InvAmtPaid") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="InvAmtBal" Visible="false">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="col_InvAmtBal" runat="server" Text='<%# Bind("InvAmtBal") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="CusName" Visible="false">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="col_CusName" runat="server" Text='<%# Bind("CusName") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="CusAdd1" Visible="false">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="col_CusAdd1" runat="server" Text='<%# Bind("CusAdd1") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="CusAdd2" Visible="false">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="col_CusAdd2" runat="server" Text='<%# Bind("CusAdd2") %>'></asp:Label>
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
                                                <div class="col-sm-1 paddingLeft0 paddingRight0">
                                                </div>
                                                <div class="col-sm-1 paddingLeft0 paddingRight0">
                                                </div>
                                                <div class="col-sm-1 paddingLeft0 paddingRight0">
                                                </div>
                                                <div class="col-sm-1 paddingLeft0 paddingRight0">
                                                </div>
                                                <div class="col-sm-1 paddingLeft0 paddingRight0">
                                                </div>
                                                <div class="col-sm-1 paddingLeft0 paddingRight0">
                                                </div>
                                                <div class="col-sm-1 paddingLeft0 paddingRight0">
                                                </div>
                                                <div class="col-sm-1 paddingLeft0 paddingRight0">
                                                </div>
                                                <div class="col-sm-1 paddingLeft0 paddingRight0">
                                                    <strong><b>
                                                        <asp:Label ID="lblTot" runat="server" Text="Total :"></asp:Label>
                                                    </b>
                                                    </strong>
                                                </div>
                                                <div class="col-sm-1 paddingLeft0 paddingRight0" style="padding-left: 100px">
                                                    <asp:Label ID="lbldisAmt" runat="server"></asp:Label>
                                                </div>
                                                <div class="col-sm-1 paddingLeft0 paddingRight0" style="padding-left: 70px">
                                                    <asp:Label ID="lbltaxAmt" runat="server"></asp:Label>
                                                </div>
                                                <div class="col-sm-1 paddingLeft0 paddingRight0" style="padding-left: 35px">
                                                    <asp:Label ID="lblLineAmt" runat="server"></asp:Label>
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
    </div>

    <asp:UpdateProgress ID="UpdateProgress2" DisplayAfter="10"
        runat="server" AssociatedUpdatePanelID="comSearch">
        <ProgressTemplate>
            <div class="divWaiting">
                <asp:Label ID="lblWaitcomS" runat="server"
                    Text="Please wait... " />
                <asp:Image ID="imgWaitcomS" runat="server"
                    ImageAlign="Middle" ImageUrl="~/images/icons/ajax-loader.gif" />
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>

    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <asp:Button ID="Button3" runat="server" Text="Button" Style="display: none;" />
            <asp:ModalPopupExtender ID="SIPopup" runat="server" TargetControlID="Button3"
                PopupControlID="pnlpopup" PopupDragHandleControlID="divSearchheader" Drag="true" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>
        </ContentTemplate>
    </asp:UpdatePanel>

    <asp:UpdatePanel runat="server" ID="comSearch">
        <ContentTemplate>
            <asp:Panel runat="server" ID="pnlpopup" DefaultButton="lbtnSearch">
                <asp:ScriptManagerProxy ID="ScriptManagerProxy17" runat="server"></asp:ScriptManagerProxy>

                <div runat="server" id="test" class="panel panel-default height400 width850">
                    <asp:Label ID="lblvalue" runat="server" Text="Label" Visible="false"></asp:Label>
                    <div class="panel panel-default">
                        <div class="panel-heading height30" id="divSearchheader">
                            <div class="col-sm-11">
                            </div>
                            <div class="col-sm-1">
                                <asp:LinkButton ID="btnCloseSearchMP" runat="server" OnClick="btnCloseSearchMP_Click">
                             <span class="glyphicon glyphicon-remove" aria-hidden="true">Close</span>
                                </asp:LinkButton>
                            </div>
                        </div>
                        <div class="panel-body">
                            <div class="col-sm-12" id="Div10" runat="server">
                                <div class="row">
                                    <div class="col-sm-12">
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-12 height5">
                                    </div>
                                </div>
                                <asp:Panel runat="server" ID="pnlsearch" Visible="true">
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
                                </asp:Panel>
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
                                                <asp:GridView ID="grdResult" CausesValidation="false" AllowPaging="true" runat="server" GridLines="None" CssClass="table table-hover table-striped" PageSize="10" PagerStyle-CssClass="cssPager" OnSelectedIndexChanged="grdResult_SelectedIndexChanged" OnPageIndexChanging="grdResult_PageIndexChanging">
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
                </div>
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>

    <asp:UpdatePanel ID="UpdatePanel20" runat="server">
        <ContentTemplate>
            <asp:Button ID="Button2" runat="server" Text="Button" Style="display: none;" />
            <asp:ModalPopupExtender ID="UserDPopoup" runat="server" Enabled="True" TargetControlID="Button2"
                PopupControlID="pnlDpopup" PopupDragHandleControlID="PopupHeader" Drag="true" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>

        </ContentTemplate>

    </asp:UpdatePanel>

    <asp:UpdatePanel ID="UpdatePanel21" runat="server">
        <ContentTemplate>
            <asp:Panel runat="server" ID="pnlDpopup" DefaultButton="lbtnSearch">
                <div runat="server" id="Div20" class="panel panel-default height400 width1085">

                    <asp:Label ID="Label12" runat="server" Text="Label" Visible="false"></asp:Label>
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
                            <div class="col-sm-12" id="Div21" runat="server">
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
                                        <div class="col-sm-1 paddingLeft0" id="srndateDiv">
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
                                        <asp:ScriptManagerProxy ID="ScriptManagerProxy18" runat="server"></asp:ScriptManagerProxy>
                                        <asp:UpdatePanel ID="UpdatePanel22" runat="server">
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
                                        <asp:ScriptManagerProxy ID="ScriptManagerProxy19" runat="server"></asp:ScriptManagerProxy>
                                        <asp:UpdatePanel ID="UpdatePanel23" runat="server">
                                            <ContentTemplate>
                                                <div class="col-sm-1 paddingLeft0">
                                                    <asp:LinkButton ID="lbtnSearchD" runat="server" OnClick="lbtnSearchD_Click">
                                                        <span class="glyphicon glyphicon-search" aria-hidden="true"  ></span>
                                                    </asp:LinkButton>
                                                </div>
                                            </ContentTemplate>

                                            <Triggers>
                                                <asp:AsyncPostBackTrigger ControlID="txtSearchbywordD" />
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
                                        <asp:ScriptManagerProxy ID="ScriptManagerProxy20" runat="server"></asp:ScriptManagerProxy>
                                        <asp:UpdatePanel ID="UpdatePanel24" runat="server">
                                            <ContentTemplate>
                                                <asp:GridView ID="grdResultD" CausesValidation="false" runat="server" EmptyDataText="No data found..." ShowHeaderWhenEmpty="True" GridLines="None" CssClass="table table-hover table-striped" PageSize="10" PagerStyle-CssClass="cssPager" AllowPaging="True" OnSelectedIndexChanged="grdResultD_SelectedIndexChanged" OnPageIndexChanging="grdResultD_PageIndexChanging">
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

                </div>
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>

    <asp:UpdatePanel ID="UpdatePanel6" runat="server">
        <ContentTemplate>
            <asp:Button ID="btnPrintPnl" runat="server" Text="Button" Style="display: none;" />
            <asp:ModalPopupExtender ID="popUpPrint" runat="server" Enabled="True" TargetControlID="btnPrintPnl"
                PopupControlID="pnlPrint" PopupDragHandleControlID="PopupHeader" Drag="true" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>
        </ContentTemplate>
    </asp:UpdatePanel>

    <asp:Panel ID="pnlPrint" runat="server" align="center">
        <div runat="server" id="Div8" class="panel panel-info height120 width250">
            <asp:Label ID="Label2" runat="server" Text="Label" Visible="false"></asp:Label>
            <asp:ScriptManagerProxy ID="ScriptManagerProxy1" runat="server"></asp:ScriptManagerProxy>
            <asp:UpdatePanel ID="UpdatePanel8" runat="server">
                <ContentTemplate>
                    <div class="panel panel-heading">
                        <span>Alert</span>
                    </div>
                    <div class="panel-body">
                        <div class="row">
                            <div class="col-sm-12">
                                <asp:Label ID="Label1" runat="server"></asp:Label>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-12">
                                <asp:Label ID="Label7" runat="server"></asp:Label>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-12">
                                <asp:Label ID="Label8" runat="server"></asp:Label>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-12">
                                <asp:Label ID="Label11" runat="server"></asp:Label>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-12">
                                <asp:Label ID="Label4" runat="server"></asp:Label>
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
                                <asp:Button ID="lbtnPrintOk" runat="server" Text="yes" Width="80px" CausesValidation="false" class="btn btn-primary btn-xs" OnClick="lbtnPrintOk_Click" />
                            </div>
                            <div class="col-sm-2 ">
                                <asp:Button ID="linkPrintNo" runat="server" Text="No" Width="80px" CausesValidation="false" class="btn btn-primary btn-xs" OnClick="linkPrintNo_Click" />
                            </div>
                        </div>
                    </div>

                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </asp:Panel>
    <asp:Panel runat="server" ID="pnlexcel">
        <div runat="server" id="Div1" class="panel panel-default height45 width700 ">
            <div class="panel panel-default">
                <div class="panel-heading">
                    <asp:LinkButton ID="btnClose3" runat="server" OnClick="btnClose3_Click">
                             <span class="glyphicon glyphicon-remove" aria-hidden="true">Close</span>
                    </asp:LinkButton>
                    <div class="col-sm-11">
                        <asp:Label ID="lblalert" runat="server" ForeColor="Red" Text="Label" Visible="false"></asp:Label>
                        <asp:Label ID="lblsuccess" runat="server" ForeColor="Green" Visible="false"></asp:Label>
                    </div>
                    <div class="col-sm-1">
                    </div>
                </div>
                <div class="panel-body">
                    <div class="col-sm-12 paddingLeft0 paddingRight0">
                        <div class="row">
                            <div class="col-sm-12">
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-12 height5">
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-12" id="Div2" runat="server">
                                <div class="col-sm-5 paddingRight5">
                                    <asp:FileUpload ID="fileupexcelupload" runat="server" />
                                </div>
                                <div class="col-sm-2 paddingRight5">
                                    <asp:Button ID="btnUpload" class="btn btn-warning btn-xs" runat="server" Text="Upload"
                                        OnClick="btnUpload_Click" />
                                </div>
                            </div>
                            <div class="col-sm-12 ">
                                <div id="divUpcompleted" visible="false" runat="server" class="alert alert-info alert-success" role="alert">
                                                                       <asp:ScriptManagerProxy ID="ScriptManagerProxy3" runat="server"></asp:ScriptManagerProxy>
            <asp:UpdatePanel ID="UpdatePanelexcel" runat="server">
                <ContentTemplate>
                                    <div class="col-sm-9">
                                        <strong>Alert!</strong>
                                        <asp:Label ID="Label5" Text="Excel file upload completed. Do you want to process?" runat="server"></asp:Label>
                                    </div>
                                    <div class="col-sm-3">
                        
                    <asp:Button ID="btnprocess" class="btn btn-success btn-xs" runat="server" Text="Process" OnClick="btnprocess_Click" />
              
                                        
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
    </asp:Panel>
         <asp:UpdateProgress ID="UpdateProgress3" DisplayAfter="10"
        runat="server" AssociatedUpdatePanelID="UpdatePanelexcel">
        <ProgressTemplate>
            <div class="divWaiting">
                <asp:Label ID="lblWaitexcel" runat="server"
                    Text="Please wait... " />
                <asp:Image ID="imgWaitexcel" runat="server"
                    ImageAlign="Middle" ImageUrl="~/images/icons/ajax-loader.gif" />
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
</asp:Content>
