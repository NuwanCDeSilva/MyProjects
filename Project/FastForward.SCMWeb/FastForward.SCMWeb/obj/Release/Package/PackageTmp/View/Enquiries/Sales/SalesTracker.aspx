<%@ Page Title="" Language="C#" MasterPageFile="~/View/AdminSite.Master" AutoEventWireup="true" CodeBehind="SalesTracker.aspx.cs" Inherits="FastForward.SCMWeb.View.Enquiries.Sales.SalesTracker" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script>
        function ConfClear() {
            var selectedvalueOrd = confirm("Do you want to clear ?");
            if (selectedvalueOrd) {
                return true;
            } else {
                return false;
            }
        };
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
            padding-top: 1px;
            padding-bottom: 0px;
            margin-bottom: 0px;
        }

        .GrdHeight {
            height: 120px;
            overflow-x: hidden;
            overflow-y: auto;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" runat="server">
    <asp:UpdateProgress ID="UpdateProgress1" DisplayAfter="10" runat="server" AssociatedUpdatePanelID="mainPnl">
        <ProgressTemplate>
            <div class="divWaiting">
                <asp:Label ID="lblWait" runat="server"
                    Text="Please wait... " />
                <asp:Image ID="imgWait" runat="server"
                    ImageAlign="Middle" ImageUrl="~/images/icons/ajax-loader.gif" />
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <div class="col-sm-12">
        <asp:UpdatePanel runat="server" ID="mainPnl">
            <ContentTemplate>
                <div class="row">
                    <div class="col-sm-12">
                        <div class="panel panel-default">
                            <div class="panel panel-body padding0">
                                <div class="row">
                                    <div class="col-sm-12">
                                        <div class="col-sm-11">
                                        </div>
                                        <div class="col-sm-1">
                                            <div class="Row buttonRow">
                                                <div class="col-sm-12 padding0">
                                                    <asp:LinkButton ID="lbtnClear" OnClientClick="return ConfClear()" CausesValidation="false" OnClick="lbtnClear_Click" runat="server"> 
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
                                                <strong><b>Sales Tracker</b></strong>
                                            </div>
                                            <div class="panel panel-body padding0">
                                                <div class="row">
                                                    <div class="col-sm-12">
                                                        <div class="col-sm-7 padding0">
                                                            <div class="panel panel-default">
                                                                <div class="panel panel-body">
                                                                    <div class="row paddingbottom1">
                                                                        <div class="col-sm-12">
                                                                            <asp:Panel runat="server" DefaultButton="lbtnSer1">
                                                                                <div class="col-sm-11 padding0">
                                                                                    <div class="row">
                                                                                        <div class="col-sm-12">
                                                                                            <div class="col-sm-3 padding0 labelText1">
                                                                                                <div class="col-sm-4 labelText1">
                                                                                                    From
                                                                                                </div>
                                                                                                <div class="col-sm-6 padding0">
                                                                                                    <asp:TextBox ID="txtFrDate" runat="server" CssClass="form-control" Format="dd/MMM/yyyy"
                                                                                                        onkeypress="return RestrictSpace()" Enabled="false"></asp:TextBox>
                                                                                                    <asp:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtFrDate"
                                                                                                        PopupButtonID="btnFrDate" Format="dd/MMM/yyyy">
                                                                                                    </asp:CalendarExtender>
                                                                                                </div>
                                                                                                <div class="col-sm-1 paddingLeft3">
                                                                                                    <asp:LinkButton ID="btnFrDate" CausesValidation="false" runat="server">
                                                                                    <span class="glyphicon glyphicon-calendar" aria-hidden="true"  ></span>
                                                                                                    </asp:LinkButton>
                                                                                                </div>
                                                                                            </div>
                                                                                            <div class="col-sm-3 padding0 labelText1">
                                                                                                <div class="col-sm-5 labelText1">
                                                                                                    P. Center
                                                                                                </div>
                                                                                                <div class="col-sm-6 padding0 ">
                                                                                                    <asp:TextBox ID="txtPc" Style="text-transform: uppercase" AutoPostBack="true" OnTextChanged="txtPc_TextChanged" runat="server" CssClass="form-control"></asp:TextBox>
                                                                                                </div>
                                                                                                <div class="col-sm-1 paddingLeft3 paddingRight0">
                                                                                                    <asp:LinkButton ID="lbtnSePC" CausesValidation="false" runat="server" OnClick="lbtnSePC_Click">
                                                                                                    <span class="glyphicon glyphicon-search" aria-hidden="true"  ></span>
                                                                                                    </asp:LinkButton>
                                                                                                </div>
                                                                                            </div>
                                                                                            <div class="col-sm-3 padding0 labelText1">
                                                                                                <div class="col-sm-5 labelText1">
                                                                                                    Customer
                                                                                                </div>
                                                                                                <div class="col-sm-6 padding0 ">
                                                                                                    <asp:TextBox ID="txtCust" Style="text-transform: uppercase" AutoPostBack="true" OnTextChanged="txtCust_TextChanged" runat="server" CssClass="form-control"></asp:TextBox>
                                                                                                </div>
                                                                                                <div class="col-sm-1 paddingLeft3 paddingRight0">
                                                                                                    <asp:LinkButton ID="lbtSeCus" CausesValidation="false" OnClick="lbtSeCus_Click" runat="server">
                                                                                                <span class="glyphicon glyphicon-search" aria-hidden="true"  ></span>
                                                                                                    </asp:LinkButton>
                                                                                                </div>
                                                                                            </div>
                                                                                            <div class="col-sm-3 padding0 labelText1">
                                                                                                <div class="col-sm-4 labelText1">
                                                                                                    Item
                                                                                                </div>
                                                                                                <div class="col-sm-7 padding0 ">
                                                                                                    <asp:TextBox ID="txtItm" Style="text-transform: uppercase" AutoPostBack="true" OnTextChanged="txtItm_TextChanged" runat="server" CssClass="form-control"></asp:TextBox>
                                                                                                </div>
                                                                                                <div class="col-sm-1 paddingLeft3 paddingRight0">
                                                                                                    <asp:LinkButton ID="lbtnSeItm" OnClick="lbtnSeItm_Click" CausesValidation="false" runat="server">
                                                                                    <span class="glyphicon glyphicon-search" aria-hidden="true"  ></span>
                                                                                                    </asp:LinkButton>
                                                                                                </div>
                                                                                            </div>
                                                                                        </div>
                                                                                    </div>
                                                                                    <div class="row">
                                                                                        <div class="col-sm-12">
                                                                                            <div class="col-sm-3 padding0 labelText1">
                                                                                                <div class="col-sm-4 labelText1">
                                                                                                    To
                                                                                                </div>
                                                                                                <div class="col-sm-6 padding0">
                                                                                                    <asp:TextBox ID="txtToDate" runat="server" CssClass="form-control" Format="dd/MMM/yyyy" Enabled="false"></asp:TextBox>
                                                                                                    <asp:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtToDate"
                                                                                                        PopupButtonID="btnToDate" Format="dd/MMM/yyyy">
                                                                                                    </asp:CalendarExtender>
                                                                                                </div>
                                                                                                <div class="col-sm-1 paddingLeft3 paddingRight0">
                                                                                                    <asp:LinkButton ID="btnToDate" CausesValidation="false" runat="server">
                                                                                                    <span class="glyphicon glyphicon-calendar" aria-hidden="true"  ></span>
                                                                                                    </asp:LinkButton>
                                                                                                </div>
                                                                                            </div>
                                                                                            <div class="col-sm-3 padding0 labelText1">
                                                                                                <div class="col-sm-5 labelText1">
                                                                                                    Category 1
                                                                                                </div>
                                                                                                <div class="col-sm-6 padding0 ">
                                                                                                    <asp:TextBox ID="txtCat1" Style="text-transform: uppercase" AutoPostBack="true" OnTextChanged="txtCat1_TextChanged" runat="server" CssClass="form-control"></asp:TextBox>
                                                                                                </div>
                                                                                                <div class="col-sm-1 paddingLeft3 paddingRight0">
                                                                                                    <asp:LinkButton ID="lbtnSeCat1" OnClick="lbtnSeCat1_Click" CausesValidation="false" runat="server">
                                                                                                    <span class="glyphicon glyphicon-search" aria-hidden="true"  ></span>
                                                                                                    </asp:LinkButton>
                                                                                                </div>
                                                                                            </div>
                                                                                            <div class="col-sm-3 padding0 labelText1">
                                                                                                <div class="col-sm-5 labelText1">
                                                                                                    Executive
                                                                                                </div>
                                                                                                <div class="col-sm-6 padding0">
                                                                                                    <asp:TextBox ID="txtExe" Style="text-transform: uppercase" AutoPostBack="true" OnTextChanged="txtExe_TextChanged" runat="server" CssClass="form-control"></asp:TextBox>
                                                                                                </div>
                                                                                                <div class="col-sm-1 paddingLeft3 paddingRight0">
                                                                                                    <asp:LinkButton ID="lbtnSeExee" OnClick="lbtnSeExee_Click" CausesValidation="false" runat="server">
                                                                                                    <span class="glyphicon glyphicon-search" aria-hidden="true"  ></span>
                                                                                                    </asp:LinkButton>
                                                                                                </div>
                                                                                            </div>

                                                                                        </div>
                                                                                    </div>
                                                                                </div>
                                                                                <div class="col-sm-1">
                                                                                    <div class="row buttonRow">
                                                                                        <div class="col-sm-2">
                                                                                        </div>
                                                                                        <div class="col-sm-10">
                                                                                            <asp:LinkButton ID="lbtnSer1" OnClick="lbtnSer1_Click" CausesValidation="false" runat="server">
                                                                                                    <span class="glyphicon glyphicon-search" aria-hidden="true"  ></span>
                                                                                            </asp:LinkButton>
                                                                                        </div>
                                                                                    </div>
                                                                                </div>
                                                                            </asp:Panel>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="col-sm-5 padding0">
                                                            <div class="panel panel-default">
                                                                <div class="panel panel-body">
                                                                    <div class="row paddingbottom1">
                                                                        <asp:Panel runat="server" DefaultButton="lbtnSer2">
                                                                            <div class="col-sm-12">
                                                                                <div class="col-sm-11 padding0">
                                                                                    <div class="row">
                                                                                        <div class="col-sm-12">
                                                                                            <div class="col-sm-6 padding0 labelText1">
                                                                                                <div class="col-sm-4 labelText1">
                                                                                                    Invoice #
                                                                                                </div>
                                                                                                <div class="col-sm-7 padding0">
                                                                                                    <asp:TextBox ID="txtInvNo" AutoPostBack="true" OnTextChanged="txtInvNo_TextChanged" runat="server" CssClass="form-control"></asp:TextBox>
                                                                                                </div>
                                                                                                <div class="col-sm-1 paddingLeft3 paddingRight0">
                                                                                                    <asp:LinkButton ID="lbtnSeInvNo" OnClick="lbtnSeInvNo_Click" CausesValidation="false" runat="server">
                                                                                                    <span class="glyphicon glyphicon-search" aria-hidden="true"  ></span>
                                                                                                    </asp:LinkButton>
                                                                                                </div>
                                                                                            </div>
                                                                                            <div class="col-sm-6 padding0 labelText1">
                                                                                                <div class="col-sm-4 labelText1">
                                                                                                    Delivery #
                                                                                                </div>
                                                                                                <div class="col-sm-6 padding0">
                                                                                                    <asp:TextBox ID="txtDelNo" AutoPostBack="true" OnTextChanged="txtDelNo_TextChanged" runat="server" CssClass="form-control"></asp:TextBox>
                                                                                                </div>
                                                                                                <div class="col-sm-1 paddingLeft3 paddingRight0">
                                                                                                    <asp:LinkButton ID="lbtnSeDelNo" OnClick="lbtnSeDelNo_Click" CausesValidation="false" runat="server">
                                                                                                    <span class="glyphicon glyphicon-search" aria-hidden="true"  ></span>
                                                                                                    </asp:LinkButton>
                                                                                                </div>
                                                                                            </div>
                                                                                        </div>
                                                                                    </div>
                                                                                    <div class="row">
                                                                                        <div class="col-sm-12">
                                                                                            <div class="col-sm-6 padding0 labelText1">
                                                                                                <div class="col-sm-4 labelText1">
                                                                                                    Serial #
                                                                                                </div>
                                                                                                <div class="col-sm-7 padding0">
                                                                                                    <asp:TextBox ID="txtSer" AutoPostBack="true" OnTextChanged="txtSer_TextChanged" runat="server" CssClass="form-control"></asp:TextBox>
                                                                                                </div>
                                                                                                <div class="col-sm-1 paddingLeft3 paddingRight0">
                                                                                                    <asp:LinkButton Visible="true" ID="lbtnSeSer" OnClick="lbtnSeSer_Click" CausesValidation="false" runat="server">
                                                                                                    <span class="glyphicon glyphicon-search" aria-hidden="true"  ></span>
                                                                                                    </asp:LinkButton>
                                                                                                </div>
                                                                                            </div>
                                                                                            <div class="col-sm-6 padding0 labelText1">
                                                                                                <div class="col-sm-5 labelText1">
                                                                                                </div>
                                                                                                <div class="col-sm-6 padding0">
                                                                                                </div>
                                                                                                <div class="col-sm-1 paddingLeft3 paddingRight0">
                                                                                                </div>
                                                                                            </div>
                                                                                        </div>
                                                                                    </div>
                                                                                </div>
                                                                                <div class="col-sm-1 padding0">
                                                                                    <div class="row buttonRow">
                                                                                        <div class="col-sm-1">
                                                                                        </div>
                                                                                        <div class="col-sm-7 padding0">
                                                                                            <asp:LinkButton ID="lbtnSer2" CausesValidation="false" runat="server" OnClick="lbtnSer2_Click">
                                                                                                    <span class="glyphicon glyphicon-search" aria-hidden="true"  ></span>
                                                                                            </asp:LinkButton>
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
                                                <div class="row">
                                                    <div class="col-sm-12">
                                                        <div class="col-sm-6 padding0">
                                                            <div class="panel panel-default">
                                                                <div class="panel panel-heading">
                                                                    <strong><b>Invoice</b></strong>
                                                                </div>
                                                                <div class="panel panel-body padding0">
                                                                    <div style="height: 120px; overflow-x: hidden; overflow-y: auto;">
                                                                        <asp:GridView ID="dgvInv" CssClass="table table-hover table-striped"
                                                                            ShowHeaderWhenEmpty="true" EmptyDataText="No data found..."
                                                                            runat="server" GridLines="None" PagerStyle-CssClass="cssPager"
                                                                            AutoGenerateColumns="False">
                                                                            <Columns>
                                                                                <asp:TemplateField HeaderText="">
                                                                                    <ItemTemplate>
                                                                                        <div class="margin-top-3">
                                                                                            <asp:LinkButton ID="lbtnInvoiceSelect" OnClick="lbtnInvoiceSelect_Click" Width="8px" CausesValidation="false" runat="server"> 
                                                                                                <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                                                            </asp:LinkButton>
                                                                                        </div>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="Inv #">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblsah_inv_no"  Text='<%# Bind("sah_inv_no") %>' runat="server"></asp:Label>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                 <asp:TemplateField HeaderText="Inv TP">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblsah_inv_tp"  Text='<%# Bind("sah_inv_tp") %>' runat="server"></asp:Label>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="Inv Date">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblsah_dt"  Text='<%# Bind("sah_dt","{0:dd/MMM/yyyy}") %>' runat="server"></asp:Label>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="Ref #">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblsah_ref_doc"  Text='<%# Bind("sah_ref_doc") %>' runat="server"></asp:Label>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="Executive">
                                                                                    <ItemTemplate>
                                                                                        <%-- Executive --%>
                                                                                        <asp:Label ID="lblsah_sales_ex_cd"  Text='<%# Bind("sah_sales_ex_cd") %>' runat="server"></asp:Label>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="Customer">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblsah_cus_cd"  Text='<%# Bind("sah_cus_cd") %>' runat="server"></asp:Label>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="Status">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblsah_stus" Text='<%# Bind("mss_desc") %>'  runat="server"></asp:Label>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                            </Columns>
                                                                            <PagerStyle CssClass="cssPager"></PagerStyle>
                                                                        </asp:GridView>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="col-sm-6 padding0">
                                                            <div class="panel panel-default">
                                                                <div class="panel panel-heading">
                                                                    <strong><b>Invoice Details</b></strong>
                                                                </div>
                                                                <div class="panel panel-body">
                                                                    <div style="height: 120px; overflow-x: hidden; overflow-y: auto;">
                                                                        <asp:GridView ID="dgvInvDet" CssClass="table table-hover table-striped"
                                                                            ShowHeaderWhenEmpty="true" EmptyDataText="No data found..."
                                                                            runat="server" GridLines="None" PagerStyle-CssClass="cssPager"
                                                                            AutoGenerateColumns="False">
                                                                            <Columns>
                                                                                <asp:TemplateField HeaderText="Item Code">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblsad_itm_cd" Text='<%# Bind("sad_itm_cd") %>' runat="server"></asp:Label>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="Qty">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblsad_qty" Text='<%# Bind("sad_qty","{0:N2}") %>' runat="server"></asp:Label>
                                                                                    </ItemTemplate>
                                                                                    <ItemStyle CssClass="gridHeaderAlignRight"/>
                                                                                    <HeaderStyle CssClass="gridHeaderAlignRight"/>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="Unit Rate">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblsah_dt" Text='<%# Bind("sad_unit_rt","{0:N2}") %>' runat="server"></asp:Label>
                                                                                    </ItemTemplate>
                                                                                    <ItemStyle CssClass="gridHeaderAlignRight"/>
                                                                                    <HeaderStyle CssClass="gridHeaderAlignRight"/>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="Unit Amt.">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblsah_ref_doc" Text='<%# Bind("sad_unit_amt","{0:N2}") %>' runat="server"></asp:Label>
                                                                                    </ItemTemplate>
                                                                                    <ItemStyle CssClass="gridHeaderAlignRight"/>
                                                                                    <HeaderStyle CssClass="gridHeaderAlignRight"/>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="Disc Rate">
                                                                                    <ItemTemplate>
                                                                                        <%-- Executive --%>
                                                                                        <asp:Label ID="lblsah_sales_ex_cd" Text='<%# Bind("sad_disc_rt","{0:N2}") %>' runat="server"></asp:Label>
                                                                                    </ItemTemplate>
                                                                                    <ItemStyle CssClass="gridHeaderAlignRight"/>
                                                                                    <HeaderStyle CssClass="gridHeaderAlignRight"/>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="Disc Amt.">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblsah_cus_cd" Text='<%# Bind("sad_disc_amt","{0:N2}") %>' runat="server"></asp:Label>
                                                                                    </ItemTemplate>
                                                                                    <ItemStyle CssClass="gridHeaderAlignRight"/>
                                                                                    <HeaderStyle CssClass="gridHeaderAlignRight"/>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="Tax Amt.">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblsah_cus_cd" Text='<%# Bind("sad_itm_tax_amt","{0:N2}") %>' runat="server"></asp:Label>
                                                                                    </ItemTemplate>
                                                                                        <ItemStyle CssClass="gridHeaderAlignRight"/>
                                                                                    <HeaderStyle CssClass="gridHeaderAlignRight"/>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="Total Amt.">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblsah_cus_cd" Text='<%# Bind("sad_tot_amt","{0:N2}") %>' runat="server"></asp:Label>
                                                                                    </ItemTemplate>
                                                                                    <ItemStyle CssClass="gridHeaderAlignRight"/>
                                                                                    <HeaderStyle CssClass="gridHeaderAlignRight"/>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="Pr. Book">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblsah_cus_cd" Text='<%# Bind("sad_pbook") %>' runat="server"></asp:Label>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="P.B. Level">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblsah_cus_cd" Text='<%# Bind("sad_pb_lvl") %>' runat="server"></asp:Label>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                 <asp:TemplateField HeaderText="Warr. Per" Visible="false">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblsah_cus_cd" Text='<%# Bind("sad_warr_period") %>' runat="server"></asp:Label>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                 <asp:TemplateField HeaderText="War. Rem." Visible="false">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblsah_cus_cd" Text='<%# Bind("sad_warr_remarks") %>' runat="server"></asp:Label>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                            </Columns>
                                                                            <PagerStyle CssClass="cssPager"></PagerStyle>
                                                                        </asp:GridView>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                </div>
                                                <div class="row">
                                                    <div class="col-sm-12">
                                                        <div class="panel panel-default">
                                                            <div class="panel panel-heading">
                                                                <strong><b>Delivery Oredr Details</b></strong>
                                                            </div>
                                                            <div class="panel panel-body">
                                                                <div class="" style="height:100px; overflow-y:auto">
                                                                    <asp:GridView ID="dgvDel" CssClass="table table-hover table-striped"
                                                                        ShowHeaderWhenEmpty="true" EmptyDataText="No data found..."
                                                                        runat="server" GridLines="None" PagerStyle-CssClass="cssPager"
                                                                        AutoGenerateColumns="False">
                                                                        <Columns>
                                                                            <asp:TemplateField HeaderText="Company">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblith_com" Text='<%# Bind("ith_com") %>' Width="60px" runat="server"></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Doc No">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblith_doc_no" Text='<%# Bind("ith_doc_no") %>' Width="120px" runat="server"></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Doc Date">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblith_doc_date" Text='<%# Bind("ith_doc_date","{0:dd/MMM/yyyy}") %>' Width="80px" runat="server"></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Tra. Loc">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblith_loc" Width="60px" Text='<%# Bind("ith_loc") %>' runat="server"></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Oth. Loc">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblith_oth_loc" Width="60px" Text='<%# Bind("ith_oth_loc") %>' runat="server"></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Other Doc #">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblith_job_no" Width="100px" Text='<%# Bind("ith_oth_docno") %>' runat="server"></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Item Code">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lbliti_itm_cd" Width="80px" Text='<%# Bind("iti_itm_cd") %>' runat="server"></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Item Description">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lbliti_itm_cd" Width="220px" Text='<%# Bind("mi_shortdesc") %>' runat="server"></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Qty">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lbliti_qty" Width="80px" Text='<%# Bind("iti_qty","{0:N2}") %>' runat="server"></asp:Label>
                                                                                </ItemTemplate>
                                                                                <ItemStyle CssClass="gridHeaderAlignRight" />
                                                                                <HeaderStyle CssClass="gridHeaderAlignRight" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="UOM">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lbliti_qty" Width="50px" Text='<%# Bind("mi_itm_uom") %>' runat="server"></asp:Label>
                                                                                </ItemTemplate>
                                                                                <ItemStyle CssClass="gridHeaderAlignRight" />
                                                                                <HeaderStyle CssClass="gridHeaderAlignRight" />
                                                                            </asp:TemplateField>
                                                                             <asp:TemplateField HeaderText="">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblTemp" Width="20px" Text='' runat="server"></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                        </Columns>
                                                                        <PagerStyle CssClass="cssPager"></PagerStyle>
                                                                    </asp:GridView>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-sm-12">
                                                        <div class="panel panel-default">
                                                            <div class="panel panel-heading">
                                                                <strong><b>Serial Details</b></strong>
                                                            </div>
                                                            <div class="panel panel-body padding0">
                                                                <div class="GrdHeight">
                                                                    <asp:GridView ID="dgvSerial" CssClass="table table-hover table-striped"
                                                                        ShowHeaderWhenEmpty="true" EmptyDataText="No data found..."
                                                                        runat="server" GridLines="None" PagerStyle-CssClass="cssPager"
                                                                        AutoGenerateColumns="False">
                                                                        <Columns>
                                                                            <asp:TemplateField HeaderText="Item Code">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblIns_itm_cd" Text='<%# Bind("Ins_itm_cd") %>' runat="server"></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Item Description">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lbltmpItmDesc" Text='<%# Bind("tmpItmDesc") %>' runat="server"></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Status">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblIns_itm_stus" Text='<%# Bind("tmpItmStsDesc") %>' runat="server"></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Item Type">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblsah_ref_doc" Text='<%# Bind("tmpItmTp") %>'  runat="server"></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Serial #">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblsah_sales_ex_cd" Text='<%# Bind("Ins_ser_1") %>' runat="server"></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Other Serial #">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblsah_cus_cd" Text='<%# Bind("Ins_ser_2") %>' runat="server"></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Waranty #">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblsah_stus" Text='<%# Bind("Ins_warr_no") %>' runat="server"></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                        </Columns>
                                                                        <PagerStyle CssClass="cssPager"></PagerStyle>
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
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <asp:UpdatePanel runat="server" ID="update">
        <ContentTemplate>
            <asp:Button ID="btn1" runat="server" Text="Button" Style="display: none;" />
            <asp:ModalPopupExtender ID="PopupSearch" runat="server" Enabled="True" TargetControlID="btn1"
                PopupControlID="pnlSearch" PopupDragHandleControlID="PopupHeader" Drag="true" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>
        </ContentTemplate>
    </asp:UpdatePanel>

    <asp:Panel runat="server" ID="pnlSearch" DefaultButton="lbtnSearch">
        <asp:ScriptManagerProxy ID="ScriptManagerProxy17" runat="server"></asp:ScriptManagerProxy>
        <asp:UpdatePanel runat="server">
            <ContentTemplate>
                <div runat="server" id="test" class="panel panel-default height350 width700">
                    <asp:Label ID="lblvalue" runat="server" Text="Label" Visible="false"></asp:Label>
                    <div class="panel panel-default">
                        <div class="panel-heading height30" id="divSearchheader">
                            <div class="col-sm-11">
                            </div>
                            <div class="col-sm-1">
                                <asp:LinkButton ID="btnCloseSearchMP" runat="server" OnClick="btnClose_Click">
                             <span class="glyphicon glyphicon-remove" aria-hidden="true">Close</span>
                                </asp:LinkButton>
                            </div>
                        </div>
                        <div class="panel-body">
                            <div class="col-sm-12" id="Div10" runat="server">
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
                                        <asp:ScriptManagerProxy ID="ScriptManagerProxy10" runat="server"></asp:ScriptManagerProxy>
                                        <asp:UpdatePanel ID="UpdatePanel10" runat="server">
                                            <ContentTemplate>
                                                <div class="col-sm-3 paddingRight5">
                                                    <asp:DropDownList ID="cmbSearchbykey" runat="server" class="form-control">
                                                    </asp:DropDownList>
                                                </div>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                        <div class="col-sm-2 labelText1">
                                            Search by word
                                        </div>
                                        <div class="col-sm-4 paddingRight5">
                                            <asp:TextBox ID="txtSearchbyword" CausesValidation="false" placeholder="Search by word" class="form-control" AutoPostBack="true" runat="server" OnTextChanged="lbtnSearch_Click"></asp:TextBox>
                                        </div>
                                        <asp:ScriptManagerProxy ID="ScriptManagerProxy9" runat="server"></asp:ScriptManagerProxy>
                                        <asp:UpdatePanel ID="UpdatePanel9" runat="server">
                                            <ContentTemplate>
                                                <div class="col-sm-1 paddingLeft0">
                                                    <asp:LinkButton ID="lbtnSearch" runat="server" OnClick="lbtnSearch_Click">
                                            <span class="glyphicon glyphicon-search" aria-hidden="true"  ></span>
                                                    </asp:LinkButton>
                                                </div>
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:AsyncPostBackTrigger ControlID="txtSearchbyword" />
                                            </Triggers>
                                        </asp:UpdatePanel>
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
                                        <asp:ScriptManagerProxy ID="ScriptManagerProxy4" runat="server"></asp:ScriptManagerProxy>
                                        <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                                            <ContentTemplate>
                                                <asp:GridView ID="dgvResult" CausesValidation="false" AllowPaging="true" runat="server" GridLines="None" ShowHeaderWhenEmpty="true" 
                                                    CssClass="table table-hover table-striped" PageSize="10" PagerStyle-CssClass="cssPager" OnPageIndexChanging="dgvResult_PageIndexChanging" 
                                                    OnSelectedIndexChanged="dgvResult_SelectedIndexChanged">
                                                    <Columns>
                                                        <asp:ButtonField Text="Select" CommandName="Select" ItemStyle-Width="10" />
                                                    </Columns>
                                                </asp:GridView>
                                            </ContentTemplate> 
                                        </asp:UpdatePanel>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </asp:Panel>
    <%--<asp:UpdatePanel runat="server">
        <ContentTemplate>
            <asp:Panel runat="server" ID="pnlSearch" DefaultButton="lbtnSearch">
                <div runat="server" id="test" class="panel panel-primary" style="width: 700px;">
                    <div class="panel panel-default" style="height: 330px;">
                        <div class="panel-heading" style="height: 25px;">
                            <div class="col-sm-11"></div>
                            <div class="col-sm-1">
                                <asp:LinkButton ID="btnClose" runat="server" OnClick="btnClose_Click">
                             <span class="glyphicon glyphicon-remove"  aria-hidden="true">Close</span>
                                </asp:LinkButton>
                                 <asp:LinkButton ID="btnClosePop" Visible="false" runat="server" OnClick="btnClose_Click">
                             <span class="glyphicon glyphicon-remove"  aria-hidden="true">Close</span>
                                </asp:LinkButton>
                            </div>
                        </div>
                        <div class="panel-body">
                            <div class="row">
                                <div class="col-sm-12">
                                    <div class="col-sm-2 labelText1">
                                        Search by key
                                    </div>
                                    <asp:ScriptManagerProxy ID="ScriptManagerProxy10" runat="server"></asp:ScriptManagerProxy>
                                    <asp:UpdatePanel ID="UpdatePanel10" runat="server">
                                        <ContentTemplate>
                                            <div class="col-sm-3 paddingRight5">
                                                <asp:DropDownList ID="cmbSearchbykey" runat="server" class="form-control" AutoPostBack="false">
                                                </asp:DropDownList>
                                            </div>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                    <div class="col-sm-2 labelText1">
                                        Search by word
                                    </div>
                                    <div class="col-sm-3 paddingRight5">
                                        <%--<asp:UpdatePanel runat="server">
                                            <ContentTemplate>
                                                <asp:TextBox ID="txtSearchbyword" OnTextChanged="lbtnSearch_Click" CausesValidation="false" class="form-control" AutoPostBack="true" runat="server"></asp:TextBox>
                                            <%--</ContentTemplate>
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
                                                <asp:AsyncPostBackTrigger ControlID="txtSearchbyword" />
                                            </Triggers>
                                    </asp:UpdatePanel>
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
                                    <asp:ScriptManagerProxy ID="ScriptManagerProxy8" runat="server"></asp:ScriptManagerProxy>
                                    <asp:UpdatePanel ID="UpdatePanel8" runat="server">
                                        <ContentTemplate>
                                            <asp:GridView ID="dgvResult" CausesValidation="false" runat="server" AllowPaging="True"
                                                GridLines="None" CssClass="table table-hover table-striped" PageSize="10" PagerStyle-CssClass="cssPager" ShowHeaderWhenEmpty="true"
                                                EmptyDataText="No data found..."
                                                OnPageIndexChanging="dgvResult_PageIndexChanging" OnSelectedIndexChanged="dgvResult_SelectedIndexChanged">
                                                <Columns>
                                                    <asp:ButtonField Text="Select" CommandName="Select" ItemStyle-Width="10" />
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
        </ContentTemplate>
    </asp:UpdatePanel>--%>
</asp:Content>
