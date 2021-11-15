<%@ Page Title="Vehical Insurance Definition" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="VehicalInsuranceDefinition.aspx.cs" Inherits="FF.WebERPClient.General_Modules.VehicalInsuranceDefinition" %>

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
    <style type="text/css">
        .fixWidth tr td
        {
            width: 45%;
        }
        
        .Panel legend
        {
            color: Blue;
        }
        .Label
        {
            border: none;
            background-color: white;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="updtMainPnl" runat="server">
        <ContentTemplate>
            <asp:HiddenField ID="HiddenFieldRowCount" runat="server" Value="0" />
            <div style="float: left; width: 100%; color: Black;">
                <div style="text-align: right;" class="PanelHeader">
                    <asp:Button ID="ButtonSave" runat="server" Text="Save" CssClass="Button" OnClick="ButtonSave_Click"
                        OnClientClick="return DisplayConfirm();" />
                    <asp:Button ID="ButtonClear" runat="server" Text="Clear" CssClass="Button" OnClick="ButtonClear_Click" />
                    <asp:Button ID="ButtonClose" runat="server" Text="Close" CssClass="Button" OnClick="ButtonClose_Click" />
                </div>
                <div style="height: 10px; float: left; width: 100%; color: Black;">
                    &nbsp;
                </div>
                <asp:Panel ID="Panel1" runat="server" GroupingText=" " CssClass="Panel">
                    <div style="float: left; width: 100%; padding-top: 10px;">
                        <div style="float: left; width: 1%;">
                            &nbsp;
                        </div>
                        <div class="PanelHeader" style="float: left; width: 98%; height: 10px;">
                            Main Details
                        </div>
                    </div>
                    <div style="float: left; height: 5px;">
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
                        <div style="float: left; width: 32%; padding-top: 5px;">
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
                    <div style="float: left; width: 100%; color: Black;">
                        <div style="float: left; width: 1%;">
                            &nbsp;
                        </div>
                        <div style="float: left; width: 32%; padding-top: 5px;">
                            <div style="float: left; width: 25%;">
                                Ins. Comp.
                            </div>
                            <div style="float: left; width: 70%;">
                                <asp:DropDownList ID="DropDownListInsCom" runat="server" CssClass="ComboBox" Width="150px">
                                </asp:DropDownList>
                                <asp:LinkButton ID="LinkButtonAddCom" runat="server" Text="Add" OnClick="LinkButtonAddCom_Click"></asp:LinkButton>
                                <asp:LinkButton ID="LinkButton1" runat="server"></asp:LinkButton>
                                <asp:ModalPopupExtender ID="ModalPopupExtender2" runat="server" TargetControlID="LinkButton1"
                                    ClientIDMode="Static" PopupControlID="PanelInsCom" BackgroundCssClass="modalBackground"
                                    PopupDragHandleControlID="divpopHeader">
                                </asp:ModalPopupExtender>
                            </div>
                        </div>
                        <div style="float: left; width: 1%;">
                            &nbsp;
                        </div>
                        <div style="float: left; width: 32%; padding-top: 5px;">
                            <div style="float: left; width: 25%;">
                                Ins. Policy
                            </div>
                            <div style="float: left; width: 70%;">
                                <asp:DropDownList ID="DropDownListInsPol" runat="server" CssClass="ComboBox" Width="150px">
                                </asp:DropDownList>
                                <asp:LinkButton ID="LinkButtonAddPol" runat="server" Text="Add" OnClick="LinkButtonAddPol_Click"></asp:LinkButton>
                                <asp:LinkButton ID="LinkButton2" runat="server"></asp:LinkButton>
                                <asp:ModalPopupExtender ID="ModalPopupExtender1" runat="server" TargetControlID="LinkButton2"
                                    ClientIDMode="Static" PopupControlID="PanelPolicy" BackgroundCssClass="modalBackground"
                                    PopupDragHandleControlID="div1">
                                </asp:ModalPopupExtender>
                            </div>
                        </div>

                       
                    </div>

                    <%--3rd row--%>
                     <div style="float: left; width: 100%; color: Black;">
                        <div style="float: left; width: 1%;">
                            &nbsp;
                        </div>
                        <div style="float: left; width: 32%; padding-top: 5px;">
                            <div style="float: left; width: 25%;">
                                Ins. Period
                            </div>
                            <div style="float: left; width: 70%;">
                               <asp:DropDownList ID="DropDownListPeriod" runat="server">
                                   <asp:ListItem>3</asp:ListItem>
                                   <asp:ListItem>6</asp:ListItem>
                                   <asp:ListItem>9</asp:ListItem>
                                   <asp:ListItem>12</asp:ListItem>
<%--                                   <asp:ListItem>15</asp:ListItem>
                                   <asp:ListItem>18</asp:ListItem>
                                   <asp:ListItem>21</asp:ListItem>
                                   <asp:ListItem>24</asp:ListItem>
                                   <asp:ListItem>27</asp:ListItem>
                                   <asp:ListItem>30</asp:ListItem>
                                   <asp:ListItem>33</asp:ListItem>
                                   <asp:ListItem>36</asp:ListItem>
                                   <asp:ListItem>39</asp:ListItem>
                                   <asp:ListItem>42</asp:ListItem>
                                   <asp:ListItem>45</asp:ListItem>
                                   <asp:ListItem>48</asp:ListItem>--%>
                                </asp:DropDownList>
                            </div>
                        </div><div style="float: left; width: 1%;">
                            &nbsp;
                        </div>
                        <div style="float: left; width: 32%; padding-top: 5px;">
                            <div style="float: left; width: 25%;">
                                Ins. value
                            </div>
                            <div style="float: left; width: 70%;">
                                <asp:TextBox ID="TextBoxVal" runat="server" CssClass="TextBox" Text="0" style="text-align:right;" onKeyPress="return numbersonly(event,true)"></asp:TextBox>
                            </div>
                        </div>
                        <div style="float: left; width: 1%;">
                            &nbsp;
                        </div>
                        <div style="float: left; width: 32%; padding-top: 5px;">
                            <div style="float: left; width: 25%;">
                                Compulsory
                            </div>
                            <div style="float: left; width: 70%;">
                               <asp:CheckBox ID="CheckBoxReq" runat="server" />
                            </div>
                        </div>
                    </div>
                    <div style="float: left; height: 5px;">
                        &nbsp;
                    </div>
                </asp:Panel>
                <div style="height: 10px;">
                    &nbsp;
                </div>
                <%--  pc and item headder--%>
                <asp:Panel ID="PanelGitm" runat="server" GroupingText="Add company and item" CssClass="Panel">
                    <div style="float: left; height: 10px;">
                            &nbsp;
                        </div>
                    <div style="float: left; width: 100%;">
                        <div style="float: left; width: 1%;">
                            &nbsp;
                        </div>
                        <div class="PanelHeader" style="float: left; width: 48%; height: 10px;">
                            Company Details
                        </div>
                        <div style="float: left; width: 2%;">
                            &nbsp;
                        </div>
                        <div class="PanelHeader" style="float: left; width: 48%; height: 10px;">
                            Item Details
                        </div>
                    </div>
                    <%--end of item headder--%>
                    <div style="float: left; width: 100%;">
                        <div style="float: left; width: 1%;">
                            &nbsp;
                        </div>
                       
                        <div style="float: left; width: 48%; padding-top: 2px;">
                         <asp:Panel ID="GCom" runat="server" GroupingText=" ">
                            <%-- company--%>
                            <div style="float: left; width: 100%;">
                                <div style="float: left; width: 25%;">
                                    Company
                                </div>
                                <div style="float: left; width: 70%;">
                                    <asp:DropDownList ID="DropDownListCompany" runat="server" CssClass="ComboBox" Width="150px"
                                        AutoPostBack="true" AppendDataBoundItems="true">
                                    </asp:DropDownList>
                                </div>
                                <div style="float: left; width: 25%;">
                                    Location
                                </div>
                                <div style="float: left; width: 70%;">
                                    <asp:TextBox ID="TextBoxLoc" runat="server" CssClass="TextBoxUpper"></asp:TextBox>
                                    <asp:CheckBox ID="CheckBoxAll" runat="server" Text="ALL" />
                                </div>
                                <div style="float: left; width: 85%; text-align: right; padding-top: 1px;">
                                    <asp:Button ID="ButtonSearchLoc" runat="server" Text="Search" CssClass="Button" OnClick="ButtonSearchLoc_Click" />
                                </div>
                                <div style="float: left; width: 100%; padding-top: 1px;">
                                    <div style="float: left; width: 15%;">
                                        Locations
                                    </div>
                                    <div style="float: left; width: 80%;">
                                        <asp:ListBox ID="ListBoxLoc" runat="server" CssClass="ComboBox" Rows="4" Width="370px"
                                            SelectionMode="Multiple"></asp:ListBox>
                                        <asp:LinkButton ID="LinkButtonLclear" runat="server" Text="Clear" OnClick="LinkButtonLclear_Click"></asp:LinkButton>
                                    </div>
                                </div>
                            </div>
                            </asp:Panel>
                        </div>
                        
                        <%--end of company--%>
                        <div style="float: left; width: 2%;">
                            &nbsp;
                        </div>
                      
                        <div style="float: left; width: 48%;">
                          <asp:Panel ID="Panel4" runat="server" GroupingText=" ">
                            <%-- item--%>
                            <div style="float: left; width: 100%; padding-top: 2px;">
                                <div style="float: left; width: 1%;">
                                    &nbsp;
                                </div>
                                <div style="float: left; width: 15%;">
                                    Brand
                                </div>
                                <div style="float: left; width: 33%;">
                                    <asp:DropDownList ID="DropDownListBrand" runat="server" CssClass="ComboBox" AutoPostBack="true"
                                        Width="150px" AppendDataBoundItems="true" OnSelectedIndexChanged="DropDownListBrand_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </div>
                                <div style="float: left; width: 2%;">
                                    &nbsp;
                                </div>
                                <div style="float: left; width: 15%;">
                                    Category
                                </div>
                                <div style="float: left; width: 33%;">
                                    <asp:DropDownList ID="DropDownListCat" runat="server" Width="150px" CssClass="ComboBox"
                                        AutoPostBack="true" AppendDataBoundItems="true" OnSelectedIndexChanged="DropDownListCat_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <%-- 2nd row--%>
                            <div style="float: left; width: 100%; color: Black; padding-top: 2px;">
                                <div style="float: left; width: 100%;">
                                    <div style="float: left; width: 1%;">
                                        &nbsp;
                                    </div>
                                    <div style="float: left; width: 15%;">
                                        Sub Cat.
                                    </div>
                                    <div style="float: left; width: 33%;">
                                        <asp:DropDownList ID="DropDownListSCat" runat="server" Width="150px" CssClass="ComboBox"
                                            AutoPostBack="true" AppendDataBoundItems="true" OnSelectedIndexChanged="DropDownListSCat_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </div>
                                    <div style="float: left; width: 2%;">
                                        &nbsp;
                                    </div>
                                    <div style="float: left; width: 15%;">
                                        Model
                                    </div>
                                    <div style="float: left; width: 33%;">
                                        <asp:DropDownList ID="DropDownListIRange" runat="server" Width="150px" CssClass="ComboBox"
                                            AutoPostBack="true" AppendDataBoundItems="true">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                            <%--       3rd row--%>
                            <div style="float: left; width: 100%; color: Black;">
                                <div style="float: left; width: 100%;">
                                    <div style="float: left; width: 1%;">
                                        &nbsp;
                                    </div>
                                    <div style="float: left; width: 15%;">
                                        Item
                                    </div>
                                    <div style="float: left; width: 70%;">
                                        <asp:TextBox ID="TextBoxItem" runat="server" CssClass="TextBoxUpper"></asp:TextBox>
                                        <asp:CheckBox ID="CheckBoxItemAll" runat="server" Text="ALL" />
                                        &nbsp;&nbsp;&nbsp;&nbsp;
                                        <asp:Button ID="ButtonSearch" runat="server" Text="Search" CssClass="Button" OnClick="ButtonSearch_Click" />
                                    </div>
                                </div>
                            </div>
                            <div style="float: left; width: 100%; padding-top: 1px;">
                                <div style="float: left; width: 15%;">
                                    Items
                                </div>
                                <div style="float: left;width:80%; height:100px; overflow:auto;">
                                    <asp:ListBox Rows="4" ID="ListBoxItems" runat="server" CssClass="ComboBox" SelectionMode="Multiple"
                                        Width="600px"></asp:ListBox>
                                    <asp:LinkButton ID="LinkButtonItClear" runat="server" Text="Clear" OnClick="LinkButtonItClear_Click"></asp:LinkButton>
                                </div>
                            </div>
                             </asp:Panel>
                        </div>
                       
                        <%--end of item details--%>
                    </div>
                    <div style="float: left; width: 85%; text-align: right; padding-top: 1px;">
                        <asp:Button ID="ButtonOAdd" runat="server" CssClass="Button" Text="Add" OnClick="ButtonOAdd_Click" />
                    </div>
                </asp:Panel>
                <div style="height: 10px; float: left; width: 100%; color: Black;">
                    &nbsp;
                </div>
                <div style="float: left; width: 100%;">
                    <asp:Panel ID="Panel2" runat="server" Height="218px" GroupingText=" " ScrollBars="Auto">
                        <asp:GridView ID="GridViewFinal" runat="server" Width="98%" EmptyDataText="No data found"
                            ShowHeaderWhenEmpty="True" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333"
                            GridLines="Both" CssClass="GridView" OnRowDeleting="GridViewFinal_RowDeleting">
                            <AlternatingRowStyle BackColor="White" />
                            <Columns>
                                <asp:BoundField HeaderText="From" DataField="From" DataFormatString='<%$ appSettings:FormatToDate %>'>
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                <asp:BoundField HeaderText="To" DataField="To" DataFormatString='<%$ appSettings:FormatToDate %>'>
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                <asp:BoundField HeaderText="Sales Type" DataField="Sales_Type" />
                                <asp:BoundField HeaderText="Ins. Com." DataField="Ins_Com" />
                                <asp:BoundField HeaderText="Ins. Pol." DataField="Ins_Pol" />
                                <asp:BoundField HeaderText="Value" DataField="Value"  DataFormatString='<%$ appSettings:FormatToCurrency %>'>
                                        <HeaderStyle HorizontalAlign="Right" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:BoundField>
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
            <asp:Panel ID="PanelInsCom" runat="server">
                <div class="popUpHeader" id="divpopHeader">
                    <div style="float: left; width: 80%">
                        Insurance Company</div>
                    <div style="float: left; width: 20%; text-align: right">
                        <asp:ImageButton ID="imgbtnCloseCom" runat="server" ImageUrl="~/Images/0032-01-16x16x16.ICO" />&nbsp;
                    </div>
                </div>
                <asp:Panel ID="PanelInsComAdd" runat="server" GroupingText=" " BackColor="White">
                    <div style="height: 10px; float: left; width: 100%; color: Black;">
                        &nbsp;
                    </div>
                    <div style="float: left; width: 100%;">
                        <div style="float: left; width: 2%;">
                            &nbsp;
                        </div>
                        <div style="float: left; width: 25%;">
                            Company Name
                        </div>
                        <div style="float: left; width: 70%;">
                            <asp:TextBox ID="TextBoxInsName" runat="server" CssClass="TextBoxUpper"></asp:TextBox>
                        </div>
                    </div>
                    <div style="height: 2px; float: left;">
                        &nbsp;
                    </div>
                    <%--2nd row--%>
                    <div style="float: left; width: 100%;">
                        <div style="float: left; width: 2%;">
                            &nbsp;
                        </div>
                        <div style="float: left; width: 25%;">
                            Company Code
                        </div>
                        <div style="float: left; width: 70%;">
                            <asp:TextBox ID="TextBoxInsCode" runat="server" CssClass="TextBoxUpper" MaxLength="5"></asp:TextBox>
                        </div>
                    </div>
                    <div style="float: left; width: 100%;">
                        <div style="float: left; width: 2%;">
                            &nbsp;
                        </div>
                        <div style="float: left; width: 25%;">
                        </div>
                        <div style="float: left; width: 60%; text-align: right;">
                            <asp:Button ID="ButtonInsComAdd" runat="server" CssClass="Button" Text="Add New"
                                OnClick="ButtonInsComAdd_Click" />
                        </div>
                    </div>
                    <div style="height: 10px; float: left; width: 100%; color: Black;">
                        &nbsp;
                    </div>
                    <div style="float: left; width: 100%;">
                        <asp:Panel ID="Panel3" runat="server" ScrollBars="Auto">
                            <asp:GridView ID="GridViewInsCompany" runat="server" Width="98%" ShowHeaderWhenEmpty="True"
                                AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="Both"
                                CssClass="GridView">
                                <AlternatingRowStyle BackColor="White" />
                                <Columns>
                                    <asp:BoundField HeaderText="Company Name" DataField="mbi_desc" />
                                    <asp:BoundField HeaderText="Company Code" DataField="mbi_cd" />
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
            </asp:Panel>
            <asp:Panel ID="PanelPolicy" runat="server" BackColor="White">
                <div class="popUpHeader" id="div1">
                    <div style="float: left; width: 80%">
                        Insurance Policy</div>
                    <div style="float: left; width: 20%; text-align: right">
                        <asp:ImageButton ID="ImageButtonPolClose" runat="server" ImageUrl="~/Images/0032-01-16x16x16.ICO" />&nbsp;
                    </div>
                </div>
                <asp:Panel ID="PanelPolAdd" runat="server" GroupingText=" " BackColor="White">
                <div style="height: 10px; float: left; width: 100%; color: Black;">
                        &nbsp;
                    </div>
                    <div style="float: left; width: 25%;">
                        Policy Type
                    </div>
                    <div style="float: left; width: 70%;">
                        <asp:TextBox ID="TextBoxPType" runat="server" CssClass="TextBoxUpper"></asp:TextBox>
                    </div>
                    <%--2nd row--%>
                    <div style="float: left; width: 100%;">
                        <div style="float: left; width: 2%;">
                            &nbsp;
                        </div>
                        <div style="float: left; width: 25%;">
                        </div>
                        <div style="float: left; width: 70%; text-align: right;padding-top:5px;">
                            <asp:Button ID="ButtonPolAdd" runat="server" CssClass="Button" Text="Add New" OnClick="ButtonPolAdd_Click" />
                        </div>
                    </div>
                    <div style="height: 10px; float: left; width: 100%; color: Black;">
                        &nbsp;
                    </div>
                    <%--grid--%>
                    <div style="float: left; width: 100%;">
                        <asp:GridView ID="GridViewPolicy" runat="server" Width="98%" ShowHeaderWhenEmpty="True"
                            AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="Both"
                            CssClass="GridView">
                            <AlternatingRowStyle BackColor="White" />
                            <Columns>
                                <asp:BoundField HeaderText="Policy Code" DataField="SVIP_POLC_CD" />
                                <asp:BoundField HeaderText="Description" DataField="SVIP_POLC_DESC" />
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
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
