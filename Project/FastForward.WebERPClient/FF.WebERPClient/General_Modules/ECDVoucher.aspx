<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
CodeBehind="ECDVoucher.aspx.cs" Inherits="FF.WebERPClient.General_Modules.ECDVoucher" %>

<%@ Register Src="../UserControls/uc_ProfitCenterSearch.ascx" TagName="uc_ProfitCenterSearch"
TagPrefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
<script type="text/javascript">
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

function LinkClick(sender, args) {
var i = sender.ID
document.getElementById('<%= LinkButtonView.ClientID %>').click()
}
function LinkClick1(sender, args) {
var i = sender.ID
document.getElementById('<%= LinkButtonView1.ClientID %>').click()
}
</script>
<style type="text/css">
.Panel legend
{
color: Blue;
}
</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<asp:UpdatePanel ID="updMain" runat="server">
<ContentTemplate>
<asp:LinkButton ID="LinkButtonView" runat="server" OnClick="LinkButtonView_Click"></asp:LinkButton>
<asp:LinkButton ID="LinkButtonView1" runat="server" OnClick="LinkButtonView1_Click"></asp:LinkButton>
<asp:TabContainer ID="tbMain" runat="server" ActiveTabIndex="0">
<asp:TabPanel ID="tbPnlDefinition" runat="server" HeaderText="Voucher Definition">
<ContentTemplate>
<div style="float: left; width: 100%; color: Black; font-family: Verdana; font-size: 11px;">
<div style="height: 22px; text-align: right;" class="PanelHeader">
<asp:Button ID="ButtonSave" runat="server" Text="Save" CssClass="Button" OnClick="ButtonSave_Click" />
<asp:Button ID="ButtonClear" runat="server" Text="Clear" CssClass="Button" OnClick="ButtonClear_Click" />
<asp:Button ID="ButtonClose" runat="server" Text="Close" CssClass="Button" OnClick="ButtonClose_Click" />
</div>
<div style="float: left; height: 10px; width: 100%">
&nbsp;
</div>
<div style="float: left; width: 100%;">
<div style="float: left; width: 60%;">
<asp:Panel ID="Panel12" runat="server" GroupingText=" " CssClass="Panel">
<div style="float: left; width: 100%; background-color: #3333FF;">
<div style="color: #FFFFFF; font-weight: bold; background-color: #0066CC;">
Business Hirc Details</div>
</div>
<div style="float: left; width: 65%;">
<uc1:uc_ProfitCenterSearch ID="uc_ProfitCenterSearchLoyaltyType" runat="server" />
</div>
<div style="float: left; width: 5%; text-align: center;">
<asp:ImageButton ID="ImageButtonAddPC" runat="server" ImageUrl="~/Images/right_arrow_icon.png"
Width="100%" ToolTip="Add to Profit Center List" OnClick="ImageButtonAddPC_Click" />
</div>
<div style="float: left; width: 30%; text-align: right;">
<asp:Panel ID="Panel13" runat="server" Height="130px" ScrollBars="Vertical" BorderColor="Blue"
BorderWidth="1px" GroupingText="Profit Centers">
<asp:GridView ID="GridViewPC" runat="server" AutoGenerateColumns="False" Style="width: 80px">
<Columns>
<asp:TemplateField>
<EditItemTemplate>
<asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
</EditItemTemplate>
<ItemTemplate>
<asp:Label ID="Label1" runat="server"></asp:Label>
<asp:CheckBox ID="chekPc" runat="server" Checked="true" />
</ItemTemplate>
</asp:TemplateField>
<asp:BoundField DataField="PROFIT_CENTER" ShowHeader="False" />
</Columns>
</asp:GridView>
</asp:Panel>
<div style="float: left; width: 100%; text-align: right;">
<div style="float: left; width: 30%; text-align: right;">
<asp:Button ID="ButtonAll" runat="server" Text="All" CssClass="Button" Width="100%"
OnClick="ButtonAll_Click" />
</div>
<div style="float: left; width: 30%; text-align: right;">
<asp:Button ID="ButtonNone" runat="server" Text="None" CssClass="Button" OnClick="ButtonNone_Click" />
</div>
<div style="float: left; width: 30%; text-align: right;">
<asp:Button ID="ButtonClearPc" runat="server" Text="Clear" CssClass="Button" OnClick="ButtonClearPc_Click" />
</div>
</div>
</div>
</asp:Panel>
</div>
<div style="float: left; width: 40%;">
<asp:Panel ID="Panel1" runat="server" GroupingText=" ">
<div style="float: left; width: 100%; background-color: #3333FF;">
<div style="color: #FFFFFF; font-weight: bold; background-color: #0066CC;">
Schema Details</div>
</div>
<div style="padding: 1.5px; float: left; width: 100%;">
</div>
<div style="float: left; width: 100%;">
<div style="float: left; width: 20%;">
<asp:Label ID="Label2" runat="server" Text="Category" ForeColor="Black" Font-Size="X-Small"></asp:Label>
</div>
<div style="float: left; width: 30%;">
<asp:TextBox ID="TextBoxSchemaCategory" runat="server" CssClass="TextBox" Width="70%"></asp:TextBox>
&nbsp;
<asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Images/icon_search.png"
OnClick="ImageButton1_Click" />
</div>
<div style="float: left; width: 15%;">
<asp:Label ID="Label3" runat="server" Text="Type" ForeColor="Black" Font-Size="X-Small"></asp:Label>
</div>
<div style="float: left; width: 30%;">
<asp:TextBox ID="TextBoxSchemaType" runat="server" CssClass="TextBox" Width="70%"></asp:TextBox>
&nbsp;
<asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/Images/icon_search.png"
OnClick="ImageButton2_Click" />
</div>
<div style="float: left; width: 5%;" align="left">
<asp:ImageButton ID="ImageButton3" runat="server" ImageUrl="~/Images/download_arrow_icon.png"
Width="100%" OnClick="ImageButton3_Click1" />
</div>
<div style="float: left; width: 100%; height: 125px;">
<asp:Panel ID="Panel3" runat="server" ScrollBars="Both" Style="margin-bottom: 5px"
Height="105px">
<asp:GridView ID="GridViewSchemas" runat="server" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True"
CellPadding="4" CssClass="GridView" ForeColor="#333333" Width="98%">
<AlternatingRowStyle BackColor="White" />
<EmptyDataTemplate>
<div style="width: 100%; text-align: center;">
No data found
</div>
</EmptyDataTemplate>
<Columns>
<asp:TemplateField>
<ItemTemplate>
<asp:Label ID="Label1" runat="server"></asp:Label>
<asp:CheckBox ID="chkSelect" runat="server" Checked="true" />
</ItemTemplate>
<EditItemTemplate>
<asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
</EditItemTemplate>
</asp:TemplateField>
<asp:BoundField HeaderText="Code" DataField="Hsd_cd" />
<asp:BoundField HeaderText="Desc" DataField="Hsd_desc" />
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
<div style="float: left; width: 100%; text-align: right;">
<div style="float: left; width: 30%; text-align: right;">
<asp:Button ID="ButtonSchAll" runat="server" Text="All" CssClass="Button" OnClick="ButtonSchAll_Click1" />
</div>
<div style="float: left; width: 30%; text-align: right;">
<asp:Button ID="ButtonSchNone" runat="server" Text="None" CssClass="Button" OnClick="ButtonSchNone_Click1" />
</div>
<div style="float: left; width: 30%; text-align: right;">
<asp:Button ID="ButtonSchClear" runat="server" Text="Clear" CssClass="Button" OnClick="ButtonSchClear_Click1" />
</div>
</div>
</div>
</div>
</asp:Panel>
</div>
</div>
<div style="float: left; height: 10px; width: 100%">
&nbsp;
</div>
<div class="div100pcelt">
<asp:Panel ID="pnlDates" runat="server" GroupingText="Date Range" CssClass="Panel">
<div style="float: left; width: 100%">
<div class="div1pcelt">
&nbsp;
</div>
<div class="div32pcelt">
<div class="div20pcelt1">
From
</div>
<div class="div80pcelt">
<asp:TextBox ID="TextBoxFrom" CssClass="TextBox" runat="server" Enabled="False" Width="80px"></asp:TextBox>
<asp:Image ID="Image1" runat="server" ImageUrl="~/Images/Calendar_scheduleHS.png" />
<asp:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="TextBoxFrom"
PopupButtonID="Image1" Enabled="True" Format="dd/MMM/yyyy">
</asp:CalendarExtender>
</div>
</div>
<div class="div1pcelt">
&nbsp;
</div>
<div style="float: left; width: 32%;">
<div class="div20pcelt1">
To
</div>
<div class="div80pcelt">
<asp:TextBox ID="TextBoxTo" CssClass="TextBox" runat="server" Enabled="False" Width="80px"></asp:TextBox>
<asp:Image ID="Image2" runat="server" ImageUrl="~/Images/Calendar_scheduleHS.png" />
<asp:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="TextBoxTo"
PopupButtonID="Image2" Enabled="True" Format="dd/MMM/yyyy">
</asp:CalendarExtender>
</div>
</div>
</div>
</asp:Panel>
<div style="float: left; height: 3px; width: 100%">
&nbsp;
</div>
<asp:Panel ID="Panel2" runat="server" GroupingText="Balance Range" CssClass="Panel">
<div style="float: left; width: 100%">
<div class="div1pcelt">
&nbsp;
</div>
<div class="div32pcelt">
<div class="div20pcelt1">
From
</div>
<div class="div80pcelt">
<asp:TextBox ID="TextBoxBalFrom" CssClass="TextBox" runat="server" onKeyPress="return numbersonly(event,true)"
MaxLength="8"></asp:TextBox>
</div>
</div>
<div class="div1pcelt">
&nbsp;
</div>
<div class="div32pcelt">
<div class="div20pcelt1">
To
</div>
<div class="div80pcelt">
<asp:TextBox ID="TextBoxBalTo" CssClass="TextBox" runat="server" onKeyPress="return numbersonly(event,true)"
MaxLength="8"></asp:TextBox>
</div>
</div>
</div>
</asp:Panel>
<div style="float: left; height: 3px; width: 100%">
&nbsp;
</div>
<asp:Panel ID="PnlValues" runat="server" GroupingText="ECD Value/Rate" CssClass="Panel">
<div class="div100pcelt">
<div class="div1pcelt">
&nbsp;
</div>
<div class="div32pcelt">
&nbsp;
</div>
<div class="div1pcelt">
&nbsp;
</div>
<div class="div32pcelt">
<div class="div20pcelt1">
Value
</div>
<div class="div80pcelt">
<asp:TextBox ID="TextBoxECDVal" CssClass="TextBox" runat="server" onKeyPress="return numbersonly(event,true)"
MaxLength="8" AutoPostBack="True" OnTextChanged="TextBoxECDVal_TextChanged"></asp:TextBox>
</div>
</div>
<div class="div1pcelt">
&nbsp;
</div>
<div class="div32pcelt">
<div class="div20pcelt1">
Rate
</div>
<div class="div80pcelt">
<asp:TextBox ID="TextBoxECDRate" CssClass="TextBox" runat="server" onKeyPress="return numbersonly(event,true)"
MaxLength="3" AutoPostBack="True" OnTextChanged="TextBoxECDRate_TextChanged"></asp:TextBox>
</div>
</div>
</div>
</asp:Panel>
</div>
</div>
<!-- TAB -->
</ContentTemplate>
</asp:TabPanel>
<asp:TabPanel ID="tbPnlGenarate" runat="server" HeaderText="Voucher Genarate">
<ContentTemplate>
<div style="float: left; width: 100%; color: Black; font-family: Verdana; font-size: 11px;">
<div style="height: 22px; text-align: right;" class="PanelHeader">
<asp:Button ID="ButtonProcess" runat="server" Text="Process" CssClass="Button" OnClick="ButtonProcess_Click" />
<asp:Button ID="ButtonGenClear" runat="server" Text="Clear" CssClass="Button" OnClick="ButtonGenClear_Click" />
<asp:Button ID="ButtonGenClose" runat="server" Text="Close" CssClass="Button" OnClick="ButtonGenClose_Click" />
</div>
<asp:Panel ID="pnlGenDates" runat="server" GroupingText="Date Range" CssClass="Panel">
<div class="div100pcelt">
<div class="div1pcelt">
&nbsp;
</div>
<div class="div32pcelt">
&nbsp;
</div>
<div class="div1pcelt">
&nbsp;
</div>
<div class="div32pcelt">
<div class="div20pcelt1">
Date
</div>
<div class="div80pcelt">
<asp:TextBox ID="TextBoxVouGenTo" CssClass="TextBox" runat="server" Enabled="False"
Width="80px"></asp:TextBox>
<asp:Image ID="Image3" runat="server" ImageUrl="~/Images/Calendar_scheduleHS.png" />
<asp:CalendarExtender ID="CalendarExtender3" runat="server" TargetControlID="TextBoxVouGenTo"
PopupButtonID="Image3" Enabled="True" Format="dd/MMM/yyyy" OnClientDateSelectionChanged="LinkClick">
</asp:CalendarExtender>
</div>
</div>
</div>
</asp:Panel>
<!-- Second row-->
<asp:Panel ID="pnlGenPC" runat="server" CssClass="Panel" GroupingText="Profit Centers">
<div class="div100pcelt" style="margin-left: 20%; margin-right: 20%;">
<asp:Panel ID="pnlGenPC1" runat="server" GroupingText=" " Width="60%">
<div class="div100pcelt" style="max-height: 200px; overflow: auto;">
<asp:GridView ID="GridViewVouGenPC" runat="server" Width="100%" ShowHeaderWhenEmpty="True"
AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" CssClass="GridView">
<AlternatingRowStyle BackColor="White" />
<Columns>
<asp:TemplateField>
<ItemTemplate>
<asp:CheckBox ID="chkSelect" Checked="true" runat="server" />
</ItemTemplate>
</asp:TemplateField>
<asp:BoundField DataField="PC" HeaderText="Code" />
<asp:BoundField DataField="DESC" HeaderText="Description" />
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
</asp:Panel>
</div>
</asp:Panel>
</div>
</ContentTemplate>
</asp:TabPanel>
<asp:TabPanel ID="tbPnlPrint" runat="server" HeaderText="Voucher Print">
<ContentTemplate>
<div style="float: left; width: 100%; color: Black; font-family: Verdana; font-size: 11px;">
<div style="height: 22px; text-align: right;" class="PanelHeader">
<asp:Button ID="ButtonPrint" runat="server" Text="Print" CssClass="Button" OnClick="ButtonPrint_Click" />
<asp:Button ID="ButtonPrintClear" runat="server" Text="Clear" CssClass="Button" OnClick="ButtonPrintClear_Click" />
<asp:Button ID="ButtonPrintClose" runat="server" Text="Close" CssClass="Button" OnClick="ButtonPrintClose_Click" />
</div>
<asp:Panel ID="Panel6" runat="server" CssClass="Panel" GroupingText="Date Range">
<div class="div100pcelt">
<div class="div1pcelt">
&nbsp;
</div>
<div class="div32pcelt">
<div class="div20pcelt1">
From
</div>
<div class="div80pcelt">
<asp:TextBox ID="TextBoxDatePriFrom" CssClass="TextBox" runat="server" Enabled="False"
Width="80px"></asp:TextBox>
<asp:Image ID="Image5" runat="server" ImageUrl="~/Images/Calendar_scheduleHS.png" />
<asp:CalendarExtender ID="CalendarExtender5" runat="server" TargetControlID="TextBoxDatePriFrom"
PopupButtonID="Image5" Enabled="True" Format="dd/MMM/yyyy" OnClientDateSelectionChanged="LinkClick1">
</asp:CalendarExtender>
</div>
</div>
<div class="div1pcelt">
&nbsp;
</div>
<div class="div32pcelt">
<div class="div20pcelt1">
To
</div>
<div class="div80pcelt">
<asp:TextBox ID="TextBoxDatePriTo" CssClass="TextBox" runat="server" Enabled="False"
Width="80px"></asp:TextBox>
<asp:Image ID="Image4" runat="server" ImageUrl="~/Images/Calendar_scheduleHS.png" />
<asp:CalendarExtender ID="CalendarExtender4" runat="server" TargetControlID="TextBoxDatePriTo"
PopupButtonID="Image4" Enabled="True" Format="dd/MMM/yyyy" OnClientDateSelectionChanged="LinkClick1">
</asp:CalendarExtender>
</div>
</div>
</div>
</asp:Panel>
<div class="div100pcelt">
<div style="float: left; width: 50%;">
<asp:Panel ID="Panel4" runat="server" CssClass="Panel" GroupingText="Profit Centers">
<div class="div100pcelt">
<asp:Panel ID="Panel5" runat="server" GroupingText=" " Width="100%">
<div class="div100pcelt" style="max-height: 200px; overflow: auto;">
<asp:GridView ID="GridViewVouPrntPC" runat="server" Width="100%" ShowHeaderWhenEmpty="True"
AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" CssClass="GridView">
<AlternatingRowStyle BackColor="White" />
<Columns>
<asp:TemplateField>
<ItemTemplate>
<asp:CheckBox ID="chkSelect" Checked="true" runat="server" OnCheckedChanged="chkSeclect_CheckdChanged"
AutoPostBack="true" />
</ItemTemplate>
</asp:TemplateField>
<asp:BoundField DataField="PC" HeaderText="Code" />
<asp:BoundField DataField="DESC" HeaderText="Description" />
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
</asp:Panel>
</div>
</asp:Panel>
</div>
<div style="float: left; width: 50%;">
<div class="div100pcelt">
<asp:Panel ID="Panel7" runat="server" GroupingText="Schmes Codes" Width="100%" CssClass="Panel">
<div class="div100pcelt" style="max-height: 200px; overflow: auto;">
<asp:GridView ID="GridViewSchmes" runat="server" Width="100%" ShowHeaderWhenEmpty="True"
AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" CssClass="GridView">
<AlternatingRowStyle BackColor="White" />
<Columns>
<asp:TemplateField>
<ItemTemplate>
<asp:CheckBox ID="chkSelect1" Checked="true" runat="server" OnCheckedChanged="chkSeclect1_CheckdChanged"
AutoPostBack="true" />
</ItemTemplate>
</asp:TemplateField>
<asp:BoundField DataField="CODE" HeaderText="Sch Cd" />
<asp:BoundField DataField="DESC" HeaderText="Description" />
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
</asp:Panel>
</div>
</div>
</div>
<div class="div100pcelt">
<div style="float: left; width: 50%;">
<asp:Panel ID="Panel8" runat="server" CssClass="Panel" GroupingText="Rata Or Amount">
<div class="div100pcelt">
<asp:Panel ID="Panel9" runat="server" GroupingText=" " Width="100%">
<div class="div100pcelt" style="max-height: 200px; overflow: auto;">
<asp:GridView ID="GridViewRate" runat="server" Width="100%" ShowHeaderWhenEmpty="True"
AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" CssClass="GridView">
<AlternatingRowStyle BackColor="White" />
<Columns>
<asp:TemplateField>
<ItemTemplate>
<asp:CheckBox ID="chkSelect2" Checked="true" runat="server" OnCheckedChanged="chkSeclect2_CheckdChanged"
AutoPostBack="true" />
</ItemTemplate>
</asp:TemplateField>
<asp:BoundField DataField="HED_ECD_IS_RT" HeaderText="Is Rate" />
<asp:BoundField DataField="HED_ECD_VAL" HeaderText="Value" />
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
</asp:Panel>
</div>
</asp:Panel>
</div>

<div style="float: left; width: 50%;">
<asp:Panel ID="Panel10" runat="server" CssClass="Panel" GroupingText="Voucher No">
<div class="div100pcelt">
<asp:Panel ID="Panel11" runat="server" GroupingText=" " Width="100%">
<div class="div100pcelt" style="max-height: 200px; overflow: auto;">
<asp:GridView ID="GridViewVouNo" runat="server" Width="100%" ShowHeaderWhenEmpty="True"
AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" CssClass="GridView">
<AlternatingRowStyle BackColor="White" />
<Columns>
<asp:TemplateField>
<ItemTemplate>
<asp:CheckBox ID="chkSelect2" Checked="true" runat="server"
/>
</ItemTemplate>
</asp:TemplateField>
<asp:BoundField DataField="hed_vou_no" HeaderText="Vou No" />

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
</asp:Panel>
</div>
</asp:Panel>
</div>
</div>
</div>
</ContentTemplate>
</asp:TabPanel>
</asp:TabContainer>
</ContentTemplate>
</asp:UpdatePanel>
</asp:Content>
