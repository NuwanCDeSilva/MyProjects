<%@ Page Title="Cash Conversion" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="CashConversion.aspx.cs" Inherits="FF.WebERPClient.HP_Module.CashConversion" %>

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
            <div id="hdnFields">
                <asp:HiddenField ID="HiddenFieldSaveConfirm" runat="server" Value="0" />
            </div>
            <div style="float: left; width: 100%; color: Black;">
                <div style="text-align: right;" class="PanelHeader">
                    <asp:Button ID="ButtonSave" runat="server" Text="Save" CssClass="Button" OnClick="ButtonSave_Click"
                        OnClientClick="return confirm('Are you sure?');" />
                    <asp:Button ID="ButtonClear" runat="server" Text="Clear" CssClass="Button" OnClick="ButtonClear_Click" />
                    <asp:Button ID="ButtonClose" runat="server" Text="Close" CssClass="Button" OnClick="ButtonClose_Click" />
                </div>
                <div style="height: 10px; float: left; width: 100%; color: Black;">
                    &nbsp;
                </div>
                <%--1 part--%>
                <asp:Panel ID="Panel6" runat="server" GroupingText=" ">
                    <div style="float: left; width: 100%; color: Black;">
                        <div style="float: left; width: 48%; color: Black;">
                            <div style="float: left; width: 100%; color: Black;">
                                <%-- <div style="float: left; width: 100%; color: Black;">--%>
                                <%--                                <div style="float: left; width: 1%; color: Black;">
                                    &nbsp;
                                </div>--%>
                                <div style="float: left; width: 100%; color: Black;">
                                    <div style="float: left; width: 25%;">
                                        Scheme Category
                                    </div>
                                    <div style="float: left; width: 75%;">
                                        <asp:DropDownList ID="DropDownListSchemeCategory" runat="server" AutoPostBack="true"
                                            OnSelectedIndexChanged="DropDownListSchemeCategory_SelectedIndexChanged" CssClass="ComboBox">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div style="float: left; width: 100%; color: Black;">
                                    <div style="float: left; width: 25%;">
                                        Scheme Type
                                    </div>
                                    <div style="float: left; width: 75%;">
                                        <asp:DropDownList ID="DropDownListSchemeType" runat="server" AutoPostBack="true"
                                            Width="300px" OnSelectedIndexChanged="DropDownListSchemeType_SelectedIndexChanged"
                                            CssClass="ComboBox">
                                        </asp:DropDownList>
                                        <asp:CheckBox ID="CheckBoxAll" runat="server" AutoPostBack="true" OnCheckedChanged="CheckBoxAll_CheckedChanged" />
                                    </div>
                                </div>
                                <%--    </div>--%>
                                <div style="float: left; width: 100%; height: 5px; color: Black;">
                                    &nbsp;
                                </div>
                                <div style="float: left; width: 100%; color: Black;">
                                    <asp:Panel ID="PanelSchemeList" runat="server" ScrollBars="Auto" Width="95%" Height="120px">
                                        <%--<asp:GridView ID="GridViewSchemes" runat="server" Width="100%" EmptyDataText="No data found"
                                            ShowHeaderWhenEmpty="True" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333"
                                            CssClass="GridView" GridLines="None">
                                            <AlternatingRowStyle BackColor="White" />
                                            <Columns>
                                                <asp:TemplateField HeaderStyle-Width="20px">
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="CheckBoxGridSelect" runat="server" Checked="true" />
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" Width="20px" Height="15px" />
                                                </asp:TemplateField>
                                                <asp:BoundField HeaderText="Sch. Code" DataField="HSD_CD" />
                                                <asp:BoundField HeaderText="Description" DataField="HSD_DESC" />
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
                                        </asp:GridView>--%>
                                        <asp:ListBox ID="ListBoxSchemes" runat="server" Width="95%" Height="100" SelectionMode="Multiple">
                                        </asp:ListBox>
                                        <div style="float: left; width: 100%; color: Black;">
                                            <asp:LinkButton ID="LinkButtonSheme" runat="server" Text="Clear" OnClick="LinkButtonSheme_Click"></asp:LinkButton>
                                        </div>
                                    </asp:Panel>
                                </div>
                            </div>
                        </div>
                        <div style="float: left; width: 1%; color: Black;">
                            &nbsp;
                        </div>
                        <div style="float: left; width: 48%; color: Black;">
                            <div style="float: left; width: 100%; color: Black;">
                                <%--                            <div style="float: left; width: 100%; color: Black;">
                                <div style="float: left; width: 1%; color: Black;">
                                    &nbsp;
                                </div>--%>
                                <div style="float: left; width: 100%; color: Black;">
                                    <div style="float: left; width: 25%;">
                                        Business Hirc.
                                    </div>
                                    <div style="float: left; width: 75%;">
                                        <asp:DropDownList ID="DropDownListPartyTypes" runat="server" AutoPostBack="true"
                                            OnSelectedIndexChanged="DropDownListPartyTypes_SelectedIndexChanged">
                                            <asp:ListItem Value="-1" Text=""></asp:ListItem>
                                            <asp:ListItem Value="COM">Company</asp:ListItem>
                                            <asp:ListItem Value="CHNL">Channel</asp:ListItem>
                                            <asp:ListItem Value="SCHNL">Sub Channel</asp:ListItem>
                                            <asp:ListItem Value="GPC">GPC</asp:ListItem>
                                            <asp:ListItem Value="AREA">Area</asp:ListItem>
                                            <asp:ListItem Value="REGION">Region</asp:ListItem>
                                            <asp:ListItem Value="ZONE">Zone</asp:ListItem>
                                            <asp:ListItem Value="PC">PC</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:CheckBox ID="CheckBoxPartyAll" runat="server" OnCheckedChanged="CheckBoxPartyAll_CheckedChanged"
                                            AutoPostBack="true" />
                                    </div>
                                </div>
                                <%--                                <div style="float: left; width: 1%; color: Black;">
                                    &nbsp;
                                </div>--%>
                                <%--                                <div style="float: left; width: 100%; color: Black;">
                                    <div style="float: left; width: 30%;">
                                        Party Code
                                    </div>
                                    <div style="float: left; width: 70%;">
                                         <asp:DropDownList ID="DropDownListPartyCodes" runat="server"></asp:DropDownList>
                                          <asp:CheckBox ID="CheckBoxPartyAll" runat="server"  AutoPostBack="true" 
                                            />
                                    </div>
                                </div>--%>
                                <%-- </div>--%>
                                <div style="float: left; width: 100%; height: 5px; color: Black;">
                                    &nbsp;
                                </div>
                                <div style="float: left; width: 100%; color: Black;">
                                    <asp:Panel ID="Panel1" runat="server" ScrollBars="Auto" Width="95%" Height="140">
                                        <%--    <asp:GridView ID="GridViewParty" runat="server" Width="100%" EmptyDataText="No data found"
                                            ShowHeaderWhenEmpty="True" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333"
                                            CssClass="GridView" GridLines="None">
                                            <AlternatingRowStyle BackColor="White" />
                                            <Columns>
                                                <asp:TemplateField HeaderStyle-Width="20px">
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="CheckBoxGridSelect" runat="server" Checked="true" />
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" Width="20px" Height="15px" />
                                                </asp:TemplateField>
                                                <asp:BoundField HeaderText="Code" DataField="Code" />
                                                <asp:BoundField HeaderText="Description" DataField="Description" />
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
                                        </asp:GridView>--%>
                                        <asp:ListBox ID="ListBoxParty" runat="server" Width="95%" Height="123" SelectionMode="Multiple">
                                        </asp:ListBox>
                                        <div style="float: left; width: 100%; color: Black;">
                                            <asp:LinkButton ID="LinkButton1" runat="server" Text="Clear" OnClick="LinkButton1_Click"></asp:LinkButton>
                                        </div>
                                    </asp:Panel>
                                </div>
                            </div>
                        </div>
                        <div style="float: left; width: 100%; height: 10px; color: Black;">
                            &nbsp;
                        </div>
                    </div>
                </asp:Panel>
                <%--2 part--%>
                <div style="float: left; width: 100%; color: Black;">
                    <%-- 1 part--%>
                    <div style="float: left; width: 60%; color: Black;">
                        <asp:Panel ID="Panel5" runat="server" GroupingText=" ">
                            <%-- account period--%>
                            <div style="float: left; width: 40%; color: Black;">
                                <asp:Panel ID="PanelAccount" runat="server" GroupingText="Account Period" CssClass="Panel">
                                    <div style="float: left; width: 100%; color: Black;">
                                        <div style="float: left; width: 100%; color: Black;">
                                            <div style="float: left; width: 15%;">
                                                From
                                            </div>
                                            <div style="float: left; width: 85%;">
                                                <asp:TextBox ID="TextBoxFromDate" CssClass="TextBoxUpper" runat="server" Enabled="false"></asp:TextBox>
                                                <asp:Image ID="Image1" runat="server" ImageUrl="~/Images/Calendar_scheduleHS.png" />
                                                <asp:CalendarExtender ID="CalendarExtender3" runat="server" TargetControlID="TextBoxFromDate"
                                                    PopupButtonID="Image1" Enabled="True" Format="dd/MMM/yyyy">
                                                </asp:CalendarExtender>
                                            </div>
                                        </div>
                                        <div style="float: left; width: 100%; color: Black;">
                                            <div style="float: left; width: 15%;">
                                                To
                                            </div>
                                            <div style="float: left; width: 85%;">
                                                <asp:TextBox ID="TextBoxToDate" CssClass="TextBoxUpper" runat="server" Enabled="false"></asp:TextBox>
                                                <asp:Image ID="Image2" runat="server" ImageUrl="~/Images/Calendar_scheduleHS.png" />
                                                <asp:CalendarExtender ID="CalendarExtender4" runat="server" TargetControlID="TextBoxToDate"
                                                    PopupButtonID="Image2" Enabled="True" Format="dd/MMM/yyyy">
                                                </asp:CalendarExtender>
                                            </div>
                                        </div>
                                    </div>
                                </asp:Panel>
                            </div>
                            <%--period--%>
                            <div style="float: left; width: 30%; color: Black;">
                                <asp:Panel ID="Panel2" runat="server" GroupingText="Period" CssClass="Panel">
                                    <div style="float: left; width: 100%; color: Black;">
                                        <div style="float: left; width: 100%; color: Black;">
                                            <div style="float: left; width: 20%;">
                                                From
                                            </div>
                                            <div style="float: left; width: 80%;">
                                                <asp:TextBox ID="TextBoxFromDy" CssClass="TextBoxUpper" runat="server" Width="120px"
                                                    onKeyPress="return numbersonly(event,false)"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div style="float: left; width: 100%; color: Black;">
                                            <div style="float: left; width: 20%;">
                                                To
                                            </div>
                                            <div style="float: left; width: 80%;">
                                                <asp:TextBox ID="TextBoxToDy" CssClass="TextBoxUpper" runat="server" Width="120px"
                                                    onKeyPress="return numbersonly(event,false)"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                </asp:Panel>
                            </div>
                            <%--value--%>
                            <div style="float: left; width: 30%; color: Black;">
                                <asp:Panel ID="Panel3" runat="server" GroupingText="Value" CssClass="Panel">
                                    <div style="float: left; width: 100%; color: Black;">
                                        <div style="float: left; width: 100%; color: Black;">
                                            <div style="float: left; width: 20%;">
                                                From
                                            </div>
                                            <div style="float: left; width: 80%;">
                                                <asp:TextBox ID="TextBoxFomAmo" CssClass="TextBoxUpper" runat="server" Width="120px"
                                                    onKeyPress="return numbersonly(event,true)" MaxLength="12"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div style="float: left; width: 100%; color: Black; padding-top: 2px;">
                                            <div style="float: left; width: 20%;">
                                                To
                                            </div>
                                            <div style="float: left; width: 80%;">
                                                <asp:TextBox ID="TextBoxToAmo" CssClass="TextBoxUpper" runat="server" Width="120px"
                                                    onKeyPress="return numbersonly(event,true)" MaxLength="12"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                </asp:Panel>
                            </div>
                            <%--add row--%>
                            <div style="float: left; width: 100%; color: Black; padding-top: 2px;">
                                <div style="float: left; width: 100%; color: Black;">
                                    <div style="float: left; width: 70%;">
                                        Check On
                                    </div>
                                    <div style="float: left; width: 30%;">
                                        <asp:DropDownList ID="DropDownListCheckOn" CssClass="ComboBox" runat="server">
                                            <asp:ListItem Value="UP">Unit Price</asp:ListItem>
                                            <asp:ListItem Value="AF">Amount Finance</asp:ListItem>
                                            <asp:ListItem Value="HP">Hire Value</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                            <%--2 row--%>
                            <div style="float: left; width: 100%; color: Black;">
                                <div style="float: left; width: 50%; color: Black;">
                                    <div style="float: left; width: 70%;">
                                        Valid UpTo
                                    </div>
                                    <div style="float: left; width: 30%;">
                                        <asp:TextBox ID="TextBoxValidUpTo" CssClass="TextBoxUpper" runat="server" Enabled="false"
                                            Width="60"></asp:TextBox>
                                        <asp:Image ID="Image3" runat="server" ImageUrl="~/Images/Calendar_scheduleHS.png" />
                                        <asp:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="TextBoxValidUpTo"
                                            PopupButtonID="Image3" Enabled="True" Format="dd/MMM/yyyy">
                                        </asp:CalendarExtender>
                                    </div>
                                </div>
                                <div style="float: left; width: 50%; color: Black;">
                                    <div style="float: left; width: 70%;">
                                        Insurance Refund Rate
                                    </div>
                                    <div style="float: left; width: 30%;">
                                        <asp:TextBox ID="TextBoxInsRefundRt" CssClass="TextBoxUpper" runat="server" Width="60px"
                                            onKeyPress="return numbersonly(event,false)" MaxLength="2"></asp:TextBox>
                                        %
                                    </div>
                                </div>
                            </div>
                            <%--add row--%>
                            <div style="float: left; width: 100%; color: Black; padding-top: 2px;">
                                <div style="float: left; width: 100%; color: Black;">
                                    <div style="float: left; width: 70%;">
                                        Calculate On
                                    </div>
                                    <div style="float: left; width: 30%;">
                                        <asp:DropDownList ID="DropDownListCalculateOnType" CssClass="ComboBox" runat="server">
                                            <asp:ListItem Value="UP">Unit Price</asp:ListItem>
                                            <asp:ListItem Value="AF">Amount Finance</asp:ListItem>
                                            <asp:ListItem Value="HP">Hire Value</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                            <%-- 3 row--%>
                            <asp:Panel ID="PanelRate" runat="server" GroupingText=" ">
                                <%--1 row--%>
                                <div style="float: left; width: 75%; color: Black;">
                                    <div style="float: left; width: 50%; color: Black;">
                                        <asp:RadioButton ID="RadioButtonRate" runat="server" Text="Rate" GroupName="G1" AutoPostBack="True"
                                            Checked="True" OnCheckedChanged="RadioButtonRate_CheckedChanged" />
                                    </div>
                                    <div style="float: left; width: 50%; color: Black;">
                                        <asp:RadioButton ID="RadioButtonAmount" runat="server" Text="Amount" GroupName="G1"
                                            AutoPostBack="True" OnCheckedChanged="RadioButtonAmount_CheckedChanged" />
                                    </div>
                                </div>
                                <%-- 2 row--%>
                                <div style="float: left; width: 100%; color: Black;">
                                    <div style="float: left; width: 70%;">
                                        Service Charge Rate
                                    </div>
                                    <div style="float: left; width: 30%;">
                                        <asp:TextBox ID="TextBoxServiceChgRt" CssClass="TextBoxUpper" runat="server" Width="60px"
                                            onKeyPress="return numbersonly(event,false)" MaxLength="2">0</asp:TextBox>
          
                                        %
                                    </div>
                                </div>
                                <%--3 row--%>
                                <div style="float: left; width: 100%; color: Black; padding-top: 1px;">
                                    <div style="float: left; width: 70%;">
                                        Service Charge Amount
                                    </div>
                                    <div style="float: left; width: 30%;">
                                        <asp:TextBox ID="TextBoxServiceChargeAmo" CssClass="TextBoxUpper" runat="server"
                                            Width="60px" Enabled="False" onKeyPress="return numbersonly(event,true)" 
                                            MaxLength="12" >0</asp:TextBox>
                                    </div>
                                </div>
                            </asp:Panel>
                            <%--4 row--%>
                            <asp:Panel ID="PanelAddRate" runat="server" GroupingText=" ">
                                <%--1 row--%>
                                <div style="float: left; width: 75%; color: Black;">
                                    <div style="float: left; width: 50%; color: Black;">
                                        <asp:RadioButton ID="RadioButtonAddrate" runat="server" Text="Rate" GroupName="G2"
                                            AutoPostBack="True" Checked="True" OnCheckedChanged="RadioButtonAddrate_CheckedChanged" />
                                    </div>
                                    <div style="float: left; width: 50%; color: Black;">
                                        <asp:RadioButton ID="RadioButtonAddAmo" runat="server" Text="Amount" GroupName="G2"
                                            AutoPostBack="True" OnCheckedChanged="RadioButtonAddAmo_CheckedChanged" />
                                    </div>
                                </div>
                                <%-- 2 row--%>
                                <div style="float: left; width: 100%; color: Black;">
                                    <div style="float: left; width: 70%;">
                                        Additional Service Charge Rate
                                    </div>
                                    <div style="float: left; width: 30%;">
                                        <asp:TextBox ID="TextBoxAddRate" CssClass="TextBoxUpper" runat="server" Width="60px"
                                            onKeyPress="return numbersonly(event,false)" MaxLength="2">0</asp:TextBox>
                                        %
                                    </div>
                                </div>
                                <%--3 row--%>
                                <div style="float: left; width: 100%; color: Black; padding-top: 1px;">
                                    <div style="float: left; width: 70%;">
                                        Additional Service Charge Amount
                                    </div>
                                    <div style="float: left; width: 30%;">
                                        <asp:TextBox ID="TextBoxAddAmo" CssClass="TextBoxUpper" runat="server" Width="60px"
                                            Enabled="False" onKeyPress="return numbersonly(event,true)" MaxLength="12">0</asp:TextBox>
                                    </div>
                                </div>
                            </asp:Panel>
                            <%--5 row--%>
                            <div style="float: left; width: 100%; color: Black; padding-top: 2px;">
                                <div style="float: left; width: 100%; color: Black;">
                                    <div style="float: left; width: 70%;">
                                        Additionl Chage Calc on
                                    </div>
                                    <div style="float: left; width: 30%;">
                                        <asp:DropDownList ID="DropDownListAddCharType" CssClass="ComboBox" runat="server">
                                            <asp:ListItem Value="UP">Unit Price</asp:ListItem>
                                            <asp:ListItem Value="AF">Amount Finance</asp:ListItem>
                                            <asp:ListItem Value="HP">Hire Value</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                            <%--6 row--%>
                            <div style="float: left; width: 100%; color: Black; padding-top: 2px;">
                                <div style="float: left; width: 100%; color: Black;">
                                    <div style="float: left; width: 70%;">
                                        Convertion upto
                                    </div>
                                    <div style="float: left; width: 30%;">
                                        <asp:TextBox ID="TextBoxConvertionupto" CssClass="TextBoxUpper" runat="server" Enabled="False"></asp:TextBox>
                                        <asp:Image ID="Image4" runat="server" ImageUrl="~/Images/Calendar_scheduleHS.png" />
                                        <asp:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="TextBoxConvertionupto"
                                            PopupButtonID="Image4" Enabled="True" Format="dd/MMM/yyyy">
                                        </asp:CalendarExtender>
                                    </div>
                                </div>
                            </div>
                        </asp:Panel>
                    </div>
                    <%--2 part--%>
                    <div style="float: left; width: 40%; color: Black;">
                        <asp:Panel ID="Panel4" runat="server" GroupingText=" ">
                            <div style="float: left; width: 100%; color: Black;">
                                <%-- 1 row--%>
                                <%--<div style="float: left; width: 100%; color: Black; padding-top: 1px;">
                                    <div style="float: left; width: 50%;">
                                        Additional Service Charge
                                    </div>
                                    <div style="float: left; width: 50%;">
                                        <asp:TextBox ID="TextBoxAdditionalSerCharge" CssClass="TextBoxUpper" runat="server"></asp:TextBox>
                                    </div>
                                </div>--%>
                                <%--2 row--%>
                                <div style="float: left; width: 100%; color: Black; padding-top: 1px;">
                                    <div style="float: left; width: 50%;">
                                        Price Book
                                    </div>
                                    <div style="float: left; width: 50%;">
                                        <asp:DropDownList ID="DropDownListPriceBook" runat="server" AutoPostBack="True" CssClass="ComboBox"
                                            OnSelectedIndexChanged="DropDownListPriceBook_SelectedIndexChanged" Width="130px">
                                        </asp:DropDownList>
                                        <asp:CheckBox ID="CheckBoxPriceBookAll" runat="server" AutoPostBack="True" OnCheckedChanged="CheckBoxPriceBookAll_CheckedChanged" />
                                    </div>
                                </div>
                                <%--3 row--%>
                                <%--<div style="float: left; width: 100%; color: Black; padding-top: 1px;">
                                    <div style="float: left; width: 60%;">
                                        Price Book Level
                                    </div>
                                    <div style="float: left; width: 40%;">
                                        <asp:DropDownList ID="DropDownListPBLevel" runat="server" Width="150px" AutoPostBack="true">
                                        </asp:DropDownList>
                                    </div>
                                </div>--%>
                                <%--4 row--%>
                                <div style="float: left; width: 100%; color: Black; padding-top: 1px;">
                                    <asp:ListBox ID="ListBoxPriceLevel" runat="server" Width="98%" Height="235px" SelectionMode="Multiple">
                                    </asp:ListBox>
                                     <div style="float: left; width: 100%; color: Black;">
                                            <asp:LinkButton ID="LinkButtonClearPb" runat="server" Text="Clear" 
                                                onclick="LinkButtonClearPb_Click"></asp:LinkButton>
                                        </div>
                                </div>
                                <div style="float: left; width: 100%; color: Black; padding-top: 3px;">
                                    <div style="float: left; width: 50%;">
                                        Price Book Convertable
                                    </div>
                                    <div style="float: left; width: 50%;">
                                        <asp:DropDownList ID="DropDownListPBConvertable" runat="server" CssClass="ComboBox"
                                            Width="150px">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                        </asp:Panel>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
