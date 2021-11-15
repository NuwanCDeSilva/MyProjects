<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="uc_ItemDetailSearch.ascx.cs"
    Inherits="FF.AbansTours.UserControls.uc_ItemDetailSearch" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<div style="float: left; width: 100%;">
    <div style="float: left; width: 60%;">
        <div style="float: left; width: 100%;">
            <div style="float: left; width: 30%;">
                Brand
            </div>
            <div style="float: left; width: 70%;">
                <asp:CheckBox ID="CheckBoxBrand" runat="server" AutoPostBack="True" OnCheckedChanged="CheckBoxBrand_CheckedChanged" />
                <asp:TextBox ID="TextBoxBrand" runat="server" CssClass="TextBox TextBoxUpper"></asp:TextBox>
                <asp:ImageButton ID="ImageButtonBrand" runat="server" ImageUrl="~/images/icon_search.png"
                    OnClick="ImageButtonBrand_Click" />
            </div>
        </div>
        <div style="float: left; width: 100%;">
            <div style="float: left; width: 30%;">
                Main Category
            </div>
            <div style="float: left; width: 70%;">
                <asp:CheckBox ID="CheckBoxMainCategory" runat="server" AutoPostBack="True" OnCheckedChanged="CheckBoxMainCategory_CheckedChanged" />
                <asp:TextBox ID="TextBoxMainCategory" runat="server" CssClass="TextBox TextBoxUpper"></asp:TextBox>
                <asp:ImageButton ID="ImageButtonMainCategory" runat="server" ImageUrl="~/Images/icon_search.png"
                    OnClick="ImageButtonMainCategory_Click" />
            </div>
        </div>
        <div style="float: left; width: 100%;">
            <div style="float: left; width: 30%;">
                Sub Category
            </div>
            <div style="float: left; width: 70%;">
                <asp:CheckBox ID="CheckBoxSubCategory" runat="server" AutoPostBack="True" OnCheckedChanged="CheckBoxSubCategory_CheckedChanged" />
                <asp:TextBox ID="TextBoxSubCategory" runat="server" CssClass="TextBox TextBoxUpper"></asp:TextBox>
                <asp:ImageButton ID="ImageButtonSubCategory" runat="server" ImageUrl="~/Images/icon_search.png"
                    OnClick="ImageButtonSubCategory_Click" />
            </div>
        </div>
        <div style="float: left; width: 100%;">
            <div style="float: left; width: 30%;">
                Sub Category 1
            </div>
            <div style="float: left; width: 70%;">
                <asp:CheckBox ID="CheckBoxSubCategory1" runat="server" AutoPostBack="True" OnCheckedChanged="CheckBoxSubCategory1_CheckedChanged" />
                <asp:TextBox ID="TextBoxSubCategory1" runat="server" CssClass="TextBox TextBoxUpper"></asp:TextBox>
                <asp:ImageButton ID="ImageButtonSubCategory1" runat="server" ImageUrl="~/Images/icon_search.png"
                    OnClick="ImageButtonSubCategory1_Click" />
            </div>
        </div>
        <div style="float: left; width: 100%;">
            <div style="float: left; width: 30%;">
                Item
            </div>
            <div style="float: left; width: 70%;">
                <asp:CheckBox ID="CheckBoxProduct" runat="server" AutoPostBack="True" OnCheckedChanged="CheckBoxProduct_CheckedChanged" />
                <asp:TextBox ID="TextBoxProduct" runat="server" CssClass="TextBox TextBoxUpper"></asp:TextBox>
                <asp:ImageButton ID="ImageButtonProduct" runat="server" ImageUrl="~/Images/icon_search.png"
                    OnClick="ImageButtonProduct_Click" />
            </div>
        </div>
        <div style="float: left; width: 100%;">
            <div style="float: left; width: 30%;">
                Serial
            </div>
            <div style="float: left; width: 70%;">
                <asp:CheckBox ID="CheckBoxSerial" runat="server" AutoPostBack="True" OnCheckedChanged="CheckBoxSerial_CheckedChanged" />
                <asp:TextBox ID="TextBoxSerial" runat="server" CssClass="TextBox TextBoxUpper"></asp:TextBox>
                <asp:ImageButton ID="ImageButtonSerial" runat="server" ImageUrl="~/Images/icon_search.png"
                    OnClick="ImageButtonSerial_Click" />
            </div>
        </div>
        <div style="float: left; height: 10px; width: 100%">
            &nbsp;
        </div>
        <div style="float: left; width: 100%;">
            <div style="float: left; width: 30%;">
                Circular No
            </div>
            <div style="float: left; width: 70%;">
                <asp:CheckBox ID="CheckBoxCircular" runat="server" AutoPostBack="True" OnCheckedChanged="CheckBoxCircular_CheckedChanged" />
                <asp:TextBox ID="TextBoxCircular" runat="server" CssClass="TextBox TextBoxUpper"></asp:TextBox>
                <asp:ImageButton ID="ImageButtonCircular" runat="server" ImageUrl="~/Images/icon_search.png"
                    OnClick="ImageButtonCircular_Click" />
            </div>
        </div>
        <div style="float: left; width: 100%;">
            <div style="float: left; width: 30%;">
                Promotion
            </div>
            <div style="float: left; width: 70%;">
                <asp:CheckBox ID="CheckBoxPromation" runat="server" AutoPostBack="True" OnCheckedChanged="CheckBoxPromation_CheckedChanged1" />
                <asp:TextBox ID="TextBoxPromotion" runat="server" CssClass="TextBox TextBoxUpper"></asp:TextBox>
                <asp:ImageButton ID="ImageButtonPromotion" runat="server" ImageUrl="~/Images/icon_search.png"
                    OnClick="ImageButtonPromotion_Click1" Style="width: 16px" />
            </div>
        </div>
    </div>
    <div style="float: left; width: 10%; text-align: center;">
        <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/images/right_arrow_icon.png"
            Width="30%" ToolTip="Add to Item List" OnClick="ImageButton1_Click" />
    </div>
    <div style="float: left; width: 30%;">
        <asp:Label ID="LabelMessage" runat="server" Font-Bold="True" Font-Size="Small" ForeColor="#3366FF"></asp:Label>
        <asp:Panel ID="Panel13" runat="server" Height="120px" ScrollBars="Vertical" BorderColor="Blue"
            BorderWidth="1px" GroupingText="Select" Visible="false">
            <asp:GridView ID="GridViewItem" runat="server" AutoGenerateColumns="False" 
                >
                <Columns>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:Label ID="Label1" runat="server"></asp:Label>
                            <asp:CheckBox ID="chekPc" runat="server" Checked="true" AutoPostBack="True" 
                                oncheckedchanged="chekPc_CheckedChanged" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="MI_CD" ShowHeader="False" />
                </Columns>
            </asp:GridView>
            <asp:GridView ID="GridViewSerial" runat="server" AutoGenerateColumns="False">
                <Columns>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:Label ID="Label1" runat="server"></asp:Label>
                            <asp:CheckBox ID="chekPc" runat="server" Checked="true" AutoPostBack="True" 
                                oncheckedchanged="chekPc_CheckedChanged1" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="INS_ITM_CD" ShowHeader="False" />
                    <asp:BoundField DataField="INS_SER_1" ShowHeader="False" />
                </Columns>
            </asp:GridView>
            <asp:GridView ID="GridViewPromotion" runat="server" AutoGenerateColumns="False">
                <Columns>
                 <asp:TemplateField>
                        <ItemTemplate>
                            <asp:Label ID="Label1" runat="server"></asp:Label>
                            <asp:CheckBox ID="chekPc" runat="server" Checked="true" AutoPostBack="True" 
                                oncheckedchanged="chekPc_CheckedChanged2" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="Circular No" ShowHeader="False" />
                    <asp:BoundField DataField="Code" ShowHeader="False" />
                   
                </Columns>
            </asp:GridView>
                    <div style="float: left; width: 100%; text-align: right;">
            <div style="float: left; width: 30%; text-align: right;">
                <asp:Button ID="ButtonAll" runat="server" Text="All" CssClass="Button" 
                    Width="100%" onclick="ButtonAll_Click" />
            </div>
            <div style="float: left; width: 30%; text-align: right;">
                <asp:Button ID="ButtonNone" runat="server" Text="None" CssClass="Button" 
                    onclick="ButtonNone_Click" />
            </div>
            <div style="float: left; width: 30%; text-align: right;">
                <asp:Button ID="ButtonClearPc" runat="server" Text="Clear" CssClass="Button" 
                    onclick="ButtonClearPc_Click" />
            </div>
        </asp:Panel>

        </div>
    </div>
</div>
