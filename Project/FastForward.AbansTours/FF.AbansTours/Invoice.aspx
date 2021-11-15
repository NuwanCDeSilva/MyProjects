<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="Invoice.aspx.cs" Inherits="FF.AbansTours.Invoice" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script type="text/javascript">

        function calculate_discount(pvalue) {
            var DicPercentage = document.getElementById('txtDisPercentage').value;
            if (DicPercentage >= 100) {
                alert("please enter valid discount percentage");
            }
        }
    </script>
    <script type="text/javascript">
        function OnlyNumeric(evt) {
            var strValue = document.getElementById('<%=txtPax.ClientID %>').value;

            var chCode = evt.keyCode ? evt.keyCode : evt.charCode ? evt.charCode : evt.which;

            if (chCode >= 48 && chCode <= 57 ||
                 chCode == 46) {
                return true;
            }
            else
                alert("Enter numbers only");
            return false;
        }
    </script>
    <script type="text/javascript">

        function jsDecimals(e) {
            var evt = (e) ? e : window.event;
            var key = (evt.keyCode) ? evt.keyCode : evt.which;
            if (key != null) {
                key = parseInt(key, 10);
                if ((key < 48 || key > 57) && (key < 96 || key > 105)) {
                    if (!jsIsUserFriendlyChar(key, "Decimals")) {
                        return false;
                    }
                }
                else {
                    if (evt.shiftKey) {
                        return false;
                    }
                }
            }
            return true;
        };

        function jsIsUserFriendlyChar(val, step) {
            if (val == 8 || val == 9 || val == 13 || val == 45 || val == 46) {
                return true;
            }
            if ((val > 16 && val < 21) || (val > 34 && val < 41)) {
                return true;
            }
            if (step == "Decimals") {
                if (val == 190 || val == 110) {
                    return true;
                }
            }
            return false;
        };

    </script>
</asp:Content>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row rowmargin0 col-md-12" style="height: 22px; width: 100%; text-align: right; background-color: Silver;">
        <div class="col-md-7">
             <asp:UpdatePanel ID="upHiden" runat="server">
            <ContentTemplate>
                <asp:HiddenField ID="hdfChargeDesc" runat="server" />
            </ContentTemplate>
        </asp:UpdatePanel>
        </div>
        <div class="col-md-1">
            <asp:Label Text="Back Date" ID="lblbakcDate" Visible="false" runat="server" />
        </div>
        <asp:UpdatePanel ID="upHedaerbtns" runat="server">
            <ContentTemplate>
                <div class="col-md-1">
                    <asp:Button ID="btnCancel" Text="Cancel" runat="server" Width="80px" Visible="false" OnClick="btnCancel_Click" />
                    <asp:ConfirmButtonExtender ID="ConfirmButtonExtender1" runat="server" TargetControlID="btnCancel"
                        ConfirmText="Do you want to cancel?" ConfirmOnFormSubmit="false">
                    </asp:ConfirmButtonExtender>
                </div>
                <div class="col-md-1">
                    <asp:Button ID="btnCreate" Text="Save" runat="server" Width="80px" OnClick="btnCreate_Click" />
                    <asp:ConfirmButtonExtender ID="ConfirmButtonExtender4" runat="server" TargetControlID="btnCreate"
                        ConfirmText="Do you want to save?" ConfirmOnFormSubmit="false">
                    </asp:ConfirmButtonExtender>

                </div>
                <div class="col-md-1">
                    <asp:Button ID="btnClear" Text="Clear" runat="server" Width="80px" OnClick="btnClear_Click" />
                    <asp:ConfirmButtonExtender ID="ConfirmButtonExtender3" runat="server" TargetControlID="btnClear"
                        ConfirmText="Do you want to clear?" ConfirmOnFormSubmit="false">
                    </asp:ConfirmButtonExtender>
                </div>
                <div class="col-md-1">
                    <asp:Button ID="btnBack" Text="Back" runat="server" Width="80px" OnClick="btnBack_Click" />
                    <asp:ConfirmButtonExtender ID="ConfirmButtonExtender2" runat="server" TargetControlID="btnBack"
                        ConfirmText="Do you want go to previous page?" ConfirmOnFormSubmit="false">
                    </asp:ConfirmButtonExtender>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <div class="row rowmargin0 col-md-12">
        <div class="col-md-10">
        </div>
        <div class="col-md-1">
            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                <ContentTemplate>
                    <asp:Button ID="btnDiscount" Text="Discount" runat="server" Width="80px" OnClick="btnDiscount_Click" />
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
        <div class="col-md-1">
            <asp:Button ID="btnPrint" Text="Print" runat="server" Width="80px" OnClick="btnPrint_Click" />
        </div>
    </div>
    <div class="row rowmargin0 col-md-12">
        <div class="panel panel-default">
            <div class="panel-heading pannelheading">
                Invoice
            </div>
            <div class="panel-body">
                <asp:UpdatePanel ID="upInvoiceDetails" runat="server">
                    <ContentTemplate>
                        <div class="row rowmargin0 col-md-12">
                            <div class="col-md-6">
                                <div class="col-md-4 padding1">
                                    Enquiry ID
                                </div>
                                <div class="col-md-8 padding2">
                                    <asp:TextBox ID="txtEnquiryID" runat="server" AutoPostBack="true" Width="80%" OnTextChanged="txtEnquiryID_TextChanged"></asp:TextBox>
                                    <asp:ImageButton ID="btnsearchEnquiry" runat="server" ImageUrl="~/Images/icon_search.png"
                                        ImageAlign="Middle" OnClick="btnsearchEnquiry_Click" />
                                    <asp:Label Text="manReffNumber" ID="lblManrefNumber" Visible="false" runat="server" />
                                </div>
                            </div>
                            <div class="col-md-6">
                                <div class="col-md-4 padding1">
                                    Customer Code
                                </div>
                                <div class="col-md-8 padding2">
                                    <asp:TextBox ID="txtCusCode" runat="server" AutoPostBack="true" Width="80%" OnTextChanged="txtCusCode_TextChanged"></asp:TextBox>
                                    <asp:ImageButton ID="btnCustomer" runat="server" ImageUrl="~/Images/icon_search.png"
                                        ImageAlign="Middle" OnClick="btnCustomer_Click" />
                                </div>
                            </div>
                            <div class="col-md-6">
                                <div class="col-md-4 padding1">
                                    invoice Number
                                </div>
                                <div class="col-md-8 padding2">
                                    <asp:TextBox ID="txtReffNum" runat="server" AutoPostBack="true" Width="80%" OnTextChanged="txtReffNum_TextChanged"></asp:TextBox>
                                    <asp:ImageButton ID="btnSearchInvoice" runat="server" ImageUrl="~/Images/icon_search.png"
                                        ImageAlign="Middle" OnClick="btnSearchInvoice_Click" />
                                </div>
                            </div>
                            <div class="col-md-6">
                                <div class="col-md-4 padding1">
                                    Address
                                </div>
                                <div class="col-md-8 padding2">
                                    <asp:TextBox ID="txtaddress" BackColor="LINEN" runat="server" AutoPostBack="true" Width="80%" ReadOnly="True"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="row rowmargin0 col-md-12">
                            <div class="col-md-6">
                                <div class="col-md-4 padding1">
                                    Date
                                </div>
                                <div class="col-md-8 padding2">
                                    <asp:TextBox ID="txtdate" Enabled="false" CssClass="input-xlarge focused" runat="server"
                                        Width="80%" onkeypress="return RestrictSpace()"></asp:TextBox>
                                    <asp:CalendarExtender ID="txtDateExtender" runat="server" Enabled="True" Format="dd/MMM/yyyy"
                                        PopupButtonID="cal" TargetControlID="txtdate">
                                    </asp:CalendarExtender>
                                    <asp:Image ID="cal" Width="16" runat="server" ImageUrl="~/images/calendar.png" />
                                </div>
                            </div>
                            <div class="col-md-6">
                                <div class="col-md-4 padding1">
                                    Mobile
                                </div>
                                <div class="col-md-8 padding2">
                                    <asp:TextBox ID="txtMobile" runat="server" BackColor="LINEN" Width="80%" ReadOnly="True"></asp:TextBox><asp:ImageButton
                                        ID="btnSearchMobile" runat="server" ImageUrl="~/Images/icon_search.png" ImageAlign="Middle"
                                        Visible="false" />
                                </div>
                            </div>
                        </div>
                        <div class="row rowmargin0 col-md-12">
                            &nbsp;
                        </div>
                        <div class="row rowmargin0 col-md-12">
                            <div class="col-md-6" style="text-align: center">
                                <div class="col-md-2 padding1">
                                    &nbsp;
                                </div>
                                <div class="col-md-8 padding1" style="background-color: PALETURQUOISE">
                                    <div class="col-md-12 padding1" style="background-color: #32A9CB;">
                                        Tax Payable
                                        <asp:CheckBox ID="chkTaxPayable" Text="  " runat="server" />
                                    </div>
                                    <div class="col-md-12 padding1">
                                        SVAT Status :
                                        <asp:Label ID="lblSVATStatus" Text="" runat="server" ForeColor="Green" />
                                    </div>
                                    <div class="col-md-12 padding1">
                                        Exempt Status :
                                        <asp:Label ID="lblExemptStatus" Text="" runat="server" ForeColor="Green" />
                                    </div>
                                </div>
                                <div class="col-md-2 padding1">
                                    &nbsp;
                                </div>


                            </div>
                            <div class="col-md-6">
                                <div class="col-md-4 padding1">
                                    Invoice Type
                                </div>
                                <div class="col-md-8 padding2">
                                    <asp:DropDownList ID="ddlInvoiceType" runat="server" Width="100%" AutoPostBack="true"
                                        OnSelectedIndexChanged="ddlInvoiceType_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </div>

                                <div class="col-md-4 padding1">
                                    Executive
                                </div>
                                <div class="col-md-8 padding2">
                                    <asp:DropDownList ID="ddlExecutive" runat="server" Width="50%" AutoPostBack="true">
                                    </asp:DropDownList>
                                </div>

                                <div class="col-md-4 padding1">
                                    Price Book
                                </div>
                                <div class="col-md-3 padding2">
                                    <asp:DropDownList ID="cmbPriceBook" runat="server" Width="80%">
                                    </asp:DropDownList>
                                </div>
                                <div class="col-md-5 padding1">
                                    <div class="col-md-5 padding1">
                                        Price Level
                                    </div>
                                    <div class="col-md-7 padding2">
                                        <asp:DropDownList ID="cmbPriceLevel" runat="server" Width="80%">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row rowmargin0 col-md-12">
                            <div class="col-md-6">
                                <div class="col-md-4 padding1">
                                    Package Type

                                </div>
                                <div class="col-md-8 padding1">
                                    <asp:DropDownList ID="ddlPackageType" Width="80%" runat="server">
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-md-6">
                            </div>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
        <div class="panel panel-default">
            <div class="panel-heading ">
                Invoice Items
            </div>
            <div class="panel-body1">
                <asp:UpdatePanel ID="upEntryLine" runat="server">
                    <ContentTemplate>
                        <div class="row rowmargin0 col-md-12">
                            <div class="col-md-1 padding0">
                                Charge Code
                                <br />
                                <asp:DropDownList ID="ddlCostType" runat="server" Width="90%" AutoPostBack="true">
                                </asp:DropDownList>
                            </div>
                            <div class="col-md-2 padding0 EntyCell">
                                Sub Type
                                <br />
                                <asp:TextBox ID="txtChargeCode" runat="server" AutoPostBack="true" Width="70%" OnTextChanged="txtChargeCode_TextChanged"></asp:TextBox>
                                <asp:ImageButton ID="btnChargeType" runat="server" ImageUrl="~/Images/icon_search.png"
                                    ImageAlign="Middle" OnClick="btnChargeType_Click" />
                            </div>
                            <div class="col-md-1 padding0">
                                Currency
                                <br />
                                <asp:DropDownList ID="ddlItmCurncy" runat="server" Width="60%" AutoPostBack="true"
                                    OnSelectedIndexChanged="ddlItmCurncy_SelectedIndexChanged">
                                </asp:DropDownList>
                                <asp:Label ID="lblItemExRate" Text="0.00" runat="server" Width="30%" ForeColor="HOTPINK" />
                            </div>
                            <div class="col-md-2 EntyCell">
                                Remark
                                <br />
                                <asp:TextBox ID="txtRemark" runat="server" Width="90%" OnTextChanged="txtRemark_TextChanged"></asp:TextBox>
                            </div>
                            <div class="col-md-1 padding0">
                                Pax
                                <br />
                                <asp:TextBox ID="txtPax" onkeydown="return jsDecimals(event);" runat="server" AutoPostBack="true" Width="90%" OnTextChanged="txtPax_TextChanged"
                                    Style="text-align: right"></asp:TextBox>
                            </div>
                            <div class="col-md-1 padding0 EntyCell">
                                Unit Rate
                                <br />
                                <asp:TextBox ID="txtUnitRate" onkeydown="return jsDecimals(event);" AutoPostBack="true" runat="server" Width="90%" OnTextChanged="txtUnitRate_TextChanged"
                                    Style="text-align: right"></asp:TextBox>
                            </div>
                            <div class="col-md-1 padding0">
                                Dis(%)
                                <br />
                                <asp:TextBox ID="txtDisPercentage" onkeydown="return jsDecimals(event);" runat="server" AutoPostBack="true" Width="90%"
                                    Style="text-align: right" MaxLength="2" OnTextChanged="txtDisPercentage_TextChanged"></asp:TextBox>
                            </div>
                            <div class="col-md-1 padding0 EntyCell">
                                Discount
                                <br />
                                <asp:TextBox ID="txtDisAmount" runat="server" onkeydown="return jsDecimals(event);" AutoPostBack="true" Width="90%" Style="text-align: right"
                                    OnTextChanged="txtDisAmount_TextChanged"></asp:TextBox>
                            </div>
                            <div class="col-md-1 padding0">
                                Total
                                <br />
                                <asp:TextBox ID="txtTotal" runat="server" onkeydown="return jsDecimals(event);" AutoPostBack="true" Width="90%" OnTextChanged="txtTotal_TextChanged"
                                    Style="text-align: right"></asp:TextBox>
                            </div>
                            <div class="col-md-1 padding0 EntyCell">
                                <br />
                                <asp:ImageButton ID="btnProcess" ImageUrl="~/images/bubble-tail.png" runat="server"
                                    Width="20px" OnClick="btnProcess_Click" Visible="false" />
                                <asp:ImageButton ID="btnAddtoGrid" ImageUrl="~/images/stat-down.png" runat="server"
                                    Width="20px" OnClick="btnAddtoGrid_Click" />
                            </div>
                        </div>
                        <div class="row rowmargin0 col-md-12">
                            &nbsp;
                        </div>
                        <div class="row rowmargin0 col-md-12">
                            <asp:GridView ID="dgvInvoiceItems" runat="server" AutoGenerateColumns="False" OnRowCommand="dgvInvoiceItems_RowCommand"
                                OnRowDeleting="dgvInvoiceItems_RowDeleting" OnRowEditing="dgvInvoiceItems_RowEditing"
                                RowStyle-CssClass="myGridViewRowStyle">
                                <Columns>
                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            <asp:CheckBox ID="chkPrintAll" AutoPostBack="true" runat="server" Text="Print" OnCheckedChanged="chkPrintAll_CheckedChanged" />
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkPrint" AutoPostBack="true" runat="server" Checked='<%# Eval("Sad_print_stus") %>'
                                                OnCheckedChanged="chkPrint_CheckedChanged" />
                                        </ItemTemplate>
                                        <HeaderStyle Width="8%" HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Charge Code">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSad_itm_cd" runat="server" Text='<%# Bind("Sad_itm_cd") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Width="13%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Remark">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSad_alt_itm_desc" runat="server" Text='<%# Bind("Sad_alt_itm_desc") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Width="30%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="PAX">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSad_qty" runat="server" Text='<%# Bind("Sad_qty") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Width="8%" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Unit Rate">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSad_unit_rt" runat="server" Text='<%# Bind("Sad_unit_rt") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Width="13%" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Total">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSad_tot_amt" runat="server" Text='<%# Bind("Sad_tot_amt") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Width="13%" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText=" ">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="btndelete" runat="server" CommandArgument="<%# Container.DisplayIndex %>"
                                                CommandName="delete" ImageUrl="~/images/deleteicon.png" ToolTip="Delete.." ImageAlign="Middle"
                                                OnClientClick="return confirm('Are you sure you want to delete?');" />
                                        </ItemTemplate>
                                        <HeaderStyle CssClass="bodycolordarkgreen" HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" />
                                        <HeaderStyle Width="8%" />
                                    </asp:TemplateField>
                                </Columns>
                                <RowStyle CssClass="myGridViewRowStyle" />
                            </asp:GridView>
                        </div>
                        <div class="row rowmargin0 col-md-12">
                            &nbsp;
                        </div>
                        <div class="row rowmargin0 col-md-12 TotalLine">
                            <div class="col-md-8 padding0">
                                &nbsp;
                            </div>
                            <div class="col-md-4 padding0">
                                <div class="col-md-6 padding0">
                                    Invoice Total Amount
                                </div>
                                <div class="col-md-6 padding0">
                                    <asp:TextBox ID="txtInvoiceTotal" onkeydown="return jsDecimals(event);" ReadOnly="true" runat="server" AutoPostBack="true"
                                        Width="90%" Style="text-align: right"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
                <div class="row rowmargin0 col-md-12">
                    &nbsp;
                </div>
                <div class="row rowmargin0 col-md-12">
                    <div class="col-md-2 ">
                        Remark
                    </div>
                    <div class="col-md-8 ">
                        <asp:UpdatePanel ID="upRemakrs" runat="server">
                            <ContentTemplate>
                                <asp:TextBox ID="txtMainRemark" runat="server" AutoPostBack="true" Width="100%" Rows="5"
                                    TextMode="MultiLine"></asp:TextBox>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                    <div class="col-md-2 ">
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div style="float: left; height: 22px; width: 5%;">
        &nbsp;
    </div>
    <div class="row rowmargin0 col-md-12">
        <div class="panel panel-default">
            <div class="panel-heading pannelheading">
                Payment Details
            </div>
            <div class="panel-body">
                <div class="col-md-6 paddingleft0">
                    <asp:UpdatePanel ID="upPaymentsDetails" runat="server">
                        <ContentTemplate>
                            <div class="col-md-6 padding0">
                                <div class="col-md-4 padding2 textaling">
                                    Pay Mode
                                </div>
                                <div class="col-md-8  padding2">
                                    <asp:DropDownList ID="ddlPayMode" runat="server" CssClass="textbox" AutoPostBack="true"
                                        OnSelectedIndexChanged="ddlPayMode_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-md-6">
                                <div class="col-md-4  padding0 textaling">
                                    Amount
                                </div>
                                <div class="col-md-8 padding2">
                                    <div class="col-md-10 padding0 displayinlineblock">
                                        <asp:TextBox ID="txtPayAmount" runat="server" onkeydown="return jsDecimals(event);" CssClass="textbox"></asp:TextBox>
                                        <%--   <asp:RegularExpressionValidator ID="rxtxtPayAmount" ControlToValidate="txtPayAmount"
                                            runat="server" ErrorMessage="Please Enter decimal only" Display="None" ValidationExpression="^[+-]?\d{1,3}(?:,\d{3})*(?:[.]\d+)?$"></asp:RegularExpressionValidator>
                                        <asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" TargetControlID="rxtxtPayAmount"
                                            runat="server">
                                        </asp:ValidatorCalloutExtender>--%>
                                    </div>
                                    <div class="col-md-2 padding0 displayinlineblock">
                                        <asp:Button ID="btnPayAdd" runat="server" CssClass="Button" Text="Add" OnClick="btnPayAdd_Click" />
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-2 padding0">
                                Remarks
                            </div>
                            <div class="col-md-10 padding0">
                                <asp:TextBox ID="txtPayRemarks" runat="server" CssClass="TextBox" Width="100%" TextMode="MultiLine"
                                    Rows="2"></asp:TextBox>
                            </div>
                            <div class="col-md-12 padding2 textaling">
                                <asp:GridView ID="gvPayment" runat="server" AutoGenerateColumns="False" CssClass="GridView"
                                    CellPadding="3" ForeColor="#333333" GridLines="Both" DataKeyNames="sard_pay_tp,sard_settle_amt"
                                    ShowHeaderWhenEmpty="true" OnRowDeleting="gvPayment_RowDeleting">
                                    <EditRowStyle BackColor="#2461BF" />
                                    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                    <HeaderStyle BackColor="#507CD1" ForeColor="White" Font-Bold="True" />
                                    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                    <RowStyle BackColor="#EFF3FB" />
                                    <AlternatingRowStyle BackColor="White" />
                                    <%--<EmptyDataTemplate>
                                    <div style="width: 100%; text-align: center;">
                                        No data found
                                    </div>
                                </EmptyDataTemplate>--%>
                                    <Columns>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:ImageButton ID="btnImgDelete" runat="server" ImageAlign="Middle" ImageUrl="~/images/deleteicon.png"
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
                                        <asp:BoundField DataField='sard_settle_amt' HeaderText='Amount' DataFormatString="{0:N2}" />
                                        <asp:BoundField DataField='sard_sim_ser' HeaderText='' Visible="false" />
                                    </Columns>
                                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                    <SortedAscendingCellStyle BackColor="#F5F7FB" />
                                    <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                                    <SortedDescendingCellStyle BackColor="#E9EBEF" />
                                    <SortedDescendingHeaderStyle BackColor="#4870BE" />
                                </asp:GridView>
                            </div>
                            <div class="col-md-6 padding0">
                                <div class="col-md-4 padding2 textaling">
                                    Paid Amount
                                </div>
                                <div class="col-md-8 padding2 textaling">
                                    <asp:Label runat="server" ID="lblPayPaid" Text="0.00"></asp:Label>
                                </div>
                            </div>
                            <div class="col-md-6 padding0">
                                <div class="col-md-5 padding2 textaling">
                                    Balance Amount
                                </div>
                                <div class="col-md-7 padding2 textaling">
                                    <asp:Label runat="server" ID="lblPayBalance" Text="0.00"></asp:Label>
                                </div>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
                <div class="col-md-6 paddingleft0">
                    <asp:UpdatePanel ID="upDepositeBank" runat="server">
                        <ContentTemplate>
                            <div class="col-md-2 padding2">
                                Deposit Bank
                            </div>
                            <div class="col-md-4 padding2">
                                <div class="col-md-10 padding0 displayinlineblock">
                                    <asp:TextBox ID="txtDepositBank" runat="server" CssClass="textbox" AutoPostBack="true"></asp:TextBox>
                                </div>
                                <div class="col-md-2 padding0 displayinlineblock">
                                    <asp:ImageButton ID="btnDepositBank" runat="server" ImageUrl="~/Images/icon_search.png"
                                        ImageAlign="Middle" OnClick="btnDepositBank_Click" />
                                </div>
                            </div>
                            <div class="col-md-2 padding2">
                                Branch
                            </div>
                            <div class="col-md-4 padding2">
                                <div class="col-md-10 padding0 displayinlineblock">
                                    <asp:TextBox ID="txtDepositBranch" runat="server" CssClass="textbox" AutoPostBack="true"></asp:TextBox>
                                </div>
                                <div class="col-md-2 padding0 displayinlineblock">
                                    <asp:ImageButton ID="btnDepositBranch" runat="server" ImageUrl="~/Images/icon_search.png"
                                        ImageAlign="Middle" OnClick="btnDepositBranch_Click" Visible="false" />
                                </div>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <asp:UpdatePanel ID="upPaymentMethods" runat="server">
                        <ContentTemplate>
                            <asp:MultiView ID="MultiView1" runat="server">
                                <asp:View ID="vCash" runat="server">
                                    &nbsp;
                                </asp:View>
                                <asp:View ID="vCRCD" runat="server">
                                    <asp:Panel ID="Panel1" runat="server" Height="100%" Width="100%">
                                        <div style="float: left; width: 100%; height: 30px;">
                                            <div style="float: left; width: 20%; height: 22px;">
                                                Card num
                                            </div>
                                            <div style="float: left; width: 74%; height: 22px;">
                                                <asp:TextBox ID="txtCardNum" Width="100%" runat="server" />
                                            </div>
                                        </div>
                                        <div style="float: left; width: 100%; height: 30px;">
                                            <div style="float: left; width: 20%; height: 100%; text-align: right;">
                                                Bank
                                            </div>
                                            <div style="float: left; width: 30%; height: 100%;">
                                                <asp:TextBox ID="txtCRCDBank" runat="server" AutoPostBack="true" Width="70%" OnTextChanged="txtCRCDBank_TextChanged"></asp:TextBox>
                                                <asp:ImageButton ID="btnCRCDBank" runat="server" ImageUrl="~/Images/icon_search.png"
                                                    ImageAlign="Middle" OnClick="btnCRCDBank_Click" />
                                            </div>
                                            <div style="float: left; width: 20%; height: 100%; text-align: right;">
                                                Batch
                                            </div>
                                            <div style="float: left; width: 30%; height: 100%;">
                                                <asp:TextBox ID="txtCRCDBranch" runat="server" AutoPostBack="true" Width="70%"></asp:TextBox>
                                                <asp:ImageButton ID="btnCRCDBranch" runat="server" ImageUrl="~/Images/icon_search.png"
                                                    ImageAlign="Middle" OnClick="btnCRCDBranch_Click" Visible="false" />
                                            </div>
                                        </div>
                                        <div style="float: left; width: 100%; height: 30px;">
                                            <div style="float: left; width: 20%; height: 100%; text-align: right;">
                                                &nbsp;
                                            </div>
                                            <div style="float: left; width: 50%; height: 100%; text-align: right;">
                                                <asp:RadioButtonList ID="rbnOnlineOffline" runat="server" RepeatDirection="Horizontal">
                                                    <asp:ListItem>Offline</asp:ListItem>
                                                    <asp:ListItem Selected="True">Online</asp:ListItem>
                                                </asp:RadioButtonList>
                                                &nbsp;
                                            </div>
                                        </div>
                                        <div style="float: left; width: 100%; height: 30px;">
                                            <div style="float: left; width: 20%; height: 100%; text-align: right;">
                                                Card Type
                                            </div>
                                            <div style="float: left; width: 30%; height: 100%;">
                                                <asp:DropDownList ID="ddlCardType" runat="server" Width="100%">
                                                </asp:DropDownList>
                                            </div>
                                            <div style="float: left; width: 20%; height: 100%; text-align: right;">
                                                Expire Date
                                            </div>
                                            <div style="float: left; width: 30%; height: 100%;">
                                                <asp:TextBox ID="txtExpireDate" runat="server" AutoPostBack="true" Width="60%"></asp:TextBox>
                                                <asp:CalendarExtender ID="CalendarExtender8" runat="server" Enabled="True" Format="dd/MMM/yyyy"
                                                    PopupButtonID="cal1" TargetControlID="txtExpireDate">
                                                </asp:CalendarExtender>
                                                <img alt="Calendar.." height="16" src="images/calendar.png" width="16" id="cal1"
                                                    style="cursor: pointer" />
                                            </div>
                                        </div>
                                        <div style="float: left; width: 100%; height: 30px;">
                                            <div style="float: left; width: 10%; height: 100%; text-align: right;">
                                            </div>
                                            <div style="float: left; width: 30%; height: 100%;">
                                                <asp:CheckBox ID="chkPromotion" Text="Promotion" runat="server" OnCheckedChanged="chkPromotion_CheckedChanged" />
                                            </div>
                                            <div style="float: left; width: 15%; height: 100%; text-align: right;">
                                                Period
                                            </div>
                                            <div style="float: left; width: 45%; height: 100%;">
                                                <asp:TextBox ID="txtCRCDPeriod" runat="server" AutoPostBack="true" Width="60%" Enabled="false"></asp:TextBox>
                                                Months
                                            </div>
                                        </div>
                                    </asp:Panel>
                                </asp:View>
                                <asp:View ID="vCheque" runat="server">
                                    <asp:Panel ID="Panel2" runat="server" Height="100%" Width="100%">
                                        <div style="float: left; width: 100%; height: 30px;">
                                            <div style="float: left; width: 20%; height: 22px;">
                                                Cheque No
                                            </div>
                                            <div style="float: left; width: 74%; height: 22px;">
                                                <asp:TextBox ID="txtChequeNum" Width="100%" runat="server" />
                                            </div>
                                            <div style="float: left; width: 100%; height: 30px;">
                                                <div style="float: left; width: 20%; height: 100%; text-align: right;">
                                                    Bank
                                                </div>
                                                <div style="float: left; width: 30%; height: 100%;">
                                                    <asp:TextBox ID="txtChequeBank" runat="server" AutoPostBack="true" Width="70%"></asp:TextBox>
                                                    <asp:ImageButton ID="btnChqBank" runat="server" ImageUrl="~/Images/icon_search.png"
                                                        ImageAlign="Middle" OnClick="btnChqBank_Click" />
                                                </div>
                                                <div style="float: left; width: 20%; height: 100%; text-align: right;">
                                                    Batch
                                                </div>
                                                <div style="float: left; width: 30%; height: 100%;">
                                                    <asp:TextBox ID="txtChequeBranch" runat="server" AutoPostBack="true" Width="70%"></asp:TextBox>
                                                    <asp:ImageButton ID="btnChqBranch" runat="server" ImageUrl="~/Images/icon_search.png"
                                                        ImageAlign="Middle" />
                                                </div>
                                            </div>
                                            <div style="float: left; width: 100%; height: 30px;">
                                                <div style="float: left; width: 20%; height: 100%; text-align: right;">
                                                    Cheque Date
                                                </div>
                                                <div style="float: left; width: 30%; height: 100%;">
                                                    <asp:TextBox ID="txtChequeDate" runat="server" AutoPostBack="true" Width="70%"></asp:TextBox>
                                                    <asp:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="True" Format="dd/MMM/yyyy"
                                                        PopupButtonID="cal" TargetControlID="txtChequeDate">
                                                    </asp:CalendarExtender>
                                                    <img alt="Calendar.." height="16" src="images/calendar.png" width="16" id="Img1"
                                                        style="cursor: pointer" />
                                                </div>
                                                <div style="float: left; width: 20%; height: 100%; text-align: right;">
                                                    &nbsp;
                                                </div>
                                                <div style="float: left; width: 30%; height: 100%;">
                                                    &nbsp;
                                                </div>
                                            </div>
                                        </div>
                                    </asp:Panel>
                                </asp:View>
                                <asp:View ID="vADVAN" runat="server">
                                    <asp:Panel ID="Panel4" runat="server" Height="100%" Width="100%">
                                        <div style="float: left; width: 100%; height: 30px;">
                                            <div style="float: left; width: 20%; height: 22px;">
                                                Ref num
                                            </div>
                                            <div style="float: left; width: 74%; height: 22px;">
                                                <asp:TextBox ID="txtADCANReffNumber" Width="80%" runat="server" AutoPostBack="True"
                                                    OnTextChanged="txtADCANReffNumber_TextChanged" />
                                                <asp:ImageButton ID="btnADVANReffNumber" runat="server" ImageUrl="~/Images/icon_search.png"
                                                    ImageAlign="Middle" OnClick="btnADVANReffNumber_Click" />
                                            </div>
                                        </div>
                                        <div style="float: left; width: 100%; height: 30px;">
                                            <div style="float: left; width: 20%; height: 22px;">
                                                Ref Amount
                                            </div>
                                            <div style="float: left; width: 74%; height: 22px;">
                                                <asp:TextBox ID="txtADVANReffAmount" Width="100%" onkeydown="return jsDecimals(event);" runat="server" />
                                            </div>
                                        </div>
                                    </asp:Panel>
                                </asp:View>
                            </asp:MultiView>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>
    </div>
    <asp:Panel ID="pnlDiscount" runat="server" Width="484px" Height="143px" CssClass="ModalPopup" Style="display: none;">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" ChildrenAsTriggers="true">
            <ContentTemplate>
                <div id="divDiscHeader" class="row rowmargin0 col-md-12" style="background-color: LIGHTSTEELBLUE;">
                    <div class="col-md-11">
                        Discount Request Detail
                    </div>
                    <div class="col-md-1" style="text-align: right;">
                        <asp:ImageButton ID="btnClose" runat="server" ImageUrl="../images/Close.png" OnClick="btnClose_Click" />
                    </div>
                </div>
                <div class="row rowmargin0 col-md-12">
                    &nbsp;
                </div>
                <div class="row rowmargin0 col-md-12">
                    <div class="col-md-4">
                        <div class="col-md-4 padding1">
                            Category
                        </div>
                        <div class="col-md-8 padding0">
                            <asp:DropDownList ID="ddlCategoryMP" runat="server" Width="100%" AutoPostBack="true">
                                <asp:ListItem Selected="True">Customer</asp:ListItem>
                                <asp:ListItem>Item</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-md-4">
                        <div class="col-md-4 padding1">
                            Rate
                        </div>
                        <div class="col-md-8 padding0">
                            <asp:TextBox ID="txtDisRateMP" onkeydown="return jsDecimals(event);" Width="80%" AutoPostBack="true" runat="server" MaxLength="3" />%
                        </div>
                    </div>
                    <div class="col-md-4">
                        <asp:Button ID="btnDisReqNew" Width="80px" Text="Request" runat="server" OnClick="btnDisReqNew_Click" />
                    </div>
                </div>
                <div class="row rowmargin0 col-md-12">
                    <asp:GridView ID="dgvDiscounts" runat="server" AutoGenerateColumns="False">
                        <Columns>
                            <asp:TemplateField HeaderText="Select">
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkSelect" runat="server" />
                                </ItemTemplate>
                                <HeaderStyle Width="20px" HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Item">
                                <ItemTemplate>
                                    <asp:Label ID="lblItmeCodeMP" runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Item Type">
                                <ItemTemplate>
                                    <asp:DropDownList ID="ddlItemTypeMP" runat="server">
                                        <asp:ListItem>Rate</asp:ListItem>
                                        <asp:ListItem>Amount</asp:ListItem>
                                    </asp:DropDownList>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Amount">
                                <ItemTemplate>
                                    <asp:Label ID="lblDisAmountMP" runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Price Book">
                                <ItemTemplate>
                                    <asp:Label ID="lblPBMP" runat="server" Text='<%# Bind("Sgdd_pb") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Price Level">
                                <ItemTemplate>
                                    <asp:Label ID="lblPLMP" runat="server" Text='<%# Bind("Sgdd_pb_lvl") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
            </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="btnDisReqNew" />
            </Triggers>
        </asp:UpdatePanel>
    </asp:Panel>
    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <asp:Button ID="btnMPDiscount" runat="server" Text="D3" Style="display: none" />
            <asp:ModalPopupExtender ID="mpDiscount" runat="server" Enabled="True"
                PopupControlID="pnlDiscount" TargetControlID="btnMPDiscount" BackgroundCssClass="modalBackground"
                PopupDragHandleControlID="divDiscHeader">
            </asp:ModalPopupExtender>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:Panel ID="pnlInvoicePrint" runat="server" Width="700px" Height="500px" CssClass="ModalPopup" Style="display: none;">
        <div style="text-align: right; background-color: Silver;">
            <asp:UpdatePanel ID="upPrint" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                <ContentTemplate>
                    <asp:Button ID="Close" Text="Close" Width="80px" runat="server" OnClick="Close_Click" />
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
        <div>
            <iframe style="width: 700px; height: 500px;" id="irm1" runat="server"></iframe>
        </div>
    </asp:Panel>
    <asp:Button ID="btnMDprint" runat="server" Text="D3" Style="display: none" />
    <asp:ModalPopupExtender ID="mpInvoicePrint" runat="server" DynamicServicePath=""
        Enabled="True" PopupControlID="pnlInvoicePrint" TargetControlID="btnMDprint"
        BackgroundCssClass="modalBackground" PopupDragHandleControlID="pnlInvoicePrint">
    </asp:ModalPopupExtender>
</asp:Content>
