jQuery(document).ready(function () {
    jQuery('#Sah_dt').val(my_date_format_with_time(new Date()));
    jQuery('#Sah_dt').datepicker({ dateFormat: "dd/M/yy" })

    jQuery(".inv-no-search").click(function () {
        var headerKeys = Array()
        headerKeys = ["Row", "Invoice", "Reference", "Balance"];
        field = "invoceSrch"
        var x = new CommonSearch(headerKeys, field);
    });
    jQuery(".cus-cd-search").click(function () {
        var headerKeys = Array()
        headerKeys = ["Row", "Code", "Name", "Nic", "Mobile", "Br No"];
        field = "cusCodeInv"
        var x = new CommonSearch(headerKeys, field);
    });

    //ISURU
  

    jQuery(".btn-cn-print-data").click(function () {
        window.open(
              "/CreditNote/CreditNotePrint?invoiceno=" + jQuery("#Sah_inv_no").val(),
              '_blank' // <- This is what makes it open in a new window.
          );
    });
    

    function PrintCreditNote(invoiceno) {
        Lobibox.confirm({
            msg: "Do you want to print ?",
            callback: function ($this, type, ev) {
                var invNo = invoiceno;
                if (type == "yes") {
                    window.open(
                                 "/CreditNote/CreditNotePrint?invoiceno=" + invNo,
                                 '_blank' // <- This is what makes it open in a new window.
                             );
                }

            }
        });
    }
    loadExecutive();
    function loadExecutive() {
        jQuery.ajax({
            type: "GET",
            url: "/Invoicing/LoadExecutive",
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            success: function (result) {
                if (result.login == true) {
                    if (result.success = true) {
                        var select = document.getElementById("Sah_sales_ex_cd");
                        jQuery("#Sah_sales_ex_cd").empty();
                        var options = [];
                        var option = document.createElement('option');
                        if (result.data != null && result.data.length != 0) {
                            for (i = 0; i < result.data.length; i++) {
                                option.text = result.data[i].Text;
                                option.value = result.data[i].Value;
                                options.push(option.outerHTML);
                            }
                        } else {
                            option.text = "Select Type";
                            option.value = "";
                            options.push(option.outerHTML);
                        }
                        select.insertAdjacentHTML('beforeEnd', options.join('\n'));
                    } else {
                        setError(result.msg);
                    }
                } else {
                    Logout();
                }
            }
        })
    }
    function LoadPriceBook() {
        jQuery.ajax({
            type: "GET",
            url: "/Invoicing/LoadPriceBook",
            data: { invType: jQuery("form.frm-inv-det #Sah_inv_tp").val() },
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            success: function (result) {
                if (result.login == true) {
                    if (result.success = true) {
                        var select = document.getElementById("PriceBook");
                        jQuery("#PriceBook").empty();
                        var options = [];
                        var option = document.createElement('option');
                        if (result.data != null && result.data.length != 0) {
                            for (i = 0; i < result.data.length; i++) {
                                option.text = result.data[i].Text;
                                option.value = result.data[i].Value;
                                options.push(option.outerHTML);
                            }
                        } else {
                            option.text = "Select Type";
                            option.value = "";
                            options.push(option.outerHTML);
                        }
                        select.insertAdjacentHTML('beforeEnd', options.join('\n'));
                    } else {
                        setError(result.msg);
                    }
                } else {
                    Logout();
                }
            }
        })
    }
    LoadPriceBook();
    function LoadPriceLevel() {
        jQuery.ajax({
            type: "GET",
            url: "/Invoicing/LoadPriceLevel",
            data: { invType: jQuery("form.frm-inv-det #Sah_inv_tp").val(), book: jQuery("form.frm-inv-det #PriceBook").val() },
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            success: function (result) {
                if (result.login == true) {
                    if (result.success = true) {
                        var select = document.getElementById("PriceLevel");
                        jQuery("#PriceLevel").empty();
                        var options = [];
                        var option = document.createElement('option');
                        if (result.data != null && result.data.length != 0) {
                            for (i = 0; i < result.data.length; i++) {
                                option.text = result.data[i].Text;
                                option.value = result.data[i].Value;
                                options.push(option.outerHTML);
                            }
                        } else {
                            option.text = "Select Type";
                            option.value = "";
                            options.push(option.outerHTML);
                        }
                        select.insertAdjacentHTML('beforeEnd', options.join('\n'));
                    } else {
                        setError(result.msg);
                    }
                } else {
                    Logout();
                }
            }
        })
    }
    LoadPriceLevel();
    loadCostType();
    function loadCostType() {
        jQuery.ajax({
            type: "GET",
            url: "/Invoicing/CostType",
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            success: function (result) {
                if (result.login == true) {
                    if (result.success = true) {
                        var select = document.getElementById("ChargeCode");
                        jQuery("#ChargeCode").empty();
                        var options = [];
                        var option = document.createElement('option');
                        if (result.data != null && result.data.length != 0) {
                            for (i = 0; i < result.data.length; i++) {
                                option.text = result.data[i].Text;
                                option.value = result.data[i].Value;
                                options.push(option.outerHTML);
                            }
                        } else {
                            option.text = "Select Type";
                            option.value = "";
                            options.push(option.outerHTML);
                        }
                        select.insertAdjacentHTML('beforeEnd', options.join('\n'));
                    } else {
                        setError(result.msg);
                    }
                } else {
                    Logout();
                }
            }
        })
    }
    loadCurrency();
    function loadCurrency() {
        jQuery.ajax({
            type: "GET",
            url: "/Invoicing/Currency",
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            success: function (result) {
                if (result.login == true) {
                    if (result.success = true) {
                        var select = document.getElementById("Currency");
                        jQuery("#Currency").empty();
                        var options = [];
                        var option = document.createElement('option');
                        if (result.data != null && result.data.length != 0) {
                            for (i = 0; i < result.data.length; i++) {
                                option.text = result.data[i].Text;
                                option.value = result.data[i].Value;
                                options.push(option.outerHTML);
                            }
                        } else {
                            option.text = "Select Type";
                            option.value = "";
                            options.push(option.outerHTML);
                        }
                        select.insertAdjacentHTML('beforeEnd', options.join('\n'));
                        if (options.length > 0)
                            jQuery("#Currency").val("LKR");
                    } else {
                        setError(result.msg);
                    }
                } else {
                    Logout();
                }
            }
        })
    }
    jQuery(".sub-type-search").click(function () {
        var data = jQuery("#ChargeCode").val();
        if (jQuery("#ChargeCode").val() == "TRANS") {
            var headerKeys = Array()
            headerKeys = ["Row", "Code", "Description", "Service By", "From Date", "To Date", "From", "To", "Rate", "Class", "Vehicle"];
            field = "invChrgCdeSrchTrans"
            var x = new CommonSearch(headerKeys, field, data);
        } else if (jQuery("#ChargeCode").val() == "AIRTVL") {
            var headerKeys = Array()
            headerKeys = ["Row", "Code", "Description", "Service By", "From", "To", "Rate", "From Date", "To Date"];
            field = "invChrgCdeSrchArival";
            var x = new CommonSearch(headerKeys, field, data);
        } else if (jQuery("#ChargeCode").val() == "MSCELNS") {
            var headerKeys = Array()
            headerKeys = ["Row", "Code", "Description", "Service By", "From Date", "To Date", "Rate"];
            field = "invChrgCdeSrchMsclens";
            var x = new CommonSearch(headerKeys, field, data);
        } else {
            var headerKeys = Array()
            headerKeys = ["Row", "Code", "Description", "Service By", "From Date", "To Date", "Rate"];
            field = "invChrgCdeSrchMsclens";
            var x = new CommonSearch(headerKeys, field, data);
        }

    });
    jQuery("#SubType").focusout(function () {
        if (jQuery(this).val() != "") {
            if (jQuery("#ChargeCode").val() != "") {
                getChargCodeDetls(jQuery(this).val(), jQuery("#ChargeCode").val());
            } else {
                setInfoMsg("Please select Charge Code.");
                jQuery("#SubType").val("");
            }

        }
    });
    function getChargCodeDetls(code, service) {
        if (code != "" && service != "") {
            jQuery.ajax({
                type: "GET",
                url: "/Invoicing/loadChargCode",
                data: { code: code, service: service },
                contentType: "application/json;charset=utf-8",
                dataType: "json",
                success: function (result) {
                    if (result.login == true) {
                        if (result.success == true) {
                            if (service == "TRANS") {
                                jQuery("#Currency").val(result.data.STC_CURR);
                                jQuery("#UnitRate").val(Number(result.data.STC_RT));
                                jQuery("#Tax").val(Number(result.data.STC_TAX_RT) * Number(result.data.STC_RT) / 100);
                                CurrencyCodeChange();
                                jQuery("#Remark").focus();
                            } else if (service == "AIRTVL") {
                                jQuery("#Currency").val(result.data.SAC_CUR);
                                jQuery("#UnitRate").val(Number(result.data.SAC_RT));
                                jQuery("#Tax").val(Number(result.data.SAC_TAX_RT) * Number(result.data.SAC_RT) / 100);
                                CurrencyCodeChange();
                                jQuery("#Remark").focus();
                            } else if (service == "MSCELNS") {
                                jQuery("#Currency").val(result.data.SSM_CUR);
                                jQuery("#UnitRate").val(Number(result.data.SSM_RT));
                                jQuery("#Tax").val(Number(result.data.SSM_TAX_RT) * Number(result.data.SSM_RT) / 100);
                                CurrencyCodeChange();
                                jQuery("#Remark").focus();
                            } else {
                                jQuery("#Currency").val(result.data.SSM_CUR);
                                jQuery("#UnitRate").val(Number(result.data.SSM_RT));
                                jQuery("#Tax").val(Number(result.data.SSM_TAX_RT) * Number(result.data.SSM_RT) / 100);
                                CurrencyCodeChange();
                                jQuery("#Remark").focus();
                            }
                        } else {
                            jQuery("#CostFare").val("");
                            jQuery("#CostTotal").val("");
                            jQuery("#CostMarkup").val("");
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
    function CurrencyCodeChange() {
        jQuery.ajax({
            type: "GET",
            url: "/Invoicing/currencyCodeChange",
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            data: { currency: jQuery("#Currency").val() },
            success: function (result) {
                if (result.login == true) {
                    if (result.success == true) {
                        jQuery(".currancy-rate-amt").empty();
                        jQuery(".currancy-rate-amt").html(result.data);
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
    jQuery("#Pax").on("input", function () {
        var pax = parseFloat(jQuery("#Pax").val());
        var UniteRate = parseFloat(jQuery("#UnitRate").val());
        var Tax = parseFloat(jQuery("#Tax").val());
        var Rate = parseFloat(jQuery(".currancy-rate-amt").html());
        // var Taxval = (Tax / 100) * pax * UniteRate * Rate;
        var Dis = parseFloat(jQuery("#Dis").val());
        var Discount = parseFloat(jQuery("#Discount").val());
        var Total = (pax * (UniteRate + Tax) * Rate);
        jQuery("#Total").val(Total.toFixed(2));
    })
    jQuery("#Dis").on("input", function () {
        var pax = parseFloat(jQuery("#Pax").val());
        var UniteRate = parseFloat(jQuery("#UnitRate").val());
        var Tax = parseFloat(jQuery("#Tax").val());
        var Rate = parseFloat(jQuery(".currancy-rate-amt").html());
        // var Taxval = (Tax / 100) * pax * UniteRate * Rate;
        var Dis = parseFloat(jQuery("#Dis").val());
        var Discount = Dis * pax * (UniteRate + Tax) / 100;
        jQuery("#Discount").val(Discount);
        var Total = (pax * (UniteRate + Tax)) - Discount;
        jQuery("#Total").val(Total.toFixed(2));
        var totallkr = Total * Rate;
        jQuery(".total-lkr").empty();
        jQuery(".total-lkr").html("Total :" + totallkr + " LKR");
    })
    jQuery("#Discount").on("input", function () {
        var pax = parseFloat(jQuery("#Pax").val());
        var UniteRate = parseFloat(jQuery("#UnitRate").val());
        var Rate = parseFloat(jQuery(".currancy-rate-amt").html());
        var Tax = parseFloat(jQuery("#Tax").val());
        // var Taxval = (Tax / 100) * pax * UniteRate * Rate;
        var Discount = parseFloat(jQuery("#Discount").val());

        var Dis = Discount / (pax * (UniteRate + Tax)) * 100;
        jQuery("#Dis").val(Dis);


        var Total = (pax * (UniteRate + Tax)) - Discount;
        jQuery("#Total").val(Total.toFixed(2));
        var totallkr = (Total * Rate).toFixed(2);

        jQuery(".total-lkr").empty();
        jQuery(".total-lkr").html("Total :" + totallkr + " LKR");
    });
    jQuery("#UnitRate").on("input", function () {
        var pax = parseFloat(jQuery("#Pax").val());
        var UniteRate = parseFloat(jQuery("#UnitRate").val());
        var Rate = parseFloat(jQuery(".currancy-rate-amt").html());
        var Tax = parseFloat(jQuery("#Tax").val());
        var Discount = parseFloat(jQuery("#Discount").val());
        var Dis = parseFloat(jQuery("#Dis").val());
        var Total = (pax * (UniteRate + Tax)) - Discount;
        jQuery("#Total").val(Total.toFixed(2));
        var totallkr = (Total * Rate).toFixed(2);
        jQuery(".total-lkr").empty();
        jQuery(".total-lkr").html("Total :" + totallkr + " LKR");
    });
    function updatePayment(amount) {
        var payAmount = parseFloat(amount);
        // var currentTot = (jQuery(".tot-amount-val").html() != "") ? parseFloat(jQuery(".tot-amount-val").html()) : 0;
        var finTot = parseFloat(0) + parseFloat(amount);
        updateCurrencyAmount(parseFloat(finTot), jQuery("#Sah_cus_cd").val(), "");

    }
    $('#Pax').focusout(function () {
        var str = $(this).val();
        var numRange = /^[0-9+]+$/;
        if (!numRange.test(str)) {
            setInfoMsg('Please enter a valid Pax !!!');
            $(this).val('');
            jQuery("#Total").val(0);
        }
    });
    $('#UnitRate').focusout(function () {
        var str = $(this).val();
        var numRange = /^[0-9+]+$/;
        if (!numRange.test(str)) {
            setInfoMsg('Please enter a valid Unit Rate !!!');
            $(this).val('');
            jQuery("#Total").val(0);
        }
    });
    $('#Dis').focus(function () {
        var str = $(this).val();

        if (jQuery("#Pax").val() == "") {
            setInfoMsg('Please enter Pax !!!');
            $(this).val('');
        }
    });
    $('#Discount').focus(function () {
        var str = $(this).val();

        if (jQuery("#Pax").val() == "") {
            setInfoMsg('Please enter Pax !!!');
            $(this).val('');
        }
    });
    $('#Dis').focusout(function () {
        var str = $(this).val();
        var numRange = /^[0-9]\d*(\.\d+)?$/;
        if (!numRange.test(str)) {
            setInfoMsg('Please enter a valid Discount !!!');
            $(this).val('');
            jQuery("#Discount").val('')
        }
    });
    $('#Discount').focusout(function () {
        var str = $(this).val();
        var numRange = /^[0-9]\d*(\.\d+)?$/;
        if (!numRange.test(str)) {
            setInfoMsg('Please enter a valid Discount !!!');
            $(this).val('');
            jQuery("#Dis").val('')
        }
    });
    jQuery("#Sah_inv_no").focusout(function () {
        if (jQuery("#Sah_inv_no").val() != "") {
            jQuery('.cost-sheet-table .new-row').remove();
            jQuery.ajax({
                type: "GET",
                url: "/CreditNote/getInvoiceDetails",
                data: { invNo: jQuery("#Sah_inv_no").val() },
                contentType: "application/json;charset=utf-8",
                dataType: "json",
                success: function (result) {
                    if (result.login == true) {
                        if (result.success == true) {
                            if (result.invHed.Sah_inv_no != null) {
                                setinvFieldValueInv(result.invdetails);
                                setAllInvData(result.invHed);
                            } else {
                                setInfoMsg("Cannot find Invoice details.");
                                jQuery("#Sah_inv_no").val("");
                                jQuery("#Sah_inv_no").focus();
                                clearall();
                            }
                        } else {
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
        }
    });
    function setinvFieldValueInv(data) {
        jQuery('.cost-sheet-table .new-row').remove();
        if (data != "") {
            if (data != null) {
                for (i = 0; i < data.length; i++) {
                    var discount = ((data[i].Sad_qty * data[i].Sad_unit_rt / data[i].SII_EX_RT) - data[i].Sad_tot_amt / data[i].SII_EX_RT).toFixed(4);
                    var dis = (discount * 100 / (data[i].Sad_qty * data[i].Sad_unit_rt)).toFixed(2);

                    updateChagresGrid(data[i].Sad_itm_cd, data[i].Sad_itm_stus, data[i].SII_CURR, data[i].Sad_qty, data[i].Sad_warr_remarks, dis, discount, data[i].Sad_unit_rt, data[i].SII_EX_RT, 0, 0);
                }


            }
        }
    }
    function updateChagresGrid(chgCd, service, currencyCode, pax, remarks, dis, discount, unitrate, Rate, Markup, Tax) {
        var row = jQuery(".cost-sheet-table .new-row")[0];
        var value;
        if (row == null) {
            value = currencyCode;
        } else {
            value = jQuery(row).find('td:eq(3)').text();
        }
        if (value == currencyCode) {
            jQuery.ajax({
                type: "GET",
                url: "/CreditNote/updateTourCharges",
                data: {
                    chgCd: chgCd,
                    service: service,
                    currencyCode: currencyCode,
                    pax: pax,
                    remarks: remarks,
                    dis: dis,
                    discount: discount,
                    unitrate: unitrate,
                    Rate: Rate,
                    Markup: Markup,
                    Tax: Tax
                },
                contentType: "application/json;charset=utf-8",
                dataType: "json",
                success: function (result) {
                    if (result.login == true) {
                        if (result.success == true) {
                            if (result.oMainItemsList.length > 0) {
                                data = result.oMainItemsList;
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
    function updateGridAndPriceValues(data) {
        var cate;
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
                }
                else {
                    cate = "Overseas Land Arrangement(per Pax)";
                }
                jQuery('.cost-sheet-table').append('<tr class="new-row">' +
                     '<td>' + '<input type="checkbox" checked="checked" />' + '</td>' +
                        '<td>' + data[i].Sad_itm_cd + '</td>' +
                        '<td>' + data[i].Sad_warr_remarks + '</td>' +
                         '<td>' + data[i].SII_CURR + '</td>' +
                         '<td class="change-price">' + data[i].Sad_qty + '</td>' +
                         '<td>' + (parseFloat(data[i].Sad_unit_rt) / parseFloat(data[i].SII_EX_RT)).toFixed(2) + '</td>' +
                        '<td> ' + (parseFloat(data[i].Sad_tot_amt) / parseFloat(data[i].SII_EX_RT)).toFixed(2) + '</td>' +

                       '<td style="text-align:center;"><img class="delete-img remove-cost-mehod-cls" src="../Resources/images/Remove.png"></td>' +
                        '</tr>');
                /*   totFareDisplay = parseFloat(totFareDisplay) + parseFloat(data[i].QCD_UNIT_COST);
                   totTaxDisplay = parseFloat(totTaxDisplay) + parseFloat(data[i].QCD_TAX);
                   totCostDisplay = parseFloat(totCostDisplay) + parseFloat(data[i].QCD_TOT_COST);
                   totCostLKRDisplay = parseFloat(totCostLKRDisplay) + parseFloat(data[i].QCD_TOT_LOCAL);
                   quotePerPax = quotePerPax + data[i].QCD_TOT_LOCAL / data[i].QCD_QTY;

                   if (parseFloat(data[i].QCD_MARKUP) > 0) {
                       jQuery("#QCH_MARKUP").attr("readonly", true);
                   }*/
                totCostDisplay = parseFloat(totCostDisplay) + parseFloat(data[i].Sad_tot_amt) / parseFloat(data[i].SII_EX_RT);
                //console.log(data[i].Sad_tot_amt);
                //jQuery("#Sird_settle_amt").val(totCostDisplay);
                //jQuery(".tot-paid-amount-val").empty();
                //jQuery(".tot-paid-amount-val").html(totCostDisplay);


            }
           removeChgClickFunction();
            /*   jQuery("#totalFare").val(totFareDisplay);
               jQuery("#totalTax").val(totTaxDisplay);
               jQuery("#totalCost").val(totCostDisplay);
               jQuery("#totalCostLKR").val(totCostLKRDisplay);
               jQuery("#QCH_TOT_VALUE").val(totCostLKRDisplay);
               jQuery("#QuotePerPax").val(quotePerPax);
               jQuery("#QCH_TOT_PAX").attr("readonly", true);
               */

           jQuery(".change-price").unbind('click').click(function (evt) {
               evt.preventDefault();
               //var td = jQuery(this).siblings('td');
               var tr = jQuery(this).parent('tr');
               //var aa = jQuery(this).parent('a');
               var rmk = jQuery(tr).find('td:eq(2)').html();
               var cgCd = jQuery(tr).find('td:eq(1)').html();
               var qty = jQuery(tr).find('td:eq(4)').html();
               var totcst = jQuery(tr).find('td:eq(5)').html();
               jQuery("#chgcd").val(cgCd);
               jQuery("#reference").val(rmk);
               jQuery("#Qtynew").val(0);
               jQuery('#addChgcdPop').modal({
                   keyboard: false,
                   backdrop: 'static'
               }, 'show');
               $('#Qtynew').focusout(function () {
                   var str = $(this).val();
                   var numRange = /^[0-9+]+$/;
                   if (!numRange.test(str)) {
                       setInfoMsg('Please enter a valid Pax !!!');
                       $(this).val('');
                   }
               });
               jQuery(".btn-price-change").unbind().click(function () {

                   console.log(Number(jQuery("#Qtynew").val()));
                   console.log(qty);
                   if (Number(jQuery("#Qtynew").val()) > Number(qty))
                   {
                       setInfoMsg("Can not exeed real qty!!");
                       return;
                   }
                   jQuery.ajax({
                       type: "GET",
                       url: "/CreditNote/ChangePrice",
                       data: { chgcd: jQuery("#chgcd").val(), reference: jQuery("#reference").val(), totalcostne: jQuery("#Qtynew").val() },
                       contentType: "application/json;charset=utf-8",
                       dataType: "json",
                       success: function (result) {
                           if (result.login == true) {
                               if (result.success == true) {
                                   updateGridAndPriceValues(result.invdetails);
                                   jQuery('#addChgcdPop').modal('hide');
                               } else {
                                   if (result.type == "Error") {
                                       setError(result.msg);
                                   } else if (result.type == "Info") {
                                       setInfoMsg(result.msg);
                                       jQuery('#addChgcdPop').modal('hide');
                                   }
                               }
                           } else {
                               Logout();
                           }
                       }

                   });
                   return false;

                   return false;
               });
           });
          
            jQuery("#totalCost").val(totCostDisplay.toFixed(2));
            updatePayment(totCostDisplay);
            clearChargeDataField();
            jQuery("#Sird_settle_amt").val(totCostDisplay);
        } else {
            jQuery("#totalFare").val("");
            jQuery("#totalTax").val("");
            jQuery("#totalCost").val("");
            jQuery("#totalCostLKR").val("");
            jQuery("#QCH_TOT_VALUE").val("");
            jQuery("#QuotePerPax").val("");
            jQuery("#QCH_MARKUP").val("");
            jQuery("#QCH_TOT_PAX").removeAttr("readonly");
            jQuery("#QCH_MARKUP").removeAttr("readonly");
            jQuery(".cost-sheet-table .new-row").remove();
            jQuery("#Sird_settle_amt").val(0);
        }
    }

    function setAllInvData(data)
    {
        jQuery("#Sah_cus_add1").val(data.Sah_cus_add1);
        jQuery("#Sah_cus_add2").val(data.Sah_cus_add2);
        jQuery("#Sah_ref_doc").val(data.Sah_ref_doc);
        jQuery("#Sah_dt").val(getFormatedDate(Date(data.Sah_dt)));
        jQuery("#Sah_inv_tp").val(data.Sah_inv_tp);
        jQuery("#Sah_sales_ex_cd").val(data.Sah_sales_ex_cd);
        jQuery("#Sah_cus_cd").val(data.Sah_cus_cd);
        jQuery("#Sah_cus_name").val(data.Sah_cus_name);
        jQuery("#Sah_remarks").val(data.Sah_remarks);
        if (data.Sah_inv_tp=="DEBT")
        {
            // jQuery("#chkdebitnote").
            $("#chkdebitnote").attr('checked', true);
        } else {
            $("#chkdebitnote").attr('checked', false);
        }
    }
    function removeChgClickFunction() {
        jQuery(".delete-img.remove-cost-mehod-cls").unbind('click').click(function (evt) {
            evt.preventDefault();
            var td = jQuery(this).parent('td');
            var tr = jQuery(td).parent('tr');
            var cgCd = jQuery(tr).find('td:eq(1)').html();
            var qty = jQuery(tr).find('td:eq(4)').html();
            var totcst = jQuery(tr).find('td:eq(6)').html();
            Lobibox.confirm({
                msg: "Do you want to remove ?",
                callback: function ($this, type, ev) {
                    if (type == "yes") {
                        jQuery.ajax({
                            type: "GET",
                            url: "/CreditNote/removeChargeItem",
                            data: { chgCd: cgCd, pax: qty, totcst: totcst },
                            contentType: "application/json;charset=utf-8",
                            dataType: "json",
                            success: function (result) {
                                if (result.login == true) {
                                    if (result.success == true) {
                                        updateGridAndPriceValues(result.oMainItemsList);
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
    jQuery(".add-inv-charg-data").click(function () {
        if (jQuery("#ChargeCode").val() != "") {
            if (jQuery("#SubType").val() != "") {
                if (jQuery("#Remark").val() != "") {
                    if (jQuery("#Pax").val() != "" || jQuery.isNumeric(jQuery("#Pax").val())) {
                        if (jQuery("#UnitRate").val() != "" || jQuery.isNumeric(jQuery("#UnitRate").val())) {
                            if (jQuery("#Dis").val() != "" || jQuery.isNumeric(jQuery("#Dis").val())) {
                                if (jQuery("#Discount").val() != "" || jQuery.isNumeric(jQuery("#Discount").val())) {
                                    if (jQuery("#UnitRate").val() != "" || jQuery.isNumeric(jQuery("#UnitRate").val())) {

                                        var chgCd = jQuery("#SubType").val();
                                        var service = jQuery("#ChargeCode").val();
                                        var currencyCode = jQuery("#Currency").val();
                                        var pax = jQuery("#Pax").val();
                                        var remarks = jQuery("#Remark").val();
                                        var dis = jQuery("#Dis").val();
                                        var discount = jQuery("#Discount").val();
                                        var unitrate = jQuery("#UnitRate").val();
                                        var Rate = parseFloat(jQuery(".currancy-rate-amt").html());
                                        var Tax = parseFloat(jQuery("#Tax").val());

                                        if (chgCd != "", service != "", currencyCode != "", pax != "", remarks != "", dis != "", discount != "") {

                                            updateChagresGrid(chgCd, service, currencyCode, pax, remarks, dis, discount, unitrate, Rate, 0, Tax);

                                        }
                                    } else {
                                        setInfoMsg("Add Unit Rate Or You Add Unit Rate Invalid");
                                    }
                                } else {
                                    setInfoMsg("Add Discount Or You Add Discount Invalid");
                                }

                            } else {
                                setInfoMsg("Add Dis Or You Add Dis Invalid");
                            }
                        } else {
                            setInfoMsg("Add Unit Rate Or You Add Rate Invalid");
                        }
                    } else {
                        setInfoMsg("Add number of pax Or You Add Pax Invalid");
                    }
                } else {
                    setInfoMsg("Plese select Remark");
                }
            } else {
                setInfoMsg("Plese select Sub Type.");
            }
        } else {
            setInfoMsg("Plese select Charge Code.");
        }


    });
    function clearChargeDataField() {
        jQuery("#ChargeCode").val("");
        jQuery("#Pax").val("");
        jQuery("#SubType").val("");
        jQuery("#Currency").val("");
        jQuery("#Remark").val("");
        jQuery("#UnitRate").val("");
        jQuery("#Dis").val(0);
        jQuery("#Discount").val(0);
        jQuery("#Total").val(0);
        jQuery("#CostChargCode").focus();
    }
    jQuery(".btn-clear-cn-data").click(function (evt) {
        evt.preventDefault();
        Lobibox.confirm({
            msg: "Do you want to clear data ?",
            callback: function ($this, type, ev) {
                if (type == "yes") {
                   // document.getElementById("cn-frm").reset();
                    isload = 0;
                    window.location.href = "/CreditNote";
                }
            }
        });

    });
    jQuery("#Sird_pay_tp").focus(function (evt) {
        evt.preventDefault();
        if ($('input[name=chkdebitnote]:checked').val()=="on")
        {
            var inv_type = "CS";
            loadPayModes(inv_type);
            jQuery("#Sird_pay_tp").val("CS");
        } else {
            var inv_type = "CRED";
            loadPayModes(inv_type);
            jQuery("#Sird_pay_tp").val("CRED");
        }
     
       
     


    });
    jQuery("#Sah_cus_cd").focusout(function ()
    {
        codeFocusOut();
    });
    function codeFocusOut() {
        if (jQuery("#Sah_cus_cd").val() != "") {
            jQuery.ajax({
                type: "GET",
                url: "/CreditNote/cusCodeTextChanged",
                data: { val: jQuery("#Sah_cus_cd").val() },
                contentType: "application/json;charset=utf-8",
                dataType: "json",
                success: function (result) {
                    if (result.login == true) {
                        if (result.success == true) {
                            if (result.success == true) {
                                if (typeof (result.local) != "undefined") {
                                    setFieldValue(result.data, false, true);
                                }
                                if (typeof (result.group) != "undefined") {
                                    setFieldValue(result.data, true, false);
                                }
                                if (typeof (result.group) == "undefined" && typeof (result.local) == "undefined") {
                                    jQuery(".btn-save-data").val("Create");
                                    jQuery(".txt-operation").val("Create");
                                    jQuery(".txt-type").val("");
                                }
                            }
                        }
                    } else {
                        Logout();
                    }

                }

            });
        }
    }
    function setFieldValue(data, group, local) {
        if (data != "" && local) {
            jQuery("#Sah_cus_name").val(data.Mbe_name);
            jQuery("#Sah_cus_add1").val(data.Mbe_add1);
            jQuery("#Sah_cus_add2").val(data.Mbe_add2);
            type = "local";
            jQuery(".txt-type").val("local");

        }
        if (data != "" && group) {
            jQuery("#Sah_cus_name").val(data.Mbe_name);
            jQuery("#Sah_cus_add1").val(data.Mbe_add1);
            jQuery("#Sah_cus_add2").val(data.Mbe_add2);
            type = "group";
            jQuery(".txt-type").val("group");
        }
    }
    function updateCurrencyAmount(amount, customerCode, invNo) {
        globeCustomer = customerCode;
        globeInvNo = invNo;
        jQuery("#Sird_settle_amt").val(amount);
        jQuery(".bal-amount-val").empty();
        jQuery(".bal-amount-val").append(amount);
        jQuery(".tot-amount-val").empty();
        jQuery(".tot-amount-val").append(amount);
    }

    jQuery(".btn-cn-Add-data").click(function (evt) {
        evt.preventDefault();
        if ($('input[name=chkdebitnote]:checked').val() != "on")
        {
            Lobibox.confirm({
                msg: "Do you want to save credit note ?",
                callback: function ($this, type, ev) {
                    var invNo = jQuery("#Sah_inv_no").val();
                    if (type == "yes") {
                        var iscreditnote = 0;
                        if ($('input[name=chkdebitnote]:checked').val() != "on")
                        {
                            iscreditnote = 1;
                        }
                        var formdata = jQuery("#inv-frm").serialize();
                        jQuery.ajax({
                            type: "GET",
                            url: "/CreditNote/SaveCreditNote?cnno=" + invNo + "&iscreditnote=" + iscreditnote,
                            data: formdata,
                            contentType: "application/json;charset=utf-8",
                            dataType: "json",
                            success: function (result) {
                                if (result.login == true) {
                                    if (result.success == true) {
                                        setSuccesssMsg(result.msg);
                                        PrintCreditNote(result.no);
                                        clearall();
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
        } else {
            //debit note
            Lobibox.confirm({
                msg: "Do you want to save debit note ?",
                callback: function ($this, type, ev) {
                    var invNo = jQuery("#Sah_inv_no").val();
                    if (type == "yes") {
                        var iscreditnote = 0;
                        if ($('input[name=chkdebitnote]:checked').val() == "on") {
                            iscreditnote = 0;
                        }
                        var formdata = jQuery("#inv-frm").serialize();
                        jQuery.ajax({
                            type: 'GET',
                            url: "/CreditNote/DebitNoteSave?dbno= " + invNo,
                            data:  formdata,
                            contentType: "application/json;charset=utf-8",
                            dataType: "json",
                            success: function (result) {
                                if (result.login == true) {
                                    if (result.success == true) {
                                        setSuccesssMsg(result.msg);
                                        clearall();
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
        }
    });
    function clearall()
    {
        jQuery("#Sah_inv_no").val("");
        jQuery("#Sah_cus_cd").val("");
        jQuery("#Sah_cus_name").val("");
        jQuery("#Sah_cus_add1").val("");
        jQuery("#Sah_cus_add2").val("");
        jQuery("#Sah_ref_doc").val("");
        jQuery("#Sah_remarks").val("");
        jQuery("#totalCost").val("");
        
        jQuery('.cost-sheet-table .new-row').remove();
       // jQuery('.payment-table .new-row').remove();
       
        clearPaymentValues();
        jQuery.ajax({
            type: "GET",
            url: '/CreditNote/ClearCreditNote',
            data: {},
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            success: function (result) {
                if (result.login == true)
                {
                   
                } else {
                    Logout();
                }
            }
        });
    }
});