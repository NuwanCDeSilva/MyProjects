<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="uc_HpAccountSummary.ascx.cs"
    Inherits="FF.AbansTours.UserControls.uc_HpAccountSummary" %>
    
<style type="text/css">
    .style1
    {
        width: 100%;
        font-size: 11px;
        font-family: Verdana;
        color: Black;
        border-spacing: 0px;
    }
    
    .style28
    {
        width: 121px;
    }
    .style32
    {
        width: 121px;
    }
    .style42
    {
        width: 5px;
    }
    .style43
    {
        width: 5px;
    }
    .style44
    {
        width: 4px;
    }
    .style45
    {
        width: 4px;
    }
    .style46
    {
        width: 4px;
    }
    .style53
    {
        width: 57px;
    }
    .style54
    {
        width: 57px;
    }
    .style55
    {
        width: 107px;
    }
    .style56
    {
        width: 1px;
    }
    .style57
    {
        width: 121px;
        height: 17px;
    }
    .style58
    {
        width: 5px;
        height: 17px;
    }
    .style59
    {
        width: 57px;
        height: 17px;
    }
    .style60
    {
        width: 1px;
        height: 17px;
    }
    .style61
    {
        width: 107px;
        height: 17px;
    }
    .style62
    {
        width: 4px;
        height: 17px;
    }
    .style63
    {
        height: 17px;
    }
</style>
<%--remove Text Comment--%>

<div style="color: Black;">
    <asp:Panel ID="Panel_summary" runat="server" Style="font-size: 11px; font-family: Verdana">
        <table class="style1">
            <tr>
                <td class="style7" colspan="5">
                    <asp:Label ID="Label1" runat="server" Text="Customer" Font-Bold="True"></asp:Label>
                    &nbsp;&nbsp; :<asp:Label ID="uc_lblCustomer" runat="server"></asp:Label>
                    <br />
                </td>
                <td class="style44">
                    &nbsp;
                </td>
                <td class="style11">
                    &nbsp;
                </td>
            </tr>
            <tr style="background-color: #EFF3FB;">
                <td class="style28" style="padding-bottom: 1px">
                    <asp:Label ID="Label2" runat="server" Text="Scheme"></asp:Label>
                </td>
                <td class="style42" style="padding-bottom: 1px">
                    :
                </td>
                <td class="style53" style="text-align: right; padding-bottom: 1px;">
                    <asp:Label ID="uc_lblScheme" runat="server"> </asp:Label>
                </td>
                <td class="style56" style="padding-bottom: 1px">
                    &nbsp;
                </td>
                <td class="style55" style="padding-bottom: 1px">
                    <asp:Label ID="Label26" runat="server" Text="E.C.D. Normal"></asp:Label>
                </td>
                <td class="style45" style="padding-bottom: 1px">
                    :
                </td>
                <td style="text-align: right; padding-bottom: 1px;">
                    <asp:Label ID="uc_lblECDNormal" runat="server">0.00</asp:Label>
                </td>
            </tr>
            <tr>
                <td class="style28" style="padding-bottom: 1px">
                    <asp:Label ID="Label40" runat="server" Text="Cash Price"></asp:Label>
                </td>
                <td class="style42" style="padding-bottom: 1px">
                    :
                </td>
                <td class="style53" style="text-align: right; padding-bottom: 1px;">
                    <asp:Label ID="uc_lblCashPrice" runat="server">0.00</asp:Label>
                </td>
                <td class="style56" style="padding-bottom: 1px">
                    &nbsp;
                </td>
                <td class="style55" style="padding-bottom: 1px">
                    <asp:Label ID="Label25" runat="server" Text="Balance"></asp:Label>
                </td>
                <td class="style45" style="padding-bottom: 1px">
                    :
                </td>
                <td style="text-align: right; padding-bottom: 1px;">
                    <asp:Label ID="uc_lblECDnormalBal" runat="server">0.00</asp:Label>
                </td>
            </tr>
            <tr style="background-color: #EFF3FB;">
                <td class="style28" style="padding-bottom: 1px">
                    <asp:Label ID="Label42" runat="server" Text="First Pay"></asp:Label>
                </td>
                <td class="style42" style="padding-bottom: 1px">
                    :
                </td>
                <td class="style53" style="text-align: right; padding-bottom: 1px;">
                    <asp:Label ID="uc_lblFirstPay" runat="server">0.00</asp:Label>
                </td>
                <td class="style56" style="padding-bottom: 1px">
                    &nbsp;
                </td>
                <td class="style55" style="padding-bottom: 1px">
                    <asp:Label ID="Label24" runat="server" Text="E.C.D. Special"></asp:Label>
                </td>
                <td class="style45" style="padding-bottom: 1px">
                    :
                </td>
                <td style="text-align: right; padding-bottom: 1px;">
                    <asp:Label ID="uc_lblECDspecial" runat="server">0.00</asp:Label>
                </td>
            </tr>
            <tr>
                <td class="style32" style="padding-bottom: 1px">
                    <asp:Label ID="Label44" runat="server" Text="Service Charge"></asp:Label>
                </td>
                <td class="style43" style="padding-bottom: 1px">
                    :
                </td>
                <td class="style54" style="text-align: right; padding-bottom: 1px;">
                    <asp:Label ID="uc_lblServiceChg" runat="server">0.00</asp:Label>
                </td>
                <td class="style56" style="padding-bottom: 1px">
                    &nbsp;
                </td>
                <td class="style55" style="padding-bottom: 1px">
                    <asp:Label ID="Label23" runat="server" Text="Balance"></asp:Label>
                </td>
                <td class="style46" style="padding-bottom: 1px">
                    :
                </td>
                <td class="style35" style="text-align: right; padding-bottom: 1px;">
                    <asp:Label ID="uc_lblECDspecialBal" runat="server">0.00</asp:Label>
                </td>
            </tr>
            <tr style="background-color: #EFF3FB;">
                <td class="style28" style="padding-bottom: 1px">
                    <asp:Label ID="Label17" runat="server" Text="Total Cash"></asp:Label>
                </td>
                <td class="style42" style="padding-bottom: 1px">
                    :
                </td>
                <td class="style53" style="text-align: right; padding-bottom: 1px;">
                    <asp:Label ID="uc_lblTotCash" runat="server">0.00</asp:Label>
                </td>
                <td class="style56" style="padding-bottom: 1px">
                    &nbsp;
                </td>
                <td class="style55" style="padding-bottom: 1px">
                    <asp:Label ID="Label54" runat="server" Text="Monthly Rental"></asp:Label>
                </td>
                <td class="style45" style="padding-bottom: 1px">
                    :
                </td>
                <td style="text-align: right; padding-bottom: 1px;">
                    <asp:Label ID="uc_lblMonthlyRental" runat="server">0.00</asp:Label>
                </td>
            </tr>
            <tr>
                <td class="style32" style="padding-bottom: 1px">
                    <asp:Label ID="Label46" runat="server" Text="Amount Finance"></asp:Label>
                </td>
                <td class="style43" style="padding-bottom: 1px">
                    :
                </td>
                <td class="style54" style="text-align: right; padding-bottom: 1px;">
                    <asp:Label ID="uc_lblAmtFinance" runat="server">0.00</asp:Label>
                </td>
                <td class="style56" style="padding-bottom: 1px">
                    &nbsp;
                </td>
                <td class="style55" style="padding-bottom: 1px">
                    <asp:Label ID="uc_lblArr_Ovp" runat="server" Text="Arrears Bal." ForeColor="#990000"></asp:Label>
                </td>
                <td class="style46" style="padding-bottom: 1px">
                    :
                </td>
                <td class="style35" style="text-align: right; padding-bottom: 1px;">
                   
                </td>
            </tr>
              <tr>
                <td class="style32" style="padding-bottom: 1px">
                  
                </td>
                <td class="style43" style="padding-bottom: 1px">
                    :
                </td>
                <td class="style54" style="text-align: right; padding-bottom: 1px;">
                   
                </td>
                <td class="style56" style="padding-bottom: 1px">
                    &nbsp;
                </td>
                <td class="style55" style="padding-bottom: 1px; text-align: right;">
                     <asp:Label ID="Label61" runat="server" ForeColor="#990000" Text="Veh.Insu" 
                         Font-Size="Smaller"></asp:Label>
                </td>
                <td class="style46" style="padding-bottom: 1px">
                    :
                </td>
                <td class="style35" style="text-align: right; padding-bottom: 1px;">
                   <asp:Label ID="lblArrVehIns" runat="server" ForeColor="#990000">0.00</asp:Label>
                </td>
            </tr>
          <tr style="text-align: right">
                <td class="style32" style="padding-bottom: 1px">
                    &nbsp;</td>
                <td class="style43" style="padding-bottom: 1px">
                    :
                </td>
                <td class="style54" style="text-align: right; padding-bottom: 1px;">
                    &nbsp;</td>
                <td class="style56" style="padding-bottom: 1px">
                    &nbsp;
                </td>
                <td class="style55" style="padding-bottom: 1px">
                  <asp:Label ID="Label5" runat="server" ForeColor="#990000" Text="HP Insu" 
                        Font-Size="Smaller"></asp:Label>
                </td>
                <td class="style46" style="padding-bottom: 1px">
                    :
                </td>
                <td class="style35" style="text-align: right; padding-bottom: 1px;">
                     <asp:Label ID="lblArrHpInsu" runat="server" ForeColor="#990000">0.00</asp:Label>
                </td>
            </tr>
              <tr style="background-color: #EFF3FB;">
                <td class="style28" style="padding-bottom: 1px">
                    
                  </td>
                <td class="style42" style="padding-bottom: 1px">
                    :
                </td>
                <td class="style53" style="text-align: right; padding-bottom: 1px;">
                     
                </td>
                <td class="style56" style="padding-bottom: 1px">
                    &nbsp;
                </td>
                <td class="style55" style="padding-bottom: 1px; text-align: right;">
                    <asp:Label ID="Label7" runat="server" Text="Collection" ForeColor="#990000" 
                        Font-Size="Smaller"></asp:Label>
                </td>
                <td class="style45" style="padding-bottom: 1px">
                    :
                </td>
                <td style="text-align: right; padding-bottom: 1px;">
                    <asp:Label ID="uc_lblArrears" runat="server" ForeColor="#990000">0.00</asp:Label>
                </td>
            </tr>      
          
            <tr style="background-color: #EFF3FB;">
                <td class="style28" style="padding-bottom: 1px">
                    <asp:Label ID="Label47" runat="server" Text="Interest Rate"></asp:Label>
                </td>
                <td class="style42" style="padding-bottom: 1px">
                    :
                </td>
                <td class="style53" style="text-align: right; padding-bottom: 1px;">
                    <asp:Label ID="uc_lblInterestRate" runat="server">0.00</asp:Label>
                    %</td>
                <td class="style56" style="padding-bottom: 1px">
                    &nbsp;
                </td>
                <td class="style55" style="padding-bottom: 1px">
                    <asp:Label ID="Label56" runat="server" Text="All Due" ForeColor="Blue"></asp:Label>
                </td>
                <td class="style45" style="padding-bottom: 1px">
                    :
                </td>
                <td style="text-align: right; padding-bottom: 1px;">
                   
                </td>
            </tr>
            <tr>
                <td class="style28" style="padding-bottom: 1px">
                    <asp:Label ID="Label49" runat="server" Text="Interest"></asp:Label>
                </td>
                <td class="style42" style="padding-bottom: 1px">
                    :
                </td>
                <td class="style53" style="text-align: right; padding-bottom: 1px;">
                    <asp:Label ID="uc_lbl_Interest" runat="server">0.00</asp:Label>
                </td>
                <td class="style56" style="padding-bottom: 1px">
                    &nbsp;
                </td>
                <td class="style55" style="padding-bottom: 1px; text-align: right;">
                    <asp:Label ID="Label58" runat="server" Text="Veh.Insu" 
                        ForeColor="Blue" Font-Size="Smaller"></asp:Label>
                </td>
                <td class="style45" style="padding-bottom: 1px">
                    :
                </td>
                <td class="style5" style="text-align: right; padding-bottom: 1px;">
                    <asp:Label ID="uc_lblVehInsDue" runat="server" ForeColor="Blue">0.00</asp:Label>
                </td>
            </tr>
            <tr style="background-color: #EFF3FB; ">
                <td class="style57" style="padding-bottom: 1px; text-align: left;">
                   <%-- <asp:Label ID="Label50" runat="server" Text="Future Rentals"></asp:Label>--%>
                      <asp:Label ID="Label3" runat="server" Text="Future Rentals"></asp:Label>
                </td>
                <td class="style58">
                    :
                </td>
                <td class="style59" style="text-align: right; padding-bottom: 1px;">
                    <asp:Label ID="uc_lblFutureRentals" runat="server">0.00</asp:Label>
                </td>
                <td class="style60" style="padding-bottom: 1px">
                    &nbsp;
                </td>
                <td class="style61" style="padding-bottom: 1px; text-align: right;">
                    <asp:Label ID="Label60" runat="server" ForeColor="Blue" Font-Size="Smaller">HP Insu</asp:Label>
                </td>
                <td class="style62" style="padding-bottom: 1px">
                    :
                </td>
                <td style="text-align: right; padding-bottom: 1px;" class="style63">
                    <asp:Label ID="uc_lblInsDue" runat="server" ForeColor="Blue">0.00</asp:Label>
                </td>
            </tr>
            <tr style="background-color: #EFF3FB;">
                <td class="style28" style="padding-bottom: 1px">
                    <asp:Label ID="Label51" runat="server" Text="Ending Date"></asp:Label>
                </td>
                <td class="style42" style="padding-bottom: 1px">
                    :
                </td>
                <td class="style53" style="text-align: right; padding-bottom: 1px;">
                    <asp:Label ID="uc_lblAC_endDate" runat="server"></asp:Label>
                </td>
                <td class="style56" style="padding-bottom: 1px">
                    &nbsp;
                </td>
                <td class="style55" style="padding-bottom: 1px; text-align: right;">
                  <asp:Label ID="Label4" runat="server" Text="Collection" ForeColor="Blue" 
                        Font-Size="Smaller"></asp:Label>
                    </td>
                <td class="style45" style="padding-bottom: 1px">
                    :
                </td>
                <td style="text-align: right; padding-bottom: 1px;">
                     <asp:Label ID="uc_lbl_All_Due" runat="server" ForeColor="Blue">0.00</asp:Label>
                </td>
            </tr>
            <tr style="background-color: #EFF3FB;">
                <td class="style28" style="padding-bottom: 1px">
                    <asp:Label ID="Label16" runat="server" Text="Inst Com Rate"></asp:Label>
                </td>
                <td class="style42" style="padding-bottom: 1px">
                    :
                </td>
                <td class="style53" style="text-align: right; padding-bottom: 1px;">
                    <asp:Label ID="uc_lbl_InstComRt" runat="server">0.00</asp:Label>
                    %</td>
                <td class="style56" style="padding-bottom: 1px">
                    &nbsp;
                </td>
                <td class="style55" style="padding-bottom: 1px">
                    <asp:Label ID="Label55" runat="server" Text="Hire Value"></asp:Label>
                </td>
                <td class="style45" style="padding-bottom: 1px">
                    :
                </td>
                <td style="text-align: right; padding-bottom: 1px;">
                    <asp:Label ID="uc_lblHireValue" runat="server">0.00</asp:Label>
                </td>
            </tr>
            <tr style="background-color: #EFF3FB;">
                <td class="style28" style="padding-bottom: 1px">
                    <asp:Label ID="Label52" runat="server" Text="Additional Com Rt."></asp:Label>
                </td>
                <td class="style42" style="padding-bottom: 1px">
                    :
                </td>
                <td class="style53" style="text-align: right; padding-bottom: 1px;">
                    <asp:Label ID="uc_lblAddiComRate" runat="server">0.00</asp:Label>
                    %</td>
                <td class="style56" style="padding-bottom: 1px">
                    &nbsp;
                </td>
                <td class="style55" style="padding-bottom: 1px">
                    <asp:Label ID="Label57" runat="server" Text="Total Receipts"></asp:Label>
                </td>
                <td class="style45" style="padding-bottom: 1px">
                    :
                </td>
                <td style="text-align: right; padding-bottom: 1px;">
                    <asp:Label ID="uc_lblTotReceipts" runat="server">0.00</asp:Label>
                </td>
            </tr>
            <tr style="background-color: #EFF3FB;">
                <td class="style28" style="padding-bottom: 1px">
                    <asp:Label ID="Label53" runat="server" Text="Protection Payment"></asp:Label>
                </td>
                <td class="style42" style="padding-bottom: 1px">
                    :
                </td>
                <td class="style53" style="text-align: right; padding-bottom: 1px;">
                    <asp:Label ID="uc_lblProtection" runat="server">0.00</asp:Label>
                </td>
                <td class="style56" style="padding-bottom: 1px">
                    &nbsp;
                </td>
                <td class="style55" style="padding-bottom: 1px">
                    <asp:Label ID="Label59" runat="server" Text="Adjustments"></asp:Label>
                </td>
                <td class="style45" style="padding-bottom: 1px">
                    :
                </td>
                <td style="text-align: right; padding-bottom: 1px;">
                    <asp:Label ID="uc_lblAdj" runat="server">0.00</asp:Label>
                </td>
            </tr>           
                        
        </table>
        <div style="float: left; width: 100%; padding-top: 2px; color: #FF2A14;">
            <div style="float: left; width: 10%;">
                &nbsp;
                </div>
            <div style="float: left; width: 35%;">
                <asp:Label ID="Label21" runat="server" Font-Bold="True" Text="Account Balance"></asp:Label>
            </div>
            <div style="float: left; text-align: right; font-weight: bold;">
                <asp:Label ID="uc_lblAccountBal" runat="server" Text="0.00"></asp:Label>
            </div>
        </div>
    </asp:Panel>
</div>
