<%@ Page Title="ECD Definition" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="ECDDefinition.aspx.cs" Inherits="FF.WebERPClient.General_Modules.ECDDefinition" %>

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

    
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="upMain" runat="server">
        <ContentTemplate>
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
                    <asp:Panel ID="Panel12" runat="server" GroupingText="Business Hirc Details" CssClass="Panel">
                        <div style="float: left; width: 65%;">
                            <uc1:uc_ProfitCenterSearch ID="uc_ProfitCenterSearchLoyaltyType" runat="server" />
                        </div>
                        <div style="float: left; width: 5%; text-align: center;">
                            <asp:ImageButton ID="ImageButtonAddPC" runat="server" ImageUrl="~/Images/right_arrow_icon.png"
                                Width="100%" ToolTip="Add to Profit Center List" OnClick="ImageButtonAddPC_Click" />
                        </div>
                        <div style="float: left; width: 30%; text-align: right;">
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
                <div style="float: left; width: 40%;">
                    <asp:Panel ID="Panel2" runat="server" GroupingText=" ">
                        <div style="float: left; width: 100%; background-color: #3333FF;">
                            <div style="color: #FFFFFF; font-weight: bold; background-color: #0066CC;">
                                Price Book Details</div>
                        </div>
                        <div style="padding: 1.5px; float: left; width: 100%;">
                        </div>
                        <div style="float: left; width: 100%;">
                            <div style="float: left; width: 20%;">
                                <asp:Label ID="Label30" runat="server" Text="Price Book" ForeColor="Black" Font-Size="X-Small"></asp:Label>
                            </div>
                            <div style="float: left; width: 30%;">
                                <asp:TextBox ID="txtPriceBook" runat="server" CssClass="TextBox" Width="70%"></asp:TextBox>
                                &nbsp;
                                <asp:ImageButton ID="imgBtnPBsearch" runat="server" ImageUrl="~/Images/icon_search.png"
                                    OnClick="imgBtnPBsearch_Click" />
                            </div>
                            <div style="float: left; width: 15%;">
                                <asp:Label ID="Label31" runat="server" Text="Book Level" ForeColor="Black" Font-Size="X-Small"></asp:Label>
                            </div>
                            <div style="float: left; width: 30%;">
                                <asp:TextBox ID="txtLevel" runat="server" CssClass="TextBox" Width="70%"></asp:TextBox>
                                &nbsp;
                                <asp:ImageButton ID="imgBtnLevelSearch" runat="server" ImageUrl="~/Images/icon_search.png"
                                    OnClick="imgBtnLevelSearch_Click" />
                            </div>
                            <div style="float: left; width: 5%;" align="left">
                                <asp:ImageButton ID="ImgBtnAddPB" runat="server" ImageUrl="~/Images/download_arrow_icon.png"
                                    Width="100%" OnClick="ImgBtnAddPB_Click" />
                            </div>
                            <div style="float: left; width: 100%; height: 118px;">
                                <asp:Panel ID="Panel7" runat="server" ScrollBars="Both" Style="margin-bottom: 20px"
                                    Height="118px">
                                    <asp:GridView ID="grvPB_PBL" runat="server" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True"
                                        CellPadding="4" CssClass="GridView" ForeColor="#333333" Width="98%" OnRowDeleting="grvPB_PBL_RowDeleting">
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
                                                    <asp:ImageButton ID="ImgBtnGrvDelPB" runat="server" CommandName="Delete" ImageUrl="~/Images/Delete.png" />
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                                                </EditItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField HeaderText="Price Book" DataField="Sapl_pb" />
                                            <asp:BoundField HeaderText="Price book Level" DataField="Sapl_pb_lvl_cd" />
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
                    </asp:Panel>
                </div>
            </div>
            <div style="float: left; height: 10px; width: 100%">
                &nbsp;
            </div>
            <div style="float: left; width: 100%">
                <div style="float: left; width: 40%">
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
                                    Width="100%" OnClick="ImageButton3_Click" />
                            </div>
                            <div style="float: left; width: 100%; height: 200px;">
                                <asp:Panel ID="Panel3" runat="server" ScrollBars="Both" Style="margin-bottom: 5px"
                                    Height="180px">
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
                                        <asp:Button ID="ButtonSchAll" runat="server" Text="All" CssClass="Button" OnClick="ButtonSchAll_Click" />
                                    </div>
                                    <div style="float: left; width: 30%; text-align: right;">
                                        <asp:Button ID="ButtonSchNone" runat="server" Text="None" CssClass="Button" OnClick="ButtonSchNone_Click" />
                                    </div>
                                    <div style="float: left; width: 30%; text-align: right;">
                                        <asp:Button ID="ButtonSchClear" runat="server" Text="Clear" CssClass="Button" OnClick="ButtonSchClear_Click" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </asp:Panel>
                </div>
                <div style="float: left; width: 60%">
                    <asp:Panel ID="Panel4" runat="server" GroupingText="Interest Based On">
                        <div style="float: left; width: 100%">
                            <div style="float: left; width: 1%">
                                &nbsp;
                            </div>
                            <div style="float: left; width: 24%">
                                <asp:RadioButton ID="RadioButtonFI" runat="server" GroupName="G1" 
                                    Text="Future interest" Checked="True" />
                            </div>
                            <div style="float: left; width: 1%">
                                &nbsp;
                            </div>
                            <div style="float: left; width: 24%">
                                <asp:RadioButton ID="RadioButtonCI" runat="server" GroupName="G1" Text="Interest in closing balance" />
                            </div>
                            <div style="float: left; width: 1%">
                                &nbsp;
                            </div>
                            <div style="float: left; width: 24%">
                                <asp:RadioButton ID="RadioButtonFR" runat="server" GroupName="G1" Text="Future rental balance" />
                            </div>
                            <div style="float: left; width: 1%">
                                &nbsp;
                            </div>
                            <div style="float: left; width: 24%">
                                <asp:RadioButton ID="RadioButtonTI" runat="server" GroupName="G1" Text="Closing balance" />
                            </div>
                        </div>
                    </asp:Panel>
                    <div style="float: left; height: 2px; width: 100%">
                        &nbsp;
                    </div>
                    <asp:Panel ID="Panel5" runat="server" GroupingText="Effective A/C's">
                        <div style="float: left; width: 100%">
                            <div style="float: left; width: 1%">
                                &nbsp;
                            </div>
                            <div style="float: left; width: 24%">
                                <asp:RadioButton ID="RadioButtonAA" runat="server" GroupName="G2" 
                                    Text="Arrears accounts" Checked="True" />
                            </div>
                            <div style="float: left; width: 1%">
                                &nbsp;
                            </div>
                            <div style="float: left; width: 24%">
                                <asp:RadioButton ID="RadioButtonGA" runat="server" GroupName="G2" Text="Good accounts" />
                            </div>
                            <div style="float: left; width: 1%">
                                &nbsp;
                            </div>
                            <div style="float: left; width: 24%">
                                <asp:RadioButton ID="RadioButtonAny" runat="server" GroupName="G2" Text="Any" />
                            </div>
                        </div>
                    </asp:Panel>
                    <div style="float: left; height: 2px; width: 100%">
                        &nbsp;
                    </div>
                    <asp:Panel ID="Panel6" runat="server" GroupingText="Effective Creation Date">
                        <div style="float: left; width: 100%">
                            <div style="float: left; width: 1%">
                                &nbsp;
                            </div>
                            <div style="float: left; width: 24%">
                                <asp:RadioButton ID="RadioButtonBC" runat="server" GroupName="G3" 
                                    Text=" Before given date" Checked="True" />
                            </div>
                            <div style="float: left; width: 1%">
                                &nbsp;
                            </div>
                            <div style="float: left; width: 24%">
                                <asp:RadioButton ID="RadioButtonAC" runat="server" GroupName="G3" Text="After given date" />
                            </div>
                            <div style="float: left; width: 1%">
                                &nbsp;
                            </div>
                            <div style="float: left; width: 24%">
                                <asp:RadioButton ID="RadioButtonDateAny" runat="server" GroupName="G3" Text="Any" />
                            </div>
                            <div style="float: left; width: 1%">
                                &nbsp;
                            </div>
                            <div style="float: left; width: 24%">
                                <div style="float: left; width: 20%">
                                    Date
                                </div>
                                <div style="float: left; width: 80%">
                                    <asp:TextBox ID="TextBoxDate" CssClass="TextBox" runat="server" Enabled="False" Width="80"></asp:TextBox>
                                    <asp:Image ID="Image1" runat="server" ImageUrl="~/Images/Calendar_scheduleHS.png" />
                                    <asp:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="TextBoxDate"
                                        PopupButtonID="Image1" Enabled="True" Format="dd/MMM/yyyy">
                                    </asp:CalendarExtender>
                                </div>
                            </div>
                        </div>
                    </asp:Panel>
                    <div style="float: left; height: 2px; width: 100%">
                        &nbsp;
                    </div>
                    <asp:Panel ID="Panel8" runat="server" GroupingText="ECD Restriction">
                        <div style="float: left; width: 100%">
                            <div style="float: left; width: 1%">
                                &nbsp;
                            </div>
                            <div style="float: left; width: 24%">
                                <asp:RadioButton ID="RadioButtonCP" runat="server" GroupName="G4" 
                                    Text="Covered cash price" Checked="True" />
                            </div>
                            <div style="float: left; width: 1%">
                                &nbsp;
                            </div>
                            <div style="float: left; width: 24%">
                                <asp:RadioButton ID="RadioButtonCS" runat="server" GroupName="G4" Text="Covered cash price and service charge" />
                            </div>
                            <div style="float: left; width: 1%">
                                &nbsp;
                            </div>
                            <div style="float: left; width: 24%">
                                <asp:RadioButton ID="RadioButtonResAny" runat="server" GroupName="G4" Text="Any" />
                            </div>
                        </div>
                    </asp:Panel>
                    <div style="float: left; height: 2px; width: 100%">
                        &nbsp;
                    </div>
                    <asp:Panel ID="Panel9" runat="server" GroupingText=" ">
                        <div style="float: left; width: 100%">
                            <div style="float: left; width: 1%">
                                &nbsp;
                            </div>
                            <div style="float: left; width: 24%">
                                &nbsp;
                            </div>
                            <div style="float: left; width: 1%">
                                &nbsp;
                            </div>
                            
                            <div style="float: left; width: 24%">
                                <div style="float: left; width: 20%">
                                    Value
                                </div>
                                <div style="float: left; width: 80%">
                                    <asp:TextBox ID="TextBoxValue" CssClass="TextBox" runat="server" Width="80" AutoPostBack="true"
                                        OnTextChanged="TextBoxValue_TextChanged"  onKeyPress="return numbersonly(event,true)" MaxLength="5"></asp:TextBox>
                                </div>
                            </div>
                            <div style="float: left; width: 1%">
                                &nbsp;
                            </div>
                            <div style="float: left; width: 24%">
                                <div style="float: left; width: 20%">
                                    Rate
                                </div>
                                <div style="float: left; width: 80%">
                                    <asp:TextBox ID="TextBoxRate" CssClass="TextBox" runat="server" Width="80" AutoPostBack="true"  onKeyPress="return numbersonly(event,true)"
                                        OnTextChanged="TextBoxRate_TextChanged" MaxLength="3"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div style="float: left; width: 100%">
                            <div style="float: left; width: 1%">
                                &nbsp;
                            </div>
                            <div style="float: left; width: 24%">
                                <div style="float: left; width: 20%">
                                    From
                                </div>
                                <div style="float: left; width: 80%">
                                    <asp:TextBox ID="TextBoxFromDate" CssClass="TextBox" runat="server" Enabled="False"
                                        Width="80"></asp:TextBox>
                                    <asp:Image ID="Image2" runat="server" ImageUrl="~/Images/Calendar_scheduleHS.png" />
                                    <asp:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="TextBoxFromDate"
                                        PopupButtonID="Image2" Enabled="True" Format="dd/MMM/yyyy">
                                    </asp:CalendarExtender>
                                </div>
                            </div>
                            <div style="float: left; width: 1%">
                                &nbsp;
                            </div>
                            <div style="float: left; width: 24%">
                                <div style="float: left; width: 20%">
                                    To
                                </div>
                                <div style="float: left; width: 80%">
                                    <asp:TextBox ID="TextBoxToDate" CssClass="TextBox" runat="server" Enabled="False"
                                        Width="80"></asp:TextBox>
                                    <asp:Image ID="Image3" runat="server" ImageUrl="~/Images/Calendar_scheduleHS.png" />
                                    <asp:CalendarExtender ID="CalendarExtender3" runat="server" TargetControlID="TextBoxToDate"
                                        PopupButtonID="Image3" Enabled="True" Format="dd/MMM/yyyy">
                                    </asp:CalendarExtender>
                                </div>
                            </div>
                        </div>
                    </asp:Panel>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
