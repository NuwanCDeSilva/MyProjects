<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="LoyaltyDefinitions.aspx.cs" Inherits="FF.WebERPClient.Sales_Module.LoyaltyDefinitions" %>

<%@ Register Src="../UserControls/uc_ProfitCenterSearch.ascx" TagName="uc_ProfitCenterSearch"
    TagPrefix="uc1" %>
<%@ Register Src="../UserControls/uc_ItemDetailSearch.ascx" TagName="uc_ItemDetailSearch"
    TagPrefix="uc2" %>
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

        function LinkClick() {
            document.getElementById('<%=LinkButtonTemp.ClientID %>').click();
        }
    </script>
    <style>
        .Panel legend
        {
            color: Blue;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Panel ID="temp" runat="server">
        <div style="height: 22px; text-align: right;" class="PanelHeader">
            <asp:Button ID="ButtonSave" runat="server" Text="Save" CssClass="Button" OnClick="ButtonSave_Click" />
            <asp:Button ID="ButtonClear" runat="server" Text="Clear" CssClass="Button" OnClick="ButtonClear_Click" />
            <asp:Button ID="ButtonClose" runat="server" Text="Close" CssClass="Button" OnClick="ButtonClose_Click" />
        </div>
        <asp:Panel ID="Panel21" runat="server">
            <asp:TabContainer ID="tcLoyaltyDefinition" runat="server" ActiveTabIndex="0">
                <%--Loyalty Types Tab--%>
                <asp:TabPanel ID="tbpLoyaltyType" HeaderText="Loyalty Types" runat="server">
                    <ContentTemplate>
                        <asp:Panel ID="Panel24" runat="server" ScrollBars="Auto" Height="505px">
                            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                <ContentTemplate>
                                    <div style="float: left; height: 10%; color: Black;">
                                        &nbsp;
                                    </div>
                                    <div style="float: left; width: 100%; color: Black; font-family: Verdana; font-size: 11px;">
                                        <div style="float: left; width: 65%;">
                                            <asp:Panel ID="Panel1" runat="server" GroupingText="Loyalty Type Details" CssClass="Panel">
                                                <div style="float: left; height: 1%; color: Black;">
                                                    &nbsp;
                                                </div>
                                                <div style="float: left; width: 100%;">
                                                    <div style="float: left; width: 49%;">
                                                        <div style="float: left; width: 30%;">
                                                            Loyalty Type
                                                        </div>
                                                        <div style="float: left; width: 70%;">
                                                            <asp:TextBox ID="TextBoxLoyaltyType" runat="server" CssClass="TextBox TextBoxUpper"
                                                                EnableViewState="false"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div style="float: left; width: 1%; color: Black;">
                                                        &nbsp;
                                                    </div>
                                                    <div style="float: left; width: 49%;">
                                                        <div style="float: left; width: 40%;">
                                                            Loyalty Description
                                                        </div>
                                                        <div style="float: left; width: 60%;">
                                                            <asp:TextBox ID="TextBoxLoyaltyDescription" runat="server" CssClass="TextBox TextBoxUpper"
                                                                Width="180px"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div style="float: left; width: 100%; padding-top: 1px;">
                                                    <div style="float: left; width: 49%;">
                                                        <div style="float: left; width: 30%;">
                                                            From Date
                                                        </div>
                                                        <div style="float: left; width: 70%;">
                                                            <asp:TextBox ID="TextBoxFromDate" CssClass="TextBox"  runat="server"></asp:TextBox>
                                                            <asp:Image ID="Image1" runat="server" ImageUrl="~/Images/Calendar_scheduleHS.png" />
                                                            <asp:CalendarExtender ID="CalendarExtender5" runat="server" TargetControlID="TextBoxFromDate"
                                                                PopupButtonID="Image1" Enabled="True" Format="dd/MMM/yyyy">
                                                            </asp:CalendarExtender>
                                                        </div>
                                                    </div>
                                                    <div style="float: left; width: 1%; color: Black;">
                                                        &nbsp;
                                                    </div>
                                                    <div style="float: left; width: 49%;">
                                                        <div style="float: left; width: 40%;">
                                                            To Date
                                                        </div>
                                                        <div style="float: left; width: 60%;">
                                                            <asp:TextBox ID="TextBoxToDate" CssClass="TextBox"  runat="server"></asp:TextBox>
                                                            <asp:Image ID="Image2" runat="server" ImageUrl="~/Images/Calendar_scheduleHS.png" />
                                                            <asp:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="TextBoxToDate"
                                                                PopupButtonID="Image2" Enabled="True" Format="dd/MMM/yyyy">
                                                            </asp:CalendarExtender>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div style="float: left; width: 100%; padding-top: 1px;">
                                                    <div style="float: left; width: 49%;">
                                                        <div style="float: left; width: 30%;">
                                                            Is Compulsory
                                                        </div>
                                                        <div style="float: left; width: 70%;">
                                                            <asp:CheckBox ID="CheckBoxIsCompl" runat="server" />
                                                        </div>
                                                    </div>
                                                    <div style="float: left; width: 1%; color: Black;">
                                                        &nbsp;
                                                    </div>
                                                    <div style="float: left; width: 49%;">
                                                        <div style="float: left; width: 40%;">
                                                            Allow Multiple
                                                        </div>
                                                        <div style="float: left; width: 60%;">
                                                            <asp:CheckBox ID="CheckBoxAllowMul" runat="server" />
                                                        </div>
                                                    </div>
                                                </div>
                                                <div style="float: left; width: 100%; padding-top: 1px;">
                                                    <div style="float: left; width: 49%;">
                                                        <div style="float: left; width: 30%;">
                                                            Renewal Chg
                                                        </div>
                                                        <div style="float: left; width: 70%;">
                                                            <asp:TextBox ID="TextBoxRenewalChg" runat="server" CssClass="TextBox TextBoxUpper"
                                                                onKeyPress="return numbersonly(event,true)"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div style="float: left; width: 1%; color: Black;">
                                                        &nbsp;
                                                    </div>
                                                    <div style="float: left; width: 49%;">
                                                        <div style="float: left; width: 40%;">
                                                            Valid Period
                                                        </div>
                                                        <div style="float: left; width: 60%;">
                                                            <asp:TextBox ID="TextBoxValidPeriod" runat="server" CssClass="TextBox TextBoxUpper"
                                                                onKeyPress="return numbersonly(event,false)"></asp:TextBox>
                                                            Days
                                                        </div>
                                                    </div>
                                                </div>
                                                <div style="float: left; width: 100%; padding-top: 1px;">
                                                    <div style="float: left; width: 49%;">
                                                        <div style="float: left; width: 30%;">
                                                            Member. Chg
                                                        </div>
                                                        <div style="float: left; width: 70%;">
                                                            <asp:TextBox ID="TextBoxMemChg" runat="server" CssClass="TextBox TextBoxUpper" onKeyPress="return numbersonly(event,true)"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                            </asp:Panel>
                                        </div>
                                        <div style="float: left; width: 1%; color: Black;">
                                            &nbsp;
                                        </div>
                                        <div style="float: left; width: 34%;">
                                            <asp:Panel ID="Panel2" runat="server" GroupingText="Price Book Details" CssClass="Panel">
                                                <div style="float: left; width: 100%; color: Black;">
                                                    <div style="float: left; height: 5%; width: 100%; color: Black;">
                                                        &nbsp;
                                                    </div>
                                                    <div style="float: left; width: 100%;">
                                                        <div style="float: left; width: 40%;">
                                                            Price Book
                                                        </div>
                                                        <div style="float: left; width: 60%;">
                                                            <asp:DropDownList ID="DropDownListPB" runat="server" AppendDataBoundItems="True"
                                                                CssClass="ComboBox" AutoPostBack="True" Width="100px" OnSelectedIndexChanged="DropDownListPB_SelectedIndexChanged">
                                                            </asp:DropDownList>
                                                            <asp:CheckBox ID="CheckBoxPBAll" runat="server" AutoPostBack="True" OnCheckedChanged="CheckBoxPBAll_CheckedChanged" />
                                                        </div>
                                                    </div>
                                                    <div style="float: left; width: 100%; padding-top: 3px;">
                                                        <asp:ListBox ID="ListBoxPBList" runat="server" Width="90%" Height="52px" SelectionMode="Multiple">
                                                        </asp:ListBox>
                                                    </div>
                                                    <div style="float: left; width: 100%; padding-top: 3px;">
                                                        <asp:LinkButton ID="LinkButtonPBClear" runat="server" Text="Clear" OnClick="LinkButtonPBClear_Click"></asp:LinkButton>
                                                    </div>
                                                </div>
                                            </asp:Panel>
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
                                                        <asp:GridView ID="GridViewPC" runat="server" AutoGenerateColumns="False" OnRowDataBound="GridViewPC_RowDataBound">
                                                            <Columns>
                                                                <asp:TemplateField>
                                                                    <EditItemTemplate>
                                                                        <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                                                                    </EditItemTemplate>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="Label1" runat="server"></asp:Label>
                                                                        <asp:CheckBox ID="chekPc" runat="server" />
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
                                        <div style="float: left; width: 100%;">
                                            <asp:Panel ID="Panel14" runat="server" GroupingText="Item Details" CssClass="Panel">
                                                <uc2:uc_ItemDetailSearch ID="uc_ItemDetailSearch1" runat="server" />
                                            </asp:Panel>
                                        </div>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </asp:Panel>
                    </ContentTemplate>
                </asp:TabPanel>
                <asp:TabPanel ID="TabPanel1" HeaderText="Loyalty Points" runat="server">
                    <ContentTemplate>
                        <asp:Panel ID="PanelDiscount" runat="server" ScrollBars="Auto" Height="505px">
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                <ContentTemplate>
                                    <div style="float: left; width: 100%; color: Black; font-family: Verdana; font-size: 11px;">
                                        <div style="float: left; width: 65%; color: Black;">
                                            <asp:Panel ID="Panel3" runat="server" GroupingText="Loyalty Point Details" CssClass="Panel">
                                                <div style="float: left; width: 100%; color: Black;">
                                                    <div style="float: left; width: 100%;">
                                                        <div style="float: left; width: 49%;">
                                                            <div style="float: left; width: 30%;">
                                                                Loyalty Type
                                                            </div>
                                                            <div style="float: left; width: 70%;">
                                                                <asp:TextBox ID="TextBoxLoyalPoLoyaltyType" CssClass="TextBox" runat="server" AutoPostBack="True"
                                                                    OnTextChanged="TextBoxLoyalPoLoyaltyType_TextChanged" onblur="LinkClick()"></asp:TextBox>
                                                                <asp:ImageButton ID="ImageButtonLoyalPoLoyalty" runat="server" ImageUrl="~/Images/icon_search.png"
                                                                    OnClick="ImageButtonLoyalPoLoyalty_Click" OnClientClick="LinkClick()" />
                                                                <asp:LinkButton ID="LinkButtonTemp" runat="server" OnClick="LinkButtonTemp_Click"></asp:LinkButton>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div style="float: left; width: 100%; padding-top: 2px;">
                                                        <div style="float: left; width: 49%;">
                                                            <div style="float: left; width: 30%;">
                                                                Valid From
                                                            </div>
                                                            <div style="float: left; width: 70%;">
                                                                <asp:TextBox ID="TextBoxLoyaltyPoValidFrom" CssClass="TextBox" runat="server"></asp:TextBox>
                                                                <asp:Image ID="Image3" runat="server" ImageUrl="~/Images/Calendar_scheduleHS.png" />
                                                                <asp:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="TextBoxLoyaltyPoValidFrom"
                                                                    PopupButtonID="Image3" Enabled="True" Format="dd/MMM/yyyy">
                                                                </asp:CalendarExtender>
                                                            </div>
                                                        </div>
                                                        <div style="float: left; width: 1%; color: Black;">
                                                            &nbsp;
                                                        </div>
                                                        <div style="float: left; width: 49%;">
                                                            <div style="float: left; width: 40%;">
                                                                Valid To
                                                            </div>
                                                            <div style="float: left; width: 60%;">
                                                                <asp:TextBox ID="TextBoxLoyaltyPovalidTo" CssClass="TextBox"  runat="server"></asp:TextBox>
                                                                <asp:Image ID="Image4" runat="server" ImageUrl="~/Images/Calendar_scheduleHS.png" />
                                                                <asp:CalendarExtender ID="CalendarExtender3" runat="server" TargetControlID="TextBoxLoyaltyPovalidTo"
                                                                    PopupButtonID="Image4" Enabled="True" Format="dd/MMM/yyyy">
                                                                </asp:CalendarExtender>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div style="float: left; width: 100%; padding-top: 3px;">
                                                        <div style="float: left; width: 49%;">
                                                            <div style="float: left; width: 30%;">
                                                                Value From
                                                            </div>
                                                            <div style="float: left; width: 70%;">
                                                                <asp:TextBox ID="TextBoxLoyaltyPoValueFrom" runat="server" CssClass="TextBox TextBoxUpper"
                                                                    onKeyPress="return numbersonly(event,true)"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                        <div style="float: left; width: 1%; color: Black;">
                                                            &nbsp;
                                                        </div>
                                                        <div style="float: left; width: 49%;">
                                                            <div style="float: left; width: 40%;">
                                                                Value To
                                                            </div>
                                                            <div style="float: left; width: 60%;">
                                                                <asp:TextBox ID="TextBoxLoyaltyPoValueTo" runat="server" CssClass="TextBox TextBoxUpper"
                                                                    onKeyPress="return numbersonly(event,true)"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div style="float: left; width: 100%; padding-top: 3px;">
                                                        <div style="float: left; width: 49%;">
                                                            <div style="float: left; width: 30%;">
                                                                Qty From
                                                            </div>
                                                            <div style="float: left; width: 70%;">
                                                                <asp:TextBox ID="TextBoxLoyaltyPoQtyFrom" runat="server" CssClass="TextBox TextBoxUpper"
                                                                    onKeyPress="return numbersonly(event,false)"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                        <div style="float: left; width: 1%; color: Black;">
                                                            &nbsp;
                                                        </div>
                                                        <div style="float: left; width: 49%;">
                                                            <div style="float: left; width: 40%;">
                                                                Qty To
                                                            </div>
                                                            <div style="float: left; width: 60%;">
                                                                <asp:TextBox ID="TextBoxLoyaltyPoQtyTo" runat="server" CssClass="TextBox TextBoxUpper"
                                                                    onKeyPress="return numbersonly(event,false)"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div style="float: left; width: 100%; padding-top: 3px;">
                                                        <div style="float: left; width: 49%;">
                                                            <div style="float: left; width: 30%;">
                                                                Pay Mode
                                                            </div>
                                                            <div style="float: left; width: 70%;">
                                                                <asp:DropDownList ID="DropDownListLoyaltyPoPayMode" runat="server" CssClass="ComboBox"
                                                                    Width="132px" AppendDataBoundItems="true">
                                                                </asp:DropDownList>
                                                            </div>
                                                        </div>
                                                        <div style="float: left; width: 1%; color: Black;">
                                                            &nbsp;
                                                        </div>
                                                        <div style="float: left; width: 49%;">
                                                            <div style="float: left; width: 40%;">
                                                                Bank
                                                            </div>
                                                            <div style="float: left; width: 60%;">
                                                                <asp:DropDownList ID="DropDownListLoyaltyPoBank" runat="server" CssClass="ComboBox"
                                                                    Width="132px" AutoPostBack="True" OnSelectedIndexChanged="DropDownListLoyaltyPoBank_SelectedIndexChanged">
                                                                </asp:DropDownList>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div style="float: left; width: 100%; padding-top: 3px;">
                                                        <div style="float: left; width: 49%;">
                                                            <div style="float: left; width: 30%;">
                                                                Card Type
                                                            </div>
                                                            <div style="float: left; width: 70%;">
                                                                <asp:DropDownList ID="DropDownListCardType" runat="server" CssClass="ComboBox" AppendDataBoundItems="True" 
                                                                    Width="132px">
                                                                </asp:DropDownList>
                                                            </div>
                                                        </div>
                                                        <div style="float: left; width: 1%; color: Black;">
                                                            &nbsp;
                                                        </div>
                                                        <div style="float: left; width: 49%;">
                                                            <div style="float: left; width: 40%;">
                                                                Points
                                                            </div>
                                                            <div style="float: left; width: 60%;">
                                                                <asp:TextBox ID="TextBoxLoyaltyPoints" runat="server" CssClass="TextBox TextBoxUpper"
                                                                    onKeyPress="return numbersonly(event,false)"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div style="float: left; width: 100%; padding-top: 3px;">
                                                        <div style="float: left; width: 49%;">
                                                            <div style="float: left; width: 30%;">
                                                                Customer Spec.
                                                            </div>
                                                            <div style="float: left; width: 70%;">
                                                                <asp:DropDownList ID="DropDownList1" runat="server" CssClass="ComboBox">
                                                                </asp:DropDownList>
                                                            </div>
                                                        </div>
                                                        <div style="float: left; width: 1%; color: Black;">
                                                            &nbsp;
                                                        </div>
                                                        <div style="float: left; width: 49%;">
                                                            <div style="float: left; width: 40%;">
                                                                Value Div.
                                                            </div>
                                                            <div style="float: left; width: 60%;">
                                                                <asp:TextBox ID="TextBoxLoyaltyPoValDiv" runat="server" CssClass="TextBox TextBoxUpper"
                                                                    onKeyPress="return numbersonly(event,false)"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div style="float: left; width: 100%; padding-top: 3px;">
                                                        <div style="float: left; width: 49%;">
                                                            <div style="float: left; width: 30%;">
                                                                Is Multiply
                                                            </div>
                                                            <div style="float: left; width: 70%;">
                                                                <asp:CheckBox ID="CheckBoxLoyaltyPointsIsMul" runat="server" />
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </asp:Panel>
                                        </div>
                                        <div style="float: left; width: 1%; color: Black;">
                                            &nbsp;
                                        </div>
                                        <div style="float: left; width: 34%; color: Black;">
                                            <asp:Panel ID="Panel5" runat="server" GroupingText="Price Book Details" CssClass="Panel">
                                                <div style="float: left; width: 100%; color: Black;">
                                                    <div style="float: left; height: 5%; width: 100%; color: Black;">
                                                        &nbsp;
                                                    </div>
                                                    <div style="float: left; width: 100%;">
                                                        <div style="float: left; width: 40%;">
                                                            Price Book
                                                        </div>
                                                        <div style="float: left; width: 60%;">
                                                            <asp:DropDownList ID="DropDownListLoyaltyPoPriceBook" runat="server" AppendDataBoundItems="True"
                                                                CssClass="ComboBox" AutoPostBack="True" Width="100px" OnSelectedIndexChanged="DropDownListLoyaltyPoPriceBook_SelectedIndexChanged">
                                                            </asp:DropDownList>
                                                            <asp:CheckBox ID="CheckBoxLoyalPoPBAll" runat="server" OnCheckedChanged="CheckBoxLoyalPoPBAll_CheckedChanged"
                                                                AutoPostBack="True" />
                                                        </div>
                                                    </div>
                                                    <div style="float: left; width: 100%; padding-top: 3px;">
                                                        <asp:ListBox ID="ListBoxLoyalPoPBList" runat="server" Width="90%" Height="75px" SelectionMode="Multiple">
                                                        </asp:ListBox>
                                                    </div>
                                                    <div style="float: left; width: 100%; padding-top: 3px;">
                                                        <asp:LinkButton ID="LinkButtonLoyalPoPBClear" runat="server" Text="Clear" OnClick="LinkButtonLoyalPoPBClear_Click"></asp:LinkButton>
                                                    </div>
                                                </div>
                                            </asp:Panel>
                                        </div>
                                        <div style="float: left; width: 100%;">
                                            <asp:Panel ID="Panel4" runat="server" GroupingText="Business Hirc Details" CssClass="Panel">
                                                <div style="float: left; width: 65%;">
                                                    <uc1:uc_ProfitCenterSearch ID="uc_ProfitCenterSearch1" runat="server" />
                                                </div>
                                                <div style="float: left; width: 10%; text-align: center;">
                                                    <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Images/right_arrow_icon.png"
                                                        Width="30%" ToolTip="Add to Profit Center List" OnClick="ImageButton1_Click" />
                                                </div>
                                                <div style="float: left; width: 15%; text-align: right;">
                                                    <asp:Panel ID="Panel15" runat="server" Height="120px" ScrollBars="Vertical" BorderColor="Blue"
                                                        BorderWidth="1px" GroupingText="Profit Centers">
                                                        <asp:GridView ID="GridViewPC1" runat="server" AutoGenerateColumns="False" OnRowDataBound="GridViewPC_RowDataBound">
                                                            <Columns>
                                                                <asp:TemplateField>
                                                                    <EditItemTemplate>
                                                                        <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                                                                    </EditItemTemplate>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="Label1" runat="server"></asp:Label>
                                                                        <asp:CheckBox ID="chekPc" runat="server" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:BoundField DataField="PROFIT_CENTER" ShowHeader="False" />
                                                            </Columns>
                                                        </asp:GridView>
                                                    </asp:Panel>
                                                    <div style="float: left; width: 100%; text-align: right;">
                                                        <div style="float: left; width: 30%; text-align: right;">
                                                            <asp:Button ID="ButtonAdd1" runat="server" Text="All" CssClass="Button" Width="100%"
                                                                OnClick="ButtonAdd1_Click" />
                                                        </div>
                                                        <div style="float: left; width: 30%; text-align: right;">
                                                            <asp:Button ID="ButtonNone1" runat="server" Text="None" CssClass="Button" OnClick="ButtonNone1_Click" />
                                                        </div>
                                                        <div style="float: left; width: 30%; text-align: right;">
                                                            <asp:Button ID="ButtonPcClear1" runat="server" Text="Clear" CssClass="Button" OnClick="ButtonPcClear1_Click" />
                                                        </div>
                                                    </div>
                                                </div>
                                            </asp:Panel>
                                        </div>
                                        <div style="float: left; width: 100%;">
                                            <asp:Panel ID="Panel16" runat="server" GroupingText="Item Details" CssClass="Panel">
                                                <uc2:uc_ItemDetailSearch ID="uc_ItemDetailSearch2" runat="server" />
                                            </asp:Panel>
                                        </div>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </asp:Panel>
                    </ContentTemplate>
                </asp:TabPanel>
                <asp:TabPanel ID="TabPanel2" HeaderText="Discount" runat="server">
                    <ContentTemplate>
                        <asp:Panel ID="Panel25" runat="server" ScrollBars="Auto" Height="505px">
                            <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                <ContentTemplate>
                                    <div style="float: left; height: 10%;">
                                        &nbsp;
                                    </div>
                                    <div style="float: left; width: 100%; color: Black; font-family: Verdana; font-size: 11px;">
                                        <div style="float: left; width: 65%; color: Black;">
                                            <asp:Panel ID="Panel6" runat="server" GroupingText="Discount Details" CssClass="Panel">
                                                <div style="float: left; width: 100%; color: Black;">
                                                    <div style="float: left; height: 5%; width: 100%; color: Black;">
                                                        &nbsp;
                                                    </div>
                                                    <div style="float: left; width: 100%;">
                                                        <div style="float: left; width: 49%;">
                                                            <div style="float: left; width: 30%;">
                                                                Loyalty Type
                                                            </div>
                                                            <div style="float: left; width: 70%;">
                                                                <asp:TextBox ID="TextBoxDiscountLoyalType" CssClass="TextBox" runat="server"></asp:TextBox>
                                                                <asp:ImageButton ID="ImageButton4" runat="server" ImageUrl="~/Images/icon_search.png"
                                                                    OnClick="ImageButton4_Click" />
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div style="float: left; width: 100%; padding-top: 2px;">
                                                        <div style="float: left; width: 49%;">
                                                            <div style="float: left; width: 30%;">
                                                                Valid From
                                                            </div>
                                                            <div style="float: left; width: 70%;">
                                                                <asp:TextBox ID="TextBoxDiscountValidFrom" CssClass="TextBox"  runat="server"></asp:TextBox>
                                                                <asp:Image ID="Image5" runat="server" ImageUrl="~/Images/Calendar_scheduleHS.png" />
                                                                <asp:CalendarExtender ID="CalendarExtender4" runat="server" TargetControlID="TextBoxDiscountValidFrom"
                                                                    PopupButtonID="Image5" Enabled="True" Format="dd/MMM/yyyy">
                                                                </asp:CalendarExtender>
                                                            </div>
                                                        </div>
                                                        <div style="float: left; width: 1%; color: Black;">
                                                            &nbsp;
                                                        </div>
                                                        <div style="float: left; width: 49%;">
                                                            <div style="float: left; width: 40%;">
                                                                Valid To
                                                            </div>
                                                            <div style="float: left; width: 60%;">
                                                                <asp:TextBox ID="TextBoxDiscountValidTo" CssClass="TextBox"  runat="server"></asp:TextBox>
                                                                <asp:Image ID="Image6" runat="server" ImageUrl="~/Images/Calendar_scheduleHS.png" />
                                                                <asp:CalendarExtender ID="CalendarExtender6" runat="server" TargetControlID="TextBoxDiscountValidTo"
                                                                    PopupButtonID="Image6" Enabled="True" Format="dd/MMM/yyyy">
                                                                </asp:CalendarExtender>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div style="float: left; width: 100%; padding-top: 3px;">
                                                        <div style="float: left; width: 49%;">
                                                            <div style="float: left; width: 30%;">
                                                                Points From
                                                            </div>
                                                            <div style="float: left; width: 70%;">
                                                                <asp:TextBox ID="TextBoxDiscountPointFrom" runat="server" CssClass="TextBox TextBoxUpper"
                                                                    onKeyPress="return numbersonly(event,false)"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                        <div style="float: left; width: 1%; color: Black;">
                                                            &nbsp;
                                                        </div>
                                                        <div style="float: left; width: 49%;">
                                                            <div style="float: left; width: 40%;">
                                                                Points To
                                                            </div>
                                                            <div style="float: left; width: 60%;">
                                                                <asp:TextBox ID="TextBoxDiscountPointTo" runat="server" CssClass="TextBox TextBoxUpper"
                                                                    onKeyPress="return numbersonly(event,false)"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div style="float: left; width: 100%; padding-top: 3px;">
                                                        <div style="float: left; width: 49%;">
                                                            <div style="float: left; width: 30%;">
                                                                Discount Rate
                                                            </div>
                                                            <div style="float: left; width: 70%;">
                                                                <asp:TextBox ID="TextBoxDiscountRate" runat="server" CssClass="TextBox TextBoxUpper"
                                                                    onKeyPress="return numbersonly(event,false)" MaxLength="2"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </asp:Panel>
                                        </div>
                                        <div style="float: left; width: 1%; color: Black;">
                                            &nbsp;
                                        </div>
                                        <div style="float: left; width: 34%; color: Black;">
                                            <asp:Panel ID="Panel10" runat="server" GroupingText="Price Book Details" CssClass="Panel">
                                                <div style="float: left; width: 100%; color: Black;">
                                                    <div style="float: left; height: 5%; width: 100%; color: Black;">
                                                        &nbsp;
                                                    </div>
                                                    <div style="float: left; width: 100%;">
                                                        <div style="float: left; width: 40%;">
                                                            Price Book
                                                        </div>
                                                        <div style="float: left; width: 60%;">
                                                            <asp:DropDownList ID="DropDownListDiscountPB" runat="server" AppendDataBoundItems="True"
                                                                CssClass="ComboBox" AutoPostBack="True" Width="100px" OnSelectedIndexChanged="DropDownListDiscountPB_SelectedIndexChanged">
                                                            </asp:DropDownList>
                                                            <asp:CheckBox ID="CheckBoxDiscoutPB" runat="server" AutoPostBack="True" OnCheckedChanged="CheckBoxDiscoutPB_CheckedChanged" />
                                                        </div>
                                                    </div>
                                                    <div style="float: left; width: 100%; padding-top: 3px;">
                                                        <asp:ListBox ID="ListBoxDiscountPBList" runat="server" Width="90%" Height="65px"
                                                            SelectionMode="Multiple"></asp:ListBox>
                                                    </div>
                                                    <div style="float: left; width: 100%; padding-top: 3px;">
                                                        <asp:LinkButton ID="LinkButtonDiscoutPBClear" runat="server" Text="Clear" OnClick="LinkButtonDiscoutPBClear_Click"></asp:LinkButton>
                                                    </div>
                                                </div>
                                            </asp:Panel>
                                        </div>
                                        <div style="float: left; width: 100%;">
                                            <asp:Panel ID="Panel9" runat="server" GroupingText="Business Hierarchy Details" CssClass="Panel">
                                                <div style="float: left; width: 65%;">
                                                    <uc1:uc_ProfitCenterSearch ID="uc_ProfitCenterSearch2" runat="server" />
                                                </div>
                                                <div style="float: left; width: 10%; text-align: center;">
                                                    <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/Images/right_arrow_icon.png"
                                                        Width="30%" ToolTip="Add to Profit Center List" OnClick="ImageButton2_Click" />
                                                </div>
                                                <div style="float: left; width: 15%; text-align: right;">
                                                    <asp:Panel ID="Panel17" runat="server" Height="120px" ScrollBars="Vertical" BorderColor="Blue"
                                                        BorderWidth="1px" GroupingText="Profit Centers">
                                                        <asp:GridView ID="GridViewPC2" runat="server" AutoGenerateColumns="False">
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
                                                            <asp:Button ID="ButtonAdd2" runat="server" Text="All" CssClass="Button" Width="100%"
                                                                OnClick="ButtonAdd2_Click" />
                                                        </div>
                                                        <div style="float: left; width: 30%; text-align: right;">
                                                            <asp:Button ID="ButtonNone2" runat="server" Text="None" CssClass="Button" OnClick="ButtonNone2_Click" />
                                                        </div>
                                                        <div style="float: left; width: 30%; text-align: right;">
                                                            <asp:Button ID="ButtonPcClear2" runat="server" Text="Clear" CssClass="Button" OnClick="ButtonPcClear2_Click" />
                                                        </div>
                                                    </div>
                                                </div>
                                            </asp:Panel>
                                        </div>
                                        <div style="float: left; width: 100%;">
                                            <asp:Panel ID="Panel18" runat="server" GroupingText="Item Details" CssClass="Panel">
                                                <uc2:uc_ItemDetailSearch ID="uc_ItemDetailSearch3" runat="server" />
                                            </asp:Panel>
                                        </div>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </asp:Panel>
                    </ContentTemplate>
                </asp:TabPanel>
                <asp:TabPanel ID="TabPanel3" HeaderText="Loyalty Redeem" runat="server">
                    <ContentTemplate>
                        <asp:Panel ID="Panel26" runat="server" ScrollBars="Auto" Height="505px">
                            <asp:UpdatePanel ID="UpdatePanelLoyRed" runat="server">
                                <ContentTemplate>
                                    <div style="float: left; width: 100%; height: 10%;">
                                        &nbsp;
                                    </div>
                                    <div style="float: left; width: 100%; color: Black; font-family: Verdana; font-size: 11px;">
                                        <div style="float: left; width: 65%; color: Black;">
                                            <asp:Panel ID="Panel7" runat="server" GroupingText="Discount Details" CssClass="Panel">
                                                <div style="float: left; width: 100%; color: Black;">
                                                    <div style="float: left; height: 5%; width: 100%; color: Black;">
                                                        &nbsp;
                                                    </div>
                                                    <div style="float: left; width: 100%;">
                                                        <div style="float: left; width: 49%;">
                                                            <div style="float: left; width: 30%;">
                                                                Loyalty Type
                                                            </div>
                                                            <div style="float: left; width: 70%;">
                                                                <asp:TextBox ID="TextBoxRedeemLoyaltyType" CssClass="TextBox" runat="server"></asp:TextBox>
                                                                <asp:ImageButton ID="ImageButton5" runat="server" ImageUrl="~/Images/icon_search.png"
                                                                    OnClick="ImageButton5_Click" />
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div style="float: left; width: 100%; padding-top: 2px;">
                                                        <div style="float: left; width: 49%;">
                                                            <div style="float: left; width: 30%;">
                                                                Valid From
                                                            </div>
                                                            <div style="float: left; width: 70%;">
                                                                <asp:TextBox ID="TextBoxRedeemValidFrom" CssClass="TextBox"  runat="server"></asp:TextBox>
                                                                <asp:Image ID="Image7" runat="server" ImageUrl="~/Images/Calendar_scheduleHS.png" />
                                                                <asp:CalendarExtender ID="CalendarExtender7" runat="server" TargetControlID="TextBoxRedeemValidFrom"
                                                                    PopupButtonID="Image7" Enabled="True" Format="dd/MMM/yyyy">
                                                                </asp:CalendarExtender>
                                                            </div>
                                                        </div>
                                                        <div style="float: left; width: 1%; color: Black;">
                                                            &nbsp;
                                                        </div>
                                                        <div style="float: left; width: 49%;">
                                                            <div style="float: left; width: 40%;">
                                                                Valid To
                                                            </div>
                                                            <div style="float: left; width: 60%;">
                                                                <asp:TextBox ID="TextBoxRedeemValidTo" CssClass="TextBox"  runat="server"></asp:TextBox>
                                                                <asp:Image ID="Image8" runat="server" ImageUrl="~/Images/Calendar_scheduleHS.png" />
                                                                <asp:CalendarExtender ID="CalendarExtender18" runat="server" TargetControlID="TextBoxRedeemValidTo"
                                                                    PopupButtonID="Image8" Enabled="True" Format="dd/MMM/yyyy">
                                                                </asp:CalendarExtender>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div style="float: left; width: 100%; padding-top: 3px;">
                                                        <div style="float: left; width: 49%;">
                                                            <div style="float: left; width: 30%;">
                                                                Redeem Point
                                                            </div>
                                                            <div style="float: left; width: 70%;">
                                                                <asp:TextBox ID="TextBoxRedeemPoint" runat="server" CssClass="TextBox TextBoxUpper"
                                                                    onKeyPress="return numbersonly(event,false)"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                        <div style="float: left; width: 1%; color: Black;">
                                                            &nbsp;
                                                        </div>
                                                        <div style="float: left; width: 49%;">
                                                            <div style="float: left; width: 40%;">
                                                                Points Value
                                                            </div>
                                                            <div style="float: left; width: 60%;">
                                                                <asp:TextBox ID="TextBoxRedeemPointValue" runat="server" CssClass="TextBox TextBoxUpper"
                                                                    onKeyPress="return numbersonly(event,false)"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </asp:Panel>
                                        </div>
                                        <div style="float: left; width: 1%; color: Black;">
                                            &nbsp;
                                        </div>
                                        <div style="float: left; width: 34%; color: Black;">
                                            <asp:Panel ID="Panel11" runat="server" GroupingText="Price Book Details" CssClass="Panel">
                                                <div style="float: left; width: 100%; color: Black;">
                                                    <div style="float: left; height: 5%; width: 100%; color: Black;">
                                                        &nbsp;
                                                    </div>
                                                    <div style="float: left; width: 100%;">
                                                        <div style="float: left; width: 40%;">
                                                            Price Book
                                                        </div>
                                                        <div style="float: left; width: 60%;">
                                                            <asp:DropDownList ID="DropDownListRedeemPB" runat="server" AppendDataBoundItems="True"
                                                                CssClass="ComboBox" AutoPostBack="True" Width="100px" OnSelectedIndexChanged="DropDownListRedeemPB_SelectedIndexChanged">
                                                            </asp:DropDownList>
                                                            <asp:CheckBox ID="CheckBoxRedeemPBAll" runat="server" AutoPostBack="True" OnCheckedChanged="CheckBoxRedeemPBAll_CheckedChanged" />
                                                        </div>
                                                    </div>
                                                    <div style="float: left; width: 100%; padding-top: 3px;">
                                                        <asp:ListBox ID="ListBoxRedeemPBList" runat="server" Width="90%" Height="65px" SelectionMode="Multiple">
                                                        </asp:ListBox>
                                                    </div>
                                                    <div style="float: left; width: 100%; padding-top: 3px;">
                                                        <asp:LinkButton ID="LinkButtonRedeemPBClear" runat="server" Text="Clear" OnClick="LinkButtonRedeemPBClear_Click"></asp:LinkButton>
                                                    </div>
                                                </div>
                                            </asp:Panel>
                                        </div>
                                        <div style="float: left; width: 100%;">
                                            <asp:Panel ID="Panel8" runat="server" GroupingText="Business Hierarchy Details" CssClass="Panel">
                                                <div style="float: left; width: 65%;">
                                                    <uc1:uc_ProfitCenterSearch ID="uc_ProfitCenterSearch3" runat="server" />
                                                </div>
                                                <div style="float: left; width: 10%; text-align: center;">
                                                    <asp:ImageButton ID="ImageButton3" runat="server" ImageUrl="~/Images/right_arrow_icon.png"
                                                        Width="30%" ToolTip="Add to Profit Center List" OnClick="ImageButton3_Click" />
                                                </div>
                                                <div style="float: left; width: 15%; text-align: right;">
                                                    <asp:Panel ID="Panel19" runat="server" Height="120px" ScrollBars="Vertical" BorderColor="Blue"
                                                        BorderWidth="1px" GroupingText="Profit Centers">
                                                        <asp:GridView ID="GridViewPC3" runat="server" AutoGenerateColumns="False" OnRowDataBound="GridViewPC_RowDataBound">
                                                            <Columns>
                                                                <asp:TemplateField>
                                                                    <EditItemTemplate>
                                                                        <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                                                                    </EditItemTemplate>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="Label1" runat="server"></asp:Label>
                                                                        <asp:CheckBox ID="chekPc" runat="server" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:BoundField DataField="PROFIT_CENTER" ShowHeader="False" />
                                                            </Columns>
                                                        </asp:GridView>
                                                    </asp:Panel>
                                                    <div style="float: left; width: 100%; text-align: right;">
                                                        <div style="float: left; width: 30%; text-align: right;">
                                                            <asp:Button ID="ButtonAdd3" runat="server" Text="All" CssClass="Button" Width="100%"
                                                                OnClick="ButtonAdd3_Click" />
                                                        </div>
                                                        <div style="float: left; width: 30%; text-align: right;">
                                                            <asp:Button ID="ButtonNone3" runat="server" Text="None" CssClass="Button" OnClick="ButtonNone3_Click" />
                                                        </div>
                                                        <div style="float: left; width: 30%; text-align: right;">
                                                            <asp:Button ID="ButtonPcClear3" runat="server" Text="Clear" CssClass="Button" OnClick="ButtonPcClear3_Click" />
                                                        </div>
                                                    </div>
                                                </div>
                                            </asp:Panel>
                                        </div>
                                        <div style="float: left; width: 100%;">
                                            <asp:Panel ID="Panel20" runat="server" GroupingText="Item Details" CssClass="Panel">
                                                <uc2:uc_ItemDetailSearch ID="uc_ItemDetailSearch4" runat="server" />
                                            </asp:Panel>
                                        </div>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </asp:Panel>
                    </ContentTemplate>
                </asp:TabPanel>
                <asp:TabPanel ID="TabPanel4" HeaderText="Customer Specification" runat="server">
                    <ContentTemplate>
                        <asp:Panel ID="Panel27" runat="server" ScrollBars="Auto" Height="505px">
                            <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                <ContentTemplate>
                                    <div style="float: left; width: 100%; color: Black; font-family: Verdana; font-size: 11px;">
                                        <asp:Panel ID="Panel22" runat="server" GroupingText=" " CssClass="Panel">
                                            <div style="float: left; width: 100%; color: Black;">
                                                <div style="float: left; width: 49%; color: Black;">
                                                    <div style="float: left; width: 30%; color: Black;">
                                                        Loyalty Type
                                                    </div>
                                                    <div style="float: left; width: 70%; color: Black;">
                                                        <asp:TextBox ID="TextBoxCSLoyaltyType" runat="server" CssClass="TextBox"></asp:TextBox>
                                                        <asp:ImageButton ID="ImageButton6" runat="server" ImageUrl="~/Images/icon_search.png"
                                                            OnClick="ImageButton6_Click" />
                                                    </div>
                                                </div>
                                            </div>
                                            <div style="float: left; width: 100%; color: Black; padding-top: 3px;">
                                                <div style="float: left; width: 49%; color: Black;">
                                                    <div style="float: left; width: 30%; color: Black;">
                                                        Points From
                                                    </div>
                                                    <div style="float: left; width: 70%; color: Black;">
                                                        <asp:TextBox ID="TextBoxCSPointsFrom" runat="server" onKeyPress="return numbersonly(event,false)"
                                                            CssClass="TextBox"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div style="float: left; width: 1%; color: Black;">
                                                    &nbsp;
                                                </div>
                                                <div style="float: left; width: 49%; color: Black;">
                                                    <div style="float: left; width: 30%; color: Black;">
                                                        Points To
                                                    </div>
                                                    <div style="float: left; width: 70%; color: Black;">
                                                        <asp:TextBox ID="TextBoxCSPointsTo" runat="server" onKeyPress="return numbersonly(event,false)"
                                                            CssClass="TextBox"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div style="float: left; width: 100%; color: Black; padding-top: 3px;">
                                                <div style="float: left; width: 49%; color: Black;">
                                                    <div style="float: left; width: 30%; color: Black;">
                                                        Specification
                                                    </div>
                                                    <div style="float: left; width: 70%; color: Black;">
                                                        <asp:DropDownList ID="DropDownListSpecification" runat="server" CssClass="ComboBox">
                                                        </asp:DropDownList>
                                                        <asp:Button ID="ButtonAdd" runat="server" Text="Add" CssClass="Button" />
                                                    </div>
                                                </div>
                                            </div>
                                            <asp:Panel ID="PanelPopUp" runat="server" CssClass="ModalWindow">
                                                <div class="popUpHeader" id="divpopHeader">
                                                    <div style="float: left; width: 80%">
                                                        Add Specification</div>
                                                    <div style="float: left; width: 20%; text-align: right">
                                                        <asp:ImageButton ID="imgbtnClose" runat="server" ImageUrl="~/Images/0032-01-16x16x16.ICO" />&nbsp;
                                                    </div>
                                                </div>
                                                <div style="float: left; width: 100%;">
                                                    <asp:Panel ID="Panel23" runat="server" ScrollBars="Auto">
                                                        <div style="float: left; width: 100%; color: Black; padding-top: 3px;">
                                                            <div style="float: left; width: 30%; color: Black;">
                                                                Specification
                                                            </div>
                                                            <div style="float: left; width: 70%; color: Black;">
                                                                <asp:TextBox ID="TextBoxSpec" runat="server" CssClass="TextBox">
                                                                </asp:TextBox>
                                                            </div>
                                                        </div>
                                                        <div style="float: left; width: 100%; color: Black;">
                                                            <asp:Button ID="ButtonAddSpec" runat="server" CssClass="Button" Text="Add" OnClick="ButtonAddSpec_Click" />
                                                        </div>
                                                    </asp:Panel>
                                                </div>
                                            </asp:Panel>
                                            <asp:ModalPopupExtender ID="ModalPopupExtender1" runat="server" TargetControlID="ButtonAdd"
                                                ClientIDMode="Static" PopupControlID="PanelPopUp" BackgroundCssClass="modalBackground"
                                                CancelControlID="imgbtnClose" PopupDragHandleControlID="divpopHeader">
                                            </asp:ModalPopupExtender>
                                        </asp:Panel>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </asp:Panel>
                    </ContentTemplate>
                </asp:TabPanel>
            </asp:TabContainer>
        </asp:Panel>
    </asp:Panel>
</asp:Content>
