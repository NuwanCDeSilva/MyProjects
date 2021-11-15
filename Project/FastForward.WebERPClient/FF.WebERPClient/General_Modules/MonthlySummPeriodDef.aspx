<%@ Page Title="Monthly Summary Period Definition" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="MonthlySummPeriodDef.aspx.cs" Inherits="FF.WebERPClient.General_Modules.MonthlySummPeriodDef" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="../UserControls/uc_ProfitCenterSearch.ascx" TagName="uc_ProfitCenterSearch"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">
        function fun1(e, button2) {
            var evt = e ? e : window.event;
            var bt = document.getElementById(button2);
            if (bt) {
                if (evt.keyCode == 13) {
                    bt.click();
                    return false;
                }
            }
        }
        function onblurFire(e, buttonid) {
            var evt = e ? e : window.event;
            var bt = document.getElementById(buttonid);
            if (bt) {

                bt.click();
                return false;


            }
        }

    </script>
    <asp:UpdatePanel ID="updtMainPnl" runat="server">
        <ContentTemplate>
            <div style="display: none">
                <asp:Button ID="btnGetDocDet" runat="server" Text="Button" OnClick="btnGetDocDet_Click" />
            </div>
            <div id="divMain" style="color: Black; text-align: right;">
                <asp:Button ID="btnClear" runat="server" Text="Clear" CssClass="Button" 
                    onclick="btnClear_Click" />
                &nbsp;
                <asp:Button ID="btnClose" runat="server" Text="Close" CssClass="Button" 
                    onclick="btnClose_Click" />
            </div>
            <div style="float: left; width: 100%;">
                <asp:Panel ID="Panel1" runat="server">
                    <div style="float: left; width: 100%;">
                        <asp:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="0" Width="100%"
                            Height="600px">
                            <%-- ------TAB------SOS Date Definition--------------%>
                            <asp:TabPanel ID="TabPanel1" runat="server" HeaderText="SOS Date Definition">
                                <ContentTemplate>
                                    <asp:Panel ID="Panel4" runat="server" CssClass="PanelHeader">
                                    </asp:Panel>
                                    <div style="padding: 1.0px; float: left; width: 100%; text-align: right;">
                                        <asp:Button ID="btnSaveSoS" runat="server" Text="Save" CssClass="Button" OnClick="btnSaveSoS_Click" />
                                    </div>
                                    <div style="float: left; width: 100%; font-size: smaller;">
                                        <div style="float: left; width: 4%;">
                                            &nbsp;&nbsp;</div>
                                        <div style="float: left; width: 25%;">
                                            <asp:Panel ID="Panel5" runat="server" CssClass="PanelHeader" BackColor="#6699FF"
                                                Font-Bold="True" ForeColor="Black">
                                                Week selection
                                            </asp:Panel>
                                            <asp:Panel ID="Panel2" runat="server" BorderColor="#99CCFF" BorderWidth="1px" Height="143px">
                                                <div style="padding: 2.5px; float: left; width: 100%;">
                                                    &#160;</div>
                                                <div style="padding: 2.5px; float: left; width: 100%;">
                                                    <div style="float: left; width: 30%;">
                                                        <asp:Label ID="Label2" runat="server" Text="Month/Year" Font-Bold="True"></asp:Label></div>
                                                    <div style="float: left; width: 69%;">
                                                        <asp:TextBox ID="txtMonthYear_" runat="server" AutoPostBack="True" CssClass="TextBox"
                                                            ValidationGroup="pc" Width="90px" OnTextChanged="txtMonthYear__TextChanged"></asp:TextBox><asp:CalendarExtender
                                                                ID="CalendarExtender_" runat="server" Enabled="True" Format="MM/yyyy" TargetControlID="txtMonthYear_">
                                                            </asp:CalendarExtender>
                                                    </div>
                                                </div>
                                                <div style="padding: 2.5px; float: left; width: 100%;">
                                                    <div style="float: left; width: 30%;">
                                                        <asp:Label ID="Label3" runat="server" Text="Week" Font-Bold="True"></asp:Label></div>
                                                    <div style="float: left; width: 69%;">
                                                        <asp:DropDownList ID="ddlWeek" runat="server" AutoPostBack="True" CssClass="ComboBox"
                                                            Width="93px" ValidationGroup="pc">
                                                            <asp:ListItem>1</asp:ListItem>
                                                            <asp:ListItem>2</asp:ListItem>
                                                            <asp:ListItem>3</asp:ListItem>
                                                            <asp:ListItem>4</asp:ListItem>
                                                            <asp:ListItem>5</asp:ListItem>
                                                            <asp:ListItem>6</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                                <div style="padding: 2px; float: left; width: 100%;">
                                                    <div style="float: left; width: 15%;">
                                                        <asp:Label ID="Label4" runat="server" Text="From: " Font-Bold="True"></asp:Label></div>
                                                    <div style="float: left; width: 30%;">
                                                        <asp:TextBox ID="txtFrom" runat="server" CssClass="TextBox" Width="100%" ForeColor="Blue"></asp:TextBox><asp:CalendarExtender
                                                            ID="CalendarExtendertxtFromDt" runat="server" Enabled="True" Format="dd/MM/yyyy"
                                                            TargetControlID="txtFrom">
                                                        </asp:CalendarExtender>
                                                    </div>
                                                    <div style="float: left; width: 2%;">
                                                        &#160;</div>
                                                    <div style="float: left; width: 10%;">
                                                        <asp:Label ID="Label5" runat="server" Text="To: " Font-Bold="True"></asp:Label></div>
                                                    <div style="float: left; width: 30%;">
                                                        <asp:TextBox ID="txtTo" runat="server" CssClass="TextBox" Width="100%" ForeColor="Blue"></asp:TextBox><asp:CalendarExtender
                                                            ID="CalendarExtender1" runat="server" Enabled="True" Format="dd/MM/yyyy" TargetControlID="txtTo">
                                                        </asp:CalendarExtender>
                                                    </div>
                                                </div>
                                                <div style="padding: 2.5px; float: left; width: 100%;">
                                                    <div style="padding: 1.0px; float: left; width: 100%;">
                                                    </div>
                                                </div>
                                            </asp:Panel>
                                        </div>
                                        <div style="float: left; width: 10%; text-align: center;">
                                            <asp:Button ID="btnAdd" runat="server" Text="Add >>" CssClass="Button" OnClick="Button1_Click" /></div>
                                        <div style="float: left; width: 60%;">
                                            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                                <ContentTemplate>
                                                    <asp:Panel ID="Panel3" runat="server">
                                                        <asp:GridView ID="grvSoSDef" runat="server" CellPadding="4" CssClass="GridView" ShowHeaderWhenEmpty="True"
                                                            ForeColor="#333333" AutoGenerateColumns="False" Width="60%" OnRowDeleting="grvSoSDef_RowDeleting"
                                                            BorderColor="#6699FF" BorderWidth="1px">
                                                            <AlternatingRowStyle BackColor="White" />
                                                            <Columns>
                                                                <asp:TemplateField ShowHeader="False">
                                                                    <ItemTemplate>
                                                                        <asp:ImageButton ID="ImageButton1" runat="server" CommandName="Delete" ImageUrl="~/Images/Delete.png" /></ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:BoundField HeaderText="Week" DataField="Gw_week" />
                                                                <asp:BoundField HeaderText="From Date" DataField="Gw_from_dt" DataFormatString="{0:d}" />
                                                                <asp:BoundField HeaderText="To Date" DataField="Gw_to_dt" DataFormatString="{0:d}" />
                                                            </Columns>
                                                            <EditRowStyle BackColor="#2461BF" />
                                                            <EmptyDataTemplate>
                                                                <div style="width: 100%; text-align: center;">
                                                                    No data found
                                                                </div>
                                                            </EmptyDataTemplate>
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
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                    </div>
                                </ContentTemplate>
                            </asp:TabPanel>
                            <%-- -------TAB-----Grace Period Definition--------------%>
                            <asp:TabPanel ID="TabPanel2" runat="server" HeaderText="Grace Period Definition">
                                <ContentTemplate>
                                    <div style="padding: 4.0px; float: left; width: 100%; text-align: right;">
                                        <asp:Panel ID="Panel6" runat="server" CssClass="PanelHeader">
                                        </asp:Panel>
                                    </div>
                                    <div style="float: left; width: 100%; font-size: smaller;">
                                        <div style="float: left; width: 1%;">
                                            &nbsp;&nbsp;</div>
                                        <div style="float: left; width: 25%;">
                                            <asp:Panel ID="Panel7" runat="server" CssClass="PanelHeader" BackColor="#6699FF"
                                                Font-Bold="True" ForeColor="Black">
                                                Dates selection
                                            </asp:Panel>
                                            <asp:Panel ID="Panel8" runat="server" BorderColor="#99CCFF" BorderWidth="1px" Height="143px">
                                                <div style="padding: 2.5px; float: left; width: 100%;">
                                                    &#160;</div>
                                                <div style="padding: 2.5px; float: left; width: 100%;">
                                                    <div style="float: left; width: 30%;">
                                                        <asp:Label ID="Label1" runat="server" Text="Month/Year" Font-Bold="True"></asp:Label></div>
                                                    <div style="float: left; width: 69%; text-align: center;">
                                                        <asp:TextBox ID="txtMonthYear_gc" runat="server" AutoPostBack="True" CssClass="TextBox"
                                                            ValidationGroup="pc" Width="90px" OnTextChanged="txtMonthYear_gc_TextChanged"></asp:TextBox><asp:CalendarExtender
                                                                ID="CalendarExtender2" runat="server" Enabled="True" Format="MM/yyyy" TargetControlID="txtMonthYear_gc">
                                                            </asp:CalendarExtender>
                                                    </div>
                                                </div>
                                                <div style="padding: 2.5px; float: left; width: 100%;">
                                                    <div style="float: left; width: 30%;">
                                                        <asp:Label ID="Label6" runat="server" Text="As at Date" Font-Bold="True"></asp:Label></div>
                                                    <div style="float: left; width: 69%; text-align: center;">
                                                        <asp:TextBox ID="txtAsAtDt" runat="server" CssClass="TextBox" Width="90px" Enabled="False"></asp:TextBox><asp:CalendarExtender
                                                            ID="CalendarExtender3" runat="server" Enabled="True" Format="dd/MM/yyyy" TargetControlID="txtAsAtDt">
                                                        </asp:CalendarExtender>
                                                    </div>
                                                </div>
                                                <div style="padding: 2.5px; float: left; width: 100%;">
                                                    <div style="float: left; width: 30%;">
                                                        <asp:Label ID="Label7" runat="server" Text="Supp. Date" Font-Bold="True"></asp:Label></div>
                                                    <div style="float: left; width: 69%; text-align: center;">
                                                        <asp:TextBox ID="txtSuppDt" runat="server" CssClass="TextBox" Width="90px"></asp:TextBox><asp:CalendarExtender
                                                            ID="CalendarExtender4" runat="server" Enabled="True" Format="dd/MM/yyyy" TargetControlID="txtSuppDt">
                                                        </asp:CalendarExtender>
                                                    </div>
                                                </div>
                                                <div style="padding: 2.5px; float: left; width: 100%;">
                                                    <div style="float: left; width: 30%;">
                                                        <asp:Label ID="Label8" runat="server" Text="Grace Date" Font-Bold="True"></asp:Label></div>
                                                    <div style="float: left; width: 69%; text-align: center;">
                                                        <asp:TextBox ID="txtGraceDt" runat="server" CssClass="TextBox" Width="90px"></asp:TextBox><asp:CalendarExtender
                                                            ID="CalendarExtender5" runat="server" Enabled="True" Format="dd/MM/yyyy" TargetControlID="txtGraceDt">
                                                        </asp:CalendarExtender>
                                                    </div>
                                                </div>
                                            </asp:Panel>
                                        </div>
                                        <div style="float: left; width: 1%;">
                                            &nbsp;</div>
                                        <div style="float: left; width: 12%;">
                                            <asp:Panel ID="Panel9" runat="server" CssClass="PanelHeader" BackColor="#6699FF"
                                                Font-Bold="True" ForeColor="Black">
                                                Party Type
                                            </asp:Panel>
                                            <div style="float: left; width: 100%;">
                                                <asp:RadioButton ID="rdoCom" runat="server" Text="COMPANY" GroupName="party" AutoPostBack="True"
                                                    OnCheckedChanged="rdoCom_CheckedChanged" /></div>
                                            <div style="float: left; width: 100%;">
                                                <asp:RadioButton ID="rdoCannel" runat="server" Text="CHANNEL" GroupName="party" AutoPostBack="True"
                                                    OnCheckedChanged="rdoCannel_CheckedChanged" /></div>
                                            <div style="float: left; width: 100%;">
                                                <asp:RadioButton ID="rdoSubChannel" runat="server" Text="SUB CHANNEL" GroupName="party"
                                                    AutoPostBack="True" OnCheckedChanged="rdoSubChannel_CheckedChanged" /></div>
                                            <div style="float: left; width: 100%;">
                                                <asp:RadioButton ID="rdoArea" runat="server" Text="AREA" GroupName="party" AutoPostBack="True"
                                                    OnCheckedChanged="rdoArea_CheckedChanged" /></div>
                                            <div style="float: left; width: 100%;">
                                                <asp:RadioButton ID="rdoRegion" runat="server" Text="REGION" GroupName="party" AutoPostBack="True"
                                                    OnCheckedChanged="rdoRegion_CheckedChanged" /></div>
                                            <div style="float: left; width: 100%;">
                                                <asp:RadioButton ID="rdoZone" runat="server" Text="Zone" GroupName="party" AutoPostBack="True"
                                                    OnCheckedChanged="rdoZone_CheckedChanged" /></div>
                                            <div style="float: left; width: 100%;">
                                                <asp:RadioButton ID="rdoPc" runat="server" Text="PROFIT CENTER" GroupName="party"
                                                    AutoPostBack="True" OnCheckedChanged="rdoPc_CheckedChanged" /></div>
                                        </div>
                                        <div style="float: left; width: 1%;">
                                            &nbsp;</div>
                                        <div style="float: left; width: 55%;">
                                            <asp:Panel ID="Panel10" runat="server" CssClass="PanelHeader" BackColor="#6699FF"
                                                Font-Bold="True" ForeColor="Black">
                                                Party Code
                                            </asp:Panel>
                                            <uc1:uc_ProfitCenterSearch ID="uc_ProfitCenterSearch1" runat="server" />
                                        </div>
                                        <div style="float: left; width: 4%;">
                                            <asp:ImageButton ID="ImgBtnAdd" runat="server" ImageUrl="~/Images/download_arrow_icon.png"
                                                Height="22px" Width="22px" OnClick="ImgBtnAdd_Click" />
                                        </div>
                                    </div>
                                    <div style="padding: 5.0px; float: left; width: 100%;">
                                        &nbsp;
                                    </div>
                                    <div style="float: left; width: 100%; font-size: smaller;">
                                        <div style="float: left; width: 1%;">
                                            &nbsp;
                                        </div>
                                        <asp:Panel ID="Panel12" runat="server" CssClass="PanelHeader">
                                        </asp:Panel>
                                        <div style="padding: 0.5px; float: left; width: 100%; text-align: right;">
                                            <asp:Button ID="btnSaveGrace" runat="server" Text="Save" CssClass="Button" OnClick="btnSaveGrace_Click" /></div>
                                        <div style="float: left; width: 60%;">
                                            <asp:Panel ID="Panel11" runat="server" BackColor="White" ScrollBars="Vertical" BorderColor="#99CCFF"
                                                Height="295px">
                                                <asp:GridView ID="grvGrace" runat="server" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True"
                                                    CellPadding="4" CssClass="GridView" ForeColor="#333333" Width="100%" OnRowDeleting="grvGrace_RowDeleting">
                                                    <AlternatingRowStyle BackColor="White" />
                                                    <EmptyDataTemplate>
                                                        <div style="width: 100%; text-align: center;">
                                                            No data found
                                                        </div>
                                                    </EmptyDataTemplate>
                                                    <Columns>
                                                        <asp:TemplateField>
                                                            <EditItemTemplate>
                                                                <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                                                            </EditItemTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="Label1" runat="server"></asp:Label>
                                                                <asp:ImageButton ID="ImgBtnDelGrc" runat="server" CommandName="Delete" ImageUrl="~/Images/Delete.png" />
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Center" />
                                                        </asp:TemplateField>
                                                        <asp:BoundField HeaderText="Party Type" DataField="Hadd_pty_tp">
                                                            <ItemStyle HorizontalAlign="Center" />
                                                        </asp:BoundField>
                                                        <asp:BoundField HeaderText="Party" DataField="Hadd_pty_cd">
                                                            <ItemStyle HorizontalAlign="Center" />
                                                        </asp:BoundField>
                                                        <asp:BoundField HeaderText="Arears Date" DataField="Hadd_ars_dt" DataFormatString="{0:d}">
                                                            <ItemStyle HorizontalAlign="Center" />
                                                        </asp:BoundField>
                                                        <asp:BoundField HeaderText="Supplimant Date" DataField="Hadd_sup_dt" DataFormatString="{0:d}">
                                                            <ItemStyle HorizontalAlign="Center" />
                                                        </asp:BoundField>
                                                        <asp:BoundField HeaderText="Grace Date" DataField="Hadd_grc_dt" DataFormatString="{0:d}">
                                                            <ItemStyle HorizontalAlign="Center" />
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
                                    </div>
                                </ContentTemplate>
                            </asp:TabPanel>
                        </asp:TabContainer>
                    </div>
                </asp:Panel>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
