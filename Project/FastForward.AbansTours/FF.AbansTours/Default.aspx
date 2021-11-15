<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="Default.aspx.cs" Inherits="FF.AbansTours._Default" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
        <ContentTemplate>
            <div class="row rowmargin0 col-md-12">
                <div class="panel panel-default">
                    <div class="panel-heading pannelheading">
                        Customer Enquiry Tracker
                    </div>
                    <div class="panel-body" style="padding-left: 0px; padding-right: 0px;">
                        <div class="row rowmargin0 col-md-12" style="padding-left: 8px; padding-right: 8px;">
                            <div class="row rowmargin0 col-md-2">
                                Filter By
                                     <asp:DropDownList ID="ddlFilter" AutoPostBack="true" runat="server" OnSelectedIndexChanged="ddlFilter_SelectedIndexChanged">
                                         <asp:ListItem Text="Customer Code" Value="C" />
                                         <asp:ListItem Text="System ID" Value="E" />
                                         <asp:ListItem Text="Reference" Value="R" />
                                         <asp:ListItem Text="PC" Value="P" />
                                         <asp:ListItem Text="Status" Value="S" />
                                     </asp:DropDownList>
                            </div>
                            <div class="row rowmargin0 col-md-1" style="padding-left: 0px; padding-right: 0px;">
                                <asp:Label ID="lblvalue" runat="server" Text="Customer Code"></asp:Label>
                            </div>
                            <div class="row rowmargin0 col-md-3" style="padding-left: 0px; padding-right: 0px;">
                                <asp:TextBox ID="txtFilter" runat="server" />
                                <asp:ImageButton ID="btnCustomer" runat="server" ImageUrl="../Images/icon_search.png"
                                    ImageAlign="Middle" OnClick="btnCustomer_Click" />
                                <asp:ImageButton ID="btnEnquiryID" Visible="false" runat="server" ImageUrl="~/Images/icon_search.png"
                                    ImageAlign="Middle" OnClick="btnEnquiryID_Click" />
                                <asp:ImageButton ID="btnPc" Visible="false" runat="server" ImageUrl="~/Images/icon_search.png"
                                    ImageAlign="Middle" OnClick="lblPC_Click" />
                            </div>
                            <div class="row rowmargin0 col-md-3" style="padding-left: 8px; padding-right: 8px;">
                                <asp:Button ID="btnSearch" Text="Search" Width="80px" runat="server" OnClick="btnSearch_Click" />
                            </div>
                        </div>
                        <div class="row" style="height: 5px;">
                        </div>
                        <div class="row rowmargin0 col-md-12" style="padding-left: 8px; padding-right: 8px;">
                            <asp:GridView ID="dgvHistry" runat="server" AutoGenerateColumns="False" Font-Size="X-Small"
                                OnRowCommand="dgvHistry_RowCommand" Style="padding: 2px 2px;" AllowPaging="true" PageSize="15" OnPageIndexChanging="dgvHistry_PageIndexChanging" OnRowDataBound="dgvHistry_RowDataBound" OnSelectedIndexChanged="dgvHistry_SelectedIndexChanged">
                                <Columns>
                                    <asp:CommandField ShowSelectButton="true" ButtonType="Link" SelectText="" />
                                    <asp:TemplateField HeaderText="Customer Code">
                                        <ItemTemplate>
                                            <asp:Label ID="lblCustCode" runat="server" Text='<%# Bind("gce_cus_cd") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Width="85px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="System ID">
                                        <ItemTemplate>
                                            <asp:Label ID="lblEnquiryID" runat="server" Text='<%# Bind("gce_enq_id") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Width="105px" />
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="gce_ref" HeaderText=" Manual Ref."></asp:BoundField>
                                    <%-- <asp:BoundField DataField="met_desc" HeaderText="Request Type">

                                        <HeaderStyle Width="60px" />
                                    </asp:BoundField>--%>
                                    <asp:TemplateField HeaderText="Request Type">
                                        <ItemTemplate>
                                            <asp:Label ID="met_desc" runat="server" Text='<%# Bind("met_desc") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Width="60px" />
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="gce_enq_pc_desc" HeaderText="Request From">
                                        <HeaderStyle Width="145px" />
                                    </asp:BoundField>
                                    <asp:TemplateField HeaderText="Requested Date">
                                        <ItemTemplate>
                                            <asp:Label ID="lblDate" runat="server" Text='<%# Bind("gce_dt", "{0:dd/MM/yyyy}") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="gce_name" HeaderText="Name">
                                        <%-- <HeaderStyle Width="200px" />--%>
                                        <HeaderStyle Width="150px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="gce_mob" HeaderText="Mobile" />
                                    <asp:TemplateField HeaderText="Expected Date">
                                        <ItemTemplate>
                                            <asp:Label ID="lblExpectedDate" runat="server" Text='<%# Bind("gce_expect_dt") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="PendingDate" HeaderText="Pending Days" />
                                    <asp:TemplateField HeaderText="Enquiry">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="btnViewEnquiry" runat="server" CommandArgument="<%# Container.DisplayIndex %>"
                                                CommandName="ViewEnquiry" ImageUrl="~/images/Details.png" ToolTip="Enquiry.."
                                                ImageAlign="Middle" />
                                        </ItemTemplate>
                                        <HeaderStyle CssClass="bodycolordarkgreen" HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Status">
                                        <ItemTemplate>
                                            <asp:Label ID="lblstatus" runat="server" Text='<%# Bind("mes_desc") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Width="65px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText=" Go to Costing">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="btnCosting" runat="server" CommandArgument="<%# Container.DisplayIndex %>"
                                                CommandName="Costing" ImageUrl="~/images/money.png" ToolTip="Costing.." ImageAlign="Middle" />
                                        </ItemTemplate>
                                        <HeaderStyle CssClass="bodycolordarkgreen" HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Go to Invoice">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="btnVInvoice" runat="server" CommandArgument="<%# Container.DisplayIndex %>"
                                                CommandName="Invoice" ImageUrl="~/images/Invoice.png" ToolTip="Invoice.." ImageAlign="Middle" />
                                        </ItemTemplate>
                                        <HeaderStyle CssClass="bodycolordarkgreen" HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" />
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
                                    <asp:TemplateField HeaderText="main" Visible="False">
                                        <ItemTemplate>
                                            <asp:Label ID="gce_mainreqid" runat="server" Text='<%# Bind("gce_mainreqid") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Size="Small" />
                            </asp:GridView>
                        </div>
                    </div>
                </div>
            </div>
            <div style="float: left; width: 100%; text-align: left">
                <div id="divColors" style="float: left; width: 100%; text-align: left" runat="server">
                    <div style="float: left; width: 10%;">
                        &nbsp;
                    </div>
                    <div style="float: left; width: 2%; background-color: LAVENDER;">
                        &nbsp;
                    </div>
                    <div style="float: left; width: 40%;">
                        Pending to Next Stage
                    </div>
                </div>
                <asp:Panel ID="pnlEnquiry" runat="server" Width="50%" Height="30%" Style="display: none">
                    <table cellpadding="3" cellspacing="1" class="tableborder" width="100%" style="background-color: SlateGray">
                        <tr>
                            <td height="22px" align="center" class="bodycolorlightgreen" width="100%">Enquiry Details
                            </td>
                        </tr>
                        <tr>
                            <td align="center" class="bodycolorlightgreen" width="100%">
                                <asp:TextBox ID="txtEnquiry" runat="server" AutoPostBack="true" Width="100%" Height="100px"
                                    Rows="100" TextMode="MultiLine" ReadOnly="true"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td height="22px" align="center" class="bodycolorlightgreen" width="100%">
                                <asp:Button ID="btnClose" Text="Close" runat="server" OnClick="btnClose_Click" />
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                <asp:Button ID="btnEnquiry" runat="server" Text="D3" Style="display: none" />
                <asp:ModalPopupExtender ID="mpEnquiry" runat="server" DynamicServicePath="" Enabled="True"
                    PopupControlID="pnlEnquiry" TargetControlID="btnEnquiry" BackgroundCssClass="modalBackground"
                    PopupDragHandleControlID="pnlEnquiry">
                </asp:ModalPopupExtender>
            </div>
            <asp:Panel ID="pnlStatus" runat="server" Visible="true">
                <div class="col-md-12">
                    <asp:GridView ID="grdstatus" ShowHeaderWhenEmpty="True" GridLines="None" AutoGenerateColumns="False"  runat="server">
                        <Columns>
                            <asp:TemplateField >
                                <ItemTemplate>
                                    <asp:Label ID="met_desc" runat="server" Text='<%# Bind("met_desc") %>' Width="450px"></asp:Label>

                                </ItemTemplate>
                            </asp:TemplateField>
                             <asp:TemplateField >
                                <ItemTemplate>
                                    <asp:Label ID="met_desc" runat="server" Text='<%# Bind("mes_desc") %>' Width="150px"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
