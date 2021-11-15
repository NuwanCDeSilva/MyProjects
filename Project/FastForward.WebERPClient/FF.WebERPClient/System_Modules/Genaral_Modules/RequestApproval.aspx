<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
 CodeBehind="RequestApproval.aspx.cs" Inherits="FF.WebERPClient.Genaral_Modules.RequestApproval" EnableEventValidation="false" %>

 <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
     <script type="text/javascript">
     
    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<asp:UpdatePanel ID="updtMainPnl" runat="server">
    <ContentTemplate>
        <div style="float: left; width: 100%; height: 20px; padding-top: 2px; padding-bottom: 2px;
                                background-color: #FFFFFF;">
             <asp:Button ID="btnRefresh" runat="server" Text="Refresh pending requests"  Height="20px" 
                                Width="100%" style="text-align :center" 
                                CssClass = "Button" BackColor="#66CCFF" 
                 onclick="btnRefresh_Click" />
        </div>

        <div style="float: left; width: 100%; height: 200px; padding-top: 2px; padding-bottom: 2px;
                                background-color: #FFFFFF;">

            <asp:Panel ID="pnlDetails" runat="server" Height="190px" ScrollBars="Auto" BorderColor="#9F9F9F"
                                    BorderWidth="0px" Font-Bold="true" Width="100%">
                <asp:GridView ID="gvPendingApp" runat="server" AutoGenerateColumns="False" 
                           Font-Bold="False" style="margin-top: 0px"  
                       GridLines ="None" RowStyle-Height="10px" Width="100%" 
                    OnRowcommand="gvPendingApp_Rowcommand" CellPadding="4" ForeColor="#333333" EmptyDataText="No data found" 
                                        ShowHeaderWhenEmpty="True" CssClass="GridView">
                    <EditRowStyle BackColor="#2461BF" />
                       <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                       <HeaderStyle BackColor="#507CD1" ForeColor="White" Font-Size="11px" 
                           Height="11px" Font-Bold="True" />
                       <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                       <RowStyle Font-Size="11px" BackColor="#EFF3FB"/>
                            <AlternatingRowStyle BackColor="White" />
                            <Columns>
                                <asp:BoundField DataField='grah_com' HeaderText='Company' ItemStyle-Width="3%" 
                                    HeaderStyle-HorizontalAlign="Left"  >
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle Width="3%" />
                                </asp:BoundField>
                                <asp:BoundField DataField='grah_loc' HeaderText='Loc / Profit'  
                                    ItemStyle-Width="8%" HeaderStyle-HorizontalAlign="Left"  >
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle Width="8%" />
                                </asp:BoundField>
                                <asp:BoundField DataField='grah_app_tp' HeaderText='Type'  ItemStyle-Width="5%" 
                                    HeaderStyle-HorizontalAlign="Left"  >
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle Width="5%" />
                                </asp:BoundField>
                                <asp:BoundField DataField='grah_fuc_cd' HeaderText='For'  ItemStyle-Width="6%" 
                                    HeaderStyle-HorizontalAlign="Left"  >
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle Width="6%" />
                                </asp:BoundField>
                                <asp:BoundField DataField='grah_ref' HeaderText='Reference'  
                                    ItemStyle-Width="10%" HeaderStyle-HorizontalAlign="Left"  >
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle Width="10%" />
                                </asp:BoundField>
                                <asp:BoundField DataField='grah_oth_loc' HeaderText='Ref. Loc'  
                                    ItemStyle-Width="8%" HeaderStyle-HorizontalAlign="Left"  >
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle Width="8%" />
                                </asp:BoundField>
                                <asp:BoundField DataField='grah_cre_by' HeaderText='' Visible="false" />
                                <asp:BoundField DataField='grah_cre_dt' HeaderText='Request date'  
                                    ItemStyle-Width="14%" HeaderStyle-HorizontalAlign="Left" 
                                    DataFormatString = "{0:d}" >
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle Width="14%" />
                                </asp:BoundField>
                                <asp:BoundField DataField='grah_mod_by' HeaderText='' Visible="false" />
                                <asp:BoundField DataField='grah_mod_dt' HeaderText='' Visible="false" />
                                <asp:BoundField DataField='grah_app_stus' HeaderText='' Visible="false" />
                                <asp:BoundField DataField='grah_app_lvl' HeaderText='' Visible="false" />
                                <asp:BoundField DataField='grah_app_by' HeaderText='' Visible="false" />
                                <asp:BoundField DataField='grah_app_dt' HeaderText='' Visible="false" />
                                <asp:BoundField DataField='grah_remaks' HeaderText='' Visible="false" />
                                <asp:TemplateField HeaderText="">
                                    <ItemTemplate>
                                        <asp:Button ID="btnApprove" runat="server" Text="Approve" CssClass = "Button" CommandName ="APPROVE"/>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" Width="8%" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="">
                                    <ItemTemplate>
                                        <asp:Button ID="btnReject" runat="server" Text="Reject" CssClass = "Button" CommandName = "REJECT"/>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" Width="8%" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="">
                                    <ItemTemplate>
                                        <asp:Button ID="btnView" runat="server" Text="View" CssClass= "Button" CommandName = "VIEW"/>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" Width="8%" />
                                </asp:TemplateField>
                            </Columns>
                       <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                       <SortedAscendingCellStyle BackColor="#F5F7FB" />
                       <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                       <SortedDescendingCellStyle BackColor="#E9EBEF" />
                       <SortedDescendingHeaderStyle BackColor="#4870BE" />
               </asp:GridView>
            </asp:Panel>
        </div>


         <div id ="divdetbtn"  runat="server"  style="float: left; width: 100%; height: 24px; text-align :right">
                                <asp:Button ID="btnClose" runat="server" Text="Close details"  Height="22px" 
                                Width="112px" 
                                BorderStyle="Solid" CssClass = "Button" onclick="btnClose_Click" />
         </div>

        <div id ="divDetails"  runat="server"  style="float: left; width: 100%; height: 165px; padding-top: 2px; padding-bottom: 2px;
                                background-color: #FFFFFF;">
             <asp:Panel ID="pnlReqDetails" runat="server" Height="140px" ScrollBars="Auto" BorderColor="#9F9F9F"
                                    BorderWidth="0px" Font-Bold="true" Width="100%">
                         <asp:GridView ID="dgvPendingDetails" runat="server" AutoGenerateColumns="False" 
                                Font-Bold="False" style="margin-top: 0px"  
                                GridLines ="None" RowStyle-Height="10px" Width="100%" CellPadding="4" 
                             ForeColor="#333333" CssClass = "GridView">
                             <EditRowStyle BackColor="#2461BF" />
                             <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                         <HeaderStyle BackColor="#507CD1" ForeColor="White" Font-Size="11px" Height="11px" 
                                 Font-Bold="True" />
                             <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                         <RowStyle Font-Size="11px" BackColor="#EFF3FB"/>
                             <AlternatingRowStyle BackColor="White" />
                          <Columns>
                                <asp:BoundField DataField='grad_ref' HeaderText='Ref. #' ItemStyle-Width="8%" 
                                    HeaderStyle-HorizontalAlign="Left"  >
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle Width="8%" />
                                </asp:BoundField>
                                <asp:BoundField DataField='grad_line' HeaderText='Seq. #'  ItemStyle-Width="3%" 
                                    HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right">
                                <HeaderStyle HorizontalAlign="Right" />
                                <ItemStyle Width="3%" />
                                </asp:BoundField>
                                <asp:BoundField DataField='grad_val1' HeaderText='Rate'  ItemStyle-Width="5%" 
                                    HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign ="Right">
                                <HeaderStyle HorizontalAlign="Right" />
                                <ItemStyle Width="5%" />
                                </asp:BoundField>
                                <asp:BoundField DataField='grad_anal1' HeaderText='Item'  ItemStyle-Width="10%" 
                                    HeaderStyle-HorizontalAlign="Left"  >
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle Width="10%" />
                                </asp:BoundField>
                                <asp:BoundField DataField='grad_anal2' HeaderText='Customer'  
                                    ItemStyle-Width="10%" HeaderStyle-HorizontalAlign="Left"  >
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle Width="10%" />
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
    </ContentTemplate>
</asp:UpdatePanel>
</asp:content>

