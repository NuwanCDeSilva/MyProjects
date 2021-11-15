<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="Summery.aspx.cs" Inherits="FF.AbansTours.Summery" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <head id="Head1" runat="server" />
    .
</asp:Content>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div style="float: left; height: auto; width: 100%;">
        <asp:Panel ID="Panel3" runat="server" Width="100%" ScrollBars="Auto">
            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                <ContentTemplate>
                    <asp:GridView ID="dgvHistry" runat="server" AutoGenerateColumns="False" Font-Size="X-Small"
                        Style="padding: 2px 2px;">
                        <Columns>
                            <asp:TemplateField HeaderText="Customer Code">
                                <ItemTemplate>
                                    <asp:Label ID="lblCustCode" runat="server" Text='<%# Bind("gce_cus_cd") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Enquiry ID">
                                <ItemTemplate>
                                    <asp:Label ID="lblEnquiryID" runat="server" Text='<%# Bind("gce_enq_id") %>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle Width="180px" />
                            </asp:TemplateField>
                            <asp:BoundField DataField="gce_ref" HeaderText="Reference" HeaderStyle-Width="150">
                                <HeaderStyle Width="150px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="met_desc" HeaderText="Type" HeaderStyle-Width="180">
                                <HeaderStyle Width="180px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="gce_enq_pc_desc" HeaderText="PC" />
                            <asp:TemplateField HeaderText="Date">
                                <ItemTemplate>
                                    <asp:Label ID="lblDate" runat="server" Text='<%# Bind("gce_dt", "{0:dd/MM/yyyy}") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="gce_name" HeaderText="Name" HeaderStyle-Width="250">
                                <HeaderStyle Width="250px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="gce_mob" HeaderText="Mobile" />
                            <asp:TemplateField HeaderText="Expected Date">
                                <ItemTemplate>
                                    <asp:Label ID="lblExpectedDate" runat="server" Text='<%# Bind("gce_expect_dt") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("gce_expect_dt") %>'></asp:TextBox>
                                </EditItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="PendingDate" HeaderText="Pending Dates" />
                            <asp:TemplateField HeaderText="Enquiry">
                                <ItemTemplate>
                                    <asp:ImageButton ID="btnViewEnquiry" runat="server" CommandArgument="<%# Container.DisplayIndex %>"
                                        CommandName="ViewEnquiry" ImageUrl="~/images/Details.png" ToolTip="Enquiry.."
                                        ImageAlign="Middle" />
                                </ItemTemplate>
                                <HeaderStyle CssClass="bodycolordarkgreen" HorizontalAlign="Center" Width="2%" />
                                <ItemStyle HorizontalAlign="Center" Width="2%" />
                            </asp:TemplateField>
                            <asp:BoundField DataField="mes_desc" HeaderText="Status" />
                            <asp:TemplateField HeaderText="Costing">
                                <ItemTemplate>
                                    <asp:ImageButton ID="btnCosting" runat="server" CommandArgument="<%# Container.DisplayIndex %>"
                                        CommandName="Costing" ImageUrl="~/images/cost.png" ToolTip="Costing.." ImageAlign="Middle" />
                                </ItemTemplate>
                                <HeaderStyle CssClass="bodycolordarkgreen" HorizontalAlign="Center" Width="2%" />
                                <ItemStyle HorizontalAlign="Center" Width="2%" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Invoice">
                                <ItemTemplate>
                                    <asp:ImageButton ID="btnVInvoice" runat="server" CommandArgument="<%# Container.DisplayIndex %>"
                                        CommandName="Invoice" ImageUrl="~/images/Invoice.png" ToolTip="Invoice.." ImageAlign="Middle" />
                                </ItemTemplate>
                                <HeaderStyle CssClass="bodycolordarkgreen" HorizontalAlign="Center" Width="2%" />
                                <ItemStyle HorizontalAlign="Center" Width="2%" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Enquiry" Visible="False">
                                <ItemTemplate>
                                    <asp:Label ID="lblEnquiry" Visible="false" runat="server" Text='<%# Bind("gce_enq") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="IsLateToNextStage" Visible="False">
                                <ItemTemplate>
                                    <asp:Label ID="IsLateToNextStage" runat="server" Text='<%# Bind("IsLateToNextStage") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <HeaderStyle Font-Bold="False" Font-Italic="False" Wrap="False" />
                    </asp:GridView>
                </ContentTemplate>
            </asp:UpdatePanel>
        </asp:Panel>
    </div>
</asp:Content>
