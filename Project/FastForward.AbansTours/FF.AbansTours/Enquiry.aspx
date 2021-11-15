<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="Enquiry.aspx.cs" Inherits="FF.AbansTours.Enquiry" %>

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


        function confDel() {
            var result = confirm("Do you want to delete this record?");
            if (result) {
                return true;
            }
            else {
                return false;
            }
        };

    </script>
</asp:Content>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <%--    <script type="text/javascript" language="javascript">
        $(document).ready(function ()
        {
            $('#<%=dgvHistry.ClientID %>').Scrollable();
        }
                        )
    </script>--%>
    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
        <ContentTemplate>
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
            <div style="height: 22px; width: 100%; text-align: right; background-color: Silver;">
                <asp:Button ID="btnCreate" Text="Save" runat="server" Width="80px" OnClick="btnCreate_Click" />
                <asp:Button ID="btnClear" Text="Clear" runat="server" Width="80px" OnClick="btnClear_Click" />
                <asp:Button ID="btnCancel" Text="Cancel" runat="server" Width="80px" OnClick="btnCancel_Click" />
                <asp:ConfirmButtonExtender ID="ConfirmButtonExtender3" runat="server" TargetControlID="btnClear"
                    ConfirmText="Do you want to clear?" ConfirmOnFormSubmit="false">
                </asp:ConfirmButtonExtender>
                <asp:ConfirmButtonExtender ID="ConfirmButtonExtender4" runat="server" TargetControlID="btnCreate"
                    ConfirmText="Do you want to save?" ConfirmOnFormSubmit="false">
                </asp:ConfirmButtonExtender>
                <asp:ConfirmButtonExtender ID="ConfirmButtonExtender2" runat="server" TargetControlID="btnCancel"
                    ConfirmText="Do you want to cancel?" ConfirmOnFormSubmit="false">
                </asp:ConfirmButtonExtender>
            </div>
            <div class="row rowmargin0">
                <div class="col-md-12">
                    <div class="panel panel-default">
                        <div class="panel-heading pannelheading">
                            Enquiry
                        </div>
                        <div class="panel-body">
                            <div class="row col-md-10">
                                <div class="row padding2">
                                    <div class="col-md-7">
                                        <div class="col-md-4">
                                            Enquiry ID
                                        </div>
                                        <div class="col-md-8">
                                            <asp:TextBox ID="txtEnquiryID" runat="server" AutoPostBack="true" Width="80%" OnTextChanged="txtEnquiryID_TextChanged"></asp:TextBox>
                                            <asp:ImageButton ID="btnEnquiryID" runat="server" ImageUrl="~/Images/icon_search.png"
                                                ImageAlign="Middle" OnClick="btnEnquiryID_Click" /> <asp:Label ID="lblSeqNum" Text="0" runat="server" Visible="false" />
                                        </div>
                                    </div>
                                    <div class="col-md-5">
                                        <div class="col-md-4 paddingright0">
                                            Required Type
                                        </div>
                                        <div class="col-md-8">
                                             <asp:DropDownList ID="ddlTripTp" Width="80%" AppendDataBoundItems="true" CssClass="ddlhight1" runat="server">
                                                                        <asp:ListItem Text="--Select--" Value="0" />
                                                                    </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                                <div class="row  padding2">
                                    <div class="col-md-7">
                                        <div class="col-md-4">
                                            Facility
                                        </div>
                                        <div class="col-md-8">
                                            <asp:DropDownList ID="ddlFacility" runat="server" Width="80%" OnSelectedIndexChanged="ddlFacility_SelectedIndexChanged" AppendDataBoundItems="true"
                                                AutoPostBack="true" OnTextChanged="ddlFacility_TextChanged">
                                                <asp:ListItem Text=" Select " Value="0" />
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-md-5">
                                        <div class="col-md-4">
                                            Reference Num
                                        </div>
                                        <div class="col-md-8">
                                            <asp:TextBox ID="txtReference" runat="server" AutoPostBack="true" Width="80%"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="row  padding2">
                                    <div class="col-md-7">
                                        <div class="col-md-4">
                                            Facility Com
                                        </div>
                                        <div class="col-md-8">
                                            <asp:TextBox ID="txtFacilityCom" runat="server" AutoPostBack="true" Width="80%" OnTextChanged="txtFacilityCom_TextChanged"></asp:TextBox>
                                            <asp:ImageButton ID="btnFacilityCom" runat="server" ImageUrl="~/Images/icon_search.png"
                                                ImageAlign="Middle" OnClick="btnFacilityCom_Click" />
                                        </div>
                                    </div>
                                    <div class="col-md-5">
                                        <div class="col-md-4">
                                            Facility SBL
                                        </div>
                                        <div class="col-md-8">
                                            <asp:TextBox ID="txtFacilityPC" runat="server" AutoPostBack="true" Width="80%" Enabled="false" />
                                        </div>
                                    </div>
                                </div>
                                <div class="row  padding2">
                                    <div class="col-md-7">
                                        <div class="col-md-4">
                                            Expected Date
                                        </div>
                                        <div class="col-md-8">
                                            <asp:TextBox ID="txtExpectedDate" Enabled="false" CssClass="input-xlarge focused"
                                                runat="server" Width="80%" onkeypress="return RestrictSpace()"></asp:TextBox>
                                            <asp:CalendarExtender ID="txtClearingDateExtender" runat="server" Enabled="True"
                                                Format="dd/MMM/yyyy" PopupButtonID="cal" TargetControlID="txtExpectedDate">
                                            </asp:CalendarExtender>
                                            <img alt="Calendar.." height="16" src="images/calendar.png" width="16" id="cal" style="cursor: pointer" />
                                        </div>
                                    </div>
                                    <div class="col-md-5">
                                        <div class="col-md-4">
                                            <asp:Label ID="lblStatus" Visible="false" Text="Status" runat="server" />
                                        </div>
                                        <div class="col-md-8">
                                            <asp:Label ID="lblStatusText" Visible="false" Text="Status" runat="server" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row col-md-2">
                                <div class="row" style="height: 30px;">
                                </div>
                                <div class="row col-md-2">
                                </div>
                                <asp:LinkButton ID="btnAddRecords" Text="text" runat="server" OnClick="btnAddRecords_Click" Visible="false">
                                    <span class="glyphicon glyphicon-plus" aria-hidden="true" style="font-size:24px;"></span>
                                </asp:LinkButton>
                                <asp:Button Text="Add Details" ID="btnAddRecords2" OnClick="btnAddRecords_Click" runat="server" Width="110px" Height="30px" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="row rowmargin0">
                <div class="col-md-12">
                    <div class="panel panel-default">
                        <asp:Panel ID="pnlCustomerDetails" runat="server">
                            <div class="panel-heading">
                                Customer Details
                            </div>
                            <div class="panel-body">
                                <div class="row col-md-12">
                                    <div class="row padding2" style="text-align: right">
                                        <div class="col-md-8">
                                        </div>
                                        <div class="col-md-2">
                                            <asp:Button ID="btnNewCustomer" Text="New Customer" runat="server" Width="100px"
                                                OnClick="btnNewCustomer_Click" />
                                            <asp:ConfirmButtonExtender ID="ConfirmButtonExtender1" runat="server" TargetControlID="btnNewCustomer"
                                                ConfirmText="Do you want to add new customer details?" ConfirmOnFormSubmit="false">
                                            </asp:ConfirmButtonExtender>
                                        </div>
                                        <div class="col-md-2">
                                            <asp:Button ID="btnHistory" Text="History" runat="server" Width="100px" OnClick="btnHistory_Click" />
                                        </div>
                                    </div>
                                    <div class="row  padding2">
                                        <div class="col-md-12">
                                            <div class="col-md-2">
                                                Customer Code
                                            </div>
                                            <div class="col-md-4">
                                                <asp:TextBox ID="txtCustomerCode" runat="server" AutoPostBack="true" Width="80%"
                                                    OnTextChanged="txtCustomerCode_TextChanged"></asp:TextBox>
                                                <asp:ImageButton ID="btnCustomerCode" runat="server" ImageUrl="~/Images/icon_search.png"
                                                    ImageAlign="Middle" OnClick="btnCustomerCode_Click" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row  padding2">
                                        <div class="col-md-12">
                                            <div class="col-md-2">
                                                Mobile
                                            </div>
                                            <div class="col-md-4">
                                                <asp:TextBox ID="txtMobile" runat="server" AutoPostBack="true" Width="80%" OnTextChanged="txtMobile_TextChanged"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row  padding2">
                                        <div class="col-md-12">
                                            <div class="col-md-2">
                                                NIC
                                            </div>
                                            <div class="col-md-4">
                                                <asp:TextBox ID="txtNIC" runat="server" AutoPostBack="true" Width="80%" OnTextChanged="txtNIC_TextChanged"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row  padding2">
                                        <div class="col-md-12">
                                            <div class="col-md-2">
                                                Name
                                            </div>
                                            <div class="col-md-10">
                                                <asp:TextBox ID="txtName" runat="server" AutoPostBack="true" Width="100%"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row  padding2">
                                        <div class="col-md-12">
                                            <div class="col-md-2">
                                                Address 1
                                            </div>
                                            <div class="col-md-10">
                                                <asp:TextBox ID="txtAddress1" runat="server" AutoPostBack="true" Width="100%"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row  padding2">
                                        <div class="col-md-12">
                                            <div class="col-md-2">
                                                Address 2
                                            </div>
                                            <div class="col-md-10">
                                                <asp:TextBox ID="txtAddress2" runat="server" AutoPostBack="true" Width="100%"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row  padding2">
                                        <div class="col-md-12">
                                            <div class="col-md-2">
                                                Email
                                            </div>
                                            <div class="col-md-10">
                                                <asp:TextBox ID="txtEmail" runat="server" AutoPostBack="true" Width="100%"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </asp:Panel>
                    </div>
                </div>
            </div>
            <div class="row rowmargin0" runat="server" id="divOther" visible="false">
                <div class="col-md-12">
                    <div class="panel panel-default">
                        <div class="panel-heading">
                            Other Details
                        </div>
                        <div class="panel-body">
                            <div class="row col-md-12">
                                <div class="row padding2">
                                    <div class="col-md-6">
                                        <div class="col-md-4">
                                            From Town
                                        </div>
                                        <div class="col-md-8">
                                            <asp:TextBox ID="txtFromTown" runat="server" AutoPostBack="true" Width="80%"></asp:TextBox>
                                            <asp:ImageButton ID="btnFromTown" runat="server" ImageUrl="~/Images/icon_search.png"
                                                ImageAlign="Middle" OnClick="btnFromTown_Click" />
                                        </div>
                                    </div>
                                    <div class="col-md-6">
                                        <div class="col-md-4">
                                            To Town
                                        </div>
                                        <div class="col-md-8">
                                            <asp:TextBox ID="txtToTown" runat="server" AutoPostBack="true" Width="80%"></asp:TextBox>
                                            <asp:ImageButton ID="btnToTown" runat="server" ImageUrl="~/Images/icon_search.png"
                                                ImageAlign="Middle" OnClick="btnToTown_Click" />
                                        </div>
                                    </div>
                                </div>
                                <div class="row padding2">
                                    <div class="col-md-6">
                                        <div class="col-md-4">
                                            Address 1
                                        </div>
                                        <div class="col-md-8">
                                            <asp:TextBox ID="txtAddFrom" Width="80%" runat="server" />
                                        </div>
                                    </div>
                                    <div class="col-md-6">
                                        <div class="col-md-4">
                                            Address 2
                                        </div>
                                        <div class="col-md-8">
                                            <asp:TextBox ID="txtAddTo" Width="80%" runat="server" />
                                        </div>
                                    </div>
                                </div>
                                <div class="row padding2">
                                    <div class="col-md-6">
                                        <div class="col-md-4">
                                            No Of Passenger
                                        </div>
                                        <div class="col-md-8">
                                            <asp:TextBox ID="txtNonOfPassengers" onkeydown="return jsDecimals(event);" Width="80%" runat="server" OnTextChanged="txtNonOfPassengers_TextChanged" />
                                        </div>
                                    </div>
                                    <div class="col-md-6">
                                        <div class="col-md-4">
                                            Vehicle Type
                                        </div>
                                        <div class="col-md-8">
                                            <asp:DropDownList ID="ddlVehicleType" Width="80%" runat="server">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                                <div class="row padding2">
                                    <div class="col-md-6">
                                        <div class="col-md-4">
                                            Return Date
                                        </div>
                                        <div class="col-md-8">
                                            <asp:TextBox ID="txtRetuenDate" Enabled="false" CssClass="input-xlarge focused"
                                                runat="server" Width="80%" onkeypress="return RestrictSpace()"></asp:TextBox>
                                            <asp:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="True"
                                                Format="dd/MMM/yyyy" PopupButtonID="calR" TargetControlID="txtRetuenDate">
                                            </asp:CalendarExtender>
                                            <img alt="Calendar.." height="16" src="images/calendar.png" width="16" id="calR" style="cursor: pointer" />
                                        </div>
                                    </div>
                                    <div class="col-md-6">
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="row rowmargin0">
                <div class="col-md-12">
                    <div class="panel panel-default">
                        <div class="panel-heading">
                            Enquiry
                        </div>
                        <div class="panel-body">
                            <div class="row col-md-12">
                                <div class="row padding2">
                                    <div class="col-md-12">
                                        <div class="col-md-2">
                                        </div>
                                        <div class="col-md-10">
                                            <asp:TextBox ID="txtEnquiry" runat="server" AutoPostBack="true" Width="100%" Height="60px" MaxLength="500"
                                                Rows="100" TextMode="MultiLine"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="row rowmargin0">
                <div class="col-md-12">
                    <div class="panel panel-default">
                        <div class="panel-heading">
                            Details
                        </div>
                        <div class="panel-body">
                            <div class="row col-md-12">
                                <div class="row padding2">
                                    <div class="col-md-2">
                                    </div>
                                    <div class="col-md-10">
                                        <asp:GridView ID="dgvAddedDetails" runat="server" AutoGenerateColumns="False" AllowSorting="True" EmptyDataText="No data found">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Enquiry ID">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgce_enq_id" runat="server" Text='<%# Bind("gce_enq_id") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Reference">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgce_ref" runat="server" Text='<%# Bind("gce_ref") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Type">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblGCE_ENQ_TP" runat="server" Text='<%# Bind("GCE_ENQ_TP") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Company">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgce_com" runat="server" Text='<%# Bind("gce_com") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="PC">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgce_pc" runat="server" Text='<%# Bind("gce_pc") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Date">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbltDate" runat="server" Text='<%# Bind("gce_dt","{0:dd/MMM/yyyy}") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Customer Code">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgce_cus_cd" runat="server" Text='<%# Bind("gce_cus_cd") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Name">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgce_name" runat="server" Text='<%# Bind("gce_name") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Address 1" Visible="False">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgce_add1" runat="server" Text='<%# Bind("gce_add1") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Mobile" Visible="False">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgce_mob" runat="server" Text='<%# Bind("gce_mob") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="NIC" Visible="False">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgce_nic" runat="server" Text='<%# Bind("gce_nic") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Expected Date">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblExpectDate" runat="server" Text='<%# Bind("gce_expect_dt","{0:dd/MMM/yyyy}") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Enquiry" Visible="False">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgce_enq" runat="server" Text='<%# Bind("gce_enq") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Status" Visible="False">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label12" runat="server" Text='<%# Bind("mes_desc") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText=" ">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="btnDeleteRecord" Text="text" runat="server" OnClientClick="return confDel()" OnClick="btnDeleteRecord_Click">
                                                            <span class="glyphicon glyphicon-remove" aria-hidden="true" ></span>
                                                        </asp:LinkButton>
                                                    </ItemTemplate>
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
            <div style="float: left; height: 22px; width: 100%;">
                <asp:HiddenField ID="_isExsit" runat="server" />
                <asp:HiddenField ID="_isGroup" runat="server" />
                <asp:HiddenField ID="HiddenField1" runat="server" />
                <asp:HiddenField ID="SeqNumber" runat="server" />
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
