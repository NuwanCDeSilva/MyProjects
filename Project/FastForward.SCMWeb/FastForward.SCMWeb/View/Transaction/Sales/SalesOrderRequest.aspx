<%@ Page Title="" Language="C#" MasterPageFile="~/View/AdminSite.Master" AutoEventWireup="true" CodeBehind="SalesOrderRequest.aspx.cs" Inherits="FastForward.SCMWeb.View.Transaction.Local_Purchasing.SalesOrderRequest" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
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

        function ConfirmUpdate() {
            var selectedvalueOrdPlace = confirm("Do you want to update ?");
            if (selectedvalueOrdPlace) {
                document.getElementById('<%=hdnUpdate.ClientID %>').value = "Yes";
            }
            else {
                document.getElementById('<%=hdnUpdate.ClientID %>').value = "No";
            }
        };

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
        function Enable() {
            return;
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" runat="server">
    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <asp:HiddenField ID="hdnSave" runat="server" />
            <asp:HiddenField ID="hdnUpdate" runat="server" />
            <asp:HiddenField ID="hdnApprove" runat="server" />
            <asp:HiddenField ID="hdnCancel" runat="server" />
            <asp:HiddenField ID="hdnClear" runat="server" />
            <asp:HiddenField ID="hdnItemDelete" runat="server" />
            <div class="panel panel-default marginLeftRight5 paddingbottom0">
                <div class="panel-body paddingtopbottom0">
                    <div class="row">
                        <div class="col-sm-7  buttonrow">
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
                            </div>
                        </div>
                        <div class="col-sm-4  buttonRow">
                            <div class="col-sm-3 paddingRight0">
                            </div>
                            <div class="col-sm-3 paddingRight0">
                                <asp:LinkButton ID="lbtnSave" CausesValidation="false" runat="server" CssClass="floatRight" OnClientClick="ConfirmSave()" OnClick="lbtnSave_Click">
                            <span class="glyphicon glyphicon-saved" aria-hidden="true"></span>Save
                                </asp:LinkButton>
                            </div>
                            <div class="col-sm-3 paddingRight0">
                                <asp:LinkButton ID="lbtnUpdate" CausesValidation="false" runat="server" CssClass="floatRight" OnClientClick="ConfirmUpdate()" OnClick="lbtnUpdate_Click">
                            <span class="glyphicon glyphicon-edit" aria-hidden="true"></span>Update
                                </asp:LinkButton>
                            </div>
                            <div class="col-sm-3 paddingRight0">
                                <asp:LinkButton ID="lbtnCancel" CausesValidation="false" runat="server" CssClass="floatRight" OnClientClick="ConfirmCancel()" OnClick="lbtnCancel_Click">
                            <span class="glyphicon glyphicon-remove" aria-hidden="true"></span>Cancel
                                </asp:LinkButton>
                            </div>
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
                                <div class="panel-heading">Sales Order Request</div>
                                <div class="panel-body">
                                    <div class="row">
                                        <div class="col-sm-6">
                                            <div class="col-sm-6">
                                                <div class="row">
                                                    <div class="col-sm-4 labelText1">
                                                        Request #
                                                    </div>
                                                    <div class="col-sm-7 paddingRight5">
                                                        <asp:TextBox ID="txtRequestNo" CausesValidation="false" ReadOnly="true" runat="server" AutoPostBack="true" class="form-control" OnTextChanged="txtRequestNo_TextChanged"></asp:TextBox>
                                                    </div>
                                                    <div class="col-sm-1 paddingLeft0">
                                                        <asp:LinkButton ID="lbtRequestNo" runat="server" OnClick="lbtRequestNo_Click">
                                                            <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                        </asp:LinkButton>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-sm-6">
                                                <div class="row">
                                                    <div class="col-sm-4 labelText1">
                                                        Request Type
                                                    </div>
                                                    <div class="col-sm-7 paddingRight5">
                                                        <asp:TextBox ID="txtRequestType" CausesValidation="false" runat="server" class="form-control" AutoPostBack="true" OnTextChanged="txtRequestType_TextChanged"></asp:TextBox>
                                                    </div>
                                                    <div class="col-sm-1 paddingLeft0">
                                                        <asp:LinkButton ID="lbtnRequestType" runat="server" OnClick="lbtnRequestType_Click">
                                                            <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                        </asp:LinkButton>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-12 height5">
                                                </div>
                                            </div>
                                            <div class="col-sm-6">
                                                <div class="row">
                                                    <div class="col-sm-4 labelText1">
                                                        Date
                                                    </div>
                                                    <div class="col-sm-7 paddingRight5">
                                                        <asp:TextBox ID="txtDate" runat="server" CssClass="form-control" onkeypress="return RestrictSpace()"
                                                            Format="dd/MMM/yyyy" Enabled="false"></asp:TextBox>
                                                    </div>
                                                    <div class="col-sm-1 paddingLeft0">
                                                        <asp:LinkButton ID="lbtnDate" runat="server" CausesValidation="false">
                                                            <span class="glyphicon glyphicon-calendar" aria-hidden="true"></span>
                                                        </asp:LinkButton>
                                                        <asp:CalendarExtender ID="CalendarExtender3" runat="server" TargetControlID="txtDate"
                                                            PopupButtonID="lbtnDate" Format="dd/MMM/yyyy">
                                                        </asp:CalendarExtender>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-sm-6">
                                                <div class="row">
                                                    <div class="col-sm-4 labelText1">
                                                        Ref / PO #
                                                    </div>
                                                    <div class="col-sm-8">
                                                        <asp:TextBox ID="txtRefNo" CausesValidation="false" runat="server" MaxLength="30" class="form-control"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-12 height5">
                                                </div>
                                            </div>
                                            <div class="col-sm-6">
                                                <div class="row">
                                                    <div class="col-sm-4 labelText1">
                                                        Expected Date
                                                    </div>
                                                    <div class="col-sm-7 paddingRight5">
                                                        <asp:TextBox ID="txtExpectedDate" runat="server" CssClass="form-control" onkeypress="return RestrictSpace()"
                                                            Format="dd/MMM/yyyy" Enabled="false"></asp:TextBox>
                                                    </div>
                                                    <div class="col-sm-1 paddingLeft0">
                                                        <asp:LinkButton ID="lbtnExpectedDate" runat="server" CausesValidation="false">
                                                            <span class="glyphicon glyphicon-calendar" aria-hidden="true"></span>
                                                        </asp:LinkButton>
                                                        <asp:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtExpectedDate"
                                                            PopupButtonID="lbtnExpectedDate" Format="dd/MMM/yyyy">
                                                        </asp:CalendarExtender>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-sm-6">
                                                <div class="row">
                                                    <div class="col-sm-3 labelText1">
                                                         Order Status
                                                    </div>
                                                    <div class="col-sm-1 labelText1">
                                                    </div>
                                                    <div class="col-sm-8">
                                                        <asp:TextBox ID="txtOrderStatus" CausesValidation="false" ReadOnly="true" runat="server" class="form-control" Font-Bold="true" ForeColor="DarkRed"></asp:TextBox>
                                                        <asp:TextBox ID="txtOrdStus" CausesValidation="false" Visible="false" runat="server" class="form-control"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-sm-6 paddingLeft0">
                                            <div class="col-sm-6">
                                                <div class="row">
                                                    <div class="col-sm-4 labelText1">
                                                        Sales Executive
                                                    </div>
                                                    <div class="col-sm-8">
                                                        <asp:TextBox ID="txtSalesExcecutive" CausesValidation="false" runat="server" MaxLength="20" class="form-control" AutoPostBack="true" OnTextChanged="txtSalesExcecutive_TextChanged"></asp:TextBox>
                                                    </div>
                                                    <%--paddingRight5--%>
                                                    <%--<div class="col-sm-1 paddingLeft0">
                                                        <asp:LinkButton ID="lbtnSalesExcecutive" runat="server" OnClick="lbtnSalesExcecutive_Click">
                                                            <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                        </asp:LinkButton>
                                                    </div>--%>
                                                </div>
                                            </div>
                                            <div class="col-sm-6">
                                                <div class="row">
                                                    <div class="col-sm-12 paddingRight5">
                                                        <asp:TextBox ID="txtSalesExcecutiveName" CausesValidation="false" ReadOnly="true" runat="server" class="form-control"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-12 height5">
                                                </div>
                                            </div>
                                            <div class="col-sm-6">
                                                <div class="row">
                                                    <div class="col-sm-4 labelText1">
                                                        Invoice Customer
                                                    </div>
                                                    <div class="col-sm-7 paddingRight5">
                                                        <asp:TextBox ID="txtInvoiceCustomer" CausesValidation="false" runat="server" MaxLength="20" class="form-control" AutoPostBack="true" OnTextChanged="txtInvoiceCustomer_TextChanged"></asp:TextBox>
                                                    </div>
                                                    <div class="col-sm-1 paddingLeft0">
                                                        <asp:LinkButton ID="lbtnInvoiceCustomer" runat="server" OnClick="lbtnInvoiceCustomer_Click">
                                                            <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                        </asp:LinkButton>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-sm-6">
                                                <div class="row">
                                                    <div class="col-sm-12 paddingRight5">
                                                        <asp:TextBox ID="txtInvoiceCustomerName" CausesValidation="false" ReadOnly="true" runat="server" class="form-control"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-12 height5">
                                                </div>
                                            </div>
                                            <div class="col-sm-6">
                                                <div class="row">
                                                    <div class="col-sm-4 labelText1">
                                                    </div>
                                                    <div class="col-sm-7 paddingRight5">
                                                    </div>
                                                    <div class="col-sm-1 paddingLeft0">
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-sm-6">
                                                <div class="row">
                                                    <div class="col-sm-1 labelText1">
                                                    </div>
                                                    <div class="col-sm-3 labelText1">
                                                    </div>
                                                    <div class="col-sm-8">
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
                            <div class="panel panel-default">
                                <div class="panel-heading">Sales Order Request Details</div>
                                <div class="row">
                                    <div class="panel-body">
                                        <div class="col-sm-12" id="ItemAdd" runat="server">
                                            <div class="panel panel-default">
                                                <div class="panel-heading">
                                                    <div class="row">
                                                        <div class="col-sm-7">
                                                            <div class="row">
                                                                <div class="col-sm-4">
                                                                    <div class="row">
                                                                        <div class="col-sm-2 labelText1">
                                                                            Item
                                                                        </div>
                                                                        <div class="col-sm-8 paddingRight5">
                                                                            <asp:TextBox ID="txtItem" CausesValidation="false" runat="server" MaxLength="30" class="form-control" AutoPostBack="true" OnTextChanged="txtItem_TextChanged"></asp:TextBox>
                                                                        </div>
                                                                        <div class="col-sm-2 paddingLeft0">
                                                                            <asp:LinkButton ID="lbtnItem" runat="server" OnClick="lbtnItem_Click">
                                                                                    <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                                            </asp:LinkButton>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class="col-sm-3 paddingLeft0">
                                                                    <div class="row">
                                                                        <div class="col-sm-2 labelText1">
                                                                            Model
                                                                        </div>
                                                                        <div class="col-sm-10 paddingRight5">
                                                                            <asp:TextBox ID="txtModel" CausesValidation="false" ReadOnly="true" runat="server" class="form-control"></asp:TextBox>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class="col-sm-5">
                                                                    <div class="row">
                                                                        <div class="col-sm-3 labelText1">
                                                                            Description
                                                                        </div>
                                                                        <div class="col-sm-9 paddingRight5 paddingLeft0">
                                                                            <asp:TextBox ID="txtDescription" CausesValidation="false" ReadOnly="true" runat="server" class="form-control"></asp:TextBox>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="col-sm-5">
                                                            <div class="row">
                                                                <div class="col-sm-3">
                                                                    <div class="row">
                                                                        <div class="col-sm-3 labelText1">
                                                                            Qty
                                                                        </div>
                                                                        <div class="col-sm-9 paddingRight5">
                                                                            <asp:TextBox ID="txtQty" CausesValidation="false" runat="server" AutoPostBack="true" onkeydown="return jsDecimals(event);" MaxLength="10" class="form-control textAlignRight" OnTextChanged="txtQty_TextChanged"></asp:TextBox>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class="col-sm-4">
                                                                    <div class="row">
                                                                        <div class="col-sm-5 labelText1">
                                                                            Unit Price
                                                                        </div>
                                                                        <div class="col-sm-7 paddingLeft5">
                                                                            <asp:TextBox ID="txtUnitPrice" CausesValidation="false" runat="server" AutoPostBack="true" onkeydown="return jsDecimals(event);" MaxLength="10" class="form-control textAlignRight" OnTextChanged="txtUnitPrice_TextChanged"></asp:TextBox>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class="col-sm-4">
                                                                    <div class="row">
                                                                        <div class="col-sm-3 labelText1">
                                                                            Value
                                                                        </div>
                                                                        <div class="col-sm-9">
                                                                            <asp:TextBox ID="txtValue" CausesValidation="false" runat="server" ReadOnly="true" onkeydown="return jsDecimals(event);" MaxLength="10" class="form-control textAlignRight"></asp:TextBox>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class="col-sm-1">
                                                                    <div class="row">
                                                                        <div class="col-sm-6 paddingRight5 paddingLeft5">
                                                                            <asp:LinkButton ID="lbtnAdd" runat="server" OnClick="lbtnAdd_Click">
                                                                                <span class="glyphicon glyphicon-plus" aria-hidden="true"></span>
                                                                            </asp:LinkButton>
                                                                        </div>
                                                                        <div class="col-sm-6 paddingRight5 paddingLeft5">
                                                                            <asp:LinkButton ID="lbtnClearItem" runat="server" OnClick="lbtnClearItem_Click">
                                                                                <span class="glyphicon glyphicon-refresh" aria-hidden="true"></span>
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
                                        <div class="col-sm-12 GridScroll230">
                                            <asp:GridView ID="grdItem" CssClass="table table-hover table-striped" runat="server" GridLines="None" ShowHeaderWhenEmpty="True"
                                                EmptyDataText="No data found..." AutoGenerateColumns="False">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Item Code">
                                                        <EditItemTemplate>
                                                            <asp:Label ID="lblItemCodeEdit" runat="server" Text='<%# Bind("ITRI_ITM_CD") %>'></asp:Label>
                                                        </EditItemTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblItemCode" runat="server" Text='<%# Bind("ITRI_ITM_CD") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle Width="15%" />
                                                        <ItemStyle Width="15%" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Item Description">
                                                        <EditItemTemplate>
                                                            <asp:Label ID="lblItemDescriptionEdit" runat="server" Text='<%# Bind("DESCRIPTION") %>'></asp:Label>
                                                        </EditItemTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblItemDescription" runat="server" Text='<%# Bind("DESCRIPTION") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle Width="25%" />
                                                        <ItemStyle Width="25%" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Model">
                                                        <EditItemTemplate>
                                                            <asp:Label ID="lblModelEdit" runat="server" Text='<%# Bind("MODEL") %>'></asp:Label>
                                                        </EditItemTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblModel" runat="server" Text='<%# Bind("MODEL") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle Width="15%" />
                                                        <ItemStyle Width="15%" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Qty">
                                                        <EditItemTemplate>
                                                            <asp:TextBox ID="txtQtyEdit" runat="server" class="form-control" onkeydown="return jsDecimals(event);" Text='<%# Bind("ITRI_QTY") %>'></asp:TextBox>
                                                        </EditItemTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblQty" runat="server" Text='<%# Bind("ITRI_QTY") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle CssClass="textAlignRight" Width="10%" />
                                                        <ItemStyle HorizontalAlign="Right" Width="10%" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Unit Price">
                                                        <EditItemTemplate>
                                                            <asp:TextBox ID="txtUnitPrice" runat="server" class="form-control" onkeydown="return jsDecimals(event);" Text='<%# Bind("ITRI_UNIT_PRICE", "{0:N2}") %>'></asp:TextBox>
                                                        </EditItemTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblUnitPrice" runat="server" Text='<%# Bind("ITRI_UNIT_PRICE", "{0:N2}") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle CssClass="textAlignRight" Width="10%" />
                                                        <ItemStyle HorizontalAlign="Right" Width="10%" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Value">
                                                        <EditItemTemplate>
                                                            <asp:Label ID="lblVlueEdit" runat="server" Text='<%# Bind("AMOUNT", "{0:N2}") %>'></asp:Label>
                                                        </EditItemTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblVlue" runat="server" Text='<%# Bind("AMOUNT", "{0:N2}") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle CssClass="textAlignRight" Width="10%" />
                                                        <ItemStyle HorizontalAlign="Right" Width="10%" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField ShowHeader="False">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lbtnGrdItemEdit" CausesValidation="false" runat="server" OnClick="lbtnGrdItemEdit_Click">
                                                                <span class="glyphicon glyphicon-pencil" aria-hidden="true"></span>
                                                            </asp:LinkButton>
                                                        </ItemTemplate>
                                                        <EditItemTemplate>
                                                            <asp:LinkButton ID="lbtnGrdItemUpdate" runat="server" CausesValidation="false" CommandName="Update" OnClick="lbtnGrdItemUpdate_Click">
                                                                <span class="glyphicon glyphicon-ok" aria-hidden="true"></span>
                                                            </asp:LinkButton>
                                                            &nbsp;<asp:LinkButton ID="lbtnGrdItemCancel" runat="server" CausesValidation="false" CommandName="Cancel" OnClick="lbtnGrdItemCancel_Click">
                                                                <span class="glyphicon glyphicon-remove" aria-hidden="true"></span>
                                                            </asp:LinkButton>
                                                        </EditItemTemplate>
                                                        <ItemStyle HorizontalAlign="Right" Width="10%" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField>
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lbtnGrdItemDalete" runat="server" Visible='<%#GetDaleteVisibility()%>' CausesValidation="false" OnClick="lbtnGrdItemDalete_Click" OnClientClick="ConfirmItemDelete()">
                                                                <span aria-hidden="true" class="glyphicon glyphicon-trash"></span>
                                                            </asp:LinkButton>
                                                        </ItemTemplate>
                                                        <ItemStyle Width="5%" HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-sm-12">
                            <div class="panel panel-default">
                                <div class="panel-body">
                                    <div class="row">
                                        <div class="col-sm-8">
                                            <div class="col-sm-12">
                                                <div class="row">
                                                    <div class="col-sm-1 labelText1">
                                                        Remarks
                                                    </div>
                                                    <div class="col-sm-11 paddingRight5">
                                                        <asp:TextBox ID="txtRemarks" CausesValidation="false" TextMode="MultiLine" Rows="5" runat="server" class="form-control"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-sm-3">
                                            <div class="col-sm-12">
                                                <div class="row">
                                                    <div class="col-sm-4 labelText1">
                                                        Total Amount
                                                    </div>
                                                    <div class="col-sm-5 paddingRight5">
                                                        <asp:TextBox ID="txtTotalAmount" CausesValidation="false" ReadOnly="true" runat="server" class="form-control textAlignRight"></asp:TextBox>
                                                    </div>
                                                    <div class="col-sm-4 labelText1">
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-sm-1">
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
            <asp:Button ID="btnhidden" runat="server" Text="Button" Style="display: none;" />
            <asp:ModalPopupExtender ID="UserPopup" runat="server" Enabled="True" TargetControlID="btnhidden"
                PopupControlID="pnlModalPopup" CancelControlID="btnClose" PopupDragHandleControlID="PopupHeader" Drag="true" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>
            <%-- Style="display: none"--%>
            <asp:Panel runat="server" ID="pnlModalPopup" DefaultButton="lbtnSearch">
                <%--<asp:UpdatePanel runat="server">
                    <ContentTemplate>--%>
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
                                                    <span class="glyphicon glyphicon-search" aria-hidden="true"  ></span>
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
                <%--</ContentTemplate>
                </asp:UpdatePanel>--%>
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
