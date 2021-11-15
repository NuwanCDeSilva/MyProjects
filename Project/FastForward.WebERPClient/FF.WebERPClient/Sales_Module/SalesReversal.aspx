<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="SalesReversal.aspx.cs" Inherits="FF.WebERPClient.Sales_Module.SalesReversal" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="../UserControls/uc_HpAccountSummary.ascx" TagName="uc_HpAccountSummary"
    TagPrefix="uc1" %>
<%@ Register Src="../UserControls/uc_HpAccountDetail.ascx" TagName="uc_HpAccountDetail"
    TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script type="text/javascript">
        function clickButton(e, buttonid) {
            var evt = e ? e : window.event;
            var bt = document.getElementById(buttonid);
            if (bt) {
                if (evt.keyCode == 113) {
                    bt.click();
                    return false;
                }

            }
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


        function uppercase() {
            key = window.event.keyCode;
            if ((key > 0x60) && (key < 0x7B))
                window.event.keyCode = key - 0x20;
        }

    </script>
    <style type="text/css">
        .Textbox
        {
            margin-left: 3px;
        }
        .GridView
        {
            margin-top: 0px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="updtMainPnl" runat="server">
        <ContentTemplate>
            <div>
                <asp:Panel ID="Panel_tabs" runat="server">
                    <asp:TabContainer ID="Tab1" runat="server" ActiveTabIndex="1" Style="text-align: left"
                        Width="98%" Font-Bold="False">
                        <asp:TabPanel ID="tbInvRev" runat="server" HeaderText="Sales reversal" Width="100%"
                            Font-Bold="False">
                            <HeaderTemplate>
                                Sales Reversal
                            </HeaderTemplate>
                            <ContentTemplate>
                                <asp:Panel ID="Panel_generalInfo" runat="server">
                                    <div style="float: left; width: 100%;">
                                        <div style="float: left; width: 100%; height: 24px; text-align: right; border-style: none">
                                            <asp:Button ID="btnSave" runat="server" Text="Save" Height="85%" Width="70px" CssClass="Button"
                                                TabIndex="5" OnClick="btnSave_Click" />
                                            <asp:Button ID="btnClear" runat="server" Text="Clear" Height="85%" Width="70px" CssClass="Button"
                                                TabIndex="6" OnClick="btnClear_Click" />
                                            <asp:Button ID="btnClose" runat="server" Text="Close" Height="85%" Width="70px" CssClass="Button"
                                                OnClick="btnClose_Click" />
                                        </div>
                                        <div style="float: left; width: 100%;" id="divHeader">
                                            <asp:Label ID="lblfdate" runat="server" Text="From Date" CssClass="Textbox" Width="8%"></asp:Label>
                                            <asp:TextBox ID="txtfDate" runat="server" CssClass="TextBox" Width="10%"></asp:TextBox>
                                            <asp:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtfDate"
                                                Format="dd/MM/yyyy" Enabled="True">
                                            </asp:CalendarExtender>
                                            <asp:Label ID="lbltdate" runat="server" Text="To Date" CssClass="Textbox" Width="8%"></asp:Label>
                                            <asp:TextBox ID="txttDate" runat="server" CssClass="TextBox" Width="10%"></asp:TextBox>
                                            <asp:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txttDate"
                                                Format="dd/MM/yyyy" Enabled="True">
                                            </asp:CalendarExtender>
                                            <asp:Label ID="lblcus" runat="server" Text="Customer" CssClass="Textbox" Width="8%"></asp:Label>
                                            <asp:TextBox ID="txtCus" runat="server" CssClass="TextBox" Width="10%"></asp:TextBox>
                                            <asp:ImageButton ID="imgcus" runat="server" ImageUrl="~/Images/icon_search.png" OnClick="imgcus_Click"
                                                TabIndex="7" />
                                            <asp:Label ID="lblinv" runat="server" Text="Invoice #" CssClass="Textbox" Width="8%"></asp:Label>
                                            <asp:TextBox ID="txtInv" runat="server" CssClass="TextBox" Width="10%"></asp:TextBox>
                                            <asp:ImageButton ID="imginv" runat="server" ImageUrl="~/Images/icon_search.png" OnClick="imginv_Click"
                                                TabIndex="8" />
                                            <asp:Button ID="btnSearch" runat="server" Text="Search" Height="3%" Width="70px"
                                                CssClass="Button" OnClick="btnSearch_Click" />
                                        </div>
                                        <div style="float: left; width: 100%; height: 10px;">
                                        </div>
                                        <div style="float: left; width: 100%; height: 150px;">
                                            <asp:Panel ID="pnlItem" runat="server" Height="130px" ScrollBars="Auto" BorderWidth="0px"
                                                Width="100%">
                                                <asp:GridView ID="gvInvoice" runat="server" AutoGenerateColumns="False" Font-Bold="False"
                                                    Width="100%" EmptyDataText="No data found" ShowHeaderWhenEmpty="True" CellPadding="4"
                                                    ForeColor="#333333" GridLines="None" CssClass="GridView" OnRowCommand="gvInvItems_Rowcommand"
                                                    DataKeyNames="sah_com,sah_pc,sah_tp,sah_inv_tp,sah_inv_sub_tp,sah_cus_name,sah_cus_add1,sah_cus_add2,sah_d_cust_cd,sah_d_cust_add1,sah_d_cust_add2,sah_ex_rt,sah_tax_inv">
                                                    <AlternatingRowStyle BackColor="White" />
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Select">
                                                            <ItemTemplate>
                                                                <asp:CheckBox ID="chkSelect" runat="server" Checked="false" Enabled="false" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="sah_seq_no" Visible="False" />
                                                        <asp:BoundField DataField="sah_com" Visible="False" />
                                                        <asp:BoundField DataField="sah_pc" Visible="False" />
                                                        <asp:BoundField DataField="sah_tp" Visible="False" />
                                                        <asp:BoundField DataField="sah_inv_tp" HeaderText="Type">
                                                            <HeaderStyle HorizontalAlign="Left" Width="10%" />
                                                            <ItemStyle HorizontalAlign="Left" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="sah_inv_sub_tp" Visible="False" />
                                                        <asp:BoundField DataField="sah_inv_no" HeaderText="Invoice #">
                                                            <HeaderStyle HorizontalAlign="Left" Width="20%" />
                                                            <ItemStyle HorizontalAlign="Left" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="sah_dt" HeaderText="Date" DataFormatString="{0:d}">
                                                            <HeaderStyle HorizontalAlign="Left" Width="10%" />
                                                            <ItemStyle HorizontalAlign="Left" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="sah_manual" Visible="False" />
                                                        <asp:BoundField DataField="sah_man_ref" HeaderText="Reference #">
                                                            <HeaderStyle HorizontalAlign="Left" Width="18%" />
                                                            <ItemStyle HorizontalAlign="Left" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="sah_sales_ex_cd" HeaderText="Excecutive">
                                                            <HeaderStyle HorizontalAlign="Left" Width="12%" />
                                                            <ItemStyle HorizontalAlign="Left" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="sah_ref_doc" Visible="False" />
                                                        <asp:BoundField DataField="sah_cus_cd" HeaderText="Customer">
                                                            <HeaderStyle HorizontalAlign="Left" Width="15%" />
                                                            <ItemStyle HorizontalAlign="Left" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="sah_cus_name" Visible="False" />
                                                        <asp:BoundField DataField="sah_cus_add1" Visible="False" />
                                                        <asp:BoundField DataField="sah_cus_add2" Visible="False" />
                                                        <asp:BoundField DataField="sah_currency" HeaderText="Currency">
                                                            <HeaderStyle HorizontalAlign="Left" Width="10%" />
                                                            <ItemStyle HorizontalAlign="Left" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="sah_ex_rt" Visible="False" />
                                                        <asp:BoundField DataField="sah_town_cd" Visible="False" />
                                                        <asp:BoundField DataField="sah_d_cust_cd" Visible="False" />
                                                        <asp:BoundField DataField="sah_d_cust_add1" Visible="False" />
                                                        <asp:BoundField DataField="sah_d_cust_add2" Visible="False" />
                                                        <asp:BoundField DataField="sah_man_cd" Visible="False" />
                                                        <asp:BoundField DataField="sah_sales_str_cd" Visible="False" />
                                                        <asp:BoundField DataField="sah_sales_sbu_cd" Visible="False" />
                                                        <asp:BoundField DataField="sah_sales_sbu_man" Visible="False" />
                                                        <asp:BoundField DataField="sah_sales_chn_cd" Visible="False" />
                                                        <asp:BoundField DataField="sah_sales_chn_man" Visible="False" />
                                                        <asp:BoundField DataField="sah_sales_region_cd" Visible="False" />
                                                        <asp:BoundField DataField="sah_sales_region_man" Visible="False" />
                                                        <asp:BoundField DataField="sah_sales_zone_cd" Visible="False" />
                                                        <asp:BoundField DataField="sah_sales_zone_man" Visible="False" />
                                                        <asp:BoundField DataField="sah_structure_seq" Visible="False" />
                                                        <asp:BoundField DataField="sah_esd_rt" Visible="False" />
                                                        <asp:BoundField DataField="sah_wht_rt" Visible="False" />
                                                        <asp:BoundField DataField="sah_epf_rt" Visible="False" />
                                                        <asp:BoundField DataField="sah_pdi_req" Visible="False" />
                                                        <asp:BoundField DataField="sah_remarks" Visible="False" />
                                                        <asp:BoundField DataField="sah_is_acc_upload" Visible="False" />
                                                        <asp:BoundField DataField="sah_stus" Visible="False" />
                                                        <asp:BoundField DataField="sah_cre_by" Visible="False" />
                                                        <asp:BoundField DataField="sah_cre_when" Visible="False" />
                                                        <asp:BoundField DataField="sah_mod_by" Visible="False" />
                                                        <asp:BoundField DataField="sah_mod_when" Visible="False" />
                                                        <asp:BoundField DataField="sah_session_id" Visible="False" />
                                                        <asp:BoundField DataField="sah_anal_1" Visible="False" />
                                                        <asp:BoundField DataField="sah_anal_2" Visible="False" />
                                                        <asp:BoundField DataField="sah_anal_3" Visible="False" />
                                                        <asp:BoundField DataField="sah_anal_4" Visible="False" />
                                                        <asp:BoundField DataField="sah_anal_5" Visible="False" />
                                                        <asp:BoundField DataField="sah_anal_6" Visible="False" />
                                                        <asp:BoundField DataField="sah_anal_7" Visible="False" />
                                                        <asp:BoundField DataField="sah_anal_8" Visible="False" />
                                                        <asp:BoundField DataField="sah_anal_9" Visible="False" />
                                                        <asp:BoundField DataField="sah_anal_10" Visible="False" />
                                                        <asp:BoundField DataField="sah_anal_11" Visible="False" />
                                                        <asp:BoundField DataField="sah_anal_12" Visible="False" />
                                                        <asp:BoundField DataField="sah_direct" Visible="False" />
                                                        <asp:BoundField DataField="sah_tax_inv" Visible="False" />
                                                        <asp:TemplateField HeaderText="Pick">
                                                            <ItemTemplate>
                                                                <asp:ImageButton ID="imgSelect" runat="server" CommandName="SELECT" ImageUrl="~/Images/download_arrow_icon.png"
                                                                    Height="15px" />
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Center" Width="20%" Height="15px" />
                                                        </asp:TemplateField>
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
                                    </div>
                                    <div style="float: left; width: 100%;">
                                        <asp:Label ID="lblEditItem" runat="server" Text="Item" CssClass="Textbox" Width="8%"></asp:Label>
                                        <asp:TextBox ID="txtEditItem" runat="server" CssClass="TextBox" Width="20%" ReadOnly="True"></asp:TextBox>
                                        <asp:Label ID="lblEditQty" runat="server" Text="Bal. Qty" CssClass="Textbox" Width="8%"></asp:Label>
                                        <asp:TextBox ID="txtEditQty" runat="server" Style="text-align: right" CssClass="TextBox"
                                            Width="10%" ReadOnly="True"></asp:TextBox>
                                        <asp:Label ID="lblRevQty" runat="server" Text="Rev. Qty" CssClass="Textbox" Width="8%"></asp:Label>
                                        <asp:TextBox ID="txtRevQty" runat="server" Style="text-align: right" CssClass="TextBox"
                                            Width="10%"></asp:TextBox>
                                        <asp:Button ID="btnEdit" runat="server" Text="Edit" Height="3%" Width="70px" CssClass="Button"
                                            OnClick="btnEdit_Click" />
                                        <div style="float: left; height: 5%;">
                                        </div>
                                        <asp:Panel ID="Panel1" runat="server" Height="210px" ScrollBars="Auto" BorderWidth="0px"
                                            Width="100%" Style="margin-top: 4px">
                                            <asp:GridView ID="gvInvItems" runat="server" AutoGenerateColumns="False" Font-Bold="False"
                                                Width="100%" EmptyDataText="No data found" ShowHeaderWhenEmpty="True" ForeColor="#333333"
                                                CssClass="GridView" OnRowCommand="gvInvItems_Rowcommand" OnRowDeleting="OnRemoveFromInvoiceItem"
                                                CellPadding="4">
                                                <AlternatingRowStyle BackColor="White" />
                                                <Columns>
                                                    <asp:BoundField DataField="sad_seq_no" Visible="False" />
                                                    <asp:BoundField DataField="sad_itm_line" HeaderText="Line">
                                                        <HeaderStyle HorizontalAlign="Left" Width="5%" />
                                                        <ItemStyle HorizontalAlign="Left" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="sad_inv_no" Visible="False" />
                                                    <asp:BoundField DataField="sad_itm_cd" HeaderText="Code">
                                                        <HeaderStyle HorizontalAlign="Left" Width="15%" />
                                                        <ItemStyle HorizontalAlign="Left" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="sad_itm_stus" HeaderText="Status">
                                                        <HeaderStyle HorizontalAlign="Left" Width="10%" />
                                                        <ItemStyle HorizontalAlign="Left" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="sad_itm_tp" Visible="False" />
                                                    <asp:BoundField DataField="sad_uom" Visible="False" />
                                                    <asp:BoundField DataField="sad_qty" HeaderText="Qty">
                                                        <HeaderStyle HorizontalAlign="Right" Width="10%" />
                                                        <ItemStyle HorizontalAlign="Right" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="sad_srn_qty" HeaderText="Rev. Qty">
                                                        <HeaderStyle HorizontalAlign="Right" Width="8%" />
                                                        <ItemStyle HorizontalAlign="Right" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="sad_do_qty" Visible="False" />
                                                    <asp:BoundField DataField="sad_unit_rt" HeaderText="Unit price">
                                                        <HeaderStyle HorizontalAlign="Right" Width="10%" />
                                                        <ItemStyle HorizontalAlign="Right" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="sad_unit_amt" HeaderText="Amount">
                                                        <HeaderStyle HorizontalAlign="Right" Width="10%" />
                                                        <ItemStyle HorizontalAlign="Right" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="sad_disc_rt" Visible="False" />
                                                    <asp:BoundField DataField="sad_disc_amt" HeaderText="Discount">
                                                        <HeaderStyle HorizontalAlign="Right" Width="10%" />
                                                        <ItemStyle HorizontalAlign="Right" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="sad_itm_tax_amt" HeaderText="Tax Amount">
                                                        <HeaderStyle HorizontalAlign="Right" Width="10%" />
                                                        <ItemStyle HorizontalAlign="Right" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="sad_tot_amt" HeaderText="Total">
                                                        <HeaderStyle HorizontalAlign="Right" Width="12%" />
                                                        <ItemStyle HorizontalAlign="Right" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="sad_pbook" Visible="False" />
                                                    <asp:BoundField DataField="sad_pb_lvl" Visible="False" />
                                                    <asp:BoundField DataField="sad_pb_price" Visible="False" />
                                                    <asp:BoundField DataField="sad_seq" Visible="False" />
                                                    <asp:BoundField DataField="sad_itm_seq" Visible="False" />
                                                    <asp:BoundField DataField="sad_warr_period" Visible="False" />
                                                    <asp:BoundField DataField="sad_warr_remarks" Visible="False" />
                                                    <asp:BoundField DataField="sad_is_promo" Visible="False" />
                                                    <asp:BoundField DataField="sad_promo_cd" Visible="False" />
                                                    <asp:BoundField DataField="sad_comm_amt" Visible="False" />
                                                    <asp:BoundField DataField="sad_alt_itm_cd" Visible="False" />
                                                    <asp:BoundField DataField="sad_alt_itm_desc" Visible="False" />
                                                    <asp:BoundField DataField="sad_print_stus" Visible="False" />
                                                    <asp:BoundField DataField="sad_res_no" Visible="False" />
                                                    <asp:BoundField DataField="sad_res_line_no" Visible="False" />
                                                    <asp:BoundField DataField="sad_job_no" Visible="False" />
                                                    <asp:BoundField DataField="sad_fws_ignore_qty" Visible="False" />
                                                    <asp:BoundField DataField="sad_warr_based" Visible="False" />
                                                    <asp:BoundField DataField="sad_merge_itm" Visible="False" />
                                                    <asp:BoundField DataField="sad_job_line" Visible="False" />
                                                    <asp:BoundField DataField="sad_outlet_dept" Visible="False" />
                                                    <asp:TemplateField>
                                                        <ItemTemplate>
                                                            <asp:ImageButton ID="imgEdit" runat="server" CommandName="CHANGE" ImageUrl="~/Images/EditIcon.png" />
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" Width="20%" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField>
                                                        <ItemTemplate>
                                                            <asp:ImageButton ID="imgDelete" runat="server" CommandName="Delete" ImageUrl="~/Images/Delete.png" />
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" Width="20%" />
                                                    </asp:TemplateField>
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
                                    <div style="float: left; width: 100%;">
                                    </div>
                                    <div style="float: left; width: 100%;">
                                        <asp:Label ID="lblRemarks" runat="server" Text="Remarks" CssClass="Textbox" Width="8%"></asp:Label>
                                        <asp:TextBox ID="txtRemarks" runat="server" CssClass="TextBox" Width="68%"></asp:TextBox>
                                        <asp:Label ID="lblTotal" runat="server" Text="Total" CssClass="Textbox" Width="10%"></asp:Label>
                                        <asp:TextBox ID="txttotal" runat="server" CssClass="TextBox" Width="10%" ReadOnly="True"
                                            Style="text-align: right"></asp:TextBox>
                                    </div>
                                </asp:Panel>
                            </ContentTemplate>
                        </asp:TabPanel>
                        <asp:TabPanel ID="tbHSRev" runat="server" HeaderText="Sales reversal" Width="100%"
                            Font-Bold="False">
                            <HeaderTemplate>
                                Hire Sales Reversal
                            </HeaderTemplate>
                            <ContentTemplate>
                                <asp:Panel ID="Panel2" runat="server">
                                    <div style="float: left; width: 100%;">
                                        <div style="float: left; width: 100%; height: 24px; text-align: right; border-style: none">
                                            <asp:Button ID="btnHSave" runat="server" Text="Save" Height="85%" Width="70px" CssClass="Button"
                                                OnClick="btnHSave_Click" /><asp:Button ID="btnHClear" runat="server" Text="Clear"
                                                    Height="85%" Width="70px" CssClass="Button" OnClick="btnHClear_Click" />
                                                    <asp:Button ID="btnHClose" runat="server" Text="Close" Height="85%" Width="70px" CssClass="Button"
                                                OnClick="btnHClose_Click" /></div>
                                        <div style="float: left; width: 100%; padding-top: 2px; padding-bottom: 2px;">
                                            <div style="height: 18px; background-color: #1E4A9F; color: White; width: 98%; float: left;
                                                margin-top: 6px;">
                                                Sales Details</div>
                                            <div style="float: left; margin-top: 6px;">
                                                <asp:Image runat="server" ID="Image2" ImageUrl="~/Images/16 X 16 DownArrow.jpg" ImageAlign="Right" /></div>
                                            <asp:CollapsiblePanelExtender ID="CollapsiblePanelExtender1" runat="server" TargetControlID="pnlHireSales"
                                                CollapsedSize="0" ExpandedSize="445" Collapsed="True" ExpandControlID="Image2"
                                                CollapseControlID="Image2" ImageControlID="Image2" ExpandedImage="~/Images/16 X 16 UpArrow.jpg"
                                                CollapsedImage="~/Images/16 X 16 DownArrow.jpg" Enabled="True">
                                            </asp:CollapsiblePanelExtender>
                                            <div style="width: 100%; float: left; padding-top: 2px; padding-bottom: 2px;">
                                                <asp:Panel runat="server" ID="pnlHireSales" Width="99.8%" BorderColor="#9F9F9F" BorderWidth="1px">
                                                    <div style="width: 100%; float: left; padding-top: 2px; padding-bottom: 2px;">
                                                        <asp:Label ID="Label1" runat="server" Text="From Date" CssClass="Textbox" Width="8%"></asp:Label><asp:TextBox
                                                            ID="txthfDate" runat="server" CssClass="TextBox" Width="8%"></asp:TextBox><asp:CalendarExtender
                                                                ID="CalendarExtender3" runat="server" TargetControlID="txthfDate" Format="dd/MM/yyyy"
                                                                Enabled="True">
                                                            </asp:CalendarExtender>
                                                        <asp:Label ID="Label2" runat="server" Text="To Date" CssClass="Textbox" Width="6%"></asp:Label><asp:TextBox
                                                            ID="txthtDate" runat="server" CssClass="TextBox" Width="8%"></asp:TextBox><asp:CalendarExtender
                                                                ID="CalendarExtender4" runat="server" TargetControlID="txthtDate" Format="dd/MM/yyyy"
                                                                Enabled="True">
                                                            </asp:CalendarExtender>
                                                        <asp:Label ID="Label3" runat="server" Text="Customer" CssClass="Textbox" Width="7%"></asp:Label><asp:TextBox
                                                            ID="txthCus" runat="server" CssClass="TextBox" Width="10%"></asp:TextBox><asp:ImageButton
                                                                ID="imgHCus" runat="server" ImageUrl="~/Images/icon_search.png" OnClick="imgHCus_Click" /><asp:Label
                                                                    ID="Label4" runat="server" Text="Invoice #" CssClass="Textbox" Width="7%"></asp:Label><asp:TextBox
                                                                        ID="txtHInv" runat="server" CssClass="TextBox" Width="10%"></asp:TextBox><asp:ImageButton
                                                                            ID="imgHInv" runat="server" ImageUrl="~/Images/icon_search.png" OnClick="imgHInv_Click" />
                                                        <asp:Label ID="Label5" runat="server" Text="Account #" Width="8%"></asp:Label>
                                                        <asp:TextBox ID="txtHAcc" runat="server" CssClass="TextBox" Width="10%"></asp:TextBox>
                                                        <asp:ImageButton ID="imgHAcc" runat="server" ImageUrl="~/Images/icon_search.png"
                                                            OnClick="imgHAcc_Click" />
                                                        <asp:Button ID="btnHSearch" runat="server" Text="Search" Height="3%" Width="50px"
                                                            CssClass="Button" OnClick="btnHSearch_Click" />
                                                    </div>
                                                    <div style="float: left; width: 100%; padding-top: 4px;">
                                                        <asp:Panel ID="Panel3" runat="server" ScrollBars="Auto" BorderWidth="1px" Width="99.5%"
                                                            Height="150px">
                                                            <asp:GridView ID="gvHInv" runat="server" AutoGenerateColumns="False" Font-Bold="False"
                                                                Width="99%" EmptyDataText="No data found" ShowHeaderWhenEmpty="True" CellPadding="4"
                                                                ForeColor="#333333" CssClass="GridView" DataKeyNames="sah_com,sah_pc,sah_tp,sah_inv_tp,sah_inv_sub_tp,sah_cus_name,sah_cus_add1,sah_cus_add2,sah_d_cust_cd,sah_d_cust_add1,sah_d_cust_add2,sah_ex_rt,sah_tax_inv,sah_man_ref"
                                                                OnRowCommand="gvHInv_Rowcommand">
                                                                <AlternatingRowStyle BackColor="White" />
                                                                <Columns>
                                                                    <asp:TemplateField HeaderText="Select">
                                                                        <ItemTemplate>
                                                                            <asp:CheckBox ID="chkHSelect" runat="server" Checked="false" Enabled="false" />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:BoundField DataField="sah_seq_no" Visible="False" />
                                                                    <asp:BoundField DataField="sah_com" Visible="False" />
                                                                    <asp:BoundField DataField="sah_pc" Visible="False" />
                                                                    <asp:BoundField DataField="sah_tp" Visible="False" />
                                                                    <asp:BoundField DataField="sah_inv_tp" HeaderText="Type">
                                                                        <HeaderStyle HorizontalAlign="Left" Width="10%" />
                                                                        <ItemStyle HorizontalAlign="Left" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="sah_inv_sub_tp" Visible="False" />
                                                                    <asp:BoundField DataField="sah_inv_no" HeaderText="Invoice #">
                                                                        <HeaderStyle HorizontalAlign="Left" Width="20%" />
                                                                        <ItemStyle HorizontalAlign="Left" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="sah_dt" HeaderText="Date" DataFormatString="{0:d}">
                                                                        <HeaderStyle HorizontalAlign="Left" Width="10%" />
                                                                        <ItemStyle HorizontalAlign="Left" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="sah_manual" Visible="False" />
                                                                    <asp:BoundField DataField="sah_anal_2" HeaderText="Account #">
                                                                        <HeaderStyle HorizontalAlign="Left" Width="18%" />
                                                                        <ItemStyle HorizontalAlign="Left" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="sah_sales_ex_cd" HeaderText="Excecutive">
                                                                        <HeaderStyle HorizontalAlign="Left" Width="12%" />
                                                                        <ItemStyle HorizontalAlign="Left" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="sah_ref_doc" Visible="False" />
                                                                    <asp:BoundField DataField="sah_cus_cd" HeaderText="Customer">
                                                                        <HeaderStyle HorizontalAlign="Left" Width="15%" />
                                                                        <ItemStyle HorizontalAlign="Left" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="sah_cus_name" Visible="False" />
                                                                    <asp:BoundField DataField="sah_cus_add1" Visible="False" />
                                                                    <asp:BoundField DataField="sah_cus_add2" Visible="False" />
                                                                    <asp:BoundField DataField="sah_currency" HeaderText="Currency">
                                                                        <HeaderStyle HorizontalAlign="Left" Width="8%" />
                                                                        <ItemStyle HorizontalAlign="Left" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="sah_ex_rt" Visible="False" />
                                                                    <asp:BoundField DataField="sah_town_cd" Visible="False" />
                                                                    <asp:BoundField DataField="sah_d_cust_cd" Visible="False" />
                                                                    <asp:BoundField DataField="sah_d_cust_add1" Visible="False" />
                                                                    <asp:BoundField DataField="sah_d_cust_add2" Visible="False" />
                                                                    <asp:BoundField DataField="sah_man_cd" Visible="False" />
                                                                    <asp:BoundField DataField="sah_sales_str_cd" Visible="False" />
                                                                    <asp:BoundField DataField="sah_sales_sbu_cd" Visible="False" />
                                                                    <asp:BoundField DataField="sah_sales_sbu_man" Visible="False" />
                                                                    <asp:BoundField DataField="sah_sales_chn_cd" Visible="False" />
                                                                    <asp:BoundField DataField="sah_sales_chn_man" Visible="False" />
                                                                    <asp:BoundField DataField="sah_sales_region_cd" Visible="False" />
                                                                    <asp:BoundField DataField="sah_sales_region_man" Visible="False" />
                                                                    <asp:BoundField DataField="sah_sales_zone_cd" Visible="False" />
                                                                    <asp:BoundField DataField="sah_sales_zone_man" Visible="False" />
                                                                    <asp:BoundField DataField="sah_structure_seq" Visible="False" />
                                                                    <asp:BoundField DataField="sah_esd_rt" Visible="False" />
                                                                    <asp:BoundField DataField="sah_wht_rt" Visible="False" />
                                                                    <asp:BoundField DataField="sah_epf_rt" Visible="False" />
                                                                    <asp:BoundField DataField="sah_pdi_req" Visible="False" />
                                                                    <asp:BoundField DataField="sah_remarks" Visible="False" />
                                                                    <asp:BoundField DataField="sah_is_acc_upload" Visible="False" />
                                                                    <asp:BoundField DataField="sah_stus" Visible="False" />
                                                                    <asp:BoundField DataField="sah_cre_by" Visible="False" />
                                                                    <asp:BoundField DataField="sah_cre_when" Visible="False" />
                                                                    <asp:BoundField DataField="sah_mod_by" Visible="False" />
                                                                    <asp:BoundField DataField="sah_mod_when" Visible="False" />
                                                                    <asp:BoundField DataField="sah_session_id" Visible="False" />
                                                                    <asp:BoundField DataField="sah_anal_1" Visible="False" />
                                                                    <asp:BoundField DataField="sah_man_ref" Visible="False" />
                                                                    <asp:BoundField DataField="sah_anal_3" Visible="False" />
                                                                    <asp:BoundField DataField="sah_anal_4" Visible="False" />
                                                                    <asp:BoundField DataField="sah_anal_5" Visible="False" />
                                                                    <asp:BoundField DataField="sah_anal_6" Visible="False" />
                                                                    <asp:BoundField DataField="sah_anal_7" Visible="False" />
                                                                    <asp:BoundField DataField="sah_anal_8" Visible="False" />
                                                                    <asp:BoundField DataField="sah_anal_9" Visible="False" />
                                                                    <asp:BoundField DataField="sah_anal_10" Visible="False" />
                                                                    <asp:BoundField DataField="sah_anal_11" Visible="False" />
                                                                    <asp:BoundField DataField="sah_anal_12" Visible="False" />
                                                                    <asp:BoundField DataField="sah_direct" Visible="False" />
                                                                    <asp:BoundField DataField="sah_tax_inv" Visible="False" />
                                                                    <asp:TemplateField HeaderText="Pick">
                                                                        <ItemTemplate>
                                                                            <asp:ImageButton ID="imgSelect" runat="server" CommandName="HSELECT" ImageUrl="~/Images/download_arrow_icon.png"
                                                                                Height="12px" />
                                                                        </ItemTemplate>
                                                                        <ItemStyle HorizontalAlign="Center" Width="10%" Height="12px" />
                                                                    </asp:TemplateField>
                                                                </Columns>
                                                                <EditRowStyle BackColor="#2461BF" />
                                                                <FooterStyle BackColor="#507CD1" Font-Bold="False" ForeColor="White" />
                                                                <HeaderStyle BackColor="#507CD1" Font-Bold="False" ForeColor="White" />
                                                                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                                                <RowStyle BackColor="#EFF3FB" />
                                                                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="False" ForeColor="#333333" />
                                                                <SortedAscendingCellStyle BackColor="#F5F7FB" />
                                                                <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                                                                <SortedDescendingCellStyle BackColor="#E9EBEF" />
                                                                <SortedDescendingHeaderStyle BackColor="#4870BE" />
                                                            </asp:GridView>
                                                        </asp:Panel>
                                                    </div>
                                                    <div style="float: left; width: 100%; padding-top: 4px;">
                                                        <asp:Panel ID="Panel4" runat="server" ScrollBars="Auto" BorderWidth="1px" Width="99.5%"
                                                            Height="165px">
                                                            <asp:GridView ID="gvHItems" runat="server" AutoGenerateColumns="False" Font-Bold="False"
                                                                Width="100%" EmptyDataText="No data found" ShowHeaderWhenEmpty="True" ForeColor="#333333"
                                                                CssClass="GridView" CellPadding="4">
                                                                <AlternatingRowStyle BackColor="White" />
                                                                <Columns>
                                                                    <asp:BoundField DataField="sad_seq_no" Visible="False" />
                                                                    <asp:BoundField DataField="sad_itm_line" HeaderText="Line">
                                                                        <HeaderStyle HorizontalAlign="Left" Width="5%" />
                                                                        <ItemStyle HorizontalAlign="Left" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="sad_inv_no" Visible="False" />
                                                                    <asp:BoundField DataField="sad_itm_cd" HeaderText="Code">
                                                                        <HeaderStyle HorizontalAlign="Left" Width="15%" />
                                                                        <ItemStyle HorizontalAlign="Left" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="sad_itm_stus" HeaderText="Status">
                                                                        <HeaderStyle HorizontalAlign="Left" Width="10%" />
                                                                        <ItemStyle HorizontalAlign="Left" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="sad_itm_tp" Visible="False" />
                                                                    <asp:BoundField DataField="sad_uom" Visible="False" />
                                                                    <asp:BoundField DataField="sad_qty" HeaderText="Qty">
                                                                        <HeaderStyle HorizontalAlign="Right" Width="10%" />
                                                                        <ItemStyle HorizontalAlign="Right" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="sad_srn_qty" Visible="False" />
                                                                    <asp:BoundField DataField="sad_do_qty" Visible="False" />
                                                                    <asp:BoundField DataField="sad_unit_rt" HeaderText="Unit price" DataFormatString="{0:F}">
                                                                        <HeaderStyle HorizontalAlign="Right" Width="10%" />
                                                                        <ItemStyle HorizontalAlign="Right" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="sad_unit_amt" HeaderText="Amount" DataFormatString="{0:F}">
                                                                        <HeaderStyle HorizontalAlign="Right" Width="10%" />
                                                                        <ItemStyle HorizontalAlign="Right" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="sad_disc_rt" Visible="False" />
                                                                    <asp:BoundField DataField="sad_disc_amt" HeaderText="Discount" DataFormatString="{0:F}">
                                                                        <HeaderStyle HorizontalAlign="Right" Width="10%" />
                                                                        <ItemStyle HorizontalAlign="Right" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="sad_itm_tax_amt" HeaderText="Tax" DataFormatString="{0:F}">
                                                                        <HeaderStyle HorizontalAlign="Right" Width="10%" />
                                                                        <ItemStyle HorizontalAlign="Right" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="sad_tot_amt" HeaderText="Total" DataFormatString="{0:F}">
                                                                        <HeaderStyle HorizontalAlign="Right" Width="12%" />
                                                                        <ItemStyle HorizontalAlign="Right" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="sad_pbook" Visible="False" />
                                                                    <asp:BoundField DataField="sad_pb_lvl" Visible="False" />
                                                                    <asp:BoundField DataField="sad_pb_price" Visible="False" />
                                                                    <asp:BoundField DataField="sad_seq" Visible="False" />
                                                                    <asp:BoundField DataField="sad_itm_seq" Visible="False" />
                                                                    <asp:BoundField DataField="sad_warr_period" Visible="False" />
                                                                    <asp:BoundField DataField="sad_warr_remarks" Visible="False" />
                                                                    <asp:BoundField DataField="sad_is_promo" Visible="False" />
                                                                    <asp:BoundField DataField="sad_promo_cd" Visible="False" />
                                                                    <asp:BoundField DataField="sad_comm_amt" Visible="False" />
                                                                    <asp:BoundField DataField="sad_alt_itm_cd" Visible="False" />
                                                                    <asp:BoundField DataField="sad_alt_itm_desc" Visible="False" />
                                                                    <asp:BoundField DataField="sad_print_stus" Visible="False" />
                                                                    <asp:BoundField DataField="sad_res_no" Visible="False" />
                                                                    <asp:BoundField DataField="sad_res_line_no" Visible="False" />
                                                                    <asp:BoundField DataField="sad_job_no" Visible="False" />
                                                                    <asp:BoundField DataField="sad_fws_ignore_qty" Visible="False" />
                                                                    <asp:BoundField DataField="sad_warr_based" Visible="False" />
                                                                    <asp:BoundField DataField="sad_merge_itm" Visible="False" />
                                                                    <asp:BoundField DataField="sad_job_line" Visible="False" />
                                                                    <asp:BoundField DataField="sad_outlet_dept" Visible="False" />
                                                                </Columns>
                                                                <EditRowStyle BackColor="#2461BF" />
                                                                <FooterStyle BackColor="#507CD1" Font-Bold="False" ForeColor="White" />
                                                                <HeaderStyle BackColor="#507CD1" Font-Bold="False" ForeColor="White" />
                                                                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                                                <RowStyle BackColor="#EFF3FB" />
                                                                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="False" ForeColor="#333333" />
                                                                <SortedAscendingCellStyle BackColor="#F5F7FB" />
                                                                <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                                                                <SortedDescendingCellStyle BackColor="#E9EBEF" />
                                                                <SortedDescendingHeaderStyle BackColor="#4870BE" />
                                                            </asp:GridView>
                                                        </asp:Panel>
                                                    </div>
                                                    <div style="float: right; width: 100%; padding-top: 4px;">
                                                        <div style="float: right; width: 100%; padding-top: 4px;">
                                                            <div style="float: right; width: 40%; padding-top: 1px; padding-bottom: 1px;">
                                                                <div style="float: right; width: 1%;">
                                                                    &nbsp;</div>
                                                                <div style="float: left; width: 40%; color: #000000;">
                                                                    Amount</div>
                                                                <div style="float: left; width: 1%; color: #000000;">
                                                                    :</div>
                                                                <div style="float: left; width: 25%; text-align: right;">
                                                                    <asp:Label ID="lblHAmt" runat="server" ForeColor="Black" Text="123456"></asp:Label>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div style="float: right; width: 100%; padding-top: 4px;">
                                                            <div style="float: right; width: 40%; padding-top: 1px; padding-bottom: 1px;">
                                                                <div style="float: right; width: 1%;">
                                                                    &nbsp;</div>
                                                                <div style="float: left; width: 40%; color: #000000;">
                                                                    Discount</div>
                                                                <div style="float: left; width: 1%; color: #000000;">
                                                                    :</div>
                                                                <div style="float: left; width: 25%; text-align: right;">
                                                                    <asp:Label ID="lblHdis" runat="server" ForeColor="Black" Text="123456"></asp:Label>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div style="float: right; width: 100%; padding-top: 4px;">
                                                            <div style="float: right; width: 40%; padding-top: 1px; padding-bottom: 1px;">
                                                                <div style="float: right; width: 1%;">
                                                                    &nbsp;</div>
                                                                <div style="float: left; width: 40%; color: #000000;">
                                                                    Tax</div>
                                                                <div style="float: left; width: 1%; color: #000000;">
                                                                    :</div>
                                                                <div style="float: left; width: 25%; text-align: right;">
                                                                    <asp:Label ID="lblHTax" runat="server" ForeColor="Black" Text="123456"></asp:Label>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div style="float: right; width: 100%; padding-top: 4px;">
                                                            <div style="float: right; width: 40%; padding-top: 1px; padding-bottom: 1px;">
                                                                <div style="float: right; width: 1%;">
                                                                    &nbsp;</div>
                                                                <div style="float: left; width: 40%; color: #000000;">
                                                                    Total Amount</div>
                                                                <div style="float: left; width: 1%; color: #000000;">
                                                                    :</div>
                                                                <div style="float: left; width: 25%; text-align: right;">
                                                                    <asp:Label ID="lblHtot" runat="server" ForeColor="Black" Text="123456"></asp:Label>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </asp:Panel>
                                            </div>
                                        </div>
                                        <div style="float: left; width: 100%; padding-top: 2px; padding-bottom: 2px;">
                                            <div style="height: 18px; background-color: #1E4A9F; color: White; width: 98%; float: left;
                                                margin-top: 6px;">
                                                Delivery details</div>
                                            <div style="float: left; margin-top: 6px;">
                                                <asp:Image runat="server" ID="Image3" ImageUrl="~/Images/16 X 16 DownArrow.jpg" ImageAlign="Right" /></div>
                                            <asp:CollapsiblePanelExtender ID="CollapsiblePanelExtender2" runat="server" TargetControlID="pnlDelivery"
                                                CollapsedSize="0" ExpandedSize="440" Collapsed="True" ExpandControlID="Image3"
                                                CollapseControlID="Image3" ImageControlID="Image3" ExpandedImage="~/Images/16 X 16 UpArrow.jpg"
                                                CollapsedImage="~/Images/16 X 16 DownArrow.jpg" Enabled="True">
                                            </asp:CollapsiblePanelExtender>
                                            <div style="width: 100%; float: left; padding-top: 2px; padding-bottom: 2px;">
                                                <asp:Panel runat="server" ID="pnlDelivery" Width="99.8%" BorderColor="#9F9F9F" BorderWidth="1px">
                                                    <div style="float: left; width: 100%; padding-top: 4px;">
                                                        <asp:Panel ID="Panel5" runat="server" ScrollBars="Auto" BorderWidth="1px" Width="99.5%"
                                                            Height="150px">
                                                            <asp:GridView ID="gvDelDetails" runat="server" AutoGenerateColumns="False" Width="99%"
                                                                EmptyDataText="No data found" CellPadding="2" ShowHeaderWhenEmpty="True" ForeColor="#333333"
                                                                CssClass="GridView">
                                                                <AlternatingRowStyle BackColor="White" />
                                                                <EditRowStyle BackColor="#2461BF" />
                                                                <EmptyDataTemplate>
                                                                    <div style="width: 100px; text-align: center;">
                                                                        No data found.
                                                                    </div>
                                                                </EmptyDataTemplate>
                                                                <Columns>
                                                                    <asp:BoundField DataField="tus_loc" HeaderText="Location">
                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                        <ItemStyle HorizontalAlign="Left" Width="8%" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="tus_doc_no" HeaderText="DO #">
                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                        <ItemStyle HorizontalAlign="Left" Width="15%" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="tus_base_doc_no" HeaderText="Invoice #">
                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                        <ItemStyle HorizontalAlign="Left" Width="10%" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="tus_doc_dt" HeaderText="DO Date" DataFormatString="{0:d}">
                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                        <ItemStyle HorizontalAlign="Left" Width="10%" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="tus_itm_cd" HeaderText="Item">
                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                        <ItemStyle HorizontalAlign="Left" Width="15%" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="tus_itm_stus" HeaderText="Status">
                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                        <ItemStyle HorizontalAlign="Left" Width="10%" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="tus_qty" HeaderText="Qty">
                                                                        <HeaderStyle HorizontalAlign="Right" />
                                                                        <ItemStyle HorizontalAlign="Right" Width="10%" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="tus_ser_1" HeaderText="Serial">
                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                        <ItemStyle HorizontalAlign="Left" Width="20%" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="tus_warr_no" HeaderText="Warranty">
                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                        <ItemStyle HorizontalAlign="Left" Width="20%" />
                                                                    </asp:BoundField>
                                                                </Columns>
                                                                <FooterStyle BackColor="#507CD1" Font-Bold="False" ForeColor="White" />
                                                                <HeaderStyle BackColor="#507CD1" Font-Bold="False" ForeColor="White" />
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
                                                </asp:Panel>
                                            </div>
                                        </div>
                                        <div style="float: left; width: 100%; padding-top: 2px; padding-bottom: 2px;">
                                            <div style="height: 18px; background-color: #1E4A9F; color: White; width: 98%; float: left;
                                                margin-top: 6px;">
                                                Account Details</div>
                                            <div style="float: left; margin-top: 6px;">
                                                <asp:Image runat="server" ID="Image4" ImageUrl="~/Images/16 X 16 DownArrow.jpg" ImageAlign="Right" /></div>
                                            <asp:CollapsiblePanelExtender ID="CollapsiblePanelExtender3" runat="server" TargetControlID="pnlAccSummary"
                                                CollapsedSize="0" ExpandedSize="440" Collapsed="True" ExpandControlID="Image4"
                                                CollapseControlID="Image4" ImageControlID="Image4" ExpandedImage="~/Images/16 X 16 UpArrow.jpg"
                                                CollapsedImage="~/Images/16 X 16 DownArrow.jpg" Enabled="True">
                                            </asp:CollapsiblePanelExtender>
                                            <div style="width: 100%; float: left; padding-top: 2px; padding-bottom: 2px;">
                                                <asp:Panel runat="server" ID="pnlAccSummary" Width="99.8%" BorderColor="#9F9F9F"
                                                    BorderWidth="1px">
                                                    <div style="float: left; width: 100%; padding-top: 3px;">
                                                        <uc1:uc_HpAccountSummary ID="uc_HpAccountSummary1" runat="server" />
                                                    </div>
                                                    <div style="float: left; width: 100%; padding-top: 3px;">
                                                        <uc2:uc_HpAccountDetail ID="uc_HpAccountDetail1" runat="server" />
                                                    </div>
                                                </asp:Panel>
                                            </div>
                                        </div>
                                    </div>
                                </asp:Panel>
                            </ContentTemplate>
                        </asp:TabPanel>
                        <contenttemplate>
                    </asp:TabContainer>
                </asp:Panel>
                <div style="display: none;">
                    <asp:Button ID="btnCustomer" runat="server" OnClick="LoadCustomerDetailsByCustomer" />
                    <asp:Button ID="btnInvoice" runat="server" OnClick="LoadInvoice" />
                    <asp:Button ID="btnHSales" runat="server" OnClick="LoadHireSales" />
                    <asp:Button ID="btnHAcc" runat="server" OnClick="CheckAccount" />
                    <asp:HiddenField ID="hdnAllowBin" runat="Server" Value="0" />
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
