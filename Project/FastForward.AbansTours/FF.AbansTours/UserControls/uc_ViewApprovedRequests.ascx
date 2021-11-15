<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="uc_ViewApprovedRequests.ascx.cs" Inherits="FF.AbansTours.UserControls.uc_ViewApprovedRequests" %>
<div style="float:left;width:100%; font-size: 11px;">
 <asp:Panel ID="Panel_view" runat="server">
    <div style="padding: 0.5px; float:left; width:100%; font-size: 11px;">
        <div  style="float:left;width:1%;"></div>
        <div style="float:left;width:25%; text-align: right;">
            <asp:Label ID="Label1" runat="server" Text="Approved Request Num :"></asp:Label> 
        </div>
        <div style="float:left;width:60%;">
            <asp:DropDownList ID="uc_ddlReqNo" runat="server" Font-Size="Small" 
                onselectedindexchanged="uc_ddlReqNo_SelectedIndexChanged" Width="20%">
            </asp:DropDownList>
        </div>
        <div style="float:left;width:10%;">
            <asp:Label ID="uc_lblSelectedReqNum" runat="server"></asp:Label>   
        </div>
    </div>
    <div style="padding: 2px; float:left; width:100%;">
      <div  style="float:left;width:1%;"></div>
        <asp:GridView ID="grv_ViewReqDet" runat="server" CellPadding="4" 
            ForeColor="#333333" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True">
            <AlternatingRowStyle BackColor="White" />
            <EmptyDataTemplate > <div style="width:100%; text-align:center;"> No data found </div> </EmptyDataTemplate>
            <Columns>
                <asp:BoundField DataField="grah_ref" HeaderText="Request #" />
                <asp:BoundField DataField="grad_line" HeaderText="Line #" />
                <asp:BoundField DataField="grad_req_param" HeaderText="Req. Parameter" />
                <asp:BoundField DataField="grah_fuc_cd" HeaderText="Reference to" />
                <asp:BoundField DataField="grah_app_stus" HeaderText="Status" />
                <asp:BoundField DataField="grah_app_lvl" HeaderText="Approved Level" />
                <asp:BoundField DataField="grah_remaks" HeaderText="Remarks" />
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
     <div  style="float:left;width:1%;"></div>
     
 </asp:Panel>
</div>