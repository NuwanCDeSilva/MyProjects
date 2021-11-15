<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="RemSumHeading.aspx.cs" Inherits="FF.WebERPClient.Financial_Modules.RemSumHeading" %>

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
    function isNumberKeyAndDot(event, value) {
        var charCode = (event.which) ? event.which : event.keyCode
        var intcount = 0;
        var stramount = value;
        for (var i = 0; i < stramount.length; i++) {
            if (stramount.charAt(i) == '.' && charCode == 46) {
                return false;
            }
        }
        if ((charCode > 31 && (charCode < 48 || charCode > 57)) && charCode != 46)
            return false;
        return true;
    }
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div style="float: left; width: 2%">
        &nbsp;
    </div>
    <div style="float: left; width: 96%">
        <asp:TabContainer ID="tbcApprovalStatus" runat="server" ActiveTabIndex="0" Height="450px">
            <asp:TabPanel ID="tbpHead" runat="server" HeaderText="Remitance Summary Headings Creation">
                <ContentTemplate>
                    <div style="float: left; width: 100%; color: Black;">
                        <div style="float: left; width: 100%;">
                            <div style="height: 22px; text-align: right;" class="PanelHeader">
                                <asp:Button ID="btnSave" runat="server" Text="Save" Height="85%" Width="70px" CssClass="Button"
                                    OnClick="btnSave_Click" />
                                <asp:Button ID="btnClear" runat="server" Text="Clear" Height="85%" Width="70px" CssClass="Button" />
                                <asp:Button ID="btnClose" runat="server" Text="Close" Height="85%" Width="70px" CssClass="Button"
                                    OnClick="btnClose_Click" />
                            </div>
                        </div>
                    </div>
                    <div style="float: left; width: 100%;">
                        <asp:UpdatePanel ID="updtMainPnl" runat="server">
                            <ContentTemplate>
                                <div style="float: left; width: 100%; color: Black;">
                                    <asp:Panel ID="pnl1" runat="server" GroupingText="-- Remitance Summary Headings --"
                                        Font-Size="11px" Width="893px" Font-Names="Tahoma" Height="400px">
                                        <div style="float: left; height: 6px; width: 100%;">
                                        </div>
                                        <div style="float: left; width: 100%;">
                                            <div style="float: left; width: 2%;">
                                                &nbsp;</div>
                                            <div style="float: left; width: 6%">
                                                Section
                                            </div>
                                            <div style="float: left; width: 2%;">
                                                &nbsp;</div>
                                            <div style="float: left; width: 15%">
                                                <asp:DropDownList ID="ddlSec" runat="server" AutoPostBack="true" AppendDataBoundItems="true"
                                                    CssClass="ComboBox" Width="140" OnSelectedIndexChanged="ddlSec_SelectedIndexChanged">
                                                </asp:DropDownList>
                                            </div>
                                            <div style="float: left; width: 50%">
                                                <asp:Label ID="lblSection" runat="server"></asp:Label>
                                            </div>
                                        </div>
                                        <div style="float: left; height: 5px; width: 100%;">
                                        </div>
                                        <div style="float: left; width: 100%;">
                                            <div style="float: left; width: 2%;">
                                                &nbsp;</div>
                                            <div style="float: left; width: 6%">
                                                Code
                                            </div>
                                            <div style="float: left; width: 2%;">
                                                &nbsp;</div>
                                            <div style="float: left; width: 15%">
                                                <asp:TextBox ID="txtCode" runat="server" CssClass="TextBox" ClientIDMode="Static"></asp:TextBox>
                                            </div>
                                            <div style="float: left; width: 7%; height: 13px;">
                                                &nbsp;</div>
                                            <div style="float: left; width: 10%">
                                                Description
                                            </div>
                                            <div style="float: left; width: 32%">
                                                <asp:TextBox ID="txtDesc" runat="server" CssClass="TextBox" Width="279px"></asp:TextBox>
                                            </div>
                                            <div style="float: left; width: 3%; height: 13px;">
                                                &nbsp;</div>
                                            <div style="float: left; width: 10%">
                                                <asp:CheckBox ID="chkStatus" runat="server" Text="Active" AutoPostBack="true" />
                                            </div>
                                        </div>
                                        <div style="float: left; height: 6px; width: 100%;">
                                        </div>
                                        <asp:GridView ID="gvRemSumHead" runat="server" AutoGenerateColumns="False" CellPadding="4"
                                            ForeColor="#333333" CssClass="GridView" EmptyDataText="No data found" ShowHeaderWhenEmpty="True"
                                            Width="100%" AllowPaging="true" PageSize="15" OnPageIndexChanging="gvRemSumHead_PageIndexChanging"
                                            PagerSettings-Mode="NextPreviousFirstLast" PagerSettings-Position="Bottom" PagerStyle-HorizontalAlign="Left"
                                            RowStyle-BackColor="#99CCFF" PagerStyle-ForeColor="White">
                                            <AlternatingRowStyle BackColor="White" />
                                            <Columns>
                                                <asp:TemplateField HeaderText="Remitance Code" HeaderStyle-HorizontalAlign="Left"
                                                    ShowHeader="true">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblRemCode" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "rsd_cd") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                    <ItemStyle HorizontalAlign="Left" Width="150px" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Description" HeaderStyle-HorizontalAlign="Left" ShowHeader="true">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblDesc" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "rsd_desc") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                    <ItemStyle HorizontalAlign="Left" Width="300px" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Fixed" HeaderStyle-HorizontalAlign="Center" ShowHeader="true">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblFixed" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "rsd_fixed") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                    <ItemStyle HorizontalAlign="Center" Width="100px" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Status" HeaderStyle-HorizontalAlign="Center" ShowHeader="true">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblStatus" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "rsd_stus") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                    <ItemStyle HorizontalAlign="Center" Width="100px" />
                                                </asp:TemplateField>
                                            </Columns>
                                            <EditRowStyle BackColor="#2461BF" />
                                            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                            <PagerStyle HorizontalAlign="Right" />
                                            <PagerStyle ForeColor="#8C4510" HorizontalAlign="Center" />
                                            <RowStyle BackColor="#EFF3FB" />
                                            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                            <SortedAscendingCellStyle BackColor="#F5F7FB" />
                                            <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                                            <SortedDescendingCellStyle BackColor="#E9EBEF" />
                                            <SortedDescendingHeaderStyle BackColor="#4870BE" />
                                        </asp:GridView>
                                        <div style="display: none;">
                                            <asp:Button ID="btnCode" runat="server" OnClick="GetRemData" />
                                        </div>
                                    </asp:Panel>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </ContentTemplate>
            </asp:TabPanel>
            <asp:TabPanel ID="tbpDef" runat="server" HeaderText="Remitance Summary Limitations">
                <ContentTemplate>
                    <div style="float: left; width: 100%; color: Black;">
                        <div style="float: left; width: 100%;">
                            <div style="height: 22px; text-align: right;" class="PanelHeader">
                                <asp:Button ID="btnSaveLimit" runat="server" Text="Save" Height="85%" Width="70px"
                                    CssClass="Button" OnClick="btnSaveLimit_Click" />
                                <asp:Button ID="Button2" runat="server" Text="Clear" Height="85%" Width="70px" CssClass="Button" />
                                <asp:Button ID="btnCloseLimit" runat="server" Text="Close" Height="85%" Width="70px"
                                    CssClass="Button" OnClick="btnCloseLimit_Click" />
                            </div>
                        </div>
                    </div>
                    <div style="float: left; width: 100%;">
                        <asp:UpdatePanel ID="updpnlLimit" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <div style="float: left; width: 100%; color: Black;">
                                    <asp:Panel ID="Panel1" runat="server" GroupingText="-- Remitance Summary Limitations --"
                                        Font-Size="11px" Width="893px" Font-Names="Tahoma" Height="400px">
                                        <div style="float: left; height: 6px; width: 100%;">
                                        </div>
                                        <div style="float: left; width: 100%;">
                                            <div style="float: left; width: 2%;">
                                                &nbsp;</div>
                                            <div style="float: left; width: 10%">
                                                Party Type
                                            </div>
                                            <div style="float: left; width: 2%;">
                                                &nbsp;</div>
                                            <div style="float: left; width: 65%">
                                                <asp:DropDownList ID="ddlPtyTp" runat="server" AutoPostBack="true" AppendDataBoundItems="true"
                                                    CssClass="ComboBox" Width="140" OnSelectedIndexChanged="ddlPtyTp_SelectedIndexChanged">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div style="float: left; height: 6px; width: 100%;">
                                        </div>
                                        <div style="float: left; width: 100%;">
                                            <div style="float: left; width: 2%;">
                                                &nbsp;</div>
                                            <div style="float: left; width: 10%">
                                                Party Code
                                            </div>
                                            <div style="float: left; width: 2%;">
                                                &nbsp;</div>
                                            <div style="float: left; width: 65%">
                                                <asp:DropDownList ID="ddlPtyCd" runat="server" AutoPostBack="true" AppendDataBoundItems="true"
                                                    CssClass="ComboBox" Width="140" OnSelectedIndexChanged="ddlPtyCd_SelectedIndexChanged">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div style="float: left; height: 6px; width: 100%;">
                                        </div>
                                        <div style="float: left; width: 100%;">
                                            <div style="float: left; width: 2%;">
                                                &nbsp;</div>
                                            <div style="float: left; width: 10%">
                                                Section
                                            </div>
                                            <div style="float: left; width: 2%;">
                                                &nbsp;</div>
                                            <div style="float: left; width: 15%">
                                                <asp:DropDownList ID="ddlSecDef" runat="server" AutoPostBack="true" AppendDataBoundItems="true"
                                                    CssClass="ComboBox" Width="140" OnSelectedIndexChanged="ddlSecDef_SelectedIndexChanged">
                                                </asp:DropDownList>
                                            </div>
                                            <div style="float: left; width: 6%;">
                                                &nbsp;</div>
                                            <div style="float: left; width: 10%">
                                                Remitance Type
                                            </div>
                                            <div style="float: left; width: 2%;">
                                                &nbsp;</div>
                                            <div style="float: left; width: 15%">
                                                <asp:DropDownList ID="ddlRemTp" runat="server" AutoPostBack="true" AppendDataBoundItems="true"
                                                    CssClass="ComboBox" Width="240" OnSelectedIndexChanged="ddlRemTp_SelectedIndexChanged">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div style="float: left; height: 6px; width: 100%;">
                                        </div>
                                        <div style="float: left; width: 100%;">
                                            <div style="float: left; width: 2%;">
                                                &nbsp;</div>
                                            <div style="float: left; width: 10%">
                                                From Date
                                            </div>
                                            <div style="float: left; width: 2%;">
                                                &nbsp;</div>
                                            <div style="float: left; width: 15%">
                                                <asp:TextBox ID="txtFrom" runat="server" CssClass="TextBox" Width="90px"   AutoPostBack="false"></asp:TextBox>
                                                <asp:Image ID="imgFromDate" runat="server" ImageUrl="~/Images/Calendar_scheduleHS.png" />
                                                <asp:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtFrom"
                                                    Format="dd/MMM/yyyy" PopupButtonID="imgFromDate" Enabled="True">
                                                </asp:CalendarExtender>
                                            </div>
                                            <div style="float: left; width: 6%;">
                                                &nbsp;</div>
                                            <div style="float: left; width: 10%">
                                                To Date
                                            </div>
                                            <div style="float: left; width: 2%;">
                                                &nbsp;</div>
                                            <div style="float: left; width: 15%">
                                                <asp:TextBox ID="txtTo" runat="server" CssClass="TextBox" Width="90px"  AutoPostBack="false"></asp:TextBox>
                                                <asp:Image ID="imgToDate" runat="server" ImageUrl="~/Images/Calendar_scheduleHS.png" />
                                                <asp:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtTo"
                                                    Format="dd/MMM/yyyy" PopupButtonID="imgToDate" Enabled="True">
                                                </asp:CalendarExtender>
                                            </div>
                                        </div>
                                        <div style="float: left; height: 6px; width: 100%;">
                                        </div>
                                        <div style="float: left; width: 100%;">
                                            <div style="float: left; width: 2%;">
                                                &nbsp;</div>
                                            <div style="float: left; width: 10%">
                                                Value
                                            </div>
                                            <div style="float: left; width: 2%;">
                                                &nbsp;</div>
                                            <div style="float: left; width: 65%">
                                                <asp:TextBox ID="txtVal" runat="server" CssClass="TextBox" ClientIDMode="Static"  Style="text-align: right" onKeyPress="return isNumberKeyAndDot(event,false)"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div style="float: left; height: 6px; width: 100%;">
                                        </div>
                                        <asp:GridView ID="gvRemLimit" runat="server" AutoGenerateColumns="False" CellPadding="4"
                                            ForeColor="#333333" CssClass="GridView" EmptyDataText="No data found" ShowHeaderWhenEmpty="True"
                                            Width="100%" AllowPaging="true" PageSize="11" OnPageIndexChanging="gvRemLimit_PageIndexChanging"
                                            PagerSettings-Mode="NextPreviousFirstLast" PagerSettings-Position="Bottom" PagerStyle-HorizontalAlign="Left"
                                            RowStyle-BackColor="#99CCFF" PagerStyle-ForeColor="White">
                                            <AlternatingRowStyle BackColor="White" />
                                            <Columns>
                                                <asp:TemplateField HeaderText="From Date" HeaderStyle-HorizontalAlign="Left" ShowHeader="true">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblFrom" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "rsmd_from_dt") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                    <ItemStyle HorizontalAlign="Right" Width="150px" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="To Date" HeaderStyle-HorizontalAlign="Left" ShowHeader="true">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblTo" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "rsmd_to_dt") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                    <ItemStyle HorizontalAlign="Right" Width="150px" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Limit" HeaderStyle-HorizontalAlign="Center" ShowHeader="true">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblLimit" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "rsmd_max_val") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                    <ItemStyle HorizontalAlign="Right" Width="150px" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Created By" HeaderStyle-HorizontalAlign="Center" ShowHeader="true">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCre" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "rsmd_cre_by") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                    <ItemStyle HorizontalAlign="Center" Width="200px" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Created Date" HeaderStyle-HorizontalAlign="Center"
                                                    ShowHeader="true">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCreDt" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "rsmd_cre_dt") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                    <ItemStyle HorizontalAlign="Center" Width="100px" />
                                                </asp:TemplateField>
                                            </Columns>
                                            <EditRowStyle BackColor="#2461BF" />
                                            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                            <PagerStyle HorizontalAlign="Right" />
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
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </ContentTemplate>
            </asp:TabPanel>
        </asp:TabContainer>
    </div>
    <div style="float: left; width: 2%">
        &nbsp;
    </div>
</asp:Content>
