<%@ Page Title="Reminder SMS" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="SMSReminders.aspx.cs" Inherits="FF.WebERPClient.HP_Module.SMSReminders" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="upMain" runat="server">
        <ContentTemplate>
            <div style="height: 22px; text-align: right;" class="PanelHeader">
                <asp:Button ID="ButtonSave" runat="server" Text="Send" CssClass="Button" OnClick="ButtonSave_Click" />
                <asp:Button ID="ButtonClear" runat="server" Text="Clear" CssClass="Button" OnClick="ButtonClear_Click" />
                <asp:Button ID="ButtonClose" runat="server" Text="Close" CssClass="Button" />
            </div>
            <div style="float: left; height: 10%; color: Black;">
                &nbsp;
            </div>
            <div style="float: left; width: 100%; color: Black;">
                <asp:Panel ID="Panel1" runat="server">
                    <div style="float: left; width: 100%; color: Black;">
                        <div style="float: left; width: 50%; color: Black;">
                            <asp:Panel ID="Panel2" runat="server" GroupingText="Messages Type">
                                <asp:RadioButtonList ID="RadioButtonListMessageType" runat="server" AutoPostBack="true"
                                    OnSelectedIndexChanged="RadioButtonListMessageType_SelectedIndexChanged">
                                    <asp:ListItem Text="Genaral Text" Value="GT" Selected="True"></asp:ListItem>
                                    <asp:ListItem Text="Arrears Accounts" Value="AA"></asp:ListItem>
                                    <asp:ListItem Text="Monthly Due" Value="MD"></asp:ListItem>
                                    <asp:ListItem Text="HP Customers" Value="HP"></asp:ListItem>
                                </asp:RadioButtonList>
                                <div style="float: left; height: 28px; color: Black;">
                                    &nbsp;
                                </div>
                            </asp:Panel>
                            <div style="float: left; height: 40px; color: Black;">
                                &nbsp;
                            </div>
                            <div style="float: left; width: 100%; color: Black;">
                                <div style="float: left; width: 75%; color: Black;">
                                    &nbsp;
                                    <asp:RadioButton ID="RadioButtonCustomer" runat="server" Text="Customer" GroupName="SendType"
                                        Checked="True" AutoPostBack="true" OnCheckedChanged="RadioButtonCustomer_CheckedChanged" />
                                    &nbsp;&nbsp;
                                    <asp:RadioButton ID="RadioButtonGuarantor" runat="server" Text="Guarantor" GroupName="SendType"
                                        Visible="false" AutoPostBack="true" OnCheckedChanged="RadioButtonGuarantor_CheckedChanged" />
                                </div>
                                <div style="float: left; width: 25%; color: Black;">
                                    <asp:Button ID="ButtonSearch" runat="server" CssClass="Button" Text="Search" OnClick="ButtonSearch_Click" />
                                </div>
                            </div>
                        </div>
                        <div style="float: left; width: 50%; color: Black;">
                            <asp:Panel ID="PanelPCs" runat="server" GroupingText="PC List">
                                <asp:Panel ID="Panel3" runat="server" GroupingText="PC List" Height="100" ScrollBars="Auto">
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
                                            <asp:BoundField DataField="Code" ShowHeader="False" />
                                        </Columns>
                                    </asp:GridView>
                                </asp:Panel>
                                <div style="float: left; width: 50%; color: Black; padding-top: 3px;">
                                    <div style="float: left; width: 50%; color: Black;">
                                        <asp:Button ID="ButtonAll" runat="server" CssClass="Button" Text="All" OnClick="ButtonAll_Click" />
                                    </div>
                                    <div style="float: left; width: 50%; color: Black;">
                                        <asp:Button ID="ButtonNone" runat="server" CssClass="Button" Text="None" OnClick="ButtonNone_Click" />
                                    </div>
                                </div>
                            </asp:Panel>
                            <div style="float: left; height: 5px; color: Black;">
                                &nbsp;
                            </div>
                            <%--arrears account--%>
                            <div style="float: left; width: 100%; color: Black;" runat="server" id="DivArrears"
                                visible="false">
                                <div style="float: left; width: 1%; color: Black;">
                                    &nbsp;
                                </div>
                                <div style="float: left; width: 49%; color: Black;">
                                    <div style="float: left; width: 30%; color: Black;">
                                        As At Date
                                    </div>
                                    <div style="float: left; width: 70%; color: Black;">
                                        <asp:TextBox ID="TextBoxAsAtDate" runat="server" CssClass="TextBox TextBoxUpper"
                                            Enabled="False" Width="108px"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <%--monthly due--%>
                            <div style="float: left; width: 100%; color: Black;" runat="server" id="DivMonthlyDue"
                                visible="false">
                                <div style="float: left; width: 1%; color: Black;">
                                    &nbsp;
                                </div>
                                <div style="float: left; width: 49%; color: Black;">
                                    <div style="float: left; width: 30%; color: Black;">
                                        Date
                                    </div>
                                    <div style="float: left; width: 70%; color: Black;">
                                        <asp:TextBox ID="TextBoxDueDt" runat="server" CssClass="TextBox TextBoxUpper" Enabled="False"
                                            Width="108px"></asp:TextBox>
                                    </div>
                                </div>
                                <div style="float: left; width: 1%; color: Black;">
                                    &nbsp;
                                </div>
                            </div>
                            <div style="float: left; width: 100%; color: Black;" runat="server" id="DivHpCus"
                                visible="false">
                                <div style="float: left; width: 1%; color: Black;">
                                    &nbsp;
                                </div>
                                <div style="float: left; width: 49%; color: Black;">
                                    <div style="float: left; width: 30%; color: Black;">
                                        Date From
                                    </div>
                                    <div style="float: left; width: 70%; color: Black;">
                                        <asp:TextBox ID="TextBoxDateFrom" runat="server" CssClass="TextBox TextBoxUpper"
                                            Enabled="False" Width="108px"></asp:TextBox>
                                        <asp:Image ID="Image3" runat="server" ImageUrl="~/Images/Calendar_scheduleHS.png" />
                                        <asp:CalendarExtender ID="CalendarExtender3" runat="server" TargetControlID="TextBoxDateFrom"
                                            PopupButtonID="Image3" Enabled="True" Format="dd/MMM/yyyy">
                                        </asp:CalendarExtender>
                                    </div>
                                </div>
                                <div style="float: left; width: 1%; color: Black;">
                                    &nbsp;
                                </div>
                                <div style="float: left; width: 49%; color: Black;">
                                    <div style="float: left; width: 30%; color: Black;">
                                        Date To
                                    </div>
                                    <div style="float: left; width: 70%; color: Black;">
                                        <asp:TextBox ID="TextBoxDateTo" runat="server" CssClass="TextBox TextBoxUpper" Enabled="False"
                                            Width="108px"></asp:TextBox>
                                        <asp:Image ID="Image2" runat="server" ImageUrl="~/Images/Calendar_scheduleHS.png" />
                                        <asp:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="TextBoxDateTo"
                                            PopupButtonID="Image2" Enabled="True" Format="dd/MMM/yyyy">
                                        </asp:CalendarExtender>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div style="float: left; width: 100%; height: 10px; color: Black;">
                            &nbsp;
                        </div>
                        <div style="float: left; width: 100%; color: Black;">
                            <asp:Panel ID="Panel4" runat="server" GroupingText=" ">
                                <div style="float: left; width: 100%; color: Black;">
                                    <div style="float: left; width: 1%; color: Black;">
                                        &nbsp;
                                    </div>
                                    <div style="float: left; width: 10%; color: Black;">
                                        Message :
                                    </div>
                                    <div style="float: left; width: 70%; color: Black;">
                                        <asp:Label ID="LabelMessage" runat="server"></asp:Label>
                                        <asp:TextBox ID="TextBoxMessage" runat="server" CssClass="TextBox" Width="750" Visible="false"></asp:TextBox>
                                    </div>
                                </div>
                            </asp:Panel>
                        </div>
                        <div style="float: left; width: 100%; height: 15px; color: Black;">
                            &nbsp;
                        </div>
                        <div style="float: left; width: 100%; color: Black;">
                            <div style="float: left; width: 50%; color: Black;">
                                <div style="float: left; width: 100%; color: Black;">
                                    <asp:Panel ID="Panel5" runat="server" GroupingText="Customer Details" Height="400"
                                        ScrollBars="Auto">
                                        <asp:GridView ID="GridViewCust" runat="server" Width="99%" ShowHeaderWhenEmpty="True"
                                            AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="Both"
                                            CssClass="GridView">
                                            <AlternatingRowStyle BackColor="White" />
                                            <Columns>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="chekPc" runat="server" Checked="true" AutoPostBack="True" OnCheckedChanged="chekPc_CheckedChanged" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="HTC_ACC_NO" HeaderText="Acc No" />
                                                <asp:BoundField DataField="MBE_NAME" HeaderText="Cus Name" />
                                                <asp:BoundField DataField="ITEM_CD" HeaderText="Product" />
                                                <asp:TemplateField HeaderText="Arrears" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="TextBoxArrears" runat="server" CssClass="TextBox" Text='<%# Bind("Arrears") %>'></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="Due Date" Visible="false">
                                                    <ItemTemplate>
                                                       <asp:Label ID="LabelDue" runat="server" Text='<%# Bind("Due") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <%--<asp:BoundField DataField="DUE" HeaderText="Due Date" Visible="false" DataFormatString='<%$ appSettings:FormatToDate %>'
                                                    HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:BoundField>--%>
                                                <asp:BoundField DataField="HPA_ACC_CRE_DT" HeaderText="Date" DataFormatString='<%$ appSettings:FormatToDate %>'
                                                    HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="MBE_MOB" HeaderText="Contact" />
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
                                    <div style="float: left; width: 100%; color: Black;">
                                        <div style="float: left; width: 30%; color: Black;">
                                            <asp:CheckBox ID="CheckBoxSelectAll" runat="server" Text="Select All" AutoPostBack="true"
                                                OnCheckedChanged="CheckBoxSelectAll_CheckedChanged" Checked="true" />
                                        </div>
                                        <div style="float: left; width: 70%; color: Black;">
                                            No Of
                                            <asp:Label ID="SendCount" runat="server" ForeColor="Blue"></asp:Label>
                                            accounts are selected from
                                            <asp:Label ID="LabelAllCount" runat="server" ForeColor="Blue"></asp:Label>
                                            accounts
                                        </div>
                                    </div>
                                    <div style="float: left; width: 100%; height: 10px; color: Black;">
                                        &nbsp;
                                    </div>
                                </div>
                            </div>
                            <div style="float: left; width: 50%; color: Black;">
                                <asp:Panel ID="Panel6" runat="server" GroupingText="Fail Customers" Height="400"
                                    ScrollBars="Auto">
                                    <asp:GridView ID="GridViewFailCust" runat="server" Width="99%" ShowHeaderWhenEmpty="True"
                                        AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="Both"
                                        CssClass="GridView">
                                        <AlternatingRowStyle BackColor="White" />
                                        <Columns>
                                            <asp:BoundField DataField="HTC_ACC_NO" HeaderText="Acc No" />
                                            <asp:TemplateField HeaderText="Reason">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblMessage" Text="Already Send" runat="server"></asp:Label>
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
                        </div>
                    </div>
                </asp:Panel>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
