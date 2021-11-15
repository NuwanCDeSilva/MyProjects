<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="CostingSheet.aspx.cs" Inherits="FF.AbansTours.CostingSheet" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
        .div_header {
            background-color: #b0b0b0;
            padding: 7px;
            color: #ffffff;
            font-size: inherit;
            font-weight: inherit;
            font-family: Arial, Helvetica, sans-serif;
            font-style: inherit;
            text-decoration: inherit;
            letter-spacing: 0.12em;
        }

        .div_Input {
            border: 1px #888888 solid;
            background-color: #919191;
        }
    </style>
    <script type="text/javascript">

        function calculate_discount(pvalue) {
            var DicPercentage = document.getElementById('txtDisPercentage').value;
            if (DicPercentage >= 100) {
                alert("please enter valid discount percentage");
            }
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
    <div class="row rowmargin0 col-md-12">
        <div class="panel panel-default">

            <div class="panel-body">
                <div class="row height5 col-md-12">
                </div>
                <div class="row">

                    <div class="col-sm-8">
                    </div>
                    <div class="col-sm-4 rowmargin0">
                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                            <ContentTemplate>

                                <asp:Button ID="btnCreate" Text="Save" CssClass="btn btn-success btn-xs" runat="server" Width="80px" OnClick="btnCreate_Click" />
                                <asp:Button ID="btnApprove" Text="Approve" CssClass="btn btn-success btn-xs" runat="server" Width="80px" Visible="false"
                                    OnClick="btnApprove_Click" />
                                <asp:Button ID="btnClear" Text="Clear" CssClass="btn btn-default btn-xs" runat="server" Width="80px" OnClick="btnClear_Click" />
                                <asp:Button ID="btnBack" Text="Back" runat="server" CssClass="btn btn-blurbg btn-xs" Width="80px" OnClick="btnBack_Click" />
                                <asp:Button ID="btnPrint" Text="Print" runat="server" CssClass="btn btn-blurbg btn-xs" Width="80px" OnClick="btnPrint_Click" />
                                <asp:ConfirmButtonExtender ID="ConfirmButtonExtender3" runat="server" TargetControlID="btnClear"
                                    ConfirmText="Do you want to clear?" ConfirmOnFormSubmit="false">
                                </asp:ConfirmButtonExtender>
                                <asp:ConfirmButtonExtender ID="ConfirmButtonExtender4" runat="server" TargetControlID="btnCreate"
                                    ConfirmText="Do you want to save?" ConfirmOnFormSubmit="false">
                                </asp:ConfirmButtonExtender>
                                <asp:ConfirmButtonExtender ID="ConfirmButtonExtender2" runat="server" TargetControlID="btnBack"
                                    ConfirmText="Do you want go to previous page?" ConfirmOnFormSubmit="false">
                                </asp:ConfirmButtonExtender>
                                <asp:ConfirmButtonExtender ID="ConfirmButtonExtender1" runat="server" TargetControlID="btnApprove"
                                    ConfirmText="Do you want to approve?" ConfirmOnFormSubmit="false">
                                </asp:ConfirmButtonExtender>

                                <div style="float: left; height: 22px; width: 100%; text-align: right">
                                    <asp:CheckBox ID="chkSendSMS" Text="Send SMS to Customer" runat="server" Checked="true"
                                        Visible="false" />
                                </div>
                            </ContentTemplate>
                            <Triggers>
                                <asp:PostBackTrigger ControlID="btnPrint" />
                            </Triggers>
                        </asp:UpdatePanel>

                    </div>
                </div>

            </div>
        </div>

    </div>

    <div class="row rowmargin0">
        <div class="col-md-12">
            <div class="panel panel-default">
                <div class="panel-heading pannelheading">
                    Costing Sheet
                </div>
                <div class="panel-body">
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <div class="col-md-6 ">
                                <div class="row">
                                    <div class="col-md-4 padding2">
                                        Client
                                    </div>
                                    <div class="col-md-8 padding2">
                                        <asp:Label ID="lblClient" Text="Client Details" runat="server" />
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-4 padding2">
                                        Enquiry ID
                                    </div>
                                    <div class="col-md-8 padding2">
                                        <asp:Label ID="lblEnquityID" Text="Client Details" runat="server" />
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-4 padding2">
                                        Cost Sheet Status
                                    </div>
                                    <div class="col-md-8 padding2">
                                        <asp:Label ID="lblStatus" Text="Cost Sheet Status" runat="server" ForeColor="Green" />
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-4 padding2">
                                        Cost Sheet Number
                                    </div>
                                    <div class="col-md-8 padding2">
                                        <asp:Label ID="lblCostSheetNumber" Text="Cost Sheet Status" runat="server" ForeColor="CORNFLOWERBLUE" />
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-6 padding2">
                                <div class="col-md-4 padding2">
                                    Date
                                </div>
                                <div class="col-md-8 padding2">
                                    <asp:TextBox ID="txtdate" Enabled="false" CssClass="input-xlarge focused" runat="server"
                                        onkeypress="return RestrictSpace()" Width="80%"></asp:TextBox>
                                    <asp:CalendarExtender ID="txtClearingDateExtender" runat="server" Enabled="True"
                                        Format="dd/MMM/yyyy" PopupButtonID="cal" TargetControlID="txtdate">
                                    </asp:CalendarExtender>
                                    <img alt="Calendar.." height="16" src="images/calendar.png" width="16" id="cal" style="cursor: pointer" />
                                </div>
                                <div class="col-md-4 padding2">
                                    REF
                                </div>
                                <div class="col-md-8 padding2">
                                    <asp:TextBox ID="txtReffNum" runat="server" AutoPostBack="true" OnTextChanged="txtReffNum_TextChanged" />
                                </div>
                                <div class="col-md-4 padding2">
                                    Total PAX
                                </div>
                                <div class="col-md-8 padding2">
                                    <asp:TextBox ID="txtTotalPAX" onkeydown="return jsDecimals(event);" runat="server" OnTextChanged="txtTotalPAX_TextChanged" />
                                </div>
                                <div class="col-md-4 padding2">
                                </div>
                                <div class="col-md-8 padding2">
                                </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>
    </div>



    <div class="row rowmargin0">
        <div class="col-md-12">
            <div class="panel panel-default">
                <div class="panel-heading pannelheading">
                    Items
                </div>
                <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                    <ContentTemplate>
                        <div class="panel-body padding0">
                            <div class="col-md-11 padding2">
                                <div class="col-md-12 height40 padding2 ">
                                    <div class="col-md-1 padding2">
                                        Charge Code
                                    <asp:DropDownList ID="ddlCostType" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlCostType_SelectedIndexChanged"
                                        Width="90%">
                                    </asp:DropDownList>
                                    </div>
                                    <div class="col-md-2 padding2">
                                        Sevice
                                    <asp:TextBox ID="txtCostSubType" runat="server" AutoPostBack="true" OnTextChanged="txtCostSubType_TextChanged"
                                        Style="text-align: right" Width="70%" />
                                        <asp:ImageButton ID="btnChargeType" runat="server" ImageAlign="Middle" ImageUrl="~/Images/icon_search.png"
                                            OnClick="btnChargeType_Click" />
                                        <asp:ImageButton ID="Imgbtncost" runat="server" ImageAlign="Middle" ImageUrl="~/Images/Details.png" OnClick="Imgbtncost_Click" />
                                    </div>
                                    <div class="col-md-1 padding2 width10">
                                        Currency
                                    <asp:DropDownList ID="ddlItmCurncy" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlItmCurncy_SelectedIndexChanged"
                                        Width="60%">
                                    </asp:DropDownList>
                                        <asp:Label ID="lblItemExRate" runat="server" ForeColor="HOTPINK" Text="0.00" Width="30%" />
                                    </div>
                                    <div class="col-md-1 padding2 width6">
                                        PAX
                                    <asp:TextBox ID="txtPAX" runat="server" onkeydown="return jsDecimals(event);" AutoPostBack="false" Style="text-align: right" Width="80%" OnTextChanged="txtPAX_TextChanged" />
                                    </div>
                                    <div class="col-md-1 padding2">
                                        FARE
                                    <asp:TextBox ID="txtUSD" runat="server" onkeydown="return jsDecimals(event);" AutoPostBack="true" OnTextChanged="txtUSD_TextChanged"
                                        Style="text-align: right" Width="90%" />
                                    </div>
                                    <div class="col-md-1 padding2">
                                        TAX
                                    <asp:TextBox ID="txtTAX" runat="server" onkeydown="return jsDecimals(event);" AutoPostBack="true" OnTextChanged="txtTAX_TextChanged"
                                        ReadOnly="True" Style="text-align: right" Width="90%">00</asp:TextBox>
                                    </div>
                                    <div class="col-md-1 padding2">
                                        Total
                                    <asp:TextBox ID="txtTotal" runat="server" onkeydown="return jsDecimals(event);" AutoPostBack="true" OnTextChanged="txtTotal_TextChanged"
                                        ReadOnly="True" Style="text-align: right" Width="90%" />
                                    </div>
                                    <div class="col-md-1 padding2">
                                        Markup
                                    <asp:TextBox ID="txtItemMarkup" runat="server" onkeydown="return jsDecimals(event);" AutoPostBack="true" OnTextChanged="txtItemMarkup_TextChanged"
                                        Style="text-align: right" Width="90%">00</asp:TextBox>
                                    </div>
                                    <div class="col-md-1 padding2">
                                        Total LKR
                                    <asp:TextBox ID="txtTotalLKR" runat="server" onkeydown="return jsDecimals(event);" AutoPostBack="true" ForeColor="MEDIUMVIOLETRED"
                                        OnTextChanged="txtTotalLKR_TextChanged" ReadOnly="True" Style="text-align: right"
                                        Width="90%">00</asp:TextBox>
                                    </div>
                                    <div class="col-md-2 padding2">
                                        Remarks
                                    <asp:TextBox ID="txtRemark" runat="server" OnTextChanged="txtRemark_TextChanged" Width="90%" />
                                    </div>
                                </div>
                                <div class="col-md-12 height40">
                                    <div class="col-md-1 padding2">
                                    </div>
                                    <div class="col-md-3 padding2">
                                        <asp:TextBox ID="txtSubCostDesc" Visible="false" runat="server" />
                                    </div>
                                    <div class="col-md-8 padding2">
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-1 padding2">
                                &nbsp;
                                    <asp:ImageButton ID="btnAddtoGrid" runat="server" ImageUrl="~/images/stat-down.png"
                                        OnClick="btnAddtoGrid_Click" Width="20px" />
                                <asp:Button ID="btnAddtoGrid2" runat="server" Width="50px" Height="50px" OnClick="btnAddtoGrid2_Click"
                                    Text="Add" />
                            </div>
                            <%--<asp:GridView ID="dgvCostSheet"  ShowHeader="true"  runat="server" Visible="true" AutoGenerateColumns="False" OnRowCommand="dgvCostSheet_RowCommand"
                                OnRowDeleting="dgvCostSheet_RowDeleting" OnRowEditing="dgvCostSheet_RowEditing" EmptyDataText="No data found..." ShowHeaderWhenEmpty="True" GridLines="None">--%>
                            <asp:GridView ID="dgvCostSheet" ShowHeader="true" runat="server" GridLines="None" ShowHeaderWhenEmpty="True"
                                EmptyDataText="No data found..." AutoGenerateColumns="False" OnRowCommand="dgvCostSheet_RowCommand"
                                OnRowDeleting="dgvCostSheet_RowDeleting" OnRowEditing="dgvCostSheet_RowEditing">
                                <RowStyle Height="10px" />
                                <Columns>
                                    <asp:TemplateField Visible="false" HeaderText="Seq" ItemStyle-Height="50px">
                                        <ItemTemplate>
                                            <asp:Label ID="lblQCD_SEQ" runat="server" Text='<%# Bind("QCD_SEQ") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField Visible="false" HeaderText="SeqLocal" ItemStyle-Height="50px">
                                        <ItemTemplate>
                                            <asp:Label ID="lblQCD_SEQLocal" runat="server" Text='<%# Bind("LocalSeq") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField Visible="false" HeaderText="Cost Number">
                                        <ItemTemplate>
                                            <asp:Label ID="lblQCD_COST_NO" runat="server" Text='<%# Bind("QCD_COST_NO") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Category">
                                        <ItemTemplate>
                                            <asp:Label ID="lblQCD_CAT" runat="server" Text='<%# Bind("QCD_CAT") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Width="100px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Sub Category">
                                        <ItemTemplate>
                                            <asp:Label ID="lblQCD_SUB_CATE" runat="server" Text='<%# Bind("QCD_SUB_CATE") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Width="100px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Description">
                                        <ItemTemplate>
                                            <asp:Label ID="lblQCD_SUB_CATEDESC" runat="server" Text='<%# Bind("QCD_DESC") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Width="100px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField Visible="false" HeaderText="Description">
                                        <ItemTemplate>
                                            <asp:Label ID="lblQCD_DESC" runat="server" Text='<%# Bind("QCD_DESC") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Width="150px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Currency">
                                        <ItemTemplate>
                                            <asp:Label ID="lblQCD_CURR" runat="server" Text='<%# Bind("QCD_CURR") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField Visible="false" HeaderText="Exchange Rate">
                                        <ItemTemplate>
                                            <asp:Label ID="lblQCD_EX_RATE" runat="server" Text='<%# Bind("QCD_EX_RATE") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Qty">
                                        <ItemTemplate>
                                            <asp:Label ID="lblQCD_QTY" runat="server" Text='<%# Bind("QCD_QTY") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Width="50px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Unit Cost">
                                        <ItemTemplate>
                                            <asp:Label ID="lblQCD_UNIT_COST" runat="server" Text='<%# Bind("QCD_UNIT_COST") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <HeaderStyle Width="100px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="TAX">
                                        <ItemTemplate>
                                            <asp:Label ID="lblQCD_TAX" runat="server" Text='<%# Bind("QCD_TAX") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <HeaderStyle Width="50px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Total Cost">
                                        <ItemTemplate>
                                            <asp:Label ID="lblQCD_TOT_COST" runat="server" Text='<%# Bind("QCD_TOT_COST") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <HeaderStyle Width="100px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Markup">
                                        <ItemTemplate>
                                            <asp:Label ID="lblQCD_MARKUP" runat="server" Text='<%# Bind("QCD_MARKUP") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <HeaderStyle Width="50px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Total Cost(LKR)">
                                        <ItemTemplate>
                                            <asp:Label ID="lblQCD_TOT_LOCAL" runat="server" Text='<%# Bind("QCD_AF_MARKUP") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <HeaderStyle Width="100px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField Visible="false" HeaderText="After Markup">
                                        <ItemTemplate>
                                            <asp:Label ID="lblQCD_AF_MARKUP" runat="server" Text='<%# Bind("QCD_AF_MARKUP") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Remark">
                                        <ItemTemplate>
                                            <asp:Label ID="lblQCD_RMK" runat="server" Text='<%# Bind("QCD_RMK") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Width="140px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="btndelete" runat="server" CommandArgument="<%# Container.DisplayIndex %>"
                                                CommandName="delete" ImageUrl="~/images/deleteicon.png" ToolTip="Delete.." ImageAlign="Middle"
                                                OnClientClick="return confirm('Are you sure you want to delete?');" />
                                        </ItemTemplate>
                                        <HeaderStyle CssClass="bodycolordarkgreen" HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" />
                                        <HeaderStyle Width="50px" />
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                            <div class="row">
                                &nbsp;
                            </div>
                            <div class="row">
                                &nbsp;
                            </div>
                            <div class="row">
                                <div class="col-md-4 padding2">
                                </div>
                                <div class="col-md-1 padding2">
                                    Total FARE
                                    <asp:TextBox ID="txtTotalUSD" runat="server" AutoPostBack="true" ReadOnly="true"
                                        Style="text-align: right" Width="90%">00</asp:TextBox>
                                </div>
                                <div class="col-md-1 padding2">
                                    Total TAX
                                    <asp:TextBox ID="txtTotalTax" runat="server" AutoPostBack="true" ReadOnly="true"
                                        Style="text-align: right" Width="90%">00</asp:TextBox>
                                </div>
                                <div class="col-md-1 padding2">
                                    Total Cost
                                    <asp:TextBox ID="txtTotalTotal" runat="server" AutoPostBack="true" ReadOnly="true"
                                        Style="text-align: right" Width="90%">00</asp:TextBox>
                                </div>
                                <div class="col-md-2 padding2">
                                    Total Amount(LkR)
                                    <asp:TextBox ID="txtTotalCostMain" onkeydown="return jsDecimals(event);" runat="server" AutoPostBack="true" OnTextChanged="txtTotalLKR_TextChanged"
                                        ReadOnly="true" Style="text-align: right" Width="90%">00</asp:TextBox>
                                </div>
                                <div class="col-md-2 padding2">
                                    Remark
                                    <asp:TextBox ID="TextBox6" runat="server" AutoPostBack="true" OnTextChanged="txtRemark_TextChanged"
                                        Width="90%" />
                                </div>
                                <div class="col-md-1 padding2">
                                </div>
                            </div>
                            <div class="row" style="background: LIGHTGREY;">
                                <div class="col-md-3 padding2">
                                </div>
                                <div class="col-md-2 padding2">
                                    Markup
                                </div>
                                <div class="col-md-1 padding2">
                                </div>
                                <div class="col-md-1 padding2">
                                </div>
                                <div class="col-md-2 padding2">
                                    <asp:TextBox ID="txtMarkUpMain" onkeydown="return jsDecimals(event);" runat="server" Width="90%" AutoPostBack="true" Style="text-align: right"
                                        OnTextChanged="txtMarkUpMain_TextChanged">00</asp:TextBox>
                                </div>
                                <div class="col-md-2 padding2">
                                    <asp:TextBox ID="TextBox10" onkeydown="return jsDecimals(event);" Text="Abans GRP RTE" ReadOnly="true" runat="server" Width="90%"
                                        AutoPostBack="true" OnTextChanged="txtRemark_TextChanged" />
                                </div>
                                <div class="col-md-1 padding2">
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-3 padding2">
                                </div>
                                <div class="col-md-2 padding2">
                                    Total
                                </div>
                                <div class="col-md-1 padding2">
                                </div>
                                <div class="col-md-1 padding2">
                                </div>
                                <div class="col-md-2 padding2">
                                    <asp:TextBox ID="txtTotalMain" onkeydown="return jsDecimals(event);" runat="server" Width="90%" AutoPostBack="true" Style="text-align: right"
                                        ReadOnly="True">00</asp:TextBox>
                                </div>
                                <div class="col-md-3 padding2">
                                </div>
                            </div>
                            <div class="row" style="background: LIGHTGREY;">
                                <div class="col-md-3 padding2">
                                </div>
                                <div class="col-md-2 padding2">
                                    Quote Per PAX
                                </div>
                                <div class="col-md-1 padding2">
                                </div>
                                <div class="col-md-1 padding2">
                                </div>
                                <div class="col-md-2 padding2">
                                    <asp:TextBox ID="txtPerPaxMain" onkeydown="return jsDecimals(event);" runat="server" Width="90%" AutoPostBack="true" Style="text-align: right"
                                        ReadOnly="True">00</asp:TextBox>
                                </div>
                                <div class="col-md-3 padding2">
                                </div>
                            </div>
                            <asp:HiddenField ID="hdfChargeDesc" runat="server" />
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>

    <div>
        <CR:CrystalReportSource ID="CrystalReportSource1" runat="server">
            <Report FileName="Reports_Module\Financial_Rep\CostingFormat_Report.rpt">
            </Report>
        </CR:CrystalReportSource>
        <asp:Panel ID="pnlReceiptPrint" runat="server" Width="950px" Height="600px" CssClass="ModalPopup">
            <div style="text-align: right; background-color: Silver;">
                <asp:UpdatePanel ID="upPrint" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                    <ContentTemplate>
                        <asp:ImageButton ID="Close" runat="server" ImageUrl="~/Images/uploadify-cancel.png"
                            ImageAlign="Middle" OnClick="Close_Click" />
                        <%--<asp:Button ID="Close" Text="Close" Width="80px" runat="server" OnClick="Close_Click" />--%>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
            <div>
                <iframe style="width: 950px; height: 600px;" id="irm1" src="Reports_Module/Financial_Rep/CostingFormatReport11.aspx"
                    runat="server"></iframe>
            </div>
        </asp:Panel>
        <asp:Button ID="btnMDprint" runat="server" Text="D3" Style="display: none" />
        <asp:ModalPopupExtender ID="mpReceiptPrint" runat="server" DynamicServicePath=""
            Enabled="True" PopupControlID="pnlReceiptPrint" TargetControlID="btnMDprint"
            BackgroundCssClass="modalBackground" PopupDragHandleControlID="pnlReceiptPrint">
        </asp:ModalPopupExtender>
    </div>


    <asp:ScriptManagerProxy ID="ScriptManagerProxy7" runat="server"></asp:ScriptManagerProxy>
    <asp:UpdatePanel ID="UpdatePanel8" runat="server">
        <ContentTemplate>
            <asp:Button ID="Button3" runat="server" Text="Button" Style="display: none;" />
            <asp:ModalPopupExtender ID="UserPopoup" runat="server" Enabled="True" TargetControlID="Button3"
                PopupControlID="pnlpopup" CancelControlID="btnClose" PopupDragHandleControlID="PopupHeader" Drag="true" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>

        </ContentTemplate>

    </asp:UpdatePanel>

    <asp:Panel runat="server" ID="pnlpopup" >
        <div runat="server" id="test" class="panel panel-default height350 width1000">
            <asp:Label ID="lblvalue" runat="server" Text="Label" Visible="false"></asp:Label>
            <div class="panel panel-default">
                <div class="panel-heading">
                    <asp:LinkButton ID="btnClose" runat="server">
                             <span class="glyphicon glyphicon-remove" aria-hidden="true">Close</span>
                    </asp:LinkButton>
                    <%--<span>Commen Search</span>--%>
                    <div class="col-sm-11">
                    </div>
                    <div class="col-sm-1">
                    </div>
                </div>
                <div class="panel-body">
                    <div class="col-sm-12" id="Div4" runat="server">
                        <div class="row">
                            <div class="col-sm-12">
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
                            <div class="col-sm-12 height5">
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-12">
                                <asp:ScriptManagerProxy ID="ScriptManagerProxy4" runat="server"></asp:ScriptManagerProxy>
                                <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                                    <ContentTemplate>
                                        <asp:GridView ID="grdResultsearch" CausesValidation="false" AutoGenerateColumns="false"
                                            runat="server" GridLines="None" CssClass="table table-hover" PageSize="7" PagerStyle-CssClass="cssPager" AllowPaging="True" OnSelectedIndexChanged="grdResultsearch_SelectedIndexChanged" OnPageIndexChanging="grdResult_PageIndexChanging">
                                            <Columns>
                                                <asp:ButtonField Text="select" CommandName="Select" ItemStyle-Width="10" />
                                                <asp:TemplateField HeaderText="Service provider">
                                                    <ItemTemplate>
                                                        <asp:Label ID="col_STC_SER_BY" runat="server" Text='<%# Bind("STC_SER_BY") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="Service provider">
                                                    <ItemTemplate>
                                                        <asp:Label ID="col_STC_CLS" runat="server" Text='<%# Bind("STC_CLS") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="Valid From">
                                                    <ItemTemplate>
                                                        <asp:Label ID="col_stc_frm_dt" runat="server" Text='<%# Bind("stc_frm_dt","{0:dd/MMM/yyyy}") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="Valid To">
                                                    <ItemTemplate>
                                                        <asp:Label ID="col_stc_to_dt" runat="server" Text='<%# Bind("stc_to_dt","{0:dd/MMM/yyyy}") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="Rate">
                                                    <ItemTemplate>
                                                        <asp:Label ID="col_stc_rt" runat="server" Text='<%# Bind("stc_rt","{0:n}") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
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
    </asp:Panel>

      <asp:ScriptManagerProxy ID="ScriptManagerProxy2" runat="server"></asp:ScriptManagerProxy>
    <asp:UpdatePanel ID="UpdatePanel4" runat="server">
        <ContentTemplate>
            <asp:Button ID="Button1" runat="server" Text="Button" Style="display: none;" />
            <asp:ModalPopupExtender ID="UserPopupMer" runat="server" Enabled="True" TargetControlID="Button1"
                PopupControlID="pnlpopup2" CancelControlID="btnClose" PopupDragHandleControlID="PopupHeader" Drag="true" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>

        </ContentTemplate>

    </asp:UpdatePanel>

    <asp:Panel runat="server" ID="pnlpopup2" >
        <div runat="server" id="Div1" class="panel panel-default height350 width1000">
            <asp:Label ID="Label1" runat="server" Text="Label" Visible="false"></asp:Label>
            <div class="panel panel-default">
                <div class="panel-heading">
                    <asp:LinkButton ID="LinkButton1" runat="server">
                             <span class="glyphicon glyphicon-remove" aria-hidden="true">Close</span>
                    </asp:LinkButton>
                    <%--<span>Commen Search</span>--%>
                    <div class="col-sm-11">
                    </div>
                    <div class="col-sm-1">
                    </div>
                </div>
                <div class="panel-body">
                    <div class="col-sm-12" id="Div2" runat="server">
                        <div class="row">
                            <div class="col-sm-12">
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
                            <div class="col-sm-12 height5">
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-12">
                                <asp:ScriptManagerProxy ID="ScriptManagerProxy6" runat="server"></asp:ScriptManagerProxy>
                                <asp:UpdatePanel ID="UpdatePanel11" runat="server">
                                    <ContentTemplate>
                                        <asp:GridView ID="grdMiscellaneous" CausesValidation="false" AutoGenerateColumns="false"
                                            runat="server" GridLines="None" CssClass="table table-hover" PageSize="7" PagerStyle-CssClass="cssPager" AllowPaging="True"  OnPageIndexChanging="grdResult_PageIndexChanging">
                                            <Columns>
                                                <asp:ButtonField Text="select" CommandName="Select" ItemStyle-Width="10" />
                                                <asp:TemplateField HeaderText="Service provider">
                                                    <ItemTemplate>
                                                        <asp:Label ID="col_ssm_ser_pro" runat="server" Text='<%# Bind("ssm_ser_pro") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="Valid From">
                                                    <ItemTemplate>
                                                        <asp:Label ID="col_ssm_frm_dt" runat="server" Text='<%# Bind("ssm_frm_dt","{0:dd/MMM/yyyy}") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="Valid To">
                                                    <ItemTemplate>
                                                        <asp:Label ID="col_ssm_to_dt" runat="server" Text='<%# Bind("ssm_to_dt","{0:dd/MMM/yyyy}") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="Rate">
                                                    <ItemTemplate>
                                                        <asp:Label ID="col_ssm_rt" runat="server" Text='<%# Bind("ssm_rt","{0:n}") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
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
    </asp:Panel>

          <asp:ScriptManagerProxy ID="ScriptManagerProxy1" runat="server"></asp:ScriptManagerProxy>
    <asp:UpdatePanel ID="UpdatePanel5" runat="server">
        <ContentTemplate>
            <asp:Button ID="Button2" runat="server" Text="Button" Style="display: none;" />
            <asp:ModalPopupExtender ID="UserAir" runat="server" Enabled="True" TargetControlID="Button2"
                PopupControlID="pnlpopup3" CancelControlID="btnClose" PopupDragHandleControlID="PopupHeader" Drag="true" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>

        </ContentTemplate>

    </asp:UpdatePanel>

    <asp:Panel runat="server" ID="Panel1" >
        <div runat="server" id="pnlpopup3" class="panel panel-default height350 width1000">
            <asp:Label ID="Label2" runat="server" Text="Label" Visible="false"></asp:Label>
            <div class="panel panel-default">
                <div class="panel-heading">
                    <asp:LinkButton ID="LinkButton2" runat="server">
                             <span class="glyphicon glyphicon-remove" aria-hidden="true">Close</span>
                    </asp:LinkButton>
                    <%--<span>Commen Search</span>--%>
                    <div class="col-sm-11">
                    </div>
                    <div class="col-sm-1">
                    </div>
                </div>
                <div class="panel-body">
                    <div class="col-sm-12" id="Div5" runat="server">
                        <div class="row">
                            <div class="col-sm-12">
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
                            <div class="col-sm-12 height5">
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-12">
                                <asp:ScriptManagerProxy ID="ScriptManagerProxy3" runat="server"></asp:ScriptManagerProxy>
                                <asp:UpdatePanel ID="UpdatePanel7" runat="server">
                                    <ContentTemplate>
                                        <asp:GridView ID="gridair" CausesValidation="false" AutoGenerateColumns="false"
                                            runat="server" GridLines="None" CssClass="table table-hover" PageSize="7" PagerStyle-CssClass="cssPager" AllowPaging="True" OnSelectedIndexChanged="gridair_SelectedIndexChanged" OnPageIndexChanging="grdResult_PageIndexChanging">
                                            <Columns>
                                                <asp:ButtonField Text="select" CommandName="Select" ItemStyle-Width="10" />
                                                <asp:TemplateField HeaderText="Service provider">
                                                    <ItemTemplate>
                                                        <asp:Label ID="col_SAC_SCV_BY" runat="server" Text='<%# Bind("SAC_SCV_BY") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="Valid From">
                                                    <ItemTemplate>
                                                        <asp:Label ID="col_SAC_CLS" runat="server" Text='<%# Bind("SAC_CLS") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="Valid From">
                                                    <ItemTemplate>
                                                        <asp:Label ID="col_sac_frm_dt" runat="server" Text='<%# Bind("sac_frm_dt","{0:dd/MMM/yyyy}") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="Valid To">
                                                    <ItemTemplate>
                                                        <asp:Label ID="col_sac_to_dt" runat="server" Text='<%# Bind("sac_to_dt","{0:dd/MMM/yyyy}") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="Rate">
                                                    <ItemTemplate>
                                                        <asp:Label ID="col_sac_rt" runat="server" Text='<%# Bind("sac_rt","{0:n}") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
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
    </asp:Panel>
</asp:Content>
