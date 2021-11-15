<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="uc_ItemSerialView.ascx.cs"
    Inherits="FF.AbansTours.UserControls.uc_ItemSerialView" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>


    <asp:TabContainer ID="TabContainerMain" runat="server" ActiveTabIndex="0">
        <asp:TabPanel runat="server" HeaderText="Documents" ID="TabPanel1">
            <ContentTemplate>
                <asp:GridView ID="GridViewDocuments" runat="server" Width="99%" EmptyDataText="No Document found"
                    ShowHeaderWhenEmpty="True" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333"
                    GridLines="Both" CssClass="GridView">
                    <AlternatingRowStyle BackColor="White" />
                    <Columns>
                        <asp:BoundField HeaderText="Batch Line" DataField="INB_BATCH_LINE" Visible="false" />
                        <asp:BoundField HeaderText="Item Code" DataField="INB_ITM_CD" />
                        <asp:BoundField HeaderText="Item Status" DataField="INB_ITM_STUS" />
                        <asp:BoundField HeaderText="Batch No" DataField="INB_BATCH_NO" />
                        <asp:BoundField HeaderText="Doc No" DataField="INB_DOC_NO" />
                        <asp:BoundField HeaderText="Date" DataField="INB_DOC_DT" DataFormatString='<%$ appSettings:FormatToDate %>' />
                        <asp:BoundField HeaderText="Unit Cost" DataField="INB_UNIT_COST" HeaderStyle-HorizontalAlign="Right"
                            ItemStyle-HorizontalAlign="Right" Visible="false" />
                        <asp:BoundField HeaderText="Qty" DataField="INB_QTY" HeaderStyle-HorizontalAlign="Right"
                            ItemStyle-HorizontalAlign="Right" Visible="false" />
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
            </ContentTemplate>
        </asp:TabPanel>
        <asp:TabPanel runat="server" HeaderText="Serials" ID="TabPanel2">
            <ContentTemplate>
                <asp:GridView ID="GridViewSerials" runat="server" Width="99%" EmptyDataText="No Serial found"
                    ShowHeaderWhenEmpty="True" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333"
                    CssClass="GridView">
                    <AlternatingRowStyle BackColor="White" />
                    <Columns>
                        <asp:BoundField HeaderText="Item Code" DataField="INS_ITM_CD" />
                        <asp:BoundField HeaderText="Item Status" DataField="INS_ITM_STUS" />
                        <asp:BoundField HeaderText="Doc No" DataField="INS_DOC_NO" />
                        <asp:BoundField HeaderText="Date" DataField="INS_DOC_DT" DataFormatString='<%$ appSettings:FormatToDate %>' />
                        <asp:BoundField HeaderText="Serial" DataField="INS_SER_1" />
                        <asp:BoundField HeaderText="Serial 1" DataField="INS_SER_1" Visible="false" />
                        <asp:BoundField HeaderText="Serial 2" DataField="INS_SER_2" Visible="false" />
                        <asp:BoundField HeaderText="Serial 3" DataField="INS_SER_3" Visible="false" />
                        <asp:BoundField HeaderText="Warranty No" DataField="INS_WARR_NO" Visible="true" />
                        <asp:BoundField HeaderText="Warranty Period" DataField="INS_WARR_PERIOD" Visible="false" />
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
            </ContentTemplate>
        </asp:TabPanel>
    </asp:TabContainer>


