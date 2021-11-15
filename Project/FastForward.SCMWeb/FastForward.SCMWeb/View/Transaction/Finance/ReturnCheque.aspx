<%@ Page Title="" Language="C#" MasterPageFile="~/View/AdminSite.Master" AutoEventWireup="true" CodeBehind="ReturnCheque.aspx.cs" Inherits="FastForward.SCMWeb.View.Transaction.Finance.ReturnCheque" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script>
        function ConfSave() {
            var selectedvalueOrd = confirm("Are you sure do you want to save ?");
            if (selectedvalueOrd) {
                return true;
            } else {
                return false;
            }
        };
        function ConfEdit() {
            var selectedvalueOrd = confirm("Are you sure do you want to update ?");
            if (selectedvalueOrd) {
                return true;
            } else {
                return false;
            }
        };
        function ConfClear() {
            var selectedvalueOrd = confirm("Are you sure do you want to clear ?");
            if (selectedvalueOrd) {
                return true;
            } else {
                return false;
            }
        };
    </script>
    <script>
        function showStickySuccessToast(value) {
            if (jQuery('.toast-item-wrapper') != null) {
                jQuery('.toast-item-wrapper').remove();
            }
            $().toastmessage('showToast', {
                text: value,
                sticky: true,
                position: 'top-center',
                type: 'success',
                closeText: '',
                close: function () {
                    console.log("toast is closed ...");
                }
            });
        }
        function showStickyNoticeToast(value) {
            if (jQuery('.toast-item-wrapper') != null) {
                jQuery('.toast-item-wrapper').remove();
            }
            $().toastmessage('showToast', {
                text: value,
                sticky: true,
                position: 'top-center',
                type: 'notice',
                closeText: '',
                close: function () { console.log("toast is closed ..."); }
            });
        }
        function showStickyWarningToast(value) {
            if (jQuery('.toast-item-wrapper') != null) {
                jQuery('.toast-item-wrapper').remove();
            }
            $().toastmessage('showToast', {
                text: value,
                sticky: true,
                position: 'top-center',
                type: 'warning',
                closeText: '',
                close: function () {
                    console.log("toast is closed ...");
                }

            });

        }
        function showStickyErrorToast(value) {
            if (jQuery('.toast-item-wrapper') != null) {
                jQuery('.toast-item-wrapper').remove();
            }
            $().toastmessage('showToast', {
                text: value,
                sticky: true,
                position: 'top-center',
                type: 'error',
                closeText: '',
                close: function () {
                    console.log("toast is closed ...");
                }
            });
        }
    </script>
    <style>
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

        .panel {
            padding-top: 0px;
            padding-bottom: 0px;
            margin-bottom: 0px;
            margin-top: 0px;
        }

        .txtUpper {
            text-transform: uppercase;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" runat="server">
    <div class="row">
        <div class="col-sm-12">
            <div class="row">
                <div class="col-sm-12">
                    <asp:UpdateProgress ID="UpdateProgress1" DisplayAfter="10" runat="server" AssociatedUpdatePanelID="pnlMain">
                        <ProgressTemplate>
                            <div class="divWaiting">
                                <asp:Label ID="lblWait" runat="server"
                                    Text="Please wait... " />
                                <asp:Image ID="imgWait" runat="server"
                                    ImageAlign="Middle" ImageUrl="~/images/icons/ajax-loader.gif" />
                            </div>
                        </ProgressTemplate>
                    </asp:UpdateProgress>
                    <asp:UpdatePanel runat="server" ID="pnlMain">
                        <ContentTemplate>
                            <div class="col-sm-12">
                                <div class="row">
                                    <div class="col-sm-12">
                                        <div class="col-sm-10">
                                        </div>
                                        <div class="col-sm-2">
                                            <div class="buttonRow" style="height: 30px; margin-top: -12px;">
                                                <div class="col-sm-1">
                                                </div>
                                                <div class="col-sm-5 padding0 text-center" style="width: 70px;">
                                                    <asp:LinkButton OnClick="lbtnSave_Click" ID="lbtnSave" OnClientClick="return ConfSave();" CausesValidation="false" runat="server"
                                                        CssClass=""> 
                                                        <span class="glyphicon glyphicon-saved" aria-hidden="true"></span>Save</asp:LinkButton>
                                                </div>

                                                <div class="col-sm-5 padding0 text-center">
                                                    <asp:LinkButton ID="lbtnClear" OnClick="lbtnClear_Click" CausesValidation="false" runat="server"
                                                        OnClientClick="return ConfClear();" CssClass=""> 
                                                        <span class="glyphicon glyphicon-refresh" aria-hidden="true"></span>Clear </asp:LinkButton>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-12">
                                        <div class="panel panel-default">
                                            <div class="panel panel-heading">
                                                <strong>Return Cheque</strong>
                                            </div>
                                            <div class="panel panel-body padding0">
                                                <div class="row">
                                                    <div class="col-sm-12">
                                                        <div class="panel panel-default">
                                                            <div class="panel panel-heading">
                                                                <strong>Cheque Details</strong>
                                                            </div>
                                                            <div class="panel panel-body padding0">
                                                                <div class="row" style="margin-top: 2px;">
                                                                    <div class="col-sm-12">
                                                                        <div class="col-sm-9 padding0">
                                                                            <div class="col-sm-8 padding0">
                                                                                <div class="col-sm-3 paddingLeft0">
                                                                                    <div class="col-sm-4 padding0 labelText1">
                                                                                        Company
                                                                                    </div>
                                                                                    <div class="col-sm-7 padding0">
                                                                                        <asp:TextBox OnTextChanged="txtCompany_TextChanged" AutoPostBack="true" ID="txtCompany" CssClass="txtUpper form-control" runat="server" />
                                                                                    </div>
                                                                                    <div class="col-sm-1 padding3">
                                                                                        <asp:LinkButton ID="lbtnSeCom" OnClick="lbtnSeCom_Click" CausesValidation="false"
                                                                                            runat="server" CssClass=""> 
                                                                                                <span class="glyphicon glyphicon-search" aria-hidden="true"></span> 
                                                                                        </asp:LinkButton>
                                                                                    </div>
                                                                                </div>
                                                                                <div class="col-sm-3 padding0">
                                                                                    <div class="col-sm-4 labelText1">
                                                                                        PC
                                                                                    </div>
                                                                                    <div class="col-sm-7 padding0">
                                                                                        <asp:TextBox OnTextChanged="txtPc_TextChanged" AutoPostBack="true" ID="txtPc" CssClass="txtUpper form-control" runat="server" />
                                                                                    </div>
                                                                                    <div class="col-sm-1 padding3">
                                                                                        <asp:LinkButton ID="lbtnSePc" OnClick="lbtnSePc_Click" CausesValidation="false"
                                                                                            runat="server" CssClass=""> 
                                                                                <span class="glyphicon glyphicon-search" aria-hidden="true"></span> 
                                                                                        </asp:LinkButton>
                                                                                    </div>
                                                                                </div>
                                                                                <div class="col-sm-3 padding0">
                                                                                    <div class="col-sm-4 labelText1">
                                                                                        Bank
                                                                                    </div>
                                                                                    <div class="col-sm-7 padding0">
                                                                                        <asp:TextBox OnTextChanged="txtBank_TextChanged" AutoPostBack="true" ID="txtBank" CssClass="txtUpper form-control" runat="server" />
                                                                                    </div>
                                                                                    <div class="col-sm-1 padding3">
                                                                                        <asp:LinkButton ID="lbtnSeBank" OnClick="lbtnSeBank_Click" CausesValidation="false"
                                                                                            runat="server" CssClass=""> 
                                                                                <span class="glyphicon glyphicon-search" aria-hidden="true"></span> 
                                                                                        </asp:LinkButton>
                                                                                    </div>
                                                                                </div>
                                                                                <div class="col-sm-3 padding0">
                                                                                    <div class="col-sm-4 labelText1">
                                                                                        Branch
                                                                                    </div>
                                                                                    <div class="col-sm-7 padding0">
                                                                                        <asp:TextBox OnTextChanged="txtBranch_TextChanged" AutoPostBack="true" ID="txtBranch" CssClass="txtUpper form-control" runat="server" />
                                                                                    </div>
                                                                                    <div class="col-sm-1 padding3">
                                                                                        <asp:LinkButton ID="lbtnSeBranch" OnClick="lbtnSeBranch_Click" CausesValidation="false"
                                                                                            runat="server" CssClass=""> 
                                                                                <span class="glyphicon glyphicon-search" aria-hidden="true"></span> 
                                                                                        </asp:LinkButton>
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                            <div class="col-sm-4 padding0">
                                                                                <div class="col-sm-6 padding0">
                                                                                    <div class="col-sm-3 labelText1">
                                                                                        From
                                                                                    </div>
                                                                                    <div class="col-sm-8 paddingRight5">
                                                                                        <asp:TextBox ID="txtFrom" runat="server" CssClass="form-control" onkeypress="return RestrictSpace()"
                                                                                            Format="dd/MMM/yyyy" Enabled="false"></asp:TextBox>
                                                                                    </div>
                                                                                    <div class="col-sm-1 paddingLeft0 paddingRight0">
                                                                                        <asp:LinkButton ID="btnFrom" runat="server" CausesValidation="false">
                                                                                                <span class="glyphicon glyphicon-calendar" aria-hidden="true"></span>
                                                                                        </asp:LinkButton>
                                                                                        <asp:CalendarExtender ID="CalendarExtender3" runat="server" TargetControlID="txtFrom"
                                                                                            PopupButtonID="btnFrom" Format="dd/MMM/yyyy">
                                                                                        </asp:CalendarExtender>
                                                                                    </div>
                                                                                </div>
                                                                                <div class="col-sm-6 padding0">
                                                                                    <div class="col-sm-3 text-right labelText1">
                                                                                        To
                                                                                    </div>
                                                                                    <div class="col-sm-8 paddingRight5">
                                                                                        <asp:TextBox ID="txtTo" runat="server" CssClass="form-control" onkeypress="return RestrictSpace()"
                                                                                            Format="dd/MMM/yyyy" Enabled="false"></asp:TextBox>
                                                                                    </div>
                                                                                    <div class="col-sm-1 paddingLeft0 paddingRight0">
                                                                                        <asp:LinkButton ID="btnTo" runat="server" CausesValidation="false">
                                                                                                <span class="glyphicon glyphicon-calendar" aria-hidden="true"></span>
                                                                                        </asp:LinkButton>
                                                                                        <asp:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtTo"
                                                                                            PopupButtonID="btnTo" Format="dd/MMM/yyyy">
                                                                                        </asp:CalendarExtender>
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                        <div class="col-sm-3 padding0">
                                                                            <div class="col-sm-11 paddingLeft0">
                                                                                <div class="col-sm-1 padding0">
                                                                                </div>
                                                                                <div class="col-sm-3 labelText1">
                                                                                    Cheque #
                                                                                </div>
                                                                                <div class="col-sm-7 padding0">
                                                                                    <asp:TextBox OnTextChanged="txtChequeNo_TextChanged" AutoPostBack="true" ID="txtChequeNo" CssClass="form-control" runat="server" />
                                                                                </div>
                                                                                <div class="col-sm-1 padding3">
                                                                                    <asp:LinkButton ID="lbtnSeChequeNo" OnClick="lbtnSeChequeNo_Click" CausesValidation="false"
                                                                                        runat="server" CssClass=""> 
                                                                                <span class="glyphicon glyphicon-search" aria-hidden="true"></span> 
                                                                                    </asp:LinkButton>
                                                                                </div>
                                                                            </div>
                                                                            <div class="col-sm-1 padding0">
                                                                                <div class="" style="margin-top: -5px;">
                                                                                    <asp:LinkButton ID="lbtnMainSer" OnClick="lbtnMainSer_Click" CausesValidation="false"
                                                                                        runat="server" CssClass=""> 
                                                                                        <span class="glyphicon glyphicon-search" aria-hidden="true" style="font-size:20px;"></span> 
                                                                                    </asp:LinkButton>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class="row">
                                                                    <div class="col-sm-12">
                                                                        <div class="">
                                                                            <div class="panel panel-default">
                                                                                <div class="panel panel-body padding0" style="height: 150px;">
                                                                                    <div class="">
                                                                                        <asp:GridView ID="dgvChekDetails" CssClass="table table-hover table-striped" runat="server" GridLines="None"
                                                                                            ShowHeaderWhenEmpty="True"
                                                                                            EmptyDataText="No data found..." AutoGenerateColumns="False" AllowPaging="true" PageSize="5"
                                                                                            OnPageIndexChanging="dgvChekDetails_PageIndexChanging"
                                                                                            PagerStyle-CssClass="cssPager">
                                                                                            <Columns>
                                                                                                <asp:TemplateField HeaderText="">
                                                                                                    <ItemTemplate>
                                                                                                        <asp:LinkButton ID="lbtnAddChkCorr" OnClick="lbtnAddChkCorr_Click" CausesValidation="false" runat="server"
                                                                                                            CssClass=""> 
                                                                                                <span class="glyphicon glyphicon-arrow-down" aria-hidden="true"></span> </asp:LinkButton>
                                                                                                    </ItemTemplate>
                                                                                                    <ItemStyle Width="10px" />
                                                                                                    <HeaderStyle Width="10px" />
                                                                                                </asp:TemplateField>
                                                                                                <asp:TemplateField HeaderText="Profit Center">
                                                                                                    <ItemTemplate>
                                                                                                        <asp:Label ID="lblsar_profit_center_cd" Text='<%# Bind("sar_profit_center_cd") %>' runat="server" Width="100%" />
                                                                                                    </ItemTemplate>
                                                                                                    <ItemStyle Width="50px" />
                                                                                                    <HeaderStyle Width="50px" />
                                                                                                </asp:TemplateField>
                                                                                                <asp:TemplateField HeaderText="Cheque #">
                                                                                                    <ItemTemplate>
                                                                                                        <asp:Label ID="lblsard_ref_no" Text='<%# Bind("sard_ref_no") %>' runat="server" Width="100%" />
                                                                                                    </ItemTemplate>
                                                                                                    <ItemStyle Width="100px" />
                                                                                                    <HeaderStyle Width="100px" />
                                                                                                </asp:TemplateField>
                                                                                                <asp:TemplateField HeaderText="Date">
                                                                                                    <ItemTemplate>
                                                                                                        <asp:Label ID="lblsard_chq_dt" Text='<%# Bind("sard_chq_dt","{0:dd/MMM/yyyy}") %>' runat="server" Width="100%" />
                                                                                                    </ItemTemplate>
                                                                                                    <ItemStyle Width="60px" />
                                                                                                    <HeaderStyle Width="60px" />
                                                                                                </asp:TemplateField>
                                                                                                <asp:TemplateField HeaderText="Bank">
                                                                                                    <ItemTemplate>
                                                                                                        <asp:Label ID="lblsard_chq_bank_cd" Text='<%# Bind("sard_chq_bank_cd") %>' runat="server" Width="100%" />
                                                                                                    </ItemTemplate>
                                                                                                    <ItemStyle Width="100px" />
                                                                                                    <HeaderStyle Width="100px" />
                                                                                                </asp:TemplateField>
                                                                                                <asp:TemplateField HeaderText="Branch">
                                                                                                    <ItemTemplate>
                                                                                                        <asp:Label ID="lblsard_chq_branch" Text='<%# Bind("sard_chq_branch") %>' runat="server" Width="100%" />
                                                                                                    </ItemTemplate>
                                                                                                    <ItemStyle Width="80px" />
                                                                                                    <HeaderStyle Width="80px" />
                                                                                                </asp:TemplateField>
                                                                                                <asp:TemplateField HeaderText="Amount">
                                                                                                    <ItemTemplate>
                                                                                                        <asp:Label ID="lblsard_settle_amt" Text='<%# Bind("sard_settle_amt","{0:N2}") %>' runat="server" Width="100%" />
                                                                                                    </ItemTemplate>
                                                                                                    <ItemStyle Width="80px" CssClass="gridHeaderAlignRight" />
                                                                                                    <HeaderStyle Width="80px" CssClass="gridHeaderAlignRight" />
                                                                                                </asp:TemplateField>
                                                                                                <asp:TemplateField HeaderText="">
                                                                                                    <ItemTemplate>
                                                                                                        <asp:Label ID="lblTemp2" Text='' runat="server" Width="100%" />
                                                                                                    </ItemTemplate>
                                                                                                    <ItemStyle Width="50px" />
                                                                                                    <HeaderStyle Width="50px" />
                                                                                                </asp:TemplateField>
                                                                                            </Columns>
                                                                                        </asp:GridView>
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>

                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-sm-12">
                                                        <div class="">
                                                            <div class="panel panel-default">
                                                                <div class="panel panel-heading">
                                                                    <strong>Cheque Correction</strong>
                                                                </div>
                                                                <div class="panel panel-body padding0">
                                                                    <asp:Panel runat="server" ID="pnlCheqCore">
                                                                        <div class="row">
                                                                            <div class="col-sm-12">
                                                                                <div class="col-sm-10">
                                                                                    <div class="row" style="margin-top: 5px;">
                                                                                        <div class="col-sm-12">
                                                                                            <div class="col-sm-7 padding0">
                                                                                                <div class="col-sm-6 padding0">
                                                                                                    <div class="col-sm-4 padding0 labelText1">
                                                                                                        Correct Cheque #
                                                                                                    </div>
                                                                                                    <div class="col-sm-8 padding0">
                                                                                                        <asp:TextBox OnTextChanged="txtCorrChkNo_TextChanged" AutoPostBack="true" ID="txtCorrChkNo" CssClass="form-control" runat="server" />
                                                                                                    </div>
                                                                                                </div>
                                                                                                <div class="col-sm-6 padding0">
                                                                                                    <div class="col-sm-2 labelText1">
                                                                                                        Bank
                                                                                                    </div>
                                                                                                    <div class="col-sm-10 padding0">
                                                                                                        <asp:DropDownList AutoPostBack="true" OnSelectedIndexChanged="ddlCorrBank_SelectedIndexChanged" ID="ddlCorrBank" CssClass="form-control" AppendDataBoundItems="true" runat="server">
                                                                                                            <asp:ListItem Text="--Select--" Value="0" />
                                                                                                        </asp:DropDownList>
                                                                                                    </div>
                                                                                                </div>
                                                                                            </div>
                                                                                            <div class="col-sm-5 padding0">
                                                                                                <div class="col-sm-8">
                                                                                                    <div class="col-sm-4 labelText1">
                                                                                                        Branch
                                                                                                    </div>
                                                                                                    <div class="col-sm-7 padding0">
                                                                                                        <asp:TextBox OnTextChanged="txtCorrBranch_TextChanged" AutoPostBack="true" ID="txtCorrBranch" CssClass="form-control" runat="server" />
                                                                                                    </div>
                                                                                                    <div class="col-sm-1 padding3">
                                                                                                        <asp:LinkButton ID="lbtnSeCorrBranch" OnClick="lbtnSeCorrBranch_Click" CausesValidation="false"
                                                                                                            runat="server" CssClass=""> 
                                                                                                    <span class="glyphicon glyphicon-search" aria-hidden="true"></span> 
                                                                                                        </asp:LinkButton>
                                                                                                    </div>
                                                                                                </div>
                                                                                                <div class="col-sm-4 padding0">
                                                                                                    <div class="col-sm-3 labelText1">
                                                                                                        Date
                                                                                                    </div>
                                                                                                    <div class="col-sm-8 paddingRight5">
                                                                                                        <asp:TextBox ID="txtCorrDate" runat="server" CssClass="form-control" onkeypress="return RestrictSpace()"
                                                                                                            Format="dd/MMM/yyyy" Enabled="false"></asp:TextBox>
                                                                                                    </div>
                                                                                                    <div class="col-sm-1 paddingLeft0 paddingRight0">
                                                                                                        <asp:LinkButton ID="btnCorrDate" runat="server" CausesValidation="false">
                                                                                                            <span class="glyphicon glyphicon-calendar" aria-hidden="true"></span>
                                                                                                        </asp:LinkButton>
                                                                                                        <asp:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtCorrDate"
                                                                                                            PopupButtonID="btnCorrDate" Format="dd/MMM/yyyy">
                                                                                                        </asp:CalendarExtender>
                                                                                                    </div>
                                                                                                </div>
                                                                                            </div>
                                                                                        </div>
                                                                                    </div>
                                                                                </div>
                                                                                <div class="col-sm-1 padding0">
                                                                                    <div class="row buttonRow">
                                                                                        <div class="col-sm-12">
                                                                                            <div class="col-sm-6 padding0">
                                                                                                <asp:LinkButton OnClick="lbtnEditCheque_Click" ID="lbtnEditCheque" CausesValidation="false" runat="server" OnClientClick="return ConfEdit();"
                                                                                                    CssClass=""> 
                                                                                                                    <span class="glyphicon glyphicon-edit" aria-hidden="true"></span>Update</asp:LinkButton>
                                                                                            </div>
                                                                                            <div class="col-sm-6 padding0">
                                                                                                <asp:LinkButton ID="lbtnClearCheque" OnClick="lbtnClearCheque_Click" CausesValidation="false" OnClientClick="return ConfClear();"
                                                                                                    runat="server" CssClass=""> 
                                                                                                        <span class="glyphicon glyphicon-refresh" aria-hidden="true"></span>Clear 
                                                                                                </asp:LinkButton>
                                                                                            </div>
                                                                                        </div>
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
                                                <div class="row">
                                                    <div class="col-sm-12">
                                                        <div class="panel panel-default">
                                                            <div class="panel panel-heading">
                                                                <strong>Return Details</strong>
                                                            </div>
                                                            <div class="panel panel-body padding0">
                                                                <div class="row">
                                                                    <div class="col-sm-12">
                                                                        <div class="row" style="margin-top: 3px;">
                                                                            <div class="col-sm-12">
                                                                                <div class="col-sm-2 padding0">
                                                                                    <div class="col-sm-5 paddingRight0 labelText1">
                                                                                        Return Date
                                                                                    </div>
                                                                                    <div class="col-sm-7 padding0">
                                                                                        <div class="col-sm-10 paddingRight5">
                                                                                            <asp:TextBox ID="txtReturnDate" runat="server" CssClass="form-control" onkeypress="return RestrictSpace()"
                                                                                                Format="dd/MMM/yyyy" Enabled="false"></asp:TextBox>
                                                                                        </div>
                                                                                        <div class="col-sm-2 paddingLeft0 paddingRight0">
                                                                                            <asp:LinkButton ID="btnRetDt" runat="server" CausesValidation="false">
                                                                                                                            <span class="glyphicon glyphicon-calendar" aria-hidden="true"></span>
                                                                                            </asp:LinkButton>
                                                                                            <asp:CalendarExtender ID="CalendarExtender4" runat="server" TargetControlID="txtReturnDate"
                                                                                                PopupButtonID="btnRetDt" Format="dd/MMM/yyyy">
                                                                                            </asp:CalendarExtender>
                                                                                        </div>
                                                                                    </div>
                                                                                </div>
                                                                                <div class="col-sm-5 padding0">
                                                                                    <div class="col-sm-2 labelText1">
                                                                                        Return Bank
                                                                                    </div>
                                                                                    <div class="col-sm-3 padding0">
                                                                                        <div class="col-sm-10 padding0">
                                                                                            <asp:TextBox OnTextChanged="txtRetBank_TextChanged" AutoPostBack="true" ID="txtRetBank" CssClass="form-control" runat="server" />
                                                                                        </div>
                                                                                        <div class="col-sm-1 padding3">
                                                                                            <asp:LinkButton ID="lbtnSeRetBank" OnClick="lbtnSeRetBank_Click" CausesValidation="false"
                                                                                                runat="server" CssClass=""> 
                                                                                                    <span class="glyphicon glyphicon-search" aria-hidden="true"></span> 
                                                                                            </asp:LinkButton>
                                                                                        </div>
                                                                                    </div>
                                                                                    <div class="col-sm-6 padding0">
                                                                                        <asp:TextBox Enabled="false" ID="txtRetBankDesc" CssClass="form-control" runat="server" />
                                                                                    </div>
                                                                                </div>
                                                                                <div class="col-sm-2 padding0">
                                                                                    <div class="col-sm-5 labelText1">
                                                                                        Bank Type
                                                                                    </div>
                                                                                    <div class="col-sm-7 padding0">
                                                                                        <asp:DropDownList runat="server" ID="ddlRetBankTp" CssClass="form-control">
                                                                                            <asp:ListItem Text="--Select--" Value="0" />
                                                                                            <asp:ListItem Text="DIRECT" Value="DIRECT" />
                                                                                            <asp:ListItem Text="CASH" Value="CASH" />
                                                                                        </asp:DropDownList>
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-sm-12">
                                                        <div class="panel panel-default">
                                                            <div class="panel panel-heading">
                                                                <strong>Receipt Details</strong>
                                                            </div>
                                                            <div class="panel panel-body padding0">
                                                                <div class="row">
                                                                    <div class="col-sm-12">
                                                                        <div class="panel panel-body padding0">
                                                                            <div class="" style="overflow-y: scroll; height: 130px;">
                                                                                <asp:GridView ID="dgvRecDet" CssClass="table table-hover table-striped" runat="server" GridLines="None"
                                                                                    ShowHeaderWhenEmpty="True"
                                                                                    EmptyDataText="No data found..." AutoGenerateColumns="False" AllowPaging="false" PageSize="5"
                                                                                    PagerStyle-CssClass="cssPager">
                                                                                    <Columns>
                                                                                        <asp:TemplateField HeaderText="Type">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="lblsar_receipt_type" Text='<%# Bind("sar_receipt_type") %>' runat="server" Width="100%" />
                                                                                            </ItemTemplate>
                                                                                            <ItemStyle Width="60px" />
                                                                                            <HeaderStyle Width="60px" />
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="Reference/Receipt # ">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="lblsar_receipt_no" Text='<%# Bind("sar_receipt_no") %>' runat="server" Width="100%" />
                                                                                            </ItemTemplate>
                                                                                            <ItemStyle Width="125px" />
                                                                                            <HeaderStyle Width="125px" />
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="Prefix">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="lblsar_prefix" Text='<%# Bind("sar_prefix") %>' runat="server" Width="100%" />
                                                                                            </ItemTemplate>
                                                                                            <ItemStyle Width="50px" />
                                                                                            <HeaderStyle Width="50px" />
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="Manual Ref">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="lblsar_manual_ref_no" Text='<%# Bind("sar_manual_ref_no") %>' runat="server" Width="100%" />
                                                                                            </ItemTemplate>
                                                                                            <ItemStyle Width="125px" />
                                                                                            <HeaderStyle Width="125px" />
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="Date">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="lblsard_chq_dt" Text='<%# Bind("sard_chq_dt","{0:dd/MMM/yyyy}") %>' runat="server" Width="100%" />
                                                                                            </ItemTemplate>
                                                                                            <ItemStyle Width="50px" />
                                                                                            <HeaderStyle Width="50px" />
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="Amount">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="lblsard_settle_amt" Text='<%# Bind("sard_settle_amt","{0:N2}") %>' runat="server" Width="100%" />
                                                                                            </ItemTemplate>
                                                                                            <ItemStyle Width="70px" CssClass="gridHeaderAlignRight" />
                                                                                            <HeaderStyle Width="70px" CssClass="gridHeaderAlignRight" />
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="lblTemp" Text='' runat="server" Width="100%" />
                                                                                            </ItemTemplate>
                                                                                            <ItemStyle Width="10px" />
                                                                                            <HeaderStyle Width="10px" />
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="" Visible="false">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="lblSfd_itm" Text='<%# Bind("sar_is_mgr_iss") %>' runat="server" Width="100%" />
                                                                                            </ItemTemplate>
                                                                                            <ItemStyle Width="100px" />
                                                                                            <HeaderStyle Width="100px" />
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="Cheque Branch">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="lblsard_chq_branch" Text='<%# Bind("sard_chq_branch") %>' runat="server" Width="100%" />
                                                                                            </ItemTemplate>
                                                                                            <ItemStyle Width="100px" />
                                                                                            <HeaderStyle Width="100px" />
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="lblTemp2" Text='' runat="server" Width="100%" />
                                                                                            </ItemTemplate>
                                                                                            <ItemStyle Width="50px" />
                                                                                            <HeaderStyle Width="50px" />
                                                                                        </asp:TemplateField>
                                                                                    </Columns>
                                                                                </asp:GridView>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="row">
                                                                <div class="col-sm-12">
                                                                    <div class="row" style="margin-top: 5px; margin-bottom: 5px;">
                                                                        <div class="col-sm-3">
                                                                        </div>
                                                                        <div class="col-sm-7">
                                                                            <div class="col-sm-4 padding0">
                                                                                <div class="col-sm-6 labelText1">
                                                                                    Total System Amount 
                                                                                </div>
                                                                                <div class="col-sm-6 paddingLeft0">
                                                                                    <asp:TextBox ID="txtTotSysAmt" CssClass="form-control text-right" Enabled="false" runat="server" />
                                                                                </div>
                                                                            </div>
                                                                            <div class="col-sm-4 padding0">
                                                                                <div class="col-sm-6 labelText1">
                                                                                    Total Return Amount 
                                                                                </div>
                                                                                <div class="col-sm-6 paddingLeft0">
                                                                                    <asp:TextBox ID="txtTotRetAmt" CssClass="txtTotRetAmt form-control text-right" Enabled="true" runat="server" />
                                                                                </div>
                                                                            </div>
                                                                            <div class="col-sm-4 padding0">
                                                                                <div class="col-sm-6 labelText1">
                                                                                    Charge For Manager
                                                                                </div>
                                                                                <div class="col-sm-6 paddingLeft0">
                                                                                    <asp:TextBox ID="txtTotManAmt" CssClass="form-control text-right" Enabled="false" runat="server" />
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                        <div class="col-sm-1">
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>
    </div>
    <%-- pnl search --%>
    <asp:UpdatePanel runat="server" ID="upSearch">
        <ContentTemplate>
            <asp:Button ID="btn1" runat="server" Text="Button" Style="display: none;" />
            <asp:ModalPopupExtender ID="PopupSearch" runat="server" Enabled="True" TargetControlID="btn1"
                PopupControlID="pnlSearch" PopupDragHandleControlID="PopupHeader" Drag="true" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:Panel runat="server" ID="pnlSearch" DefaultButton="lbtnSearch">
        <div runat="server" id="test" class="panel panel-primary" style="padding: 5px;">
            <div class="panel panel-default" style="height: 350px; width: 700px;">
                <div class="panel-heading" style="height: 25px;">
                    <div class="col-sm-11"></div>
                    <div class="col-sm-1">
                        <asp:LinkButton ID="btnClose" runat="server" OnClick="btnClose_Click">
                             <span class="glyphicon glyphicon-remove"  aria-hidden="true">Close</span>
                        </asp:LinkButton>
                    </div>
                </div>
                <div class="panel-body">
                    <div class="row height16">
                    </div>
                    <div class="row">
                        <div class="col-sm-12">
                            <div class="col-sm-2 labelText1">
                                Search by key
                            </div>
                            <asp:ScriptManagerProxy ID="ScriptManagerProxy10" runat="server"></asp:ScriptManagerProxy>
                            <asp:UpdatePanel ID="UpdatePanel10" runat="server">
                                <ContentTemplate>
                                    <div class="col-sm-3 paddingRight5">
                                        <asp:DropDownList ID="ddlSerByKey" runat="server" class="form-control" AutoPostBack="false">
                                        </asp:DropDownList>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                            <div class="col-sm-2 labelText1">
                                Search by word
                            </div>
                            <div class="col-sm-3 paddingRight5">
                                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                    <ContentTemplate>
                                        <asp:TextBox ID="txtSerByKey" CausesValidation="false" class="form-control" AutoPostBack="true" OnTextChanged="lbtnSearch_Click" runat="server"></asp:TextBox>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                            <asp:ScriptManagerProxy ID="ScriptManagerProxy9" runat="server"></asp:ScriptManagerProxy>
                            <asp:UpdatePanel ID="UpdatePanel9" runat="server">
                                <ContentTemplate>
                                    <div class="col-sm-1 paddingLeft0">
                                        <asp:LinkButton ID="lbtnSearch" runat="server" OnClick="lbtnSearch_Click">
                                     <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                        </asp:LinkButton>
                                    </div>
                                </ContentTemplate>

                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="txtSerByKey" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-sm-12 height16">
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-sm-12">
                            <asp:ScriptManagerProxy ID="ScriptManagerProxy8" runat="server"></asp:ScriptManagerProxy>
                            <asp:UpdatePanel ID="UpdatePanel8" runat="server">
                                <ContentTemplate>
                                    <asp:GridView ID="dgvResult" CausesValidation="false" runat="server" AllowPaging="True"
                                        GridLines="None" CssClass="table table-hover table-striped" PageSize="10" PagerStyle-CssClass="cssPager" ShowHeaderWhenEmpty="true"
                                        EmptyDataText="No data found..."
                                        OnPageIndexChanging="dgvResult_PageIndexChanging" OnSelectedIndexChanged="dgvResult_SelectedIndexChanged">
                                        <Columns>
                                            <asp:ButtonField Text="Select" CommandName="Select" ItemStyle-Width="12" />
                                        </Columns>
                                    </asp:GridView>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </asp:Panel>
    <script>
        Sys.Application.add_load(func);
        function func() {
            $('.txtTotRetAmt').keypress(function (evt) {
                // var ch = (evt.which) ? evt.which : evt.keyCode;
                var ch = evt.which;
                var str = $(this).val();
                console.log(ch);
                if (ch == 46) {
                    if (str.indexOf(".") == -1) {
                        return true;
                    } else {
                        return false;
                    }
                }
                else if ((ch == 8) || (ch == 9) || (ch == 46) || (ch == 0)) {
                    return true;
                }
                else if (ch > 47 && ch < 58) {
                    return true;
                }
                else {
                    return false;
                }
            })
            $('.txtTotRetAmt').mousedown(function (e) {
                if (e.button == 2) {
                    alert('This functionality is disabled !');
                    return false;
                } else {
                    return true;
                }
            });
        }
    </script>
</asp:Content>
