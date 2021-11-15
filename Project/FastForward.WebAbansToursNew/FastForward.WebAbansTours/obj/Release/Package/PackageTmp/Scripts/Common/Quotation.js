jQuery(document).ready(function () {
    clearCostingForm();
    jQuery("#QCH_CUS_CD").val("");
    jQuery("#QCH_REF").val("");
    jQuery("#QCH_TOT_PAX").val("");
    jQuery(".send-cus-sms").hide();

    if (jQuery("#QCH_OTH_DOC").val() != "") {
        getCostingSheetEnquiryDetails(jQuery("#QCH_OTH_DOC").val());
    }
    if (jQuery("#CostService").length > 0) {
        jQuery.ajax({
            type: "GET",
            url: "/Quotation/loadServiceTypes",
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            success: function (result) {
                if (result.login == true) {
                    var select = document.getElementById("CostService");
                    jQuery("#CostService").empty();
                    var options = [];
                    var option = document.createElement('option');
                    if (result.success == true) {
                        if (result.data != null && result.data.length != 0) {
                            for (var i = 0; i < result.data.length; i++) {
                                option.text = result.data[i].MCC_DESC;
                                option.value = result.data[i].MCC_CD;
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
                        setError(result.msg);
                    }
                } else {
                    Logout();
                }
            }

        });
    }
    if (jQuery("#CostCurrency").length > 0) {
        jQuery.ajax({
            type: "GET",
            url: "/Quotation/loadCurrencyCode",
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            success: function (result) {
                if (result.login == true) {
                    if (result.success == true) {
                        var select = document.getElementById("CostCurrency");
                        jQuery("#CostCurrency").empty();
                        var options = [];
                        var option = document.createElement('option');
                        if (result.data != null && result.data.length != 0) {
                            for (var i = 0; i < result.data.length; i++) {
                                option.text = result.data[i].Mcr_cd;
                                option.value = result.data[i].Mcr_cd;
                                options.push(option.outerHTML);
                            }
                        } else {
                            option.text = "-Select-";
                            option.value = "";
                            options.push(option.outerHTML);
                        }
                        select.insertAdjacentHTML('beforeEnd', options.join('\n'));
                        jQuery("#CostCurrency").val("LKR");
                        CurrencyCodeChange(jQuery("#CostCurrency").val(), jQuery("#CostChargCode").val(), jQuery("#CostFare").val(), jQuery("#CostPax").val(), jQuery("#CostMarkup").val(), jQuery("#CostService").val());
                    } else {
                        option.text = "-Select-";
                        option.value = "";
                        options.push(option.outerHTML);
                        setError(result.msg);
                    }
                } else {
                    Logout();
                }
            }

        });
    }
    //jQuery('#QCH_DT').datepicker({ dateFormat: "dd/M/yy" });
    jQuery('#QCH_DT').val(getFormatedDate(new Date()));
    jQuery("#CostCurrency").on("change", function () {
        CurrencyCodeChange(jQuery(this).val(), jQuery("#CostChargCode").val(), jQuery("#CostFare").val(), jQuery("#CostPax").val(), jQuery("#CostMarkup").val(), jQuery("#CostService").val());
    });

    function CurrencyCodeChange(code, chargeCd, fare, pax, mrkUpMain, service) {
        jQuery.ajax({
            type: "GET",
            url: "/Quotation/currencyCodeChange",
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            data: { code: code, chargeCd: chargeCd, fare: fare, pax: pax, mrkUpMain: mrkUpMain, service: service },
            success: function (result) {
                if (result.login == true) {
                    if (result.success == true) {
                        jQuery(".currancy-rate-amt").empty();
                        jQuery(".currancy-rate-amt").html(result.ItemExRate);
                        //if (result.spVal == false) {
                        //    setInfoMsg(result.msg);
                        //} else {

                        //} ////////////still cannot change the currency code it will set for selected chage code

                    } else {
                        jQuery(".currancy-rate-amt").empty();
                        jQuery(".currancy-rate-amt").html("0.00");
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

    jQuery("#CostChargCode").on("keydown", function (evt) {
        var data = jQuery("#CostService").val();
        if (evt.keyCode == 113) {
            if (jQuery("#CostService").val() == "TRANS") {
                var headerKeys = Array()
                headerKeys = ["Row", "Code", "Description", "Service By", "From Date", "To Date", "From", "To", "Rate", "Class", "Vehicle"];
                field = "costChrgCdeSrchTrans";
                var x = new CommonSearch(headerKeys, field, data);
            } else if (jQuery("#CostService").val() == "AIRTVL") {
                var headerKeys = Array()
                headerKeys = ["Row", "Code", "Description", "Service By", "From", "To", "Rate", "From Date", "To Date"];
                field = "costChrgCdeSrchArival";
                var x = new CommonSearch(headerKeys, field, data);
            } else if (jQuery("#CostService").val() == "MSCELNS") {
                var headerKeys = Array()
                headerKeys = ["Row", "Code", "Description", "Service By", "From Date", "To Date", "Rate"];
                field = "costChrgCdeSrchMsclens";
                var x = new CommonSearch(headerKeys, field, data);
            } else {
                var headerKeys = Array()
                headerKeys = ["Row", "Code", "Description", "Service By", "From Date", "To Date", "Rate"];
                field = "costChrgCdeSrchMsclens";
                var x = new CommonSearch(headerKeys, field, data);
            }
        }
        if (evt.keyCode == 13) {
            if (jQuery(this).val() != "") {
                if (jQuery("#CostService").val() != "") {
                    getChargCodeDetls(jQuery(this).val(), jQuery("#CostService").val());
                } else {
                    setInfoMsg("Please select service.");
                }
            }
        }

    });
    jQuery("#CostChargCode").focusout(function () {
        if (jQuery(this).val() != "") {
            if (jQuery("#CostService").val() != "") {
                getChargCodeDetls(jQuery(this).val(), jQuery("#CostService").val());
            } else {
                setInfoMsg("Please select service.");
                jQuery("#CostChargCode").val("");
            }

        }
    });
    jQuery(".chrg-data-cd-search").click(function () {
        var data = jQuery("#CostService").val();
        if (jQuery("#CostService").val() == "TRANS") {
            var headerKeys = Array()
            headerKeys = ["Row", "Code", "Description", "Service By", "From Date", "To Date", "From", "To", "Rate", "Class", "Vehicle"];
            field = "costChrgCdeSrchTrans"
            var x = new CommonSearch(headerKeys, field, data);
        } else if (jQuery("#CostService").val() == "AIRTVL") {
            var headerKeys = Array()
            headerKeys = ["Row", "Code", "Description", "Service By", "From", "To", "Rate", "From Date", "To Date"];
            field = "costChrgCdeSrchArival";
            var x = new CommonSearch(headerKeys, field, data);
        } else if (jQuery("#CostService").val() == "MSCELNS") {
            var headerKeys = Array()
            headerKeys = ["Row", "Code", "Description", "Service By", "From Date", "To Date", "Rate"];
            field = "costChrgCdeSrchMsclens";
            var x = new CommonSearch(headerKeys, field, data);
        } else {
            var headerKeys = Array()
            headerKeys = ["Row", "Code", "Description", "Service By", "From Date", "To Date", "Rate"];
            field = "costChrgCdeSrchMsclens";
            var x = new CommonSearch(headerKeys, field, data);
        }

    });
    function getChargCodeDetls(code, service) {
        if (code != "" && service != "") {
            jQuery.ajax({
                type: "GET",
                url: "/Quotation/loadChargCode",
                data: { code: code, service: service },
                contentType: "application/json;charset=utf-8",
                dataType: "json",
                success: function (result) {
                    if (result.login == true) {
                        if (result.success == true) {

                            if (service == "TRANS") {
                                jQuery("#CostCurrency").val(result.data.STC_CURR);
                                CurrencyCodeChange(result.data.STC_CURR, jQuery("#CostChargCode").val(), jQuery("#CostFare").val(), jQuery("#CostPax").val(), jQuery("#CostMarkup").val(), jQuery("#CostService").val());
                                jQuery("#CostTax").val(result.data.STC_TAX_RT);
                                if (jQuery("#CostPax").val() == "") {
                                    jQuery("#CostFare").val(result.data.STC_RT);
                                } else {
                                    //var cost = parseFloat(jQuery("#CostPax").val()) * parseFloat(result.data.STC_RT);
                                    jQuery("#CostFare").val(result.data.STC_RT);
                                    updateValue(result.data.STC_RT);
                                }

                                jQuery("#CostPax").focus();
                            } else if (service == "AIRTVL") {
                                jQuery("#CostCurrency").val(result.data.SAC_CUR);
                                CurrencyCodeChange(result.data.SAC_CUR, jQuery("#CostChargCode").val(), jQuery("#CostFare").val(), jQuery("#CostPax").val(), jQuery("#CostMarkup").val(), jQuery("#CostService").val());
                                jQuery("#CostTax").val(result.data.SAC_TAX_RT);
                                if (jQuery("#CostPax").val() == "") {
                                    jQuery("#CostFare").val(result.data.SAC_RT);
                                } else {
                                    //var cost = parseFloat(jQuery("#CostPax").val()) * parseFloat(result.data.SAC_RT);
                                    jQuery("#CostFare").val(result.data.SAC_RT);
                                    updateValue(result.data.SAC_RT);
                                }
                                jQuery("#CostPax").focus();
                            } else if (service == "MSCELNS") {
                                jQuery("#CostCurrency").val(result.data.SSM_CUR);
                                CurrencyCodeChange(result.data.SSM_CUR, jQuery("#CostChargCode").val(), jQuery("#CostFare").val(), jQuery("#CostPax").val(), jQuery("#CostMarkup").val(), jQuery("#CostService").val());
                                jQuery("#CostTax").val(result.data.SSM_TAX_RT);
                                if (jQuery("#CostPax").val() == "") {
                                    jQuery("#CostFare").val(result.data.SSM_RT);
                                } else {
                                    //var cost = parseFloat(jQuery("#CostPax").val()) * parseFloat(result.data.SSM_RT);
                                    jQuery("#CostFare").val(result.data.SSM_RT);
                                    updateValue(result.data.SSM_RT);
                                }
                                jQuery("#CostPax").focus();
                            } else {
                                jQuery("#CostCurrency").val(result.data.SSM_CUR);
                                CurrencyCodeChange(result.data.SSM_CUR, jQuery("#CostChargCode").val(), jQuery("#CostFare").val(), jQuery("#CostPax").val(), jQuery("#CostMarkup").val(), jQuery("#CostService").val());
                                jQuery("#CostTax").val(result.data.SSM_TAX_RT);
                                if (jQuery("#CostPax").val() == "") {
                                    jQuery("#CostFare").val(result.data.SSM_RT);
                                } else {
                                    //var cost = parseFloat(jQuery("#CostPax").val()) * parseFloat(result.data.SSM_RT);
                                    jQuery("#CostFare").val(result.data.SSM_RT);
                                    updateValue(result.data.SSM_RT);
                                }
                                jQuery("#CostPax").focus();
                            }
                        } else {
                            jQuery("#CostFare").val("");
                            jQuery("#CostTotal").val("");
                            jQuery("#CostMarkup").val("");
                            jQuery("#CostMarkupAmount").val("");
                            jQuery("#CostTotalLkr").val("");
                            jQuery("#CostChargCode").val("");
                            if (result.type == "Error") {
                                setError(result.msg);
                            }
                            if (result.type == "Info") {
                                setInfoMsg(result.msg);
                            }
                            jQuery("#ChargCode").focus();
                        }
                    } else {
                        Logout();
                    }
                }
            });
        }
    }
    jQuery("#CostPax").on("change", function () {
        if (jQuery("#CostChargCode").val() != "") {
            if (jQuery("#QCH_TOT_PAX").val() != "") {
                if (parseFloat(jQuery(this).val()) <= parseFloat(jQuery("#QCH_TOT_PAX").val())) {
                    jQuery("#CostFare").focus();
                } else {
                    setInfoMsg("Total number of packs cannot be less than item line pax.");
                    jQuery("#CostTotal").val("");
                    jQuery("#CostTotalLkr").val("");
                    jQuery("#CostMarkup").val("");
                    jQuery("#CostMarkupAmount").val("");
                    jQuery("#CostPax").val("");
                }
            } else {
                setInfoMsg("Please enter total number of pax pax");
                jQuery("#CostPax").val("");
                jQuery("#QCH_TOT_PAX").focus();
            }
        } else {
            setInfoMsg("Please select charge code.");
            jQuery("#CostPax").val("");
            jQuery("#CostFare").val("");
            jQuery("#CostChargCode").focus();
        }
    });
    jQuery("#CostPax").on("input", function () {
        if (jQuery(this).val() != "") {
            if (jQuery("#CostFare").val() != "") {
                //if (parseFloat(jQuery(this).val() <= parseFloat(jQuery("#QCH_TOT_PAX").val()))) {
                updateValue(jQuery("#CostFare").val());
                //} else {
                //    setInfoMsg("Total number of packs cannot be less than item line pax");
                //}
            } else {
                setInfoMsg("Please update unit rate.");
                jQuery("#CostFare").focus();
            }
        } else {
            jQuery("#CostTotal").val("");
            jQuery("#CostTotalLkr").val("");
        }
    });
    jQuery("#CostFare").on("change", function () {
        if (jQuery("#CostChargCode").val() != "") {
            jQuery("#CostMarkup").focus();
        } else {
            setInfoMsg("Please select charge code.");
            jQuery("#CostPax").val("");
            jQuery("#CostFare").val("");
            jQuery("#CostChargCode").focus();
        }
    });
    jQuery("#CostFare").on("input", function () {

        if (jQuery(this).val() != "") {
            if (jQuery("#CostPax").val() != "") {
                updateValue(jQuery(this).val());
            } else {
                setInfoMsg("Please update pax.");
                jQuery("#CostPax").focus();
            }
        } else {
            jQuery("#CostTotal").val("");
            jQuery("#CostTotalLkr").val("");
        }

    });
    function updateValue(unitRt) {
        jQuery("#CostMarkup").val("");
        if (jQuery("#CostPax").val() != "") {
            var total = parseFloat(unitRt) * parseFloat(jQuery("#CostPax").val());
            total = parseFloat(total) + parseFloat(total) * parseFloat(jQuery("#CostTax").val()) / 100;
            jQuery("#CostTotal").val(total);
            var totalLkr = parseFloat(jQuery(".currancy-rate-amt").html()) * total;
            jQuery("#CostTotalLkr").val(totalLkr);
        }
    }
    jQuery("#CostMarkup").on("input", function () {
        if (jQuery("#CostChargCode").val() != "" && jQuery("#CostPax").val() != "" && jQuery("#CostFare").val() != "") {
            if (jQuery(this).val() != "") {
                if (parseFloat(jQuery(this).val()) > 0 && parseFloat(jQuery(this).val()) <= 1000) {
                    var total = parseFloat(jQuery("#CostFare").val()) * parseFloat(jQuery("#CostPax").val()) * parseFloat(jQuery(".currancy-rate-amt").html());
                    total = parseFloat(total) + parseFloat(total) * parseFloat(jQuery("#CostTax").val()) / 100;
                    var totalLkr = total + total * parseFloat(jQuery(this).val()) / 100;
                    var markupVal = total * parseFloat(jQuery(this).val()) / 100;
                    jQuery("#CostMarkupAmount").val(markupVal.toFixed(4));
                    jQuery("#CostTotalLkr").val(totalLkr);
                    //jQuery("#QCH_MARKUP").attr("disabled", true);
                } else {
                    setInfoMsg("Maximum and minimum markup value range is 100%-0%.");
                    jQuery(this).val("");
                    jQuery("#CostMarkupAmount").val("");
                }
            } else {
                updateValue(jQuery("#CostFare").val());
                jQuery("#CostMarkupAmount").val("");
                //jQuery("#QCH_MARKUP").removeAttr("disabled");
            }
        } else {
            jQuery("#CostMarkup").val("");
            jQuery("#CostMarkupAmount").val("");
        }
    });
    //check numbers and decimal  only
    jQuery('#CostMarkup,#CostMarkupAmount,#CostPax,#CostFare,#QCH_MARKUP,#QCH_MARKUP_AMT,#QCH_TOT_PAX').on("input", function (event) {
        if (!jQuery.isNumeric(this.value)) {
            this.value = "";
        }
        if (parseFloat(this.value) < 0) {
            this.value = "";
        }

    });

    jQuery('#CostMarkup,#CostMarkupAmount,#CostPax,#CostFare,#QCH_MARKUP,#QCH_MARKUP_AMT,#QCH_TOT_PAX').keypress(function (event) {
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
    jQuery("#CostService").on("change", function () {
        jQuery("#CostChargCode").val("");
        jQuery("#CostPax").val("");
        jQuery("#CostFare").val("");
        jQuery("#CostTax").val("");
        jQuery("#CostTotal").val("");
        jQuery("#CostMarkup").val("");
        jQuery("#CostMarkupAmount").val("");
        jQuery("#CostTotalLkr").val("");
        jQuery("#CostRemarks").val("");
        jQuery("#CostChargCode").focus();
    });
    jQuery(".add-cost-charg-data").click(function () {
        if (jQuery("#CostService").val() != "") {
            if (jQuery("#CostChargCode").val() != "") {
                if (jQuery("#CostPax").val() != "") {
                    if (jQuery("#CostFare").val() != "") {
                        if (jQuery("#CostRemarks").val() != "") {
                            var chgCd = jQuery("#CostChargCode").val();
                            var service = jQuery("#CostService").val();
                            var currencyCode = jQuery("#CostCurrency").val();
                            var pax = jQuery("#CostPax").val();
                            var fare = jQuery("#CostFare").val();
                            var tax = jQuery("#CostTax").val();
                            var markup = jQuery("#CostMarkup").val();
                            var markupAmt = jQuery("#CostMarkupAmount").val();
                            var remarks = jQuery("#CostRemarks").val();
                            var totalPax = jQuery("#QCH_TOT_PAX").val();
                            if (chgCd != "" && service != "" && currencyCode != "" && pax != "" && fare != "" && remarks != "" && totalPax != "") {
                                if (!jQuery.isNumeric(totalPax)) {
                                    setInfoMsg("Invalid total number of pax.");
                                } else {
                                    if (!jQuery.isNumeric(pax)) {
                                        setInfoMsg("Invalid line pax number.");
                                    } else {
                                        if (!jQuery.isNumeric(fare)) {
                                            setInfoMsg("Please enter valid cost of fare.");
                                        } else {
                                            if (markup != "") {
                                                if (!jQuery.isNumeric(markup)) {
                                                    setInfoMsg("Please enter valid markup value.");
                                                } else {
                                                    if (tax != "") {
                                                        if (!jQuery.isNumeric(tax)) {
                                                            setInfoMsg("Please enter valid tax value.");
                                                        } else {
                                                            updateChagresGrid(chgCd, service, currencyCode, pax, fare, tax, markup, markupAmt, remarks, totalPax);
                                                        }
                                                    } else {

                                                        updateChagresGrid(chgCd, service, currencyCode, pax, fare, tax, markup, markupAmt, remarks, totalPax);
                                                    }
                                                }
                                            } else {
                                                if (tax != "") {
                                                    if (!jQuery.isNumeric(tax)) {
                                                        setInfoMsg("Please enter valid tax value.");
                                                    } else {
                                                        updateChagresGrid(chgCd, service, currencyCode, pax, fare, tax, markup, markupAmt, remarks, totalPax);
                                                    }

                                                } else {
                                                    updateChagresGrid(chgCd, service, currencyCode, pax, fare, tax, markup, markupAmt, remarks, totalPax);
                                                }
                                            }
                                        }
                                    }
                                }

                            } else {
                                /////////////check here

                            }

                        } else {
                            setInfoMsg("Please enter remark.");
                        }
                    } else {
                        setInfoMsg("Add cost fare.");
                    }
                } else {
                    setInfoMsg("Add number of pax");
                }
            } else {
                setInfoMsg("Add charge code.");
            }
        } else {
            setInfoMsg("Plese select service.");
        }
    });
    function updateChagresGrid(chgCd, service, currencyCode, pax, fare, tax, markup, markupAmt, remarks, totalPax) {
        var row = jQuery(".cost-sheet-table .new-row")[0];
        var value;
        if (row == null) {
            value = currencyCode;
        } else {
            value = jQuery(row).find('td:eq(5)').text();
        }
        var serBy = jQuery("#ServiceByCus").val();
        if (value == currencyCode) {
            jQuery.ajax({
                type: "GET",
                url: "/Quotation/updateTourCharges",
                data: {
                    chgCd: chgCd,
                    service: service,
                    currencyCode: currencyCode,
                    pax: pax,
                    fare: fare,
                    tax: tax,
                    markup: markup,
                    markupAmt: markupAmt,
                    remarks: remarks,
                    totalPax: totalPax,
                    serBy: serBy
                },
                contentType: "application/json;charset=utf-8",
                dataType: "json",
                success: function (result) {
                    if (result.login == true) {
                        if (result.success == true) {
                            if (result.oMainItems.length > 0) {
                                data = result.oMainItems;
                                updateGridAndPriceValues(data);
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
            setInfoMsg("Cannot use different currecy type for charges.");
        }
    }


    jQuery("#QCH_TOT_PAX").on("input", function () {
        if (jQuery(".cost-sheet-table .new-row").length == 0) {
            jQuery("#CostPax").val(jQuery(this).val());
            if (jQuery("#CostFare").val() != "") {
                updateValue(jQuery("#CostFare").val());
            } else {
                updateValue(0);
            }
        } else {
            setInfoMsg("You cannot change total pax.");

        }
    });

    jQuery("#QCH_MARKUP").on("input", function () {
        if (jQuery(".cost-sheet-table .new-row").length > 0) {
            if (jQuery(this).val() != "") {
                var inputMrk = parseFloat(jQuery(this).val());
                var total = parseFloat(jQuery("#totalCostLKR").val())
                var grandTotal = total + (total * inputMrk / 100);
                jQuery("#QCH_TOT_VALUE").val(grandTotal);
                var packPerPerson = grandTotal / parseFloat(jQuery("#QCH_TOT_PAX").val());
                jQuery("#QuotePerPax").val(packPerPerson);
                jQuery("#CostMarkup").attr("readonly", true);
                jQuery("#CostMarkupAmount").attr("readonly", true);
            } else {
                jQuery("#QCH_MARKUP_AMT").val("");
                jQuery("#CostMarkup").removeAttr("readonly");
                jQuery("#CostMarkupAmount").removeAttr("readonly");
                jQuery("#QCH_TOT_VALUE").val(jQuery("#totalCostLKR").val());
                jQuery("#QuotePerPax").val(parseFloat(jQuery("#totalCostLKR").val()) / parseFloat(parseFloat(jQuery("#QCH_TOT_PAX").val())));
            }
        } else {
            setInfoMsg("Please add charge values.");

        }
    });

    jQuery("#QCH_MARKUP_AMT").on("input", function () {
        if (jQuery(".cost-sheet-table .new-row").length > 0) {
            if (jQuery(this).val() != "") {
                var markupAmt = parseFloat(jQuery(this).val());
                var total = parseFloat(jQuery("#totalCostLKR").val());
                var mrkPer = parseFloat(jQuery(this).val()) / total * 100;
                jQuery("#QCH_MARKUP").val(mrkPer.toFixed(4));
                var grandTotal = total + (total * mrkPer / 100);
                jQuery("#QCH_TOT_VALUE").val(grandTotal);
                var packPerPerson = grandTotal / parseFloat(jQuery("#QCH_TOT_PAX").val());
                jQuery("#QuotePerPax").val(packPerPerson);
                jQuery("#CostMarkup").attr("readonly", true);
                jQuery("#CostMarkupAmount").attr("readonly", true);
            } else {
                jQuery("#QCH_MARKUP").val("");
                jQuery("#CostMarkup").removeAttr("readonly");
                jQuery("#CostMarkupAmount").removeAttr("readonly");
                jQuery("#QCH_TOT_VALUE").val(jQuery("#totalCostLKR").val());
                jQuery("#QuotePerPax").val(parseFloat(jQuery("#totalCostLKR").val()) / parseFloat(parseFloat(jQuery("#QCH_TOT_PAX").val())));
            }
        } else {
            setInfoMsg("Please add charge values.");

        }
    });
    var first = true;
    jQuery(".qch-cuscd-add").click(function () {
        jQuery(".customer-create-popup").dialog({
            height: 580,
            width: "75%",
            resizable: false,
            draggable: false,
            //closeOnEscape: true,
            //title: "Create Customer",
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
            //,
            //buttons: {
            //    Close: function () {
            //        jQuery(this).dialog('close');
            //        clearCustomerData()
            //    }
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
        event.preventDefault();
        jQuery(this).attr("disabled", true);
        var formdata = jQuery("#customer-crte-frm").serialize();
        jQuery.ajax({
            type: 'POST',
            url: '/DataEntry/CustomerCreation',
            data: formdata,
            success: function (response) {
                if (response.success == true) {
                    document.getElementById("customer-crte-frm").reset();
                    fieldEnable();
                    clearCustomerData();
                    setSuccesssMsg(response.msg);
                    jQuery(".btn-save-data").removeAttr("disabled");
                    jQuery(".customer-create-popup").dialog('close');
                    if (response.cusCd != "") {
                        jQuery("#costing-crte-frm #QCH_CUS_CD").val(response.cusCd);
                    } else {
                        jQuery("#costing-crte-frm #QCH_CUS_CD").val(cuscd);
                    }
                } else {
                    jQuery(".btn-save-data").removeAttr("disabled");
                    if (response.type == "Error") {
                        setError(response.msg);
                    } else if (response.type == "Info") {
                        setInfoMsg(response.msg);
                    }

                }
            }
        });
        return false;
    });
    jQuery("#QCH_CUS_CD").focusout(function () {
        codeCusFocusOut(jQuery(this).val());
    });
    jQuery("#QCH_CUS_CD").on("keydown", function (evt) {
        if (evt.keyCode == 113) {
            var headerKeys = Array()
            headerKeys = ["Row", "Code", "Name", "Nic", "Mobile", "Br No"];
            field = "cusCodeForCost"
            var x = new CommonSearch(headerKeys, field);
        }
        if (evt.keyCode == 13) {
            codeCusFocusOut(jQuery(this).val());
        }
    })
    jQuery(".qch-cuscd-search").click(function () {
        var headerKeys = Array()
        headerKeys = ["Row", "Code", "Name", "Nic", "Mobile", "Br No"];
        field = "cusCodeForCost"
        var x = new CommonSearch(headerKeys, field);
    });

    function codeCusFocusOut(code) {
        if (code != "") {
            jQuery.ajax({
                type: "GET",
                url: "/Quotation/cusCodeTextChanged",
                data: { cusCd: code },
                contentType: "application/json;charset=utf-8",
                dataType: "json",
                success: function (result) {
                    if (result.login == true) {
                        if (result.success == true) {
                            if (result.success == true) {
                                if (typeof (result.local) != "undefined") {
                                    jQuery("#QCH_CUS_CD").val(result.data.Mbe_cd);
                                }
                                if (typeof (result.group) != "undefined") {
                                    jQuery("#QCH_CUS_CD").val(result.data.Mbg_cd);
                                }
                            } else {
                                jQuery("#QCH_CUS_CD").val("");
                                if (result.type == "Info") {
                                    setInfoMsg(result.msg);
                                }
                                if (result.type == "Error") {
                                    setInfoMsg(result.msg);
                                }
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
    jQuery(".btn-qtatn-clear-data").click(function () {
        Lobibox.confirm({
            msg: "Do you want to clear ?",
            callback: function ($this, type, ev) {
                if (type == "yes") {
                    clearCostingForm();
                    jQuery("#QCH_OTH_DOC").val("");
                    jQuery("#QCH_CUS_CD").val("");
                    jQuery("#QCH_REF").val("");
                    jQuery("#QCH_TOT_PAX").val("");
                }
            }
        });
    });

    jQuery("#QCH_OTH_DOC").focusout(function () {
        getCostingSheetEnquiryDetails(jQuery(this).val());
    });
    jQuery("#QCH_OTH_DOC").on("keydown", function (evt) {
        if (evt.keyCode == 113) {
            var headerKeys = Array()
            headerKeys = ["Row", "Enquiry Id", "Ref Num", "Type", "Customer Code", "Name", "Address"];
            field = "costEnqIdSrch";
            var x = new CommonSearch(headerKeys, field);
        }
        if (evt.keyCode == 13) {
            getCostingSheetEnquiryDetails(jQuery(this).val());
        }
    })
    jQuery(".costsheet-enq-search").click(function () {
        var headerKeys = Array()
        headerKeys = ["Row", "Enquiry Id", "Ref Num", "Type", "Customer Code", "Name", "Address"];
        field = "costEnqIdSrch";
        var x = new CommonSearch(headerKeys, field);
    });
    
    jQuery("#CostMarkupAmount").on("input", function () {
        if (jQuery("#CostChargCode").val() != "" && jQuery("#CostPax").val() != "" && jQuery("#CostFare").val() != "") {
            if (jQuery(this).val() != "") {
                if (parseFloat(jQuery(this).val()) > 0) {
                    var total = parseFloat(jQuery("#CostFare").val()) * parseFloat(jQuery("#CostPax").val()) * parseFloat(jQuery(".currancy-rate-amt").html());
                    var markUpPercentage = parseFloat(jQuery(this).val()) / total * 100;
                    jQuery("#CostMarkup").val(markUpPercentage.toFixed(4));
                    var totalLkr = total + parseFloat(jQuery(this).val());
                    jQuery("#CostTotalLkr").val(totalLkr);
                    //jQuery("#QCH_MARKUP").attr("disabled", true);
                }
            } else {
                updateValue(jQuery("#CostFare").val());
                //jQuery("#QCH_MARKUP").removeAttr("disabled");
            }
        } else {
            jQuery("#CostMarkup").val("");
            jQuery("#CostMarkupAmount").val("");
        }
    });
    jQuery(".btn-qtatn-save-data").click(function () {
        Lobibox.confirm({
            msg: "Do you want to save ?",
            callback: function ($this, type, ev) {
                if (type == "yes") {
                    var formdata = jQuery("#quotation-crte-frm").serialize();
                    jQuery.ajax({
                        type: "GET",
                        url: "/Quotation/saveQuotation",
                        data: formdata,
                        contentType: "application/json;charset=utf-8",
                        dataType: "json",
                        success: function (result) {
                            if (result.login == true) {
                                if (result.success == true) {
                                    setSuccesssMsg(result.msg);
                                    clearCostingForm();
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
        });
    });
    jQuery(".btn-qtatn-enq-approve").click(function () {
        Lobibox.confirm({
            msg: "Do you want to approve ?",
            callback: function ($this, type, ev) {
                if (type == "yes") {
                    var enqId = jQuery("#QCH_OTH_DOC").val();
                    var sendSms = "false";
                    if (jQuery("#SendSmsCus").is(":checked")) {
                        sendSms = "true";
                    }
                    var totVal = jQuery("#QCH_TOT_VALUE").val();
                    jQuery.ajax({
                        type: "GET",
                        url: "/Quotation/approveQuotation?enqId=" + enqId + "&sendSms=" + sendSms + "&totVal=" + totVal,
                        contentType: "application/json;charset=utf-8",
                        dataType: "json",
                        success: function (result) {
                            if (result.login == true) {
                                if (result.success == true) {
                                    setSuccesssMsg(result.msg);
                                    clearCostingForm();
                                    //location.reload();
                                    getCostingSheetEnquiryDetails(enqId);
                                    //jQuery(location).attr('href', '/Home/Index');
                                    //getCostingSheetEnquiryDetails(enqId);
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
        });

    });
    jQuery(".btn-qtatn-summary").click(function (evt) {
        evt.preventDefault();
        if (jQuery(".table.cost-sheet-table .new-row").length > 0) {

            jQuery.ajax({
                type: "GET",
                url: "/Quotation/getChargSummery",
                contentType: "application/json;charset=utf-8",
                dataType: "json",
                success: function (result) {
                    if (result.login == true) {
                        if (result.success == true) {
                            jQuery(".charge-content").empty();
                            var html = "";
                            for (i = 0; i < result.summary.length; i++) {
                                html += "<div class='col-md-12'><div class='col-md-8'><b>" + result.summary[i].name + " </b></div>" + "<div class='col-md-4'><b>:</b> " +addCommas(parseFloat(result.summary[i].amount).toFixed(2))+ "</div></div></br></br>"
                            }
                            jQuery(".charge-content").html(html);
                            jQuery("#dialog-message").dialog({
                                modal: true,
                                draggable: false,
                                resizable: false,
                                position: { my: 'top', at: 'top+150' },
                                show: 'blind',
                                hide: 'blind',
                                width: 400,
                                dialogClass: 'ui-dialog-osx',
                                buttons: {
                                    "Close": function () {
                                        jQuery(this).dialog("close");
                                    }
                                }
                            });
                        }
                    } else {
                        Logout();
                    }
                }
            });
        } else {
            setInfoMsg("No charges add for display summary.");
        }
    });
    jQuery(".btn-qtatn-enq-reset").click(function () {
        Lobibox.confirm({
            msg: "Do you want to reset approval ?",
            callback: function ($this, type, ev) {
                if (type == "yes") {
                    var enqId = jQuery("#QCH_OTH_DOC").val();
                    jQuery.ajax({
                        type: "GET",
                        url: "/Quotation/resetQuotation?enqId=" + enqId,
                        contentType: "application/json;charset=utf-8",
                        dataType: "json",
                        success: function (result) {
                            if (result.login == true) {
                                if (result.success == true) {
                                    setSuccesssMsg(result.msg);
                                    getCostingSheetEnquiryDetails(jQuery("#QCH_OTH_DOC").val());
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
        });

    });
    jQuery(".btn-qtatn-genarate-po").click(function () {
        Lobibox.confirm({
            msg: "Do you want to genarate purchase order ?",
            callback: function ($this, type, ev) {
                if (type == "yes") {
                    var costingSheetNo = jQuery(".cost-sheet-number").html();
                    if (costingSheetNo != "") {
                        jQuery.ajax({
                            type: "GET",
                            url: "/Quotation/genaratePurchaseOrder?costingSheetNo=" + costingSheetNo,
                            contentType: "application/json;charset=utf-8",
                            dataType: "json",
                            success: function (result) {
                                if (result.login == true) {
                                    if (result.success == true) {
                                        Lobibox.alert("success", {
                                            msg: result.msg,
                                            callback: function ($this, type, ev) {
                                                getCostingSheetEnquiryDetails(jQuery("#QCH_OTH_DOC").val());
                                            }
                                        });
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
                        setInfoMsg("Invalid cosing sheet number.");
                    }
                }
            }
        });

    });
});
function clearChargeDataField() {
    jQuery("#CostChargCode").val("");
    jQuery("#CostPax").val(jQuery("#QCH_TOT_PAX").val());
    jQuery("#CostFare").val("");
    jQuery("#CostTax").val("");
    jQuery("#CostTotal").val("");
    jQuery("#CostMarkup").val("");
    jQuery("#CostMarkupAmount").val("");
    jQuery("#CostTotalLkr").val("");
    jQuery("#CostRemarks").val("");
    jQuery("#CostChargCode").focus();
}
function removeChgClickFunction() {
    jQuery(".delete-img.remove-cost-mehod-cls").unbind('click').click(function (evt) {
        evt.preventDefault();
        var td = jQuery(this).parent('td');
        var tr = jQuery(td).parent('tr');
        var ser = jQuery(tr).find('td:eq(1)').html();
        var cgCd = jQuery(tr).find('td:eq(2)').html();
        var curr = jQuery(tr).find('td:eq(5)').html();
        var qty = jQuery(tr).find('td:eq(6)').html();
        var totcst = jQuery(tr).find('td:eq(12)').html().replace(",", "");
        var rmk = jQuery(tr).find('td:eq(13)').html();
        Lobibox.confirm({
            msg: "Do you want to remove ?",
            callback: function ($this, type, ev) {
                if (type == "yes") {
                    jQuery.ajax({
                        type: "GET",
                        url: "/Quotation/removeChargeItem",
                        data: { chgCd: cgCd, service: ser, currencyCode: curr, pax: qty, totcst: totcst, rmk: rmk },
                        contentType: "application/json;charset=utf-8",
                        dataType: "json",
                        success: function (result) {
                            if (result.login == true) {
                                if (result.success == true) {
                                    updateGridAndPriceValues(result.oMainItems);
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
function showHideButton(status) {
    jQuery(".btn-qtatn-save-data").hide();
    jQuery(".btn-qtatn-enq-reset").hide();
    jQuery(".btn-qtatn-enq-approve").hide();
    jQuery(".btn-qtatn-genarate-po").hide();
    jQuery(".btn-print-qtatn-sheet").hide();
    jQuery(".send-cus-sms").hide();
    if (status == 1) {
        jQuery(".btn-print-qtatn-sheet").show();
        jQuery(".btn-qtatn-enq-reset").show();
        jQuery(".btn-qtatn-genarate-po").show();
    } else if (status == 2) {
        jQuery(".btn-qtatn-save-data").show();
        jQuery(".btn-qtatn-enq-approve").show();
        jQuery(".btn-print-qtatn-sheet").show();
        jQuery(".send-cus-sms").show();
    }
    else if (status == 3) {
        jQuery(".btn-print-qtatn-sheet").show();
        jQuery(".btn-qtatn-enq-reset").show();
    } else {
        jQuery(".btn-qtatn-save-data").show();
    }
}
function getCostingSheetEnquiryDetails(enqId) {
    if (enqId != "") {
        jQuery.ajax({
            type: "GET",
            url: "/Quotation/getEnquiryDetails?enqId=" + enqId,
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            success: function (result) {
                if (result.login == true) {
                    if (result.success == true) {
                        if (typeof result.oHeader != "undefined") {
                            if (result.oHeader.QCH_SEQ != 0 && result.oHeader.QCH_COST_NO != null) {
                                setCostingValues(result.oHeader, result.oMainItems);
                                //if (result.oHeader.QCH_ACT == 1 || result.oHeader.QCH_ACT == 2 || result.oHeader.QCH_ACT == 3) {
                                //    jQuery(".btn-print-qtatn-sheet").show();
                                //} else {
                                //    jQuery(".btn-print-qtatn-sheet").hide();
                                //}
                                //if (result.oHeader.QCH_ACT == 2) {
                                //    jQuery(".btn-qtatn-enq-approve").show();
                                //    jQuery(".send-cus-sms").show();
                                //    jQuery(".btn-qtatn-save-data").show();
                                //} else {
                                //    jQuery(".btn-qtatn-enq-approve").hide();
                                //    jQuery(".send-cus-sms").hide();
                                //    jQuery(".btn-qtatn-save-data").hide();
                                //}
                                //if (result.oHeader.QCH_ACT == 1) {
                                //    jQuery(".btn-qtatn-enq-reset").show();
                                //    jQuery(".btn-qtatn-genarate-po").show();
                                //}
                                //if (result.oHeader.QCH_ACT == 3) {
                                //    jQuery(".btn-qtatn-genarate-po").hide();
                                //    jQuery(".btn-qtatn-enq-reset").show();
                                //    jQuery(".btn-qtatn-save-data").hide();
                                //}
                                showHideButton(result.oHeader.QCH_ACT);
                            }
                        } else if (typeof result.enqDt != "undefined") {
                            if (result.enqDt != null) {
                                setEnqDetails(result.enqDt);
                            }
                            jQuery(".btn-qtatn-enq-reset").hide();
                            jQuery(".btn-qtatn-genarate-po").hide();
                            jQuery(".btn-qtatn-enq-approve").hide();
                            jQuery(".send-cus-sms").hide();
                            jQuery(".btn-print-qtatn-sheet").hide();
                        } else {
                            setInfoMsg("Invalid enquiry id.");
                            jQuery(".btn-qtatn-enq-reset").hide();
                            jQuery(".btn-qtatn-genarate-po").hide();
                            jQuery(".btn-qtatn-enq-approve").hide();
                            jQuery(".send-cus-sms").hide();
                            jQuery(".btn-print-qtatn-sheet").hide();
                        }
                    } else {
                        jQuery(".btn-qtatn-enq-approve").hide();
                        jQuery(".send-cus-sms").hide();
                        jQuery(".btn-qtatn-enq-reset").hide();
                        jQuery(".btn-qtatn-genarate-po").hide();
                        jQuery(".btn-print-qtatn-sheet").hide();
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
        jQuery(".btn-cost-enq-reset").hide();
        jQuery(".btn-cost-genarate-po").hide();
        jQuery(".btn-cost-enq-approve").hide();
        jQuery(".send-cus-sms").hide();
        jQuery(".btn-print-cost-sheet").hide();
    }
}
function clearSessionData() {
    jQuery.ajax({
        type: "GET",
        url: "/Quotation/clearValues",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            if (result.login == true) {
                if (result.success == true) {

                }
            } else {
                Logout();
            }
        }
    });
}
function clearCostingForm() {
    clearSessionData();
    clearFormBoxes();
}
function clearFormBoxes() {
    jQuery(".btn-qtatn-enq-approve").hide();
    jQuery(".btn-qtatn-genarate-po").hide();
    jQuery(".btn-qtatn-enq-reset").hide();
    jQuery(".btn-qtatn-save-data").show();
    jQuery(".send-cus-sms").hide();
    jQuery(".btn-print-qtatn-sheet").hide();
    jQuery('#QCH_DT').val(getFormatedDate(new Date()));
    jQuery(".cost-sheet-status").empty();
    jQuery(".cost-sheet-status").html("---");
    jQuery(".cost-sheet-number").empty();
    jQuery(".cost-sheet-number").html("---");
    clearChargeDataField();
    jQuery("#totalFare").val("");
    jQuery("#totalTax").val("");
    jQuery("#totalCost").val("");
    jQuery("#totalCostLKR").val("");
    jQuery("#QCH_TOT_VALUE").val("");
    jQuery("#QuotePerPax").val("");
    jQuery("#QCH_MARKUP").val("");
    jQuery("#QCH_MARKUP_AMT").val("");
    jQuery("#QCH_TOT_PAX").removeAttr("readonly");
    jQuery("#QCH_MARKUP").removeAttr("readonly");
    jQuery("#CostMarkup").removeAttr("readonly");
    jQuery("#CostMarkupAmount").removeAttr("readonly");

    jQuery("#QCH_MARKUP_AMT").removeAttr("readonly");
    jQuery("#CostMarkupAmount").removeAttr("readonly");

    jQuery(".cost-sheet-table .new-row").remove();
}
function setCostingValues(headerDet, itmDet) {
    clearFormBoxes();
    if (headerDet != null) {
        jQuery("#QCH_CUS_CD").val(headerDet.QCH_CUS_CD);
        jQuery("#QCH_DT").val(getFormatedDateInput(headerDet.QCH_DT));
        jQuery("#QCH_OTH_DOC").val(headerDet.QCH_OTH_DOC);
        jQuery("#QCH_REF").val(headerDet.QCH_REF);
        jQuery(".cost-sheet-status").empty();
        jQuery(".cost-sheet-number").empty();
        jQuery(".cost-sheet-number").html(headerDet.QCH_COST_NO);
        jQuery("#QCH_TOT_PAX").val(headerDet.QCH_TOT_PAX);
        switch (headerDet.QCH_ACT) {
            case 1:
                jQuery(".cost-sheet-status").html("Approved");
                break;

            case 2:
                jQuery(".cost-sheet-status").html("Pending");
                break;
            case 0:
                jQuery(".cost-sheet-status").html("Cancel");
                break;
            case 3:
                jQuery(".cost-sheet-status").html("POGenarated");
                break;
        }
        jQuery("#totalFare").val(headerDet.QCH_TOT_COST);
        //jQuery("#totalTax").val();disabled
        jQuery("#totalCost").val(headerDet.QCH_TOT_COST);
        jQuery("#totalCostLKR").val((parseFloat(headerDet.QCH_TOT_COST) * parseFloat(itmDet[0].QCD_EX_RATE)).toFixed(2));
        jQuery("#QCH_MARKUP").val(headerDet.QCH_MARKUP);
        jQuery("#QCH_MARKUP_AMT").val(headerDet.QCH_MARKUP_AMT);
        if (parseFloat(headerDet.QCH_MARKUP_AMT) > 0) {
            jQuery("#CostMarkup").attr("readonly", true);
            jQuery("#CostMarkupAmount").attr("readonly", true);
        }
        jQuery("#QCH_TOT_VALUE").val(headerDet.QCH_TOT_VALUE);
        jQuery("#QuotePerPax").val((parseFloat(headerDet.QCH_TOT_VALUE) / parseFloat(headerDet.QCH_TOT_PAX)).toFixed(2));
    }
    if (itmDet != null) {
        for (i = 0; i < itmDet.length; i++) {
            if (itmDet[i].QCD_CAT == "AIRTVL") {
                cate = "Air Travel";
            } else if (itmDet[i].QCD_CAT == "TRANS") {
                cate = "Travels";
            } else if (itmDet[i].QCD_CAT == "MSCELNS") {
                cate = "Miscellaneous";
            } else if (itmDet[i].QCD_CAT == "HTLRTS") {
                cate = "Hotel Rates";
            } else if (itmDet[i].QCD_CAT == "OVSLAGMT") {
                cate = "Overseas Land Arrangement(per Pax)";
            }
            jQuery('.cost-sheet-table').append('<tr class="new-row">' +
                    '<td>' + cate + '</td>' +
                    '<td style="display:none;">' + itmDet[i].QCD_CAT + '</td>' +
                    '<td>' + itmDet[i].QCD_SUB_CATE + '</td>' +
                    '<td>' + itmDet[i].QCD_DESC + '</td>' +
                    '<td>' + itmDet[i].QCD_ANAL1 + '</td>' +
                    '<td>' + itmDet[i].QCD_CURR + '</td>' +
                    '<td class="text-left-align">' + itmDet[i].QCD_QTY + '</td>' +
                    '<td class="text-left-align">' + addCommas(itmDet[i].QCD_UNIT_COST) + '</td>' +
                    '<td class="text-left-align">' + itmDet[i].QCD_TAX + '</td>' +
                    '<td class="text-left-align">' + addCommas(itmDet[i].QCD_TOT_COST) + '</td>' +
                    '<td class="text-left-align">' + addCommas(itmDet[i].QCD_MARKUP) + '%</td>' +
                    '<td class="text-left-align">' + addCommas(itmDet[i].QCD_MARKUP_AMT) + '</td>' +
                    '<td class="text-left-align">' + addCommas(itmDet[i].QCD_AF_MARKUP) + '</td>' +
                    '<td>' + itmDet[i].QCD_RMK + '</td>' +
                   '<td style="text-align:center;"><img class="delete-img remove-cost-mehod-cls" src="/Resources/images/Remove.png"></td>' +
                    '</tr>');
            if (parseFloat(itmDet[i].QCD_MARKUP) > 0) {
                jQuery("#QCH_MARKUP").attr("readonly", true);
                jQuery("#QCH_MARKUP_AMT").attr("readonly", true);
            }
        }
        removeChgClickFunction()
    }
}
function updateGridAndPriceValues(data) {
    var cate;
    var totFareDisplay = 0;
    var totTaxDisplay = 0;
    var totCostDisplay = 0;
    var totCostLKRDisplay = 0;
    var quotePerPax = 0;
    jQuery('.cost-sheet-table .new-row').remove();
    if (data.length > 0) {
        for (i = 0; i < data.length; i++) {
            if (data[i].QCD_CAT == "AIRTVL") {
                cate = "Air Travel";
            } else if (data[i].QCD_CAT == "TRANS") {
                cate = "Travels";
            } else if (data[i].QCD_CAT == "MSCELNS") {
                cate = "Miscellaneous";
            } else if (data[i].QCD_CAT=="HTLRTS") {
                cate = "Hotel Rates";
            } else if (data[i].QCD_CAT == "OVSLAGMT") {
                cate = "Overseas Land Arrangement(per Pax)";
            }
            jQuery('.cost-sheet-table').append('<tr class="new-row">' +
                    '<td>' + cate + '</td>' +
                    '<td style="display:none;">' + data[i].QCD_CAT + '</td>' +
                    '<td>' + data[i].QCD_SUB_CATE + '</td>' +
                    '<td>' + data[i].QCD_DESC + '</td>' +
                    '<td>' + data[i].QCD_ANAL1 + '</td>' +
                    '<td>' + data[i].QCD_CURR + '</td>' +
                    '<td class="text-left-align">' + data[i].QCD_QTY + '</td>' +
                    '<td class="text-left-align">' + addCommas(data[i].QCD_UNIT_COST) + '</td>' +
                    '<td class="text-left-align">' + data[i].QCD_TAX + '</td>' +
                    '<td class="text-left-align">' + addCommas(data[i].QCD_TOT_COST) + '</td>' +
                    '<td class="text-left-align">' + addCommas(data[i].QCD_MARKUP) + '%</td>' +
                    '<td class="text-left-align">' + addCommas(data[i].QCD_MARKUP_AMT) + '</td>' +
                    '<td class="text-left-align">' + addCommas(data[i].QCD_AF_MARKUP) + '</td>' +
                    '<td>' + data[i].QCD_RMK + '</td>' +
                   '<td style="text-align:center;"><img class="delete-img remove-cost-mehod-cls" src="/Resources/images/Remove.png"></td>' +
                    '</tr>');
            totFareDisplay = parseFloat(totFareDisplay) + parseFloat(data[i].QCD_UNIT_COST);
            totTaxDisplay = parseFloat(totTaxDisplay) + parseFloat(data[i].QCD_TAX);

            totCostDisplay = parseFloat(totCostDisplay) + parseFloat(data[i].QCD_TOT_COST);
            totCostDisplayLkr = (parseFloat(totCostDisplay) * data[i].QCD_EX_RATE).toFixed(2);
            totCostLKRDisplay = parseFloat(totCostLKRDisplay) + parseFloat(data[i].QCD_AF_MARKUP);
            quotePerPax = (parseFloat(quotePerPax) + parseFloat(data[i].QCD_AF_MARKUP) / parseFloat(jQuery("#QCH_TOT_PAX").val())).toFixed(2);
            if (parseFloat(data[i].QCD_MARKUP) > 0) {
                jQuery("#QCH_MARKUP").attr("readonly", true);
                jQuery("#QCH_MARKUP_AMT").attr("readonly", true);
            }
        }
        removeChgClickFunction();
        jQuery("#totalFare").val(totFareDisplay);
        jQuery("#totalTax").val(totTaxDisplay);
        jQuery("#totalCost").val(totCostDisplay);
        jQuery("#totalCostLKR").val(totCostDisplayLkr);
        jQuery("#QCH_TOT_VALUE").val(totCostLKRDisplay);
        jQuery("#QuotePerPax").val(quotePerPax);
        jQuery("#QCH_TOT_PAX").attr("readonly", true);
        clearChargeDataField();
    } else {
        jQuery("#totalFare").val("");
        jQuery("#totalTax").val("");
        jQuery("#totalCost").val("");
        jQuery("#totalCostLKR").val("");
        jQuery("#QCH_TOT_VALUE").val("");
        jQuery("#QuotePerPax").val("");
        jQuery("#QCH_MARKUP").val("");
        jQuery("#QCH_MARKUP_AMT").val("");
        jQuery("#QCH_TOT_PAX").removeAttr("readonly");
        jQuery("#QCH_MARKUP").removeAttr("readonly");
        jQuery("#QCH_MARKUP_AMT").removeAttr("readonly");
        jQuery(".cost-sheet-table .new-row").remove();
    }


}
function setEnqDetails(enqData) {
    clearCostingForm();
    if (enqData.GCE_SEQ != 0 && enqData.GCE_ENQ_ID != "") {
        jQuery("#QCH_CUS_CD").val(enqData.GCE_CUS_CD);
        jQuery("#QCH_OTH_DOC").val(enqData.GCE_ENQ_ID);
        jQuery("#QCH_REF").val(enqData.GCE_REF);
        jQuery("#QCH_TOT_PAX").val(enqData.GCE_NO_PASS);
    } else {
        setInfoMsg("Invalid enquiry id.")
    }
}
