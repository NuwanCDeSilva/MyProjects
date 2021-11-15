<%@ Page Title="" Language="C#" MasterPageFile="~/View/AdminSite.Master" AutoEventWireup="true" CodeBehind="ItemSplitting.aspx.cs" Inherits="FastForward.SCMWeb.View.Transaction.Inventory.ItemSplitting" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script src="<%=Request.ApplicationPath%>Js/jquery.min.js"></script>
    <script type="text/javascript">

        function ConfirmPrint() {
            var selectedvalueOrd = confirm("Do you want to print ?");
            if (selectedvalueOrd) {
                return true;
            } else {
                return false;
            }
        };
        function ConfDel() {
            var selectedvalueOrd = confirm("Are you sure do you want to remove this serial ?");
            if (selectedvalueOrd) {
                return true;
            } else {
                return false;
            }
        };

        function ConfirmClearForm() {
            var selectedvalueclear = confirm("Do you want to clear all details ?");
            if (selectedvalueclear) {
                document.getElementById('<%=txtconfirmclear.ClientID %>').value = "Yes";
            } else {
                document.getElementById('<%=txtconfirmclear.ClientID %>').value = "No";
            }
        };

        function ConfirmSave() {
            var selectedvalsave = confirm("Do you want to save ?");
            if (selectedvalsave) {
                document.getElementById('<%=txtsaveconfirm.ClientID %>').value = "Yes";
            } else {
                document.getElementById('<%=txtsaveconfirm.ClientID %>').value = "No";
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

        function showSuccessToast() {
            if (jQuery('.toast-item-wrapper') != null) {
                jQuery('.toast-item-wrapper').remove();
            }
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
            if (jQuery('.toast-item-wrapper') != null) {
                jQuery('.toast-item-wrapper').remove();
            }
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
            if (jQuery('.toast-item-wrapper') != null) {
                jQuery('.toast-item-wrapper').remove();
            }
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
            if (jQuery('.toast-item-wrapper') != null) {
                jQuery('.toast-item-wrapper').remove();
            }
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

        function Enable() {
            return;
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
        }

        function Check(textBox, maxLength) {
            if (textBox.value.length > maxLength) {
                alert("Maximum characters allowed are " + maxLength);
                textBox.value = textBox.value.substr(0, maxLength);
            }
        };

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

    <style>
        .panel {
            margin-bottom: 0px;
            margin-top: 0px;
            padding-bottom: 2px;
            padding-top: 2px;
        }

        .panel-body {
            margin-bottom: 0px;
            margin-top: 0px;
            padding-bottom: 2px;
            padding-top: 2px;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" runat="server">

    <asp:UpdateProgress ID="UpdateProgress1" DisplayAfter="10"
        runat="server" AssociatedUpdatePanelID="UpdatePanelMain">

        <ProgressTemplate>
            <div class="divWaiting">
                <asp:Label ID="lblWait" runat="server"
                    Text="Please wait... " />
                <asp:Image ID="imgWait" runat="server"
                    ImageAlign="Middle" ImageUrl="~/images/icons/ajax-loader.gif" />
            </div>
        </ProgressTemplate>

    </asp:UpdateProgress>

    <asp:UpdatePanel ID="UpdatePanelMain" runat="server">
        <ContentTemplate>

            <asp:HiddenField ID="txtconfirmclear" runat="server" />
            <asp:HiddenField ID="txtsaveconfirm" runat="server" />
            <asp:HiddenField ID="txtconfirmdelete" runat="server" />

            <div class="panel panel-default marginLeftRight5">
                <asp:HiddenField runat="server" ID="hdfCostTp" Value="Cost" />
                <div class="row">
                    <div class="col-sm-12" style="height: 28px;">
                        <div class="col-sm-8 ">
                            <div id="Information" runat="server" visible="false" class="alert alert-success alert-info" role="alert">
                                <div class="col-sm-11">
                                    <strong>Info!</strong>
                                    <asp:Label ID="lblBackDateInfor" runat="server"></asp:Label>
                                    <asp:Label ID="lblH1" runat="server" Text="Label"></asp:Label>
                                </div>
                            </div>
                        </div>

                        <div class="col-sm-4  buttonRow floatRight padding0 paddingtopbottom0">
                            <div class="col-sm-3 paddingRight0">
                                <asp:LinkButton ID="btnSave" CausesValidation="false" runat="server" CssClass="floatRight" OnClientClick="ConfirmSave();" OnClick="btnSave_Click">
                                        <span class="glyphicon glyphicon-saved" aria-hidden="true"></span>Save
                                </asp:LinkButton>
                            </div>
                            <div class="col-sm-3 paddingRight0">
                                <asp:LinkButton ID="lbtnPrint" CausesValidation="false" runat="server" CssClass="floatRight" OnClientClick="return ConfirmPrint();" OnClick="lbtnPrint_Click">
                                        <span class="glyphicon glyphicon-print" aria-hidden="true"></span>Print
                                </asp:LinkButton>
                            </div>
                            <div class="col-sm-3">
                                <asp:LinkButton ID="btnClear" runat="server" CssClass="floatRight" OnClientClick="ConfirmClearForm();" OnClick="btnClear_Click">
                                        <span class="glyphicon glyphicon-refresh" aria-hidden="true"></span>Clear
                                </asp:LinkButton>
                            </div>
                            <div class="col-sm-3 paddingRight0">
                                <asp:LinkButton ID="lbtnBPrint" CausesValidation="false" runat="server" CssClass="floatRight" OnClick="lbtnBPrint_Click">
                                        <span class="glyphicon glyphicon-print" aria-hidden="true"></span>Print BarCode
                                </asp:LinkButton>
                            </div>
                        </div>
                    </div>
                </div>


                <div class="row">

                    <div class="panel-body">

                        <div class="col-sm-12">

                            <div class="panel panel-default">

                                <div class="panel-heading pannelheading " style="margin-bottom: 6px;">
                                    <strong>
                                        <asp:Label runat="server" ID="lblHeding"></asp:Label>
                                    </strong>
                                </div>
                                <div class="panel-body" id="panelbodydiv">
                                    <div class="col-sm-4">
                                        <div class="row">
                                            <div class="col-sm-2 labelText1">
                                                Type
                                            </div>

                                            <div>
                                                <div class="col-sm-4">
                                                    <asp:DropDownList ID="ddlsplittype" CssClass="form-control" AutoPostBack="true" TabIndex="1" runat="server" OnSelectedIndexChanged="ddlsplittype_SelectedIndexChanged">
                                                        <asp:ListItem Value="0">Select</asp:ListItem>
                                                        <asp:ListItem Value="A">Accessories</asp:ListItem>
                                                        <asp:ListItem Value="M">Main Item</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>

                                        </div>

                                    </div>

                                    <div class="col-sm-5">
                                        <asp:Panel runat="server" ID="pnlDocSer">
                                            <div class="row">
                                                <div class="col-sm-8">
                                                    <div class="col-sm-3 labelText1 padding0">
                                                        Document #
                                                    </div>
                                                    <div class="col-sm-6 paddingRight0">
                                                        <asp:TextBox ID="txtDocNo" AutoPostBack="true" OnTextChanged="txtDocNo_TextChanged" runat="server" CssClass="form-control"></asp:TextBox>
                                                    </div>

                                                    <div class="col-sm-1 paddingLeft3">
                                                        <asp:LinkButton ID="lbtnSeDoc" OnClick="lbtnSeDoc_Click" CausesValidation="false" runat="server">
                                                        <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                        </asp:LinkButton>
                                                    </div>
                                                </div>
                                            </div>
                                        </asp:Panel>

                                    </div>

                                    <div class="col-sm-3">
                                        <div class="row">

                                            <div class="col-sm-4 labelText1">
                                                <div style="margin-left: 65px">
                                                    Date
                                                </div>
                                            </div>

                                            <div>
                                                <div class="col-sm-7">
                                                    <asp:TextBox ID="dtpFromDate" runat="server" CssClass="form-control" Format="dd/MMM/yyyy"
                                                        onkeypress="return RestrictSpace()" Enabled="false"></asp:TextBox>
                                                    <asp:CalendarExtender ID="calexdate" runat="server" TargetControlID="dtpFromDate"
                                                        PopupButtonID="lbtnassmentdate" Format="dd/MMM/yyyy">
                                                    </asp:CalendarExtender>
                                                </div>

                                                <div id="caldvd" class="col-sm-1 paddingLeft0">
                                                    <asp:LinkButton ID="lbtndate" TabIndex="7" CausesValidation="false" runat="server" Visible="false">
                                                        <span class="glyphicon glyphicon-calendar" aria-hidden="true"></span>
                                                    </asp:LinkButton>
                                                </div>

                                            </div>

                                        </div>

                                    </div>

                                    <div class="row">
                                        <div class="col-sm-12 height5">
                                        </div>
                                    </div>

                                    <div class="col-sm-12" style="padding-left: 1px; padding-right: 1px; margin-top: 6px;">

                                        <div class="panel panel-default">

                                            <div class="panel-heading" style="height: 22px;">
                                                General Details
                                            </div>

                                            <div class="panel-body" style="margin-top: 3px;">

                                                <div>

                                                    <div class="row">

                                                        <div class="col-sm-3">

                                                            <div class="row">

                                                                <div class="col-sm-3 labelText1">
                                                                    Main Item
                                                                </div>

                                                                <div class="col-sm-8 paddingRight5">
                                                                    <asp:TextBox ID="txtItem" TabIndex="2" runat="server" CssClass="form-control" AutoPostBack="true" OnTextChanged="txtItem_TextChanged"></asp:TextBox>
                                                                </div>

                                                                <div class="col-sm-1 paddingLeft0">
                                                                    <asp:LinkButton ID="btnsrch_Item" runat="server" OnClick="btnsrch_Item_Click">
                                                                                <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                                    </asp:LinkButton>
                                                                </div>

                                                            </div>

                                                        </div>

                                                        <div class="col-sm-3">

                                                            <div class="row">

                                                                <div class="col-sm-3 labelText1">
                                                                    Serial #
                                                                </div>

                                                                <div class="col-sm-8 paddingRight5">
                                                                    <asp:TextBox ID="txtSer" TabIndex="3" runat="server" CssClass="form-control" AutoPostBack="true"
                                                                        OnTextChanged="txtSer_TextChanged"></asp:TextBox>
                                                                </div>

                                                                <div class="col-sm-1 paddingLeft0">
                                                                    <asp:LinkButton ID="btnsrch_ser" runat="server" OnClick="btnsrch_ser_Click">
                                                                                <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                                    </asp:LinkButton>
                                                                </div>

                                                            </div>

                                                        </div>

                                                        <div class="col-sm-3">

                                                            <div class="row">

                                                                <div class="col-sm-3 labelText1">
                                                                    Description
                                                                </div>

                                                                <div class="col-sm-8 paddingRight5">
                                                                    <asp:TextBox ID="txtItemDesn" TabIndex="4" ReadOnly="true" runat="server" CssClass="form-control"></asp:TextBox>
                                                                </div>

                                                            </div>

                                                        </div>

                                                        <div class="col-sm-3">

                                                            <div class="row">

                                                                <div class="col-sm-3 labelText1">
                                                                    Warranty #
                                                                </div>

                                                                <div class="col-sm-8 paddingRight5">
                                                                    <asp:TextBox ID="txtWarr" TabIndex="5" runat="server" ReadOnly="true" CssClass="form-control"></asp:TextBox>
                                                                </div>

                                                            </div>

                                                        </div>

                                                    </div>

                                                    <div class="row">
                                                        <div class="col-sm-12 height5">
                                                        </div>
                                                    </div>

                                                    <div class="row">

                                                        <div class="col-sm-3">

                                                            <div class="row">

                                                                <div class="col-sm-3 labelText1">
                                                                    Model
                                                                </div>

                                                                <div class="col-sm-8 paddingRight5">
                                                                    <asp:TextBox ID="txtModel" TabIndex="6" runat="server" ReadOnly="true" CssClass="form-control"></asp:TextBox>
                                                                </div>

                                                            </div>

                                                        </div>

                                                        <div class="col-sm-3">

                                                            <div class="row">

                                                                <div class="col-sm-3 labelText1">
                                                                    Brand
                                                                </div>

                                                                <div class="col-sm-8 paddingRight5">
                                                                    <asp:TextBox ID="txtBrand" TabIndex="7" runat="server" ReadOnly="true" CssClass="form-control"></asp:TextBox>
                                                                </div>

                                                            </div>

                                                        </div>

                                                        <div class="col-sm-3">

                                                            <div class="row">

                                                                <div class="col-sm-3 labelText1">
                                                                    Manual Ref
                                                                </div>

                                                                <div class="col-sm-8 paddingRight5">
                                                                    <asp:TextBox ID="txtManual" TabIndex="8" runat="server" CssClass="form-control"></asp:TextBox>
                                                                </div>

                                                            </div>

                                                        </div>

                                                        <div class="col-sm-3">

                                                            <div class="row">

                                                                <div class="col-sm-3 labelText1">
                                                                    Remarks
                                                                </div>

                                                                <div class="col-sm-8 paddingRight5">
                                                                    <asp:TextBox ID="txtRem" TabIndex="9" runat="server" CssClass="form-control"></asp:TextBox>
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
                                                                    Technician
                                                                </div>

                                                                <div class="col-sm-8 paddingRight5">
                                                                    <asp:TextBox ID="txtTech" TabIndex="10" runat="server" CssClass="form-control" AutoPostBack="true" OnTextChanged="txtTech_TextChanged"></asp:TextBox>
                                                                </div>

                                                                <div class="col-sm-1 paddingLeft0">
                                                                    <asp:LinkButton ID="lbtnTech" runat="server" OnClick="lbtnTech_Click">
                                                                                <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                                    </asp:LinkButton>
                                                                </div>

                                                            </div>

                                                        </div>

                                                        <div class="col-sm-3">
                                                            <div class="row">
                                                                <asp:Label runat="server" ID="lbltechName"></asp:Label>
                                                            </div>
                                                        </div>

                                                        <div class="row">
                                                            <div class="col-sm-12 height5">
                                                            </div>
                                                        </div>

                                                        <div class="col-sm-4">

                                                            <div class="row">

                                                                <div class="col-sm-3 labelText1">
                                                                    <strong>Item Status :</strong>
                                                                </div>
                                                                <div class="col-sm-3 stockinwardlabels" runat="server" visible="false">
                                                                    <asp:Label ID="lblStus" CssClass="labelText1" runat="server"></asp:Label>
                                                                </div>
                                                                <div class="col-sm-5 stockinwardlabels">
                                                                    <asp:Label ID="lblstustext" CssClass="labelText1" ForeColor="Green" runat="server"></asp:Label>
                                                                </div>

                                                            </div>

                                                        </div>

                                                        <div class="col-sm-4">

                                                            <div class="row">

                                                                <div class="col-sm-4 labelText1">
                                                                    <strong>Free Quantity :</strong>
                                                                </div>
                                                                <div class="col-sm-8 stockinwardlabels">
                                                                    <asp:Label ID="lblFree" CssClass="labelText1" runat="server"></asp:Label>
                                                                </div>

                                                            </div>

                                                        </div>

                                                        <div class="col-sm-4">

                                                            <div class="row">

                                                                <div class="col-sm-4 labelText1">
                                                                    <strong>Reserved Quantity :</strong>
                                                                </div>
                                                                <div class="col-sm-8 stockinwardlabels">
                                                                    <asp:Label ID="lblRes" CssClass="labelText1" runat="server"></asp:Label>
                                                                </div>

                                                            </div>

                                                        </div>


                                                    </div>

                                                    <div class="row">

                                                        <div class="panel-body" style="margin-top: 6px;">

                                                            <div class="col-sm-12">

                                                                <div class="panel panel-default">

                                                                    <div class="panel-heading pannelheading " style="height: 26px;">
                                                                        <div class="col-sm-12">
                                                                            <div class="col-sm-6">
                                                                                Item Component Details
                                                                            </div>
                                                                            <div class="col-sm-6">
                                                                                <div class="col-sm-6">
                                                                                </div>
                                                                                <div class="col-sm-3 labelText1 text-right">
                                                                                    Serial status
                                                                                </div>
                                                                                <div class="col-sm-3">
                                                                                    <asp:DropDownList ID="ddlSerialized" CssClass="form-control" AutoPostBack="true" runat="server"
                                                                                        OnSelectedIndexChanged="ddlSerialized_SelectedIndexChanged">
                                                                                        <asp:ListItem Value="0">--All--</asp:ListItem>
                                                                                        <asp:ListItem Value="Serialized">Serialized</asp:ListItem>
                                                                                        <asp:ListItem Value="NONSerialized">Non-Serialized</asp:ListItem>
                                                                                    </asp:DropDownList>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                    <div class="panel-body">
                                                                        <div class="row">
                                                                            <div class="col-sm-12">
                                                                                <div class="" style="height: 240px; overflow-y: auto;">
                                                                                    <asp:GridView ID="dgvSelect" AutoGenerateColumns="false" runat="server" CssClass="table table-hover table-striped" GridLines="None" ShowHeaderWhenEmpty="True" EmptyDataText="No data found..." OnRowDataBound="dgvSelect_RowDataBound" OnRowUpdating="dgvSelect_RowUpdating">

                                                                                        <Columns>

                                                                                            <asp:CommandField ShowSelectButton="true" ButtonType="Link" SelectText="" />
                                                                                            <asp:TemplateField>
                                                                                                <ItemTemplate>
                                                                                                    <asp:LinkButton Width="10px" ID="lbtngrdInvoiceDetailsDelete" OnClientClick="return ConfDel();" CausesValidation="false" runat="server" OnClick="lbtngrdInvoiceDetailsDelete_Click">
                                                                                                    <span class="glyphicon glyphicon-trash" aria-hidden="true"></span>
                                                                                                    </asp:LinkButton>
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>
                                                                                            <asp:TemplateField ShowHeader="false">
                                                                                                <ItemTemplate>
                                                                                                    <asp:LinkButton Visible='<%# !Eval("MIKC_ITM_TYPE").ToString().Equals("M")%>' Width="10px" ID="lbtngrdInvoiceDetailsEdit" CausesValidation="false" runat="server" OnClick="lbtngrdInvoiceDetailsEdit_Click">
                                                                                                    <span class="glyphicon glyphicon-pencil" aria-hidden="true"></span>
                                                                                                    </asp:LinkButton>
                                                                                                </ItemTemplate>
                                                                                                <EditItemTemplate>
                                                                                                    <asp:LinkButton Visible='<%# !Eval("MIKC_ITM_TYPE").ToString().Equals("M")%>' ID="lbtngrdInvoiceDetailsUpdate" runat="server" CausesValidation="false" CommandName="Update"
                                                                                                        OnClick="lbtngrdInvoiceDetailsUpdate_Click">
                                                                                                    <span class="glyphicon glyphicon-ok" aria-hidden="true"></span>
                                                                                                    </asp:LinkButton>
                                                                                                    <asp:LinkButton ID="lbtngrdInvoiceDetailsCancel" runat="server" CausesValidation="false" CommandName="Cancel" OnClick="lbtngrdInvoiceDetailsCancel_Click">
                                                                                                    <span class="glyphicon glyphicon-remove" aria-hidden="true"></span>
                                                                                                    </asp:LinkButton>
                                                                                                </EditItemTemplate>
                                                                                                <ItemStyle HorizontalAlign="Center" Width="3%" />
                                                                                            </asp:TemplateField>

                                                                                            <asp:TemplateField>
                                                                                                <ItemTemplate>
                                                                                                    <asp:LinkButton ID="lbtngrdInvoiceDetailsDalete" runat="server" CausesValidation="false" Visible="false" OnClientClick="ConfirmDelete();" OnClick="lbtngrdInvoiceDetailsDalete_Click">
                                                                                                    <span aria-hidden="true" class="glyphicon glyphicon-trash"></span>
                                                                                                    </asp:LinkButton>
                                                                                                </ItemTemplate>

                                                                                            </asp:TemplateField>

                                                                                            <asp:TemplateField HeaderText="Item Code">
                                                                                                <ItemTemplate>
                                                                                                    <asp:Label ID="lblitemcode" runat="server" Text='<%# Bind("MIKC_ITM_CODE_COMPONENT") %>'></asp:Label>
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>
                                                                                            <asp:TemplateField HeaderText="Description">
                                                                                                <ItemTemplate>
                                                                                                    <asp:Label ID="lbldesc" runat="server" Text='<%# Bind("MIKC_DESC_COMPONENT") %>'></asp:Label>
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>

                                                                                            <asp:TemplateField HeaderText="Item Status" Visible="true">
                                                                                                <ItemTemplate>
                                                                                                    <asp:LinkButton ID="lbtnstus" runat="server" Text='<%# Bind("MIKC_STATUS") %>' OnClick="lbtnstus_Click"></asp:LinkButton>
                                                                                                    <asp:Label ID="lblChStus" Visible="false" runat="server" Text='<%# Bind("MIKC_STATUS_CD") %>'></asp:Label>
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>

                                                                                            <asp:TemplateField HeaderText="MFC" Visible="false">
                                                                                                <ItemTemplate>
                                                                                                    <%-- <asp:Label ID="lblmfc" Width="30px" runat="server" Text='<%# Bind("irsms_mfc") %>'></asp:Label>--%>
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>



                                                                                            <%--  <asp:TemplateField HeaderText="Item Status Value" Visible="false">
                                                                                                <ItemTemplate>
                                                                                                    <asp:Label ID="lblstus" runat="server" Text='<%# Bind("irsms_itm_stus") %>'></asp:Label>
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>--%>

                                                                                            <%--                                                                                            <asp:TemplateField HeaderText="Current Status Code">--%>
                                                                                            <%-- <asp:TemplateField HeaderText="Current Status Code" Visible="false">
                                                                                                <ItemTemplate>
                                                                                                    <asp:Label ID="lblcurrstusvalue" Visible="false" runat="server" Text='<%# Bind("irsms_itm_stus") %>'></asp:Label>
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>--%>

                                                                                            <%--  <asp:TemplateField HeaderText="Current Status" Visible="false">
                                                                                                <ItemTemplate>
                                                                                                    <asp:Label ID="lblcurstusval" runat="server" Text='<%# Bind("irsms_itm_stus") %>'></asp:Label>
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>--%>

                                                                                            <%--<asp:TemplateField HeaderText="Change Status Code">--%>
                                                                                            <%-- <asp:TemplateField HeaderText="Change Status Code" Visible="false">
                                                                                                <ItemTemplate>
                                                                                                    <asp:LinkButton ID="lbtnstus" runat="server" Text='<%# Bind("Irsms_itm_ch_stus") %>' OnClick="lbtnstus_Click"></asp:LinkButton>
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>--%>


                                                                                              <asp:TemplateField HeaderText="No units #">
                                                                                            <%--    <EditItemTemplate>
                                                                                                    <asp:TextBox ID="txtnoofunits"  CssClass="txteditser form-control" runat="server" Text='<%# Bind("MIKC_NO_OF_UNIT") %>'></asp:TextBox>
                                                                                                </EditItemTemplate>--%>
                                                                                                <ItemTemplate>
                                                                                                    <asp:Panel runat="server" DefaultButton="lbtnAddSer">
                                                                                                        <asp:TextBox ID="txtnoofunitsid" Enabled='<%# !Eval("MIKC_ALLOW_EDIT").ToString().Equals("0")%>' CssClass="txteditser form-control" runat="server" Text='<%# Bind("MIKC_NO_OF_UNIT") %>'></asp:TextBox>
                                                                                                        <asp:LinkButton  Text="" Style="display: none;" runat="server" />
                                                                                                    </asp:Panel>
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>
                                                                                            <asp:TemplateField>
                                                                                                <ItemTemplate>
                                                                                                    <asp:Label Text="" Width="20px" runat="server" />
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>
                                                                                            <asp:TemplateField HeaderText="Serial #">
                                                                                                <EditItemTemplate>
                                                                                                    <asp:TextBox ID="txteditser" Enabled='<%# !Eval("MIKC_ITM_TYPE").ToString().Equals("M")%>' CssClass="txteditser form-control" runat="server" Text='<%# Bind("MIKC_SERIAL_NO") %>'></asp:TextBox>
                                                                                                </EditItemTemplate>
                                                                                                <ItemTemplate>
                                                                                                    <asp:Panel runat="server" DefaultButton="lbtnAddSer">
                                                                                                        <asp:TextBox Enabled='<%# !Eval("MIKC_ITM_TYPE").ToString().Equals("M")%>' ID="txtSerId" CssClass="txteditser form-control" runat="server" Text='<%# Bind("MIKC_SERIAL_NO") %>'></asp:TextBox>
                                                                                                        <asp:LinkButton Enabled='<%# !Eval("MIKC_ITM_TYPE").ToString().Equals("M")%>' Text="" Style="display: none;" ID="lbtnAddSer" OnClick="lbtnAddSer_Click" runat="server" />
                                                                                                    </asp:Panel>
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>
                                                                                            <asp:TemplateField>
                                                                                                <HeaderTemplate>
                                                                                                    <asp:Label runat="server" ID="lclCostHed" Text='Cost' CssClass="text-right"></asp:Label>
                                                                                                </HeaderTemplate>
                                                                                                <ItemTemplate>
                                                                                                    <div class="oprefnotxtbox">
                                                                                                        <asp:Label ID="lblcost" runat="server" Text='<%# Bind("MIKC_COST","{0:N2}") %>' Width="75px"></asp:Label>
                                                                                                    </div>
                                                                                                </ItemTemplate>
                                                                                                <ItemStyle CssClass="gridHeaderAlignRight" />
                                                                                                <HeaderStyle CssClass="gridHeaderAlignRight" />
                                                                                            </asp:TemplateField>
                                                                                            <asp:TemplateField>
                                                                                                <ItemTemplate>
                                                                                                    <asp:Label Text="" Width="20px" runat="server" />
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>
                                                                                            <asp:TemplateField HeaderText="Location">
                                                                                                <ItemTemplate>
                                                                                                    <asp:LinkButton ID="lbtnshcngeloc" runat="server" ToolTip='<%# Bind("MIKC_LOC_DES") %>' Text='<%# Bind("MIKC_LOC") %>' OnClick="lbtnshcngeloc_Click"></asp:LinkButton>
                                                                                                    <%--<asp:Label ID="lblloc" runat="server" Text='<%# Bind("MIKC_LOC") %>'></asp:Label>--%>
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>

                                                                                            <%-- <asp:TemplateField HeaderText="Change Location" Visible="false">
                                                                                                <ItemTemplate>
                                                                                                    <asp:LinkButton ID="lbtnshcngeloc" runat="server" Text='<%# Bind("Irsms_loc_chg") %>' OnClick="lbtnshcngeloc_Click"></asp:LinkButton>
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>--%>

                                                                                            <asp:TemplateField HeaderText="LineNo" Visible="false">
                                                                                                <ItemTemplate>
                                                                                                    <asp:Label ID="lblLineNo" runat="server" Text='<%# Bind("MIKC_LINE") %>'></asp:Label>
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>

                                                                                            <asp:TemplateField HeaderText="Bin">
                                                                                                <ItemTemplate>
                                                                                                    <asp:Label ID="lblbincode" runat="server" Text='<%# Eval("MIKC_LOC") %>' Visible="false" />
                                                                                                    <asp:DropDownList ID="ddlbincode" runat="server" CssClass="form-control" Width="80px">
                                                                                                    </asp:DropDownList>
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
                                        <asp:GridView ID="grdResult" CausesValidation="false" AllowPaging="true" runat="server"
                                            ShowHeaderWhenEmpty="true" EmptyDataText="No data found ."
                                            GridLines="None" CssClass="table table-hover table-striped" PageSize="7"
                                            PagerStyle-CssClass="cssPager" OnPageIndexChanging="grdResult_PageIndexChanging"
                                            OnSelectedIndexChanged="grdResult_SelectedIndexChanged">
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
            <asp:Button ID="btnexcel" runat="server" Text="Button" Style="display: none;" />
            <asp:ModalPopupExtender ID="mpexcel" runat="server" Enabled="True" TargetControlID="btnexcel"
                PopupControlID="pnlpopupExcel" CancelControlID="btnClose" PopupDragHandleControlID="PopupHeaderExcel" Drag="true" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>

        </ContentTemplate>
    </asp:UpdatePanel>

    <asp:Panel runat="server" ID="pnlpopupExcel" DefaultButton="lbtnSearch">
        <div runat="server" id="Div1" class="panel panel-primary Mheight">

            <asp:Label ID="Label3" runat="server" Text="Label" Visible="false"></asp:Label>
            <div class="panel panel-default">
                <div class="panel-heading">
                    <asp:LinkButton ID="LinkButton8" runat="server">
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

                            <asp:UpdatePanel runat="server">
                                <ContentTemplate>
                                    <asp:Panel runat="server" ID="pnlpopupExcelss">

                                        <div class="col-sm-12">

                                            <div class="panel panel-default">

                                                <div class="panel-heading">
                                                    <strong>
                                                        <asp:Label ID="lblcaption" runat="server"></asp:Label>
                                                    </strong>
                                                </div>

                                                <div class="panel-body">

                                                    <div class="row">

                                                        <div id="dvitm" runat="server" visible="false">
                                                            <div class="col-sm-12">

                                                                <div class="panelscoll">

                                                                    <asp:GridView ID="dgvItem" AutoGenerateColumns="false" runat="server" CssClass="table table-hover table-striped" GridLines="None" ShowHeaderWhenEmpty="True" EmptyDataText="No data found..." OnSelectedIndexChanged="gvitems_SelectedIndexChanged">

                                                                        <Columns>

                                                                            <asp:CommandField ShowSelectButton="true" ButtonType="Link" />

                                                                            <asp:TemplateField HeaderText="Item Code">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblitemcodepopup" runat="server" Text='<%# Bind("ins_itm_cd") %>' Width="100px"></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>

                                                                        </Columns>
                                                                    </asp:GridView>

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

                                                        <div id="divstus" runat="server" visible="false">
                                                            <div class="col-sm-12">

                                                                <div class="panelscoll250">

                                                                    <asp:GridView ID="gvStatus" AutoGenerateColumns="false" runat="server" CssClass="table table-hover table-striped" GridLines="None"
                                                                        ShowHeaderWhenEmpty="True" EmptyDataText="No data found..." OnSelectedIndexChanged="gvStatus_SelectedIndexChanged">

                                                                        <Columns>

                                                                            <asp:CommandField ShowSelectButton="true" ButtonType="Link" />

                                                                            <asp:TemplateField HeaderText="Status Code">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblseectedstus" runat="server" Text='<%# Bind("MIC_CD") %>' Width="75px"></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField HeaderText="Description">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblstustext" runat="server" Text='<%# Bind("MIS_DESC") %>' Width="75px"></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>

                                                                        </Columns>
                                                                    </asp:GridView>

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

                                                        <div id="Divloc" runat="server" visible="false">
                                                            <div class="col-sm-12">

                                                                <div class="panelscoll250">

                                                                    <asp:GridView ID="gvLoc" AutoGenerateColumns="false" runat="server" CssClass="table table-hover table-striped" GridLines="None" ShowHeaderWhenEmpty="True"
                                                                        EmptyDataText="No data found..." OnSelectedIndexChanged="gvLoc_SelectedIndexChanged">

                                                                        <Columns>

                                                                            <asp:CommandField ShowSelectButton="true" ButtonType="Link" />

                                                                            <asp:TemplateField HeaderText="Location Code">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblloccode" runat="server" Text='<%# Bind("SEL_LOC_CD") %>' Width="50px"></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField HeaderText="Description">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lbllocdesc" runat="server" Text='<%# Bind("SEL_LOC_CD") %>'></asp:Label>
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
                                            <div class="col-sm-12 height5">
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
    </asp:Panel>

    <asp:UpdatePanel runat="server" ID="u1">
        <ContentTemplate>
            <asp:Button ID="Button1" runat="server" Text="Button" Style="display: none;" />
            <asp:ModalPopupExtender ID="PopupSerial" runat="server" Enabled="True" TargetControlID="Button1"
                PopupControlID="testPanel" CancelControlID="btnClose" PopupDragHandleControlID="PopupHeader" Drag="true" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>
        </ContentTemplate>
    </asp:UpdatePanel>

    <asp:Panel runat="server" ID="testPanel" DefaultButton="LinkButton1">
        <div runat="server" id="Div2" class="panel panel-primary Mheight" style="width: 700px;">
            <asp:Label ID="Label1" runat="server" Text="Label" Visible="false"></asp:Label>
            <div class="panel panel-default" style="height: 370px;">
                <div class="panel-heading" style="height: 25px;">
                    <div class="col-sm-11"><b>Main Item Advanced Search</b></div>
                    <div class="col-sm-1">
                        <asp:LinkButton ID="LinkButton1" runat="server">
                             <span class="glyphicon glyphicon-remove"  aria-hidden="true">Close</span>
                        </asp:LinkButton>
                    </div>
                </div>
                <div class="panel-body">

                    <div class="row">
                        <div class="col-sm-12">
                            <asp:ScriptManagerProxy ID="ScriptManagerProxy8" runat="server"></asp:ScriptManagerProxy>
                            <asp:UpdatePanel ID="UpdatePanel8" runat="server">
                                <ContentTemplate>
                                    <asp:GridView ID="dgvResult" CausesValidation="false" runat="server" AllowPaging="True"
                                        GridLines="None" CssClass="table table-hover table-striped" PageSize="15" PagerStyle-CssClass="cssPager" ShowHeaderWhenEmpty="true"
                                        EmptyDataText="No data found..."
                                        OnPageIndexChanged="dgvResult_PageIndexChanged" OnSelectedIndexChanged="dgvResult_SelectedIndexChanged">
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

    <asp:UpdatePanel ID="UpdatePanel4" runat="server">
        <ContentTemplate>
            <asp:Button ID="Button2" runat="server" Text="Button" Style="display: none;" />
            <asp:ModalPopupExtender ID="PopupSearch" runat="server" Enabled="True" TargetControlID="Button2"
                PopupControlID="pnlDpopup" PopupDragHandleControlID="PopupHeader" Drag="true" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>

        </ContentTemplate>

    </asp:UpdatePanel>

    <asp:UpdatePanel ID="UpdatePanel7" runat="server">
        <ContentTemplate>
            <asp:Panel runat="server" ID="pnlDpopup" DefaultButton="lbtnSearch">
                <div runat="server" id="Div3" class="panel panel-default height400 width700">

                    <asp:Label ID="Label2" runat="server" Text="Label" Visible="false"></asp:Label>
                    <div class="panel panel-default">
                        <div class="panel-heading">
                            <asp:LinkButton ID="btnDClose" OnClick="btnDClose_Click" runat="server">
                             <span class="glyphicon glyphicon-remove" aria-hidden="true">Close</span>
                            </asp:LinkButton>
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
                                        <div class="col-sm-4 paddingRight5 paddingLeft0">
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
                                        <div class="col-sm-4 paddingRight5 paddingLeft0">
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
                                            <asp:LinkButton ID="lbtnSeByDate" runat="server" OnClick="lbtnSeByDate_Click">
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
                                            <asp:TextBox ID="txtSearchbywordD" CausesValidation="false" placeholder="Search by word" class="form-control"
                                                AutoPostBack="true" runat="server" OnTextChanged="txtSearchbywordD_TextChanged"></asp:TextBox>
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
                                    <div class="col-sm-12" style="height: 300px; overflow-y: auto;">
                                        <asp:ScriptManagerProxy ID="ScriptManagerProxy2" runat="server"></asp:ScriptManagerProxy>
                                        <asp:UpdatePanel ID="UpdatePanel13" runat="server">
                                            <ContentTemplate>
                                                <asp:GridView ID="grdResultD" CausesValidation="false" runat="server" EmptyDataText="No data found..."
                                                    ShowHeaderWhenEmpty="True" GridLines="None" CssClass="table table-hover table-striped" PageSize="10"
                                                    PagerStyle-CssClass="cssPager" AllowPaging="True"
                                                    OnSelectedIndexChanged="grdResultD_SelectedIndexChanged" OnPageIndexChanging="grdResultD_PageIndexChanging">
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

    <asp:UpdatePanel ID="UpdatePanel25" runat="server">
        <ContentTemplate>
            <asp:Button ID="btnconfbox" runat="server" Text="Button" Style="display: none;" />
            <asp:ModalPopupExtender ID="PopupConfBox" runat="server" Enabled="True" TargetControlID="btnconfbox"
                PopupControlID="pnlConfBox" PopupDragHandleControlID="PopupHeader" Drag="true" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>
        </ContentTemplate>
    </asp:UpdatePanel>

    <asp:Panel ID="pnlConfBox" runat="server" align="center">
        <div runat="server" id="Div5" class="panel panel-info height120 width250">
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

    <%--Barcode Print--%>
    <asp:UpdatePanel ID="UpdatePanel32" runat="server">
        <ContentTemplate>
            <asp:Button ID="Button12" runat="server" Text="Button" Style="display: none;" />
            <asp:ModalPopupExtender ID="popupBarcode" runat="server" Enabled="True" TargetControlID="Button12"
                PopupControlID="pnlBarcode" PopupDragHandleControlID="PopupHeader" Drag="true" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:Panel ID="pnlBarcode" runat="server" align="center">
        <div runat="server" id="Div13" class="panel panel-info height120 width250">
            <asp:Label ID="Label17" runat="server" Text="Label" Visible="false"></asp:Label>
            <asp:ScriptManagerProxy ID="ScriptManagerProxy18" runat="server"></asp:ScriptManagerProxy>
            <asp:UpdatePanel ID="UpdatePanel33" runat="server">
                <ContentTemplate>

                    <div class="panel panel-heading">
                        <span>Information</span>
                    </div>
                    <div class="panel-body">
                        <div class="row">
                            <div class="col-sm-12">
                                <asp:Label ID="lblMssgBarcode" runat="server"></asp:Label>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-12">
                                <asp:Label ID="lblMsg" runat="server"></asp:Label>
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
                                <asp:Button ID="btnBarcdYes" runat="server" Text="yes" Width="80px" CausesValidation="false" class="btn btn-primary btn-xs" OnClick="btnBarcdYes_Click" />
                            </div>
                            <div class="col-sm-2 ">
                                <asp:Button ID="btnBarcdNo" runat="server" Text="No" Width="80px" CausesValidation="false" class="btn btn-primary btn-xs" OnClick="btnBarcdNo_Click" />
                            </div>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </asp:Panel>


    <script>
        if (typeof jQuery == 'undefined') {
            alert('jQuery is not loaded');
        }
        Sys.Application.add_load(func);
        function func() {
            $('.txteditser').keypress(function (evt) {
                // var ch = (evt.which) ? evt.which : evt.keyCode;
                var charCode = evt.which;
                var str = $(this).val();
                //console.log(charCode);
                if ((charCode == 8) || (charCode == 9) || (charCode == 0)) {
                    return true;
                }
                else if (str.length < 40) {
                    if (charCode > 47 && charCode < 58) {
                        return true;
                    }
                    else {
                        return true;
                    }
                }
                else {
                    $(this).value = str.substr(0, 40);
                    alert('Maximum 40 characters are allowed ');
                    return false;
                }
            });
        }
    </script>
</asp:Content>
