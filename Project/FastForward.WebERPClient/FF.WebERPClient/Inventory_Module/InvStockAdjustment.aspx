<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
CodeBehind="InvStockAdjustment.aspx.cs" Inherits="FF.WebERPClient.InvStockAdjustment" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/UserControls/uc_MsgInfo.ascx" TagName="uc_MsgInfo" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
<script type="text/javascript">
function numbersonly(e, decimal) {
var key;
var keychar;
if (window.event) { key = window.event.keyCode;}else if (e) {key = e.which;}else {return true;} keychar = String.fromCharCode(key);
if ((key == null) || (key == 0) || (key == 8) || (key == 9) || (key == 13) || (key == 27)) {return true;}
else if ((("0123456789").indexOf(keychar) > -1)) {return true;} else if (decimal && (keychar == ".")) { return true; } else return false;}
function LoadPopUp() { document.getElementById('<%=HiddenFieldCusCrePopUpStats.ClientID %>').value = "1"; }
function ClosePopUp() { document.getElementById('<%=HiddenFieldCusCrePopUpStats.ClientID %>').value = "0"; }
function SelectAll(Id) { var myform = document.forms[0]; var len = myform.elements.length; document.getElementById(Id).checked == true ? document.getElementById(Id).checked = false : document.getElementById(Id).checked = true; for (var i = 0; i < len; i++) { if (myform.elements[i].type == 'checkbox') { if (myform.elements[i].checked) { myform.elements[i].checked = false; } else { myform.elements[i].checked = true; } } } }
function clickButton(e, buttonid) { var evt = e ? e : window.event; var bt = document.getElementById(buttonid); if (bt) { if (evt.keyCode == 113) { bt.click(); return false; } } }
function ToUpper(ctrl) { var t = ctrl.value; ctrl.value = t.toUpperCase(); } function ToLower(ctrl) { var t = ctrl.value; ctrl.value = t.toLowerCase(); }
function DeleteConfirm() { if (confirm("Are you sure to delete?")) { return true; } else { return false; } }
</script>
<style type="text/css"> .style1 { font-family: Verdana; font-size: 11px; margin-left: 0px; } .style4 { width: 99%; } .style12 { width: 217px; height: 22px; } .style16 { color: black; font-size: 11px; font-family: Verdana; } .style17 { width: 139px; }  .style18 { width: 226px; } .style19 { width: 113px; }  .style20 { width: 261px; } .style22 { width: 6px; height: 22px; } .style28 { width: 426px; height: 22px; }  .style30 { height: 22px; } .style31 { width: 98px; height: 22px; } .style32 { width: 125px; height: 22px; } .style33 { width: 39px; }  .GridView { margin-left: 0px; } </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<div style="text-align: right; width: 100%; height: 18px;" class="PanelHeader invcollapsovrid">
<asp:Button ID="btnFinalSave0" runat="server" CssClass="Button" OnClick="btnFinalSave_Click"
Text="Save Adjustment" ValidationGroup="newScan" />
&nbsp;
<asp:Button ID="ButtonAddCost" runat="server" CssClass="Button" OnClientClick="LoadPopUp()"
Text="Add Cost" Visible="false" />
&nbsp;&nbsp;<asp:Button ID="btnCancel" runat="server" CssClass="Button" OnClick="btnCancel_Click"
Text="Clear" Width="83px" />
&nbsp;
<br />
</div>
<div style="float: left; width: 100%;">
<div style="float: left; width: 100%;">
<table class="style4">
<tr>
<td class="style17">
<span class="style16"><span class="style1">Reasent Scan Batches :</span></span>
</td>
<td class="style18">
<asp:DropDownList ID="ddlScanBatches" runat="server" AutoPostBack="True" CssClass="ComboBox"
Height="20px" OnSelectedIndexChanged="DropDownList3_SelectedIndexChanged" Width="115px">
</asp:DropDownList>
<asp:Label ID="lblBatchSeq" runat="server" CssClass="style16" Text="Label" Visible="False"></asp:Label>
</td>
<td class="style19">
<asp:Label ID="Label19" runat="server" CssClass="style16" Font-Bold="False" ForeColor="Black"
Text="Adjustment Type :"></asp:Label>
</td>
<td class="style20">
<asp:DropDownList ID="ddlAdjType_" runat="server" AutoPostBack="True" CssClass="ComboBox"
Height="18px" OnSelectedIndexChanged="ddlAdjType__SelectedIndexChanged" Width="98px">
<asp:ListItem>+ADJ</asp:ListItem>
<asp:ListItem>- ADJ</asp:ListItem>
<asp:ListItem Selected="True"></asp:ListItem>
</asp:DropDownList>
<asp:Label ID="lblDirect" runat="server" CssClass="style16" Text="Label" Visible="False"></asp:Label>
</td>
<td class="style33">
&nbsp; &nbsp;
</td>
<td style="text-align: right">
<asp:Button ID="btnSerialScan" runat="server" CssClass="Button" Font-Bold="True"
OnClick="btnSerialScan_Click" Style="margin-left: 0px; font-weight: 400; text-align: right;"
Text="New Serial Scan" Width="108px" />
</td>
</tr>
</table>
</div>
<div style="text-align: left; width: 100%; height: 15px;" class="PanelHeader invcollapsovrid">
General Detail
</div>
<div style="float: left; width: 100%; color: Black;">
<div>
<table class="style4">
<tr>
<td class="style31">
<asp:Label ID="Label16" runat="server" Font-Bold="True" Text="Date"></asp:Label>
</td>
<td class="style30">
<asp:TextBox ID="txtDate_" runat="server" CssClass="TextBox" Enabled="False" Width="142px"></asp:TextBox>
<asp:CalendarExtender ID="txtDate__CalendarExtender" runat="server" Animated="true"
EnabledOnClient="true" Format="dd/MM/yyyy" PopupPosition="BottomLeft" TargetControlID="txtDate_">
</asp:CalendarExtender>
<asp:RequiredFieldValidator ID="RequiredFieldValidator17" runat="server" ControlToValidate="txtDate_"
ErrorMessage="*" ForeColor="Red" ValidationGroup="newScan"></asp:RequiredFieldValidator>
</td>
<td class="style22">
&nbsp;
</td>
<td class="style32">
<asp:Label ID="Label21" runat="server" Text="Adjustment Sub Type"></asp:Label>
</td>
<td class="style28">
<asp:DropDownList ID="ddlAdjSubTyepe" runat="server" AutoPostBack="True" CssClass="ComboBox"
OnSelectedIndexChanged="ddlAdjSubTyepe_SelectedIndexChanged" Width="99px">
<asp:ListItem></asp:ListItem>
</asp:DropDownList>
&nbsp;<asp:Label ID="lblSubTpDesc" runat="server"></asp:Label>
</td>
<td class="style30">
&nbsp;
</td>
</tr>
<tr>
<td class="style31">
<asp:Label ID="Label3" runat="server" Text="Manual Ref. No "></asp:Label>
</td>
<td class="style12">
<asp:TextBox ID="txtManualRefNo" runat="server" CssClass="TextBox" Width="142px"
MaxLength="30"></asp:TextBox>
</td>
<td class="style22">
</td>
<td class="style32">
<asp:Label ID="Label22" runat="server" Text="Adjustment Based" Visible="False"></asp:Label>
</td>
<td class="style28">
<asp:DropDownList ID="ddlAdjBased" runat="server" CssClass="ComboBox" Height="23px"
Visible="False" Width="99px">
<asp:ListItem></asp:ListItem>
<asp:ListItem>PHYSICAL</asp:ListItem>
<asp:ListItem>DOCUMENT</asp:ListItem>
</asp:DropDownList>
&nbsp;<asp:Label ID="lblNewScanDirect" runat="server" CssClass="style16" Visible="False"></asp:Label>
</td>
<td class="style30">
&nbsp;
</td>
</tr>
<tr>
<td class="style31">
<asp:Label ID="Label18" runat="server" Text="Remarks"></asp:Label>
</td>
<td class="style30" colspan="5">
<asp:TextBox ID="txtRemarks" runat="server" CssClass="TextBox" MaxLength="30" Width="100%"></asp:TextBox>
</td>
</tr>
</table>
</div>
</div>
<div style="text-align: left; width: 100%; height: 15px;" class="PanelHeader invcollapsovrid">
Item Detail
</div>
<div style="float: left; width: 100%; height: 44px; color: Black;">
<div style="float: left; width: 100%; padding-top: 2px;">
<div style="float: left; width: 1%;">
&nbsp;</div>
<div style="float: left; width: 5%;">
Item
</div>
<div style="float: left; width: 25%;">
<asp:TextBox ID="txtItem" runat="server" CssClass="TextBoxUpper" Width="80%" onchange="ToUpper(this)"></asp:TextBox>
<asp:ImageButton ID="imgBtnItem" runat="server" ImageUrl="~/Images/icon_search.png"
ImageAlign="Middle" OnClick="imgBtnItem_Click" />
</div>
<div style="float: left; width: 5%;">
&nbsp;</div>
<div style="float: left; width: 5%;">
Status</div>
<div style="float: left; width: 10%;">
<asp:DropDownList ID="ddlStatus" runat="server" Width="100%" CssClass="ComboBox">
</asp:DropDownList>
</div>
<div style="float: left; width: 5%;">
&nbsp;</div>
<div style="float: left; width: 3%;">
Qty</div>
<div style="float: left; width: 10%;">
<asp:TextBox ID="txtQty" runat="server" CssClass="TextBox" Width="100%" onKeyPress="return numbersonly(event,false)"></asp:TextBox>
</div>
<div style="float: left; width: 5%;">
&nbsp;</div>
<div style="float: left; width: 3%;">
Unit Cost</div>
<div style="float: left; width: 10%;">
<asp:TextBox ID="txtUnitCost" runat="server" CssClass="TextBox" Width="100%" onKeyPress="return numbersonly(event,false)"></asp:TextBox>
</div>
<div style="float: left; width: 3%;">
&nbsp;<asp:ImageButton ID="imgBtnAddItem" runat="server" ImageAlign="Middle" ImageUrl="~/Images/download_arrow_icon.png"
Width="16px" Height="16px" OnClick="AddItem" />
</div>
</div>
<div style="float: left; width: 100%; padding-top: 2px;">
<div style="float: left; width: 1%;">
&nbsp;</div>
<div style="float: left; width: 90%;">
<asp:TextBox ID="lblModel" ForeColor="BlueViolet" runat="server" Font-Size="11px"
BorderColor="White" BorderWidth="0px" Width="100%"></asp:TextBox>
</div>
</div>
</div>
<div style="height: 9px; float: left; width: 100%;">
&nbsp;&nbsp;</div>
<div style="float: left; width: 100%;">
<asp:TabContainer ID="tbContainer" runat="server" Width="100%" Height="161px" ActiveTabIndex="0">
<asp:TabPanel ID="tpItem" HeaderText="Item " runat="server">
<ContentTemplate>
<asp:Panel ID="PanelGridItm" runat="server" ScrollBars="Auto">
<asp:GridView ID="gvItem" runat="server" CellPadding="3" ForeColor="#333333" AutoGenerateColumns="False"
Width="100%" OnSelectedIndexChanged="gvItem_SelectedIndexChanged" ShowHeaderWhenEmpty="True"
EmptyDataText="No Data" OnRowDataBound="BindItem" OnRowDeleting="OnRemoveFromItemGrid"
PageSize="12" CssClass="GridView" DataKeyNames="Tui_req_itm_cd,Tui_req_itm_stus,Tui_req_itm_qty,Tui_pic_itm_cd">
<AlternatingRowStyle BackColor="White" />
<Columns>
<asp:BoundField DataField="Tui_req_itm_cd" HeaderText="Item" />
<asp:TemplateField>
<HeaderTemplate>
Description
</HeaderTemplate>
<ItemTemplate>
<asp:Label runat="server" ID="lblgvItemDesc" Text=""></asp:Label>
</ItemTemplate>
</asp:TemplateField>
<asp:TemplateField>
<HeaderTemplate>
Model
</HeaderTemplate>
<ItemTemplate>
<asp:Label runat="server" ID="lblgvItemModel" Text=""></asp:Label>
</ItemTemplate>
</asp:TemplateField>
<asp:BoundField DataField="Tui_req_itm_stus" HeaderText="Status" />
<asp:BoundField DataField="Tui_req_itm_qty" HeaderText="Req Qty" />
<asp:BoundField DataField="Tui_pic_itm_qty" HeaderText="Pick Qty" />
<asp:BoundField DataField="Tui_pic_itm_stus" HeaderText="Unit Cost" />
<asp:CommandField ButtonType="Image" ShowSelectButton="True" SelectImageUrl="~/Images/Add-16x16x16.ICO" />
<asp:TemplateField>
<ItemTemplate>
<asp:ImageButton ID="imgBtnRemoveItem" runat="server" ImageAlign="Middle" ImageUrl="~/Images/Delete.png"
Width="11px" Height="11px" OnClientClick="return confirm('Do you want to delete?');"
CommandName="Delete" />
</ItemTemplate>
</asp:TemplateField>
</Columns>
<EditRowStyle BackColor="#2461BF" />
<FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
<HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
<PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
<RowStyle BackColor="#EFF3FB" />
<SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
<SortedAscendingCellStyle BackColor="#F5F7FB" />
<SortedAscendingHeaderStyle BackColor="#6D95E1" />
<SortedDescendingCellStyle BackColor="#E9EBEF" />
<SortedDescendingHeaderStyle BackColor="#4870BE" />
</asp:GridView>
</asp:Panel>
<div>
<asp:Label ID="Label111" runat="server" Text="rows inserted:" Visible="False"></asp:Label>
<asp:Label ID="lblrowsInserted1" runat="server" Text="Label" Visible="False"></asp:Label>
</div>
</ContentTemplate>
</asp:TabPanel>
<asp:TabPanel ID="tpSerial" HeaderText="Serial " runat="server">
<ContentTemplate>
<asp:Panel ID="PanelGridSer" runat="server" ScrollBars="Auto">
<asp:GridView ID="gridShowAdjustedData" runat="server" CellPadding="3" ForeColor="#333333"
AutoGenerateColumns="False" Width="100%" AllowPaging="True" OnPageIndexChanging="gridShowAdjustedData_PageIndexChanging"
OnRowDeleting="OnRemoveFromSerialGrid" ShowHeaderWhenEmpty="true" EmptyDataText="No Data"
DataKeyNames="Tus_itm_cd,Tus_itm_stus,Tus_ser_id,Tus_bin,Tus_ser_1" PageSize="12"
CssClass="GridView">
<AlternatingRowStyle BackColor="White" />
<Columns>
<asp:BoundField DataField="Tus_itm_cd" HeaderText="Item" />
<asp:BoundField DataField="Tus_itm_desc" HeaderText="Description" />
<asp:BoundField DataField="Tus_itm_model" HeaderText="Model" />
<asp:BoundField DataField="Tus_itm_stus" HeaderText="Status" />
<asp:BoundField DataField="Tus_qty" HeaderText="Qty" />
<asp:BoundField DataField="Tus_ser_1" HeaderText="Serial No" />
<asp:BoundField DataField="tus_unit_cost" HeaderText="Unit Cost" />
<asp:TemplateField>
<ItemTemplate>
<asp:ImageButton ID="imgBtnRemoveItem" runat="server" ImageAlign="Middle" ImageUrl="~/Images/Delete.png"
Width="11px" Height="11px" OnClientClick="return confirm('Do you want to delete?');"
CommandName="Delete" />
</ItemTemplate>
</asp:TemplateField>
</Columns>
<EditRowStyle BackColor="#2461BF" />
<FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
<HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
<PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
<RowStyle BackColor="#EFF3FB" />
<SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
<SortedAscendingCellStyle BackColor="#F5F7FB" />
<SortedAscendingHeaderStyle BackColor="#6D95E1" />
<SortedDescendingCellStyle BackColor="#E9EBEF" />
<SortedDescendingHeaderStyle BackColor="#4870BE" />
</asp:GridView>
</asp:Panel>
<div>
<asp:Label ID="Label11" runat="server" Text="rows inserted:" Visible="False"></asp:Label>
<asp:Label ID="lblrowsInserted" runat="server" Text="Label" Visible="False"></asp:Label>
</div>
</ContentTemplate>
</asp:TabPanel>
</asp:TabContainer>
</div>
<br />
</div>
<%-- ADDED BY SACHITH--%>
<%--COST ADD POP UP SCREEN--%>
<asp:HiddenField ID="HiddenFieldCusCrePopUpStats" runat="server" Value="0" />
<asp:ModalPopupExtender ID="ModalPopupExtender1" TargetControlID="ButtonAddCost"
runat="server" ClientIDMode="Static" PopupControlID="pnlShowCost" BackgroundCssClass="modalBackground"
CancelControlID="ImgAdd" PopupDragHandleControlID="div3">
</asp:ModalPopupExtender>
<asp:Panel ID="pnlShowCost" runat="server" Width="650px" CssClass="ModalWindow">
<div class="popUpHeader" id="div1" runat="server">
<div style="float: left; width: 80%" runat="server" id="div3">
Add Item Unit Cost</div>
<div style="float: left; width: 20%; text-align: right">
<asp:ImageButton ID="ImgAdd" runat="server" ImageUrl="~/Images/0032-01-16x16x16.ICO"
OnClientClick="ClosePopUp()" />&nbsp;</div>
</div>
<asp:Panel ID="PanelCCre" runat="server">
<div style="float: left; width: 100%; height: 10px; color: Black;">
&nbsp;
</div>
<div style="float: left; width: 1%; color: Black;">
&nbsp;
</div>
<asp:Panel ID="Panel3" runat="server" GroupingText=" ">
<%-- 1 row--%>
<div style="float: left; width: 100%; color: Black;">
<div style="float: left; width: 30%;">
Item Code
</div>
<div style="float: left; width: 70%;">
<asp:DropDownList ID="DropDownListItemCode" CssClass="ComboBox" runat="server" AppendDataBoundItems="true"
AutoPostBack="true" OnSelectedIndexChanged="DropDownListItemCode_SelectedIndexChanged">
</asp:DropDownList>
</div>
</div>
<%--2 row--%>
<div style="float: left; width: 100%; color: Black; padding-top: 5px;">
<div style="float: left; width: 30%;">
Item Status
</div>
<div style="float: left; width: 70%;">
<asp:DropDownList ID="DropDownListStatus" runat="server" CssClass="ComboBox" AutoPostBack="true"
AppendDataBoundItems="true" OnSelectedIndexChanged="DropDownListStatus_SelectedIndexChanged">
</asp:DropDownList>
</div>
</div>
<%--grid view--%>
<div style="float: left; width: 100%; color: Black; padding-top: 5px;">
<asp:GridView ID="GridViewPopUp" runat="server" Width="100%" EmptyDataText="No data found"
ShowHeaderWhenEmpty="True" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333"
CssClass="GridView" GridLines="None">
<AlternatingRowStyle BackColor="White" />
<Columns>
<asp:TemplateField HeaderStyle-Width="20px">
<HeaderTemplate>
<asp:CheckBox ID="CheckBoxGridHdrSelect" runat="server" Checked="true" onclick="SelectAll(this.id)" />
</HeaderTemplate>
<ItemTemplate>
<asp:CheckBox ID="CheckBoxGridSelect" runat="server" Checked="true" />
</ItemTemplate>
<ItemStyle HorizontalAlign="Center" Width="20px" Height="15px" />
</asp:TemplateField>
<asp:BoundField HeaderText="Itm Code" DataField="TUS_ITM_CD" />
<asp:BoundField HeaderText="Itm Status" DataField="TUS_ITM_STUS" />
<asp:BoundField HeaderText="Ser. 01" DataField="TUS_SER_1" />
<asp:BoundField HeaderText="Ser. 02" DataField="TUS_SER_2" />
<asp:BoundField HeaderText="Ser. Id" DataField="TUS_SER_ID" />
<asp:BoundField HeaderText="Unit Cost" DataField="TUS_UNIT_COST" />
</Columns>
<EditRowStyle BackColor="#2461BF" />
<FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
<HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
<PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
<RowStyle BackColor="#EFF3FB" />
<SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
<SortedAscendingCellStyle BackColor="#F5F7FB" />
<SortedAscendingHeaderStyle BackColor="#6D95E1" />
<SortedDescendingCellStyle BackColor="#E9EBEF" />
<SortedDescendingHeaderStyle BackColor="#4870BE" />
</asp:GridView>
</div>
<%--cost--%>
<div style="float: left; width: 100%; color: Black; padding-top: 5px;">
<div style="float: left; width: 30%;">
Unit Cost
</div>
<div style="float: left; width: 70%;">
<asp:TextBox ID="TextBoxUnitCost" runat="server" CssClass="TextBox" onKeyPress="return numbersonly(event,true)"></asp:TextBox>
</div>
</div>
<div style="float: left; width: 75%; color: Black; text-align: right;">
<asp:Button ID="ButtonApply" Text="Apply" CssClass="Button" runat="server" OnClick="ButtonApply_Click" />
</div>
</asp:Panel>
</asp:Panel>
</asp:Panel>
<%--Modal popup panel--%><%-- ******** AOD - OUT ******--%>
<div>
<asp:ModalPopupExtender ID="ModalPopupExtItem" runat="server" CancelControlID="btnimgCancel"
PopupControlID="PanelItemPopUp" TargetControlID="btnHidden_popup" ClientIDMode="Static">
</asp:ModalPopupExtender>
<asp:Panel ID="PanelItemPopUp" runat="server" Height="320px" Width="642px" BackColor="#A7C2DA"
BorderColor="#3333FF" BorderWidth="2px">
<div style="float: left; width: 100%; height: 22px; text-align: right; padding-top: 2px">
<div style="float: left; width: 2%; height: 22px; text-align: left;">
</div>
<div id="divPopupImg" runat="server" visible="false" style="float: left; width: 3%;
height: 22px; text-align: left;">
<asp:Image ID="Image4" runat="server" ImageUrl="~/Images/warning.gif" Width="15px"
Height="15px" />
</div>
<div style="float: left; width: 65%; height: 22px; text-align: left;">
<asp:Label ID="lblpopupMsg" runat="server" Width="100%" ForeColor="Red" />
</div>
<div style="float: left; width: 30%; height: 22px; text-align: right;">
<asp:ImageButton ID="btnimgAdd" runat="server" ImageUrl="~/Images/approve_img.png"
ImageAlign="Middle" OnClick="btnPopupOk_Click" Visible="true" Width="20px" Height="20px" />
&nbsp;
<asp:ImageButton ID="btnimgCancel" runat="server" ImageUrl="~/Images/error_icon.png"
ImageAlign="Middle" OnClick="btnPopupCancel_Click" Visible="true" Width="22px"
Height="22px" />
&nbsp;
</div>
</div>
<div style="text-align: right">
<asp:HiddenField ID="hdnInvoiceNo" runat="server" />
<asp:HiddenField ID="hdnInvoiceLineNo" runat="server" />
<asp:Label ID="lblPopupAmt" runat="server" Style="text-align: right"></asp:Label>&nbsp;
</div>
<div style="float: right; width: 100%; height: 22px; text-align: left; padding-top: 2px;
padding-bottom: 2px">
Item Code:
<asp:Label ID="lblPopupItemCode" runat="server" Font-Bold="True"></asp:Label>
<asp:Label ID="lblPopupBinCode" runat="server" Font-Bold="True"></asp:Label>
</div>
<div style="float: left; width: 100%; text-align: left;">
<div id="divSerialSelect" runat="server" style="float: left; width: 100%; text-align: left;">
<div style="float: left; width: 3%; padding-top: 2px; padding-bottom: 3px">
</div>
<div style="float: left; width: 15%;">
Search by :
</div>
<div style="float: left; width: 14%;">
<asp:DropDownList ID="ddlPopupSerial" runat="server" Width="85%" CssClass="ComboBox">
<asp:ListItem>Serial 1</asp:ListItem>
<asp:ListItem>Serial 2</asp:ListItem>
</asp:DropDownList>
</div>
<div style="float: left; width: 15%;">
<asp:TextBox ID="txtPopupSearchSer" runat="server" CssClass="TextBox" Width="100%"></asp:TextBox>
</div>
<div style="float: left; width: 11%;">
&nbsp;
<asp:Button ID="btnPopupSarch" runat="server" CssClass="Button" OnClick="btnPopupSarch_Click"
Text="Search" />
</div>
</div>
<div id="divQtySelect" runat="server" visible="false" style="float: left; width: 100%;
text-align: left; padding-top: 2px; padding-bottom: 3px">
<div style="float: left; width: 3%;">
</div>
<div style="float: left; width: 15%; text-align: left;">
<asp:Label ID="lblPopQty" runat="server" Text="Qty:" Visible="False"></asp:Label>
</div>
<div style="float: left; width: 29%; text-align: left;">
<asp:TextBox ID="txtPopupQty" runat="server" CssClass="TextBox" Visible="False" Width="100%"
ClientIDMode="Static"></asp:TextBox>
</div>
<div style="float: left; width: 30%; text-align: left;">
&nbsp;
<asp:Button ID="btnPopupAutoSelect" runat="server" CssClass="Button" OnClick="btnPopupAutoSelect_Click"
OnClientClick="SelectAuto()" Text="Auto Select" visble="false" />
</div>
</div>
<div style="float: left; width: 100%; text-align: left; padding-top: 1px; padding-bottom: 2px">
<div style="float: left; width: 3%;">
</div>
<div style="float: left; width: 15%; text-align: left;">
Requested Qty :
</div>
<div style="float: left; width: 15%; text-align: left;">
<asp:Label ID="lblInvoiceQty" runat="server" CssClass="TextBox" Width="85%"></asp:Label>
</div>
</div>
<div style="float: left; width: 100%; text-align: left; padding-top: 1px; padding-bottom: 2px">
<div style="float: left; width: 3%;">
</div>
<div style="float: left; width: 15%; text-align: left;">
Scaned Qty :
</div>
<div style="float: left; width: 15%; text-align: left;">
<asp:Label ID="lblScanQty" runat="server" CssClass="TextBox" Width="85%"></asp:Label>
</div>
</div>
</div>
<div style="width: 608px">
<asp:Panel ID="popupGridPanel" runat="server" Height="172px" ScrollBars="Auto" Style="margin-left: 15px;
margin-bottom: 13px" Width="100%">
<asp:GridView ID="GridPopup" runat="server" AutoGenerateColumns="False" CellPadding="4"
Height="45px" Width="95%" CssClass="GridView" ShowHeaderWhenEmpty="True" EmptyDataText="No data found">
<AlternatingRowStyle BackColor="White" />
<Columns>
<asp:TemplateField>
<HeaderTemplate>
<asp:CheckBox ID="chkPopupSelectAll" runat="server" ClientIDMode="Static" onclick="SelectAll(this.id)" />
</HeaderTemplate>
<EditItemTemplate>
<asp:CheckBox ID="CheckBox2" runat="server" />
</EditItemTemplate>
<ItemTemplate>
<asp:CheckBox ID="checkPopup" runat="server" ClientIDMode="Static" />
</ItemTemplate>
</asp:TemplateField>
<asp:BoundField DataField="Tus_ser_1" HeaderText="Serial No 1" />
<asp:BoundField DataField="Tus_ser_2" HeaderText="Serial No 2" />
<asp:BoundField DataField="Tus_itm_stus" HeaderText="Current Status" />
<asp:BoundField DataField="Tus_warr_no" HeaderText="Warrant #" />
<asp:BoundField DataField="Tus_bin" HeaderText="Bin Code" />
<asp:BoundField DataField="Tus_itm_desc" HeaderText="Description" />
<asp:BoundField DataField="Tus_ser_id" HeaderText="Serial ID" />
</Columns>
<EditRowStyle BackColor="#2461BF" />
<FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
<HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
<PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
<RowStyle BackColor="#EFF3FB" />
<SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
<SortedAscendingCellStyle BackColor="#F5F7FB" />
<SortedAscendingHeaderStyle BackColor="#6D95E1" />
<SortedDescendingCellStyle BackColor="#E9EBEF" />
<SortedDescendingHeaderStyle BackColor="#4870BE" />
</asp:GridView>
</asp:Panel>
</div>
</asp:Panel>
</div>
<%--Modal Pop-up Area for ADJ + --%>
<div>
<asp:ModalPopupExtender ID="serialmdpExtender" runat="server" TargetControlID="lnkbtnDummy"
ClientIDMode="Static" PopupControlID="pnlAddSerials" BackgroundCssClass="modalBackground"
CancelControlID="imgbtnClose" PopupDragHandleControlID="divpopHeader">
</asp:ModalPopupExtender>
<asp:Panel ID="pnlAddSerials" runat="server" Height="380px" Width="500px" CssClass="ModalWindow">
<div class="popUpHeader" id="divpopHeader">
<div style="float: left; width: 80%">
Add Items
</div>
<div style="float: left; width: 20%; text-align: right">
<asp:ImageButton ID="imgbtnClose" runat="server" ImageUrl="~/Images/0032-01-16x16x16.ICO" />&nbsp;
</div>
</div>
<div style="float: left; width: 100%">
<br />
<div class="MainDivCss">
<uc1:uc_MsgInfo ID="uc_SerialPopUpMsgInfo" runat="server" />
</div>
<div align="right">
<asp:Button ID="btnAdd" runat="server" Text="Add" CssClass="Button" OnClick="btnAdd_Click" />&nbsp;&nbsp;
</div>
<div class="MainDivCss">
Item Code : &nbsp;<asp:Label ID="lblmpeItemCode" runat="server" CssClass="commonDDLCss"></asp:Label>
<asp:HiddenField ID="hdnSelectedReqNo" runat="server" />
</div>
<div class="MainDivCss">
Req Qty : &nbsp;<asp:Label ID="lblReqQty" runat="server" CssClass="commonDDLCss"></asp:Label>&nbsp;&nbsp;
Actual Qty: &nbsp;<asp:Label ID="lblActQty_" runat="server" CssClass="commonDDLCss"></asp:Label>
</div>
<br />
<br />
<div class="MainDivCss">
<div class="SubDivCss">
<div class="innerLeftDivCss">
Bin Code :</div>
<div class="innerRightDivCss">
<asp:DropDownList ID="ddlmpeBinCode" runat="server" CssClass="commonDDLCss">
</asp:DropDownList>
</div>
</div>
<div class="SubDivCss">
<div class="innerLeftDivCss">
Status :</div>
<div class="innerRightDivCss">
<asp:DropDownList ID="ddlmpeItemStatus" runat="server" CssClass="commonDDLCss">
</asp:DropDownList>
</div>
</div>
</div>
<br />
<div class="MainDivCss" id="divNonSerial" runat="server" visible="false">
Add to Actual Qty : &nbsp;<asp:TextBox ID="txtActualQty" runat="server" CssClass="commonDDLCss"
Width="75px"></asp:TextBox>
</div>
<div style="float: left; width: 100%" id="divSerial" runat="server" visible="false">
<div class="MainDivCss">
Serial No 1 : &nbsp;<asp:TextBox ID="txtSerialNo1" runat="server" CssClass="commonDDLCss"
Width="150px" MaxLength="40"></asp:TextBox>
</div>
<div class="MainDivCss">
Serial No 2 : &nbsp;<asp:TextBox ID="txtSerialNo2" runat="server" CssClass="commonDDLCss"
Width="150px" MaxLength="40"></asp:TextBox>
</div>
<div class="MainDivCss">
Serial No 3 : &nbsp;<asp:TextBox ID="txtSerialNo3" runat="server" CssClass="commonDDLCss"
Width="150px" MaxLength="40"></asp:TextBox>
&nbsp;
</div>
<div class="MainDivCss">
<br />
<asp:Panel ID="pnlItemSerials" runat="server" Height="150px" Width="475px" ScrollBars="Auto">
<asp:GridView ID="gvItemSerials" runat="server" AutoGenerateColumns="False" CellPadding="4"
ForeColor="#333333" GridLines="None" OnRowCommand="gvItemSerials_RowCommand">
<Columns>
<asp:BoundField DataField="Tus_usrseq_no" HeaderText="Sequance" />
<asp:BoundField DataField="Tus_ser_1" HeaderText="Serial No1" />
<asp:BoundField DataField="Tus_ser_2" HeaderText="Serial No2" />
<asp:BoundField DataField="Tus_ser_3" HeaderText="Serial No3" />
<asp:BoundField DataField="Tus_itm_stus" HeaderText="Item Status" />
<asp:BoundField DataField="Tus_bin" HeaderText="Bin Code" />
<asp:TemplateField HeaderText="Delete" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="55px"
ShowHeader="true">
<ItemTemplate>
<asp:ImageButton ID="imgbtndelSerial" runat="server" ImageUrl="~/Images/Delete.png"
CommandName="DeleteItem" CommandArgument='<%# Eval("Tus_ser_id") + "|" + Eval("Tus_itm_cd") + "|" + Eval("Tus_ser_1") + "|" + Eval("Tus_bin") + "|" + Eval("Tus_itm_stus") %>'
OnClientClick="return DeleteConfirm()" />
</ItemTemplate>
</asp:TemplateField>
</Columns>
<AlternatingRowStyle BackColor="White" ForeColor="#284775" />
<EditRowStyle BackColor="#999999" />
<FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
<HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
<PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
<RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
<SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
<SortedAscendingCellStyle BackColor="#E9E7E2" />
<SortedAscendingHeaderStyle BackColor="#506C8C" />
<SortedDescendingCellStyle BackColor="#FFFDF8" />
<SortedDescendingHeaderStyle BackColor="#6F8DAE" />
</asp:GridView>
</asp:Panel>
</div>
</div>
<br />
<br />
<div class="MainDivCss">
<%--<asp:Button ID="btnAdd" runat="server" Text="Add" CssClass="Button" OnClick="btnAdd_Click" />--%>
&nbsp;&nbsp;<asp:Button ID="btnSubmit" runat="server" Text="Done" CssClass="Button"
OnClick="btnSubmit_Click" Visible="false" />
</div>
</div>
<asp:LinkButton ID="lnkbtnDummy" runat="server"></asp:LinkButton>
<asp:HiddenField ID="hdnUnitPrice" runat="server"></asp:HiddenField>
<asp:HiddenField ID="hdnLineNo" runat="server"></asp:HiddenField>
<asp:HiddenField ID="hdngvRowIndex" runat="server"></asp:HiddenField>
</asp:Panel>
</div>
<%--END--%>
<div style="display: none;">
<asp:Button ID="btnItem" runat="server" OnClick="CheckItem" />
<asp:Button ID="btnQty" runat="server" OnClick="CheckQty" />
<asp:Button ID="btnHidden_popup" runat="server" Text="Button" />
</div>
</asp:Content>
