<%@ Page Title="Return Cheque Settlement" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    EnableEventValidation="false" CodeBehind="ReturnChequeSettlement.aspx.cs" Inherits="FF.WebERPClient.Financial_Modules.ReturnChequeSettlement" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="../UserControls/uc_Payment.ascx" TagName="Pay" TagPrefix="uc3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script type="text/javascript">
        //javascript
        function DisplayConfirmation() {
            var val = document.getElementById('<%=HiddenFieldPay.ClientID  %>').value;

            if (val != "" && val != null && val != "-999") {
                return confirm('Are you sure?');
            }
//            else if (val != "0.00" && val != "999") {
//                alert('Can not save, balance greater than 0');
//            }
            { return false; }}

            function checkTextAreaMaxLength( ) {
                var mLen = document.getElementById('<%=txtPayRemarks.ClientID %>').value;
                if (mLen.length > 50) {
                    document.getElementById('<%=txtPayRemarks.ClientID %>').focus(); //set focus to prevent jumping         o
                    document.getElementById('<%=txtPayRemarks.ClientID %>').value = document.getElementById('<%=txtPayRemarks.ClientID %>').value.substring(0, 50); //truncate the value         
                    document.getElementById('<%=txtPayRemarks.ClientID %>').scrollTop = document.getElementById('MainContent_txtPayRemarks').scrollHeight; //scroll to the end to prevent jumping         
                    return false; 
                }

            }

            function numbersonly(e, decimal) {
                var key;
                var keychar;

                if (window.event) {
                    key = window.event.keyCode;
                }
                else if (e) {
                    key = e.which;
                }
                else {
                    return true;
                }
                keychar = String.fromCharCode(key);

                if ((key == null) || (key == 0) || (key == 8) || (key == 9) || (key == 13) || (key == 27)) {
                    return true;
                }
                else if ((("0123456789").indexOf(keychar) > -1)) {
                    return true;
                }
                else if (decimal && (keychar == ".")) {
                    return true;
                }
                else
                    return false;
            }

        
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="updtMainPnl" runat="server">
        <ContentTemplate>
            <asp:HiddenField ID="HiddenFieldPay" runat="server" Value="-999" />
            <div style="float: left; width: 100%; color: Black;">
                <div style="height: 22px; text-align: right;" class="PanelHeader">
                    <asp:Button ID="ButtonSave" runat="server" Text="Save" CssClass="Button" OnClick="ButtonSave_Click"
                        OnClientClick="return DisplayConfirmation()" />
                    <asp:Button ID="ButtonClear" runat="server" Text="Clear" CssClass="Button" OnClick="ButtonClear_Click" />
                    <asp:Button ID="ButtonClose" runat="server" Text="Close" CssClass="Button" OnClick="ButtonClose_Click" />
                </div>
                <div>
                    &nbsp;
                </div>
                <div style="float: left; width: 100%; color: Black;">
                    <div class="PanelHeader">
                        Retun Cheque Details
                    </div>
                    <div>
                        &nbsp;
                    </div>
                    <div style="float: left; width: 99%;">
                        <asp:Panel ID="Panel1" runat="server" ScrollBars="Auto">
                        <div style="float: left; width:1%;">
                                            &nbsp;</div>
                            <asp:GridView ID="GridViewCheques" runat="server" Width="98%" EmptyDataText="No data found"
                                ShowHeaderWhenEmpty="True" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333"
                                CssClass="GridView" DataKeyNames="srcq_pc,SRCQ_REF" OnSelectedIndexChanged="GridViewCheques_SelectedIndexChanged">
                                <AlternatingRowStyle BackColor="White" />
                                <Columns>
                                    <asp:BoundField HeaderText="Cheque No." DataField="SRCQ_CHQ" />
                                    <asp:BoundField HeaderText="Bank" DataField="SRCQ_BANK" />
                                    <asp:BoundField HeaderText="Amount" DataField="SRCQ_ACT_VAL" DataFormatString='<%$ appSettings:FormatToCurrency  %>' >
                                     <HeaderStyle HorizontalAlign="Right" />
                                        <ItemStyle HorizontalAlign="Right" />
                                        </asp:BoundField>
                                    <asp:BoundField HeaderText="Charge Interest " DataField="SRCQ_INTR" >
                                     <HeaderStyle HorizontalAlign="Right" />
                                        <ItemStyle HorizontalAlign="Right" />
                                        </asp:BoundField>
                                    <asp:BoundField HeaderText="Settle Amount" DataField="SRCQ_SET_VAL" DataFormatString='<%$ appSettings:FormatToCurrency  %>'>
                                        <HeaderStyle HorizontalAlign="Right" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:BoundField>
                                    <asp:BoundField HeaderText="Return Date " DataField="SRCQ_RTN_DT" DataFormatString='<%$ appSettings:FormatToDate %>'>
                                    </asp:BoundField>
                                    <asp:BoundField HeaderText="Return Bank " DataField="SRCQ_RTN_BANK" />
                                    <asp:TemplateField HeaderStyle-Width="20px">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="imgSelect" runat="server" CommandName="SELECT" ImageUrl="~/Images/download_arrow_icon.png"
                                                Height="15px" />
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" Width="20px" Height="15px" />
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
                <div>
                    &nbsp;
                </div>
                <div style="float: left; width: 100%; color: Black;" id="DivSettleAmo" runat="server"
                    visible="true">
                    <div style="float: left; width: 100%;">
                    <div style="float: left; width: 0.5%;">
                                            &nbsp;</div>
                        <div style="float: left; width: 15%;">
                            Settle Amount
                        </div>
                        <div style="float: left; width: 25%;">
                            <asp:TextBox ID="TextBoxSAmo" runat="server" CssClass="TextBox" Enabled="false"></asp:TextBox>
                        </div>
                    </div>
                    <div>
                     &nbsp;
                    </div>
                    <%--    <div style="float: left; width: 100%;padding:2px 0px 0px 0px;"> 
                      <div style="float: left; width: 20%;">
                    Select Paymode
                    </div>
                     <div style="float: left; width: 20%;">
                  <asp:DropDownList ID="DropDownListPayMode" runat="server" CssClass="ComboBox"></asp:DropDownList>
                    </div>
                    </div>--%>
                    <div style="float: left; width: 100%; padding: 2px 0px 0px 0px;">
                        <div style="float: left; width: 100%; padding-top: 3px">
                            <asp:Panel ID="pnlPayment" runat="server" Height="171px">
                                <div style="float: left; width: 50%;">
                                    <div style="float: left; width: 100%;">
                                        <div style="float: left; width: 1%;">
                                            &nbsp;</div>
                                        <div style="float: left; width: 15%;">
                                            Pay Mode
                                        </div>
                                        <div style="float: left; width: 25%;">
                                            <asp:DropDownList ID="ddlPayMode" runat="server" Width="80%" CssClass="ComboBox"
                                                OnSelectedIndexChanged="PaymentType_LostFocus" AutoPostBack="true">
                                            </asp:DropDownList>
                                        </div>
                                        <div style="float: left; width: 1%;">
                                            &nbsp;</div>
                                        <div style="float: left; width: 10%;">
                                            Amount
                                        </div>
                                        <div style="float: left; width: 35%;">
                                            <asp:TextBox ID="txtPayAmount" runat="server" CssClass="TextBoxUpper" 
                                                Width="50%" MaxLength="8" onKeyPress="return numbersonly(event,true)"></asp:TextBox>
                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                            <asp:Button ID="btnPayAdd" runat="server" CssClass="Button" Text="Add" OnClick="AddPayment" />
                                        </div>
                                    </div>
                                    <div style="float: left; width: 100%; padding: 2px 2px 0px 0px;">
                                        <div style="float: left; width: 1%;">
                                            &nbsp;</div>
                                        <div style="float: left; width: 15%;">
                                            Remarks
                                        </div>
                                        <div style="float: left; width: 75%;">
                                            <asp:TextBox ID="txtPayRemarks" runat="server" CssClass="TextBox" Width="100%" TextMode="MultiLine"
                                                Rows="2"  onKeyUp="checkTextAreaMaxLength();"></asp:TextBox></div>
                                    </div>
                                    <div style="float: left; width: 100%; padding: 2px 2px 0px 0px;">
                                        <%--Credit/Cheque/Bank Slip payment--%>
                                        <div style="float: left; width: 100%; height: 100px;" runat="server" id="divCredit"
                                            visible="false">
                                            <div style="float: left; width: 100%;" id="divCRDno" runat="server">
                                                <div style="float: left; width: 1%;">
                                                    &nbsp;</div>
                                                <div style="float: left; width: 15%;">
                                                    Card No</div>
                                                <div style="float: left; width: 75%; padding-bottom: 2px;">
                                                    <asp:TextBox runat="server" ID="txtPayCrCardNo" CssClass="TextBox" Width="80%"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div style="float: left; width: 100%;">
                                                <div style="float: left; width: 1%;">
                                                    &nbsp;</div>
                                                <div style="float: left; width: 15%;">
                                                    Bank
                                                </div>
                                                <div style="float: left; width: 20%; padding-bottom: 2px;">
                                                    <asp:TextBox runat="server" ID="txtPayCrBank" CssClass="TextBox" Width="30%"></asp:TextBox>
                                                </div>
                                                <div style="float: left; width: 1%;">
                                                    &nbsp;</div>
                                                <div style="float: left; width: 15%;">
                                                    Branch
                                                </div>
                                                <div style="float: left; width: 20%; padding-bottom: 2px;">
                                                    <asp:TextBox runat="server" ID="txtPayCrBranch" CssClass="TextBox" Width="20%"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div style="float: left; width: 100%;">
                                                <div style="float: left; width: 1%;">
                                                    &nbsp;</div>
                                                <div style="float: left; width: 15%;">
                                                    Card Type
                                                </div>
                                                <div style="float: left; width: 15%; padding-bottom: 2px;">
                                                    <asp:TextBox runat="server" ID="txtPayCrCardType" CssClass="TextBox" Width="90%"></asp:TextBox>
                                                </div>
                                                <div style="float: left; width: 1%;">
                                                    &nbsp;</div>
                                                <div style="float: left; width: 15%;">
                                                    Expiry Date
                                                </div>
                                                <div style="float: left; width: 25%; padding-bottom: 2px;">
                                                    <asp:TextBox runat="server" ID="txtPayCrExpiryDate" CssClass="TextBox" Width="70%"></asp:TextBox>
                                                    <asp:CalendarExtender ID="CalendarExtender2" runat="server" Enabled="True" Format="dd/MM/yyyy"
                                                        TargetControlID="txtPayCrExpiryDate">
                                                    </asp:CalendarExtender>
                                                    &nbsp;<asp:Image ID="btnImgCrExpiryDate" runat="server" ImageUrl="~/Images/Calendar_scheduleHS.png"
                                                        ImageAlign="Middle" />
                                                </div>
                                            </div>
                                            <div style="float: left; width: 100%;">
                                                <div style="float: left; width: 1%;">
                                                    &nbsp;</div>
                                                <div style="float: left; width: 15%;">
                                                    Promotion
                                                </div>
                                                <div style="float: left; width: 75%; padding-bottom: 2px;">
                                                    <asp:CheckBox runat="server" ID="chkPayCrPromotion" />
                                                    &nbsp; Period &nbsp;
                                                    <asp:TextBox runat="server" ID="txtPayCrPeriod" CssClass="TextBoxNumeric" Width="20%"></asp:TextBox>
                                                    months
                                                </div>
                                            </div>
                                            <div style="float: left; width: 100%;" id="divCredBatch">
                                                <div style="float: left; width: 1%;">
                                                    &nbsp;</div>
                                                <div style="float: left; width: 15%;">
                                                    Batch No
                                                </div>
                                                <div style="float: left; width: 75%; padding-bottom: 2px;">
                                                    <asp:TextBox runat="server" ID="txtPayCrBatchNo" CssClass="TextBox" Width="80%"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div style="float: left; width: 100%;" id="divChequeNum" runat="server">
                                                <div style="float: left; width: 1%;">
                                                    &nbsp;</div>
                                                <div style="float: left; width: 15%;">
                                                    Cheque No
                                                </div>
                                                <div style="float: left; width: 75%; padding-bottom: 2px;">
                                                    <asp:TextBox runat="server" ID="txtChequeNo" CssClass="TextBox" Width="80%"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <%--Advance receipt/Credit Note payment--%>
                                        <div style="float: left; width: 100%;" runat="server" id="divAdvReceipt" visible="false">
                                            <div style="float: left; width: 100%;">
                                                <div style="float: left; width: 1%;">
                                                    &nbsp;</div>
                                                <div style="float: left; width: 20%;">
                                                    Referance</div>
                                                <div style="float: left; width: 75%; padding-bottom: 2px;">
                                                    <asp:TextBox runat="server" ID="txtPayAdvReceiptNo" CssClass="TextBox" Width="80%"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div style="float: left; width: 100%;">
                                                <div style="float: left; width: 1%;">
                                                    &nbsp;</div>
                                                <div style="float: left; width: 20%;">
                                                    Ref. Amount</div>
                                                <div style="float: left; width: 75%; padding-bottom: 2px;">
                                                    <asp:TextBox runat="server" ID="txtPayAdvRefAmount" CssClass="TextBox" Width="80%"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div style="float: left; width: 50%;">
                                    <div style="float: left; width: 100%;">
                                        <asp:Panel ID="pnlPay" runat="server" Height="140px" ScrollBars="Auto">
                                            <asp:GridView ID="gvPayment" runat="server" AutoGenerateColumns="False" CssClass="GridView"
                                                CellPadding="3" ForeColor="#333333" DataKeyNames="sard_pay_tp,sard_settle_amt"
                                                OnRowDeleting="gvPayment_OnDelete" ShowHeaderWhenEmpty="True">
                                                <EditRowStyle BackColor="#2461BF" />
                                                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                                <HeaderStyle BackColor="#507CD1" ForeColor="White" Font-Bold="True" />
                                                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                                <RowStyle BackColor="#EFF3FB" />
                                                <AlternatingRowStyle BackColor="White" />
                                                <EmptyDataTemplate>
                                                    <div style="width: 100%; text-align: center;">
                                                        No data found
                                                    </div>
                                                </EmptyDataTemplate>
                                                <Columns>
                                                    <asp:TemplateField>
                                                        <ItemTemplate>
                                                            <asp:ImageButton ID="btnImgDelete" runat="server" ImageAlign="Middle" ImageUrl="~/Images/Delete.png"
                                                                Width="10px" Height="10px" CommandName="Delete" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField='sard_seq_no' HeaderText='' Visible="false" />
                                                    <asp:BoundField DataField='sard_line_no' HeaderText='' Visible="false" />
                                                    <asp:BoundField DataField='sard_receipt_no' HeaderText='' Visible="false" />
                                                    <asp:BoundField DataField='sard_inv_no' HeaderText='' Visible="false" />
                                                    <asp:BoundField DataField='sard_pay_tp' HeaderText='Payment Type' ItemStyle-Width="110px"
                                                        HeaderStyle-Width="110px">
                                                        <HeaderStyle Width="110px" />
                                                        <ItemStyle Width="110px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField='sard_ref_no' HeaderText='' Visible="false" />
                                                    <asp:BoundField DataField='sard_chq_bank_cd' HeaderText='Bank' ItemStyle-Width="90px"
                                                        HeaderStyle-Width="90px">
                                                        <HeaderStyle Width="90px" />
                                                        <ItemStyle Width="90px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField='sard_chq_branch' HeaderText='Branch' ItemStyle-Width="90px"
                                                        HeaderStyle-Width="90px">
                                                        <HeaderStyle Width="90px" />
                                                        <ItemStyle Width="90px" />
                                                    </asp:BoundField>
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
                                                    <asp:BoundField DataField='sard_receipt_no' HeaderText='receipt_no' Visible="False" />
                                                    <asp:BoundField DataField='sard_anal_3' HeaderText="Bank/Other Charges" />
                                                </Columns>
                                                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                                <SortedAscendingCellStyle BackColor="#F5F7FB" />
                                                <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                                                <SortedDescendingCellStyle BackColor="#E9EBEF" />
                                                <SortedDescendingHeaderStyle BackColor="#4870BE" />
                                            </asp:GridView>
                                        </asp:Panel>
                                    </div>
                                    <div style="float: left; width: 100%;">
                                        <div style="float: left; width: 1%;">
                                            &nbsp;</div>
                                        <div style="float: left; width: 20%; background-color: #1569C7; color: White; height: 22px;">
                                            Paid Amount
                                        </div>
                                        <div style="float: left; width: 18%; text-align: right; border-color: Black; border-style: solid;
                                            border-width: 1px;">
                                            <asp:Label runat="server" ID="lblPayPaid" Text="0.00"></asp:Label>
                                        </div>
                                        <div style="float: left; width: 18%;">
                                            &nbsp;
                                        </div>
                                        <div style="float: left; width: 20%; background-color: #1569C7; color: White; height: 22px;">
                                            Balance Amount
                                        </div>
                                        <div style="float: left; width: 18%; text-align: right; border-color: Black; border-style: solid;
                                            border-width: 1px;">
                                            <asp:Label runat="server" ID="lblPayBalance" Text="0.00"></asp:Label>
                                        </div>
                                        <div style="float: left; width: 1%;">
                                            &nbsp;</div>
                                    </div>
                                </div>
                            </asp:Panel>
                        </div>
                    </div>
                    <div>
                        &nbsp;
                    </div>
                    <div style="float: left; text-align: right; width: 90%;">
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
