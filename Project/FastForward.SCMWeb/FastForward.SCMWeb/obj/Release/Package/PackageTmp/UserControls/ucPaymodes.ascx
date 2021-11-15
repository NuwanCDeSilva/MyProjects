<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucPaymodes.ascx.cs" Inherits="FastForward.SCMWeb.UserControls.ucPaymodes" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<script src="<%=Request.ApplicationPath%>Js/jquery.min.js"></script>
<script src="<%=Request.ApplicationPath%>Js/bootstrap.min.js"></script>
<link href="<%=Request.ApplicationPath%>Css/bootstrap.css" rel="stylesheet" />

<link href="<%=Request.ApplicationPath%>Css/style.css" rel="stylesheet" />

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
    }

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
    }

    function ConfirmItemDelete() {
        var selectedvalueOrdPlace = confirm("Do you want to delete ?");
        if (selectedvalueOrdPlace) {
            document.getElementById('<%=hdnItemDelete.ClientID %>').value = "Yes";
        } else {
            document.getElementById('<%=hdnItemDelete.ClientID %>').value = "No";
        }
    };

    function Enable() {
        return;
    };


</script>

<style type="text/css">
    .DatePanel {
        position: absolute;
        background-color: #FFFFFF;
        border: 1px solid #646464;
        color: #000000;
        z-index: 1;
        font-family: tahoma,verdana,helvetica;
        font-size: 11px;
        padding: 4px;
        text-align: center;
        cursor: default;
        line-height: 20px;
    }

    .divWaiting {
        position: absolute;
        background-color: #FAFAFA;
        opacity: 0.8;
        overflow: hidden;
        text-align: center;
        top: 0;
        left: 0;
        height: 100%;
        width: 100%;
        padding-top: 20%;
    }

    .setposition {
        position: fixed !important;
    }
</style>

<div class="row">

    <asp:UpdateProgress ID="UpdateProgress3" DisplayAfter="10"
        runat="server" AssociatedUpdatePanelID="upMain">
        <ProgressTemplate>
            <div class="divWaiting">
                <asp:Label ID="lblWait3" runat="server" Text="Please wait... " />
                <asp:Image ID="imgWait3" runat="server" ImageAlign="Middle" ImageUrl="~/images/icons/ajax-loader.gif" />
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>

    <asp:UpdatePanel ID="upMain" runat="server">
        <ContentTemplate>
            <asp:HiddenField ID="hdnCustomerCode" runat="server" />
            <asp:HiddenField ID="hdnItemDelete" runat="server" />
            <asp:Label ID="lblTransDate" runat="server"></asp:Label>
            <div class="col-sm-12">
                <div class="panel panel-default">
                    <div class="panel-heading height16 padding0 ">Payment details</div>
                    <div class="panel-body">
                        <div class="row">
                            <div class="col-sm-12  buttonrow">
                                <div id="divWarning" visible="false" runat="server" class="alert alert-danger alert-dismissible" role="alert">
                                    <strong>Warning!</strong>
                                    <asp:Label ID="lblWarning" runat="server"></asp:Label>
                                    <asp:LinkButton ID="lbtnlWarning" runat="server" CausesValidation="false" CssClass="floatRight">
                                        <span class="glyphicon glyphicon-ok" aria-hidden="true"></span>
                                    </asp:LinkButton>
                                </div>
                                <div id="divSuccess" runat="server" visible="false" class="alert alert-success alert-dismissible" role="alert">
                                    <strong>Success!</strong>
                                    <asp:Label ID="lblSuccess" runat="server"></asp:Label>
                                    <asp:LinkButton ID="lbtnSuccess" runat="server" CausesValidation="false" CssClass="floatRight">
                                        <span class="glyphicon glyphicon-ok" aria-hidden="true"></span>
                                    </asp:LinkButton>
                                </div>
                                <div id="divAlert" runat="server" visible="false" class="alert alert-info alert-dismissible" role="alert">
                                    <strong>Alert!</strong>
                                    <asp:Label ID="lblAlert" runat="server"></asp:Label>
                                    <asp:LinkButton ID="lbtnAlert" runat="server" CausesValidation="false" CssClass="floatRight">
                                <span class="glyphicon glyphicon-ok" aria-hidden="true"></span>
                                    </asp:LinkButton>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-12">
                                <div class="col-sm-5 paddingLeft0">
                                    <div class="panel panel-default">
                                        <div class="panel-body height243"><%--190--%>
                                            <div class="row">
                                                <div class="col-sm-6">
                                                    <div class="row">
                                                        <div class="col-sm-4 labelText1">
                                                            Pay Mode
                                                        </div>
                                                        <div class="col-sm-8 paddingLeft0">
                                                            <asp:DropDownList ID="ddlPayMode" runat="server" class="form-control" AutoPostBack="true" OnTextChanged="ddlPayMode_TextChanged">
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-sm-6">
                                                    <div class="row">
                                                        <div class="col-sm-3 labelText1">
                                                            Amount
                                                        </div>
                                                        <div class="col-sm-7 paddingLeft0 paddingRight0">
                                                            <asp:TextBox ID="txtAmount" runat="server" onkeydown="return jsDecimals(event);" MaxLength="10" class="form-control textAlignRight" AutoPostBack="true" OnTextChanged="txtAmount_TextChanged"></asp:TextBox>
                                                        </div>
                                                        <div class="col-sm-2 paddingLeft5">
                                                            <asp:LinkButton ID="lbtnAdd" runat="server" OnClick="lbtnAdd_Click">
                                                                <span class="glyphicon glyphicon-plus" aria-hidden="true"></span>
                                                            </asp:LinkButton>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-12 height5">
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-2 labelText1 paddingRight0">
                                                    <%--Remark--%>
                                                </div>
                                                <div class="col-sm-10 paddingLeft0">
                                                    <asp:TextBox ID="txtRemark" runat="server" class="form-control" Rows="3" TextMode="MultiLine" Visible="false"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-12 height5">
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-12">
                                                    <asp:GridView ID="grdPayments" CssClass="table table-hover table-striped" runat="server" GridLines="None" ShowHeaderWhenEmpty="True"
                                                        EmptyDataText="No data found..." AutoGenerateColumns="False">
                                                        <Columns>
                                                            <asp:BoundField HeaderText='Pay Type' DataField="SARD_PAY_TP" />
                                                            <asp:BoundField HeaderText='Bank' DataField="sard_chq_bank_cd" />
                                                             <asp:BoundField HeaderText='Bank Code' DataField="Sard_deposit_bank_cd" />
                                                             <asp:BoundField HeaderText='Invoice' DataField="SARD_INV_NO" />
                                                            <asp:BoundField HeaderText='Branch' DataField="sard_chq_branch" />
                                                            <asp:BoundField HeaderText='Ref No' DataField="Sard_ref_no" />
                                                            <asp:BoundField HeaderText='CC Type' DataField="sard_cc_tp" />
                                                            <asp:BoundField HeaderText='Bank Charge' DataField="Sard_anal_3" />
                                                            <asp:BoundField HeaderText='Amount' DataField="sard_settle_amt" DataFormatString="{0:N2}" />
                                                            <asp:TemplateField>
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="lbtnPayModeDalete" runat="server" CausesValidation="false" OnClick="lbtnPayModeDalete_Click" OnClientClick="ConfirmItemDelete()">
                                                                <span aria-hidden="true" class="glyphicon glyphicon-trash"></span>
                                                                    </asp:LinkButton>
                                                                </ItemTemplate>
                                                                <ItemStyle Width="5%" />
                                                                <HeaderStyle CssClass="gridHeaderAlignRight" />
                                                                <ItemStyle HorizontalAlign="Right" />
                                                            </asp:TemplateField>
                                                        </Columns>
                                                    </asp:GridView>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-sm-5 paddingLeft0">
                                    <div class="panel panel-default">
                                        <div class="panel-body height190">
                                            <asp:Panel ID="pnlDef" runat="server" Visible="false">
                                                <div class="col-sm-12">
                                                </div>
                                            </asp:Panel>
                                            <asp:Panel ID="pnlCash" runat="server" Visible="false">
                                                <div class="row">
                                                    <div class="col-sm-6">
                                                        <div class="row">
                                                            <div class="col-sm-4 labelText1">
                                                                  Deposit Bank
                                                            </div>
                                                            <div class="col-sm-6 paddingRight0 paddingLeft0">
                                                                <asp:TextBox ID="txtDepositBankCash" runat="server" class="form-control" MaxLength="30" AutoPostBack="true"  OnTextChanged="txtDepositBankCash_TextChanged"></asp:TextBox>
                                                            </div>
                                                            <div class="col-sm-2 paddingLeft5">
                                                                <asp:LinkButton ID="lbtnDepositBankCash" runat="server"  OnClick="lbtnDepositBankCash_Click">
                                                                    <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                                </asp:LinkButton>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="col-sm-6">
                                                        <div class="row">
                                                            <div class="col-sm-3 labelText1">
                                                                 Branch
                                                            </div>
                                                            <div class="col-sm-9 paddingLeft0">
                                                                <asp:TextBox ID="txtBranchCash" runat="server"  class="form-control" MaxLength="30"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </asp:Panel>
                                            <asp:Panel ID="PnlAdvan" runat="server" Visible="false">
                                                <div class="row">
                                                    <div class="col-sm-2 labelText1">
                                                        Ref No
                                                    </div>
                                                    <div class="col-sm-9 paddingRight0 paddingLeft0">
                                                        <asp:TextBox ID="txtRefNoAdvan" runat="server" MaxLength="50" class="form-control" AutoPostBack="true" OnTextChanged="txtRefNoAdvan_TextChanged"></asp:TextBox>
                                                    </div>
                                                    <div class="col-sm-1 paddingLeft5">
                                                        <asp:LinkButton ID="lbtnRefNoAdvan" runat="server" OnClick="lbtnRefNoAdvan_Click">
                                                            <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                        </asp:LinkButton>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-sm-12 height5">
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-sm-2 labelText1">
                                                        Ref Amount
                                                    </div>
                                                    <div class="col-sm-4 paddingRight0 paddingLeft0">
                                                        <asp:TextBox ID="txtRefAmountAdvan" runat="server" MaxLength="10" class="form-control textAlignRight"></asp:TextBox>
                                                    </div>
                                                    <div class="col-sm-6">
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-sm-12 height5">
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-sm-6">
                                                        <div class="row">
                                                            <div class="col-sm-4 labelText1">
                                                                <%--Deposit Bank--%>
                                                            </div>
                                                            <div class="col-sm-7 paddingRight0 paddingLeft0">
                                                                <asp:TextBox ID="txtDepositBankAdvan" runat="server" class="form-control" MaxLength="30" AutoPostBack="true" OnTextChanged="txtDepositBankAdvan_TextChanged" Visible="false"></asp:TextBox>
                                                            </div>
                                                            <div class="col-sm-1 paddingLeft5">
                                                                <asp:LinkButton ID="lbtnDepositBankAdvan" runat="server" OnClick="lbtnDepositBankAdvan_Click" Visible="false">
                                                                    <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                                </asp:LinkButton>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="col-sm-6">
                                                        <div class="row">
                                                            <div class="col-sm-3 labelText1">
                                                                <%--  Branch--%>
                                                            </div>
                                                            <div class="col-sm-9 paddingLeft0">
                                                                <asp:TextBox ID="txtBranchAdvan" runat="server" MaxLength="30" class="form-control" Visible="false"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-sm-12 height5">
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-sm-2 labelText1">
                                                        <%-- SCM--%>
                                                    </div>
                                                    <div class="col-sm-8 paddingLeft0">
                                                        <asp:CheckBox ID="chkSCMAdvan" runat="server" Visible="false" />
                                                    </div>
                                                </div>
                                            </asp:Panel>
                                            <asp:Panel ID="pnlCrcd" runat="server" Visible="false">
                                                <div class="row">
                                                    <div class="col-sm-2 labelText1">
                                                        Card No
                                                    </div>
                                                    <div class="col-sm-10 paddingLeft0">
                                                        <asp:TextBox ID="txtCardNoCrcd" runat="server" MaxLength="30" class="form-control"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-sm-12 height5">
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-sm-6">
                                                        <div class="row">
                                                            <div class="col-sm-4 labelText1">
                                                                Bank
                                                            </div>
                                                            <div class="col-sm-7 paddingRight0 paddingLeft0">
                                                                <asp:TextBox ID="txtBankCrcd" runat="server" MaxLength="30" class="form-control" AutoPostBack="true" OnTextChanged="txtBankCrcd_TextChanged"></asp:TextBox>
                                                            </div>
                                                            <div class="col-sm-1 paddingLeft5">
                                                                <asp:LinkButton ID="lbtnBankCrcd" runat="server" OnClick="lbtnBankCrcd_Click">
                                                                    <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                                </asp:LinkButton>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="col-sm-6">
                                                        <div class="row">
                                                            <div class="col-sm-3 labelText1 paddingRight0">
                                                                Batch
                                                            </div>
                                                            <div class="col-sm-9 paddingLeft0">
                                                                <asp:TextBox ID="txtBatchCrcd" runat="server" MaxLength="30" class="form-control" AutoPostBack="true" OnTextChanged="txtBatchCrcd_TextChanged"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-sm-12 height5">
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-sm-6">
                                                        <div class="row">
                                                            <div class="col-sm-12 labelText1">
                                                                <asp:Label ID="lblBankNameCrcd" runat="server"></asp:Label>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="col-sm-6">
                                                        <div class="row">
                                                            <div class="col-sm-4 paddingLeft0">
                                                            </div>
                                                            <div class="col-sm-4 paddingLeft0">
                                                                <asp:RadioButton ID="rbtnofflineCrcd" runat="server" Text="offline" AutoPostBack="true" OnCheckedChanged="rbtnofflineCrcd_CheckedChanged" />
                                                            </div>
                                                            <div class="col-sm-4 paddingLeft0">
                                                                <asp:RadioButton ID="rbtnOnlineCrcd" runat="server" Text="Online" AutoPostBack="true" OnCheckedChanged="rbtnOnlineCrcd_CheckedChanged" />
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-sm-12 height5">
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-sm-6">
                                                        <div class="row">
                                                            <div class="col-sm-4 labelText1">
                                                                Card Type
                                                            </div>
                                                            <div class="col-sm-8 paddingLeft0 paddingRight0">
                                                                <asp:DropDownList ID="ddlCardTypeCrcd" runat="server" class="form-control">
                                                                </asp:DropDownList>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="col-sm-6">
                                                        <div class="row">
                                                            <div class="col-sm-3 labelText1">
                                                                Expire
                                                            </div>
                                                            <div class="col-sm-7 paddingRight0 paddingLeft0">
                                                                <asp:TextBox ID="txtExpireCrcd" runat="server" class="form-control" AutoPostBack="true" OnTextChanged="txtExpireCrcd_TextChanged"></asp:TextBox>
                                                            </div>
                                                            <div class="col-sm-2 paddingLeft5">
                                                                <asp:LinkButton ID="lbtnExpireCrcd" runat="server" CausesValidation="false">
                                                                    <span class="glyphicon glyphicon-calendar" aria-hidden="true"></span>
                                                                </asp:LinkButton>
                                                                <asp:CalendarExtender ID="CalendarExtender3" runat="server" TargetControlID="txtExpireCrcd"
                                                                    PopupButtonID="lbtnExpireCrcd" Format="dd/MMM/yyyy">
                                                                </asp:CalendarExtender>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-sm-12 height5">
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-sm-6">
                                                        <div class="row">
                                                            <div class="col-sm-4 labelText1">
                                                                Deposit Bank
                                                            </div>
                                                            <div class="col-sm-7 paddingRight0 paddingLeft0">
                                                                <asp:TextBox ID="txtDepositBankCrcd" runat="server" MaxLength="30" class="form-control" AutoPostBack="true" OnTextChanged="txtDepositBankCrcd_TextChanged"></asp:TextBox>
                                                            </div>
                                                            <div class="col-sm-1 paddingLeft5">
                                                                <asp:LinkButton ID="lbtnDepositBankCrcd" runat="server" OnClick="lbtnDepositBankCrcd_Click">
                                                                    <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                                </asp:LinkButton>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="col-sm-6">
                                                        <div class="row">
                                                            <div class="col-sm-3 labelText1">
                                                                Branch
                                                            </div>
                                                            <div class="col-sm-9 paddingLeft0">
                                                                <asp:TextBox ID="txtBranchCrcd" runat="server" MaxLength="30" class="form-control" AutoPostBack="true" OnTextChanged="txtBranchCrcd_TextChanged"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-sm-12 height5">
                                                    </div>
                                                </div>
                                                <asp:Panel ID="pnlPromotionCrcd1" runat="server">
                                                    <div class="row">
                                                        <div class="col-sm-6">
                                                            <div class="row">
                                                                <div class="col-sm-4 labelText1">
                                                                    Promotion
                                                                </div>
                                                                <div class="col-sm-2 paddingLeft0">
                                                                    <asp:CheckBox ID="chkPromotionCrcd" runat="server" AutoPostBack="true" OnCheckedChanged="chkPromotionCrcd_CheckedChanged" />
                                                                </div>
                                                                <div class="col-sm-6 paddingLeft0">
                                                                    <asp:Label ID="lblPromotion" runat="server"></asp:Label>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="col-sm-6">
                                                            <div class="row">
                                                                <div class="col-sm-3 labelText1">
                                                                    Period
                                                                </div>
                                                                <div class="col-sm-6 paddingLeft0 paddingRight5">
                                                                    <asp:TextBox ID="txtPeriodCrcd1" runat="server" class="form-control" ReadOnly="True" AutoPostBack="true" onkeydown="return jsDecimals(event);" OnTextChanged="txtPeriodCrcd1_TextChanged"></asp:TextBox>
                                                                </div>
                                                                <div class="col-sm-3 labelText1 paddingLeft0">
                                                                    Months
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </asp:Panel>
                                                <asp:Panel ID="pnlPromotionCrcd2" runat="server">
                                                    <div class="row">
                                                        <div class="col-sm-6">
                                                            <div class="row">
                                                                <div class="col-sm-4 labelText1">
                                                                    <%--Promotion--%>
                                                                </div>
                                                                <div class="col-sm-8 paddingLeft0">
                                                                    <asp:CheckBox ID="chkPromotionCrcd2" runat="server" Visible="false" />
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="col-sm-6">
                                                            <div class="row">
                                                                <div class="col-sm-3 labelText1">
                                                                    <%--Period--%>
                                                                </div>
                                                                <div class="col-sm-6 paddingLeft0 paddingRight5">
                                                                    <asp:DropDownList ID="ddlPromotionCrcd" Visible="false" runat="server" class="form-control" onkeydown="return jsDecimals(event);">
                                                                    </asp:DropDownList>
                                                                </div>
                                                                <div class="col-sm-3 labelText1 paddingLeft0">
                                                                    <%--Months--%>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </asp:Panel>
                                            </asp:Panel>
                                            <asp:Panel ID="pnlCrnote" runat="server" Visible="false">
                                                <div class="row">
                                                    <div class="col-sm-2 labelText1">
                                                        Ref No
                                                    </div>
                                                    <div class="col-sm-9 paddingRight0 paddingLeft0">
                                                        <asp:TextBox ID="txtRefNoCrnote" runat="server" class="form-control"></asp:TextBox>
                                                    </div>
                                                    <div class="col-sm-1 paddingLeft5">
                                                        <asp:LinkButton ID="lbtnRefNoCrnote" runat="server">
                                                    <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                        </asp:LinkButton>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-sm-12 height5">
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-sm-2 labelText1">
                                                        Ref Amount
                                                    </div>
                                                    <div class="col-sm-4 paddingRight0 paddingLeft0">
                                                        <asp:TextBox ID="txtRefAmountCrnote" runat="server" class="form-control"></asp:TextBox>
                                                    </div>
                                                    <div class="col-sm-6">
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-sm-12 height5">
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-sm-6">
                                                        <div class="row">
                                                            <div class="col-sm-4 labelText1">
                                                                <%--Deposit Bank--%>
                                                            </div>
                                                            <div class="col-sm-7 paddingRight0 paddingLeft0">
                                                                <asp:TextBox ID="txtDepositBankCrnote" runat="server" class="form-control" Visible="false"></asp:TextBox>
                                                            </div>
                                                            <div class="col-sm-1 paddingLeft5">
                                                                <asp:LinkButton ID="lbtnDepositBankCrnote" runat="server" Visible="false">
                                                            <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                                </asp:LinkButton>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="col-sm-6">
                                                        <div class="row">
                                                            <div class="col-sm-3 labelText1">
                                                                <%--  Branch--%>
                                                            </div>
                                                            <div class="col-sm-9 paddingLeft0">
                                                                <asp:TextBox ID="txtBranchCrnote" runat="server" class="form-control" Visible="false"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-sm-12 height5">
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-sm-2 labelText1">
                                                        <%--  SCM--%>
                                                    </div>
                                                    <div class="col-sm-8 paddingLeft0">
                                                        <asp:CheckBox ID="chkScmCrnote" runat="server" Visible="false" />
                                                    </div>
                                                </div>
                                            </asp:Panel>
                                            <asp:Panel ID="pnlCheque" runat="server" Visible="false">
                                                <div class="row">
                                                    <div class="col-sm-2 labelText1">
                                                        Cheque No
                                                    </div>
                                                    <div class="col-sm-10 paddingLeft0">
                                                        <asp:TextBox ID="txtChequeNoCheque" runat="server" MaxLength="30" class="form-control" AutoPostBack="true" OnTextChanged="txtChequeNoCheque_TextChanged"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-sm-12 height5">
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-sm-6">
                                                        <div class="row">
                                                            <div class="col-sm-4 labelText1">
                                                                Bank
                                                            </div>
                                                            <div class="col-sm-7 paddingRight0 paddingLeft0">
                                                                <asp:TextBox ID="txtBankCheque" runat="server" MaxLength="30" class="form-control" AutoPostBack="true" OnTextChanged="txtBankCheque_TextChanged"></asp:TextBox>
                                                            </div>
                                                            <div class="col-sm-1 paddingLeft5">
                                                                <asp:LinkButton ID="lbtnBankCheque" runat="server" OnClick="lbtnBankCheque_Click">
                                                                    <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                                </asp:LinkButton>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="col-sm-6">
                                                        <div class="row">
                                                            <div class="col-sm-3 labelText1 paddingRight0">
                                                                Branch
                                                            </div>
                                                            <div class="col-sm-7 paddingLeft0">
                                                                <asp:TextBox ID="txtBranchCheque" runat="server" MaxLength="30" class="form-control" AutoPostBack="true" OnTextChanged="txtBranchCheque_TextChanged"></asp:TextBox>
                                                            </div>
                                                            <div class="col-sm-1 paddingLeft5">
                                                                <asp:LinkButton ID="lbtnBranchCheque" runat="server" OnClick="lbtnBranchCheque_Click">
                                                                    <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                                </asp:LinkButton>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-sm-12 height5">
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-sm-6">
                                                        <div class="row">
                                                            <div class="col-sm-12 labelText1">
                                                                <asp:Label ID="lblBankNameCheque" runat="server"></asp:Label>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="col-sm-6">
                                                        <div class="row">
                                                            <div class="col-sm-3 labelText1">
                                                                Chq. Date
                                                            </div>
                                                            <div class="col-sm-7 paddingRight0 paddingLeft0">
                                                                <asp:TextBox ID="txtChequeDateCheque" runat="server" class="form-control" ReadOnly="false"></asp:TextBox>
                                                            </div>
                                                            <div class="col-sm-2 paddingLeft5">
                                                                <asp:LinkButton ID="lbtnChequeDateCheque" runat="server" CausesValidation="false">
                                                                    <span class="glyphicon glyphicon-calendar" aria-hidden="true"></span>
                                                                </asp:LinkButton>
                                                                <asp:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtChequeDateCheque"
                                                                    PopupButtonID="lbtnChequeDateCheque" Format="dd/MMM/yyyy">
                                                                </asp:CalendarExtender>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-sm-12 height5">
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-sm-6">
                                                        <div class="row">
                                                            <div class="col-sm-4 labelText1">
                                                                Deposit Bank
                                                            </div>
                                                            <div class="col-sm-7 paddingRight0 paddingLeft0">
                                                                <asp:TextBox ID="txtDepositBankCheque" runat="server" MaxLength="30" class="form-control" AutoPostBack="true" OnTextChanged="txtDepositBankCheque_TextChanged"></asp:TextBox>
                                                            </div>
                                                            <div class="col-sm-1 paddingLeft5">
                                                                <asp:LinkButton ID="lbtnDepositBankCheque" runat="server" OnClick="lbtnDepositBankCheque_Click">
                                                                    <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                                </asp:LinkButton>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="col-sm-6">
                                                        <div class="row">
                                                            <div class="col-sm-3 labelText1">
                                                                Branch
                                                            </div>
                                                            <div class="col-sm-9 paddingLeft0">
                                                                <asp:TextBox ID="txtDepositBranchCheque" runat="server" MaxLength="30" class="form-control" AutoPostBack="true"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-sm-12 height5">
                                                    </div>
                                                </div>
                                            </asp:Panel>
                                            <asp:Panel ID="pnlGvo" runat="server" Visible="false">
                                                <div class="row">
                                                    <div class="col-sm-3 labelText1">
                                                        Gift Voucher No
                                                    </div>
                                                    <div class="col-sm-8 paddingRight0 paddingLeft0">
                                                        <asp:TextBox ID="txtGiftVoucherNoGvo" runat="server" MaxLength="30" class="form-control" AutoPostBack="true" OnTextChanged="txtGiftVoucherNoGvo_TextChanged"></asp:TextBox>
                                                    </div>
                                                    <div class="col-sm-1 paddingLeft5">
                                                        <asp:LinkButton ID="lbtnGiftVoucherNoGvo" runat="server" OnClick="lbtnGiftVoucherNoGvo_Click">
                                                            <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                        </asp:LinkButton>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-sm-12 height5">
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-sm-3 labelText1">
                                                        Customer Code
                                                    </div>
                                                    <div class="col-sm-1 labelText1">
                                                        :
                                                    </div>
                                                    <div class="col-sm-8 paddingRight0 paddingLeft0">
                                                        <asp:Label ID="lblCustomerCodeGvo" runat="server"></asp:Label>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-sm-12 height5">
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-sm-3 labelText1">
                                                        Customer Name
                                                    </div>
                                                    <div class="col-sm-1 labelText1">
                                                        :
                                                    </div>
                                                    <div class="col-sm-8 paddingRight0 paddingLeft0">
                                                        <asp:Label ID="lblCustomerNameGvo" runat="server"></asp:Label>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-sm-12 height5">
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-sm-3 labelText1">
                                                        Address
                                                    </div>
                                                    <div class="col-sm-1 labelText1">
                                                        :
                                                    </div>
                                                    <div class="col-sm-8 paddingRight0 paddingLeft0">
                                                        <asp:Label ID="lblAddressGvo" runat="server"></asp:Label>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-sm-12 height5">
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-sm-3 labelText1">
                                                        Mobile
                                                    </div>
                                                    <div class="col-sm-1 labelText1">
                                                        :
                                                    </div>
                                                    <div class="col-sm-8 paddingRight0 paddingLeft0">
                                                        <asp:Label ID="lblMobileGvo" runat="server"></asp:Label>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-sm-12 height5">
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <%--<div class="col-sm-6">--%>
                                                    <%--<div class="row">--%>
                                                    <div class="col-sm-3 labelText1">
                                                        Book
                                                    </div>
                                                    <div class="col-sm-1 labelText1">
                                                        :
                                                    </div>
                                                    <div class="col-sm-8 paddingRight0 paddingLeft0">
                                                        <asp:Label ID="lblBookGvo" runat="server"></asp:Label>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-sm-12 height5">
                                                    </div>
                                                </div>
                                                <%--</div>--%>
                                                <%--</div>--%>
                                                <%--<div class="col-sm-6">--%>
                                                <div class="row">
                                                    <div class="col-sm-3 labelText1">
                                                        Code
                                                    </div>
                                                    <div class="col-sm-1 labelText1">
                                                        :
                                                    </div>
                                                    <div class="col-sm-8 paddingLeft0">
                                                        <asp:Label ID="lblCodeGvo" runat="server"></asp:Label>
                                                    </div>
                                                </div>
                                                <%--</div>--%>
                                                <%--</div>--%>
                                                <div class="row">
                                                    <div class="col-sm-12 height5">
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-sm-3 labelText1">
                                                        Prefix
                                                    </div>
                                                    <div class="col-sm-1 labelText1">
                                                        :
                                                    </div>
                                                    <div class="col-sm-8 paddingRight0 paddingLeft0">
                                                        <asp:Label ID="lblPrefixGvo" runat="server"></asp:Label>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-sm-12 height5">
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-sm-6">
                                                        <div class="row">
                                                            <div class="col-sm-4 labelText1">
                                                                <%-- Deposit Bank--%>
                                                            </div>
                                                            <div class="col-sm-7 paddingRight0 paddingLeft0">
                                                                <asp:TextBox ID="txtDepositBankGvo" runat="server" MaxLength="30" class="form-control" Visible="false"></asp:TextBox>
                                                            </div>
                                                            <div class="col-sm-1 paddingLeft5">
                                                                <asp:LinkButton ID="lbtnDepositBankGvo" runat="server" OnClick="lbtnDepositBankGvo_Click" Visible="false">
                                                                    <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                                </asp:LinkButton>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="col-sm-6">
                                                        <div class="row">
                                                            <div class="col-sm-3 labelText1">
                                                                <%-- Branch--%>
                                                            </div>
                                                            <div class="col-sm-9 paddingLeft0">
                                                                <asp:TextBox ID="txtBranchGvo" runat="server" MaxLength="30" class="form-control" Visible="false"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </asp:Panel>
                                            <asp:Panel ID="pnlGvs" runat="server" Visible="false">
                                                <div class="row">
                                                    <div class="col-sm-2 labelText1">
                                                        Ref No  :
                                                    </div>
                                                    <div class="col-sm-10 paddingLeft0">
                                                        <asp:TextBox ID="txtRefNoGvs" runat="server" class="form-control"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-sm-12 height5">
                                                    </div>
                                                </div>
                                            </asp:Panel>
                                            <asp:Panel ID="pnlLore" runat="server" Visible="false">
                                                <div class="row">
                                                    <div class="col-sm-2 labelText1">
                                                        Card No
                                                    </div>
                                                    <div class="col-sm-9 paddingRight0 paddingLeft0">
                                                        <asp:TextBox ID="txtCardNoLore" runat="server" class="form-control" AutoPostBack="true" OnTextChanged="txtCardNoLore_TextChanged"></asp:TextBox>
                                                    </div>
                                                    <div class="col-sm-1 paddingLeft5">
                                                        <asp:LinkButton ID="lbtnCardNoLore" runat="server" OnClick="lbtnCardNoLore_Click">
                                                            <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                        </asp:LinkButton>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-sm-12 height5">
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-sm-3 labelText1">
                                                        Customer
                                                    </div>
                                                    <div class="col-sm-1 labelText1">
                                                        :
                                                    </div>
                                                    <div class="col-sm-8 paddingRight0 paddingLeft0">
                                                        <asp:Label ID="lblCustomerLore" runat="server"></asp:Label>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-sm-12 height5">
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-sm-3 labelText1">
                                                        Balance Points
                                                    </div>
                                                    <div class="col-sm-1 labelText1">
                                                        :
                                                    </div>
                                                    <div class="col-sm-8 paddingRight0 paddingLeft0">
                                                        <asp:Label ID="lblBalancePointsLore" runat="server"></asp:Label>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-sm-12 height5">
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-sm-3 labelText1">
                                                        Loyalty Type
                                                    </div>
                                                    <div class="col-sm-1 labelText1">
                                                        :
                                                    </div>
                                                    <div class="col-sm-8 paddingRight0 paddingLeft0">
                                                        <asp:Label ID="lblLoyaltyTypeLore" runat="server"></asp:Label>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-sm-12 height5">
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-sm-3 labelText1">
                                                        Point Value
                                                    </div>
                                                    <div class="col-sm-1 labelText1">
                                                        :
                                                    </div>
                                                    <div class="col-sm-8 paddingRight0 paddingLeft0">
                                                        <asp:Label ID="lblPointValueLore" runat="server"></asp:Label>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-sm-12 height5">
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-sm-6">
                                                        <div class="row">
                                                            <div class="col-sm-4 labelText1">
                                                                <%--Deposit Bank--%>
                                                            </div>
                                                            <div class="col-sm-7 paddingRight0 paddingLeft0">
                                                                <asp:TextBox ID="txtDepositBankLore" runat="server" class="form-control" Visible="false"></asp:TextBox>
                                                            </div>
                                                            <div class="col-sm-1 paddingLeft5">
                                                                <asp:LinkButton ID="lbtnDepositBankLore" runat="server" OnClick="lbtnDepositBankLore_Click" Visible="false">
                                                            <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                                </asp:LinkButton>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="col-sm-6">
                                                        <div class="row">
                                                            <div class="col-sm-3 labelText1">
                                                                <%-- Branch--%>
                                                            </div>
                                                            <div class="col-sm-9 paddingLeft0">
                                                                <asp:TextBox ID="txtBranchLore" runat="server" class="form-control" Visible="false"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </asp:Panel>
                                            <asp:Panel ID="pnlBankSlip" runat="server" Visible="false">
                                                <div class="row">
                                                    <div class="col-sm-2 labelText1">
                                                        Acc No
                                                    </div>
                                                    <div class="col-sm-10 paddingLeft0">
                                                        <asp:TextBox ID="txtAccNoBankSlip" runat="server" class="form-control"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-sm-12 height5">
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-sm-6">
                                                        <div class="row">
                                                            <div class="col-sm-4 labelText1">
                                                                Date
                                                            </div>
                                                            <div class="col-sm-7 paddingRight0 paddingLeft0">
                                                                <%--<asp:TextBox ID="txtDateBankSlip" runat="server" class="form-control" ReadOnly="True"></asp:TextBox>--%>
                                                                <asp:TextBox ID="txtDateBankSlip" runat="server" CssClass="form-control" Format="dd/MMM/yyyy"
                                                                    onkeypress="return RestrictSpace()" Enabled="false"></asp:TextBox>
                                                            </div>
                                                            <div class="col-sm-1 paddingLeft5">
                                                                <asp:LinkButton ID="lbtnDateBankSlip" runat="server" CausesValidation="false">
                                                            <span class="glyphicon glyphicon-calendar" aria-hidden="true"></span>
                                                                </asp:LinkButton>
                                                                <asp:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtDateBankSlip"
                                                                    PopupButtonID="lbtnDateBankSlip" Format="dd/MMM/yyyy">
                                                                </asp:CalendarExtender>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="col-sm-6">
                                                        <div class="row">
                                                            <div class="col-sm-4 labelText1">
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-sm-12 height5">
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-sm-6">
                                                        <div class="row">
                                                            <div class="col-sm-4 labelText1">
                                                                Deposit Bank
                                                            </div>
                                                            <div class="col-sm-7 paddingRight0 paddingLeft0">
                                                                <asp:TextBox ID="txtDepositBankBankSlip" runat="server" class="form-control" AutoPostBack="true" OnTextChanged="txtDepositBankBankSlip_TextChanged"></asp:TextBox>
                                                            </div>
                                                            <div class="col-sm-1 paddingLeft5">
                                                                <asp:LinkButton ID="lbtnDepositBankBankSlip" runat="server" OnClick="lbtnDepositBankBankSlip_Click">
                                                                    <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                                </asp:LinkButton>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="col-sm-6">
                                                        <div class="row">
                                                            <div class="col-sm-3 labelText1">
                                                                Branch
                                                            </div>
                                                            <div class="col-sm-9 paddingLeft0">
                                                                <asp:TextBox ID="txtBranchBankSlip" runat="server" class="form-control"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-sm-12 height5">
                                                    </div>
                                                </div>
                                            </asp:Panel>
                                            <asp:Panel ID="pnlStar" runat="server" Visible="false">
                                                <div class="row">
                                                    <div class="col-sm-2 labelText1">
                                                        Mobile No
                                                    </div>
                                                    <div class="col-sm-10 paddingLeft0">
                                                        <asp:TextBox ID="txtMobileNoStar" runat="server" class="form-control" onkeydown="return jsDecimals(event);"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-sm-12 height5">
                                                    </div>
                                                </div>
                                            </asp:Panel>
                                            <asp:Panel ID="pnlDebit" runat="server" Visible="false">
                                                <div class="row">
                                                    <div class="col-sm-2 labelText1">
                                                        Card No
                                                    </div>
                                                    <div class="col-sm-10 paddingLeft0">
                                                        <asp:TextBox ID="txtCardNoDebit" runat="server" class="form-control"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-sm-12 height5">
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-sm-6">
                                                        <div class="row">
                                                            <div class="col-sm-4 labelText1">
                                                                Bank
                                                            </div>
                                                            <div class="col-sm-7 paddingRight0 paddingLeft0">
                                                                <asp:TextBox ID="txtBankDebit" runat="server" class="form-control"></asp:TextBox>
                                                            </div>
                                                            <div class="col-sm-1 paddingLeft5">
                                                                <asp:LinkButton ID="lbtnBankDebit" runat="server" OnClick="lbtnBankDebit_Click">
                                                            <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                                </asp:LinkButton>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="col-sm-6">
                                                        <div class="row">
                                                            <div class="col-sm-3 labelText1 paddingRight0">
                                                            </div>
                                                            <div class="col-sm-9 paddingLeft0">
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-sm-12 height5">
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-sm-6">
                                                        <div class="row">
                                                            <div class="col-sm-4 labelText1">
                                                                Deposit Bank
                                                            </div>
                                                            <div class="col-sm-7 paddingRight0 paddingLeft0">
                                                                <asp:TextBox ID="txtDepositBankDebit" runat="server" class="form-control" OnTextChanged="txtDepositBankDebit_TextChanged"></asp:TextBox>
                                                            </div>
                                                            <div class="col-sm-1 paddingLeft5">
                                                                <asp:LinkButton ID="lbtnDepositBankDebit" runat="server">
                                                                    <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                                </asp:LinkButton>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="col-sm-6">
                                                        <div class="row">
                                                            <div class="col-sm-3 labelText1">
                                                                Branch
                                                            </div>
                                                            <div class="col-sm-9 paddingLeft0">
                                                                <asp:TextBox ID="txtBranchDebit" runat="server" class="form-control"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                
                                            </asp:Panel>
                                            <div class="row">
                                                <div class="col-sm-2">Is Clear</div>
                                                <div class="col-sm-1"><asp:CheckBox ID="chkisclear" runat="server"/></div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-sm-2 paddingLeft0" style="padding-right: 0px;">
                                    <asp:Panel ID="Panel1" runat="server">
                                        <div class="panel panel-default">
                                            <div class="panel-body height190">
                                                <div class="row">
                                                    <div class="col-sm-12">
                                                        Paid Amount
                                                    </div>
                                                    <div class="col-sm-12 height60">
                                                        <asp:Label ID="lblPaidAmount" runat="server" Style="text-align: right"></asp:Label><asp:Label ID="Label2" runat="server"></asp:Label>
                                                    </div>
                                                    <div class="col-sm-12">
                                                        Balance Amount
                                                    </div>
                                                    <div class="col-sm-12 height55">
                                                        <asp:Label ID="lblBalanceAmount" runat="server" Style="text-align: right"></asp:Label>
                                                    </div>
                                                    <div class="col-sm-12">
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-sm-12 height5">
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </asp:Panel>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <asp:Button ID="btnhidden" runat="server" Text="Button" Style="display: none;" />
            <asp:ModalPopupExtender ID="UserPopup" runat="server" Enabled="True" TargetControlID="btnhidden"
                PopupControlID="pnlModalPopup" CancelControlID="btnClose" PopupDragHandleControlID="PopupHeader" Drag="true" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>
            <%-- Style="display: none"--%>
            <asp:Panel runat="server" ID="pnlModalPopup" DefaultButton="lbtnSearch" Style="position: fixed !important" CssClass="setposition">
                <%--<asp:UpdatePanel runat="server">
                    <ContentTemplate>--%>
                <div runat="server" id="test" class="panel panel-primary Mheight">
                    <asp:Label ID="lblvalue" runat="server" Text="Label" Visible="false"></asp:Label>
                    <div class="panel panel-default">
                        <div class="panel-heading">
                            <asp:LinkButton ID="btnClose" runat="server">
                                <span class="glyphicon glyphicon-remove" aria-hidden="true">Close</span>
                            </asp:LinkButton>
                            <div class="col-sm-11">
                            </div>
                            <div class="col-sm-1">
                            </div>
                        </div>
                        <div class="panel-body">
                            <div class="row">
                                <div class="col-sm-12">
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-12 height5">
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-12">
                                    <div class="col-sm-2 labelText1">
                                        Search by key
                                    </div>
                                    <div class="col-sm-3 paddingRight5">
                                        <asp:DropDownList ID="ddlSearchbykey" runat="server" class="form-control">
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-sm-2 labelText1">
                                        Search by word
                                    </div>
                                    <div class="col-sm-4 paddingRight5">
                                        <asp:TextBox ID="txtSearchbyword" CausesValidation="true" placeholder="Search by word" class="form-control" AutoPostBack="true" runat="server" OnTextChanged="txtSearchbyword_TextChanged"></asp:TextBox>
                                    </div>
                                    <div class="col-sm-1 paddingLeft0">
                                        <asp:LinkButton ID="lbtnSearch" runat="server" OnClick="lbtnSearch_Click">
                                                    <span class="glyphicon glyphicon-search" aria-hidden="true"  ></span>
                                        </asp:LinkButton>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-12 height5">
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-12 height5">
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-12">
                                    <asp:GridView ID="grdResult" CausesValidation="false" runat="server" OnSelectedIndexChanged="grdResult_SelectedIndexChanged" AllowPaging="True" OnPageIndexChanging="grdResult_PageIndexChanging" GridLines="None" CssClass="table table-hover table-striped" PageSize="7" PagerStyle-CssClass="cssPager">
                                        <Columns>
                                            <asp:ButtonField Text="select" CommandName="Select" ItemStyle-Width="10" />
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <%--</ContentTemplate>
                </asp:UpdatePanel>--%>
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
</div>

