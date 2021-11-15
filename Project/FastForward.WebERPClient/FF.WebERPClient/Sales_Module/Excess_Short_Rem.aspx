<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="Excess_Short_Rem.aspx.cs" Inherits="FF.WebERPClient.Sales_Module.Excess_Short_Rem" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/UserControls/uc_MsgInfo.ascx" TagName="uc_MsgInfo" TagPrefix="uc1" %>
<%@ Register Src="../UserControls/uc_CommonSearch.ascx" TagName="uc_CommonSearch"
    TagPrefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
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
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:TabContainer ID="tbcRequest" runat="server" ActiveTabIndex="0" Height="450px">
        <cc1:TabPanel runat="server" HeaderText="Excess/Short Settlement" ID="tbpSettlement">
            <ContentTemplate>
                <asp:UpdatePanel ID="pnlButtons" runat="server">
                    <ContentTemplate>
                        <div class="commonPageCss" style="float: left; width: 100%">
                            <div style="float: left; width: 1%">
                                &nbsp;
                            </div>
                            <div style="float: left; width: 98%">
                                <div class="CollapsiblePanelHeader" style="width: 98%">
                                    Previous Month Excess/Short Balances
                                </div>
                                <asp:Panel ID="pnlPendingDoc" runat="server" ScrollBars="Vertical" BorderColor="#9F9F9F"
                                    BorderWidth="1px" Font-Bold="true" Height="115px" Width="98%">
                                    <asp:GridView ID="gvBal" runat="server" AutoGenerateColumns="False" Font-Bold="False"
                                        DataKeyNames="exss_desc,exss_mnth,BAL" CssClass="GridView" Width="100%" CellPadding="4"
                                        EmptyDataText="No data found" ForeColor="#333333" ShowHeaderWhenEmpty="True">
                                        <EditRowStyle BackColor="#2461BF" />
                                        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                        <HeaderStyle BackColor="#507CD1" ForeColor="White" Font-Size="12px" Font-Bold="True" />
                                        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                        <RowStyle Font-Size="12px" BackColor="#EFF3FB" />
                                        <AlternatingRowStyle BackColor="White" />
                                        <Columns>
                                            <asp:BoundField DataField='exss_desc' HeaderText='Description' HeaderStyle-Width="170px"
                                                HeaderStyle-HorizontalAlign="Left">
                                                <HeaderStyle HorizontalAlign="Left" Width="170px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField='exss_mnth' HeaderText='Month' HeaderStyle-Width="75px"
                                                DataFormatString="{0:dd/MMM/yyyy}" HeaderStyle-HorizontalAlign="Left">
                                                <HeaderStyle HorizontalAlign="Left" Width="75px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField='BAL' HeaderText='Amount' DataFormatString='<%$ appSettings:FormatToCurrency %>'>
                                                <HeaderStyle HorizontalAlign="Right" Width="70px" />
                                                <ItemStyle HorizontalAlign="Right" />
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
                            <div style="float: left; width: 1%">
                                &nbsp;
                            </div>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
                <div style="float: left; height: 8px; width: 100%;">
                </div>
                <asp:UpdatePanel ID="UpdatePanel222" runat="server">
                    <ContentTemplate>
                        <div class="commonPageCss" style="float: left; width: 100%">
                            <div style="float: left; width: 1%">
                                &nbsp;
                            </div>
                            <div style="float: left; width: 30%">
                                <div style="float: left; width: 100%;">
                                    <div style="float: left; width: 2%;">
                                        &nbsp;</div>
                                    <div style="float: left; width: 24%">
                                        Year
                                    </div>
                                    <div style="float: left; width: 1%;">
                                        &nbsp;</div>
                                    <div style="float: left; width: 50%">
                                        <asp:DropDownList ID="ddlYear" runat="server" AutoPostBack="True" AppendDataBoundItems="True"
                                            CssClass="ComboBox" Width="100%" OnSelectedIndexChanged="ddlYear_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </div>
                                    <div style="float: left; width: 2%;">
                                        &nbsp;</div>
                                </div>
                                <div style="float: left; height: 3px; width: 100%;">
                                </div>
                                <div style="float: left; width: 100%;">
                                    <div style="float: left; width: 2%;">
                                        &nbsp;</div>
                                    <div style="float: left; width: 24%">
                                        Month
                                    </div>
                                    <div style="float: left; width: 1%;">
                                        &nbsp;</div>
                                    <div style="float: left; width: 50%">
                                        <asp:DropDownList ID="ddlMonth" runat="server" AutoPostBack="True" AppendDataBoundItems="True"
                                            CssClass="ComboBox" Width="100%" OnSelectedIndexChanged="ddlMonth_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </div>
                                    <div style="float: left; width: 2%;">
                                        &nbsp;</div>
                                </div>
                                <div style="display: none">
                                    <asp:TextBox ID="txtSetMonth" runat="server" CssClass="TextBox" Width="93px"></asp:TextBox>
                                </div>
                                <div style="float: left; height: 3px; width: 100%;">
                                </div>
                                <div style="float: left; width: 100%;">
                                    <div style="float: left; width: 2%;">
                                        &nbsp;</div>
                                    <div style="float: left; width: 24%">
                                        Settle Date
                                    </div>
                                    <div style="float: left; width: 1%;">
                                        &nbsp;</div>
                                    <div style="float: left; width: 50%">
                                        <asp:TextBox ID="txtSetDate" runat="server" CssClass="TextBox" Width="93px"></asp:TextBox>
                                        <asp:Image ID="imgSetDate" runat="server" ImageUrl="~/Images/Calendar_scheduleHS.png" />
                                        <cc1:CalendarExtender ID="CalendarExtender1" runat="server" PopupButtonID="imgSetDate"
                                            TargetControlID="txtSetDate" Format="dd/MMM/yyyy" Enabled="True">
                                        </cc1:CalendarExtender>
                                    </div>
                                    <div style="float: left; width: 2%;">
                                        &nbsp;</div>
                                </div>
                                <div style="float: left; height: 3px; width: 100%;">
                                </div>
                                <div style="float: left; width: 100%;">
                                    <div style="float: left; width: 2%;">
                                        &nbsp;</div>
                                    <div style="float: left; width: 24%">
                                        Balance
                                    </div>
                                    <div style="float: left; width: 1%;">
                                        &nbsp;</div>
                                    <div style="float: left; width: 50%">
                                        <asp:TextBox ID="txtBalance" runat="server" CssClass="TextBox" Width="93px" Enabled="False"
                                            Style="text-align: right"></asp:TextBox>
                                    </div>
                                    <div style="float: left; width: 2%;">
                                        &nbsp;</div>
                                </div>
                                <div style="float: left; height: 3px; width: 100%;">
                                </div>
                                <div style="float: left; width: 100%;">
                                    <div style="float: left; width: 2%;">
                                        &nbsp;</div>
                                    <div style="float: left; width: 24%">
                                        Amount
                                    </div>
                                    <div style="float: left; width: 1%;">
                                        &nbsp;</div>
                                    <div style="float: left; width: 50%">
                                        <asp:TextBox ID="txtSetAmt" runat="server" CssClass="TextBox" Width="93px" Style="text-align: right"
                                            onKeyPress="return isNumberKeyAndDot(event,value)"></asp:TextBox>
                                    </div>
                                    <div style="float: left; width: 2%;">
                                        &nbsp;</div>
                                </div>
                                <div style="float: left; height: 3px; width: 100%;">
                                </div>
                                <div style="float: left; width: 100%;">
                                    <div style="float: left; width: 2%;">
                                        &nbsp;</div>
                                    <div style="float: left; width: 24%">
                                        Remarks
                                    </div>
                                    <div style="float: left; width: 1%;">
                                        &nbsp;</div>
                                    <div style="float: left; width: 50%">
                                        <asp:TextBox ID="txtSetRem" runat="server" CssClass="TextBox" Width="93px"></asp:TextBox>
                                    </div>
                                    <div style="float: left; width: 2%;">
                                        &nbsp;</div>
                                </div>
                                <div style="float: left; height: 6px; width: 100%;">
                                </div>
                                <div style="float: left; width: 100%;">
                                    <div style="float: left; width: 27%;">
                                        &nbsp;</div>
                                    <div style="float: left; width: 33%">
                                        <asp:Button Text="Add" ID="btnSetAdd" runat="server" CssClass="Button" Width="75%"
                                            Style="text-align: center" OnClick="btnSetAdd_Click" />
                                    </div>
                                    <div style="float: left; width: 4%;">
                                        &nbsp;</div>
                                    <div style="float: left; width: 33%">
                                        <asp:Button Text="Delete" ID="btnSetDel" runat="server" CssClass="Button" Width="75%"
                                            Style="text-align: center" OnClick="btnSetDel_Click" OnClientClick="return ProcessConfirm()" />
                                    </div>
                                    <div style="float: left; width: 3%;">
                                        &nbsp;</div>
                                </div>
                            </div>
                            <div style="float: left; width: 1%">
                                &nbsp;
                            </div>
                            <div style="float: left; width: 67%">
                                <div class="CollapsiblePanelHeader" style="width: 98%">
                                    Excess/Short Settlement Details
                                </div>
                                <asp:Panel ID="Panel1" runat="server" ScrollBars="Vertical" BorderColor="#9F9F9F"
                                    BorderWidth="1px" Font-Bold="True" Height="115px" Width="98%">
                                    <asp:GridView ID="GvSettle" runat="server" AutoGenerateColumns="False" Font-Bold="False"
                                        DataKeyNames="exss_desc,exss_amt,exss_txn_dt,exss_user" CssClass="GridView" Width="100%"
                                        CellPadding="4" EmptyDataText="No data found" ForeColor="#333333" ShowHeaderWhenEmpty="True">
                                        <EditRowStyle BackColor="#2461BF" />
                                        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                        <HeaderStyle BackColor="#507CD1" ForeColor="White" Font-Size="12px" Font-Bold="True" />
                                        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                        <RowStyle Font-Size="12px" BackColor="#EFF3FB" />
                                        <AlternatingRowStyle BackColor="White" />
                                        <Columns>
                                            <asp:BoundField DataField='exss_desc' HeaderText='Description'>
                                                <HeaderStyle HorizontalAlign="Left" Width="170px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField='exss_amt' HeaderText='Amount' DataFormatString='<%$ appSettings:FormatToCurrency %>'>
                                                <HeaderStyle HorizontalAlign="Right" Width="70px" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField='exss_txn_dt' HeaderText='Settlement Date' DataFormatString="{0:dd/MMM/yyyy}">
                                                <HeaderStyle HorizontalAlign="Left" Width="75px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField='exss_user' HeaderText='User'>
                                                <HeaderStyle HorizontalAlign="Left" Width="70px" />
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
                            <div style="float: left; width: 1%">
                                &nbsp;
                            </div>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </ContentTemplate>
        </cc1:TabPanel>
        <cc1:TabPanel runat="server" HeaderText="Process Excess/Short" ID="tbpProcess">
            <ContentTemplate>
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <div style="float: left; width: 100%;">
                            <div style="float: left; width: 99%; height: 22px; text-align: right; background-color: #1E4A9F">
                                <div style="float: left;">
                                    <asp:Label ID="lblDispalyInfor" runat="server" CssClass="Label" ForeColor="Yellow"></asp:Label>
                                </div>
                                <div style="float: right;">
                                    <asp:Button Text="Print" ID="btnPrint" runat="server" CssClass="Button" OnClick="btnPrint_Click" />
                                    &nbsp;
                                    <asp:Button Text="Confirm" ID="btnConfirm" runat="server" CssClass="Button" OnClick="btnConfirm_Click"
                                        OnClientClick="return ProcessConfirm()" />
                                    &nbsp;
                                    <asp:Button Text="Process" ID="btnProcess" runat="server" CssClass="Button" Enabled="false"
                                        OnClick="btnProcess_Click" />
                                    &nbsp;
                                    <asp:Button Text="Close" ID="btnClose" runat="server" CssClass="Button" OnClick="btnClose_Click" />
                                    &nbsp;
                                </div>
                            </div>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
                <div style="float: left; height: 3px; width: 100%;">
                </div>
                <asp:UpdatePanel ID="UpdPnl" runat="server">
                    <ContentTemplate>
                        <div class="commonPageCss" style="float: left; width: 100%">
                            <div style="float: left; width: 1%">
                                &nbsp;
                            </div>
                            <div style="float: left; width: 40%">
                                <div style="float: left; width: 100%;">
                                    <div style="float: left; width: 2%;">
                                        &nbsp;</div>
                                    <div style="float: left; width: 18%">
                                        Year
                                    </div>
                                    <div style="float: left; width: 1%;">
                                        &nbsp;</div>
                                    <div style="float: left; width: 50%">
                                        <asp:DropDownList ID="ddlYear1" runat="server" AutoPostBack="True" AppendDataBoundItems="True"
                                            CssClass="ComboBox" Width="100px" OnSelectedIndexChanged="ddlYear1_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </div>
                                    <div style="float: left; width: 2%;">
                                        &nbsp;</div>
                                </div>
                                <div style="float: left; height: 3px; width: 100%;">
                                </div>
                                <div style="float: left; width: 100%;">
                                    <div style="float: left; width: 2%;">
                                        &nbsp;</div>
                                    <div style="float: left; width: 18%">
                                        Month
                                    </div>
                                    <div style="float: left; width: 1%;">
                                        &nbsp;</div>
                                    <div style="float: left; width: 39%">
                                        <asp:DropDownList ID="ddlMonth_1" runat="server" AutoPostBack="True" AppendDataBoundItems="True"
                                            CssClass="ComboBox" Width="100%" OnSelectedIndexChanged="ddlMonth_1_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </div>
                                    <div style="float: left; width: 1%;">
                                        &nbsp;</div>
                                    <div style="float: left; width: 10%">
                                        <asp:Label ID="lblStatus" runat="server" ForeColor="Blue"></asp:Label>
                                    </div>
                                    <div style="display: none">
                                        <asp:TextBox ID="txtProcMonth" runat="server" CssClass="TextBox" Width="93px"></asp:TextBox>
                                    </div>
                                    <div style="float: left; width: 2%;">
                                        &nbsp;</div>
                                </div>
                                <div style="float: left; height: 3px; width: 100%;">
                                </div>
                                <div style="float: left; width: 100%;">
                                    <div style="float: left; width: 20%;">
                                        &nbsp;</div>
                                    <div style="float: left; width: 20%;">
                                        <asp:RadioButton ID="optE" runat="server" Text="Excesss" GroupName="Rpt" Checked="true"
                                            OnCheckedChanged="opt_Changed" AutoPostBack="true" /></div>
                                    <div style="float: left; width: 10%;">
                                        &nbsp;</div>
                                    <div style="float: left; width: 20%;">
                                        <asp:RadioButton ID="optS" runat="server" Text="Short" GroupName="Rpt" OnCheckedChanged="opt_Changed"
                                            AutoPostBack="true" /></div>
                                    <div style="float: left; width: 2%;">
                                        &nbsp;</div>
                                </div>
                                <div style="float: left; height: 3px; width: 100%;">
                                </div>
                                <div style="float: left; width: 100%;">
                                    <div style="float: left; width: 2%;">
                                        &nbsp;</div>
                                    <div style="float: left; width: 18%">
                                        Remit Type
                                    </div>
                                    <div style="float: left; width: 1%;">
                                        &nbsp;</div>
                                    <div style="float: left; width: 77%">
                                        <asp:DropDownList ID="ddlRemType" runat="server" AutoPostBack="True" AppendDataBoundItems="True"
                                            CssClass="ComboBox" Width="100%">
                                        </asp:DropDownList>
                                    </div>
                                    <div style="float: left; width: 2%;">
                                        &nbsp;</div>
                                </div>
                                <div style="float: left; height: 3px; width: 100%;">
                                </div>
                                <div style="float: left; width: 100%;">
                                    <div style="float: left; width: 2%;">
                                        &nbsp;</div>
                                    <div style="float: left; width: 18%">
                                        Date
                                    </div>
                                    <div style="float: left; width: 1%;">
                                        &nbsp;</div>
                                    <div style="float: left; width: 50%">
                                        <asp:TextBox ID="txtProcDate" runat="server" CssClass="TextBox" Width="93px"></asp:TextBox>
                                        <asp:Image ID="ImgDate" runat="server" ImageUrl="~/Images/Calendar_scheduleHS.png" />
                                        <cc1:CalendarExtender ID="CalendarExtender2" runat="server" PopupButtonID="ImgDate"
                                            TargetControlID="txtProcDate" Format="dd/MMM/yyyy" Enabled="True">
                                        </cc1:CalendarExtender>
                                    </div>
                                    <div style="float: left; width: 2%;">
                                        &nbsp;</div>
                                </div>
                                <div style="float: left; height: 3px; width: 100%;">
                                </div>
                                <div style="float: left; width: 100%;">
                                    <div style="float: left; width: 2%;">
                                        &nbsp;</div>
                                    <div style="float: left; width: 18%">
                                        Week
                                    </div>
                                    <div style="float: left; width: 1%;">
                                        &nbsp;</div>
                                    <div style="float: left; width: 50%">
                                        <asp:TextBox ID="txtWeek" runat="server" CssClass="TextBox" Width="93px" Enabled="false"
                                            Style="text-align: center"></asp:TextBox>
                                    </div>
                                    <div style="float: left; width: 2%;">
                                        &nbsp;</div>
                                </div>
                                <div style="float: left; height: 3px; width: 100%;">
                                </div>
                                <div style="float: left; width: 100%;">
                                    <div style="float: left; width: 2%;">
                                        &nbsp;</div>
                                    <div style="float: left; width: 18%">
                                        Amount
                                    </div>
                                    <div style="float: left; width: 1%;">
                                        &nbsp;</div>
                                    <div style="float: left; width: 50%">
                                        <asp:TextBox ID="txtAmt" runat="server" CssClass="TextBox" Width="93px" Style="text-align: right"
                                            onKeyPress="return isNumberKeyAndDot(event,value)"></asp:TextBox>
                                    </div>
                                    <div style="float: left; width: 2%;">
                                        &nbsp;</div>
                                </div>
                                <div style="float: left; height: 6px; width: 100%;">
                                </div>
                                <div style="float: left; width: 100%;">
                                    <div style="float: left; width: 27%;">
                                        &nbsp;</div>
                                    <div style="float: left; width: 33%">
                                        <asp:Button Text="Add" ID="btnAdd" runat="server" CssClass="Button" Width="75%" Style="text-align: center"
                                            OnClick="btnAdd_Click" />
                                    </div>
                                    <div style="float: left; width: 4%;">
                                        &nbsp;</div>
                                    <div style="float: left; width: 33%">
                                        <asp:Button Text="Delete" ID="btnDel" runat="server" CssClass="Button" Width="75%"
                                            Style="text-align: center" OnClick="btnDel_Click" OnClientClick="return ProcessConfirm()" />
                                    </div>
                                    <div style="float: left; width: 3%;">
                                        &nbsp;</div>
                                </div>
                            </div>
                            <div style="float: left; width: 1%">
                                &nbsp;
                            </div>
                            <div style="float: left; width: 57%">
                                <div class="CollapsiblePanelHeader" style="width: 98%">
                                    Other Excess/Short Rremittance Details
                                </div>
                                <asp:Panel ID="Panel2" runat="server" ScrollBars="Vertical" BorderColor="#9F9F9F"
                                    BorderWidth="1px" Font-Bold="True" Height="115px" Width="98%">
                                    <asp:GridView ID="GvOthRem" runat="server" AutoGenerateColumns="False" Font-Bold="False"
                                        DataKeyNames="ESRD_CD,ESRD_DESC,ESRD_WEEK,ESRD_EXCES,ESRD_SHORT" CssClass="GridView"
                                        Width="100%" CellPadding="4" EmptyDataText="No data found" ForeColor="#333333"
                                        ShowHeaderWhenEmpty="True">
                                        <EditRowStyle BackColor="#2461BF" />
                                        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                        <HeaderStyle BackColor="#507CD1" ForeColor="White" Font-Size="12px" Font-Bold="True" />
                                        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                        <RowStyle Font-Size="12px" BackColor="#EFF3FB" />
                                        <AlternatingRowStyle BackColor="White" />
                                        <Columns>
                                            <asp:BoundField DataField='ESRD_CD' HeaderText='Code'>
                                                <HeaderStyle HorizontalAlign="Left" Width="80px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField='ESRD_DESC' HeaderText='Description'>
                                                <HeaderStyle HorizontalAlign="Left" Width="260px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField='ESRD_WEEK' HeaderText='Week'>
                                                <HeaderStyle HorizontalAlign="Left" Width="70px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField='ESRD_EXCES' HeaderText='Excess' DataFormatString='<%$ appSettings:FormatToCurrency %>'>
                                                <HeaderStyle HorizontalAlign="Right" Width="70px" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField='ESRD_SHORT' HeaderText='Short' DataFormatString='<%$ appSettings:FormatToCurrency %>'>
                                                <HeaderStyle HorizontalAlign="Right" Width="70px" />
                                                <ItemStyle HorizontalAlign="Right" />
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
                            <div style="float: left; width: 1%">
                                &nbsp;
                            </div>
                            <div style="display: none;">
                                <asp:Button ID="btnDate" runat="server" OnClick="Process_Date_change" />
                            </div>
                        </div>
                        <div style="float: left; height: 3px; width: 100%;">
                        </div>
                        <div class="commonPageCss" style="float: left; width: 100%">
                            <div style="float: left; width: 1%">
                                &nbsp;
                            </div>
                            <div style="float: left; width: 98%">
                                <div class="CollapsiblePanelHeader" style="width: 98%">
                                    Excess/Short Remittance Summary
                                </div>
                                <asp:Panel ID="Panel3" runat="server" ScrollBars="Vertical" BorderColor="#9F9F9F"
                                    BorderWidth="1px" Font-Bold="true" Height="115px" Width="98%">
                                    <asp:GridView ID="GvExcRemSum" runat="server" AutoGenerateColumns="False" Font-Bold="False"
                                        DataKeyNames="ESRD_WEEK,ESRD_DESC,ESRD_EXCES,ESRD_SHORT" CssClass="GridView"
                                        Width="100%" CellPadding="4" EmptyDataText="No data found" ForeColor="#333333"
                                        ShowHeaderWhenEmpty="True">
                                        <EditRowStyle BackColor="#2461BF" />
                                        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                        <HeaderStyle BackColor="#507CD1" ForeColor="White" Font-Size="12px" Font-Bold="True" />
                                        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                        <RowStyle Font-Size="12px" BackColor="#EFF3FB" />
                                        <AlternatingRowStyle BackColor="White" />
                                        <Columns>
                                            <asp:BoundField DataField='ESRD_WEEK' HeaderText='Week' HeaderStyle-Width="170px"
                                                HeaderStyle-HorizontalAlign="Center">
                                                <HeaderStyle HorizontalAlign="Center" Width="80px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField='ESRD_DESC' HeaderText='Description' HeaderStyle-Width="170px"
                                                HeaderStyle-HorizontalAlign="Left">
                                                <HeaderStyle HorizontalAlign="Left" Width="220px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField='ESRD_EXCES' HeaderText='Excess' DataFormatString='<%$ appSettings:FormatToCurrency %>'>
                                                <HeaderStyle HorizontalAlign="Right" Width="70px" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField='ESRD_SHORT' HeaderText='Short' DataFormatString='<%$ appSettings:FormatToCurrency %>'>
                                                <HeaderStyle HorizontalAlign="Right" Width="70px" />
                                                <ItemStyle HorizontalAlign="Right" />
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
                            <div style="float: left; width: 1%">
                                &nbsp;
                            </div>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </ContentTemplate>
        </cc1:TabPanel>
    </asp:TabContainer>
</asp:Content>
