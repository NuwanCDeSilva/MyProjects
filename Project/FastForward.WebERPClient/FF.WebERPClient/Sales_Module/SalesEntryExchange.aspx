<%@ Page Title="Invoice" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="SalesEntryExchange.aspx.cs" Inherits="FF.WebERPClient.Sales_Module.SalesEntryExchasnge"
    EnableEventValidation="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="../UserControls/uc_CustomerCreation.ascx" TagName="uc_CustomerCreation"
    TagPrefix="uc1" %>
<%@ Register Src="../UserControls/uc_CustCreationExternalDet.ascx" TagName="uc_CustCreationExternalDet"
    TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">
        //#region  Click Event for the invoice search area
        function clickButton(e, buttonid) {
            var evt = e ? e : window.event;
            var bt = document.getElementById(buttonid);
            if (bt) {
                if (evt.keyCode == 113) {
                    bt.click();
                    return false;
                }

            }
        }
        //#endregion

        //#region Get Item Description
        function GetItemDescription(src, dest) {
            var ctrl = document.getElementById(src);
            if (ctrl.value != "") {
                FF.WebERPClient.LocalWebServices.CommonSearchWebServive.GetAllItemDetailsByItemCode(ctrl.value, onDescriptionPass, onDescriptionFail, dest);
                FF.WebERPClient.LocalWebServices.CommonSearchWebServive.GetBlockedItem(ctrl.value, onBlockItemPass, onBlockItemFail);
            }
        }

        function onDescriptionPass(result, destCtrl) {
            var objs = new Array();
            objs = destCtrl.split("|");
            var _desc = document.getElementById(objs[0]);
            var _item = document.getElementById(objs[1]);
            var hdnItemCode = document.getElementById('<%=hdnItemCode.ClientID %>');

            if (result == '' || result == null) {
                _desc.value = '';
                _item.value = '';
                hdnItemCode.value = '';
                alert('Invalid Item Code');
            }
            else {

                if (result.Mi_anal3) {
                    if (document.getElementById('chkDeliveryNow').checked == false) {
                        alert('Consumer products are allow only when delivery now!');
                        _desc.value = '';
                        _item.value = '';
                        hdnItemCode.value = '';
                        return;
                    }
                }
                _desc.value = result.Mi_longdesc;
                hdnItemCode.value = _item;

            }
            document.getElementById('<%=txtStatus.ClientID %>').focus();
            //GetBlockedItem()
        }

        function onDescriptionFail(error, destCtrl) {
            var objs = new Array();
            objs = destCtrl.split('|');
            var _desc = document.getElementById(objs[0]);
            var _item = document.getElementById(objs[1]);

        }

        function onBlockItemPass(result) {

            if (result == true) {
                var txtItem = document.getElementById('<%=txtItem.ClientID%>');
                var txtDescription = document.getElementById('<%=txtDescription.ClientID%>');
                var hdnItemCode = document.getElementById('<%=hdnItemCode.ClientID %>');

                txtItem.value = '';
                hdnItemCode.value = '';
                txtDescription.value = '';
                alert('Selected item is not allow for invoice independently');
                txtItem.focus();
                return;
            }

        }

        function onBlockItemFail(error) {
            //Nothing :)
        }

        //#endregion

        //#region Set Focus Common function

        function SetFocus(toFocus) {
            if (event.which || event.keyCode) {
                if ((event.which == 13) || (event.keyCode == 13)) {
                    var k = document.getElementById(toFocus);
                    document.getElementById(toFocus).focus();
                    return false;
                }
            }
            else {

                return true
            };
        }


        //#endregion

        //#region Toggle CreditCard Promotion in Payment Area

        function PromotionPeriod() {
            if (document.getElementById('<%=chkPayCrPromotion.ClientID%>').checked)
                document.getElementById('<%=txtPayCrPeriod.ClientID%>').disabled = false;
            else
                document.getElementById('<%=txtPayCrPeriod.ClientID%>').disabled = true;
            document.getElementById('<%=txtPayCrPeriod.ClientID%>').value = '';
        }
        //#endregion

        //#region Make TextBox Upper Case/Lower Case
        function ToUpper(ctrl) {
            var t = ctrl.value;
            ctrl.value = t.toUpperCase();
        }
        function ToLower(ctrl) {
            var t = ctrl.value;
            ctrl.value = t.toLowerCase();
        }
        //#endregion

        //#region  Check for the Item UOM and change Qty Behaviour
        function IsUOMDecimalAllow(src, qty) {
            var ctrl = document.getElementById(src);
            if (ctrl.value != "") {
                FF.WebERPClient.LocalWebServices.CommonSearchWebServive.IsUOMDecimalAllow(ctrl.value, onUOMPass, onUOMFail, qty);
            }
        }

        function onUOMPass(result, qty) {
            var objCtrl = document.getElementById(qty);
            objCtrl.setAttribute("onKeyPress", "return numbersonly(event," + result + ")");

        }

        function onUOMFail(error, qty) {
            var objCtrl = document.getElementById(qty);
            objCtrl.setAttribute("onKeyPress", "return numbersonly(event,false)");
        }

        //#endregion

        //#region Allow only numaric and decimal values
        function numbersonly(e, decimal) {
            var key;
            var keychar;

            if (window.event) {
                key = window.event.keyCode;
            }
            else if (e) {
                key = e.which;
            }
            else {
                return true;
            }
            keychar = String.fromCharCode(key);

            if ((key == null) || (key == 0) || (key == 8) || (key == 9) || (key == 13) || (key == 27)) {
                return true;
            }
            else if ((("0123456789").indexOf(keychar) > -1)) {
                return true;
            }
            else if (decimal && (keychar == ".")) {
                return true;
            }
            else
                return false;
        }
        //#endregion

        //#region Change number to your max length.
        function CheckCharacterCount(text, long) {
            var maxlength = new Number(long);
            if (text.value.length > maxlength) {
                text.value = text.value.substring(0, maxlength);

            }
        }
        //#endregion

        //#region Change business entity conteroler
        function TabModalPopupInBusinessEntityOpen() {
            //document.getElementById('hdnBusinessEntityModalControler').value = 1;
            window.open('../Sales_Module/ExchangeCustomerCreate.aspx', '_blank');

        }

        function TabModalPopupInBusinessEntityClose() {
            //document.getElementById('hdnBusinessEntityModalControler').value = 0;

        }
        //#endregion

        //#region Check Invoice Type 

        var _isValid = true;

        function BindInvoiceType() {
            var _type = document.getElementById('<%= txtInvType.ClientID %>');
            var _date = document.getElementById('<%= txtDate.ClientID %>');
            var _ddl = document.getElementById('<%= ddlPayMode.ClientID %>');

            if (_type != null)
                if (_type.value != null && _type.value != '')
                    PageMethods.IsValidInvoiceType(_type.value, OnCheckInvoiceType, onCheckInvoiceTypeFail);
        }

        function onCheckInvoiceTypeFail() {

        }

        function OnCheckInvoiceType(responce) {
            _isValid = responce;
            var _type = document.getElementById('<%= txtInvType.ClientID %>');

            if (_isValid == false) {
                alert('Invalid Invoice Type');
                var _ddl = document.getElementById('<%= ddlPayMode.ClientID %>');
                _ddl.options.length = 0;
                _type.value = ''; _type.focus(); return;
            }

            var chkSearchBySerial = document.getElementById('<%= chkSearchBySerial.ClientID %>');

            if (_type.value == "CRED") chkSearchBySerial.checked = true; else chkSearchBySerial.checked = false;
            OnBySerialCheckChange();

            var cc = document.getElementById('<%= divCredit.ClientID %>');
            var adv = document.getElementById('<%= divAdvReceipt.ClientID %>');

            if (cc != null) cc.style.display = 'block';
            if (adv != null) adv.style.display = 'block';

            FF.WebERPClient.LocalWebServices.CommonSearchWebServive.PopulatePayMode(_type.value, OnPayModePopulated, onUOMFail);
        }

        function OnPayModePopulated(response) {
            var _ddl = document.getElementById('<%= ddlPayMode.ClientID %>');
            _ddl.options.length = 0;

            while (_ddl.childNodes.length > 0)
                _ddl.removeChild(_ddl.childNodes[0]);

            for (var iCount = 0; iCount < response.length; iCount++) {
                var option = document.createElement("option");
                option.value = response[iCount];
                option.innerHTML = response[iCount];
                _ddl.appendChild(option);
            }

        }
        //#endregion

        //#region Get/Check Advance Receipt Amount
        function GetAdvanceReceiptAmount() {
            var _payMode = document.getElementById('<%= ddlPayMode.ClientID %>');
            if (_payMode != null)
                if (_payMode.value != null || _payMode.value == '') {
                    if (_payMode.value == "ADVAN") {
                        var _recno = document.getElementById('<%= txtPayAdvReceiptNo.ClientID %>');
                        var _amt = document.getElementById('<%= txtPayAdvRefAmount.ClientID %>');
                        FF.WebERPClient.LocalWebServices.CommonSearchWebServive.GetReceiptAmount(_recno.value, OnSuccessGetAmount, onUOMFail);
                    }
                }
        }

        function OnSuccessGetAmount(response) {
            var _amt = document.getElementById('<%= txtPayAdvRefAmount.ClientID %>');
            var _recno = document.getElementById('<%= txtPayAdvReceiptNo.ClientID %>');
            var _payAmt = document.getElementById('<%= txtPayAmount.ClientID %>');
            if (response == 0) {
                alert('Selected Document does not having amount for settle');
                _recno.value = '';
            }
            _amt.value = response;
            _payAmt.value = response;

        }
        //#endregion

        //#region Get/Check Customer Code/Mobile/NIC

        var _isCheckByNIC = false; //controling whether checking by NIC no
        var _isCheckByMobile = false; //controling whether checking by Mobile no


        function CheckByCustomer() {
            var txtCustomer = document.getElementById('<%= txtCustomer.ClientID %>');
            if (txtCustomer.value == '' || txtCustomer.value == null) return;
            _isCheckByNIC = false;
            _isCheckByMobile = false;
            FF.WebERPClient.LocalWebServices.CommonSearchWebServive.GetCustomerInformationByCustomer(txtCustomer.value, OnSuccessCustomerDetail, onUOMFailCustomerDetail);
        }

        function CheckByNIC() {
            _isCheckByNIC = true;
            var txtNIC = document.getElementById('<%= txtNIC.ClientID %>');
            if (txtNIC.value == '' || txtNIC.value == null) return;
            PageMethods.IsValidNIC(txtNIC.value, OnSuccessNIC, OnFailNIC);

        }

        function CheckByMobile() {
            _isCheckByMobile = true;
            var txtMobile = document.getElementById('<%= txtMobile.ClientID %>');
            if (txtMobile.value == '' || txtMobile.value == null) return;
            PageMethods.IsValidMobileOrLandNo(txtMobile.value, OnSuccessMobile, OnFailMobile);

        }

        function OnSuccessMobile(result) {
            var txtMobile = document.getElementById('<%= txtMobile.ClientID %>');
            if (!result) { alert('Please select the valid Mobile no'); txtMobile.value = ''; txtMobile.focus(); return; }
            FF.WebERPClient.LocalWebServices.CommonSearchWebServive.GetCustomerInformationByMobile(txtMobile.value, OnSuccessCustomerDetail, onUOMFailCustomerDetail);
        }

        function OnFailMobile(error) {
            //nothing
        }

        function OnSuccessNIC(result) {
            var txtNIC = document.getElementById('<%= txtNIC.ClientID %>');
            if (!result) { alert('Please select the valid NIC'); txtNIC.value = ''; txtNIC.focus(); return; }
            FF.WebERPClient.LocalWebServices.CommonSearchWebServive.GetCustomerInformationByNIC(txtNIC.value, OnSuccessCustomerDetail, onUOMFailCustomerDetail);
        }

        function OnFailNIC(error) {
            //nothing
        }

        function OnSuccessCustomerDetail(result) {
            var txtCustomer = document.getElementById('<%= txtCustomer.ClientID %>');
            if (result == null) {
                alert('Please select the valid customer');
                txtCustomer.value = '';
                CustomerDetailClear();
                txtCustomer.focus();
                return false;
            }
            if (result != null) {
                //if the customer not available in the system ie.CASH is only allow
                if (result.Mbe_cd == "" || result.Mbe_cd == null) {
                    if (_isCheckByNIC == false && _isCheckByMobile == false) {
                        alert('Please select the valid customer');
                        CustomerDetailClear();
                        txtCustomer.value = '';
                        txtCustomer.focus();
                        return false;
                    }
                    else {
                        // if (_isCheckByNIC) document.getElementById('<%= txtMobile.ClientID %>').focus();
                        // if (_isCheckByMobile) document.getElementById('<%= txtPriceBook.ClientID %>').focus();
                        return false;
                    }
                }


                if (result.Mbe_cd == "CASH") {
                    txtCustomer.value = result.Mbe_cd;
                    CustomerDetailClear();
                    return false;
                }
                else {
                    var txtCusName = document.getElementById('<%= txtCusName.ClientID %>');
                    var txtAddress1 = document.getElementById('<%= txtAddress1.ClientID %>');
                    var txtAddress2 = document.getElementById('<%= txtAddress2.ClientID %>');
                    var txtMobile = document.getElementById('<%= txtMobile.ClientID %>');
                    var txtNIC = document.getElementById('<%= txtNIC.ClientID %>');
                    var chkTaxInvoice = document.getElementById('<%= chkTaxInvoice.ClientID %>');

                    txtCustomer.value = result.Mbe_cd;
                    txtCusName.value = result.Mbe_name;
                    txtAddress1.value = result.Mbe_add1;
                    txtAddress2.value = result.Mbe_add2;
                    txtMobile.value = result.Mbe_mob;
                    txtNIC.value = result.Mbe_nic;
                    if (result.Mbe_is_tax) {
                        chkTaxInvoice.checked = true;
                        chkTaxInvoice.enabled = true;
                    }
                    else {
                        chkTaxInvoice.checked = false;
                        chkTaxInvoice.enabled = false;
                    }
                    var txtDelAddress1 = document.getElementById('<%= txtDelAddress1.ClientID %>');
                    var txtDelAddress2 = document.getElementById('<%= txtDelAddress2.ClientID %>');
                    var txtDelCustomer = document.getElementById('<%= txtDelCustomer.ClientID %>');
                    var txtDelName = document.getElementById('<%= txtDelName.ClientID %>');

                    txtDelAddress1.value = result.Mbe_add1;
                    txtDelAddress2.value = result.Mbe_add2;
                    txtDelCustomer.value = result.Mbe_cd;
                    txtDelName.value = result.Mbe_name;

                    ViewCustomerAccountDetails();
                }
            }
        }

        function onUOMFailCustomerDetail(error) {
            var txtCustomer = document.getElementById('<%= txtCustomer.ClientID %>');
            alert('Please select the valid customer');
            txtCustomer.value = '';
            CustomerDetailClear();
            txtCustomer.focus();
            return false;
        }

        function CustomerDetailClear() {
            var txtCustomer = document.getElementById('<%= txtCustomer.ClientID %>');
            var txtCusName = document.getElementById('<%= txtCusName.ClientID %>');
            var txtAddress1 = document.getElementById('<%= txtAddress1.ClientID %>');
            var txtAddress2 = document.getElementById('<%= txtAddress2.ClientID %>');
            var txtMobile = document.getElementById('<%= txtMobile.ClientID %>');
            var txtNIC = document.getElementById('<%= txtNIC.ClientID %>');
            var chkTaxInvoice = document.getElementById('<%= chkTaxInvoice.ClientID %>');

            txtCusName.value = '';
            txtAddress1.value = '';
            txtAddress2.value = '';
            txtMobile.value = '';
            txtNIC.value = '';
            chkTaxInvoice.checked = false;

            _isCheckByNIC = false;
            _isCheckByMobile = false;

        }

        function ViewCustomerAccountDetails() {
            var txtCustomer = document.getElementById('<%= txtCustomer.ClientID %>');
            if ((txtCustomer.value != '' || txtCustomer.value != null) && txtCustomer.value != 'CASH')
                FF.WebERPClient.LocalWebServices.CommonSearchWebServive.GetCustomerAccountDetail(txtCustomer.value, OnSuccessCustomerAccountDetail, onFailCustomerAccountDetail);
        }

        function OnSuccessCustomerAccountDetail(result) {
            if (result == null) return;
            var hdnAccBalance = document.getElementById('<%= hdnAccountBalance.ClientID %>');
            var hdnAvailableCredit = document.getElementById('<%= hdnAvailableCredit.ClientID %>');

            var txtAvailableCredit = document.getElementById('<%= txtAvailableCredit.ClientID %>');
            var txtAccBalance = document.getElementById('<%= txtAccBalance.ClientID %>')


            if (result.Saca_cust_cd != '' && result.Saca_cust_cd != null) {
                PageMethods.FormatToCurrency(result.Saca_acc_bal, FormatToCurrencySuccess, onFailCustomerAccountDetail, hdnAccBalance);

                PageMethods.FormatToCurrency((result.Saca_crdt_lmt - result.Saca_ord_bal - result.Saca_acc_bal), FormatToCurrencySuccess, onFailCustomerAccountDetail, hdnAvailableCredit);
            }
            else {
                txtAccBalance.value = '0.00';
                txtAvailableCredit.value = '0.00';
                hdnAccBalance.value = '0.00';
                hdnAvailableCredit.value = '0.00';
            }

        }

        function onFailCustomerAccountDetail(error) {
            //nothing :P
        }

        function FormatToCurrencySuccess(result, destination) {
            if (destination.id == 'hdnAvailableCredit') { document.getElementById('<%= txtAvailableCredit.ClientID %>').value = result; }
            if (destination.id == 'hdnAccountBalance') { document.getElementById('<%= txtAccBalance.ClientID %>').value = result; }
            destination.value = result;
        }

        //#endregion

        //#region Check Price Book
        function CheckPriceBook() {
            var txtPriceBook = document.getElementById('<%= txtPriceBook.ClientID %>');
            var txtInvType = document.getElementById('<%= txtInvType.ClientID %>');
            if (txtPriceBook.value == '') return;
            if (txtInvType.value == '') { alert('Please select the invoice type'); txtInvType.focus(); return; }
            FF.WebERPClient.LocalWebServices.CommonSearchWebServive.GetPriceDefinitionByBookAndLevel(txtPriceBook.value, txtInvType.value, OnSuccessPriceBook, OnFailPriceBook);
        }

        function OnSuccessPriceBook(result) {
            if (result == false) {
                alert('Please select the valid price book');
                var txtPriceBook = document.getElementById('<%= txtPriceBook.ClientID %>');
                txtPriceBook.value = '';
                txtPriceBook.focus();
                return;
            }
        }

        function OnFailPriceBook(error) {
            alert('Please select the valid price book');
            return;
        }

        //#endregion

        //#region Set Default Delivery Location
        function MakeDefaultLocationforDelivery() {

            var txtDelLocation = document.getElementById('<%= txtDelLocation.ClientID %>');
            var chkOpenDelivery = document.getElementById('<%= chkOpenDelivery.ClientID %>');

            if (chkOpenDelivery.checked) {
                txtDelLocation.value = '';
            }
            else {
                FF.WebERPClient.LocalWebServices.CommonSearchWebServive.GetDefaultDeliveryLocation(OnSuccessDefaultLocation, OnFailDefaultLocation);
            }
        }

        function OnSuccessDefaultLocation(result) {
            var txtDelLocation = document.getElementById('<%= txtDelLocation.ClientID %>');
            if (result != null)
                txtDelLocation.value = result == null ? '' : result;
            return;
        }

        function OnFailDefaultLocation() {
            //Nothing Horny :)
        }
        //#endregion

        //#region Check Item Status
        function CheckItemStatus() {
            var txtBook = document.getElementById('<%=txtPriceBook.ClientID %>');
            var txtLevel = document.getElementById('<%=txtPriceLevel.ClientID %>');
            var txtStatus = document.getElementById('<%=txtStatus.ClientID %>');

            if (txtStatus.value == '') return;

            if (txtBook.value == '') { alert('Please select the price book.'); txtBook.value = ''; txtBook.focus(); return; }
            if (txtLevel.value == '') { alert('Please select the price level'); txtLevel.value = ''; txtLevel.focus(); return; }

            var _concatedString = txtBook.value + '|' + txtLevel.value + '|' + txtStatus.value;

            FF.WebERPClient.LocalWebServices.CommonSearchWebServive.IsValidItenStatusforPricing(_concatedString, OnSuccessItemStatus, OnFailItemStatus);
        }
        function OnSuccessItemStatus(result) {
            var txtStatus = document.getElementById('<%=txtStatus.ClientID %>');

            if (result == false) {
                txtStatus.value = '';
                alert('Invalid item status');
                txtStatus.focus();
                return;
            }
            else {
                var txtQty = document.getElementById('<%=txtQty.ClientID %>');
                txtQty.focus();
            }

        }
        function OnFailItemStatus(error) {
            //Nothing ha :)
        }
        //#endregion

        //#region By Serial Scan for invoice item/ inventory
        function OnBySerialCheckChange() {

            var txtSerialNo = document.getElementById('<%= txtSerialNo.ClientID %>');
            var chkSearchBySerial = document.getElementById('<%= chkSearchBySerial.ClientID %>');
            var txtItem = document.getElementById('<%= txtItem.ClientID %>');
            var txtDescription = document.getElementById('<%= txtDescription.ClientID %>');
            txtSerialNo.value = '';
            txtItem.value = '';
            txtDescription.value = '';
            var hdnBySerialCheckStatus = document.getElementById('<%= hdnBySerialCheckStatus.ClientID %>');
            hdnBySerialCheckStatus.value = chkSearchBySerial.checked;

            if (chkSearchBySerial.checked) {
                txtSerialNo.readOnly = false;
            }
            else {
                txtSerialNo.readOnly = true;
            }
        }

        function CheckSerial() {
            var txtSerialNo = document.getElementById('<%= txtSerialNo.ClientID %>');
            if (txtSerialNo.value != '') {
                FF.WebERPClient.LocalWebServices.CommonSearchWebServive.CheckSerialAvailability(txtSerialNo.value, OnSuccessSerial, OnFailSerial);
            }
        }
        function OnSuccessSerial(result) {
            var txtSerialNo = document.getElementById('<%= txtSerialNo.ClientID %>');
            var hdnSerialNo = document.getElementById('<%= hdnSerialNo.ClientID %>');
            var hdnItemCode = document.getElementById('<%= hdnItemCode.ClientID %>');

            if (result <= 0) {
                alert('Invalid Serial No');
                txtSerialNo.value = '';
                hdnSerialNo.value = '';
                hdnItemCode.value = '';
                txtSerialNo.focus();
                return;
            }
            if (result > 1) {
                alert('Selected serial having multiple items. Please use searching option to take required serial and item');
                hdnSerialNo.value = '';
                hdnItemCode.value = '';
                txtSerialNo.focus();
                return;
            }

            if (result = 1) {
                FF.WebERPClient.LocalWebServices.CommonSearchWebServive.GetAvailableSerialShortDetail(txtSerialNo.value, OnSuccessGetSerial, OnFailGetSerial);
                return;
            }

        }

        function OnSuccessGetSerial(result) {
            var txtSerialNo = document.getElementById('<%= txtSerialNo.ClientID %>');
            var txtItem = document.getElementById('<%= txtItem.ClientID %>');
            var txtDescription = document.getElementById('<%= txtDescription.ClientID %>');
            var txtUnitPrice = document.getElementById('<%= txtUnitPrice.ClientID %>');
            var txtStatus = document.getElementById('<%= txtStatus.ClientID %>');
            var txtQty = document.getElementById('<%= txtQty.ClientID %>');
            var hdnSerialNo = document.getElementById('<%= hdnSerialNo.ClientID %>');
            var hdnItemCode = document.getElementById('<%= hdnItemCode.ClientID %>');

            if (result == null) {
                alert('Invalid Serial No');
                txtSerialNo.value = '';
                hdnSerialNo.value = '';
                hdnItemCode.value = '';
                txtSerialNo.focus();
                return;
            }
            if (result != null) {
                var objs = new Array();
                objs = result.split("|");
                var _item = objs[0];
                var _desc = objs[1];
                hdnItemCode.value = _item;
                FF.WebERPClient.LocalWebServices.CommonSearchWebServive.BindMultiItem(_item, OnSuccessGetMultiple, OnFailGetMultiple, result);
            }

        }

        function OnSuccessGetMultiple(result, previousresult) {
            var objs = new Array();
            objs = previousresult.split("|");

            if (result.indexOf(objs[0]) != -1) {
                var divMultiResult = document.getElementById('<%= divMultiResult.ClientID %>');
                divMultiResult.innerHTML = result;
                ShowPopupN();
            }
            else {
                var _item = objs[0];
                var _desc = objs[1];

                var txtSerialNo = document.getElementById('<%= txtSerialNo.ClientID %>');
                var txtItem = document.getElementById('<%= txtItem.ClientID %>');
                var txtDescription = document.getElementById('<%= txtDescription.ClientID %>');
                var txtUnitPrice = document.getElementById('<%= txtUnitPrice.ClientID %>');
                var txtStatus = document.getElementById('<%= txtStatus.ClientID %>');
                var txtQty = document.getElementById('<%= txtQty.ClientID %>');
                var hdnSerialNo = document.getElementById('<%= hdnSerialNo.ClientID %>');
                var hdnItemCode = document.getElementById('<%= hdnItemCode.ClientID %>');

                txtItem.value = _item;
                hdnItemCode.value = _item;
                txtDescription.value = _desc;
                hdnSerialNo.value = txtSerialNo.value;
                txtQty.value = '1';

                if (txtStatus.value == '') { txtStatus.focus(); return; }
                if (txtStatus.value != '') txtQty.focus();
                txtUnitPrice.focus();
            }
        }

        function SaleTableRowClick(rowIndex) {
            var imgBtnComItemMultiClose = document.getElementById('<%= imgBtnComItemMultiClose.ClientID %>');
            imgBtnComItemMultiClose.click();

            var tabRowId = "tabRow" + rowIndex;
            var selectedRow = document.getElementById(tabRowId);
            var Cells = selectedRow.getElementsByTagName("td");

            var txtSerialNo = document.getElementById('<%= txtSerialNo.ClientID %>');
            var txtItem = document.getElementById('<%= txtItem.ClientID %>');
            var txtDescription = document.getElementById('<%= txtDescription.ClientID %>');
            var hdnSerialNo = document.getElementById('<%= hdnSerialNo.ClientID %>');
            var txtQty = document.getElementById('<%= txtQty.ClientID %>');
            var btnAddItem = document.getElementById('<%= btnAddItem.ClientID %>');
            var txtStatus = document.getElementById('<%= txtStatus.ClientID %>');


            txtItem.value = Cells[0].innerText;
            txtDescription.value = Cells[1].innerText;
            hdnSerialNo.value = txtSerialNo.value;
            txtQty.value = '1';
            if (txtStatus.value == '') { txtStatus.focus(); return; }
            if (txtStatus.value != '') txtQty.focus();
            btnAddItem.focus();

        }


        function OnFailGetMultiple(error) {
        }

        function ShowPopupN() {
            var btnComItemMulti = document.getElementById('<%= btnComItemMulti.ClientID %>');
            btnComItemMulti.click();
        }

        function OnFailGetSerial(error) {

        }

        function OnFailSerial(error) {
            var txtSerialNo = document.getElementById('<%= txtSerialNo.ClientID %>');
            var hdnSerialNo = document.getElementById('<%= hdnSerialNo.ClientID %>');
            alert('Invalid Serial No');
            txtSerialNo.value = '';
            hdnSerialNo.value = '';
            txtSerialNo.focus();
        }
        //#endregion

        function numbersonly(e, decimal) {
            var key;
            var keychar;

            if (window.event) {
                key = window.event.keyCode;
            }
            else if (e) {
                key = e.which;
            }
            else {
                return true;
            }
            keychar = String.fromCharCode(key);

            if ((key == null) || (key == 0) || (key == 8) || (key == 9) || (key == 13) || (key == 27)) {
                return true;
            }
            else if ((("0123456789").indexOf(keychar) > -1)) {
                return true;
            }
            else if (decimal && (keychar == ".")) {
                return true;
            }
            else
                return false;
        }



        //#endregion

    </script>
    <asp:UpdatePanel ID="updtMainPnl" runat="server">
        <ContentTemplate>
            <%--Whole Page--%>
            <asp:Panel runat="server" ID="pnlInvoice" DefaultButton="btnDefBtn" class="inv100break">
                <div class="inv100break">
                    <%--Button Panel--%>
                    <div class="PanelHeader invheadersize">
                        <div style="float: left;">
                            <asp:Label runat="server" ID="lblDispalyInfor" Text="" Style="float: left; overflow-y: auto;
                                overflow-x: auto; font-size: xx-small; color: Yellow;"></asp:Label>
                        </div>
                        <%--background-color: #E6EBF2--%>
                        <asp:CheckBox ID="chkDeliveryNow" Text="Deliver Later!" runat="server" ClientIDMode="Static"
                            Checked="True" />
                        <asp:Button ID="btnSave" runat="server" Text="Process" CssClass="Button invbtn" OnClick="SaveInvoice" />
                        <asp:Button ID="btnHold" runat="server" Text="Hold" CssClass="Button invbtn" OnClick="Hold" />
                        <asp:Button ID="btnClear" runat="server" Text="Clear" CssClass="Button invbtn" OnClick="btnClear_Click" />
                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="Cancel" CssClass="Button invbtn" />
                        <asp:Button ID="btnPrint" runat="server" Text="Print" OnClick="Print" CssClass="Button invbtn"
                            Visible="false" />
                        <asp:Button ID="btnDOPrint" runat="server" Text="Print DO" OnClick="Print" CssClass="Button invbtn" />
                    </div>
                    <%--General details to start the invoice--%>
                    <div class="div97pcelt">
                        <div class="inv100pceltpd2">
                            <div class="div1pcelt">
                                &nbsp;</div>
                            <div class="div7pcelt">
                                Types
                            </div>
                            <div class="div1pcelt">
                                &nbsp;</div>
                            <div class="div10pcelt">
                                <asp:TextBox ID="txtInvType" onchange="ToUpper(this)" runat="server" Width="100%"
                                    CssClass="TextBoxUpper" MaxLength="10" onblur="BindInvoiceType()"></asp:TextBox></div>
                            <div class="div4pcelt">
                                &nbsp;&nbsp;<asp:ImageButton ID="imgBtnInvType" runat="server" ImageUrl="~/Images/icon_search.png"
                                    ImageAlign="Middle" OnClick="imgBtnInvType_Click" /></div>
                            <div class="div1pcelt">
                                &nbsp;</div>
                            <div class="div5pcelt">
                                Ref. No
                            </div>
                            <div class="div1pcelt">
                                &nbsp;</div>
                            <div class="div8pcelt">
                                <asp:TextBox ID="txtReferance" onchange="ToUpper(this)" runat="server" Width="100%"
                                    CssClass="TextBoxUpper" ClientIDMode="Static"></asp:TextBox></div>
                            <div class="div1pcelt">
                                &nbsp;<asp:ImageButton ID="imgBtnRefNo" runat="server" ImageUrl="~/Images/icon_search.png"
                                    ImageAlign="Middle" OnClick="imgBtnRefNo_Click" /></div>
                            <div class="div1pcelt">
                                &nbsp;</div>
                            <div class="div5pcelt">
                                &nbsp;&nbsp; Exe.
                            </div>
                            <div class="div1pcelt">
                                &nbsp;</div>
                            <div class="div8pcelt">
                                <asp:TextBox runat="server" onchange="ToUpper(this)" ID="txtExecutive" CssClass="TextBoxUpper"
                                    Width="55%" MaxLength="10"></asp:TextBox>&nbsp;<asp:ImageButton ID="imgBtnExecutive"
                                        runat="server" ImageUrl="~/Images/icon_search.png" ImageAlign="Middle" OnClick="imgBtnExecutive_Click" />
                            </div>
                            <%--                            <div class="div21pcelt">
                                &nbsp; &nbsp; &nbsp; &nbsp;Group &nbsp;
                                <asp:TextBox runat="server" ID="txtGroup" CssClass="TextBox" Width="45%" onblur="CheckGroupSalesCode()"></asp:TextBox>&nbsp;<asp:ImageButton
                                    ID="imgBtnGroup" runat="server" ImageUrl="~/Images/icon_search.png" OnClick="imgbtnSearchGrpCode_Click"
                                    ImageAlign="Middle" />
                            </div>--%>
                            <div class="div7pcelt">
                                Invoice No
                            </div>
                            <div class="div1pcelt">
                                &nbsp;</div>
                            <div class="div12pcelt">
                                <asp:TextBox ID="txtInvoiceNo" onchange="ToUpper(this)" runat="server" Width="66%"
                                    CssClass="TextBoxUpper" MaxLength="10"></asp:TextBox>
                                <asp:ImageButton ID="imgBtnInvNo" runat="server" ImageUrl="~/Images/icon_search.png"
                                    ImageAlign="Middle" OnClick="imgBtnInvNo_Click" /></div>
                            <div class="div1pcelt">
                                &nbsp;</div>
                            <div class="div6pcelt">
                                Date
                            </div>
                            <div class="div18pcelt">
                                <asp:TextBox runat="server" ID="txtDate" CssClass="TextBoxUpper" Enabled="false"
                                    Width="45%"></asp:TextBox>
                                <asp:Image ID="imgBtnDate" runat="server" ImageUrl="~/Images/Calendar_scheduleHS.png"
                                    ImageAlign="Middle" Visible="false" />
                                <asp:TextBox runat="server" ID="TextBoxTime" CssClass="TextBoxUpper" Enabled="false"
                                    Width="35%"></asp:TextBox>
                            </div>
                        </div>
                        <%--First Row--%>
                        <div class="inv100pceltpd2">
                            <div class="div1pcelt">
                                &nbsp;</div>
                            <div class="div7pcelt">
                                Customer
                            </div>
                            <div class="div1pcelt">
                                &nbsp;</div>
                            <div class="div10pcelt">
                                <asp:TextBox ID="txtCustomer" onchange="ToUpper(this)" runat="server" onblur="CheckByCustomer()"
                                    Width="100%" CssClass="TextBoxUpper" MaxLength="12"></asp:TextBox></div>
                            <div class="div4pcelt">
                                &nbsp;&nbsp;<asp:ImageButton ID="imgBtnCustomer" runat="server" ImageUrl="~/Images/icon_search.png"
                                    ImageAlign="Middle" OnClick="imgBtnCustomer_Click" /></div>
                            <div class="div1pcelt">
                                &nbsp;</div>
                            <div class="div5pcelt">
                                Name
                            </div>
                            <div class="div1pcelt">
                                &nbsp;</div>
                            <div class="div46pcelt">
                                <asp:TextBox ID="txtCusName" onchange="ToUpper(this)" runat="server" Width="100%"
                                    CssClass="TextBoxUpper" MaxLength="50" Enabled="false" Style="border: none;"></asp:TextBox></div>
                            <div class="div5pcelt">
                                &nbsp;</div>
                            <div class="div6pcelt" runat="server" visible="false">
                                Currency
                            </div>
                            <div class="div11pcelt" runat="server" visible="false">
                                <asp:TextBox runat="server" onchange="ToUpper(this)" ID="txtCurrency" CssClass="TextBoxUpper"
                                    Width="75%" MaxLength="5"></asp:TextBox>&nbsp;<asp:ImageButton ID="imgBtnCurrency"
                                        runat="server" ImageUrl="~/Images/icon_search.png" ImageAlign="Middle" OnClick="imgBtnCurrency_Click" /></div>
                        </div>
                        <%--Second Row--%>
                        <div class="inv100pceltpd2">
                            <div class="div1pcelt">
                                &nbsp;</div>
                            <div class="div7pcelt">
                                NIC No
                            </div>
                            <div class="div1pcelt">
                                &nbsp;</div>
                            <div class="div10pcelt">
                                <asp:TextBox ID="txtNIC" onchange="ToUpper(this)" runat="server" Width="100%" CssClass="TextBoxUpper"
                                    MaxLength="10" onblur="CheckByNIC()"></asp:TextBox>
                            </div>
                            <div class="div4pcelt">
                                &nbsp;
                                <asp:ImageButton ID="imgBtnNICNo" runat="server" ImageUrl="~/Images/icon_search.png"
                                    ImageAlign="Middle" OnClick="imgBtnNICNo_Click" />
                            </div>
                            <div class="div1pcelt">
                                &nbsp;</div>
                            <div class="div5pcelt">
                                Address
                            </div>
                            <div class="div1pcelt">
                                &nbsp;</div>
                            <div class="div46pcelt">
                                <asp:TextBox ID="txtAddress1" runat="server" Width="100%" MaxLength="100" CssClass="TextBoxUpper"
                                    ClientIDMode="Static" Enabled="false" Style="border: none;"></asp:TextBox></div>
                            <div class="div5pcelt">
                                &nbsp;</div>
                            <div class="invlblacc">
                                Acc. Balance
                            </div>
                            <div class="invacclblcol">
                                &nbsp;</div>
                            <div class="invlblaccx">
                                <asp:TextBox ID="txtAccBalance" runat="server" Width="95%" ReadOnly="true" BorderStyle="None"
                                    BackColor="#E6EBF2" Text="0.00" class="invtxtacc"></asp:TextBox></div>
                        </div>
                        <%--Third Row--%>
                        <div class="inv100pceltpd2">
                            <div class="div1pcelt">
                                &nbsp;</div>
                            <div class="div7pcelt">
                                Mobile No
                            </div>
                            <div class="div1pcelt">
                                &nbsp;</div>
                            <div class="div10pcelt">
                                <asp:TextBox ID="txtMobile" runat="server" Width="100%" CssClass="TextBoxUpper" MaxLength="10"
                                    onblur="CheckByMobile()"></asp:TextBox></div>
                            <div class="div4pcelt">
                                &nbsp;&nbsp;<asp:ImageButton ID="imgBtnMobile" runat="server" ImageUrl="~/Images/icon_search.png"
                                    ImageAlign="Middle" OnClick="imgBtnMobile_Click" /></div>
                            <div class="div1pcelt">
                                &nbsp;</div>
                            <div class="div5pcelt">
                                &nbsp;
                            </div>
                            <div class="div1pcelt">
                                &nbsp;</div>
                            <div class="div46pcelt">
                                <asp:TextBox ID="txtAddress2" runat="server" MaxLength="100" Width="100%" CssClass="TextBoxUpper"
                                    ClientIDMode="Static" Enabled="false" Style="border: none;"></asp:TextBox></div>
                            <div class="div5pcelt">
                                &nbsp;</div>
                            <div class="invlblaccx">
                                Available Credit
                            </div>
                            <div class="invacclblcol">
                                &nbsp;</div>
                            <div class="invlblacc">
                                <asp:TextBox ID="txtAvailableCredit" runat="server" Width="95%" BorderStyle="None"
                                    BackColor="#E6EBF2" Text="0.00" ReadOnly="true" CssClass="invunkwn3"></asp:TextBox></div>
                        </div>
                    </div>
                    <%--General details to start the invoice/Buttons for additional information--%>
                    <div class="invunkwn1">
                        <div>
                            <asp:ImageButton class="invadvbtn" ID="btnBusAdvDetail" runat="server" OnClientClick="TabModalPopupInBusinessEntityOpen()"
                                ImageAlign="Middle" ImageUrl="~/Images/customer2.jpg" ToolTip="Advance Customer Creation"
                                Visible="false" /></div>
                        <div>
                            <asp:ImageButton CssClass="invadvbtn2" ID="btnApplyDiscount" runat="server" OnClick="BindInvoiceItemToDiscountItem"
                                ImageAlign="Middle" ImageUrl="~/Images/discount_icon.jpg" ToolTip="Customer Discount Request" /></div>
                        <div>
                            <asp:ImageButton runat="server" ID="btnDeliver" ImageAlign="Middle" ImageUrl="~/Images/delivery_on_time.png"
                                CssClass="invadvbtn3" ToolTip="Delivery Details" />
                        </div>
                    </div>
                    <div class="invunkwn2">
                        <%-- Collaps Header - Invoice Items --%>
                        <div class="CollapsiblePanelHeader invcollapsovrid">
                            Customer Detail</div>
                        <%-- Collaps Image - Invoice Items --%>
                        <div class="invfltlt">
                            <asp:Image runat="server" ID="Image3" ImageUrl="~/Images/16 X 16 DownArrow.jpg" ImageAlign="Right" /></div>
                        <%-- Collaps control - Invoice Items --%>
                        <asp:CollapsiblePanelExtender ID="CollapsiblePanelExtender1" runat="server" TargetControlID="Panel5"
                            CollapsedSize="0" ExpandedSize="380" ScrollContents="true" Collapsed="False"
                            ExpandControlID="Image3" CollapseControlID="Image3" AutoCollapse="False" AutoExpand="False"
                            ExpandDirection="Vertical" ImageControlID="Image3" ExpandedImage="~/Images/16 X 16 UpArrow.jpg"
                            CollapsedImage="~/Images/16 X 16 DownArrow.jpg">
                        </asp:CollapsiblePanelExtender>
                        <div style="clear: both; float: left; display: block;">
                            <asp:Panel runat="server" ID="Panel5" Width="99%" ScrollBars="Auto" BorderColor="#9F9F9F"
                                BorderWidth="1px">
                                <div style="float: left; width: 100%; padding-top: 1px;">
                                    <div style="float: left; width: 100%">
                                        <asp:Panel ID="PanelCCre" runat="server" ScrollBars="Auto" GroupingText=" ">
                                            <div style="float: left; height: 8px; width: 95%; text-align: right;">
                                                <asp:Button ID="ButtonCreate" runat="server" CssClass="Button" OnClick="ButtonCreate_Click"
                                                    Text="Create" />
                                                <%-- <asp:Button ID="ButtonUpdate" runat="server" CssClass="Button" OnClick="ButtonUpdate_Click"
                                                    Text="Update" />--%>
                                            </div>
                                            <div style="float: left; width: 100%; height: 5px;">
                                                &nbsp;
                                            </div>
                                            <div style="float: left; width: 100%">
                                                <div style="float: left; width: 60%">
                                                    <uc1:uc_CustomerCreation ID="cusCreP1" runat="server" />
                                                    <div style="float: left; width: 100%">
                                                        <asp:Panel ID="Panel4" runat="server" ScrollBars="Auto" GroupingText="Other Details">
                                                            <div style="float: left; width: 100%">
                                                                <div style="float: left; width: 100%">
                                                                    <div style="float: left; width: 1%">
                                                                        &nbsp;
                                                                    </div>
                                                                    <div style="float: left; width: 30%">
                                                                        Flight No
                                                                    </div>
                                                                    <div style="float: left; width: 69%">
                                                                        <asp:TextBox ID="TextBoxFlightNo" runat="server" CssClass="TextBox"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                                <div style="float: left; width: 100%; padding-top: 3px;">
                                                                    <div style="float: left; width: 1%">
                                                                        &nbsp;
                                                                    </div>
                                                                    <div style="float: left; width: 30%">
                                                                        Passport No
                                                                    </div>
                                                                    <div style="float: left; width: 69%">
                                                                        <asp:TextBox ID="TextBoxPassport" runat="server" CssClass="TextBox"></asp:TextBox>
                                                                        &nbsp;&nbsp;&nbsp;&nbsp;
                                                                        <asp:Button ID="ButtonAddPassport" runat="server" CssClass="Button" Text="Add" OnClick="ButtonAddPassport_Click" />
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </asp:Panel>
                                                    </div>
                                                </div>
                                                <div style="float: left; width: 40%; overflow: auto;">
                                                    <div style="float: left; width: 500px;">
                                                        <uc2:uc_CustCreationExternalDet ID="cusCreP2" runat="server" EnableViewState="false" />
                                                    </div>
                                                </div>
                                            </div>
                                        </asp:Panel>
                                    </div>
                                    <div style="float: left; width: 100%">
                                        <asp:Panel ID="Panel6" runat="server" ScrollBars="Auto" GroupingText=" ">
                                            <asp:GridView ID="GridViewItemDetails" runat="server" Width="50%" EmptyDataText="No data found"
                                                ShowHeaderWhenEmpty="True" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333"
                                                GridLines="Both" CssClass="GridView" OnRowDeleting="GridViewItemDetails_RowDeleting">
                                                <AlternatingRowStyle BackColor="White" />
                                                <Columns>
                                                    <asp:TemplateField>
                                                        <ItemTemplate>
                                                            <asp:ImageButton ID="btnImgDelete" runat="server" ImageAlign="Middle" ImageUrl="~/Images/Delete.png"
                                                                Width="10px" Height="10px" CommandName="Delete" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField HeaderText="Passport No" DataField="PassportNo" />
                                                    <asp:BoundField HeaderText="Flight No" DataField="FlightNo" />
                                                </Columns>
                                                <EditRowStyle BackColor="#2461BF" />
                                                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                                <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                                <RowStyle BackColor="#EFF3FB" />
                                                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                                <SortedAscendingCellStyle BackColor="#F5F7FB" />
                                                <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                                                <SortedDescendingCellStyle BackColor="#E9EBEF" />
                                                <SortedDescendingHeaderStyle BackColor="#4870BE" />
                                            </asp:GridView>
                                        </asp:Panel>
                                    </div>
                                </div>
                            </asp:Panel>
                        </div>
                    </div>
                    <%--Invoice item details/Buyback details--%>
                    <div class="invunkwn2">
                        <%-- Collaps Header - Invoice Items --%>
                        <div class="CollapsiblePanelHeader invcollapsovrid">
                            Item Detail</div>
                        <%-- Collaps Image - Invoice Items --%>
                        <div class="invfltlt">
                            <asp:Image runat="server" ID="Image1" ImageUrl="~/Images/16 X 16 DownArrow.jpg" ImageAlign="Right" /></div>
                        <%-- Collaps control - Invoice Items --%>
                        <asp:CollapsiblePanelExtender ID="CPEInvoiceItem" runat="server" TargetControlID="pnlInvItem"
                            CollapsedSize="0" ExpandedSize="225" Collapsed="true" ExpandControlID="Image1"
                            CollapseControlID="Image1" AutoCollapse="False" AutoExpand="False" ScrollContents="false"
                            ExpandDirection="Vertical" ImageControlID="Image1" ExpandedImage="~/Images/16 X 16 UpArrow.jpg"
                            CollapsedImage="~/Images/16 X 16 DownArrow.jpg">
                        </asp:CollapsiblePanelExtender>
                        <%-- Collaps Area - Invoice Items --%>
                        <div class="invcolarea">
                            <asp:Panel runat="server" ID="pnlInvItem" Width="99.8%" ScrollBars="Auto" BorderColor="#9F9F9F"
                                BorderWidth="1px" Font-Bold="true">
                                <div class="invunkwn4">
                                    <div class="invunkwn5">
                                        <div runat="server" visible="false" id="jhjgh">
                                            <div class="div1pcelt">
                                                &nbsp;</div>
                                            <div class="div7pcelt">
                                                Quotation
                                            </div>
                                            <div class="div14pcelt">
                                                <asp:TextBox runat="server" ID="txtQuotation" CssClass="TextBox" Width="70%"></asp:TextBox>
                                                &nbsp;<asp:ImageButton ID="imgBtnQuotation" runat="server" ImageUrl="~/Images/icon_search.png"
                                                    ImageAlign="Middle" OnClick="imgBtnQuotation_Click" />
                                            </div>
                                        </div>
                                        <div class="div1pcelt">
                                            &nbsp;</div>
                                        <div class="div5pcelt">
                                            Book
                                        </div>
                                        <div class="div16pcelt">
                                            <asp:TextBox runat="server" onchange="ToUpper(this)" CssClass="TextBoxUpper" onblur="CheckPriceBook()"
                                                ID="txtPriceBook" Width="70%" MaxLength="10"></asp:TextBox>&nbsp;<asp:ImageButton
                                                    ID="imgBtnPriceBook" runat="server" ImageUrl="~/Images/icon_search.png" ImageAlign="Middle"
                                                    OnClick="imgBtnPriceBook_Click" />
                                        </div>
                                        <div class="div1pcelt">
                                            &nbsp;</div>
                                        <div class="div5pcelt">
                                            Level
                                        </div>
                                        <div class="div16pcelt">
                                            <asp:TextBox runat="server" onchange="ToUpper(this)" ID="txtPriceLevel" CssClass="TextBoxUpper"
                                                Width="70%" MaxLength="10"></asp:TextBox>&nbsp;<asp:ImageButton ID="imgBtnPriceLevel"
                                                    runat="server" ImageUrl="~/Images/icon_search.png" ImageAlign="Middle" OnClick="imgBtnPriceLevel_Click" />
                                        </div>
                                        <div class="div8pcelt">
                                            <asp:CheckBox runat="server" ID="chkSearchBySerial" Text="By Serial" onchange="OnBySerialCheckChange()"
                                                ClientIDMode="Static" />
                                        </div>
                                        <div class="div1pcelt">
                                            &nbsp;</div>
                                        <div class="div25pcelt">
                                            <asp:TextBox runat="server" ID="txtSerialNo" CssClass="TextBox" Width="70%" onblur="CheckSerial(); return false;"></asp:TextBox>
                                            &nbsp;<asp:ImageButton ID="imgBtnSerial" runat="server" ImageUrl="~/Images/icon_search.png"
                                                ImageAlign="Middle" OnClick="imgBtnSerial_Click" />
                                        </div>
                                    </div>
                                </div>
                                <div class="invunkwn6">
                                    <div class="invunkwn7">
                                        No</div>
                                    <div class="invunkwn8">
                                        Item</div>
                                    <div class="invunkwn9">
                                        Description</div>
                                    <div class="invunkwn10">
                                        Status</div>
                                    <div class="invunkwn11">
                                        Qty</div>
                                    <div class="invunkwn12">
                                        Unit Price</div>
                                    <div class="invunkwn12">
                                        Dis. Rate</div>
                                    <div class="invunkwn12">
                                        Dis. Amt</div>
                                    <div class="invunkwn12">
                                        VAT Amt</div>
                                    <div class="invunkwn12">
                                        Amt</div>
                                    <div class="invunkwn13">
                                        Book</div>
                                    <div class="invunkwn13">
                                        Level</div>
                                </div>
                                <div class="invunkwn4">
                                    <div class="invunkwn14">
                                        No
                                    </div>
                                    <div class="invunkwn15">
                                        <asp:TextBox runat="server" onchange="ToUpper(this)" ID="txtItem" CssClass="TextBoxUpper"
                                            ClientIDMode="Static" Width="95%" MaxLength="20"></asp:TextBox></div>
                                    <div class="invunkwn16">
                                        <asp:TextBox runat="server" ID="txtDescription" CssClass="TextBox" Enabled="false"
                                            ClientIDMode="Static" Width="95%" Font-Size="11px"></asp:TextBox></div>
                                    <div class="invunkwn17">
                                        <asp:TextBox runat="server" onchange="ToUpper(this)" ID="txtStatus" CssClass="TextBoxUpper"
                                            Width="94%" ClientIDMode="Static" MaxLength="10" onblur="CheckItemStatus()"></asp:TextBox></div>
                                    <div class="invunkwn18">
                                        <asp:TextBox runat="server" ID="txtQty" CssClass="TextBox" class="invtxtalnrt" Width="87.5%"></asp:TextBox></div>
                                    <div class="invunkwn19">
                                        <asp:TextBox runat="server" ID="txtUnitPrice" CssClass="TextBox" class="invtxtalnrt"
                                            Width="94.4%"></asp:TextBox></div>
                                    <div class="invunkwn19">
                                        <asp:TextBox runat="server" ID="txtDiscount" CssClass="TextBox" class="invtxtalnrt"
                                            Width="94.4%"></asp:TextBox></div>
                                    <div class="invunkwn19">
                                        <asp:TextBox runat="server" ID="txtDiscountAmt" CssClass="TextBox" class="invtxtalnrt"
                                            Width="94.4%"></asp:TextBox></div>
                                    <div class="invunkwn19">
                                        <asp:TextBox runat="server" ID="txtVATAmt" CssClass="TextBox" class="invtxtalnrt"
                                            Width="94.4%"></asp:TextBox></div>
                                    <div class="invunkwn19">
                                        <asp:TextBox runat="server" ID="txtTotalAmt" Style="text-align: right; font-size: 11px;"
                                            Width="100%" BackColor="#EEEEEE" BorderWidth="0px" ReadOnly="true"></asp:TextBox></div>
                                    <div class="invunkwn20">
                                        &nbsp;</div>
                                    <div class="div1pcelt">
                                        <asp:ImageButton runat="server" ID="btnAddItem" ImageAlign="Middle" ImageUrl="~/Images/download_arrow_icon.png"
                                            ToolTip="Add Item" Width="16px" Height="16px" OnClick="AddItem" />
                                    </div>
                                    <div class="div1pcelt">
                                        &nbsp;
                                    </div>
                                    <div class="div1pcelt">
                                        <asp:ImageButton runat="server" ID="btnSerial" ImageAlign="Middle" Width="22px" Height="16px"
                                            ImageUrl="~/Images/serialno2.gif" ToolTip="Show Serials" />
                                    </div>
                                </div>
                                <%-- Item Detail --%>
                                <div class="invunkwn21">
                                    <asp:Panel ID="pnlItem" runat="server" Height="125px" ScrollBars="Auto" BorderColor="#9F9F9F"
                                        BorderWidth="1px" Font-Bold="true" Width="99%">
                                        <asp:GridView ID="gvInvoiceItem" CssClass="GridView" runat="server" AutoGenerateColumns="false"
                                            Font-Bold="false" CellPadding="3" OnRowDeleting="OnRemoveFromInvoiceItemGrid"
                                            OnRowCommand="OnRowSelectCommand" ForeColor="#333333" GridLines="Both" DataKeyNames="sad_itm_cd,sad_itm_stus,sad_unit_rt,sad_pbook,sad_pb_lvl,Sad_qty,sad_disc_amt,sad_itm_tax_amt,sad_promo_cd,sad_is_promo,sad_job_line"
                                            ShowHeader="false" ShowHeaderWhenEmpty="true" OnRowDataBound="OnBindInvoiceItem">
                                            <HeaderStyle BackColor="White" Font-Size="0%" />
                                            <EditRowStyle BackColor="#2461BF" />
                                            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                            <RowStyle BackColor="#EFF3FB" />
                                            <AlternatingRowStyle BackColor="White" />
                                            <EmptyDataTemplate>
                                                <div class="invunkwn22">
                                                    No data found
                                                </div>
                                            </EmptyDataTemplate>
                                            <Columns>
                                                <asp:BoundField DataField='sad_itm_line' HeaderText='No' ItemStyle-Width="10px" HeaderStyle-HorizontalAlign="Right"
                                                    ItemStyle-HorizontalAlign="Right" />
                                                <asp:BoundField DataField='sad_itm_cd' HeaderText='Item' ItemStyle-Width="75px" HeaderStyle-HorizontalAlign="Left" />
                                                <asp:TemplateField ItemStyle-Width="11px" HeaderStyle-Width="11px">
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="imgBtnGridAltItm" runat="server" ImageUrl="~/Images/icon_search.png"
                                                            Width="10px" Height="10px" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField='mi_longdesc' HeaderText='Description' ItemStyle-Width="165px"
                                                    HeaderStyle-HorizontalAlign="Left" />
                                                <asp:BoundField DataField='sad_itm_stus' HeaderText='Status' ItemStyle-Width="65px"
                                                    HeaderStyle-HorizontalAlign="Left" />
                                                <asp:TemplateField HeaderText="Qty" HeaderStyle-HorizontalAlign="Right" ControlStyle-Width="24px"
                                                    ItemStyle-Width="27px" ItemStyle-HorizontalAlign="Right">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblGridQty" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Sad_qty","{0:#,##0.00;(#,##0.00);0}" ) %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="txtGridQty" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Sad_qty","{0:#,##0.00;(#,##0.00);0}") %>'></asp:TextBox>
                                                    </EditItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField='sad_unit_rt' HeaderText='Unit Price' HeaderStyle-HorizontalAlign="Right"
                                                    ItemStyle-HorizontalAlign="Right" DataFormatString="{0:#,##0.00;(#,##0.00);0}"
                                                    HtmlEncode="false" ItemStyle-Width="66px" HeaderStyle-Width="66px" />
                                                <asp:BoundField DataField='sad_disc_rt' HeaderText='Dis. Rate' ItemStyle-Width="66px"
                                                    HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:#,##0.00;(#,##0.00);0}" />
                                                <asp:BoundField DataField='sad_disc_amt' HeaderText='Dis. Amt' ItemStyle-Width="66px"
                                                    HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:#,##0.00;(#,##0.00);0}" />
                                                <asp:BoundField DataField='sad_itm_tax_amt' HeaderText='TAX Amt' ItemStyle-Width="66px"
                                                    HeaderStyle-Width="66px" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right"
                                                    DataFormatString="{0:#,##0.00;(#,##0.00);0}" />
                                                <asp:BoundField DataField='sad_tot_amt' HeaderText='Amt' ItemStyle-Width="66px" HeaderStyle-HorizontalAlign="Right"
                                                    ItemStyle-HorizontalAlign="Right" DataFormatString="{0:#,##0.00;(#,##0.00);0}" />
                                                <asp:BoundField DataField='sad_pbook' HeaderText='Book' ItemStyle-Width="40px" HeaderStyle-HorizontalAlign="Left" />
                                                <asp:BoundField DataField='sad_pb_lvl' HeaderText='Level' ItemStyle-Width="40px"
                                                    HeaderStyle-HorizontalAlign="Left" />
                                                <%-- <asp:TemplateField ItemStyle-Width="5px" >
                                                <ItemTemplate >
                                                    <asp:ImageButton ID="imgBtnGridEdit" runat="server" ImageUrl="~/Images/EditIcon.png"
                                                        Width="14px" Height="14px" />
                                                </ItemTemplate>
                                            </asp:TemplateField>--%>
                                                <asp:TemplateField ItemStyle-Width="5px">
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="imgBtnGridDelete" runat="server" ImageUrl="~/Images/Delete.png"
                                                            Width="14px" Height="14px" CommandName="Delete" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField ItemStyle-Width="5px">
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="imgBtnGridSerial" runat="server" ImageUrl="~/Images/Add-16x16x16.ICO"
                                                            Width="14px" Height="14px" CommandName="SELECT" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "Sad_itm_line" ) +"|" + DataBinder.Eval(Container.DataItem,"sad_itm_cd") +"|" + DataBinder.Eval(Container.DataItem,"sad_itm_stus") +"|" +DataBinder.Eval(Container.DataItem,"Sad_qty") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <%--<asp:BoundField DataField='Sad_job_line' HeaderText='combine line' ItemStyle-Width="40px"
                                                HeaderStyle-HorizontalAlign="Left" />--%>
                                            </Columns>
                                            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                            <SortedAscendingCellStyle BackColor="#F5F7FB" />
                                            <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                                            <SortedDescendingCellStyle BackColor="#E9EBEF" />
                                            <SortedDescendingHeaderStyle BackColor="#4870BE" />
                                        </asp:GridView>
                                    </asp:Panel>
                                </div>
                                <div class="invunkwn23">
                                    <div class="div1pcelt">
                                        &nbsp;</div>
                                    <div class="invunkwn24">
                                        Sub Total</div>
                                    <div class="invunkwn25">
                                        <asp:Label runat="server" ID="lblGrndSubTotal" Text="0.00"></asp:Label>
                                    </div>
                                    <div class="div1pcelt">
                                        &nbsp;</div>
                                    <div class="invunkwn26">
                                        Discount</div>
                                    <div class="invunkwn27">
                                        <asp:Label runat="server" ID="lblGrndDiscount" Text="0.00"></asp:Label>
                                    </div>
                                    <div class="div1pcelt">
                                        &nbsp;</div>
                                    <div class="invunkwn28">
                                        After Discount</div>
                                    <div class="invunkwn29">
                                        <asp:Label runat="server" ID="lblGrndAfterDiscount" Text="0.00"></asp:Label>
                                    </div>
                                </div>
                                <div class="invunkwn30">
                                    <div class="div1pcelt">
                                        &nbsp;</div>
                                    <div class="invunkwn31">
                                        Other Chargers</div>
                                    <div class="invunkwn32">
                                        <asp:Label runat="server" ID="lblGrndOtherChargers" Text="0.00"></asp:Label>
                                    </div>
                                    <div class="div1pcelt">
                                        &nbsp;</div>
                                    <div class="invunkwn33">
                                        Tax
                                    </div>
                                    <div class="invunkwn34">
                                        <asp:Label runat="server" ID="lblGrndTax" Text="0.00" class="invtxtalnrt"></asp:Label>
                                    </div>
                                    <div class="div1pcelt">
                                        &nbsp;</div>
                                    <div class="invunkwn35">
                                        Total Amount
                                    </div>
                                    <div class="invunkwn36">
                                        <asp:Label runat="server" ID="lblGrndTotalAmount" Text="0.00"></asp:Label>
                                    </div>
                                </div>
                            </asp:Panel>
                        </div>
                    </div>
                    <%--Payment details--%>
                    <div class="invunkwn39">
                        <%--Collaps Header - Payment Items--%>
                        <div class="CollapsiblePanelHeader invcollapsovrid">
                            Payment Details</div>
                        <%--Collaps Image - Payment Items--%>
                        <div class="invfltlt">
                            <asp:Image runat="server" ID="Image4" ImageUrl="~/Images/16 X 16 DownArrow.jpg" ImageAlign="Right" /></div>
                        <%--Collaps control - Payment Items--%>
                        <asp:CollapsiblePanelExtender ID="CPEPayment" runat="server" TargetControlID="pnlPayment"
                            CollapsedSize="0" ExpandedSize="250" Collapsed="true" ExpandControlID="Image4"
                            ScrollContents="true" CollapseControlID="Image4" AutoCollapse="False" AutoExpand="False"
                            ExpandDirection="Vertical" ImageControlID="Image4" ExpandedImage="~/Images/16 X 16 UpArrow.jpg"
                            CollapsedImage="~/Images/16 X 16 DownArrow.jpg">
                        </asp:CollapsiblePanelExtender>
                        <div class="invunkwn40">
                            <asp:Panel ID="pnlPayment" runat="server" Height="171px">
                                <div class="invunkwn41">
                                    <div class="invunkwn5">
                                        <div class="div1pcelt">
                                            &nbsp;</div>
                                        <div class="invunkwn20">
                                            Pay Mode
                                        </div>
                                        <div style="float: left; width: 25%;">
                                            <asp:DropDownList ID="ddlPayMode" runat="server" Width="80%" CssClass="ComboBox"
                                                OnSelectedIndexChanged="PaymentType_LostFocus" AutoPostBack="true">
                                            </asp:DropDownList>
                                        </div>
                                        <div class="div1pcelt">
                                            &nbsp;</div>
                                        <div class="div10pcelt">
                                            To Pay
                                        </div>
                                        <div class="div35pcelt">
                                            <asp:TextBox ID="txtPayAmount" runat="server" CssClass="TextBoxUpper" Width="50%"
                                                Enabled="False"></asp:TextBox>
                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                            <asp:Button ID="btnPayAdd" runat="server" CssClass="Button" Text="Add" OnClick="AddPayment" />
                                        </div>
                                    </div>
                                    <div class="invunkwn42">
                                        <div class="div1pcelt">
                                            &nbsp;</div>
                                        <div class="invunkwn44" style="height: 70px;">
                                            <div style="float: left; width: 100%;">
                                                <div style="float: left; width: 1%;">
                                                    &nbsp;
                                                </div>
                                                <div style="float: left; width: 30%;">
                                                    Currancy
                                                </div>
                                                <div style="float: left; width: 69%;">
                                                    <asp:DropDownList ID="DropDownCurrancy" runat="server" CssClass="ComboBox" AutoPostBack="True"
                                                        OnSelectedIndexChanged="DropDownCurrancy_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                            <div style="float: left; width: 100%;">
                                                <div style="float: left; width: 1%;">
                                                    &nbsp;
                                                </div>
                                                <div style="float: left; width: 30%;">
                                                    Amount
                                                </div>
                                                <div style="float: left; width: 69%;">
                                                    <asp:TextBox ID="TextBoxCurAmo" runat="server" CssClass="TextBox" onkeypress="return numbersonly(event,true)"
                                                        AutoPostBack="True" OnTextChanged="TextBoxCurAmo_TextChanged"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div style="float: left; width: 100%;">
                                                <div style="float: left; width: 1%;">
                                                    &nbsp;
                                                </div>
                                                <div style="float: left; width: 49%;">
                                                    <div style="float: left; width: 30%;">
                                                        Exc. Rate
                                                    </div>
                                                    <div style="float: left; width: 69%;">
                                                        <asp:Label ID="LabelExRate" runat="server"></asp:Label>
                                                    </div>
                                                </div>
                                                <div style="float: left; width: 1%;">
                                                    &nbsp;
                                                </div>
                                                <div style="float: left; width: 49%;">
                                                    <div style="float: left; width: 30%;">
                                                        PC Amount
                                                    </div>
                                                    <div style="float: left; width: 69%;">
                                                        <asp:Label ID="LabelPcAmo" runat="server"></asp:Label>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="div1pcelt">
                                            &nbsp;</div>
                                        <div class="invunkwn20">
                                            Remarks
                                        </div>
                                        <div class="invunkwn43">
                                            <asp:TextBox ID="txtPayRemarks" runat="server" CssClass="TextBox" Width="100%" TextMode="MultiLine"
                                                Rows="2" onKeyUp="javascript:CheckCharacterCount(this,250);" onChange="javascript:CheckCharacterCount(this,100);"></asp:TextBox></div>
                                    </div>
                                    <div class="invunkwn42">
                                        <%--Credit/Cheque/Bank Slip payment--%>
                                        <div class="invunkwn44" runat="server" id="divCredit" visible="false">
                                            <div class="invunkwn5">
                                                <div class="div1pcelt">
                                                    &nbsp;</div>
                                                <div class="invunkwn20">
                                                    <asp:Label ID="lblPayCrCardNo" runat="server" Text="Card No"></asp:Label></div>
                                                <div class="invunkwn45">
                                                    <asp:TextBox onchange="ToUpper(this)" runat="server" ID="txtPayCrCardNo" CssClass="TextBox"
                                                        Width="80%" MaxLength="15"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="invunkwn5">
                                                <div class="div1pcelt">
                                                    &nbsp;</div>
                                                <div class="invunkwn20">
                                                    Bank
                                                </div>
                                                <div class="invunkwn45">
                                                    <asp:TextBox runat="server" onchange="ToUpper(this)" ID="txtPayCrBank" CssClass="TextBox"
                                                        Width="66%" MaxLength="10"></asp:TextBox><asp:ImageButton ID="imgBtnBank" runat="server"
                                                            ImageUrl="~/Images/icon_search.png" ImageAlign="Middle" OnClick="ImgBankSearch_Click" />
                                                </div>
                                                <div class="div1pcelt">
                                                    &nbsp;</div>
                                                <div class="div10pcelt">
                                                    Branch
                                                </div>
                                                <div class="invunkwn46">
                                                    <asp:TextBox runat="server" onchange="ToUpper(this)" ID="txtPayCrBranch" CssClass="TextBox"
                                                        Width="75%" MaxLength="15"></asp:TextBox>
                                                    <asp:ImageButton ID="imgBtnBranch" runat="server" ImageUrl="~/Images/icon_search.png"
                                                        ImageAlign="Middle" OnClick="ImgBankBranchSearch_Click" />
                                                </div>
                                            </div>
                                            <div class="invunkwn5">
                                                <div class="div1pcelt">
                                                    &nbsp;</div>
                                                <div class="invunkwn20">
                                                    Card Type
                                                </div>
                                                <div class="invunkwn47">
                                                    <asp:DropDownList runat="server" ID="txtPayCrCardType" CssClass="ComboBox" Width="80%">
                                                        <asp:ListItem Text="" Selected="True"></asp:ListItem>
                                                        <asp:ListItem Text="AMEX"></asp:ListItem>
                                                        <asp:ListItem Text="VISA"></asp:ListItem>
                                                        <asp:ListItem Text="MASTER"></asp:ListItem>
                                                        <asp:ListItem Text="DISCOVER"></asp:ListItem>
                                                        <asp:ListItem Text="2CO"></asp:ListItem>
                                                        <asp:ListItem Text="SAGE"></asp:ListItem>
                                                        <asp:ListItem Text="DELTA"></asp:ListItem>
                                                        <asp:ListItem Text="CIRRUS"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                                <div class="div1pcelt">
                                                    &nbsp;</div>
                                                <div class="invunkwn20">
                                                    Expiry Date
                                                </div>
                                                <div class="invunkwn48">
                                                    <asp:TextBox runat="server" ID="txtPayCrExpiryDate" Enabled="false" CssClass="TextBox"
                                                        Width="70%"></asp:TextBox>
                                                    &nbsp;<asp:Image ID="btnImgCrExpiryDate" runat="server" ImageUrl="~/Images/Calendar_scheduleHS.png"
                                                        ImageAlign="Middle" />
                                                </div>
                                            </div>
                                            <div class="invunkwn5">
                                                <div class="div1pcelt">
                                                    &nbsp;</div>
                                                <div class="invunkwn20">
                                                    Promotion
                                                </div>
                                                <div class="invunkwn49">
                                                    <asp:CheckBox runat="server" ID="chkPayCrPromotion" onclick="PromotionPeriod()" />
                                                    &nbsp; Period &nbsp;
                                                    <asp:TextBox runat="server" ID="txtPayCrPeriod" CssClass="TextBoxNumeric" Width="20%"
                                                        MaxLength="2"></asp:TextBox>
                                                    months
                                                </div>
                                            </div>
                                        </div>
                                        <%--Advance receipt/Credit Note payment/Loyalty/Gift vouchas--%>
                                        <div class="invunkwn5" runat="server" id="divAdvReceipt" visible="false">
                                            <div class="invunkwn5">
                                                <div class="div1pcelt">
                                                    &nbsp;</div>
                                                <div class="invunkwn50">
                                                    Referance</div>
                                                <div class="invunkwn51">
                                                    <asp:TextBox runat="server" ID="txtPayAdvReceiptNo" onchange="ToUpper(this)" onblur="GetAdvanceReceiptAmount()"
                                                        CssClass="TextBox" Width="80%"></asp:TextBox><asp:ImageButton ID="ImageButton2" runat="server"
                                                            ImageUrl="~/Images/icon_search.png" ImageAlign="Middle" OnClick="imgrecSearch_Click" />
                                                </div>
                                            </div>
                                            <div class="invunkwn5">
                                                <div class="div1pcelt">
                                                    &nbsp;</div>
                                                <div class="invunkwn50">
                                                    Ref. Amount</div>
                                                <div class="invunkwn52">
                                                    <asp:TextBox runat="server" ID="txtPayAdvRefAmount" CssClass="TextBox" Width="80%"
                                                        Enabled="false"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="invunkwn41">
                                    <div class="invunkwn5">
                                        <asp:Panel ID="pnlPay" runat="server" Height="120px" ScrollBars="Auto">
                                            <asp:GridView ID="gvPayment" runat="server" AutoGenerateColumns="False" CssClass="GridView"
                                                CellPadding="3" ForeColor="#333333" GridLines="Both" DataKeyNames="sard_pay_tp,sard_settle_amt"
                                                OnRowDeleting="gvPayment_OnDelete" ShowHeaderWhenEmpty="true">
                                                <EditRowStyle BackColor="#2461BF" />
                                                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                                <HeaderStyle BackColor="#507CD1" ForeColor="White" Font-Bold="True" />
                                                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                                <RowStyle BackColor="#EFF3FB" />
                                                <AlternatingRowStyle BackColor="White" />
                                                <EmptyDataTemplate>
                                                    <div class="invunkwn53">
                                                        No data found
                                                    </div>
                                                </EmptyDataTemplate>
                                                <Columns>
                                                    <asp:TemplateField>
                                                        <ItemTemplate>
                                                            <asp:ImageButton ID="btnImgDelete" runat="server" ImageAlign="Middle" ImageUrl="~/Images/Delete.png"
                                                                Width="10px" Height="10px" CommandName="Delete" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField='sard_seq_no' HeaderText='' Visible="false" />
                                                    <asp:BoundField DataField='sard_line_no' HeaderText='' Visible="false" />
                                                    <asp:BoundField DataField='sard_receipt_no' HeaderText='' Visible="false" />
                                                    <asp:BoundField DataField='sard_inv_no' HeaderText='' Visible="false" />
                                                    <asp:BoundField DataField='sard_pay_tp' HeaderText='Payment Type' ItemStyle-Width="110px"
                                                        HeaderStyle-Width="110px" />
                                                    <asp:BoundField DataField='Sard_anal_1' HeaderText='Currancy' ItemStyle-Width="110px"
                                                        HeaderStyle-Width="110px" />
                                                    <asp:BoundField DataField='Sard_anal_3' HeaderText='Amount' ItemStyle-Width="110px"
                                                        HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right" DataFormatString='<%$ appSettings:FormatToCurrency %>' />
                                                    <asp:BoundField DataField='sard_ref_no' HeaderText='' Visible="false" />
                                                    <asp:BoundField DataField='sard_chq_bank_cd' HeaderText='Bank' ItemStyle-Width="90px"
                                                        HeaderStyle-Width="90px" />
                                                    <asp:BoundField DataField='sard_chq_branch' HeaderText='Branch' ItemStyle-Width="90px"
                                                        HeaderStyle-Width="90px" />
                                                    <asp:BoundField DataField='sard_deposit_bank_cd' HeaderText='' Visible="false" />
                                                    <asp:BoundField DataField='sard_deposit_branch' HeaderText='' Visible="false" />
                                                    <asp:BoundField DataField='sard_credit_card_bank' HeaderText='' Visible="false" />
                                                    <asp:BoundField DataField='sard_cc_tp' HeaderText='Card Type' />
                                                    <asp:BoundField DataField='sard_cc_expiry_dt' HeaderText='Expiry Date' Visible="false" />
                                                    <asp:BoundField DataField='sard_cc_is_promo' HeaderText='Promotion' Visible="false" />
                                                    <asp:BoundField DataField='sard_cc_period' HeaderText='Period' Visible="false" />
                                                    <asp:BoundField DataField='sard_gv_issue_loc' HeaderText='' Visible="false" />
                                                    <asp:BoundField DataField='sard_gv_issue_dt' HeaderText='' Visible="false" />
                                                    <asp:BoundField DataField='sard_settle_amt' HeaderText='Final Amount' HeaderStyle-HorizontalAlign="Right"
                                                        ItemStyle-HorizontalAlign="Right" DataFormatString='<%$ appSettings:FormatToCurrency %>' />
                                                    <asp:BoundField DataField='sard_sim_ser' HeaderText='' Visible="false" />
                                                </Columns>
                                                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                                <SortedAscendingCellStyle BackColor="#F5F7FB" />
                                                <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                                                <SortedDescendingCellStyle BackColor="#E9EBEF" />
                                                <SortedDescendingHeaderStyle BackColor="#4870BE" />
                                            </asp:GridView>
                                        </asp:Panel>
                                    </div>
                                    <div class="invunkwn5">
                                        <div class="div1pcelt">
                                            &nbsp;</div>
                                        <div class="invunkwn54">
                                            Paid Amount
                                        </div>
                                        <div class="invunkwn55">
                                            <asp:Label runat="server" ID="lblPayPaid" Text="0.00"></asp:Label>
                                        </div>
                                        <div class="div18pcelt">
                                            &nbsp;
                                        </div>
                                        <div class="invunkwn54">
                                            Balance Amount
                                        </div>
                                        <div class="invunkwn55">
                                            <asp:Label runat="server" ID="lblPayBalance" Text="0.00"></asp:Label>
                                        </div>
                                        <div class="div1pcelt">
                                            &nbsp;</div>
                                    </div>
                                </div>
                            </asp:Panel>
                        </div>
                    </div>
                    <%--Invoice header details--%>
                    <div class="invunkwn5">
                        <%-- Collaps Header - Invoice Header --%>
                        <div class="CollapsiblePanelHeader invcollapsovrid">
                            General Details</div>
                        <%-- Collaps Image - Invoice Header --%>
                        <div class="invfltlt">
                            <asp:Image runat="server" ID="Image2" ImageUrl="~/Images/16 X 16 DownArrow.jpg" ImageAlign="Right" /></div>
                        <%-- Collaps control - Invoice Header --%>
                        <asp:CollapsiblePanelExtender ID="CPEHeader" runat="server" TargetControlID="pnlHeader"
                            CollapsedSize="0" ExpandedSize="50" Collapsed="True" ExpandControlID="Image2"
                            CollapseControlID="Image2" AutoCollapse="False" AutoExpand="False" ScrollContents="false"
                            ExpandDirection="Vertical" ImageControlID="Image2" ExpandedImage="~/Images/16 X 16 UpArrow.jpg"
                            CollapsedImage="~/Images/16 X 16 DownArrow.jpg">
                        </asp:CollapsiblePanelExtender>
                        <div class="invcolarea">
                            <asp:Panel runat="server" Height="100%" ID="pnlHeader" Width="99.8%" ScrollBars="Auto"
                                BorderColor="#9F9F9F" BorderWidth="1px" Font-Bold="false">
                                <%-- Row 1 --%>
                                <div class="invunkwn37">
                                    <div class="div1pcelt">
                                        &nbsp;</div>
                                    <div class="div16pcelt">
                                        Manual Ref. No
                                    </div>
                                    <div class="invunkwn38">
                                        <asp:TextBox runat="server" onchange="ToUpper(this)" ID="txtManualRefNo" CssClass="TextBox"
                                            Width="55%" MaxLength="10"></asp:TextBox>&nbsp;<asp:CheckBox ID="chkManualRef" runat="server"
                                                Text="By memo" OnCheckedChanged="chkManual_CheckedChanged" AutoPostBack="true" />
                                    </div>
                                    <div class="div1pcelt">
                                        &nbsp;</div>
                                </div>
                                <%-- Row 2 --%>
                                <div class="inv100pceltpd2">
                                    <div class="div1pcelt">
                                        &nbsp;</div>
                                    <div class="div16pcelt">
                                        Tax Invoice
                                    </div>
                                    <div class="div16pcelt">
                                        <asp:CheckBox ID="chkTaxInvoice" runat="server" Enabled="false" />
                                    </div>
                                    <div class="div1pcelt">
                                        &nbsp;</div>
                                    <div class="div16pcelt">
                                        PO No
                                    </div>
                                    <div class="div16pcelt">
                                        <asp:TextBox runat="server" onchange="ToUpper(this)" ID="txtPOno" CssClass="TextBoxUpper"
                                            Width="75%" MaxLength="10"></asp:TextBox>&nbsp;<asp:ImageButton ID="imgBtnPO" runat="server"
                                                ImageUrl="~/Images/icon_search.png" ImageAlign="Middle" /></div>
                                    <div class="div1pcelt">
                                        &nbsp;</div>
                                </div>
                            </asp:Panel>
                        </div>
                    </div>
                </div>
            </asp:Panel>
            <%-- Calender Extender --%>
            <asp:CalendarExtender ID="CEdate" runat="server" TargetControlID="txtDate" PopupPosition="BottomLeft"
                PopupButtonID="imgBtnDate" EnabledOnClient="true" Animated="true" Format="dd/MM/yyyy">
            </asp:CalendarExtender>
            <asp:CalendarExtender ID="CEExpirydate" runat="server" TargetControlID="txtPayCrExpiryDate"
                PopupPosition="BottomLeft" PopupButtonID="btnImgCrExpiryDate" EnabledOnClient="true"
                Animated="true" Format="dd/MM/yyyy">
            </asp:CalendarExtender>
            <%-- Modal Popup Delivery details--%>
            <div>
                <asp:ModalPopupExtender ID="MPEDelivery" RepositionMode="RepositionOnWindowScroll"
                    BehaviorID="DeliveryModal" TargetControlID="btnDeliver" runat="server" ClientIDMode="Static"
                    PopupControlID="pnlDeliveryPopUp" BackgroundCssClass="modalBackground" CancelControlID="imgbtnDelClose"
                    PopupDragHandleControlID="divpopDeliveryHeader">
                </asp:ModalPopupExtender>
                <asp:Panel runat="server" ID="pnlDeliveryPopUp" Height="160px" Width="550px" CssClass="ModalWindow"
                    class="invunkwn56">
                    <div class="popUpHeader" id="divpopDeliveryHeader" runat="server">
                        <div class="div80pcelt" runat="server">
                            Delivery Instruction Details</div>
                        <div class="invunkwn57">
                            <asp:ImageButton ID="imgbtnDelClose" runat="server" ImageUrl="~/Images/0032-01-16x16x16.ICO" />&nbsp;</div>
                    </div>
                    <%-- PopUp Message Area --%>
                    <div class="invunkwn58">
                        <asp:Label ID="lblPopDelMsg" runat="server" Text="" Width="100%"></asp:Label>
                    </div>
                    <div class="invunkwn59">
                        <div class="invunkwn60">
                            <div class="div1pcelt">
                                &nbsp;</div>
                            <div class="invunkwn50">
                                Location</div>
                            <div class="div77pcelt">
                                <asp:TextBox runat="server" onchange="ToUpper(this)" ID="txtDelLocation" CssClass="TextBox"
                                    Width="40%" MaxLength="10" ReadOnly="false"></asp:TextBox>
                                &nbsp;
                                <asp:CheckBox runat="server" ID="chkOpenDelivery" Text="Open Delivery" Checked="false"
                                    onchange="MakeDefaultLocationforDelivery()" />
                            </div>
                        </div>
                        <div class="invunkwn60">
                            <div class="div1pcelt">
                                &nbsp;</div>
                            <div class="invunkwn50">
                                Customer</div>
                            <div class="div30pcelt">
                                <asp:TextBox runat="server" onchange="ToUpper(this)" ID="txtDelCustomer" CssClass="TextBox"
                                    Width="100%" MaxLength="12"></asp:TextBox></div>
                        </div>
                        <div class="invunkwn60">
                            <div class="div1pcelt">
                                &nbsp;</div>
                            <div class="invunkwn50">
                                Name</div>
                            <div class="invunkwn41">
                                <asp:TextBox runat="server" onchange="ToUpper(this)" ID="txtDelName" CssClass="TextBox"
                                    Width="100%" MaxLength="50"></asp:TextBox></div>
                        </div>
                        <div class="invunkwn60">
                            <div class="div1pcelt">
                                &nbsp;</div>
                            <div class="invunkwn50">
                                Address</div>
                            <div class="div55pcelt">
                                <asp:TextBox runat="server" onchange="ToUpper(this)" ID="txtDelAddress1" CssClass="TextBox"
                                    Width="100%" MaxLength="100"></asp:TextBox></div>
                        </div>
                        <div class="invunkwn60">
                            <div class="div1pcelt">
                                &nbsp;</div>
                            <div class="invunkwn50">
                                &nbsp;</div>
                            <div class="div55pcelt">
                                <asp:TextBox runat="server" onchange="ToUpper(this)" ID="txtDelAddress2" CssClass="TextBox"
                                    Width="100%" MaxLength="100"></asp:TextBox></div>
                            <div class="invunkwn50">
                                &nbsp;
                                <asp:Button runat="server" ID="btnDelConfirm" CssClass="Button" Text="Confirm" Width="50%" />
                            </div>
                        </div>
                    </div>
                </asp:Panel>
            </div>
            <%-- Modal Popup Extenders --%>
            <div>
                <asp:ModalPopupExtender ID="MPEPopup" RepositionMode="RepositionOnWindowScroll" BehaviorID="Modal"
                    TargetControlID="btnPopUp" runat="server" ClientIDMode="Static" PopupControlID="pnlPopUp"
                    BackgroundCssClass="modalBackground" CancelControlID="imgbtnClose" PopupDragHandleControlID="divpopHeader">
                </asp:ModalPopupExtender>
                <asp:Panel ID="pnlPopUp" runat="server" Height="550px" Width="500px" CssClass="ModalWindow">
                    <%-- PopUp Handler for drag and control --%>
                    <div class="popUpHeader" id="divpopHeader" runat="server">
                        <div class="div80pcelt" runat="server" id="divPopCaption">
                            Select Price</div>
                        <div class="invunkwn61">
                            <asp:ImageButton ID="imgbtnClose" runat="server" ImageUrl="~/Images/0032-01-16x16x16.ICO" />&nbsp;</div>
                    </div>
                    <%-- PopUp Message Area --%>
                    <div class="invunkwn58">
                        <asp:Label ID="lblMsg" runat="server" Text="" Width="100%"></asp:Label>
                    </div>
                    <asp:Panel runat="server" ID="pnlPopMain" Width="100%" ScrollBars="None">
                        <%-- Popup for Multiple Price Pick for Serialized price level --%>
                        <div class="invunkwn62" runat="server" id="divPopSerialPriceList" visible="false">
                            <%-- Confirm Button Area --%>
                            <div class="inv100break">
                                <div class="div1pcelt">
                                    &nbsp;</div>
                                <div class="invunkwn63">
                                    Selected Qty
                                </div>
                                <div class="invunkwn64">
                                    <asp:Label ID="lblPopSerialQty" runat="server" Text="" Width="100%"></asp:Label>
                                </div>
                                <div class="div20pcelt">
                                    <asp:Button ID="btnPopSerialConfirm" Text="Confirm" runat="server" CssClass="Button"
                                        OnClick="btnPopConfirm_Click" />
                                </div>
                            </div>
                            <asp:Panel ID="pnlPopPricePick" runat="server" ScrollBars="Auto" Height="111px" Width="100%">
                                <asp:GridView ID="gvPopSerialPricePick" CssClass="GridView" CellPadding="4" ForeColor="#333333"
                                    GridLines="Both" runat="server" AutoGenerateColumns="false" DataKeyNames="sars_price_type">
                                    <EditRowStyle BackColor="#2461BF" />
                                    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                    <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                    <RowStyle BackColor="#EFF3FB" />
                                    <AlternatingRowStyle BackColor="White" />
                                    <Columns>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:CheckBox runat="server" ID="chkPopPricePick" ClientIDMode="Static" OnCheckedChanged="CheckPopPriceListClick"
                                                    AutoPostBack="true" />
                                                <asp:HiddenField ID="hdnPriceType" Value=' <%# DataBinder.Eval(Container.DataItem, "sars_price_type") %>'
                                                    runat="server" />
                                                <asp:HiddenField ID="hdnPbSeq" Value=' <%# DataBinder.Eval(Container.DataItem, "Sars_pb_seq") %>'
                                                    runat="server" />
                                                <asp:HiddenField ID="hdnMainItem" Value=' <%# DataBinder.Eval(Container.DataItem, "Sars_itm_cd") %>'
                                                    runat="server" />
                                                <asp:HiddenField ID="hdnIsFixQty" Value=' <%# DataBinder.Eval(Container.DataItem, "sars_is_fix_qty") %>'
                                                    runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField='sars_circular_no' HeaderText='Circler No' HeaderStyle-Width="100px"
                                            HeaderStyle-HorizontalAlign="Left" />
                                        <asp:BoundField DataField='sars_price_type_desc' HeaderText='Price Type' HeaderStyle-Width="100px"
                                            HeaderStyle-HorizontalAlign="Left" />
                                        <asp:BoundField DataField='sars_ser_no' HeaderText='Serial No' HeaderStyle-Width="150px"
                                            HeaderStyle-HorizontalAlign="Left" />
                                        <asp:BoundField DataField='sars_itm_price' HeaderText='Unit Price' HeaderStyle-Width="100px"
                                            HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:#,##0.00;(#,##0.00);0}"
                                            HtmlEncode="false" />
                                        <asp:BoundField DataField='sars_val_to' HeaderText='Valid Until' HeaderStyle-Width="100px"
                                            DataFormatString="{0:d}" />
                                    </Columns>
                                    <SelectedRowStyle BackColor="#D7D3F2" Font-Bold="True" ForeColor="#333333" />
                                    <SortedAscendingCellStyle BackColor="#F5F7FB" />
                                    <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                                    <SortedDescendingCellStyle BackColor="#E9EBEF" />
                                    <SortedDescendingHeaderStyle BackColor="#4870BE" />
                                </asp:GridView>
                            </asp:Panel>
                        </div>
                        <%-- Popup for Multiple Price Pick from Non-Serialized price level --%>
                        <div class="invunkwn65" runat="server" id="divPopPriceList" visible="false">
                            <%-- Confirm Button Area --%>
                            <div class="inv100break">
                                <div class="div1pcelt">
                                    &nbsp;</div>
                                <div class="invunkwn63">
                                    <asp:Label ID="lblPopNonSerialQty" runat="server" Text="" Width="100%"></asp:Label>
                                </div>
                                <div class="invunkwn50">
                                    <asp:Button ID="btnPopPriceListNonSerial" Text="Confirm" runat="server" CssClass="Button"
                                        OnClick="btnPopConfirm_Click" />
                                </div>
                            </div>
                            <asp:Panel ID="Panel1" runat="server" ScrollBars="Auto" Height="111px" Width="100%">
                                <asp:GridView ID="gvPopPricePick" CssClass="GridView" CellPadding="4" ForeColor="#333333"
                                    GridLines="Both" runat="server" AutoGenerateColumns="false" DataKeyNames="sapd_itm_cd,sapd_pb_seq,sapd_is_fix_qty,sapd_price_type,sapd_itm_price,sapd_seq_no,sapd_circular_no"
                                    OnRowDataBound="gvPopPricePick_OnRowBind" OnSelectedIndexChanged="LoadNonSerializedCombination">
                                    <EditRowStyle BackColor="#2461BF" />
                                    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                    <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                    <RowStyle BackColor="#EFF3FB" />
                                    <AlternatingRowStyle BackColor="White" />
                                    <Columns>
                                        <asp:BoundField DataField='Sapd_circular_no' HeaderText='Circler No' HeaderStyle-Width="100px"
                                            HeaderStyle-HorizontalAlign="Left" />
                                        <asp:BoundField DataField='Sarpt_cd' HeaderText='Price Type' HeaderStyle-Width="100px"
                                            HeaderStyle-HorizontalAlign="Left" />
                                        <asp:BoundField DataField='Sapd_itm_price' HeaderText='Unit Price' HeaderStyle-Width="100px"
                                            HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:#,##0.00;(#,##0.00);0}"
                                            HtmlEncode="false" />
                                        <asp:BoundField DataField='Sapd_to_date' HeaderText='Valid Until' HeaderStyle-Width="100px"
                                            DataFormatString="{0:d}" />
                                    </Columns>
                                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                    <SortedAscendingCellStyle BackColor="#F5F7FB" />
                                    <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                                    <SortedDescendingCellStyle BackColor="#E9EBEF" />
                                    <SortedDescendingHeaderStyle BackColor="#4870BE" />
                                </asp:GridView>
                            </asp:Panel>
                        </div>
                        <%-- Popup for Load MRP List for the consumable items --%>
                        <div class="invunkwn65" runat="server" id="divConsumPricePick" visible="false">
                            <%-- Confirm Button Area --%>
                            <div class="inv100break">
                                <div class="div1pcelt">
                                    &nbsp;</div>
                                <div class="invunkwn63">
                                    <asp:Label ID="lblConsumReqQty" runat="server" Text="" Width="100%"></asp:Label>
                                </div>
                            </div>
                            <asp:Panel ID="Panel2" runat="server" ScrollBars="Auto" Height="151px" Width="100%">
                                <asp:GridView ID="gvPopConsumPricePick" CssClass="GridView" CellPadding="4" ForeColor="#333333"
                                    GridLines="Both" runat="server" AutoGenerateColumns="false" DataKeyNames="inb_unit_price,inb_doc_no,inb_itm_stus,inb_free_qty,inb_itm_line"
                                    OnRowDataBound="gvPopConsumPricePick_OnRowBind" OnSelectedIndexChanged="LoadConsumablePriceList">
                                    <AlternatingRowStyle BackColor="White" />
                                    <Columns>
                                        <asp:BoundField DataField='inb_doc_no' HeaderText='Document No' HeaderStyle-Width="100px"
                                            HeaderStyle-HorizontalAlign="Left" />
                                        <asp:BoundField DataField='inb_itm_stus' HeaderText='Item Status' HeaderStyle-Width="100px"
                                            HeaderStyle-HorizontalAlign="Left" />
                                        <asp:BoundField DataField='inb_unit_price' HeaderText='Unit Price' HeaderStyle-Width="100px"
                                            HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:#,##0.00;(#,##0.00);0}"
                                            HtmlEncode="false" />
                                        <asp:BoundField DataField='inb_free_qty' HeaderText='Qty' HeaderStyle-Width="100px" />
                                    </Columns>
                                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                    <SortedAscendingCellStyle BackColor="#F5F7FB" />
                                    <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                                    <SortedDescendingCellStyle BackColor="#E9EBEF" />
                                    <SortedDescendingHeaderStyle BackColor="#4870BE" />
                                </asp:GridView>
                            </asp:Panel>
                        </div>
                        <%-- Popup for Price Item Combination With Pick Serial --%>
                        <div class="invunkwn65" runat="server" id="divPopPriceItemCombination" visible="false">
                            <%-- Confirm Button Area --%>
                            <div style="float: left; width: 100%; color: Black; text-align: right;">
                                <div style="float: right;">
                                    <asp:Button ID="btnPopPriceItmCombinConfirm" Text="Confirm" runat="server" CssClass="Button"
                                        OnClick="btnPopConfirm_Click" />
                                    &nbsp;
                                </div>
                                <div style="float: right;">
                                    <asp:Button ID="btnPopPriceItmCombinCancel" Text="Cancel" runat="server" CssClass="Button"
                                        OnClick="btnPopCancel_Click" />
                                    &nbsp;
                                </div>
                            </div>
                            <%-- Combine Item Show when select above grid --%>
                            <asp:Panel ID="pnlPopItemCombine" runat="server" ScrollBars="Auto" Height="100px"
                                Width="100%">
                                <asp:GridView ID="gvPriceItemCombine" runat="server" AutoGenerateColumns="false"
                                    OnRowDataBound="gvPriceItemCombine_OnRowBind" CssClass="GridView" CellPadding="4"
                                    OnSelectedIndexChanged="LoadSelectedpPriceComSerialForPriceComItemSerialGv" ForeColor="#333333"
                                    GridLines="Both" DataKeyNames="sapc_qty,sapc_main_itm_cd,sapc_pb_seq,sapc_main_line,sapc_itm_line,sapc_main_ser,sapc_itm_cd">
                                    <EditRowStyle BackColor="#2461BF" />
                                    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                    <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                    <RowStyle BackColor="#EFF3FB" />
                                    <AlternatingRowStyle BackColor="White" />
                                    <Columns>
                                        <asp:BoundField DataField='sapc_itm_cd' HeaderText='Item' />
                                        <asp:BoundField DataField='mi_longdesc' HeaderText='Description' HeaderStyle-Width="220px" />
                                        <asp:BoundField DataField='mi_model' HeaderText='Model' />
                                        <asp:BoundField DataField='sapc_price' HeaderText='UnitPrice' />
                                        <asp:BoundField DataField='sapc_qty' HeaderText='Qty' />
                                        <asp:TemplateField HeaderText="Selected Qty" HeaderStyle-HorizontalAlign="Right"
                                            HeaderStyle-Width="95px">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtPopPriceItmComSelQty" Enabled="false" runat="server" Width="100%"
                                                    Text='<%# DataBinder.Eval(Container.DataItem, "sapc_qty") %>' Style="text-align: right;
                                                    border-style: none;"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                    <SortedAscendingCellStyle BackColor="#F5F7FB" />
                                    <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                                    <SortedDescendingCellStyle BackColor="#E9EBEF" />
                                    <SortedDescendingHeaderStyle BackColor="#4870BE" />
                                </asp:GridView>
                            </asp:Panel>
                            <%-- pick serial for combine items --%>
                            <div runat="server" id="divPopPriceComSerialPick">
                                <div class="invunkwn68">
                                    <div class="invunkwn69">
                                        &nbsp;
                                        <asp:Label ID="lblPopPriceComSerMsg" runat="server" Text="" Width="80%" ForeColor="Red"></asp:Label>
                                    </div>
                                    <div class="invunkwn70">
                                        <asp:Button runat="server" ID="btnPopPriceComSerAdd" CssClass="Button" Text="Add Serial"
                                            OnClick="AddPriceComItemSerialToList" />
                                    </div>
                                </div>
                                <asp:Panel runat="server" ID="pnlPopPriceComSerPnl" Width="100%" Height="200px" ScrollBars="Auto">
                                    <asp:GridView runat="server" ID="gvPopPriceComSerPick" AutoGenerateColumns="false"
                                        CssClass="GridView" CellPadding="3" ForeColor="#333333" GridLines="Both" ShowHeaderWhenEmpty="true"
                                        Width="100%">
                                        <EditRowStyle BackColor="#2461BF" />
                                        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                        <RowStyle BackColor="#EFF3FB" />
                                        <AlternatingRowStyle BackColor="White" />
                                        <Columns>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkPopPriceComSerSelect" runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField='Tus_itm_cd' HeaderText='Item' />
                                            <asp:BoundField DataField="Tus_ser_1" HeaderText="Serial No 1" />
                                            <asp:BoundField DataField="Tus_ser_2" HeaderText="Serial No 2" />
                                            <asp:BoundField DataField="tus_warr_no" HeaderText="Warranty" />
                                            <asp:BoundField DataField="Tus_itm_stus" HeaderText="Status" />
                                            <asp:BoundField DataField="Tus_ser_id" HeaderText="Serial ID" />
                                        </Columns>
                                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                        <SortedAscendingCellStyle BackColor="#F5F7FB" />
                                        <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                                        <SortedDescendingCellStyle BackColor="#E9EBEF" />
                                        <SortedDescendingHeaderStyle BackColor="#4870BE" />
                                    </asp:GridView>
                                </asp:Panel>
                            </div>
                        </div>
                    </asp:Panel>
                </asp:Panel>
            </div>
            <%-- Modal Popup Extenders for Customer --%>
            <div>
                <asp:ModalPopupExtender ID="MPEBusinessEntity" RepositionMode="RepositionOnWindowScroll"
                    BehaviorID="Business" TargetControlID="btnBusAdvDetailNo" runat="server" ClientIDMode="Static"
                    PopupControlID="pnlBusinessEntity" BackgroundCssClass="modalBackground" CancelControlID="imgBtnBusClose"
                    PopupDragHandleControlID="divpopBusHeader">
                </asp:ModalPopupExtender>
                <asp:Panel ID="pnlBusinessEntity" runat="server" Height="500px" Width="490px" CssClass="ModalWindow">
                    <%-- PopUp Handler for drag and control --%>
                    <div class="popUpHeader" id="divpopBusHeader" runat="server">
                        <div class="div80pcelt" runat="server" id="div2">
                            Business Entity Advance Detail</div>
                        <div class="invunkwn61">
                            <asp:ImageButton ID="imgBtnBusClose" runat="server" ImageUrl="~/Images/0032-01-16x16x16.ICO"
                                OnClientClick="TabModalPopupInBusinessEntityClose()" />&nbsp;</div>
                    </div>
                    <%-- PopUp Message Area --%>
                    <div class="invunkwn58">
                        <asp:Label ID="lblBusMsg" runat="server" Text="" Width="100%"></asp:Label>
                    </div>
                    <div class="div1pcelt">
                        &nbsp;
                    </div>
                    <div class="invunkwn5">
                        <uc1:uc_CustomerCreation ID="uc_CustomerCreation1" runat="server" />
                        <uc2:uc_CustCreationExternalDet ID="uc_CustCreationExternalDet1" runat="server" />
                    </div>
                    <div class="div1pcelt">
                        &nbsp;
                    </div>
                </asp:Panel>
            </div>
            <%-- Modal Popup Extenders for Re-Adding payments --%>
            <div>
                <asp:ModalPopupExtender ID="MPEReAddPayment" RepositionMode="RepositionOnWindowScroll"
                    BehaviorID="RePayment" TargetControlID="btnRePay" runat="server" ClientIDMode="Static"
                    PopupControlID="pnlRePayment" BackgroundCssClass="modalBackground" CancelControlID="imgBtnPayClose"
                    PopupDragHandleControlID="divRePayHeader">
                </asp:ModalPopupExtender>
                <asp:Panel ID="pnlRePayment" runat="server" Height="300px" Width="590px" CssClass="ModalWindow">
                    <%-- PopUp Handler for drag and control --%>
                    <div class="popUpHeader" id="divRePayHeader" runat="server">
                        <div class="div80pcelt" runat="server" id="div3">
                            Payment Detail</div>
                        <div class="invunkwn61">
                            <asp:ImageButton ID="imgBtnPayClose" runat="server" ImageUrl="~/Images/0032-01-16x16x16.ICO" />&nbsp;</div>
                    </div>
                    <%-- PopUp Message Area --%>
                    <div class="invunkwn58">
                        <asp:Label ID="lblRePayMsg" runat="server" Text="" Width="100%"></asp:Label>
                    </div>
                    <div class="invunkwn5">
                        <div class="invunkwn5">
                            <div class="div1pcelt">
                                &nbsp;
                            </div>
                            <div style="float: left; width: 19%; font-weight: bold;">
                                To be Pay
                            </div>
                            <div class="div30pcelt">
                                <asp:Label runat="server" ID="lblRePayToBePay" Text=""></asp:Label>
                            </div>
                        </div>
                        <asp:Panel runat="server" ID="pnlRePayDet" ScrollBars="Auto" Width="500px" Height="200px"
                            BorderStyle="Solid" BorderColor="Silver" BorderWidth="1px">
                            <asp:GridView ID="gvRePayment" runat="server" AutoGenerateColumns="False" CssClass="GridView"
                                Width="500px" CellPadding="3" ForeColor="#333333" GridLines="Both" DataKeyNames="sard_pay_tp,sard_settle_amt,sard_anal_3"
                                OnRowDeleting="gvPayment_OnDelete" ShowHeaderWhenEmpty="true">
                                <EditRowStyle BackColor="#2461BF" />
                                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                <HeaderStyle BackColor="#507CD1" ForeColor="White" Font-Bold="True" />
                                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                <RowStyle BackColor="#EFF3FB" />
                                <AlternatingRowStyle BackColor="White" />
                                <EmptyDataTemplate>
                                    <div style="width: 100%; text-align: center;">
                                        No data found
                                    </div>
                                </EmptyDataTemplate>
                                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                <SortedAscendingCellStyle BackColor="#F5F7FB" />
                                <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                                <SortedDescendingCellStyle BackColor="#E9EBEF" />
                                <SortedDescendingHeaderStyle BackColor="#4870BE" />
                                <Columns>
                                    <asp:BoundField DataField='sard_pay_tp' HeaderText='Payment Type' ItemStyle-Width="110px"
                                        HeaderStyle-Width="110px" />
                                    <asp:BoundField DataField='sard_chq_bank_cd' HeaderText='Bank' ItemStyle-Width="90px"
                                        HeaderStyle-Width="90px" />
                                    <asp:BoundField DataField='sard_chq_branch' HeaderText='Branch' ItemStyle-Width="90px"
                                        HeaderStyle-Width="90px" />
                                    <asp:BoundField DataField='sard_cc_tp' HeaderText='Card Type' />
                                    <asp:BoundField DataField='sard_settle_amt' HeaderText='Amount' ItemStyle-HorizontalAlign="Right" />
                                    <asp:TemplateField HeaderText="Collect Amount" ItemStyle-Width="110px" HeaderStyle-Width="110px"
                                        ItemStyle-HorizontalAlign="Right">
                                        <ItemTemplate>
                                            <asp:TextBox runat="server" ID="txtRePayCollectAmt" CssClass="TextBox" Text='<%#  DataBinder.Eval(Container.DataItem, "sard_anal_3") %>'></asp:TextBox>
                                            <asp:HiddenField runat="server" ID="hdnRePayLineNo" Value='<%#  DataBinder.Eval(Container.DataItem, "Sard_line_no") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </asp:Panel>
                        <div class="invunkwn5">
                            <div class="div5pcelt">
                                &nbsp;</div>
                            <div class="invunkwn50">
                                <asp:Button runat="server" ID="btnRePayConfirm" Text="Confirm" CssClass="Button"
                                    Width="100%" OnClick="btnConfirm_CheckUserNewPaymentAmount" />
                            </div>
                            <div class="div10pcelt">
                                &nbsp;</div>
                            <div class="invunkwn50">
                                <asp:Button runat="server" ID="btnRePayCancel" Text="Cancel" CssClass="Button" Width="100%" />
                            </div>
                        </div>
                    </div>
                </asp:Panel>
            </div>
            <%-- Modal Popup Extenders for discount --%>
            <div>
                <asp:ModalPopupExtender ID="MPEDiscount" RepositionMode="RepositionOnWindowScroll"
                    BehaviorID="Discount" TargetControlID="btnDisPnl" runat="server" ClientIDMode="Static"
                    PopupControlID="pnlApplyDiscount" BackgroundCssClass="modalBackground" CancelControlID="imgBtnDisClose"
                    PopupDragHandleControlID="divReDisHeader">
                </asp:ModalPopupExtender>
                <asp:Panel ID="pnlApplyDiscount" runat="server" Height="300px" Width="500px" CssClass="ModalWindow">
                    <div class="popUpHeader" id="divReDisHeader" runat="server">
                        <div class="div80pcelt">
                            Discount Request Detail</div>
                        <div class="invunkwn61">
                            <asp:ImageButton ID="imgBtnDisClose" runat="server" ImageUrl="~/Images/0032-01-16x16x16.ICO" />&nbsp;</div>
                    </div>
                    <%-- PopUp Message Area --%>
                    <div class="invunkwn58">
                        <asp:Label ID="lblDisMsg" runat="server" Text="" Width="100%"></asp:Label>
                    </div>
                    <div class="invunkwn5">
                        <div class="invunkwn5">
                            <div class="div5pcelt">
                                &nbsp;</div>
                            <div class="invunkwn20">
                                Category</div>
                            <div class="invunkwn50">
                                <asp:DropDownList runat="server" ID="ddlDisCategory" CssClass="ComboBox" Width="100%"
                                    OnSelectedIndexChanged="Category_onChange" AutoPostBack="true">
                                    <asp:ListItem Text="Customer"> </asp:ListItem>
                                    <asp:ListItem Text="Item"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <div class="div10pcelt">
                                &nbsp;</div>
                            <div class="div1pcelt">
                                &nbsp;</div>
                            <div class="invunkwn20">
                                Amount</div>
                            <div class="invunkwn20">
                                <asp:TextBox runat="server" ID="txtDisAmount" CssClass="TextBox" Text="0.00" Width="100%"></asp:TextBox>
                            </div>
                        </div>
                        <div class="invunkwn5">
                            &nbsp;</div>
                        <div class="invunkwn5">
                            <div class="invunkwn5">
                                <asp:Panel runat="server" ID="pnlDisItem" ScrollBars="Auto" Height="200px">
                                    <asp:GridView ID="gvDisItem" runat="server" AutoGenerateColumns="False" CssClass="GridView"
                                        CellPadding="3" ForeColor="#333333" GridLines="Both" ShowHeaderWhenEmpty="true"
                                        Width="500px">
                                        <EditRowStyle BackColor="#2461BF" />
                                        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                        <HeaderStyle BackColor="#507CD1" ForeColor="White" Font-Bold="True" />
                                        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                        <RowStyle BackColor="#EFF3FB" />
                                        <AlternatingRowStyle BackColor="White" />
                                        <EmptyDataTemplate>
                                            <div style="width: 100%; text-align: center;">
                                                No data found
                                            </div>
                                        </EmptyDataTemplate>
                                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                        <SortedAscendingCellStyle BackColor="#F5F7FB" />
                                        <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                                        <SortedDescendingCellStyle BackColor="#E9EBEF" />
                                        <SortedDescendingHeaderStyle BackColor="#4870BE" />
                                        <Columns>
                                            <asp:TemplateField ItemStyle-Width="20px" HeaderStyle-Width="20px">
                                                <ItemTemplate>
                                                    <asp:CheckBox runat="server" ID="chkDisSelect" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField='Sgdd_itm' HeaderText='Item' ItemStyle-Width="80px" HeaderStyle-Width="80px" />
                                            <asp:TemplateField HeaderText="Type" ItemStyle-Width="50px" HeaderStyle-Width="50px">
                                                <ItemTemplate>
                                                    <asp:DropDownList runat="server" ID="ddlDisType" Style="font-family: Verdana; font: 11px;">
                                                        <asp:ListItem Text="Rate"></asp:ListItem>
                                                        <asp:ListItem Text="Amount"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Amount" ItemStyle-Width="20px" HeaderStyle-Width="20px">
                                                <ItemTemplate>
                                                    <asp:TextBox runat="server" ID="txtDisItmAmount" Text="0.00" Style="text-align: right;
                                                        font-family: Verdana; font: 11px; width: 80%;"></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField='Sgdd_pb' HeaderText='Book' ItemStyle-Width="50px" HeaderStyle-Width="50px" />
                                            <asp:BoundField DataField='Sgdd_pb_lvl' HeaderText='Level' ItemStyle-Width="50px"
                                                HeaderStyle-Width="50px" />
                                        </Columns>
                                    </asp:GridView>
                                </asp:Panel>
                            </div>
                        </div>
                        <div class="invunkwn5">
                            <asp:Button runat="server" ID="btnDisConfirm" CssClass="button" Text="Confirm" OnClick="SaveDiscountRequest" />
                        </div>
                    </div>
                </asp:Panel>
            </div>
            <%-- Modal Popup Scaned Serials --%>
            <div>
                <asp:ModalPopupExtender ID="MPESerial" RepositionMode="RepositionOnWindowScroll"
                    BehaviorID="Serial" TargetControlID="btnSerial" runat="server" ClientIDMode="Static"
                    PopupControlID="pnlSerial" BackgroundCssClass="modalBackground" CancelControlID="imgBtnSerClose"
                    PopupDragHandleControlID="divSerialHeader">
                </asp:ModalPopupExtender>
                <asp:Panel ID="pnlSerial" runat="server" Height="200px" Width="955px" CssClass="ModalWindow">
                    <div class="popUpHeader" id="divSerialHeader" runat="server" style="height: 16px;">
                        <div class="div80pcelt" runat="server">
                            Invoice Item Serials</div>
                        <div class="invunkwn61">
                            <asp:ImageButton ID="imgBtnSerClose" runat="server" ImageUrl="~/Images/0032-01-16x16x16.ICO" />&nbsp;</div>
                    </div>
                    <%-- PopUp Message Area --%>
                    <div class="invunkwn58">
                        <asp:Label ID="lblSerMsg" runat="server" Text="" Width="100%"></asp:Label>
                    </div>
                    <asp:Panel runat="server" ID="pnlPopSerialgv" ScrollBars="Auto" Width="950px" Height="165px"
                        BorderStyle="Solid" BorderColor="Black" BorderWidth="1px">
                        <asp:GridView runat="server" ID="gvPopSerial" AutoGenerateColumns="false" CssClass="GridView"
                            CellPadding="3" ForeColor="#333333" GridLines="Both" ShowHeaderWhenEmpty="true"
                            OnRowDeleting="OnRemoveFromInvoiceItemSerialGrid" DataKeyNames="Tus_itm_cd,Tus_serial_id,Tus_unit_price,Tus_base_itm_line,Tus_new_status,Tus_ser_1">
                            <AlternatingRowStyle BackColor="White" />
                            <EmptyDataTemplate>
                                <div class="invunkwn66">
                                    No data found
                                </div>
                            </EmptyDataTemplate>
                            <Columns>
                                <asp:BoundField DataField='tus_itm_cd' HeaderText='Item'>
                                    <HeaderStyle HorizontalAlign="Left" Width="140px" />
                                </asp:BoundField>
                                <asp:BoundField DataField='tus_itm_model' HeaderText='Model'>
                                    <HeaderStyle HorizontalAlign="Left" Width="95px" />
                                </asp:BoundField>
                                <asp:BoundField DataField='tus_itm_stus' HeaderText='Status'>
                                    <HeaderStyle HorizontalAlign="Left" Width="75px" />
                                </asp:BoundField>
                                <asp:BoundField DataField='tus_qty' HeaderText='Qty'>
                                    <HeaderStyle HorizontalAlign="Right" Width="80px" />
                                </asp:BoundField>
                                <asp:BoundField DataField='tus_ser_1' HeaderText='Serial 1'>
                                    <HeaderStyle HorizontalAlign="Left" Width="155px" />
                                </asp:BoundField>
                                <asp:BoundField DataField='tus_ser_2' HeaderText='Serial 2'>
                                    <HeaderStyle HorizontalAlign="Left" Width="155px" />
                                </asp:BoundField>
                                <asp:BoundField DataField='tus_warr_no' HeaderText='Warranty'>
                                    <HeaderStyle HorizontalAlign="Left" Width="155px" />
                                </asp:BoundField>
                                <asp:TemplateField HeaderStyle-Width="11px">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="imgBtnRemoveSerial" runat="server" ImageAlign="Middle" ImageUrl="~/Images/Delete.png"
                                            Width="11px" Height="11px" OnClientClick="return confirm('Do you want to delete?');"
                                            CommandName="Delete" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <%-- <asp:BoundField DataField='tus_base_itm_line' HeaderText='tus_base_itm_line'>
                                    <HeaderStyle HorizontalAlign="Left" Width="155px" />
                                </asp:BoundField>
                                <asp:BoundField DataField='Tus_serial_id' HeaderText='Tus_serial_id'>
                                    <HeaderStyle HorizontalAlign="Left" Width="155px" />
                                </asp:BoundField>
                                <asp:BoundField DataField='Tus_new_status' HeaderText='Tus_new_status'>
                                    <HeaderStyle HorizontalAlign="Left" Width="155px" />
                                </asp:BoundField>--%>
                            </Columns>
                            <EditRowStyle BackColor="#2461BF" />
                            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                            <RowStyle BackColor="#EFF3FB" />
                            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                            <SortedAscendingCellStyle BackColor="#F5F7FB" />
                            <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                            <SortedDescendingCellStyle BackColor="#E9EBEF" />
                            <SortedDescendingHeaderStyle BackColor="#4870BE" />
                        </asp:GridView>
                    </asp:Panel>
                </asp:Panel>
            </div>
            <%-- Modal Popup Scaned Serials - Com Code Components --%>
            <div>
                <asp:ModalPopupExtender ID="MPEComItemSerial" RepositionMode="RepositionOnWindowScroll"
                    BehaviorID="ComItemSerial" TargetControlID="btnComItemSerial" runat="server"
                    ClientIDMode="Static" PopupControlID="pnlComItemSerial" BackgroundCssClass="modalBackground"
                    CancelControlID="imgBtnComItemSerClose" PopupDragHandleControlID="divComItemSerialHeader">
                </asp:ModalPopupExtender>
                <asp:Panel runat="server" ID="pnlComItemSerial" Height="300px" Width="500px" CssClass="ModalWindow">
                    <asp:HiddenField ID="hdnComItemSerQty" runat="server" Value="0" ClientIDMode="Static" />
                    <div class="popUpHeader" id="divComItemSerialHeader" runat="server">
                        <div class="div80pcelt" runat="server">
                            Combine Item Serial Pick
                        </div>
                    </div>
                    <%-- PopUp Message Area --%>
                    <div class="invunkwn58">
                        <asp:Label ID="lblComItemSerMsg" runat="server" Text="" Width="100%"></asp:Label>
                        &nbsp;
                        <asp:Button runat="server" ID="btnPopComItemSerConfirm" CssClass="Button" Text="Confirm"
                            OnClick="ConfirmComItemSerialWithQty" />
                        &nbsp;
                        <asp:Button runat="server" ID="btnPopComItemSerCancel" CssClass="Button" Text="Cancel"
                            OnClick="ConfirmComItemSerialCancel" />
                    </div>
                    <div class="invunkwn5">
                        <asp:Panel runat="server" ID="pnlComItemgv" ScrollBars="Auto" class="invunkwn67">
                            <asp:GridView runat="server" ID="gvPopComItem" AutoGenerateColumns="false" CssClass="GridView"
                                CellPadding="3" ForeColor="#333333" GridLines="Both" ShowHeaderWhenEmpty="true"
                                DataKeyNames="Micp_itm_cd,Micp_qty" OnSelectedIndexChanged="LoadSelectedItemSerialForComItemSerialGv"
                                OnRowDataBound="gvComItemSerialBind">
                                <EditRowStyle BackColor="#2461BF" />
                                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                <RowStyle BackColor="#EFF3FB" />
                                <AlternatingRowStyle BackColor="White" />
                                <Columns>
                                    <asp:TemplateField HeaderText="ItemCode" HeaderStyle-HorizontalAlign="Left" ShowHeader="true">
                                        <ItemTemplate>
                                            <asp:Label ID="lblPopComItemSerItemCode" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "ComponentItem.Mi_cd") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" Width="120px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Description" HeaderStyle-HorizontalAlign="Left" ShowHeader="true">
                                        <ItemTemplate>
                                            <asp:Label ID="lblPopComItemSerDesc" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "ComponentItem.Mi_longdesc") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" Width="280px" />
                                    </asp:TemplateField>
                                    <asp:BoundField DataField='Micp_itm_cd' HeaderText='Status' />
                                    <asp:BoundField DataField='Micp_qty' HeaderText='Qty' />
                                </Columns>
                                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                <SortedAscendingCellStyle BackColor="#F5F7FB" />
                                <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                                <SortedDescendingCellStyle BackColor="#E9EBEF" />
                                <SortedDescendingHeaderStyle BackColor="#4870BE" />
                            </asp:GridView>
                        </asp:Panel>
                        <div class="invunkwn68">
                            <div class="invunkwn69">
                                &nbsp;
                                <asp:Label ID="lblPopComItemSerSerMsg" runat="server" Text="" Width="80%"></asp:Label>
                            </div>
                            <div class="invunkwn70">
                                <asp:Button runat="server" ID="btnPopComItemSerAdd" CssClass="Button" Text="Add Serial"
                                    OnClick="AddComItemSerialToList" />
                            </div>
                        </div>
                        <asp:Panel runat="server" ID="pnlComItemSerialgv" class="invunkwn71">
                            <asp:GridView runat="server" ID="gvPopComItemSerial" AutoGenerateColumns="false"
                                CssClass="GridView" CellPadding="3" ForeColor="#333333" GridLines="Both" ShowHeaderWhenEmpty="true"
                                OnRowDataBound="gvComItemSerialWithSerialOnBind">
                                <EditRowStyle BackColor="#2461BF" />
                                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                <RowStyle BackColor="#EFF3FB" />
                                <AlternatingRowStyle BackColor="White" />
                                <Columns>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkPopComItemSerSelect" runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField='Tus_itm_cd' HeaderText='Item' />
                                    <asp:BoundField DataField="Tus_ser_1" HeaderText="Serial No 1" />
                                    <asp:BoundField DataField="Tus_ser_2" HeaderText="Serial No 2" />
                                    <asp:BoundField DataField="tus_warr_no" HeaderText="Warranty" />
                                    <asp:BoundField DataField="Tus_itm_stus" HeaderText="Status" />
                                    <asp:BoundField DataField="Tus_ser_id" HeaderText="Serial ID" />
                                </Columns>
                                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                <SortedAscendingCellStyle BackColor="#F5F7FB" />
                                <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                                <SortedDescendingCellStyle BackColor="#E9EBEF" />
                                <SortedDescendingHeaderStyle BackColor="#4870BE" />
                            </asp:GridView>
                        </asp:Panel>
                    </div>
                </asp:Panel>
            </div>
            <%-- Modal Popup Scaned Serials - Com Code Component (Multiple Com Item) --%>
            <div>
                <asp:ModalPopupExtender ID="MPEMultiCom" RepositionMode="RepositionOnWindowScroll"
                    BehaviorID="ComItemMulti" TargetControlID="btnComItemMulti" runat="server" ClientIDMode="Static"
                    PopupControlID="pnlComItemMulti" BackgroundCssClass="modalBackground" CancelControlID="imgBtnComItemMultiClose"
                    PopupDragHandleControlID="divComItemMultiHeader">
                </asp:ModalPopupExtender>
                <asp:Panel runat="server" ID="pnlComItemMulti" Height="300px" Width="500px" CssClass="ModalWindow">
                    <div class="popUpHeader" id="divComItemMultiHeader" runat="server">
                        <div class="div80pcelt">
                            Pick an item
                        </div>
                    </div>
                    <div class="invunkwn5">
                        <asp:Panel runat="server" ID="Panel3" ScrollBars="Auto" class="invunkwn72">
                            <div runat="server" id="divMultiResult">
                            </div>
                        </asp:Panel>
                    </div>
                </asp:Panel>
            </div>
            <%-- Modal Popup Scaned Serials - Invoice Item Detail Pick Serial --%>
            <div>
                <asp:ModalPopupExtender ID="MPEItemSerialItm" RepositionMode="RepositionOnWindowScroll"
                    BehaviorID="ItemSerialItm" TargetControlID="btnItemSerialItm" runat="server"
                    ClientIDMode="Static" PopupControlID="pnlItemSerialItm" BackgroundCssClass="modalBackground"
                    CancelControlID="imgBtnItemSerItmClose" PopupDragHandleControlID="divComItemSerialItmHeader">
                </asp:ModalPopupExtender>
                <asp:Panel runat="server" ID="pnlItemSerialItm" Height="300px" Width="700px" CssClass="ModalWindow">
                    <div class="popUpHeader" id="divComItemSerialItmHeader" runat="server">
                        <div class="div80pcelt" runat="server">
                            Item wise Serial Pick
                        </div>
                        <div class="invunkwn61">
                            <asp:ImageButton ID="imgBtnItemSerItmClose" runat="server" ImageUrl="~/Images/0032-01-16x16x16.ICO" />&nbsp;</div>
                    </div>
                    <%-- PopUp Message Area --%>
                    <div class="invunkwn58">
                        <asp:Label ID="lblSerItemSerMsg" runat="server" Text="" Width="100%"></asp:Label>
                    </div>
                    <div class="invunkwn5">
                        <asp:Panel runat="server" ID="pnlSerialInvItem" class="invunkwn73">
                            <asp:GridView runat="server" ID="gvPickItemSerial" AutoGenerateColumns="false" CssClass="GridView"
                                CellPadding="3" ForeColor="#333333" GridLines="Both" ShowHeaderWhenEmpty="true">
                                <EditRowStyle BackColor="#2461BF" />
                                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                <RowStyle BackColor="#EFF3FB" />
                                <AlternatingRowStyle BackColor="White" />
                                <Columns>
                                    <%-- <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkPickItemSerSelect" runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>--%>
                                    <asp:BoundField DataField='Tus_itm_cd' HeaderText='Item' />
                                    <asp:BoundField DataField="Tus_ser_1" HeaderText="Serial No 1" />
                                    <asp:BoundField DataField="Tus_ser_2" HeaderText="Serial No 2" />
                                    <asp:BoundField DataField="tus_warr_no" HeaderText="Warranty" />
                                    <asp:BoundField DataField="Tus_itm_stus" HeaderText="Status" />
                                    <asp:BoundField DataField="Tus_ser_id" HeaderText="Serial ID" />
                                </Columns>
                                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                <SortedAscendingCellStyle BackColor="#F5F7FB" />
                                <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                                <SortedDescendingCellStyle BackColor="#E9EBEF" />
                                <SortedDescendingHeaderStyle BackColor="#4870BE" />
                            </asp:GridView>
                        </asp:Panel>
                    </div>
                </asp:Panel>
            </div>
            <%-- Modal Popup Serial Scan for Quotation --%>
            <div>
                <asp:ModalPopupExtender ID="ModalPopupExtItem" runat="server" CancelControlID="btnimgCancel"
                    PopupControlID="PanelItemPopUp" TargetControlID="btnHidden_popup" ClientIDMode="Static">
                </asp:ModalPopupExtender>
                <asp:Panel ID="PanelItemPopUp" runat="server" Height="320px" Width="642px" BackColor="#A7C2DA"
                    BorderColor="#3333FF" BorderWidth="2px">
                    <div style="float: left; width: 100%; height: 22px; text-align: right; padding-top: 2px">
                        <div style="float: left; width: 2%; height: 22px; text-align: left;">
                        </div>
                        <div id="divPopupImg" runat="server" visible="false" style="float: left; width: 3%;
                            height: 22px; text-align: left;">
                            <asp:Image ID="Image5" runat="server" ImageUrl="~/Images/warning.gif" Width="15px"
                                Height="15px" />
                        </div>
                        <div style="float: left; width: 65%; height: 22px; text-align: left;">
                            <asp:Label ID="lblpopupMsg" runat="server" Width="100%" ForeColor="Red" />
                        </div>
                        <div style="float: left; width: 30%; height: 22px; text-align: right;">
                            <asp:ImageButton ID="btnimgAdd" runat="server" ImageUrl="~/Images/approve_img.png"
                                ImageAlign="Middle" OnClick="btnPopupOk_Click" Visible="true" Width="20px" Height="20px" />
                            &nbsp;
                            <asp:ImageButton ID="btnimgCancel" runat="server" ImageUrl="~/Images/error_icon.png"
                                ImageAlign="Middle" OnClick="btnPopupCancel_Click" Visible="true" Width="22px"
                                Height="22px" />
                            &nbsp;
                        </div>
                    </div>
                    <div style="text-align: right">
                        <asp:HiddenField ID="hdnInvoiceNo" runat="server" />
                        <asp:HiddenField ID="hdnInvoiceLineNo" runat="server" />
                        <asp:Label ID="lblPopupAmt" runat="server" Style="text-align: right"></asp:Label>&nbsp;
                    </div>
                    <div style="float: right; width: 100%; height: 22px; text-align: left; padding-top: 2px;
                        padding-bottom: 2px">
                        Item Code:
                        <asp:Label ID="lblPopupItemCode" runat="server" Font-Bold="True"></asp:Label>
                        <asp:Label ID="lblPopupBinCode" runat="server" Font-Bold="True"></asp:Label>
                    </div>
                    <div style="float: left; width: 100%; text-align: left;">
                        <div id="divSerialSelect" runat="server" style="float: left; width: 100%; text-align: left;">
                            <div style="float: left; width: 3%; padding-top: 2px; padding-bottom: 3px">
                            </div>
                            <div style="float: left; width: 15%;">
                                Search by :
                            </div>
                            <div style="float: left; width: 14%;">
                                <asp:DropDownList ID="ddlPopupSerial" runat="server" Width="85%" CssClass="ComboBox">
                                    <asp:ListItem>Serial 1</asp:ListItem>
                                    <asp:ListItem>Serial 2</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <div style="float: left; width: 15%;">
                                <asp:TextBox ID="txtPopupSearchSer" runat="server" CssClass="TextBox" Width="100%"></asp:TextBox>
                            </div>
                            <div style="float: left; width: 11%;">
                                &nbsp;
                                <asp:Button ID="btnPopupSarch" runat="server" CssClass="Button" OnClick="btnPopupSarch_Click"
                                    Text="Search" />
                            </div>
                        </div>
                        <div id="divQtySelect" runat="server" visible="false" style="float: left; width: 100%;
                            text-align: left; padding-top: 2px; padding-bottom: 3px">
                            <div style="float: left; width: 3%;">
                            </div>
                            <div style="float: left; width: 15%; text-align: left;">
                                <asp:Label ID="lblPopQty" runat="server" Text="Qty:" Visible="False"></asp:Label>
                            </div>
                            <div style="float: left; width: 29%; text-align: left;">
                                <asp:TextBox ID="txtPopupQty" runat="server" CssClass="TextBox" Visible="False" Width="100%"
                                    ClientIDMode="Static"></asp:TextBox>
                            </div>
                            <div style="float: left; width: 30%; text-align: left;">
                                &nbsp;
                                <asp:Button ID="btnPopupAutoSelect" runat="server" CssClass="Button" OnClick="btnPopupAutoSelect_Click"
                                    OnClientClick="SelectAuto()" Text="Auto Select" visble="false" />
                            </div>
                        </div>
                        <div style="float: left; width: 100%; text-align: left; padding-top: 1px; padding-bottom: 2px">
                            <div style="float: left; width: 3%;">
                            </div>
                            <div style="float: left; width: 15%; text-align: left;">
                                Invoice Qty :
                            </div>
                            <div style="float: left; width: 15%; text-align: left;">
                                <asp:Label ID="lblInvoiceQty" runat="server" CssClass="TextBox" Width="85%"></asp:Label>
                            </div>
                        </div>
                        <div style="float: left; width: 100%; text-align: left; padding-top: 1px; padding-bottom: 2px">
                            <div style="float: left; width: 3%;">
                            </div>
                            <div style="float: left; width: 15%; text-align: left;">
                                Delivered Qty :
                            </div>
                            <div style="float: left; width: 15%; text-align: left;">
                                <asp:Label ID="lblDeliveredQty" runat="server" CssClass="TextBox" Width="85%"></asp:Label>
                            </div>
                        </div>
                        <div style="float: left; width: 100%; text-align: left; padding-top: 1px; padding-bottom: 2px">
                            <div style="float: left; width: 3%;">
                            </div>
                            <div style="float: left; width: 15%; text-align: left;">
                                Scan Qty :
                            </div>
                            <div style="float: left; width: 15%; text-align: left;">
                                <asp:Label ID="lblScanQty" runat="server" CssClass="TextBox" Width="85%"></asp:Label>
                            </div>
                        </div>
                    </div>
                    <div style="width: 608px">
                        <asp:Panel ID="popupGridPanel" runat="server" Height="172px" ScrollBars="Auto" Style="margin-left: 15px;
                            margin-bottom: 13px" Width="100%">
                            <asp:GridView ID="gvPopSerialPick" runat="server" AutoGenerateColumns="False" CellPadding="4"
                                Height="45px" Width="95%" CssClass="GridView" ShowHeaderWhenEmpty="True" EmptyDataText="No data found">
                                <AlternatingRowStyle BackColor="White" />
                                <Columns>
                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            <asp:CheckBox ID="chkPopupSelectAll" runat="server" ClientIDMode="Static" onclick="SelectAll(this.id)" />
                                        </HeaderTemplate>
                                        <EditItemTemplate>
                                            <asp:CheckBox ID="CheckBox2" runat="server" />
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:CheckBox ID="checkPopup" runat="server" ClientIDMode="Static" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="Tus_ser_1" HeaderText="Serial No 1" />
                                    <asp:BoundField DataField="Tus_ser_2" HeaderText="Serial No 2" />
                                    <asp:BoundField DataField="Tus_itm_stus" HeaderText="Current Status" />
                                    <asp:BoundField DataField="Tus_warr_no" HeaderText="Warrant #" />
                                    <asp:BoundField DataField="Tus_bin" HeaderText="Bin Code" />
                                    <asp:BoundField DataField="Tus_itm_desc" HeaderText="Description" />
                                    <asp:BoundField DataField="Tus_ser_id" HeaderText="Serial ID" />
                                </Columns>
                                <EditRowStyle BackColor="#2461BF" />
                                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                <RowStyle BackColor="#EFF3FB" />
                                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                <SortedAscendingCellStyle BackColor="#F5F7FB" />
                                <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                                <SortedDescendingCellStyle BackColor="#E9EBEF" />
                                <SortedDescendingHeaderStyle BackColor="#4870BE" />
                            </asp:GridView>
                        </asp:Panel>
                    </div>
                    <br />
                    &nbsp;
                </asp:Panel>
            </div>
            <%-- Control Area --%>
            <div style="display: none;">
                <asp:Button ID="btnPopUp" runat="server" ClientIDMode="Static" />
                <asp:Button ID="btnPopUpCombine" runat="server" ClientIDMode="Static" />
                <asp:Button ID="btnPopUpCombineItemQty" runat="server" ClientIDMode="Static" OnClick="CheckPopUpCombineItemQty" />
                <asp:Button ID="btnRefNo" runat="server" OnClick="CheckRefNo" />
                <asp:Button ID="btnInvoiceNo" runat="server" OnClick="CheckInvoiceNo" />
                <asp:Button ID="btnPriceLevel" runat="server" OnClick="CheckPriceLevel" />
                <asp:Button ID="btnUnitPrice" runat="server" OnClick="CheckUnitPrice" />
                <asp:Button ID="btnQty" runat="server" OnClick="CheckQty" />
                <asp:Button ID="btnDiscountRate" runat="server" OnClick="CheckDiscountRate" />
                <asp:Button ID="btnDiscountAmt" runat="server" OnClick="CheckDiscountAmount" />
                <asp:Button ID="btnVAT" runat="server" OnClick="CheckVAT" />
                <asp:Button ID="btnTotalAmt" runat="server" OnClick="CheckTotalAmt" />
                <asp:ImageButton ID="imgBtnItem" runat="server" ImageUrl="~/Images/icon_search.png"
                    ImageAlign="Middle" OnClick="imgBtnItem_Click" ClientIDMode="Static" />
                <asp:ImageButton ID="imgBtnStatus" runat="server" ImageUrl="~/Images/icon_search.png"
                    ImageAlign="Middle" OnClick="imgBtnStatus_Click" />
                <asp:Button ID="btnManual" runat="server" OnClick="IsValidManualNo" />
                <asp:Button ID="btnBank" runat="server" OnClick="CheckBank" />
                <asp:Button ID="btnRePay" runat="server" />
                <asp:Button ID="btnDisPnl" runat="server" />
                <asp:HiddenField ID="hdnBusinessEntityModalControler" runat="server" Value="0" ClientIDMode="Static" />
                <asp:Button ID="btnAdvCus" runat="server" />
                <asp:HiddenField ID="hdnAccountBalance" runat="server" Value="0.00" ClientIDMode="Static" />
                <asp:HiddenField ID="hdnAvailableCredit" runat="server" Value="0.00" ClientIDMode="Static" />
                <asp:HiddenField ID="hdnBySerialCheckStatus" runat="server" Value="false" ClientIDMode="Static" />
                <asp:HiddenField ID="hdnSerialNo" runat="server" Value="" ClientIDMode="Static" />
                <asp:Button ID="btnComItemSerial" runat="server" />
                <asp:Button ID="btnComItemMulti" runat="server" />
                <asp:ImageButton ID="imgBtnComItemSerClose" runat="server" />
                <asp:Button ID="imgBtnComItemMultiClose" runat="server" />
                <asp:HiddenField ID="hdnItemCode" runat="server" Value="" ClientIDMode="Static" />
                <asp:Button runat="server" ID="btnDefBtn" OnClientClick="return false;" />
                <asp:Button ID="btnItemSerialItm" runat="server" />
                <asp:Button ID="btnBusAdvDetailNo" runat="server" />
                <asp:HiddenField ID="hdnEditQty" runat="server" Value="" ClientIDMode="Static" />
                <asp:Button ID="btnEditQty" runat="server" OnClick="gvInvoiceItem_RowEditing" Text="0" />
                <asp:Button ID="btnHidden_popup" runat="server" />
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
