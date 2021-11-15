<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="uc_Payment.ascx.cs"
    Inherits="FF.AbansTours.UserControls.uc_Payment" %>
<%--<link href="../Styles/Site.css" rel="stylesheet" type="text/css" />--%>
<div style="float: left; width: 100%; background-color:CORNSILK;">
    <div style="float: left; width: 100%; padding-bottom: 2px;">
        <div style="float: left; width: 1%;">
            &nbsp;</div>
        <div style="float: left; width: 13%;">
            Pay Mode
        </div>
        <div style="float: left; width: 35%;">
            <asp:DropDownList ID="ddlPayMode" Width="60%" runat="server" CssClass="ComboBox"
                OnSelectedIndexChanged="PaymentType_LostFocus" AutoPostBack="true">
            </asp:DropDownList>
        </div>
        <div style="float: left; width: 1%;">
            &nbsp;</div>
        <div style="float: left; width: 15%;">
            Amount
        </div>
        <div style="float: left; width: 29%;">
            <asp:TextBox ID="txtPayAmount" runat="server" CssClass="TextBoxUpper" Width="50%"></asp:TextBox>
            &nbsp;
            <asp:Button ID="btnPayAdd" runat="server" CssClass="Button" Text="Add" OnClick="AddPayment" />
        </div>
    </div>
    <div style="float: left; width: 100%; padding-bottom: 2px;">
        <div style="float: left; width: 1%;">
            &nbsp;</div>
        <div style="float: left; width: 13%;">
            Remarks
        </div>
        <div style="float: left; width: 75%;">
            <asp:TextBox ID="txtPayRemarks" runat="server" CssClass="TextBox" Width="100%" TextMode="MultiLine"
                Rows="2"></asp:TextBox></div>
    </div>
    <div style="float: left; width: 50%;">
        <div style="float: left; width: 100%; padding: 2px 2px 0px 0px;">
            <%--Credit/Cheque/Bank Slip payment--%>
            <div style="float: left; width: 100%; height: 100px;" runat="server" id="divCredit"
                visible="false">
                <div style="float: left; width: 100%;">
                    <div style="float: left; width: 1%;">
                        &nbsp;</div>
                    <div style="float: left; width: 27%;">
                        Card No</div>
                    <div style="float: left; width: 72%; padding-bottom: 2px;">
                        <asp:TextBox runat="server" ID="txtPayCrCardNo" CssClass="TextBox" Width="100%" MaxLength="15"></asp:TextBox>
                    </div>
                </div>
                <div style="float: left; width: 100%;">
                    <div style="float: left; width: 1%;">
                        &nbsp;</div>
                    <div style="float: left; width: 27%;">
                        Bank
                    </div>
                    <div style="float: left; width: 50%; padding-bottom: 2px;">
                        <asp:TextBox runat="server" ID="txtPayCrBank" CssClass="TextBox" Width="60%"></asp:TextBox><asp:ImageButton
                            ID="ImageButton1" runat="server" ImageUrl="~/Images/icon_search.png" ImageAlign="Middle"
                            OnClick="ImgBankSearch_Click" />
                    </div>
                </div>
                <div style="float: left; width: 100%;">
                    <div style="float: left; width: 1%;">
                        &nbsp;</div>
                    <div style="float: left; width: 27%;">
                        Branch
                    </div>
                    <div style="float: left; width: 65%; padding-bottom: 2px;">
                        <asp:TextBox runat="server" ID="txtPayCrBranch" CssClass="TextBox" Width="100%" MaxLength="15"></asp:TextBox>
                    </div>
                </div>
                <div style="float: left; width: 100%;">
                    <div style="float: left; width: 1%;">
                        &nbsp;</div>
                    <div style="float: left; width: 27%;">
                        Card Type
                    </div>
                    <div style="float: left; width: 27%; padding-bottom: 2px;">
                        <asp:DropDownList runat="server" ID="txtPayCrCardType" CssClass="ComboBox" Width="90%">
                            <asp:ListItem Text="" Selected="True"></asp:ListItem>
                            <asp:ListItem Text="HSBC"></asp:ListItem>
                            <asp:ListItem Text="AMEX"></asp:ListItem>
                            <asp:ListItem Text="VISA"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>
                <div style="float: left; width: 100%;">
                    <div style="float: left; width: 1%;">
                        &nbsp;</div>
                    <div style="float: left; width: 27%;">
                        Expiry Date
                    </div>
                    <div style="float: left; width: 40%; padding-bottom: 2px;">
                        <asp:TextBox runat="server" ID="txtPayCrExpiryDate" Enabled="false" CssClass="TextBox"
                            Width="70%"></asp:TextBox>
                        <asp:Image ID="btnImgCrExpiryDate" runat="server" ImageUrl="~/Images/Calendar_scheduleHS.png"
                            ImageAlign="Middle" />
                    </div>
                </div>
                <div style="float: left; width: 100%;">
                    <div style="float: left; width: 1%;">
                        &nbsp;</div>
                    <div style="float: left; width: 27%;">
                        Promotion
                    </div>
                    <div style="float: left; width: 67%; padding-bottom: 2px;">
                        <asp:CheckBox runat="server" ID="chkPayCrPromotion" onclick="PromotionPeriod()" />
                        &nbsp;&nbsp;&nbsp; Period &nbsp;
                        <asp:TextBox runat="server" ID="txtPayCrPeriod" CssClass="TextBoxNumeric" Width="20%"
                            MaxLength="2"></asp:TextBox>
                        months
                    </div>
                </div>
            </div>
            <%--Advance receipt/Credit Note payment/Loyalty/Gift vouchas--%>
            <div style="float: left; width: 100%;" runat="server" id="divAdvReceipt" visible="false">
                <div style="float: left; width: 100%;">
                    <div style="float: left; width: 1%;">
                        &nbsp;</div>
                    <div style="float: left; width: 27%;">
                        Referance</div>
                    <div style="float: left; width: 71%; padding-bottom: 2px;">
                        <asp:TextBox runat="server" ID="txtPayAdvReceiptNo" CssClass="TextBox" Width="80%"></asp:TextBox><asp:ImageButton
                            ID="ImageButton2" runat="server" ImageUrl="~/Images/icon_search.png" ImageAlign="Middle"
                            OnClick="ImgBankSearch_Click" />
                    </div>
                </div>
                <div style="float: left; width: 100%;">
                    <div style="float: left; width: 1%;">
                        &nbsp;</div>
                    <div style="float: left; width: 27%;">
                        Ref. Amount</div>
                    <div style="float: left; width: 25%; padding-bottom: 2px;">
                        <asp:TextBox runat="server" ID="txtPayAdvRefAmount" CssClass="TextBox" Width="80%"></asp:TextBox>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div style="float: left; width: 1%;">
        &nbsp;
    </div>
    <div style="float: left; width: 49%;">
        <div style="float: left; width: 100%;">
            <asp:Panel ID="pnlPay" runat="server" Height="120px" ScrollBars="Auto">
                <asp:GridView ID="gvPayment" runat="server" AutoGenerateColumns="False" CssClass="GridView"
                    CellPadding="3" ForeColor="#333333" GridLines="Both" DataKeyNames="sard_pay_tp,sard_settle_amt"
                    OnRowDeleting="gvPayment_OnDelete" ShowHeaderWhenEmpty="true">
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
                            HeaderStyle-Width="110px" />
                        <asp:BoundField DataField='sard_ref_no' HeaderText='' Visible="false" />
                        <asp:BoundField DataField='sard_chq_bank_cd' HeaderText='Bank' ItemStyle-Width="90px"
                            HeaderStyle-Width="90px" />
                        <asp:BoundField DataField='sard_chq_branch' HeaderText='Branch' ItemStyle-Width="90px"
                            HeaderStyle-Width="90px" />
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
        <div style="float: left; width: 100%;">
            <div style="float: left; width: 1%;">
                &nbsp;</div>
            <div style="float: left; width: 20%; background-color: #1569C7; color: White; height: 15px;">
                Paid Amount
            </div>
            <div style="float: left; width: 18%; text-align: right; border-color: Black; border-style: solid;
                border-width: 1px;">
                <asp:Label runat="server" ID="lblPayPaid" Text="0.00"></asp:Label>
            </div>
            <div style="float: left; width: 18%;">
                &nbsp;
            </div>
            <div style="float: left; width: 20%; background-color: #1569C7; color: White; height: 15px;">
                Balance Amount</div>
            <div style="float: left; width: 18%; text-align: right; border-color: Black; border-style: solid;
                border-width: 1px;">
                <asp:Label runat="server" ID="lblPayBalance" Text="0.00"></asp:Label>
            </div>
            <div style="float: left; width: 1%;">
                &nbsp;</div>
        </div>
    </div>
</div>
