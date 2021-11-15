<%@ Page Title="Receipt Refund" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="ReceiptReversal.aspx.cs" Inherits="FF.WebERPClient.Financial_Modules.ReceiptReversal" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
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
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="updtMainPnl" runat="server">
        <ContentTemplate>
            <div style="float: left; width: 100%;">
                <div>
                    <%--Button Panel--%>
                    <div style="float: left; width: 100%; height: 22px; text-align: right;">
                        <asp:Button ID="btnSave" runat="server" Text="Save" Height="100%" Width="70px" CssClass="Button"
                            OnClick="btnSave_Click" />
                        <asp:Button ID="btnClear" runat="server" Text="Clear" Height="100%" Width="70px"
                            CssClass="Button" OnClick="btnClear_Click" />
                        <asp:Button ID="btnPrint" runat="server" Text="Print" Height="100%" Width="70px"
                            CssClass="Button" />
                    </div>
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
                    <asp:Button ID="btnSearch" runat="server" Text="Search" Height="3%" Width="70px"
                        CssClass="Button" OnClick="btnSearch_Click" />
                </div>
                <div style="float: left; width: 100%; height: 10px;">
                </div>
                <div style="float: left; width: 100%; height: 175px; padding-top: 2px">
                    <asp:Panel ID="pnlReceipt" runat="server" Height="170px" ScrollBars="Auto" BorderWidth="0px"
                        Width="100%">
                        <asp:GridView ID="gvReceipt" runat="server" AutoGenerateColumns="False" Font-Bold="False"
                            Width="100%" EmptyDataText="No data found" ShowHeaderWhenEmpty="True" CellPadding="4"
                            ForeColor="#333333" GridLines="None" CssClass="GridView" OnRowCommand="gvReceipt_Rowcommand"
                            DataKeyNames="sar_receipt_type,sar_prefix,sar_debtor_add_1,sar_debtor_add_2,sar_tel_no,sar_mob_no,sar_nic_no,sar_comm_amt,sar_esd_rate,sar_wht_rate,sar_epf_rate,sar_remarks,sar_anal_1,sar_anal_2,sar_tot_settle_amt,sar_anal_3">
                            <AlternatingRowStyle BackColor="White" />
                            <Columns>
                                <asp:TemplateField HeaderText="Select">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkSelect" runat="server" Checked="false" Enabled="false" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="sar_seq_no" Visible="False" />
                                <asp:BoundField DataField="sar_com_cd" Visible="False" />
                                <asp:BoundField DataField="sar_receipt_type" Visible="False" />
                                <asp:BoundField DataField="sar_receipt_no" HeaderText="Receipt #">
                                    <HeaderStyle HorizontalAlign="Left" Width="10%" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="sar_prefix" Visible="False" />
                                <asp:BoundField DataField="sar_manual_ref_no" HeaderText="Reference #">
                                    <HeaderStyle HorizontalAlign="Left" Width="20%" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="sar_receipt_date" HeaderText="Date" DataFormatString="{0:d}">
                                    <HeaderStyle HorizontalAlign="Left" Width="10%" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="sar_direct" Visible="False" />
                                <asp:BoundField DataField="sar_acc_no" Visible="False" />
                                <asp:BoundField DataField="sar_is_oth_shop" Visible="False" />
                                <asp:BoundField DataField="sar_oth_sr" Visible="False" />
                                <asp:BoundField DataField="sar_profit_center_cd" Visible="False" />
                                <asp:BoundField DataField="sar_debtor_cd" HeaderText="Customer">
                                    <HeaderStyle HorizontalAlign="Left" Width="15%" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="sar_debtor_name" HeaderText="Name">
                                    <HeaderStyle HorizontalAlign="Left" Width="30%" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="sar_debtor_add_1" Visible="False" />
                                <asp:BoundField DataField="sar_debtor_add_2" Visible="False" />
                                <asp:BoundField DataField="sar_tel_no" Visible="False" />
                                <asp:BoundField DataField="sar_mob_no" Visible="False" />
                                <asp:BoundField DataField="sar_nic_no" Visible="False" />
                                <asp:BoundField DataField="sar_tot_settle_amt" HeaderText="Amount">
                                    <HeaderStyle HorizontalAlign="Right" Width="15%" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:BoundField>
                                <asp:BoundField DataField="sar_comm_amt" Visible="False" />
                                <asp:BoundField DataField="sar_is_mgr_iss" Visible="False" />
                                <asp:BoundField DataField="sar_esd_rate" Visible="False" />
                                <asp:BoundField DataField="sar_wht_rate" Visible="False" />
                                <asp:BoundField DataField="sar_epf_rate" Visible="False" />
                                <asp:BoundField DataField="sar_currency_cd" Visible="False" />
                                <asp:BoundField DataField="sar_uploaded_to_finance" Visible="False" />
                                <asp:BoundField DataField="sar_act" Visible="False" />
                                <asp:BoundField DataField="sar_direct_deposit_bank_cd" Visible="False" />
                                <asp:BoundField DataField="sar_direct_deposit_branch" Visible="False" />
                                <asp:BoundField DataField="sar_remarks" Visible="False" />
                                <asp:BoundField DataField="sar_ref_doc" Visible="False" />
                                <asp:BoundField DataField="sar_ser_job_no" Visible="False" />
                                <asp:BoundField DataField="sar_used_amt" Visible="False" />
                                <asp:BoundField DataField="sar_anal_1" Visible="False" />
                                <asp:BoundField DataField="sar_anal_2" Visible="False" />
                                 <asp:BoundField DataField="sar_anal_3" Visible="False" />
                                <asp:TemplateField HeaderText="Pick">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="imgSelect" runat="server" CommandName="SELECT" ImageUrl="~/Images/download_arrow_icon.png"
                                            Height="15px" />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" Width="10%" Height="15px" />
                                </asp:TemplateField>
                            </Columns>
                            <EditRowStyle BackColor="#2461BF" />
                            <FooterStyle BackColor="#507CD1" Font-Bold="false" ForeColor="White" />
                            <HeaderStyle BackColor="#507CD1" Font-Bold="false" ForeColor="White" />
                            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                            <RowStyle BackColor="#EFF3FB" />
                            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="false" ForeColor="#333333" />
                            <SortedAscendingCellStyle BackColor="#F5F7FB" />
                            <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                            <SortedDescendingCellStyle BackColor="#E9EBEF" />
                            <SortedDescendingHeaderStyle BackColor="#4870BE" />
                        </asp:GridView>
                    </asp:Panel>
                </div>
                <div style="float: left; width: 100%; height: 200px; padding-top: 4px">
                    <asp:Panel ID="Panel1" runat="server" Height="200px" ScrollBars="Auto" BorderWidth="0px"
                        Width="100%">
                        <asp:GridView ID="gvItems" runat="server" AutoGenerateColumns="False" Font-Bold="False"
                            Width="100%" EmptyDataText="No data found" ShowHeaderWhenEmpty="True" CellPadding="4"
                            ForeColor="#333333" GridLines="None" CssClass="GridView">
                            <AlternatingRowStyle BackColor="White" />
                            <Columns>
                                <asp:BoundField DataField="sard_seq_no" Visible="False" />
                                <asp:BoundField DataField="sard_line_no" Visible="False" />
                                <asp:BoundField DataField="sard_receipt_no" HeaderText="Receipt #">
                                    <HeaderStyle HorizontalAlign="Left" Width="10%" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="sard_inv_no" Visible="False" />
                                <asp:BoundField DataField="sard_pay_tp" HeaderText="Payment Type">
                                    <HeaderStyle HorizontalAlign="Left" Width="20%" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="sard_ref_no" HeaderText="Reference">
                                    <HeaderStyle HorizontalAlign="Left" Width="30%" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="sard_chq_bank_cd" Visible="False" />
                                <asp:BoundField DataField="sard_chq_branch" Visible="False" />
                                <asp:BoundField DataField="sard_deposit_bank_cd" Visible="False" />
                                <asp:BoundField DataField="sard_deposit_branch" Visible="False" />
                                <asp:BoundField DataField="sard_credit_card_bank" Visible="False" />
                                <asp:BoundField DataField="sard_cc_tp" Visible="False" />
                                <asp:BoundField DataField="sard_cc_expiry_dt" Visible="False" />
                                <asp:BoundField DataField="sard_cc_is_promo" Visible="False" />
                                <asp:BoundField DataField="sard_cc_period" Visible="False" />
                                <asp:BoundField DataField="sard_gv_issue_loc" Visible="False" />
                                <asp:BoundField DataField="sard_gv_issue_dt" Visible="False" />
                                <asp:BoundField DataField="sard_settle_amt" HeaderText="Amount">
                                    <HeaderStyle HorizontalAlign="Right" Width="15%" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:BoundField>
                                <asp:BoundField DataField="sard_sim_ser" Visible="False" />
                                <asp:BoundField DataField="sard_anal_1" Visible="False" />
                                <asp:BoundField DataField="sard_anal_2" Visible="False" />
                                <asp:BoundField DataField="sard_anal_3" Visible="False" />
                                <asp:BoundField DataField="sard_anal_4" Visible="False" />
                                <asp:BoundField DataField="sard_anal_5" Visible="False" />
                                <asp:BoundField DataField="sard_chq_dt" Visible="False" />
                                <asp:BoundField DataField="sard_cc_batch" Visible="False" />
                                <asp:BoundField DataField="sard_chq_stus" Visible="False" />
                            </Columns>
                            <EditRowStyle BackColor="#2461BF" />
                            <FooterStyle BackColor="#507CD1" Font-Bold="false" ForeColor="White" />
                            <HeaderStyle BackColor="#507CD1" Font-Bold="false" ForeColor="White" />
                            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                            <RowStyle BackColor="#EFF3FB" />
                            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="false" ForeColor="#333333" />
                            <SortedAscendingCellStyle BackColor="#F5F7FB" />
                            <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                            <SortedDescendingCellStyle BackColor="#E9EBEF" />
                            <SortedDescendingHeaderStyle BackColor="#4870BE" />
                        </asp:GridView>
                    </asp:Panel>
                </div>
                <asp:Label ID="Label4" runat="server" Text="Remarks" CssClass="Textbox" Width="7%"></asp:Label><asp:TextBox
                    ID="txtRemarks" runat="server" CssClass="TextBox" Width="70%"></asp:TextBox>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
