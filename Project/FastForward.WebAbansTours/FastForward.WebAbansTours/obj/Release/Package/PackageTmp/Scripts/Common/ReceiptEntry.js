jQuery(document).ready(function () {
    //loadPrefix();
    //loadDistrict();
    //loadDivision();
    //loadPayModesTypes(jQuery("#Sir_receipt_type").val());

    clearReciptForm()
    jQuery("input#CheckManSys").click(function () {
        loadPrefix();
    });
    if (jQuery("#Sir_receipt_type").val()=="ADVAN") {
        jQuery(".other-part-cls").show();
    }
    function loadPrefix() {
        if (typeof jQuery("input#CheckManSys:checked").val() != "undefined") {
            jQuery.ajax({
                type: "GET",
                url: "/ReceiptEntry/loadPrefixes",
                contentType: "application/json;charset=utf-8",
                data: { val: jQuery("input#CheckManSys:checked").val() },
                dataType: "json",
                success: function (result) {
                    if (result.login == true) {
                        if (result.success == true) {
                            var select = document.getElementById("Sir_prefix");
                            jQuery("#Sir_prefix").empty();
                            var options = [];
                            var option = document.createElement('option');
                            if (result.data != null && result.data.length != 0) {
                                for (var i = 0; i < result.data.length; i++) {
                                    option.text = result.data[i];
                                    option.value = result.data[i];
                                    options.push(option.outerHTML);
                                }
                            } else {
                                option.text = "-Select-";
                                option.value = "";
                                options.push(option.outerHTML);
                            }
                            select.insertAdjacentHTML('beforeEnd', options.join('\n'));
                        } else {
                            option.text = "-Select-";
                            option.value = "";
                            options.push(option.outerHTML);
                            if (typeof result.msg != "undefined")
                                setError(result.msg);
                        }
                    } else {
                        Logout();
                    }
                }

            });
        }
    }
   
    function loadDistrict() {
        jQuery.ajax({
            type: "GET",
            url: "/ReceiptEntry/loadDistrict",
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            success: function (result) {
                if (result.login == true) {
                    if (result.success == true) {
                        var select = document.getElementById("Sir_anal_1");
                        jQuery("#Sir_anal_1").empty();
                        var options = [];
                        var option = document.createElement('option');
                        if (result.data != null && result.data.length != 0) {
                            for (var i = 0; i < result.data.length; i++) {
                                option.text = result.data[i];
                                option.value = result.data[i];
                                options.push(option.outerHTML);
                            }
                        } else {
                            option.text = "-Select-";
                            option.value = "";
                            options.push(option.outerHTML);
                        }
                        select.insertAdjacentHTML('beforeEnd', options.join('\n'));
                        loadProvince(jQuery("#Sir_anal_1").val());
                    } else {
                        option.text = "-Select-";
                        option.value = "";
                        options.push(option.outerHTML);
                        if (typeof result.msg != "undefined")
                            setError(result.msg);
                    }
                } else {
                    Logout();
                }
            }

        });
    }
    jQuery("#Division").focusout(function () {
        if (jQuery(this).val() != "") {
            getDevision(jQuery(this).val());
        }
    });

    jQuery("#Division").on("keydown", function (evt) {
        if (evt.keyCode == 113) {
            var headerKeys = Array()
            headerKeys = ["Row", "Code", "Description"];
            field = "divisionSearch"
            var x = new CommonSearch(headerKeys, field);
        }
        if (evt.keyCode == 13) {
            recipTypeFocusOut(jQuery(this).val());
        }
    });
    jQuery(".division-search").click(function () {
        var headerKeys = Array()
        headerKeys = ["Row", "Code", "Description"];
        field = "divisionSearch"
        var x = new CommonSearch(headerKeys, field);
    });
    jQuery("#Sir_manual_ref_no").focusout(function () {
        refNumberFocusout(jQuery(this).val());
    });
    jQuery("#Sir_manual_ref_no").on("keydown", function (evt) {
        if (evt.keyCode == 113) {
            var headerKeys = Array();
            headerKeys = ["Row", "Enquiry", "Refference Num", "Customer Code", "Name", "Address"];
            field = "receLogEnqSearch";
            var x = new CommonSearch(headerKeys, field);
        }
        if (evt.keyCode == 13) {
            refNumberFocusout(jQuery(this).val());
        }
    })

    jQuery(".recpt-enq-search").click(function (){
        var headerKeys = Array();
        headerKeys = ["Row", "Enquiry", "Refference Num", "Customer Code", "Name", "Address"];
        field = "receLogEnqSearch";
        var x = new CommonSearch(headerKeys, field);
    });
    function getDevision(division) {
        jQuery.ajax({
            type: "GET",
            url: "/ReceiptEntry/validDivision",
            data:{division:division},
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            success: function (result) {
                if (result.login == true) {
                    if (result.success == false) {
                        jQuery("#Division").val("");
                        if (result.type == "Error") {
                            setError(result.msg);
                        }
                        if (result.type == "Info") {
                            setInfoMsg(result.msg);
                        }
                    } 

                } else {
                    Logout();
                }
            }
        });
    }
    function loadDivision() {
            jQuery.ajax({
                type: "GET",
                url: "/ReceiptEntry/getDivision",
                contentType: "application/json;charset=utf-8",
                dataType: "json",
                success: function (result) {
                    if (result.login == true) {
                        jQuery("#Division").val("");
                        if (result.success == true) {
                            jQuery("#Division").val(result.division);
                        } else {
                            if (result.type == "Error") {
                                setError(result.msg);
                            }
                            if (result.type == "Info") {
                                setInfoMsg(result.msg);
                            }
                        }

                    } else {
                        Logout();
                    }
                }
            });
    }


    jQuery("#Sir_anal_1").on("change", function () {
            loadProvince(jQuery(this).val());
    });
    function loadProvince(district) {
        if (district != "") {
            jQuery.ajax({
                type: "GET",
                url: "/ReceiptEntry/getProvince",
                data: { district: district },
                contentType: "application/json;charset=utf-8",
                dataType: "json",
                success: function (result) {
                    if (result.login == true) {
                        jQuery("#Sir_anal_2").val("");
                        if (result.success == true) {
                            jQuery("#Sir_anal_2").val(result.province);
                        } else {
                            if (result.type == "Error") {
                                setError(result.msg);
                            }
                            if (result.type == "Info") {
                                setInfoMsg(result.msg);
                            }
                        }

                    } else {
                        Logout();
                    }
                }
            });
        } else {
            jQuery("#Sir_anal_2").val("");
        }
    }

    jQuery('#Sir_mob_no').on('input', function (event) {
        this.value = this.value.replace(/[^0-9]/g, '');
    });
    //jQuery('#Sir_receipt_date').datepicker({ dateFormat: "dd/M/yy" });
    jQuery('#Sir_receipt_date').val(getFormatedDate(new Date()));
    jQuery('#ExpireOnDt').datepicker({ dateFormat: "dd/M/yy" });
    jQuery('#ExpireOnDt').val(getFormatedDate(new Date()));


    //check numbers and decimal  only
    jQuery('#CustPayment,#PageValue,#InvAmount,#TotalValue').on("input", function (event) {
        if (!jQuery.isNumeric(this.value)) {
            this.value = "";
        }
        if (parseFloat(this.value) < 0) {
            this.value = "";
        }

    });

    jQuery('#CustPayment,#PageValue,#InvAmount,#TotalValue').keypress(function (event) {
        if ((event.which != 46 || $(this).val().indexOf('.') != -1) &&
          ((event.which < 48 || event.which > 57) &&
            (event.which != 0 && event.which != 8))) {
            event.preventDefault();
        }

        var text = $(this).val();

        if ((text.indexOf('.') != -1) &&
          (text.substring(text.indexOf('.')).length > 2) &&
          (event.which != 0 && event.which != 8) &&
          ($(this)[0].selectionStart >= text.length - 2)) {
            event.preventDefault();
        }
    });


    jQuery("#Sir_receipt_type").focusout(function () {
        recipTypeFocusOut(jQuery(this).val());
    });
    jQuery("#Sir_receipt_type").on("keydown", function (evt) {
        if (evt.keyCode == 113) {
            var headerKeys = Array()
            headerKeys = ["Row", "Type","Description"];
            field = "receiptTYypeSearch"
            var x = new CommonSearch(headerKeys, field);
        }
        if (evt.keyCode == 13) {
            recipTypeFocusOut(jQuery(this).val());
        }
    })
    jQuery(".recpt-typ-search").click(function () {
        var headerKeys = Array()
        headerKeys = ["Row", "Type", "Description"];
        field = "receiptTYypeSearch"
        var x = new CommonSearch(headerKeys, field);
    });

    function recipTypeFocusOut(type) {
        if (type != "") {
            jQuery.ajax({
                type: "GET",
                url: "/ReceiptEntry/getReciptTypes",
                data: { type: type },
                contentType: "application/json;charset=utf-8",
                dataType: "json",
                success: function (result) {
                    if (result.login == true) {
                        if (result.success == false) {
                            jQuery("#Sir_receipt_type").val("");
                            jQuery(".debt-panel-reciept").hide();
                            jQuery(".search-unallow-recpt").hide();
                            jQuery(".gvisu-panel-reciept").hide();
                            if (result.type == "Error") {
                                setError(result.msg);
                            }
                            if (result.type == "Info") {
                                setInfoMsg(result.msg);
                            }
                        } else {
                            loadPayModesTypes(jQuery("#Sir_receipt_type").val());
                            if (type == "DEBT") {
                                jQuery(".gvisu-panel-reciept").hide();
                                jQuery(".debt-panel-reciept").show();
                                jQuery(".cust-pay-panel-reciept").show();
                                jQuery(".search-unallow-recpt").show();
                            } else if (type == "GVISU") {
                                jQuery(".gvisu-panel-reciept").show();
                                jQuery(".debt-panel-reciept").hide();
                                jQuery(".search-unallow-recpt").hide();
                                jQuery(".cust-pay-panel-reciept").hide();
                            } else {
                                jQuery(".gvisu-panel-reciept").hide();
                                jQuery(".debt-panel-reciept").hide();
                                jQuery(".search-unallow-recpt").hide();
                                jQuery(".cust-pay-panel-reciept").show();
                            }
                            if (jQuery("#Sir_receipt_type").val() == "ADVAN") {
                                jQuery(".other-part-cls").show();
                            } else {
                                jQuery(".other-part-cls").hide();
                            }
                        }

                    } else {
                        Logout();
                    }
                }
            });
        }
    }


    jQuery("#Sir_debtor_cd").focusout(function () {
        codeCusFocusOut(jQuery(this).val());
    });

    jQuery("#Sir_debtor_cd").on("keydown", function (evt) {
        if (evt.keyCode == 113) {
            var headerKeys = Array()
            headerKeys = ["Row", "Code", "Name", "Nic", "Mobile", "Br No"];
            field = "srchCusReceipt"
            var x = new CommonSearch(headerKeys, field);
        }
        if (evt.keyCode == 13) {
            codeCusFocusOut(jQuery(this).val());
        }
    })
    jQuery(".cust-recipt-search").click(function () {
        var headerKeys = Array()
        headerKeys = ["Row", "Code", "Name", "Nic", "Mobile", "Br No"];
        field = "srchCusReceipt"
        var x = new CommonSearch(headerKeys, field);
    });
    
    jQuery("#Sir_nic_no").focusout(function () {
        loadDataFromNic(jQuery(this).val());
    });
    function loadDataFromNic(nic) {
        if (nic != "") {
            jQuery.ajax({
                type: "GET",
                url: "/DataEntry/getDataCustomerFromNic",
                data: { nic: nic },
                contentType: "application/json;charset=utf-8",
                dataType: "json",
                success: function (result) {
                    if (result.login == true) {
                        if (result.success == true) {
                            if (typeof (result.local) != "undefined") {
                                jQuery("#Sir_debtor_cd").val(result.data.Mbe_cd);
                                jQuery("#Sir_debtor_name").val(result.data.Mbe_name);
                                jQuery("#Sir_debtor_add_1").val(result.data.Mbe_cr_add1);
                                jQuery("#Sir_debtor_add_2").val(result.data.Mbe_cr_add2);
                                jQuery("#Sir_nic_no").val(result.data.Mbe_nic);
                                jQuery("#Sir_mob_no").val(result.data.Mbe_mob);
                            }
                            if (typeof (result.group) != "undefined") {
                                jQuery("#Sir_debtor_cd").val(result.data.Mbg_cd);
                                jQuery("#Sir_debtor_name").val(result.data.Mbg_name);
                                jQuery("#Sir_debtor_add_1").val(result.data.Mbg_cr_add1);
                                jQuery("#Sir_debtor_add_2").val(result.data.Mbg_cr_add2);
                                jQuery("#Sir_nic_no").val(result.data.Mbg_nic);
                                jQuery("#Sir_mob_no").val(result.data.Mbg_mob);
                            }
                            if (typeof (result.group) == "undefined" && typeof (result.local) == "undefined") {
                                jQuery("#Sir_debtor_cd").val("");
                                jQuery("#Sir_debtor_name").val("");
                                jQuery("#Sir_debtor_add_1").val("");
                                jQuery("#Sir_debtor_add_2").val("");
                                jQuery("#Sir_nic_no").val("");
                                jQuery("#Sir_mob_no").val("");
                                setInfoMsg("Invalid customer NIC number.");
                            }
                        } else {
                            jQuery("#Sir_debtor_cd").val("");
                            jQuery("#Sir_debtor_name").val("");
                            jQuery("#Sir_debtor_add_1").val("");
                            jQuery("#Sir_debtor_add_2").val("");
                            jQuery("#Sir_nic_no").val("");
                            jQuery("#Sir_mob_no").val("");
                            if (result.type == "Info") {
                                setInfoMsg(result.msg);
                            }
                            if (result.type == "Error") {
                                setInfoMsg(result.msg);
                            }
                        }
                    } else {
                        Logout();
                    }
                }
            });
        } else {
            jQuery("#Mbe_dob").val("");
        }
    }

    jQuery("#InvoiceAdd").focusout(function () {
        var chk = "0";
        if (jQuery('#OthSr').is(":checked")) {
            chk = "1";
        }
        codeInvFocusOut(jQuery(this).val(), jQuery("#Sir_debtor_cd").val(), jQuery("#Sir_receipt_type").val(), jQuery("#Sir_receipt_date").val(), chk, jQuery("#OthSrVal").val());
    });

    jQuery("#InvoiceAdd").on("keydown", function (evt) {
        if (jQuery("#Sir_debtor_cd").val() != "") {
            var chk = "0";
            if (jQuery('#OthSr').is(":checked")) {
                chk = "1";
            }
            if (evt.keyCode == 113) {
                if (chk == "1" && jQuery("#OthSrVal").val() == "") {
                    setInfoMsg("Please select show room.");
                } else {
                    var headerKeys = Array()
                    headerKeys = ["Row", "Invoice", "Reference", "Balance"];
                    data = { srChk: chk, srkVal: jQuery("#OthSrVal").val(), cusCd: jQuery("#Sir_debtor_cd").val(), type: jQuery("#Sir_receipt_type").val() };
                    field = "invoceReceiptSrch"
                    var x = new CommonSearch(headerKeys, field, data);
                }
            }
            if (evt.keyCode == 13) {
                codeInvFocusOut(jQuery(this).val(), jQuery("#Sir_debtor_cd").val(), jQuery("#Sir_receipt_type").val(), jQuery("#Sir_receipt_date").val(), chk, jQuery("#OthSrVal").val());
            }
        } else {
            jQuery("#Sir_debtor_cd").focus();
            setInfoMsg("Please select customer")
        }
        
    })
    jQuery(".invoice-receipt-search").click(function () {
        var chk = "0";
        if (jQuery('#OthSr').is(":checked")) {
            chk = "1";
        }
        if (jQuery("#Sir_debtor_cd").val() != "") {
            if (chk == "1" && jQuery("#OthSrVal").val() == "") {
                setInfoMsg("Please select show room.");
            } else {
                var headerKeys = Array()
                headerKeys = ["Row", "Invoice", "Reference", "Balance"];
                data = { srChk: chk, srkVal: jQuery("#OthSrVal").val(), cusCd: jQuery("#Sir_debtor_cd").val(), type: jQuery("#Sir_receipt_type").val() };
                field = "invoceReceiptSrch"
                var x = new CommonSearch(headerKeys, field, data);
            }
        } else {
            jQuery("#Sir_debtor_cd").focus();
            setInfoMsg("Please select customer")
        }
    });

    function codeInvFocusOut(invNo,cusCd,type,date,chkOth,chkOthVal) {
        if (invNo != "" && cusCd != "" && type != "" && date != "") {
            if (chkOth == "1") {
                if (chkOthVal == "") {
                    setInfoMsg("Please select profit center.");
                }
            }
            jQuery.ajax({
                type: "GET",
                url: "/ReceiptEntry/getInvoiceDetails",
                data: { invNo: invNo, cusCd: cusCd, type: type, date: date, chkOth: chkOth, chkOthVal: chkOthVal },
                contentType: "application/json;charset=utf-8",
                dataType: "json",
                success: function (result) {
                    if (result.login == true) {
                        if (result.success == true) {
                            jQuery("#InvAmount").val(result.invAmnt);
                        } else {
                            jQuery("#InvAmount").val("");
                            jQuery("#InvoiceAdd").val("");
                            if (result.type == "Info") {
                                setInfoMsg(result.msg);
                            }
                            if (result.type == "Error") {
                                setInfoMsg(result.msg);
                            }
                        }
                    } else {
                        Logout();
                    }
                }

            });
        } else {
            if (invNo == "") {
                setInfoMsg("Please enter invoice number.");
            }else if(cusCd == ""){
                setInfoMsg("Please enter customer number.");
            }else if(type == ""){
                setInfoMsg("Please enter receipt type.");
            }else if(date == ""){
                setInfoMsg("Please enter date.");
            }
        }
    }

    jQuery("#OthSr").click(function () {
        jQuery("#InvoiceAdd").val("");
        jQuery("#InvAmount").val("");
        if (!jQuery('#OthSr').is(":checked")) {
            jQuery("#OthSrVal").val("");
        }
    });

    jQuery("#OthSrVal").focusout(function () {
        //profCenCusFocusOut(jQuery(this).val());
    });

    jQuery("#OthSrVal").on("keydown", function (evt) {
        var chk = false;
        if (jQuery('#OthSr').is(":checked")) {
            chk = true;
        }
        if (chk == true) {
            if (evt.keyCode == 113) {
                var headerKeys = Array()
                headerKeys = ["Row", "Code", "Description"];
                field = "profCenSrch"
                var x = new CommonSearch(headerKeys, field);
            }
            if (evt.keyCode == 13) {
                //profCenCusFocusOut(jQuery(this).val());
            }
        } else {
            setInfoMsg("Please check oth show room to add profit center.");
        }
        
    })
    jQuery(".other-sr-search").click(function () {
        var chk = false;
        if (jQuery('#OthSr').is(":checked")) {
            chk = true;
        }
        if (chk == true) {
            var headerKeys = Array()
            headerKeys = ["Row", "Code", "Description"];
            field = "profCenSrch"
            var x = new CommonSearch(headerKeys, field);
        } else {
            setInfoMsg("Please check oth show room to add profit center.");
        }
    });
    jQuery(".add-cost-data").on("click", function () {
        paymentValue = jQuery("#CustPayment").val();
        customer = jQuery("#Sir_debtor_cd").val();
        type = jQuery("#Sir_receipt_type").val();
        invAmount = jQuery("#InvAmount").val();
        invAmount = invAmount.replace(/\,/g, '');
        invNo = jQuery("#InvoiceAdd").val();
        validate = true;
        if (type == "DEBT") {
            if (invNo == "") {
                setInfoMsg("Please add invoice");
                validate = false;
            }
            if (invAmount == "" || parseFloat(invAmount) <= 0) {
                setInfoMsg("Invoice amount must be greater than zero");
                validate = false;
            } else {
                if (parseFloat(paymentValue) > parseFloat(invAmount)) {
                    setInfoMsg("Payment cannot exceed outstanding amount.");
                    validate = false;
                }
            }
        }
        if (paymentValue=="" || parseFloat(paymentValue) <= 0) {
            setInfoMsg("Please enter valid payment amount.");
            validate = false;
        }
        if (customer == "") {
            setInfoMsg("Select valid customer.");
            validate = false;
        }
        if (validate == true) {
            jQuery.ajax({
                type: "GET",
                url: "/ReceiptEntry/addSettlementDetails",
                data: { paymentValue: paymentValue, customer: customer, type: type, invAmount: invAmount, invNo: invNo },
                contentType: "application/json;charset=utf-8",
                dataType: "json",
                success: function (result) {
                    if (result.login == true) {
                        if (result.success == true) {
                            updatePayment(paymentValue,type);
                        } else {
                            if (result.type == "Info") {
                                setInfoMsg(result.msg);
                            }
                            if (result.type == "Error") {
                                setInfoMsg(result.msg);
                            }
                        }
                    } else {
                        Logout();
                    }
                }

            });
        }
        
    });
    function updatePayment(amount,type) {
        var payAmount = parseFloat(amount);
        var currentTot = (jQuery(".tot-amount-val").html() != "") ? parseFloat(jQuery(".tot-amount-val").html()) : 0;
        var finTot =parseFloat(currentTot )+ parseFloat(amount);
        if (type == "DEBT") {
            updateCurrencyAmount(parseFloat(finTot), jQuery("#Sir_debtor_cd").val(), jQuery("#InvoiceAdd").val());
        } else {
            updateCurrencyAmount(parseFloat(finTot), jQuery("#Sir_debtor_cd").val(), "");
        }
       
    }
    jQuery(".btn-Receipt-entry-clear-data").click(function () {
        Lobibox.confirm({
            msg: "Do you want to clear ?",
            callback: function ($this, type, ev) {
                if (type == "yes") {
                    clearReciptForm();
                }
            }
        });
    });
    function clearReciptForm() {
        jQuery(".Receipt-entry-cls .btn-print-receipt").hide();
        loadPrefix();
        loadDistrict();
        loadDivision();
        loadPayModesTypes(jQuery("#Sir_receipt_type").val());
        clearPaymentValues();
        clearSesstionValue();
        jQuery("#Sir_receipt_no").val("");
        jQuery("#CheckManSys").val("1");
        jQuery("#Sir_manual_ref_no").val("");
        jQuery('#Sir_receipt_date').val(getFormatedDate(new Date()));
        jQuery("#Sir_debtor_cd").val("");
        jQuery("#Sir_debtor_name").val("");
        jQuery("#Sir_debtor_add_1").val("");
        jQuery("#Sir_debtor_add_2").val("");
        jQuery("#Sir_nic_no").val("");
        jQuery("#Sir_mob_no").val("");
        jQuery("#VoucherCode").val("");
        jQuery("#VoucherBook").val("");
        jQuery("#PagesTo").val("");
        jQuery(".pages-from-label").empty();
        jQuery(".pages-from-label").html("--");
        jQuery(".num-issu-pages-label").empty();
        jQuery(".num-issu-pages-label").html("--");
        jQuery("#PageValue").val("");
        jQuery("#TotalValue").val("");
        jQuery('#ExpireOnDt').val(getFormatedDate(new Date()));
        jQuery('#IssueAsFoc').attr('checked', false);
        jQuery("#InvoiceAdd").val("");
        jQuery("#InvAmount").val("");
        jQuery('#OthSr').attr('checked', false);
        jQuery("#OthSrVal").val("");
        jQuery("#CustPayment").val("");
        jQuery(".giftvoc-listing-table .new-row").remove();
        jQuery(".payment-table .new-row").remove();
        jQuery("#Sir_remarks").val("");
        jQuery("#Sir_anal_4").val("");
        jQuery("#TotalAmount").val("");
        
        jQuery("#VoucherCode").removeAttr("disabled")
        jQuery("#VoucherBook").removeAttr("disabled")
        jQuery("#PagesTo").removeAttr("disabled");
        jQuery("#PageValue").removeAttr("disabled");
        jQuery("#TotalValue").removeAttr("disabled");
        jQuery('#ExpireOnDt').removeAttr("disabled");
        jQuery('#IssueAsFoc').removeAttr("disabled");
        jQuery(".gift-vouch-search").show();
        jQuery(".tot-amount-val").html("0");//2016.03.12
        jQuery(".btn-Receipt-entry-save-data").removeAttr("disabled");
        jQuery(".btn-Receipt-entry-save-data").show();
        jQuery('#OtherParty').prop('checked', false);
        jQuery("#Sir_oth_partycd").val("");
        jQuery("#Sir_oth_partyname").val("");
    }
    function clearSesstionValue() {

        jQuery.ajax({
            type: "GET",
            url: "/ReceiptEntry/clearValues",
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            success: function (result) {
                if (result.login == true) {
                    if (result.success == false) {
                        if (result.type == "Info") {
                            setInfoMsg(result.msg);
                        }
                        if (result.type == "Error") {
                            setInfoMsg(result.msg);
                        }
                    } 
                } else {
                    Logout();
                }
            }

        });
    }
    var first = true;
    jQuery(".add-cust-recipt-data").click(function () {
        jQuery(".customer-create-popup").dialog({
            height: 580,
            width: "75%",
            resizable: false,
            draggable: false,
            //closeOnEscape: true,
           // title: "Create Customer",
            modal: true,
            open: function (event, ui) {
                //$(event.target).parent().css('position', 'fixed');
                jQuery(event.target).parent().css('top', '50px');
                jQuery(event.target).parent().css('left', '10%');
                jQuery(".customer-create-popup").css('overflow-x', '-moz-hidden-unscrollable');
                if (first == true) {
                    dataEntry();
                    first = false;
                }
                jQuery(".customer-create-popup .btn-close-form").click(function () {
                    jQuery(".customer-create-popup").dialog('close');
                    clearCustomerData();
                });
            }
            //buttons: {
            //    //Close: function () {
            //    //    jQuery(this).dialog('close');
            //    //    clearCustomerData();
            //    //}
            //}
        });
    });
    var cuscd;
    var mob;
    var nic;
    var name;
    var add1;
    var add2;

    jQuery(".btn-save-data").click(function (event) {
            cuscd = jQuery("#customer-crte-frm #Mbe_cd").val();
            mob = jQuery("#customer-crte-frm #Mbe_mob").val();
            nic = jQuery("#customer-crte-frm #Mbe_nic").val();
            name = jQuery("#customer-crte-frm #Mbe_name").val();
            add1 = jQuery("#customer-crte-frm #Mbe_add1").val();
            add2 = jQuery("#customer-crte-frm #Mbe_add2").val();
            event.preventDefault();
            jQuery(this).attr("disabled", true);
            var formdata = jQuery("#customer-crte-frm").serialize();
            jQuery.ajax({
                type: 'POST',
                url: '/DataEntry/CustomerCreation',
                data: formdata,
                success: function (response) {
                    if (response.login == true) {
                        if (response.success == true) {
                            document.getElementById("customer-crte-frm").reset();
                            fieldEnable();
                            clearCustomerData();
                            setSuccesssMsg(response.msg);
                            jQuery(".btn-save-data").removeAttr("disabled");
                            jQuery(".customer-create-popup").dialog('close');
                            //jQuery("#Receipt-entry-crte-frm #Sir_debtor_cd").val(cuscd);
                            if (response.cusCd != "") {
                                jQuery("#Receipt-entry-crte-frm #Sir_debtor_cd").val(response.cusCd);
                            } else {
                                jQuery("#Receipt-entry-crte-frm #Sir_debtor_cd").val(cuscd);
                            }
                            jQuery("#Receipt-entry-crte-frm #Sir_debtor_name").val(name);
                            jQuery("#Receipt-entry-crte-frm #Sir_debtor_add_1").val(add1);
                            jQuery("#Receipt-entry-crte-frm #Sir_debtor_add_2").val(add2);
                            jQuery("#Receipt-entry-crte-frm #Sir_nic_no").val(nic);
                            jQuery("#Receipt-entry-crte-frm #Sir_mob_no").val(mob);

                        } else {
                            jQuery(".btn-save-data").removeAttr("disabled");
                            if (response.type == "Error") {
                                setError(response.msg);
                            } else if (response.type == "Info") {
                                setInfoMsg(response.msg);
                            }

                        }
                    } else {
                        Logout();
                    }

                }
            });
            return false;
        
    });


    jQuery("#VoucherCode").focusout(function () {
        voucherFocusOut(jQuery(this).val());
    });

    jQuery("#VoucherCode").on("keydown", function (evt) {
        if (evt.keyCode == 113) {
            var headerKeys = Array()
            headerKeys = ["Row", "Item","Description","Model"];
            field = "gftVouReceipt"
            var x = new CommonSearch(headerKeys, field);
        }
        if (evt.keyCode == 13) {
            voucherFocusOut(jQuery(this).val());
        }
    })
    jQuery(".gift-vouch-search").click(function () {
        var headerKeys = Array()
        headerKeys = ["Row", "Item", "Description", "Model"];
        field = "gftVouReceipt"
        var x = new CommonSearch(headerKeys, field);
    });

    function voucherFocusOut(vouNo) {
        if (vouNo != "") {
            jQuery.ajax({
                type: "GET",
                url: "/ReceiptEntry/getGiftVoucherDetails",
                data: { vouNo: vouNo },
                contentType: "application/json;charset=utf-8",
                dataType: "json",
                success: function (result) {
                    if (result.login == true) {
                        if (result.success == true) {
                            var select = document.getElementById("VoucherBook");
                            jQuery("#VoucherBook").empty();
                            var options = [];
                            var option = document.createElement('option');
                            if (result.success == true) {
                                if (result.data != null && result.data.length != 0) {
                                    for (var i = 0; i < result.data.length; i++) {
                                        option.text = result.data[i];
                                        option.value = result.data[i];
                                        options.push(option.outerHTML);
                                    }
                                    getGiftVoucherPagesTo(result.data[0]);
                                } else {
                                    option.text = "-Select-";
                                    option.value = "";
                                    options.push(option.outerHTML);
                                }
                                select.insertAdjacentHTML('beforeEnd', options.join('\n'));
                            } else {
                                option.text = "-Select-";
                                option.value = "";
                                options.push(option.outerHTML);
                                select.insertAdjacentHTML('beforeEnd', options.join('\n'));
                            }
                        } else {
                            if (result.type == "Error") {
                                setError(result.msg);
                            }
                            if (result.type == "Info") {
                                setInfoMsg(result.msg);
                            }
                        }

                    } else {
                        Logout();
                    }
                }
            });
        }

    }
    jQuery("#VoucherBook").on("change", function () {
        getGiftVoucherPagesTo(jQuery(this).val());
    });

    function getGiftVoucherPagesTo(book) {
        if (book != "" && jQuery("#VoucherCode").val() != "") {
            jQuery.ajax({
                type: "GET",
                url: "/ReceiptEntry/getGiftVoucherPagesTo",
                data: { vouBook: book, vouNo: jQuery("#VoucherCode").val() },
                contentType: "application/json;charset=utf-8",
                dataType: "json",
                success: function (result) {
                    if (result.login == true) {
                        if (result.success == true) {
                            var select = document.getElementById("PagesTo");
                            jQuery("#PagesTo").empty();
                            var options = [];
                            var option = document.createElement('option');
                            if (result.success == true) {
                                if (result.data != null && result.data.length != 0) {
                                    for (var i = 0; i < result.data.length; i++) {
                                        option.text = result.data[i].Gvp_page;
                                        option.value = result.data[i].Gvp_page;
                                        options.push(option.outerHTML);
                                    }
                                    jQuery(".pages-from-label").empty();
                                    jQuery(".pages-from-label").html(result.fromPg);
                                    getIssuPages(result.data[0].Gvp_page);
                                } else {
                                    option.text = "-Select-";
                                    option.value = "";
                                    options.push(option.outerHTML);
                                }
                                select.insertAdjacentHTML('beforeEnd', options.join('\n'));
                            } else {
                                option.text = "-Select-";
                                option.value = "";
                                options.push(option.outerHTML);
                                select.insertAdjacentHTML('beforeEnd', options.join('\n'));
                                jQuery(".pages-from-label").empty();
                                jQuery(".pages-from-label").html("--");
                            }
                        } else {
                            jQuery(".pages-from-label").empty();
                            jQuery(".pages-from-label").html("--");
                            option.text = "-Select-";
                            option.value = "";
                            options.push(option.outerHTML);
                            select.insertAdjacentHTML('beforeEnd', options.join('\n'));
                            if (result.type == "Error") {
                                setError(result.msg);
                            }
                            if (result.type == "Info") {
                                setInfoMsg(result.msg);
                            }
                        }

                    } else {
                        Logout();
                    }
                }

            });
        } else {
            if (jQuery("#VoucherCode").val() == "") {
                setInfoMsg("Please enter voucher code.")
            }
            else if (book == "") {
                setInfoMsg("Invalid book number.")
            }
        }
    }

    jQuery("#PagesTo").on("change", function () {
        getIssuPages(jQuery(this).val());
    });

    function getIssuPages(pagesto) {
        if (pagesto != "" && jQuery("#VoucherCode").val() != "" && jQuery("#VoucherBook").val() != "" && jQuery(".pages-from-label").html() != "" || jQuery(".pages-from-label").html() != "--") {
            jQuery.ajax({
                type: "GET",
                url: "/ReceiptEntry/getIssuPages",
                data: { pagesto: pagesto, vouNo: jQuery("#VoucherCode").val(), vouBook: jQuery("#VoucherBook").val(), frmPg: jQuery(".pages-from-label").html() },
                contentType: "application/json;charset=utf-8",
                dataType: "json",
                success: function (result) {
                    if (result.login == true) {
                        if (result.success == true) {
                                if (result.pageCnt != null) {
                                    jQuery(".num-issu-pages-label").empty();
                                    jQuery(".num-issu-pages-label").html(result.pageCnt);
                                } else {
                                    jQuery(".num-issu-pages-label").empty();
                                    jQuery(".num-issu-pages-label").html("--");
                                }
                        } else {
                            if (result.type == "Error") {
                                setError(result.msg);
                            }
                            if (result.type == "Info") {
                                setInfoMsg(result.msg);
                            }
                        }
                    } else {
                        Logout();
                    }
                }

            });
        } else {
            if (jQuery("#VoucherCode").val() == "") {
                setInfoMsg("Please enter voucher code.")
            }
            else if (pagesto == "") {
                setInfoMsg("Invalid pages to number.")
            } else if (jQuery("#VoucherBook").val() != "") {
                setInfoMsg("Invalid voucher book number.");
            } else if (jQuery(".pages-from-label").html() != "" || jQuery(".pages-from-label").html() != "--") {
                setInfoMsg("Invalid from page number.")
            }
        }
    }
    jQuery("#PageValue").on("input", function () {
        if (jQuery(".num-issu-pages-label").html() != "" || jQuery(".num-issu-pages-label").html() != "--") {
            if (jQuery.isNumeric(jQuery(".num-issu-pages-label").html())) {
                if (jQuery.isNumeric(jQuery("#PageValue").val())) {

                    if (parseFloat(jQuery("#PageValue").val()) > 0) {
                        jQuery("#PageValue").val(parseFloat(jQuery("#PageValue").val()));
                        jQuery("#TotalValue").val(parseFloat(parseFloat(jQuery("#PageValue").val()) * parseFloat(jQuery(".num-issu-pages-label").html())));
                    } else {
                        setInfoMsg("Invalid amount.");
                        jQuery("#PageValue").val("");
                        jQuery("#TotalValue").val("");
                    }
                } else {
                    jQuery("#PageValue").val("");
                    jQuery("#TotalValue").val("");
                    setInfoMsg("Invalid amount.");
                }
            } else {
                jQuery("#PageValue").val("");
                jQuery("#TotalValue").val("");
                setInfoMsg("Pages not select properly.");
            }
        } else {
            jQuery("#PageValue").val("");
            jQuery("#TotalValue").val("");
            setInfoMsg("Pages not select properly.");
        }
    });

    jQuery(".add-voucher-data").click(function () {
        if (jQuery(".num-issu-pages-label").html() != "" || jQuery(".num-issu-pages-label").html() != "--") {
            if (jQuery.isNumeric(jQuery(".num-issu-pages-label").html())) {
                if (jQuery.isNumeric(jQuery("#PageValue").val())) {
                    if (parseFloat(jQuery("#PageValue").val()) > 0) {
                        var formdata = jQuery("#Receipt-entry-crte-frm").serialize();
                        var pagesto = jQuery("#PagesTo").val();
                        var vouNo = jQuery("#VoucherCode").val();
                        var vouBook = jQuery("#VoucherBook").val();
                        var frmPg = jQuery(".pages-from-label").html();
                        var pages = jQuery(".num-issu-pages-label").html();
                        var pgAmt = jQuery("#PageValue").val();
                        var ExpireOnDt = jQuery("#ExpireOnDt").val();
                        jQuery.ajax({
                            type: "GET",
                            url: "/ReceiptEntry/addGiftVouchers?" + "vouBook=" + vouBook + "&vouCd=" + vouNo + "&frmPg=" + frmPg + "&toPg=" + pagesto + "&pgAmt=" + pgAmt + "&ExpireOnDt=" + ExpireOnDt + "&pages=" + pages,
                            data:formdata,
                            contentType: "application/json;charset=utf-8",
                            dataType: "json",
                            success: function (result) {
                                if (result.login == true) {
                                    jQuery('table.giftvoc-listing-table .new-row').remove();
                                    if (result.success == true) {
                                        console.log(result.data);
                                        for (i = 0; i < result.data.length; i++) {
                                            jQuery('table.giftvoc-listing-table').append('<tr class="new-row"><td>'
                                                + result.data[i].Gvp_gv_cd + '</td><td>'
                                                + result.data[i].Gvp_book + '</td><td>'
                                                + result.data[i].Gvp_page + '</td><td>'
                                                + result.data[i].Gvp_gv_prefix + '</td><td>'
                                                + getFormatedDate(new Date(parseInt(result.data[i].Gvp_valid_from.substr(6)))) + '</td><td>'
                                                + getFormatedDate(new Date(parseInt(result.data[i].Gvp_valid_to.substr(6)))) + '</td><td>'
                                                + result.data[i].Gvp_amt + '</td></tr>');
                                        }
                                        updateCurrencyAmount(result.data[0].Gvp_amt, jQuery("#Sir_debtor_cd").val(), "");
                                        jQuery("#VoucherCode").val("");
                                        jQuery("#VoucherBook").val("");
                                        jQuery("#PagesTo").val("");
                                        jQuery(".pages-from-label").empty();
                                        jQuery(".pages-from-label").html("--");
                                        jQuery(".num-issu-pages-label").empty();
                                        jQuery(".num-issu-pages-label").html("--");
                                        jQuery("#PageValue").val("");
                                        jQuery("#TotalValue").val("");
                                        jQuery('#ExpireOnDt').val(getFormatedDate(new Date()));
                                        jQuery('#IssueAsFoc').attr('checked', false);


                                        jQuery("#VoucherCode").attr("disabled", true)
                                        jQuery("#VoucherBook").attr("disabled", true)
                                        jQuery("#PagesTo").attr("disabled",true);
                                        jQuery("#PageValue").attr("disabled", true);
                                        jQuery("#TotalValue").attr("disabled", true);
                                        jQuery('#ExpireOnDt').attr("disabled", true);
                                        jQuery("#IssueAsFoc").attr("disabled", true);
                                        jQuery(".gift-vouch-search").hide();

                                    } else {
                                        if (result.type == "Error") {
                                            setError(result.msg);
                                        }
                                        if (result.type == "Info") {
                                            setInfoMsg(result.msg);
                                        }
                                    }
                                } else {
                                    Logout();
                                }
                            }

                        });
                    } else {
                        setInfoMsg("Invalid amount.");
                        jQuery("#PageValue").val("");
                        jQuery("#TotalValue").val("");
                    }
                } else {
                    jQuery("#PageValue").val("");
                    jQuery("#TotalValue").val("");
                    setInfoMsg("Invalid amount.");
                }
            } else {
                jQuery("#PageValue").val("");
                jQuery("#TotalValue").val("");
                setInfoMsg("Pages not select properly.");
            }
        } else {
            jQuery("#PageValue").val("");
            jQuery("#TotalValue").val("");
            setInfoMsg("Pages not select properly.");
        }
    });
    jQuery(".btn-Receipt-entry-save-data").click(function (event) {
        Lobibox.confirm({
            msg: "Do you want to save ?",
            callback: function ($this, type, ev) {
                if (type == "yes") {
                    var attr = $(this).attr('disabled');

                    if (typeof attr !== typeof undefined && attr !== false) {
                        setInfoMsg("Cannot update exist recept entry");
                    } else {
                        jQuery(this).attr("disabled", true);
                        event.preventDefault();
                        var formdata = jQuery("#Receipt-entry-crte-frm").serialize();
                        var Division = jQuery("#Division").val();
                        var CheckManSys = jQuery("input#CheckManSys:checked").val();
                        var isOtherParty = "False";
                        if (jQuery("#OtherParty").is(":checked")) {
                            isOtherParty = "True";
                        }
                        jQuery.ajax({
                            type: 'POST',
                            url: '/ReceiptEntry/saveReciptData?Division=' + Division + "&CheckManSys=" + CheckManSys + "&isOtherParty=" + isOtherParty,
                            data: formdata,
                            success: function (response) {
                                if (response.login == true) {
                                    if (response.success == true) {
                                        clearReciptForm();
                                        setSuccesssMsg(response.msg);
                                        Lobibox.confirm({
                                            msg: "Do you want to print receipt ?",
                                            callback: function ($this, type, ev) {
                                                if (type == "yes") {
                                                    //window.location.href = "/Invoicing/InvoicingReport?invNo=" + jQuery("#Sah_inv_no").val();
                                                    window.open(
                                                      "/ReceiptEntry/ReceiptReport?recNo=" + response.recNo,
                                                      '_blank' // <- This is what makes it open in a new window.
                                                  );
                                                }
                                            }
                                        });

                                    } else {
                                        if (response.type == "Error") {
                                            setError(response.msg);
                                        } else if (response.type == "Info") {
                                            setInfoMsg(response.msg);
                                        }
                                    }
                                    jQuery(".btn-Receipt-entry-save-data").removeAttr("disabled");
                                } else {
                                    Logout();
                                }
                            }
                        });
                    }
                }
            }
        });
    });
    

    jQuery("#Sir_receipt_no").focusout(function () {
        receiptFocusOut(jQuery(this).val());
    });

    jQuery("#Sir_receipt_no").on("keydown", function (evt) {
        var chk = false;
        if (jQuery('#CheckUnallocated').is(":checked")) {
            chk = true;
        }
        if (evt.keyCode == 113) {
            var headerKeys = Array()
            headerKeys = ["Row", "Doc No", "Ref No", "Rec Date","Oth Ref No"];
            field = "receiptSearch";
            var data={chk:chk,recTyp:jQuery("#Sir_receipt_type").val()};
            var x = new CommonSearch(headerKeys, field,data);
        }
        if (evt.keyCode == 13) {
            receiptFocusOut(jQuery(this).val());
        }
    })
    jQuery(".recpt-num-search").click(function () {
        var chk = false;
        if (jQuery('#CheckUnallocated').is(":checked")) {
            chk = true;
        }
        var headerKeys = Array()
        headerKeys = ["Row", "Doc No", "Ref No", "Rec Date", "Oth Ref No"];
        field = "receiptSearch"
        var data={chk:chk,recTyp:jQuery("#Sir_receipt_type").val()};
        var x = new CommonSearch(headerKeys, field,data);
    });
    function receiptFocusOut(recNo) {
        if (recNo != "") {
            jQuery.ajax({
                type: "GET",
                url: "/ReceiptEntry/getReceiptDetails",
                data: { receiptNo: recNo },
                contentType: "application/json;charset=utf-8",
                dataType: "json",
                success: function (result) {
                    if (result.login == true) {
                        if (result.success == true) {
                            if (result.ReceiptHeader != null) {
                                ReceiptHeader = result.ReceiptHeader;
                                jQuery("#Sir_receipt_type").val(ReceiptHeader.Sir_receipt_type);
                                jQuery("#Sir_manual_ref_no").val(ReceiptHeader.Sir_manual_ref_no);
                                jQuery('#Sir_receipt_date').val(getFormatedDateInput(ReceiptHeader.Sir_receipt_date));
                                jQuery("#Sir_debtor_cd").val(ReceiptHeader.Sir_debtor_cd);
                                jQuery("#Sir_debtor_name").val(ReceiptHeader.Sir_debtor_name);
                                jQuery("#Sir_debtor_add_1").val(ReceiptHeader.Sir_debtor_add_1);
                                jQuery("#Sir_debtor_add_2").val(ReceiptHeader.Sir_debtor_add_2);
                                jQuery("#Sir_nic_no").val(ReceiptHeader.Sir_nic_no);
                                jQuery("#Sir_mob_no").val(ReceiptHeader.Sir_mob_no);
                                jQuery("#Sir_manual_ref_no").val(ReceiptHeader.Sir_manual_ref_no);
                                if (ReceiptHeader.Sir_oth_party == 1) {
                                    jQuery("#OtherParty").prop("checked",true);
                                }
                                else {
                                    jQuery("#OtherParty").prop("checked", false);
                                }
                                jQuery("#Sir_oth_partycd").val(ReceiptHeader.Sir_oth_partycd);
                                jQuery("#Sir_oth_partyname").val(ReceiptHeader.Sir_oth_partyname);                                
                                jQuery(".tot-paid-amount-val").empty();
                                jQuery(".tot-paid-amount-val").html(ReceiptHeader.Sir_tot_settle_amt);
                                jQuery("#TotalAmount").val(ReceiptHeader.Sir_tot_settle_amt);
                                jQuery("#Sir_anal_4").val(ReceiptHeader.Sir_anal_4);
                                jQuery("#Sar_anal_1").val(ReceiptHeader.Sar_anal_1);
                                if (ReceiptHeader.Sar_anal_8 == 1) {
                                    jQuery("#CheckManSys").val("1");
                                }
                                else {
                                    jQuery("#CheckManSys").val("0");
                                }

                                jQuery('table.payment-table .new-row').remove();
                                if (result.receiptItem != null) {
                                    receiptItem = result.receiptItem;
                                    for (i = 0; i < receiptItem.length; i++) {
                                        jQuery('table.payment-table').append('<tr class="new-row">' +
                                           '<td style="text-align:center;"></td>' +
                                           '<td>' + receiptItem[i].Sird_pay_tp + '</td>' +
                                           '<td>' + receiptItem[i].Sird_deposit_bank_cd + '</td>' +
                                           '<td>' + receiptItem[i].Sird_deposit_branch + '</td>' +
                                           '<td>' + receiptItem[i].Sird_cc_tp + '</td>' +
                                           '<td>' + receiptItem[i].Sird_settle_amt + '</td>' +
                                           '</tr>');
                                    }
                                }
                                jQuery('table.giftvoc-listing-table .new-row').remove();
                                if (result.gvDetails != null) {
                                    gvDetails = result.gvDetails;
                                    for (i = 0; i < gvDetails.length; i++) {
                                        jQuery('table.giftvoc-listing-table').append('<tr class="new-row">' +
                                           '<td>' + gvDetails[i].Gvp_gv_cd + '</td>' +
                                           '<td>' + gvDetails[i].Gvp_book + '</td>' +
                                           '<td>' + gvDetails[i].Gvp_page + '</td>' +
                                           '<td>' + gvDetails[i].Gvp_gv_prefix + '</td>' +
                                           '<td>' + gvDetails[i].Gvp_valid_from + '</td>' +
                                           '<td>' + gvDetails[i].Gvp_valid_to + '</td>' +
                                           '<td>' + gvDetails[i].Gvp_amt + '</td>' +
                                           '</tr>');
                                    }
                                }
                                jQuery(".btn-Receipt-entry-save-data").attr("disabled", true);
                                jQuery(".btn-Receipt-entry-save-data").hide();
                                jQuery(".Receipt-entry-cls .btn-print-receipt").show();
                            } else {
                                setInfoMsg("Cannot find receipt details.");
                                clearReciptForm();
                            }
                        } else {
                            jQuery(".Receipt-entry-cls .btn-print-receipt").hide();
                            if (result.type == "Error") {
                                setError(result.msg);
                            } else if (result.type == "Info") {
                                setInfoMsg(result.msg);
                            }
                        }
                    } else {
                        Logout();
                    }
                }
            });
        }
    }
    jQuery(".Receipt-entry-cls .btn-print-receipt").click(function () {
            if (jQuery("#Sir_receipt_no").val() != "") {
                //window.location.href = "/ReceiptEntry/ReceiptReport?recNo=" + jQuery("#Sir_receipt_no").val();
                window.open(
                   "/ReceiptEntry/ReceiptReport?recNo=" + jQuery("#Sir_receipt_no").val(),
                   '_blank' // <- This is what makes it open in a new window.
               );
            }
            else {
                setInfoMsg("Invalid receipt number.");
            }
    });
    jQuery("#OtherParty").click(function () {
        if (jQuery(this).is(":checked")) {
            jQuery("#Sir_oth_partycd").removeAttr("readonly");
        } else {
            jQuery("#Sir_oth_partycd").attr("readonly",true);
        }
    });
    jQuery("#Sir_oth_partycd").on("keydown", function (evt) {
        if (jQuery("#OtherParty").is(":checked")) {
            if (evt.keyCode == 113) {
                var headerKeys = Array()
                headerKeys = ["Row", "Code", "Name", "Nic", "Mobile", "Br No"];
                field = "othPrtyCus"
                var x = new CommonSearch(headerKeys, field);
            }
            if (evt.keyCode == 13) {
                othPartyFoOut(jQuery(this).val());
            }
        } else {
            setInfoMsg("Please check other party option.");
        }
        
    });
    jQuery(".oth-party-search").click(function () {
        if (jQuery("#OtherParty").is(":checked")) {
            var headerKeys = Array()
            headerKeys = ["Row", "Code", "Name", "Nic", "Mobile", "Br No"];
            field = "othPrtyCus"
            var x = new CommonSearch(headerKeys, field);
        } else {
            setInfoMsg("Please check other party option.");
        }
    });
    jQuery("#Sir_oth_partycd").focusout(function () {
        if (jQuery("#OtherParty").is(":checked")) {
            othPartyFoOut(jQuery(this).val());
        } else {
            setInfoMsg("Please check other party option.");
        }
    });
});
function othPartyFoOut(code) {
    if (code != "") {
        jQuery.ajax({
            type: "GET",
            url: "/ReceiptEntry/cusCodeTextChanged",
            data: { cusCd: code },
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            success: function (result) {
                if (result.login == true) {
                    if (result.success == true) {
                        if (typeof (result.local) != "undefined") {
                            jQuery("#Sir_oth_partycd").val(result.data.Mbe_cd);
                            jQuery("#Sir_oth_partyname").val(result.data.Mbe_name);
                        }
                        if (typeof (result.group) != "undefined") {
                            jQuery("#Sir_oth_partycd").val(result.data.Mbg_cd);
                            jQuery("#Sir_oth_partyname").val(result.data.Mbg_name);
                        }
                    } else {
                        jQuery("#Sir_oth_partycd").val("");
                        jQuery("#Sir_oth_partyname").val("");
                        if (result.type == "Info") {
                            setInfoMsg(result.msg);
                        }
                        if (result.type == "Error") {
                            setInfoMsg(result.msg);
                        }
                    }
                } else {
                    Logout();
                }
            }

        });
    }
}
function refNumberFocusout(enqId) {
    if (enqId != "") {
        jQuery.ajax({
            type: "GET",
            url: "/ReceiptEntry/getEnquiryData",
            data: { enqId: enqId },
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            success: function (result) {
                if (result.login == true) {
                    if (result.success == true) {
                        if (result.enqData.GCE_CUS_CD != null) {
                            jQuery("#Sir_debtor_cd").val(result.enqData.GCE_CUS_CD);
                            codeCusFocusOut(result.enqData.GCE_CUS_CD);
                        }
                    } else {
                        if (result.type == "Error") {
                            setError(result.msg);
                        } else if (result.type == "Info") {
                            setInfoMsg(result.msg);
                        }
                    }
                } else {
                    Logout();
                }
            }
        });
    }
}
function codeCusFocusOut(code) {
    if (code != "") {
        jQuery.ajax({
            type: "GET",
            url: "/ReceiptEntry/cusCodeTextChanged",
            data: { cusCd: code },
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            success: function (result) {
                if (result.login == true) {
                    if (result.success == true) {
                        if (typeof (result.local) != "undefined") {
                            jQuery("#Sir_debtor_cd").val(result.data.Mbe_cd);
                            jQuery("#Sir_debtor_name").val(result.data.Mbe_name);
                            jQuery("#Sir_debtor_add_1").val(result.data.Mbe_add1);
                            jQuery("#Sir_debtor_add_2").val(result.data.Mbe_add2);
                            jQuery("#Sir_nic_no").val(result.data.Mbe_nic);
                            jQuery("#Sir_mob_no").val(result.data.Mbe_mob);
                        }
                        if (typeof (result.group) != "undefined") {
                            jQuery("#Sir_debtor_cd").val(result.data.Mbg_cd);
                            jQuery("#Sir_debtor_name").val(result.data.Mbg_name);
                            jQuery("#Sir_debtor_add_1").val(result.data.Mbg_add1);
                            jQuery("#Sir_debtor_add_2").val(result.data.Mbg_add2);
                            jQuery("#Sir_nic_no").val(result.data.Mbg_nic);
                            jQuery("#Sir_mob_no").val(result.data.Mbg_mob);
                        }
                    } else {
                        jQuery("#Sir_debtor_cd").val("");
                        jQuery("#Sir_debtor_name").val("");
                        jQuery("#Sir_debtor_add_1").val("");
                        jQuery("#Sir_debtor_add_2").val("");
                        jQuery("#Sir_nic_no").val("");
                        jQuery("#Sir_mob_no").val("");
                        if (result.type == "Info") {
                            setInfoMsg(result.msg);
                        }
                        if (result.type == "Error") {
                            setInfoMsg(result.msg);
                        }
                    }
                } else {
                    Logout();
                }
            }

        });
    }
}
