jQuery(document).ready(function () {
    jQuery('#DateFrom').datepicker({ maxDate: new Date(), dateFormat: "dd/M/yy" });
    jQuery('#DateTo').datepicker({ maxDate: new Date(), dateFormat: "dd/M/yy" });



    jQuery('#DateFrom').val(getFormatedDate(getMonthAgoMonth(new Date())));
    jQuery('#DateTo').val(getFormatedDate(new Date()));
    
    jQuery('#DateFrom').focusout(function () {
        if (ValidDate(jQuery(this).val()) == "false") {
            setInfoMsg("Invalid from date.");
            jQuery('#DateFrom').val(getFormatedDate(getMonthAgoMonth(new Date())));
        } else {
            if (Date.parse(jQuery(this).val()) > new Date()) {
                setInfoMsg("Date cannot greter than Today.");
                jQuery('#DateFrom').val(getFormatedDate(getMonthAgoMonth(new Date())));
            }
        }
    });
    clearSessionData();
   
    jQuery('#DateTo').focusout(function () {
        if (ValidDate(jQuery(this).val()) == "false") {
            setInfoMsg("Invalid to date.");
            jQuery('#DateTo').val(getFormatedDate(new Date()));
        } else {
            if (Date.parse(jQuery(this).val()) > new Date()) {
                setInfoMsg("Date cannot greter than Today.");
                jQuery('#DateTo').val(getFormatedDate(new Date()));
            }
        }
    });

    jQuery("#Other_Party").on("keydown", function (evt) {
        if (evt.keyCode == 113) {
            var headerKeys = Array()
            headerKeys = ["Row", "Code", "Name", "Nic", "Mobile", "Br No"];
            field = "othConCusSrch"
            var x = new CommonSearch(headerKeys, field);
        }
        if (evt.keyCode == 13) {
            codeOthFocusOut(jQuery(this).val());
        }
    });
    jQuery(".othparty-search").click(function () {
        var headerKeys = Array()
        headerKeys = ["Row", "Code", "Name", "Nic", "Mobile", "Br No"];
        field = "othConCusSrch"
        var x = new CommonSearch(headerKeys, field);
    });
    jQuery("#Other_Party").focusout(function () {
        codeOthFocusOut(jQuery(this).val());
    });

    jQuery("#Customer").on("keydown", function (evt) {
        if (evt.keyCode == 113) {
            var headerKeys = Array()
            headerKeys = ["Row", "Code", "Name", "Nic", "Mobile", "Br No"];
            field = "payConCusfSrch"
            var x = new CommonSearch(headerKeys, field);
        }
        if (evt.keyCode == 13) {
            codeFocusOut(jQuery(this).val());
        }
    });
    jQuery(".cus-cd-search").click(function () {
        var headerKeys = Array()
        headerKeys = ["Row", "Code", "Name", "Nic", "Mobile", "Br No"];
        field = "payConCusfSrch"
        var x = new CommonSearch(headerKeys, field);
    });
    jQuery("#Customer").focusout(function () {
        codeFocusOut(jQuery(this).val());
    });
    jQuery(".load-receipt-data").click(function () {
        loadOtherPartyReceipts();
    });
    jQuery(".paymet-conf-save-data").click(function () {
        Lobibox.confirm({
            msg: "Do you want to save ?",
            callback: function ($this, type, ev) {
                if (type == "yes") {
                    jQuery.ajax({
                        type: "GET",
                        url: "/OtherPartyPaymentConf/saveOtherPartyPayment",
                        contentType: "application/json;charset=utf-8",
                        dataType: "json",
                        success: function (result) {
                            if (result.login == true) {
                                if (result.success == true) {
                                    if (result.type == "Success") {
                                        setSuccesssMsg(result.msg);
                                        clearFormData();
                                    }
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
        });
    });
});
jQuery(".paymet-conf-clear-data").click(function () {
    Lobibox.confirm({
        msg: "Do you want to clear ?",
        callback: function ($this, type, ev) {
            if (type == "yes") {
                clearFormData();
            }
        }
    });
});
function clearFormData() {
    clearSessionData();
    jQuery("#Other_Party").val("");
    jQuery("#Customer").val("");
    clearAllValues();
    jQuery('#DateFrom').val(getFormatedDate(getMonthAgoMonth(new Date())));
    jQuery('#DateTo').val(getFormatedDate(new Date()));
    jQuery("table.payment-table .new-row").remove();
    jQuery(".bal-amount-val").empty(); 
    jQuery(".bal-amount-val").html("0.00");
    jQuery(".tot-amount-val").empty();
    jQuery(".tot-amount-val").html("0.00");
    jQuery(".tot-paid-amount-val").empty();
    jQuery(".tot-paid-amount-val").html("0.00");
    jQuery(".total-paid-amt").empty();
    jQuery(".total-paid-amt").html(": 0.00");
}
function codeOthFocusOut(code) {
    if (code != "") {
        jQuery.ajax({
            type: "GET",
            url: "/OtherPartyPaymentConf/cusCodeTextChanged",
            data: { cusCd: code },
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            success: function (result) {
                if (result.login == true) {
                    if (result.success == true) {
                        if (typeof (result.local) != "undefined") {
                            jQuery("#Other_Party").val(result.data.Mbe_cd);
                        }
                        if (typeof (result.group) != "undefined") {
                            jQuery("#Other_Party").val(result.data.Mbg_cd);
                        }
                    } else {
                        jQuery("#Other_Party").val("");
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

function codeFocusOut(code) {
    if (code != "") {
        jQuery.ajax({
            type: "GET",
            url: "/OtherPartyPaymentConf/cusCodeTextChanged",
            data: { cusCd: code },
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            success: function (result) {
                if (result.login == true) {
                    if (result.success == true) {
                        if (typeof (result.local) != "undefined") {
                            jQuery("#Customer").val(result.data.Mbe_cd);
                        }
                        if (typeof (result.group) != "undefined") {
                            jQuery("#Customer").val(result.data.Mbg_cd);
                        }
                    } else {
                        jQuery("#Customer").val("");
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
function loadOtherPartyReceipts() {
    if (jQuery('#DateFrom').val() != "" && jQuery('#DateTo').val() != "") {
        var dateFrom = jQuery('#DateFrom').val();
        var dateTo = jQuery('#DateTo').val();
        var OthCus = jQuery('#Other_Party').val();
        var Cus = jQuery('#Customer').val();
        jQuery.ajax({
            type: "GET",
            url: "/OtherPartyPaymentConf/loadOtherPartyReceipts",
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            data: { dateFrom: dateFrom, dateTo: dateTo, OthCus: OthCus, Cus: Cus },
            success: function (result) {
                if (result.login == true) {
                    if (result.success == true) {
                        jQuery('.other-party-payment .new-row').remove();
                        if (result.data.length > 0) {
                            for (i = 0; i < result.data.length; i++) {
                                jQuery('.other-party-payment #othrpty-paydtl-tbl').append('<tr class="new-row">' +
                                        '<td><input type="checkbox" class="chk-select-receipt" name=""></td>' +
                                        '<td>' + result.data[i].Sir_manual_ref_no + '</td>' +
                                        '<td>' + result.data[i].Sir_receipt_no + '</td>' +
                                        '<td>' + result.data[i].Sir_debtor_cd + '</td>' +
                                        '<td>' + result.data[i].Sir_debtor_name + '</td>' +
                                        '<td>' + result.data[i].Sir_oth_partycd + '</td>' +
                                        '<td class="text-left-align">' + addCommas(result.data[i].Sir_oth_partystltamt) + '</td>' +
                                        '<td class="text-left-align">' + addCommas((parseFloat(result.data[i].Sir_oth_partystltamt) - parseFloat(result.data[i].Sir_oth_paidamt))) + '</td>' +
                                        '<td><input type="text" class="form-control required-field text-box update-pay-amt text-left-align" readonly="readonly" name="' +result.data[i].Sir_receipt_no + '" id="' +result.data[i].Sir_receipt_no + '"></td>' +
                                        '</tr>');
                            }

                            jQuery(".chk-select-receipt").click(function () {
                                var check ="";
                                if (jQuery(this).is(":checked")) {
                                    check = "True";
                                } else {
                                    check="False";
                                }
                                var receiptNo = jQuery(this, '#othrpty-paydtl-tbl').closest("tr").find('td:eq(2)').text();
                                var othPartyCd = jQuery(this, '#othrpty-paydtl-tbl').closest("tr").find('td:eq(5)').text();
                                var obj = jQuery(this);
                                jQuery.ajax({
                                    type: "GET",
                                    url: "/OtherPartyPaymentConf/UpdateLinePrice",
                                    contentType: "application/json;charset=utf-8",
                                    dataType: "json",
                                    data: { check: check, receiptNo: receiptNo, payAmount: "", othPartyCd: othPartyCd },
                                    success: function (result) {
                                        if (result.login == true) {
                                            var bid = jQuery(obj).parent().parent().find("input.update-pay-amt");
                                            if (result.success == true) {
                                                if (check == "True") {
                                                    jQuery(bid).removeAttr("readonly");
                                                } else {
                                                    jQuery(bid).val("");
                                                    jQuery(bid).prop("readonly", true);
                                                }
                                            }else{
                                                jQuery(bid).val("");
                                                jQuery(obj).prop("checked", false);
                                                if (result.type == "Info") {
                                                    setInfoMsg(result.msg);
                                                }
                                                if (result.type == "Error") {
                                                    setInfoMsg(result.msg);
                                                }
                                            }
                                        }else{
                                            Logout();
                                        }
                                    }
                                });
                            });
                        }
                        
                        jQuery('.update-pay-amt').on("focusout", function (event) {
                            var receiptNo = jQuery(this, '#othrpty-paydtl-tbl').closest("tr").find('td:eq(2)').text();
                            var othPartyCd = jQuery(this, '#othrpty-paydtl-tbl').closest("tr").find('td:eq(5)').text();
                            var txtBox = jQuery(this);
                            var bid = jQuery(txtBox).parent().parent().find("input.chk-select-receipt");
                            var check = "";
                            if (jQuery(bid).is(":checked")) {
                                check = "True";
                            } else {
                                check = "False";
                            }
                            jQuery.ajax({
                                type: "GET",
                                url: "/OtherPartyPaymentConf/UpdateLinePrice",
                                contentType: "application/json;charset=utf-8",
                                dataType: "json",
                                data: { check: check, receiptNo: receiptNo, payAmount: jQuery(txtBox).val(), othPartyCd: othPartyCd },
                                success: function (result) {
                                    if (result.login == true) {
                                        if (result.success == true) {
                                            jQuery(".total-paid-amt").empty();
                                            jQuery(".total-paid-amt").html(": " + addCommas(result.totPaid));
                                            updateCurrencyAmount(parseFloat(result.totPaid), othPartyCd, "");
                                        } else {
                                            jQuery(txtBox).val("");
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
                        });
                        //check numbers and decimal  only
                        jQuery('.update-pay-amt').on("input", function (event) {
                            if (!jQuery.isNumeric(this.value)) {
                                this.value = "";
                            }
                            if (parseFloat(this.value) < 0) {
                                this.value = "";
                            }
                        });

                        jQuery('.update-pay-amt').keypress(function (event) {
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
    } else {
        setInfoMsg("Please select date range.");
    }
}
function clearSessionData() {
    jQuery.ajax({
        type: "GET",
        url: "/OtherPartyPaymentConf/clearSession",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            if (result.login == true) {
                if (result.success == true) {
                    loadPayModesTypes("CS");
                    loadOtherPartyReceipts();
                }
            } else {
                Logout();
            }
        }
    });
}