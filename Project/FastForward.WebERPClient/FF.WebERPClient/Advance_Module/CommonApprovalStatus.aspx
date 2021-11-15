<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="CommonApprovalStatus.aspx.cs" Inherits="FF.WebERPClient.Advance_Module.CommonApprovalStatus" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
<style type="text/css">
   .MainDivCss
        {
            float:left;
            font-family: Verdana;
            font-size: 11px;
        }

</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div style="float: left; width: 3%">
        &nbsp;
    </div>
    <div style="float: left; width: 94%">
        <asp:TabContainer ID="tbcApprovalStatus" runat="server" ActiveTabIndex="0" Height="450px">
            <asp:TabPanel ID="tbpInventory" runat="server">
                <ContentTemplate>
                <div class="MainDivCss">
                    <div style="float: left; width: 100%">
                    <br />
                        <asp:Label ID="lblMRN" runat="server" Font-Bold="True"></asp:Label>
                    </div>
                    <div style="float: left; width: 100%">
                        <asp:GridView ID="gvMRN" runat="server" AutoGenerateColumns="False" 
                            CellPadding="4" ForeColor="#333333" GridLines="None">
                            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                            <Columns>
                                <asp:TemplateField HeaderText="DocumentNo">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDocumentNo" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "DocumentNo") %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" Width="150px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Remarks">
                                    <ItemTemplate>
                                        <asp:Label ID="lblRemarks" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Remarks") %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" Width="400px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Approve">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="imgbtnApprove" runat="server" ImageUrl="~/Images/approve_img.png"
                                            CommandName="ApproveRequest" />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" Width="75px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Reject">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="imgbtnReject" runat="server" ImageUrl="~/Images/icon_reject2.PNG"
                                            CommandName="RejectRequest" />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" Width="75px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="View Details">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="imgbtnviewDetails" runat="server" ImageUrl="~/Images/icon_reject2.PNG"
                                            CommandName="ViewDeatils" />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" Width="75px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Approval History">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="imgbtnApprovalHistory" runat="server" ImageUrl="~/Images/reject icon.jpg"
                                            CommandName="ApprovalHistory" />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" Width="100px" />
                                </asp:TemplateField>
                            </Columns>
                            <EditRowStyle BackColor="#999999" />
                            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                            <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                            <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                            <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                            <SortedAscendingCellStyle BackColor="#E9E7E2" />
                            <SortedAscendingHeaderStyle BackColor="#506C8C" />
                            <SortedDescendingCellStyle BackColor="#FFFDF8" />
                            <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                        </asp:GridView>
                        <br />
                    </div>
                    <hr />
                    <div style="float: left; width: 100%">
                    <br />
                        <asp:Label ID="lblINTR" runat="server" Font-Bold="True"></asp:Label></div>
                    <div style="float: left; width: 100%">
                        <asp:GridView ID="gvINTR" runat="server" AutoGenerateColumns="False" CellPadding="4"
                            ForeColor="#333333" GridLines="None">
                            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                            <Columns>
                                <asp:TemplateField HeaderText="DocumentNo">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDocumentNo" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "DocumentNo") %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" Width="150px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Remarks">
                                    <ItemTemplate>
                                        <asp:Label ID="lblRemarks" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Remarks") %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" Width="400px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Approve">
                                    <ItemTemplate>
                                        <asp:Button ID="btnApprove" runat="server" Text="Approve" />
                                      <%--  <asp:ImageButton ID="imgbtnApprove" runat="server" ImageUrl="~/Images/approve_img.png" CommandName="ApproveRequest" />--%>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" Width="75px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Reject">
                                    <ItemTemplate>
                                        <asp:Button ID="btnReject" runat="server" Text="Reject" />
                                       <%-- <asp:ImageButton ID="imgbtnReject" runat="server" ImageUrl="~/Images/icon_reject2.PNG" CommandName="RejectRequest" />--%>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" Width="75px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="View Details">
                                    <ItemTemplate>
                                        <asp:Button ID="btnView" runat="server" Text="Details" />
                                       <%-- <asp:ImageButton ID="imgbtnviewDetails" runat="server" ImageUrl="~/Images/icon_reject2.PNG" CommandName="ViewDeatils" />--%>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" Width="75px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Approval History">
                                    <ItemTemplate>
                                    <asp:Button ID="btnAppHistory" runat="server" Text="Approval History" />
                                    <%--<asp:ImageButton ID="imgbtnApprovalHistory" runat="server" ImageUrl="~/Images/reject icon.jpg" CommandName="ApprovalHistory" />--%>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" Width="100px" />
                                </asp:TemplateField>
                            </Columns>
                            <EditRowStyle BackColor="#999999" />
                            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                            <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                            <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                            <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                            <SortedAscendingCellStyle BackColor="#E9E7E2" />
                            <SortedAscendingHeaderStyle BackColor="#506C8C" />
                            <SortedDescendingCellStyle BackColor="#FFFDF8" />
                            <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                        </asp:GridView>
                        <br />
                    </div>
                </div>
                </ContentTemplate>
            </asp:TabPanel>
            <asp:TabPanel ID="tbpSales" runat="server">
                <ContentTemplate>
                    Sales Data
                </ContentTemplate>
            </asp:TabPanel>
            <asp:TabPanel ID="tbpHirePurchase" runat="server">
                <ContentTemplate>
                    HP
                </ContentTemplate>
            </asp:TabPanel>
        </asp:TabContainer>
    </div>
    <div style="float: left; width: 3%">
        &nbsp;
    </div>
</asp:Content>
