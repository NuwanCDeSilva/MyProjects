<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="ReceiptEnrty.aspx.cs" Inherits="FF.AbansTours.ReceiptEnrty" %>

<%--<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>--%>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="upReceiptEntry" runat="server" ChildrenAsTriggers="true" UpdateMode="Always">
        <ContentTemplate>
            <div style="height: 22px; width: 100%; text-align: right; background-color: Silver;">
                <asp:Button ID="btnSave" Text="Save" runat="server" Width="80px" Enabled="true" OnClick="btnSave_Click"
                    ValidationGroup="Save" />
                <asp:Button ID="btnClear" Text="Clear" runat="server" Width="80px" OnClick="btnClear_Click" />
                <asp:ConfirmButtonExtender ID="CbeSave" runat="server" TargetControlID="btnSave"
                    ConfirmText="Do you want to save?" ConfirmOnFormSubmit="false">
                </asp:ConfirmButtonExtender>
                <asp:ConfirmButtonExtender ID="CbeClear" runat="server" TargetControlID="btnClear"
                    ConfirmText="Do you want to clear?" ConfirmOnFormSubmit="false">
                </asp:ConfirmButtonExtender>
                <asp:Button ID="btnCancel" Text="Cancel" runat="server" Width="80px" OnClick="btnCancel_Click" />
                <asp:ConfirmButtonExtender ID="CbeCancel" runat="server" TargetControlID="btnCancel"
                    ConfirmText="Do you want to Cancel?" ConfirmOnFormSubmit="false">
                </asp:ConfirmButtonExtender>
                <asp:Button ID="btnPrint" Text="Print" runat="server" Width="80px" OnClick="btnPrint_Click" />
            </div>
            <div class="col-md-12">
                &nbsp;
            </div>
            <div>
                <div class="row rowmargin0 col-md-12">
                    <div class="panel panel-default">
                        <div class="panel-heading">
                        </div>
                        <div class="panel-body">
                            <div class="col-md-1 textaling padding1">
                                Receipt Type
                            </div>
                            <div class="col-md-1 padding0 ">
                                <div class="col-md-10 padding0 displayinlineblock">
                                    <asp:TextBox ID="txtReceiptType" runat="server" CssClass="textbox" AutoPostBack="true"
                                        ValidationGroup="Save" OnTextChanged="txtReceiptType_TextChanged"></asp:TextBox>
                                </div>
                                <div class="col-md-2 padding0 displayinlineblock">
                                    <asp:ImageButton ID="ImgbtnReceiptType" runat="server" ImageUrl="~/Images/icon_search.png"
                                        ImageAlign="Middle" CssClass="imageicon" OnClick="ImgbtnReceiptType_Click" />
                                </div>
                            </div>
                            <div class="col-md-1  textaling padding1">
                                Date
                            </div>
                            <div class="col-md-2 padding0">
                                <div class="col-md-8 padding0 displayinlineblock">
                                    <asp:TextBox ID="txtDate" runat="server" CssClass="textbox" onkeypress="return RestrictSpace()"
                                        Enabled="false"></asp:TextBox>
                                    <%--<asp:CalendarExtender ID="CeDate" runat="server" TargetControlID="txtDate" Format="dd-MMM-yyyy"
                                        PopupButtonID="imgbtnDate">
                                    </asp:CalendarExtender>--%>
                                </div>
                                <div class="col-md-4 padding0 displayinlineblock">
                                    <asp:ImageButton ID="imgbtnDate" runat="server" ImageUrl="~/Images/calendar.png"
                                        ImageAlign="Middle" CssClass="imageicon" Enabled="false" />
                                </div>
                            </div>
                            <div class="col-md-1  textaling padding1">
                                Receipt No
                            </div>
                            <div class="col-md-2 padding0 ">
                                <div class="col-md-8 padding0 displayinlineblock">
                                    <asp:TextBox ID="txtReceiptNo" runat="server" CssClass="textbox" OnTextChanged="txtReceiptNo_TextChanged"
                                        AutoPostBack="true"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Receipt No is required."
                                        Display="None" CssClass="Validators" ControlToValidate="txtReceiptNo" ValidationGroup="ReceiptNo"></asp:RequiredFieldValidator>
                                    <asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server" TargetControlID="RequiredFieldValidator1">
                                    </asp:ValidatorCalloutExtender>
                                </div>
                                <div class="col-md-2 padding0 displayinlineblock">
                                    <asp:ImageButton ID="ImgReceiptNo" runat="server" ImageUrl="~/Images/icon_search.png"
                                        ImageAlign="Middle" CssClass="imageicon" OnClick="ImgReceiptNo_Click" />
                                </div>
                                <div class="col-md-2 padding0 displayinlineblock">
                                    <asp:ImageButton ID="ImgReceiptNoserch" runat="server" ImageUrl="~/Images/LoadDetails.png"
                                        ValidationGroup="ReceiptNo" ImageAlign="Middle" CssClass="imageicon" OnClick="ImgReceiptNoserch_Click" />
                                </div>
                            </div>
                            <div class="col-md-4 paddingright0">
                                <div class="col-md-3  textaling padding1">
                                    Is Manual
                                </div>
                                <div class="col-md-1 padding0">
                                    <asp:CheckBox ID="chkIsManual" runat="server" OnCheckedChanged="chkIsManual_CheckedChanged"
                                        Enabled="true" AutoPostBack="True" />
                                </div>
                                <div class="col-md-5  textaling padding1">
                                    Manual Referance
                                </div>
                                <div class="col-md-3 padding0">
                                    <asp:TextBox ID="txtManualReferance" runat="server" CssClass="textbox" OnTextChanged="txtManualReferance_TextChanged"
                                        AutoPostBack="true"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="row rowmargin0">
                    <div class="col-md-6">
                        <div class="panel panel-default">
                            <div class="panel-heading pannelheading">
                                Receive From
                            </div>
                            <div class="panel-body">
                                <div class="col-md-2 padding2">
                                    Code
                                </div>
                                <div class="col-md-10 padding2">
                                    <div class="col-md-10 padding0 displayinlineblock">
                                        <asp:TextBox ID="txtCode" runat="server" CssClass="textbox" OnTextChanged="txtCode_TextChanged"
                                            AutoPostBack="true" MaxLength="30"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Code No is required."
                                            Display="None" CssClass="Validators" ControlToValidate="txtCode" ValidationGroup="Code"></asp:RequiredFieldValidator>
                                        <asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender4" runat="server" TargetControlID="RequiredFieldValidator3">
                                        </asp:ValidatorCalloutExtender>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="Code No is required."
                                            Display="None" CssClass="Validators" ControlToValidate="txtCode" ValidationGroup="Save"></asp:RequiredFieldValidator>
                                        <asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender5" runat="server" TargetControlID="RequiredFieldValidator4">
                                        </asp:ValidatorCalloutExtender>
                                    </div>
                                    <div class="col-md-2 padding0 displayinlineblock">
                                        <asp:ImageButton ID="imgbtnCode" runat="server" ImageUrl="~/Images/icon_search.png"
                                            ImageAlign="Middle" CssClass="imageicon" OnClick="imgbtnCode_Click" />
                                        <asp:ImageButton ID="imgbtncodeseach" runat="server" ImageUrl="~/Images/LoadDetails.png"
                                            ImageAlign="Middle" CssClass="imageicon" OnClick="imgbtncodeseach_Click" ValidationGroup="Code" />
                                    </div>
                                </div>
                                <div class="col-md-2 padding2">
                                    Name
                                </div>
                                <div class="col-md-10 padding2">
                                    <asp:TextBox ID="txtName" runat="server" CssClass="textbox" MaxLength="200"></asp:TextBox>
                                </div>
                                <div class="col-md-2 padding2">
                                    Address
                                </div>
                                <div class="col-md-10 padding2">
                                    <asp:TextBox ID="txtAddress" runat="server" CssClass="textbox" MaxLength="200"></asp:TextBox>
                                </div>
                                <div class="col-md-2 padding2">
                                </div>
                                <div class="col-md-10 padding2">
                                    <asp:TextBox ID="txtAddress2" runat="server" CssClass="textbox" MaxLength="200"></asp:TextBox>
                                </div>
                                <div class="col-md-2 padding2">
                                    NIC
                                </div>
                                <div class="col-md-4 padding2">
                                    <asp:TextBox ID="txtNIC" runat="server" CssClass="textbox" MaxLength="10"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtNIC"
                                        ErrorMessage="NIC Number is required." Display="None" ValidationGroup="Save"></asp:RequiredFieldValidator>
                                    <asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender7" runat="server" TargetControlID="RequiredFieldValidator5">
                                    </asp:ValidatorCalloutExtender>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ErrorMessage="Enter valid NIC Number."
                                        ControlToValidate="txtNIC" Display="None" ValidationExpression="[0-9]{9}[v|x|V|X]{1}"
                                        ValidationGroup="Save"></asp:RegularExpressionValidator>
                                    <asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender8" runat="server" TargetControlID="RegularExpressionValidator3">
                                    </asp:ValidatorCalloutExtender>
                                </div>
                                <div class="col-md-2 padding2 textaling">
                                    Mobile
                                </div>
                                <div class="col-md-4 padding2">
                                    <asp:TextBox ID="txtMobile" runat="server" CssClass="textbox" MaxLength="15"></asp:TextBox>
                                    <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtMobile"
                                        ErrorMessage="Mobile Number is required." Display="None"></asp:RequiredFieldValidator>
                                    <asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender5" runat="server" TargetControlID="RequiredFieldValidator4">
                                    </asp:ValidatorCalloutExtender>--%>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ErrorMessage="Enter valid Mobile Number."
                                        ControlToValidate="txtMobile" Display="None" ValidationExpression="([+]{0,1}[0-9]{0,11})"
                                        ValidationGroup="Save"></asp:RegularExpressionValidator>
                                    <asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender6" runat="server" TargetControlID="RegularExpressionValidator2">
                                    </asp:ValidatorCalloutExtender>
                                </div>
                                <div class="col-md-2 padding2">
                                    District
                                </div>
                                <div class="col-md-4 padding2">
                                    <asp:DropDownList ID="ddlDistrict" runat="server" CssClass="textbox ddlhight1" AutoPostBack="true"
                                        OnTextChanged="ddlDistrict_TextChanged">
                                    </asp:DropDownList>
                                </div>
                                <div class="col-md-2 padding2 textaling">
                                    Province
                                </div>
                                <div class="col-md-4 padding2">
                                    <asp:TextBox ID="txtProvince" runat="server" CssClass="textbox"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-6 paddingleft0">
                        <div class="panel panel-default">
                            <div class="panel-heading pannelheading">
                                Settlement Details
                            </div>
                            <div class="panel-body">
                                <div class="col-md-3 padding2">
                                    Invoice
                                </div>
                                <div class="col-md-3 padding0">
                                    <div class="col-md-8 padding0">
                                        <asp:TextBox ID="txtInvoice" runat="server" CssClass="textbox"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ErrorMessage="Invoice no is required."
                                            Display="None" CssClass="Validators" ControlToValidate="txtInvoice" ValidationGroup="Invoice"></asp:RequiredFieldValidator>
                                        <asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender10" runat="server" TargetControlID="RequiredFieldValidator7">
                                        </asp:ValidatorCalloutExtender>
                                    </div>
                                    <div class="col-md-2 padding0">
                                        <asp:ImageButton ID="imgbtnInvoice" runat="server" ImageUrl="~/Images/icon_search.png"
                                            ImageAlign="Middle" CssClass="imageicon" OnClick="imgbtnInvoice_Click" />
                                    </div>
                                    <div class="col-md-2 padding0">
                                        <asp:ImageButton ID="imgbtnInvoicedetals" runat="server" ImageUrl="~/Images/LoadDetails.png"
                                            ImageAlign="Middle" CssClass="imageicon2" OnClick="imgbtnInvoicedetals_Click"
                                            ValidationGroup="Invoice" />
                                    </div>
                                </div>
                                <div class="col-md-3 padding2 textaling">
                                    Invoice Amount
                                </div>
                                <div class="col-md-3 padding2">
                                    <asp:TextBox ID="txtInvoiceAmount" runat="server" CssClass="textbox" Enabled="false"
                                        Text="0.00"></asp:TextBox>
                                </div>
                                <div class="col-md-3 padding2 left">
                                    Customer Payment
                                </div>
                                <div class="col-md-3 padding2">
                                    <asp:TextBox ID="txtCustomerPayment" runat="server" CssClass="textbox"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtCustomerPayment"
                                        ErrorMessage="Customer Payment is required." ValidationGroup="CustomerPayment"
                                        Display="None"></asp:RequiredFieldValidator>
                                    <asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender2" runat="server" TargetControlID="RequiredFieldValidator2">
                                    </asp:ValidatorCalloutExtender>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="Enter valid payment amount."
                                        ValidationGroup="CustomerPayment" ControlToValidate="txtCustomerPayment" Display="None"
                                        ValidationExpression="([0-9]{1,8}[.]{0,1}[0-9]{0,2})"></asp:RegularExpressionValidator>
                                    <asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender3" runat="server" TargetControlID="RegularExpressionValidator1">
                                    </asp:ValidatorCalloutExtender>
                                </div>
                                <div class="col-md-3  padding2">
                                    <asp:Button ID="btnCustomerPaymentAdd" runat="server" Text="Add" ValidationGroup="CustomerPayment"
                                        OnClick="btnCustomerPaymentAdd_Click" />
                                </div>
                                <div class="col-md-3 padding2 hight2">
                                    &nbsp;
                                </div>
                                <div class="col-md-3 padding2">
                                    Paid Amount
                                </div>
                                <div class="col-md-3 padding2">
                                    <asp:TextBox ID="txtPaidAmount" runat="server" CssClass="textbox"></asp:TextBox>
                                </div>
                                <div class="col-md-3 padding2 textaling">
                                    Balance Amount
                                </div>
                                <div class="col-md-3 padding2">
                                    <asp:TextBox ID="txtBalanceAmount" runat="server" CssClass="textbox"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="row rowmargin0 col-md-12">
                    <div class="panel panel-default">
                        <div class="panel-heading pannelheading">
                            Payment Details
                        </div>
                        <div class="panel-body">
                            <div class="col-md-6 paddingleft0">
                                <div class="col-md-6 padding0">
                                    <div class="col-md-4  padding2">
                                        Pay Mode
                                    </div>
                                    <div class="col-md-8  padding2">
                                        <asp:DropDownList ID="ddlPayMode" runat="server" CssClass="textbox ddlhight1" AutoPostBack="True"
                                            OnTextChanged="ddlPayMode_TextChanged">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="col-md-4  padding0 textaling">
                                        Amount
                                    </div>
                                    <div class="col-md-8 padding2">
                                        <div class="col-md-10 padding0 displayinlineblock">
                                            <asp:TextBox ID="txtAmount" runat="server" CssClass="textbox"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ErrorMessage="Amount is required."
                                                Display="None" CssClass="Validators" ControlToValidate="txtAmount" ValidationGroup="Amount"></asp:RequiredFieldValidator>
                                            <asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender9" runat="server" TargetControlID="RequiredFieldValidator6">
                                            </asp:ValidatorCalloutExtender>
                                        </div>
                                        <div class="col-md-2 padding0 displayinlineblock">
                                            <asp:ImageButton ID="ImgAmount" runat="server" ImageUrl="~/Images/dwnarrowgridicon.png"
                                                ImageAlign="Middle" CssClass="imageicon" OnClick="ImgAmount_Click" ValidationGroup="Amount" />
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-12 padding0">
                                    <div class="col-md-2 padding2">
                                        Remark
                                    </div>
                                    <div class="col-md-10 padding2">
                                        <asp:TextBox ID="txtRemark" runat="server" CssClass="textbox form-control" Rows="3"
                                            TextMode="MultiLine"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-12  padding2 displayinlineblock">
                                    <asp:GridView ID="grdPaymentDetails" runat="server" AutoGenerateColumns="false" CssClass="table table-bordered"
                                        OnRowCommand="grdPaymentDetails_RowCommand">
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
                                            <asp:TextBox ID="txtCardNo" runat="server" CssClass="textbox"></asp:TextBox>
                                        </div>
                                        <div class="col-md-2 padding2">
                                            Bank
                                        </div>
                                        <div class="col-md-4 padding2">
                                            <div class="col-md-9 padding0 displayinlineblock">
                                                <asp:TextBox ID="txtBankCard" runat="server" CssClass="textbox" OnTextChanged="txtBankCard_TextChanged"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ErrorMessage="Bank is required."
                                                    Display="None" CssClass="Validators" ControlToValidate="txtBankCard" ValidationGroup="BankCard"></asp:RequiredFieldValidator>
                                                <asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender12" runat="server" TargetControlID="RequiredFieldValidator9">
                                                </asp:ValidatorCalloutExtender>
                                            </div>
                                            <div class="col-md-2 padding0 displayinlineblock">
                                                <asp:ImageButton ID="imgbtntxtBankCard" runat="server" ImageUrl="~/Images/icon_search.png"
                                                    ImageAlign="Middle" CssClass="imageicon" OnClick="imgbtntxtBankCard_Click" />
                                            </div>
                                            <div class="col-md-1 padding0 displayinlineblock">
                                                <asp:ImageButton ID="imgbtnbankcard" runat="server" ImageUrl="~/Images/LoadDetails.png"
                                                    ImageAlign="Middle" CssClass="imageicon" ValidationGroup="BankCard" OnClick="imgbtnbankcard_Click" />
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
                                                    ImageAlign="Middle" CssClass="imageicon" OnClick="imgbtnDepositBranchCard_Click" />
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
                                                    OnTextChanged="txtPeriod_TextChanged"></asp:TextBox>
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
                <div class="row rowmargin0 col-md-12">
                    <div class="panel panel-default">
                        <div class="panel-heading pannelheading">
                            Additional Details
                        </div>
                        <div class="panel-body">
                            <div class="col-md-7">
                                <div class="col-md-1 ">
                                    Note
                                </div>
                                <div class="col-md-11 padding2">
                                    <asp:TextBox ID="txtNote" runat="server" CssClass="textbox form-control" Rows="3"
                                        TextMode="MultiLine"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-5">
                                <div class="col-md-4 padding2 textaling">
                                    Sales Executive
                                </div>
                                <div class="col-md-8 padding2">
                                    <div class="col-md-11 padding0 displayinlineblock">
                                        <asp:TextBox ID="txtSalesExecutive" runat="server" CssClass="textbox"></asp:TextBox>
                                    </div>
                                    <div class="col-md-1 padding0 displayinlineblock">
                                        <asp:ImageButton ID="imgbtnSalesExecutive" runat="server" ImageUrl="~/Images/icon_search.png"
                                            ImageAlign="Middle" CssClass="imageicon" OnClick="imgbtnSalesExecutive_Click" />
                                    </div>
                                </div>
                                <div class="col-md-4 padding2 textaling">
                                    Total Receipt Amount
                                </div>
                                <div class="col-md-8 padding2">
                                    <asp:TextBox ID="txtTotalReceiptAmount" runat="server" CssClass="textbox"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <%--<div class="col-md-12">
                    <asp:TextBox ID="TextBox1" runat="server" CssClass="textbox"></asp:TextBox>
                    <cr:crystalreportviewer id="crvReceiptPrint" runat="server" displaytoolbar="False"
                        displaystatusbar="false" />
                </div>--%>
            </div>
            <div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnPrint" />
        </Triggers>
    </asp:UpdatePanel>
    <div>
        <asp:Panel ID="pnlReceiptPrint" runat="server" Width="920px" Height="500px" CssClass="ModalPopup">
            <div style="text-align: right; background-color: Silver;">
                <asp:UpdatePanel ID="upPrint" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                    <ContentTemplate>
                        <asp:Button ID="Close" Text="Close" Width="80px" runat="server" OnClick="Close_Click" />
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
            <div>
                <iframe style="width: 920px; height: 500px;" id="irm1" src="Reports_Module/Financial_Rep/ReceiptPrint.aspx" runat="server"></iframe>
            </div>
        </asp:Panel>
        <asp:Button ID="btnMDprint" runat="server" Text="D3" Style="display: none" />
        <asp:ModalPopupExtender ID="mpReceiptPrint" runat="server" DynamicServicePath=""
            Enabled="True" PopupControlID="pnlReceiptPrint" TargetControlID="btnMDprint"
            BackgroundCssClass="modalBackground" PopupDragHandleControlID="pnlReceiptPrint">
        </asp:ModalPopupExtender>
    </div>
</asp:Content>
