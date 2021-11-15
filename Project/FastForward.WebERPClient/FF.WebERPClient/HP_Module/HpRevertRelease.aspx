<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="HpRevertRelease.aspx.cs" Inherits="FF.WebERPClient.HP_Module.HpRevertRelease"
    EnableEventValidation="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="../UserControls/uc_HpAccountSummary.ascx" TagName="uc_HpAccountSummary"
    TagPrefix="uc1" %>
<%@ Register Src="~/UserControls/uc_MsgInfo.ascx" TagName="uc_MsgInfo" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server"></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
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

        function ToUpper(ctrl) {
            var t = ctrl.value;
            ctrl.value = t.toUpperCase();
        }
        function ToLower(ctrl) {
            var t = ctrl.value;
            ctrl.value = t.toLowerCase();
        }

        function CheckProfitCenter(pcenter) {

            var _pvalue = document.getElementById(pcenter);
            FF.WebERPClient.LocalWebServices.CommonSearchWebServive.CheckProfitCenter(_pvalue.value, onProfitCheckPass, onProfitCheckFail, pcenter);
        }

        function onProfitCheckPass(result, destCtrl) {
            var _pvalue = document.getElementById(destCtrl);

            if (result == null) {
                alert('Invalid profit center');

                _pvalue.value = '';
                return;
            }
            window.location = 'HpRevertRelease.aspx?pc=' + result.Mpc_cd;
        }
        function onProfitCheckFail(error, destCtrl) {

            alert('Invalid profit center');
            var _pvalue = document.getElementById(destCtrl);
            var _phide = document.getElementById('hdnProfitCenter');
            _pvalue.value = '';
            _phide.value = '';
        }


        function CheckChangeRadioControl() {

            var radPartRelease = document.getElementById('<%=radPartRelease.ClientID  %>');
            var radDiscount = document.getElementById('<%= radDiscount.ClientID %>');

            var txtRPartRelease = document.getElementById('<%=txtRPartRelease.ClientID %>');
            var lblReqReleaseAmount = document.getElementById('<%=lblReqReleaseAmount.ClientID %>');

            var txtRDiscount = document.getElementById('<%=txtRDiscount.ClientID %>');
            var lblReqDiscountAmount = document.getElementById('<%=lblReqDiscountAmount.ClientID %>');

            if (radDiscount.checked) {
                txtRDiscount.value = '';
                lblReqDiscountAmount.value = '';
                txtRPartRelease.value = '';
                lblReqReleaseAmount.value = '';
                txtRDiscount.focus();
            }

            if (radPartRelease.checked) {
                txtRDiscount.value = '';
                lblReqDiscountAmount.value = '';
                txtRPartRelease.value = '';
                lblReqReleaseAmount.value = '';
                txtRPartRelease.focus();
            }

        }



    </script>
    
<asp:UpdatePanel runat="server" ID="pnlUdtRevertRelease" >
<ContentTemplate>
    <div class="inv100break">
        <%--Button Area--%>
        <div  class="PanelHeader invheadersize">
            <asp:Button ID="btnSave" runat="server" Text="Process" CssClass="Button invbtn" 
                onclick="Process" />
            <asp:Button ID="btnClear" runat="server" Text="Clear"  CssClass="Button invbtn" />
        </div>
        <%--Top Criteriai--%>
        <div class="inv100pceltpd2">
            <div class="div1pcelt">
                &nbsp;</div>
            <div class="div5pcelt">
                Date
            </div>
            <div class="div10pcelt">
                <asp:TextBox ID="txtDate" runat="server" CssClass="TextBox" Width="70%" Enabled="false"></asp:TextBox>&nbsp;<asp:Image
                    ID="imgDate" runat="server" ImageUrl="~/Images/Calendar_scheduleHS.png" ImageAlign="Middle" /></div>
            <div class="div5pcelt">
                &nbsp;</div>
            <div class="div8pcelt">
                Profit Center
            </div>
            <div class="div10pcelt">
                <asp:TextBox ID="txtProfitCenter" runat="server" CssClass="TextBox" Width="70%"></asp:TextBox>
                &nbsp;<asp:ImageButton ID="ImgBtnProfitCenter" runat="server" ImageUrl="~/Images/icon_search.png"
                    ImageAlign="Middle" OnClick="ImgBtnPC_Click" /></div>
            <div class="div5pcelt">
                &nbsp;</div>
            <div class="div8pcelt">
                Account No</div>
            <div class="div15pcelt">
                <asp:TextBox ID="txtAccountNo" runat="server" CssClass="TextBox" Width="70%"></asp:TextBox>
                &nbsp;<asp:ImageButton ID="ImgBtnAccountNo" runat="server" ImageUrl="~/Images/icon_search.png"
                    ImageAlign="Middle" OnClick="btn_validateACC_Click" /></div>
            <div class="div3pcelt">
                &nbsp;</div>
            <div class="div10pcelt">
                <asp:Label ID="lblAccountNo" runat="server"></asp:Label>
            </div>
        </div>
        <div class="inv100pceltpd2">
            <div class="hprvtrls1">
                <%--Request Detail--%>
                <div class="inv100pceltpd2">
                    <div class="PanelHeader hprvtrls2" >
                        Request Detail
                    </div>
                    <div class="invunkwn2">
                        <div class="div1pcelt">
                            &nbsp;</div>
                        <div class="div20pcelt">
                            Request No</div>
                        <div class="div20pcelt">
                            <asp:DropDownList ID="ddlRequestNo" runat="server" CssClass="ComboBox" Width="90%">
                            </asp:DropDownList>
                        </div>
                        <div class="div40pcelt">
                            <asp:CheckBox runat="server" ID="chkApproved" Text="Approved Request" AutoPostBack="true"
                                OnCheckedChanged="chkApproved_CheckedChanged" />
                        </div>
                        <div class="div18pcelt">
                            <asp:Button ID="btnRequest" runat="server" Text="Request" CssClass="TextBox" OnClick="btnSendEcdReq_Click" />
                        </div>
                    </div>
                    <div class="invunkwn2">
                        <div class="div1pcelt">
                            &nbsp;</div>
                        <div class="div30pcelt">
                            <asp:RadioButton runat="server" Text="Patial Release" ID="radPartRelease" GroupName="Reqests" onchange="CheckChangeRadioControl()" />
                        </div>
                        <div class="div30pcelt">
                            <asp:TextBox runat="server" ID="txtRPartRelease" CssClass="TextBox" Width="85%"></asp:TextBox>%
                        </div>
                        <div class="div8pcelt"> &nbsp; => </div>
                        <div class="hprvtrls3">
                            &nbsp; <asp:Label runat="server" ID="lblReqReleaseAmount"></asp:Label>
                        </div>
                    </div>
                    <div class="hprvtrls4">
                        <div class="div1pcelt">
                            &nbsp;</div>
                        <div class="div30pcelt">
                            <asp:RadioButton runat="server" Text="Discount" ID="radDiscount" GroupName="Reqests" onchange="CheckChangeRadioControl()" />
                        </div>
                        <div class="div30pcelt">
                            <asp:TextBox runat="server" ID="txtRDiscount" CssClass="TextBox" Width="85%"></asp:TextBox>%
                        </div>
                        <div class="div8pcelt">&nbsp; => </div>
                        <div class="hprvtrls3">
                            &nbsp;  <asp:Label runat="server" ID="lblReqDiscountAmount"></asp:Label>
                        </div>
                    </div>
                </div>
                <%--Account Item Detail--%>
                <div class="inv100pceltpd2">
                    <div class="PanelHeader hprvtrls2" >
                        Trade Detail
                    </div>
                    <div class="inv100pceltpd2">
                        <asp:Panel ID="pnlTrade" runat="server" ScrollBars="Auto" Height="121px">
                            <asp:GridView runat="server" ID="gvATradeItem" AutoGenerateColumns="False" CssClass="GridView"
                                RowStyle-Wrap="false" CellPadding="3" ForeColor="#333333" GridLines="Both" DataKeyNames="Sad_itm_cd,Sad_qty,Sad_unit_rt,Sad_itm_line,Mi_act,sad_inv_no,Sad_itm_stus"
                                ShowHeaderWhenEmpty="true" OnRowDataBound="AccountItem_OnRowBind">
                                <EmptyDataTemplate>
                                    <div class="invunkwn53">
                                        No data found
                                    </div>
                                </EmptyDataTemplate>
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
                                <Columns>
                                    <asp:BoundField DataField='Sad_itm_cd' HeaderText='Item' HeaderStyle-Width="100px"
                                        HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                                    <asp:BoundField DataField='Mi_longdesc' HeaderText='Description' HeaderStyle-Width="250px"
                                        HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                                    <asp:BoundField DataField='Mi_model' HeaderText='Model' HeaderStyle-Width="100px"
                                        HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                                    <asp:BoundField DataField='Sad_qty' HeaderText='Qty' HeaderStyle-Width="70px" HeaderStyle-HorizontalAlign="Right"
                                        ItemStyle-HorizontalAlign="Right" />
                                    <asp:BoundField DataField='Sad_unit_rt' HeaderText='U. Price' HeaderStyle-Width="150px"
                                        HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right" />
                                    <asp:TemplateField Visible="false">
                                        <ItemTemplate>
                                            <asp:HiddenField runat="server" ID="hdnlineNo" Value='<%# DataBinder.Eval(Container.DataItem, "Sad_itm_line") %>' />
                                            <asp:HiddenField runat="server" ID="hdnIsForwardSale" Value='<%# DataBinder.Eval(Container.DataItem, "Mi_act") %>' />
                                            <asp:HiddenField runat="server" ID="hdnInvoiceNo" Value='<%# DataBinder.Eval(Container.DataItem, "sad_inv_no") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </asp:Panel>
                    </div>
                </div>
                <%--Revert Item Detail--%>
                <div class="inv100pceltpd2">
                    <div class="PanelHeader hprvtrls2" >
                        Reverted Item Detail
                    </div>
                    <div class="inv100pceltpd2">
                        <asp:Panel ID="pnlRItem" runat="server" ScrollBars="Auto" Height="111px">
                            <asp:GridView runat="server" ID="gvRevetedItem" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True"
                                CellPadding="3" ForeColor="#333333" CssClass="GridView" OnRowDataBound="gvRevertedItem_OnBind"
                                GridLines="Both" OnSelectedIndexChanged="GridViewDo_itm_SelectedIndexChanged">
                                <EmptyDataTemplate>
                                    <div class="invunkwn53">
                                        No data found
                                    </div>
                                </EmptyDataTemplate>
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
                                <Columns>
                                    <asp:BoundField DataField='tus_ser_line' HeaderText='#' HeaderStyle-Width="10px"
                                        HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                                    <asp:BoundField DataField='tus_itm_cd' HeaderText='Item' HeaderStyle-Width="100px"
                                        HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                                    <asp:BoundField DataField='tus_itm_stus' HeaderText='Status' HeaderStyle-Width="70px"
                                        HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                                    <asp:BoundField DataField='tus_qty' HeaderText='Qty' HeaderStyle-Width="50px" HeaderStyle-HorizontalAlign="Left"
                                        ItemStyle-HorizontalAlign="Left" />
                                    <asp:BoundField DataField='tus_serial_id' HeaderText='Aval. Qty' HeaderStyle-Width="50px"
                                        HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                                    <asp:BoundField DataField='tus_doc_no' HeaderText='Base Doc' HeaderStyle-Width="100px"
                                        HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                                </Columns>
                            </asp:GridView>
                        </asp:Panel>
                    </div>
                </div>
                <%--Revert Serial Detail--%>
                <div class="inv100pceltpd2">
                    <div class="PanelHeader hprvtrls2" >
                        Reverted Serial Detail
                    </div>
                    <div class="inv100pceltpd2">
                        <asp:Panel ID="pnlRSerial" runat="server" ScrollBars="Auto" Height="121px">
                            <asp:GridView runat="server" ID="gvRevertedSerial" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True"
                                CellPadding="3" ForeColor="#333333" CssClass="GridView" GridLines="Both" OnRowDataBound="gvRevertedSerial_OnBind"
                                OnRowDeleting="SelectedItem_OnDelete" DataKeyNames="tus_itm_cd,tus_ser_id">
                                <EmptyDataTemplate>
                                    <div class="invunkwn53">
                                        No data found
                                    </div>
                                </EmptyDataTemplate>
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
                                <Columns>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:ImageButton runat="server" ID="imgBtnRDelSerial" ImageAlign="Middle" ImageUrl="~/Images/Delete.png"
                                                Width="10px" Height="10px" CommandName="Delete" />
                                            <asp:HiddenField ID="hdnIsPicked" runat="server" Value='<%# DataBinder.Eval(Container.DataItem, "Tus_new_remarks") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField='tus_itm_cd' HeaderText='Item' HeaderStyle-Width="100px"
                                        HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                                    <asp:BoundField DataField='tus_itm_stus' HeaderText='Status' HeaderStyle-Width="100px"
                                        HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                                    <asp:BoundField DataField='tus_qty' HeaderText='Qty' HeaderStyle-Width="100px" HeaderStyle-HorizontalAlign="Left"
                                        ItemStyle-HorizontalAlign="Left" />
                                    <asp:BoundField DataField='tus_ser_1' HeaderText='Serial 1' HeaderStyle-Width="100px"
                                        HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                                    <asp:BoundField DataField='tus_warr_no' HeaderText='Warranty' HeaderStyle-Width="100px"
                                        HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                                    <asp:BoundField DataField='tus_ser_id' HeaderText='Id' HeaderStyle-Width="100px"
                                        HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                                </Columns>
                            </asp:GridView>
                        </asp:Panel>
                    </div>
                </div>
            </div>
            <div class="hprvtrls5">
                &nbsp;</div>
            <div class="hprvtrls6">
                <%--Account Summary--%>
                <div class="hprvtrls7">
                    <uc1:uc_HpAccountSummary ID="uc_HpAccountSummary1" runat="server" />
                </div>
                <div class="hprvtrls8">
                    &nbsp;</div>
                <%--Balance Summary--%>
                <div class="hprvtrls9">
                    <div class="PanelHeader hprvtrls2" >
                        Release Summary
                    </div>
                    <div class="hprvtrls10">
                        <div class="div1pcelt">
                            &nbsp;</div>
                        <div class="hprvtrls11">
                            Acc. Balance</div>
                        <div class="hprvtrls12">
                            <asp:Label ID="lblSumAccBalance" runat="server" Text="0.00"></asp:Label></div>
                    </div>
                    <div class="hprvtrls4">
                        <div class="div3pcelt">
                            &nbsp;</div>
                        <div class="div43pcelt">
                            Release Amt</div>
                        <div class="hprvtrls13">
                            <asp:Label ID="lblSumReleaseAmt" runat="server" Text="0.00"></asp:Label>
                        </div>
                    </div>
                    <div class="hprvtrls4">
                        <div class="div3pcelt">
                            &nbsp;</div>
                        <div class="div43pcelt">
                            Dicount Amt</div>
                        <div class="hprvtrls13">
                            <asp:Label ID="lblSumDiscountAmt" runat="server" Text="0.00"></asp:Label>
                        </div>
                    </div>
                    <div class="hprvtrls10">
                        <div class="div1pcelt">
                            &nbsp;</div>
                        <div class="hprvtrls14">
                            Receivable</div>
                        <div class="hprvtrls15">
                            <asp:Label ID="lblTotalReceivable" runat="server" Text="0.00"></asp:Label></div>
                    </div>
                    <div class="hprvtrls4">
                        <div class="div3pcelt">
                            &nbsp;</div>
                        <div class="div46pcelt">
                            To be Receipt</div>
                        <div class="hprvtrls16">
                            <asp:Label ID="lblSumReceipt" runat="server" Text="0.00"></asp:Label>
                        </div>
                    </div>
                    <div class="hprvtrls4">
                        <div class="div3pcelt">
                            &nbsp;</div>
                        <div class="div43pcelt">
                            To be Pay</div>
                        <div class="hprvtrls13">
                            <asp:Label ID="lblSumPay" runat="server" Text="0.00"></asp:Label>
                        </div>
                    </div>
                </div>
                <%--Receipt Collection--%>
                <div class="inv100pceltpd2">
                    <div class="PanelHeader hprvtrls2" >
                        Issue Receipts
                    </div>
                    <div class="invunkwn6">
                        <div class="hprvtrls17">
                            Seq</div>
                        <div class="hprvtrls18">
                            <%-- VAT Amt--%>
                            Prefix</div>
                        <div class="hprvtrls19">
                            <%--Amt--%>
                            Receipt No
                        </div>
                        <div class="hprvtrls20">
                            <%--Book--%>
                            Amount</div>
                        <div class="hprvtrls21">
                        </div>
                    </div>
                    <div class="hprvtrls22">
                        <div class="hprvtrls23">
                            Seq</div>
                        <div class="hprvtrls24">
                            <asp:DropDownList ID="ddlPrefix" runat="server" CssClass="ComboBox" Width="95%">
                            </asp:DropDownList>
                        </div>
                        <div class="hprvtrls25">
                            <asp:TextBox runat="server" ID="txtReceiptNo" CssClass="TextBox" class="invtxtalnrt"
                                Width="95.4%"></asp:TextBox>
                            &nbsp;</div>
                        <div class="hprvtrls26">
                            <asp:ImageButton ID="ImageButton3" runat="server" ImageUrl="~/Images/icon_search.png" />
                        </div>
                        <div class="hprvtrls27">
                            <asp:TextBox runat="server" ID="txtReciptAmount" CssClass="TextBox" class="invtxtalnrt"
                                Width="97%"></asp:TextBox></div>
                        <div class="invunkwn17">
                            <asp:ImageButton ID="ImgBtnAddReceipt" runat="server" ImageUrl="~/Images/Add-16x16x16.ICO"
                                OnClick="ImgBtnAddReceipt_Click" Width="16px" />
                        </div>
                        <div class="hprvtrls22">
                            <asp:Panel ID="Panel_ReceiptDet" runat="server" Height="69px" ScrollBars="Auto">
                                <asp:GridView ID="gvReceipts" runat="server" AutoGenerateColumns="False" CellPadding="1"
                                    CssClass="GridView" DataKeyNames="Sar_manual_ref_no,Sar_prefix" ForeColor="#333333"
                                    GridLines="Both" OnRowDeleting="gvReceipts_RowDeleting" ShowHeader="False" ShowHeaderWhenEmpty="true"
                                    OnRowDataBound="gvReceipts_OnRowDataBind">
                                    <AlternatingRowStyle BackColor="White" />
                                    <EmptyDataTemplate>
                                        <div class="hprvtrls28">
                                            No data found
                                        </div>
                                    </EmptyDataTemplate>
                                    <Columns>
                                        <asp:BoundField DataField="Sar_anal_7" HeaderText="Seq" ItemStyle-Width="25px" HeaderStyle-Width="25px" />
                                        <asp:BoundField DataField="Sar_prefix" HeaderText="Prefix" ItemStyle-Width="107px"
                                            HeaderStyle-Width="107px" />
                                        <asp:BoundField DataField="Sar_manual_ref_no" HeaderText="Receipt No" ItemStyle-Width="162px"
                                            HeaderStyle-Width="162px" />
                                        <asp:BoundField DataField="Sar_tot_settle_amt" HeaderText="Amount" ItemStyle-HorizontalAlign="Right"
                                            ItemStyle-Width="134px" HeaderStyle-Width="134px" />
                                        <asp:TemplateField ItemStyle-Width="80px" HeaderStyle-Width="80px" ItemStyle-HorizontalAlign="Right">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="ImgBtnDelRecipt" runat="server" CommandName="Delete" ImageUrl="~/Images/Delete.png"
                                                    Width="16px" />
                                            </ItemTemplate>
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
                </div>
                <%--Payment--%>
                <div class="inv100pceltpd2">
                    <div class="PanelHeader hprvtrls2" >
                        Payment
                        <div class="div80pcert">
                            <div class="div1pcelt">
                                &nbsp;</div>
                            <div class="hprvtrls29">
                                Paid Amount
                            </div>
                            <div class="hprvtrls30">
                                <asp:Label runat="server" ID="lblPayPaid" Text="0.00"></asp:Label>
                            </div>
                            <div class="div18pcelt">
                                &nbsp;
                            </div>
                            <div class="hprvtrls29">
                                Balance Amount</div>
                            <div class="hprvtrls30">
                                <asp:Label runat="server" ID="lblPayBalance" Text="0.00"></asp:Label>
                            </div>
                        </div>
                    </div>
                    <div class="hprvtrls31">
                        <div class="div1pcelt">
                            &nbsp;</div>
                        <div class="div13pcelt">
                            Pay Mode
                        </div>
                        <div class="div35pcelt">
                            <asp:DropDownList ID="ddlPayMode" Width="60%" runat="server" CssClass="ComboBox"
                                OnSelectedIndexChanged="PaymentType_LostFocus" AutoPostBack="true">
                            </asp:DropDownList>
                        </div>
                        <div class="div1pcelt">
                            &nbsp;</div>
                        <div class="invunkwn20">
                            Amount
                        </div>
                        <div class="div29pcelt">
                            <asp:TextBox ID="txtPayAmount" runat="server" CssClass="TextBoxUpper" Width="50%"></asp:TextBox>
                            &nbsp;
                            <asp:Button ID="btnPayAdd" runat="server" CssClass="Button" Text="Add" OnClick="AddPayment" />
                        </div>
                    </div>
                    <div class="hprvtrls4">
                        <div class="div1pcelt">
                            &nbsp;</div>
                        <div class="div13pcelt">
                            Remarks
                        </div>
                        <div class="invunkwn43">
                            <asp:TextBox ID="txtPayRemarks" runat="server" CssClass="TextBox" Width="100%" TextMode="MultiLine"
                                Rows="2"></asp:TextBox></div>
                    </div>
                    <div class="invunkwn41">
                        <div class="invunkwn42">
                            <%--Credit/Cheque/Bank Slip payment--%>
                            <div class="invunkwn44" runat="server" id="divCredit"
                                visible="false">
                                <div class="invunkwn5">
                                    <div class="div1pcelt">
                                        &nbsp;</div>
                                    <div class="div27pcelt">
                                        Card No</div>
                                    <div class="hprvtrls32">
                                        <asp:TextBox runat="server" ID="txtPayCrCardNo" CssClass="TextBox" Width="100%" MaxLength="15"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="invunkwn5">
                                    <div class="div1pcelt">
                                        &nbsp;</div>
                                    <div class="div27pcelt">
                                        Bank
                                    </div>
                                    <div class="hprvtrls33">
                                        <asp:TextBox runat="server" ID="txtPayCrBank" CssClass="TextBox" Width="60%"></asp:TextBox><asp:ImageButton
                                            ID="ImageButton1" runat="server" ImageUrl="~/Images/icon_search.png" ImageAlign="Middle"
                                            OnClick="ImgBankSearch_Click" />
                                    </div>
                                </div>
                                <div class="invunkwn5">
                                    <div class="div1pcelt">
                                        &nbsp;</div>
                                    <div class="div27pcelt">
                                        Branch
                                    </div>
                                    <div class="hprvtrls34">
                                        <asp:TextBox runat="server" ID="txtPayCrBranch" CssClass="TextBox" Width="100%" MaxLength="15"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="invunkwn5">
                                    <div class="div1pcelt">
                                        &nbsp;</div>
                                    <div class="div27pcelt">
                                        Card Type
                                    </div>
                                    <div class="hprvtrls35">
                                        <asp:DropDownList runat="server" ID="txtPayCrCardType" CssClass="ComboBox" Width="90%">
                                            <asp:ListItem Text="" Selected="True"></asp:ListItem>
                                            <asp:ListItem Text="HSBC"></asp:ListItem>
                                            <asp:ListItem Text="AMEX"></asp:ListItem>
                                            <asp:ListItem Text="VISA"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="invunkwn5">
                                    <div class="div1pcelt">
                                        &nbsp;</div>
                                    <div class="div27pcelt">
                                        Expiry Date
                                    </div>
                                    <div class="invunkwn51">
                                        <asp:TextBox runat="server" ID="txtPayCrExpiryDate" Enabled="false" CssClass="TextBox"
                                            Width="70%"></asp:TextBox>
                                        <asp:Image ID="btnImgCrExpiryDate" runat="server" ImageUrl="~/Images/Calendar_scheduleHS.png"
                                            ImageAlign="Middle" />
                                    </div>
                                </div>
                                <div class="invunkwn5">
                                    <div class="div1pcelt">
                                        &nbsp;</div>
                                    <div class="div27pcelt">
                                        Promotion
                                    </div>
                                    <div class="hprvtrls36">
                                        <asp:CheckBox runat="server" ID="chkPayCrPromotion" onclick="PromotionPeriod()" />
                                        &nbsp;&nbsp;&nbsp; Period &nbsp;
                                        <asp:TextBox runat="server" ID="txtPayCrPeriod" CssClass="TextBoxNumeric" Width="20%"
                                            MaxLength="2"></asp:TextBox>
                                        months
                                    </div>
                                </div>
                            </div>
                            <%--Advance receipt/Credit Note payment/Loyalty/Gift vouchas--%>
                            <div class="invunkwn5" runat="server" id="divAdvReceipt" visible="false">
                                <div class="invunkwn5">
                                    <div class="div1pcelt">
                                        &nbsp;</div>
                                    <div class="div27pcelt">
                                        Referance</div>
                                    <div class="hprvtrls37">
                                        <asp:TextBox runat="server" ID="txtPayAdvReceiptNo" CssClass="TextBox" Width="80%"></asp:TextBox><asp:ImageButton
                                            ID="ImageButton2" runat="server" ImageUrl="~/Images/icon_search.png" ImageAlign="Middle"
                                            OnClick="ImgBankSearch_Click" />
                                    </div>
                                </div>
                                <div class="invunkwn5">
                                    <div class="div1pcelt">
                                        &nbsp;</div>
                                    <div class="div27pcelt">
                                        Ref. Amount</div>
                                    <div class="invunkwn48">
                                        <asp:TextBox runat="server" ID="txtPayAdvRefAmount" CssClass="TextBox" Width="80%"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="div1pcelt">
                        &nbsp;
                    </div>
                    <div class="div49pcelt">
                        <div class="invunkwn5">
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
                                        <div class="invunkwn53">
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
                    </div>
                </div>
            </div>
        </div>
    </div>
    <%--Modal pop-ups --%>
    <div>
        <%--Modal pop-up for multiple accounts --%>
        <asp:ModalPopupExtender ID="ModalPopupAccItem" runat="server" CancelControlID="btnPopupCancel"
            ClientIDMode="Static" PopupControlID="Panel_popUp" TargetControlID="btnHidden_popup"
            BackgroundCssClass="modalBackground" PopupDragHandleControlID="divpopHeader">
        </asp:ModalPopupExtender>
        <div class="invunkwn5">
            <asp:Panel ID="Panel_popUp" runat="server" Width="500px">
                <%-- PopUp Handler for drag and control --%>
                <div class="popUpHeader" id="divpopHeader" runat="server">
                    <div class="div80pcelt" runat="server" id="divPopCaption">
                        Select Account
                    </div>
                    <div class="invunkwn57">
                        <asp:ImageButton ID="btnPopupCancel" runat="server" ImageUrl="~/Images/0032-01-16x16x16.ICO" />&nbsp;</div>
                </div>
                <asp:Panel ID="PanelPopup_grv" runat="server" ScrollBars="Both">
                    <asp:GridView ID="grvMpdalPopUp" runat="server" AutoGenerateColumns="False" DataKeyNames="HPA_ACC_NO,HPA_ACC_CRE_DT"
                        OnSelectedIndexChanged="grvMpdalPopUp_SelectedIndexChanged">
                        <Columns>
                            <asp:CommandField SelectText="select" ShowSelectButton="True" />
                            <asp:BoundField DataField="HPA_ACC_NO" HeaderText="Account No." />
                            <asp:BoundField DataField="HPA_ACC_CRE_DT" HeaderText="Created Date" />
                        </Columns>
                    </asp:GridView>
                </asp:Panel>
            </asp:Panel>
        </div>
        <%--Modal pop-up for pick serials --%>
        <asp:ModalPopupExtender ID="ModalPopupScanItem" runat="server" CancelControlID="btnimgCancel"
            PopupControlID="PanelItemPopUp" TargetControlID="btnHidden_popupScan" ClientIDMode="Static">
        </asp:ModalPopupExtender>
        <div class="invunkwn5">
            <asp:Panel ID="PanelItemPopUp" runat="server" CssClass="hprvtrlmod1">
                <div class="hprvtrls38">
                    <div class="hprvtrls39">
                    </div>
                    <div id="divPopupImg" runat="server" visible="false" class="hprvtrls40">
                        <asp:Image ID="Image4" runat="server" ImageUrl="~/Images/warning.gif" Width="15px"
                            Height="15px" />
                    </div>
                    <div class="hprvtrls41">
                        <asp:Label ID="lblpopupMsg" runat="server" Width="100%" ForeColor="Red" />
                    </div>
                    <div class="hprvtrls42">
                        <asp:ImageButton ID="btnimgAdd" runat="server" ImageUrl="~/Images/approve_img.png"
                            ImageAlign="Middle" OnClick="btnPopupOk_Click" Visible="true" Width="20px" Height="20px" />
                        &nbsp;
                        <asp:ImageButton ID="btnimgCancel" runat="server" ImageUrl="~/Images/error_icon.png"
                            ImageAlign="Middle" OnClick="btnPopupCancel_Click" Visible="true" Width="22px"
                            Height="22px" />
                        &nbsp;
                    </div>
                </div>
                <div class="invtxtalnrt">
                    <asp:HiddenField ID="hdnInvoiceNo" runat="server" />
                    <asp:HiddenField ID="hdnInvoiceLineNo" runat="server" />
                    <asp:Label ID="lblPopupAmt" runat="server" class="invtxtalnrt"></asp:Label>&nbsp;
                </div>
                <div class="hprvtrls43">
                    Item Code:
                    <asp:Label ID="lblPopupItemCode" runat="server" Font-Bold="True"></asp:Label>
                    <asp:Label ID="lblPopupBinCode" runat="server" Font-Bold="True"></asp:Label>
                </div>
                <div class="hprvtrls44">
                    <div id="divSerialSelect" runat="server" class="hprvtrls44">
                        <div class="hprvtrls45">
                        </div>
                        <div class="invunkwn20">
                            Search by :
                        </div>
                        <div class="div14pcelt">
                            <asp:DropDownList ID="ddlPopupSerial" runat="server" Width="85%" CssClass="ComboBox">
                                <asp:ListItem>Serial 1</asp:ListItem>
                                <asp:ListItem>Serial 2</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <div class="invunkwn20">
                            <asp:TextBox ID="txtPopupSearchSer" runat="server" CssClass="TextBox" Width="100%"></asp:TextBox>
                        </div>
                        <div class="div11pcelt">
                            &nbsp;
                            <asp:Button ID="btnPopupSarch" runat="server" CssClass="Button" OnClick="btnPopupSarch_Click"
                                Text="Search" />
                        </div>
                    </div>
                    <div id="divQtySelect" runat="server" visible="false" class="hprvtrls46">
                        <div class="div3pcelt">
                        </div>
                        <div class="hprvtrls47">
                            <asp:Label ID="lblPopQty" runat="server" Text="Qty:" Visible="False"></asp:Label>
                        </div>
                        <div class="hprvtrls48">
                            <asp:TextBox ID="txtPopupQty" runat="server" CssClass="TextBox" Visible="False" Width="100%"
                                ClientIDMode="Static"></asp:TextBox>
                        </div>
                        <div class="hprvtrls49">
                            &nbsp;
                            <asp:Button ID="btnPopupAutoSelect" runat="server" CssClass="Button" OnClick="btnPopupAutoSelect_Click"
                                OnClientClick="SelectAuto()" Text="Auto Select" visble="false" />
                        </div>
                    </div>
                    <div class="hprvtrls50">
                        <div class="div3pcelt">
                        </div>
                        <div class="hprvtrls47">
                            Reverted Qty :
                        </div>
                        <div class="hprvtrls47">
                            <asp:Label ID="lblRevertedQty" runat="server" CssClass="TextBox" Width="85%"></asp:Label>
                        </div>
                    </div>
                    <div class="hprvtrls50">
                        <div class="div3pcelt">
                        </div>
                        <div class="hprvtrls47">
                            Scan Qty :
                        </div>
                        <div class="hprvtrls47">
                            <asp:Label ID="lblScanQty" runat="server" CssClass="TextBox" Width="85%"></asp:Label>
                        </div>
                    </div>
                </div>
                <div class="hprvtrls51">
                    <asp:Panel ID="popupGridPanel" runat="server" Height="172px" ScrollBars="Auto" class="hprvtrls52">
                        <asp:GridView ID="GridPopup" runat="server" AutoGenerateColumns="False" CellPadding="4"
                            Height="45px" Width="95%" CssClass="GridView" ShowHeaderWhenEmpty="True" EmptyDataText="No data found">
                            <AlternatingRowStyle BackColor="White" />
                            <Columns>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        <asp:CheckBox ID="chkPopupSelectAll" runat="server" ClientIDMode="Static" onclick="SelectAll(this.id)" />
                                    </HeaderTemplate>
                                    <EditItemTemplate>
                                        <asp:CheckBox ID="CheckBox2" runat="server" />
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="checkPopup" runat="server" ClientIDMode="Static" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="Tus_ser_1" HeaderText="Serial No 1" />
                                <asp:BoundField DataField="Tus_ser_2" HeaderText="Serial No 2" />
                                <asp:BoundField DataField="Tus_itm_stus" HeaderText="Current Status" />
                                <asp:BoundField DataField="Tus_warr_no" HeaderText="Warrant #" />
                                <asp:BoundField DataField="Tus_bin" HeaderText="Bin Code" />
                                <asp:BoundField DataField="Tus_itm_desc" HeaderText="Description" />
                                <asp:BoundField DataField="Tus_ser_id" HeaderText="Serial ID" />
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
                <br />
                &nbsp;
            </asp:Panel>
        </div>
    </div>
    <%-- Control Area --%>
    <div style="display: none;">
        <asp:Button ID="btnBank" runat="server" OnClick="CheckBank" />
        <asp:Button ID="btnReceiptEnter" runat="server" Text="Button" OnClick="btnReceiptEnter_Click" />
        <asp:Button ID="btnHidden_popup" runat="server" Text="Button" />
        <asp:Button ID="btnHidden_popupScan" runat="server" Text="Button" />
        <asp:Button ID="btnRCalculate" runat="server" Text="Button" OnClick="CalculateRequest" />
        <asp:Button ID="btnAccount" runat="server" OnClick="btn_validateACC_Click" />
    </div>
</ContentTemplate>
</asp:UpdatePanel>
</asp:Content>
