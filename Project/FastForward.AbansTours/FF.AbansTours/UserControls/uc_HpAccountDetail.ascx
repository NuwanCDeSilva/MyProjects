<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="uc_HpAccountDetail.ascx.cs" Inherits="FF.AbansTours.UserControls.uc_HpAccountDetail" %>

<link href="../Styles/Site.css" rel="stylesheet" type="text/css" />
<div style="float:left;width:100%;">
        <asp:Panel ID="Panel_grvDet" runat="server"  Height="91px" Width="100%" 
            ScrollBars="Both">
            <asp:GridView ID="grv_ucAccDetails" runat="server" AutoGenerateColumns="False" 
                        CellPadding="3" ForeColor="#333333" CssClass="GridView" 
                ShowHeaderWhenEmpty="True" Width="100%">
                    <AlternatingRowStyle BackColor="White" />
                    <EmptyDataTemplate > <div style="width:100%; text-align:center;"> No data found </div> </EmptyDataTemplate>
                    <Columns>
                        <asp:BoundField DataField="HPT_REF_NO" HeaderText="Reference No." 
                            HeaderStyle-Width ="20%" HeaderStyle-HorizontalAlign="Left" 
                            Visible="False" >
                        <HeaderStyle HorizontalAlign="Left" Width="20%" />
                        </asp:BoundField>
                        <asp:BoundField DataField="hpt_txn_dt" DataFormatString="{0:d}" 
                            HeaderText="Transaction Date" />
                        <asp:BoundField DataField="hpt_desc" HeaderText="Description" />
                        <asp:BoundField DataField="HPT_MNL_REF" HeaderText="Reference #" />
                        <asp:BoundField DataField="HPT_DBT" HeaderStyle-HorizontalAlign="Right" DataFormatString="{0:n2}"
                            HeaderStyle-Width="15%" HeaderText="Dr.">
                        <HeaderStyle HorizontalAlign="Right" Width="15%" />
                        </asp:BoundField>
                        <asp:BoundField DataField="HPT_CRDT" HeaderStyle-HorizontalAlign="Right" DataFormatString="{0:n2}"
                            HeaderStyle-Width="15%" HeaderText="Cr.">
                        <HeaderStyle HorizontalAlign="Right" Width="15%" />
                        </asp:BoundField>
                        <asp:BoundField DataField="SAR_PREFIX" HeaderText="Prefix" 
                            HeaderStyle-Width ="10%" HeaderStyle-HorizontalAlign="Left" >
                        <HeaderStyle HorizontalAlign="Left" Width="10%" />
                        </asp:BoundField>
                        <asp:BoundField DataField="SAR_MANUAL_REF_NO" HeaderText="Receipt No" 
                            HeaderStyle-Width ="20%" HeaderStyle-HorizontalAlign="Left" >
                        <HeaderStyle HorizontalAlign="Left" Width="20%" />
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
