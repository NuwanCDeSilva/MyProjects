<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucInScan.ascx.cs" Inherits="FastForward.SCMWeb.UserControls.ucInScan" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<link href="<%=Request.ApplicationPath%>Css/bootstrap.css" rel="stylesheet" />
<script src="<%=Request.ApplicationPath%>Js/jquery.min.js"></script>
<script src="<%=Request.ApplicationPath%>Js/bootstrap.min.js"></script>
<link href="<%=Request.ApplicationPath%>Css/style.css" rel="stylesheet" />
<script type="text/javascript">
    function isNumberKey(evt) {
        var charCode = (evt.which) ? evt.which : evt.keyCode;
        if (charCode != 46 && charCode > 31
          && (charCode < 48 || charCode > 57))
            return false;

        return true;
    };
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
            sticky: false,
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
    }
</script>
<div class="row">
    <%--<div class="col-sm-8">
        <div class="panel panel-default">
            <div class="panel-body">
                <div class="row">
                    <div class="col-sm-8">
                        <div class="row">
                            <div class="col-sm-4">
                                <div class="row">
                                    <div class="col-sm-1">
                                        <asp:CheckBox ID="chkLockItem" runat="server" OnCheckedChanged="chkLockItem_CheckedChanged" />
                                    </div>
                                    <div class="col-sm-10 labelText1">
                                        Lock Item
                                    </div>
                                </div>
                            </div>
                            <div class="col-sm-8">
                                <div class="row">
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-12 height5">
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-4">
                                <div class="row">
                                    <div class="col-sm-5 paddingRight0">
                                        <asp:DropDownList ID="ddlItemType" runat="server" class="form-control">
                                            <asp:ListItem Text="Item Code"></asp:ListItem>
                                            <asp:ListItem Text="Model #"></asp:ListItem>
                                            <asp:ListItem Text="Part #"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-sm-6 paddingRight5">
                                        <asp:TextBox ID="txtItemCode" CausesValidation="false" runat="server" MaxLength="20" class="form-control" AutoPostBack="true" OnTextChanged="txtItemCode_TextChanged"></asp:TextBox>
                                    </div>
                                    <div class="col-sm-1 paddingLeft0">
                                        <asp:LinkButton ID="lbtnItemCode" runat="server" OnClick="lbtnItemCode_Click">
                                            <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                        </asp:LinkButton>
                                    </div>
                                </div>
                            </div>
                            <div class="col-sm-8">
                                <div class="row">
                                    <div class="col-sm-4">
                                        <div class="row">
                                            <div class="col-sm-4 labelText1">
                                                Bin Code
                                            </div>
                                            <div class="col-sm-7 paddingRight5 paddingLeft5">
                                                <asp:TextBox ID="txtBinCode" CausesValidation="false" runat="server" MaxLength="20" class="form-control" OnTextChanged="txtBinCode_TextChanged"></asp:TextBox>
                                            </div>
                                            <div class="col-sm-1 paddingLeft0">
                                                <asp:LinkButton ID="lbtnBinCode" runat="server" OnClick="lbtnBinCode_Click">
                                                    <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                </asp:LinkButton>
                                            </div>

                                        </div>
                                    </div>
                                    <div class="col-sm-4">
                                        <div class="row">
                                            <div class="col-sm-3 labelText1">
                                                Status
                                            </div>
                                            <div class="col-sm-9">
                                                <asp:DropDownList ID="ddlStatus" runat="server" class="form-control">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-4">
                                        <div class="row">
                                            <div class="col-sm-3 labelText1">
                                                Qty
                                            </div>
                                            <div class="col-sm-9">
                                                <asp:TextBox ID="txtQty" CausesValidation="false" runat="server" MaxLength="10" class="form-control textAlignRight" onkeypress="return isNumberKey(event)"></asp:TextBox>
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
                        <div class="row" id="divExpiryDate" runat="server">
                            <div class="col-sm-4">
                                <div class="row">
                                    <div class="col-sm-5 labelText1">
                                        Manufacture Date
                                    </div>
                                    <div class="col-sm-6 paddingRight5">
                                        <asp:TextBox ID="txtManufactureDate" runat="server" CssClass="form-control" onkeypress="return RestrictSpace()"
                                            Format="dd/MMM/yyyy" Enabled="false"></asp:TextBox>
                                    </div>
                                    <div class="col-sm-1 paddingLeft0 paddingRight0">
                                        <asp:LinkButton ID="lbtnManufactureDate" runat="server" CausesValidation="false">
                                            <span class="glyphicon glyphicon-calendar" aria-hidden="true"></span>
                                        </asp:LinkButton>
                                        <asp:CalendarExtender ID="CalendarExtender3" runat="server" TargetControlID="txtManufactureDate"
                                            PopupButtonID="lbtnManufactureDate" Format="dd/MMM/yyyy">
                                        </asp:CalendarExtender>
                                    </div>
                                </div>
                            </div>
                            <div class="col-sm-4">
                                <div class="row">
                                    <div class="col-sm-3 labelText1">
                                        Expiry Date
                                    </div>
                                    <div class="col-sm-6 paddingRight5">
                                        <asp:TextBox ID="txtExpiryDate" runat="server" CssClass="form-control" onkeypress="return RestrictSpace()"
                                            Format="dd/MMM/yyyy" Enabled="false"></asp:TextBox>
                                    </div>
                                    <div class="col-sm-1 paddingLeft0 paddingRight0">
                                        <asp:LinkButton ID="lbtnExpiryDate" runat="server" CausesValidation="false">
                                            <span class="glyphicon glyphicon-calendar" aria-hidden="true"></span>
                                        </asp:LinkButton>
                                        <asp:CalendarExtender ID="CalendarExtender4" runat="server" TargetControlID="txtExpiryDate"
                                            PopupButtonID="lbtnExpiryDate" Format="dd/MMM/yyyy">
                                        </asp:CalendarExtender>
                                    </div>
                                </div>
                            </div>
                            <div class="col-sm-4">
                                <div class="row">
                                    <div class="col-sm-3 labelText1">
                                    </div>
                                    <div class="col-sm-9">
                                    </div>
                                </div>
                            </div>
                        </div>
                        <asp:Panel ID="pnlSerialized" runat="server">
                            <div class="row">
                                <div class="col-sm-12 height5">
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-4">
                                    <div class="row">
                                        <div class="col-sm-3 labelText1">
                                            Serial # I
                                        </div>
                                        <div class="col-sm-9">
                                            <asp:TextBox ID="txtSerialI" CausesValidation="false" runat="server" MaxLength="20" class="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-sm-4">
                                    <div class="row">
                                        <div class="col-sm-3 labelText1">
                                            Serial # II
                                        </div>
                                        <div class="col-sm-9">
                                            <asp:TextBox ID="txtSerialII" CausesValidation="false" runat="server" MaxLength="20" class="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-sm-4">
                                    <div class="row">
                                        <div class="col-sm-3 labelText1">
                                            Serial # III
                                        </div>
                                        <div class="col-sm-9">
                                            <asp:TextBox ID="txtSerialIII" CausesValidation="false" runat="server" MaxLength="20" class="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </asp:Panel>
                        <div class="row">
                            <div class="col-sm-12 height5">
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-4">
                                <div class="row">
                                    <div class="col-sm-4 labelText1">
                                        Description
                                    </div>
                                    <div class="col-sm-8 paddingLeft0 paddingRight0 labelText1">
                                        <asp:Label ID="lblDescription" runat="server" Text=""></asp:Label>
                                    </div>
                                </div>
                            </div>
                            <div class="col-sm-3">
                                <div class="row">
                                    <div class="col-sm-3 labelText1">
                                        Model
                                    </div>
                                    <div class="col-sm-9 labelText1">
                                        <asp:Label ID="lblModel" runat="server" Text=""></asp:Label>
                                    </div>
                                </div>
                            </div>
                            <div class="col-sm-3">
                                <div class="row">
                                    <div class="col-sm-3 labelText1">
                                        Brand
                                    </div>
                                    <div class="col-sm-9 labelText1">
                                        <asp:Label ID="lblBrand" runat="server" Text=""></asp:Label>
                                    </div>
                                </div>
                            </div>
                            <div class="col-sm-2">
                                <div class="row">
                                    <div class="col-sm-4 labelText1">
                                        Part #
                                    </div>
                                    <div class="col-sm-7 labelText1">
                                        <asp:Label ID="lblPart" runat="server" Text=""></asp:Label>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-sm-4">
                        <div class="row">
                            <div class="col-sm-8">
                                <div class="row">
                                    <div class="col-sm-1">
                                    </div>
                                    <div class="col-sm-10 labelText1">
                                    </div>
                                </div>
                            </div>
                            <div class="col-sm-4">
                                <div class="row">
                                    <div class="col-sm-5 labelText1">
                                    </div>
                                    <div class="col-sm-2">
                                        <asp:LinkButton ID="lbtnAdd" runat="server" OnClick="lbtnAdd_Click">
                                    <span class="glyphicon glyphicon-plus" aria-hidden="true"></span>
                                        </asp:LinkButton>
                                    </div>
                                    <div class="col-sm-3">
                                        <asp:LinkButton ID="lbtnClear" runat="server" CssClass="floatRight" OnClick="lbtnClear_Click">
                                    <span class="glyphicon glyphicon-refresh" aria-hidden="true"></span>
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
                            <div class="col-sm-5">
                                <div class="row">
                                    <div class="col-sm-4 labelText1">
                                        Cost
                                    </div>
                                    <div class="col-sm-8 paddingLeft0">
                                        <asp:TextBox ID="txtCost" CausesValidation="false" runat="server" MaxLength="10" class="form-control textAlignRight" onkeypress="return isNumberKey(event)"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="col-sm-7">
                                <div class="row">
                                    <div class="col-sm-3 labelText1">
                                        Batch
                                    </div>
                                    <div class="col-sm-9">
                                        <asp:TextBox ID="txtBatch" CausesValidation="false" runat="server" MaxLength="20" class="form-control"></asp:TextBox>
                                    </div>
                                </div>
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
    </div>--%>
    <div class="col-sm-8">
        <div class="panel panel-default">
            <div class="panel-body">
                <div class="row">
                    <div class="col-sm-4">
                        <div class="row">
                            <div class="col-sm-1">
                                <asp:CheckBox ID="chkLockItem" runat="server" AutoPostBack="true" OnCheckedChanged="chkLockItem_CheckedChanged" />
                            </div>
                            <div class="col-sm-10 labelText1" runat="server" id="lockItem">
                                Lock Item
                            </div>
                        </div>
                    </div>
                    <div class="col-sm-6">
                        <div class="row">
                        </div>
                    </div>
                    <div class="col-sm-2">
                        <div class="row">
                            <div class="col-sm-5 labelText1">
                            </div>
                            <div class="col-sm-2">
                                <asp:LinkButton ID="lbtnAdd" runat="server" OnClick="lbtnAdd_Click">
                                    <span class="glyphicon glyphicon-plus" aria-hidden="true"></span>
                                </asp:LinkButton>
                            </div>
                            <div class="col-sm-3">
                                <asp:LinkButton ID="lbtnClear" runat="server" CssClass="floatRight" OnClick="lbtnClear_Click">
                                    <span class="glyphicon glyphicon-refresh" aria-hidden="true"></span>
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
                    <div class="col-sm-4">
                        <div class="row">
                            <div class="col-sm-5 paddingRight0" runat="server" id="itemCodeDiv">
                                Item code
                                <asp:DropDownList ID="ddlItemType" runat="server" class="form-control" AutoPostBack="true" Visible="false">
                                    <asp:ListItem Text="Item Code" Value="I"></asp:ListItem>
                                    <asp:ListItem Text="Model #" Value="M"></asp:ListItem>
                                    <asp:ListItem Text="Part #" Value="P"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <div class="col-sm-6 paddingRight5">
                                <asp:TextBox ID="txtItemCode" CausesValidation="false" runat="server" MaxLength="20" class="form-control" AutoPostBack="true" OnTextChanged="txtItemCode_TextChanged"></asp:TextBox>
                            </div>
                            <div class="col-sm-1 paddingLeft0">
                                <asp:LinkButton ID="lbtnItemCode" runat="server" OnClick="lbtnItemCode_Click">
                                    <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                </asp:LinkButton>
                            </div>
                        </div>
                    </div>
                    <div class="col-sm-8">
                        <div class="row">
                            <div class="col-sm-4">
                                <div class="row">
                                    <div class="col-sm-4 labelText1">
                                        Bin Code
                                    </div>
                                    <div class="col-sm-7 paddingRight5 paddingLeft5">
                                        <asp:TextBox ID="txtBinCode" CausesValidation="false" runat="server" MaxLength="20" class="form-control" AutoPostBack="true" OnTextChanged="txtBinCode_TextChanged"></asp:TextBox>
                                    </div>
                                    <div class="col-sm-1 paddingLeft0">
                                        <asp:LinkButton ID="lbtnBinCode" runat="server" OnClick="lbtnBinCode_Click">
                                                    <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                        </asp:LinkButton>
                                    </div>
                                </div>
                            </div>
                            <div class="col-sm-4">
                                <div class="row">
                                    <div class="col-sm-3 labelText1">
                                        Status
                                    </div>
                                    <div class="col-sm-9">
                                        <asp:DropDownList ID="ddlStatus" runat="server" class="form-control">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                            <div class="col-sm-4">
                                <div class="row">
                                    <div class="col-sm-3 labelText1">
                                        Qty
                                    </div>
                                    <div class="col-sm-9">
                                        <asp:TextBox ID="txtQty" CausesValidation="false" runat="server" class="form-control textAlignRight" MaxLength="10" onkeypress="return isNumberKey(event)"></asp:TextBox>
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
                    <asp:Panel ID="pnlExpiryDate" runat="server">
                        <div class="col-sm-4">
                            <div class="row">
                                <div class="col-sm-5 labelText1">
                                    Manufacture Date
                                </div>
                                <div class="col-sm-6 paddingRight5">
                                    <asp:TextBox ID="txtManufactureDate" runat="server" CssClass="form-control" onkeypress="return RestrictSpace()"
                                        Format="dd/MMM/yyyy" Enabled="false"></asp:TextBox>
                                </div>
                                <div class="col-sm-1 paddingLeft0 paddingRight0">
                                    <asp:LinkButton ID="lbtnManufactureDate" runat="server" CausesValidation="false">
                                            <span class="glyphicon glyphicon-calendar" aria-hidden="true"></span>
                                    </asp:LinkButton>
                                    <asp:CalendarExtender ID="CalendarExtender3" runat="server" TargetControlID="txtManufactureDate"
                                        PopupButtonID="lbtnManufactureDate" Format="dd/MMM/yyyy">
                                    </asp:CalendarExtender>
                                </div>
                            </div>
                        </div>
                        <div class="col-sm-3">
                            <div class="row">
                                <div class="col-sm-4 labelText1">
                                    Expiry Date
                                </div>
                                <div class="col-sm-7 paddingRight5">
                                    <asp:TextBox ID="txtExpiryDate" runat="server" CssClass="form-control" onkeypress="return RestrictSpace()"
                                        Format="dd/MMM/yyyy" Enabled="false"></asp:TextBox>
                                </div>
                                <div class="col-sm-1 paddingLeft0 paddingRight0">
                                    <asp:LinkButton ID="lbtnExpiryDate" runat="server" CausesValidation="false">
                                            <span class="glyphicon glyphicon-calendar" aria-hidden="true"></span>
                                    </asp:LinkButton>
                                    <asp:CalendarExtender ID="CalendarExtender4" runat="server" TargetControlID="txtExpiryDate"
                                        PopupButtonID="lbtnExpiryDate" Format="dd/MMM/yyyy">
                                    </asp:CalendarExtender>
                                </div>
                            </div>
                        </div>
                    </asp:Panel>
                    <div class="col-sm-3">
                        <div class="row">
                            <div class="col-sm-3 labelText1">
                                Batch
                            </div>
                            <div class="col-sm-9 paddingLeft0 paddingRight0">
                                <asp:TextBox ID="txtBatch" CausesValidation="false" runat="server" MaxLength="20" class="form-control"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="col-sm-2">
                        <div class="row">
                            <div class="col-sm-5 labelText1">
                                Unit cost
                            </div>
                            <div class="col-sm-7 paddingLeft0">
                                <asp:TextBox ID="txtCost" CausesValidation="false" runat="server" MaxLength="10" class="form-control textAlignRight" onkeypress="return isNumberKey(event)"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-sm-12 height5">
                    </div>
                </div>
                <asp:Panel ID="pnlSerialized" runat="server">
                    <div class="row">

                        <div class="col-sm-4">
                            <div class="row">
                                <asp:Panel runat="server" DefaultButton="btna">
                                    <div class="col-sm-3 labelText1">
                                        Serial # I
                                    </div>
                                    <div class="col-sm-9 paddingRight5">
                                        <asp:TextBox ID="txtSerialI" CausesValidation="false" runat="server" MaxLength="65" class="form-control"></asp:TextBox>
                                    </div>
                                    <div class="col-sm-1">
                                        <asp:Button ID="btna" runat="server" OnClick="test_Click" Text="Submit" Style="display: none;" />
                                    </div>
                                </asp:Panel>
                            </div>
                        </div>


                        <div class="col-sm-4">
                            <div class="row">
                                <div class="col-sm-3 labelText1">
                                    Serial # II
                                </div>
                                <div class="col-sm-9 paddingRight5">
                                    <asp:TextBox ID="txtSerialII" CausesValidation="false" runat="server" MaxLength="20" class="form-control"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="col-sm-4">
                            <div class="row">
                                <div class="col-sm-3 labelText1">
                                    Serial # III
                                </div>
                                <div class="col-sm-9">
                                    <asp:TextBox ID="txtSerialIII" CausesValidation="false" runat="server" MaxLength="20" class="form-control"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                    </div>
                </asp:Panel>
                <div class="row">
                    <div class="col-sm-12 height5">
                    </div>
                </div>
                <div class="row">
                    <div class="col-sm-4">
                        <div class="row">
                            <div class="col-sm-3 labelText1">
                                Supplier
                            </div>
                            <div class="col-sm-8">
                                <asp:TextBox ID="txtSupplier" CausesValidation="false" runat="server" MaxLength="20" class="form-control" AutoPostBack="true" OnTextChanged="txtSupplier_TextChanged"></asp:TextBox>
                            </div>
                            <div class="col-sm-1 paddingLeft0">
                                <asp:LinkButton ID="lbtnSupplier" runat="server" OnClick="lbtnSupplier_Click">
                                                    <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                </asp:LinkButton>
                            </div>
                        </div>
                    </div>
                    <div class="col-sm-4">
                        <div class="row">
                            <div class="col-sm-3 labelText1">
                                GRN #
                            </div>
                            <div class="col-sm-8">
                                <asp:TextBox ID="txtGRNNo" CausesValidation="false" runat="server" MaxLength="20" class="form-control" AutoPostBack="true" OnTextChanged="txtGRNNo_TextChanged"></asp:TextBox>
                            </div>
                            <div class="col-sm-1 paddingLeft0">
                                <asp:LinkButton ID="lbtntxtGRNNo" runat="server" OnClick="lbtntxtGRNNo_Click">
                                    <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                </asp:LinkButton>
                            </div>
                        </div>
                    </div>
                    <div class="col-sm-4">
                        <div class="row">
                            <div class="col-sm-3 labelText1">
                                GRN Date
                            </div>
                            <div class="col-sm-5">
                                <asp:TextBox ID="txtGRNDate" runat="server" CssClass="form-control" onkeypress="return RestrictSpace()"
                                    Format="dd/MMM/yyyy" Enabled="false"></asp:TextBox>
                            </div>
                            <div class="col-sm-1 paddingLeft0 paddingRight0">
                                <asp:LinkButton ID="lbtnGRNDate" runat="server" CausesValidation="false">
                                    <span class="glyphicon glyphicon-calendar" aria-hidden="true"></span>
                                </asp:LinkButton>
                                <asp:CalendarExtender ID="CalendarExtender5" runat="server" TargetControlID="txtGRNDate"
                                    PopupButtonID="lbtnGRNDate" Format="dd/MMM/yyyy">
                                </asp:CalendarExtender>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-sm-12 height5">
                    </div>
                </div>
                <div class="row">
                    <div class="col-sm-4">
                        <div class="row">
                            <div class="col-sm-4 labelText1">
                                Description
                            </div>
                            <div class="col-sm-8 paddingLeft0 paddingRight0 labelText1">
                                <asp:Label ID="lblDescription" runat="server" Text="" CssClass="Color1 fontWeight900"></asp:Label>
                            </div>
                        </div>
                    </div>
                    <div class="col-sm-3">
                        <div class="row">
                            <div class="col-sm-3 labelText1">
                                Model
                            </div>
                            <div class="col-sm-9 labelText1">
                                <asp:Label ID="lblModel" runat="server" Text="" CssClass="Color1 fontWeight900"></asp:Label>
                            </div>
                        </div>
                    </div>
                    <div class="col-sm-3">
                        <div class="row">
                            <div class="col-sm-3 labelText1">
                                Brand
                            </div>
                            <div class="col-sm-9 labelText1">
                                <asp:Label ID="lblBrand" runat="server" Text="" CssClass="Color1 fontWeight900"></asp:Label>
                            </div>
                        </div>
                    </div>
                    <div class="col-sm-2">
                        <div class="row">
                            <div class="col-sm-4 labelText1">
                                Part #
                            </div>
                            <div class="col-sm-7 labelText1">
                                <asp:Label ID="lblPart" runat="server" Text="" CssClass="Color1 fontWeight900"></asp:Label>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="col-sm-4 paddingLeft0">
        <div class="panel panel-default">
            <div class="panel-body">
                <div class="col-sm-5 labelText1">
                    Inventory Balance Break-up
                </div>
                <div class="row">
                    <div class="col-sm-12">
                        <div class="row">
                            <div class="col-sm-12 GridScroll155">
                                <asp:GridView ID="grdInventoryBalance" CssClass="table table-hover table-striped" runat="server" GridLines="None" ShowHeaderWhenEmpty="True"
                                    EmptyDataText="No data found..." AutoGenerateColumns="False">
                                    <Columns>
                                        <asp:BoundField DataField="MIS_DESC" HeaderText="Status" ReadOnly="true" />
                                        <asp:BoundField DataField="" HeaderText="Description" ReadOnly="true" Visible="false" />
                                        <asp:BoundField DataField="INL_FREE_QTY" HeaderText="Free Qty" ReadOnly="true" />
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
<asp:UpdatePanel runat="server">
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

<asp:UpdatePanel ID="UpdatePanel16" runat="server">
    <ContentTemplate>
        <asp:Button ID="Button4" runat="server" Text="Button" Style="display: none;" />
        <asp:ModalPopupExtender ID="userPrefixSerial" runat="server" Enabled="True" TargetControlID="Button4"
            PopupControlID="pnlpopupPrefixSerial" CancelControlID="LinkButton4" PopupDragHandleControlID="PopupHeader" Drag="true" BackgroundCssClass="modalBackground">
        </asp:ModalPopupExtender>
    </ContentTemplate>
</asp:UpdatePanel>

<asp:UpdatePanel ID="UpdatePanel17" runat="server">
    <ContentTemplate>
        <asp:Panel runat="server" ID="pnlpopupPrefixSerial">
            <div class="panel panel-default  height200 Dwidth">
                <div class="panel-heading">
                    <asp:LinkButton ID="LinkButton4" runat="server">
                             <span class="glyphicon glyphicon-remove" aria-hidden="true">Close</span>
                    </asp:LinkButton>
                    <%--<span>Commen Search</span>--%>
                    <div class="col-sm-10">
                    </div>
                    <div class="col-sm-1">
                    </div>
                </div>
                <asp:UpdatePanel ID="UpdatePanel18" runat="server">
                    <ContentTemplate>
                        <div class="panel-body">
                            <div class="row">
                                <div class="col-sm-12">
                                    <div class="panel panel-default">
                                        <div class="panel-body">
                                            <div class="row">
                                                <div class="col-sm-3 labelText1">
                                                    PreFix
                                                </div>
                                                <div class="col-sm-5">
                                                    <asp:DropDownList ID="ddlPreFix" CausesValidation="false" runat="server" CssClass="form-control">
                                                        <asp:ListItem Text=" - - Select - - " Value="0" Selected="True"></asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:TextBox runat="server" ID="txtFix" Enabled="false" Visible="false" CausesValidation="false" CssClass="form-control"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-12 height5">
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-3 labelText1">
                                                    No of pages
                                                </div>
                                                <div class="col-sm-4">
                                                    <asp:TextBox runat="server" ID="txtNoOfPages" Enabled="false" CausesValidation="false" CssClass="form-control"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-12 height5">
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-3 labelText1">
                                                    Start Page #
                                                </div>
                                                <div class="col-sm-4">
                                                    <asp:TextBox runat="server" ID="txtStartPage" CausesValidation="false" onkeypress="return isNumberKey(event)" CssClass="form-control" OnTextChanged="txtStartPage_TextChanged"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-12 height5">
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-3 labelText1">
                                                    last Page #
                                                </div>
                                                <div class="col-sm-4">
                                                    <asp:TextBox runat="server" ID="txtlastPages" Enabled="false" CausesValidation="false" CssClass="form-control"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-12 height10">
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-4">
                                                    <asp:LinkButton ID="lbtnSavePreFix" CausesValidation="false" runat="server" CssClass="floatRight" OnClick="lbtnSavePreFix_Click">
                                                            <span class="glyphicon glyphicon-saved" aria-hidden="true"></span>Save
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
        </asp:Panel>

    </ContentTemplate>
</asp:UpdatePanel>


<asp:UpdatePanel ID="UpdatePanel14" runat="server">
    <ContentTemplate>
        <asp:Button ID="Button2" runat="server" Text="Button" Style="display: none;" />
        <asp:ModalPopupExtender ID="userSubSerial" runat="server" Enabled="True" TargetControlID="Button2"
            PopupControlID="pnlpopupSubSerial" CancelControlID="LinkButton3" PopupDragHandleControlID="PopupHeader" Drag="true" BackgroundCssClass="modalBackground">
        </asp:ModalPopupExtender>
    </ContentTemplate>
</asp:UpdatePanel>


<asp:UpdatePanel ID="UpdatePanel5" runat="server">
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
