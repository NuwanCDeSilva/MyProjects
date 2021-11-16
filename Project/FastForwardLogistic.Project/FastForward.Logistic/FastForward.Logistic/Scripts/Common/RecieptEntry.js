jQuery(document).ready(function () {

    jQuery("#cus_heading").text("Debtor details");
    clearSessions();
    jQuery('#Sar_receipt_date').val(my_date_format_tran(my_date_format_with_time(new Date()).toString()));
    jQuery('#Cheque_dt').val(my_date_format_tran(my_date_format_with_time(new Date()).toString()));
    jQuery(".cancel-rec").hide();
    loadDivision();
    function loadDivision() {
        jQuery.ajax({
            type: "GET",
            url: "/RecieptEntry/getDivision",
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
    jQuery(".division-search").click(function () {
        var headerKeys = Array()
        headerKeys = ["Row", "Code", "Description"];
        field = "divisionSearch"
        var x = new CommonSearch(headerKeys, field);
    });
    jQuery(".btn-cust_search").click(function () {
        var headerKeys = Array()
        headerKeys = ["Row", "Name", "Code", "NIC", "Mobile", "BR No"];
        field = "cusCode11"
        var x = new CommonSearch(headerKeys, field);
    });
    jQuery(".inv-no-search").click(function () {
        var isOtherParty = "NO";
        if (jQuery("#OtherPcChk").is(":checked")) {
            isOtherParty = "YES";
        }

        var headerKeys = Array()
        headerKeys = ["Row", "Code", "Type of Shipment", "Amount", "Date"];
        field = "InvoiceNo2"
        data = { debt: jQuery("#Sar_debtor_cd").val(), othpc: isOtherParty };
        var x = new CommonSearch(headerKeys, field, data);
    });
    // Added by Chathura on 20-sep-2017
    jQuery("#InvoiceNo").on("keydown", function (evt) {
        if (evt.keyCode == 113) {
            var isOtherParty = "NO";
            if (jQuery("#OtherPcChk").is(":checked")) {
                isOtherParty = "YES";
            }
            var headerKeys = Array()
            headerKeys = ["Row", "Code", "Type of Shipment", "Amount", "Date"];
            field = "InvoiceNo2"
            data = { debt: jQuery("#Sar_debtor_cd").val(), othpc: isOtherParty };
            var x = new CommonSearch(headerKeys, field, data);
        }
    });
    jQuery("#InvoiceNo").focusout(function () {

        var cusC = jQuery("#Sar_debtor_cd").val();
        var invC = jQuery("#InvoiceNo").val();
        var isOtherParty = "NO";
        if (jQuery("#OtherPcChk").is(":checked")) {
            isOtherParty = "YES";
        }
        if (cusC != "" && invC != "") {
            getInvoicedValue(cusC, invC, isOtherParty);
        }
        else {
            //setInfoMsg("");
        }

    });
    //add-inv-amount-btn
    jQuery('.add-inv-amount-btn').click(function (e) {
        var unallow = false;
        if (jQuery("#Unallow").is(":checked")) {
            unallow = true;
        }
        if (jQuery("#Sar_receipt_type").val() != "ADVAN" && jQuery("#Sar_receipt_type").val() != "AD-HOC" && jQuery("#Sar_receipt_type").val() != "DIRECT") {
            if ((jQuery("#InvoiceNo").val() == null || jQuery("#InvoiceNo").val() == "") && unallow == false) {
                setInfoMsg("Please select an invoice number.");
                return;
            }
            else if ((Number(jQuery("#Ammountdup").val().toString().replace(/[^0-9\.-]+/g, "")) < Number(jQuery("#Ammount").val().toString().replace(/[^0-9\.-]+/g, ""))) && unallow == false) {
                setInfoMsg("Settle ammount cannot larger than invoice amount");
                return;
            }
            else {
                if (jQuery("#Sar_debtor_cd").val() == null || jQuery("#Sar_debtor_cd").val() == "") {
                    setInfoMsg("Please select debtor.");
                    return;
                }
                else {

                    paymentValue = jQuery("#Ammount").val();
                    customer = jQuery("#Sar_debtor_cd").val();
                    type = jQuery("#Sar_receipt_type").val();
                    invAmount = jQuery("#Ammountdup").val();
                    invAmount = invAmount.replace(/\,/g, '');
                    invNo = jQuery("#InvoiceNo").val();
                    validate = true;
                    if (type == "DEBT") {
                        if (invNo == "" && unallow == false) {
                            setInfoMsg("Please add invoice");
                            validate = false;
                        }
                        if ((invAmount == "" || parseFloat(invAmount) <= 0) && unallow == false) {
                            setInfoMsg("Invoice amount must be greater than zero");
                            validate = false;
                        } else {
                            if ((parseFloat(paymentValue) > parseFloat(invAmount)) && unallow == false) {
                                setInfoMsg("Payment cannot exceed outstanding amount.");
                                validate = false;
                            }
                        }
                    }
                    if (paymentValue == "" || parseFloat(paymentValue) <= 0) {
                        setInfoMsg("Please enter valid settle amount.");
                        validate = false;
                    }
                    if (customer == "") {
                        setInfoMsg("Select valid customer.");
                        validate = false;
                    }
                    if (validate == true) {

                        jQuery.ajax({
                            type: "GET",
                            url: "/RecieptEntry/addSettlementDetails",
                            data: { paymentValue: paymentValue, customer: customer, type: type, invAmount: invAmount, invNo: invNo, unallow: unallow },
                            contentType: "application/json;charset=utf-8",
                            dataType: "json",
                            success: function (result) {
                                if (result.login == true) {
                                    if (result.success == true) {
                                        updatePayment(addCommas(result.TotalAmount), type);
                                        if (result.sesRecieptItemList != null && result.sesRecieptItemList.length > 0) {
                                            jQuery('table.stlinv-table tbody .new-row').empty();
                                            receiptItem = result.sesRecieptItemList;
                                            for (i = 0; i < receiptItem.length; i++) {
                                                jQuery('table.stlinv-table tbody').append('<tr class="new-row">' +
                                                   '<td style="width:50px;" class="invno">' + receiptItem[i].Sird_inv_no + '</td>' +
                                                   '<td style="width:36px;">' + addCommas(parseFloat(receiptItem[i].Sird_anal_3).toFixed(2)) + '</td>' +
                                                   '<td style="width:36px;">' + addCommas(parseFloat(receiptItem[i].Sird_settle_amt).toFixed(2)) + '</td>' +
                                                   '<td style="width:10px; " class="remove_invoce"><i class="fa fa-times" aria-hidden="true"></i></td>' +
                                                   '</tr>');
                                            }
                                            removeinvoice();
                                        }

                                        jQuery("#Ammount").val(""); // Commented by Chathura on 20-sep-2017
                                        jQuery("#InvoiceNo").val(""); // Added by Chathura on 20-sep-2017
                                        jQuery("#Ammountdup").val(""); // Commented by Chathura on 20-sep-2017

                                        //jQuery(".usedamt").empty();
                                        //jQuery(".usedamt").html(addCommas(parseFloat(used)));
                                        var balamnt = Number((jQuery(".balamt").html().replace(/\,/g, "")))
                                        var acbalamnt = balamnt - Number((paymentValue.replace(/\,/g, "")));

                                        //jQuery(".balamt").empty();
                                        jQuery(".balamt").html(addCommas(parseFloat(acbalamnt).toFixed(2)));


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
                }

                //updatePayment(jQuery("#Ammount").val(), "DEBT");
                //jQuery("#Ammount").val(""); // Commented by Chathura on 20-sep-2017
                //jQuery("#InvoiceNo").val(""); // Added by Chathura on 20-sep-2017
                //jQuery("#Ammountdup").val(""); // Commented by Chathura on 20-sep-2017
            }
        }
        else {
            if (jQuery("#Sar_debtor_cd").val() == null || jQuery("#Sar_debtor_cd").val() == "") {
                setInfoMsg("Please select debtor.");
                return;
            }
            else {
                updatePayment(jQuery("#Ammount").val(), jQuery("#Sar_receipt_type").val());
                jQuery("#Ammount").val(""); // Commented by Chathura on 20-sep-2017
                jQuery("#InvoiceNo").val(""); // Added by Chathura on 20-sep-2017
                jQuery("#Ammountdup").val(""); // Commented by Chathura on 20-sep-2017
            }
        }


    });

    jQuery(".vehlcph-search").click(function () {
        var headerKeys = Array()
        headerKeys = ["Row", "Code", "Description"];
        field = "detserch"
        var x = new CommonSearch(headerKeys, field);
    });
    jQuery("#VehLcTel").focusout(function () {
        var code = jQuery(this).val();
        telVehLcFocusout(code);
    });
    function telVehLcFocusout(code) {
        if (code != "") {
            jQuery.ajax({
                type: "GET",
                url: "/Validation/validateTelVehLc?code=" + code,
                contentType: "application/json;charset=utf-8",
                dataType: "json",
                success: function (result) {
                    if (result.login == false) {
                        Logout();
                    } else {
                        if (result.success == false) {
                            setInfoMsg(result.msg);
                            jQuery("#VehLcTel").val("");
                            jQuery("#VehLcTel").focus();
                        }
                        if (result.success == true) {
                            if (result.data.FVN_CD == null) {
                                setInfoMsg("Invelid selection.");
                                jQuery("#VehLcTel").val("");
                                jQuery("#VehLcTel").focus();
                            }
                        }
                    }
                }
            });
        }
    }

    function getInvoicedValue(cusCode, invCode, othpc) {
        if (cusCode != "" && invCode != "") {
            jQuery.ajax({
                type: "GET",
                url: "/RecieptEntry/getInvoiceNoByCus?invno=" + invCode + "&cus=" + cusCode + "&othpc=" + othpc,
                contentType: "application/json;charset=utf-8",
                dataType: "json",
                success: function (result) {

                    if (result.login == false) {
                        Logout();
                    } else {
                        if (result.success == false) {
                            setInfoMsg(result.msg);
                            //jQuery("#VehLcTel").val("");
                            //jQuery("#VehLcTel").focus();
                        }
                        if (result.success == true) {
                            console.log(result.data);
                            jQuery('#Ammountdup').val("");
                            jQuery('#Ammount').val("");
                            jQuery('#Ammountdup').val(result.data.Tih_bal_settle_amt);
                            jQuery('#Ammount').val(result.data.Tih_bal_settle_amt);
                            jQuery('#Ammount').focus();
                        }
                    }
                }
            });
        }
    }
    //Ammount
    //$('input#Ammount').blur(function () {
    //    
    //    var num = parseFloat($(this).val());
    //    var cleanNum = ReplaceNumberWithCommas(Number(num).toFixed(2));
    //    
    //    if (cleanNum == "NaN") {
    //        cleanNum = 0;
    //    }
    //    if (Number(cleanNum) < 0) {
    //        cleanNum = 0;
    //    }
    //    $(this).val(cleanNum);
    //});
    jQuery('.save-rec').click(function (e) {

        if (jQuery("#Sar_debtor_cd").val() == "") {
            setInfoMsg("Please select debtor code");
            //clearpage();
            return;
        }
        if (jQuery("#Sar_receipt_type").val() == "") {
            setInfoMsg("Please Select receipt type");
            //clearpage();
            return;
        }
        if (jQuery("#Sar_manual_ref_no").val() == "") {
            setInfoMsg("Please enter manual ref.");
            //clearpage();
            return;
        }
        //if (jQuery("#VehLcTel").val() == "") {    //// Vehical validation is commented by Chathura on 20-sep-2017
        //    setInfoMsg("Please Select Vehicle No");
        //    return;
        //}
        //if ($("#debtor").is(':checked') == true) {
        //    angtarget = "1";
        //}
        Lobibox.confirm({
            msg: "Do you want to save?",
            callback: function ($this, type, ev) {
                var unallow = false;
                if (jQuery("#Unallow").is(":checked")) {
                    unallow = true;
                }
                if (type == "yes") {
                    var check = "";
                    var formdata = jQuery("#rec-data").serialize();
                    formdata = formdata + "&recdate=" + jQuery("#Sar_receipt_date").val() + "&unallow=" + unallow;


                    jQuery.ajax({
                        type: "GET",
                        url: "/RecieptEntry/SaveRecieptEntry",
                        data: formdata,
                        contentType: "application/json;charset=utf-8",
                        dataType: "json",
                        success: function (result) {

                            if (result.login == true) {
                                if (result.success == true) {

                                    //clear page
                                    clearpage();
                                    setSuccesssMsg(result.msg);
                                    window.open("/RecieptEntry/PrintReceipt?ReceiptNo=" + result.Type , "_blank");

                                } else {
                                    if (result.Type == "Error") {
                                        setError(result.msg);

                                    }
                                    if (result.Type == "Info") {
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

        });
    });
    jQuery(".update-rec").click(function () {

        if (jQuery("#Sar_receipt_type").val() == "DEBT") {
            if (jQuery("#Unallow").is(":checked")) {
                if (jQuery("#Sar_receipt_no").val() != "") {
                    Lobibox.confirm({
                        msg: "Do you want to update?",
                        callback: function ($this, type, ev) {
                            var unallow = false;
                            if (jQuery("#Unallow").is(":checked")) {
                                unallow = true;
                            }
                            if (type == "yes") {
                                var check = "";
                                var formdata = jQuery("#rec-data").serialize();


                                jQuery.ajax({
                                    type: "GET",
                                    url: "/RecieptEntry/updateReceiptdetails",
                                    data: formdata,
                                    contentType: "application/json;charset=utf-8",
                                    dataType: "json",
                                    success: function (result) {
                                        if (result.login == true) {
                                            if (result.success == true) {

                                                //clear page
                                                clearpage();
                                                setSuccesssMsg(result.msg);
                                                //if (msg) {
                                                //    window.location.href = "/RecieptEntry";
                                                //}


                                            } else {
                                                if (result.Type == "Error") {
                                                    setError(result.msg);

                                                }
                                                if (result.Type == "Info") {
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

                    });
                } else {
                    setInfoMsg("Please select valid receipt number.");
                }
            }
        } else {
            setInfoMsg("Only DEBT unallowcated receipt can update.");
        }
    });
    jQuery("#Sar_receipt_type").focusout(function () {
        recipTypeFocusOut(jQuery(this).val());
    });
    function recipTypeFocusOut(type) {
        if (type != "") {
            jQuery.ajax({
                type: "GET",
                url: "/RecieptEntry/getReciptTypes",
                data: { type: type },
                contentType: "application/json;charset=utf-8",
                dataType: "json",
                success: function (result) {
                    if (result.login == true) {
                        if (result.success == false) {
                            jQuery("#Sar_receipt_type").val("");
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
                            loadPayModesTypes(jQuery("#Sar_receipt_type").val());
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
                            if (jQuery("#Sar_receipt_type").val() == "DEBT") {
                                jQuery(".other-part-cls").show();
                                jQuery(".search-unallow-recpt").show();
                            } else {
                                jQuery(".other-part-cls").hide();
                                jQuery(".search-unallow-recpt").hide();
                            }
                            clearPageAfterChangeRType();
                        }

                    } else {
                        Logout();
                    }
                }
            });
        }
    }
    function clearPageAfterChangeRType() {
        //var rtpe = jQuery("#Sar_receipt_type").val();
        jQuery("#Sar_manual_ref_no").val("");
        jQuery('#Sar_receipt_date').val(my_date_format_tran(my_date_format_with_time(new Date()).toString()));
        jQuery("#Sar_debtor_cd").val("");
        jQuery("#Sar_debtor_name").val("");
        jQuery("#Sar_manual_ref_no").val("");
        jQuery("#Sar_receipt_no").val("");
        jQuery(".tot-paid-amount-val").empty();
        jQuery(".tot-paid-amount-val").html("");
        jQuery(".bal-amount-val").empty();
        jQuery(".bal-amount-val").html("");
        jQuery("#TotalAmount").val(0);
        jQuery('table.payment-table .new-row').remove();
        //jQuery('table.payment-table').empty();
        jQuery("#VehLcTel").val(""); // Added by Chathura on 20-sep-2017
        jQuery("#InvoiceNo").val(""); // Added by Chathura on 20-sep-2017
        jQuery(".tbl-cus-name tr").remove(); // Added by Chathura on 20-sep-2017
        jQuery("#Ammountdup").val(""); // Commented by Chathura on 20-sep-2017
        jQuery("#Ammount").val(""); // Commented by Chathura on 20-sep-2017
        jQuery(".cancel-rec").hide();
        jQuery("#cus_heading").text("Debtor details");
        jQuery(".save-rec").show();

        $('#OtherPcChk').attr('checked', false);
        //jQuery(".cancel-rec").hide();
        //jQuery.ajax({
        //    type: "GET",
        //    url: "/RecieptEntry/ClearSession",
        //    data: {},
        //    contentType: "application/json;charset=utf-8",
        //    dataType: "json",
        //})
    }
    jQuery(".recpt-typ-search").click(function () {
        var headerKeys = Array()
        headerKeys = ["Row", "Type", "Description"];
        field = "receiptTYypeSearch"
        var x = new CommonSearch(headerKeys, field);
    });
    //jQuery(".recpt-num-search").click(function () {
    //    var chk = false;
    //    //if (jQuery('#CheckUnallocated').is(":checked")) {
    //    //    chk = true;
    //    //}
    //    var headerKeys = Array()
    //    headerKeys = ["Row", "Doc No", "Ref No", "Rec Date", "Oth Ref No"];
    //    field = "receiptSearch"
    //    var data = { chk: chk, recTyp: jQuery("#Sar_receipt_type").val() };
    //    var x = new CommonSearch(headerKeys, field, data);
    //});
    ////Commented above and added below code segment by Chathura on 27-sep-2017
    jQuery(".recpt-num-search").click(function (evt) {

        var chk = "false";
        if (jQuery("#Unallow").is(":checked")) {
            chk = "true";
        }
        if (chk == "true" && jQuery("#Sar_debtor_cd").val() == "") {
            setInfoMsg("Please select customer for get unallowcate receipt");

        }
        var headerKeys = Array()
        headerKeys = ["Row", "Doc No", "Ref No", "Rec Date", "Oth Ref No"];
        field = "receiptSearch"
        var data = { chk: chk, fromDate: null, toDate: null, recTyp: jQuery("#Sar_receipt_type").val(), customer: jQuery("#Sar_debtor_cd").val() };

        var x = new CommonSearchDateFiltered(headerKeys, field, data);
    });

    function receiptFocusOut(recNo) {

        if (recNo != "") {
            jQuery.ajax({
                type: "GET",
                url: "/RecieptEntry/getReceiptDetails",
                data: { receiptNo: recNo },
                contentType: "application/json;charset=utf-8",
                dataType: "json",
                success: function (result) {
                    if (result.login == true) {
                        if (result.success == true) {
                            if (result.ReceiptHeader != null) {
                                jQuery("#Unallow").prop('checked', false);
                                jQuery(".search-unallow-recpt").hide();
                                jQuery(".update-rec").hide();
                                jQuery('table.stlinv-table tbody').empty();
                                ReceiptHeader = result.ReceiptHeader;
                                jQuery("#Sar_receipt_no").val(ReceiptHeader.Sar_receipt_no);
                                jQuery("#Sar_receipt_type").val(ReceiptHeader.Sar_receipt_type);
                                jQuery("#Sar_manual_ref_no").val(ReceiptHeader.Sar_manual_ref_no);
                                jQuery('#Sar_receipt_date').val(getFormatedDateInput(ReceiptHeader.Sar_receipt_date));
                                jQuery("#Sar_debtor_cd").val(ReceiptHeader.Sar_debtor_cd);
                                jQuery("#Sar_debtor_name").val(ReceiptHeader.Sar_debtor_name);
                                global_receipt_date = getFormatedDateInput(ReceiptHeader.Sar_receipt_date);

                                jQuery('.tbl-cus-name .new-row').remove();
                                jQuery('.tbl-cus-name').append('<tr class="new-row">' +
                                     '<td>' + ReceiptHeader.Sar_debtor_name + '</td>' + '</tr>'
                                    );
                                jQuery("#Sar_manual_ref_no").val(ReceiptHeader.Sar_manual_ref_no);
                                //if (ReceiptHeader.Sir_oth_party == 1) {
                                //    jQuery("#OtherParty").prop("checked", true);
                                //}
                                //else {
                                //    jQuery("#OtherParty").prop("checked", false);
                                //}
                                //jQuery("#Sir_oth_partycd").val(ReceiptHeader.Sir_oth_partycd);
                                jQuery(".tot-paid-amount-val").empty();
                                jQuery(".tot-paid-amount-val").html(ReceiptHeader.Sir_tot_settle_amt);
                                jQuery("#TotalAmount").val(ReceiptHeader.Sar_tot_settle_amt);
                                jQuery('table.payment-table .new-row').remove();
                                if (result.receiptItem != null) {
                                    receiptItem = result.receiptItem;
                                    for (i = 0; i < receiptItem.length; i++) {
                                        jQuery('table.payment-table').append('<tr class="new-row">' +
                                           '<td style="text-align:center;"></td>' +
                                           '<td>' + receiptItem[i].Sard_pay_tp + '</td>' +
                                           '<td>' + receiptItem[i].Sard_deposit_bank_cd + '</td>' +
                                           '<td>' + receiptItem[i].Sard_deposit_branch + '</td>' +
                                           '<td>' + receiptItem[i].Sard_cc_tp + '</td>' +
                                           '<td>' + receiptItem[i].Sard_ref_no.slice(-6) + '</td>' +
                                           '<td>' + addCommas(parseFloat(receiptItem[i].Sard_settle_amt)) + '</td>' +
                                           '</tr>');
                                    }
                                }
                                if (ReceiptHeader.Sar_receipt_type == "DEBT") {

                                    if (result.receiptItem != null) {

                                        var bal = 0;
                                        var used = 0;
                                        receiptItem = result.receiptItem;
                                        if (receiptItem.length > 0) {
                                            jQuery(".unalow-unit").show();
                                            for (i = 0; i < receiptItem.length; i++) {
                                                if (receiptItem[i].Sard_inv_no != "") {
                                                    jQuery('table.stlinv-table tbody').append('<tr class="new-row">' +
                                                       '<td style="width:50px;" class="invno">' + receiptItem[i].Sard_inv_no + '</td>' +
                                                       '<td style="width:36px;">' + '-' + '</td>' +
                                                       '<td style="width:36px;">' + addCommas(parseFloat(receiptItem[i].Sard_settle_amt)) + '</td>' +
                                                       '<td style="width:10px;"></td>' +
                                                       '</tr>');
                                                    used = parseFloat(used) + parseFloat(receiptItem[i].Sard_settle_amt);
                                                } else {
                                                    bal = parseFloat(receiptItem[i].Sard_settle_amt);
                                                }
                                            }

                                            jQuery(".usedamt").empty();
                                            jQuery(".usedamt").html(addCommas(parseFloat(used)));

                                            jQuery(".balamt").empty();
                                            jQuery(".balamt").html(addCommas(parseFloat(bal)));
                                        }
                                    }
                                    jQuery(".search-unallow-recpt").show();
                                    if (ReceiptHeader.Sar_anal_9 == 1) {
                                        jQuery("#Unallow").prop('checked', true);
                                        jQuery(".update-rec").show();
                                    }
                                } else {
                                    jQuery(".unalow-unit").hide();
                                }
                                jQuery(".save-rec").attr("disabled", true);
                                jQuery(".save-rec").hide();
                                jQuery(".cancel-rec").show();
                            } else {
                                setInfoMsg("Cannot find receipt details.");
                                clearpage();
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
    jQuery("#Sar_receipt_no").focusout(function () {
        receiptFocusOut(jQuery(this).val());
    });
    function clearpage() {
        jQuery(".unalow-unit").hide();
        jQuery("#Unallow").prop('checked', false);
        jQuery(".search-unallow-recpt").hide();
        jQuery(".update-rec").hide();
        jQuery("#Sar_receipt_no").val("");
        jQuery("#Sar_receipt_type").val("");
        jQuery("#Sar_manual_ref_no").val("");
        jQuery('#Sar_receipt_date').val(my_date_format_tran(my_date_format_with_time(new Date()).toString()));
        jQuery("#Sar_debtor_cd").val("");
        jQuery("#Sar_debtor_name").val("");
        jQuery("#Sar_manual_ref_no").val("");
        jQuery(".tot-paid-amount-val").empty();
        jQuery(".tot-paid-amount-val").html("");
        jQuery(".bal-amount-val").empty();
        jQuery(".bal-amount-val").html("");
        jQuery("#TotalAmount").val(0);
        jQuery('table.payment-table .new-row').remove();
        //jQuery('table.payment-table tr').remove();
        jQuery("#VehLcTel").val(""); // Added by Chathura on 20-sep-2017
        jQuery("#InvoiceNo").val(""); // Added by Chathura on 20-sep-2017
        jQuery(".tbl-cus-name tr").remove(); // Added by Chathura on 20-sep-2017
        jQuery("#Ammountdup").val(""); // Commented by Chathura on 20-sep-2017
        jQuery("#Ammount").val(""); // Commented by Chathura on 20-sep-2017
        jQuery(".cancel-rec").hide();
        jQuery("#cus_heading").text("Debtor details");
        jQuery(".save-rec").show();
        jQuery('.save-rec').removeAttr("disabled");
        jQuery("#OtherPcChk").prop('checked', false);
        jQuery(".other-part-cls").hide();

        //jQuery(".cancel-rec").hide();
        jQuery.ajax({
            type: "GET",
            url: "/RecieptEntry/ClearSession",
            data: {},
            contentType: "application/json;charset=utf-8",
            dataType: "json",
        });
        jQuery('table.stlinv-table tbody').empty();
    }
    jQuery(".clear-rec").click(function (evt) {
        evt.preventDefault();
        Lobibox.confirm({
            msg: "Do you want to clear data ?",
            callback: function ($this, type, ev) {
                if (type == "yes") {
                    window.location.href = "/RecieptEntry";
                    jQuery("#cus_heading").text("Debtor details");
                }
            }
        })

    });

    // Added by Chathura on 20-sep-2017
    jQuery("#Sar_receipt_type").on("keydown", function (evt) {
        if (evt.keyCode == 113) {
            var headerKeys = Array()
            headerKeys = ["Row", "Type", "Description"];
            field = "receiptTYypeSearch"
            var x = new CommonSearch(headerKeys, field);
        }
    });
    // Added by Chathura on 20-sep-2017
    jQuery("#Division").on("keydown", function (evt) {
        if (evt.keyCode == 113) {

            var headerKeys = Array()
            headerKeys = ["Row", "Code", "Description"];
            field = "divisionSearch"
            var x = new CommonSearch(headerKeys, field);
        }
    });
    // Added by Chathura on 20-sep-2017
    jQuery("#Sar_debtor_cd").on("keydown", function (evt) {
        if (evt.keyCode == 113) {
            var headerKeys = Array()
            headerKeys = ["Row", "Name", "Code", "NIC", "Mobile", "BR No"];
            field = "cusCode11"
            var x = new CommonSearch(headerKeys, field);
        }
    });
    // Added by Chathura on 20-sep-2017
    jQuery("#VehLcTel").on("keydown", function (evt) {
        if (evt.keyCode == 113) {
            var headerKeys = Array()
            headerKeys = ["Row", "Code", "Description"];
            field = "detserch"
            var x = new CommonSearch(headerKeys, field);
        }
    });
    // Added by Chathura on 20-sep-2017
    jQuery("#Sar_receipt_no").on("keydown", function (evt) {
        if (evt.keyCode == 113) {
            var chk = "false";
            if (jQuery("#Unallow").is(":checked")) {
                chk = "true";
            }
            if (chk == "true" && jQuery("#Sar_debtor_cd").val() == "") {
                setInfoMsg("Please select customer for get unallowcate receipt");

            }
            var headerKeys = Array()
            headerKeys = ["Row", "Doc No", "Ref No", "Rec Date", "Oth Ref No"];
            field = "receiptSearch"
            var data = { chk: chk, fromDate: null, toDate: null, recTyp: jQuery("#Sar_receipt_type").val(), customer: jQuery("#Sar_debtor_cd").val() };
            var x = new CommonSearchDateFiltered(headerKeys, field, data);
        }
    });

    // Added by Chathura on 21-sep-2017
    jQuery('.cancel-rec').click(function (e) {

        if (jQuery("#Sar_receipt_no").val() == "") {
            setInfoMsg("Please Select receipt no.");
            return;
        }
        //var todaydate = my_date_format_tran(my_date_format_with_time(new Date()).toString());
        //var rDate = my_date_format_tran(my_date_format_with_time(global_receipt_date).toString());
        //var dateCompare = todaydate.localeCompare(rDate);
        //if (dateCompare == 0) {
        Lobibox.confirm({
            msg: "Do you want to cancel the receipt?",
            callback: function ($this, type, ev) {
                if (type == "yes") {
                    var check = "";
                    jQuery.ajax({
                        type: "GET",
                        url: "/RecieptEntry/CancelRecieptEntry",
                        data: { data: jQuery('#Sar_receipt_no').val(), receptDate: jQuery("#Sar_receipt_date").val(), type: jQuery("#Sar_receipt_type").val() },
                        contentType: "application/json;charset=utf-8",
                        dataType: "json",
                        success: function (result) {
                            if (result.login == true) {
                                if (result.success == true) {
                                    clearpage();
                                    //window.location.href = "/RecieptEntry";
                                    setSuccesssMsg(result.msg);
                                    jQuery('#Sar_receipt_no').val("");
                                    //jQuery(".cancel-rec").show();
                                } else {
                                    if (result.Type == "Error") {
                                        setError(result.msg);
                                    }
                                    if (result.Type == "Info") {
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

        });
        //}
        //else {
        //    setInfoMsg("Backdated receipt cannot be canceled.");
        //}
    });
    //function clearpageOnReceiptType() {
    //    
    //    jQuery("#Sar_manual_ref_no").val("");
    //    jQuery('#Sar_receipt_date').val(my_date_format_tran(my_date_format_with_time(new Date()).toString()));
    //    jQuery("#Sar_debtor_cd").val("");
    //    jQuery("#Sar_debtor_name").val("");
    //    jQuery("#Sar_manual_ref_no").val("");
    //    jQuery(".tot-paid-amount-val").empty();
    //    jQuery(".tot-paid-amount-val").html("");
    //    jQuery("#TotalAmount").val(0);
    //    jQuery('table.payment-table .new-row').remove();
    //    jQuery('table.payment-table').remove();
    //    jQuery("#VehLcTel").val(""); // Added by Chathura on 20-sep-2017
    //    jQuery("#InvoiceNo").val(""); // Added by Chathura on 20-sep-2017
    //    jQuery(".tbl-cus-name tr").remove(); // Added by Chathura on 20-sep-2017
    //    jQuery("#Ammountdup").val(""); // Commented by Chathura on 20-sep-2017
    //    jQuery("#Ammount").val(""); // Commented by Chathura on 20-sep-2017
    //    jQuery(".cancel-rec").hide();
    //    jQuery("#cus_heading").text("Debtor details");
    //    jQuery.ajax({
    //        type: "GET",
    //        url: "/RecieptEntry/ClearSession",
    //        data: {},
    //        contentType: "application/json;charset=utf-8",
    //        dataType: "json",
    //    })
    //}



});
function clearSessions() {
    jQuery.ajax({
        type: "GET",
        url: "/RecieptEntry/ClearSession",
        data: {},
        contentType: "application/json;charset=utf-8",
        dataType: "json",
    });
}

function removeinvoice() {
    jQuery(".remove_invoce").click(function (evt) {
        evt.preventDefault();
        var td = jQuery(this).parent("td");
        var invNo = jQuery(this).siblings("td.invno").html();
        jQuery('table.stlinv-table tbody .new-row').empty();
        jQuery.ajax({
            type: "GET",
            url: "/RecieptEntry/removeInvoice",
            data: { invNo: invNo },
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            success: function (result) {


                var balamnt = Number((jQuery(".balamt").html().replace(/\,/g, "")));
                var acbalamnt = balamnt + result.settleamnt;

                //jQuery(".balamt").empty();
                jQuery(".balamt").html(addCommas(parseFloat(acbalamnt).toFixed(2)));

                updatePayment(addCommas(result.setamt), type);
                if (result.sesRecieptItemList != null && result.sesRecieptItemList.length > 0) {


                    receiptItem = result.sesRecieptItemList;
                    for (i = 0; i < receiptItem.length; i++) {
                        jQuery('table.stlinv-table tbody').append('<tr class="new-row">' +
                           '<td style="width:50px;" class="invno">' + receiptItem[i].Sird_inv_no + '</td>' +
                           '<td style="width:36px;">' + addCommas(parseFloat(receiptItem[i].Sird_anal_3).toFixed(2)) + '</td>' +
                           '<td style="width:36px;">' + addCommas(parseFloat(receiptItem[i].Sird_settle_amt).toFixed(2)) + '</td>' +
                           '<td style="width:10px; " class="remove_invoce"><i class="fa fa-times" aria-hidden="true"></i></td>' +
                           '</tr>');
                    }



                    removeinvoice();
                }
            }

        });
    });
}
function updatePayment(amount, type) {
    var payAmount = parseFloat(amount.replace(/\,/g, ""));
    var currentTot = (jQuery(".tot-amount-val").html() != "") ? parseFloat((jQuery(".tot-amount-val").html()).replace(/\,/g, "")) : 0;
    //var finTot = parseFloat(currentTot) + parseFloat(amount.replace(/\,/g, "")); // Commented by Chathura on 23-sep-2017 senario request by Vajira
    var finTot = parseFloat(amount.replace(/\,/g, ""));    // Added instead of above commented

    if (type == "DEBT") {
        updateCurrencyAmount(parseFloat(payAmount), jQuery("#Sar_debtor_cd").val(), jQuery("#InvoiceNo").val(), "DEBT");
    } else {
        updateCurrencyAmount(parseFloat(finTot), jQuery("#Sar_debtor_cd").val(), "");
    }

}


jQuery('.btn-blue-fullbg').click(function (e) {

    if (jQuery("#Sar_receipt_no").val() == "") {
        setInfoMsg("Please Receipt No");
        debugger;
        return;
    }

    Lobibox.confirm({
        msg: "Do you want to print data?",
        callback: function ($this, type, ev) {
            if (type == "yes") {

                window.open("/RecieptEntry/PrintReceipt?ReceiptNo=" + jQuery("#Sar_receipt_no").val(), '_blank' // <- This is what makes it open in a new window.
                    );
            }
        }

    });
});