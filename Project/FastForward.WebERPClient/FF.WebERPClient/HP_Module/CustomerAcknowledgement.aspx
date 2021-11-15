<%@ Page Title="Customer Acknoledgement" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="CustomerAcknowledgement.aspx.cs" Inherits="FF.WebERPClient.HP_Module.CustomerAcknowledgement" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
<script type="text/javascript">
    function LinkClick(sender, args) {
        document.getElementById('<%= LinkButtonTemp.ClientID %>').click()
    }
    function TextBoxBlur() {
        document.getElementById('<%= LinkButtonTemp.ClientID %>').click()
    }
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="upMain" runat="server">
        <ContentTemplate>
            <div style="height: 22px; text-align: right;" class="PanelHeader">
                <asp:Button ID="ButtonSave" runat="server" Text="Print" CssClass="Button" 
                    onclick="ButtonSave_Click" />
                <asp:Button ID="ButtonClear" runat="server" Text="Clear" CssClass="Button" 
                    onclick="ButtonClear_Click" />
                <asp:Button ID="ButtonClose" runat="server" Text="Close" CssClass="Button" 
                    onclick="ButtonClose_Click" />
            </div>
            <div style="float: left; height: 10%; color: Black;">
                &nbsp;
            </div>
            <div style="float: left; width: 100%; color: Black;">
                <asp:Panel ID="PanelParm" runat="server" GroupingText=" ">
                    <div style="float: left; width: 80%; color: Black;">
                        <div style="float: left; width: 1%; color: Black;">
                            &nbsp;
                        </div>
                        <div style="float: left; width: 49%; color: Black;">
                            <div style="float: left; width: 30%; color: Black;">
                                Profit Center
                            </div>
                            <div style="float: left; width: 70%; color: Black;">
                                <asp:TextBox ID="TextBoxLocation" runat="server" CssClass="TextBoxUpper" onblur="TextBoxBlur()"></asp:TextBox>
                                <asp:ImageButton ID="imgbtnSearchLocation" runat="server" 
                                    ImageUrl="~/Images/icon_search.png" ImageAlign="Middle" 
                                    onclick="imgbtnSearchLocation_Click"  />
                            </div>
                        </div>
                        <div style="float: left; width: 1%; color: Black;">
                            &nbsp;
                        </div>
                      
                    </div>
                    <div style="float: left; height: 2%; color: Black;">
                        &nbsp;
                    </div>
                    <div style="float: left; width: 80%; color: Black;">
                        <div style="float: left; width: 1%; color: Black;">
                            &nbsp;
                        </div>
                        <div style="float: left; width: 49%; color: Black;">
                            <div style="float: left; width: 30%; color: Black;">
                                Creation Date From
                            </div>
                            <div style="float: left; width: 70%; color: Black;">
                                <asp:TextBox ID="TextBoxCreationDateFrom" runat="server" CssClass="TextBox TextBoxUpper"
                                    Enabled="False" Width="108px"></asp:TextBox>
                                <asp:Image ID="Image3" runat="server" ImageUrl="~/Images/Calendar_scheduleHS.png" />
                                <asp:CalendarExtender ID="CalendarExtender3" runat="server" TargetControlID="TextBoxCreationDateFrom"
                                    PopupButtonID="Image3" Enabled="True" Format="dd/MMM/yyyy"  OnClientDateSelectionChanged="LinkClick">
                                </asp:CalendarExtender>
                            </div>
                        </div>
                        <div style="float: left; width: 1%; color: Black;">
                            &nbsp;
                        </div>
                        <div style="float: left; width: 49%; color: Black;">
                            <div style="float: left; width: 30%; color: Black;">
                                Creation Date To
                            </div>
                            <div style="float: left; width: 70%; color: Black;">
                                <asp:TextBox ID="TextBoxCreationDateTo" runat="server" CssClass="TextBox TextBoxUpper"
                                    Enabled="False" Width="108px"></asp:TextBox>
                                <asp:Image ID="Image2" runat="server" ImageUrl="~/Images/Calendar_scheduleHS.png" />
                                <asp:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="TextBoxCreationDateTo"
                                    PopupButtonID="Image2" Enabled="True" Format="dd/MMM/yyyy"  OnClientDateSelectionChanged="LinkClick">
                                </asp:CalendarExtender>
                            </div>
                        </div>
                    </div>
                </asp:Panel>
                <div style="float: left; height: 10%; color: Black;">
                    &nbsp;
                </div>
                <div style="float: left; width: 100%; color: Black;">
                    <asp:Panel ID="PanelGrid" runat="server" GroupingText=" ">

                        <asp:GridView ID="GridViewDetails" runat="server" AutoGenerateColumns="False" CellPadding="4"
                            ForeColor="#333333" CssClass="GridView" EmptyDataText="No data found" ShowHeaderWhenEmpty="True"
                            Width="100%" >
                            <AlternatingRowStyle BackColor="White" />
                            <Columns>
                                <asp:TemplateField HeaderText="Select">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkSelect" runat="server" />
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" Width="35px" />
                                </asp:TemplateField>
                              
                       <asp:TemplateField HeaderText="Account No">
                                    <ItemTemplate>
                                        <asp:Label ID="lblAcc" runat="server" Text='<%# Bind("HPA_ACC_NO") %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" Width="150px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Customer">
                                    <ItemTemplate>
                                        <asp:Label ID="lblCus" runat="server" Text='<%# Bind("MBE_NAME") %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" Width="35px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Address">
                                    <ItemTemplate>
                                        <asp:Label ID="lblAdd" runat="server" Text='<%# Bind("HTC_ADR_01")+" "+Bind("HTC_ADR_02")+" "+Bind("HTC_ADR_03")  %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" Width="300px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Printed Status">
                                    <ItemTemplate>
                                        <asp:Label ID="lblPrints" runat="server" Text='<%# Bind("Print") %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Right" />
                                    <ItemStyle HorizontalAlign="Right" Width="100px" />
                                </asp:TemplateField>
                            </Columns>
                            <EditRowStyle BackColor="#2461BF" />
                            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                            <PagerSettings Mode="Numeric" />
                            <PagerStyle ForeColor="#8C4510" HorizontalAlign="Center" />
                            <RowStyle BackColor="#EFF3FB" />
                            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                            <SortedAscendingCellStyle BackColor="#F5F7FB" />
                            <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                            <SortedDescendingCellStyle BackColor="#E9EBEF" />
                            <SortedDescendingHeaderStyle BackColor="#4870BE" />
                        </asp:GridView>
                       
                    </asp:Panel>
                </div>
                <div style="float: left; height: 5%; color: Black;">
                    &nbsp;
                </div>
                <div style="float: left; width: 100%; color: Black;">
                    <div style="float: left; width: 10%; color: Black;">
                        &nbsp;
                    </div>
                    <div style="float: left; width: 20%; color: Black;">
                        <asp:Button ID="ButtonAll" runat="server" CssClass="Button"  Text="All" 
                            onclick="ButtonAll_Click"/>
                        &nbsp;&nbsp;
                        <asp:Button ID="ButtonNone" runat="server" CssClass="Button" Text="None" 
                            onclick="ButtonNone_Click" />
                    </div>
                    <div style="float: left; width: 10%; color: Black;">
                        &nbsp;
                    </div>
                    <div style="float: left; width: 60%; color: Black;">
                    <asp:RadioButtonList ID="RadioButtonListType" runat="server" >
                    <asp:ListItem  Text="Customer" Value="Customer" Selected="True"></asp:ListItem>
                    <asp:ListItem  Text="Gruantor1" Value="Gruantor1"></asp:ListItem>
                    <asp:ListItem  Text="Gruantor2" Value="Gruantor2"></asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:LinkButton ID="LinkButtonTemp" runat="server" onclick="LinkButtonTemp_Click"></asp:LinkButton>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
