<%@ Page Title="Registration and Insurance Fee Info" Language="C#" MasterPageFile="~/Site.Master"
    AutoEventWireup="true" CodeBehind="RegistrationAndInsuranceFeeInfo.aspx.cs" Inherits="FF.WebERPClient.General_Modules.RegistrationAndInsuranceFeeInfo" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script type="text/javascript">
        function clickButton(e, buttonid) {
            var evt = window.event;
            var bt = document.getElementById(buttonid);
            if (bt) {
                if (evt.keyCode == 113) {
                    bt.click();
                    return false;
                }
            }
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
    <asp:UpdatePanel ID="updtMainPnl" runat="server">
        <ContentTemplate>
            <div style="float: left; width: 100%; color: Black;">
                <div style="text-align: right;" class="PanelHeader">
                    <asp:Button ID="ButtonView" runat="server" Text="View" CssClass="Button" OnClick="ButtonView_Click" />
                    <asp:Button ID="ButtonClear" runat="server" Text="Clear" CssClass="Button" OnClick="ButtonClear_Click" />
                    <asp:Button ID="ButtonClose" runat="server" Text="Close" CssClass="Button" OnClick="ButtonClose_Click" />
                </div>
                <div style="height: 10px; float: left; width: 100%; color: Black;">
                    &nbsp;
                </div>
                <div style="float: left; width: 100%; color: Black;">
                    <asp:Panel ID="Panel1" runat="server" GroupingText=" " CssClass="Panel">
                        <div style="float: left; width: 100%; color: Black;">
                            <asp:Panel ID="Panel2" runat="server" GroupingText="Item Details" CssClass="Panel">
                                <div style="height: 10px; float: left; width: 100%; color: Black;">
                                    &nbsp;
                                </div>
                                <%--search terms--%>
                                <%--1st row--%>
                                <div style="float: left; width: 100%; color: Black;">
                                    <div style="float: left; width: 1%; color: Black;">
                                        &nbsp;
                                    </div>
                                    <div style="float: left; width: 48%; color: Black;">
                                        <div style="float: left; width: 30%;">
                                            Sales Type
                                        </div>
                                        <div style="float: left; width: 70%;">
                                            <asp:TextBox ID="TextBoxSalesType" CssClass="TextBoxUpper" runat="server"></asp:TextBox>
                                            <asp:ImageButton ID="ImageButtonSalesType" runat="server" ImageUrl="~/Images/icon_search.png"
                                                Style="width: 16px" OnClick="ImageButtonSalesType_Click" />
                                        </div>
                                    </div>
                                    <div style="float: left; width: 1%; color: Black;">
                                        &nbsp;
                                    </div>
                                    <div style="float: left; width: 48%; color: Black;">
                                        <div style="float: left; width: 30%;">
                                            Model
                                        </div>
                                        <div style="float: left; width: 70%;">
                                            <asp:TextBox ID="TextBoxModel" CssClass="TextBoxUpper" runat="server"></asp:TextBox>
                                            <asp:ImageButton ID="ImageButtonModel" runat="server" ImageUrl="~/Images/icon_search.png"
                                                Style="width: 16px" OnClick="ImageButtonModel_Click" />
                                        </div>
                                    </div>
                                </div>
                                <%-- end of 1st row--%>
                                <%-- 2nd row--%>
                                <div style="float: left; width: 100%; color: Black;">
                                    <div style="float: left; width: 1%; color: Black;">
                                        &nbsp;
                                    </div>
                                    <div style="float: left; width: 48%; color: Black;">
                                        <div style="float: left; width: 30%;">
                                            Term
                                        </div>
                                        <div style="float: left; width: 70%;">
                                            <asp:TextBox ID="TextBoxTerm" CssClass="TextBoxUpper" runat="server"></asp:TextBox>
                                            <asp:ImageButton ID="ImageButtonTerm" runat="server" ImageUrl="~/Images/icon_search.png"
                                                Style="width: 16px" OnClick="ImageButtonTerm_Click" />
                                        </div>
                                    </div>
                                    <div style="float: left; width: 1%; color: Black;">
                                        &nbsp;
                                    </div>
                                    <div style="float: left; width: 48%; color: Black;">
                                        <div style="float: left; width: 30%;">
                                            Item
                                        </div>
                                        <div style="float: left; width: 70%;">
                                            <asp:TextBox ID="TextBoxItem" CssClass="TextBoxUpper" runat="server"></asp:TextBox>
                                            <asp:ImageButton ID="ImageButtonItem" runat="server" ImageUrl="~/Images/icon_search.png"
                                                Style="width: 16px" OnClick="ImageButtonItem_Click" />
                                            &nbsp; &nbsp; &nbsp; &nbsp;
                                            <asp:Button ID="ButtonAdd" runat="server" Text="Add" CssClass="Button" OnClick="ButtonAdd_Click" />
                                        </div>
                                    </div>
                                </div>
                                <%--end of 2nd row--%>
                            </asp:Panel>
                            <div style="float: left; width: 100%; color: Black;">
                                <asp:Panel ID="Panel3" runat="server" GroupingText="Items" CssClass="Panel">
                                    <div style="float: left; width: 100%; color: Black;">
                                        <asp:Panel ID="Panel4" runat="server">
                                            <div style="height: 10px; float: left; width: 100%; color: Black;">
                                                &nbsp;
                                            </div>
                                            <asp:GridView ID="GridViewItems" runat="server" Width="100%" EmptyDataText="No data found"
                                                ShowHeaderWhenEmpty="True" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333"
                                                CssClass="GridView" OnRowDeleting="GridViewItems_RowDeleting" GridLines="None"
                                                AllowPaging="true" PageSize="4" OnPageIndexChanging="GridViewItems_PageIndexChanging">
                                                <AlternatingRowStyle BackColor="White" />
                                                <Columns>
                                                    <asp:TemplateField HeaderStyle-Width="20px">
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="CheckBoxGridSelect" runat="server" Checked="true" />
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" Width="20px" Height="15px" />
                                                    </asp:TemplateField>
                                                    <asp:BoundField HeaderText="Item" DataField="Item" />
                                                    <asp:BoundField HeaderText="Model" DataField="Model" />
                                                    <asp:BoundField HeaderText="Term" DataField="Term" />
                                                    <asp:BoundField HeaderText="Sales Type" DataField="Sales_Type" />
                                                    <asp:TemplateField>
                                                        <ItemTemplate>
                                                            <asp:ImageButton ID="ImageButton5" runat="server" ImageUrl="~/Images/Delete.png"
                                                                CommandName="DELETE" OnClientClick="return confirm('Are you sure?')" />
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
                                    </div>
                                </asp:Panel>
                            </div>
                        </div>
                        <%-- main detail grid--%>
                        <div style="float: left; width: 100%; color: Black;">
                            <asp:Panel ID="Panel5" runat="server" GroupingText="All Details" CssClass="Panel">
                                <div style="float: left; width: 100%; color: Black;">
                                    <asp:Panel ID="Panel6" runat="server">
                                        <div style="float: left; width: 100%; color: Black;">
                                            <asp:Panel ID="Panel7" runat="server">
                                                <div style="height: 10px; float: left; width: 100%; color: Black;">
                                                    &nbsp;
                                                </div>
                                                <asp:GridView ID="GridViewMainDetails" runat="server" Width="100%" EmptyDataText="No data found"
                                                    ShowHeaderWhenEmpty="True" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333"
                                                    CssClass="GridView" GridLines="None" AllowPaging="true" OnPageIndexChanging="GridViewMainDetails_PageIndexChanging">
                                                    <AlternatingRowStyle BackColor="White" />
                                                    <Columns>
                                                        <asp:BoundField HeaderText="Model" DataField="Model" />
                                                        <asp:BoundField HeaderText="Description" DataField="Description" />
                                                        <asp:BoundField HeaderText="Item" DataField="Item" />
                                                        <asp:BoundField HeaderText="Sales Type" DataField="Sales_Type" />
                                                        <asp:BoundField HeaderText="Term" DataField="Term" DataFormatString='<%$ appSettings:FormatToCurrency  %>'>
                                                            <HeaderStyle HorizontalAlign="Right" />
                                                            <ItemStyle HorizontalAlign="Right" />
                                                        </asp:BoundField>
                                                        <asp:BoundField HeaderText="Ins. Fee" DataField="Tot_Val" DataFormatString='<%$ appSettings:FormatToCurrency  %>'>
                                                            <HeaderStyle HorizontalAlign="Right" />
                                                            <ItemStyle HorizontalAlign="Right" />
                                                        </asp:BoundField>
                                                        <asp:BoundField HeaderText="Reg. Fee" DataField="Reg_val" DataFormatString='<%$ appSettings:FormatToCurrency  %>'>
                                                            <HeaderStyle HorizontalAlign="Right" />
                                                            <ItemStyle HorizontalAlign="Right" />
                                                        </asp:BoundField>
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
                            </asp:Panel>
                        </div>
                    </asp:Panel>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
