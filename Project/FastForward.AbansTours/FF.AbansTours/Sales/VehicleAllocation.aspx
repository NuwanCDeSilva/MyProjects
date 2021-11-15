<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="VehicleAllocation.aspx.cs" Inherits="FF.AbansTours.Sales.VehicleAllocation" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="TimePicker" Namespace="MKB.TimePicker" TagPrefix="MKB" %>
<%@ Register Src="~/UserControls/uc_VehicleEnquiry.ascx" TagPrefix="uc1" TagName="uc_VehicleEnquiry" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script type="text/javascript">
         function showSuccessToast() {
            $().toastmessage('showSuccessToast', "Success Dialog which is fading away ...");
        }
        function showStickySuccessToast(value) {
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
        function showNoticeToast() {
            $().toastmessage('showNoticeToast', "Notice  Dialog which is fading away ...");
        }
        function showStickyNoticeToast(value) {
            $().toastmessage('showToast', {
                text: value,
                sticky: true,
                position: 'top-left',
                type: 'notice',
                closeText: '',
                close: function () { console.log("toast is closed ..."); }
            });
        }
        function showWarningToast() {
            $().toastmessage('showWarningToast', "Warning Dialog which is fading away ...");
        }
        function showStickyWarningToast(value) {
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

        function showErrorToast() {
            $().toastmessage('showErrorToast', "Error Dialog which is fading away ...");
        }
        function showStickyErrorToast(value) {

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





<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="upCommonBtn" runat="server">
        <ContentTemplate>
            <div style="height: 22px; width: 100%; text-align: right; background-color: Silver;">
                <asp:Button ID="btnCreate" Text="Save" runat="server" Width="80px" OnClick="btnCreate_Click" />
                <asp:Button ID="btnClear" Text="Clear" runat="server" Width="80px" OnClick="btnClear_Click" />
                <asp:Button ID="btnBack" Text="Back" runat="server" Width="80px" OnClick="btnBack_Click" />
                <asp:Button ID="btnPrint" Text="Print" runat="server" Width="80px" OnClick="btnPrint_Click" />
                <asp:ConfirmButtonExtender ID="ConfirmButtonExtender3" runat="server" TargetControlID="btnClear"
                    ConfirmText="Do you want to clear?" ConfirmOnFormSubmit="false">
                </asp:ConfirmButtonExtender>
                <asp:ConfirmButtonExtender ID="ConfirmButtonExtender4" runat="server" TargetControlID="btnCreate"
                    ConfirmText="Do you want to save?" ConfirmOnFormSubmit="false">
                </asp:ConfirmButtonExtender>
                <asp:ConfirmButtonExtender ID="ConfirmButtonExtender2" runat="server" TargetControlID="btnBack"
                    ConfirmText="Do you want go to previous page?" ConfirmOnFormSubmit="false">
                </asp:ConfirmButtonExtender>
                <asp:ConfirmButtonExtender ID="ConfirmButtonExtender5" runat="server" TargetControlID="btnPrint"
                    ConfirmText="Do you want go to previous page?" ConfirmOnFormSubmit="false">
                </asp:ConfirmButtonExtender>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnPrint" />
        </Triggers>
    </asp:UpdatePanel>
    <div class="col-md-12">
        &nbsp;
    </div>
    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <div class="row rowmargin0">
                <div class="col-md-12">
                    <div class="panel panel-default">
                        <div class="panel-heading pannelheading">
                            Fleet Schedule
                        </div>
                        <div class="panel-body">
                            <div class="row rowmargin0 col-md-12">
                                <div class="row padding2">
                                    <div class="col-md-6">
                                        <div class="col-md-4 padding2">
                                            Pay Type
                                        </div>
                                        <div class="col-md-8 padding2">
                                            <asp:DropDownList ID="ddlPayType" runat="server" Width="80%" CssClass="ddlhight1"></asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-md-6">
                                        <asp:Button ID="btnNewCustomer" Text="New Customer" runat="server" Width="100px" OnClick="btnNewCustomer_Click" />
                                        <asp:ConfirmButtonExtender ID="ConfirmButtonExtender1" runat="server" TargetControlID="btnNewCustomer" ConfirmText="Do you want to add new customer details?" ConfirmOnFormSubmit="false">
                                        </asp:ConfirmButtonExtender>
                                    </div>
                                </div>

                                <div class="row">
                                    <div class="col-md-6">
                                        <div class="col-md-4 padding2">
                                            Enquiry ID
                                        </div>
                                        <div class="col-md-8 padding2">
                                            <asp:TextBox ID="txtEnquiryID" runat="server" Width="80%" AutoPostBack="True" OnTextChanged="txtEnquiryID_TextChanged" />
                                            <asp:Label ID="lblEnquirySeq" Text="0" Visible="false" runat="server" />
                                            <asp:ImageButton ID="btnEnquiryID" runat="server" ImageUrl="~/Images/icon_search.png" ImageAlign="Middle" OnClick="btnEnquiryID_Click" />
                                            <asp:ImageButton ID="btnEnquiryIDLoad" runat="server" ImageUrl="~/Images/LoadDetails.png" ImageAlign="Middle" OnClick="btnEnquiryIDLoad_Click" />
                                        </div>
                                    </div>
                                    <div class="col-md-6">
                                        <div class="col-md-4 padding2">
                                            Customer
                                        </div>
                                        <div class="col-md-8 padding2">
                                            <asp:TextBox ID="txtCustomer" runat="server" Width="80%" AutoPostBack="True" OnTextChanged="txtCustomer_TextChanged" />
                                            <asp:ImageButton ID="btnCustomer" runat="server" ImageUrl="~/Images/icon_search.png" ImageAlign="Middle" OnClick="btnCustomer_Click" />
                                            <asp:ImageButton ID="btnCustomerLoad" Visible="false" runat="server" ImageUrl="~/Images/LoadDetails.png" ImageAlign="Middle" OnClick="btnCustomerLoad_Click" />
                                        </div>
                                    </div>
                                </div>

                                <div class="row">
                                    <div class="col-md-6">
                                        <div class="col-md-4 padding2">
                                            Facility Com
                                        </div>
                                        <div class="col-md-8 padding2">
                                            <asp:TextBox ID="txtFacilityCom" runat="server" Width="80%" AutoPostBack="True" OnTextChanged="txtFacilityCom_TextChanged" />
                                            <asp:ImageButton ID="btnFacilityCom" runat="server" ImageUrl="~/Images/icon_search.png" ImageAlign="Middle" OnClick="btnFacilityCom_Click" />
                                            <asp:ImageButton ID="btnFacilityComLoad" runat="server" ImageUrl="~/Images/LoadDetails.png" ImageAlign="Middle" OnClick="btnFacilityComLoad_Click" />
                                        </div>
                                    </div>
                                    <div class="col-md-6">
                                        <div class="col-md-4 padding2">
                                            Facility SBL
                                        </div>
                                        <div class="col-md-8 padding2">
                                            <asp:TextBox ID="txtFacilitySBL" ReadOnly="true" runat="server" Width="80%" />
                                        </div>
                                    </div>
                                </div>

                                <div class="row">
                                    <div class="col-md-6">
                                        <div class="col-md-4 padding2">
                                            Facility
                                        </div>
                                        <div class="col-md-8 padding2">
                                            <asp:TextBox ID="txtFacility" Text="Transport" ReadOnly="true" runat="server" Width="80%" />
                                        </div>
                                    </div>
                                    <div class="col-md-6">
                                        <div class="col-md-4 padding2">
                                            Reference Num
                                        </div>
                                        <div class="col-md-8 padding2">
                                            <asp:TextBox ID="txtReferenceNum" runat="server" Width="80%" />
                                        </div>
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
                        Trip Details
                    </div>
                    <div class="panel-body">
                        <div class="row rowmargin0 col-md-12">
                            <div class="row">
                                <div class="col-md-6">
                                    <div class="col-md-4 padding2">
                                        From Town
                                    </div>
                                    <div class="col-md-8 padding2">
                                        <asp:TextBox ID="txtFromTown" runat="server" Width="80%" />
                                        <asp:ImageButton ID="btnFromTown" runat="server" ImageUrl="~/Images/icon_search.png" ImageAlign="Middle" OnClick="btnFromTown_Click" />
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="col-md-4 padding2">
                                        To Town
                                    </div>
                                    <div class="col-md-8 padding2">
                                        <asp:TextBox ID="txtToTown" runat="server" Width="80%" />
                                        <asp:ImageButton ID="btnToTown" runat="server" ImageUrl="~/Images/icon_search.png" ImageAlign="Middle" OnClick="btnToTown_Click" />
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-6">
                                    <div class="col-md-4 padding2">
                                        Address 1
                                    </div>
                                    <div class="col-md-8 padding2">
                                        <asp:TextBox ID="txtAddress1" runat="server" Width="80%" />
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="col-md-4 padding2">
                                        Address 2
                                    </div>
                                    <div class="col-md-8 padding2">
                                        <asp:TextBox ID="txtAddress2" runat="server" Width="80%" />
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-6">
                                    <div class="col-md-4 padding2">
                                        No Of Passengers
                                    </div>
                                    <div class="col-md-8 padding2">
                                        <asp:TextBox ID="txtNoOfPassengers" onkeydown="return jsDecimals(event);" AutoPostBack="true" runat="server" Width="80%" OnTextChanged="txtNoOfPassengers_TextChanged" />
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="col-md-4 padding2">
                                        Vehicle Type
                                    </div>
                                    <div class="col-md-8 padding2">
                                        <asp:DropDownList ID="ddlVehicleType" runat="server" Width="80%" CssClass="ddlhight1"></asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-6">
                                    <div class="col-md-4 padding2">
                                        Expected Date
                                    </div>
                                    <div class="col-md-8 padding2">
                                        <asp:TextBox ID="txtExpectedDate" CssClass="input-xlarge focused" runat="server" Width="40%"></asp:TextBox>
                                        <asp:CalendarExtender ID="txtExpectedDateExtender" runat="server" Enabled="True" Format="dd/MMM/yyyy" PopupButtonID="cal" TargetControlID="txtExpectedDate"></asp:CalendarExtender>
                                        <img alt="Calendar.." height="16" src="../images/calendar.png" width="16" id="cal" style="cursor: pointer" />
                                        <MKB:TimeSelector ID="tmExpect" Height="12px" runat="server" BorderStyle="None" DisplayButtons="False" DisplaySeconds="False" SelectedTimeFormat="TwentyFour"></MKB:TimeSelector>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="col-md-4 padding2">
                                        Return Date
                                    </div>
                                    <div class="col-md-8 padding2">
                                        <asp:TextBox ID="txtReturnDate" CssClass="input-xlarge focused" runat="server" Width="40%" AutoPostBack="True" EnableViewState="True" CausesValidation="True"></asp:TextBox>
                                        <asp:CalendarExtender ID="txtReturnDateExtender" runat="server" Enabled="True" Format="dd/MMM/yyyy" PopupButtonID="cal2" TargetControlID="txtReturnDate"></asp:CalendarExtender>
                                        <img alt="Calendar.." height="16" src="../images/calendar.png" width="16" id="cal2" style="cursor: pointer" />
                                        <MKB:TimeSelector ID="tmReturn" Height="12px" runat="server" BorderStyle="None" DisplayButtons="False" DisplaySeconds="False" SelectedTimeFormat="TwentyFour"></MKB:TimeSelector>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-6">
                                    <div class="col-md-4 padding2">
                                        Vehicle
                                    </div>
                                    <div class="col-md-8 padding2">
                                        <asp:TextBox ID="txtVehicle" runat="server" Width="80%" />
                                        <asp:ImageButton ID="btnVehicle" runat="server" ImageUrl="~/Images/icon_search.png" ImageAlign="Middle" OnClick="btnVehicle_Click" />
                                        <asp:ImageButton ID="lbtnSeEnquery" runat="server" ImageUrl="~/Images/load1.png" ImageAlign="Middle" OnClick="lbtnSeEnquery_Click" />
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="col-md-4 padding2">
                                        Driver
                                    </div>
                                    <div class="col-md-8 padding2">
                                        <asp:TextBox ID="txtDriver" AutoPostBack="true" runat="server" Width="20%" OnTextChanged="txtDriver_TextChanged" />
                                        <asp:ImageButton ID="btnDriver" runat="server" ImageUrl="~/Images/icon_search.png" ImageAlign="Middle" OnClick="btnDriver_Click" />
                                        <asp:TextBox ID="txtDriverName" ReadOnly="true" runat="server" Width="60%" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <div class="row rowmargin0 col-md-6">
                <div class="panel panel-default">
                    <div class="panel-heading">
                        Charges
                    </div>
                    <div class="panel-body">
                        <div class="row rowmargin0 col-md-12">
                            <div class="row">
                                <div class="col-md-4 padding2">
                                    Charge Code
                                </div>
                                <div class="col-md-8 padding2">
                                    <asp:TextBox ID="txtChargeCode" runat="server" Width="80%" AutoPostBack="True" OnTextChanged="txtChargeCode_TextChanged" />
                                    <asp:ImageButton ID="btnChargeCode" runat="server" ImageUrl="~/Images/icon_search.png" ImageAlign="Middle" OnClick="btnChargeCode_Click" />
                                    <asp:ImageButton ID="btnChargeCodeLoad" runat="server" ImageUrl="~/Images/LoadDetails.png" ImageAlign="Middle" OnClick="btnChargeCodeLoad_Click" />
                                </div>
                                <div class="col-md-4 padding2">
                                    Unit Rate
                                </div>
                                <div class="col-md-8 padding2">
                                    <asp:TextBox ID="txtUnitRate" onkeydown="return jsDecimals(event);"  runat="server" Width="80%" />
                                </div>
                                <div class="row">
                                    <asp:Button ID="btnAdd" Text="Add" runat="server" Width="200px" OnClick="btnAdd_Click" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <div class="row rowmargin0 col-md-6">
                <div class="panel panel-default">
                    <div class="panel-heading">
                        Charge Items
                    </div>
                    <div class="panel-body">
                        <div class="row rowmargin0 col-md-12">
                            <div class="row">
                                <asp:GridView ID="dgvChargeItems" runat="server" AutoGenerateColumns="False" OnRowCommand="dgvChargeItems_RowCommand" OnRowDeleting="dgvChargeItems_RowDeleting" OnRowEditing="dgvChargeItems_RowEditing">
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
        </ContentTemplate>
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
                <iframe style="width: 920px; height: 500px;" id="irm1" src="../Reports_Module/Financial_Rep/TripRequestSheetPrint.aspx" runat="server"></iframe>
            </div>
        </asp:Panel>
        <asp:Button ID="btnMDprint" runat="server" Text="D3" Style="display: none" />
        <asp:ModalPopupExtender ID="mpReceiptPrint" runat="server" DynamicServicePath=""
            Enabled="True" PopupControlID="pnlReceiptPrint" TargetControlID="btnMDprint"
            BackgroundCssClass="modalBackground" PopupDragHandleControlID="pnlReceiptPrint">
        </asp:ModalPopupExtender>
    </div>

     <asp:UpdatePanel runat="server" ID="update">
        <ContentTemplate>
            <asp:Button ID="Button3" runat="server" Text="Button" Style="display: none;" />
            <asp:ModalPopupExtender ID="PopupSearch" runat="server" Enabled="True" TargetControlID="Button3"
                PopupControlID="testPanel" CancelControlID="btnClose" PopupDragHandleControlID="PopupHeader" Drag="true" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:Panel runat="server" ID="testPanel" DefaultButton="lbtnSearch">
        <div runat="server" id="test" class="panel panel-primary Mheight" style="width: 900px;">
            <asp:Label ID="Label3" runat="server" Text="Label" Visible="false"></asp:Label>
            <div class="panel panel-default" style="height: 370px;">
                <div class="panel-heading" style="height: 25px;">
                    <div class="col-sm-11"></div>
                    <div class="col-sm-1">
                        <asp:LinkButton ID="btnClose" runat="server">
                             <span class="glyphicon glyphicon-remove"  aria-hidden="true">Close</span>
                        </asp:LinkButton>
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
                            <asp:ScriptManagerProxy ID="ScriptManagerProxy10" runat="server"></asp:ScriptManagerProxy>
                            <asp:UpdatePanel ID="UpdatePanel10" runat="server">
                                <ContentTemplate>
                                    <div class="col-sm-2 paddingRight5">
                                        <asp:DropDownList ID="cmbSearchbykey" runat="server" class="form-control" AutoPostBack="false">
                                        </asp:DropDownList>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                            <div class="col-sm-2 labelText1">
                                Search by word
                            </div>

                            <asp:Label ID="lblSearchType" runat="server" Text="" Visible="False"></asp:Label>
                            <div class="col-sm-3 paddingRight5">
                                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                    <ContentTemplate>
                                        <asp:TextBox ID="txtSearchbyword" CausesValidation="false" class="form-control" AutoPostBack="false" runat="server"></asp:TextBox>
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
                                        GridLines="None" CssClass="table table-hover table-striped" PageSize="7" PagerStyle-CssClass="cssPager" OnPageIndexChanging="dgvResult_PageIndexChanging" OnSelectedIndexChanged="dgvResult_SelectedIndexChanged">
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

    <asp:UpdatePanel runat="server" ID="up1">
        <ContentTemplate>
            <asp:Button ID="Button1" runat="server" Text="Button" Style="display: none;" />
            <asp:ModalPopupExtender ID="PopupEnquery" runat="server" Enabled="True" TargetControlID="Button1"
                PopupControlID="EnqueryPanel" CancelControlID="btnClose" PopupDragHandleControlID="PopupHeader" Drag="true" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>
        </ContentTemplate>
    </asp:UpdatePanel>

    <asp:Panel runat="server" ID="EnqueryPanel" DefaultButton="lbtnSearch">
        <div runat="server" id="Div1" class="panel panel-primary Mheight" style="width: 1000px;">
            <div class="col-sm-12" style="padding-left:0px; padding-right:0px;">
                 <asp:Label ID="Label1" runat="server" Text="Label" Visible="false"></asp:Label>
            <div class="panel panel-default" style="height: 447px;">
                <div class="panel-heading" style="height: 25px; padding-right:0px;">
                    <div class="col-sm-11"></div>
                    <div class="col-sm-1">
                        <div style="margin-top:5px; padding-right:0px;">
                            <asp:LinkButton ID="LinkButton1" OnClick="LinkButton1_Click" runat="server">
                             <span class="glyphicon glyphicon-remove"  aria-hidden="true">Close</span>
                        </asp:LinkButton>
                        </div>
                    </div>
                </div>
                <div class="panel-body" style="padding-left:0px; padding-right:0px;">
                    <div class="row">
                       <%-- add user controler --%>
                        <br />
                        <asp:UpdatePanel runat="server">
                            <ContentTemplate>
                                 <uc1:uc_VehicleEnquiry runat="server" id="uc_VehicleEnquiry" />
                            </ContentTemplate>
                        </asp:UpdatePanel>
                       
                    </div>
                    
                  
                    <div class="row">
                        <div class="col-sm-12">
                            <asp:ScriptManagerProxy ID="ScriptManagerProxy3" runat="server"></asp:ScriptManagerProxy>
                            <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                                <ContentTemplate>
                                    <asp:GridView ID="GridView1" CausesValidation="false" runat="server" AllowPaging="True"
                                        GridLines="None" CssClass="table table-hover table-striped" PageSize="7" PagerStyle-CssClass="cssPager" OnPageIndexChanging="dgvResult_PageIndexChanging" OnSelectedIndexChanged="dgvResult_SelectedIndexChanged">
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
</asp:Content>
