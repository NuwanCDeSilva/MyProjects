<%@ Page Title="HP Insurance Definition" Language="C#" MasterPageFile="~/Site.Master"
    AutoEventWireup="true" CodeBehind="DiriyaDefinition.aspx.cs" Inherits="FF.WebERPClient.General_Modules.DiriyaDefinition" %>

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

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="updtMainPnl" runat="server">
        <ContentTemplate>
            <div class="inv100break">
                <div style="height: 22px; text-align: right;" class="PanelHeader">
                    <asp:Button ID="ButtonSave" runat="server" Text="Save" CssClass="Button" OnClick="ButtonSave_Click" />
                    <asp:Button ID="ButtonClear" runat="server" Text="Clear" CssClass="Button" OnClick="ButtonClear_Click" />
                    <asp:Button ID="ButtonClose" runat="server" Text="Close" CssClass="Button" OnClick="ButtonClose_Click" />
                </div>
                <div style="float: left; height: 1%; color: Black;">
                    &nbsp;
                </div>
                <asp:TabContainer ID="TabContainerMain" runat="server" ActiveTabIndex="0">
                    <asp:TabPanel ID="MainDetails" runat="server" TabIndex="0" HeaderText="Main Details">
                        <ContentTemplate>
                            <div style="float: left; width: 100%; color: Black; font-family: Verdana; font-size: 11px;">
                                <div class="inv100break">
                                    <div style="float: left; width: 60%;font-size:9px;">
                                        <div class="inv100break">
                                            <asp:Panel ID="Panel12" runat="server" GroupingText="Business Hirc Details" CssClass="Panel">
                                                <div style="float: left; width: 60%;font-size:9px;">
                                                    <uc1:uc_ProfitCenterSearch ID="uc_ProfitCenterSearchLoyaltyType" runat="server" />
                                                </div>
                                                <div style="float: left; width: 10%; text-align: center;">
                                                    <asp:ImageButton ID="ImageButtonAddPC" runat="server" ImageUrl="~/Images/right_arrow_icon.png"
                                                        Width="50%" ToolTip="Add to Profit Center List" OnClick="ImageButtonAddPC_Click" />
                                                </div>
                                                <div style="float: left; width: 30%; text-align: right;">
                                                    <asp:Panel ID="Panel13" runat="server" Height="220px" ScrollBars="Vertical" BorderColor="Blue"
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
                                    </div>
                                    <div style="float: left; width: 40%;">
                                        <asp:Panel ID="Panel1" runat="server" GroupingText="Scheme Details" CssClass="Panel">
                                            <div class="inv100break">
                                                <div class="invunkwn41">
                                                    <asp:RadioButton ID="RadioButtonSchemeCategory" runat="server" Text="Scheme Category"
                                                        GroupName="G1" AutoPostBack="True" Checked="True" OnCheckedChanged="RadioButtonSchemeCategory_CheckedChanged" />
                                                </div>
                                                <div class="invunkwn41">
                                                    <asp:RadioButton ID="RadioButtonSchemeType" runat="server" Text="Scheme Type" GroupName="G1"
                                                        AutoPostBack="True" OnCheckedChanged="RadioButtonSchemeType_CheckedChanged" />
                                                </div>
                                            </div>
                                            <div style="float: left; height: 3px; color: Black;">
                                                &nbsp;
                                            </div>
                                            <div class="inv100break">
                                                <div style="float: left; width: 45%;">
                                                    Term &nbsp;&nbsp;<asp:DropDownList ID="DropDownListCoundition" runat="server" CssClass="ComboBox"
                                                        AutoPostBack="True" OnSelectedIndexChanged="DropDownListCoundition_SelectedIndexChanged">
                                                        <asp:ListItem Text="<" Value="<"></asp:ListItem>
                                                        <asp:ListItem Text=">" Value=">"></asp:ListItem>
                                                        <asp:ListItem Text="=" Value="="></asp:ListItem>
                                                        <asp:ListItem Text="&lt;=" Value="&lt;="></asp:ListItem>
                                                        <asp:ListItem Text="&gt;=" Value="&gt;="></asp:ListItem>
                                                    </asp:DropDownList>
                                                    &nbsp;&nbsp;<asp:TextBox ID="TextBoxTerm" runat="server" CssClass="TextBox" Width="60px"
                                                        MaxLength="2" onKeyPress="return numbersonly(event,false)" AutoPostBack="True"
                                                        OnTextChanged="TextBoxTerm_TextChanged"></asp:TextBox>
                                                </div>
                                                <div style="float: left; width: 55%;">
                                                    <asp:DropDownList ID="DropDownListSchemeType" runat="server" CssClass="ComboBox"
                                                        Width="200px" AutoPostBack="True" OnSelectedIndexChanged="DropDownListSchemeType_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                            <div style="float: left; height: 3px; color: Black;">
                                                &nbsp;
                                            </div>
                                            <div style="float: left; width: 100%; margin-left: 10%; margin-left: 10%">
                                                <asp:Panel ID="Panel2" runat="server" GroupingText=" " CssClass="Panel" Width="80%"
                                                    Height="180px" ScrollBars="Auto">
                                                    <asp:GridView ID="GridViewScheme" runat="server" AutoGenerateColumns="False" OnRowDataBound="GridViewPC_RowDataBound">
                                                        <Columns>
                                                            <asp:TemplateField>
                                                                <EditItemTemplate>
                                                                    <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                                                                </EditItemTemplate>
                                                                <ItemTemplate>
                                                                    <asp:CheckBox ID="chekPc" runat="server" Checked="true" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:BoundField DataField="HSD_CD" ShowHeader="False" />
                                                        </Columns>
                                                    </asp:GridView>
                                                </asp:Panel>
                                                <div style="float: left; width: 80%; text-align: right;">
                                                    <div style="float: left; width: 30%; text-align: right;">
                                                        <asp:Button ID="ButtonSchemeAll" runat="server" Text="All" CssClass="Button" OnClick="ButtonSchemeAll_Click" />
                                                    </div>
                                                    <div style="float: left; width: 30%; text-align: right;">
                                                        <asp:Button ID="ButtonSchemeNone" runat="server" Text="None" CssClass="Button" OnClick="ButtonSchemeNone_Click" />
                                                    </div>
                                                    <div style="float: left; width: 30%; text-align: right;">
                                                        <asp:Button ID="ButtonSchemeClear" runat="server" Text="Clear" CssClass="Button"
                                                            OnClick="ButtonSchemeClear_Click" />
                                                    </div>
                                                </div>
                                            </div>
                                        </asp:Panel>
                                    </div>
                                </div>
                                <div style="float: left; height: 1%; color: Black;">
                                    &nbsp;
                                </div>
                                <div class="inv100break">
                                    <asp:Panel ID="Panel3" runat="server" GroupingText=" " CssClass="Panel">
                                        <div class="inv100break">
                                            <div class="div1pcelt">
                                                &nbsp;
                                            </div>
                                            <div class="div32pcelt">
                                                <div style="float: left; width: 30%">
                                                    From
                                                </div>
                                                <div style="float: left; width: 70%">
                                                    <asp:TextBox ID="TextBoxFromDate" CssClass="TextBox" runat="server" Enabled="False"></asp:TextBox>
                                                    <asp:Image ID="Image1" runat="server" ImageUrl="~/Images/Calendar_scheduleHS.png" />
                                                    <asp:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="TextBoxFromDate"
                                                        PopupButtonID="Image1" Enabled="True" Format="dd/MMM/yyyy">
                                                    </asp:CalendarExtender>
                                                </div>
                                            </div>
                                            <div class="div1pcelt">
                                                &nbsp;
                                            </div>
                                            <div class="div32pcelt">
                                                <div style="float: left; width: 30%">
                                                    To
                                                </div>
                                                <div style="float: left; width: 70%">
                                                    <asp:TextBox ID="TextBoxTodate" CssClass="TextBox" runat="server" Enabled="False"></asp:TextBox>
                                                    <asp:Image ID="Image2" runat="server" ImageUrl="~/Images/Calendar_scheduleHS.png" />
                                                    <asp:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="TextBoxToDate"
                                                        PopupButtonID="Image2" Enabled="True" Format="dd/MMM/yyyy">
                                                    </asp:CalendarExtender>
                                                </div>
                                            </div>
                                            <div class="div1pcelt">
                                                &nbsp;
                                            </div>
                                            <div class="div32pcelt">
                                                <div style="float: left; width: 30%">
                                                    Check On
                                                </div>
                                                <div style="float: left; width: 70%">
                                                    <asp:DropDownList ID="DropDownListCheckOn" runat="server" CssClass="ComboBox">
                                                        <asp:ListItem Text="Unit Price" Value="UP"></asp:ListItem>
                                                        <asp:ListItem Text="Amount Finance" Value="AF"></asp:ListItem>
                                                        <asp:ListItem Text="Hire Value" Value="HP"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                        <div style="float: left; height: 2px; color: Black;">
                                            &nbsp;
                                        </div>
                                        <div class="inv100break">
                                            <div class="div1pcelt">
                                                &nbsp;
                                            </div>
                                            <div class="div32pcelt">
                                                <div style="float: left; width: 30%">
                                                    Value From
                                                </div>
                                                <div style="float: left; width: 70%">
                                                    <asp:TextBox ID="TextBoxValueFrom" CssClass="TextBox" runat="server" onKeyPress="return numbersonly(event,true)"
                                                        MaxLength="10"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="div1pcelt">
                                                &nbsp;
                                            </div>
                                            <div class="div32pcelt">
                                                <div style="float: left; width: 30%">
                                                    Value To
                                                </div>
                                                <div style="float: left; width: 70%">
                                                    <asp:TextBox ID="TextBoxValueTo" CssClass="TextBox" runat="server" onKeyPress="return numbersonly(event,true)"
                                                        MaxLength="10"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="div1pcelt">
                                                &nbsp;
                                            </div>
                                            <div class="div32pcelt">
                                                <div style="float: left; width: 30%">
                                                    Calculates On
                                                </div>
                                                <div style="float: left; width: 70%">
                                                    <asp:DropDownList ID="DropDownListCalcOn" runat="server" CssClass="ComboBox">
                                                        <asp:ListItem Text="Unit Price" Value="UP"></asp:ListItem>
                                                        <asp:ListItem Text="Amount Finance" Value="AF"></asp:ListItem>
                                                        <asp:ListItem Text="Hire Value" Value="HP"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                        <div style="float: left; height: 2px; color: Black;">
                                            &nbsp;
                                        </div>
                                        <div class="inv100break" style="display: none">
                                            <div class="div1pcelt">
                                                &nbsp;
                                            </div>
                                            <div style="float: left; width: 15%">
                                                Calculation By
                                            </div>
                                            <div style="float: left; width: 25%">
                                                <asp:RadioButton ID="RadioButtonRate" runat="server" Text="Rate" GroupName="G2" AutoPostBack="True"
                                                    OnCheckedChanged="RadioButtonRate_CheckedChanged" Checked="True" />
                                                <asp:RadioButton ID="RadioButtonValue" runat="server" Text="Value" GroupName="G2"
                                                    AutoPostBack="True" OnCheckedChanged="RadioButtonValue_CheckedChanged" />
                                            </div>
                                            <div style="float: left; width: 59%">
                                            </div>
                                        </div>
                                        <div class="inv100break">
                                            <div class="div1pcelt">
                                                &nbsp;
                                            </div>
                                            <div class="div32pcelt">
                                                Calculation By
                                            </div>
                                            <div class="div1pcelt">
                                                &nbsp;
                                            </div>
                                            <div class="div32pcelt">
                                                <div style="float: left; width: 30%">
                                                    Rate
                                                </div>
                                                <div style="float: left; width: 70%">
                                                    <asp:TextBox ID="TextBoxRate" CssClass="TextBox" runat="server" onKeyPress="return numbersonly(event,true)"
                                                        AutoPostBack="True" OnTextChanged="TextBoxRate_TextChanged"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="div1pcelt">
                                                &nbsp;
                                            </div>
                                            <div class="div32pcelt">
                                                <div style="float: left; width: 30%">
                                                    Value
                                                </div>
                                                <div style="float: left; width: 70%">
                                                    <asp:TextBox ID="TextBoxValue" CssClass="TextBox" runat="server" onKeyPress="return numbersonly(event,true)"
                                                        AutoPostBack="True" OnTextChanged="TextBoxValue_TextChanged"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div style="float: left; height: 2px; color: Black;">
                                            &nbsp;
                                        </div>
                                        <div class="inv100break">
                                            <div class="div1pcelt">
                                                &nbsp;
                                            </div>
                                            <div class="div32pcelt">
                                                <div style="float: left; width: 30%">
                                                    VAT %
                                                </div>
                                                <div style="float: left; width: 70%">
                                                    <asp:TextBox ID="TextBoxVat" CssClass="TextBox" runat="server" onKeyPress="return numbersonly(event,true)"
                                                        MaxLength="4"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="inv100break">
                                            <div class="div1pcelt">
                                                &nbsp;
                                            </div>
                                            <div class="div32pcelt">
                                                Commission
                                            </div>
                                            <div class="div1pcelt">
                                                &nbsp;
                                            </div>
                                            <div class="div32pcelt">
                                                <div style="float: left; width: 30%">
                                                    Rate
                                                </div>
                                                <div style="float: left; width: 70%">
                                                    <asp:TextBox ID="TextBoxComValue" CssClass="TextBox" runat="server" onKeyPress="return numbersonly(event,true)"
                                                        MaxLength="4" AutoPostBack="True" OnTextChanged="TextBoxComValue_TextChanged"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="div1pcelt">
                                                &nbsp;
                                            </div>
                                            <div class="div32pcelt">
                                                <div style="float: left; width: 30%">
                                                    Value
                                                </div>
                                                <div style="float: left; width: 70%">
                                                    <asp:TextBox ID="TextBoxComRate" CssClass="TextBox" runat="server" onKeyPress="return numbersonly(event,true)"
                                                        MaxLength="4" AutoPostBack="True" OnTextChanged="TextBoxComRate_TextChanged"></asp:TextBox>
                                                </div>
                                            </div>
                                    </asp:Panel>
                                </div>
                            </div>
                        </ContentTemplate>
                    </asp:TabPanel>
                    <asp:TabPanel ID="TabPanel1" runat="server" TabIndex="0" HeaderText="Transfer Details">
                        <ContentTemplate>
                            <div style="float: left; width: 100%; color: Black; font-family: Verdana; font-size: 11px;">
                                <div class="inv100break">
                                    <div style="float: left; width: 50%">
                                        <div class="div1pcelt">
                                            &nbsp;
                                        </div>
                                        <div style="float: left; width: 99%">
                                            <asp:RadioButton ID="RadioButtonPC" runat="server" 
                                                Text="Transfer Profit Center wise" GroupName="G3" AutoPostBack="True"  
                                                Checked="True" oncheckedchanged="RadioButtonPC_CheckedChanged"/>
                                        </div>
                                    </div>
                                    <div style="float: left; width: 50%">
                                        <div class="div1pcelt">
                                            &nbsp;
                                        </div>
                                        <div style="float: left; width: 99%">
                                            <asp:RadioButton ID="RadioButtonScheme" runat="server" 
                                                Text="Transfer Scheme wise" GroupName="G3" AutoPostBack="True" 
                                                oncheckedchanged="RadioButtonScheme_CheckedChanged" />
                                        </div>
                                    </div>
                                </div>
                                <div style="float:left;width:100%;height:10px;">
                                &nbsp;
                                </div>
                                <asp:Panel ID="PanelPC" runat="server">
                                    <div class="inv100break">
                                        <div style="float: left; width: 50%">
                                            <div class="div1pcelt">
                                                &nbsp;
                                            </div>
                                            <div style="float: left; width: 99%">
                                                <div style="float: left; width: 30%">
                                                  Original Profit Center
                                                </div>
                                                <div style="float: left; width: 70%">
                                                    <asp:TextBox ID="TextBoxPC" runat="server" CssClass="TextBox"></asp:TextBox>
                                                    <asp:ImageButton ID="ImageButtonPC" runat="server" 
                                                        ImageUrl="~/Images/icon_search.png" onclick="ImageButtonPC_Click" />
                                                </div>
                                            </div>
                                        </div>
                                        <div style="float: left; width: 50%">
                                            <div class="div1pcelt">
                                                &nbsp;
                                            </div>
                                            <div style="float: left; width: 99%">
                                                <div style="float: left; width: 30%">
                                                   Transfer Profit Center
                                                </div>
                                                <div style="float: left; width: 70%">
                                                    <asp:TextBox ID="TextBoxPC1" runat="server" CssClass="TextBox"></asp:TextBox>
                                                    <asp:ImageButton ID="ImageButtonPC1" runat="server" 
                                                        ImageUrl="~/Images/icon_search.png" onclick="ImageButtonPC1_Click" />
                                                    &nbsp;<asp:Button ID="ButtonAdd" runat="server" Text="Add" CssClass="Button" OnClick="ButtonAdd_Click" />
                                                </div>
                                            </div>
                                            <div class="div1pcelt">
                                                &nbsp;
                                            </div>
                                            <div style="float: left; width: 99%">
                                                <div style="float: left; width: 30%">
                                                    &nbsp;
                                                </div>
                                                <div style="float: left; width: 70%">
                                                    <asp:ListBox ID="ListBoxProfitCenters" runat="server" Width="130px"></asp:ListBox>
                                                     &nbsp; &nbsp; &nbsp;
                                                   <asp:LinkButton ID="LinkButtonRemovePC" runat="server" Text="Remove" 
                                                        onclick="LinkButtonRemovePC_Click"></asp:LinkButton>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </asp:Panel>
                                <asp:Panel ID="PanelScheme" runat="server" Visible="False">
                                 <div class="inv100break">
                                        <div style="float: left; width: 50%">
                                            <div class="div1pcelt">
                                                &nbsp;
                                            </div>
                                            <div style="float: left; width: 99%">
                                                <div style="float: left; width: 30%">
                                                   Original Scheme
                                                </div>
                                                <div style="float: left; width: 70%">
                                                    <asp:TextBox ID="TextBoxScheme" runat="server" CssClass="TextBox"></asp:TextBox>
                                                    <asp:ImageButton ID="ImageButtonScheme" runat="server" 
                                                        ImageUrl="~/Images/icon_search.png" onclick="ImageButtonScheme_Click" />
                                                </div>
                                            </div>
                                        </div>
                                        <div style="float: left; width: 50%">
                                            <div class="div1pcelt">
                                                &nbsp;
                                            </div>
                                            <div style="float: left; width: 99%">
                                                <div style="float: left; width: 30%">
                                                   Transfer Scheme
                                                </div>
                                                <div style="float: left; width: 70%">
                                                    <asp:TextBox ID="TextBoxScheme1" runat="server" CssClass="TextBox"></asp:TextBox>
                                                    <asp:ImageButton ID="ImageButtonScheme1" runat="server" 
                                                        ImageUrl="~/Images/icon_search.png" onclick="ImageButtonScheme1_Click" />
                                                    &nbsp;<asp:Button ID="ButtonAddScheme" runat="server" Text="Add" 
                                                        CssClass="Button" onclick="ButtonAddScheme_Click" />
                                                </div>
                                            </div>
                                            <div class="div1pcelt">
                                                &nbsp;
                                            </div>
                                            <div style="float: left; width: 99%">
                                                <div style="float: left; width: 30%">
                                                    &nbsp;
                                                </div>
                                                <div style="float: left; width: 70%">
                                                    <asp:ListBox ID="ListBoxScheme" runat="server" Width="130px"></asp:ListBox>
                                                    &nbsp; &nbsp; &nbsp;
                                                    <asp:LinkButton ID="LinkButtonRemoveScheme" runat="server" Text="Remove" 
                                                        onclick="LinkButtonRemoveScheme_Click"></asp:LinkButton>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </asp:Panel>
                            </div>
                        </ContentTemplate>
                    </asp:TabPanel>
                </asp:TabContainer>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
