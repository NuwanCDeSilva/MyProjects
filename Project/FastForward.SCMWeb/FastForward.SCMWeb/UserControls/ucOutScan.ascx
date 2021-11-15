<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucOutScan.ascx.cs" Inherits="FastForward.SCMWeb.UserControls.ucOutScan" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<%--<link href="../Css/bootstrap.css" rel="stylesheet" />
<link href="../Css/style.css" rel="stylesheet" />--%>

 <script src="<%=Request.ApplicationPath%>Js/jquery.min.js"></script>
    <script src="<%=Request.ApplicationPath%>Js/bootstrap.min.js"></script>
    <script src="<%=Request.ApplicationPath%>Js/jquery.toastmessage.js"></script>

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


</script>
<asp:UpdateProgress ID="UpdateProgress1" DisplayAfter="10"
        runat="server" AssociatedUpdatePanelID="pnlMain">

        <ProgressTemplate>
            <div class="divWaiting">
                <asp:Label ID="lblWait" runat="server"
                    Text="Please wait... " />
                <asp:Image ID="imgWait" runat="server"
                    ImageAlign="Middle" ImageUrl="~/images/icons/ajax-loader.gif" />
            </div>
        </ProgressTemplate>

    </asp:UpdateProgress>
<div class="row">
     
    <asp:UpdatePanel runat="server" ID="pnlMain">
        <ContentTemplate>
    <div class="col-sm-8" style="padding-right: 5px">
        <div class="panel panel-default">
            <div class="panel-body" style="padding: 0">

                <asp:Panel ID="pnlPDA" runat="server" Visible="false">
                    <div class="row">
                        <div class="col-sm-4">
                            <div class="row">
                                <div class="col-sm-1">
                                    <asp:CheckBox ID="CheckBox1" runat="server" AutoPostBack="true" OnCheckedChanged="chkDirectScan_CheckedChanged" Visible="false" />
                                </div>
                                <div class="col-sm-10 labelText1">
                                    <%-- Direct Scan--%>
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
                                    <asp:LinkButton ID="lbtnAdd_PDA" runat="server" OnClick="lbtnAdd_PDA_Click">
                                    <span class="glyphicon glyphicon-plus" aria-hidden="true"></span>
                                    </asp:LinkButton>
                                </div>
                                <div class="col-sm-3">
                                    <asp:LinkButton ID="LinkButton2" runat="server" CssClass="floatRight" OnClick="lbtnClear_Click">
                                    <span class="glyphicon glyphicon-refresh" aria-hidden="true"></span>
                                    </asp:LinkButton>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-sm-4">
                            <div class="row">
                                <div class="col-sm-2" >
                                    Item Code
                                </div>

                                <div class="col-sm-8 paddingRight5">
                                    <asp:TextBox ID="txtPDAItemcode" CausesValidation="false" runat="server" MaxLength="20" class="form-control" AutoPostBack="true" OnTextChanged="txtItemCode_TextChanged" Style="text-transform: uppercase;"></asp:TextBox>
                                </div>
                                <div class="col-sm-1 paddingLeft0">
                                    <asp:LinkButton ID="lbtnPDAItemcode" runat="server" OnClick="lbtnPDAItemcode_Click">
                                    <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                    </asp:LinkButton>
                                </div>
                            </div>
                        </div>
                        <div class="col-sm-8">
                            <div class="row">
                                <div class="col-sm-5">
                                    <div class="row">
                                        <div class="col-sm-4 labelText1">
                                            Bin Code
                                        </div>
                                        <div class="col-sm-8 paddingLeft5">
                                            <asp:DropDownList ID="ddlBinCode_PDA" runat="server" class="form-control">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-sm-4">
                                    <div class="row">
                                        <div class="col-sm-4 labelText1">
                                            Status
                                        </div>
                                        <div class="col-sm-8">
                                            <asp:DropDownList ID="ddlStatus_PDA" runat="server" class="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlStatus_PDA_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-sm-2">
                                    <div class="row">
                                        <div class="col-sm-3 labelText1">
                                            Qty
                                        </div>
                                        <div class="col-sm-8">
                                            <asp:TextBox ID="txtQty_PDA" CausesValidation="false" runat="server" class="form-control textAlignRight" AutoPostBack="true" OnTextChanged="txtQty_PDA_TextChanged"
                                                MaxLength="10" onkeypress="return isNumberKey(event)"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>

                            </div>
                        </div>
                    </div>
                </asp:Panel>
                <asp:Panel ID="Defaul" runat="server" Visible="true">
                    <div class="row">
                        <div class="col-sm-4">
                            <div class="row">
                                <div class="col-sm-1">
                                    <asp:CheckBox ID="chkDirectScan" runat="server" AutoPostBack="true" OnCheckedChanged="chkDirectScan_CheckedChanged" Visible="false" />
                                </div>
                                <div class="col-sm-10 labelText1">
                                    <%-- Direct Scan--%>
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
                        <div class="col-sm-12">
                            
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-sm-4">
                            <div class="row">
                                <div class="col-sm-3 paddingRight0" runat="server" id="divItemCodeDiv" >
                                    Item code
                                <asp:DropDownList ID="ddlItemType" runat="server" class="form-control" AutoPostBack="true" Visible="false">
                                    <asp:ListItem Text="Item Code" Value="I"></asp:ListItem>
                                    <asp:ListItem Text="Model #" Value="M"></asp:ListItem>
                                    <asp:ListItem Text="Part #" Value="P"></asp:ListItem>
                                </asp:DropDownList>
                                </div>

                                <div class="col-sm-8 paddingRight5">
                                    <asp:TextBox ID="txtItemCode" CausesValidation="false" runat="server" MaxLength="20" class="form-control" AutoPostBack="true" OnTextChanged="txtItemCode_TextChanged" Style="text-transform: uppercase;"></asp:TextBox>
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
                                <div class="col-sm-12">
                                    <div class="col-sm-4">
                                        <div class="row">
                                            <div class="col-sm-4 labelText1">
                                                Bin Code
                                            </div>
                                            <div class="col-sm-8 paddingLeft5">
                                                <asp:DropDownList ID="ddlBinCode" runat="server" class="form-control">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-4 padding0">
                                        <div class="row">
                                            <div class="col-sm-4 labelText1">
                                                Status
                                            </div>
                                            <div class="col-sm-8">
                                                <asp:DropDownList ID="ddlStatus" runat="server" class="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlStatus_SelectedIndexChanged">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-4 paddingleft0">
                                        <div class="row">
                                            <div class="col-sm-2  labelText1">
                                                Qty
                                            </div>
                                            <div class="col-sm-6">
                                                <asp:TextBox ID="txtQty" CausesValidation="false" runat="server" class="form-control textAlignRight"
                                                    AutoPostBack="true" OnTextChanged="txtQty_TextChanged"
                                                    MaxLength="10" onkeypress="return isNumberKey(event)"></asp:TextBox>
                                            </div>
                                            <div class="col-sm-1 padding0">
                                                <asp:CheckBox Text="" ID="chkOnlyQty" AutoPostBack="true" OnCheckedChanged="chkOnlyQty_CheckedChanged" runat="server" />
                                            </div>
                                            <div class="col-sm-3 padding0">
                                                <asp:Label Text="Only Qty" runat="server" />
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
                    <asp:Panel ID="pnlTobechange" runat="server">
                        <div class="row">
                            <div class="col-sm-4">
                                <div class="row">
                                    <div class="col-sm-5 labelText1">
                                        Item to be change
                                    </div>
                                    <div class="col-sm-6 paddingRight5">
                                        <asp:TextBox ID="txtItemToBeChange" CausesValidation="false" runat="server" MaxLength="20" class="form-control" AutoPostBack="true" OnTextChanged="txtItemToBeChange_TextChanged"></asp:TextBox>
                                    </div>
                                    <div class="col-sm-1 paddingLeft0">
                                        <asp:LinkButton ID="lbtnItemToBeChange" runat="server" OnClick="lbtnItemToBeChange_Click">
                                    <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                        </asp:LinkButton>
                                    </div>
                                </div>
                            </div>
                            <div class="col-sm-4">
                                <div class="row">
                                    <div class="col-sm-5 labelText1" style="color: red">
                                        Status to be change
                                    </div>
                                    <div class="col-sm-7 paddingLeft0">
                                        <asp:DropDownList ID="ddlStatusToBeChange" runat="server" class="form-control">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                            <div class="col-sm-4">
                                <div class="row">
                                    <div class="col-sm-3 labelText1">
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-12 height5">
                            </div>
                        </div>
                    </asp:Panel>
                    <div class="row" id="divExpiryDate" runat="server">
                        <div class="col-sm-4">
                            <div class="row">
                                <div class="col-sm-5 labelText1">
                                    <%--Manufacture Date--%>
                                </div>
                                <div class="col-sm-7">
                                    <asp:DropDownList ID="ddlManufactureDate" runat="server" class="form-control" Visible="false">
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                        <div class="col-sm-4">
                            <div class="row">
                                <div class="col-sm-5 labelText1">
                                    <%--Expiry Date--%>
                                </div>
                                <div class="col-sm-7 paddingLeft0">
                                    <asp:DropDownList ID="ddlExpiryDate" runat="server" class="form-control" Visible="false">
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                        <div class="col-sm-4">
                            <div class="row">
                                <div class="col-sm-3 labelText1">
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
                            <asp:UpdatePanel runat="server">
                                <ContentTemplate>
                                    <div class="col-sm-4">
                                        <div class="row">
                                            <div class="col-sm-3 labelText1">
                                                Serial # I
                                            </div>
                                            <div class="col-sm-8 paddingRight5">
                                                <asp:Panel runat="server" DefaultButton="lbtnSerClick">
                                                    <asp:TextBox ID="txtSerialI" CausesValidation="false" runat="server" class="form-control"
                                                    AutoPostBack="false"></asp:TextBox>
                                                 <asp:LinkButton ID="lbtnSerClick" runat="server" OnClick="lbtnSerClick_Click" style="display:none;">
                                                        <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                </asp:LinkButton>
                                                </asp:Panel>
                                            </div>
                                            <div class="col-sm-1 paddingLeft0">
                                                <asp:LinkButton ID="lbtnSerialI" runat="server" OnClick="lbtnSerialI_Click" >
                                    <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                </asp:LinkButton>
                                            </div>
                                              <div class="col-sm-1 paddingLeft0">
                                                <asp:LinkButton ID="lbseradd" runat="server" OnClick="lbseradd_Click" Visible="false">
                                    <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                </asp:LinkButton>
                                            </div>
                                        </div>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                            <div class="col-sm-4">
                                <div class="row">
                                    <div class="col-sm-3 labelText1">
                                        Serial # II
                                    </div>
                                    <div class="col-sm-9" style="padding-left: 0;">
                                        <asp:TextBox ID="txtSerialII" CausesValidation="false" runat="server" class="form-control"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="col-sm-4">
                                <div class="row">
                                    <div class="col-sm-3 labelText1">
                                        Serial # III
                                    </div>
                                    <div class="col-sm-9">
                                        <asp:TextBox ID="txtSerialIII" CausesValidation="false" runat="server" class="form-control"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </asp:Panel>
                    <div class="row">
                        <div class="col-sm-12 height5">
                        </div>
                    </div>

                </asp:Panel>
                <asp:Panel runat="server" Visible="false" ID="pnlPkg">
                    <div class="row">
                        <div class="col-sm-12">
                            <div class="col-sm-1 padding0 labelText1">
                               Package Type  
                            </div>
                            <div class="col-sm-2 padding0">
                                <asp:DropDownList class="form-control" AutoPostBack="true" runat="server" ID="ddlBoxTp" OnSelectedIndexChanged="ddlBoxTp_SelectedIndexChanged">
                                </asp:DropDownList>
                            </div>
                            <div class="col-sm-2 labelText1">
                                Package Quantity
                            </div>
                            <div class="col-sm-2 padding0 labelText1">
                                <asp:Label Text="" ID="lblPkgQty" runat="server" />
                            </div>
                        </div>
                    </div>
                </asp:Panel>
                <div class="row">
                    <div class="col-sm-4">
                        <div class="row">
                            <div class="col-sm-4 labelText1 fontWeight900">
                                Description :
                            </div>
                            <div class="col-sm-8 paddingLeft0 paddingRight0 labelText1">
                                <asp:Label ID="lblDescription" runat="server" Text="" CssClass="Color1 fontWeight900"></asp:Label>
                            </div>
                        </div>
                    </div>
                    <div class="col-sm-3">
                        <div class="row">
                            <div class="col-sm-4 labelText1 fontWeight900">
                                Model :
                            </div>
                            <div class="col-sm-8 labelText1">
                                <asp:Label ID="lblModel" runat="server" Text="" CssClass="Color1 fontWeight900"></asp:Label>
                            </div>
                        </div>
                    </div>
                    <div class="col-sm-3">
                        <div class="row">
                            <div class="col-sm-4 labelText1 fontWeight900 ">
                                Brand :
                            </div>
                            <div class="col-sm-8 labelText1">
                                <asp:Label ID="lblBrand" runat="server" Text="" CssClass="Color1 fontWeight900"></asp:Label>
                            </div>
                        </div>
                    </div>
                    <div class="col-sm-2">
                        <div class="row">
                            <div class="col-sm-5 labelText1 fontWeight900">
                                Part # :
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
                    <div class="col-sm-12" style="padding-right: 30px">
                        <div class="row">
                            <div class="col-sm-12 panelscollbar height100">
                                <asp:GridView ID="grdInventoryBalance" CssClass="table table-hover table-striped" OnSelectedIndexChanged="grdInventoryBalance_SelectedIndexChanged" runat="server" GridLines="None" ShowHeaderWhenEmpty="True"
                                    EmptyDataText="No data found..." AutoGenerateColumns="False" OnRowDataBound="DrugDetailGridView_RowDataBound">
                                    <Columns>
                                        <asp:ButtonField Text="Select" CommandName="Select" ItemStyle-Width="10" Visible="false" />
                                        <asp:BoundField DataField="MIS_DESC" HeaderText="Status" ReadOnly="true" />
                                        <asp:BoundField DataField="" HeaderText="Description" ReadOnly="true" Visible="false" />
                                        <asp:BoundField DataField="INL_FREE_QTY" HeaderText="Free Qty" ReadOnly="true" />
                                        <asp:BoundField DataField="INL_ITM_STUS" HeaderText="Free Qty" ReadOnly="false" Visible="false" />
                                        <asp:TemplateField HeaderText="#" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="INL_ITM_STUS" runat="server" Text='<%# Bind("INL_ITM_STUS") %>' Width="5px"></asp:Label>
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
            </ContentTemplate>
    </asp:UpdatePanel>
</div>

<asp:UpdatePanel runat="server">
    <ContentTemplate>
        <asp:Button ID="btnhidden2" runat="server" Text="Button" Style="display: none;" />
        <asp:ModalPopupExtender ID="Userpopup2" runat="server" Enabled="True" TargetControlID="btnhidden2"
            PopupControlID="pnlModalPopup2" PopupDragHandleControlID="PopupHeader2" Drag="true" BackgroundCssClass="modalBackground">
        </asp:ModalPopupExtender>

    </ContentTemplate>
</asp:UpdatePanel>

<asp:UpdatePanel ID="UpdatePanel3" runat="server">
    <ContentTemplate>
        <asp:Panel runat="server" ID="pnlModalPopup2" DefaultButton="lbtnSearch">
            <div runat="server" id="Div2" class="panel panel-primary Mheight">

                <asp:Label ID="Label1" runat="server" Text="Label" Visible="false"></asp:Label>
                <div class="panel panel-default">
                    <div class="panel-heading">
                        <asp:LinkButton ID="btnclose2" runat="server" OnClick="btnclose2_Click">
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
                                        <asp:Panel runat="server" ID="Panel2">

                                            <div class="col-sm-12">

                                                <div class="panel panel-default">

                                                    <div class="panel-heading">
                                                        <strong>
                                                            <asp:Label ID="Label2" runat="server"></asp:Label>
                                                        </strong>
                                                    </div>

                                                    <div class="panel-body">
                                                        <div class="row">
                                                            <div class="col-md-3">
                                                                <div class="col-md-9">Sellect All</div>
                                                                  <div class="col-md-3"><asp:CheckBox runat="server" ID="chkall" OnCheckedChanged="chkall_CheckedChanged"  AutoPostBack="true" /></div>
                                                            </div>
                                                            <div class="col-md-3">
                                                                <asp:Button ID="btnaddserall" CssClass="btn-default" Text="Add" OnClick="btnaddserall_Click" runat="server"/>
                                                            </div>
                                                        </div>
                                                        <div class="row">

                                                            <div id="Div3" runat="server">
                                                                <div class="col-sm-12">

                                                                    <div class="panelscoll" style="height:300px">

                                                                        <asp:GridView ID="grdserdata" AutoGenerateColumns="false" runat="server" CssClass="table table-hover table-striped" GridLines="None" ShowHeaderWhenEmpty="True" EmptyDataText="No data found...">

                                                                            <Columns>
                                                                                 <asp:TemplateField HeaderText="">
                                                                                    <ItemTemplate>
                                                                                        <asp:CheckBox ID="chkser" runat="server" Width="10px"></asp:CheckBox>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>

                                                                                <asp:TemplateField HeaderText="Serial 1">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lbser1" runat="server" Text='<%# Bind("Serial1") %>' Width="100px"></asp:Label>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>

                                                                                <asp:TemplateField HeaderText="Serial 2">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lbser2" runat="server" Text='<%# Bind("Serial2") %>' Width="100px"></asp:Label>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>

                                                                                <asp:TemplateField HeaderText="Item Status">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lbitmstatus" runat="server" Text='<%# Bind("ItemStatus") %>' Width="50px"></asp:Label>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>

                                                                                  <asp:TemplateField HeaderText="Warranty No">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lbwarrno" runat="server" Text='<%# Bind("WarrantyNo") %>' Width="100px"></asp:Label>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>

                                                                                  <asp:TemplateField HeaderText="Doc No">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblstuscodetext" runat="server" Text='<%# Bind("DocNo") %>' Width="100px"></asp:Label>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>

                                                                                 <asp:TemplateField HeaderText="Doc Date">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lbdocno" runat="server" Text='<%# Bind("DocDate") %>' Width="100px"></asp:Label>
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
    </ContentTemplate>
</asp:UpdatePanel>



<asp:UpdatePanel runat="server">
    <ContentTemplate>
        <asp:Button ID="btnhidden" runat="server" Text="Button" Style="display: none;" />
        <asp:ModalPopupExtender ID="UserPopup" runat="server" Enabled="True" TargetControlID="btnhidden"
            PopupControlID="pnlModalPopup" PopupDragHandleControlID="PopupHeader" Drag="true" BackgroundCssClass="modalBackground">
        </asp:ModalPopupExtender>

    </ContentTemplate>
</asp:UpdatePanel>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <asp:Panel runat="server" ID="pnlModalPopup" DefaultButton="lbtnSearch">
            <div runat="server" id="test" class="panel panel-default height400 width700 setpossition">
                <asp:Label ID="lblvalue" runat="server" Text="Label" Visible="false"></asp:Label>
                <div class="panel panel-default">
                    <div id="PopupHeader" class="panel-heading">
                        <asp:LinkButton ID="btnClose" runat="server" OnClick="btnClose_Click">
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
                                <asp:UpdatePanel runat="server">
                                    <ContentTemplate>
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
                                    </ContentTemplate>
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
<asp:UpdateProgress ID="UpdateProgress2" DisplayAfter="10"
        runat="server" AssociatedUpdatePanelID="UpdatePanel1">
        <ProgressTemplate>
            <div class="divWaiting">
                <asp:Label ID="lblWait5" runat="server"
                    Text="Please wait... " />
                <asp:Image ID="imgWait5" runat="server"
                    ImageAlign="Middle" ImageUrl="~/images/banners/hidden_progress_bar.gif" />
            </div>
        </ProgressTemplate>

    </asp:UpdateProgress>

<asp:UpdatePanel runat="server">
    <ContentTemplate>
        <asp:Button ID="btnexcel" runat="server" Text="Button" Style="display: none;" />
        <asp:ModalPopupExtender ID="mpexcel" runat="server" Enabled="True" TargetControlID="btnexcel"
            PopupControlID="pnlpopupExcel" PopupDragHandleControlID="PopupHeaderExcel" Drag="true" BackgroundCssClass="modalBackground">
        </asp:ModalPopupExtender>

    </ContentTemplate>
</asp:UpdatePanel>
<asp:UpdatePanel ID="UpdatePanel2" runat="server">
    <ContentTemplate>
        <asp:Panel runat="server" ID="pnlpopupExcel" DefaultButton="lbtnSearch">
            <div runat="server" id="Div1" class="panel panel-primary Mheight">

                <asp:Label ID="Label3" runat="server" Text="Label" Visible="false"></asp:Label>
                <div class="panel panel-default">
                    <div class="panel-heading">
                        <asp:LinkButton ID="LinkButton8" runat="server" OnClick="LinkButton8_Click">
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

                                                            <div id="dvitm" runat="server">
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

                                                                                <asp:TemplateField HeaderText="Status" Visible="false">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblstuscode" runat="server" Text='<%# Bind("ins_itm_stus") %>' Width="1px"></asp:Label>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>

                                                                                <asp:TemplateField HeaderText="Status">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblstuscodetext" runat="server" Text='<%# Bind("ins_itm_stus") %>' Width="100px"></asp:Label>
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
    </ContentTemplate>
</asp:UpdatePanel>

