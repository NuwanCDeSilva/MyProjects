<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="TransportEnquiryManagement.aspx.cs" Inherits="FF.AbansTours.Sales.TransportEnquiryManagement" %>

<%@ Register Assembly="TimePicker" Namespace="MKB.TimePicker" TagPrefix="MKB" %>
<%@ Register Src="~/UserControls/uc_VehicleEnquiry.ascx" TagPrefix="uc1" TagName="uc_VehicleEnquiry" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script>
        function ClearConfirm() {
            var selectedvalue = confirm("Do you want to clear data?");
            if (selectedvalue) {
                document.getElementById('<%=hdfClearData.ClientID %>').value = "1";
            } else {
                document.getElementById('<%=hdfClearData.ClientID %>').value = "0";
            }
        };
    </script>
    <style>
        .panel-body {
            padding-left: 5px;
            padding-right: 5px;
            margin-bottom: 0px;
        }

        .row {
            padding-bottom: 2px;
        }

        .panel {
            margin-bottom: 0px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div class="col-md-12" style="padding-right: 1px; padding-left: 1px;">

        <div style="height: 22px; width: 100%; text-align: right; background-color: Silver;">
            <asp:Button ID="btnCreate" Text="Save" runat="server" Width="80px" OnClick="btnCreate_Click" />
            <asp:Button ID="btnClear" Text="Clear" runat="server" Width="80px" OnClick="btnClear_Click" OnClientClick="ClearConfirm()" />
            <asp:Button ID="btnCancel" Text="Cancel" runat="server" Width="80px" OnClick="btnCancel_Click" />
           
            <asp:ConfirmButtonExtender ID="ConfirmButtonExtender4" runat="server" TargetControlID="btnCreate"
                ConfirmText="Do you want to save?" ConfirmOnFormSubmit="false">
            </asp:ConfirmButtonExtender>
            <asp:ConfirmButtonExtender ID="ConfirmButtonExtender2" runat="server" TargetControlID="btnCancel"
                ConfirmText="Do you want to cancel?" ConfirmOnFormSubmit="false">
            </asp:ConfirmButtonExtender>
        </div>

        <div class="panel panel-default">
            <div class="panel panel-heading" style="margin-bottom: 2px;">
                <strong><b>Transport Enquiry Management</b></strong>
                <asp:Label Text="0" ID="lblSeqNum" Visible="false" runat="server" />
                <asp:HiddenField runat="server" ID="hdfClearData" Value="0" />
            </div>
            <asp:UpdatePanel runat="server">
                <ContentTemplate>

              
            <div class="panel panel-body">
                <div class="row">
                    <div class="col-md-12">
                        <div class="row">
                            <div class="col-md-6" style="padding-right: 1px;">
                                <div class="row">
                                    <div class="col-md-12">
                                        <%-- <div class="panel panel-default">--%>
                                        <div class="">
                                            <asp:Panel ID="pnlCustomerDetails" runat="server">
                                                <div class="panel panel-default">
                                                    <div class="panel panel-heading" style="margin-bottom: 2px;">
                                                        Customer Details
                                                    </div>
                                                    <div class="panel panel-body">
                                                        <div class="row ">
                                                            <div class="col-md-12">

                                                                <div class="row">
                                                                    <div class="col-md-7">
                                                                    </div>
                                                                    <div class="col-md-5">
                                                                        <div class="col-md-6 padding0">
                                                                            <asp:Button ID="btnNewCustomer" Text="New Customer" runat="server" Width="100%"
                                                                                OnClick="btnNewCustomer_Click" />
                                                                            <asp:ConfirmButtonExtender ID="ConfirmButtonExtender1" runat="server" TargetControlID="btnNewCustomer"
                                                                                ConfirmText="Do you want to add new customer details?" ConfirmOnFormSubmit="false">
                                                                            </asp:ConfirmButtonExtender>
                                                                        </div>
                                                                        <div class="col-md-6 padding0">
                                                                            <asp:Button ID="btnHistory" Text="History" runat="server" Width="100%" OnClick="btnHistory_Click" />
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class="row ">
                                                                    <div class="col-md-4">
                                                                        <div class="col-md-4 padding0">
                                                                            Code
                                                                        </div>
                                                                        <div class="col-md-8 padding0">
                                                                            <asp:TextBox ID="txtCustomerCode" runat="server" AutoPostBack="true" Width="78%"
                                                                                OnTextChanged="txtCustomerCode_TextChanged"></asp:TextBox>
                                                                            <asp:ImageButton ID="btnCustomerCode" runat="server" ImageUrl="~/Images/icon_search.png"
                                                                                ImageAlign="Middle" OnClick="btnCustomerCode_Click" />
                                                                        </div>
                                                                    </div>
                                                                    <div class="col-md-4">
                                                                        <div class="col-md-4 padding0">
                                                                            Mobile
                                                                        </div>
                                                                        <div class="col-md-8 padding0">
                                                                            <asp:TextBox ID="txtMobile" runat="server" AutoPostBack="true" Width="80%" OnTextChanged="txtMobile_TextChanged"></asp:TextBox>
                                                                        </div>
                                                                    </div>
                                                                    <div class="col-md-4">
                                                                        <div class="col-md-4 padding0">
                                                                            NIC
                                                                        </div>
                                                                        <div class="col-md-8 padding0">
                                                                            <asp:TextBox ID="txtNIC" runat="server" AutoPostBack="true" Width="80%" OnTextChanged="txtNIC_TextChanged"></asp:TextBox>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class="row ">
                                                                    <div class="col-md-12">
                                                                        <div class="col-md-1 padding0">
                                                                            Name
                                                                        </div>
                                                                        <div class="col-md-11" style="padding-left: 7px; padding-right: 19px;">
                                                                            <asp:TextBox ID="txtName" runat="server" AutoPostBack="true" Width="100%"></asp:TextBox>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class="row ">
                                                                    <div class="col-md-12">
                                                                        <div class="col-md-1 padding0">
                                                                            Address
                                                                        </div>
                                                                        <div class="col-md-11" style="padding-left: 7px; padding-right: 19px;">
                                                                            <asp:TextBox ID="txtAddress1" runat="server" AutoPostBack="true" Width="100%"></asp:TextBox>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class="row ">
                                                                    <div class="col-md-12">
                                                                        <div class="col-md-1 padding0">
                                                                        </div>
                                                                        <div class="col-md-11" style="padding-left: 7px; padding-right: 19px;">
                                                                            <asp:TextBox ID="txtAddress2" runat="server" AutoPostBack="true" Width="100%"></asp:TextBox>
                                                                        </div>
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
                                <div class="row">
                                    <div class="col-md-12">
                                        <%--  <div class="panel panel-default">--%>
                                        <div class="">
                                            <asp:Panel ID="Panel4" runat="server">
                                                <div class="panel panel-default">
                                                    <div class="panel panel-heading" style="margin-bottom: 2px;">
                                                        Vehicle Allocation
                                                    </div>
                                                    <div class="panel panel-body" style="height: 252px;">
                                                        <div class="row">
                                                            <div class="col-md-12">
                                                                <div class="row">
                                                                    <div class="col-md-12">
                                                                        <div class="col-md-2 padding0">
                                                                            Request Date
                                                                        </div>
                                                                        <div class="col-md-4 padding0">
                                                                            <asp:TextBox ID="txtRequestDate" CssClass="input-xlarge focused" runat="server" Width="55%"></asp:TextBox>
                                                                            <asp:CalendarExtender ID="txtRequestDateExtender" runat="server" Enabled="True" Format="dd/MMM/yyyy" PopupButtonID="txtRequestDateCal" TargetControlID="txtRequestDate"></asp:CalendarExtender>
                                                                            <img alt="Calendar.." height="16" src="../images/calendar.png" width="16" id="txtRequestDateCal" style="cursor: pointer" />
                                                                            <MKB:TimeSelector ID="tmExpect" Height="12px" runat="server" BorderStyle="None" DisplayButtons="False" DisplaySeconds="False" SelectedTimeFormat="TwentyFour"></MKB:TimeSelector>
                                                                        </div>
                                                                        <div class="col-md-2 padding0">
                                                                            Return Date
                                                                        </div>
                                                                        <div class="col-md-4 padding0">
                                                                            <asp:TextBox ID="txtReturnDate" CssClass="input-xlarge focused" runat="server" Width="55%"></asp:TextBox>
                                                                            <asp:CalendarExtender ID="txtReturnDateExtender" runat="server" Enabled="True" Format="dd/MMM/yyyy" PopupButtonID="txtReturnDateCal" TargetControlID="txtReturnDate"></asp:CalendarExtender>
                                                                            <img alt="Calendar.." height="16" src="../images/calendar.png" width="16" id="txtReturnDateCal" style="cursor: pointer" />
                                                                            <MKB:TimeSelector ID="tmReturn" Height="12px" runat="server" BorderStyle="None" DisplayButtons="False" DisplaySeconds="False" SelectedTimeFormat="TwentyFour"></MKB:TimeSelector>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class="row">
                                                                    <div class="col-md-12">
                                                                        <div class="col-md-2 padding0">
                                                                            Service By
                                                                        </div>
                                                                        <div class="col-md-4 padding0">
                                                                            <asp:DropDownList ID="ddlServiceType" Width="80%" CssClass="ddlhight1" runat="server">
                                                                                <asp:ListItem Text="--Select--" Value="0" />
                                                                                <asp:ListItem Text="Internal" Value="0" />
                                                                                <asp:ListItem Text="External" Value="0" />
                                                                            </asp:DropDownList>
                                                                        </div>
                                                                        <div class="col-md-2 padding0">
                                                                            Vehicle
                                                                        </div>
                                                                        <div class="col-md-4 padding0">
                                                                            <asp:TextBox ID="txtVehicle" runat="server" Width="80%" />
                                                                            <asp:ImageButton ID="btnVehicle" runat="server" ImageUrl="~/Images/icon_search.png" ImageAlign="Middle" OnClick="btnVehicle_Click" />
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class="row">
                                                                    <div class="col-md-12">
                                                                        <div class="col-md-2 padding0">
                                                                            Driver
                                                                        </div>
                                                                        <div class="col-md-4 padding0">
                                                                            <asp:TextBox ID="txtDriver" AutoPostBack="true" runat="server" Width="80%" OnTextChanged="txtDriver_TextChanged" />
                                                                            <asp:ImageButton ID="btnDriver" runat="server" ImageUrl="~/Images/icon_search.png" ImageAlign="Middle" OnClick="btnDriver_Click" />
                                                                        </div>
                                                                        <div class="col-md-2 padding0">
                                                                            Name
                                                                        </div>
                                                                        <div class="col-md-4 padding0">
                                                                            <asp:TextBox ID="txtDriverName" Enabled="false" ReadOnly="true" runat="server" Width="80%" />
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class="row">
                                                                    <div class="col-md-12">
                                                                        <div class="col-md-2 padding0">
                                                                            Driver Contact
                                                                        </div>
                                                                        <div class="col-md-4 padding0">
                                                                            <asp:TextBox ID="txtDriverContact" Enabled="false" AutoPostBack="true" runat="server" Width="80%" />
                                                                        </div>
                                                                        <div class="col-md-2 padding0">
                                                                        </div>
                                                                        <div class="col-md-4 padding0">
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-md-12">
                                                                <div class="panel panel-default">
                                                                    <div class="panel panel-heading" style="margin-bottom: 2px;">
                                                                        Charges 
                                                                    </div>
                                                                    <div class="panel panel-body">
                                                                        <div class="row">
                                                                            <div class="col-md-12">
                                                                                <div class="col-md-2 padding0">
                                                                                    Charge Code
                                                                                </div>
                                                                                <div class="col-md-4 padding0">
                                                                                    <asp:TextBox ID="txtChargeCode" runat="server" Width="60%" AutoPostBack="True" OnTextChanged="txtChargeCode_TextChanged" />
                                                                                    <asp:ImageButton ID="btnChargeCode" runat="server" ImageUrl="~/Images/icon_search.png" ImageAlign="Middle" OnClick="btnChargeCode_Click" />
                                                                                    <asp:ImageButton ID="btnChargeCodeLoad" runat="server" ImageUrl="~/Images/bubble-tail.png" ImageAlign="Middle" OnClick="btnChargeCodeLoad_Click" />
                                                                                </div>
                                                                                <div class="col-md-2 padding0">
                                                                                    Unit Rate
                                                                                </div>
                                                                                <div class="col-md-3 padding0">
                                                                                    <asp:TextBox ID="txtUnitRate" onkeydown="return jsDecimals(event);" runat="server" Width="80%" />
                                                                                </div>
                                                                                <div class="col-md-1 padding0">
                                                                                    <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Images/dwnarrowgridicon.png" ImageAlign="Middle" OnClick="btnAdd_Click" />
                                                                                    <%--<asp:Button ID="btnAdd" Text="Add" runat="server" Width="100%" OnClick="btnAdd_Click" />--%>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                        <div class="row">
                                                                            <div class="col-md-12">
                                                                                <div class="panel panel-default">
                                                                                    <div class="panel-heading" style="margin-bottom: 2px;">
                                                                                        Charge Items
                                                                                    </div>
                                                                                    <div class="panel-body" style="height: 83px; overflow-y: auto;">
                                                                                        <div class="col-md-12 paddingleft0">
                                                                                            <asp:GridView ID="dgvChargeItems" runat="server" AutoGenerateColumns="False" OnRowCommand="dgvChargeItems_RowCommand"
                                                                                                EmptyDataText="No data found..." ShowHeaderWhenEmpty="True">
                                                                                                <Columns>
                                                                                                    <asp:TemplateField>
                                                                                                        <ItemTemplate>
                                                                                                            <asp:ImageButton ID="btnImgDelete" runat="server" ImageAlign="Middle" ImageUrl="~/images/deleteicon.png"
                                                                                                                Width="10px" Height="10px" CommandName="Delete" CommandArgument="<%# Container.DisplayIndex %>" />
                                                                                                        </ItemTemplate>
                                                                                                    </asp:TemplateField>
                                                                                                    <asp:TemplateField HeaderText="Charge Code">
                                                                                                        <ItemTemplate>
                                                                                                            <asp:Label ID="lblSad_itm_cd" runat="server" Text='<%# Bind("Sad_itm_cd") %>'></asp:Label>
                                                                                                        </ItemTemplate>
                                                                                                    </asp:TemplateField>
                                                                                                    <asp:BoundField DataField='Sad_unit_rt' HeaderText='Unote Rate' />
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
                                                </div>
                                            </asp:Panel>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-6" style="padding-left: 1px;">
                                <div class="row">
                                    <div class="col-md-12">
                                        <%--<div class="panel panel-default">--%>
                                        <div class="">
                                            <asp:Panel ID="Panel1" runat="server">
                                                <div class="panel  panel-default">
                                                    <div class="row">
                                                        <div class="col-md-12">
                                                            <asp:UpdatePanel runat="server">
                                                                <ContentTemplate>
                                                                    <div class="col-md-3">
                                                                        Enquiry ID
                                                                    </div>
                                                                    <div class="col-md-4">
                                                                        <asp:TextBox ID="txtEnquiryID" runat="server" AutoPostBack="true" Width="80%" OnTextChanged="txtEnquiryID_TextChanged"></asp:TextBox>
                                                                        <asp:ImageButton ID="btnEnquiryID" runat="server" ImageUrl="~/Images/icon_search.png"
                                                                            ImageAlign="Middle" OnClick="btnEnquiryID_Click" />
                                                                    </div>
                                                                    <div class="col-md-5">
                                                                        <div class="col-md-6">
                                                                            <asp:Button Text="Recall" runat="server" Width="100%" ID="btnRecall" OnClick="btnRecall_Click" />
                                                                        </div>
                                                                        <div class="col-md-6 paddingright0">
                                                                            <asp:Button Text="Fleet Schedule" runat="server" Width="100%" ID="btnFleetShed" OnClick="btnFleetShed_Click" />
                                                                        </div>
                                                                    </div>
                                                                </ContentTemplate>
                                                                <Triggers>
                                                                    <asp:PostBackTrigger ControlID="txtEnquiryID" />
                                                                </Triggers>
                                                            </asp:UpdatePanel>
                                                        </div>
                                                    </div>
                                                    <div class="row">
                                                        <div class="col-md-12">
                                                            <div class="panel panel-default">
                                                                <div class="panel panel-heading" style="margin-bottom: 2px;">
                                                                    Request Details
                                                                </div>
                                                                <div class="panel panel-body" style="height: 115px; "">
                                                                    <asp:GridView ID="dgvReq" runat="server" AutoGenerateColumns="False" Font-Size="X-Small" EmptyDataText="No data found..."
                                                                        OnRowCommand="dgvReq_RowCommand" Style="padding: 2px 2px;" AllowPaging="true" ShowHeaderWhenEmpty="true"
                                                                        PageSize="4" OnPageIndexChanging="dgvReq_PageIndexChanging"
                                                                        OnRowDataBound="dgvReq_RowDataBound"
                                                                        OnSelectedIndexChanged="dgvReq_SelectedIndexChanged">
                                                                        <Columns>
                                                                            <%-- <asp:CommandField ShowSelectButton="true" ButtonType="Link" SelectText="" />--%>
                                                                            <%-- <asp:TemplateField HeaderText="Enquiry">
                                                                            <ItemTemplate>
                                                                                <asp:ImageButton ID="btnViewEnquiry" runat="server" CommandArgument="<%# Container.DisplayIndex %>"
                                                                                    CommandName="ViewEnquiry" ImageUrl="~/images/Details.png" ToolTip="Enquiry.."
                                                                                    ImageAlign="Middle" />
                                                                            </ItemTemplate>
                                                                            <HeaderStyle CssClass="bodycolordarkgreen" HorizontalAlign="Center" />
                                                                            <ItemStyle HorizontalAlign="Center" />
                                                                        </asp:TemplateField>--%>
                                                                            <asp:ButtonField Text="Select" CommandName="Select" ItemStyle-Width="45px" />
                                                                            <asp:TemplateField HeaderText="Customer Code">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblCustCode" runat="server" Text='<%# Bind("gce_cus_cd") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                                <HeaderStyle Width="130px" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="System ID">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblEnquiryID" runat="server" Text='<%# Bind("gce_enq_id") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                                <HeaderStyle Width="120px" />
                                                                            </asp:TemplateField>
                                                                            <asp:BoundField DataField="gce_ref" ControlStyle-Width="130px" HeaderText=" Manual Ref."></asp:BoundField>

                                                                            <%--<asp:BoundField DataField="gce_enq_pc_desc" HeaderText="Request From">
                                                                            <HeaderStyle Width="145px" />
                                                                        </asp:BoundField>--%>
                                                                            <%--  <asp:TemplateField>
                                                                            <HeaderTemplate>
                                                                                <asp:Label runat="server" Text="Requested Date" Visible="false"></asp:Label>
                                                                            </HeaderTemplate>
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblDate" Visible="false" runat="server" Text='<%# Bind("gce_dt", "{0:dd/MM/yyyy}") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>--%>
                                                                            <%-- <asp:BoundField DataField="gce_name" HeaderText="Name">
                                                                            </asp:BoundField>--%>
                                                                            <%--<asp:BoundField DataField="gce_mob" HeaderText="Mobile" />--%>
                                                                            <asp:TemplateField>
                                                                                <HeaderTemplate>
                                                                                    <asp:Label runat="server" Text="Expected Date" Visible="true"></asp:Label>
                                                                                </HeaderTemplate>
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblExpectedDate" Visible="true" runat="server"
                                                                                        Text='<%# Bind("gce_expect_dt","{0:dd/MMM/yyyy}") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                                <HeaderStyle Width="100px" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField>
                                                                                <HeaderTemplate>
                                                                                    <asp:Label runat="server" Text="Request Type" Visible="false"></asp:Label>
                                                                                </HeaderTemplate>
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="met_desc" Visible="false" runat="server" Text='<%# Bind("met_desc") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <%--<asp:BoundField DataField="PendingDate" HeaderText="Pending Days" />--%>

                                                                            <%-- <asp:TemplateField HeaderText="Status">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblstatus" runat="server" Text='<%# Bind("mes_desc") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                            <HeaderStyle Width="65px" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText=" Go to Costing">
                                                                            <ItemTemplate>
                                                                                <asp:ImageButton ID="btnCosting" runat="server" CommandArgument="<%# Container.DisplayIndex %>"
                                                                                    CommandName="Costing" ImageUrl="~/images/money.png" ToolTip="Costing.." ImageAlign="Middle" />
                                                                            </ItemTemplate>
                                                                            <HeaderStyle CssClass="bodycolordarkgreen" HorizontalAlign="Center" />
                                                                            <ItemStyle HorizontalAlign="Center" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Go to Invoice">
                                                                            <ItemTemplate>
                                                                                <asp:ImageButton ID="btnVInvoice" runat="server" CommandArgument="<%# Container.DisplayIndex %>"
                                                                                    CommandName="Invoice" ImageUrl="~/images/Invoice.png" ToolTip="Invoice.." ImageAlign="Middle" />
                                                                            </ItemTemplate>
                                                                            <HeaderStyle CssClass="bodycolordarkgreen" HorizontalAlign="Center" />
                                                                            <ItemStyle HorizontalAlign="Center" />
                                                                        </asp:TemplateField>--%>
                                                                            <asp:TemplateField HeaderText="Enquiry" Visible="False">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblEnquiry" Visible="false" runat="server" Text='<%# Bind("gce_enq") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <%--<asp:TemplateField HeaderText="IsLateToNextStage" Visible="False">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="IsLateToNextStage" runat="server" Text='<%# Bind("IsLateToNextStage") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="main" Visible="False">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="gce_mainreqid" runat="server" Text='<%# Bind("gce_mainreqid") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>--%>
                                                                        </Columns>
                                                                        <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Size="Small" />
                                                                    </asp:GridView>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </asp:Panel>
                                        </div>
                                    </div>
                                </div>
                                 <div class="row">
                                    <div class="col-md-12">
                                        <%--<div class="panel panel-default">--%>
                                        <div class="">
                                            <asp:Panel ID="Panel2" runat="server">
                                                <div class="panel panel-default">
                                                    <div class="panel panel-heading" style="margin-bottom: 2px;">
                                                        Trip Details
                                                    </div>
                                                    <div class="panel panel-body" style="height: 222px;">
                                                        <div class="row">
                                                            <div class="col-md-12">
                                                                <div class="col-md-3" style="padding-left: 0px;">
                                                                    Trip Type
                                                                </div>
                                                                <div class="col-md-3 paddingleft0">
                                                                    <asp:DropDownList ID="ddlTripTp" Width="100%" AppendDataBoundItems="true" CssClass="ddlhight1" runat="server">
                                                                        <asp:ListItem Text="--Select--" Value="0" />
                                                                    </asp:DropDownList>
                                                                </div>
                                                                <div class="col-md-3 padding0">
                                                                    No. Of Passangers
                                                                </div>
                                                                <div class="col-md-3 paddingleft0">
                                                                    <asp:TextBox ID="txtNoOfPassengers" Width="100%" CssClass="" runat="server"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-md-12">
                                                                <div class="col-md-3 padding0" style="padding-left: 0px;">
                                                                    Req. Vehicle Type
                                                                </div>
                                                                <div class="col-md-3 paddingleft0">
                                                                    <asp:DropDownList ID="ddlVehicleType" Width="100%" AppendDataBoundItems="true" CssClass="ddlhight1" runat="server">
                                                                        <asp:ListItem Text="--Select--" Value="0" />
                                                                    </asp:DropDownList>
                                                                </div>
                                                                <div class="col-md-3 padding0">
                                                                    No Of Req. Vehicle
                                                                </div>
                                                                <div class="col-md-3 paddingleft0">
                                                                    <asp:TextBox ID="txtNoOfreqVehicle" Width="100%" CssClass="" runat="server"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="row" style="padding-bottom: 1px;">
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-md-6" style="padding-left: 10px; padding-right: 1px;">
                                                                <div class="panel panel-default" style="padding-left: 0px;">
                                                                    <div class="panel panel-heading" style="padding-left: 0px; margin-bottom: 2px;">
                                                                        <strong>Picked Up Details</strong>
                                                                    </div>
                                                                    <div class="row">
                                                                        <div class="col-md-12">
                                                                            <div class="col-md-6" style="padding-left: 6px;">
                                                                                Town
                                                                            </div>
                                                                            <div class="col-md-6 paddingleft0" style="padding-left: 18px; padding-right: 12px;">
                                                                                <asp:TextBox ID="txtPTown" Width="80%" CssClass="" runat="server"></asp:TextBox>
                                                                                <asp:ImageButton ID="btnpiUpTown" runat="server" ImageUrl="~/Images/icon_search.png"
                                                                                    ImageAlign="Middle" OnClick="btnpiUpTown_Click" />
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                    <div class="row">
                                                                        <div class="col-md-12">
                                                                            <div class="col-md-6" style="padding-left: 6px;">
                                                                                Address
                                                                            </div>
                                                                            <div class="col-md-6 paddingleft0" style="padding-left: 18px; padding-right: 12px;">
                                                                                <asp:TextBox ID="txtPAddress" TextMode="MultiLine" Width="100%" Height="35px" CssClass="" runat="server"></asp:TextBox>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>

                                                            <div class="col-md-6" style="padding-left: 1px; padding-right: 10px;">
                                                                <div class="panel panel-default" style="padding-left: 0px;">
                                                                    <div class="panel panel-heading" style="padding-left: 0px; margin-bottom: 2px;">
                                                                        <strong>Drop Details</strong>
                                                                    </div>
                                                                    <div class="row">
                                                                        <div class="col-md-12">
                                                                            <div class="col-md-6" style="padding-left: 12px;">
                                                                                Town
                                                                            </div>
                                                                            <div class="col-md-6" style="padding-left: 0px; padding-right: 12px;">
                                                                                <asp:TextBox ID="tXtDTown" Width="80%" CssClass="ddlhight1" runat="server"></asp:TextBox>
                                                                                <asp:ImageButton ID="btnDrTown" runat="server" ImageUrl="~/Images/icon_search.png"
                                                                                    ImageAlign="Middle" OnClick="btnDrTown_Click" />
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                    <div class="row">
                                                                        <div class="col-md-12">
                                                                            <div class="col-md-6" style="padding-left: 12px;">
                                                                                Address
                                                                            </div>
                                                                            <div class="col-md-6" style="padding-left: 0px; padding-right: 12px;">
                                                                                <asp:TextBox ID="txtDAddress" TextMode="MultiLine" Width="100%" Height="35px" CssClass="ddlhight1" runat="server"></asp:TextBox>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-md-12">
                                                                <div class="col-md-3" style="padding-left: 0px;">
                                                                    Contact Person
                                                                </div>
                                                                <div class="col-md-3 paddingleft0">
                                                                    <asp:TextBox ID="txtContactPerson" Width="100%" CssClass="ddlhight1" runat="server"></asp:TextBox>
                                                                </div>
                                                                <div class="col-md-3" style="padding-left: 12px; padding-right: 12px;">
                                                                    Contact Mobile
                                                                </div>
                                                                <div class="col-md-3 paddingleft0" style="padding-left: 15px; padding-right: 7px;">
                                                                    <asp:TextBox ID="txtContactMobile" Width="100%" CssClass="" runat="server"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-md-12">
                                                                <div class="col-md-3" style="padding-left: 0px;">
                                                                    Reference Num
                                                                </div>
                                                                <div class="col-md-3 paddingleft0">
                                                                    <asp:TextBox ID="txtReference" runat="server" AutoPostBack="true" Width="100%"></asp:TextBox>
                                                                </div>
                                                                <div class="col-md-3" style="padding-left: 12px; padding-right: 12px;">
                                                                   
                                                                </div>
                                                                <div class="col-md-3 paddingleft0" style="padding-left: 15px; padding-right: 7px;">
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-md-12">
                                                                <div class="col-md-3" style="padding-left: 0px;">
                                                                    Remarks
                                                                </div>
                                                                <div class="col-md-9 paddingleft0" style="padding-right: 6px;">
                                                                    <asp:TextBox ID="txtRemarks" TextMode="MultiLine" Width="100%" style="height:35px;" CssClass="" runat="server"></asp:TextBox>
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
                            <div class="col-md-6" style="padding-left: 1px;">
                                
                            </div>
                            <div class="col-md-6" style="padding-right: 1px;">
                               
                            </div>
                            
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-12">
                        <div class="panel panel-default">
                            <div class="panel panel-heading" style="margin-bottom: 2px;">
                                Payment Details 
                            </div>
                            <div class="panel panel-body">
                                <div class="col-md-6 paddingleft0">
                                    <div class="col-md-6 padding0">
                                        <div class="col-md-4  padding2">
                                            Pay Mode
                                        </div>
                                        <div class="col-md-8  padding2">
                                            <asp:DropDownList ID="ddlPayMode" runat="server" Width="70%" CssClass="textbox ddlhight1" AutoPostBack="True"
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
                                                <asp:TextBox ID="txtAmount" Width="90%" runat="server" CssClass="textbox"></asp:TextBox>
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
                                            <asp:TextBox ID="txtRemark" runat="server" Width="90%"
                                                TextMode="MultiLine"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-12  padding2 displayinlineblock">
                                        <asp:GridView ID="grdPaymentDetails" runat="server" AutoGenerateColumns="false" CssClass="table table-bordered"
                                            EmptyDataText="No data found..." ShowHeaderWhenEmpty="True"
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
                </div>
            </div>
                      </ContentTemplate>
            </asp:UpdatePanel>
        </div>

    </div>


    <asp:Panel ID="pnlBatchConfirm" runat="server" Width="80%" Height="50%" Style="display: none"
        CssClass="ModalPopup">
        <table cellpadding="3" cellspacing="1" class="tableborder" width="100%">
            <tr>
                <td height="22px" align="center" class="bodycolorlightgreen" width="100%">Customer Enquiry History
                </td>
            </tr>
            <tr>
                <td height="400px" align="center" class="bodycolorlightgreen" width="100%">
                    <asp:Panel ID="Panel3" runat="server" Width="100%" Height="100%" ScrollBars="Auto">
                        <asp:GridView ID="dgvHistry" runat="server" AutoGenerateColumns="False" AllowSorting="True">
                            <Columns>
                                <asp:BoundField DataField="gce_enq_id" HeaderText="Enquiry ID" />
                                <asp:BoundField DataField="gce_ref" HeaderText="Reference" />
                                <asp:BoundField DataField="met_desc" HeaderText="Type" />
                                <asp:BoundField DataField="gce_com" HeaderText="Company" />
                                <asp:BoundField DataField="gce_pc" HeaderText="PC" />
                                <asp:TemplateField HeaderText="Date">
                                    <ItemTemplate>
                                        <asp:Label ID="lbltDate" runat="server" Text='<%# Bind("gce_dt") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="gce_cus_cd" HeaderText="Customer Code" />
                                <asp:BoundField DataField="gce_name" HeaderText="Name" />
                                <asp:BoundField DataField="gce_add1" HeaderText="Address 1" />
                                <asp:BoundField DataField="gce_mob" HeaderText="Mobile" />
                                <asp:BoundField DataField="gce_nic" HeaderText="NIC" />
                                <asp:TemplateField HeaderText="Expected Date">
                                    <ItemTemplate>
                                        <asp:Label ID="lblExpectDate" runat="server" Text='<%# Bind("gce_expect_dt") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="gce_enq" HeaderText="Enquiry" />
                                <asp:BoundField DataField="mes_desc" HeaderText="Status" />
                            </Columns>
                        </asp:GridView>
                    </asp:Panel>
                </td>
            </tr>
            <tr>
                <td height="35px" align="center" width="100%" bgcolor="#969696">
                    <asp:Button ID="btnClose" Width="80px" Text="Close" runat="server" OnClick="btnClose_Click" />
                </td>
            </tr>
        </table>
    </asp:Panel>
    <asp:Button ID="btnPopup_BatchConfirmationOne" runat="server" Text="D3" Style="display: none" />
    <asp:ModalPopupExtender ID="mpBatchConfirmationOne" runat="server" DynamicServicePath=""
        Enabled="True" PopupControlID="pnlBatchConfirm" TargetControlID="btnPopup_BatchConfirmationOne"
        BackgroundCssClass="modalBackground" PopupDragHandleControlID="pnlBatchConfirm">
    </asp:ModalPopupExtender>

    <asp:UpdatePanel runat="server" ID="up1">
        <ContentTemplate>
            <asp:Button ID="Button1" runat="server" Text="Button" Style="display: none;" />
            <asp:ModalPopupExtender ID="PopupEnquery" runat="server" Enabled="True" TargetControlID="Button1"
                PopupControlID="EnqueryPanel" PopupDragHandleControlID="PopupHeader" Drag="true" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>
        </ContentTemplate>
    </asp:UpdatePanel>

    <asp:Panel runat="server" ID="EnqueryPanel" DefaultButton="LinkButton1">
        <div runat="server" id="Div1" class="panel panel-primary Mheight" style="width: 1000px;">
            <div class="col-sm-12" style="padding-left: 0px; padding-right: 0px;">
                <asp:Label ID="Label1" runat="server" Text="Label" Visible="false"></asp:Label>
                <div class="panel panel-default" style="height: 447px;">
                    <div class="panel-heading" style="height: 25px; padding-right: 0px;">
                        <div class="col-sm-11"></div>
                        <div class="col-sm-1">
                            <div style="margin-top: 5px; padding-right: 0px;">
                                <asp:LinkButton ID="LinkButton1" OnClick="LinkButton1_Click" runat="server">
                             <span class="glyphicon glyphicon-remove"  aria-hidden="true">Close</span>
                                </asp:LinkButton>
                            </div>
                        </div>
                    </div>
                    <div class="panel-body" style="padding-left: 0px; padding-right: 0px;">
                        <div class="row">
                            <%-- add user controler --%>
                            <br />
                            <asp:UpdatePanel runat="server">
                                <ContentTemplate>
                                    <uc1:uc_VehicleEnquiry runat="server" ID="uc_VehicleEnquiry" />
                                </ContentTemplate>
                            </asp:UpdatePanel>

                        </div>


                        <div class="row">
                            <div class="col-sm-12">
                                <asp:ScriptManagerProxy ID="ScriptManagerProxy3" runat="server"></asp:ScriptManagerProxy>
                                <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                                    <ContentTemplate>
                                        <asp:GridView ID="GridView1" CausesValidation="false" runat="server" AllowPaging="True"
                                            GridLines="None" CssClass="table table-hover table-striped" PageSize="7" PagerStyle-CssClass="cssPager">
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
    </asp:Panel>
    <asp:HiddenField ID="_isExsit" runat="server" />
    <asp:HiddenField ID="_isGroup" runat="server" />
</asp:Content>
