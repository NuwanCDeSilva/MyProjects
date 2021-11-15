<%@ Page Title="Vehical Registration Definition" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="VehicalRegistrationDefinition.aspx.cs" Inherits="FF.WebERPClient.Advance_Module.VehicalRegistrationDefinition" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script type="text/javascript">
        function DisplayConfirm() {
            var val = document.getElementById('<%=HiddenFieldRowCount.ClientID %>').value;
            if (val != null && val != "" && val != "0") {
                return confirm('Are you sure?');
            }
            else {
                alert('Please fill required information before save');
                return false;
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
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="updtMainPnl" runat="server">
        <ContentTemplate>
            <div style="float: left; width: 100%; color: Black;">
                <asp:HiddenField ID="HiddenFieldRowCount" runat="server" Value="0" />
                <div style="height: 22px; text-align: right;" class="PanelHeader">
                    <asp:Button ID="ButtonSave" runat="server" Text="Save" CssClass="Button" OnClick="ButtonSave_Click"
                        OnClientClick="return DisplayConfirm()" />
                    <asp:Button ID="ButtonClear" runat="server" Text="Clear" CssClass="Button" OnClick="ButtonClear_Click" />
                    <asp:Button ID="ButtonClose" runat="server" Text="Close" CssClass="Button" OnClick="ButtonClose_Click" />
                </div>
                <div style="height:3px;float: left; width: 100%; color: Black;">
                        &nbsp;
                    </div>
                <div class="PanelHeader">
                    Company Details
                </div>
                <div style="height:1px;float: left; width: 100%; color: Black;">
                     &nbsp;   
                    </div>
                <div style="float: left; width: 100%; color: Black;">
                    <%--Party Type and code--%>
                    <div style="float: left; width: 1%;">
                        &nbsp;
                    </div>
                    <div style="float: left; width: 35%;">
                        <div style="float: left; width: 25%;">
                            Company
                        </div>
                        <div style="float: left; width: 70%;">
                            <asp:DropDownList ID="DropDownListCompany" runat="server" CssClass="ComboBox" 
                                Width="150px" AutoPostBack="true" AppendDataBoundItems="true">
                            </asp:DropDownList>
                        </div>
                        <div style="float: left; width: 25%;">
                            Location
                        </div>
                        <div style="float: left; width: 70%;">
                           <asp:TextBox ID="TextBoxLoc" runat="server" CssClass="TextBox"></asp:TextBox>
                           <asp:CheckBox ID="CheckBoxAll" runat="server" Text="ALL" />
                        </div>
                        <div style="float: left; width: 85%;text-align:right;padding-top:1px;">
                        <asp:Button ID="ButtonSearchLoc" runat="server"  Text="Search" CssClass="Button" 
                                onclick="ButtonSearchLoc_Click"/>
                         </div>
                    </div>
                    <div style="float: left; width: 1%;">
                        &nbsp;
                    </div>
                    <div style="float: left; width: 63%;">
                        <div style="float: left; width: 20%;">
                            Locations
                        </div>
                        <div style="float: left; width: 80%;">
                            <asp:ListBox ID="ListBoxLoc" runat="server" CssClass="ComboBox" Rows="4" Width="370px" SelectionMode="Multiple">
                            </asp:ListBox>
                            <asp:LinkButton ID="LinkButtonLclear" runat="server" Text="Clear" 
                                onclick="LinkButtonLclear_Click"></asp:LinkButton>
                        </div>
                    </div>
                    <div style="float: left; width: 85%; text-align: right;height:1px;">
                    &nbsp;
                       <%-- <asp:Button ID="ButtonPAdd" runat="server" CssClass="Button" Text="Add" OnClick="ButtonPAdd_Click" />--%>
                    </div>
                </div>
                <%--grid view--%>
            <%--    <div style="float: left; width: 100%; color: Black; padding-top: 5px;">
                    <div style="float: left; width: 1%;">
                        &nbsp;
                    </div>
                    <asp:Panel ID="Panelparty" runat="server" Height="60px" ScrollBars="Auto">
                    <asp:GridView ID="GridViewParty" runat="server" Width="98%" EmptyDataText="No data found"
                        ShowHeaderWhenEmpty="True" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333"
                        GridLines="Both" CssClass="GridView" OnRowDeleting="GridViewParty_RowDeleting">
                        <AlternatingRowStyle BackColor="White" />
                        <Columns>
                            <asp:BoundField HeaderText="Company" DataField="Party_Type" />
                            <asp:BoundField HeaderText="PC" DataField="Party_Code" />
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:ImageButton ID="imgSelect" runat="server" CommandName="DELETE" ImageUrl="~/Images/Delete.png"
                                        Height="15px" />
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" Width="3%" Height="15px" />
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
                </div>--%>
                <%-- end of party--%>
                <%-- start of category--%>
                <div style="float: left; width: 100%; color: Black;">
                    <%-- 1st row--%>
                    <div class="PanelHeader">
                        Item Details
                    </div>
                    <div style="height:1px;float: left; width: 100%; color: Black;">
                        &nbsp;
                    </div>
                     <div style="float: left; width: 100%; color: Black;">
                       <div style="float: left; width: 1%;">
                                    &nbsp;
                                </div>
                    <div style="float: left; width: 35%; color: Black;">
                   
                        <div style="float: left; width: 100%; color: Black;">
                            <div style="float: left; width: 100%;">
                             <div style="float: left; width: 1%;">
                                    &nbsp;
                                </div>
                                <div style="float: left; width: 25%;">
                                    Brand
                                </div>
                                <div style="float: left; width: 70%;">
                                    <asp:DropDownList ID="DropDownListBrand" runat="server" CssClass="ComboBox" OnSelectedIndexChanged="DropDownListBrand_SelectedIndexChanged"
                                        AutoPostBack="true" Width="150px" AppendDataBoundItems="true">
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                        <%--2nd row--%>
                        <div style="float: left; width: 100%; color: Black; padding-top: 2px;">
                            <div style="float: left; width: 100%;">
                                <div style="float: left; width: 1%;">
                                    &nbsp;
                                </div>
                                <div style="float: left; width: 25%;">
                                    Category
                                </div>
                                <div style="float: left; width: 70%;">
                                    <asp:DropDownList ID="DropDownListCat" runat="server" Width="150px" CssClass="ComboBox" OnSelectedIndexChanged="DropDownListCat_SelectedIndexChanged"
                                        AutoPostBack="true" AppendDataBoundItems="true">
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                        <%-- 3rd row--%>
                        <div style="float: left; width: 100%; color: Black; padding-top: 2px;">
                            <div style="float: left; width: 100%;">
                                <div style="float: left; width: 1%;">
                                    &nbsp;
                                </div>
                                <div style="float: left; width: 25%;">
                                    Sub Category
                                </div>
                                <div style="float: left; width: 70%;">
                                    <asp:DropDownList ID="DropDownListSCat" runat="server" Width="150px" CssClass="ComboBox" OnSelectedIndexChanged="DropDownListSCat_SelectedIndexChanged"
                                        AutoPostBack="true" AppendDataBoundItems="true">
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                        <%-- 4th row--%>
                        <div style="float: left; width: 100%; color: Black; padding-top: 2px;">
                            <div style="float: left; width: 100%;">
                                <div style="float: left; width: 1%;">
                                    &nbsp;
                                </div>
                                <div style="float: left; width: 25%;">
                                    Item Model
                                </div>
                                <div style="float: left; width: 70%;">
                                    <asp:DropDownList ID="DropDownListIRange" runat="server" Width="150px" CssClass="ComboBox" OnSelectedIndexChanged="DropDownListIRange_SelectedIndexChanged"
                                        AutoPostBack="true"  AppendDataBoundItems="true">
                                    </asp:DropDownList>
                                    
                                </div>
                            </div>
                        </div>
                 <%--       5th row--%>
                        <div style="float: left; width: 100%; color: Black;">
                            <div style="float: left; width: 100%;">
                                <div style="float: left; width: 1%;">
                                    &nbsp;
                                </div>
                                <div style="float: left; width: 25%;">
                                    Item
                                </div>
                                <div style="float: left; width: 70%;">
                                    <asp:TextBox ID="TextBoxItem" runat="server" CssClass="TextBox"></asp:TextBox>
                           <asp:CheckBox ID="CheckBoxItemAll" runat="server" Text="ALL" />
                           
                                </div>
                            </div>
                        </div>
                        <div style="float: left; width: 85%; color: Black;text-align:right;">
                        <asp:Button ID="ButtonSearch" runat="server" Text="Search" CssClass="Button" 
                                        onclick="ButtonSearch_Click" />
                                        </div>
                    </div>
                      <div style="float: left; width: 1%;">
                        &nbsp;
                    </div>
                    <div style="float: left; width: 63%;">
                        <div style="float: left; width: 20%;">
                            Items
                        </div>
                        <div style="float: left; width: 80%;">
                            <asp:ListBox Rows="7" ID="ListBoxItems" runat="server" CssClass="ComboBox" SelectionMode="Multiple" Width="370px">
                            </asp:ListBox>
                            <asp:LinkButton ID="LinkButtonItClear" runat="server" Text="Clear" 
                                onclick="LinkButtonItClear_Click"></asp:LinkButton>
                        </div>
                    </div>
                   <div style="float: left; width: 85%; text-align: right;height:1px;">
                   </div>
                        <%--<asp:Button ID="ButtonCAdd" runat="server" CssClass="Button" Text="Add" OnClick="ButtonCAdd_Click" />--%>
                    </div>
                    <%--grid view--%>
                   <%-- <div style="float: left; width: 100%; color: Black; padding-top: 5px;">
                        <div style="float: left; width: 1%;">
                            &nbsp;
                        </div>
                        <asp:Panel ID="PanelGri" runat="server" Height="60px" ScrollBars="Auto">
                            <asp:GridView ID="GridViewBrand" runat="server" Width="98%" EmptyDataText="No data found"
                                ShowHeaderWhenEmpty="True" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333"
                                GridLines="Both" CssClass="GridView" OnRowDeleting="GridViewBrand_RowDeleting1">
                                <AlternatingRowStyle BackColor="White" />
                                <Columns>
                                   <asp:BoundField HeaderText="Brand" DataField="Brand" />
                                    <asp:BoundField HeaderText="Category" DataField="Category" />
                                    <asp:BoundField HeaderText="Sub Category" DataField="Sub_Category" />
                                    <asp:BoundField HeaderText="Item Range" DataField="Item_Range" />
                                    <asp:BoundField HeaderText="Item" DataField="Item" />
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:ImageButton ID="imgSelect" runat="server" CommandName="DELETE" ImageUrl="~/Images/Delete.png"
                                                Height="15px" />
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" Width="3%" Height="15px" />
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
                    </div>--%>
                </div>
                <%--end of brand details--%>
                <%-- start of other datails--%>
                <div style="float: left; width: 100%; color: Black;">
                    <%-- 1st row--%>
                    <div class="PanelHeader">
                        Other Details
                    </div>
                    <div style="float: left; width: 100%; color: Black;height:1px;">
                        &nbsp;
                    </div>
                    <div style="float: left; width: 100%; color: Black;">
                        <div style="float: left; width: 1%;">
                            &nbsp;
                        </div>
                        <div style="float: left; width: 32%;">
                            <div style="float: left; width: 25%;">
                                From
                            </div>
                            <div style="float: left; width: 70%;">
                                <asp:TextBox ID="TextBoxFrom" CssClass="TextBox" Enabled="False" runat="server"></asp:TextBox>
                                <asp:Image ID="imgFromDate" runat="server" ImageUrl="~/Images/Calendar_scheduleHS.png" />
                                <asp:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="TextBoxFrom"
                                    PopupButtonID="imgFromDate" Enabled="True" Format="dd/MMM/yyyy">
                                </asp:CalendarExtender>
                            </div>
                        </div>
                        <div style="float: left; width: 1%;">
                            &nbsp;
                        </div>
                        <div style="float: left; width: 32%;">
                            <div style="float: left; width: 25%;">
                                To
                            </div>
                            <div style="float: left; width: 70%;">
                                <asp:TextBox ID="TextBoxTo" CssClass="TextBox" Enabled="False" runat="server"></asp:TextBox>
                                <asp:Image ID="ImageTo" runat="server" ImageUrl="~/Images/Calendar_scheduleHS.png" />
                                <asp:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="TextBoxTo"
                                    PopupButtonID="ImageTo" Enabled="True" Format="dd/MMM/yyyy">
                                </asp:CalendarExtender>
                            </div>
                        </div>
                        <div style="float: left; width: 1%;">
                            &nbsp;
                        </div>
                        <div style="float: left; width: 32%;">
                            <div style="float: left; width: 30%;">
                                Sales Type
                            </div>
                            <div style="float: left; width: 70%;">
                                <asp:DropDownList ID="DropDownListSType" runat="server" CssClass="ComboBox" Width="200px">
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    <%--2nd row--%>
                    <div style="float: left; width: 100%; color: Black; padding-top: 2px;">
                        <div style="float: left; width: 1%;">
                            &nbsp;
                        </div>
                        <div style="float: left; width: 32%;">
                            <div style="float: left; width: 25%;">
                                Reg. Value
                            </div>
                            <div style="float: left; width: 70%;">
                                <asp:TextBox ID="TextBoxRvalue" CssClass="TextBox" runat="server" Text="0" style="text-align: right;" onKeyPress="return numbersonly(event,true)"></asp:TextBox>
                            </div>
                        </div>
                        <div style="float: left; width: 1%;">
                            &nbsp;
                        </div>
                        <div style="float: left; width: 32%;">
                            <div style="float: left; width: 25%;">
                                Claim Value
                            </div>
                            <div style="float: left; width: 70%;">
                                <asp:TextBox ID="TextBoxCvalue" CssClass="TextBox" runat="server" Text="0" style="text-align: right;" onKeyPress="return numbersonly(event,true)"></asp:TextBox>
                            </div>
                        </div>
                         <div style="float: left; width: 1%;">
                            &nbsp;
                        </div>
                        <div style="float: left; width: 32%;">
                            <div style="float: left; width: 25%;">
                                Mandatory
                            </div>
                            <div style="float: left; width: 70%;">
                               <asp:CheckBox ID="CheckBoxMandatory" runat="server" />
                            </div>
                        </div>
                    </div>
                    <div style="float: left; width: 85%; text-align: right;">
                        <asp:Button ID="ButtonOAdd" runat="server" CssClass="Button" Text="Add" OnClick="ButtonOAdd_Click" />
                    </div>
                    <%--grid view--%>
                    <div style="float: left; width: 100%; color: Black; padding-top: 5px;">
                        <div style="float: left; width: 1%;">
                            &nbsp;
                        </div>
                        <asp:Panel ID="PanelFinal" runat="server" Height="220px" ScrollBars="Auto">
                        <asp:GridView ID="GridViewFinal" runat="server" Width="98%" EmptyDataText="No data found"
                            ShowHeaderWhenEmpty="True" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333"
                            CssClass="GridView"  OnRowDeleting="GridViewFinal_RowDeleting">
                            <AlternatingRowStyle BackColor="White" />
                            <Columns>
                             <%--   <asp:TemplateField HeaderText="Party Details">
                                    <ItemTemplate>
                                        <asp:Panel runat="server" ID="P1" ScrollBars="Auto" Height="60px">
                                            <asp:GridView ID="GridViewCParty" runat="server" Width="98%" EmptyDataText="No data found"
                                                ShowHeaderWhenEmpty="True" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333"
                                                GridLines="Both" CssClass="GridView">
                                                <AlternatingRowStyle BackColor="White" />
                                                <Columns>
                                                    <asp:BoundField HeaderText="Party Type" DataField="Party_Type" />
                                                    <asp:BoundField HeaderText="Party Code" DataField="Party_Code" />
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
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Brand Details">
                                    <ItemTemplate>
                                        <asp:Panel runat="server" ID="P2" ScrollBars="Auto" Height="60px">
                                            <asp:GridView ID="GridViewCBrand" runat="server" Width="98%" EmptyDataText="No data found"
                                                ShowHeaderWhenEmpty="True" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333"
                                                GridLines="Both" CssClass="GridView">
                                                <AlternatingRowStyle BackColor="White" />
                                                <Columns>
                                                    <asp:BoundField HeaderText="Brand" DataField="Brand" />
                                                    <asp:BoundField HeaderText="Category" DataField="Category" />
                                                    <asp:BoundField HeaderText="Sub Category" DataField="Sub_Category" />
                                                    <asp:BoundField HeaderText="Item Range" DataField="Item_Range" />
                                                    <asp:BoundField HeaderText="Item" DataField="Item" />
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
                                    </ItemTemplate>
                                </asp:TemplateField>--%>
                                <asp:BoundField HeaderText="From" DataField="From" DataFormatString='<%$ appSettings:FormatToDate %>'
                                    HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                                <asp:BoundField HeaderText="To" DataField="To" DataFormatString='<%$ appSettings:FormatToDate %>'
                                    HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                                <asp:BoundField HeaderText="Sales Type" DataField="Sales_Type" />
                                <asp:BoundField HeaderText="Registration Value" DataField="Registration_Value" />
                                <asp:BoundField HeaderText="Claim Value" DataField="Claim_Value" />
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:ImageButton ID="imgSelect" runat="server" CommandName="DELETE" ImageUrl="~/Images/Delete.png"
                                            Height="15px" />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" Width="3%" Height="15px" />
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
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
