<%@ Page Title="Dealer Commission" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="DealerCommission.aspx.cs" Inherits="FF.WebERPClient.Sales_Module.DealerCommission" %>

<%@ Register Src="../UserControls/uc_ProfitCenterSearch.ascx" TagName="uc_ProfitCenterSearch"
    TagPrefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="upMain" runat="server">
        <ContentTemplate>
            <div style="height: 22px; text-align: right;" class="PanelHeader">
                <asp:Button ID="ButtonSearch" runat="server" Text="Search" CssClass="Button" OnClick="ButtonSearch_Click" />
                <asp:Button ID="ButtonApply" runat="server" Text="Apply" CssClass="Button" 
                    onclick="ButtonApply_Click" />
                <asp:Button ID="ButtonClear" runat="server" Text="Clear" CssClass="Button" OnClick="ButtonClear_Click" />
                <asp:Button ID="ButtonClose" runat="server" Text="Close" CssClass="Button" OnClick="ButtonClose_Click" />
            </div>
            <div style="float: left; height: 10px; width: 100%">
                &nbsp;
            </div>
            <div style="float: left; width: 100%;">
                <asp:Panel ID="Panel12" runat="server" GroupingText="Business Hirc Details" CssClass="Panel">
                    <div style="float: left; width: 65%;">
                        <uc1:uc_ProfitCenterSearch ID="uc_ProfitCenterSearchLoyaltyType" runat="server" />
                    </div>
                    <div style="float: left; width: 10%; text-align: center;">
                        <asp:ImageButton ID="ImageButtonAddPC" runat="server" ImageUrl="~/Images/right_arrow_icon.png"
                            Width="30%" ToolTip="Add to Profit Center List" OnClick="ImageButtonAddPC_Click" />
                    </div>
                    <div style="float: left; width: 15%; text-align: right;">
                        <asp:Panel ID="Panel13" runat="server" Height="120px" ScrollBars="Vertical" BorderColor="Blue"
                            BorderWidth="1px" GroupingText="Profit Centers">
                            <asp:GridView ID="GridViewPC" runat="server" AutoGenerateColumns="False">
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
            <div style="float: left; height: 10px; width: 100%">
                &nbsp;
            </div>
            <div style="float: left; width: 100%">
                <asp:Panel ID="Panel1" runat="server" GroupingText=" ">
                    <div style="float: left; width: 50%">
                        <div style="float: left; width: 1%; color: Black;">
                            &nbsp;
                        </div>
                        <div style="float: left; width: 30%;">
                            From Date
                        </div>
                        <div style="float: left; width: 69%;">
                            <asp:TextBox ID="TextBoxFromDate" CssClass="TextBox" runat="server" Enabled="false"></asp:TextBox>
                            <asp:Image ID="Image1" runat="server" ImageUrl="~/Images/Calendar_scheduleHS.png" />
                            <asp:CalendarExtender ID="CalendarExtender5" runat="server" TargetControlID="TextBoxFromDate"
                                PopupButtonID="Image1" Enabled="True" Format="dd/MMM/yyyy">
                            </asp:CalendarExtender>
                        </div>
                    </div>
                    <div style="float: left; width: 50%">
                        <div style="float: left; width: 1%; color: Black;">
                            &nbsp;
                        </div>
                        <div style="float: left; width: 30%;">
                            To Date
                        </div>
                        <div style="float: left; width: 69%;">
                            <asp:TextBox ID="TextBoxToDate" CssClass="TextBox" runat="server" Enabled="false"></asp:TextBox>
                            <asp:Image ID="Image2" runat="server" ImageUrl="~/Images/Calendar_scheduleHS.png" />
                            <asp:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="TextBoxToDate"
                                PopupButtonID="Image2" Enabled="True" Format="dd/MMM/yyyy">
                            </asp:CalendarExtender>
                        </div>
                    </div>
                </asp:Panel>
            </div>
            <div style="float: left; height: 10px; width: 100%">
                &nbsp;
            </div>
            <div style="float: left; width: 100%">
                <asp:Panel ID="Panel2" runat="server" GroupingText=" ">
                    <asp:GridView ID="GridViewItemDetails" runat="server" Width="99%" EmptyDataText="No data found"
                        ShowHeaderWhenEmpty="True" AutoGenerateColumns="False" AllowPaging="True" PageSize="10" 
                        CellPadding="4" ForeColor="#333333" GridLines="Both" CssClass="GridView" OnPageIndexChanging="GridViewItemDetails_PageIndexChanging">
                        <AlternatingRowStyle BackColor="White" />
                        <Columns>
                            <asp:BoundField HeaderText="Inv. Type" DataField="TYPE" />
                            <asp:BoundField HeaderText="Inv. No" DataField="SAH_INV_NO" />
                            <asp:BoundField HeaderText="Inv. Date" DataField="SAH_DT" 
                                HeaderStyle-Width="100px" DataFormatString='<%$ appSettings:FormatToDate %>'
                                HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                            <asp:BoundField HeaderText="Itm. Code" DataField="SAD_ITM_CD" />
                            <asp:BoundField HeaderText="Model" DataField="MODEL" />
                            <asp:BoundField HeaderText="Qty" DataField="SAD_QTY" HeaderStyle-HorizontalAlign="Right" />
                            <asp:BoundField HeaderText="Net Amo" DataField="SAD_UNIT_AMT" HeaderStyle-Width="100px"
                                HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right" DataFormatString='<%$ appSettings:FormatToCurrency %>' />
                            <asp:BoundField HeaderText="Total Amo" DataField="SAD_TOT_AMT" HeaderStyle-Width="100px"
                                HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right" DataFormatString='<%$ appSettings:FormatToCurrency %>' />
                            <asp:BoundField HeaderText="Comm. Amo" DataField="SAD_COMM_AMT" HeaderStyle-Width="100px"
                                HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right" DataFormatString='<%$ appSettings:FormatToCurrency %>' />
                            <asp:BoundField HeaderText="Price Book" DataField="SAD_PBOOK" HeaderStyle-HorizontalAlign="Right"
                                ItemStyle-HorizontalAlign="Right" />
                            <asp:BoundField HeaderText="PB Lvl" DataField="SAD_PB_LVL" />
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
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
