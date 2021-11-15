/// <reference path="LogSheet.js" />
jQuery(document).ready(function () {
    clearFormValues();
    jQuery("#TLH_DT").val(getFormatedDate(new Date()));
    jQuery('#TLH_ST_DT').datetimepicker({ minDate: new Date(), dateFormat: "dd/M/yy", timeFormat: "HH:mm" });//{ minDate: new Date(), dateFormat: "dd/M/yy", timeFormat: "hh:mm tt" }
    jQuery('#TLH_ED_DT').datetimepicker({ minDate: new Date(), dateFormat: "dd/M/yy", timeFormat: "HH:mm" });//{ minDate: new Date(), dateFormat: "dd/M/yy", timeFormat: "hh:mm tt" }
    jQuery('#TLH_ST_DT').val(my_date_format_with_time(new Date()));
    jQuery('#TLH_ED_DT').val(my_date_format_with_time(new Date()));


    jQuery("#TLH_COM").on("keydown", function (evt) {
        if (evt.keyCode == 113) {
            var headerKeys = Array()
            headerKeys = ["Row", "Company", "PC"];
            field = "facComSearch"
            var x = new CommonSearch(headerKeys, field);
        }
    })
    jQuery(".log-com-search").click(function () {
        var headerKeys = Array()
        headerKeys = ["Row", "Company", "PC"];
        field = "facComSearch"
        var x = new CommonSearch(headerKeys, field);
    });

    jQuery("#TLH_LOG_NO").focusout(function () {
       logNumFocusOut(jQuery(this).val());
    });

    jQuery("#TLH_LOG_NO").on("keydown", function (evt) {
        var selCompany = jQuery("#TLH_COM").val();
        var profCen = jQuery("#TLH_PC").val();
        if (selCompany != "" && profCen != "") {
            if (evt.keyCode == 113) {
                var headerKeys = Array()
                headerKeys = ["Row", "Log Number", "Enquiry", "Customer", "Driver", "Vehicle"];
                field = "logSheetSearch";
                data = { selCompany: selCompany, profCen: profCen }
                var x = new CommonSearch(headerKeys, field, data);
            } else if (evt.keyCode == 13) {
                logNumFocusOut(jQuery(this).val());
            }
        } else {
            setInfoMsg("Please select company.");
        }
    })
    jQuery(".log-num-search").click(function () {
        var selCompany = jQuery("#TLH_COM").val();
        var profCen = jQuery("#TLH_PC").val();
        if (selCompany != "" && profCen != "") {
            var headerKeys = Array()
            headerKeys = ["Row", "Log Number", "Enquiry", "Customer", "Driver", "Vehicle"];
            field = "logSheetSearch";
            data = { selCompany: selCompany, profCen: profCen }
            var x = new CommonSearch(headerKeys, field, data);
        } else {
            setInfoMsg("Please select company.");
        }
    });

    jQuery("#TLH_REQ_NO").focusout(function () {
        logEnqFocusOut(jQuery(this).val());
    });

    jQuery("#TLH_REQ_NO").on("keydown", function (evt) {
        if (evt.keyCode == 113) {
            var headerKeys = Array()
            headerKeys = ["Row", "Enquiry", "Refference Num", "Customer Code", "Name", "Address"];
            field = "logEnqSearch";
            var x = new CommonSearch(headerKeys, field);
        } else if (evt.keyCode == 13) {
            logEnqFocusOut(jQuery(this).val());
        }
    })
    jQuery(".enq-id-search").click(function () {
        var headerKeys = Array()
        headerKeys = ["Row", "Enquiry", "Refference Num", "Customer Code", "Name", "Address"];
        field = "logEnqSearch";
        var x = new CommonSearch(headerKeys, field);
    });

    jQuery("#TLH_CUS_CD").on("keydown", function (evt) {
        if (evt.keyCode == 113) {
            var headerKeys = Array()
            headerKeys = ["Row", "Code", "Name", "Address1", "Address2", "Nic", "Mobile", "Br No", "VAT Reg"];
            field = "logCusSearch";
            var x = new CommonSearch(headerKeys, field);
        }
    })
    jQuery(".log-cus-search").click(function () {
        var headerKeys = Array()
        headerKeys = ["Row", "Code", "Name", "Address1", "Address2", "Nic", "Mobile", "Br No", "VAT Reg"];
        field = "logCusSearch";
        var x = new CommonSearch(headerKeys, field);
    });
    jQuery("#TLH_DRI_CD").on("keydown", function (evt) {
        if (evt.keyCode == 113) {
            var headerKeys = Array()
            headerKeys = ["Row", "Epf No", "First Name", "Last Name", "MOBILE", "NIC"];
            field = "logDriSearch";
            var x = new CommonSearch(headerKeys, field);
        }
    })
    jQuery(".dri-cd-search").click(function () {
        var headerKeys = Array()
        headerKeys = ["Row", "Epf No", "First Name", "Last Name", "MOBILE", "NIC"];
        field = "logDriSearch";
        var x = new CommonSearch(headerKeys, field);
    });
    jQuery("#TLH_FLEET").on("keydown", function (evt) {
        if (evt.keyCode == 113) {
            var headerKeys = Array()
            headerKeys = ["Row", "Reg No", "Brand", "Model", "Vehicle Type", "Owner", "Owner Contact"];
            field = "logVehiSearch";
            var x = new CommonSearch(headerKeys, field);
        }
    })
    jQuery(".fleet-search").click(function () {
        var headerKeys = Array()
        headerKeys = ["Row", "Reg No", "Brand", "Model", "Vehicle Type", "Owner", "Owner Contact"];
        field = "logVehiSearch";
        var x = new CommonSearch(headerKeys, field);
    });

    jQuery("#CostChargCode").focusout(function () {
        logChgCdFocusOut(jQuery(this).val());
    });

    jQuery("#CostChargCode").on("keydown", function (evt) {
        var type = jQuery("input[name='travel-type']:checked");
        if (typeof jQuery(type).val() != "undefined") {
            if (jQuery(type).val() == "M") {
                if (evt.keyCode == 113) {
                    var headerKeys = Array()
                    headerKeys = ["Row", "Code", "Description", "Service By", "From Date", "To Date", "Rate"];
                    field = "logChargCdearch"
                    data = "MSCELNS";
                    var x = new CommonSearch(headerKeys, field, data);
                } else if (evt.keyCode == 13) {
                    logChgCdFocusOut(jQuery(this).val());
                }
            }
            else if (jQuery(type).val() == "T") {
                if (evt.keyCode == 113) {
                    var headerKeys = Array()
                    headerKeys = ["Row", "Code", "Description", "Service By", "From Date", "To Date", "From", "To", "Rate", "Class", "Vehicle"];
                    data = "TRANS";
                    field = "logChrgCdeSrchTrans";
                    var x = new CommonSearch(headerKeys, field, data);
                } else if (evt.keyCode == 13) {
                    logChgCdFocusOut(jQuery(this).val());
                }
            }
        } else {
            setInfoMsg("Please select travel type.");
        }
        
    })
    jQuery(".chrg-data-cd-search").click(function () {
        var type = jQuery("input[name='travel-type']:checked");
        if (typeof jQuery(type).val() != "undefined") {
            if (jQuery(type).val() == "M") {
                var headerKeys = Array()
                headerKeys = ["Row", "Code", "Description", "Service By", "From Date", "To Date", "Rate"];
                field = "logChargCdearch"
                data = "MSCELNS";
                var x = new CommonSearch(headerKeys, field, data);
            } else if (jQuery(type).val() == "T") {
                var headerKeys = Array()
                headerKeys = ["Row", "Code", "Description", "Service By", "From Date", "To Date", "From", "To", "Rate", "Class", "Vehicle"];
                data = "TRANS";
                field = "logChrgCdeSrchTrans";
                var x = new CommonSearch(headerKeys, field, data);
            }
        } else {
            setInfoMsg("Please select travel type.");
        }
       
    });
    jQuery("#unitrte").on("input", function () {
        if (jQuery("#CostChargCode").val() != "") {
            if (jQuery("#quantity").val() != "") {
                if (jQuery(this).val() != "") {
                    if (validNumber(jQuery(this).val())) {
                        updateChargAmounts();
                    } else {
                        setInfoMsg("Invalid quatity.");
                    }
                }
            } else {
                setInfoMsg("Please enter quantity.");
            }
        } else {
            setInfoMsg("Please select charge code first.");
        }

    });
    jQuery("#quantity").on("input", function () {
        if (jQuery("#CostChargCode").val() != "") {
            if (jQuery(this).val() != "") {
                if (validNumber(jQuery(this).val())) {
                    updateChargAmounts();
                } else {
                    setInfoMsg("Invalid quatity.");
                }
            }
        } else {
            setInfoMsg("Please select charge code first.");
        }

    });
    jQuery("#disrate").on("input", function () {
        if (jQuery("#CostChargCode").val() != "") {
            if (jQuery("#quantity").val() != "") {
                if (validNumber(jQuery(this).val())) {
                    if (jQuery("#unitamount").val() != "") {
                        var unotAmt = jQuery("#unitamount").val();
                        var disRate = jQuery(this).val();
                        var disAmt = parseFloat(unotAmt) * parseFloat(disRate) / 100;
                        jQuery("#disamount").val(disAmt.toFixed(2));
                        updateChargAmounts();
                    } else {
                        setInfoMsg("Invalid unit amount.")
                    }


                } else {
                    setInfoMsg("Invalid quatity.");
                    jQuery(this).val("");
                }
            }
        } else {
            setInfoMsg("Please select charge code first.");
        }

    });

    jQuery("#disamount").on("input", function () {
        if (jQuery("#CostChargCode").val() != "") {
            if (jQuery("#quantity").val() != "") {
                if (validNumber(jQuery(this).val())) {
                    if (jQuery("#unitamount").val() != "") {
                        var unotAmt = jQuery("#unitamount").val();
                        var disAmt = jQuery(this).val();
                        var disRte = (parseFloat(disAmt) / parseFloat(unotAmt)) * 100;
                        jQuery("#disrate").val(disRte.toFixed(4));
                        updateChargAmounts();
                    } else {
                        setInfoMsg("Invalid unit amount.");
                        jQuery("#unitamount").val("");
                    }
                } else {
                    setInfoMsg("Invalid quatity.");
                    jQuery("#quantity").val("");
                }
            }
        } else {
            setInfoMsg("Please select charge code first.");
        }

    });

    jQuery(".add-log-charg-data").click(function () {

        var chgCd = jQuery("#CostChargCode").val();
        var description = jQuery("#description").val();
        var rteType = jQuery("#rtetype").val();
        var qty = jQuery("#quantity").val();
        var unitrate = jQuery("#unitrte").val();
        var tax = jQuery("#tax").val();
        var disRate = jQuery("#disrate").val();
        var disAmt = jQuery("#disamount").val();
        var isDri = "false";
        var isCus = "false";
        if (jQuery('#isdriver').is(":checked")) {
            isDri = "true";
        }
        if (jQuery('#iscustomer').is(":checked")) {
            isCus = "true";
        }
        updatechargcodescom(chgCd, description, rteType, qty, unitrate, tax, disRate, disAmt, isDri, isCus);
    });

    jQuery(".btn-log-sheet-clear").click(function () {
        Lobibox.confirm({
            msg: "Do you want to clear ?",
            callback: function ($this, type, ev) {
                if (type == "yes") {
                    clearFormValues();
                }
            }
        });
    });

    jQuery(".btn-log-sheet-save").click(function () {
        Lobibox.confirm({
            msg: "Do you want to save log sheet ?",
            callback: function ($this, type, ev) {
                if (type == "yes") {
                    var formdata = jQuery("#frm-logsheet").serialize();
                    jQuery.ajax({
                        type: "GET",
                        url: "/LogSheet/saveLogSheet",
                        data: formdata,
                        contentType: "application/json;charset=utf-8",
                        dataType: "json",
                        success: function (result) {
                            if (result.login == true) {
                                if (result.success == true) {
                                    clearFormValues();
                                    setSuccesssMsg(result.msg);
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
        });
    });

    //check numbers and decimal  only
    jQuery('#TLH_INV_MIL,#TLH_DRI_MIL,#TLH_MET_IN,#TLH_MET_OUT,#quantity,#unitrte,#disrate,#disamount').on("input", function (event) {
        if (!jQuery.isNumeric(this.value)) {
            this.value = "";
        }
        if (parseFloat(this.value) < 0) {
            this.value = "";
        }

    });
    jQuery('#TLH_INV_MIL,#TLH_DRI_MIL,#TLH_MET_IN,#TLH_MET_OUT,#quantity,#unitrte,#disrate,#disamount').keypress(function (event) {
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

});
function logChgCdFocusOut(chgCd) {
    var type = jQuery("input[name='travel-type']:checked");
    var chgTyp=jQuery(type).val();
    jQuery.ajax({
        type: "GET",
        url: "/LogSheet/chargCodeDetail",
        data: { chgCd: chgCd, chgTyp: chgTyp },
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            if (result.login == true) {
                if (result.success == true) {
                    if (chgTyp == "M") {
                        jQuery("#description").val(result.data.SSM_DESC);
                        jQuery("#rtetype").val(result.data.SSM_RT_TP);
                        jQuery("#unitrte").val(result.data.SSM_RT);
                        jQuery("#tax").val(result.data.SSM_TAX_RT);
                        if (jQuery("#quantity").val() != "") {
                            jQuery("#unitamount").val((parseFloat(result.data.SSM_RT) * parseFloat(jQuery("#quantity").val())).toFixed(2));
                        }
                        jQuery("#quantity").focus();
                    } else if (chgTyp == "T") {
                        jQuery("#description").val(result.data.STC_DESC);
                        jQuery("#rtetype").val(result.data.STC_RT_TP);
                        jQuery("#unitrte").val(result.data.STC_RT);
                        jQuery("#tax").val(result.data.STC_TAX_RT);
                        if (jQuery("#quantity").val() != "") {
                            jQuery("#unitamount").val((parseFloat(result.data.STC_RT) * parseFloat(jQuery("#quantity").val())).toFixed(2));
                        }
                        jQuery("#quantity").focus();
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
function logEnqFocusOut(enqId) {
    if (enqId != "") {
        clearFormValues();
        jQuery.ajax({
            type: "GET",
            url: "/LogSheet/getEnquiryData",
            data: { enqId: enqId },
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            success: function (result) {
                if (result.login == true) {
                    if (result.success == true) {
                        setEnquiryData(result.data)
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
function logNumFocusOut(logNum) {
    if (logNum != "") {
        var selCompany = jQuery("#TLH_COM").val();
        var profCen = jQuery("#TLH_PC").val();
        jQuery.ajax({
            type: "GET",
            url: "/LogSheet/getLogDetails",
            data: { logNum: logNum, selCompany: selCompany, profCen: profCen },
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            success: function (result) {
                if (result.login == true) {
                    if (result.success == true) {
                        //clearFormValues();
                        updateTable(result.oMainItems);
                        updateHEdVal(result.oheader);
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

function updateChargAmounts() {
    if (jQuery("#CostChargCode").val() != "") {
        if (jQuery("#quantity").val() != "") {
            if (validNumber(jQuery("#quantity").val())) {
                if (jQuery("#disrate").val() != "") {
                    if (validNumber(jQuery("#disamount").val())) {
                        var qty = jQuery("#quantity").val();
                        //var dis = jQuery("#disamount").val();
                        var unitRate = jQuery("#unitrte").val();

                        var disRate = jQuery("#disrate").val();
                        var dis = (parseFloat(unitRate) * parseFloat(qty)) * parseFloat(disRate) / 100;
                        jQuery("#disamount").val(dis);
                        var unitamount = (parseFloat(unitRate) * parseFloat(qty)).toFixed(2);
                        jQuery("#unitamount").val(unitamount);

                        var totalval = (parseFloat(unitamount) - parseFloat(dis)).toFixed(2);
                        if (jQuery("#tax").val() != "") {
                            if (validNumber(jQuery("#tax").val())) {
                                totalval = parseFloat(totalval) +(parseFloat(totalval))* parseFloat(jQuery("#tax").val())/100;
                            } else {
                                setInfoMsg("Invalid tax amount.");
                                jQuery("#tax").val("");
                            }
                        }
                        jQuery("#totalval").val(totalval);

                    } else {
                        setInfoMsg("Invalid discount entered.");
                        jQuery("#disamount").val("");
                        jQuery("#disrate").val("");
                    }

                } else {
                    var qty = jQuery("#quantity").val();
                    var unitRate = jQuery("#unitrte").val();

                    var unitamount = (parseFloat(unitRate) * parseFloat(qty)).toFixed(2);
                    jQuery("#unitamount").val(unitamount);

                    var totalval = (parseFloat(unitamount)).toFixed(2);
                    if (jQuery("#tax").val() != "") {
                        if (validNumber(jQuery("#tax").val())) {
                            totalval = parseFloat(totalval) + parseFloat(totalval)*parseFloat(jQuery("#tax").val())/100;
                        } else {
                            setInfoMsg("Invalid tax amount.");
                            jQuery("#tax").val("");
                        }
                    }
                    jQuery("#totalval").val(totalval);
                }
            } else {
                setInfoMsg("Invalid quatity.");
            }
        }
    } else {
        setInfoMsg("Please select charge code first.");
    }
}

function clearChargValues() {
    jQuery("#CostChargCode").val("");
    jQuery("#description").val("");
    jQuery("#quantity").val("");
    jQuery("#rtetype").val("");
    jQuery("#unitrte").val("");
    jQuery("#unitamount").val("");
    jQuery("#tax").val("");
    jQuery("#disrate").val("");
    jQuery("#disamount").val("");
    jQuery("#totalval").val("");
    jQuery('#isdriver').prop('checked', false);
    jQuery('#iscustomer').prop('checked', false);
}

function clearFormValues() {
    clearChargValues();
    clearSession()
    jQuery("#TLH_DT").val(getFormatedDate(new Date()));
    jQuery('#TLH_ST_DT').val(my_date_format_with_time(new Date()));
    jQuery('#TLH_ED_DT').val(my_date_format_with_time(new Date()));
    jQuery("#TLH_COM").val("");
    jQuery("#TLH_PC").val("");
    jQuery("#TLH_LOG_NO").val("");
    jQuery("#TLH_REQ_NO").val("");
    jQuery("#TLH_CUS_CD").val("");
    jQuery("#TLH_DRI_CD").val("");
    jQuery("#TLH_GUST").val("");
    jQuery("#TLH_FLEET").val("");
    jQuery("#TLH_RMK").val("");
    jQuery("#TLH_INV_MIL").val("");
    jQuery("#TLH_DRI_MIL").val("");
    jQuery("#TLH_MET_IN").val("");
    jQuery("#TLH_MET_OUT").val("");
    jQuery("#TLH_SEQ").val("");
    jQuery('.log-charge-table .new-row').remove();
}

function clearSession() {
    jQuery.ajax({
        type: "GET",
        url: "/LogSheet/clearValues",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            if (result.login == true) {
                if (result.success == true) {
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
function updateHEdVal(oHeader) {
    if (oHeader != null) {
        jQuery("#TLH_DT").val(getFormatedDateInput(oHeader.TLH_DT));
        jQuery('#TLH_ST_DT').val(my_date_format_with_time(convertDate(oHeader.TLH_ED_DT)));
        jQuery('#TLH_ED_DT').val(my_date_format_with_time(convertDate(oHeader.TLH_ST_DT)));
        jQuery("#TLH_COM").val(oHeader.TLH_COM);
        jQuery("#TLH_PC").val(oHeader.TLH_PC);
        jQuery("#TLH_LOG_NO").val(oHeader.TLH_LOG_NO);
        jQuery("#TLH_REQ_NO").val(oHeader.TLH_REQ_NO);
        jQuery("#TLH_CUS_CD").val(oHeader.TLH_CUS_CD);
        jQuery("#TLH_DRI_CD").val(oHeader.TLH_DRI_CD);
        jQuery("#TLH_GUST").val(oHeader.TLH_GUST);
        jQuery("#TLH_FLEET").val(oHeader.TLH_FLEET);
        jQuery("#TLH_RMK").val(oHeader.TLH_RMK);
        jQuery("#TLH_INV_MIL").val(oHeader.TLH_INV_MIL);
        jQuery("#TLH_DRI_MIL").val(oHeader.TLH_DRI_MIL);
        jQuery("#TLH_MET_IN").val(oHeader.TLH_MET_IN);
        jQuery("#TLH_MET_OUT").val(oHeader.TLH_MET_OUT);
        jQuery("#TLH_SEQ").val(oHeader.TLH_SEQ);
    }
}
function updateTable(oMainItems) {
    jQuery('.log-charge-table .new-row').remove();
    if (oMainItems != null) {
        for (i = 0; i < oMainItems.length; i++) {
            var cusimg = "";
            var driimg = "";
            if (oMainItems[i].TLD_IS_CUS == "1") {
                cusimg = "/Resources/images/True.png";
            } else {
                cusimg = "/Resources/images/False.png";
            }
            if (oMainItems[i].TLD_IS_DRI == "1") {
                driimg = "/Resources/images/True.png";
            } else {
                driimg = "/Resources/images/False.png";
            }
            jQuery('.log-charge-table').append('<tr class="new-row">' +
                    '<td>' + oMainItems[i].TLD_CHR_CD + '</td>' +
                    '<td>' + oMainItems[i].TLD_CHR_DESC + '</td>' +
                    '<td class="text-left-align">' + oMainItems[i].TLD_QTY + '</td>' +
                    '<td>' + oMainItems[i].TLD_RT_TP + '</td>' +
                    '<td class="text-left-align">' + oMainItems[i].TLD_U_RT + '</td>' +
                    '<td class="text-left-align">' + addCommas(oMainItems[i].TLD_U_AMT) + '</td>' +
                    '<td class="text-left-align">' + addCommas(oMainItems[i].TLD_TAX) + '</td>' +
                    '<td class="text-left-align">' + oMainItems[i].TLD_DIS_RT + '</td>' +
                    '<td class="text-left-align">' + addCommas(oMainItems[i].TLD_DIS_AMT) + '</td>' +
                    '<td class="text-left-align">' + addCommas(oMainItems[i].TLD_TOT) + '</td>' +
                     '<td style="text-align:center;"><img class="right-img" src="' + cusimg + '"></td>' +
                    '<td style="text-align:center;"><img class="right-img" src="' + driimg + '"></td>' +
                    '<td style="text-align:center;"><img class="delete-img remove-log-chg-cls" src="/Resources/images/Remove.png"></td>' +
                    '</tr>');
        }
         removelogChgClickFunction()
    }


}
function removelogChgClickFunction() {

    jQuery(".remove-log-chg-cls").click(function () {
        var td = jQuery(this);
        var value = jQuery(td).closest("tr").find('td:eq(0)').text();
        Lobibox.confirm({
            msg: "Do you want to remove this ?",
            callback: function ($this, type, ev) {
                if (type == "yes") {
                    jQuery.ajax({
                        type: "GET",
                        url: "/LogSheet/removeChargeItems?chargCode=" + value,
                        contentType: "application/json;charset=utf-8",
                        dataType: "json",
                        success: function (result) {
                            if (result.login == true) {
                                if (result.success == true) {
                                    updateTable(result.oMainItems)
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
        });
    });


}
function setEnquiryData(data) {
    jQuery("#TLH_REQ_NO").val(data.GCE_ENQ_ID);
    jQuery("#TLH_FLEET").val(data.GCE_FLEET);
    jQuery("#TLH_CUS_CD").val(data.GCE_CUS_CD);
    jQuery("#TLH_ST_DT").val(my_date_format_with_time(convertDate(data.GCE_EXPECT_DT)));
    jQuery("#TLH_ED_DT").val(my_date_format_with_time(convertDate(data.GCE_RET_DT)));
    jQuery("#TLH_DRI_CD").val(data.GCE_DRIVER);
    if (data.CHARGER_VALUE != null) {
        updateChargCodes(data.CHARGER_VALUE);
    }
}
function updateChargCodes(oMainItems) {
    jQuery('.log-charge-table .new-row').remove();
    if (oMainItems != null) {
        for (i = 0; i < oMainItems.length; i++) {
            var cusimg = "";
            var driimg = "";
            if (oMainItems[i].TLD_IS_CUS == "1") {
                cusimg = "/Resources/images/True.png";
            } else {
                cusimg = "/Resources/images/False.png";
            }
            if (oMainItems[i].TLD_IS_DRI == "1") {
                driimg = "/Resources/images/True.png";
            } else {
                driimg = "/Resources/images/False.png";
            }
            var rtetyp = "";
            var item = oMainItems[i];
            if (oMainItems[i].Sad_itm_cd != "") {
                jQuery.ajax({
                    type: "GET",
                    url: "/CostingSheet/loadChargCode",
                    contentType: "application/json;charset=utf-8",
                    data: { code: oMainItems[i].Sad_itm_cd, service: "TRANS" },
                    dataType: "json",
                    success: function (result) {
                        if (result.login == true) {
                            if (result.success == true) {
                                updateTableFrmEnq(item, cusimg, driimg, result.data.STC_RT_TP);
                            } else {
                                updateTableFrmEnq(item, cusimg, driimg, "");
                            }
                        } else {
                            Logout();
                        }
                    }
                });
            }
            
        }
        removelogChgClickFunction()
    }

}
function updateTableFrmEnq(oMainItems, cusimg, driimg, type) {
    jQuery('.log-charge-table').append('<tr class="new-row">' +
    '<td>' + oMainItems.Sad_itm_cd + '</td>' +
    '<td>' + oMainItems.Sad_alt_itm_desc + '</td>' +
    '<td class="text-left-align">' + oMainItems.Sad_qty + '</td>' +
    '<td>' + type + '</td>' +
    '<td class="text-left-align">' + oMainItems.Sad_unit_rt + '</td>' +
    '<td class="text-left-align">' + addCommas(oMainItems.Sad_unit_amt) + '</td>' +
    '<td class="text-left-align">' + addCommas(oMainItems.Sad_itm_tax_amt) + '</td>' +
    '<td class="text-left-align">' + oMainItems.Sad_disc_rt + '</td>' +
    '<td class="text-left-align">' + addCommas(oMainItems.Sad_disc_amt) + '</td>' +
    '<td class="text-left-align">' + addCommas(oMainItems.Sad_tot_amt) + '</td>' +
        '<td style="text-align:center;"><img class="right-img" src="' + cusimg + '"></td>' +
    '<td style="text-align:center;"><img class="right-img" src="' + driimg + '"></td>' +
    '<td style="text-align:center;"><img class="delete-img remove-log-chg-cls" src="/Resources/images/Remove.png"></td>' +
    '</tr>');
    updatechargcodescom(oMainItems.Sad_itm_cd, oMainItems.Sad_alt_itm_desc, type, oMainItems.Sad_qty, oMainItems.Sad_unit_rt, oMainItems.Sad_itm_tax_amt, oMainItems.Sad_disc_rt, oMainItems.Sad_disc_amt, "false", "true");
}
function updatechargcodescom(chgCd,description,rteType,qty,unitrate,tax,disRate,disAmt,isDri,isCus){
    jQuery.ajax({
        type: "GET",
        url: "/LogSheet/updateChargeCodes",
        data: { chgCd: chgCd, description: description, rteType: rteType, qty: qty, unitrate: unitrate, tax: tax, disRate: disRate, disAmt: disAmt, isDri: isDri, isCus: isCus },
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            if (result.login == true) {
                if (result.success == true) {
                    clearChargValues();
                    updateTable(result.oMainItems)
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