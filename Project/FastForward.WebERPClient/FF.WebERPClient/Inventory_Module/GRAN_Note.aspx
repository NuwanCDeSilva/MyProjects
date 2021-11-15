<%@ Page Title="Goods Return App. Note/Damage Inform Note" Language="C#" MasterPageFile="~/Site.Master"
AutoEventWireup="true" CodeBehind="GRAN_Note.aspx.cs" Inherits="FF.WebERPClient.Inventory_Module.GRAN_Note" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="../UserControls/uc_CommonSearch.ascx" TagName="uc_CommonSearch"
TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
<%--javascript--%>
<script type="text/javascript">


function clickButton(e, buttonid) {
var evt = e ? e : window.event;
var bt = document.getElementById(buttonid);
if (bt) {
if (evt.keyCode == 113) {
bt.click();
return false;
}

}
}

//-------------------------------------GetItemData()------------------------------------------------------//
function GetItemData() {
var itemCode = document.getElementById("txtItemCode").value;
itemCode = itemCode.toUpperCase();
if (itemCode != "") {
FF.WebERPClient.LocalWebServices.CommonSearchWebServive.GetAllItemDetailsByItemCode(itemCode, onGetItemDataPass, onGetItemDataFail);
}
else { ClearItemFields(); }
}
//SucceededCallback method.
function onGetItemDataPass(result) {
if (result != null) {
// alert("Code :" + result.Mi_cd + " Desc : " + result.Mi_longdesc + " Brand :" + result.Mi_brand + " Model : " + result.Mi_model);
document.getElementById("txtItemDescription").value = result.Mi_longdesc;
document.getElementById("txtModelNo").value = result.Mi_model;
document.getElementById("txtBrand").value = result.Mi_brand;
document.getElementById("hdn_IN_SEQNO").value = "";
document.getElementById("hdn_IN_ITMLINE").value = "";
document.getElementById("hdn_IN_BATCHLINE").value = "";
document.getElementById("hdn_IN_SERLINE").value = "";
document.getElementById("hdn_IN_DOCDT").value = "";
document.getElementById("txtSerNo").value = "";
// document.getElementById("hdn_SER2").value = "";
document.getElementById("txtInDocNo").value = "";
document.getElementById("txtQty").value = "1";
document.getElementById("txtItemRemarks").value = "";
document.getElementById('<%=ddlItemStatus.ClientID%>').options.value = "";
document.getElementById('txtQty').disabled = true;
// document.getElementById('imgSearchSer').disabled = true;
//document.getElementById('txtSerNo').disabled = true;
// document.getElementById('imgSearchQty').disabled = true;
// if (result.Mi_is_ser1 == 1) {
// document.getElementById('txtSerNo').disabled = false;
// document.getElementById('imgSearchSer').disabled = false;
// }
// if (result.Mi_is_ser1 == 0) {
// document.getElementById('imgSearchQty').disabled = false;
// }
if (result.Mi_is_ser1 == -1) {
alert("Item not allow for GRAN/DIN");
ClearItemFields();
}
}
else { ClearItemFields(); }

}
//FailedCallback method.
function onGetItemDataFail(error) {
ClearItemFields();
}

function ClearItemFields() {
document.getElementById("txtItemDescription").value = "";
document.getElementById("txtModelNo").value = "";
document.getElementById("txtBrand").value = "";
document.getElementById("hdn_IN_SEQNO").value = "";
document.getElementById("hdn_IN_ITMLINE").value = "";
document.getElementById("hdn_IN_BATCHLINE").value = "";
document.getElementById("hdn_IN_SERLINE").value = "";
document.getElementById("hdn_IN_DOCDT").value = "";
document.getElementById("txtSerNo").value = "";
document.getElementById("hdn_SER2").value = "";
document.getElementById("txtInDocNo").value = "";
document.getElementById("txtQty").value = "";
document.getElementById("txtItemRemarks").value = "";
document.getElementById('<%=ddlItemStatus.ClientID%>').options.value = "";
document.getElementById("txtItemCode").value = "";
}

//-------------------------------------GetSerialInfor()------------------------------------------------------//

//var SerialArr = new Array();

function SerialObj(itemcode, ser1, ser2, serid) {
this.ItemCode = itemcode;
this.Ser1 = ser1;
this.Ser2 = ser2;
this.SerID = parseInt(serid);
}


function GetSerialInfor(com, loc) {

var itemCode = document.getElementById("txtItemCode").value;
var serial1 = document.getElementById("txtSerNo").value;
var serial2 = "N/A"; //document.getElementById("<%=hdn_SER2.ClientID %>").value;
var serialID = document.getElementById("txtSerialID").value;
itemCode = itemCode.toUpperCase();
//var inforpara = new Array(itemCode, serial1, serial2, serialID);

// SerialObj(itemCode, serial1, serial2, serialID);

var inforpara = com + "|" + loc + "|" + itemCode + "|" + serial1 + "|" + serial2 + "|" + serialID;
//SerialArr = new Array();
//SerialArr[SerialArr.length++] = new SerialObj(itemCode, serial1, serial2, serialID); ;

if (itemCode != "") {
FF.WebERPClient.LocalWebServices.CommonSearchWebServive.GetAvailableSerIDInformation(inforpara, onGetSerialDataPass, onGetSerialDataFail);
}
else { ClearItemSerialsFields(); }
}
//SucceededCallback method.
function onGetSerialDataPass(result) {
if (result != null) {
//alert(result.Tus_seq_no);
// alert("Code :" + result.Mi_cd + " Desc : " + result.Mi_longdesc + " Brand :" + result.Mi_brand + " Model : " + result.Mi_model);
document.getElementById("hdn_IN_SEQNO").value = result.Tus_seq_no;
document.getElementById("hdn_IN_ITMLINE").value = result.Tus_itm_line;
document.getElementById("hdn_IN_BATCHLINE").value = result.Tus_batch_line;
document.getElementById("hdn_IN_SERLINE").value = result.Tus_ser_line;
document.getElementById("hdn_IN_DOCDT").value = result.Tus_doc_dt;
document.getElementById("txtSerialID").value = result.Tus_ser_id;
// document.getElementById("txtSerNo").value = "";
document.getElementById("hdn_SER2").value = result.Tus_ser_2;
if (result.Tus_doc_no != null)
document.getElementById("txtInDocNo").value = result.Tus_doc_no;
else
document.getElementById("txtInDocNo").value = "";
document.getElementById("txtQty").value = "1";
document.getElementById('<%=ddlItemStatus.ClientID%>').options.value = result.Tus_itm_stus;
//document.getElementById('imgSearchQty').disabled = true;
}
else { ClearItemSerialsFields(); }

}
//FailedCallback method.
function onGetSerialDataFail(error) {
ClearItemSerialsFields();
}

function ClearItemSerialsFields() {
// document.getElementById("txtItemDescription").value = "";
// document.getElementById("txtModelNo").value = "";
// document.getElementById("txtBrand").value = "";
// document.getElementById("hdn_IN_SEQNO").value = "";
// document.getElementById("hdn_IN_ITMLINE").value = "";
// document.getElementById("hdn_IN_BATCHLINE").value = "";
// document.getElementById("hdn_IN_SERLINE").value = "";
// document.getElementById("hdn_IN_DOCDT").value = "";
// document.getElementById("txtSerNo").value = "";
// document.getElementById("hdn_SER2").value = "";
// document.getElementById("txtInDocNo").value = "";
// document.getElementById("txtQty").value = "";
// document.getElementById("txtItemRemarks").value = "";
// document.getElementById('<%=ddlItemStatus.ClientID%>').options.value = "";
// document.getElementById("txtItemCode").value = "";


}


//----------------------------------------------------------------------------------------------------------//

function GetSerialInforDoc(com, loc) {

var itemCode = document.getElementById("txtItemCode").value;
var serial1 = "N/A";
var serial2 = null;
var serialID = document.getElementById("txtSerialID").value;
itemCode = itemCode.toUpperCase();
//var inforpara = new Array(itemCode, serial1, serial2, serialID);

// SerialObj(itemCode, serial1, serial2, serialID);

var inforpara = com + "|" + loc + "|" + itemCode + "|" + serial1 + "|" + serial2 + "|" + serialID;
//SerialArr = new Array();
//SerialArr[SerialArr.length++] = new SerialObj(itemCode, serial1, serial2, serialID); ;

if (itemCode != "") {
FF.WebERPClient.LocalWebServices.CommonSearchWebServive.GetAvailableSerIDInformation(inforpara, onGetSerialDataPassDoc, onGetSerialDataFailDoc);
}
else { ClearItemSerialsFields(); }
}
//SucceededCallback method.
function onGetSerialDataPassDoc(result) {
if (result != null) {
//alert(result.Tus_seq_no);
// alert("Code :" + result.Mi_cd + " Desc : " + result.Mi_longdesc + " Brand :" + result.Mi_brand + " Model : " + result.Mi_model);
document.getElementById("hdn_IN_SEQNO").value = result.Tus_seq_no;
document.getElementById("hdn_IN_ITMLINE").value = result.Tus_itm_line;
document.getElementById("hdn_IN_BATCHLINE").value = result.Tus_batch_line;
document.getElementById("hdn_IN_SERLINE").value = result.Tus_ser_line;
document.getElementById("hdn_IN_DOCDT").value = result.Tus_doc_dt;
document.getElementById("txtSerNo").value = result.Tus_ser_1;
document.getElementById("hdn_SER2").value = result.Tus_ser_2;
if (result.Tus_doc_no != null)
document.getElementById("txtInDocNo").value = result.Tus_doc_no;
else
document.getElementById("txtInDocNo").value = "";
document.getElementById("txtQty").value = "1";
document.getElementById('<%=ddlItemStatus.ClientID%>').options.value = result.Tus_itm_stus;
//document.getElementById('imgSearchSer').disabled = true;
}
else { ClearItemSerialsFields(); }

}
//FailedCallback method.
function onGetSerialDataFailDoc(error) {
ClearItemSerialsFields();
}

//-------------------------------------------CheckMaxText()-------------------------------------------------//
// Change number to your max length.
function CheckCharacterCount(text, long) {
var maxlength = new Number(long);
if (document.getElementById(text).value.length > maxlength) {
document.getElementById(text).value = document.getElementById(text).value.substring(0, maxlength);
}
}


function numbersonly(e, decimal) {
var key;
var keychar;

if (window.event) {
key = window.event.keyCode;
}
else if (e) {
key = e.which;
}
else {
return true;
}
keychar = String.fromCharCode(key);

if ((key == null) || (key == 0) || (key == 8) || (key == 9) || (key == 13) || (key == 27)) {
return true;
}
else if ((("0123456789").indexOf(keychar) > -1)) {
return true;
}
else if (decimal && (keychar == ".")) {
return true;
}
else
return false;
}



</script>
<%--javascript--%>
<link href="../MainStyleSheet.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<asp:UpdatePanel ID="updtMainPnl" runat="server">
<ContentTemplate>
<asp:TabContainer ID="tcGRNContainer" runat="server" ActiveTabIndex="0" Height="530px">
<%--MRN Entering Tab--%>
<asp:TabPanel ID="tbpGRNDataEntry" HeaderText="Request Data Entry" runat="server">
<ContentTemplate>
<asp:Panel ID="PanelGranAll" runat="server" ScrollBars="Both" Height="528px">
<div style="float: left; width: 100%; color: Black;">
<div style="height: 22px; text-align: right;" class="PanelHeader">
<div style="float: left;">
<asp:Label runat="server" ID="lblDispalyInfor" Text="" Style="float: left; overflow-y: auto;
overflow-x: auto; font-size: xx-small; color: Yellow;"></asp:Label>
</div>
<asp:Button ID="btnApproved" runat="server" Text="Approved" Height="100%" Width="70px"
OnClick="btnApproved_Click" CssClass="Button" />
<asp:Button ID="btnReject" runat="server" Text="Rejected" Height="100%" Width="70px"
CssClass="Button" OnClick="btnReject_Click" />
<asp:Button ID="btnSave" runat="server" Text="Save" Height="100%" Width="70px" OnClick="btnSave_Click"
CssClass="Button" />
<asp:Button ID="btnPrint" runat="server" Text="Print" Height="100%" Width="70px"
CssClass="Button" OnClick="btnPrint_Click1" />
<asp:Button ID="btnCancel" runat="server" Text="Cancel" Height="100%" Width="70px"
OnClick="btnCancel_Click" CssClass="Button" />
<asp:Button ID="btnClear" runat="server" Text="Clear" Height="100%" Width="70px"
OnClick="btnClear_Click" CssClass="Button" />
<asp:Button ID="btnClose" runat="server" Text="Close" Height="100%" Width="70px"
OnClick="btnClose_Click" CssClass="Button" />
</div>
<div style="float: left; width: 99%; height: 116px;" class="MainDivCss">
<div style="float: left; width: 100%; padding: 2px 0px 0px 0px;">
<div style="float: left; width: 1%;">
&nbsp;</div>
<div style="float: left; width: 12%;">
Request Type
</div>
<div style="float: left; width: 1%;">
&nbsp;</div>
<div style="float: left; width: 18%;">
<asp:DropDownList ID="ddlRequestType" runat="server" CssClass="ComboBox" AppendDataBoundItems="True"
Width="100%" AutoPostBack="True" ClientIDMode="Static" OnSelectedIndexChanged="ddlRequestType_SelectedIndexChanged">
</asp:DropDownList>
</div>
<div style="float: left; width: 2%;">
&nbsp;</div>
<div style="float: left; width: 12%;">
Request Date
</div>
<div style="float: left; width: 1%;">
&nbsp;</div>
<div style="float: left; width: 18%;">
<asp:TextBox ID="txtRequestDate" runat="server" CssClass="TextBox" Width="100px"
Enabled="False"></asp:TextBox>
<asp:Image ID="imgBtnDate" runat="server" ImageUrl="~/Images/Calendar_scheduleHS.png"
ImageAlign="Middle" Visible="false" />
<asp:CalendarExtender ID="CEdate" runat="server" TargetControlID="txtRequestDate"
PopupPosition="BottomLeft" PopupButtonID="imgBtnDate" EnabledOnClient="true"
Animated="true" Format="dd/MM/yyyy">
</asp:CalendarExtender>
</div>
<div id="GRAN_DIN" runat="server" visible="False">
<div style="float: left; width: 1%;">
&nbsp;</div>
<div style="float: left; width: 12%;">
DIN No
</div>
<div style="float: left; width: 1%;">
&nbsp;</div>
<div style="float: left; width: 18%;">
<asp:DropDownList ID="DropDownListDINNo" runat="server" CssClass="ComboBox" AppendDataBoundItems="True"
Width="100%" AutoPostBack="True" ClientIDMode="Static" OnSelectedIndexChanged="DropDownListDINNo_SelectedIndexChanged">
</asp:DropDownList>
</div>
</div>
</div>
<div style="float: left; width: 100%; padding: 2px 0px 0px 0px;">
<div style="float: left; width: 1%;">
&nbsp;</div>
<div style="float: left; width: 12%;">
Transfer Location
</div>
<div style="float: left; width: 1%;">
&nbsp;</div>
<div style="float: left; width: 12%;">
<asp:TextBox ID="txtTransferLocation" runat="server" CssClass="TextBoxUpper" Width="72px"></asp:TextBox>
<asp:ImageButton ID="imgTransferLocation" runat="server" ImageUrl="~/Images/icon_search.png"
OnClick="imgTransferLocation_Click" />
</div>
<div style="float: left; width: 12%;">
</div>
<div style="float: left; width: 1%;">
&nbsp;</div>
<div style="float: left; width: 20%;">
<asp:CheckBox ID="CheckBoxManual" runat="server" Text="Is Manual" AutoPostBack="True"
OnCheckedChanged="CheckBoxManual_CheckedChanged" />
<asp:TextBox ID="txtManualRef" runat="server" CssClass="TextBox" Width="20%" MaxLength="30"
Enabled="False" onKeyPress="return numbersonly(event,false)"></asp:TextBox>
</div>
</div>
<div style="float: left; width: 100%; padding-top: 2px; height: 29px;">
<div style="float: left; width: 1%;">
&nbsp;</div>
<div style="float: left; width: 12%;">
Remarks
</div>
<div style="float: left; width: 1%;">
&nbsp;</div>
<div style="float: left; width: 45%;">
<asp:TextBox ID="txtRemarks" runat="server" TextMode="MultiLine" Rows="2" Width="100%"
CssClass="TextBox" onKeyUp="javascript:CheckCharacterCount('txtRemarks',250);"
onChange="javascript:CheckCharacterCount('txtRemarks',250);" ClientIDMode="Static"></asp:TextBox>
</div>
<div style="float: left; width: 11%;">
&nbsp;</div>
<div style="float: left; width: 14%;">
FM/RM/SZM/ZM :</div>
<div style="float: left; width: 15%;">
<asp:TextBox ID="txtFeildManager" runat="server" CssClass="TextBox" Width="90%" MaxLength="100"></asp:TextBox>
</div>
</div>
<div style="float: left; width: 100%; padding-top: 2px;">
<div style="float: left; width: 1%;">
&nbsp;</div>
<div style="float: left; width: 12%;">
Request Condition
</div>
<div style="float: left; width: 1%;">
&nbsp;</div>
<div style="float: left; width: 15%;">
<asp:CheckBox ID="chkToReport" runat="server" Text="Only to report" />
<br />
<asp:CheckBox ID="chkToStores" runat="server" Text="Return to stores" />
<br />
</div>
<div style="float: left; width: 15%;">
<asp:CheckBox ID="chkSellAtShop" runat="server" Text="To sell at shop level" Visible="False" />
<br />
<asp:CheckBox ID="chkDiscount" runat="server" Text="Need discount" Visible="False" />
</div>
<div style="float: left; width: 15%;">
&nbsp;</div>
<div style="float: left; width: 11%;">
&nbsp;</div>
<div style="float: left; width: 14%;">
Status to be change :</div>
<div style="float: left; width: 15%;">
<asp:DropDownList ID="ddlNewStatus" runat="server" AppendDataBoundItems="True" AutoPostBack="True"
CssClass="ComboBox" Width="90%" OnSelectedIndexChanged="ddlNewStatus_SelectedIndexChanged">
</asp:DropDownList>
</div>
</div>
</div>
<div class="CollapsiblePanelHeader">
Item Detail</div>
<div style="float: left;">
<asp:Image runat="server" ID="Image1" ImageUrl="~/Images/16 X 16 DownArrow.jpg" ImageAlign="Right" />
</div>
<asp:CollapsiblePanelExtender ID="cpeGRANItem" runat="server" TargetControlID="pnlGRANItem"
CollapsedSize="0" ExpandedSize="250" ExpandControlID="Image1" CollapseControlID="Image1"
ImageControlID="Image1" ExpandedImage="~/Images/16 X 16 UpArrow.jpg" CollapsedImage="~/Images/16 X 16 DownArrow.jpg"
Enabled="True">
</asp:CollapsiblePanelExtender>
<div style="width: 100%; float: left; padding-top: 1px; padding-bottom: 1px; background-color: #EEEEEE;
font-size: 11px;">
<asp:Panel runat="server" ID="pnlGRANItem" Width="99.8%" ScrollBars="Auto" BorderColor="#9F9F9F"
BorderWidth="1px">
<div style="float: left; width: 100%">
<div style="width: 1%; float: left">
&nbsp;
</div>
<div style="width: 15%; float: left">
Item Code :<br />
<asp:TextBox ID="txtItemCode" runat="server" Width="81%" ClientIDMode="Static" CssClass="TextBoxUpper"
MaxLength="20"></asp:TextBox>
<asp:ImageButton ID="imgItemSearch" runat="server" ImageUrl="~/Images/icon_search.png"
OnClick="ImgItemSearch_Click" Style="height: 16px" />
</div>
<div style="width: 51%; float: left">
Description :<br />
<asp:TextBox ID="txtItemDescription" runat="server" Width="95%" Enabled="False" ClientIDMode="Static"
CssClass="TextBox"></asp:TextBox>
</div>
<div style="width: 14%; float: left">
Brand :<br />
<asp:TextBox ID="txtBrand" runat="server" Width="85%" Enabled="False" ClientIDMode="Static"
CssClass="TextBox"></asp:TextBox>
</div>
<div style="width: 17%; float: left">
Model :<br />
<asp:TextBox ID="txtModelNo" runat="server" Width="95%" Enabled="False" ClientIDMode="Static"
CssClass="TextBox"></asp:TextBox>
</div>
</div>
<div style="float: left; width: 100%">
<div style="width: 1%; float: left">
&nbsp;
</div>
<div style="width: 15%; float: left">
Item Status :<br />
<asp:DropDownList ID="ddlItemStatus" runat="server" AppendDataBoundItems="True" CssClass="ComboBox"
Width="90%" Enabled="False">
</asp:DropDownList>
</div>
<div style="width: 24%; float: left">
Serial No :<br />
<asp:TextBox ID="txtSerNo" runat="server" Width="85%" CssClass="TextBox" ClientIDMode="Static"></asp:TextBox>
<asp:ImageButton ID="imgSearchSer" runat="server" ImageUrl="~/Images/icon_search.png"
OnClick="ImgSearchSer_Click" ClientIDMode="Static" />
</div>
<div style="width: 8%; float: left">
Qty :<br />
<asp:TextBox ID="txtQty" runat="server" CssClass="TextBoxNumeric" Width="85%" MaxLength="10"
ClientIDMode="Static" Enabled="False"></asp:TextBox>
<asp:FilteredTextBoxExtender ID="ajaxNumericText" runat="server" TargetControlID="txtQty"
FilterType="Custom, Numbers" ValidChars="." Enabled="True" />
</div>
<div style="width: 35%; float: left">
Document No :<br />
<asp:TextBox ID="txtSerialID" runat="server" Width="22%" CssClass="TextBox" ClientIDMode="Static"></asp:TextBox>
<asp:TextBox ID="txtInDocNo" runat="server" Width="65%" MaxLength="50" CssClass="TextBox"
ClientIDMode="Static" ReadOnly="True" Text="N/A"></asp:TextBox>
<asp:ImageButton ID="imgSearchQty" runat="server" ImageUrl="~/Images/icon_search.png"
OnClick="ImgSearchQty_Click" ClientIDMode="Static" />
</div>
</div>
<div style="float: left; width: 100%; padding-bottom: 2px">
<div style="width: 1%; float: left">
&nbsp;
</div>
<div style="width: 8%; float: left">
Remarks :
</div>
<div style="width: 74%; float: left;">
<asp:TextBox ID="txtItemRemarks" runat="server" TextMode="MultiLine" Rows="2" Width="96%"
CssClass="TextBox" onKeyUp="javascript:CheckCharacterCount('txtItemRemarks',250);"
onChange="javascript:CheckCharacterCount('txtItemRemarks',250);" ClientIDMode="Static"></asp:TextBox>
</div>
<div style="width: 10%; float: left;">
<asp:Button ID="btnAddItem" runat="server" CssClass="Button" OnClick="btnAddItem_Click"
Text="Add Item &gt;&gt;" Width="95%" />
</div>
</div>
<div style="float: left; width: 100%;">
<asp:GridView ID="gvItem" runat="server" AutoGenerateColumns="False" CellPadding="4"
CssClass="GridView" DataKeyNames="ITRS_ITM_CD,ITRS_SER_ID" ForeColor="#333333"
Width="99%" EmptyDataText="No data found" ShowHeaderWhenEmpty="True" OnRowDeleting="gvItem_RowDeleting">
<AlternatingRowStyle BackColor="White" />
<Columns>
<asp:BoundField DataField="ITRS_ITM_CD" HeaderText="Item">
<HeaderStyle HorizontalAlign="Left" Width="120px" />
</asp:BoundField>
<asp:BoundField DataField="MI_LONGDESC" HeaderText="Description">
<HeaderStyle HorizontalAlign="Left" Width="275px" />
</asp:BoundField>
<asp:BoundField DataField="MI_MODEL" HeaderText="Model">
<HeaderStyle HorizontalAlign="Left" Width="120px" />
</asp:BoundField>
<asp:BoundField DataField="MI_BRAND" HeaderText="Brand">
<HeaderStyle HorizontalAlign="Left" Width="60px" />
</asp:BoundField>
<asp:BoundField DataField="ITRS_ITM_STUS" HeaderText="Status">
<HeaderStyle HorizontalAlign="Left" Width="50px" />
</asp:BoundField>
<asp:BoundField DataField="ITRS_SER_ID" HeaderText="Serial ID" Visible="False">
<HeaderStyle HorizontalAlign="Right" Width="70px" />
</asp:BoundField>
<asp:BoundField DataField="ITRS_SER_1" HeaderText="Serial No">
<HeaderStyle HorizontalAlign="Left" Width="70px" />
</asp:BoundField>
<asp:BoundField DataField="ITRS_SER_2" HeaderText="Other Serial No">
<HeaderStyle HorizontalAlign="Left" Width="70px" />
</asp:BoundField>
<asp:BoundField DataField="ITRS_QTY" HeaderText="Qty" Visible="False">
<HeaderStyle HorizontalAlign="Right" Width="70px" />
</asp:BoundField>
<asp:BoundField DataField="ITRS_IN_DOCNO" HeaderText="Inward Document">
<HeaderStyle HorizontalAlign="Left" Width="70px" />
</asp:BoundField>
<asp:BoundField DataField="ITRS_IN_ITMLINE" HeaderText="Inward Item Line" Visible="False">
<HeaderStyle HorizontalAlign="Right" Width="70px" />
</asp:BoundField>
<asp:BoundField DataField="ITRS_IN_BATCHLINE" HeaderText="Inward Batch Line" Visible="False">
<HeaderStyle HorizontalAlign="Right" Width="70px" />
</asp:BoundField>
<asp:BoundField DataField="ITRS_IN_SERLINE" HeaderText="Inward Serial Line" Visible="False">
<HeaderStyle HorizontalAlign="Right" Width="70px" />
</asp:BoundField>
<asp:BoundField DataField="ITRS_IN_DOCDT" HeaderText="Inward Date" Visible="False">
<HeaderStyle HorizontalAlign="Left" Width="70px" />
</asp:BoundField>
<asp:BoundField DataField="Tus_base_itm_line" Visible="False" />
<asp:BoundField DataField="ITRS_TRNS_TP" HeaderText="Transfer Type">
<HeaderStyle HorizontalAlign="Left" Width="70px" />
</asp:BoundField>
<asp:BoundField DataField="ITRS_RMK" HeaderText="Remarks">
<HeaderStyle HorizontalAlign="Left" Width="70px" />
</asp:BoundField>
<asp:TemplateField>
<ItemTemplate>
<asp:ImageButton ID="imgBtnRemoveItem" runat="server" CommandName="Delete" Height="12px"
ImageAlign="Middle" ImageUrl="~/Images/Delete.png" OnClientClick="return confirm('Do you want to delete?');"
Width="12px" />
</ItemTemplate>
</asp:TemplateField>
</Columns>
<EditRowStyle BackColor="#2461BF" />
<FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
<HeaderStyle BackColor="#507CD1" Font-Bold="True" Font-Size="12px" ForeColor="White" />
<PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
<RowStyle BackColor="#EFF3FB" Font-Size="12px" />
<SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
<SortedAscendingCellStyle BackColor="#F5F7FB" />
<SortedAscendingHeaderStyle BackColor="#6D95E1" />
<SortedDescendingCellStyle BackColor="#E9EBEF" />
<SortedDescendingHeaderStyle BackColor="#4870BE" />
</asp:GridView>
</div>
</asp:Panel>
</div>
<div style="float: left; width: 100%; padding-top: 2px; padding-bottom: 2px;">
<div class="CollapsiblePanelHeader">
Approval</div>
<div style="float: left;">
<asp:Image runat="server" ID="Image2" ImageUrl="~/Images/16 X 16 DownArrow.jpg" ImageAlign="Right" />
</div>
<asp:CollapsiblePanelExtender ID="cpeGRANApp" runat="server" TargetControlID="pnlGRANApp"
CollapsedSize="0" ExpandedSize="150" ExpandControlID="Image2" Collapsed="True"
CollapseControlID="Image2" ImageControlID="Image2" ExpandedImage="~/Images/16 X 16 UpArrow.jpg"
CollapsedImage="~/Images/16 X 16 DownArrow.jpg" Enabled="True">
</asp:CollapsiblePanelExtender>
<div style="width: 100%; float: left; padding-top: 1px; padding-bottom: 1px; background-color: #EEEEEE;
font-size: 11px;">
<asp:Panel runat="server" ID="pnlGRANApp" Width="99.8%" ScrollBars="Auto" BorderColor="#9F9F9F"
BorderWidth="1px">
<div style="float: left; width: 100%; padding-top: 3px; padding-bottom: 3px;">
<div style="width: 1%; float: left">
&nbsp;
</div>
<div style="width: 12%; float: left">
Approved By :<br />
</div>
<div style="width: 40%; float: left;">
<asp:Label ID="lblAppby" runat="server" />
&nbsp;
</div>
<div style="width: 10%; float: left;">
&nbsp;
</div>
<div style="width: 20%; float: left;">
Current Status of GRAN/DIN :
</div>
<div style="width: 5%; float: left;">
<asp:Label ID="lblReqCurrStatus" runat="server" />
</div>
</div>
<div style="float: left; width: 100%; padding-top: 3px; padding-bottom: 3px;">
<div style="width: 1%; float: left">
&nbsp;
</div>
<div style="width: 12%; float: left">
Approve Naration :<br />
</div>
<div style="width: 40%; float: left;">
<asp:DropDownList ID="ddlAppNaration" runat="server" AppendDataBoundItems="True"
AutoPostBack="True" CssClass="ComboBox" Width="90%">
</asp:DropDownList>
</div>
<div style="width: 10%; float: left;">
&nbsp;
</div>
<div style="float: left; width: 20%;">
Status to be change :</div>
<div style="float: left; width: 15%;">
<asp:DropDownList ID="DropDownListAppStatus" runat="server" AppendDataBoundItems="True"
AutoPostBack="True" CssClass="ComboBox" Width="90%" OnSelectedIndexChanged="ddlNewStatus_SelectedIndexChanged">
</asp:DropDownList>
</div>
</div>
<div style="float: left; width: 100%; padding-top: 3px; padding-bottom: 3px;">
<div style="width: 1%; float: left">
&nbsp;
</div>
<div style="width: 12%; float: left">
Approve Remarks :<br />
</div>
<div style="width: 50%; float: left;">
<asp:TextBox ID="txtAppRemarks" runat="server" ClientIDMode="Static" CssClass="TextBox"
onChange="javascript:CheckCharacterCount('txtRemarks',250);" onKeyUp="javascript:CheckCharacterCount('txtRemarks',250);"
Rows="4" TextMode="MultiLine" Width="96%"></asp:TextBox>
</div>
</div>
</asp:Panel>
</div>
</div>
</div>
</asp:Panel>
</ContentTemplate>
</asp:TabPanel>
<asp:TabPanel ID="TabPanel1" HeaderText="Request Enquiry" runat="server">
<ContentTemplate>
<asp:Panel ID="PanelSearGRAN" runat="server" Width="900px">
<asp:UpdatePanel ID="updPnlGRANList" runat="server">
<ContentTemplate>
<br />
<div class="MainDivCss">
<table border="0" cellpadding="2" cellspacing="0">
<tr>
<td style="width: 100px;">
Type
</td>
<td colspan="4">
<asp:DropDownList ID="DropDownListType" runat="server" CssClass="ComboBox" AppendDataBoundItems="True"
Width="80%" ClientIDMode="Static">
</asp:DropDownList>
</td>
</tr>
<tr>
<td style="width: 100px;">
From :
</td>
<td>
<asp:TextBox ID="txtFromDate" runat="server" Width="75px"></asp:TextBox>
<asp:Image ID="imgFromDate" runat="server" ImageUrl="~/Images/Calendar_scheduleHS.png" />
<asp:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtFromDate"
Format="dd/MMM/yyyy" PopupButtonID="imgFromDate" Enabled="True">
</asp:CalendarExtender>
</td>
<td>
&nbsp;&nbsp;&nbsp;To :
</td>
<td>
<asp:TextBox ID="txtToDate" runat="server" Width="75px"></asp:TextBox>
<asp:Image ID="imgToDate" runat="server" ImageUrl="~/Images/Calendar_scheduleHS.png" />
<asp:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtToDate"
Format="dd/MMM/yyyy" PopupButtonID="imgToDate" Enabled="True">
</asp:CalendarExtender>
</td>
<td>
&nbsp;&nbsp;<asp:ImageButton ID="imgbtnRequestSearch" runat="server" ImageUrl="~/Images/icon_search.png"
OnClick="imgbtnRequestSearch_Click" />
</td>
</tr>
<tr>
<td style="width: 100px;">
Status :
</td>
<td colspan="4">
<asp:DropDownList ID="ddlRequestStatus" runat="server" CssClass="ComboBox">
<asp:ListItem Text="Pending" Value="P"></asp:ListItem>
<asp:ListItem Text="Approved" Value="A"></asp:ListItem>
<asp:ListItem Text="" Value="X" Selected="True"></asp:ListItem>
</asp:DropDownList>
</td>
</tr>
<tr>
<td style="width: 100px;">
Created User :
</td>
<td>
<asp:CheckBox ID="chbCreatedUser" runat="server" />
</td>
</tr>
</table>
</div>
<div class="MainDivCss">
<br />
<br />
<div style="float: left; width: 100%; height: 400px; overflow: auto;">
<%-- <asp:Panel id="pnlGRNList" runat="server" Height="600" ScrollBars="Auto">--%>
<%--Request Search Grid--%>
<asp:GridView ID="gvGRANList" runat="server" AutoGenerateColumns="False" CellPadding="4"
ForeColor="#333333" GridLines="Both" CssClass="GridView" EmptyDataText="No data found"
ShowHeaderWhenEmpty="True" Width="90%" OnRowCommand="gvGRANList_RowCommand">
<AlternatingRowStyle BackColor="White" />
<Columns>
<asp:TemplateField HeaderText="Req No">
<ItemTemplate>
<asp:LinkButton ID="lnkbtnReqNo" Text='<%# DataBinder.Eval(Container.DataItem, "Itr_req_no") %>'
ClientIDMode="Static" CommandName="SelectInvReq" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "Itr_req_no") %>'
runat="server"></asp:LinkButton>
</ItemTemplate>
<HeaderStyle HorizontalAlign="Left" />
<ItemStyle HorizontalAlign="Left" Width="200px" />
</asp:TemplateField>
<asp:TemplateField HeaderText="Company Code">
<ItemTemplate>
<asp:Label ID="lblCompCode" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Itr_com") %>'></asp:Label>
</ItemTemplate>
<HeaderStyle HorizontalAlign="Left" />
<ItemStyle HorizontalAlign="Left" Width="100px" />
</asp:TemplateField>
<asp:TemplateField HeaderText="Status">
<ItemTemplate>
<%--<asp:Label ID="lblReqStatus" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Itr_stus").ToString().ToUpper().Equals("P") ? "Pending" : "Approved" %>'></asp:Label>--%>
<asp:Label ID="Label1" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Itr_stus") %>'></asp:Label>
</ItemTemplate>
<HeaderStyle HorizontalAlign="Left" />
<ItemStyle HorizontalAlign="Left" Width="100px" />
</asp:TemplateField>
<asp:TemplateField HeaderText="Type">
<ItemTemplate>
<asp:Label ID="lblType" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Itr_tp") %>'></asp:Label>
</ItemTemplate>
<HeaderStyle HorizontalAlign="Left" />
<ItemStyle HorizontalAlign="Left" Width="100px" />
</asp:TemplateField>
<%-- <asp:TemplateField HeaderText="Sub Type">
<ItemTemplate>
<asp:Label ID="lblSubType" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "SubTpDesc") %>'></asp:Label>
</ItemTemplate>
<HeaderStyle HorizontalAlign="Left" />
<ItemStyle HorizontalAlign="Left" Width="100px" />
</asp:TemplateField>--%>
<asp:TemplateField HeaderText="Date">
<ItemTemplate>
<asp:Label ID="lblDate" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Itr_dt", "{0:dd/MM/yyyy}") %>'></asp:Label>
</ItemTemplate>
<HeaderStyle HorizontalAlign="Left" />
<ItemStyle HorizontalAlign="Left" Width="200px" />
</asp:TemplateField>
</Columns>
<AlternatingRowStyle BackColor="White" />
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
<%-- </asp:Panel>--%>
</div>
<%--Request Search Grid--%>
</div>
</ContentTemplate>
</asp:UpdatePanel>
</asp:Panel>
</ContentTemplate>
</asp:TabPanel>
</asp:TabContainer>
<%-- END--%>
<div visible="false">
<asp:HiddenField ID="hdn_SER2" runat="server" ClientIDMode="Static" Value="N/A" />
<asp:HiddenField ID="hdn_IN_SEQNO" runat="server" ClientIDMode="Static" />
<asp:HiddenField ID="hdn_IN_ITMLINE" runat="server" ClientIDMode="Static" />
<asp:HiddenField ID="hdn_IN_BATCHLINE" runat="server" ClientIDMode="Static" />
<asp:HiddenField ID="hdn_IN_SERLINE" runat="server" ClientIDMode="Static" />
<asp:HiddenField ID="hdn_IN_DOCDT" runat="server" ClientIDMode="Static" />
<asp:HiddenField ID="hdn_SelectedReqNo" runat="server" ClientIDMode="Static" />
</div>
</ContentTemplate>
</asp:UpdatePanel>
</asp:Content>
