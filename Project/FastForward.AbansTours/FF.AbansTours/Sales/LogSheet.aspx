<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="LogSheet.aspx.cs" Inherits="FF.AbansTours.Sales.LogSheet" %>

<%@ Register Assembly="TimePicker" Namespace="MKB.TimePicker" TagPrefix="MKB" %>

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
                            <asp:Button ID="btnClear" Text="Clear" runat="server" Width="80px" OnClick="btnClear_Click" />
                            <asp:Button ID="btnBack" Text="Back" runat="server" Width="80px" OnClick="btnBack_Click" />
                            <asp:ConfirmButtonExtender ID="ConfirmButtonExtender4" runat="server" TargetControlID="btnCreate"
                                ConfirmText="Do you want to save?" ConfirmOnFormSubmit="false">
                            </asp:ConfirmButtonExtender>
                            <asp:ConfirmButtonExtender ID="ConfirmButtonExtender2" runat="server" TargetControlID="btnBack"
                                ConfirmText="Do you want go to previous page?" ConfirmOnFormSubmit="false">
                            </asp:ConfirmButtonExtender>
                            <asp:ConfirmButtonExtender ID="asd" runat="server" TargetControlID="btnClear"
                                ConfirmText="Do you want to clear?" ConfirmOnFormSubmit="false">
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
                        Log Sheet
                    </div>
                    <div class="panel-body">
                        <div class="row rowmargin0 col-md-12">
                            <div class="row">
                                <div class="col-md-6">
                                    <div class="col-md-4 padding2">
                                        Company
                                    </div>
                                    <div class="col-md-8 padding2">
                                        <asp:TextBox ID="txtCompany" Enabled="false" AutoPostBack="true" runat="server" Width="80%" OnTextChanged="txtCompany_TextChanged" />
                                        <asp:ImageButton ID="btnCompany" runat="server" ImageUrl="~/Images/icon_search.png" ImageAlign="Middle" OnClick="btnCompany_Click" />
                                        <asp:ImageButton ID="btnCompanyLoad" runat="server" ImageUrl="~/Images/LoadDetails.png" ImageAlign="Middle" OnClick="btnCompanyLoad_Click" />
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="col-md-4 padding2">
                                        PC
                                    </div>
                                    <div class="col-md-8 padding2">
                                        <asp:TextBox ID="txtPC" Enabled="false" AutoPostBack="true" runat="server" Width="80%" OnTextChanged="txtPC_TextChanged" />
                                        <asp:ImageButton ID="btnPC" runat="server" ImageUrl="~/Images/icon_search.png" ImageAlign="Middle" Visible="false" OnClick="btnPC_Click" />
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-6">
                                    <div class="col-md-4 padding2">
                                        Log Sheet No
                                    </div>
                                    <div class="col-md-8 padding2">
                                        <asp:TextBox ID="txtLogSheetNo" AutoPostBack="true" runat="server" Width="80%" OnTextChanged="txtLogSheetNo_TextChanged" />
                                        <asp:ImageButton ID="btnLogSheetNo" runat="server" ImageUrl="~/Images/icon_search.png" ImageAlign="Middle" OnClick="btnLogSheetNo_Click" />
                                        <asp:ImageButton ID="btnLogSheetNoLoad" runat="server" ImageUrl="~/Images/LoadDetails.png" ImageAlign="Middle" OnClick="btnLogSheetNoLoad_Click" />
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="col-md-4 padding2">
                                        Date
                                    </div>
                                    <div class="col-md-8 padding2">

                                        <asp:TextBox ID="txtDate" AutoPostBack="true" Enabled="false" CssClass="input-xlarge focused" runat="server" Width="80%" OnTextChanged="txtDate_TextChanged"></asp:TextBox>
                                        <%--   <asp:CalendarExtender ID="txtDateExtender" runat="server" Enabled="True" Format="dd/MMM/yyyy" PopupButtonID="calDate" TargetControlID="txtDate"></asp:CalendarExtender>
                                        <img alt="Calendar.." height="16" src="../images/calendar.png" width="16" id="calDate" style="cursor: pointer"/>--%>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-6">
                                    <div class="col-md-4 padding2">
                                        Enquiry ID
                                    </div>
                                    <div class="col-md-8 padding2">
                                        <asp:Label ID="lblEnquirySeq" Visible="false" Text="text" runat="server" />
                                        <asp:TextBox ID="txtEnquiryID" Enabled="false" AutoPostBack="true" runat="server" Width="80%" OnTextChanged="txtEnquiryID_TextChanged" />
                                        <asp:ImageButton ID="btntxtEnquiryID" runat="server" ImageUrl="~/Images/icon_search.png" ImageAlign="Middle" OnClick="btntxtEnquiryID_Click" />
                                        <asp:ImageButton ID="btntxtEnquiryIDLoad" runat="server" ImageUrl="~/Images/LoadDetails.png" ImageAlign="Middle" OnClick="btntxtEnquiryIDLoad_Click" />
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="col-md-4 padding2">
                                        Customer Code
                                    </div>
                                    <div class="col-md-8 padding2">
                                        <asp:TextBox ID="txtCustomerCode" AutoPostBack="true" runat="server" Width="80%" OnTextChanged="txtCustomerCode_TextChanged" />
                                        <asp:ImageButton ID="btnCustomerCode" runat="server" ImageUrl="~/Images/icon_search.png" ImageAlign="Middle" OnClick="btnCustomerCode_Click" />
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-6">
                                    <div class="col-md-4 padding2">
                                        Start Date
                                    </div>
                                    <div class="col-md-8 padding2">
                                        <asp:TextBox ID="txtStartDate" Enabled="false" AutoPostBack="true" CssClass="input-xlarge focused" runat="server" Width="40%" OnTextChanged="txtStartDate_TextChanged"></asp:TextBox>
                                        <asp:CalendarExtender ID="txtStartDateExtender" runat="server" Enabled="True" Format="dd/MMM/yyyy" PopupButtonID="calStartDate" TargetControlID="txtStartDate"></asp:CalendarExtender>
                                        <img alt="Calendar.." height="16" src="../images/calendar.png" width="16" id="calStartDate" style="cursor: pointer" />
                                        <MKB:TimeSelector ID="tmStartTime" Height="12px" runat="server" BorderStyle="None" DisplayButtons="False" DisplaySeconds="False" SelectedTimeFormat="TwentyFour"></MKB:TimeSelector>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="col-md-4 padding2">
                                        End Date
                                    </div>
                                    <div class="col-md-8 padding2">
                                        <asp:TextBox ID="txtEndDate" AutoPostBack="true" Enabled="false" CssClass="input-xlarge focused" runat="server" Width="40%" OnTextChanged="txtEndDate_TextChanged"></asp:TextBox>
                                        <asp:CalendarExtender ID="txtEndDateExtender" runat="server" Enabled="True" Format="dd/MMM/yyyy" PopupButtonID="caltxtEndDate" TargetControlID="txtEndDate"></asp:CalendarExtender>
                                        <img alt="Calendar.." height="16" src="../images/calendar.png" width="16" id="caltxtEndDate" style="cursor: pointer" />
                                        <MKB:TimeSelector ID="tmEndTime" Height="12px" runat="server" BorderStyle="None" DisplayButtons="False" DisplaySeconds="False" SelectedTimeFormat="TwentyFour"></MKB:TimeSelector>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-6">
                                    <div class="col-md-4 padding2">
                                        Driver Code
                                    </div>
                                    <div class="col-md-8 padding2">
                                        <asp:TextBox ID="txtDriverCode" Enabled="false" AutoPostBack="true" runat="server" Width="80%" OnTextChanged="txtDriverCode_TextChanged" />
                                        <asp:ImageButton ID="btnDriverCode" runat="server" ImageUrl="../Images/icon_search.png" ImageAlign="Middle" OnClick="btnDriverCode_Click" />
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="col-md-4 padding2">
                                        Guest
                                    </div>
                                    <div class="col-md-8 padding2">
                                        <asp:TextBox ID="txtGuest" AutoPostBack="false" runat="server" Width="80%" OnTextChanged="txtGuest_TextChanged" />
                                        <asp:ImageButton ID="btnGuest" runat="server" ImageUrl="~/Images/icon_search.png" Visible="false" ImageAlign="Middle" OnClick="btnGuest_Click" />
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-6">
                                    <div class="col-md-4 padding2">
                                        Fleet
                                    </div>
                                    <div class="col-md-8 padding2">
                                        <asp:TextBox ID="txtFleet" Enabled="false" AutoPostBack="true" runat="server" Width="80%" OnTextChanged="txtFleet_TextChanged" />
                                        <asp:ImageButton ID="btnFleet" runat="server" ImageUrl="~/Images/icon_search.png" ImageAlign="Middle" OnClick="btnFleet_Click" />
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="col-md-4 padding2">
                                        Remark
                                    </div>
                                    <div class="col-md-8 padding2">
                                        <asp:TextBox ID="txtRemark" AutoPostBack="false" runat="server" Width="80%" />
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-6">
                                    <div class="col-md-4 padding2">
                                        Invoice Mileage
                                    </div>
                                    <div class="col-md-8 padding2">
                                        <asp:TextBox ID="txtInvoiceMileage" AutoPostBack="false" onkeydown="return jsDecimals(event);" runat="server" Width="80%" />
                                        <asp:FilteredTextBoxExtender ID="txtInvoiceMileageF" runat="server" TargetControlID="txtInvoiceMileage" FilterType="Custom, Numbers" ValidChars="."></asp:FilteredTextBoxExtender>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="col-md-4 padding2">
                                        Driver Mileage
                                    </div>
                                    <div class="col-md-8 padding2">
                                        <asp:TextBox ID="txtDriverMileage" AutoPostBack="false" onkeydown="return jsDecimals(event);" runat="server" Width="80%" />
                                        <asp:FilteredTextBoxExtender ID="txtDriverMileageF" runat="server" TargetControlID="txtDriverMileage" FilterType="Custom, Numbers" ValidChars="."></asp:FilteredTextBoxExtender>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-6">
                                    <div class="col-md-4 padding2">
                                        Meter In
                                    </div>
                                    <div class="col-md-8 padding2">
                                        <asp:TextBox ID="txtMeterIn" AutoPostBack="false" onkeydown="return jsDecimals(event);" runat="server" Width="80%" />
                                        <asp:FilteredTextBoxExtender ID="txtMeterInF" runat="server" TargetControlID="txtMeterIn" FilterType="Custom, Numbers" ValidChars="."></asp:FilteredTextBoxExtender>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="col-md-4 padding2">
                                        Meter Out
                                    </div>
                                    <div class="col-md-8 padding2">
                                        <asp:TextBox ID="txtMeterOut" AutoPostBack="false" onkeydown="return jsDecimals(event);" runat="server" Width="80%" />
                                        <asp:FilteredTextBoxExtender ID="txtMeterOutF" runat="server" TargetControlID="txtMeterOut" FilterType="Custom, Numbers" ValidChars="."></asp:FilteredTextBoxExtender>
                                    </div>
                                </div>
                            </div>

                        </div>
                    </div>
                </div>
            </div>
            <div class="row rowmargin0 col-md-12">
                <div class="panel panel-default">
                    <div class="panel-heading">
                        Details
                    </div>
                    <div class="panel-body">
                        <div class="row rowmargin0 col-md-12 textaling paddingleft0 paddingright0">
                            <div class="col-md-1 padding0" style="width: 11%;">
                                Charge Code
                        <asp:TextBox ID="txtChargeCode" AutoPostBack="true" runat="server" Width="60%" OnTextChanged="txtChargeCode_TextChanged" />
                                <asp:ImageButton ID="btnChargeCode" runat="server" ImageUrl="~/Images/icon_search.png" ImageAlign="Middle" OnClick="btnChargeCode_Click" />
                                <asp:ImageButton ID="btnLoad" runat="server" ImageUrl="~/Images/LoadDetails.png" ImageAlign="Middle" OnClick="btnLoad_Click" />
                            </div>
                            <div class="col-md-1 padding0" style="width: 11%;">
                                Description
                        <asp:TextBox ID="txtDescription" AutoPostBack="true" runat="server" Width="95%" />
                            </div>
                            <div class="col-md-1 padding0" style="width: 5%;">
                                Qty
                        <asp:TextBox ID="txtQty" runat="server" AutoPostBack="true" Width="95%" CssClass="textalingright" OnTextChanged="txtQty_TextChanged" />
                                <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" TargetControlID="txtQty" FilterType="Custom, Numbers" ValidChars="."></asp:FilteredTextBoxExtender>
                            </div>
                            <div class="col-md-1 padding0">
                                Rate Type
                        <asp:TextBox ID="txtRateType" AutoPostBack="true" runat="server" Width="95%" />
                            </div>
                            <div class="col-md-1 padding0" style="width: 5%;">
                                Unit Rate
                        <asp:TextBox ID="txtUnitRate" AutoPostBack="true" runat="server" Width="95%" CssClass="textalingright" OnTextChanged="txtUnitRate_TextChanged" />
                                <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" TargetControlID="txtUnitRate" FilterType="Custom, Numbers" ValidChars="."></asp:FilteredTextBoxExtender>
                            </div>
                            <div class="col-md-1 padding0">
                                Unit Amount
                        <asp:TextBox ID="txtUnitAmount" AutoPostBack="true" runat="server" Width="95%" CssClass="textalingright" OnTextChanged="txtUnitAmount_TextChanged" />
                                <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" TargetControlID="txtUnitAmount" FilterType="Custom, Numbers" ValidChars="."></asp:FilteredTextBoxExtender>
                            </div>
                            <div class="col-md-1 padding0">
                                TAX
                        <asp:TextBox ID="txtTAX" runat="server" Enabled="false" AutoPostBack="true" Width="95%" CssClass="textalingright" OnTextChanged="txtTAX_TextChanged" />
                                <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server" TargetControlID="txtTAX" FilterType="Custom, Numbers" ValidChars="."></asp:FilteredTextBoxExtender>
                            </div>
                            <div class="col-md-1 padding0">
                                Dis Rate
                        <asp:TextBox ID="txtDisRate" AutoPostBack="true" runat="server" Width="95%" CssClass="textalingright" OnTextChanged="txtDisRate_TextChanged" />
                                <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" runat="server" TargetControlID="txtDisRate" FilterType="Custom, Numbers" ValidChars="."></asp:FilteredTextBoxExtender>
                            </div>
                            <div class="col-md-1 padding0">
                                Dis Amount
                        <asp:TextBox ID="txtDisAmount" runat="server" AutoPostBack="true" Width="95%" CssClass="textalingright" OnTextChanged="txtDisAmount_TextChanged" />
                                <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender6" runat="server" TargetControlID="txtDisAmount" FilterType="Custom, Numbers" ValidChars="."></asp:FilteredTextBoxExtender>
                            </div>
                            <div class="col-md-1 padding0">
                                Total
                        <asp:TextBox ID="txtTotal" runat="server" AutoPostBack="true" Width="95%" CssClass="textalingright" />
                                <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender7" runat="server" TargetControlID="txtTotal" FilterType="Custom, Numbers" ValidChars="."></asp:FilteredTextBoxExtender>
                            </div>
                            <div class="col-md-1 padding0" style="text-align: left">
                                <asp:CheckBox ID="chkIsDriver" Text="Is Driver" runat="server" />
                                <br />
                                <asp:CheckBox ID="chkIsCustomer" runat="server" Text="Is Customer" />
                            </div>
                            <div class="col-md-1 padding0">
                                <asp:Button Text="Add" Width="50px" Height="50px" runat="server" ID="btnAddTogrid" OnClick="btnAddTogrid_Click" />
                            </div>
                        </div>
                        <div class="row rowmargin0 col-md-12 textaling paddingleft0 paddingright0">
                            <asp:GridView ID="dgvItems" runat="server" AutoGenerateColumns="false" OnRowCommand="dgvItems_RowCommand" OnRowDeleting="dgvItems_RowDeleting" OnRowEditing="dgvItems_RowEditing">
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
                                    <asp:TemplateField HeaderText="Is Customer">
                                        <ItemTemplate>
                                            <asp:Label ID="lblTLD_IS_CUS" Visible="false" runat="server" Text='<%# Bind("TLD_IS_CUS") %>'></asp:Label>
                                            <asp:ImageButton ID="imgCusFalse" Visible="false" runat="server" ImageUrl="../images/False.png" />
                                            <asp:ImageButton ID="imgCusTrue" Visible="false" runat="server" ImageUrl="../images/True.png" />
                                        </ItemTemplate>
                                        <HeaderStyle Width="5%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Is Driver">
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
                                    <asp:TemplateField HeaderText=" ">
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
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

