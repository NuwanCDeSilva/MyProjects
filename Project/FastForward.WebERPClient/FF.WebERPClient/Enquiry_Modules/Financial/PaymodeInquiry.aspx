<%@ Page Title="Paymode Inquiry" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PaymodeInquiry.aspx.cs" Inherits="FF.WebERPClient.Enquiry_Modules.Financial.PaymodeInquiry" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
        .GridView
        {}
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">
    function fun1(e, button2) {
        var evt = e ? e : window.event;
        var bt = document.getElementById(button2);
        if (bt) {
            if (evt.keyCode == 13) {
                bt.click();
                return false;
            }
        }
    }
    function onblurFire(e, buttonid) {
        var evt = e ? e : window.event;
        var bt = document.getElementById(buttonid);
        if (bt) {

            bt.click();
            return false;


        }
    }

</script>
<asp:UpdatePanel ID="updtMainPnl" runat="server">
<ContentTemplate>
<div id="divMain" style="color: Black;">
    <div style="text-align: right">
       <asp:Button ID="btnClear" runat="server" CssClass="Button" Text="Clear" 
            onclick="btnClear_Click" />
        &nbsp;<asp:Button ID="btnClose" runat="server" Text="Close" CssClass="Button" 
            onclick="btnClose_Click" />
    </div>
</div>
<div style="display: none">
    <asp:Button ID="btnGetDocDet" runat="server" Text="Button" 
        onclick="btnGetDocDet_Click" />
         <asp:Button ID="btnHidden_popup" runat="server" Text="PopUp" 
        onclick="btnGetDocDet_Click" />
</div>
<div style="float: left; width: 100%;">
    <asp:Panel ID="PanelHeader1" runat="server" CssClass="PanelHeader" Font-Bold="True">Document Selection
    </asp:Panel>
    <asp:Panel ID="Panel2" runat="server">
    <div class="div100pcelt" >
        <div class="invunkwn20">
            <asp:Label ID="Label1" runat="server" Text="Paymode" Font-Bold="True"></asp:Label> 
        </div>
        <div  class="invunkwn50">
            <asp:DropDownList ID="ddlPayMode" runat="server" CssClass="ComboBox" 
                AutoPostBack="True" onselectedindexchanged="ddlPayMode_SelectedIndexChanged">
            </asp:DropDownList>
        </div>
    </div>
    <div class="div100pcelt" >
        <div class="invunkwn20">
            <asp:Label ID="Label2" runat="server" Text="Document No." Font-Bold="True"></asp:Label> 
        </div>
        <div  class="invunkwn50">
            <asp:TextBox ID="txtDocNo" runat="server" CssClass="TextBox"></asp:TextBox>   
            &nbsp;<asp:ImageButton ID="ImgBtnDocSearch" runat="server" 
                ImageUrl="~/Images/icon_search.png" onclick="ImgBtnDocSearch_Click" />
        </div>
    </div>
    </asp:Panel>
</div>

<div style="float: left; width: 100%;">
    <asp:Panel ID="Panel5" runat="server" CssClass="PanelHeader" Font-Bold="True">Payment Document Detail
    </asp:Panel>
    <asp:Panel ID="Panel4" runat="server" Height="90px" ScrollBars="Both">
        <asp:GridView ID="grvPaymentDocDet" runat="server" CellPadding="4" 
            ForeColor="#333333" CssClass="GridView" 
            ShowHeaderWhenEmpty="True" 
            onrowdatabound="grvPaymentDocDet_RowDataBound" Width="706px" 
            HorizontalAlign="Center">
            <AlternatingRowStyle BackColor="White" />
            <EditRowStyle BackColor="#2461BF" />
             <EmptyDataTemplate>
                <div style="width: 100%; text-align: center;">
                    No data found
                </div>
             </EmptyDataTemplate>
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
    <asp:Panel ID="Panel_RtnChq" runat="server" CssClass="PanelHeader" Font-Bold="True" 
        Visible="False">Retun Cheque Details
    </asp:Panel>
    <asp:Panel ID="Panel11" runat="server" Height="100px" ScrollBars="Both">

        <asp:GridView ID="grvReturnChequeDet" runat="server" CellPadding="4" 
            ForeColor="#333333" Width="706px" CssClass="GridView" 
            HorizontalAlign="Center">
            <AlternatingRowStyle BackColor="White" />
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
    <asp:Panel ID="Panel6" runat="server" CssClass="PanelHeader" Font-Bold="True">Invoice Details
    </asp:Panel>
    <asp:Panel ID="Panel1" runat="server" Height="100px" ScrollBars="Both">
        <asp:GridView ID="grvInvoiceDet" runat="server" CellPadding="4" 
            ForeColor="#333333" CssClass="GridView" 
            ShowHeaderWhenEmpty="True" Width="98%" AutoGenerateColumns="False">
            <AlternatingRowStyle BackColor="White" />
            <Columns>
                <asp:BoundField HeaderText="Invoice No." DataField="Sad_inv_no" >
                <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField HeaderText="Profit center" DataField="sah_pc" >
                <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField HeaderText="Date" DataField="sah_dt" DataFormatString="{0:d}" >
                <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField HeaderText="Item Code" DataField="Sad_itm_cd" >
                <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="sad_qty" HeaderText="Qty." >
                <HeaderStyle HorizontalAlign="Right" />
                <ItemStyle HorizontalAlign="Right" />
                </asp:BoundField>
                <asp:BoundField HeaderText="Amount" DataField="Sad_tot_amt" 
                    DataFormatString="{0:n2}" >
                <HeaderStyle HorizontalAlign="Right" />
                <ItemStyle HorizontalAlign="Right" />
                </asp:BoundField>
            </Columns>
            <EditRowStyle BackColor="#2461BF" />
             <EmptyDataTemplate>
                <div style="width: 100%; text-align: center;">
                    No data found
                </div>
             </EmptyDataTemplate>
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
    <asp:Panel ID="Panel7" runat="server" CssClass="PanelHeader" Font-Bold="True">Receipts Based On Selected Document
    </asp:Panel>
    <asp:Panel ID="Panel3" runat="server" Height="100px" ScrollBars="Both">
        <asp:GridView ID="grvReceptDet" runat="server" CellPadding="4" 
            ForeColor="#333333" CssClass="GridView" 
            ShowHeaderWhenEmpty="True" Width="98%" AutoGenerateColumns="False" 
            onrowdatabound="grvReceptDet_RowDataBound">
            <AlternatingRowStyle BackColor="White" />
            <Columns>
                <asp:BoundField DataField="msrt_desc" HeaderText="Receipt Type">
                <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField HeaderText="Receipt No." DataField="sar_receipt_no" >
                <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField HeaderText="Account No." DataField="sar_acc_no" >
                <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField HeaderText="Date" DataField="sar_receipt_date" 
                    DataFormatString="{0:d}" >
                <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="sar_tot_settle_amt" HeaderText="Receipt Amt." 
                    DataFormatString="{0:n2}" >
                <HeaderStyle HorizontalAlign="Right" />
                <ItemStyle HorizontalAlign="Right" />
                </asp:BoundField>
                <asp:BoundField HeaderText="Remarks" DataField="sar_remarks" >
                <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:TemplateField HeaderText="Issued By">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("sar_is_mgr_iss") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>                        
                        <asp:Label ID="lblIssuedBy" runat="server" Text='<%# ((Eval("sar_is_mgr_iss").ToString()))=="1" ? "Manager" : "Customer"%>' Font-Bold="True"></asp:Label>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
            </Columns>
            <EditRowStyle BackColor="#2461BF" />
             <EmptyDataTemplate>
                <div style="width: 100%; text-align: center;">
                    No data found
                </div>
             </EmptyDataTemplate>
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
    <asp:Panel ID="PanelPopUpSelect" runat="server" 
        BackColor="White">
        <asp:Panel ID="Panel8" runat="server"  CssClass="PanelHeader">
        <div style="float: left; width: 100%; text-align: right;">
            <asp:ImageButton ID="btnPopUpClose" runat="server" 
                ImageUrl="~/Images/icon_reject2.PNG" onclick="ImageButton2_Click" />
        </div>
       <div style="padding: 1.5px; float: left; width: 100%; text-align: right;">
            
        </div>
        </asp:Panel>
        <asp:Panel ID="Panel9" runat="server" BackColor="White" ScrollBars="Vertical">      

        <asp:GridView ID="grvPopUpSelect" runat="server" CellPadding="4" 
            ForeColor="#333333" CssClass="GridView" 
            ShowHeaderWhenEmpty="True" Width="100%" 
            onselectedindexchanged="grvPopUpSelect_SelectedIndexChanged">
            <AlternatingRowStyle BackColor="White" />
            <Columns>
                <asp:CommandField SelectText="SELECT" ShowSelectButton="True">
                <ItemStyle ForeColor="#009933" />
                </asp:CommandField>
            </Columns>
            <EditRowStyle BackColor="#2461BF" />
             <EmptyDataTemplate>
                <div style="width: 100%; text-align: center;">
                    No data found
                </div>
             </EmptyDataTemplate>
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
    
    </asp:Panel>
</div>

     <asp:ModalPopupExtender ID="ModalPopupExtItem" runat="server" ClientIDMode="Static"
                    BackgroundCssClass="modalBackground" PopupControlID="PanelPopUpSelect" TargetControlID="btnHidden_popup" Drag="true" OkControlID="btnPopUpClose">
                </asp:ModalPopupExtender>
</ContentTemplate>
</asp:UpdatePanel>

</asp:Content>
