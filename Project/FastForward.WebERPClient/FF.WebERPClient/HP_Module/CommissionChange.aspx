<%@ Page Title="Commission Change" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CommissionChange.aspx.cs" Inherits="FF.WebERPClient.HP_Module.CommissionChange" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register src="../UserControls/uc_ProfitCenterSearch.ascx" tagname="uc_ProfitCenterSearch" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
 <asp:UpdatePanel ID="updtMainPnl" runat="server">
<ContentTemplate>
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

    </script>
<div id="divMain" style="color: Black;">
    </div>
    <div style="text-align: right">
       <asp:Button ID="btnClear" runat="server" CssClass="Button" Text="Clear" 
            onclick="btnClear_Click" />
        &nbsp;<asp:Button ID="btnClose" runat="server" Text="Close" CssClass="Button" 
            onclick="btnClose_Click"  />
    </div>
<div style="float: left; width: 100%;">
        <asp:Panel ID="Panel1" runat="server">
        <div style="float: left; width: 100%;">
            <asp:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="0" 
                Height="550px" Width="100%">
                <asp:TabPanel ID="TabPanel1" runat="server" HeaderText="Invoice Commission Change" CssClass="TabContainer">
                   <ContentTemplate>
                   <div style="float: left; width: 100%; font-size: smaller;">
                       <div style="float: left; width: 25%;">
                           <div style="float: left; width: 20%;">
                            <asp:Label ID="Label1" runat="server" Text="Type" Font-Bold="True"></asp:Label>
                           </div>
                          
                            <div style="float: left; width: 70%;">
                                <asp:TextBox ID="txtType" runat="server" Width="95%" CssClass="TextBox"></asp:TextBox>
                             </div>
                             <div style="float: left; width: 9%;">
                                 <asp:ImageButton ID="ImgBtnType" runat="server" 
                                     ImageUrl="~/Images/icon_search.png" onclick="ImgBtnType_Click" />
                           </div>
                           <div style="float: left; width: 30%;">
                           </div>
                       </div>
                       <div style="float: left; width: 35%;">
                           <div style="float: left; width: 20%;">
                            <asp:Label ID="Label2" runat="server" Text="Sub Type" Font-Bold="True"></asp:Label>
                           </div>
                          
                            <div style="float: left; width: 65%;">
                                <asp:TextBox ID="txtSubType" runat="server" Width="95%" CssClass="TextBox"></asp:TextBox>
                             </div>
                             <div style="float: left; width: 9%;">
                                 <asp:ImageButton ID="ImgBtnSubType" runat="server" 
                                     ImageUrl="~/Images/icon_search.png" onclick="ImgBtnSubType_Click" />
                           </div>
                           <div style="float: left; width: 30%;">
                           </div>
                       </div>
                         <div style="float: left; width: 30%;">
                           <div style="float: left; width: 20%;">
                            <asp:Label ID="Label3" runat="server" Text="Invoice#" Font-Bold="True"></asp:Label>
                           </div>
                          
                            <div style="float: left; width: 70%;">
                                <asp:TextBox ID="txtInvoiceNo" runat="server" Width="95%" CssClass="TextBox"></asp:TextBox>
                             </div>
                             <div style="float: left; width: 9%;">
                                 <asp:ImageButton ID="ImgBtnInvoiceNo" runat="server" 
                                     ImageUrl="~/Images/icon_search.png" onclick="ImgBtnInvoiceNo_Click" />
                           </div>
                           <div style="float: left; width: 10%;">
                           </div>
                       </div>
                       <div style="float: left; width: 3%;">
                           <asp:ImageButton ID="ImgBtnAddInvoice" runat="server" 
                               ImageUrl="~/Images/download_arrow_icon.png" Width="100%" 
                               onclick="ImgBtnAddInvoice_Click" />
                       </div>
                   </div>
                    <div style="float: left; width: 100%; font-size: smaller;">
                         <div style="padding: 1.5px; float: left; width: 50%;">
                           <div style="float: left; width: 20%;">
                            <asp:Label ID="Label7" runat="server" Text="Invoice Date :" Font-Bold="True"></asp:Label>
                           </div>
                          
                            <div style="float: left; width: 70%;">
                                <asp:Label ID="lblInvoiceDate" runat="server"></asp:Label> 
                             </div>
                           <div style="float: left; width: 30%;">
                               &nbsp;</div>
                       </div>
                    </div>
                   <div style="float: left; width: 100%;">
                       <asp:Panel ID="Panel2" runat="server" ScrollBars="Both" Height="120px">
                       <asp:GridView ID="grvInvItems" runat="server" CellPadding="4" 
                           ForeColor="#333333" AutoGenerateColumns="False" Width="99%" ShowHeaderWhenEmpty="True"
                               CssClass="GridView" 
                               onselectedindexchanged="grvInvItems_SelectedIndexChanged" 
                               onrowdatabound="grvInvItems_RowDataBound">
                           <AlternatingRowStyle BackColor="White" />
                           <EmptyDataTemplate>
                                    <div style="width: 100%; text-align: center;">
                                        No data found
                                    </div>
                                </EmptyDataTemplate>
                           <Columns>
                               <asp:CommandField SelectText="SELECT" ShowSelectButton="True">
                               <ControlStyle Font-Bold="True" ForeColor="#009933" />
                               </asp:CommandField>
                               <asp:BoundField DataField="sad_itm_cd" HeaderText="Item Code" />
                               <asp:BoundField HeaderText="Item Description" DataField="mi_shortdesc" />
                               <asp:BoundField HeaderText="Modle" DataField="mi_model" />
                               <asp:BoundField HeaderText="Price" />
                               <asp:BoundField DataField="comm_amt" HeaderText="Comm. Amt." />
                               <asp:BoundField DataField="net_value" HeaderText="Net Value" />
                               <asp:BoundField DataField="final_comm_amt" HeaderText="Final Commission Amt." />
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
                   <div style="float: left; width: 96%;">
                       &nbsp;</div>
                   <div style="float: left; width: 100%; font-size: smaller;">
                       <asp:Panel ID="Panel3" runat="server" GroupingText=" ">
                        <div style="float: left; width: 100%;"> 
                            <div style="float: left; width: 20%;">
                                <asp:Label ID="Label4" runat="server" Text="Item Code" Font-Bold="True"></asp:Label>
                            </div>
                            <div style="float: left; width: 20%;">
                            <asp:Label ID="Label5" runat="server" Text="Description" Font-Bold="True"></asp:Label>
                            </div>
                            <div style="float: left; width: 20%;">
                            <asp:Label ID="Label6" runat="server" Text="Net Value" Font-Bold="True"></asp:Label>
                            </div>
                            <div style="float: left; width: 19%;">
                            <asp:Label ID="Label8" runat="server" Text="Comm Amt." Font-Bold="True"></asp:Label>
                            </div>
                        </div>
                      
                      <div style="float: left; width: 100%;"> 
                            <div style="float: left; width: 20%;">
                                <asp:TextBox ID="txtItmCode" runat="server" CssClass="TextBox"></asp:TextBox>
                            </div>
                            <div style="float: left; width: 20%;">
                                <asp:TextBox ID="txtDescript" runat="server" CssClass="TextBox"></asp:TextBox>
                            </div>
                            <div style="float: left; width: 20%;">
                                <asp:TextBox ID="txtNetVal" runat="server" CssClass="TextBox"></asp:TextBox>
                            </div>
                            <div style="float: left; width: 19%;">
                                <asp:TextBox ID="txtComAmt" runat="server" CssClass="TextBox"></asp:TextBox>
                            </div>
                        </div>
                       </asp:Panel>                   
                   </div>
                   <div style="float: left; width: 100%; font-size: smaller;">
                   <div style="float: left; width: 100%; font-size: small; background-color: #3366FF; color: #FFFFFF;">
                        Pay-Mode Wice Claimed Commissions 
                   </div>
                   <div style="padding: 0.5px; float: left; width: 100%;"> &nbsp;</div>
                   <div style="float: left; width: 100%;">
                       <asp:Panel ID="Panel4" runat="server" ScrollBars="Both" Height="150px">
                           <asp:GridView ID="grvPaymodeComm" runat="server" CellPadding="4" 
                               CssClass="GridView" ForeColor="#333333" Width="98%" ShowHeaderWhenEmpty="True"
                               AutoGenerateColumns="False" onrowediting="grvPaymodeComm_RowEditing" 
                               onselectedindexchanged="grvPaymodeComm_SelectedIndexChanged" 
                               DataKeyNames="sac_comm_line,sac_itm_cd,sac_invoice_no" 
                               onrowdeleting="grvPaymodeComm_RowDeleting" 
                               onrowdatabound="grvPaymodeComm_RowDataBound">
                               <AlternatingRowStyle BackColor="White" />
                               <EmptyDataTemplate>
                                    <div style="width: 100%; text-align: center;">
                                        No data found
                                    </div>
                                </EmptyDataTemplate>
                               <Columns>
                                   <asp:CommandField ShowSelectButton="True" Visible="False" />
                                   <asp:BoundField DataField="sac_itm_cd" HeaderText="Item Code" />
                                   <asp:BoundField DataField="sac_pay_mode" HeaderText="Pay Mode" />
                                   <asp:BoundField DataField="sac_calc_on" HeaderText="Calculate On" />
                                   <asp:BoundField DataField="sac_comm_rate" HeaderText="Comm. Rate" />
                                   <asp:BoundField DataField="sac_comm_amt" HeaderText="Comm. Amt." />

                                   <asp:TemplateField HeaderText="Final Comm. Rate">
                                       <ItemTemplate>
                                         <%--  <asp:Label ID="Label3" runat="server"></asp:Label>--%>
                                           <asp:TextBox ID="txtFinCommRt" runat="server" 
                                               Text='<%# Bind("sac_comm_rate_final") %>' CssClass="TextBox" onkeypress="return isNumberKeyAndDot(event,this.value)"
                                               BorderColor="#99CCFF" BorderStyle="Groove" AutoPostBack="True"></asp:TextBox>
                                           <asp:ImageButton ID="ImgCalc" runat="server" Height="15px" 
                                               ImageUrl="~/Images/right_arrow_icon.png" onclick="ImgCalc_Click" Width="15px" />
                                       </ItemTemplate>
                                       <ItemStyle HorizontalAlign="Center" />
                                   </asp:TemplateField>
                                   <asp:TemplateField HeaderText="Final Comm. Amt">
                                       <EditItemTemplate>
                                           <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
                                       </EditItemTemplate>
                                       <ItemTemplate>
                                           <asp:Label ID="lblTest" runat="server"></asp:Label>
                                           <asp:TextBox ID="txtFinCommAmt" runat="server" CssClass="TextBox" 
                                               BorderColor="#99CCFF" BorderStyle="Groove" 
                                               Text='<%# Bind("sac_comm_amt_final") %>' Enabled="False" Font-Bold="True" 
                                               ForeColor="Blue"></asp:TextBox>
                                       </ItemTemplate>
                                       <ItemStyle HorizontalAlign="Center" />
                                   </asp:TemplateField>
                                   <asp:TemplateField HeaderText="line" Visible="False">
                                       <EditItemTemplate>
                                           <asp:TextBox ID="TextBox3" runat="server" Text='<%# Bind("sac_comm_line") %>'></asp:TextBox>
                                       </EditItemTemplate>
                                       <ItemTemplate>
                                           <asp:Label ID="lblCommLine" runat="server" Text='<%# Bind("sac_comm_line") %>'></asp:Label>
                                       </ItemTemplate>
                                   </asp:TemplateField>
                                   <asp:TemplateField HeaderText="Invoice No" Visible="False">
                                       <EditItemTemplate>
                                           <asp:TextBox ID="TextBox4" runat="server" Text='<%# Bind("sac_invoice_no") %>'></asp:TextBox>
                                       </EditItemTemplate>
                                       <ItemTemplate>
                                           <asp:Label ID="lblInvoiceNo" runat="server" 
                                               Text='<%# Bind("sac_invoice_no") %>'></asp:Label>
                                       </ItemTemplate>
                                   </asp:TemplateField>
                                   <asp:TemplateField HeaderText="Item line" Visible="False">
                                       <EditItemTemplate>
                                           <asp:TextBox ID="TextBox5" runat="server" Text='<%# Bind("sac_itm_line") %>'></asp:TextBox>
                                       </EditItemTemplate>
                                       <ItemTemplate>
                                           <asp:Label ID="lblItemLine" runat="server" Text='<%# Bind("sac_itm_line") %>'></asp:Label>
                                       </ItemTemplate>
                                   </asp:TemplateField>
                                   <asp:TemplateField HeaderText="Change">
                                       <EditItemTemplate>
                                           <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                                       </EditItemTemplate>
                                       <ItemTemplate>
                                           <asp:Label ID="Label1" runat="server"></asp:Label>
                                           <asp:ImageButton ID="ImgBtnApprove" runat="server" CommandName="Delete" 
                                               ImageUrl="~/Images/approve_img.png" />
                                       </ItemTemplate>
                                       <ItemStyle HorizontalAlign="Center" />
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

                   <div style="float: left; width: 100%; font-size: smaller; background-color: #FFFFFF;">
                    <div style="float: left; width: 100%; font-size: small; background-color: #3366FF; color: #FFFFFF;">
                        Commission Changed Invoices 
                   </div>
                    
                    <div style="float: left; width: 100%; font-size: small;">
                        <div style="float: left; width: 93%; font-size: small;">
                        
                            &nbsp;</div>
                        <div style="float: left; width: 7%; font-size: small; text-align: right;">
                            <asp:Button ID="btnFinalSave" runat="server" Text="Finalize" CssClass="Button" 
                                Width="100%" onclick="btnFinalSave_Click" />
                        </div>
                   </div>
                   
                       <asp:Panel ID="Panel7" runat="server" Height="130px" 
                           BackColor="White">
                           <asp:GridView ID="grvChangedInvoices" runat="server" 
                               AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" 
                               Width="100%" ShowHeaderWhenEmpty="True">
                               <AlternatingRowStyle BackColor="White" />
                                <EmptyDataTemplate>
                                    <div style="width: 100%; text-align: center;">
                                        No data found
                                    </div>
                                </EmptyDataTemplate>
                               <Columns>
                                   <asp:BoundField DataField="Sah_dt" HeaderText="Invoice Date" 
                                       DataFormatString="{0:d}" />
                                   <asp:BoundField DataField="Sah_inv_no" HeaderText="Invoice Number" />
                                   <asp:BoundField DataField="Sah_cus_name" HeaderText="Customer " />
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
                   </ContentTemplate>
                </asp:TabPanel>
                <asp:TabPanel ID="TabPanel2" runat="server" HeaderText="Circular Commission Change" CssClass="TabContainer">
                    <ContentTemplate>
                    <div style="float: left; width: 100%; font-size: smaller;">
                    <div style="padding: 5.0px; width: 100%;">     
                        <div style="float: left; width: 80%;">
                        <div style="padding: 2.0px; float: left; width: 100%;">                        
                            <div style="float: left; width: 13%;">                                
                                 <div style="float: left; width: 70%;">
                                 <asp:Label ID="Label9" runat="server" Text="From Date" Font-Bold="True" 
                                    ForeColor="#0066FF"></asp:Label>
                                </div>
                                
                            </div>
                            <div style="float: left; width: 25%;">
                                <asp:TextBox ID="txtFrm_dt" runat="server" CssClass="TextBox" 
                                    ForeColor="#0066FF"></asp:TextBox>
                                 <asp:CalendarExtender ID="CalendarExtender2" runat="server" Enabled="True" Format="dd/MM/yyyy"
                                                TargetControlID="txtFrm_dt">
                                            </asp:CalendarExtender>
                            </div>
                             <div style="float: left; width: 10%;">
                                
                                 <div style="float: left; width: 70%;">
                                 <asp:Label ID="Label10" runat="server" Text="To Date" Font-Bold="True" 
                                    ForeColor="#0066FF"></asp:Label>
                                </div>
                                
                            </div>
                            <div style="float: left; width: 25%;">
                                <asp:TextBox ID="txtTo_dt" runat="server" CssClass="TextBox" 
                                    ForeColor="#0066FF"></asp:TextBox>
                                     <asp:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="True" Format="dd/MM/yyyy"
                                                TargetControlID="txtTo_dt">
                                            </asp:CalendarExtender>
                                
                            </div>
                            <div style="float: left; width: 25%;">
                            
                              
                            
                            </div>
                        </div>
                            &nbsp;</div> 
                              <div style="float: left; width: 9%;">
                                    <asp:Button ID="btnProcessCirc" runat="server" CssClass="Button" 
                                        onclick="btnProcessCirc_Click" Text="Process" ValidationGroup="circ" />
                                </div>
                        <div style="float: left; width: 9%;">
                         <asp:Button ID="btnFinalize" runat="server" CssClass="Button" Text="Finalize" 
                                ValidationGroup="circ" onclick="btnFinalize_Click" />
                        </div> 
                         </div>
                       
                        <div style="padding: 2.0px; float: left; width: 100%; background-color: #3366FF; color: #FFFFFF;">
                            Select Profit Centers
                        </div>
                        <div style="float: left; width: 100%;">
                        <div style="padding: 3.0px; float: left; width: 100%;"> </div>
                        <div style="float: left; width: 50%;">
                        
                            <asp:Panel ID="Panel5" runat="server">

                                <uc1:uc_ProfitCenterSearch ID="uc_ProfitCenterSearch1" runat="server" />

                            </asp:Panel>
                        </div>
                        <div style="float: left; width: 49%;">
                            <div  style="float: left; width: 5%;">
                                <asp:ImageButton ID="btnAddToPC_list" runat="server" 
                                    ImageUrl="~/Images/right_arrow_icon.png" Width="100%" 
                                    onclick="btnAddToPC_list_Click" />
                            </div>
                            <div style="float: left; width: 25%;" align="left">
                    <div style="float: left; width: 100%; text-align: center;">
                        <asp:Panel ID="Panel6" runat="server" Height="150px" ScrollBars="Vertical" BorderColor="Blue"
                            BorderWidth="1px" GroupingText="Profit Centers">
                            <asp:GridView ID="grvProfCents" runat="server" AutoGenerateColumns="False" 
                                OnRowDataBound="grvProfCents_RowDataBound">
                                <Columns>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:Label ID="Label1" runat="server"></asp:Label>
                                            <asp:CheckBox ID="chekPc" runat="server" />
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                                        </EditItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="PROFIT_CENTER" ShowHeader="False" />
                                </Columns>
                            </asp:GridView>
                        </asp:Panel>
                    </div>
                    <div style="float: left; width: 100%; text-align: right;">
                        <div style="float: left; width: 33%; text-align: right;">
                            <asp:Button ID="btnAll" runat="server" Text="All" CssClass="Button" Width="100%"
                                OnClick="btnAll_Click" Font-Size="XX-Small" />
                        </div>
                        <div style="float: left; width: 33%; text-align: right;">
                            <asp:Button ID="btnNone" runat="server" Text="None" CssClass="Button" OnClick="btnNone_Click"
                                Width="100%" Font-Size="XX-Small" />
                        </div>
                        <div style="float: left; width: 33%; text-align: right;">
                            <asp:Button ID="btnClearPcList" runat="server" Text="Clear" CssClass="Button" OnClick="btnClearPcList_Click"
                                Width="100%" Font-Size="XX-Small" />
                        </div>
                    </div>
                </div>
                        </div>
                        </div>
                    </div>
                    <div>
                   
                </ContentTemplate>
                </asp:TabPanel>
            </asp:TabContainer>
        </div>
        </asp:Panel>
    </div>


</ContentTemplate>
</asp:UpdatePanel>
</asp:Content>
