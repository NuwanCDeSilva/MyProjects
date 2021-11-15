<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" EnableEventValidation="false"
    CodeBehind="DayEndProcess.aspx.cs" Inherits="FF.WebERPClient.Financial_Modules.DayEndProcess" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/UserControls/uc_MsgInfo.ascx" TagName="uc_MsgInfo" TagPrefix="uc1" %>
<%@ Register Src="../UserControls/uc_CommonSearch.ascx" TagName="uc_CommonSearch"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script type="text/javascript">
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
        function ProcessConfirm() {
            if (confirm("Are you sure ?")) {
                return true;
            }
            else {
                return false;
            }
        }
        function DeleteConfirm() {
            if (confirm("Are you sure to delete?")) {
                return true;
            }
            else {
                return false;
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="pnlButtons" runat="server">
        <ContentTemplate>
            <div style="float: left; width: 100%;">
                <div style="float: left; width: 99%; height: 22px; text-align: right; background-color: #1E4A9F">
                    <div style="float: left;">
                        <asp:Label ID="lblDispalyInfor" runat="server" CssClass="Label" ForeColor="Yellow"></asp:Label>
                    </div>
                    <div style="float: right;">
                        <%--        <asp:Button Text="Confirm" ID="btnConfirm" runat="server" CssClass="Button" Enabled="false"
                            OnClick="btnConfirm_Click" OnClientClick="return ProcessConfirm()" />
                        &nbsp;--%>
                        <asp:Button Text="Process" ID="btnProcess" runat="server" CssClass="Button" OnClick="btnProcess_Click"
                            OnClientClick="return ProcessConfirm()" />
                        &nbsp;
                        <asp:Button Text="Clear" ID="btnClear" runat="server" CssClass="Button" OnClick="btnClear_Click" />
                        &nbsp;
                        <asp:Button Text="Close" ID="btnClose" runat="server" CssClass="Button" OnClick="btnClose_Click" />
                        &nbsp;
                    </div>
                </div>
            </div>
            <%--            ******************************************************************************---%>
            <div style="float: left; width: 100%; padding-top: 2px; padding-bottom: 2px; background-color: #CCE6FF;">
                <div class="CollapsiblePanelHeader">
                    Remittance Summary Details Entry</div>
                <div style="float: left;">
                    <asp:Image runat="server" ID="Image4" ImageUrl="~/Images/16 X 16 DownArrow.jpg" ImageAlign="Right" /></div>
                <asp:CollapsiblePanelExtender ID="CPEPayment" runat="server" TargetControlID="pnlRemDet"
                    CollapsedSize="0" ExpandedSize="140" Collapsed="True" ExpandControlID="Image4"
                    CollapseControlID="Image4" AutoCollapse="False" AutoExpand="False" ScrollContents="false"
                    ExpandDirection="Vertical" ImageControlID="Image4" ExpandedImage="~/Images/16 X 16 UpArrow.jpg"
                    CollapsedImage="~/Images/16 X 16 DownArrow.jpg">
                </asp:CollapsiblePanelExtender>
                <div style="float: left; width: 100%; padding-top: 3px">
                    <asp:Panel ID="pnlRemDet" runat="server" Height="171px">
                        <div style="float: left; width: 100%; padding-top: 3px">
                            <div style="float: left; width: 45%;">
                                <div style="float: left; width: 100%;">
                                    <div style="float: left; width: 2%;">
                                        &nbsp;</div>
                                    <div style="float: left; width: 24%">
                                        Section
                                    </div>
                                    <div style="float: left; width: 1%;">
                                        &nbsp;</div>
                                    <div style="float: left; width: 15%">
                                        <asp:DropDownList ID="ddlSecDef" runat="server" AutoPostBack="true" AppendDataBoundItems="true"
                                            CssClass="ComboBox" Width="140" OnSelectedIndexChanged="ddlSecDef_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div style="float: left; height: 6px; width: 100%;">
                                </div>
                                <div style="float: left; width: 100%;">
                                    <div style="float: left; width: 2%;">
                                        &nbsp;</div>
                                    <div style="float: left; width: 24%">
                                        Remitance Type
                                    </div>
                                    <div style="float: left; width: 1%;">
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
                                    <div style="float: left; width: 24%">
                                        Amount
                                    </div>
                                    <div style="float: left; width: 1%;">
                                        &nbsp;</div>
                                    <div style="float: left; width: 23%">
                                        <asp:TextBox ID="txtVal" runat="server" CssClass="TextBox" ClientIDMode="Static"
                                            Style="text-align: right" 
                                            onKeyPress="return isNumberKeyAndDot(event,value)" Width="84px"></asp:TextBox>
                                    </div>
                                    <div style="float: left; width: 2%;">
                                        &nbsp;</div>
                                    <div style="float: left; width: 16%">
                                        Voucher No
                                    </div>
                                    <div style="float: left; width: 1%;">
                                        &nbsp;</div>
                                    <div style="float: left; width: 23%">
                                        <asp:TextBox ID="txtVoucher" runat="server" CssClass="TextBox" ClientIDMode="Static" Enabled="false"
                                            Style="text-align: left" Width="103px" ></asp:TextBox>
                                    </div>
                                </div>
                                <div style="float: left; height: 6px; width: 100%;">
                                </div>
                                <div style="float: left; width: 100%;">
                                    <div style="float: left; width: 2%;">
                                        &nbsp;</div>
                                    <div style="float: left; width: 24%">
                                        Remarks
                                    </div>
                                    <div style="float: left; width: 1%;">
                                        &nbsp;</div>
                                    <div style="float: left; width: 58%">
                                        <asp:TextBox ID="txtRem" runat="server" CssClass="TextBox" ClientIDMode="Static"
                                            MaxLength="100" Width="285px" Style="text-align: left"></asp:TextBox>
                                    </div>
                                </div>
                                <div style="float: left; height: 6px; width: 100%;">
                                </div>
                                <div style="float: left; width: 100%;">
                                    <div style="float: left; width: 2%;">
                                        &nbsp;</div>
                                    <div style="float: left; width: 24%">
                                        Gross Amount
                                    </div>
                                    <div style="float: left; width: 1%;">
                                        &nbsp;</div>
                                    <div style="float: left; width: 23%">
                                        <asp:TextBox ID="txtGross" runat="server" CssClass="TextBox" ClientIDMode="Static"
                                            Enabled="false" Text="0" Style="text-align: right" onKeyPress="return isNumberKeyAndDot(event,value)"
                                            Width="88px"></asp:TextBox>
                                    </div>
                                    <div style="float: left; width: 2%;">
                                        &nbsp;</div>
                                    <div style="float: left; width: 16%; height: 13px;">
                                        Additions
                                    </div>
                                    <div style="float: left; width: 1%;">
                                        &nbsp;</div>
                                    <div style="float: left; width: 23%">
                                        <asp:TextBox ID="txtAdd" runat="server" CssClass="TextBox" ClientIDMode="Static"
                                            Enabled="false" Text="0" Style="text-align: right" onKeyPress="return isNumberKeyAndDot(event,value)"
                                            Width="103px"></asp:TextBox>
                                    </div>
                                </div>
                                <div style="float: left; height: 6px; width: 100%;">
                                </div>
                                <div style="float: left; width: 100%;">
                                    <div style="float: left; width: 2%;">
                                        &nbsp;</div>
                                    <div style="float: left; width: 24%">
                                        Deductions
                                    </div>
                                    <div style="float: left; width: 1%;">
                                        &nbsp;</div>
                                    <div style="float: left; width: 23%">
                                        <asp:TextBox ID="txtDeduct" runat="server" CssClass="TextBox" ClientIDMode="Static"
                                            Enabled="false" Text="0" Style="text-align: right" onKeyPress="return isNumberKeyAndDot(event,value)"
                                            Width="88px"></asp:TextBox>
                                    </div>
                                    <div style="float: left; width: 2%;">
                                        &nbsp;</div>
                                    <div style="float: left; width: 16%; height: 25px;">
                                        Net Amount
                                    </div>
                                    <div style="float: left; width: 1%;">
                                        &nbsp;</div>
                                    <div style="float: left; width: 18%">
                                        <asp:TextBox ID="txtNet" runat="server" CssClass="TextBox" ClientIDMode="Static"
                                            Enabled="false" Text="0" Style="text-align: right" onKeyPress="return isNumberKeyAndDot(event,value)"
                                            Width="90px"></asp:TextBox>
                                    </div>
                                    <div style="float: left; width: 5%;">
                                        &nbsp;</div>
                                    <div style="float: left; height: 8px; width: 5%;">
                                        <asp:ImageButton ID="btnAddItemNew" runat="server" ImageUrl="~/Images/download_arrow_icon.png"
                                            OnClick="btnAdd_Click" Height="16px" Width="16px" ToolTip="Add Item" />
                                    </div>
                                </div>
                                <div style="display: none;">
                                    <asp:Button ID="btnVal" runat="server" OnClick="LoadGrossBonus" />
                                    <asp:Button ID="btnCalc" runat="server" OnClick="CalBonusNet" />
                                    <asp:Button ID="btnDate" runat="server" OnClick="Date_change" />
                                </div>
                            </div>
                            <div style="float: left; width: 54%;">
                                <div class="commonPageCss" style="float: left; width: 100%">
                                    <asp:Panel ID="Panel8" runat="server" ScrollBars="Vertical" BorderColor="#9F9F9F"
                                        BorderWidth="1px" Font-Bold="true" Height="125px" Width="100%">
                                        <asp:GridView ID="gvRemLimit" runat="server" AutoGenerateColumns="False" CellPadding="4"
                                            OnRowCommand="gvRemLimit_RowCommand" ForeColor="#333333" CssClass="GridView"
                                            EmptyDataText="No data found" ShowHeaderWhenEmpty="True" Width="100%" PagerStyle-HorizontalAlign="Left"
                                            RowStyle-BackColor="#99CCFF" PagerStyle-ForeColor="White">
                                            <AlternatingRowStyle BackColor="White" />
                                            <Columns>
                                                <asp:TemplateField HeaderText="Section" HeaderStyle-HorizontalAlign="Left" ShowHeader="true" HeaderStyle-Width="100px">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSec" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "REM_SEC") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                    <ItemStyle HorizontalAlign="Left" Width="100px" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="code" HeaderStyle-HorizontalAlign="Left" ShowHeader="true"
                                                    Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCode" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "REM_CD") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                    <ItemStyle HorizontalAlign="Left" Width="100px" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Description" HeaderStyle-HorizontalAlign="Left" ShowHeader="true">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblDesc" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "RemSumDet.RSD_DESC") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                    <ItemStyle HorizontalAlign="Left" Width="300px" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Date" HeaderStyle-HorizontalAlign="Center" ShowHeader="true">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblDate" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "REM_DT","{0:dd/MM/yyyy}") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                    <ItemStyle HorizontalAlign="Right" Width="100px" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Amount" HeaderStyle-HorizontalAlign="Right" ShowHeader="true">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCre" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "REM_VAL","{0:f}") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Right" />
                                                    <ItemStyle HorizontalAlign="Right" Width="150px" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Remarks" HeaderStyle-HorizontalAlign="Center" ShowHeader="true">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblRem" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "REM_RMK") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                    <ItemStyle HorizontalAlign="Left" Width="300px" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Delete">
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="imgbtndelAllSerial" runat="server" ImageUrl="~/Images/Delete.png"
                                                            CommandName="DeleteItem" OnClientClick="return DeleteConfirm()" />
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" />
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
                                        <%--             <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" Font-Bold="False"
                                                DataKeyNames="rem_cd,REM_SH_DESC,rem_val1" CssClass="GridView" Width="100%" CellPadding="4"
                                                EmptyDataText="No data found" ForeColor="#333333" ShowHeaderWhenEmpty="True">
                                                <EditRowStyle BackColor="#2461BF" />
                                                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                                <HeaderStyle BackColor="#507CD1" ForeColor="White" Font-Size="12px" Font-Bold="True" />
                                                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                                <RowStyle Font-Size="12px" BackColor="#EFF3FB" />
                                                <AlternatingRowStyle BackColor="White" />
                                                <Columns>
                                                    <asp:BoundField DataField='rem_cd' HeaderText='Code' HeaderStyle-Width="170px" HeaderStyle-HorizontalAlign="Left">
                                                        <HeaderStyle HorizontalAlign="Left" Width="60px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField='rem_desc' HeaderText='Description' HeaderStyle-Width="170px"
                                                        HeaderStyle-HorizontalAlign="Left">
                                                        <HeaderStyle HorizontalAlign="Left" Width="200px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField='rem_val1' HeaderText='Value' HeaderStyle-Width="170px"
                                                        DataFormatString='<%$ appSettings:FormatToCurrency %>' HeaderStyle-HorizontalAlign="Right"
                                                        ItemStyle-HorizontalAlign="Right">
                                                        <HeaderStyle HorizontalAlign="Right" Width="70px" />
                                                    </asp:BoundField>
                                                </Columns>
                                                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                                <SortedAscendingCellStyle BackColor="#F5F7FB" />
                                                <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                                                <SortedDescendingCellStyle BackColor="#E9EBEF" />
                                                <SortedDescendingHeaderStyle BackColor="#4870BE" />
                                            </asp:GridView>--%>
                                    </asp:Panel>
                                </div>
                            </div>
                            <div style="float: left; width: 1%;">
                            </div>
                        </div>
                    </asp:Panel>
                </div>
            </div>
            <%--*******************************************************************--%>
            <div class="commonPageCss" style="float: left; width: 100%">
                <div style="float: left; width: 1%">
                    &nbsp;
                </div>
                <div style="float: left; width: 10%">
                    Date</div>
                <div style="float: left; width: 1%">
                    &nbsp;
                </div>
                <div style="float: left; width: 15%">
                    <asp:TextBox ID="txtDate" runat="server" CssClass="TextBox"  Width="93px"
                         AutoPostBack="false"></asp:TextBox>
                    <asp:Image ID="imgFromDate" runat="server" ImageUrl="~/Images/Calendar_scheduleHS.png" />
                    <cc1:CalendarExtender ID="CalendarExtender1" runat="server" PopupButtonID="imgFromDate"
                        TargetControlID="txtDate" Format="dd/MMM/yyyy">
                    </cc1:CalendarExtender>
                </div>
                <div style="float: left; width: 1%">
                    &nbsp;
                </div>
            </div>
            <div style="float: left; height: 1px; width: 100%;">
            </div>
            <div class="commonPageCss" style="float: left; width: 100%">
                <div style="float: left; width: 1%">
                    &nbsp;
                </div>
                <div style="float: left; width: 10%">
                    Cash In Hand</div>
                <div style="float: left; width: 1%">
                    &nbsp;
                </div>
                <div style="float: left; width: 10%">
                    <asp:TextBox ID="txtCIH" runat="server" CssClass="TextBox" Width="93px" AutoPostBack="false"
                        Text="0" Style="text-align: right;" onKeyPress="return isNumberKeyAndDot(event,value)"></asp:TextBox>
                </div>
                <div style="float: left; width: 3%">
                    &nbsp;
                </div>
                <div style="float: left; width: 28%">
                    Collection Bonus & Commission Withdrawn</div>
                <div style="float: left; width: 1%">
                    &nbsp;
                </div>
                <div style="float: left; width: 15%">
                    <asp:TextBox ID="txtColBonus" runat="server" CssClass="TextBox" Width="93px" AutoPostBack="false"
                        Text="0" Style="text-align: right;" onKeyPress="return isNumberKeyAndDot(event,value)"></asp:TextBox>
                </div>
                <div style="float: left; width: 1%">
                    &nbsp;
                </div>
            </div>
            <div style="float: left; height: 1px; width: 100%;">
            </div>
            <div class="commonPageCss" style="float: left; width: 100%">
                <div style="float: left; width: 49%">
                    <asp:Panel ID="pnlHeader" runat="server" GroupingText="." Font-Size="11px" Font-Names="Tahoma">
                        <div class="commonPageCss" style="float: left; width: 100%">
                            <div class="CollapsiblePanelHeader" style="width: 100%">
                                Receipts
                            </div>
                            <asp:Panel ID="pnlRemSum" runat="server" ScrollBars="Vertical" BorderColor="#9F9F9F"
                                BorderWidth="1px" Font-Bold="true" Height="115px" Width="100%">
                                <asp:GridView ID="gvRec" runat="server" AutoGenerateColumns="False" Font-Bold="False"
                                    DataKeyNames="rem_cd,rem_sh_desc,rem_val1" CssClass="GridView" Width="94%" CellPadding="4"
                                    EmptyDataText="No data found" ForeColor="#333333" ShowHeaderWhenEmpty="True">
                                    <EditRowStyle BackColor="#2461BF" />
                                    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                    <HeaderStyle BackColor="#507CD1" ForeColor="White" Font-Size="12px" Font-Bold="True" />
                                    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                    <RowStyle Font-Size="12px" BackColor="#EFF3FB" />
                                    <AlternatingRowStyle BackColor="White" />
                                    <Columns>
                                        <asp:BoundField DataField='rem_cd' HeaderText='Code' HeaderStyle-Width="15%" HeaderStyle-HorizontalAlign="Left">
                                            <HeaderStyle HorizontalAlign="Left"/>
                                        </asp:BoundField>
                                        <asp:BoundField DataField='rem_sh_desc' HeaderText='Description' HeaderStyle-Width="56%"
                                            HeaderStyle-HorizontalAlign="Left">
                                            <HeaderStyle HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField='rem_val1' HeaderText='Value' HeaderStyle-Width="23%"
                                            DataFormatString='<%$ appSettings:FormatToCurrency %>' HeaderStyle-HorizontalAlign="Right"
                                            ItemStyle-HorizontalAlign="Right">
                                            <HeaderStyle HorizontalAlign="Right" Width="70px" />
                                        </asp:BoundField>
                                    </Columns>
                                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                    <SortedAscendingCellStyle BackColor="#F5F7FB" />
                                    <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                                    <SortedDescendingCellStyle BackColor="#E9EBEF" />
                                    <SortedDescendingHeaderStyle BackColor="#4870BE" />
                                </asp:GridView>
                            </asp:Panel>
                        </div>
                    </asp:Panel>
                    <div style="float: left; height: 1px; width: 100%;">
                    </div>
                    <div class="commonPageCss" style="float: left; width: 100%">
                        <div style="float: left; width: 71%">
                            &nbsp;
                        </div>
                        <div style="float: left; width: 15%">
                            <asp:TextBox ID="txtRecTot" runat="server" CssClass="TextBox" Width="110px" Enabled="false"
                                Style="text-align: right" AutoPostBack="false"></asp:TextBox>
                        </div>
                        <div style="float: left; width: 1%">
                            &nbsp;
                        </div>
                    </div>
                </div>
                <div style="float: left; width: .5%">
                    &nbsp;
                </div>
                <div style="float: left; width: 49%">
                    <asp:Panel ID="Panel1" runat="server" GroupingText="." Font-Size="11px" Font-Names="Tahoma">
                        <div class="commonPageCss" style="float: left; width: 100%">
                            <div class="CollapsiblePanelHeader" style="width: 100%">
                                Disbursements
                            </div>
                            <asp:Panel ID="Panel2" runat="server" ScrollBars="Vertical" BorderColor="#9F9F9F"
                                BorderWidth="1px" Font-Bold="true" Height="115px" Width="100%">
                                <asp:GridView ID="gvDisb" runat="server" AutoGenerateColumns="False" Font-Bold="False"
                                    DataKeyNames="rem_cd,rem_sh_desc,rem_val1" CssClass="GridView" Width="94%" CellPadding="4"
                                    EmptyDataText="No data found" ForeColor="#333333" ShowHeaderWhenEmpty="True">
                                    <EditRowStyle BackColor="#2461BF" />
                                    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                    <HeaderStyle BackColor="#507CD1" ForeColor="White" Font-Size="12px" Font-Bold="True" />
                                    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                    <RowStyle Font-Size="12px" BackColor="#EFF3FB" />
                                    <AlternatingRowStyle BackColor="White" />
                                    <Columns>
                                        <asp:BoundField DataField='rem_cd' HeaderText='Code' HeaderStyle-Width="15%" HeaderStyle-HorizontalAlign="Left">
                                            <HeaderStyle HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField='rem_sh_desc' HeaderText='Description' HeaderStyle-Width="56%"
                                            HeaderStyle-HorizontalAlign="Left">
                                            <HeaderStyle HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField='rem_val1' HeaderText='Value' HeaderStyle-Width="23%"
                                            DataFormatString='<%$ appSettings:FormatToCurrency %>' HeaderStyle-HorizontalAlign="Right"
                                            ItemStyle-HorizontalAlign="Right">
                                            <HeaderStyle HorizontalAlign="Right" />
                                        </asp:BoundField>
                                    </Columns>
                                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                    <SortedAscendingCellStyle BackColor="#F5F7FB" />
                                    <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                                    <SortedDescendingCellStyle BackColor="#E9EBEF" />
                                    <SortedDescendingHeaderStyle BackColor="#4870BE" />
                                </asp:GridView>
                            </asp:Panel>
                        </div>
                    </asp:Panel>
                    <div style="float: left; height: 1px; width: 100%;">
                    </div>
                    <div class="commonPageCss" style="float: left; width: 100%">
                        <div style="float: left; width: 71%">
                            &nbsp;
                        </div>
                        <div style="float: left; width: 15%">
                            <asp:TextBox ID="txtDisbTot" runat="server" CssClass="TextBox" Width="110px" Enabled="false"
                                Style="text-align: right" AutoPostBack="false"></asp:TextBox>
                        </div>
                        <div style="float: left; width: 1%">
                            &nbsp;
                        </div>
                    </div>
                </div>
            </div>
            <div style="float: left; height: 1px; width: 100%;">
            </div>
            <div class="commonPageCss" style="float: left; width: 100%">
                <div style="float: left; width: 49%">
                    <asp:Panel ID="Panel3" runat="server" GroupingText="." Font-Size="11px" Font-Names="Tahoma">
                        <div class="commonPageCss" style="float: left; width: 100%">
                            <div class="CollapsiblePanelHeader" style="width: 100%">
                                Summary
                            </div>
                            <asp:Panel ID="Panel4" runat="server" ScrollBars="Vertical" BorderColor="#9F9F9F"
                                BorderWidth="1px" Font-Bold="true" Height="95px" Width="100%">
                                <asp:GridView ID="gvSum" runat="server" AutoGenerateColumns="False" Font-Bold="False"
                                    DataKeyNames="rem_cd,rem_sh_desc,rem_val1" CssClass="GridView" Width="94%" CellPadding="4"
                                    EmptyDataText="No data found" ForeColor="#333333" ShowHeaderWhenEmpty="True">
                                    <EditRowStyle BackColor="#2461BF" />
                                    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                    <HeaderStyle BackColor="#507CD1" ForeColor="White" Font-Size="12px" Font-Bold="True" />
                                    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                    <RowStyle Font-Size="12px" BackColor="#EFF3FB" />
                                    <AlternatingRowStyle BackColor="White" />
                                    <Columns>
                                        <asp:BoundField DataField='rem_cd' HeaderText='Code' HeaderStyle-Width="15%" HeaderStyle-HorizontalAlign="Left">
                                            <HeaderStyle HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField='rem_sh_desc' HeaderText='Description' HeaderStyle-Width="56%"
                                            HeaderStyle-HorizontalAlign="Left">
                                            <HeaderStyle HorizontalAlign="Left"  />
                                        </asp:BoundField>
                                        <asp:BoundField DataField='rem_val1' HeaderText='Value' HeaderStyle-Width="23%"
                                            DataFormatString='<%$ appSettings:FormatToCurrency %>' HeaderStyle-HorizontalAlign="Right"
                                            ItemStyle-HorizontalAlign="Right">
                                            <HeaderStyle HorizontalAlign="Right" />
                                        </asp:BoundField>
                                    </Columns>
                                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                    <SortedAscendingCellStyle BackColor="#F5F7FB" />
                                    <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                                    <SortedDescendingCellStyle BackColor="#E9EBEF" />
                                    <SortedDescendingHeaderStyle BackColor="#4870BE" />
                                </asp:GridView>
                            </asp:Panel>
                        </div>
                    </asp:Panel>
                    <div style="float: left; height: 1px; width: 100%;">
                    </div>
                    <div class="commonPageCss" style="float: left; width: 100%">
                        <div style="float: left; width: 71%">
                            &nbsp;
                        </div>
                        <div style="float: left; width: 15%">
                            <asp:TextBox ID="txtSumTot" runat="server" CssClass="TextBox" Width="110px" Enabled="false"
                                Style="text-align: right" AutoPostBack="false"></asp:TextBox>
                        </div>
                        <div style="float: left; width: 1%">
                            &nbsp;
                        </div>
                    </div>
                </div>
                <div style="float: left; width: .5%">
                    &nbsp;
                </div>
                <div style="float: left; width: 49%">
                    <asp:Panel ID="Panel5" runat="server" GroupingText="." Font-Size="11px" Font-Names="Tahoma">
                        <div class="commonPageCss" style="float: left; width: 100%">
                            <div class="CollapsiblePanelHeader" style="width: 100%">
                                Less
                            </div>
                            <asp:Panel ID="Panel6" runat="server" ScrollBars="Vertical" BorderColor="#9F9F9F"
                                BorderWidth="1px" Font-Bold="true" Height="95px" Width="100%">
                                <asp:GridView ID="gvLess" runat="server" AutoGenerateColumns="False" Font-Bold="False"
                                    DataKeyNames="rem_cd,rem_sh_desc,rem_val1" CssClass="GridView" Width="94%" CellPadding="4"
                                    EmptyDataText="No data found" ForeColor="#333333" ShowHeaderWhenEmpty="True">
                                    <EditRowStyle BackColor="#2461BF" />
                                    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                    <HeaderStyle BackColor="#507CD1" ForeColor="White" Font-Size="12px" Font-Bold="True" />
                                    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                    <RowStyle Font-Size="12px" BackColor="#EFF3FB" />
                                    <AlternatingRowStyle BackColor="White" />
                                    <Columns>
                                        <asp:BoundField DataField='rem_cd' HeaderText='Code' HeaderStyle-Width="15%" HeaderStyle-HorizontalAlign="Left">
                                            <HeaderStyle HorizontalAlign="Left"  />
                                        </asp:BoundField>
                                        <asp:BoundField DataField='rem_sh_desc' HeaderText='Description' HeaderStyle-Width="56%"
                                            HeaderStyle-HorizontalAlign="Left">
                                            <HeaderStyle HorizontalAlign="Left"  />
                                        </asp:BoundField>
                                        <asp:BoundField DataField='rem_val1' HeaderText='Value' HeaderStyle-Width="23%"
                                            DataFormatString='<%$ appSettings:FormatToCurrency %>' HeaderStyle-HorizontalAlign="Right"
                                            ItemStyle-HorizontalAlign="Right">
                                            <HeaderStyle HorizontalAlign="Right" />
                                        </asp:BoundField>
                                    </Columns>
                                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                    <SortedAscendingCellStyle BackColor="#F5F7FB" />
                                    <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                                    <SortedDescendingCellStyle BackColor="#E9EBEF" />
                                    <SortedDescendingHeaderStyle BackColor="#4870BE" />
                                </asp:GridView>
                            </asp:Panel>
                        </div>
                    </asp:Panel>
                    <div style="float: left; height: 1px; width: 100%;">
                    </div>
                    <div class="commonPageCss" style="float: left; width: 100%">
                        <div style="float: left; width: 71%">
                            &nbsp;
                        </div>
                        <div style="float: left; width: 15%">
                            <asp:TextBox ID="txtLessTot" runat="server" CssClass="TextBox" Width="110px" Enabled="false"
                                Style="text-align: right" AutoPostBack="false"></asp:TextBox>
                        </div>
                        <div style="float: left; width: 1%">
                            &nbsp;
                        </div>
                    </div>
                </div>
            </div>
            <div style="float: left; height: 1px; width: 100%;">
            </div>
            <div style="float: left; width: 100%">
                <div style="float: left; width: 8%">
                    &nbsp;
                </div>
                <div style="float: left; width: 80%">
                    <table>
                        <tr style="background-color: #EFF3FB;">
                            <td style="padding-top: 6px; width: 150px; background-color: #D1DCF3;">
                                <asp:Label ID="Label21" runat="server" Text="Remitance to be Banked"></asp:Label>
                            </td>
                            <td class="style42" style="padding-bottom: 1px">
                                :
                            </td>
                            <td style="text-align: right; background-color: #EFF3FB; padding-top: 6px; width: 210px"
                                align="left">
                                <asp:Label ID="lbl_banked" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr style="background-color: #EFF3FB;">
                            <td style="padding-top: 6px; width: 150px; background-color: #D1DCF3;">
                                <asp:Label ID="Label22" runat="server" Text="Cash In Hand"></asp:Label>
                            </td>
                            <td class="style42" style="padding-bottom: 1px">
                                :
                            </td>
                            <td style="text-align: right; background-color: #EFF3FB; padding-top: 6px; width: 210px"
                                align="left">
                                <asp:Label ID="lbl_CIH" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr style="background-color: #EFF3FB;">
                            <td style="padding-top: 6px; width: 150px; background-color: #D1DCF3;">
                                <asp:Label ID="Label23" runat="server" Text="Total Remitance"></asp:Label>
                            </td>
                            <td class="style42" style="padding-bottom: 1px">
                                :
                            </td>
                            <td style="text-align: right; background-color: #EFF3FB; padding-top: 6px; width: 210px"
                                align="left">
                                <asp:Label ID="lbl_TotRem" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr style="background-color: #EFF3FB;">
                            <td style="padding-top: 6px; width: 150px; background-color: #D1DCF3;">
                                <asp:Label ID="Label26" runat="server" Text="Difference"></asp:Label>
                            </td>
                            <td class="style42" style="padding-bottom: 1px">
                                :
                            </td>
                            <td style="text-align: right; background-color: #EFF3FB; padding-top: 6px; width: 210px"
                                align="left">
                                <asp:Label ID="lbl_diff" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr style="background-color: #EFF3FB;">
                            <td style="padding-top: 6px; width: 150px; background-color: #D1DCF3;">
                                <asp:Label ID="Label27" runat="server" Text="Commission Withdrawn"></asp:Label>
                            </td>
                            <td class="style42" style="padding-bottom: 1px">
                                :
                            </td>
                            <td style="text-align: right; background-color: #EFF3FB; padding-top: 6px; width: 210px"
                                align="left">
                                <asp:Label ID="lbl_comm_wdr" runat="server"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
