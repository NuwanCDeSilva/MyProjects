<%@ Page Title="" Language="C#" MasterPageFile="~/View/AdminSite.Master" AutoEventWireup="true" CodeBehind="ReceiptEntry.aspx.cs" Inherits="FastForward.SCMWeb.View.Transaction.Finance.ReceiptEntry" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/UserControls/ucPaymodes.ascx" TagPrefix="uc1" TagName="ucPaymodes" %>
<%@ Register Src="~/UserControls/ucCustomer.ascx" TagPrefix="uc1" TagName="ucCustomer" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">


    <script src="<%=Request.ApplicationPath%>Js/jquery.min.js"></script>
    <script src="<%=Request.ApplicationPath%>Js/bootstrap.min.js"></script>


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

        .readOnlyText {
            background-color: #F2F2F2 !important;
            color: #C6C6C6;
            border-color: #ddd;
        }
    </style>

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
                position: 'top-center',
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
        function confSave() {
            var res = confirm("Do you want to save?");
            if (res) {
                return true;
            }
            else {
                return false;
            }
        };
        function confClear() {
            var res = confirm("Do you want to clear?");
            if (res) {
                return true;
            }
            else {
                return false;
            }
        };

        function confCancel() {
            var res = confirm("Do you want to cancel?");
            if (res) {
                return true;
            }
            else {
                return false;
            }
        };

        function confDeleInvoItems() {
            var res = confirm("Do you want to delete this item?");
            if (res) {
                return true;
            }
            else {
                return false;
            }
        };

        function ChangeSelctedTab(inValue) {
            document.getElementById("lstVehicleRegistrationDetails").className = "";
            document.getElementById("lstVehicleInsuranceDetails").className = "";
            document.getElementById("lstItemDetails").className = "";
            document.getElementById("lstGiftVoucherDetails").className = "";
            document.getElementById("lstAdditionalDetails").className = "";
            if (inValue == 0) {
                document.getElementById("lstVehicleRegistrationDetails").className = "active";
            }
            else if (inValue == 1) {
                document.getElementById("lstVehicleInsuranceDetails").className = "active";
            }
            else if (inValue == 2) {
                document.getElementById("lstItemDetails").className = "active";
            }
            else if (inValue == 3) {
                document.getElementById("lstGiftVoucherDetails").className = "active";
            }
            else if (inValue == 4) {
                document.getElementById("lstAdditionalDetails").className = "active";
            };
        };


        function confAllocate() {
            var result = confirm("Do you want to allocate?");
            if (result) {
                return true;
            }
            else {
                return false;
            }
        }

    </script>

    <script type="text/javascript">
        function selectnormalserializedprice() {
            var result = confirm("This item is having normal serialized price. Do you need to select normal serialized price?");
            if (result) {
                document.getElementById('<%=Con_selectnormalserializedprice.ClientID %>').value = "Y";
            }
            else {
                document.getElementById('<%=Con_selectnormalserializedprice.ClientID %>').value = "N";
            }
        };

        function continuewithnormalSerializedprice() {
            var result = confirm("This item having normal serialized price. Do you need to continue with normal serialized price?");
            if (result) {
                document.getElementById('<%=Con_continuewithnormalSerializedprice.ClientID %>').value = "Y";
            }
            else {
                document.getElementById('<%=Con_continuewithnormalSerializedprice.ClientID %>').value = "N";
            }
        };
        function selectPromotionalSerializedPrice() {
            var result = confirm("This item is having promotional serialized price.Do you need to select promotional serialized price?");
            if (result) {
                document.getElementById('<%=Con_selectPromotionalSerializedPrice.ClientID %>').value = "Y";
            }
            else {
                document.getElementById('<%=Con_selectPromotionalSerializedPrice.ClientID %>').value = "N";
            }
        };

        function selectPromotionalSerializedPriceTwo() {
            var result = confirm("This item is having promotional serialized price. Do you need to select promotional serialized price?");
            if (result) {
                document.getElementById('<%=Con_selectPromotionalSerializedPriceTwo.ClientID %>').value = "Y";
            }
            else {
                document.getElementById('<%=Con_selectPromotionalSerializedPriceTwo.ClientID %>').value = "N";
            }
        };
        function continueWithTheAvailablePromotions() {
            var result = confirm("This item is having promotions. Do you need to continue with the available promotions?");
            if (result) {
                document.getElementById('<%=Con_continueWithTheAvailablePromotions.ClientID %>').value = "Y";
                document.getElementById('<%=TxtAdvItem.ClientID %>').onblur();
            }
            else {
                document.getElementById('<%=Con_continueWithTheAvailablePromotions.ClientID %>').value = "N";
            }
        };

        function selectPriceBookPrice(input) {
            var result = confirm(input);
            if (result) {
                document.getElementById('<%=Con_selectPriceBookPrice.ClientID %>').value = "Y";
            }
            else {
                document.getElementById('<%=Con_selectPriceBookPrice.ClientID %>').value = "N";
            };
            document.getElementById('<%= btnChechQty.ClientID %>').click();
        };
        function itemswhichYouSelectIsSorrect() {
            var result = confirm("Confirm the items which you select is correct.?");
            if (result) {
                document.getElementById('<%=Con_itemswhichYouSelectIsSorrect.ClientID %>').value = "Y";
                document.getElementById('<%= btnSaveSub.ClientID %>').click();
            }
            else {
                document.getElementById('<%=Con_itemswhichYouSelectIsSorrect.ClientID %>').value = "N";
            }
        };

        function RegistrationIsNotAvailableAreYouSure() {
            var result = confirm("Commission is not calculated. Reason: Registration is not available. Are you sure ?");
            if (result) {
                document.getElementById('<%=Con_RegistrationIsNotAvailableAreYouSure.ClientID %>').value = "Y";
                document.getElementById('<%= btnSaveSub.ClientID %>').click();
            }
            else {
                document.getElementById('<%=Con_RegistrationIsNotAvailableAreYouSure.ClientID %>').value = "N";
            }
        };
        function RegAftrThAlowPridAreYouSure() {
            var result = confirm("Commission is not calculated. Reason: Registration is done after the allow period. Are you sure ?");
            if (result) {
                document.getElementById('<%=Con_RegAftrThAlowPridAreYouSure.ClientID %>').value = "Y";
                document.getElementById('<%= btnSaveSub.ClientID %>').click();
            }
            else {
                document.getElementById('<%=Con_RegAftrThAlowPridAreYouSure.ClientID %>').value = "N";
            }
        };
        function CommissionIsNotCalculated() {
            var result = confirm("Commission is not calculated. Reason: Registration is not available. Are you sure ?");
            if (result) {
                document.getElementById('<%=Con_CommissionIsNotCalculated.ClientID %>').value = "Y";
                document.getElementById('<%= btnSave.ClientID %>').click();  //btnSaveSub
            }
            else {
                document.getElementById('<%=Con_CommissionIsNotCalculated.ClientID %>').value = "N";
            }
        };

        function IsThisSelectProvinceAndDistrictIsCorrect() {
            var result = confirm("Is this select province and district is correct ?");
            if (result) {
                document.getElementById('<%=Con_IsThisSelectProvinceAndDistrictIsCorrect.ClientID %>').value = "Y";
                document.getElementById('<%= btnPaymentSub.ClientID %>').click();
            }
            else {
                document.getElementById('<%=Con_IsThisSelectProvinceAndDistrictIsCorrect.ClientID %>').value = "N";
            }
        };
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" runat="server">
    <asp:UpdateProgress ID="UpdateProgress3" DisplayAfter="80"
        runat="server" AssociatedUpdatePanelID="upTopControls">
        <ProgressTemplate>
            <div class="divWaiting">
                <asp:Label ID="lblWait3" runat="server" Text="Please wait... " />
                <asp:Image ID="imgWait3" runat="server" ImageAlign="Middle" ImageUrl="~/images/icons/ajax-loader.gif" />
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <asp:UpdateProgress ID="UpdateProgress4" DisplayAfter="20"
        runat="server" AssociatedUpdatePanelID="upCustomerDetails">
        <ProgressTemplate>
            <div class="divWaiting">
                <asp:Label ID="lblWait4" runat="server" Text="Please wait... " />
                <asp:Image ID="imgWait4" runat="server" ImageAlign="Middle" ImageUrl="~/images/icons/ajax-loader.gif" />
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <asp:UpdateProgress ID="UpdateProgress6" DisplayAfter="20"
        runat="server" AssociatedUpdatePanelID="UpdatePanel1">
        <ProgressTemplate>
            <div class="divWaiting">
                <asp:Label ID="lblWait5" runat="server" Text="Please wait... " />
                <asp:Image ID="imgWait5" runat="server" ImageAlign="Middle" ImageUrl="~/images/icons/ajax-loader.gif" />
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <div class="panel panel-default marginLeftRight5">
        <div class="panel-body paddingtop0 paddingbottom0">
            <asp:ScriptManagerProxy ID="ScriptManagerProxy1" runat="server"></asp:ScriptManagerProxy>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <asp:HiddenField ID="hdfClear" runat="server" />
                    <asp:HiddenField ID="hdfSave" runat="server" />
                    <asp:HiddenField ID="hdfSerialDelete" runat="server" />

                    <asp:HiddenField ID="Con_selectnormalserializedprice" runat="server" />
                    <asp:HiddenField ID="Con_continuewithnormalSerializedprice" runat="server" />
                    <asp:HiddenField ID="Con_selectPromotionalSerializedPrice" runat="server" />
                    <asp:HiddenField ID="Con_selectPromotionalSerializedPriceTwo" runat="server" />
                    <asp:HiddenField ID="Con_continueWithTheAvailablePromotions" runat="server" />
                    <asp:HiddenField ID="Con_selectPriceBookPrice" runat="server" />

                    <asp:HiddenField ID="Con_itemswhichYouSelectIsSorrect" runat="server" />
                    <asp:HiddenField ID="Con_RegistrationIsNotAvailableAreYouSure" runat="server" />
                    <asp:HiddenField ID="Con_RegAftrThAlowPridAreYouSure" runat="server" />
                    <asp:HiddenField ID="Con_CommissionIsNotCalculated" runat="server" />

                    <asp:HiddenField ID="Con_IsThisSelectProvinceAndDistrictIsCorrect" runat="server" />

                    <asp:HiddenField ID="hdfCheckQty" runat="server" />
                    
                    <asp:HiddenField ID="amountLabl" runat="server" />
                    <asp:Button ID="btnChechQty" Text="btnChechQty" runat="server" OnClick="btnChechQty_Click" Visible="false" />




                    <div class="row">
                        <div class="col-sm-12" style="height: 28px;">
                            <div class="col-sm-2 ">
                                <asp:Label ID="lblBackDateInfor" Text="" runat="server" Visible="false" />
                                <asp:Button ID="btnTest" Text="Test" runat="server" OnClick="btnTest_Click" Visible="false" />
                            </div>
                            <div class="col-sm-5  buttonRow floatRight padding0 paddingtopbottom0">
                                <div class="col-sm-2 paddingRight0">
                                    <asp:LinkButton ID="btnAllocate" CausesValidation="false" runat="server" CssClass="floatRight" OnClientClick="return confAllocate()" OnClick="btnAllocate_Click" Visible="false">
                                        <span class="glyphicon glyphicon-tags" aria-hidden="true"></span>Allocate
                                    </asp:LinkButton>
                                </div>
                                <div class="col-sm-2 paddingRight0">
                                    <asp:LinkButton ID="btnSave" CausesValidation="false" runat="server" CssClass="floatRight" OnClientClick="return confSave()" OnClick="btnSave_Click">
                                        <span class="glyphicon glyphicon-saved" aria-hidden="true"></span>Save
                                    </asp:LinkButton>
                                    <asp:Button ID="btnSaveSub" Text="btnSaveSub" runat="server" Visible="false" OnClick="btnSaveSub_Click" />
                                </div>
                                <div class="col-sm-2 paddingRight0">
                                    <asp:LinkButton ID="btnCancel" CausesValidation="false" runat="server" CssClass="floatRight" OnClientClick="return confCancel()" OnClick="btnCancel_Click">
                                        <span class="glyphicon glyphicon-remove" aria-hidden="true"></span>Cancel
                                    </asp:LinkButton>
                                </div>
                                <div class="col-sm-2">
                                    <asp:LinkButton ID="btnClear" runat="server" CssClass="floatRight" OnClientClick="return confClear()" OnClick="btnClear_Click">
                                        <span class="glyphicon glyphicon-refresh" aria-hidden="true"></span>Clear
                                    </asp:LinkButton>
                                </div>

                                <div class="col-sm-2">
                                    <asp:LinkButton ID="LinkButton3" CausesValidation="false" runat="server" OnClick="lbtnPrint_Click">
                                                        <span class="glyphicon glyphicon-print" aria-hidden="true"></span>Print
                                    </asp:LinkButton>
                                </div>

                            </div>
                        </div>
                    </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
            <asp:ScriptManagerProxy ID="ScriptManagerProxy8" runat="server"></asp:ScriptManagerProxy>
            <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                <ContentTemplate>
                    <div class="row">
                        <asp:MultiView ID="MultiView1" runat="server" OnActiveViewChanged="MultiView1_ActiveViewChanged">
                            <asp:View ID="View1" runat="server">
                                <asp:UpdatePanel ID="upTopControls" runat="server">
                                    <ContentTemplate>
                                        <div class="col-sm-12">
                                            <div class="panel panel-default marginBottom0">
                                                <div class="panel-heading height16 padding0">
                                                    <div class="col-sm-12 padding0">
                                                        <div class="col-sm-10 padding0">
                                                            Receipt Entry
                                                        </div>
                                                        <div class="col-sm-2 padding0">
                                                            <asp:CheckBox ID="chkUnAllocated" Text="Search un-allocated receipts" runat="server" Visible="false" />
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="panel-body paddingtopbottom0">
                                                    <div class="col-sm-4 padding0">
                                                        <div class="col-sm-6 padding0">
                                                            <div class="col-sm-4 labelText1 paddingLeft0">
                                                                Receipt Type
                                                            </div>
                                                            <div class="col-sm-6 paddingRight5 paddingLeft0">
                                                                <asp:TextBox ID="txtRecType" runat="server" CssClass="form-control" AutoPostBack="true" OnTextChanged="txtRecType_TextChanged" />
                                                            </div>
                                                            <div class="col-sm-1 paddingLeft0">
                                                                <asp:LinkButton ID="btnRecType" runat="server" CausesValidation="false" OnClick="btnRecType_Click">
                                                        <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                                </asp:LinkButton>
                                                            </div>
                                                        </div>
                                                        <div class="col-sm-6 padding0">
                                                            <div class="col-sm-3 labelText1 paddingLeft0">
                                                                Division
                                                            </div>
                                                            <div class="col-sm-7 paddingRight5 paddingLeft0">
                                                                <asp:TextBox ID="txtDivision" runat="server" CssClass="form-control" AutoPostBack="true" OnTextChanged="txtDivision_TextChanged" />
                                                            </div>
                                                            <div class="col-sm-1 paddingLeft0">
                                                                <asp:LinkButton ID="btnDivision" runat="server" CausesValidation="false" OnClick="btnDivision_Click">
                                                        <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                                </asp:LinkButton>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="col-sm-4 padding0">
                                                        <div class="col-sm-6 padding0">
                                                            <div class="col-sm-3 labelText1 paddingLeft0">
                                                                Date
                                                            </div>
                                                            <div class="col-sm-7 paddingRight5 paddingLeft0">
                                                                <asp:TextBox ID="dtpRecDate" runat="server" CssClass="form-control" ReadOnly="true" />
                                                                <asp:CalendarExtender ID="dtpRecDateCal" runat="server" TargetControlID="dtpRecDate"
                                                                    PopupButtonID="btndtpRecDate" Format="dd/MMM/yyyy">
                                                                </asp:CalendarExtender>
                                                            </div>
                                                            <div class="col-sm-1 paddingLeft0">
                                                                <asp:LinkButton ID="btndtpRecDate" TabIndex="1" CausesValidation="false" runat="server">
                                                     <span class="glyphicon glyphicon-calendar" aria-hidden="true"></span>
                                                                </asp:LinkButton>
                                                            </div>
                                                        </div>
                                                        <div class="col-sm-6 padding0">
                                                            <div class="col-sm-3 labelText1 paddingLeft0">
                                                                Receipt No
                                                            </div>
                                                            <div class="col-sm-7 paddingRight5 paddingLeft0">
                                                                <asp:TextBox ID="txtRecNo" runat="server" CssClass="form-control" AutoPostBack="true" OnTextChanged="txtRecNo_TextChanged" />
                                                            </div>
                                                            <div class="col-sm-1 paddingLeft0">
                                                                <asp:LinkButton ID="btnReceiptNo" runat="server" CausesValidation="false" OnClick="btnReceiptNo_Click">
                                                        <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                                </asp:LinkButton>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="col-sm-4 padding0">
                                                        <div class="col-sm-4 padding0">
                                                            <asp:RadioButton ID="radioButtonManual" Text="Manual" runat="server" GroupName="ReTypes" AutoPostBack="true" OnCheckedChanged="radioButtonManual_CheckedChanged" CssClass="RadioBtnStace" />
                                                            <asp:RadioButton ID="radioButtonSystem" Text="System" runat="server" GroupName="ReTypes" AutoPostBack="true" OnCheckedChanged="radioButtonSystem_CheckedChanged" CssClass="RadioBtnStace" />
                                                        </div>
                                                        <div class="col-sm-8 padding0">
                                                            <div class="col-sm-6 padding0">
                                                                <div class="col-sm-3 labelText1 paddingLeft0">
                                                                    Prefix
                                                                </div>
                                                                <div class="col-sm-7 paddingRight5 paddingLeft0">
                                                                    <asp:DropDownList ID="comboBoxPrefix" runat="server" CssClass="form-control"></asp:DropDownList>
                                                                </div>
                                                                <div class="col-sm-1 paddingLeft0">
                                                                </div>
                                                            </div>
                                                            <div class="col-sm-6 padding0">
                                                                <div class="col-sm-3 labelText1 paddingLeft0">
                                                                    Ref
                                                                </div>
                                                                <div class="col-sm-7 paddingRight5 paddingLeft0">
                                                                    <asp:TextBox ID="txtManual" runat="server" CssClass="form-control" AutoPostBack="true" OnTextChanged="txtManual_TextChanged" />
                                                                </div>
                                                                <div class="col-sm-1 paddingLeft0">
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                                <div class="col-sm-6 paddingRight0">
                                    <div class="panel panel-default marginBottom0">
                                        <div class="panel-heading height16 padding0">
                                            Receive From
                                        </div>
                                        <div class="panel-body paddingbottom0">
                                            <asp:UpdatePanel ID="upCustomerDetails" runat="server">
                                                <ContentTemplate>
                                                    <div class="col-sm-12 padding0">
                                                        <div class="col-sm-6 padding0">
                                                            <div class="col-sm-4 labelText1 paddingLeft0">
                                                                Code
                                                            </div>
                                                            <div class="col-sm-6 paddingRight5 paddingLeft0">
                                                                <asp:TextBox ID="txtCusCode" runat="server" CssClass="form-control" AutoPostBack="true" OnTextChanged="txtCusCode_TextChanged" />
                                                            </div>
                                                            <div class="col-sm-1 paddingLeft0">
                                                                <asp:LinkButton ID="btnCode" runat="server" CausesValidation="false" OnClick="btnCode_Click">
                                                        <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                                </asp:LinkButton>
                                                            </div>
                                                        </div>
                                                        <div class="col-sm-6 padding0">
                                                            <div class="col-sm-6 ">
                                                                <asp:Button ID="btnCustomer" Text="Customer" runat="server" Width="100%" OnClick="btnCustomer_Click" />
                                                            </div>
                                                            <div class="col-sm-6 ">
                                                                <asp:Button ID="btn_add_ser" Text="Add Items" runat="server" Width="100%" OnClick="btn_add_ser_Click" />
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="col-sm-12 padding0">
                                                        <div class="col-sm-2 labelText1 paddingLeft0">
                                                            Name
                                                        </div>
                                                        <div class="col-sm-9 paddingRight5 paddingLeft0">
                                                            <asp:TextBox ID="txtCusName" runat="server" CssClass="form-control" />
                                                        </div>
                                                        <div class="col-sm-1 paddingLeft0">
                                                        </div>
                                                    </div>
                                                    <div class="col-sm-12 padding0">
                                                        <div class="col-sm-2 labelText1 paddingLeft0">
                                                            Address
                                                        </div>
                                                        <div class="col-sm-9 paddingRight5 paddingLeft0">
                                                            <asp:TextBox ID="txtCusAdd1" runat="server" CssClass="form-control" />
                                                        </div>
                                                        <div class="col-sm-1 paddingLeft0">
                                                        </div>
                                                    </div>
                                                    <div class="col-sm-12 padding0">
                                                        <div class="col-sm-2 labelText1 paddingLeft0">
                                                        </div>
                                                        <div class="col-sm-9 paddingRight5 paddingLeft0">
                                                            <asp:TextBox ID="txtCusAdd2" runat="server" CssClass="form-control" />
                                                        </div>
                                                        <div class="col-sm-1 paddingLeft0">
                                                        </div>
                                                    </div>
                                                    <div class="col-sm-12 padding0">
                                                        <div class="col-sm-6 padding0">
                                                            <div class="col-sm-4 labelText1 paddingLeft0">
                                                                NIC
                                                            </div>
                                                            <div class="col-sm-6 paddingRight5 paddingLeft0">
                                                                <asp:TextBox ID="txtNIC" runat="server" CssClass="form-control" AutoPostBack="true" OnTextChanged="txtNIC_TextChanged" MaxLength="12" />
                                                            </div>
                                                            <div class="col-sm-1 paddingLeft0">
                                                            </div>
                                                        </div>
                                                        <div class="col-sm-6 padding0">
                                                            <div class="col-sm-4 labelText1 paddingLeft0">
                                                                Mobile
                                                            </div>
                                                            <div class="col-sm-6 paddingRight5 paddingLeft0">
                                                                <asp:TextBox ID="txtMobile" runat="server" CssClass="form-control" AutoPostBack="true" OnTextChanged="txtMobile_TextChanged" />
                                                            </div>
                                                            <div class="col-sm-1 paddingLeft0">
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="col-sm-12 padding0">
                                                        <div class="col-sm-6 padding0">
                                                            <div class="col-sm-4 labelText1 paddingLeft0">
                                                                District
                                                            </div>
                                                            <div class="col-sm-6 paddingRight5 paddingLeft0">
                                                                <asp:DropDownList ID="cmbDistrict" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="cmbDistrict_SelectedIndexChanged"></asp:DropDownList>
                                                            </div>
                                                            <div class="col-sm-1 paddingLeft0">
                                                            </div>
                                                        </div>
                                                        <div class="col-sm-6 padding0">
                                                            <div class="col-sm-4 labelText1 paddingLeft0">
                                                                Province
                                                            </div>
                                                            <div class="col-sm-6 paddingRight5 paddingLeft0">
                                                                <asp:TextBox ID="txtProvince" runat="server" CssClass="form-control" />
                                                            </div>
                                                            <div class="col-sm-1 paddingLeft0">
                                                            </div>
                                                        </div>
                                                    </div>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-sm-6">
                                    <div class="panel panel-default">
                                        <asp:Panel ID="gbsettle" runat="server">
                                            <div class="panel panel-default">
                                                <div class="panel-heading height16 padding0">
                                                    Settlement Details
                                                </div>
                                                <div class="panel-body">
                                                    <div class="col-sm-12 padding0">
                                                        <div class="col-sm-4 padding0">
                                                            <div class="col-sm-4 labelText1 paddingLeft0">
                                                                Invoice 
                                                            </div>
                                                            <div class="col-sm-6 paddingRight5 paddingLeft0">
                                                                <asp:TextBox ID="txtInvoice" runat="server" CssClass="form-control" AutoPostBack="true" OnTextChanged="txtInvoice_TextChanged" />
                                                            </div>
                                                            <div class="col-sm-1 paddingLeft0">
                                                                <asp:LinkButton ID="btnInvoiceSearch" runat="server" CausesValidation="false" OnClick="btnInvoiceSearch_Click">
                                                        <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                                </asp:LinkButton>
                                                            </div>
                                                        </div>
                                                        <div class="col-sm-3 padding0">
                                                            <div class="col-sm-4 labelText1 paddingLeft0">
                                                                Amount 
                                                            </div>
                                                            <div class="col-sm-6 paddingRight5 paddingLeft0">
                                                                <asp:TextBox ID="txtBalance" runat="server" onkeydown="return jsDecimals(event);" Style="text-align: right;" CssClass="form-control" />
                                                            </div>
                                                            <div class="col-sm-1 paddingLeft0">
                                                            </div>
                                                        </div>
                                                        <div class="col-sm-3 padding0">
                                                            <div class="col-sm-6 paddingLeft0">
                                                                <asp:CheckBox ID="chkOth" Text="Oth. SR" runat="server" AutoPostBack="true" OnCheckedChanged="chkOth_CheckedChanged" />
                                                            </div>
                                                            <div class="col-sm-6 paddingLeft0 paddingRight0">
                                                                <div class="col-sm-9 paddingLeft0 paddingRight0">
                                                                    <asp:TextBox ID="txtOthSR" runat="server" CssClass="form-control" AutoPostBack="true" OnTextChanged="txtOthSR_TextChanged" />
                                                                </div>
                                                                <div class="col-sm-2 padding0">
                                                                    <asp:LinkButton ID="btnOthSR" runat="server" CausesValidation="false" OnClick="btnOthSR_Click">
                                                                    <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                                    </asp:LinkButton>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="col-sm-2 padding0">
                                                            <asp:LinkButton ID="btnAddInvcoices" runat="server" CausesValidation="false" OnClick="btnAddInvcoices_Click">
                                                                    <span class="glyphicon glyphicon-plus" aria-hidden="true"></span>
                                                            </asp:LinkButton>
                                                        </div>
                                                        <div class="col-sm-3 paddingLeft0 buttonRow" id="invExcelUpload">
                                                            <div class="col-sm-12  paddingRight0 paddingLeft0">
                                                                <asp:LinkButton Visible="false" ID="lbtnExcelUpload" runat="server" OnClick="lbtnExcelUpload_Click">
                                                                    <span class="glyphicon glyphicon-upload" aria-hidden="true"></span>Excel Upload
                                                                </asp:LinkButton>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <asp:Panel ID="gbItem" runat="server">
                                                        <div class="col-sm-12 padding0">
                                                            <div class="col-sm-6 padding0">
                                                                <div class="col-sm-4 labelText1 paddingLeft0">
                                                                    Item 
                                                                </div>
                                                                <div class="col-sm-6 paddingRight5 paddingLeft0">
                                                                    <asp:TextBox ID="txtItem" runat="server" Style="text-transform: uppercase;" CssClass="form-control" AutoPostBack="true" OnTextChanged="txtItem_TextChanged" />
                                                                </div>
                                                                <div class="col-sm-1 paddingLeft0">
                                                                    <asp:LinkButton ID="btnItem" runat="server" CausesValidation="false" OnClick="btnItem_Click">
                                                        <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                                    </asp:LinkButton>
                                                                </div>
                                                            </div>
                                                            <div class="col-sm-6 padding0">
                                                                <div class="col-sm-4 labelText1 paddingLeft0">
                                                                    <asp:Label ID="lblSer1" Text="Engine #" runat="server" />
                                                                </div>
                                                                <div class="col-sm-6 paddingRight5 paddingLeft0">
                                                                    <asp:TextBox ID="txtengine" runat="server" CssClass="form-control" AutoPostBack="true" OnTextChanged="txtengine_TextChanged" />
                                                                </div>
                                                                <div class="col-sm-1 paddingLeft0">
                                                                    <asp:LinkButton ID="btnSerial" runat="server" CausesValidation="false" OnClick="btnSerial_Click">
                                                        <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                                    </asp:LinkButton>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="col-sm-12 padding0">
                                                            <div class="col-sm-6 padding0">
                                                                <div class="col-sm-4 labelText1 paddingLeft0">
                                                                    <asp:Label ID="lblSer2" Text="Chassis #" runat="server" />
                                                                </div>
                                                                <div class="col-sm-6 paddingRight5 paddingLeft0">
                                                                    <asp:TextBox ID="txtChasis" runat="server" CssClass="form-control" />
                                                                </div>
                                                                <div class="col-sm-1 paddingLeft0">
                                                                    <asp:LinkButton ID="btnChasis" runat="server" CausesValidation="false" Visible="false">
                                                                        <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                                    </asp:LinkButton>
                                                                </div>
                                                            </div>
                                                            <div class="col-sm-6 padding0">
                                                                <asp:CheckBox ID="chkDel" Text="Delivery items" runat="server" />
                                                            </div>
                                                        </div>
                                                    </asp:Panel>
                                                    <div class="col-sm-12 padding0">
                                                        <asp:Panel ID="gbInsu" runat="server">
                                                            <div class="panel panel-default">
                                                                <div class="panel-heading height16 padding0">
                                                                    Insurance Details
                                                                </div>
                                                                <div class="panel-body">
                                                                    <div class="col-sm-12 padding0">
                                                                        <div class="col-sm-5 padding0">
                                                                            <div class="col-sm-3 labelText1 paddingLeft0">
                                                                                Company
                                                                            </div>
                                                                            <div class="col-sm-8 paddingRight5 paddingLeft0">
                                                                                <asp:TextBox ID="txtInsCom" runat="server" CssClass="form-control" AutoPostBack="true" OnTextChanged="txtInsCom_TextChanged" />
                                                                            </div>
                                                                            <div class="col-sm-1 paddingLeft0">
                                                                                <asp:LinkButton ID="btnInsCom" runat="server" CausesValidation="false" OnClick="btnInsCom_Click">
                                                                                    <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                                                </asp:LinkButton>
                                                                            </div>
                                                                        </div>
                                                                        <div class="col-sm-5 padding0">
                                                                            <div class="col-sm-2 labelText1 paddingLeft0">
                                                                                Policy
                                                                            </div>
                                                                            <div class="col-sm-9 paddingRight5 paddingLeft0">
                                                                                <asp:TextBox ID="txtInsPol" runat="server" CssClass="form-control" AutoPostBack="true" OnTextChanged="txtInsPol_TextChanged" />
                                                                            </div>
                                                                            <div class="col-sm-1 paddingLeft0">
                                                                                <asp:LinkButton ID="btnPolicy" runat="server" CausesValidation="false" OnClick="btnPolicy_Click">
                                                                                    <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                                                </asp:LinkButton>
                                                                            </div>
                                                                        </div>
                                                                        <div class="col-sm-2 padding0">
                                                                            <asp:CheckBox ID="chkAnnual" Text="Annual" runat="server" />
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </asp:Panel>
                                                    </div>
                                                    <div class="col-sm-12 padding0">
                                                        <asp:Panel ID="pnlItemAlloc" runat="server" Visible="false">
                                                            <div class="panel panel-default">
                                                                <div class="panel panel-default">
                                                                    <div class="panel-heading padding0">
                                                                        Allocation Details
                                                                    </div>
                                                                    <div class="panel-body">
                                                                        <div class="col-sm-12 padding0">
                                                                            <div class="col-sm-4 padding0">
                                                                                <div class="col-sm-4 labelText1 paddingLeft0">
                                                                                    Qty
                                                                                </div>
                                                                                <div class="col-sm-6 paddingRight5 paddingLeft0">
                                                                                    <asp:TextBox ID="txtAllocQty" Enabled="false" onkeydown="return jsDecimals(event);" Style="text-align: right;" runat="server" CssClass="form-control" />
                                                                                </div>
                                                                                <div class="col-sm-1 paddingLeft0">
                                                                                </div>
                                                                            </div>
                                                                            <div class="col-sm-4 padding0">
                                                                                <div class="col-sm-4 labelText1 paddingLeft0">
                                                                                    Pre-Order Qty
                                                                                </div>
                                                                                <div class="col-sm-6 paddingRight5 paddingLeft0">
                                                                                    <asp:TextBox ID="txtRecQty" Enabled="false" onkeydown="return jsDecimals(event);" Style="text-align: right;" runat="server" CssClass="form-control" />
                                                                                </div>
                                                                                <div class="col-sm-1 paddingLeft0">
                                                                                </div>
                                                                            </div>
                                                                            <div class="col-sm-4 padding0">
                                                                                <div class="col-sm-4 labelText1 paddingLeft0">
                                                                                    Free Qty
                                                                                </div>
                                                                                <div class="col-sm-6 paddingRight5 paddingLeft0">
                                                                                    <asp:TextBox ID="txtFreeQty" Enabled="false" onkeydown="return jsDecimals(event);" Style="text-align: right;" runat="server" CssClass="form-control" />
                                                                                </div>
                                                                                <div class="col-sm-1 paddingLeft0">
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
                                        </asp:Panel>

                                        <div class="col-sm-12 padding0">
                                            <asp:Panel ID="pnlDisposal" runat="server" Visible="false">
                                                <div class="panel panel-default">
                                                    <div class="panel-heading height16 padding0">
                                                        Disposal Job Number
                                                    </div>
                                                    <div class="panel-body">
                                                        <div class="col-sm-12 padding0">
                                                            <div class="col-sm-5 padding0">
                                                                <div class="col-sm-3 labelText1 paddingLeft0">
                                                                    Job Number
                                                                </div>
                                                                <div class="col-sm-8 paddingRight5 paddingLeft0">
                                                                    <asp:TextBox ID="txtDisposalJob" runat="server" CssClass="form-control" AutoPostBack="true" OnTextChanged="txtDisposalJob_TextChanged" />
                                                                </div>
                                                                <div class="col-sm-1 paddingLeft0">
                                                                    <asp:LinkButton ID="btnSearchDisposal" runat="server" CausesValidation="false" OnClick="btnSearchDisposal_Click">
                                                                                    <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                                    </asp:LinkButton>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </asp:Panel>
                                        </div>

                                        <div class="col-sm-12 padding0">
                                            <asp:Panel ID="pnlPaymentsadd" runat="server">
                                                <div class="col-sm-6 padding0">
                                                    <div class="col-sm-4 labelText1 paddingLeft0">
                                                        Customer Payment
                                                    </div>
                                                    <div class="col-sm-5 paddingRight5 paddingLeft0">
                                                        <asp:TextBox ID="txtPayment" Style="text-align: right" runat="server" onkeydown="return jsDecimals(event);" CssClass="form-control" />
                                                    </div>
                                                    <div class="col-sm-2 paddingLeft0">
                                                        <asp:LinkButton ID="btnAddPanyment" runat="server" CausesValidation="false" OnClick="btnAddPanyment_Click">
                                                        <span class="glyphicon glyphicon-plus" aria-hidden="true"></span>
                                                        </asp:LinkButton>
                                                        <asp:Button ID="btnPayment" Visible="false" Text="Add" runat="server" OnClick="btnPayment_Click" />
                                                        <asp:Button ID="btnPaymentSub" Text="Add" runat="server" OnClick="btnPaymentSub_Click" Style="display: none" />
                                                    </div>
                                                </div>
                                                <div class="col-sm-5 padding0">
                                                    <asp:Panel ID="pnlExtraChg" runat="server">
                                                        <div class="col-sm-5 labelText1 paddingLeft0">
                                                            Extra Charge
                                                        </div>
                                                        <div class="col-sm-6 paddingRight5 paddingLeft0">
                                                            <asp:Label ID="lblExtraChg" Text="text" runat="server" />
                                                        </div>
                                                        <div class="col-sm-1 paddingLeft0">
                                                            <asp:Button Text="Add" runat="server" />
                                                        </div>
                                                    </asp:Panel>
                                                </div>
                                            </asp:Panel>
                                            <div class="col-sm-6 padding0">
                                                <div class="col-sm-2 labelText1 paddingLeft0">
                                                    Remarks
                                                </div>
                                                <div class="col-sm-10 paddingRight5 paddingLeft0 ">
                                                    <asp:TextBox ID="txtRemarks" CssClass="form-control" TextMode="MultiLine" runat="server" />
                                                </div>
                                            </div>
                                        </div>

                                        <asp:Panel ID="gbGVDet" runat="server" Visible="false">
                                            <asp:UpdatePanel runat="server">
                                                <ContentTemplate>
                                                    <div class="panel panel-default">
                                                        <div class="panel-heading height16 padding0">
                                                            Gift Voucher Details
                                                        </div>
                                                        <div class="panel-body marginBottom0">
                                                            <div class="col-sm-12 padding0">
                                                                <div class="col-sm-12 padding0">
                                                                    <div class="col-sm-6 padding0">
                                                                        <div class="col-sm-4 labelText1 paddingLeft0">
                                                                            Code
                                                                        </div>
                                                                        <div class="col-sm-6 paddingRight5 paddingLeft0">
                                                                            <asp:TextBox ID="txtGVCode" runat="server" CssClass="form-control" AutoPostBack="true" OnTextChanged="txtGVCode_TextChanged" />
                                                                        </div>
                                                                        <div class="col-sm-1 paddingLeft0">
                                                                            <asp:LinkButton ID="btnGVCode" runat="server" CausesValidation="false" OnClick="btnGVCode_Click">
                                                                                <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                                            </asp:LinkButton>
                                                                        </div>
                                                                    </div>
                                                                    <div class="col-sm-6 padding0">
                                                                        <div class="col-sm-4 labelText1 paddingLeft0">
                                                                            Book
                                                                        </div>
                                                                        <div class="col-sm-6 paddingRight5 paddingLeft0">
                                                                            <asp:DropDownList ID="cmbGvBook" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="cmbGvBook_SelectedIndexChanged"></asp:DropDownList>
                                                                        </div>
                                                                        <div class="col-sm-1 paddingLeft0">
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class="col-sm-12 padding0">
                                                                    <div class="col-sm-6 padding0">
                                                                        <div class="col-sm-4 labelText1 paddingLeft0">
                                                                            Pages From
                                                                        </div>
                                                                        <div class="col-sm-6 paddingRight5 paddingLeft0">
                                                                            <asp:Label ID="lblFrompg" Text="text" runat="server" />
                                                                        </div>
                                                                        <div class="col-sm-1 paddingLeft0">
                                                                        </div>
                                                                    </div>
                                                                    <div class="col-sm-6 padding0">
                                                                        <div class="col-sm-4 labelText1 paddingLeft0">
                                                                            Pages To
                                                                        </div>
                                                                        <div class="col-sm-6 paddingRight5 paddingLeft0">
                                                                            <asp:DropDownList ID="cmbTopg" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="cmbTopg_SelectedIndexChanged"></asp:DropDownList>
                                                                        </div>
                                                                        <div class="col-sm-1 paddingLeft0">
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class="col-sm-12 padding0">
                                                                    <div class="col-sm-6 padding0">
                                                                        <div class="col-sm-4 labelText1 paddingLeft0">
                                                                            No of issue pages
                                                                        </div>
                                                                        <div class="col-sm-6 paddingRight5 paddingLeft0">
                                                                            <asp:Label ID="lblPageCount" Text="text" runat="server" />
                                                                        </div>
                                                                        <div class="col-sm-1 paddingLeft0">
                                                                        </div>
                                                                    </div>
                                                                    <div class="col-sm-6 padding0">
                                                                    </div>
                                                                </div>
                                                                <div class="col-sm-12 padding0">
                                                                    <div class="col-sm-6 padding0">
                                                                        <div class="col-sm-4 labelText1 paddingLeft0">
                                                                            Page Value
                                                                        </div>
                                                                        <div class="col-sm-6 paddingRight5 paddingLeft0">
                                                                            <asp:TextBox ID="txtPgAmt" Style="text-align: right" runat="server" onkeydown="return jsDecimals(event);" CssClass="form-control" AutoPostBack="true" OnTextChanged="txtPgAmt_TextChanged" />
                                                                        </div>
                                                                        <div class="col-sm-1 paddingLeft0">
                                                                        </div>
                                                                    </div>
                                                                    <div class="col-sm-6 padding0">
                                                                        <asp:CheckBox ID="chkGvFOC" Text="Issue as FOC" runat="server" AutoPostBack="true" OnCheckedChanged="chkGvFOC_CheckedChanged" />
                                                                    </div>
                                                                </div>
                                                                <div class="col-sm-12 padding0">
                                                                    <div class="col-sm-6 padding0">
                                                                        <div class="col-sm-4 labelText1 paddingLeft0">
                                                                            Total Value
                                                                        </div>
                                                                        <div class="col-sm-6 paddingRight5 paddingLeft0">
                                                                            <asp:TextBox ID="txtTotGvAmt" Style="text-align: right" runat="server" onkeydown="return jsDecimals(event);" CssClass="form-control" />
                                                                        </div>
                                                                        <div class="col-sm-1 paddingLeft0">
                                                                        </div>
                                                                    </div>
                                                                    <div class="col-sm-6 padding0">
                                                                        <div class="col-sm-3 labelText1 paddingLeft0">
                                                                            <asp:Button ID="btnAddGv" Text="Add" runat="server" OnClick="btnAddGv_Click" />
                                                                        </div>
                                                                        <div class="col-sm-9 paddingRight5 paddingLeft0">
                                                                            <div class="col-sm-6 padding0">
                                                                                <asp:Label ID="lblGVExp" Text="Expired On" runat="server" />
                                                                            </div>
                                                                            <div class="col-sm-6 padding0">
                                                                                <div class="col-sm-10 padding0">
                                                                                    <asp:TextBox ID="dtGVExp" runat="server" CssClass="form-control" />
                                                                                    <asp:CalendarExtender ID="dtGVExpCal" runat="server" TargetControlID="dtGVExp"
                                                                                        PopupButtonID="btnExpirdOn" Format="dd/MMM/yyyy">
                                                                                    </asp:CalendarExtender>
                                                                                </div>
                                                                                <div class="col-sm-2 padding0">
                                                                                    <asp:LinkButton ID="btnExpirdOn" TabIndex="1" CausesValidation="false" runat="server">
                                                                <span class="glyphicon glyphicon-calendar" aria-hidden="true"></span>
                                                                                    </asp:LinkButton>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class="col-sm-12 padding0">
                                                                    <div class="col-sm-4 labelText1 paddingLeft0">
                                                                    </div>
                                                                    <div class="col-sm-6 paddingRight5 paddingLeft0">
                                                                        <asp:CheckBox ID="chkAllowPromo" Text="Vouchers allow for promotions" runat="server" />
                                                                    </div>
                                                                    <div class="col-sm-1 paddingLeft0">
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </asp:Panel>

                                        <asp:Panel ID="pnlDebtInvoices" runat="server">
                                            <asp:UpdatePanel runat="server">
                                                <ContentTemplate>
                                                    <div class="panel panel-default">
                                                        <div class="panel-heading height16 padding0">
                                                            Invoice Allocation
                                                        </div>
                                                        <div class="panel-body marginBottom0">
                                                            <div class="col-sm-12 padding0">
                                                                <div class="col-sm-3 padding0">
                                                                    <div class="col-sm-3 padding0">
                                                                        Invoice
                                                                    </div>
                                                                    <div class="col-sm-7 padding0">
                                                                        <asp:TextBox ID="txtInvoiceNumAllo" CssClass="form-control" runat="server" AutoPostBack="true" OnTextChanged="txtInvoiceNumAllo_TextChanged" />
                                                                    </div>
                                                                    <div class="col-sm-1 padding0">
                                                                        <asp:LinkButton ID="btnSearchInvoiceAll" CausesValidation="false" runat="server" OnClick="btnSearchInvoiceAll_Click">
                                                                    <span class="glyphicon glyphicon-search" aria-hidden="true" style="font-size:15px"></span> 
                                                                        </asp:LinkButton>
                                                                    </div>
                                                                </div>
                                                                <div class="col-sm-2 padding0">
                                                                    <div class="col-sm-4 padding0">
                                                                        Amount
                                                                    </div>
                                                                    <div class="col-sm-8 padding0" style="padding-left: 6px;">
                                                                        <asp:TextBox ID="txtAmountAlloca" Style="text-align: right" CssClass="form-control" runat="server" onkeydown="return jsDecimals(event);" />
                                                                    </div>
                                                                </div>
                                                                <div class="col-sm-3 padding0">
                                                                    <div class="col-sm-6 padding0" style="padding-left: 10px;">
                                                                        Pay Type
                                                                    </div>
                                                                    <div class="col-sm-6 padding0">
                                                                        <asp:DropDownList ID="cmdPayTypeAlloca" CssClass="form-control" runat="server" AutoPostBack="true" OnSelectedIndexChanged="cmdPayTypeAlloca_SelectedIndexChanged"></asp:DropDownList>
                                                                    </div>
                                                                </div>
                                                                <div class="col-sm-3 padding0" style="padding-left: 10px;">
                                                                    <div class="col-sm-3 padding0">
                                                                        Cheque
                                                                    </div>
                                                                    <div class="col-sm-9 padding0">
                                                                        <asp:DropDownList ID="cmdCheqNums" CssClass="form-control" runat="server" Enabled="false"></asp:DropDownList>
                                                                    </div>
                                                                </div>
                                                                <div class="col-sm-1 padding0 paddingLeft15">
                                                                    <asp:LinkButton ID="btnAddInvoiceAlloc" CausesValidation="false" runat="server" OnClick="btnAddInvoiceAlloc_Click">
                                                                    <span class="glyphicon glyphicon-plus" aria-hidden="true" style="font-size:15px"></span> 
                                                                    </asp:LinkButton>
                                                                </div>
                                                            </div>
                                                            <div class="col-sm-12 padding0">
                                                                <asp:GridView ID="dgvDebtInvoiceList" AutoGenerateColumns="False" TabIndex="39" runat="server" CssClass="table table-hover table-striped" GridLines="None" ShowHeaderWhenEmpty="True" EmptyDataText="No data found...">
                                                                    <Columns>
                                                                        <asp:TemplateField HeaderText="Invoice">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblcol_regInv" runat="server" Text='<%# Bind("col_regInv") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Amount">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblcol_Amount" runat="server" Text='<%# Bind("col_Amount", "{0:N2}") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                            <HeaderStyle CssClass="gridHeaderAlignRight" />
                                                                            <ItemStyle HorizontalAlign="Right" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Pay Type">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblcol_PayType" runat="server" Text='<%# Bind("col_PayType") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Cheque No">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblcol_regModel" runat="server" Text='<%# Bind("col_regModel") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField>
                                                                            <ItemTemplate>
                                                                                <div id="delbtndiv">
                                                                                    <asp:LinkButton ID="btnDeletInvoices" CausesValidation="false" runat="server" OnClick="btnDeletInvoices_Click" OnClientClick="return confDeleInvoItems()">
                                                                                     <span class="glyphicon glyphicon-trash" aria-hidden="true" style="font-size:15px"></span> 
                                                                                    </asp:LinkButton>
                                                                                </div>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                    </Columns>
                                                                </asp:GridView>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </asp:Panel>
                                    </div>
                                </div>
                                <div class="col-sm-12">
                                    <div class="panel-body">
                                        <div class="row">
                                            <div class="col-sm-12">
                                                <uc1:ucPaymodes ID="ucPayModes1" runat="server" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-sm-12">
                                    <div class="row">
                                        <div class="col-sm-12">
                                            <ul id="myTabNew" class="nav nav-tabs">
                                                <li id="lstVehicleRegistrationDetails" class="active">
                                                    <a href="#VehicleRegistrationDetails" data-toggle="tab">Vehicle Registration Details</a>
                                                </li>
                                                <li id="lstVehicleInsuranceDetails">
                                                    <a href="#VehicleInsuranceDetails" data-toggle="tab">Vehicle Insurance Details</a>
                                                </li>
                                                <li id="lstItemDetails">
                                                    <a href="#ItemDetails" data-toggle="tab">Item Details</a>
                                                </li>
                                                <li id="lstGiftVoucherDetails">
                                                    <a href="#GiftVoucherDetails" data-toggle="tab">Gift Voucher Details</a>
                                                </li>
                                                <li id="lstAdditionalDetails">
                                                    <a href="#AdditionalDetails" data-toggle="tab">Additional Details</a>
                                                </li>

                                                <div class="col-sm-4 padding0 floatRight">
                                                    <div class="col-sm-6 padding0">
                                                        <div class="col-sm-6 padding0">
                                                            Sales Type
                                                        </div>
                                                        <div class="col-sm-6 padding0">
                                                            <asp:Label ID="lblSalesType" Text="text" runat="server" />
                                                        </div>
                                                    </div>
                                                    <div class="col-sm-6 padding0">
                                                        <div class="col-sm-6 padding0">
                                                        </div>
                                                        <div class="col-sm-6 padding0">
                                                        </div>
                                                    </div>
                                                </div>
                                            </ul>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-sm-12">
                                            <div id="myTabContent" class="tab-content">
                                                <div class="tab-pane fade in active" id="VehicleRegistrationDetails">
                                                    <asp:ScriptManagerProxy ID="ScriptManagerProxy7" runat="server"></asp:ScriptManagerProxy>
                                                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                                        <ContentTemplate>
                                                            <div class="col-sm-12 ">
                                                                <asp:GridView ID="dgvReg" AutoGenerateColumns="False" TabIndex="39" runat="server" CssClass="table table-hover table-striped" GridLines="None" ShowHeaderWhenEmpty="True" EmptyDataText="No data found...">
                                                                    <Columns>
                                                                        <%--<asp:BoundField DataField="itri_seq_no" HeaderText="Seq No" ItemStyle-Width="150" ReadOnly="true" />--%>
                                                                        <asp:BoundField DataField="col_regInv" HeaderText="Invoice" />
                                                                        <asp:BoundField DataField="col_regCus" HeaderText="Customer" />
                                                                        <asp:BoundField DataField="col_regItem" HeaderText="Item Code" />
                                                                        <asp:BoundField DataField="col_regModel" HeaderText="Model" />
                                                                        <asp:BoundField DataField="col_regBrand" HeaderText="Brand" />
                                                                        <asp:BoundField DataField="col_regDis" HeaderText="District" />
                                                                        <asp:BoundField DataField="col_regPro" HeaderText="Province" />
                                                                        <asp:BoundField DataField="col_regEngine" HeaderText="Engine#" />
                                                                        <asp:BoundField DataField="col_regChasis" HeaderText="Chasis#" />
                                                                    </Columns>
                                                                </asp:GridView>
                                                            </div>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </div>
                                                <div class="tab-pane fade" id="VehicleInsuranceDetails">
                                                    <asp:ScriptManagerProxy ID="ScriptManagerProxy2" runat="server"></asp:ScriptManagerProxy>
                                                    <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                                        <ContentTemplate>
                                                            <div class="col-sm-12 ">
                                                                <asp:GridView ID="dgvIns" AutoGenerateColumns="False" TabIndex="39" runat="server" CssClass="table table-hover table-striped" GridLines="None" ShowHeaderWhenEmpty="True" EmptyDataText="No data found...">
                                                                    <Columns>
                                                                        <%--<asp:BoundField DataField="itri_seq_no" HeaderText="Seq No" ItemStyle-Width="150" ReadOnly="true" />--%>
                                                                        <asp:BoundField DataField="col_insInv" HeaderText="Invoice" />
                                                                        <asp:BoundField DataField="col_insCus" HeaderText="Customer" />
                                                                        <asp:BoundField DataField="col_insItem" HeaderText="Item Code" />
                                                                        <asp:BoundField DataField="col_insModel" HeaderText="Model" />
                                                                        <asp:BoundField DataField="col_insDistrict" HeaderText="District" />
                                                                        <asp:BoundField DataField="col_insPro" HeaderText="Province" />
                                                                        <asp:BoundField DataField="col_insCom" HeaderText="Company" />
                                                                        <asp:BoundField DataField="col_insPol" HeaderText="Policy" />
                                                                        <asp:BoundField DataField="col_insEngine" HeaderText="Engine #" />
                                                                        <asp:BoundField DataField="col_insChasis" HeaderText="Chasis #" />
                                                                        <asp:BoundField DataField="col_insVal" HeaderText="Amount" />
                                                                    </Columns>
                                                                </asp:GridView>
                                                            </div>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </div>
                                                <div class="tab-pane fade" id="ItemDetails">
                                                    <asp:ScriptManagerProxy ID="ScriptManagerProxy3" runat="server"></asp:ScriptManagerProxy>
                                                    <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                                                        <ContentTemplate>
                                                            <div class="col-sm-12 ">
                                                                <asp:GridView ID="dgvItem" AutoGenerateColumns="False" TabIndex="39" runat="server" CssClass="table table-hover table-striped" GridLines="None" ShowHeaderWhenEmpty="True" EmptyDataText="No data found...">
                                                                    <Columns>
                                                                        <asp:BoundField DataField="col_itmItem" HeaderText="Item Code" />
                                                                        <asp:BoundField DataField="col_itmDesc" HeaderText="Description" />
                                                                        <asp:BoundField DataField="col_itmModel" HeaderText="Model" />
                                                                        <asp:BoundField DataField="col_itmStatus" HeaderText="Status" Visible="false" />
                                                                        <asp:BoundField DataField="col_itmStatus_Desc" HeaderText="Status" />
                                                                        <asp:BoundField DataField="col_itmSerial" HeaderText="Serial #" />
                                                                        <asp:BoundField DataField="col_itmOthSerial" HeaderText="Other Serial" />
                                                                        <asp:BoundField DataField="colpb" HeaderText="Price Book" />
                                                                        <asp:BoundField DataField="colpblvl" HeaderText="Level" />
                                                                        <asp:BoundField DataField="colQty" HeaderText="Qty" DataFormatString="{0:N2}" />
                                                                        <asp:BoundField DataField="colRate" HeaderText="Unit Rate" />
                                                                        <asp:BoundField DataField="colamt" HeaderText="Amt" />
                                                                        <asp:BoundField DataField="colTax" HeaderText="Tax" />
                                                                        <asp:BoundField DataField="colpay" HeaderText="Should Pay" Visible="false" />
                                                                    </Columns>
                                                                </asp:GridView>
                                                            </div>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </div>
                                                <div class="tab-pane fade" id="GiftVoucherDetails">
                                                    <asp:ScriptManagerProxy ID="ScriptManagerProxy4" runat="server"></asp:ScriptManagerProxy>
                                                    <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                                                        <ContentTemplate>
                                                            <div class="col-sm-12 ">
                                                                <asp:GridView ID="dgvGv" AutoGenerateColumns="False" TabIndex="39" runat="server" CssClass="table table-hover table-striped" GridLines="None" ShowHeaderWhenEmpty="True" EmptyDataText="No data found...">
                                                                    <Columns>
                                                                        <%--<asp:BoundField DataField="itri_seq_no" HeaderText="Seq No" ItemStyle-Width="150" ReadOnly="true" />--%>
                                                                        <asp:BoundField HeaderText="Code" DataField="gvp_gv_cd" />
                                                                        <asp:BoundField HeaderText="Book" DataField="gvp_book" />
                                                                        <asp:BoundField HeaderText="Page" DataField="gvp_page" />
                                                                        <asp:BoundField HeaderText="Prefix" DataField="gvp_gv_prefix" />
                                                                        <asp:BoundField HeaderText="Valid From" DataField="gvp_valid_from" />
                                                                        <asp:BoundField HeaderText="Valid To" DataField="gvp_valid_to" />
                                                                        <asp:BoundField HeaderText="Amount" DataField="gvp_amt" />
                                                                    </Columns>
                                                                </asp:GridView>
                                                            </div>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </div>
                                                <div class="tab-pane fade" id="AdditionalDetails">
                                                    <asp:ScriptManagerProxy ID="ScriptManagerProxy5" runat="server"></asp:ScriptManagerProxy>
                                                    <asp:UpdatePanel ID="UpdatePanel7" runat="server">
                                                        <ContentTemplate>
                                                            <div class="col-sm-12 ">
                                                                <div class="col-sm-6 padding0">
                                                                    <div class="col-sm-4 labelText1 paddingLeft0">
                                                                        Note
                                                                    </div>
                                                                    <div class="col-sm-6 paddingRight5 paddingLeft0">
                                                                        <asp:TextBox ID="txtNote" CssClass="form-control" runat="server" TextMode="MultiLine" />
                                                                    </div>
                                                                    <div class="col-sm-1 paddingLeft0">
                                                                    </div>
                                                                </div>
                                                                <div class="col-sm-6 padding0">
                                                                    <div class="col-sm-12 ">
                                                                        <div class="col-sm-4 labelText1 paddingLeft0">
                                                                            Sales Executive
                                                                        </div>
                                                                        <div class="col-sm-6 paddingRight5 paddingLeft0">
                                                                            <asp:TextBox ID="txtSalesEx" runat="server" CssClass="form-control" />
                                                                        </div>
                                                                        <div class="col-sm-1 paddingLeft0">
                                                                            <asp:LinkButton ID="btnSearch_Executive" runat="server" CausesValidation="false">
                                                                        <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                                            </asp:LinkButton>
                                                                        </div>
                                                                    </div>
                                                                    <div class="col-sm-12 ">
                                                                        <div class="col-sm-4 labelText1 paddingLeft0">
                                                                            Total Receipt Amount
                                                                        </div>
                                                                        <div class="col-sm-6 paddingRight5 paddingLeft0">
                                                                            <asp:TextBox ID="txtTotal" runat="server" CssClass="form-control" />
                                                                        </div>
                                                                        <div class="col-sm-1 paddingLeft0">
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
                                </div>
                                <div class="col-sm-12 height10">
                                </div>
                            </asp:View>
                            <asp:View ID="View2" runat="server">
                                <div class="col-sm-12">
                                    <div class="panel panel-default">
                                        <div class="panel-heading height16 padding0">
                                            Item Details
                                        </div>
                                        <div class="panel-body">
                                            <div class="col-sm-12 padding0">
                                                <div class="col-sm-3 padding0">
                                                    <div class="col-sm-4 labelText1 paddingLeft0">
                                                        Invoice Type
                                                    </div>
                                                    <div class="col-sm-6 paddingRight5 paddingLeft0">
                                                        <asp:DropDownList ID="cmbInvType" runat="server" CssClass="form-control"></asp:DropDownList>
                                                    </div>
                                                    <div class="col-sm-1 paddingLeft0">
                                                    </div>
                                                    <div class="col-sm-12 paddingLeft0">
                                                        <asp:Label ID="lblLvlMsg" Text="" runat="server" />
                                                    </div>
                                                </div>
                                                <div class="col-sm-3 padding0">
                                                    <div class="col-sm-4 labelText1 paddingLeft0">
                                                        Price Valid Till 
                                                    </div>
                                                    <div class="col-sm-6 paddingRight5 paddingLeft0">
                                                        <asp:TextBox ID="dtpValidTill" runat="server" />
                                                        <asp:CalendarExtender ID="dtpValidTillCal" runat="server" TargetControlID="dtpValidTill"
                                                            PopupButtonID="btnPriceValidTill" Format="dd/MMM/yyyy">
                                                        </asp:CalendarExtender>
                                                    </div>
                                                    <div class="col-sm-1 paddingLeft0">
                                                        <asp:LinkButton ID="btnPriceValidTill" TabIndex="1" CausesValidation="false" runat="server">
                                                     <span class="glyphicon glyphicon-calendar" aria-hidden="true"></span>
                                                        </asp:LinkButton>
                                                    </div>
                                                </div>
                                                <div class="col-sm-3 padding0" style="padding-right: 48px">
                                                    <div class="col-sm-12 padding0" style="background-color: lavender">
                                                        <div class="col-sm-2 padding0">
                                                            <asp:CheckBox ID="chkTaxPayable" Text="" runat="server" />
                                                        </div>
                                                        <div class="col-sm-10 padding0">
                                                            Tax Payable
                                                        </div>
                                                        <div class="col-sm-12 padding0">
                                                            <div class="col-sm-8 padding0" style="padding-left: 25px;">
                                                                SVat Status
                                                            </div>
                                                            <div class="col-sm-4 padding0">
                                                                <asp:Label ID="lblVatExemptStatus" Text="None" runat="server" />
                                                            </div>
                                                        </div>
                                                        <div class="col-sm-12 padding0">
                                                            <div class="col-sm-8 padding0" style="padding-left: 25px;">
                                                                Exempt Status
                                                            </div>
                                                            <div class="col-sm-4 padding0">
                                                                <asp:Label ID="lblSVatStatus" Text="None" runat="server" />
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-sm-3 padding0" style="float: right">
                                                    <asp:UpdatePanel ID="upconfirm" runat="server">
                                                        <ContentTemplate>
                                                            <div class="col-sm-6 padding0">
                                                                <asp:Button ID="btnConfirm" Text="Confirm" runat="server" Width="100%" OnClick="btnConfirm_Click" />
                                                            </div>
                                                            <div class="col-sm-6 padding0">
                                                                <asp:Button ID="btnBack" Text="Back" runat="server" OnClick="btnBack_Click" Width="100%" />
                                                            </div>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                    <div class="col-sm-12 padding0">
                                                        <asp:UpdateProgress ID="UpdateProgress2" DisplayAfter="10"
                                                            runat="server" AssociatedUpdatePanelID="upItemAdd">
                                                            <ProgressTemplate>
                                                                <div class="divWaiting">
                                                                    <asp:Label ID="lblWait2" runat="server"
                                                                        Text="Please wait... " />
                                                                    <asp:Image ID="imgWait2" runat="server"
                                                                        ImageAlign="Middle" ImageUrl="~/images/banners/hidden_progress_bar.gif" />
                                                                </div>
                                                            </ProgressTemplate>
                                                        </asp:UpdateProgress>
                                                        <asp:UpdateProgress ID="UpdateProgress5" DisplayAfter="10"
                                                            runat="server" AssociatedUpdatePanelID="upconfirm">
                                                            <ProgressTemplate>
                                                                <div class="divWaiting">
                                                                    <asp:Label ID="lblWait5" runat="server"
                                                                        Text="Please wait... " />
                                                                    <asp:Image ID="imgWait5" runat="server"
                                                                        ImageAlign="Middle" ImageUrl="~/images/banners/hidden_progress_bar.gif" />
                                                                </div>
                                                            </ProgressTemplate>
                                                        </asp:UpdateProgress>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-sm-12 padding0">
                                                <asp:Panel ID="pnlItem" runat="server">
                                                    <asp:UpdatePanel ID="upItemAdd" runat="server">
                                                        <ContentTemplate>
                                                            <div class="col-sm-3 padding0">
                                                                <div class="col-sm-6 padding0">
                                                                    <div class="col-sm-12 padding0">
                                                                        Serial
                                                                    </div>
                                                                    <div class="col-sm-12 padding0">
                                                                        <div class="col-sm-10 padding0">
                                                                            <asp:TextBox ID="txtSerialNo" runat="server" CssClass="form-control" onblur="__doPostBack('txtSerialNo','OnBlur');" />
                                                                        </div>
                                                                        <div class="col-sm-2 padding0">
                                                                            <asp:LinkButton ID="btnSearch_Serial" runat="server" CausesValidation="false" OnClick="btnSearch_Serial_Click">
                                                        <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                                            </asp:LinkButton>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class="col-sm-6 padding0">
                                                                    <div class="col-sm-12 padding0">
                                                                        Item
                                                                    </div>
                                                                    <div class="col-sm-12 padding0">
                                                                        <div class="col-sm-10 padding0">
                                                                            <asp:TextBox ID="TxtAdvItem" runat="server" CssClass="form-control" Style="text-transform: uppercase" onblur="__doPostBack('TxtAdvItem','OnBlur');" />
                                                                        </div>
                                                                        <div class="col-sm-2 padding0">
                                                                            <asp:LinkButton ID="btnSearch_Item" runat="server" CausesValidation="false" OnClick="btnSearch_Item_Click">
                                                        <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                                            </asp:LinkButton>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="col-sm-3 padding0">
                                                                <div class="col-sm-4 padding0">
                                                                    <div class="col-sm-12 padding0">
                                                                        Book
                                                                    </div>
                                                                    <div class="col-sm-12 padding0">
                                                                        <asp:DropDownList ID="cmbBook" AutoPostBack="true" runat="server" CssClass=" form-control" OnSelectedIndexChanged="cmbBook_SelectedIndexChanged"></asp:DropDownList>
                                                                    </div>
                                                                </div>
                                                                <div class="col-sm-4 padding0">
                                                                    <div class="col-sm-12 padding0">
                                                                        Level
                                                                    </div>
                                                                    <div class="col-sm-12 padding0">
                                                                        <asp:DropDownList ID="cmbLevel" runat="server" CssClass=" form-control" AutoPostBack="true" OnSelectedIndexChanged="cmbLevel_SelectedIndexChanged"></asp:DropDownList>
                                                                    </div>
                                                                </div>
                                                                <div class="col-sm-4 padding0">
                                                                    <div class="col-sm-12 padding0">
                                                                        Status
                                                                    </div>
                                                                    <div class="col-sm-12 padding0">
                                                                        <asp:DropDownList ID="cmbStatus" runat="server" CssClass=" form-control"></asp:DropDownList>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="col-sm-3 padding0">
                                                                <div class="col-sm-3 padding0">
                                                                    <div class="col-sm-12 padding0">
                                                                        Qty
                                                                    </div>
                                                                    <div class="col-sm-12 padding0">
                                                                        <asp:TextBox ID="txtQty" Style="text-align: right" runat="server" onkeydown="return jsDecimals(event);" CssClass="form-control" AutoPostBack="true" OnTextChanged="txtQty_TextChanged" />
                                                                    </div>
                                                                </div>
                                                                <div class="col-sm-3 padding0">
                                                                    <div class="col-sm-12 padding0">
                                                                        Unit Price
                                                                    </div>
                                                                    <div class="col-sm-12 padding0">
                                                                        <asp:TextBox ID="txtUnitPrice" runat="server" onkeydown="return jsDecimals(event);" AutoPostBack="true" CssClass="form-control" />
                                                                    </div>
                                                                </div>
                                                                <div class="col-sm-3 padding0">
                                                                    <div class="col-sm-12 padding0">
                                                                        Unit Amount
                                                                    </div>
                                                                    <div class="col-sm-12 padding0">
                                                                        <asp:TextBox ID="txtUnitAmt" runat="server" onkeydown="return jsDecimals(event);" CssClass="form-control" />
                                                                    </div>
                                                                </div>
                                                                <div class="col-sm-3 padding0">
                                                                    <div class="col-sm-12 padding0">
                                                                        Dis. Rate
                                                                    </div>
                                                                    <div class="col-sm-12 padding0">
                                                                        <asp:TextBox ID="txtDisRate" Style="text-align: right" onkeydown="return jsDecimals(event);" runat="server" CssClass="form-control" AutoPostBack="true" OnTextChanged="txtDisRate_TextChanged" />
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="col-sm-3 padding0">
                                                                <div class="col-sm-3 padding0">
                                                                    <div class="col-sm-12 padding0">
                                                                        Dis. Amount
                                                                    </div>
                                                                    <div class="col-sm-12 padding0">
                                                                        <asp:TextBox ID="txtDisAmt" Style="text-align: right" runat="server" onkeydown="return jsDecimals(event);" CssClass="form-control" AutoPostBack="true" OnTextChanged="txtDisAmt_TextChanged" />
                                                                    </div>
                                                                </div>
                                                                <div class="col-sm-3 padding0">
                                                                    <div class="col-sm-12 padding0">
                                                                        Tax
                                                                    </div>
                                                                    <div class="col-sm-12 padding0">
                                                                        <asp:TextBox ID="txtTaxAmt" runat="server" onkeydown="return jsDecimals(event);" CssClass="form-control" />
                                                                    </div>
                                                                </div>
                                                                <div class="col-sm-3 padding0">
                                                                    <div class="col-sm-12 padding0">
                                                                        Line Amount
                                                                    </div>
                                                                    <div class="col-sm-12 padding0">
                                                                        <asp:TextBox ID="txtLineTotAmt" runat="server" onkeydown="return jsDecimals(event);" CssClass="form-control" />
                                                                    </div>
                                                                </div>
                                                                <div class="col-sm-3 padding0" style="padding-top: 16px; padding-left: 5px;">
                                                                    <asp:Button ID="btnAddItem" Text="Add" runat="server" Width="100%" Height="18px" OnClick="btnAddItem_Click" />
                                                                </div>
                                                            </div>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </asp:Panel>
                                            </div>
                                            <div class="col-sm-12 padding0 height10">
                                            </div>
                                            <div class="col-sm-12 padding0">
                                                <asp:GridView ID="gvInvoiceItem" AutoGenerateColumns="False" TabIndex="39" runat="server" CssClass="table table-hover table-striped" GridLines="None" ShowHeaderWhenEmpty="True" EmptyDataText="No data found...">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="No">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblsad_itm_line" runat="server" Text='<%# Bind("sad_itm_line")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Item">
                                                            <ItemTemplate>
                                                                <asp:Label ID="InvItm_Item" runat="server" Text='<%# Bind("sad_itm_cd")%>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle Width="150px" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Description">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblMi_longdesc" runat="server" Text='<%# Bind("Mi_longdesc")%>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle Width="150px" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Status">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblsad_itm_stus" runat="server" Text='<%# Bind("sad_itm_stus")%>' Visible="false"></asp:Label>
                                                                <asp:Label ID="lblsad_itm_stus_Desc" runat="server" Text='<%# Bind("sad_itm_stus_desc") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle Width="150px" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Qty" HeaderStyle-BackColor="AliceBlue">
                                                            <ItemTemplate>
                                                                <asp:Label ID="InvItm_Qty" runat="server" Text='<%# Bind("sad_qty", "{0:N2}")%>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle Width="100px" BackColor="AliceBlue" />
                                                            <HeaderStyle CssClass="gridHeaderAlignRight" />
                                                            <ItemStyle HorizontalAlign="Right" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Unit Price">
                                                            <ItemTemplate>
                                                                <asp:Label ID="InvItm_UPrice" runat="server" Text='<%# Bind("sad_unit_rt", "{0:N2}")%>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle Width="150px" />
                                                            <ItemStyle HorizontalAlign="Right" />
                                                            <HeaderStyle CssClass="gridHeaderAlignRight" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Unit Amt" HeaderStyle-BackColor="AliceBlue">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblsad_unit_amt" runat="server" Text='<%# Bind("sad_unit_amt", "{0:N2}")%>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle Width="150px" BackColor="AliceBlue" />
                                                            <ItemStyle HorizontalAlign="Right" />
                                                            <HeaderStyle CssClass="gridHeaderAlignRight" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Dis.Rate">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblsad_disc_rt" runat="server" Text='<%# Bind("sad_disc_rt", "{0:N2}")%>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle Width="150px" />
                                                            <ItemStyle HorizontalAlign="Right" />
                                                            <HeaderStyle CssClass="gridHeaderAlignRight" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Dis.Amt" HeaderStyle-BackColor="AliceBlue">
                                                            <ItemTemplate>
                                                                <asp:Label ID="InvItm_DisAmt" runat="server" Text='<%# Bind("sad_disc_amt", "{0:N2}")%>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle Width="150px" BackColor="AliceBlue" />
                                                            <ItemStyle HorizontalAlign="Right" />
                                                            <HeaderStyle CssClass="gridHeaderAlignRight" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Tax Amt">
                                                            <ItemTemplate>
                                                                <asp:Label ID="InvItm_TaxAmt" runat="server" Text='<%# Bind("sad_itm_tax_amt", "{0:N2}")%>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle Width="150px" />
                                                            <ItemStyle HorizontalAlign="Right" />
                                                            <HeaderStyle CssClass="gridHeaderAlignRight" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Line Amt" HeaderStyle-BackColor="AliceBlue">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblsad_tot_amt" runat="server" Text='<%# Bind("sad_tot_amt", "{0:N2}")%>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle Width="150px" BackColor="AliceBlue" />
                                                            <ItemStyle HorizontalAlign="Right" />
                                                            <HeaderStyle CssClass="gridHeaderAlignRight" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Book">
                                                            <ItemTemplate>
                                                                <asp:Label ID="InvItm_Book" runat="server" Text='<%# Bind("sad_pbook")%>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle Width="150px" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Level">
                                                            <ItemTemplate>
                                                                <asp:Label ID="InvItm_Level" runat="server" Text='<%# Bind("sad_pb_lvl")%>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle Width="150px" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Reservation No" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblsad_res_no" runat="server" Text='<%# Bind("sad_res_no")%>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle Width="150px" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Job Line" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="InvItm_JobLine" runat="server" Text='<%# Bind("sad_job_line")%>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle Width="150px" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Res.No" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="InvItm_ResNo" runat="server" Text='<%# Bind("sad_res_no")%>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle Width="150px" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Res.Line No" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="InvItm_ResLine" runat="server" Text='<%# Bind("sad_res_line_no")%>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle Width="150px" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Promotion Code" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="InvItm_PromoCd" runat="server" Text='<%# Bind("sad_promo_cd")%>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle Width="150px" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField>
                                                            <ItemTemplate>
                                                                <div id="delbtndiv">
                                                                    <asp:LinkButton ID="lbtndelitem" CausesValidation="false" runat="server" OnClick="lbtndelitem_Click" OnClientClick="return confDeleInvoItems()">
                                                                 <span class="glyphicon glyphicon-trash" aria-hidden="true" style="font-size:15px"></span> 
                                                                    </asp:LinkButton>
                                                                </div>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField>
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="btnAddSerials" CausesValidation="false" runat="server" Visible="false">
                                                            <span class="glyphicon glyphicon-paperclip" aria-hidden="true" style="font-size:15px"></span> 
                                                                </asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </div>

                                            <div class="col-sm-12 padding0 height10">
                                            </div>
                                            <div class="col-sm-12 padding0">
                                                <div class="col-sm-1 padding0">
                                                </div>
                                                <div class="col-sm-2 padding0">
                                                    <div class="col-sm-6 paddingRight0" style="background-color: lavender; font-weight: bold;">
                                                        Sub Total
                                                    </div>
                                                    <div class="col-sm-6 padding0">
                                                        <asp:Label ID="lblGrndSubTotal" Text="0.00" Style="font-weight: bold;" runat="server" />
                                                    </div>
                                                </div>
                                                <div class="col-sm-2 padding0">
                                                    <div class="col-sm-6 paddingRight0" style="background-color: lavender; font-weight: bold;">
                                                        Discount
                                                    </div>
                                                    <div class="col-sm-6 padding0">
                                                        <asp:Label ID="lblGrndDiscount" Text="0.00" Style="font-weight: bold;" runat="server" />
                                                    </div>
                                                </div>
                                                <div class="col-sm-2 padding0">
                                                    <div class="col-sm-6 paddingRight0" style="background-color: lavender; font-weight: bold;">
                                                        After Discount
                                                    </div>
                                                    <div class="col-sm-6 padding0">
                                                        <asp:Label ID="lblGrndAfterDiscount" Text="0.00" Style="font-weight: bold;" runat="server" />
                                                    </div>
                                                </div>
                                                <div class="col-sm-2 padding0">
                                                    <div class="col-sm-6 paddingRight0" style="background-color: lavender; font-weight: bold;">
                                                        Tax
                                                    </div>
                                                    <div class="col-sm-6 padding0">
                                                        <asp:Label ID="lblGrndTax" Text="0.00" Style="font-weight: bold;" runat="server" />
                                                    </div>
                                                </div>
                                                <div class="col-sm-2 padding0">
                                                    <div class="col-sm-6 paddingRight0" style="background-color: lavender; font-weight: bold;">
                                                        Total Amount
                                                    </div>
                                                    <div class="col-sm-6 padding0">
                                                        <asp:Label ID="lblGrndTotalAmount" Text="0.00" Style="font-weight: bold;" runat="server" />
                                                    </div>
                                                </div>
                                                <div class="col-sm-1 padding0">
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </asp:View>
                        </asp:MultiView>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>

    <asp:UpdatePanel ID="UpdatePanel8" runat="server">
        <ContentTemplate>
            <asp:Button ID="btnSearchAd" runat="server" Text="Button" Style="display: none;" />
            <asp:ModalPopupExtender ID="mpSearchAdance" runat="server" Enabled="True" TargetControlID="btnSearchAd"
                PopupControlID="pnlpopup" PopupDragHandleControlID="PopupHeader" Drag="true" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>
        </ContentTemplate>
    </asp:UpdatePanel>

    <asp:UpdatePanel ID="UpdatePanel9" runat="server">
        <ContentTemplate>
            <asp:Panel runat="server" ID="pnlpopup" DefaultButton="lbtnSearch" Style="display: none;">
                <div runat="server" id="test" class="panel panel-default width950 height450">
                    <asp:Label ID="lblvalue" runat="server" Text="Label" Visible="false"></asp:Label>
                    <div id="PopupHeader" class="panel-heading">
                        <asp:LinkButton ID="btnSchAdvClose" CausesValidation="false" runat="server" OnClick="btnSchAdvClose_Click">
                             <span class="glyphicon glyphicon-remove" aria-hidden="true">Close</span>
                        </asp:LinkButton>
                        <div class="col-sm-11">
                        </div>
                        <div class="col-sm-1">
                        </div>
                    </div>
                    <div class="panel-body">
                        <div class="col-sm-12" id="Div3" runat="server">
                            <div class="row">
                                <div class="col-sm-12 height5">
                                </div>
                            </div>
                            <asp:Panel ID="pnlSearchByDate" runat="server">
                                <div class="col-sm-5">
                                    <div class="row">
                                        <div class="col-sm-2 labelText1">
                                            From
                                        </div>
                                        <div class="col-sm-6 paddingRight5 paddingLeft0">
                                            <asp:TextBox runat="server" ID="txtFDate" CausesValidation="false" CssClass="form-control readOnlyText"></asp:TextBox>
                                        </div>
                                        <div class="col-sm-1 paddingLeft0">
                                            <asp:LinkButton ID="lbtnFDate" runat="server" CausesValidation="false">
                                                <span class="glyphicon glyphicon-calendar" aria-hidden="true"></span>
                                            </asp:LinkButton>
                                            <asp:CalendarExtender ID="CalendarExtender3" runat="server" TargetControlID="txtFDate"
                                                PopupButtonID="lbtnFDate" Format="dd/MMM/yyyy">
                                            </asp:CalendarExtender>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-sm-2 labelText1">
                                            To
                                        </div>
                                        <div class="col-sm-6 paddingRight5 paddingLeft0">
                                            <asp:TextBox runat="server" ID="txtTDate" CausesValidation="false" CssClass="form-control readOnlyText"></asp:TextBox>
                                        </div>
                                        <div class="col-sm-1 paddingLeft0">
                                            <asp:LinkButton ID="lbtnTDate" runat="server" CausesValidation="false">
                                                <span class="glyphicon glyphicon-calendar" aria-hidden="true"></span>
                                            </asp:LinkButton>
                                            <asp:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtTDate"
                                                PopupButtonID="lbtnTDate" Format="dd/MMM/yyyy">
                                            </asp:CalendarExtender>
                                        </div>
                                        <div class="col-sm-1 paddingLeft0">
                                            <asp:LinkButton ID="lbtnDateS" runat="server" OnClick="lbtnDateS_Click">
                                            <span class="glyphicon glyphicon-search" aria-hidden="true"  ></span>
                                            </asp:LinkButton>
                                        </div>
                                    </div>
                                </div>
                            </asp:Panel>
                            <div class="col-sm-7">
                                <div class="row">
                                    <div class="col-sm-3 labelText1">
                                        Search by key
                                    </div>
                                    <asp:ScriptManagerProxy ID="ScriptManagerProxy10" runat="server"></asp:ScriptManagerProxy>
                                    <asp:UpdatePanel ID="UpdatePanel10" runat="server">
                                        <ContentTemplate>
                                            <div class="col-sm-8 paddingRight5">
                                                <asp:DropDownList ID="ddlSearchbykey" runat="server" class="form-control">
                                                </asp:DropDownList>
                                            </div>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                                <div class="row">
                                    <div class="col-sm-3 labelText1">
                                        Search by word
                                    </div>
                                    <div class="col-sm-8 paddingRight5">
                                        <asp:TextBox ID="txtSearchbyword" CausesValidation="false" placeholder="Search by word" class="form-control" AutoPostBack="true" runat="server" OnTextChanged="txtSearchbyword_TextChanged"></asp:TextBox>
                                    </div>
                                    <asp:ScriptManagerProxy ID="ScriptManagerProxy9" runat="server"></asp:ScriptManagerProxy>
                                    <asp:UpdatePanel ID="UpdatePanel11" runat="server">
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
                                    <asp:ScriptManagerProxy ID="ScriptManagerProxy6" runat="server"></asp:ScriptManagerProxy>
                                    <asp:UpdatePanel ID="UpdatePanel12" runat="server">
                                        <ContentTemplate>
                                            <asp:GridView ID="grdResult" CausesValidation="false" runat="server" CssClass="table table-hover table-striped" PageSize="6" PagerStyle-CssClass="cssPager" AllowPaging="True" OnSelectedIndexChanged="grdResult_SelectedIndexChanged" OnPageIndexChanging="grdResult_PageIndexChanging" GridLines="None" ShowHeaderWhenEmpty="True" EmptyDataText="No data found...">
                                                <Columns>
                                                    <asp:ButtonField Text="Select" CommandName="Select" ItemStyle-Width="10" />
                                                </Columns>
                                            </asp:GridView>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                            </div>
                            <asp:UpdateProgress ID="UpdateProgress1" DisplayAfter="10"
                                runat="server" AssociatedUpdatePanelID="UpdatePanel12">
                                <ProgressTemplate>
                                    <div class="divWaiting">
                                        <asp:Label ID="lblWait" runat="server"
                                            Text="Please wait... " />
                                        <asp:Image ID="imgWait" runat="server"
                                            ImageAlign="Middle" ImageUrl="~/images/banners/hidden_progress_bar.gif" />
                                    </div>
                                </ProgressTemplate>
                            </asp:UpdateProgress>
                        </div>
                    </div>
                </div>
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>

    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <asp:Button ID="btncus" runat="server" Text="Button" Style="display: none;" />
            <asp:ModalPopupExtender ID="mpCustomer" runat="server" Enabled="True" TargetControlID="btncus"
                PopupControlID="pnlCustomer" PopupDragHandleControlID="hdCUsuc" Drag="true" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:Panel runat="server" ID="pnlCustomer" Style="display: none;">
        <div runat="server" id="Div4" class="panel panel-default height600 width700">
            <asp:Label ID="Label14" runat="server" Text="Label" Visible="false"></asp:Label>
            <div class="panel panel-default">
                <div id="hdCUsuc" class="panel-heading height30">
                    <asp:LinkButton ID="btnCloseCustomer" runat="server" CausesValidation="false" OnClick="btnCloseCustomer_Click">
                             <span class="glyphicon glyphicon-remove" aria-hidden="true">Close</span>
                    </asp:LinkButton>
                    <div class="col-sm-11">
                    </div>
                    <div class="col-sm-1">
                    </div>
                </div>
                <div class="panel-body">
                    <div class="col-sm-12 paddingLeft0 paddingRight0">
                        <asp:UpdatePanel runat="server">
                            <ContentTemplate>
                                <uc1:ucCustomer runat="server" ID="ucCustomer" />
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </div>
        </div>
    </asp:Panel>

    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <asp:Button ID="btnPromositon" runat="server" Text="btnConf" Style="display: none;" />
            <asp:ModalPopupExtender ID="mpPriceNPromotion" runat="server" Enabled="True" TargetControlID="btnPromositon"
                PopupControlID="pnlPriceNPromotion" PopupDragHandleControlID="divPromhdr" Drag="true">
            </asp:ModalPopupExtender>
        </ContentTemplate>
    </asp:UpdatePanel>

    <asp:ScriptManagerProxy ID="ScriptManagerProxy14" runat="server"></asp:ScriptManagerProxy>
    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <asp:Panel runat="server" ID="pnlPriceNPromotion" Style="display: none" Width="1100px">
                <div runat="server" id="Div7" class="panel panel-primary">
                    <asp:Label ID="Label3" runat="server" Text="Label" Visible="false"></asp:Label>
                    <div class="panel panel-default">
                        <div class="panel-heading" id="divPromhdr">
                            Promotions
                                <asp:LinkButton ID="btnPromoCLose" runat="server" Style="float: right" OnClick="btnPromoCLose_Click">
                                    <span class="glyphicon glyphicon-remove" style="float:right" aria-hidden="true">Close</span>
                                </asp:LinkButton>
                            <div class="col-sm-11">
                            </div>
                            <div class="col-sm-1">
                            </div>
                        </div>
                        <div class="panel-body">
                            <div class="col-sm-12 paddingLeft0">
                                <div class="col-sm-7">
                                    <div class="row">
                                        <div class="panel panel-default">
                                            <div class="panel-heading">
                                                Available Normal Price
                                            <div class="col-sm-11">
                                            </div>
                                                <div class="col-sm-1">
                                                </div>
                                            </div>
                                            <div class="panel-body">
                                                <div class="col-sm-12">
                                                    <%--gvNormalPrice--%>
                                                    <asp:Panel runat="server" ScrollBars="Both">
                                                        <asp:GridView ID="gvNormalPrice" AutoGenerateColumns="False" CausesValidation="false" runat="server" GridLines="None" CssClass="table table-hover table-striped" PagerStyle-CssClass="cssPager" ShowHeaderWhenEmpty="True"
                                                            EmptyDataText="No data found...">
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="Select">
                                                                    <ItemTemplate>
                                                                        <asp:CheckBox ID="chkSelectNormalPrice_test" AutoPostBack="true" runat="server" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:BoundField DataField="Serial" HeaderText="Serial" Visible="false" />
                                                                <asp:BoundField DataField="sapd_itm_cd" HeaderText="Item" />
                                                                <asp:BoundField DataField="Sapd_itm_price" HeaderText="U.Price" />
                                                                <asp:BoundField DataField="Sapd_circular_no" HeaderText="Circuler" />
                                                                <asp:BoundField DataField="Sarpt_cd" HeaderText="PriceType" />
                                                                <asp:BoundField DataField="" HeaderText="Type" />
                                                                <asp:BoundField DataField="Sapd_to_date" DataFormatString="{0:dd/MMM/yyyy}" HeaderText="Ref Date" />
                                                                <%--<asp:TemplateField HeaderText="Ref Date">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblValidTill" runat="server" Text='<%# Bind("ValidTill", "{0:dd/MMM/yyyy}") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>--%>
                                                                <asp:BoundField DataField="sapd_pb_seq" HeaderText="PbSeq" Visible="false" />
                                                                <asp:BoundField DataField="sapd_seq_no" HeaderText="PbLineSeq" Visible="false" />
                                                                <asp:BoundField DataField="sapd_promo_cd" HeaderText="Promo.CD" />
                                                                <asp:BoundField DataField="sapd_is_fix_qty" HeaderText="IsFixQty" Visible="false" />
                                                                <asp:BoundField DataField="sapd_cre_by" HeaderText="BkpUPrice" Visible="false" />
                                                                <asp:BoundField DataField="Sapd_warr_remarks" HeaderText="WarrantyRmk" Visible="false" />
                                                                <asp:BoundField DataField="sapd_pb_tp_cd" HeaderText="Book" Visible="false" />
                                                                <asp:BoundField DataField="sapd_pbk_lvl_cd" HeaderText="Level" Visible="false" />
                                                            </Columns>
                                                            <PagerStyle CssClass="cssPager" />
                                                        </asp:GridView>
                                                    </asp:Panel>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="panel panel-default">
                                            <div class="panel-heading">
                                                Available Promotions
                                            <div class="col-sm-11">
                                            </div>
                                                <div class="col-sm-1">
                                                </div>
                                            </div>
                                            <div class="panel-body">
                                                <div class="row">
                                                    <div class="col-sm-12">
                                                        <%--gvPromotionPrice--%>
                                                        <asp:ScriptManagerProxy ID="ScriptManagerProxy12" runat="server"></asp:ScriptManagerProxy>
                                                        <asp:UpdatePanel runat="server">
                                                            <ContentTemplate>
                                                                <asp:Panel runat="server" ScrollBars="Both">
                                                                    <asp:GridView ID="gvPromotionPrice" AutoGenerateColumns="False" CausesValidation="false" runat="server" GridLines="None" CssClass="table table-hover table-striped" PagerStyle-CssClass="cssPager" ShowHeaderWhenEmpty="True"
                                                                        EmptyDataText="No data found...">
                                                                        <Columns>
                                                                            <asp:TemplateField HeaderText="Select">
                                                                                <ItemTemplate>
                                                                                    <asp:CheckBox ID="chkSelectPromPrice_test" AutoPostBack="true" OnCheckedChanged="chkSelectPromPrice_CheckedChanged" runat="server" Enabled="false" />
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Item">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblSapd_itm_cd" runat="server" Text='<%# Bind("Sapd_itm_cd")%>' Visible="false"></asp:Label>
                                                                                    <asp:LinkButton ID="btnSapd_itm_cd" Text='<%# Bind("Sapd_itm_cd")%>' runat="server" OnClick="btnSapd_itm_cd_Click" />
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="U.Price">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblSapd_itm_price" runat="server" Text='<%# Bind("Sapd_itm_price")%>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Circuler">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblSapd_circular_no" runat="server" Text='<%# Bind("Sapd_circular_no")%>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="PriceType">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblSapd_price_type" runat="server" Text='<%# Bind("Sapd_price_type")%>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Type">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="Label5" runat="server" Text='<%# Bind("Sarpt_cd")%>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Valid Till">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblSapd_to_date" runat="server" Text='<%# Bind("Sapd_to_date", "{0:dd/MMM/yyyy}")%>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="PbSeq">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblPbSeq" runat="server" Text='<%# Bind("Sapd_pb_seq")%>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="sapd_seq_no" Visible="false">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblsapd_seq_no" runat="server" Text='<%# Bind("sapd_seq_no")%>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="PbLineSeq" Visible="false">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblPbLineSeq" runat="server" Text='<%# Bind("sapd_seq_no")%>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <%--<asp:BoundField DataField="PbLineSeq" HeaderText="PbLineSeq" Visible="false" />
                                                                                <asp:BoundField DataField="Sapd_promo_cd" HeaderText="Promo.CD" />--%>
                                                                            <asp:TemplateField HeaderText="Promo.CD">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblSapd_promo_cd" runat="server" Text='<%# Bind("Sapd_promo_cd")%>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="IsFixQty" Visible="False">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblIsFixQty" runat="server" Text='<%# Bind("sapd_is_fix_qty")%>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="BkpUPrice" Visible="False">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblBkpUPrice" runat="server" Text='<%# Bind("sapd_cre_by")%>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="WarrantyRmk" Visible="False">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblSapd_warr_remarks" runat="server" Text='<%# Bind("Sapd_warr_remarks")%>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Book" Visible="False">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblBook" runat="server" Text='<%# Bind("sapd_pb_tp_cd")%>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Level" Visible="False">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblLevel" runat="server" Text='<%# Bind("sapd_pbk_lvl_cd")%>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                        </Columns>
                                                                        <PagerStyle CssClass="cssPager" />
                                                                    </asp:GridView>
                                                                </asp:Panel>
                                                            </ContentTemplate>
                                                        </asp:UpdatePanel>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="panel panel-default">
                                            <div class="panel-heading">
                                                Available Promotions
                                            <div class="col-sm-11">
                                            </div>
                                                <div class="col-sm-1">
                                                </div>
                                            </div>
                                            <div class="panel-body">
                                                <div class="row">
                                                    <div class="col-sm-12">
                                                        <%--gvPromotionItem--%>
                                                        <asp:Panel runat="server" ScrollBars="Both">
                                                            <asp:GridView ID="gvPromotionItem" AutoGenerateColumns="False" CausesValidation="false" runat="server" GridLines="None" CssClass="table table-hover table-striped" PagerStyle-CssClass="cssPager" ShowHeaderWhenEmpty="True"
                                                                EmptyDataText="No data found..." OnRowDataBound="gvPromotionItem_RowDataBound">
                                                                <Columns>
                                                                    <asp:TemplateField Visible="false">
                                                                        <ItemTemplate>
                                                                            <asp:LinkButton ID="btnPromotionItemSimiler" CausesValidation="false" CommandName="Select" OnClick="btnPromotionItemSimiler_Click" CommandArgument="<%# Container.DataItemIndex%>" runat="server">
                                                                                <span class="glyphicon glyphicon-tasks" aria-hidden="true"></span>
                                                                            </asp:LinkButton>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField Visible="false">
                                                                        <ItemTemplate>
                                                                            <asp:LinkButton ID="btnPromotionItemSelect" CausesValidation="false" OnClick="btnPromotionItemSelect_Click" CommandName="Select" CommandArgument="<%# Container.DataItemIndex%>" runat="server">
                                                                                <span class="glyphicon glyphicon-play-circle" aria-hidden="true"></span>
                                                                            </asp:LinkButton>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="No">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblsapc_itm_line" runat="server" Text='<%# Bind("sapc_itm_line")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Item">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblsapc_itm_cd" runat="server" Text='<%# Bind("sapc_itm_cd")%>' Visible="false"></asp:Label>
                                                                            <asp:LinkButton ID="btnsapc_itm_cd" Text='<%# Bind("sapc_itm_cd")%>' runat="server" OnClick="btnsapc_itm_cd_Click" />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Similer Item">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblSimiler_item" runat="server" Text='<%# Bind("Similer_item")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Description">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblmi_longdesc1" runat="server" Text='<%# Bind("mi_longdesc")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Model">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblmi_model" runat="server" Text='<%# Bind("mi_model")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Serial">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblsapc_sub_ser" runat="server" Text='<%# Bind("sapc_sub_ser")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Status">
                                                                        <ItemTemplate>
                                                                            <asp:DropDownList ID="PromItm_Status" AutoPostBack="true" OnSelectedIndexChanged="PromItm_Status_SelectedIndexChanged" CssClass="form-control" runat="server"></asp:DropDownList>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Qty">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblsapc_qty" runat="server" Text='<%# Bind("sapc_qty")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Unit Price">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblsapc_price" runat="server" Text='<%# Bind("sapc_price")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="PB Seq" Visible="false">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblsapc_pb_seq" runat="server" Text='<%# Bind("sapc_pb_seq")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="PB Line Seq" Visible="false">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblsapc_main_line" runat="server" Text='<%# Bind("sapc_main_line")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Main Item" Visible="false">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblsapc_main_itm_cd" runat="server" Text='<%# Bind("sapc_main_itm_cd")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Main Item Serial" Visible="false">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblsapc_main_ser" runat="server" Text='<%# Bind("sapc_main_ser")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <%--           <asp:TemplateField HeaderText="PromItm_BkpUnitPrice" Visible="false">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblsapc_price" runat="server" Text='<%# Bind("sapc_price") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>--%>
                                                                    <asp:TemplateField HeaderText="PromItm_increse" Visible="false">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblsapc_increse" runat="server" Text='<%# Bind("sapc_increse")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                </Columns>
                                                                <PagerStyle CssClass="cssPager" />
                                                            </asp:GridView>
                                                        </asp:Panel>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-sm-2">
                                            Availability
                                        </div>
                                        <div class="col-sm-2">
                                            <asp:Label Text="0.00" ID="lblPriNProAvailableQty" ForeColor="#A513D0" runat="server" />
                                        </div>
                                        <div class="col-sm-2">
                                            Status
                                        </div>
                                        <div class="col-sm-2">
                                            <asp:Label Text="0.00" ID="lblPriNProAvailableStatusQty" ForeColor="#A513D0" runat="server" />
                                        </div>
                                        <div class="col-sm-2">
                                            <asp:Button ID="btnPriNProConfirm" OnClick="btnPriNProConfirm_Click" Text="Confirm" runat="server" />
                                        </div>
                                        <div class="col-sm-2">
                                            <asp:Button ID="btnPriNProCancel" Text="Cancel" OnClick="btnPriNProCancel_Click" runat="server" />
                                        </div>
                                    </div>
                                </div>
                                <div class="col-sm-5">
                                    <div class="row">
                                        <div class="panel panel-default">
                                            <div class="panel-heading">
                                                Pick serial for promotion
                                            <div class="col-sm-11">
                                            </div>
                                                <div class="col-sm-1">
                                                </div>
                                            </div>
                                            <div class="panel-body">
                                                <div class="row">
                                                    <div class="col-sm-12">
                                                        <asp:Panel runat="server" ScrollBars="Both" Height="400px">
                                                            <asp:GridView ID="gvPromotionSerial" AutoGenerateColumns="False" CausesValidation="false" runat="server" GridLines="None" CssClass="table table-hover table-striped" PagerStyle-CssClass="cssPager" ShowHeaderWhenEmpty="True"
                                                                EmptyDataText="No data found...">
                                                                <Columns>
                                                                    <asp:TemplateField HeaderText="Select">
                                                                        <ItemTemplate>
                                                                            <asp:CheckBox ID="chkSelectPromSerial_test" AutoPostBack="true" runat="server" />
                                                                        </ItemTemplate>
                                                                        <ItemStyle Width="10%" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Item">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblTus_itm_cd" runat="server" Text='<%# Bind("Tus_itm_cd")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <ItemStyle Width="10%" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Serial 1">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblTus_ser_1" runat="server" Text='<%# Bind("Tus_ser_1")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <ItemStyle Width="10%" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Serial 2">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblTus_ser_2" runat="server" Text='<%# Bind("Tus_ser_2")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <ItemStyle Width="10%" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Warranty">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lbltus_warr_no" runat="server" Text='<%# Bind("tus_warr_no")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <ItemStyle Width="10%" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Status">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblTus_itm_stus" runat="server" Text='<%# Bind("Tus_itm_stus")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <ItemStyle Width="10%" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Serial ID">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblTus_ser_id" runat="server" Text='<%# Bind("Tus_ser_id")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <ItemStyle Width="10%" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Serial 3">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblTus_ser_3" runat="server" Text='<%# Bind("Tus_ser_3")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <ItemStyle Width="10%" />
                                                                    </asp:TemplateField>
                                                                </Columns>
                                                                <PagerStyle CssClass="cssPager" />
                                                            </asp:GridView>
                                                        </asp:Panel>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-sm-2">
                                                        Availability
                                                    </div>
                                                    <div class="col-sm-6">
                                                        <div class="col-sm-10 paddingLeft0 paddingRight0">
                                                            <asp:TextBox ID="txtPriNProSerialSearch" runat="server" CssClass="form-control" AutoPostBack="true" OnTextChanged="txtPriNProSerialSearch_TextChanged" />
                                                        </div>
                                                        <div class="col-sm-2  paddingLeft0 paddingRight0">
                                                            <asp:LinkButton ID="btnSearchFreSerials" runat="server" TabIndex="5" CausesValidation="false" OnClick="btnSearchFreSerials_Click">
                                                        <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                            </asp:LinkButton>
                                                        </div>
                                                    </div>
                                                    <div class="col-sm-2">
                                                        <asp:Button ID="btnPriNProSerClear" Text="Clear" runat="server" OnClick="btnPriNProSerClear_Click" />
                                                    </div>
                                                    <div class="col-sm-2">
                                                        <asp:Button ID="btnPriNProSerConfirm" Text="Confirm" OnClick="btnPriNProSerConfirm_Click" runat="server" />
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
        </ContentTemplate>
    </asp:UpdatePanel>


    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <asp:Button ID="btnSilItmswt" runat="server" Text="btnConf" Style="display: none;" />
            <asp:ModalPopupExtender ID="mpSimilrItmes" runat="server" Enabled="True" TargetControlID="btnSilItmswt"
                PopupControlID="pnlSimilerItems" PopupDragHandleControlID="divpnlSimilerItems" Drag="true" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>
        </ContentTemplate>
    </asp:UpdatePanel>

    <asp:Panel runat="server" ID="pnlSimilerItems" Style="display: none" Width="400px" Height="115px">
        <div runat="server" id="Div13" class="panel panel-primary">
            <asp:UpdatePanel runat="server" ID="UpdatePanel13">
                <ContentTemplate>
                    <asp:HiddenField ID="hdfOriginalItem" runat="server" />
                    <div class="panel panel-default">
                        <div class="panel-heading" id="divpnlSimilerItems">
                            Pick Similar Item
                                <asp:LinkButton ID="btnCloseSimiler" runat="server" OnClick="btnCloseSimiler_Click">
                                    <span class="glyphicon glyphicon-remove" style="float:right" aria-hidden="true">Close</span>
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
                                <div class="col-sm-12">
                                    <asp:GridView ID="dgvSimilerItemPick" AutoGenerateColumns="false" runat="server" CssClass="table table-hover table-striped" GridLines="None" ShowHeaderWhenEmpty="True" EmptyDataText="No data found...">
                                        <Columns>
                                            <asp:BoundField DataField="MISI_SEQ_NO" HeaderText="Item" Visible="false" />
                                            <asp:BoundField DataField="MISI_TP" HeaderText="Item" Visible="false" />
                                            <asp:BoundField DataField="MISI_COM" HeaderText="Item" Visible="false" />
                                            <asp:BoundField DataField="MISI_ITM_CD" HeaderText="Item" Visible="false" />
                                            <asp:TemplateField HeaderText="Item Code">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblMISI_SIM_ITM_CD" runat="server" Text='<%# Bind("MISI_SIM_ITM_CD")%>' Visible="false"></asp:Label>
                                                    <asp:LinkButton ID="btnMISI_SIM_ITM_CD" Text='<%# Bind("MISI_SIM_ITM_CD")%>' runat="server" OnClick="btnMISI_SIM_ITM_CD_Click" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="MI_LONGDESC" HeaderText="Item" />
                                            <asp:BoundField DataField="MI_MODEL" HeaderText="Item" />
                                            <asp:BoundField DataField="MI_BRAND" HeaderText="Item" />
                                            <asp:BoundField DataField="MISI_FROM" HeaderText="Item" Visible="false" />
                                            <asp:BoundField DataField="MISI_TO" HeaderText="Item" Visible="false" />
                                            <asp:BoundField DataField="MISI_PC" HeaderText="Item" Visible="false" />
                                            <asp:BoundField DataField="MISI_LOC" HeaderText="Item" Visible="false" />
                                            <asp:BoundField DataField="MISI_DOC_NO" HeaderText="Item" Visible="false" />
                                            <asp:BoundField DataField="MISI_PROMO" HeaderText="Item" Visible="false" />
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </asp:Panel>

    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <asp:Button ID="btnConf" runat="server" Text="btnConf" Style="display: none;" />
            <asp:ModalPopupExtender ID="mpConfirmation" runat="server" Enabled="True" TargetControlID="btnConf"
                PopupControlID="pnlConfirmation" PopupDragHandleControlID="divCOnf" Drag="true" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>
        </ContentTemplate>
    </asp:UpdatePanel>

    <asp:UpdatePanel ID="UpdatePanel14" runat="server">
        <ContentTemplate>
            <asp:Button ID="Button4" runat="server" Text="Button" Style="display: none;" />
            <asp:ModalPopupExtender ID="excelUpload" runat="server" Enabled="True" TargetControlID="Button4"
                PopupControlID="pnlexcel" PopupDragHandleControlID="PopupHeader" Drag="true" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>
        </ContentTemplate>
    </asp:UpdatePanel>

    <asp:Panel runat="server" ID="pnlexcel">
        <div runat="server" id="Div1" class="panel panel-default height45 width700 ">
            <div class="panel panel-default">
                <div class="panel-heading">
                    <asp:LinkButton ID="btnClose3" runat="server" OnClick="btnClose3_Click">
                             <span class="glyphicon glyphicon-remove" aria-hidden="true">Close</span>
                    </asp:LinkButton>
                    <div class="col-sm-11">
                        <asp:Label ID="lblalert" runat="server" ForeColor="Red" Text="Label" Visible="false"></asp:Label>
                        <asp:Label ID="lblsuccess" runat="server" ForeColor="Green" Visible="false"></asp:Label>
                    </div>
                    <div class="col-sm-1">
                    </div>
                </div>
                <div class="panel-body">
                    <div class="col-sm-12 paddingLeft0 paddingRight0">
                        <div class="row">
                            <div class="col-sm-12">
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-12 height5">
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-12" id="Div8" runat="server">
                                <div class="col-sm-5 paddingRight5">
                                    <asp:FileUpload ID="fileupexcelupload" runat="server" />
                                </div>
                                <div class="col-sm-2 paddingRight5">
                                    <asp:Button ID="btnUpload" class="btn btn-warning btn-xs" runat="server" Text="Upload"
                                        OnClick="btnUpload_Click" />
                                </div>
                            </div>
                            <div class="col-sm-12 ">
                                <div id="divUpcompleted" visible="false" runat="server" class="alert alert-info alert-success" role="alert">
                                    <div class="col-sm-9">
                                        <strong>Alert!</strong>
                                        <asp:Label ID="Label4" Text="Excel file upload completed. Do you want to process?" runat="server"></asp:Label>
                                    </div>
                                    <div class="col-sm-3">
                                        <asp:Button ID="btnprocess" class="btn btn-success btn-xs" runat="server" Text="Process" OnClick="btnprocess_Click" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </asp:Panel>

    <asp:Panel runat="server" ID="pnlConfirmation" Style="display: none" Width="400px" Height="115px">
        <div runat="server" id="Div6" class="panel panel-primary">
            <asp:Label ID="Label2" runat="server" Text="Label" Visible="false"></asp:Label>
            <asp:UpdatePanel runat="server">
                <ContentTemplate>
                    <div class="panel panel-default">
                        <div class="panel-heading" id="divCOnf">
                            Confirmation
                    <asp:LinkButton ID="btnConfClose" runat="server" OnClick="btnConfClose_Click">
                             <span class="glyphicon glyphicon-remove" style="float:right" aria-hidden="true">Close</span>
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
                                <div class="col-sm-12">
                                    <asp:Label ID="lblConfText" Text="" runat="server" />
                                    <asp:HiddenField ID="hdfConfItem" runat="server" />
                                    <asp:HiddenField ID="hdfConfStatus" runat="server" />
                                    <asp:HiddenField ID="hdfConf" runat="server" />
                                </div>
                                <div class="col-sm-12">
                                    <div class="col-sm-3">
                                    </div>
                                    <div class="col-sm-3">
                                        <asp:Button ID="btnConfYes" CssClass="form-control" Text="Yes" runat="server" OnClick="btnConfYes_Click" />
                                    </div>
                                    <div class="col-sm-3">
                                    </div>
                                    <div class="col-sm-3">
                                        <asp:Button ID="btnConfNo" CssClass="form-control" Text="No" runat="server" OnClick="btnConfNo_Click" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </asp:Panel>

    <asp:UpdatePanel ID="upnlDebBalance" runat="server">
        <ContentTemplate>
            <asp:Button ID="Button16" runat="server" Text="Button" Style="display: none;" />
            <asp:ModalPopupExtender ID="mdlconfDb" runat="server" Enabled="True" TargetControlID="Button16"
                PopupControlID="pnlconfDb" PopupDragHandleControlID="PopupHeader" Drag="true" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdateProgress ID="UpdateProgress7" DisplayAfter="10"
        runat="server" AssociatedUpdatePanelID="upnlDebBalance">
        <ProgressTemplate>
            <div class="divWaiting">
                <asp:Label ID="lblWaitMee" runat="server"
                    Text="Please wait... " />
                <asp:Image ID="imgWaitMee" runat="server"
                    ImageAlign="Middle" ImageUrl="~/images/icons/ajax-loader.gif" />
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <asp:Panel ID="pnlconfDb" runat="server" align="center">
        <asp:Label ID="Label12" runat="server" Text="Label" Visible="false"></asp:Label>
        <asp:ScriptManagerProxy ID="ScriptManagerProxy23" runat="server"></asp:ScriptManagerProxy>
        <asp:UpdatePanel ID="UpdatePanel43" runat="server">
            <ContentTemplate>
                <div class="panel panel-danger">
                    <div class="panel-heading">
                        <span>Information</span>
                    </div>
                    <div class="panel-body">
                        <div class="row">
                            <div class="col-sm-12">
                                <asp:Label ID="Label13" runat="server"></asp:Label>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-12">
                                <strong>
                                    <asp:Label ID="lblerror" Text="" runat="server"></asp:Label></strong>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-12">
                                <strong>
                                    <asp:Label ID="lbldata" Text="" runat="server"></asp:Label></strong>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-12">
                                <asp:Label ID="lblrow3" runat="server"></asp:Label>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-12">
                                <asp:Label ID="lblmsg" runat="server"></asp:Label>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-12 height22">
                            </div>
                        </div>

                        <div class="row">
                            <div class="col-sm-1">
                            </div>
                            <div class="col-sm-4">
                                <asp:Button ID="btnDebOK" runat="server" Text="yes" Width="80px" CausesValidation="false" class="btn btn-primary btn-xs" OnClick="btnDebOK_Click" />
                            </div>
                            <div class="col-sm-2 ">
                                <asp:Button ID="btnDebNO" runat="server" Text="No" Width="80px" CausesValidation="false" class="btn btn-primary btn-xs" OnClick="btnDebNO_Click" />
                            </div>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </asp:Panel>

    

    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <asp:Button ID="Button1" runat="server" Text="btnConfex" Style="display: none;" />
            <asp:ModalPopupExtender ID="ModalPopupExtenderOutstanding" runat="server" Enabled="True" TargetControlID="Button1"
                PopupControlID="pnlConfirmationoutstanding" PopupDragHandleControlID="divCOnfOutstading" Drag="true" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>
        </ContentTemplate>
    </asp:UpdatePanel>

      <asp:Panel runat="server" ID="pnlConfirmationoutstanding" Style="display: none" Width="400px" Height="115px">
        <div runat="server" id="Div5" class="panel panel-primary">
            <asp:Label ID="Label7" runat="server" Text="Label" Visible="false"></asp:Label>
            <asp:UpdatePanel runat="server">
                <ContentTemplate>
                    <div class="panel panel-default">
                        <div class="panel-heading" id="divCOnfOutstading">
                            Confirmation
                    <asp:LinkButton ID="LinkButton2" runat="server" OnClick="btnConfClose_ClickExcel">
                             <span class="glyphicon glyphicon-remove" style="float:right" aria-hidden="true">Close</span>
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
                                <div class="col-sm-12">
                                    <asp:Label ID="Label8" Text="Below invoices settleamount is greater than outstading amount. Do you want to continue?" runat="server" />
                                    <asp:HiddenField ID="HiddenField4" runat="server" />
                                    <asp:HiddenField ID="HiddenField5" runat="server" />
                                    <asp:HiddenField ID="HiddenField6" runat="server" />
                                </div>
                                <div class="col-sm-12">
                                    <div class="col-sm-3">
                                    </div>
                                    <div class="col-sm-3">
                                        <asp:Button ID="Button5" CssClass="form-control" Text="Yes" runat="server" OnClick="btnConOutfYes_Click" />
                                    </div>
                                    <div class="col-sm-3">
                                    </div>
                                    <div class="col-sm-3">
                                        <asp:Button ID="Button6" CssClass="form-control" Text="No" runat="server" OnClick="btnConfNoOut_Click" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div>
                          <asp:GridView ID="GridViewInv" AutoGenerateColumns="false" runat="server" CssClass="table table-hover table-striped" GridLines="None" ShowHeaderWhenEmpty="True" EmptyDataText="No data found...">
                                        <Columns>
                                            <asp:BoundField DataField="invoiceNo" HeaderText="Invoice"  />
                                            <asp:BoundField DataField="amount" HeaderText="Amount"  />                                           
                                        </Columns>
                                    </asp:GridView>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </asp:Panel>
     <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <asp:Button ID="btnConfex" runat="server" Text="btnConfex" Style="display: none;" />
            <asp:ModalPopupExtender ID="ModalPopupExtenderExcel" runat="server" Enabled="True" TargetControlID="btnConfex"
                PopupControlID="pnlConfirmationexcel" PopupDragHandleControlID="divCOnfExcel" Drag="true" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:Panel runat="server" ID="pnlConfirmationexcel" Style="display: none" Width="400px" Height="115px">
        <div runat="server" id="Div2" class="panel panel-primary">
            <asp:Label ID="Label1" runat="server" Text="Label" Visible="false"></asp:Label>
            <asp:UpdatePanel runat="server">
                <ContentTemplate>
                    <div class="panel panel-default">
                        <div class="panel-heading" id="divCOnfExcel">
                            Confirmation
                    <asp:LinkButton ID="LinkButton1" runat="server" OnClick="btnConfClose_ClickExcel">
                             <span class="glyphicon glyphicon-remove" style="float:right" aria-hidden="true">Close</span>
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
                                <div class="col-sm-12">
                                    <asp:Label ID="Label6" Text="Upload invoice numbers are not in logged PC Do you want to continue" runat="server" />
                                    <asp:HiddenField ID="HiddenField1" runat="server" />
                                    <asp:HiddenField ID="HiddenField2" runat="server" />
                                    <asp:HiddenField ID="HiddenField3" runat="server" />
                                </div>
                                <div class="col-sm-12">
                                    <div class="col-sm-3">
                                    </div>
                                    <div class="col-sm-3">
                                        <asp:Button ID="Button2" CssClass="form-control" Text="Yes" runat="server" OnClick="btnConfYes_ClickExcel" />
                                    </div>
                                    <div class="col-sm-3">
                                    </div>
                                    <div class="col-sm-3">
                                        <asp:Button ID="Button3" CssClass="form-control" Text="No" runat="server" OnClick="btnConfNo_ClickExcel" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </asp:Panel>

    <script src="<%=Request.ApplicationPath%>Js/jquery.min.js"></script>
    <script src="<%=Request.ApplicationPath%>Js/bootstrap.min.js"></script>

</asp:Content>
