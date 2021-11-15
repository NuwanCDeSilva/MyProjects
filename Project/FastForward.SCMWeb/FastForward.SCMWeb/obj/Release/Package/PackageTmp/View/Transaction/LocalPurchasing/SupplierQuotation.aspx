<%@ Page Title="" Language="C#" MasterPageFile="~/View/AdminSite.Master" AutoEventWireup="true" CodeBehind="SupplierQuotation.aspx.cs" Inherits="FastForward.SCMWeb.View.Transaction.Local_Purchasing.SupplierQuotation" %>

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
            var selectedvalueOrdPlace = confirm("Do you want to Update ?");
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
                                <asp:LinkButton ID="lbtnSave" CausesValidation="false" runat="server" CssClass="floatRight" OnClientClick="ConfirmSave()" OnClick="lbtnSave_Click">
                            <span class="glyphicon glyphicon-saved" aria-hidden="true"></span>Save
                                </asp:LinkButton>
                            </div>
                            <div class="col-sm-3 paddingRight0">
                                <asp:LinkButton ID="lbtnApprove" CausesValidation="false" runat="server" CssClass="floatRight" OnClientClick="ConfirmApprove()" OnClick="lbtnApprove_Click">
                                    <span class="glyphicon glyphicon-thumbs-up fontsize18" aria-hidden="true"></span>Approve
                                </asp:LinkButton>
                            </div>
                            <div class="col-sm-3 paddingRight0">
                                <asp:LinkButton ID="lbtnCancel" CausesValidation="false" runat="server" CssClass="floatRight" OnClientClick="ConfirmCancel()" OnClick="lbtnCancel_Click">
                                                        <span class="glyphicon glyphicon-remove" aria-hidden="true"></span>Cancel
                                </asp:LinkButton>
                            </div>

                            <div class="col-sm-3 paddingRight0">
                                <asp:LinkButton ID="lbtnClear" runat="server" CssClass="floatRight" OnClientClick="ConfirmClear()" OnClick="lbtnClear_Click">
                            <span class="glyphicon glyphicon-refresh" aria-hidden="true"></span>Clear
                                </asp:LinkButton>
                            </div>

                        </div>
                        <%--<div class="col-sm-1  buttonRow paddingLeft0">
                             
                        </div>--%>
                        <div class="col-sm-1  buttonRow">
                            <div class="col-sm-12">
                                <div class="col-sm-12 paddingRight0">
                                    <div class="dropdown">
                                        <a id="dLabel" data-toggle="dropdown" role="button" aria-haspopup="true" aria-expanded="false">
                                            <span class="glyphicon glyphicon-menu-hamburger"></span>
                                        </a>
                                        <div class="dropdown-menu menupopup" aria-labelledby="dLabel">
                                            <div class="row">
                                                <div class="col-sm-12">
                                                    <div class="row">
                                                        <div class="col-sm-12 height10">
                                                        </div>
                                                    </div>
                                                    <div class="col-sm-3 paddingRight0">

                                                        <asp:LinkButton ID="lbtnUpdate" CausesValidation="false" runat="server" CssClass="floatRight" OnClientClick="ConfirmUpdate()" OnClick="lbtnUpdate_Click">
                                                            <span class="glyphicon glyphicon-edit" aria-hidden="true"></span>Update
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
                        <div class="col-sm-12">
                            <div class="panel panel-default">
                                <div class="panel-heading">Supplier Quotation</div>
                                <div class="panel-body">
                                    <div class="row">
                                        <div class="col-sm-6">
                                            <div class="col-sm-6">
                                                <div class="row">
                                                    <div class="col-sm-4 labelText1">
                                                        Quotation #
                                                    </div>
                                                    <div class="col-sm-7 paddingRight5">
                                                        <asp:TextBox ID="txtQuotationNo" CausesValidation="false" ReadOnly="true" runat="server" class="form-control"></asp:TextBox>
                                                         <div class="col-sm-1">
                                                        </div>
                                                        <asp:TextBox ID="txtQuoStatus" CausesValidation="false" ReadOnly="true" runat="server" class="form-control" ForeColor="DarkRed" Font-Bold="true"></asp:TextBox>
                                                    </div>
                                                    <div class="col-sm-1 paddingLeft0">
                                                        <asp:LinkButton ID="lbtQuotationNo" runat="server" OnClick="lbtQuotationNo_Click">
                                                            <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                        </asp:LinkButton>
                                                        <asp:TextBox ID="txtQuoStus" CausesValidation="false" ReadOnly="true" runat="server" class="form-control" Visible="false"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-sm-6">
                                                <div class="row">
                                                    <div class="col-sm-1 labelText1">
                                                    </div>
                                                    <div class="col-sm-3">
                                                        <asp:CheckBox ID="chkCopyFromPreviousOrder" runat="server" AutoPostBack="true" OnCheckedChanged="chkCopyFromPreviousOrder_CheckedChanged" />
                                                    </div>
                                                    <div class="col-sm-8 labelText1">
                                                        Copy From Previous Order
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
                                                        <asp:TextBox ID="txtDate" runat="server" class="form-control" ReadOnly="True"></asp:TextBox>
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
                                                    <div class="col-sm-1 labelText1">
                                                    </div>
                                                    <div class="col-sm-3 labelText1">
                                                        Type
                                                    </div>
                                                    <div class="col-sm-8">
                                                        <asp:DropDownList ID="ddlType" runat="server" class="form-control">
                                                            <asp:ListItem Text=" - - Select - - " Value="0" Selected="True"></asp:ListItem>
                                                            <asp:ListItem Text="Nomal" Value="N"></asp:ListItem>
                                                            <asp:ListItem Text="Consignment" Value="C"></asp:ListItem>
                                                        </asp:DropDownList>
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
                                                        Ex. Date
                                                    </div>
                                                    <div class="col-sm-7 paddingRight5">
                                                        <asp:TextBox ID="txtExDate" runat="server" class="form-control" ReadOnly="True"></asp:TextBox>
                                                    </div>
                                                    <div class="col-sm-1 paddingLeft0">
                                                        <asp:LinkButton ID="lbtnExDate" runat="server" CausesValidation="false">
                                                            <span class="glyphicon glyphicon-calendar" aria-hidden="true"></span>
                                                        </asp:LinkButton>
                                                        <asp:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtExDate"
                                                            PopupButtonID="lbtnExDate" Format="dd/MMM/yyyy">
                                                        </asp:CalendarExtender>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-sm-6">
                                                <div class="row">
                                                    <div class="col-sm-1 labelText1">
                                                    </div>
                                                    <div class="col-sm-3 labelText1">
                                                        Ref #
                                                    </div>
                                                    <div class="col-sm-8">
                                                        <asp:TextBox ID="txtRefNo" CausesValidation="false" runat="server" MaxLength="30" class="form-control"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-sm-6 paddingLeft0">
                                            <div class="col-sm-6">
                                                <div class="row">
                                                    <div class="col-sm-4 labelText1">
                                                        Supplier
                                                    </div>
                                                    <div class="col-sm-7 paddingRight5">
                                                        <asp:TextBox ID="txtSupplier" CausesValidation="false" AutoPostBack="true" runat="server" class="form-control" OnTextChanged="txtSupCode_TextChanged"></asp:TextBox>
                                                    </div>
                                                    <div class="col-sm-1 paddingLeft0">
                                                        <asp:LinkButton ID="lbtnSupplier" runat="server" OnClick="lbtnSupplier_Click">
                                                            <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                        </asp:LinkButton>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-sm-6">
                                                <div class="row">
                                                    <div class="col-sm-4 labelText1">
                                                        Currency
                                                    </div>
                                                    <div class="col-sm-7 paddingRight5">
                                                        <asp:TextBox ID="txtCurrency" CausesValidation="false" ReadOnly="true" runat="server" class="form-control"></asp:TextBox>
                                                    </div>
                                                    <div class="col-sm-1 paddingLeft0">
                                                        <asp:LinkButton ID="lbtnCurrency" runat="server" OnClick="lbtnCurrency_Click">
                                                            <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                        </asp:LinkButton>
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
                                                        Remark
                                                    </div>
                                                    <div class="col-sm-10 paddingRight5">
                                                        <asp:TextBox ID="txtRemark" runat="server" class="form-control" Rows="3" TextMode="MultiLine"></asp:TextBox>
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
                                <div class="panel-heading">Supplier Quotation Details</div>
                                <div class="row">
                                    <div class="panel-body">
                                        <div class="col-sm-12">
                                            <div class="panel panel-default">
                                                <div class="panel-heading">
                                                    <div class="row">
                                                        <%--<div class="col-sm-6">
                                                            <div class="row">--%>
                                                        <div class="col-sm-2">
                                                            <div class="row">
                                                                <div class="col-sm-3 labelText1">
                                                                    Item
                                                                </div>
                                                                <div class="col-sm-8 paddingRight5">
                                                                    <asp:TextBox ID="txtItem" style="text-transform:uppercase;"  CausesValidation="false" TabIndex="100" runat="server" MaxLength="30" class="form-control" AutoPostBack="true" OnTextChanged="txtItem_TextChanged"></asp:TextBox>
                                                                </div>
                                                                <div class="col-sm-1 paddingLeft0">
                                                                    <asp:LinkButton ID="lbtnItem" runat="server" OnClick="lbtnItem_Click">
                                                                                    <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                                    </asp:LinkButton>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="col-sm-2">
                                                            <div class="row">
                                                                <div class="col-sm-4 labelText1">
                                                                    Status
                                                                </div>
                                                                <div class="col-sm-8 paddingRight5">
                                                                    <asp:DropDownList ID="ddlStatus" runat="server" TabIndex="101" class="form-control">
                                                                        <asp:ListItem Text=" - - Select - - " Value="0" Selected="True"></asp:ListItem>
                                                                        <asp:ListItem Text="Good" Value="GOD"></asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="col-sm-2">
                                                            <div class="row">
                                                                <div class="col-sm-1 labelText1">
                                                                </div>
                                                                <div class="col-sm-4 labelText1">
                                                                    From Qty
                                                                </div>
                                                                <div class="col-sm-7 paddingRight5">
                                                                    <asp:TextBox ID="txtFromQty" CausesValidation="false" TabIndex="102" runat="server" AutoPostBack="true" onkeydown="return jsDecimals(event);" MaxLength="10" class="form-control textAlignRight" OnTextChanged="txtFromQty_TextChanged"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <%--</div>
                                                        </div>--%>
                                                        <%--<div class="col-sm-6">
                                                            <div class="row">--%>
                                                        <div class="col-sm-2">
                                                            <div class="row">
                                                                <div class="col-sm-4 labelText1">
                                                                    To Qty
                                                                </div>
                                                                <div class="col-sm-8 paddingRight5">
                                                                    <asp:TextBox ID="txtToQty" CausesValidation="false" runat="server" TabIndex="103" AutoPostBack="true" onkeydown="return jsDecimals(event);" MaxLength="10" class="form-control textAlignRight" OnTextChanged="txtToQty_TextChanged"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="col-sm-2">
                                                            <div class="row">
                                                                <div class="col-sm-6 labelText1">
                                                                    Unit Price(Tax Ex.)
                                                                </div>
                                                                <div class="col-sm-6 paddingRight5">
                                                                    <asp:TextBox ID="txtUnitPrice" CausesValidation="false" runat="server" TabIndex="104" AutoPostBack="true" onkeydown="return jsDecimals(event);" MaxLength="10" class="form-control textAlignRight" OnTextChanged="txtUnitPrice_TextChanged"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="col-sm-2">
                                                            <div class="row">
                                                                <div class="col-sm-6 paddingRight5 paddingLeft5">
                                                                    <asp:LinkButton ID="lbtnAdd" runat="server" TabIndex="105" OnClick="lbtnAdd_Click">
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
                                                        <%--</div>
                                                        </div>--%>
                                                    </div>
            </div>
                                            </div>
                                        </div>
                                        <div class="col-sm-12">
                                            <div class="panel panel-default">
                                                <div class="row">
                                                    <div class="panel-heading panelHeadingInfoBar">
                                                        <%--<div class="col-sm-12">--%>
                                                        <div class="col-sm-1 labelText1">Model</div>
                                                        <div class="col-sm-1 labelText1">
                                                            <asp:Label ID="lblModel" runat="server" Text="" CssClass="Color1 fontWeight900"></asp:Label>
                                                        </div>
                                                        <div class="col-sm-2 labelText1">Description</div>
                                                        <div class="col-sm-3 labelText1">
                                                            <asp:Label ID="lblDescription" runat="server" Text="" CssClass="Color1 fontWeight900"></asp:Label>
                                                        </div>
                                                        <div class="col-sm-1 labelText1">Brand</div>
                                                        <div class="col-sm-2 labelText1">
                                                            <asp:Label ID="lblBrand" runat="server" Text="" CssClass="Color1 fontWeight900"></asp:Label>
                                                        </div>
                                                        <div class="col-sm-1 labelText1">Part #</div>
                                                        <div class="col-sm-1 labelText1">
                                                            <asp:Label ID="lblPartNo" runat="server" Text="" CssClass="Color1 fontWeight900"></asp:Label>
                                                        </div>
                                                        <%--</div>--%>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-sm-12 GridScroll">
                                            <asp:GridView ID="grdSupplierQuotation" CssClass="table table-hover table-striped" runat="server" GridLines="None" ShowHeaderWhenEmpty="True"
                                                EmptyDataText="No data found..." AutoGenerateColumns="False">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Item Code">
                                                        <EditItemTemplate>
                                                            <asp:Label ID="lblItemCodeEdit" runat="server" Text='<%# Bind("Qd_itm_cd") %>'></asp:Label>
                                                        </EditItemTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblItemCode" runat="server" Text='<%# Bind("Qd_itm_cd") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle Width="15%" />
                                                        <ItemStyle Width="15%" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Item Description">
                                                        <EditItemTemplate>
                                                            <asp:Label ID="lblItemDescriptionEdit" runat="server" Text='<%# Bind("Qd_itm_desc") %>'></asp:Label>
                                                        </EditItemTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblItemDescription" runat="server" Text='<%# Bind("Qd_itm_desc") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle Width="20%" />
                                                        <ItemStyle Width="20%" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Status" Visible="false">
                                                        <EditItemTemplate>
                                                            <asp:Label ID="lblStatusEdit" runat="server" Text='<%# Bind("Qd_itm_stus") %>'></asp:Label>
                                                        </EditItemTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblStatus" runat="server" Text='<%# Bind("Qd_itm_stus") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle Width="10%" />
                                                        <ItemStyle Width="10%" />
                                                    </asp:TemplateField>
                                                     <asp:TemplateField HeaderText="Status">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblMi_statusDes" runat="server" Text='<%# Bind("Mi_statusDes") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle Width="10%" />
                                                        <ItemStyle Width="10%" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="From Qty">
                                                        <EditItemTemplate>
                                                            <asp:TextBox ID="txtQtyEdit" runat="server" class="form-control" onkeydown="return jsDecimals(event);" Text='<%# Bind("Qd_frm_qty", "{0:N2}") %>'></asp:TextBox>
                                                        </EditItemTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblQty" runat="server" Text='<%# Bind("Qd_frm_qty", "{0:N2}") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle CssClass="textAlignRight" Width="10%" />
                                                        <ItemStyle HorizontalAlign="Right" Width="10%" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="To Qty">
                                                        <EditItemTemplate>
                                                            <asp:TextBox ID="txtTQtyEdit" runat="server" class="form-control" onkeydown="return jsDecimals(event);" Text='<%# Bind("Qd_To_qty", "{0:N2}") %>'></asp:TextBox>
                                                        </EditItemTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblTQty" runat="server" Text='<%# Bind("Qd_To_qty", "{0:N2}") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle CssClass="textAlignRight" Width="10%" />
                                                        <ItemStyle HorizontalAlign="Right" Width="10%" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Unit Price">
                                                        <EditItemTemplate>
                                                            <asp:TextBox ID="txtUnitPrice" runat="server" class="form-control" onkeydown="return jsDecimals(event);" Text='<%# Bind("Qd_unit_price", "{0:N2}") %>'></asp:TextBox>
                                                        </EditItemTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblUnitPrice" runat="server" Text='<%# Bind("Qd_unit_price", "{0:N2}") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle CssClass="textAlignRight" Width="10%" />
                                                        <ItemStyle HorizontalAlign="Right" Width="10%" />
                                                    </asp:TemplateField>
                                                    <%--<asp:TemplateField HeaderText="Tax">
                                                        <EditItemTemplate>
                                                            <asp:Label ID="lblTaxEdit" runat="server" Text='<%# Bind("Qd_itm_tax", "{0:N2}") %>'></asp:Label>
                                                        </EditItemTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblTax" runat="server" Text='<%# Bind("Qd_itm_tax", "{0:N2}") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle CssClass="textAlignRight" Width="10%" />
                                                        <ItemStyle HorizontalAlign="Right" Width="10%" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Value">
                                                        <EditItemTemplate>
                                                            <asp:Label ID="lblVlueEdit" runat="server" Text='<%# Bind("Qd_amt", "{0:N2}") %>'></asp:Label>
                                                        </EditItemTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblVlue" runat="server" Text='<%# Bind("Qd_amt", "{0:N2}") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle CssClass="textAlignRight" Width="10%" />
                                                        <ItemStyle HorizontalAlign="Right" Width="10%" />
                                                    </asp:TemplateField>--%>
                                                    <asp:TemplateField ShowHeader="False">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lbtnSupplierQuotationEdit" CausesValidation="false" runat="server" OnClick="lbtnSupplierQuotationEdit_Click" Visible="false">
                                                                <span class="glyphicon glyphicon-pencil" aria-hidden="true"></span>
                                                            </asp:LinkButton>
                                                        </ItemTemplate>
                                                        <EditItemTemplate>
                                                            <asp:LinkButton ID="lbtnSupplierQuotationUpdate" runat="server" CausesValidation="false" CommandName="Update" OnClick="lbtnSupplierQuotationUpdate_Click">
                                                                <span class="glyphicon glyphicon-ok" aria-hidden="true"></span>
                                                            </asp:LinkButton>
                                                            &nbsp;<asp:LinkButton ID="lbtnSupplierQuotationCancel" runat="server" CausesValidation="false" CommandName="Cancel" OnClick="lbtnSupplierQuotationCancel_Click">
                                                                <span class="glyphicon glyphicon-remove" aria-hidden="true"></span>
                                                            </asp:LinkButton>
                                                        </EditItemTemplate>
                                                        <ItemStyle HorizontalAlign="Right" Width="10%" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField>
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lbtnSupplierQuotationDalete" runat="server" Visible='<%#GetDaleteVisibility()%>' CausesValidation="false" OnClick="lbtnSupplierQuotationDalete_Click" OnClientClick="ConfirmItemDelete()">
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
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="lbtnSupplier" EventName="Click" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
