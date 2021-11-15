<%@ Page Title="HP Collection" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Collection.aspx.cs"  Inherits="FF.WebERPClient.HP_Module.Collection"  EnableEventValidation="false"  %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
        .style1
        {
            font-family: Verdana;
            font-size: 11px;
        }
        .GridView
        {}
    </style>
     <script language="javascript" type="text/javascript">

         function DeleteConfirm() {
             if (confirm("Are you sure to delete?")) {
                 return true;
             }
             else {
                 return false;
             }
         }
    
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div style="text-align: right"> &nbsp;<asp:Button ID="btnSave" runat="server" CssClass="Button" Text="Save" />
&nbsp;<asp:Button ID="btnClear" runat="server" CssClass="Button" Text="Clear" />
    &nbsp;</div>
    <asp:UpdatePanel runat="server" ID="updpnl" >
    <ContentTemplate>
<div id="divMain" style="color:Black;">

   <div style="float: left; width: 2%;">&nbsp;</div>

       <%--<div style="float: left; width: 45%;">
                    Entry Date:&nbsp;&nbsp; 
                    <asp:Label ID="lblEntryDate" runat="server" Text="dd/mm/yyyy"></asp:Label>
                </div>--%>
       <div id="div_ReciptDet" style="float: left; width: 59%; color:Navy;" >
            <asp:Panel ID="Panel_ReciptDetails" runat="server" GroupingText="Recipt Details">
            
               <%-- <div style="float: left; width: 50%;">
                
                </div>--%>
            <div style="float: left; width: 100%;">
              <div style="float: left; width: 2%;"></div>
              <div style="float: left; width: 54%;"> &nbsp;&nbsp;&nbsp; </div>
              <div style="float: left; width: 2%;"></div>
              <div style="float: left; width: 40%; text-align: right;">
                    Entry Date: 
                    <asp:Label ID="lblEntryDate" runat="server" Text="dd/mm/yyyy" 
                        style="text-align: right"></asp:Label>
                </div>
              <div style="float: left; width: 2%;">&nbsp;</div>
            </div>
                <%--<div style="float: left; width: 45%;">
                    Entry Date:&nbsp;&nbsp; 
                    <asp:Label ID="lblEntryDate" runat="server" Text="dd/mm/yyyy"></asp:Label>
                </div>--%>
            
           
           <div style="float: left; width: 100%;">
             <div style="float: left; width: 2%;">&nbsp;</div>
             <div style="float: left; width: 96%;">&nbsp;</div>
              <div style="float: left; width: 2%;">&nbsp;</div>
             </div>
            <div style="float: left; width: 100%;">
                    <div style="float: left; width: 2%;">&nbsp;</div>
                    <div style="float: left; width: 96%;">
                          <div style="float: left; width: 18%;">
                            <asp:Label ID="Label2" runat="server" Text="Receipt Date: "></asp:Label>
                          </div>
                           
                             
                             <asp:TextBox ID="txtReceiptDate" runat="server" CssClass="TextBox"></asp:TextBox>
                   
                    </div>
                    <div style="float: left; width: 2%;">&nbsp;</div>
              </div>

             <div style="float: left; width: 100%;">
                    <div style="float: left; width: 2%;">&nbsp;</div>
                    <div style="float: left; width: 96%;">
                       <div style="float: left; width: 18%;">
                             <asp:Label ID="Label3" runat="server" Text="Location: "></asp:Label>
                         </div>
                        <asp:DropDownList ID="ddl_Location" runat="server" CssClass="style1" 
                            AutoPostBack="True" onselectedindexchanged="ddl_Location_SelectedIndexChanged">
                        </asp:DropDownList>
                        &nbsp;<asp:ImageButton ID="ImgBtnLocation" runat="server"  ImageUrl="~/Images/icon_search.png" />
                    </div>
                    
                    <div style="float: left; width: 2%;">&nbsp;</div>
             </div>

             <div style="float: left; width: 100%;">
                    <div style="float: left; width: 2%;">&nbsp;</div>
                    <div style="float: left; width: 96%;">
                    <div style="float: left; width: 18%;">
                     <asp:Label ID="Label4" runat="server" Text="Account No:  "></asp:Label>
                    </div>
                       
                        <asp:TextBox ID="TextBox1" runat="server" CssClass="TextBox"></asp:TextBox>
                        &nbsp;<asp:ImageButton ID="ImgBtnAccountNo" runat="server"  ImageUrl="~/Images/icon_search.png"/>
                    </div>
                    
                    <div style="float: left; width: 2%;">&nbsp;</div>
             </div>

             <div style="float: left; width: 100%;">
             <div style="float: left; width: 2%;">&nbsp;</div>
             <div style="float: left; width: 96%;">&nbsp;</div>
              <div style="float: left; width: 2%;">&nbsp;</div>
             </div>

               
                   <div style="float: left; width: 100%;">
                    <div style="float: left; width: 2%;">&nbsp;</div>
                    <div style="float: left; width: 96%;">
                        <div style="float: left; width: 15%; text-align: right;">&nbsp;Type:</div>
                        <div style="float: left; width: 18%;">&nbsp;
                        <asp:RadioButton ID="rdoBtnSystem" runat="server" Text="System" GroupName="Type" />
                        </div>
                        <div style="float: left; width: 18%;">&nbsp;
                        <asp:RadioButton ID="rdoBtnManual" runat="server" Text="Manual" GroupName="Type" />
                        </div>
                        
                    </div>
             
           
                    
                    <div style="float: left; width: 2%;">&nbsp;</div>
             </div>
               <div style="float: left; width: 100%;">
                    <div style="float: left; width: 2%;">&nbsp;</div>
                    <div style="float: left; width: 96%;">
                        <div style="float: left; width: 15%; text-align: right;">&nbsp;<asp:Label ID="Label1" runat="server" 
                                Text="Issued By :"></asp:Label>
                        </div>
                         <div style="float: left; width: 18%;">&nbsp;
                        <asp:RadioButton ID="rdoBtnCustomer" runat="server" Text="Customer" 
                                 GroupName="IssueBy" />
                        </div>
                        <div style="float: left; width: 18%;">&nbsp;
                        <asp:RadioButton ID="RadioButton3" runat="server" Text="Manager" 
                                GroupName="IssueBy" />
                        </div>
                    </div>
                    
                    <div style="float: left; width: 2%;">&nbsp;</div>
             </div>
              <div style="float: left; width: 100%;">
                    <div style="float: left; width: 2%;">&nbsp;</div>
                    <div style="float: left; width: 96%;">
                        <asp:Panel ID="Panel_ECD" runat="server" GroupingText="ECD">
                             <div style="float: left; width: 100%;">
                             <div style="float: left; width: 2%;">&nbsp;</div>
                             <div style="float: left; width: 96%;">&nbsp;</div>
                              <div style="float: left; width: 2%;">&nbsp;</div>
                             </div>
                            <asp:Label ID="Label5" runat="server" Text="ECD Type : "></asp:Label>
                            <asp:DropDownList ID="DropDownList1" runat="server" 
                                style="font-size: 11px; font-family: Verdana">
                                <asp:ListItem>Normal</asp:ListItem>
                                <asp:ListItem>Special</asp:ListItem>
                                <asp:ListItem>Voucher</asp:ListItem>
                                <asp:ListItem>Request</asp:ListItem>
                                <asp:ListItem>Req. Approve</asp:ListItem>
                            </asp:DropDownList>
                             <div style="float: left; width: 100%;">
                             <div style="float: left; width: 2%;">&nbsp;</div>
                             <div style="float: left; width: 96%;">&nbsp;</div>
                              <div style="float: left; width: 2%;">&nbsp;</div>
                             </div>
                        </asp:Panel>   
                    </div>
                    
                    <div style="float: left; width: 2%;">&nbsp;</div>
             </div>
               <div style="float: left; width: 100%;">
                    <div style="float: left; width: 2%;">&nbsp;</div>
                    <div style="float: left; width: 97%;">
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</div>
                    
                    <div style="float: left; width: 1%;">&nbsp;</div>
             </div>
             <div style="float: left; width: 100%; font-weight: normal; padding-top: 1px; padding-bottom: 1px;
                                height: 15px;">
                                <div style="float: left; width: 20px; border-right: 1px solid white;
                                    color: White; height: 15px;">
                                   </div>
                               
                                <div style="float: left; width: 72px; background-color: #507CD1; text-align: center;
                                    border-right: 1px solid white; color: White; height: 15px;">
                                   <%-- VAT Amt--%> Prefix</div>
                                <div style="float: left; width: 97px; background-color: #507CD1; text-align: center;
                                    border-right: 1px solid white; color: White; height: 15px;">
                                    <%--Amt--%> Receipt No
                                    </div>
                                <div style="float: left; width: 71px; background-color: #507CD1; text-align: center;
                                    border-right: 1px solid white; color: White; height: 15px;">
                                    <%--Book--%> Amount</div>
                                <div style="float: left; width: 78px; background-color: #507CD1; text-align: right;
                                    border-right: 1px solid white; color: White; height: 15px;">
                                    </div>
             </div>

             <div style="float: left; width: 100%; font-weight: normal; padding-top: 1px; padding-bottom: 1px;">
                                <div style="float: left; width: 20px; border-right: 1px solid white;">
                                    &nbsp;</div>
                               <%-- <div style="float: left; width: 99px; border-right: 1px solid white;">
                                    <asp:TextBox runat="server" ID="txtItem" CssClass="TextBoxUpper" ClientIDMode="Static"
                                        Width="95%"></asp:TextBox></div>--%>
                              <%--  <div style="float: left; width: 170px; border-right: 1px solid white;">
                                    <asp:TextBox runat="server" ID="txtDescription" BackColor="#EEEEEE" BorderWidth="0px"
                                        ClientIDMode="Static" Width="95%" Font-Size="10px"></asp:TextBox></div>--%>

                                <div style="float: left; width: 72px; border-right: 1px solid white;">
                                    <asp:DropDownList ID="ddlPrefix" runat="server" CssClass="style1" 
                                        Width="72px">
                                      </asp:DropDownList></div>
                                <div style="float: left; width: 78px; text-align: right; border-right: 1px solid white; height: 32px;">
                               
                                    <asp:TextBox runat="server" ID="txtReceiptNo" CssClass="TextBox" Style="text-align: right;"
                                        Width="95.4%"></asp:TextBox>
                                    &nbsp;</div>
                                    <div style="float: left; width: 16px; text-align: left; border-right: 1px solid white; height: 32px;">
                                        <asp:ImageButton ID="ImageButton3" runat="server" 
                                            ImageUrl="~/Images/icon_search.png" />
                                </div>
                                 
                                <div style="float: left; width: 72px; text-align: right; border-right: 1px solid white;">
                                    <asp:TextBox runat="server" ID="txtReciptAmount" CssClass="TextBox" Style="text-align: right;"
                                        Width="95.4%"></asp:TextBox></div>
                                <div style="float: left; width: 77px; text-align: right; border-right: 1px solid white;">
                                   <%-- <asp:TextBox runat="server" ID="txtDiscountAmt" CssClass="TextBox" Style="text-align: right;"
                                        Width="94.4%"></asp:TextBox>--%>
                                         <asp:ImageButton ID="ImgBtnAddReceipt" runat="server" 
                                            ImageUrl="~/Images/Add-16x16x16.ICO" 
                                        onclick="ImgBtnAddReceipt_Click" />
                                        </div>
                                <div style="float: left; width: 72px; text-align: right; border-right: 1px solid white;">
                                    </div>
                                <div style="float: left; width: 72px; text-align: right; border-right: 1px solid white;">
                                    </div>
                                <div style="float: left; width: 72px;">
                                    &nbsp;</div>
                                <div style="float: left; width: 90px;">
                                    &nbsp;</div>
                                <div style="float: left; width: 1%;">
                                    &nbsp;
                                  
                                </div>
                            </div>
             <div style="float: left; width: 100%;">
                    <div style="float: left; width: 96%;">
                        <div style="float: left; width: 20px;">&nbsp;</div>
                        <asp:Panel ID="Panel_ReceiptDet" runat="server" ScrollBars="Vertical">
                        <asp:GridView ID="grvReceiptDet" runat="server" CellPadding="4" 
                            ForeColor="#333333" CssClass="GridView" Width="252px" Height="90px" 
                                onrowdeleting="grvReceiptDet_RowDeleting">
                            <AlternatingRowStyle BackColor="White" />
                            <Columns>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:ImageButton ID="ImageButton5" runat="server" ImageUrl="~/Images/Delete.png"  CommandName="DeleteItem"/>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <EditRowStyle BackColor="#2461BF" />
                            <EmptyDataTemplate>
                                <asp:ImageButton ID="ImageButton4" runat="server" ImageUrl="~/Images/Delete.png"  />
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
                    
                    <div style="float: left; width: 2%;">&nbsp;</div>
             </div>

             <div style="float: left; width: 100%;">
             <div style="float: left; width: 2%;">&nbsp;</div>
             <div style="float: left; width: 96%;">&nbsp;</div>
              <div style="float: left; width: 2%;">&nbsp;</div>
             </div>

            </asp:Panel>
        </div>
   <div style="float: left; width: 1%;"> &nbsp;</div>
 <%--  *********************************Copied from Prabhath***********************************************************--%>
        <div id="div_AccountSummary" style="float: left; width: 35%; color:Navy;">
            <asp:Panel ID="Panel_AccountSummary" runat="server" GroupingText="Account Summary">
                
            </asp:Panel>
        </div>
    <div style="float: left; width: 2%;"> &nbsp;</div> 
    
    <%--Collaps control - Payment Items--%>
              
            <div style="float: left; width: 100%;">
            <div style="float: left; width: 2%;"> &nbsp;</div> 
                <div style="height: 18px; background-color: #1E4A9F; color: #FFFFFF; width: 96%;
                    float: left;"><div style="float: left; width: 2%;">&nbsp;</div>
                    <asp:Label ID="lbl_PaymentColl" runat="server" Text="Payments" Font-Bold="True"></asp:Label>
                    </div>
                    <div style="float: left;">
                    
                    <asp:Image runat="server" ID="Image4" ImageUrl="~/Images/16 X 16 DownArrow.jpg" ImageAlign="Right" />
                    </div>
               
                <%--<div style="float: left; width: 2%; padding-top:0.1px"" > &nbsp;</div>--%><%--OnSelectedIndexChanged="PaymentType_LostFocus" --%><%--OnClick="AddPayment"--%> <%--OnSelectedIndexChanged="PaymentType_LostFocus" --%>
    <%--                <asp:CollapsiblePanelExtender ID="CPEPayment" runat="server" TargetControlID="pnlPayment"
                        CollapsedSize="0" ExpandedSize="155" Collapsed="True" ExpandControlID="Image4"
                        CollapseControlID="Image4" AutoCollapse="False" AutoExpand="False" ScrollContents="false"
                        ExpandDirection="Vertical" ImageControlID="Image4" ExpandedImage="~/Images/16 X 16 UpArrow.jpg"
                        CollapsedImage="~/Images/16 X 16 DownArrow.jpg">
                    </asp:CollapsiblePanelExtender>--%>
                  <div style="float: left; width: 100%;"> 
                      <%--OnClick="AddPayment"--%> 

                    <div style="float: left; width: 100%; padding-top:0.2px">
                        <asp:Panel ID="pnlPayment" runat="server" Height="171px" GroupingText="  ">
                            <div style="float: left; width: 50%;">
                                <div style="float: left; width: 100%;">
                                    <div style="float: left; width: 3%;">&nbsp;</div>
                                    <div style="float: left; width: 15%;">  Pay Mode </div>
                                    <div style="float: left; width: 25%;">  <%--OnSelectedIndexChanged="PaymentType_LostFocus" --%> 
                                        <asp:DropDownList ID="ddlPayMode" runat="server">
                                        </asp:DropDownList>
                                    </div>
                                    <div style="float: left; width: 1%;">&nbsp;</div>
                                    <div style="float: left; width: 10%;">Amount  </div>
                                    <div style="float: left; width: 35%;"> <asp:TextBox ID="txtPayAmount" runat="server" CssClass="TextBoxUpper" Width="50%"></asp:TextBox> &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <asp:Button ID="btnPayAdd" runat="server" CssClass="Button" Text="Add" OnClick="AddPayment"   /><%--OnClick="AddPayment"--%> </div>
                                </div>
                                <div style="float: left; width: 100%; padding: 2px 2px 0px 0px;">
                                    <div style="float: left; width: 3%;">&nbsp;</div>
                                    <div style="float: left; width: 15%;"> Remarks  </div>
                                    <div style="float: left; width: 75%;"> <asp:TextBox ID="txtPayRemarks" runat="server" CssClass="TextBox" Width="100%" TextMode="MultiLine"  Rows="2"></asp:TextBox></div>
                                </div>
                                <div style="float: left; width: 100%; padding: 2px 2px 0px 0px;">
                                    <%--Credit/Cheque/Bank Slip payment--%>
                                    <div style="float: left; width: 100%; height:100px;" runat="server" id="divCredit" visible="false" >
                                        <div style="float: left; width: 100%;">
                                            <div style="float: left; width: 3%;">&nbsp;</div>
                                            <div style="float: left; width: 15%;">Card No</div>
                                            <div style="float: left; width: 75%;  padding-bottom:2px;"> <asp:TextBox runat="server" ID="txtPayCrCardNo" CssClass="TextBox" Width="80%"></asp:TextBox> </div>
                                        </div>
                                        <div style="float: left; width: 100%;">
                                            <div style="float: left; width: 3%;">&nbsp;</div>
                                            <div style="float: left; width: 15%;">Bank </div>
                                            <div style="float: left; width: 20%;  padding-bottom:2px;"> <asp:TextBox runat="server" ID="txtPayCrBank" CssClass="TextBox" Width="30%"></asp:TextBox> </div>

                                            <div style="float: left; width: 1%;">&nbsp;</div>
                                            <div style="float: left; width: 15%;">Branch </div>
                                            <div style="float: left; width: 20%;  padding-bottom:2px;"> <asp:TextBox runat="server" ID="txtPayCrBranch" CssClass="TextBox" Width="20%"></asp:TextBox> </div>
                                        </div>
                                        <div style="float: left; width: 100%;">
                                            <div style="float: left; width: 3%;">&nbsp;</div>
                                            <div style="float: left; width: 15%;">Card Type </div>
                                            <div style="float: left; width: 15%;  padding-bottom:2px;"> <asp:TextBox runat="server" ID="txtPayCrCardType" CssClass="TextBox" Width="90%"></asp:TextBox> </div>

                                            <div style="float: left; width: 3%;">&nbsp;</div>
                                            <div style="float: left; width: 15%;">Expiry Date </div>
                                            <div style="float: left; width: 25%;  padding-bottom:2px;"> <asp:TextBox runat="server" ID="txtPayCrExpiryDate" CssClass="TextBox" Width="70%"></asp:TextBox> &nbsp;<asp:Image ID="btnImgCrExpiryDate" runat="server" ImageUrl="~/Images/Calendar_scheduleHS.png"  ImageAlign="Middle" /> </div>
                                        </div>
                                        <div style="float: left; width: 100%;">
                                            <div style="float: left; width: 3%;">&nbsp;</div>
                                            <div style="float: left; width: 15%;">Promotion </div>
                                            <div style="float: left; width: 75%;  padding-bottom:2px;"> <asp:CheckBox runat="server" ID="chkPayCrPromotion" /> &nbsp;  Period &nbsp; <asp:TextBox runat="server" ID="txtPayCrPeriod" CssClass="TextBoxNumeric" Width="20%"></asp:TextBox> months </div>
                                        </div>
                                        <div style="float: left; width: 100%;">
                                            <div style="float: left; width: 3%;">&nbsp;</div>
                                            <div style="float: left; width: 15%;">Batch No </div>
                                            <div style="float: left; width: 75%;  padding-bottom:2px;"> <asp:TextBox runat="server" ID="txtPayCrBatchNo" CssClass="TextBox" Width="80%"></asp:TextBox> </div>
                                        </div>

                                    </div>
                                    <%--Advance receipt/Credit Note payment--%>
                                    <div style="float: left; width: 100%;" runat="server" id="divAdvReceipt" visible="false">
                                        <div style="float: left; width: 100%;">
                                            <div style="float: left; width: 3%;">&nbsp;</div>
                                            <div style="float: left; width: 20%;">Referance</div>
                                            <div style="float: left; width: 75%;  padding-bottom:2px;"> <asp:TextBox runat="server" ID="txtPayAdvReceiptNo" CssClass="TextBox" Width="80%"></asp:TextBox> </div>
                                        </div>
                                        <div style="float: left; width: 100%;">
                                            <div style="float: left; width: 3%;">&nbsp;</div>
                                            <div style="float: left; width: 20%;">Ref. Amount</div>
                                            <div style="float: left; width: 75%;  padding-bottom:2px;"> <asp:TextBox runat="server" ID="txtPayAdvRefAmount" CssClass="TextBox" Width="80%"></asp:TextBox> </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div style="float: left; width: 50%;">
                                <div style="float: left; width: 100%; ">
                                                                <asp:Panel ID="pnlPay" runat="server" Height="140px" ScrollBars="Auto">
                                <asp:GridView ID="gv_Payment" runat="server" AutoGenerateColumns="False"   CssClass="GridView" 
                                    CellPadding="3" ForeColor="#333333" GridLines="Both" DataKeyNames="sard_pay_tp,sard_settle_amt" OnRowDeleting="gVPayment_OnDelete"  ShowHeaderWhenEmpty="true">
                                    <EditRowStyle BackColor="#2461BF" />
                                    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                    <HeaderStyle BackColor="#507CD1" ForeColor="White" Font-Bold="True" />
                                    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                    <RowStyle BackColor="#EFF3FB" />
                                    <AlternatingRowStyle BackColor="White" />
                                    <EmptyDataTemplate > <div style="width:100%; text-align:center;"> No data found </div> </EmptyDataTemplate>
                                    <Columns>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:ImageButton ID="btnImgDelete" runat="server" ImageAlign="Middle" ImageUrl="~/Images/Delete.png" Width="10px" Height="10px" CommandName="Delete" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField='sard_seq_no' HeaderText='' Visible="false" />
                                        <asp:BoundField DataField='sard_line_no' HeaderText='' Visible="false" />
                                        <asp:BoundField DataField='sard_receipt_no' HeaderText=''  Visible="false" />
                                        <asp:BoundField DataField='sard_inv_no' HeaderText=''  Visible="false"/>
                                        <asp:BoundField DataField='sard_pay_tp' HeaderText='Payment Type' ItemStyle-Width="110px" HeaderStyle-Width="110px" />
                                        <asp:BoundField DataField='sard_ref_no' HeaderText='' Visible="false" />
                                        <asp:BoundField DataField='sard_chq_bank_cd' HeaderText='Bank' ItemStyle-Width="90px"  HeaderStyle-Width="90px" />
                                        <asp:BoundField DataField='sard_chq_branch' HeaderText='Branch'  ItemStyle-Width="90px"  HeaderStyle-Width="90px" />
                                        <asp:BoundField DataField='sard_deposit_bank_cd' HeaderText='' Visible="false" />
                                        <asp:BoundField DataField='sard_deposit_branch' HeaderText='' Visible="false" />
                                        <asp:BoundField DataField='sard_credit_card_bank' HeaderText='' Visible="false" />
                                        <asp:BoundField DataField='sard_cc_tp' HeaderText='Card Type' />
                                        <asp:BoundField DataField='sard_cc_expiry_dt' HeaderText='Expiry Date' Visible="false" />
                                        <asp:BoundField DataField='sard_cc_is_promo' HeaderText='Promotion' Visible="false" />
                                        <asp:BoundField DataField='sard_cc_period' HeaderText='Period' Visible="false" />
                                        <asp:BoundField DataField='sard_gv_issue_loc' HeaderText='' Visible="false" />
                                        <asp:BoundField DataField='sard_gv_issue_dt' HeaderText='' Visible="false" />
                                        <asp:BoundField DataField='sard_settle_amt' HeaderText='Amount' />
                                        <asp:BoundField DataField='sard_sim_ser' HeaderText='' Visible="false" />
                                    </Columns>
                                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                    <SortedAscendingCellStyle BackColor="#F5F7FB" />
                                    <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                                    <SortedDescendingCellStyle BackColor="#E9EBEF" />
                                    <SortedDescendingHeaderStyle BackColor="#4870BE" />
                                </asp:GridView>
                                </asp:Panel>
                                </div>
                                <div style="float: left; width: 100%; "> 
                                    <div style="float: left; width: 100%; "> Paid Amount &nbsp; <asp:Label runat="server" ID="lblPayPaid" Text ="0.00"></asp:Label> &nbsp; &nbsp; Balance Amount &nbsp; <asp:Label runat="server" ID="lblPayBalance" Text ="0.00"></asp:Label> </div>
                                </div>
                            </div>
                        </asp:Panel>
                    </div>
                 
                  </div>
<%--*********************************************************************************************--%>
                 </div>
</div>
</ContentTemplate>
</asp:UpdatePanel>
   
</asp:Content>
