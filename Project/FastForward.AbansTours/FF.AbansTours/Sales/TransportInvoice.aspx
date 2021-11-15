<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="TransportInvoice.aspx.cs" Inherits="FF.AbansTours.Sales.TransportInvoice" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
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
    <style type="text/css">
        .divWaiting {
            position: absolute;
            background-color: #FAFAFA;
            z-index: 2147483647 !important;
            opacity: 0.8;
            overflow: hidden;
            text-align: center;
            top: 0;
            left: 0;
            height: 100%;
            width: 100%;
            padding-top: 20%;
        }
    </style>
    <asp:UpdatePanel ID="messages" runat="server">
        <ContentTemplate>
            <div class="row">
                <div visible="false" class="alert alert-success  divWaiting" role="alert" runat="server" id="DivAlert">
                    <div class="col-sm-11  buttonrow ">
                        <strong>Information! </strong>
                        <asp:Label ID="lblAsk" runat="server"></asp:Label>
                    </div>
                    <div class="col-sm-1  buttonrow">
                        <div class="col-sm-3  buttonrow">
                            <asp:LinkButton ID="lbtColse" runat="server" CausesValidation="false" CssClass="floatright" OnClick="lbtColse_Click">
                                        <span class="glyphicon glyphicon-remove " aria-hidden="true" ></span>
                            </asp:LinkButton>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <div class="row rowmargin0 col-md-12" style="height: 22px; width: 100%; text-align: right; background-color: Silver;">
                <div class="col-md-8">
                </div>
                <asp:UpdatePanel ID="upCommonBtn" runat="server">
                    <ContentTemplate>
                        <div style="height: 22px; width: 100%; text-align: right; background-color: Silver;">
                            <asp:Button ID="btnCreate" Text="Save" runat="server" Width="80px" OnClick="btnCreate_Click" />
                            <asp:Button ID="btnCancel" Text="Cancel" runat="server" Width="80px" OnClick="btnCancel_Click" />
                            <asp:Button ID="btnClear" Text="Clear" runat="server" Width="80px" OnClick="btnClear_Click" />
                            <asp:Button ID="btnBack" Text="Back" runat="server" Width="80px" OnClick="btnBack_Click" />
                            <asp:ConfirmButtonExtender ID="ConfirmButtonExtender3" runat="server" TargetControlID="btnClear"
                                ConfirmText="Do you want to clear?" ConfirmOnFormSubmit="false">
                            </asp:ConfirmButtonExtender>
                            <asp:ConfirmButtonExtender ID="ConfirmButtonExtender4" runat="server" TargetControlID="btnCreate"
                                ConfirmText="Do you want to save?" ConfirmOnFormSubmit="false">
                            </asp:ConfirmButtonExtender>
                            <asp:ConfirmButtonExtender ID="ConfirmButtonExtender2" runat="server" TargetControlID="btnBack"
                                ConfirmText="Do you want go to previous page?" ConfirmOnFormSubmit="false">
                            </asp:ConfirmButtonExtender>
                            <asp:ConfirmButtonExtender ID="ConfirmButtonExtender1" runat="server" TargetControlID="btnCancel"
                                ConfirmText="Do you want to cancel this invoice?" ConfirmOnFormSubmit="false">
                            </asp:ConfirmButtonExtender>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
            <div class="col-md-12">
                &nbsp;
            </div>
            <div class="row rowmargin0 col-md-12">
                <div class="panel panel-default">
                    <div class="panel-heading pannelheading">
                        Transport Invoice
                    </div>
                    <div class="panel-body paddingleft0 paddingright30">
                        <div class="row rowmargin0 col-md-12">
                            <div class="row no-margin-left">
                                <div class="col-md-2 padding3">
                                    Process Date
                                </div>
                                <div class="col-md-2 padding5">
                                    <div class="col-md-10 padding0 displayinlineblock">
                                        <asp:TextBox ID="txtProcessDate" runat="server" CssClass="textbox" Format="dd/MMM/yyyy"
                                            onkeypress="return RestrictSpace()" Enabled="false"></asp:TextBox>
                                        <asp:CalendarExtender ID="CalendarExtender4" runat="server" TargetControlID="txtProcessDate" Enabled="false"
                                            PopupButtonID="btnProcessDate" Format="dd/MMM/yyyy">
                                        </asp:CalendarExtender>
                                    </div>
                                    <div class="col-md-2 padding0 displayinlineblock">
                                        <%--  <asp:ImageButton ID="btnProcessDate" runat="server" ImageUrl="~/Images/calendar.png"
                                            ImageAlign="Middle" CssClass="imageicon" />--%>
                                    </div>
                                </div>
                                <div class="col-md-2 padding3">
                                    Customer Code
                                </div>
                                <div class="col-md-2 padding5 ">
                                    <div class="col-md-10 padding0 displayinlineblock">
                                        <asp:TextBox ID="txtCustCode" runat="server" CssClass="textbox" OnTextChanged="txtCustCode_TextChanged"></asp:TextBox>
                                    </div>
                                    <div class="col-md-2 padding0 displayinlineblock">
                                        <asp:ImageButton ID="btnCustCode" runat="server" ImageUrl="~/Images/icon_search.png"
                                            ImageAlign="Middle" CssClass="imageicon" OnClick="btnCustCode_Click" />
                                    </div>
                                </div>
                                <div class="col-md-2 padding3">
                                    Invoice Number
                                </div>
                                <div class="col-md-2 padding5">
                                    <div class="col-md-8 padding0 displayinlineblock">
                                        <asp:TextBox ID="txtPaymentNo" runat="server" CssClass="textbox" OnTextChanged="txtPaymentNo_TextChanged"></asp:TextBox>
                                    </div>
                                    <div class="col-md-2 padding0 displayinlineblock">
                                        <asp:ImageButton ID="btnPaymentNo" runat="server" ImageUrl="~/Images/icon_search.png"
                                            ImageAlign="Middle" CssClass="imageicon" OnClick="btnPaymentNo_Click" />
                                    </div>
                                    <div class="col-md-2 padding0 displayinlineblock">
                                        <asp:ImageButton ID="ImgPaymentNoserch" runat="server" ImageUrl="~/Images/LoadDetails.png"
                                            ImageAlign="Middle" CssClass="imageicon" OnClick="ImgPaymentNoserch_Click" />
                                    </div>
                                </div>
                            </div>
                            <div class="row no-margin-left">
                                <div class="col-md-2 padding3">
                                    Payment From Date
                                </div>
                                <div class="col-md-2 padding5">
                                    <div class="col-md-10 padding0 displayinlineblock">
                                        <asp:TextBox ID="txtPaymentFromDate" runat="server" CssClass="textbox" Format="dd/MMM/yyyy"
                                            onkeypress="return RestrictSpace()" Enabled="false"></asp:TextBox>
                                        <asp:CalendarExtender ID="CalendarExtender5" runat="server" TargetControlID="txtPaymentFromDate"
                                            PopupButtonID="imagPaymentFromDate" Format="dd/MMM/yyyy">
                                        </asp:CalendarExtender>
                                    </div>
                                    <div class="col-md-2 padding0 displayinlineblock">
                                        <asp:ImageButton ID="imagPaymentFromDate" runat="server" ImageUrl="~/Images/calendar.png"
                                            ImageAlign="Middle" CssClass="imageicon" />
                                    </div>
                                </div>
                                <div class="col-md-2 padding3">
                                    Payment Type
                                </div>
                                <div class="col-md-2 padding5 ">
                                    <asp:DropDownList ID="ddlPaymentType" runat="server" CssClass="textbox ddlhight1" AutoPostBack="true">
                                    </asp:DropDownList>
                                </div>
                                <div class="col-md-2 padding3">
                                    Total Amount
                                </div>
                                <div class="col-md-2 padding5">
                                    <asp:TextBox ID="txtTotalAmount" runat="server" CssClass="textbox textAlign" Enabled="false" onkeydown="return jsDecimals(event);" MaxLength="9" AutoPostBack="true"></asp:TextBox>
                                </div>
                            </div>
                            <div class="row no-margin-left">
                                <div class="col-md-2 padding3">
                                    Payment To Date
                                </div>
                                <div class="col-md-2 padding5">
                                    <div class="col-md-10 padding0 displayinlineblock">
                                        <asp:TextBox ID="txtPaymentToDate" runat="server" CssClass="textbox" Format="dd/MMM/yyyy"
                                            onkeypress="return RestrictSpace()" Enabled="false"></asp:TextBox>
                                        <asp:CalendarExtender ID="CalendarExtender6" runat="server" TargetControlID="txtPaymentToDate"
                                            PopupButtonID="btnPaymentToDate" Format="dd/MMM/yyyy">
                                        </asp:CalendarExtender>
                                    </div>
                                    <div class="col-md-2 padding0 displayinlineblock">
                                        <asp:ImageButton ID="btnPaymentToDate" runat="server" ImageUrl="~/Images/calendar.png"
                                            ImageAlign="Middle" CssClass="imageicon" />
                                    </div>
                                </div>
                                <div class="col-md-2 padding3">
                                    <asp:Button ID="btnSearch" Text="Search" runat="server" Width="80px" Enabled="true" OnClick="btnSearch_Click" />
                                </div>
                                <div class="col-md-2 padding0">
                                </div>
                                <div class="col-md-2 padding3">
                                    Balance Amount
                                </div>
                                <div class="col-md-2 padding5">
                                    <asp:TextBox ID="txtBalanceAmount" runat="server" CssClass="textbox textAlign" Enabled="false"></asp:TextBox>
                                </div>
                            </div>
                            <div class="row no-margin-left">
                                <div class="col-md-2 padding3">
                                    Remark
                                </div>
                                <div class="col-md-10 padding5">
                                    <asp:TextBox ID="txtRemark" runat="server" CssClass="textbox" TextMode="MultiLine" Rows="2"></asp:TextBox>
                                </div>
                            </div>
                            <div class="row no-margin-left">
                                <div class="col-md-2 padding3">
                                </div>
                                <div class="col-md-10 padding5">
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-md-12">
                &nbsp;
            </div>
            <div class="row rowmargin0 col-md-12">
                <div class="panel panel-default">
                    <div class="panel-body paddingleft0 paddingright30">
                        <div class="row rowmargin0 col-md-12">
                            <asp:GridView ID="dgvItems" runat="server" AutoGenerateColumns="false" CssClass="table table-bordered tablemod">
                                <Columns>
                                    <asp:TemplateField HeaderText="TLD_SEQ" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblTLD_SEQ" runat="server" Text='<%# Bind("TLD_SEQ") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Line" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblTLD_LINE" runat="server" Text='<%# Bind("TLD_LINE") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Charge Code">
                                        <ItemTemplate>
                                            <asp:Label ID="lblTLD_CHR_CD" runat="server" Text='<%# Bind("TLD_CHR_CD") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Width="7%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Description">
                                        <ItemTemplate>
                                            <asp:Label ID="lblTLD_CHR_DESC" runat="server" Text='<%# Bind("TLD_CHR_DESC") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Width="7%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Date">
                                        <ItemTemplate>
                                            <asp:Label ID="lblTLH_DT" runat="server" Text='<%# Bind("TLH_DT") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Width="3%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Qty">
                                        <ItemTemplate>
                                            <asp:Label ID="lblTLD_QTY" runat="server" Text='<%# Bind("TLD_QTY") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Width="3%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Rate Type">
                                        <ItemTemplate>
                                            <asp:Label ID="lblTLD_RT_TP" runat="server" Text='<%# Bind("TLD_RT_TP") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Width="7%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Unit Rate">
                                        <ItemTemplate>
                                            <asp:Label ID="lblTLD_U_RT" runat="server" Text='<%# Bind("TLD_U_RT") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Width="7%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Unit Amount">
                                        <ItemTemplate>
                                            <asp:Label ID="lblTLD_U_AMT" runat="server" Text='<%# Bind("TLD_U_AMT") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Width="7%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="TAX">
                                        <ItemTemplate>
                                            <asp:Label ID="lblTLD_TAX" runat="server" Text='<%# Bind("TLD_TAX") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Width="3%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Dis Rate">
                                        <ItemTemplate>
                                            <asp:Label ID="lblTLD_DIS_RT" runat="server" Text='<%# Bind("TLD_DIS_RT") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Width="5%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Dis Amount">
                                        <ItemTemplate>
                                            <asp:Label ID="lblTLD_DIS_AMT" runat="server" Text='<%# Bind("TLD_DIS_AMT") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Width="7%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Total">
                                        <ItemTemplate>
                                            <asp:Label ID="lblTLD_TOT" runat="server" Text='<%# Bind("TLD_TOT") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Width="7%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Is Customer" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblTLD_IS_CUS" Visible="false" runat="server" Text='<%# Bind("TLD_IS_CUS") %>'></asp:Label>
                                            <asp:ImageButton ID="imgCusFalse" Enabled="false" Visible="false" runat="server" ImageUrl="../images/False.png" />
                                            <asp:ImageButton ID="imgCusTrue" Enabled="false" Visible="false" runat="server" ImageUrl="../images/True.png" />
                                        </ItemTemplate>
                                        <HeaderStyle Width="5%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Is Driver" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblTLD_IS_DRI" Visible="false" runat="server" Text='<%# Bind("TLD_IS_DRI") %>'></asp:Label>
                                            <asp:ImageButton ID="imgDriverFalse" Visible="false" runat="server" ImageUrl="../images/False.png" />
                                            <asp:ImageButton ID="imgDriverTrue" Visible="false" runat="server" ImageUrl="../images/True.png" />
                                        </ItemTemplate>
                                        <HeaderStyle Width="5%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="TLD_IS_ADD" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblTTLD_IS_ADD" runat="server" Text='<%# Bind("TLD_IS_ADD") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Width="7%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText=" " Visible="false">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="btndelete" runat="server" CommandArgument="<%# Container.DisplayIndex %>"
                                                CommandName="delete" ImageUrl="~/images/deleteicon.png" ToolTip="Delete.." ImageAlign="Middle"
                                                OnClientClick="return confirm('Are you sure you want to delete?');" />
                                        </ItemTemplate>
                                        <HeaderStyle CssClass="bodycolordarkgreen" HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" />
                                        <HeaderStyle Width="1%" />
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>
                </div>
            </div>
            <div class="row rowmargin0 col-md-12">
                <div class="panel panel-default">
                    <div class="panel-heading pannelheading">
                        Payment Details
                    </div>
                    <div class="panel-body paddingleft0 paddingright30">
                        <div class="row rowmargin0 col-md-12">
                            <div class="col-md-6 paddingleft0">
                                <div class="col-md-6 padding0">
                                    <div class="col-md-4  padding2">
                                        Pay Mode
                                    </div>
                                    <div class="col-md-8  padding2">
                                        <asp:DropDownList ID="ddlPayMode" runat="server" CssClass="textbox ddlhight1" AutoPostBack="True" OnSelectedIndexChanged="ddlPayMode_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="col-md-4  padding0 textaling">
                                        Amount
                                    </div>
                                    <div class="col-md-8 padding2">
                                        <div class="col-md-10 padding0 displayinlineblock">
                                            <asp:TextBox ID="txtAmount" runat="server" CssClass="textbox textAlign" onkeydown="return jsDecimals(event);" MaxLength="9"></asp:TextBox>
                                        </div>
                                        <div class="col-md-2 padding0 displayinlineblock">
                                            <asp:ImageButton ID="ImgAmount" runat="server" ImageUrl="~/Images/LoadDetails.png"
                                                ImageAlign="Middle" CssClass="imageicon" OnClick="ImgAmount_Click" />
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-12 padding0">
                                    <div class="col-md-2 padding2">
                                        Remark
                                    </div>
                                    <div class="col-md-10 padding2">
                                        <asp:TextBox ID="txtRemarkDetails" runat="server" CssClass="textbox form-control" Rows="3"
                                            TextMode="MultiLine"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-12  padding2 displayinlineblock">
                                    <asp:GridView ID="grdPaymentDetails" runat="server" AutoGenerateColumns="false" CssClass="table table-bordered" OnRowCommand="grdPaymentDetails_RowCommand1">
                                        <Columns>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="btnImgDelete" runat="server" ImageAlign="Middle" ImageUrl="~/images/deleteicon.png"
                                                        Width="10px" Height="10px" CommandName="DeleteAmount" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" />
                                                    <asp:ConfirmButtonExtender ID="CbeCancel" runat="server" TargetControlID="btnImgDelete"
                                                        ConfirmText="Do you want to Delete?" ConfirmOnFormSubmit="false">
                                                    </asp:ConfirmButtonExtender>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField='sird_seq_no' HeaderText='' Visible="false" />
                                            <asp:BoundField DataField='sird_line_no' HeaderText='' Visible="false" />
                                            <asp:BoundField DataField='sird_receipt_no' HeaderText='' Visible="false" />
                                            <asp:BoundField DataField='sird_inv_no' HeaderText='' Visible="false" />
                                            <asp:BoundField DataField='Sird_pay_tp' HeaderText='Payment Type' ItemStyle-Width="110px"
                                                HeaderStyle-Width="110px" />
                                            <asp:BoundField DataField='sird_ref_no' HeaderText='' Visible="false" />
                                            <asp:BoundField DataField='sird_chq_bank_cd' HeaderText='Bank' ItemStyle-Width="90px"
                                                HeaderStyle-Width="90px" />
                                            <asp:BoundField DataField='sird_chq_branch' HeaderText='Branch' ItemStyle-Width="90px"
                                                HeaderStyle-Width="90px" />
                                            <asp:BoundField DataField='sird_deposit_bank_cd' HeaderText='' Visible="false" />
                                            <asp:BoundField DataField='sird_deposit_branch' HeaderText='' Visible="false" />
                                            <asp:BoundField DataField='sird_credit_card_bank' HeaderText='' Visible="false" />
                                            <asp:BoundField DataField='sird_cc_tp' HeaderText='Card Type' />
                                            <asp:BoundField DataField='sird_cc_expiry_dt' HeaderText='Expiry Date' Visible="false" />
                                            <asp:BoundField DataField='sird_cc_is_promo' HeaderText='Promotion' Visible="false" />
                                            <asp:BoundField DataField='sird_cc_period' HeaderText='Period' Visible="false" />
                                            <asp:BoundField DataField='sird_gv_issue_loc' HeaderText='' Visible="false" />
                                            <asp:BoundField DataField='sird_gv_issue_dt' HeaderText='' Visible="false" />
                                            <asp:BoundField DataField='sird_settle_amt' HeaderText='Amount' />
                                            <asp:BoundField DataField='sird_sim_ser' HeaderText='' Visible="false" />
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>
                            <div class="col-md-6 paddingright0">
                                <asp:MultiView ID="mltPaymentDetails" runat="server" ActiveViewIndex="0">
                                    <asp:View ID="Cash" runat="server">
                                        <div class="col-md-2 padding2">
                                            Deposit Bank
                                        </div>
                                        <div class="col-md-4 padding2">
                                            <div class="col-md-10 padding0 displayinlineblock">
                                                <asp:TextBox ID="txtDepositBank" runat="server" CssClass="textbox"></asp:TextBox>
                                            </div>
                                            <div class="col-md-2 padding0 displayinlineblock">
                                                <asp:ImageButton ID="ImagebtnDepositBank" runat="server" ImageUrl="~/Images/icon_search.png"
                                                    ImageAlign="Middle" CssClass="imageicon" OnClick="ImagebtnDepositBank_Click" />
                                            </div>
                                        </div>
                                        <div class="col-md-2 padding2 textaling">
                                            Branch
                                        </div>
                                        <div class="col-md-4 padding2">
                                            <asp:TextBox ID="txtDepositBranch" runat="server" CssClass="textbox"></asp:TextBox>
                                        </div>
                                    </asp:View>
                                    <asp:View ID="CRCD" runat="server">
                                        <div class="col-md-2 padding2">
                                            Card No
                                        </div>
                                        <div class="col-md-10 padding2">
                                            <asp:TextBox ID="txtCardNo" runat="server" CssClass="textbox" onkeydown="return jsDecimals(event);" MaxLength="9"></asp:TextBox>
                                        </div>
                                        <div class="col-md-2 padding2">
                                            Bank
                                        </div>
                                        <div class="col-md-4 padding2">
                                            <div class="col-md-9 padding0 displayinlineblock">
                                                <asp:TextBox ID="txtBankCard" runat="server" CssClass="textbox" OnTextChanged="txtBankCard_TextChanged"></asp:TextBox>
                                            </div>
                                            <div class="col-md-2 padding0 displayinlineblock">
                                                <asp:ImageButton ID="imgbtntxtBankCard" runat="server" ImageUrl="~/Images/icon_search.png"
                                                    ImageAlign="Middle" CssClass="imageicon" OnClick="imgbtntxtBankCard_Click" />
                                            </div>
                                            <div class="col-md-1 padding0 displayinlineblock">
                                                <asp:ImageButton ID="imgbtnbankcard" runat="server" ImageUrl="~/Images/LoadDetails.png"
                                                    ImageAlign="Middle" CssClass="imageicon" OnClick="imgbtnbankcard_Click" />
                                            </div>
                                        </div>
                                        <div class="col-md-2 padding2 textaling">
                                            Branch
                                        </div>
                                        <div class="col-md-4 padding2">
                                            <asp:TextBox ID="txtBranchCard" runat="server" CssClass="textbox"></asp:TextBox>
                                        </div>
                                        <div class="col-md-8 padding2">
                                            <asp:Label ID="lblbank" runat="server" Text=""></asp:Label>
                                        </div>
                                        <div class="col-md-4 padding2">
                                            <asp:RadioButtonList ID="rblMeasurementSystem" runat="server" RepeatDirection="Horizontal"
                                                CssClass="table0">
                                                <asp:ListItem Text="Offline" Value="0" />
                                                <asp:ListItem Text="Online" Value="0" />
                                            </asp:RadioButtonList>
                                        </div>
                                        <div class="col-md-2 padding2">
                                            Card Type
                                        </div>
                                        <div class="col-md-4 padding2">
                                            <asp:DropDownList ID="ddlCardType" runat="server" CssClass="textbox ddlhight1">
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-md-2 padding2 textaling">
                                            Expire
                                        </div>
                                        <div class="col-md-4 padding2">
                                            <div class="col-md-11 padding0 displayinlineblock">
                                                <asp:TextBox ID="txtExpireCard" runat="server" CssClass="textbox" Format="MMM/yyyy"
                                                    onkeypress="return RestrictSpace()" Enabled="false"></asp:TextBox>
                                                <asp:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtExpireCard"
                                                    PopupButtonID="imgbtnExpireCard" DefaultView="Months" Format="MMM/yyyy">
                                                </asp:CalendarExtender>
                                            </div>
                                            <div class="col-md-1 padding0 displayinlineblock">
                                                <asp:ImageButton ID="imgbtnExpireCard" runat="server" ImageUrl="~/Images/calendar.png"
                                                    ImageAlign="Middle" CssClass="imageicon" />
                                            </div>
                                        </div>
                                        <div class="col-md-2 padding2">
                                            Deposit Bank
                                        </div>
                                        <div class="col-md-4 padding2">
                                            <div class="col-md-11 padding0 displayinlineblock">
                                                <asp:TextBox ID="txtDepositBankCard" runat="server" CssClass="textbox"></asp:TextBox>
                                            </div>
                                            <div class="col-md-1 padding0 displayinlineblock">
                                                <asp:ImageButton ID="imgbtnDepositBankCard" runat="server" ImageUrl="~/Images/icon_search.png"
                                                    ImageAlign="Middle" CssClass="imageicon" OnClick="imgbtnDepositBankCard_Click" />
                                            </div>
                                        </div>
                                        <div class="col-md-2 padding2 textaling">
                                            Branch
                                        </div>
                                        <div class="col-md-4 padding2">
                                            <div class="col-md-11 padding0 displayinlineblock">
                                                <asp:TextBox ID="txtDepositBranchCard" runat="server" CssClass="textbox"></asp:TextBox>
                                            </div>
                                            <div class="col-md-1 padding0 displayinlineblock">
                                                <asp:ImageButton ID="imgbtnDepositBranchCard" runat="server" ImageUrl="~/Images/icon_search.png"
                                                    ImageAlign="Middle" CssClass="imageicon" />
                                            </div>
                                        </div>
                                        <asp:Panel ID="pnlPermotion" runat="server">
                                            <div class="col-md-2 padding2">
                                                Promotion
                                            </div>
                                            <div class="col-md-2 padding2">
                                                <asp:CheckBox ID="chkPromotion" runat="server" />
                                            </div>
                                            <div class="col-md-2 padding2">
                                                <asp:Label ID="lblPromotion" runat="server" Text=""></asp:Label>
                                            </div>
                                            <div class="col-md-2 padding2 textaling">
                                                Period
                                            </div>
                                            <div class="col-md-4 padding2">
                                                <asp:TextBox ID="txtPeriod" runat="server" CssClass="textbox" AutoPostBack="true"
                                                    onkeydown="return jsDecimals(event);" MaxLength="9" OnTextChanged="txtPeriod_TextChanged"></asp:TextBox>
                                            </div>
                                        </asp:Panel>
                                    </asp:View>
                                    <asp:View ID="Cheque" runat="server">
                                        <div class="col-md-2 padding2">
                                            Cheque No
                                        </div>
                                        <div class="col-md-10 padding2">
                                            <asp:TextBox ID="txtChequeNo" runat="server" CssClass="textbox"></asp:TextBox>
                                        </div>
                                        <div class="col-md-2 padding2">
                                            Bank
                                        </div>
                                        <div class="col-md-4 padding2">
                                            <div class="col-md-10 padding0 displayinlineblock">
                                                <asp:TextBox ID="txtBankCheque" runat="server" CssClass="textbox"></asp:TextBox>
                                            </div>
                                            <div class="col-md-2 padding0 displayinlineblock">
                                                <asp:ImageButton ID="imgbtnBankCheque" runat="server" ImageUrl="~/Images/icon_search.png"
                                                    ImageAlign="Middle" CssClass="imageicon" OnClick="imgbtnBankCheque_Click" />
                                            </div>
                                        </div>
                                        <div class="col-md-2 padding2 textaling">
                                            Branch
                                        </div>
                                        <div class="col-md-4 padding2">
                                            <div class="col-md-10 padding0 displayinlineblock">
                                                <asp:TextBox ID="txtBranchCheque" runat="server" CssClass="textbox"></asp:TextBox>
                                            </div>
                                            <div class="col-md-2 padding0 displayinlineblock">
                                                <asp:ImageButton ID="imgbtnBranchCheque" runat="server" ImageUrl="~/Images/icon_search.png"
                                                    ImageAlign="Middle" CssClass="imageicon" OnClick="imgbtnBranchCheque_Click" />
                                            </div>
                                        </div>
                                        <div class="col-md-2 padding2">
                                            Cheque Date
                                        </div>
                                        <div class="col-md-4 padding0">
                                            <div class="col-md-10 padding0 displayinlineblock">
                                                <asp:TextBox ID="txtChequeDate" runat="server" CssClass="textbox" onkeypress="return RestrictSpace()"
                                                    Enabled="false"></asp:TextBox>
                                                <asp:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtChequeDate"
                                                    PopupButtonID="imgbtnChequeDate" Format="dd-MMM-yyyy">
                                                </asp:CalendarExtender>
                                            </div>
                                            <div class="col-md-2 padding0 displayinlineblock">
                                                <asp:ImageButton ID="imgbtnChequeDate" runat="server" ImageUrl="~/Images/calendar.png"
                                                    ImageAlign="Middle" CssClass="imageicon" />
                                            </div>
                                        </div>
                                        <div class="col-md-6 padding2 hight1">
                                        </div>
                                        <div class="col-md-2 padding2 ">
                                            Deposit Bank
                                        </div>
                                        <div class="col-md-4 padding2">
                                            <div class="col-md-10 padding0 displayinlineblock">
                                                <asp:TextBox ID="txtDepositBankCheque" runat="server" CssClass="textbox"></asp:TextBox>
                                            </div>
                                            <div class="col-md-2 padding0 displayinlineblock">
                                                <asp:ImageButton ID="imgbtnDepositBankCheque" runat="server" ImageUrl="~/Images/icon_search.png"
                                                    ImageAlign="Middle" CssClass="imageicon" OnClick="imgbtnDepositBankCheque_Click" />
                                            </div>
                                        </div>
                                        <div class="col-md-2 padding2 textaling">
                                            Branch
                                        </div>
                                        <div class="col-md-4 padding0">
                                            <asp:TextBox ID="txtDepositBranchCheque" runat="server" CssClass="textbox"></asp:TextBox>
                                        </div>
                                    </asp:View>
                                </asp:MultiView>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
