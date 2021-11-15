<%@ Page Title="Cash Conversion/ Account Settlement" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AccountSettlement.aspx.cs" Inherits="FF.WebERPClient.HP_Module.AccountSettlement" %>
<%@ Register src="../UserControls/uc_HpAccountSummary.ascx" tagname="uc_HpAccountSummary" tagprefix="AC" %>
<%@ Register src="../UserControls/uc_HpAccountDetail.ascx" tagname="uc_HpAccountDetail" tagprefix="AD" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server"></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<script type="text/javascript"  >

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

    function PromotionPeriod() {
        if (document.getElementById('<%=chkPayCrPromotion.ClientID%>').checked)
            document.getElementById('<%=txtPayCrPeriod.ClientID%>').disabled = false;
        else
            document.getElementById('<%=txtPayCrPeriod.ClientID%>').disabled = true;
        document.getElementById('<%=txtPayCrPeriod.ClientID%>').value = '';
    }
</script>
<%--Main Page--%>

<div style=" float:left; width:100%; color:Black;" >
    <div style=" float:left; width:100%;" >
        <%--Button Area--%>
        <div style="height: 22px; text-align: right;" class="PanelHeader">
            <asp:Button ID="btnSave" runat="server" Text="Process" Height="85%" Width="70px"  CssClass="Button" OnClick="Process" />
            <asp:Button ID="btnClear" runat="server" Text="Clear" Height="85%" Width="70px" CssClass="Button" OnClick="Clear" />
            <asp:Button ID="btnClose" runat="server" Text="Close" Height="85%" Width="70px" CssClass="Button" OnClick="Close" />
        </div>
        <%--Top Criteria--%>
        <div style=" float:left; width:100%; padding-top:2px;" >
            <div style="float: left; width: 1%;">&nbsp;</div>
            <div style="float: left; width: 5%;">Date </div>
            <div style="float: left; width: 11%;"><asp:TextBox ID="txtDate" runat="server" CssClass="TextBox" Width="70%" Enabled="false"></asp:TextBox><asp:Image ID="imgDate" runat="server" ImageUrl="~/Images/Calendar_scheduleHS.png" Visible="false" ImageAlign="Middle" /></div>

            <div style=" float:left; width:1%;" >&nbsp;</div>
            <div style=" float:left; width:10%;" >Account No</div>
            <div style=" float:left; width:10%;"> <asp:TextBox ID="txtAccountNo" runat="server" CssClass="TextBox" Width="70%"></asp:TextBox>  &nbsp;<asp:ImageButton ID="ImgBtnAccountNo" runat="server"  ImageUrl="~/Images/icon_search.png" ImageAlign="Middle" OnClick="ImgAccountSearch_Click"/></div>
            <div style=" float:left; width:7%;" ><asp:Label ID="lblAccountNo" runat="server" Width="100%"  ></asp:Label> </div>
 
            <div style=" float:left; width:1%;" >&nbsp;</div>
            <div style=" float:left; width:7%;" >Request No</div>
            <div style=" float:left; width:10%;" ><asp:DropDownList ID="ddlRequestNo" runat="server" CssClass="ComboBox" Width="90%" ></asp:DropDownList> </div>
            <div style=" float:left; width:10%;" > <asp:CheckBox runat="server" ID="chkApproved" Text="Approved" AutoPostBack="true" OnCheckedChanged="chkApproved_CheckedChanged" /> </div>

            <div style=" float:left; width:10%;" >Service Charge</div>
            <div style=" float:left; width:10%;" ><asp:TextBox  runat ="server" ID="txtRServiceCharge" CssClass="TextBox" Width="80%" ></asp:TextBox> </div>
            <div style=" float:left; width:1%;" >&nbsp;</div>
            <div style=" float:left; width:6%;" ><asp:ImageButton runat ="server" ID="imgBtnRequest" ImageAlign="Middle" ImageUrl="~/Images/EditIcon.png" OnClick="btnSendEcdReq_Click" /> </div>
        </div>
    </div>

  <asp:UpdatePanel runat="server" ID="uppnlmain">
            <ContentTemplate>
    <div style=" float:left; width:40%; padding-top:3px; " >
        <div class="PanelHeader" > Account Detail and Summary</div>
        <div style=" float:left; width:100%; padding-top:3px;" >   <AC:uc_HpAccountSummary ID="uc_HpAccountSummary1" runat="server" /> </div>
        <div style=" float:left; width:100%;  padding-top:3px;" > <AD:uc_HpAccountDetail ID="uc_HpAccountDetail1" runat="server" /> </div>

        <%-- Product Detail --%>
        <div style=" float:left; width:100%; padding-top:8px;" >
            <div class="PanelHeader"> Product Detail</div>
            <asp:Panel runat="server" ID="pnlItemDetail"  ScrollBars="None"   Height="131px" >
                    <asp:GridView runat="server" ID="gvItemDetail"  AutoGenerateColumns="false" 
                    CssClass="GridView" CellPadding="3" ForeColor="#333333" GridLines="Both"  ShowHeaderWhenEmpty="true" DataKeyNames="sad_inv_no,sad_itm_line,Mi_act" OnRowDataBound="AccountItem_OnRowBind" >
                       <EmptyDataTemplate > <div style="width:100%; text-align:center;"> No data found </div> </EmptyDataTemplate>
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
                           <asp:BoundField DataField='Sad_itm_cd' HeaderText='Item' HeaderStyle-Width ="120px"  HeaderStyle-HorizontalAlign="Left" />
                           <asp:BoundField DataField='Mi_longdesc' HeaderText='Description'  HeaderStyle-Width ="220px" HeaderStyle-HorizontalAlign="Left" />
                           <asp:BoundField DataField='Sad_qty' HeaderText='Qty' HeaderStyle-Width ="70px" HeaderStyle-HorizontalAlign="Right" />
                           <asp:BoundField DataField='Sad_unit_rt' HeaderText='Unit Price'  HeaderStyle-Width ="150px" HeaderStyle-HorizontalAlign="Right" />
                           <asp:TemplateField Visible="false" > <ItemTemplate>  <asp:HiddenField runat="server" ID="hdnForwardSale"                                              Value='<%# DataBinder.Eval(Container.DataItem, "Mi_act") %>' />  </ItemTemplate>    </asp:TemplateField>
                       </Columns>
                    
                   </asp:GridView>
            </asp:Panel>
        </div> 

        <%-- Receipt Detail --%>
        <div style=" float:left; width:100%; padding-top:3px;" >
            <div class="PanelHeader"> Receipt Detail</div>
            <asp:Panel runat="server" ID="pnlReceiptDetail"  ScrollBars="Vertical"   Height="80px" >
                <asp:GridView runat="server" ID="gvPaidDetail"  AutoGenerateColumns="false" 
                    CssClass="GridView" CellPadding="3" ForeColor="#333333" GridLines="Both"  ShowHeaderWhenEmpty="true">
                        <EmptyDataTemplate > <div style="width:100%; text-align:center;"> No data found </div> </EmptyDataTemplate>
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
                            <%--<asp:BoundField DataField='sar_receipt_date' HeaderText='Date' ItemStyle-Width="75px" HeaderStyle-Width="75px" HeaderStyle-HorizontalAlign="Left" DataFormatString="{0:d}"   />--%>
                            <asp:BoundField DataField='sar_prefix' HeaderText='Prefix' ItemStyle-Width="60px" HeaderStyle-Width="60px" HeaderStyle-HorizontalAlign="Left"  />
                            <asp:BoundField DataField='sar_manual_ref_no' HeaderText='Receipt No'  ItemStyle-Width="100px" HeaderStyle-Width="100px" HeaderStyle-HorizontalAlign="Left"  />
                            <asp:BoundField DataField='sar_tot_settle_amt' HeaderText='Amount' ItemStyle-Width="85px"  HeaderStyle-Width="85px" HeaderStyle-HorizontalAlign="Right" DataFormatString="{0:#,##0.00;(#,##0.00);0}"  />
                        </Columns>
                </asp:GridView>
            </asp:Panel>
        </div>
    </div>

    <%--Seperator--%>
    <div style=" float:left; width:1%;" >&nbsp;</div>

    <div style=" float:left; width:24%; padding-top:3px; " >
        <%--Conversion Detail--%>
        <div style=" float:left; width:100%;" >
            <div class="PanelHeader"> Conversion Detail</div>

            <div style="float: left; width: 100%; padding-top:1px; padding-bottom:1px; background-color:#EFF3FB;">
                <div style="float: left; width: 1%;">&nbsp;</div>
                <div style="float: left; width: 55%;">Convertable Price Book</div><div style="float: left; width: 1%;">:</div>
                <div style="float: left; width: 40%; text-align:right; "><asp:Label ID="lblConvertablePriceBook" runat="server"  ></asp:Label></div>
            </div>

            <div style="float: left; width: 100%; padding-top:1px; padding-bottom:1px;">
                <div style="float: left; width: 1%;">&nbsp;</div>
                <div style="float: left; width: 55%;">Price Book</div><div style="float: left; width: 1%;">:</div>
                <div style="float: left; width: 40%; text-align:right;"><asp:Label ID="lblPBook" runat="server" ></asp:Label> </div>
            </div>

            <div style="float: left; width: 100%; padding-top:1px; padding-bottom:1px; background-color:#EFF3FB;">
                <div style="float: left; width: 1%;">&nbsp;</div>
                <div style="float: left; width: 55%;">Price Level</div><div style="float: left; width: 1%;">:</div>
                <div style="float: left; width: 40%;text-align:right;"><asp:Label ID="lblPLevel" runat="server" ></asp:Label></div>                
            </div>

            <div style="float: left; width: 100%;  padding-top:1px; padding-bottom:1px;">
                <div style="float: left; width: 1%;">&nbsp;</div>
                <div style="float: left; width: 55%;">Create Date</div><div style="float: left; width: 1%;">:</div>
                <div style="float: left; width: 40%;text-align:right;"><asp:Label runat="server" ID="lblCreateDate"   ></asp:Label> </div>
            </div>

            <div style="float: left; width: 100%;  padding-top:1px; padding-bottom:1px; background-color:#EFF3FB;">
                <div style="float: left; width: 1%;">&nbsp;</div>
                <div style="float: left; width: 55%;">Conversion Period</div><div style="float: left; width: 1%;">:</div>
                <div style="float: left; width: 40%; text-align:right;  "><asp:Label runat="server" ID="lblConversionPeriod"  ></asp:Label> </div>                
            </div>

            <div style="float: left; width: 100%;  padding-top:1px; padding-bottom:1px;">
                <div style="float: left; width: 1%;">&nbsp;</div>
                <div style="float: left; width: 55%;">Service Chargs</div><div style="float: left; width: 1%;">:</div>
                <div style="float: left; width: 40%; text-align:right;  "><asp:Label runat="server" ID="lblServiceCharge"   ></asp:Label> </div>
            </div>

             <div style="float: left; width: 100%;  padding-top:1px; padding-bottom:1px;background-color:#EFF3FB;">
                <div style="float: left; width: 1%;">&nbsp;</div>
                <div style="float: left; width: 55%;">Add. Service Charge</div><div style="float: left; width: 1%;">:</div>
                <div style="float: left; width: 40%; text-align:right; "><asp:Label runat="server" ID="lblAdditionalCharge"  ></asp:Label> </div>
            </div> 

        </div>
        
        <%-- Insurance Detail --%>
        <div style=" float:left; width:100%; padding-top:3px;" >
            <div class="PanelHeader"> Insurance Detail</div>
            <div style=" float:left; width:100%; background-color:#EFF3FB;">
                <div style=" float:left; width:1%;">&nbsp;</div>
                <div style=" float:left; width:55%;">Cash Memo</div><div style="float: left; width: 1%;">:</div>
                <div style=" float:left; width:40%;  text-align:right;"><asp:Label ID="lblInsCM" runat="server" ></asp:Label></div>
            </div>
            
            <div style=" float:left; width:100%;">
                <div style=" float:left; width:1%;">&nbsp;</div>
                <div style=" float:left; width:55%;">Amount</div><div style="float: left; width: 1%;">:</div>
                <div style=" float:left; width:40%; text-align:right;"><asp:Label ID="lblInsAmt" runat="server" ></asp:Label></div>
            </div>
       
            <div style=" float:left; width:100%;background-color:#EFF3FB;">
                <div style=" float:left; width:1%;">&nbsp;</div>
                <div style=" float:left; width:55%;">Comm. Rate</div><div style="float: left; width: 1%;">:</div>
                <div style=" float:left; width:40%; text-align:right;"><asp:Label ID="lblInsComRate" runat="server" ></asp:Label></div>
            </div>
            <div style=" float:left; width:100%;">
                <div style=" float:left; width:1%;">&nbsp;</div>
                <div style=" float:left; width:55%;">Comm. Amount</div><div style="float: left; width: 1%;">:</div>
                <div style=" float:left; width:40%; text-align:right;"><asp:Label ID="lblInsComAmt" runat="server" ></asp:Label></div>
            </div>
            <div style=" float:left; width:100%;background-color:#EFF3FB;">
                <div style=" float:left; width:1%;">&nbsp;</div>
                <div style=" float:left; width:55%;">Tax Rate</div><div style="float: left; width: 1%;">:</div>
                <div style=" float:left; width:40%; text-align:right;"><asp:Label ID="lblInsComTax" runat="server" ></asp:Label></div>
            </div>
            <div style=" float:left; width:100%;">
                <div style=" float:left; width:1%;">&nbsp;</div>
                <div style=" float:left; width:55%;">Tax Amount</div><div style="float: left; width: 1%;">:</div>
                <div style=" float:left; width:40%; text-align:right;"><asp:Label ID="lblInsComTaxAmt" runat="server" ></asp:Label></div>
            </div>

        </div>
    </div>

    <%--Seperator--%>
    <div style=" float:left; width:1%;" >&nbsp;</div>

    <div style=" float:left; width:34%; padding-top:3px;" >
        <div style=" float:left; width:100%;">
            <div class="PanelHeader"> Balance Sheet</div>

            <%--Receivable--%>
            <div style=" float:left; width:100%; text-decoration:underline; font-weight:bold; padding-left:1px;">Receivable</div>
            <div style=" float:left; width:100%;">
                <div style=" float:left; width:100%; padding-bottom:2px;">
                    <div style=" float:left; width:10%; ">&nbsp;</div>
                    <div style=" float:left; width:43%; ">Cash Price</div>
                    <div style=" float:left; width:30%; text-align:right; "><asp:Label ID="lblCashPrice" runat ="server" Text="0.00" ></asp:Label> </div>
                </div>

                <div style=" float:left; width:100%; padding-bottom:2px;">
                    <div style=" float:left; width:10%; ">&nbsp;</div>
                    <div style=" float:left; width:43%; ">Cash on service charge</div>
                    <div style=" float:left; width:30%; text-align:right; "><asp:Label ID="lblCashonService" runat ="server" Text="0.00" ></asp:Label> </div>
                </div>

                <div style=" float:left; width:100%; border-top-style:solid;border-top-width:1px; padding-top:3px;">
                    <div style=" float:left; width:1%; ">&nbsp;</div>
                    <div style=" float:left; width:40%;  font-weight:bold; ">Total Receivable</div>
                    <div style=" float:left; width:55%; text-align:right; font-weight:bold; "><asp:Label ID="lblTotalReceivable" runat ="server" Text="0.00" ></asp:Label></div>
                </div>
            </div>

            <%--Reversed--%>
            <div style=" float:left; width:100%; text-decoration:underline; font-weight:bold; padding-left:1px; padding-top:10px;">Reversed</div>
            <div style=" float:left; width:100%;">
                <div style=" float:left; width:100%; padding-bottom:2px;">
                    <div style=" float:left; width:10%; ">&nbsp;</div>
                    <div style=" float:left; width:43%; ">Receipt Total</div>
                    <div style=" float:left; width:30%; text-align:right; "><asp:Label ID="lblReceiptTotal" runat ="server" Text="0.00" ></asp:Label> </div>
                </div>

                <div style=" float:left; width:100%; padding-bottom:2px;">
                    <div style=" float:left; width:10%; ">&nbsp;</div>
                    <div style=" float:left; width:43%; ">Insurance</div>
                    <div style=" float:left; width:30%; text-align:right; "><asp:Label ID="lblInsurance" runat ="server" Text="0.00" ></asp:Label> </div>
                </div>

                <div style=" float:left; width:100%; padding-bottom:2px;">
                    <div style=" float:left; width:10%; ">&nbsp;</div>
                    <div style=" float:left; width:43%; ">Stamp Duty</div>
                    <div style=" float:left; width:30%; text-align:right; "><asp:Label ID="lblStampDuty" runat ="server" Text="0.00" ></asp:Label> </div>
                </div>

                <div style=" float:left; width:100%; padding-bottom:2px;">
                    <div style=" float:left; width:10%; ">&nbsp;</div>
                    <div style=" float:left; width:43%; ">Other Charges</div>
                    <div style=" float:left; width:30%; text-align:right; "><asp:Label ID="lblOtherCharges" runat ="server" Text="0.00" ></asp:Label> </div>
                </div>

                <div style=" float:left; width:100%; padding-bottom:2px;">
                    <div style=" float:left; width:10%; ">&nbsp;</div>
                    <div style=" float:left; width:43%; ">Adjustments</div>
                    <div style=" float:left; width:30%; text-align:right; "><asp:Label ID="lblAdjustment" runat ="server" Text="0.00" ></asp:Label> </div>
                </div>

                <div style=" float:left; width:100%; border-top-style:solid;border-top-width:1px; padding-top:3px;">
                    <div style=" float:left; width:1%; ">&nbsp;</div>
                    <div style=" float:left; width:40%;  font-weight:bold; ">Total Reversed</div>
                    <div style=" float:left; width:55%; text-align:right; font-weight:bold; "><asp:Label ID="lblTotalReversed" runat ="server" Text="0.00" ></asp:Label></div>
                </div>

                <div style=" float:left; width:100%; padding-top:3px; border-bottom-style:solid;border-bottom-width:1px; padding-bottom:3px;">
                    <div style=" float:left; width:1%; ">&nbsp;</div>
                    <div style=" float:left; width:40%;  font-weight:bold; ">Received Amount</div>
                    <div style=" float:left; width:55%; text-align:right; font-weight:bold; "><asp:Label ID="lblReceivedAmount" runat ="server" Text="0.00" ></asp:Label></div>
                </div>

                <div style=" float:left; width:100%; padding-top:3px;  padding-bottom:3px;">
                    <div style=" float:left; width:1%; ">&nbsp;</div>
                    <div style=" float:left; width:40%;  font-weight:bold; ">Balance to be pay</div>
                    <div style=" float:left; width:55%; text-align:right; font-weight:bold; "><asp:Label ID="lblBalancetoPay" runat ="server" Text="0.00" ></asp:Label></div>
                </div>
            </div>


        </div>
    </div>

    <div style=" float:left; width:60%; padding-top:3px;" >
        <div style=" float:left; width:2%; padding-top:3px;" >  &nbsp;  </div>
        <div style=" float:left; width:98%; padding-top:10px;  background-color :#FFFFFF ;height:272px;" >
             <div style=" float:left;width:100%;" >
    <div style="float: left; width: 100%; padding-bottom:2px;">
        <div style="float: left; width: 1%;">  &nbsp;</div>
        <div style="float: left; width: 13%;">  Pay Mode </div>
        <div style="float: left; width: 35%;"> <asp:DropDownList ID="ddlPayMode" Width="60%" runat="server"  CssClass="ComboBox"   OnSelectedIndexChanged="PaymentType_LostFocus" AutoPostBack="true"> </asp:DropDownList>  </div>
               
        <div style="float: left; width: 1%;">  &nbsp;</div>
        <div style="float: left; width: 15%;">  Amount </div>
        <div style="float: left; width: 29%;"> <asp:TextBox ID="txtPayAmount" runat="server" CssClass="TextBoxUpper" Width="50%" ></asp:TextBox> &nbsp; <asp:Button ID="btnPayAdd" runat="server" CssClass="Button" Text="Add" OnClick="AddPayment" />   </div>
     </div>
    <div style="float: left; width: 100%; padding-bottom:2px;">
        <div style="float: left; width: 1%;"> &nbsp;</div>
        <div style="float: left; width: 13%;">Remarks </div>
        <div style="float: left; width: 75%;">  <asp:TextBox ID="txtPayRemarks" runat="server" CssClass="TextBox" Width="100%" TextMode="MultiLine"   Rows="2"></asp:TextBox></div>
     </div>

    <div style="float: left; width: 50%; ">
        <div style="float: left; width: 100%; padding: 2px 2px 0px 0px;">
            <%--Credit/Cheque/Bank Slip payment--%>
            <div style="float: left; width: 100%; height: 100px;" runat="server" id="divCredit"
                    visible="false">
                    <div style="float: left; width: 100%;">
                        <div style="float: left; width: 1%;"> &nbsp;</div>
                        <div style="float: left; width: 27%;"> <asp:Label ID="lblPayCrCardNo" runat="server" Text="Card No"></asp:Label></div>
                        <div style="float: left; width: 72%; padding-bottom: 2px;"> <asp:TextBox runat="server" ID="txtPayCrCardNo" CssClass="TextBox" Width="100%" MaxLength="15"></asp:TextBox>  </div>
                    </div>

                    <div style="float: left; width: 100%;">
                        <div style="float: left; width: 1%;"> &nbsp;</div>
                        <div style="float: left; width: 27%;"> Bank </div>
                        <div style="float: left; width: 50%; padding-bottom: 2px;"> <asp:TextBox runat="server" ID="txtPayCrBank" CssClass="TextBox" Width="60%"></asp:TextBox><asp:ImageButton  ID="imgBtnBank" runat="server" ImageUrl="~/Images/icon_search.png" ImageAlign="Middle"  OnClick="ImgBankSearch_Click" />   </div>
                     </div>

                    <div style="float: left; width: 100%;">
                        <div style="float: left; width: 1%;"> &nbsp;</div>
                        <div style="float: left; width: 27%;"> Branch </div>
                        <div style="float: left; width: 65%; padding-bottom: 2px;"> <asp:TextBox runat="server" ID="txtPayCrBranch" CssClass="TextBox" Width="75%" MaxLength="15"></asp:TextBox> <asp:ImageButton ID="imgBtnBranch" runat="server" ImageUrl="~/Images/icon_search.png"
                                                    ImageAlign="Middle" OnClick="ImgBankBranchSearch_Click" /> </div>                        
                    </div>

                    <div style="float: left; width: 100%;">
                        <div style="float: left; width: 1%;"> &nbsp;</div>
                        <div style="float: left; width: 27%;"> Card Type </div>
                        <div style="float: left; width: 27%; padding-bottom: 2px;"> 
                        <asp:DropDownList runat="server" ID="txtPayCrCardType" CssClass="ComboBox" Width="90%"> 
                             <asp:ListItem Text="" Selected="True"></asp:ListItem>
                             <asp:ListItem Text="AMEX"></asp:ListItem>
                             <asp:ListItem Text="VISA"></asp:ListItem>
                             <asp:ListItem Text="MASTER"></asp:ListItem>
                             <asp:ListItem Text="DISCOVER"></asp:ListItem>
                             <asp:ListItem Text="2CO"></asp:ListItem>
                             <asp:ListItem Text="SAGE"></asp:ListItem>
                             <asp:ListItem Text="DELTA"></asp:ListItem>
                             <asp:ListItem Text="CIRRUS"></asp:ListItem>
                        </asp:DropDownList> </div>
                     </div>

                    <div style="float: left; width: 100%;">
                        <div style="float: left; width: 1%;"> &nbsp;</div>
                        <div style="float: left; width: 27%;"> Expiry Date </div>
                        <div style="float: left; width: 40%; padding-bottom: 2px;"> <asp:TextBox runat="server" ID="txtPayCrExpiryDate" Enabled="false" CssClass="TextBox"  Width="70%"></asp:TextBox> <asp:Image ID="btnImgCrExpiryDate" runat="server" ImageUrl="~/Images/Calendar_scheduleHS.png"  ImageAlign="Middle" /> </div>                        
                    </div>

                    <div style="float: left; width: 100%;">
                        <div style="float: left; width: 1%;"> &nbsp;</div>
                        <div style="float: left; width: 27%;"> Promotion </div>
                        <div style="float: left; width: 67%; padding-bottom: 2px;"> <asp:CheckBox runat="server" ID="chkPayCrPromotion" onclick="PromotionPeriod()" /> &nbsp;&nbsp;&nbsp; Period &nbsp; <asp:TextBox runat="server" ID="txtPayCrPeriod" CssClass="TextBoxNumeric" Width="20%" MaxLength="2"></asp:TextBox> months </div>
                    </div>
                </div>
            <%--Advance receipt/Credit Note payment/Loyalty/Gift vouchas--%>
            <div style="float: left; width: 100%;" runat="server" id="divAdvReceipt" visible="false">
                    <div style="float: left; width: 100%;">
                        <div style="float: left; width: 1%;"> &nbsp;</div>
                        <div style="float: left; width: 27%;">Referance</div>
                        <div style="float: left; width: 71%; padding-bottom: 2px;">  <asp:TextBox runat="server" ID="txtPayAdvReceiptNo" CssClass="TextBox" Width="80%"></asp:TextBox><asp:ImageButton  ID="ImageButton2" runat="server" ImageUrl="~/Images/icon_search.png" ImageAlign="Middle"  OnClick="ImgBankSearch_Click" /> </div>
                    </div>
                    <div style="float: left; width: 100%;">
                        <div style="float: left; width: 1%;">  &nbsp;</div>
                        <div style="float: left; width: 27%;">Ref. Amount</div>
                        <div style="float: left; width: 25%; padding-bottom: 2px;"> <asp:TextBox runat="server" ID="txtPayAdvRefAmount" CssClass="TextBox" Width="80%"></asp:TextBox>  </div>
                    </div>
                </div>
        </div>
    </div>

    <div style="float: left; width: 1%; ">&nbsp; </div>

     <div style="float:left; width: 49%; ">
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
                    <EmptyDataTemplate><div style="width: 100%; text-align: center;"> No data found </div> </EmptyDataTemplate>
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
                        <asp:BoundField DataField='sard_pay_tp' HeaderText='Payment Type' ItemStyle-Width="110px"   HeaderStyle-Width="110px" />
                        <asp:BoundField DataField='sard_ref_no' HeaderText='' Visible="false" />
                        <asp:BoundField DataField='sard_chq_bank_cd' HeaderText='Bank' ItemStyle-Width="90px" HeaderStyle-Width="90px" />
                        <asp:BoundField DataField='sard_chq_branch' HeaderText='Branch' ItemStyle-Width="90px" HeaderStyle-Width="90px" />
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
     <div style="float: left; width: 100%; height:85px;"> &nbsp;</div>
     <div style="float: left; width: 100%;">
            <div style="float: left; width: 1%;">&nbsp;</div>
            <div style="float: left; width: 20%; background-color: #1569C7; color: White; height: 15px;"> Paid Amount </div>
            <div style="float: left; width: 18%; text-align: right; border-color: Black; border-style: solid;  border-width: 1px;"> <asp:Label runat="server" ID="lblPayPaid" Text="0.00"></asp:Label> </div>
                
            <div style="float: left; width: 18%;"> &nbsp; </div>
            <div style="float: left; width: 20%; background-color: #1569C7; color: White; height: 15px;">Balance Amount</div>
            <div style="float: left; width: 18%; text-align: right; border-color: Black; border-style: solid; border-width: 1px;"> <asp:Label runat="server" ID="lblPayBalance" Text="0.00"></asp:Label> </div>
            <div style="float: left; width: 1%;">&nbsp;</div>
        </div>
</div>
        </div>
        <div style=" float:left; width:2%; padding-top:3px;" >  &nbsp;  </div>
    </div>
    </ContentTemplate>
</asp:UpdatePanel>

</div>

<div>
    <%--Modal pop-up --%>
    <asp:ModalPopupExtender ID="ModalPopupExtItem" runat="server" CancelControlID="btnPopupCancel" ClientIDMode="Static"  PopupControlID="Panel_popUp" TargetControlID="btnHidden_popup" BackgroundCssClass="modalBackground"    PopupDragHandleControlID="divpopHeader"> </asp:ModalPopupExtender>
    <div style="float: left; width: 100%;">
    <asp:Panel ID="Panel_popUp" runat="server" Width="500px" CssClass="ModalWindow" >
        <%-- PopUp Handler for drag and control --%>
        <div class="popUpHeader" id="divpopHeader" runat="server">
        <div style="float: left; width: 80%" runat="server" id="divPopCaption"> Select Account </div>
        <div style="float: left; width: 20%; text-align: right"> <asp:ImageButton ID="btnPopupCancel" runat="server" ImageUrl="~/Images/0032-01-16x16x16.ICO" />&nbsp;</div> 
        </div>
        <asp:Panel ID="PanelPopup_grv" runat="server"  >
            <asp:GridView ID="grvMpdalPopUp" runat="server" AutoGenerateColumns="False" 
            DataKeyNames="HPA_ACC_NO,HPA_ACC_CRE_DT" 
            onselectedindexchanged="grvMpdalPopUp_SelectedIndexChanged">
            <Columns>
                <asp:CommandField SelectText="select" ShowSelectButton="True" />
                <asp:BoundField DataField="HPA_ACC_NO" HeaderText="Account No." />
                <asp:BoundField DataField="HPA_ACC_CRE_DT" HeaderText="Created Date" DataFormatString="{0:d}" />
            </Columns>
            </asp:GridView>
        </asp:Panel>
    </asp:Panel>
    </div>

    <%-- Doc Date --%>
    <asp:CalendarExtender ID="CEDocDate" runat="server" TargetControlID="txtDate" PopupButtonID="imgDate"   PopupPosition="BottomLeft" EnabledOnClient="true" Animated="true" Format="dd/MM/yyyy">   </asp:CalendarExtender>
</div>

<%-- Control Area --%>
<div style="display: none;">
    <asp:Button ID="btnHidden_popup" runat="server" Text="Button" />
    <asp:Button ID="btnBank" runat="server" OnClick="CheckBank" />
    <asp:Button ID="btnAccount" runat="server" OnClick="btn_validateACC_Click" />
</div>

</asp:Content>


